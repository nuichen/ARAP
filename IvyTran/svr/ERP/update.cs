using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class update : BaseService
    {

        IUpdate bll = new UpdateBLL();
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

        public void IsOk(WebHelper w, Dictionary<string, object> kv)
        {
            bool flag = bll.IsOk();
            w.WriteResult(flag);
        }
        public void AutoUpdate(WebHelper w, Dictionary<string, object> kv)
        {
            bll.AutoUpdate();
        }
        public void Update(WebHelper w, Dictionary<string, object> kv)
        {
            bll.Update();
        }
        public void GetServerVer(WebHelper w, Dictionary<string, object> kv)
        {
            string ver = bll.GetServerVer();
            w.WriteResult(ver);
        }


    }
}