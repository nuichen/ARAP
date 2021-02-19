using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using IvyTran.Helper;

namespace IvyTran.BLL.API
{
    public class OperBLL:IOperBLL
    {
        bool IOperBLL.Login(string oper_id, string pwd,out string errMsg, out Model.sa_t_operator_i item)
        {
            item = null;
            errMsg = "";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_operator_i where oper_id=@oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@oper_id",oper_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                var temp_pwd = dt.Rows[0]["oper_pw"].ToString();
                if (temp_pwd.ToLower() != pwd.ToLower())
                {
                    errMsg = "密码错误";
                    return false;
                }
                if (dt.Rows[0]["oper_status"].ToString() != "1") 
                {
                    errMsg = "账号状态异常";
                    return false;
                }
                item = new Model.sa_t_operator_i();
                item.oper_id = dt.Rows[0]["oper_id"].ToString();
                item.oper_name = dt.Rows[0]["oper_name"].ToString();
                item.oper_pw = dt.Rows[0]["oper_pw"].ToString();
                item.oper_type = dt.Rows[0]["oper_type"].ToString();
                item.oper_status = dt.Rows[0]["oper_status"].ToString();

                return true;
            }
            else
            {
                errMsg = "账号不存在";
                return false;
            }

        }

        void IOperBLL.UpdatePwd(string oper_id, string pwd, string new_pwd)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_operator_i where oper_id=oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@oper_id",oper_id)
            };
            var dt = db.ExecuteToTable(sql,pars);
            if (dt.Rows.Count > 0)
            {
                var temp_pwd = dt.Rows[0]["oper_pw"].ToString();
                if (temp_pwd.ToLower() != pwd.ToLower())
                {
                    throw new Exception("旧密码不正确");
                }
                sql = "update sa_t_operator_i set oper_pw=@new_pwd where oper_id=oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id",oper_id),
                    new System.Data.SqlClient.SqlParameter("@new_pwd",new_pwd)
                };
                db.ExecuteScalar(sql, pars);
            }
            else
            {
                throw new Exception("操作员不存在");
            }
        }

        void IOperBLL.ResetPwd(string oper_id, string pwd)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_operator_i where oper_id=oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@oper_id",oper_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                sql = "update sa_t_operator_i set oper_pw=@pwd where oper_id=oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id",oper_id),
                    new System.Data.SqlClient.SqlParameter("@pwd",pwd)
                };
                db.ExecuteScalar(sql, pars);
            }
            else
            {
                throw new Exception("操作员不存在");
            }
        }

        DataTable IOperBLL.GetOperList(string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var condition_sql = "";
            if (keyword != "") condition_sql += " and (a.oper_name like '%'+@keyword+'%' or a.oper_id  like '%'+@keyword+'%')";

            string sql = "select a.*,c.type_name from sa_t_operator_i a ";
            sql += "left join sa_t_oper_type c on a.oper_type=c.oper_type ";
            sql += "where 1=1 " + condition_sql + " order by oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;

        }

        DataTable IOperBLL.GetOper(string oper_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_operator_i where oper_id=@oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@oper_id",oper_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;

        }

        DataTable IOperBLL.GetOperTypeList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_oper_type ";
            var dt = db.ExecuteToTable(sql, null);
            return dt;

        }



        void IOperBLL.AddOper(Model.sa_t_operator_i item)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select 1 from sa_t_operator_i where oper_id=@oper_id  ";
                var pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id", item.oper_id)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0) throw new Exception("操作员编码已存在");

                sql = "insert into sa_t_operator_i(oper_id,oper_type,oper_name,oper_pw,oper_status,update_time) ";
                sql += "values(@oper_id,@oper_type,@oper_name,@oper_pw,@oper_status,getdate()) ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id", item.oper_id),
                    new System.Data.SqlClient.SqlParameter("@oper_type", item.oper_type),
                    new System.Data.SqlClient.SqlParameter("@oper_pw", item.oper_pw),
                    new System.Data.SqlClient.SqlParameter("@oper_name", item.oper_name),
                    new System.Data.SqlClient.SqlParameter("@oper_status", "1")
                };

                db.ExecuteScalar(sql,pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("OperBLL.AddOper()", ex.ToString(), item.oper_id, item.oper_name);
                throw ex;
            }
        }

        void IOperBLL.UpdateOper(Model.sa_t_operator_i item)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select 1 from sa_t_operator_i where oper_id=@oper_id  ";
                var pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id", item.oper_id)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count <= 0) throw new Exception("操作员不存在");

                sql = "update sa_t_operator_i set oper_type=@oper_type,oper_name=@oper_name,update_time=getdate() ";
                sql += "where oper_id=@oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id", item.oper_id),
                    new System.Data.SqlClient.SqlParameter("@oper_type", item.oper_type),
                    new System.Data.SqlClient.SqlParameter("@oper_name", item.oper_name)
                };

                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("OperBLL.UpdateOper()", ex.ToString(), item.oper_id, item.oper_name);
                throw ex;
            }
        }

        void IOperBLL.StopOper(string oper_id)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select 1 from sa_t_operator_i where oper_id=@oper_id  ";
                var pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count <= 0) throw new Exception("操作员不存在");

                sql = "update sa_t_operator_i set oper_status='0',update_time=getdate() where oper_id=@oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };

                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("OperBLL.StopOper()", ex.ToString(), oper_id);
                throw ex;
            }
        }

        void IOperBLL.StartOper(string oper_id)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select 1 from sa_t_operator_i where oper_id=@oper_id  ";
                var pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count <= 0) throw new Exception("操作员不存在");

                sql = "update sa_t_operator_i set oper_status='1',update_time=getdate() where oper_id=@oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                };

                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("OperBLL.StartOper()", ex.ToString(), oper_id);
                throw ex;
            }
        }

    }
}