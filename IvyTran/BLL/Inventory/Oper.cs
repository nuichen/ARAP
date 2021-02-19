using System;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.Inventory;

namespace IvyTran.BLL.Inventory
{
    public class Oper : IOper
    {
        public void Init()
        {
            string sql = "select count(*) from pda_st_t_oper_info where oper_id='9999'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            object obj = db.ExecuteScalar(sql, null);
            int cnt = 0;
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out cnt);
            }

            if (cnt == 0)
            {
                sql = "insert into pda_st_t_oper_info(oper_id,oper_name,pwd,oper_type,status)values('9999','系统管理员','" + Conv.ToMD5("9999") + "','0','1')";

                db.ExecuteScalar(sql, null);
            }
        }

        public bool Login(string oper_id, string pwd)
        {
            string sql = "select count(*) from pda_st_t_oper_info where oper_id='" + oper_id + "' and pwd='" + pwd + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            object obj = db.ExecuteScalar(sql, null);
            int cnt = 0;
            if (obj == null)
            {
                return false;
            }
            else
            {
                int.TryParse(obj.ToString(), out cnt);
                if (cnt == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public DataTable GetList()
        {
            string sql = "select * from pda_st_t_oper_info order by oper_id";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public bool Add(string oper_id, string oper_name, string pwd, string oper_type, string status)
        {
            string sql = "select count(*) from pda_st_t_oper_info where oper_id='" + oper_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            object obj = db.ExecuteScalar(sql, null);
            int cnt = 0;
            if (obj != null)
            {

                int.TryParse(obj.ToString(), out cnt);
                if (cnt == 0)
                {

                }
                else
                {
                    throw new Exception("已经存在操作员ID" + oper_id);
                }

            }
            //
            sql = "insert into pda_st_t_oper_info(oper_id,oper_name,pwd,oper_type,status)" +
                  "values('" + oper_id + "','" + oper_name + "','" + pwd + "','" + oper_type + "','" + status + "')";
            db.ExecuteScalar(sql, null);
            return true;
        }

        public bool Change(string oper_id, string oper_name, string oper_type, string status)
        {
            string sql = "update pda_st_t_oper_info set oper_name='" + oper_name + "',oper_type='" +
                         oper_type + "',status='" + status + "' where oper_id='" + oper_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.ExecuteScalar(sql, null);
            return true;
        }

        public bool ChangePWD(string oper_id, string pwd)
        {
            string sql = "update pda_st_t_oper_info set pwd='" + pwd + "' where oper_id='" + oper_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.ExecuteScalar(sql, null);
            return true;
        }

        public bool GetOne(string oper_id, out string oper_name, out string oper_type, out string status)
        {
            string sql = "select * from pda_st_t_oper_info where oper_id='" + oper_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count == 0)
            {
                throw new Exception("不存在操作员" + oper_id);
            }
            else
            {
                DataRow row = tb.Rows[0];
                oper_name = row["oper_name"].ToString();
                oper_type = row["oper_type"].ToString();
                status = row["status"].ToString();
                return true;
            }
        }
    }
}