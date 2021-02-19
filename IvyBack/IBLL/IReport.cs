using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IvyBack.Helper;

namespace IvyBack.IBLL
{
    public interface IReport
    {

        #region 应收应付
        Page<object> GetCusContactDetails(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page);
        Page<object> GetCusBalance(DateTime start_time, DateTime end_time, string cust_from, string company_type, bool isnull, Page<object> page);
        Page<object> GetSupContactDetails(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page);
        Page<object> GetSupBalance(DateTime start_time, DateTime end_time, string cust_from, string company_type, bool isnull, Page<object> page);
        Page<object> GetCusAgingGroup(DateTime start_time, string cust_from,string company_type, Page<object> page);
        Page<object> GetSupAgingGroup(DateTime start_time, string cust_from, string company_type, Page<object> page);
        #endregion

        #region 出纳管理

        Page<object> GetBankCashDetailed(DateTime start_time, DateTime end_time, string visa_id, Page<object> page);
        Page<object> GetBankCashBalance(DateTime start_time, DateTime end_time, string visa_id1, Page<object> page);
        #endregion
        //采购 PI F
        Page<object> GetCGSum(DateTime start_time, DateTime end_time, string branch_no, Page<object> page);//采购汇总
        Page<object> GetCGDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string supcust_no, string trans_no, Page<object> page);//采购明细
        Page<object> GetCGOrderSum(DateTime start_time, DateTime end_time, string supcust_no, string barcode, Page<object> page);
        Page<object> GetCGMoreSup(DateTime start_time, DateTime end_time, string supcust_no, string barcode, Page<object> page);

        Page<object> GetOrderInLoss_D(DateTime start_time, DateTime end_time, string keyword, string supcust_no,
            Page<object> page);
        Page<object> GetOrderInLoss(DateTime start_time, DateTime end_time, string keyword, string supcust_no, Page<object> page);

        Page<object> GetCGItemDetail(DateTime start_time, DateTime end_time, string supcust_no, string keyword,
            Page<object> page);
        //销售
        Page<object> GetSaleSum(DateTime start_time, DateTime end_time, string branch_no, string supcust_no, Page<object> page);//销售汇总
        Page<object> GetSaleDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string cust_no, Page<object> page);//销售明细
        Page<object> GetSaleItemDetail(DateTime start_time, DateTime end_time, string branch_no, string cust_no, Page<object> page);//批发销售商品明细
        Page<object> GetSaleOutDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string supcust_no, Page<object> page);//退货
        Page<object> GetCusCredit(string supcust_no, Page<object> page);//客户信誉度
        Page<object> GetNoSaleCus(DateTime start_time, DateTime end_time, Page<object> page);//时段无销售客户
        Page<object> GetSheetPayInfo(DateTime start_time, DateTime end_time, string trans_no, string supcust_no, string sheet_no, string type, Page<object> page);
        Page<object> GetSaleOrderSum(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page);//销售订单汇总
        Page<object> GetSaleOrderDetail(DateTime start_time, DateTime end_time, string sheet_no, Page<object> page);//销售订单明细
        Page<object> GetInOutDiff(DateTime start_time, DateTime end_time, Page<object> page);//销售订单实际出库差异

        //库存
        Page<object> GetICSum(string branch_no, string item_clsno, string item_name, string barcode, string sup_no, string stock_qty, Page<object> page);
        Page<object> GetICFlow(DateTime start_time, DateTime end_time, string branch_no, string str, Page<object> page);
        Page<object> GetICOutDetail(DateTime start_time, DateTime end_time, string branch_no, string barcode, string item_name, string sheet_no, string item_clsno, Page<object> page);
        Page<object> GetJXCSum(DateTime start_time, DateTime end_time, string branch_no, string item_clsno, string item_name, string barcode, Page<object> page);
        Page<object> GetPmDetail(DateTime start_time, DateTime end_time, string barcode, string item_name, Page<object> page);

        //库存盘点报告
        Page<object> GetCheckPlan(DateTime start_time, DateTime end_time, string barcode, Page<object> page);
        Page<object> GetCheckPlanDetail(DateTime start_time, DateTime end_time, string sheet_no, string barcode, string item_clsno, string branch_no, Page<object> page);

        //财务
        Page<object> GetRpCusSum(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page);
        Page<object> GetRpCusDetail(DateTime start_time, DateTime end_time, string supcust_no, string deal_man, string sheet_no, Page<object> page);
        Page<object> GetRpSupSum(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page);
        Page<object> GetRpSupDetail(DateTime start_time, DateTime end_time, string sheet_no, string deal_man, string supcust_no, Page<object> page);
        Page<object> GetRpSupAccount(DateTime start_time, DateTime end_time, string sheet_no, string oper_type, string supcust_no, Page<object> page);
        Page<object> GetRpCusAccount(DateTime start_time, DateTime end_time, string sheet_no, string oper_type, string supcust_no, Page<object> page);
        Page<object> GetRpTodayInc(string sheet_no, string supcust_no, Page<object> page);
        Page<object> GetRpTodayPay(string sheet_no, string supcust_no, Page<object> page);
        Page<object> GetRpCusFyDetail(DateTime start_time, DateTime end_time, string supcust_no, string kk_no, string sheet_no, Page<object> page);
        Page<object> GetRpSupFyDetail(DateTime start_time, DateTime end_time, string supcust_no, string kk_no, string sheet_no, Page<object> page);
        Page<object> GetRpCashBank(DateTime start_time, DateTime end_time, string visa_id, Page<object> page);
        Page<object> GetRpAdminCost(DateTime start_time, DateTime end_time, string sheet_no, string type_no, Page<object> page);

        //采购助手
        Page<object> GetAssCGFlow(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page);
        Page<object> GetAssCGPlanDetail(DateTime start_time, DateTime end_time, string keyword, Page<object> page);
        DataTable GetAssCGPlanDetailExport(DateTime start_time, DateTime end_time, string keyword);
        Page<object> GetAssCGPreDetail(DateTime start_time, DateTime end_time, string keyword, Page<object> page);

        //叉车秤收获流水
        Page<object> GetReceiveOrderDetail(DateTime start_time, DateTime end_time, string item_no, string is_build, Page<object> page);

        //实拣
        Page<object> GetPickingDetail(DateTime start_time, DateTime end_time, string item, Page<object> page);
        Page<object> GetPickingDiff(DateTime start_time, DateTime end_time, string item, Page<object> page);

        //盘点
        Page<object> GetInventoryCheck(DateTime start_time, DateTime end_time, Page<object> page);

        //个人空间
        Page<object> GetOperatingStatement(DateTime start_time, DateTime end_time, Page<object> page);

        //生产加工
        Page<object> GetProcessDetail(DateTime start_time, DateTime end_time, string ph_sheet_no, string oper_type, Page<object> page);
        Page<object> GetProcessLoss(DateTime start_time, DateTime end_time, Page<object> page);
        //菜单
        Page<object> GetMenuTotalCost(DateTime start_time, DateTime end_time, string cust_no, Page<object> page);
    }
}
