using System;
using System.Collections.Generic;
using System.Data;
 
using Model;
using Model.BaseModel;
using Model.PaymentModel;

namespace IvyTran.IBLL.ERP
{
    interface ICashierBLL
    {
        System.Data.DataTable GetBankCashBeginbalance( string keyword,string is_jail, int page_index, int page_size, out int total_count);


        DataTable GetBankList( string keyword, string is_jail, int page_index, int page_size, out int total_count);
        void SavaBankCashBeginbalance(List<rp_t_bank_beginbalance> lr, string oper_id);
        void DeleteBankCashBeginbalance(string visa_id, DateTime update_time);
        void CheckBankCashBeginbalance();
        void NotCheckBankCashBeginbalance();
        System.Data.DataTable GetCollectionList( string type, string supcust_no, string visa_id, string start_date, string end_date);
        DataTable GetCollectionWayList(string sheet_no);
        void AddCollectionPay(Model.rp_t_pay_info ord, List<Model.rp_t_pay_detail> lines, out string sheet_no);
        void DeleteCollectionPay(string sheet_no, DateTime update_time);
        void CheckCollectionPay(string sheet_no, string approve_man, DateTime update_time);
        void NotCheckCollectionPay(string sheet_no, string approve_man, DateTime update_time);
        void ChangeCollectionPay(Model.rp_t_pay_info ord, List<Model.rp_t_pay_detail> lines,out string sheet_no);
        System.Data.DataTable GetCollectionPayList(DateTime date1, DateTime date2, string is_cs, string visa_id);
        void GetCollectionPayOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        System.Data.DataTable GetOtherList(DateTime date1, DateTime date2, string is_cs);
        void GetOtherOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        void AddOther(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines, out string sheet_no);
        void ChangeOther(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines);
        void DeleteOther(string sheet_no);
        void CheckOther(string sheet_no, string approve_man);
        void NotCheckOther(string sheet_no, string approve_man);




    }
}
