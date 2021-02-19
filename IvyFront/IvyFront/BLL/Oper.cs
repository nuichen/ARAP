using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyFront.BLL
{
    class Oper : IBLL.IOper
    {
        bool IBLL.IOper.Login(string oper_id, string pwd, out string errMsg)
        {
            errMsg = "";
            if (pwd != "")
            {
                Helper.sec sec = new Helper.sec();
                pwd = sec.des(pwd);
            }
            string sql = "select * from sa_t_operator_i where oper_id='" + oper_id + "' and branch_no='" + Program.branch_no + "' ";
            var dt = Program.db.ExecuteToTable(sql, null);
            if (dt.Rows.Count > 0) 
            {
                if (dt.Rows[0]["oper_pw"].ToString().ToUpper() != pwd.ToUpper()) 
                {
                    errMsg = "密码错误";
                    return false;
                }
                if (dt.Rows[0]["oper_status"].ToString() != "1")
                {
                    errMsg = "账号状态异常";
                    return false;
                }
                Program.oper_id = oper_id;
                Program.oper_name = dt.Rows[0]["oper_name"].ToString();
                Program.oper_type = dt.Rows[0]["oper_type"].ToString();
                return true;
            }
            errMsg = "账号或者密码错误";
            return false;
        }

        bool IBLL.IOper.GetModel(string oper_id, out Model.sa_t_operator_i model)
        {
            string sql = @"select * from sa_t_operator_i where oper_id='" + oper_id + "' and branch_no='" + Program.branch_no + "' limit 0,1 ";

            var m = Program.db.ExecuteToModel<Model.sa_t_operator_i>(sql, null);
            if (m == null)
            {
                model = null;
                return false;
            }
            else
            {
                model = m;
                return true;
            }
        }

        Dictionary<string, Model.sa_t_operator_i> IBLL.IOper.GetList()
        {
            string sql = "select * from sa_t_operator_i where oper_type<>'0'";
            var tb = Program.db.ExecuteToTable(sql, null);
            Dictionary<string, Model.sa_t_operator_i> dic = new Dictionary<string, Model.sa_t_operator_i>();
            foreach (DataRow row in tb.Rows)
            {
                var item = DB.ReflectionHelper.DataRowToModel<Model.sa_t_operator_i>(row);
                dic.Add(row["oper_id"].ToString(), item);
            }
            return dic;
        }

        void IBLL.IOper.UpdatePwd(string branch_no, string oper_id, string new_pwd)
        {
            string sql = "select * from sa_t_operator_i where branch_no='" + branch_no + "' and oper_id='" + oper_id + "' ";
            var dt = Program.db.ExecuteToTable(sql, null);
            if (dt.Rows.Count > 0)
            {
                sql = "update sa_t_operator_i set oper_pw='" + new_pwd + "' where branch_no='" + branch_no + "' and oper_id='" + oper_id + "' ";

                Program.db.ExecuteScalar(sql, null);
            }
            else
            {
                throw new Exception("操作员不存在");
            }

        }

    }

}
