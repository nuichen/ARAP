using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Model;
using Model.BaseModel;

namespace IvyBack.IBLL
{
    public interface ISupcustGroup
    {
        DataTable GetALl();
        DataTable GetSupGroup();
        DataTable GetCusGroup();

        void SaveGroup(List<bi_t_supcust_group> lis);

        DataTable GetAllCls(string status);
        void SaveCls(List<bi_t_company_type> lis);
    }
}