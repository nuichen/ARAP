using System;
using System.Data;

namespace IvyTran.IBLL.ERP
{
    public interface IReport
    {

        #region 应收应付
        DataTable GetCusContactDetails(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetCusBalance(DateTime start_time, DateTime end_time, string cust_from, string company_type, int isnull, int page_index, int page_size, out int total_count);
        DataTable GetSupContactDetails(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetSupBalance(DateTime start_time, DateTime end_time, string cust_from, string company_type, int isnull, int page_index, int page_size, out int total_count);
        DataTable GetCusAgingGroup(DateTime start_time, string cust_from, string company_type, int page_index, int page_size, out int total_count);
        DataTable GetSupAgingGroup(DateTime start_time, string cust_from, string company_type, int page_index, int page_size, out int total_count);

        #endregion

        #region 出纳管理

        DataTable GetBankCashDetailed(DateTime start_time, DateTime end_time, string visa_id, int page_index, int page_size, out int total_count);
        DataTable GetBankCashBalance(DateTime start_time, DateTime end_time, string visa_id1, int page_index, int page_size, out int total_count);
        #endregion
        //采购 PI F
        DataTable GetCGSum(DateTime start_time, DateTime end_time, string branch_no, int page_index, int page_size, out int total_count);//采购汇总
        DataTable GetCGDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string supcust_no, string trans_no, int page_index, int page_size, out int total_count);//采购明细
        DataTable GetCGOrderSum(DateTime start_time, DateTime end_time, string supcust_no, string barcode, int page_index, int page_size, out int total_count);
        DataTable GetCGMoreSup(DateTime start_time, DateTime end_time, string supcust_no, string barcode, int page_index, int page_size, out int total_count);
        DataTable GetCGItemDetail(DateTime start_time, DateTime end_time, string supcust_no, string keyword, int page_index, int page_size, out int total_count);
        DataTable GetOrderInLoss(DateTime start_time, DateTime end_time, string keyword, string supcust_no, int page_index, int page_size, out int total_count);

        //销售
        DataTable GetSaleSum(DateTime start_time, DateTime end_time, string branch_no, string supcust_no, int page_index, int page_size, out int total_count);//销售汇总
        DataTable GetSaleDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string cust_no, int page_index, int page_size, out int total_count);//销售明细
        DataTable GetSaleItemDetail(DateTime start_time, DateTime end_time, string branch_no, string cust_no, int page_index, int page_size, out int total_count);//批发销售商品明细
        DataTable GetSaleOutDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string supcust_no, int page_index, int page_size, out int total_count);//退货
        DataTable GetCusCredit(string supcust_no, int page_index, int page_size, out int total_count);//客户信誉度
        DataTable GetNoSaleCus(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count);//时段无销售客户
        DataTable GetSheetPayInfo(DateTime start_time, DateTime end_time, string trans_no, string supcust_no, string sheet_no, string type, int page_index, int page_size, out int total_count);
        DataTable GetSaleOrderSum(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count);//销售订单汇总
        DataTable GetSaleOrderDetail(DateTime start_time, DateTime end_time, string sheet_no, int page_index, int page_size, out int total_count);//销售订单明细
        DataTable GetInOutDiff(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count);//销售订单实际出库差异

        //库存
        DataTable GetICSum(string branch_no, string item_clsno, string item_name, string barcode, string sup_no, string stock_qty, int page_index, int page_size, out int total_count);
        DataTable GetICFlow(DateTime start_time, DateTime end_time, string branch_no, string str, int page_index, int page_size, out int total_count);
        DataTable GetICOutDetail(DateTime start_time, DateTime end_time, string branch_no, string barcode, string item_name, string sheet_no, string item_clsno, int page_index, int page_size, out int total_count);
        DataTable GetJXCSum(DateTime start_time, DateTime end_time, string branch_no, string item_clsno, string item_name, string barcode, int page_index, int page_size, out int total_count);
        DataTable GetPmDetail(DateTime start_time, DateTime end_time, string barcode, string item_name, int page_index, int page_size, out int total_count);

        //盘点进度报告
        DataTable GetCheckPlan(DateTime start_time, DateTime end_time, string branch_no, int page_index, int page_size, out int total_count);
        DataTable GetCheckPlanDetail(DateTime start_time, DateTime end_time, string sheet_no, string barcode, string item_clsno, string branch_no, int page_index, int page_size, out int total_count);

        //财务
        DataTable GetRpCusSum(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetRpCusDetail(DateTime start_time, DateTime end_time, string supcust_no, string deal_man, string sheet_no, int page_index, int page_size, out int total_count);
        DataTable GetRpSupSum(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetRpSupDetail(DateTime start_time, DateTime end_time, string sheet_no, string deal_man, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetRpSupAccount(DateTime start_time, DateTime end_time, string sheet_no, string oper_type, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetRpCusAccount(DateTime start_time, DateTime end_time, string sheet_no, string oper_type, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetRpTodayInc(string sheet_no, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetRpTodayPay(string sheet_no, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetRpCusFyDetail(DateTime start_time, DateTime end_time, string supcust_no, string kk_no, string sheet_no, int page_index, int page_size, out int total_count);
        DataTable GetRpSupFyDetail(DateTime start_time, DateTime end_time, string supcust_no, string kk_no, string sheet_no, int page_index, int page_size, out int total_count);
        DataTable GetRpCashBank(DateTime start_time, DateTime end_time, string visa_id, int page_index, int page_size, out int total_count);
        DataTable GetRpAdminCost(DateTime start_time, DateTime end_time, string sheet_no, string type_no, int page_index, int page_size, out int total_count);


        //采购助手
        DataTable GetAssCGFlow(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count);
        DataTable GetAssCGPlanDetail(DateTime start_time, DateTime end_time, string keyword, int page_index, int page_size, out int total_count);
        DataTable GetAssCGPlanDetailExport(DateTime start_time, DateTime end_time, string keyword);
        DataTable GetAssCGPreDetail(DateTime start_time, DateTime end_time, string keyword, int page_index, int page_size, out int total_count);

        //叉车秤收获流水
        DataTable GetReceiveOrderDetail(DateTime start_time, DateTime end_time, string item_no, string is_build, int page_index, int page_size, out int total_count);

        //实拣
        DataTable GetPickingDetail(DateTime start_time, DateTime end_time, string item, int page_index, int page_size, out int total_count);
        DataTable GetPickingDiff(DateTime start_time, DateTime end_time, string item, int page_index, int page_size, out int total_count);


        //前台库存盘点明细
        DataTable GetInventoryCheck(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count);


        #region 桌面

        DataTable GetOperatingStatement(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count);

        #endregion

        //加工
        DataTable GetProcessDetail(DateTime start_time, DateTime end_time, string ph_sheet_no, string oper_type, int page_index, int page_size, out int total_count);
        DataTable GetProcessLoss(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count);


        //菜单
        DataTable GetMenuTotalCost(DateTime start_time, DateTime end_time, string cust_no, int page_index, int page_size, out int total_count);

    }
}
