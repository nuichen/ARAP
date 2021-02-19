
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IvyTran.BLL.OnLine;
using IvyTran.IBLL.OnLine;

namespace IvyTran.svr.OnLine
{
    public class customer : BaseService
    {

        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }
        ICustomer bll = new Customer();


        public void get_cus_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "date1", "date2", "status", "salesman_id", "keyword", "page_no", "page_size") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var date1 = ObjectToString(kv, "date1");
            var date2 = ObjectToString(kv, "date2");
            var status = ObjectToString(kv, "status");
            var salesman_id = ObjectToString(kv, "salesman_id");
            var keyword = ObjectToString(kv, "keyword");
            var page_no = ObjectToInt(kv, "page_no");
            var page_size = ObjectToInt(kv, "page_size");
            int total = 0;
            var dt = bll.GetCusList(date1, date2, status, salesman_id, keyword, page_no, page_size, out total);
            //
            w.Write("total", total.ToString());
            w.Write("datas", dt);
        }
        public void get_group_list(WebHelper w, Dictionary<string, object> kv)
        {
            var dt = bll.GetCusGroupList();
            //
            w.Write("errId", "0");
            w.Write("errMsg", "");
            w.Write("datas", dt);
        }
        public void approve(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cus_no", "status", "oper_id", "cus_level", "settle_type", "remark") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var cus_no = ObjectToString(kv, "cus_no");
            var status = ObjectToString(kv, "status");
            var oper_id = ObjectToString(kv, "oper_id");
            var cus_level = ObjectToString(kv, "cus_level");
            var settle_type = ObjectToString(kv, "settle_type");
            var remark = ObjectToString(kv, "remark");
            bll.Approve(cus_no, status, oper_id, cus_level, settle_type, remark);
        }
        public void get_customer(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cus_no") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var cus_no = ObjectToString(kv, "cus_no");
            var cus = bll.GetCusInfo(cus_no);
            //
            w.Write("cus_no", cus.cus_no);
            w.Write("login_no", cus.login_no);
            w.Write("cus_level", cus.cus_level);
            w.Write("cus_start_date", cus.cus_start_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Write("cus_end_date", cus.cus_end_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Write("cus_name", cus.cus_name);
            w.Write("cus_tel", cus.cus_tel);
            w.Write("mobile", cus.mobile);
            w.Write("cus_idcard", cus.cus_idcard);
            w.Write("cus_area", cus.cus_area);
            w.Write("contact_address", cus.contact_address);
            w.Write("detail_address", cus.detail_address);
            w.Write("remark", cus.remark);
            w.Write("settle_type", cus.settle_type);
            w.Write("salesman_id", cus.salesman_id);
            w.Write("img_url", cus.img_url);
            w.Write("status", cus.status);
            w.Write("is_branch", cus.is_branch);
        }
        public void get_new_msg(WebHelper w, Dictionary<string, object> kv)
        {
            var dt = bll.GetNewMsg();
            w.Write("datas", dt);
        }
        public void get_first_new_cus(WebHelper w, Dictionary<string, object> kv)
        {
            var cus = new Model.customer();
            var un_read_num = 0;
            bll.GetFirstNewCus(out cus, out un_read_num);
            //
            if (cus == null || un_read_num == 0)
            {
                throw new ExceptionBase(-8, "不存在未阅读订单");
            }
            //
            w.Write("un_read_num", un_read_num.ToString());
            w.Write("cus_no", cus.cus_no);
            w.Write("login_no", cus.login_no);
            w.Write("cus_level", cus.cus_level);
            w.Write("cus_start_date", cus.cus_start_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Write("cus_end_date", cus.cus_end_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Write("cus_name", cus.cus_name);
            w.Write("cus_tel", cus.cus_tel);
            w.Write("mobile", cus.mobile);
            w.Write("cus_idcard", cus.cus_idcard);
            w.Write("cus_area", cus.cus_area);
            w.Write("contact_address", cus.contact_address);
            w.Write("detail_address", cus.detail_address);
            w.Write("remark", cus.remark);
            w.Write("settle_type", cus.settle_type);
            w.Write("salesman_id", cus.salesman_id);
            w.Write("img_url", cus.img_url);
            w.Write("status", cus.status);
            w.Write("is_branch", cus.is_branch);
        }
        public void sign_read(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cus_no") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var cus_no = ObjectToString(kv, "cus_no");
            //
            bll.SignRead(cus_no);
        }
        public void delete_customer(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cus_no") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var cus_no = ObjectToString(kv, "cus_no");
            //
            bll.Delete(cus_no);
        }
        public void get_salesman_list(WebHelper w, Dictionary<string, object> kv)
        {
            var dt = bll.GetSalesmanList();
            //
            w.Write("datas", dt);
        }
        public void update_customer(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cus_no", "login_no", "cus_name", "cus_tel", "mobile", "cus_idcard", "cus_area", "contact_addr", "detail_addr",
                    "cus_level", "remark", "settle_type", "salesman_id", "cus_start_date", "cus_end_date", "is_branch") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var cus_no = ObjectToString(kv, "cus_no");
            var login_no = ObjectToString(kv, "login_no");
            var cus_name = ObjectToString(kv, "cus_name");
            var mobile = ObjectToString(kv, "mobile");
            var cus_idcard = ObjectToString(kv, "cus_idcard");
            var cus_tel = ObjectToString(kv, "cus_tel");
            var cus_area = ObjectToString(kv, "cus_area");
            var contact_addr = ObjectToString(kv, "contact_addr");
            var detail_addr = ObjectToString(kv, "detail_addr");
            var cus_level = ObjectToString(kv, "cus_level");
            var remark = ObjectToString(kv, "remark");
            var settle_type = ObjectToString(kv, "settle_type");
            var salesman_id = ObjectToString(kv, "salesman_id");
            var cus_start_date = ObjectToString(kv, "cus_start_date");
            var cus_end_date = ObjectToString(kv, "cus_end_date");
            var is_branch = ObjectToString(kv, "is_branch");
            bll.UpdateCustomer(cus_no, login_no, cus_name, cus_tel, mobile, cus_idcard, cus_area, contact_addr, detail_addr,
                cus_level, remark, settle_type, salesman_id, cus_start_date, cus_end_date, is_branch);
        }
        public void add_customer(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cus_pwd", "login_no", "cus_name", "cus_tel", "mobile", "cus_idcard", "cus_area", "contact_addr", "detail_addr",
                    "cus_level", "remark", "settle_type", "salesman_id", "cus_start_date", "cus_end_date", "is_branch") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var cus_pwd = ObjectToString(kv, "cus_pwd");
            var login_no = ObjectToString(kv, "login_no");
            var cus_name = ObjectToString(kv, "cus_name");
            var mobile = ObjectToString(kv, "mobile");
            var cus_idcard = ObjectToString(kv, "cus_idcard");
            var cus_tel = ObjectToString(kv, "cus_tel");
            var cus_area = ObjectToString(kv, "cus_area");
            var contact_addr = ObjectToString(kv, "contact_addr");
            var detail_addr = ObjectToString(kv, "detail_addr");
            var cus_level = ObjectToString(kv, "cus_level");
            var remark = ObjectToString(kv, "remark");
            var settle_type = ObjectToString(kv, "settle_type");
            var salesman_id = ObjectToString(kv, "salesman_id");
            var cus_start_date = ObjectToString(kv, "cus_start_date");
            var cus_end_date = ObjectToString(kv, "cus_end_date");
            var is_branch = ObjectToString(kv, "is_branch");
            bll.AddCustomer(login_no, cus_pwd, cus_name, cus_tel, mobile, cus_idcard, cus_area, contact_addr, detail_addr,
                cus_level, remark, settle_type, salesman_id, cus_start_date, cus_end_date, is_branch);
        }
        public void select_salesman_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "keyword") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var keyword = ObjectToString(kv, "keyword");
            var dt = bll.GetSalesmanList(keyword);
            //
            w.Write("datas", dt);
        }
        public void get_balance(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cus_no") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var cus_no = ObjectToString(kv, "cus_no");
            decimal bal = 0;
            decimal credit_amt = 0;
            bll.GetCusBalance(cus_no, out bal, out credit_amt);
            //
            w.Write("balance", bal.ToString("F2"));
            w.Write("credit_amt", credit_amt.ToString("F2"));
        }
        public void search_cus_balance(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "keyword", "supcust_group") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var keyword = ObjectToString(kv, "keyword");
            var supcust_group = ObjectToString(kv, "supcust_group");
            var dt = bll.SearchCusBalanceList(keyword, supcust_group);
            //
            w.Write("datas", dt);
        }

    }
}