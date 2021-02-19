using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyFront.IBLL
{
    interface ISaleData
    {
        void Insert(Model.t_order_detail item, out int flow_id);
        void InsertPayFlow(Model.ot_pay_flow item, out int flow_id);
        void SubmitOrder(string sheet_no, decimal discount, string oper_id);
        void DeleteSaleFlow(string sheet_no, int flow_id, string oper_id);
        void UpdateSaleFlowQty(string sheet_no, int flow_id, decimal qty, string oper_id);
        void UpdateSaleFlowPrice(string sheet_no, int flow_id, decimal sale_price, string oper_id);
        void GiveSaleFlow(string sheet_no, int flow_id, string oper_id);

        List<Model.t_order_detail> GetSaleFlow(string date1, string date2, string order_key, int pageIndex, int pageSize, out int total);
        void GetSaleFlowSum(string date1, string date2, string order_key, out int order_count, out decimal total_amt, out decimal give_amt);
        List<Model.ot_pay_flow> GetSaleSum(string date1, string date2, string order_key, out int order_count, out decimal total_amt, out decimal give_amt);
        List<Model.ot_pay_flow> GetPayFlow(string date1, string date2, string order_key, int page_no, int page_size, out int total);
        DataTable GetSaleSheet(string date1, string date2, int page_no, int page_size, out int total);
        void GetSaleSheetSum(string date1, string date2, out int order_count, out decimal total_amt, out decimal fact_amt);
        DataTable GetSaleSheetDetail(string sheet_no);
        Model.sm_t_salesheet GetSaleSheetMasterDetail(string sheet_no, out DataTable lines, out decimal ml);
        DataTable GetInOutSheet(string date1, string date2, int page_no, int page_size, out int total);
        void GetInOutSheetSum(string date1, string date2, out int order_count, out decimal total_amt, out decimal fact_amt);
        DataTable GetTHInOutSheet(string date1, string date2, int page_no, int page_size, out int total);
        void GetTHInOutSheetSum(string date1, string date2, out int order_count, out decimal total_amt, out decimal fact_amt);
        DataTable GetInOutSheetDetail(string sheet_no);

        decimal GetCusItemPrice(string cust_id, string item_no);
        decimal GetSupItemPrice(string sup_id, string item_no);
        bool CheckCusIsRetail(string cust_id);

        decimal GetCusNoPayAmt(string cust_id);
    }
}
