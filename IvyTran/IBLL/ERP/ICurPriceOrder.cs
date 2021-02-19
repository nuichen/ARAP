using System;
using System.Collections.Generic;

namespace IvyTran.IBLL.ERP
{
    interface  ICusPriceOrder
    {
        System.Data.DataTable GetList(DateTime date1, DateTime date2);
        void GetOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        string MaxCode();
        void Add(Model.pm_t_flow_main ord, List<Model.pm_t_price_flow_detial> lines,out string sheet_no);
        void Change(Model.pm_t_flow_main ord, List<Model.pm_t_price_flow_detial> lines);
        void Delete(string sheet_no);
        void Check(string sheet_no, string approve_man);
    }
}
