
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.svr.API
{
    public class common : BaseService
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


        public void GetItem(WebHelper w, Dictionary<string, object> kv)
        {

        }

    }
}