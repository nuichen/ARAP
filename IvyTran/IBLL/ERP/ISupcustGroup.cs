using System.Collections.Generic;
using System.Data;
using Model;
using Model.BaseModel;

namespace IvyTran.IBLL.ERP
{
    public interface ISupcustGroup
    {
        DataTable GetAll();
        DataTable GetSupGroup();
        DataTable GetCusGroup();

        void SaveGroup(List<bi_t_supcust_group> lis);
        DataTable GetAllCls(string status);
        void SaveCls(List<bi_t_company_type> lis);
    }
}