using System.Data;
using System.Collections.Generic;

namespace IvyTran.IBLL.ERP
{
    public interface IFinanceBLL
    {
        DataTable GetSZTypeList(string is_show_stop);
        DataTable GetSZTypeItem(string pay_way);
        void InsertSZType(Model.bi_t_sz_type item);
        void UpdateSZType(Model.bi_t_sz_type item);
        void DeleteSZType(string pay_way);

        DataTable GetIncomeRevenueList(string supcust_no, string month);
        void SaveIncomeRevenue(string supcust_no, List<Model.rp_t_supcust_income_revenue> details, string oper_id, string month);
    }
}
