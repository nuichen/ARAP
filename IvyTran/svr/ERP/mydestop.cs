using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class mydestop : BaseService
    {

        IMyDestop bll = new MyDestop();
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

        public void GetAll(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = bll.GetAll();
            w.Write(tb);
        }
        public void GetMyLove(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            DataTable tb = bll.GetMyLove(oper_id);
            w.Write(tb);
        }
        public void AddMyLove(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_oper_mylove mylove = w.GetObject<sys_t_oper_mylove>("mylove");
            bll.AddMyLove(mylove);
        }
        public void deleteMyLove(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_oper_mylove mylove = w.GetObject<sys_t_oper_mylove>("mylove");
            bll.deleteMyLove(mylove);
        }

    }
}