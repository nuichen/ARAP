using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Model;
using Model.InOutModel;

namespace IvyTran.IBLL.ERP
{
    public interface IInOutBLL
    { 
        DataTable GetReceiveOrderDetail(DateTime start_time, DateTime end_time, string item_no, string is_build);

        void UpdateReceiveOrderDetail(DataTable dt);
        void InOutTZSheetDetail_D(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        DataTable GetInOutTZSheet(string sheet_no);
        void DeleteInOutTZ(string sheet_no, string type);
        void InOutTZ(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);

        void add_InOutTZ(List<Model.InOutModel.ic_t_inoutstore_recpay_detail> lr,
            Model.InOutModel.ic_t_inoutstore_recpay_main main);
        DataTable InOutTZList(string date1, string date2, string cust_no);
        void InOutTZSheetDetail(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void GetSaleSheetJGSheetDetail(string sheet_no, out System.Data.DataTable tb1,
            out System.Data.DataTable tb2);
        DataTable GetSaleSheetJGSheet(string sheet_no,DateTime date1,DateTime date2);
        DataTable GetSaleSheetTZSheet(string sheet_no);
        void GetSaleSheetTZSheetDetail(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        DataTable GetSaleSheetList(string date1, string date2, string cust_no, string sale_man);
        DataTable GetSimpleSaleSheetList(string date1, string date2, string cust_no);
        void add_TZ(List<Model.InOutModel.sm_t_salesheet_recpay_detail> lr, Model.InOutModel.sm_t_salesheet_recpay_main main);
        DataTable GetSaleSheetTZList(string date1, string date2, string cust_no);
        void GetSaleSheetTZ(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void GetSaleSheet(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void AddSaleSheet(body.sm_t_salesheet ord, List<body.sm_t_salesheet_detail> lines, out string sheet_no);
        void ChangeSaleSheet(body.sm_t_salesheet ord, List<body. sm_t_salesheet_detail> lines);
        void DeleteSaleSheet(string sheet_no, DateTime update_time);
        void CheckSaleSheet(string sheet_no, string approve_man, DateTime update_time);
        DataTable GetSaleSheetExport(string sheet_no, string sup_no, DateTime oper_date);
        DataTable GetImportSSSheet();


        DataTable GetSaleSSSheetList(string date1, string date2, string sup_no, string order_main);
        void GetSaleSSSheet(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void AddSaleSSSheet(co_t_order_main ord, List<co_t_order_child> lines, string is_gen_cg, out string sheet_no);
        void CGInSO(string Sal, string Pal);
        void ChangeSaleSSSheet(co_t_order_main ord, List<co_t_order_child> lines);
        void ChangeSaleSSheetGenPC(Dictionary<string, string> dic);
        void DeleteSaleSSSheet(string sheet_no, DateTime update_time);
        void CheckSaleSSSheet(string sheet_no, string approve_man, DateTime update_time);
        DataTable GetSaleSSDetailSum(List<string> sheet_nos);

        DataTable GetImportCGOrder();
        DataTable GetCGOrderList(string date1, string date2, string sup_no, string order_main);
        DataTable GetCGOrderList(string date1, string date2, string sup_no, string order_main, int type);
        DataTable GetSaleSSSheetListPS(string date1, string date2, string sup_no, string order_main);
        void GetCGOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void AddCGOrder(co_t_order_main ord, List<co_t_order_child> lines, out string sheet_no);
        void ChangeCGOrder(co_t_order_main ord, List<co_t_order_child> lines);
        void DeleteCGOrder(string sheet_no, DateTime update_time);
        void CheckCGOrder(string sheet_no, string approve_man, DateTime update_time);

        DataTable GetInOutList(string date1, string date2, string supcust_no, string trans_no);
        DataTable GetSimpleInOutList(string date1, string date2, string supcust_no, string trans_no);
        DataTable GetOtherInOutList(string date1, string date2, string trans_no);
        void GetInOut(string sheet_no, string trans_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void GetInOut(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void AddInOut(Model.ic_t_inout_store_master ord, List<body.ic_t_inout_store_detail> lines, out string sheet_no);
        void ChangeInOut(Model.ic_t_inout_store_master ord, List<body. ic_t_inout_store_detail> lines);
        void DeleteInOut(string sheet_no, DateTime update_time);
        void CheckInOut(string sheet_no, string approve_man, DateTime update_time);



        void AssAddCG(Model.ic_t_inout_store_master ord, List<ic_t_inout_store_detail> lines, out string sheet_no);//采购助手生成采购单
        void AssGenCG(string flow_id, string oper_id);//采购助手生成采购单
        void AssGenPlanCG(string flow_id, string oper_id);//采购助手生成采购单

        void ReceiveGenCG(string flow_id, string oper_id);

        void PickingGenSaleSheet(string flow_id, string oper_id);

        void InventoryAdjustment(string flow_id, string oper_id);

        void GenProcessDetail(string flowIds, string oper_id,string pro_dept_no,string fee_dept_no);
        #region 销售计划
        void AddSaleSPSheet(IvyTran.body.co_t_order_main ord, List<IvyTran.body.co_t_order_child> lines, out string sheet_no);
        void ChangeSaleSPSheet(IvyTran.body.co_t_order_main ord, List<IvyTran.body.co_t_order_child> lines);
        void DeleteSaleSPSheet(string sheet_no, DateTime update_time);
        void CheckSaleSPSheet(string sheet_no, string approve_man, DateTime update_time);
        DataTable GetSaleSPSheetList(string date1, string date2, string cust_id, string sale_man);
        void GetSaleSPSheet(string sheet_no, out DataTable tb1, out DataTable tb2);
        void GetSaleSPSheetWeek(string date1, string date2, string cust_id, string sale_man, out DataTable tb1, out DataTable tb2);
        #endregion

    }
}
