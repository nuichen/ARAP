using System;
using System.Collections.Generic;
using System.Data;
 
using Model;
using Model.BaseModel;
using Model.PaymentModel;

namespace IvyTran.IBLL.ERP
{
    interface IARAP_SCPaymentBLL
    {
        System.Data.DataTable GetSupcustBeginbalance(string region_no, string keyword, string is_cs,string is_jail, int page_index, int page_size, out int total_count);
        System.Data.DataTable GetSupcustList(string region_no, string keyword, string is_cs, string is_jail, int page_index, int page_size, out int total_count);
        void SavaSupcustInitial(List<ot_supcust_beginbalance> lr, string is_cs, string oper_id);
        void CheckSupcustInitial(string supcust_flag);
        void NotCheckSupcustInitial( string supcust_flag);
        DataTable GetAccountFlows(Model.rp_t_accout_payrec_flow flow);
        DataTable GetNoticeList(DateTime date1, DateTime date2, string cus_no, string is_cs, string sheet_no);
        DataTable GetCollectionNotice(List<rp_t_account_notice> lr);
        System.Data.DataTable GetSupcustList( string is_cs);
        System.Data.DataTable GetSupcustInfoList(string is_cs);
        System.Data.DataTable GetSupcustBeginbalanceModel(string supcust_no);
        void AddNotice(Model.rp_t_account_notice ord, List<rp_t_account_notice_detail> lines, out string sheet_no);
        void ChangeNotice(Model.rp_t_account_notice ord, List<Model.rp_t_account_notice_detail> lines);
        void DeleteNotice(string sheet_no, DateTime update_time);
        void DeleteInitial(string keyword, string is_cs, DateTime update_time);
        void CheckNotice(string sheet_no, string approve_man, DateTime update_time);
        void AddSupcustInitial(string supcust, string is_cs, string oper_id, string  type);
        System.Data.DataTable GetPaymentList();
        System.Data.DataTable GetRecpayRecordModel(string supcust_no, string is_cs);
        System.Data.DataTable GetList(DateTime date1, DateTime date2, string cus_no, string is_cs);
        void GetOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        DataTable GetPaymentList(string sheet_no);
        System.Data.DataTable GetArApList(DateTime date1, DateTime date2, string supcust_form, string supcust_to, string sheet_id);
        void GetArApOrder(string sheet_no , out System.Data.DataTable tb1);
        void AddArAp(rp_t_arap_transformation ord, out string sheet_no);
        void DeleteArAp(string sheet_no, DateTime update_time);
        void CheckArAp(string sheet_no, string approve_man, DateTime update_time);
        void NotCheckArAp(string sheet_no, string approve_man, DateTime update_time);
        void ChangeArAp(rp_t_arap_transformation ord,out string sheet_no);
        void SaveAgingGroup(DataTable dt);
        DataTable GetAgingGroup(string is_cs);


    }
}
