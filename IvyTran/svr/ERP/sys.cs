using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class sys : BaseService
    {

        ISys bll = new Sys();
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

        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_system system = w.GetObject<sys_t_system>();
            bll.add(system);
        }
        public void addDic(WebHelper w, Dictionary<string, object> kv)
        {
            Dictionary<string, sys_t_system> dic = w.GetDic<sys_t_system>();
            bll.addDic(dic);
        }
        public void update(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_system system = w.GetObject<sys_t_system>();
            bll.update(system);
        }
        public void updateDic(WebHelper w, Dictionary<string, object> kv)
        {
            Dictionary<string, sys_t_system> dic = w.GetDic<sys_t_system>();
            bll.updatedic(dic);
        }
        public void GetAll(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetAll();
            w.Write(tb);
        }
        public void read(WebHelper w, Dictionary<string, object> kv)
        {
            string sys_var_id = w.Read("sys_var_id");
            string value = bll.Read(sys_var_id);
            w.Write("value", value);
        }
        public void GetSheetNo(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = bll.GetSheetNo();
            w.Write(tb);
        }
        public void AddJH(WebHelper w, Dictionary<string, object> kv)
        {
            netsetup ns = w.GetObject<netsetup>("ns");
            bll.AddJH(ns);
        }
        public void DelJH(WebHelper w, Dictionary<string, object> kv)
        {
            netsetup ns = w.GetObject<netsetup>("ns");
            bll.DelJH(ns);
        }
        public void GetJH(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = bll.GetJH();
            w.Write(tb);
        }
        public void GetAllGrant(WebHelper w, Dictionary<string, object> kv)
        {
            sa_t_oper_grant grant = w.GetObject<sa_t_oper_grant>("grant");
            DataTable tb = bll.GetAllGrant(grant);
            w.Write(tb);
        }
        public void SaveGrant(WebHelper w, Dictionary<string, object> kv)
        {
            List<sa_t_oper_grant> grant = w.GetList<sa_t_oper_grant>("grant");
            bll.SaveGrant(grant);
        }


    }
}