using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class Sys : ISys
    {
        public void add(sys_t_system sys)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Insert(sys);
        }

        public void update(sys_t_system sys)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(sys, "sys_var_id");
        }

        public System.Data.DataTable GetAll()
        {
            string sql = "select * from sys_t_system";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public void addDic(Dictionary<string, sys_t_system> sys)
        {
            foreach (var key in sys.Keys)
            {
                add(sys[key]);
            }
        }

        public void updatedic(Dictionary<string, sys_t_system> sys)
        {
            foreach (var key in sys.Keys)
            {
                update(sys[key]);
            }
        }

        public string Read(string sys_var_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_system where sys_var_id='" + sys_var_id + "'";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                return tb.Rows[0]["sys_var_value"].ToString();
            }
            return "";
        }

        public void Write(string parId, string val)
        {
            string sql = "update sys_t_system set sys_var_value='" + val + "' where sys_var_id='" + parId + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.ExecuteScalar(sql, null);
        }

        public System.Data.DataTable GetSheetNo()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DataTable tb = db.ExecuteToTable("select * from sys_t_sheet_no", null);
            return tb;
        }


        public void AddJH(global::Model.netsetup ns)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var count = Convert.ToInt32(db.ExecuteScalar("select count(*) from netsetup where jh='" + ns.jh + "' and softpos='" + ns.softpos + "'", null));
            if (count < 1)
            {
                db.Insert(ns);
            }
            else
            {
                throw new Exception("机号[" + ns.jh + "]已存在");
            }
        }

        public void DelJH(global::Model.netsetup ns)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var count = Convert.ToInt32(db.ExecuteScalar("select count(*) from netsetup where jh='" + ns.jh + "' and softpos='" + ns.softpos + "'", null));
            if (count > 0)
            {
                db.ExecuteScalar("delete  netsetup where jh='" + ns.jh + "' and softpos='" + ns.softpos + "' ", null);
            }
            else
            {
                throw new Exception("机号[" + ns.jh + "]不存在");
            }
        }

        public DataTable GetJH()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            var tb = db.ExecuteToTable("select * from netsetup ", null);
            return tb;
        }


        public DataTable GetAllGrant(global::Model.sa_t_oper_grant grant)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"select * from sa_t_oper_grant where 1=1";
            if (grant != null)
            {
                if (!string.IsNullOrEmpty(grant.oper_id))
                    sql += " and oper_id='" + grant.oper_id + "' ";
            }

            DataTable tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        public void SaveGrant(List<global::Model.sa_t_oper_grant> grant)
        {
            DB.DBByHandClose db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                global::Model.sa_t_oper_grant g = d.ExecuteToModel<global::Model.sa_t_oper_grant>(
                    "select top 1 * from sa_t_oper_grant where oper_id='" + grant[0].oper_id + "'", null);
                if (g != null && g.update_time > grant[0].update_time)
                {
                    throw new Exception("权限已被修改");
                }
                else
                {
                    d.ExecuteScalar("delete sa_t_oper_grant where oper_id='" + grant[0].oper_id + "'", null);
                    foreach (var item in grant)
                    {
                        d.Insert(item);
                    }
                }

                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }

        }
    }
}
