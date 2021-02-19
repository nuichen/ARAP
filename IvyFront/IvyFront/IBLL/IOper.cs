using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.IBLL
{
    interface IOper
    {
        bool Login(string oper_id, string pwd, out string errMsg);
        bool GetModel(string oper_id, out Model.sa_t_operator_i model);
        Dictionary<string, Model.sa_t_operator_i> GetList();

        void UpdatePwd(string branch_no, string oper_id, string new_pwd);
    }
}
