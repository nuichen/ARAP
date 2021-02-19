using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using Oper = IvyTran.BLL.ERP.Oper;

namespace IvyTran.svr.ERP
{
    public class oper : BaseService
    {

        IOper bll = new Oper();
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

        public void Login(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "oper_id", "pwd") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            string oper_id = w.Read("oper_id");
            string pwd = w.Read("pwd");
            string oper_name;
            string oper_type;
            bll.Login(oper_id, pwd, out oper_name, out oper_type);

            w.Write("oper_name", oper_name);
            w.Write("oper_type", oper_type);
            w.Write("mc_id", "0");
            w.Write("can_see_price", "1");

            IMyDestop desbll = new MyDestop();
            var tbs = desbll.GetAll();
            w.Write("data", tbs);

            //我的权限
            ISys sysbll = new Sys();
            tbs = sysbll.GetAllGrant(new global::Model.sa_t_oper_grant() { oper_id = oper_type });
            w.Write("data2", tbs);

        }
        public void change_pwd(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            string old_pwd = w.Read("old_pwd");
            string new_pwd = w.Read("new_pwd");
            bll.ChangePWD(oper_id, old_pwd, new_pwd);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            Model.sa_t_operator_i oper = w.GetObject<Model.sa_t_operator_i>();
            bll.Add(oper);
        }
        public void GetMaxCode(WebHelper w, Dictionary<string, object> kv)
        {
            string code = bll.GetMaxCode();
            w.Write("code", code);
        }
        public void GetOperType(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetOperType();

            w.Write(tb);
        }
        public void GetOpers(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetOpers();

            w.Write(tb);
        }
        public void Upload(WebHelper w, Dictionary<string, object> kv)
        {
            var oper = w.GetObject<Model.sa_t_operator_i>();
            bll.Upload(oper);
        }
        public void Del(WebHelper w, Dictionary<string, object> kv)
        {
            var oper = w.GetObject<Model.sa_t_operator_i>();
            bll.Del(oper.oper_id);
        }
        public void reset_pwd(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            string new_pwd = w.Read("new_pwd");
            bll.ResetPWD(oper_id, new_pwd);
        }
        public void AddOperType(WebHelper w, Dictionary<string, object> kv)
        {
            sa_t_oper_type type = w.GetObject<sa_t_oper_type>("type");
            bll.AddOperType(type);
        }
        public void ChangeOperType(WebHelper w, Dictionary<string, object> kv)
        {
            sa_t_oper_type type = w.GetObject<sa_t_oper_type>("type");
            bll.ChangeOperType(type);
        }
        public void DelOperType(WebHelper w, Dictionary<string, object> kv)
        {
            sa_t_oper_type type = w.GetObject<sa_t_oper_type>("type");
            bll.DelOperType(type);
        }



    }
}