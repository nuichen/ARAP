using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.body.Inventory;

namespace IvyTran.IBLL.Inventory
{
    public interface ICheckDetail
    {
        DataTable GetList(DateTime date1, DateTime date2, string sheet_no, string counter_no, string jh, string oper);
        bool Insert(List<pda_ot_t_check_detail> lst);
        decimal GetQtySum(string item_no);
        void Clear();
    }
}