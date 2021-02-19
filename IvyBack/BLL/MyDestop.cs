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
    public class MyDestop : IMyDestop
    {
        System.Data.DataTable IMyDestop.GetAll()
        {
            JsonRequest r = new JsonRequest();

            r.request("/mydestop?t=GetAll");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }
        Dictionary<string, Model.sys_t_oper_type> IMyDestop.GetDic()
        {
            JsonRequest r = new JsonRequest();

            r.request("/mydestop?t=GetAll");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            Dictionary<string, sys_t_oper_type> dic = r.GetDicOfTable<string, sys_t_oper_type>("type_id");

            return dic;
        }
        public List<sys_t_oper_type> GetOperTypeList()
        {
            JsonRequest r = new JsonRequest();

            r.request("/mydestop?t=GetAll");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            List<sys_t_oper_type> lis = r.GetList<sys_t_oper_type>();

            return lis;
        }

        List<sys_t_oper_mylove> IMyDestop.GetMyLove(string oper_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", oper_id);

            r.request("/mydestop?t=GetMyLove");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            List<sys_t_oper_mylove> lis = r.GetList<sys_t_oper_mylove>();

            return lis;
        }
        void IMyDestop.AddMyLove(sys_t_oper_mylove mylove)
        {
            JsonRequest r = new JsonRequest();

            r.Write<sys_t_oper_mylove>("mylove", mylove);

            r.request("/mydestop?t=AddMyLove");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        void IMyDestop.deleteMyLove(sys_t_oper_mylove mylove)
        {
            JsonRequest r = new JsonRequest();

            r.Write<sys_t_oper_mylove>("mylove", mylove);

            r.request("/mydestop?t=deleteMyLove");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }



    }
}
