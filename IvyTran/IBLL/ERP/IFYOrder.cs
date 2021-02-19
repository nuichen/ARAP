using System;
using System.Collections.Generic;

namespace IvyTran.IBLL.ERP
{
    interface IFYOrder
    {
        System.Data.DataTable GetList(DateTime date1, DateTime date2, string visa_id);
        void GetOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        string MaxCode();
        void Add(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines, out string sheet_no);
        void Change(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines);
        void Delete(string sheet_no);
        void Check(string sheet_no, string approve_man);
    }
}
