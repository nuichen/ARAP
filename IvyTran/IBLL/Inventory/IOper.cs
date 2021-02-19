using System;
using System.Data;

namespace IvyTran.IBLL.Inventory
{
    public interface IOper
    {
        void Init();
        bool Login(string oper_id, string pwd);
        DataTable GetList();
        bool Add(string oper_id, string oper_name, string pwd, string oper_type, string status);
        bool Change(string oper_id, string oper_name, string oper_type, string status);
        bool ChangePWD(string oper_id, string pwd);
        bool GetOne(string oper_id, out string oper_name, out string oper_type, out string status);
    }
}