using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class supcust : BaseService
    {
        ISup supbll = new Sup();
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

        public void get_sup_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = supbll.GetList();
            w.Write("data", tb);
        }

    }
}