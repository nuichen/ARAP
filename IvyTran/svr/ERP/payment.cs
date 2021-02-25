using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class payment:BaseService
    {
        IPayment bll = new Payment();
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

        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetList();
            w.Write("data", tb);
        }
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string pay_way = w.Read("pay_way");
            var tb = bll.GetItem(pay_way);
            w.Write("data", tb);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_payment_info item = w.GetObject<Model.bi_t_payment_info>();
            bll.Add(item);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_payment_info item = w.GetObject<Model.bi_t_payment_info>();
            bll.Change(item);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string pay_way = w.Read("pay_way");
            bll.Delete(pay_way);
        }
        public void Getlist(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.Getlist();
            w.Write("data", tb);
        }

    }
}