using System.Data;

namespace IvyTran.IBLL.ERP
{

    interface IOper
    {
        void Login(string oper_id, string pwd, out string oper_name, out string oper_type);
        void ChangePWD(string oper_id, string old_pwd, string new_pwd);
        void Add(Model.sa_t_operator_i oper);
        void Upload(Model.sa_t_operator_i oper);
        void Del(string oper_id);
        string GetMaxCode();
        DataTable GetOpers();

        DataTable GetOperType();
        void AddOperType(Model.sa_t_oper_type type);
        void ChangeOperType(Model.sa_t_oper_type type);
        void DelOperType(Model.sa_t_oper_type type);

        void ResetPWD(string oper_id, string new_pwd);
    }

}
