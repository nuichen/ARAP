using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using IvyBack.Helper;
using System.Data;
using Model;

namespace IvyBack.BLL
{
    public class SysBLL : ISys
    {
        public void add(Model.sys_t_system sys)
        {
            JsonRequest r = new JsonRequest();

            r.Write<Model.sys_t_system>(sys);

            r.request("/sys?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void add(Dictionary<string, sys_t_system> sys_dic)
        {
            JsonRequest r = new JsonRequest();

            r.Write<sys_t_system>(sys_dic);

            r.request("/sys?t=addDic");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void update(Model.sys_t_system sys)
        {
            JsonRequest r = new JsonRequest();

            r.Write<Model.sys_t_system>(sys);

            r.request("/sys?t=update");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void updateDic(Dictionary<string, sys_t_system> sys_dic)
        {
            JsonRequest r = new JsonRequest();

            r.Write<sys_t_system>(sys_dic);

            r.request("/sys?t=updateDic");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public Dictionary<string, Model.sys_t_system> GetAllDic()
        {
            JsonRequest r = new JsonRequest();

            r.request("/sys?t=GetAll");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            Dictionary<string, Model.sys_t_system> dic = new Dictionary<string, Model.sys_t_system>();

            foreach (DataRow dr in tb.Rows)
            {
                var system = DB.ReflectionHelper.DataRowToModel<sys_t_system>(dr);

                dic.Add(system.sys_var_id, system);
            }

            return dic;
        }

        public string Read(string sys_var_id)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sys_var_id", sys_var_id);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/sys?t=read", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                return r.Read("value");
            }
        }

        public DataTable GetSheetNo()
        {
            JsonRequest r = new JsonRequest();

            r.request("/sys?t=GetSheetNo");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public void AddJH(netsetup ns)
        {
            JsonRequest r = new JsonRequest();

            r.Write("ns", ns);

            r.request("/sys?t=AddJH");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void DelJH(netsetup ns)
        {
            JsonRequest r = new JsonRequest();

            r.Write("ns", ns);

            r.request("/sys?t=DelJH");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public DataTable GetJH()
        {
            JsonRequest r = new JsonRequest();

            r.request("/sys?t=GetJH");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }


        public List<sa_t_oper_grant> GetAllGrant(sa_t_oper_grant grant)
        {
            JsonRequest r = new JsonRequest();

            r.Write("grant", grant);

            r.request("/sys?t=GetAllGrant");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            List<sa_t_oper_grant> lis = r.GetList<sa_t_oper_grant>();
            return lis;
        }

        public Dictionary<string, sa_t_oper_grant> GetAllGrantDic(sa_t_oper_grant grant)
        {
            JsonRequest r = new JsonRequest();

            r.Write("grant", grant);

            r.request("/sys?t=GetAllGrant");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            Dictionary<string, sa_t_oper_grant> dic = r.GetDicOfTable<string, sa_t_oper_grant>("func_id");

            return dic;
        }

        public void SaveGrant(List<sa_t_oper_grant> grant)
        {
            JsonRequest r = new JsonRequest();

            r.Write("grant", grant);

            r.request("/sys?t=SaveGrant");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

     
    }
}
