using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace IvyBack.IBLL
{
    public interface IFinanceBLL
    {
        DataTable GetSZTypeList();
        Model.bi_t_sz_type GetSZTypeItem(string pay_way);
        void InsertSZType(Model.bi_t_sz_type item);
        void UpdateSZType(Model.bi_t_sz_type item);
        void DeleteSZType(string pay_way);
        DataTable GetIncomeRevenueList(string supcust_no, string month);
        void SaveIncomeRevenue(string supcust_no, string month, string oper_id, List<rp_t_supcust_income_revenue> details);
    }
}
