using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface ISaleOrderBLL
    {
        string GetNewSheetNo();
        string GetRowId();
        DataTable GetBranchList();
        DataTable GetSupList();
        DataTable GetOperList();
        DataTable GetItemList(string item_name, string barcode);
        void InsertOrderMainAndChilds(co_t_order_main main, List<co_t_order_child> childs);
        DataTable GetOrderMainList(DateTime dtStart, DateTime dtEnd);
        DataTable GetOrderChildList(string sheet_no);
    }
}