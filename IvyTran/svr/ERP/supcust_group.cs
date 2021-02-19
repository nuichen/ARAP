using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using Model.BaseModel;

namespace IvyTran.svr.ERP
{
    public class supcust_group : BaseService
    {
        ISupcustGroup bll = new SupcustGroupBLL();
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
            var tb = bll.GetAll();
            w.Write(tb);
        }

        public void GetSupGroup(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetSupGroup();
            w.Write(tb);
        }
        public void GetCusGroup(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetCusGroup();
            w.Write(tb);
        }
        public void SaveGroup(WebHelper w, Dictionary<string, object> kv)
        {
            List<bi_t_supcust_group> lis = w.GetList<bi_t_supcust_group>("lis");
            bll.SaveGroup(lis);
        }
        public void GetAllCls(WebHelper w, Dictionary<string, object> kv)
        {
            string status = w.Read("status");
            var tb = bll.GetAllCls(status);
            w.Write(tb);
        }
        public void SaveCls(WebHelper w, Dictionary<string, object> kv)
        {
            List<bi_t_company_type> lis = w.GetList<bi_t_company_type>("lis");
            bll.SaveCls(lis);
        }
    }
}