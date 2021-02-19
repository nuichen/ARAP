
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IvyTran.BLL.Inventory;
using IvyTran.IBLL.Inventory;

namespace IvyTran.svr.pdasvr
{
    public class oper : BaseService
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
        IOper bll = new Oper();

        public void login(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "oper_id", "pwd") == false)
            {
                throw new Exception("par err");
            }

            bll.Init();

            string oper_id = ObjectToString(kv, "oper_id");
            string pwd = ObjectToString(kv, "pwd");
            if (!bll.Login(oper_id, pwd))
            {
                throw new Exception("用户名或密码不正确");
            }
        }
        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetList();

            w.Write(tb);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "oper_id", "oper_name", "pwd", "oper_type", "status") == false)
            {
                throw new Exception("par err");
            }
            string oper_id = ObjectToString(kv, "oper_id");
            string oper_name = ObjectToString(kv, "oper_name");
            string pwd = ObjectToString(kv, "pwd");
            string oper_type = ObjectToString(kv, "oper_type");
            string status = ObjectToString(kv, "status");
            if (!bll.Add(oper_id, oper_name, pwd, oper_type, status))
            {
                throw new Exception("添加操作员失败");
            }
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "oper_id", "oper_name", "oper_type", "status") == false)
            {
                throw new Exception("par err");
            }
            string oper_id = ObjectToString(kv, "oper_id");
            string oper_name = ObjectToString(kv, "oper_name");
            string oper_type = ObjectToString(kv, "oper_type");
            string status = ObjectToString(kv, "status");
            if (bll.Change(oper_id, oper_name, oper_type, status))
            {
                throw new Exception("修改操作员失败");
            }
        }
        public void change_pwd(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "oper_id", "pwd") == false)
            {
                throw new Exception("par err");
            }
            string oper_id = ObjectToString(kv, "oper_id");
            string pwd = ObjectToString(kv, "pwd");

            if (bll.ChangePWD(oper_id, pwd))
            {
                throw new Exception("修改操作员密码失败");
            }
        }
        public void get_one(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "oper_id") == false)
            {
                throw new Exception("par err");
            }
            string oper_id = ObjectToString(kv, "oper_id");
            string oper_name;
            string oper_type;
            string status;
            if (bll.GetOne(oper_id, out oper_name, out oper_type, out status) == true)
            {
                w.Write("oper_id", oper_id);
                w.Write("oper_name", oper_name);
                w.Write("oper_type", oper_type);
                w.Write("status", status);
            }
            else
            {
                throw new Exception("读不到操作员失败");
            }
        }
        public void get_server_time(WebHelper w, Dictionary<string, object> kv)
        {
            w.Write("server_time", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}