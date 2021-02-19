using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class bank : BaseService
    {
        IBank bll = new Bank();
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
            w.Write(tb);
        }
        public void GetSubjectList(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetSubjectList();
            w.Write(tb);
        }
        
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string visa_id = w.Read("visa_id");
            var tb = bll.GetItem(visa_id);
            w.Write(tb);
        }

        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_bank_info item = w.GetObject<Model.bi_t_bank_info>();
            bll.Add(item);
        }

        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_bank_info item = w.GetObject<Model.bi_t_bank_info>();
            bll.Change(item);
        }

        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string visa_id = w.Read("visa_id");
            bll.Delete(visa_id);
        }

    }
}