using System;
using System.Collections.Generic;

namespace IvyTran.IBLL.ERP
{
    interface ISupFY
    {
        System.Data.DataTable GetList(DateTime date1, DateTime date2, string sup_no);
        void GetOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        string MaxCode();
        void Add(Model.rp_t_supcust_fy_master ord, List<Model.rp_t_supcust_fy_detail> lines, out string sheet_no);
        void Change(Model.rp_t_supcust_fy_master ord, List<Model.rp_t_supcust_fy_detail> lines);
        void Delete(string sheet_no);
        void Check(string sheet_no, string approve_man);
        void NotCheck(string sheet_no, string approve_man);
    }
}
