using System;
using System.Data;
using DB;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class MyDestop : IMyDestop
    {
        IDB d = new DBByAutoClose(AppSetting.conn);
        public System.Data.DataTable GetAll()
        {
            string sql = @"select * from sys_t_oper_type";
            DataTable tb = d.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetMyLove(string oper_id)
        {
            string sql = @"select * from sys_t_oper_mylove where oper_id='" + oper_id + "'";
            DataTable tb = d.ExecuteToTable(sql, null);
            return tb;
        }

        public void AddMyLove(global::Model.sys_t_oper_mylove mylove)
        {
            string sql = "select * from sys_t_oper_mylove where oper_id='" + mylove.oper_id + "' and substring(oper_type,3,2) " + (mylove.oper_type.Substring(1, 2).Equals("00") ? "=" : "!=") + " '00' ";
            var tb = d.ExecuteToTable(sql, null);
            if ((tb.Rows.Count < 8 && !mylove.oper_type.Substring(1, 2).Equals("00")) || (tb.Rows.Count < 10 && mylove.oper_type.Substring(1, 2).Equals("00")))
            {
                tb = d.ExecuteToTable("select * from sys_t_oper_mylove where oper_id='" + mylove.oper_id + "' and oper_type='" + mylove.oper_type + "'", null);
                if (tb.Rows.Count < 1)
                    d.Insert(mylove);
            }
            else
            {
                throw new Exception("超过收藏数量");
            }
        }

        public void deleteMyLove(global::Model.sys_t_oper_mylove mylove)
        {
            d.ExecuteScalar("delete from sys_t_oper_mylove where oper_id='" + mylove.oper_id + "' and oper_type='" + mylove.oper_type + "'", null);
        }

    }
}
