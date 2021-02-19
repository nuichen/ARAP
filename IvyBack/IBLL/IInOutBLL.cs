using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Model.InOutModel;

namespace IvyBack.IBLL
{
    public interface IInOutBLL
    {
        #region 收货明细
        DataTable GetReceiveOrderDetail(DateTime start_time, DateTime end_time, string item_no, string is_build);
        void UpdateReceiveOrderDetail(DataTable dt);
        #endregion
        void GetInOutZTSheetD_D(string sheet_no, out DataTable tb1, out DataTable tb2);
        DataTable GetInOutZTSheet(string sheet_no);
        void DeleteZT(string sheet_no, string type);
        void CheckInOutZT(string sheet_no, string approve_man, string update_date);
        void AddInOutZT(List<Model.InOutModel.ic_t_inoutstore_recpay_detail> lr,
            ic_t_inoutstore_recpay_main main);
        DataTable GetInOutZTList(DateTime date1, DateTime date2, string sup_no);
        void GetInOutZTSheetD(string sheet_no, out DataTable tb1, out DataTable tb2);
        DataTable GetSaleSheetJGSheet(DateTime date1, DateTime date2, string sheet_no);
        void GetSaleSheetJGSheetD(string sheet_no, out DataTable tb1, out DataTable tb2);
        DataTable GetSaleSheetZTSheet(string sheet_no);
        void GetSaleSheetZTSheetD(string sheet_no, out DataTable tb1, out DataTable tb2);
        DataTable GetSaleSheetZTList(DateTime date1, DateTime date2, string sup_no);
        void AddZT(List<Model.InOutModel.sm_t_salesheet_recpay_detail> lr, sm_t_salesheet_recpay_main main);
        void GetSaleSheetZT(string sheet_no, out DataTable tb1, out DataTable tb2);
        void CheckZT(string sheet_no, string approve_man, string update_date);
        bool CgToSS(string sstr, string str);
        DataTable GetSaleSheetList(DateTime date1, DateTime date2, string cust_id, string sale_man);
        DataTable GetSimpleSaleSheetList(DateTime date1, DateTime date2, string cust_id);

        void GetSaleSheet(string sheet_no, out DataTable tb1, out DataTable tb2);
        void AddSaleSheet(Model.sm_t_salesheet ord, List<Model.sm_t_salesheet_detail> lines, out string sheet_no);
        void ChangeSaleSheet(Model.sm_t_salesheet ord, List<Model.sm_t_salesheet_detail> lines);
        void DeleteSaleSheet(string sheet_no, DateTime update_time);
        void CheckSaleSheet(string sheet_no, string approve_man, DateTime update_time);
        DataTable GetSaleSheetExport(string sheet_no, string sup_no, DateTime oper_date);

        DataTable GetImportSSSheet();
        DataTable GetSaleSSSheetList(DateTime date1, DateTime date2, string sup_no, string order_main);
        void GetSaleSSSheet(string sheet_no, out DataTable tb1, out DataTable tb2);
        void AddSaleSSSheet(Model.co_t_order_main ord, List<Model.co_t_order_child> lines, string is_gen_cg, out string sheet_no);
        void ChangeSaleSSSheet(Model.co_t_order_main ord, List<Model.co_t_order_child> lines);
        void ChangeSaleSSheetGenPC(Dictionary<string, string> dic);
        void DeleteSaleSSSheet(string sheet_no, DateTime update_time);
        void CheckSaleSSSheet(string sheet_no, string approve_man, DateTime update_time);
        DataTable GetSaleSSDetailSum(List<string> sheet_nos);

        DataTable GetImportCGOrder();
        DataTable GetCGOrderList(DateTime date1, DateTime date2, string sup_no, string order_main, int type);
        DataTable GetSaleSSSheetListPS(DateTime date1, DateTime date2, string sup_no, string order_main);
        void GetCGOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void AddCGOrder(co_t_order_main ord, List<co_t_order_child> lines, out string sheet_no);
        void ChangeCGOrder(co_t_order_main ord, List<co_t_order_child> lines);
        void DeleteCGOrder(string sheet_no, DateTime update_time);
        void CheckCGOrder(string sheet_no, string approve_man, DateTime update_time);

        DataTable GetInOutList(DateTime date1, DateTime date2, string supcust_no, string trans_no);
        DataTable GetSimpleInOutList(DateTime date1, DateTime date2, string supcust_no, string trans_no);
        DataTable GetOtherInOutList(DateTime date1, DateTime date2, string trans_no);
        void GetInOut(string sheet_no, string trans_no, out DataTable tb1, out DataTable tb2);
        void GetOtherInOut(string sheet_no, out DataTable tb1, out DataTable tb2);
        void AddInOut(Model.ic_t_inout_store_master ord, List<Model.ic_t_inout_store_detail> lines, out string sheet_no);
        void ChangeInOut(Model.ic_t_inout_store_master ord, List<Model.ic_t_inout_store_detail> lines);
        void DeleteInOut(string sheet_no, DateTime update_time);
        void CheckInOut(string sheet_no, string approve_man, DateTime update_time);

        void AssAddCG(Model.ic_t_inout_store_master ord, List<Model.ic_t_inout_store_detail> lines, out string sheet_no);
        void AssGenCG(string flow_id, string oper_id);//采购助手生成采购单
        void AssGenPlanCG(string flow_id, string oper_id);//采购助手生成采购单

        void ReceiveGenCG(string flow_id, string oper_id);//收货生成采购单

        void PickingGenSaleSheet(string flow_id, string oper_id);//实拣明细生成销售单

        void InventoryAdjustment(string flow_id, string oper_id);//盘点调整单

        void GenProcessDetail(string flowIds, string oper_id, string pro_dept_no, string fee_dept_no);
    }
}
