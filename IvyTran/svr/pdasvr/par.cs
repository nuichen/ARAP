
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IvyTran.BLL;
using IvyTran.BLL.ERP;
using IvyTran.IBLL;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.pdasvr
{
    public class par : BaseService
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
        ISys bll = new Sys();

        public void read(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "par_id") == false)
            {
                throw new Exception("par err");
            }
            string par_id = ObjectToString(kv, "par_id");

            var val = bll.Read(par_id);

            w.Write("val", val);
        }

        public void write(WebHelper w, Dictionary<string, object> kv)
        { if (ExistsKeys(kv, "par_id", "val") == false)
            {
                throw new Exception("par err");
            }
            string par_id = ObjectToString(kv, "par_id");
            string val = ObjectToString(kv, "val");

            bll.Write(par_id, val);

        }
    }
}