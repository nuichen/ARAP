using Model.PaymentModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace IvyTran.IBLL.ERP
{
    interface ICusSettle
    {
        System.Data.DataTable GetList(DateTime date1, DateTime date2, string cus_no, string is_cs);
        DataTable GetFYList(string supcust_no, string supcust_flag);
        void GetOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2);
        string MaxCode();
        void Add(Model.rp_t_recpay_record_info ord, List<Model.rp_t_recpay_record_detail> lines, DataTable dt, string is_cs, out string sheet_no);
        void Change(Model.rp_t_recpay_record_info ord, List<Model.rp_t_recpay_record_detail> lines, DataTable dt);
        void Delete(string sheet_no);
        void Check(string sheet_no, string approve_man, string is_cs);
        void NotCheck(string sheet_no, string approve_man, string is_cs);

        DataTable GetAccountFlows( Model.rp_t_accout_payrec_flow flow);
    }
}
