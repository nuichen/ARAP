using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace IvyTran.BLL.API
{
    public interface IOperBLL
    {
        bool Login(string oper_id, string pwd, out string errMsg, out Model.sa_t_operator_i item);

        void UpdatePwd(string oper_id, string pwd, string new_pwd);

        void ResetPwd(string oper_id, string pwd);

        DataTable GetOperList(string keyword);

        DataTable GetOper(string oper_id);

        DataTable GetOperTypeList();

        void AddOper(Model.sa_t_operator_i item);

        void UpdateOper(Model.sa_t_operator_i item);

        void StopOper(string oper_id);

        void StartOper(string oper_id);
    }
}