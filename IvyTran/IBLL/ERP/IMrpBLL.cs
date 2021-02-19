using System;
using System.Data;

namespace IvyTran.IBLL.ERP
{
    public interface IMrpBLL
    {
        DataTable SearchMaterialList(string ph_sheet_no, string cust_no, string keyword, string only_show_nosup);

        void BindItemSup(string ph_sheet_no, string oper_id, DataTable lines);

        void DoMrp(string ph_sheet_no, string oper_id);

        void CreateCgOrderByDtl(string ph_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id);

        void CreateCgOrderBySum(string ph_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id);

        void DoBulkMrp(string sheet_nos, DateTime date1, DateTime date2, string ms_other, string oper_id);

        void CreateBulkCgOrder(string cb_sheet_no, string op_type, string item_nos, string is_min_stock, string oper_id);

        /// <summary>
        /// 大宗采购锁库存
        /// </summary>
        /// <param name="sheet_nos">销售订单号</param>
        void DoLockInventory(string cb_sheet_no, string sheet_nos, string lock_sheet_nos);
    }
}