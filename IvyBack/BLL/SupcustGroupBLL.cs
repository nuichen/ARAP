using IvyBack.Helper;
using IvyBack.IBLL;
using Model;
using Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IvyBack.BLL
{
    public class SupcustGroupBLL : ISupcustGroup
    {
        public DataTable GetALl()
        {
            JsonRequest r = new JsonRequest();

            r.request("/supcust_group?t=GetAll");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public DataTable GetCusGroup()
        {
            JsonRequest r = new JsonRequest();

            r.request("/supcust_group?t=GetCusGroup");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public DataTable GetSupGroup()
        {
            JsonRequest r = new JsonRequest();

            r.request("/supcust_group?t=GetSupGroup");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public void SaveGroup(List<bi_t_supcust_group> lis)
        {
            JsonRequest r = new JsonRequest();

            r.Write("lis", lis);

            r.request("/supcust_group?t=SaveGroup");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }
        public DataTable GetAllCls(string status)
        {
            JsonRequest r = new JsonRequest();
            r.Write("status", status);

            r.request("/supcust_group?t=GetAllCls");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public void SaveCls(List<bi_t_company_type> lis)
        {
            JsonRequest r = new JsonRequest();

            r.Write("lis", lis);

            r.request("/supcust_group?t=SaveCls");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
    }
}
