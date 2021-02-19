using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class price : BaseService
    {

        IPriceBLL bll = new PriceBLL();
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

        public void get_cust_price_list(WebHelper w, Dictionary<string, object> kv)
        {
            string cust_id = w.Read("cust_id");
            var tb = bll.GetCustPriceList(cust_id);
            w.Write(tb);
        }
        public void get_sup_price_list(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_id = w.Read("sup_id");
            var tb = bll.GetSupPriceList(sup_id);
            w.Write("data", tb);
        }
        public void get_cust_item_price(WebHelper w, Dictionary<string, object> kv)
        {
            string cust_id = w.Read("cust_id");
            string item_no = w.Read("item_no");
            string type = w.Read("type");
            decimal price = bll.GetCusItemPrice(cust_id, item_no, type);
            w.Write("price", price.ToString());
        }
        public void get_sup_item_price(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_id = w.Read("sup_id");
            string item_no = w.Read("item_no");
            string type = w.Read("type");
            decimal price = bll.GetSupItemPrice(sup_id, item_no, type);
            w.Write("price", price.ToString());
        }
        public void get_last_in_price(WebHelper w, Dictionary<string, object> kv)
        {
            string item_no = w.Read("item_no");
            decimal price = bll.GetLastInPrice(item_no);
            w.Write("price", price.ToString());
        }

    }
}