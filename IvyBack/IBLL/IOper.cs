using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
namespace IvyBack.IBLL
{
    public interface IOper
    {
        bool Login(sa_t_operator_i oper);
        bool Loginbyarap(sa_t_operator_i oper);
        bool Login(sa_t_operator_i oper,out string mc_id);
        bool UploadPw(string oper_id, string old_pwd, string new_pwd);
        void Add(Model.sa_t_operator_i oper);
        void Upload(Model.sa_t_operator_i oper);
        void Del(string oper_id);
        string GetMaxCode();
        DataTable GetOpers();

        DataTable GetOperType();
        List<sa_t_oper_type> GetOperTypeList();

        void AddOperType(Model.sa_t_oper_type type);
        void ChangeOperType(Model.sa_t_oper_type type);
        void DelOperType(Model.sa_t_oper_type type);

        void ResetPWD(string oper_id, string new_pwd);
    }
}
