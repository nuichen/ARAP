
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.BLL.OnLine;
using IvyTran.IBLL.OnLine;

namespace IvyTran.svr.OnLine
{
    public class order : BaseService
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

        IOrder bll = new Order();
        IPager pager = new Pager();

        public void get_order_new(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "clear_key") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }

            string clear_key = w.Read("clear_key");
            pager.ClearData(clear_key);
            bll.GetOrderNew(out clear_key);

            w.Write("key", clear_key);
        }
        public void get_order_pass(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "clear_key", "date1", "date2", "keyword") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }

            var date1 = ObjectToString(kv, "date1");
            var date2 = ObjectToString(kv, "date2");
            var keyword = ObjectToString(kv, "keyword");
            var clear_key = ObjectToString(kv, "clear_key");
            pager.ClearData(clear_key);
            bll.GetOrderPass(date1, date2, keyword, out clear_key);
            w.Write("key", clear_key);
        }
        public void get_order_disable(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "date1", "date2", "keyword", "clear_key") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var date1 = ObjectToString(kv, "date1");
            var date2 = ObjectToString(kv, "date2");
            var keyword = ObjectToString(kv, "keyword");
            var clear_key = ObjectToString(kv, "clear_key");
            pager.ClearData(clear_key);
            bll.GetOrderDisable(date1, date2, keyword, out clear_key);

            w.Write("key", clear_key);
        }
        public void get_order_complete(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "date1", "date2", "keyword", "clear_key") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var date1 = ObjectToString(kv, "date1");
            var date2 = ObjectToString(kv, "date2");
            var keyword = ObjectToString(kv, "keyword");
            var only_show_no_receice = ObjectToString(kv, "only_show_no_receice");
            var clear_key = ObjectToString(kv, "clear_key");
            pager.ClearData(clear_key);
            bll.GetOrderComplete(date1, date2, keyword, only_show_no_receice, out clear_key);
            //
            w.Write("key", clear_key);
        }
        public void pass(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ord_id") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var ord_id = ObjectToString(kv, "ord_id");
            var oper_id = ObjectToString(kv, "oper_id");
            //
            bll.Pass(ord_id, oper_id);
        }
        public void preview(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ord_id") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var ord_id = ObjectToString(kv, "ord_id");
            //
            bll.Preview(ord_id);
        }
        public void disable(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ord_id") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var ord_id = ObjectToString(kv, "ord_id");
            //
            bll.Disable(ord_id);
        }
        public void disable_row(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ord_id", "row_index") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var ord_id = ObjectToString(kv, "ord_id");
            var row_index = ObjectToInt(kv, "row_index");
            //
            bll.DisableRow(ord_id, row_index);
        }
        public void send(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ord_id") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var ord_id = ObjectToString(kv, "ord_id");
            string oper_id = ObjectToString(kv, "oper_id");
            //
            bll.Send(ord_id, oper_id);
        }
        public void get_order(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ord_id") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var ord_id = ObjectToString(kv, "ord_id");
            //
            var od = new Model.tr_order();
            var dtLine = new DataTable();
            int un_read_num = 0;
            bll.GetOrder(ord_id, out od, out dtLine, out un_read_num);
            //
            w.Write("un_read_num", un_read_num.ToString());
            w.Write("ord_id", od.ord_id);
            w.Write("create_time", od.create_time.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Write("mobile", od.mobile);
            w.Write("sname", od.sname);
            w.Write("address", od.address);
            //w.Write("qty", od.qty.ToString());
            //w.Write("amount", od.amount.ToString());
            //w.Write("enable_qty", od.enable_qty.ToString());
            //w.Write("enable_amount", od.enable_amount.ToString());
            w.Write("status", od.status);
            w.Write("pay_type", od.pay_type);

            w.Write("reach_time", od.reach_time);
            w.Write("cus_remark", od.cus_remark);
            w.Write("cus_no", od.cus_no);
            w.Write("salesman_id", od.salesman_id);
            w.Write("discount_amt", od.discount_amt.ToString());
            w.Write("take_fee", od.take_fee.ToString());
            //
            w.Write("lines", dtLine);
        }
        public void get_new_msg(WebHelper w, Dictionary<string, object> kv)
        {
            var lst = bll.GetNewMsg();
            w.Write("errId", "0");
            w.Write("errMsg", "");
            var tb = new DataTable();
            tb.Columns.Add("ord_id");
            tb.Columns.Add("create_time");
            foreach (Model.tr_order ord in lst)
            {
                tb.Rows.Add(ord.ord_id, ord.create_time.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            w.Write("datas", tb);
        }
        public void get_first_new_order(WebHelper w, Dictionary<string, object> kv)
        {
            var od = new Model.tr_order();
            var dtLine = new DataTable();
            var un_read_num = 0;
            bll.GetFirstNewOrder(out od, out dtLine, out un_read_num);
            //
            if (od == null)
            {
                throw new ExceptionBase(-8, "不存在未阅读订单");
            }
            //
            w.Write("un_read_num", un_read_num.ToString());
            w.Write("ord_id", od.ord_id);
            w.Write("create_time", od.create_time.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Write("mobile", od.mobile);
            w.Write("sname", od.sname);
            w.Write("address", od.address);
            //w.Write("qty", od.qty.ToString());
            //w.Write("amount", od.amount.ToString());
            //w.Write("enable_qty", od.enable_qty.ToString());
            //w.Write("enable_amount", od.enable_amount.ToString());
            w.Write("status", od.status);
            w.Write("pay_type", od.pay_type);
            w.Write("reach_time", od.reach_time);
            w.Write("cus_remark", od.cus_remark);
            w.Write("cus_no", od.cus_no);
            w.Write("salesman_id", od.salesman_id);
            w.Write("discount_amt", od.discount_amt.ToString());
            w.Write("take_fee", od.take_fee.ToString());
            w.Write("lines", dtLine);
        }
        public void sign_read(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ord_id") == false)
            {
                throw new ExceptionBase(-4, "参数出错误");
            }
            var ord_id = ObjectToString(kv, "ord_id");
            //
            bll.SignRead(ord_id);
        }

    }
}