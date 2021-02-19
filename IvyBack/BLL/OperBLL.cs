using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using IvyBack.Helper;
using Model;
using System.Data;
namespace IvyBack.BLL
{
    public class OperBLL : IOper
    {
        public bool Login(Model.sa_t_operator_i oper)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", oper.oper_id);
            r.Write("pwd", oper.oper_pw);
            r.Write("id", "");
            //用erp接口需要用
            //    r.request("/oper?t=Login");
            r.request("/oper?t=Login");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            oper.oper_name = r.Read("oper_name");
            oper.oper_type = r.Read("oper_type");

            AppSetting.oper_types = r.GetDicOfTable<string, sys_t_oper_type>("type_id");
            AppSetting.oper_grants = r.GetDicOfTable<string, sa_t_oper_grant>("func_id", "data2");

            return true;
        }
        public bool Login(Model.sa_t_operator_i oper,out string mc_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", oper.oper_id);
            r.Write("pwd", oper.oper_pw);

            r.request("/oper?t=login");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            oper.oper_name = r.Read("oper_name");
            oper.oper_type = r.Read("oper_type");
            mc_id = r.Read("mc_id");

            AppSetting.oper_types = r.GetDicOfTable<string, sys_t_oper_type>("type_id");
            AppSetting.oper_grants = r.GetDicOfTable<string, sa_t_oper_grant>("func_id", "data2");

            return true;
        }

        public bool UploadPw(string oper_id, string old_pwd, string new_pwd)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", oper_id);
            r.Write("old_pwd", old_pwd);
            r.Write("new_pwd", new_pwd);

            r.request("/oper?t=change_pwd");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            return true;
        }

        public void Add(sa_t_operator_i oper)
        {
            JsonRequest r = new JsonRequest();

            r.Write<sa_t_operator_i>(oper);

            r.request("/oper?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public string GetMaxCode()
        {
            JsonRequest r = new JsonRequest();

            r.request("/oper?t=GetMaxCode");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            string code = r.Read("code");
            return code;
        }

        public System.Data.DataTable GetOperType()
        {
            JsonRequest r = new JsonRequest();

            r.request("/oper?t=GetOperType");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            var tb = r.GetDataTable();
            return tb;
        }

        public List<sa_t_oper_type> GetOperTypeList()
        {
            JsonRequest r = new JsonRequest();

            r.request("/oper?t=GetOperType");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            List<sa_t_oper_type> list = r.GetList<sa_t_oper_type>();
            return list;
        }

        public void AddOperType(sa_t_oper_type type)
        {
            JsonRequest r = new JsonRequest();

            r.Write("type", type);

            r.request("/oper?t=AddOperType");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        public void ChangeOperType(sa_t_oper_type type)
        {
            JsonRequest r = new JsonRequest();

            r.Write("type", type);

            r.request("/oper?t=ChangeOperType");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void DelOperType(sa_t_oper_type type)
        {
            JsonRequest r = new JsonRequest();

            r.Write("type", type);

            r.request("/oper?t=DelOperType");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public System.Data.DataTable GetOpers()
        {
            JsonRequest r = new JsonRequest();

            r.request("/oper?t=GetOpers");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }


        public void Upload(sa_t_operator_i oper)
        {
            JsonRequest r = new JsonRequest();

            r.Write<sa_t_operator_i>(oper);

            r.request("/oper?t=Upload");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Del(string oper_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", oper_id);

            r.request("/oper?t=Del");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }


        public void ResetPWD(string oper_id, string new_pwd)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", oper_id);
            r.Write("new_pwd", new_pwd);

            r.request("/oper?t=reset_pwd");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());


        }

        public bool Loginbyarap(sa_t_operator_i oper)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", oper.oper_id);
            r.Write("pwd", oper.oper_pw);
            //用erp接口需要用
            //    r.request("/oper?t=Login");
            r.request("/oper?t=Loginbyarap");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            oper.oper_name = r.Read("oper_name");
            oper.oper_type = r.Read("oper_type");

            AppSetting.oper_types = r.GetDicOfTable<string, sys_t_oper_type>("type_id");
            AppSetting.oper_grants = r.GetDicOfTable<string, sa_t_oper_grant>("func_id", "data2");

            return true;
        }
    }
}
