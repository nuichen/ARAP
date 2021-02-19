using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.IBLL
{
    interface ICashOrder
    {

        System.Data.DataTable GetList(DateTime date1, DateTime date2, string cus_no);
        void GetOrder(string sheet_no, out System.Data.DataTable tb1);
        string MaxCode();
        void Add(Model.bank_t_cash_master ord, out string sheet_no);
        void Change(Model.bank_t_cash_master ord);
        void Delete(string sheet_no);
        void Check(string sheet_no, string approve_man);
        void NotCheck(string sheet_no, string approve_man);

    }
}
