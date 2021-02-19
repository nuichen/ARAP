using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using IvyBack.Helper;
using System.Data;
using Model.BaseModel;
using Model.PaymentModel;

namespace IvyBack.IBLL
{
    public interface IARAP_SCPaymentBLL
    {

        Page<ot_supcust_beginbalance> GetSupcustBeginbalance(string region_no, string keyword, string isCS, Page<ot_supcust_beginbalance> page);
        Page<ot_supcust_beginbalance> GetSupcustList(string region_no, string keyword, string isCS, Page<ot_supcust_beginbalance> page);
        void SavaSupcustInitial(List<ot_supcust_beginbalance> lr, string is_CS,string oper_id);
        void CheckSupcustInitial( string supcust_flag);
        void NotCheckSupcustInitial(string supcust_flag);
        DataTable GetAccountFlows(Model.rp_t_accout_payrec_flow flow);
        DataTable GetNoticeList(DateTime date1, DateTime date2, string cus_no, string is_cs, string sheet_no);
        DataTable GetCollectionNotice(List<rp_t_account_notice> lr);
        DataTable GetSupcustList(string is_cs);
        DataTable GetPaymentList();
        DataTable GetSupcustBeginbalanceModel(string supcust_no);
        void AddNotice(Model.rp_t_account_notice ord, List<Model.rp_t_account_notice_detail> lines, out string sheet_no);
        void DeleteNotice(string sheet_no, DateTime update_time);
        void DeleteInitial(string keyword,string is_cs, DateTime update_time);
        void CheckNotice(string sheet_no, string approve_man, DateTime update_time);
        void ChangeNotice(Model.rp_t_account_notice ord, List<Model.rp_t_account_notice_detail> lines, out string sheet_no);
        List<ot_supcust_beginbalance> GetSupcustInfoList(string is_cs);
        void AddSupcustInitial(string supcust, string is_cs, string oper_id, int type);
        DataTable GetRecpayRecordModel(string supcust_no,string is_cs);
        System.Data.DataTable GetList(DateTime date1, DateTime date2, string cus_no, string is_cs);
        void GetOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        System.Data.DataTable GetArApList(DateTime date1, DateTime date2, string supcust_form, string supcust_to, string sheet_id);
        void GetArApOrder(string sheet_no, out System.Data.DataTable tb1);
        void AddArAp(rp_t_arap_transformation ord, out string sheet_no);
        void DeleteArAp(string sheet_no, DateTime update_time);
        void CheckArAp(string sheet_no, string approve_man, DateTime update_time);
        void NotCheckArAp(string sheet_no, string approve_man, DateTime update_time);
        void ChangeArAp(rp_t_arap_transformation ord,out  string sheet_no);
        DataTable GetPaymentList(string sheet_no);

        void SaveAgingGroup(DataTable dt);
        DataTable GetAgingGroup(string is_cs);
        //DataTable GetCar(string id);


        //bool Supcust_type(DataTable dt);
        //DataTable Supcust_type(string sup_name, string flag, string type, string region);
        //DataTable GetDataTable(string region_no, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        //Page<bi_t_supcust_info> GetDataTable(string region_no, string keyword, int show_stop, Page<bi_t_supcust_info> page);

        //bi_t_supcust_info GetItem(string supcust_no);

        //string GetMaxCode();

        //void Add(bi_t_supcust_info sup);
        //void Adds(List<bi_t_supcust_info> supcustInfos);
        //void Update(bi_t_supcust_info sup);
        //void Del(bi_t_supcust_info sup);

        //DataTable QuickSearchList(string keyword);


        //DataTable GetCustItemList(DateTime date1, DateTime date2, string cust_id);
        //void GetCustItem(string sheet_no, out DataTable tb1, out DataTable tb2);
        //void AddCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines, out string sheet_no);
        //void ChangeCustItem(Model.rp_t_cust_item_master ord, List<Model.rp_t_cust_item_detail> lines);
        //void DeleteCustItem(string sheet_no);
        //void CheckCustItem(string sheet_no, string approve_man);
    }
}
