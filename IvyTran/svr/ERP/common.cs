using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class common : BaseService
    {
        ICommon bll = new Common();
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

        public void connect_server(WebHelper w, Dictionary<string, object> kv)
        {
            w.Write("is_connect", "1");
        }
        public void get_item_count(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count = bll.GetItemCount(sysn_time);

            w.Write("total_count", count);
        }
       
        public void get_item_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetItemList(sysn_time);

            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }
        public void get_itemcls_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetItemClsList(sysn_time);

            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }
        public void get_branch_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetBranchList(sysn_time);

            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }
        public void get_stock_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time", "branch_no") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            string branch_no = w.Read("branch_no");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetBranchStockList(sysn_time, branch_no);

            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }
        public void get_oper_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time", "branch_no") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            string branch_no = w.Read("branch_no");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetOperList(sysn_time, branch_no);
            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }
        public void get_sup_count(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count = bll.GetSupCusCount(sysn_time);
            w.Write("total_count", count.ToString());
        }
        public void get_sup_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetSupCusList(sysn_time);
            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }

        public void get_cus_price_count(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time", "cust_id", "item_no") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            var cust_id = w.Read("cust_id");
            var item_no = w.Read("item_no");
            int count = bll.GetCusPriceCount(sysn_time, cust_id, item_no);

            w.Write("total_count", count.ToString());
        }
        public void get_cus_price_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time", "cust_id", "item_no") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            string cust_id = w.Read("cust_id");
            string item_no = w.Read("item_no");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetCusPriceList(sysn_time, cust_id, item_no);
            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }

        public void get_sup_price_count(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time", "sup_id", "item_no") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            string sup_id = w.Read("sup_id");
            string item_no = w.Read("item_no");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int count = bll.GetSupPriceCount(sysn_time, sup_id, item_no);
            w.Write("total_count", count.ToString());
        }

        public void get_sup_price_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time", "sup_id", "item_no") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            string sup_id = w.Read("sup_id");
            string item_no = w.Read("item_no");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetSupPriceList(sysn_time, sup_id, item_no);
            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }
        public void get_system_pars(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sysn_time") == false)
            {
                throw new Exception("参数错误");
            }
            string sysn_time = w.Read("sysn_time");
            var dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetSystemPars(sysn_time);

            w.Write("sysn_time", dtime);
            w.Write("datas", dt);
        }
        public void get_payway_list(WebHelper w, Dictionary<string, object> kv)
        {
            var dt = bll.GetPayWayList();
            w.Write("datas", dt);
        }
        public void update_pwd(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "branch_no", "oper_id", "pwd", "new_pwd") == false)
            {
                throw new Exception("参数错误");
            }
            string branch_no = w.Read("branch_no");
            string oper_id = w.Read("oper_id");
            string pwd = w.Read("pwd").ToLower();//加密
            string new_pwd = w.Read("new_pwd").ToLower();//加密
            bll.UpdatePwd(branch_no, oper_id, pwd, new_pwd);
        }
        public void get_cus_item_price(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cust_id", "item_no") == false)
            {
                throw new Exception("参数错误");
            }
            string cust_id = w.Read("cust_id");
            string item_no = w.Read("item_no");
            var price = bll.GetCusItemPrice(cust_id, item_no);
            w.Write("price", price.ToString());
        }
        public void get_cus_balance(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "cust_id") == false)
            {
                throw new Exception("参数错误");
            }
            string cust_id = w.Read("cust_id");
            decimal balance = 0;
            decimal credit_amt = 0;
            bll.GetBalance(cust_id, out balance, out credit_amt);

            w.Write("balance", balance.ToString());
            w.Write("credit_amt", credit_amt.ToString());
        }

        //收货称
        public void add_jh(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "jh", "softpos", "comp_name", "other", "ip", "memo") == false)
            {
                throw new Exception("参数错误");
            }
            Model.netsetup ns = new Model.netsetup();
            ns.jh = w.Read("jh");
            ns.softpos = ObjectToString(kv, "softpos");
            ns.comp_name = ObjectToString(kv, "comp_name");
            ns.other = ObjectToString(kv, "other");
            ns.ip = ObjectToString(kv, "ip");
            ns.memo = ObjectToString(kv, "memo");
            int err_id = 0;
            string msg = "";
            var jh = "";
            bll.AddJH(ns, out jh, out err_id, out msg);
            w.Write("jh", jh);
        }
        public void del_jh(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "jh", "softpos", "ip") == false)
            {
                throw new Exception("参数错误");
            }
            Model.netsetup ns = new Model.netsetup();
            ns.jh = ("jh");
            ns.softpos = w.Read("softpos");
            ns.ip = w.Read("ip");
            bll.DelJH(ns);
        }
        public void get_jh(WebHelper w, Dictionary<string, object> kv)
        {
            var dt = bll.GetJHList();
            w.Write("datas", dt);
        }
        public void get_mac_jh(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "mac_id") == false)
            {
                throw new Exception("参数错误");
            }
            var mac_id = w.Read("mac_id");
            var jh = bll.GetMacJH(mac_id);
            w.Write("jh", jh);
        }


       
  
                   
         

       
    }
}