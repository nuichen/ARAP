using IvyTran.IBLL.ERP;
using Model.SysModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IvyTran.BLL.ERP
{
    public class SysLogBLL : ISysLogBLL
    {
        void ISysLogBLL.WriteSysLog(sys_t_operator_log item)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                db.Insert(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        DataTable ISysLogBLL.GetSysLog(decimal flow_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"select *  from sys_t_operator_log where flow_id='" + flow_id + "'";

            return db.ExecuteToTable(sql, null);
        }

        /// <summary>
        /// 日志查询
        /// </summary>
        /// <param name="date1">起始时间:yyyy-MM-dd HH:mm:ss</param>
        /// <param name="date2">结束时间:yyyy-MM-dd HH:mm:ss</param>
        /// <param name="log_type">系统日志，操作日志，监控日志</param>
        /// <param name="log_level">WARNING,ERROR,EXCEPTION,</param>
        /// <param name="keyword1">单号，操作员，函数，功能模块</param>
        /// <param name="keyword2">其他关键字</param>
        /// <param name="page_no"></param>
        /// <param name="page_size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        DataTable ISysLogBLL.GetSysLogList(string date1, string date2, string log_type, string log_level, string oper_id, string keyword1, string keyword2, int page_no, int page_size, out int total)
        {
            var condition_sql = "";
            if (!string.IsNullOrEmpty(log_type))
            {
                condition_sql += " and log_type=@log_type ";
            }
            if (!string.IsNullOrEmpty(log_level))
            {
                condition_sql += " and log_level=@log_level ";
            }
            if (!string.IsNullOrEmpty(oper_id))
            {
                condition_sql += " and oper_id=@oper_id ";
            }
            if (!string.IsNullOrEmpty(keyword1))
            {
                condition_sql += " and (func_id like '%'+@keyword1+'%' or module_name like '%'+@keyword1+'%' or memo like '%'+@keyword1+'%' ) ";
            }
            if (!string.IsNullOrEmpty(keyword2))
            {
                condition_sql += " and (log_info like '%'+@keyword2+'%') ";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "SELECT t.*,m.oper_name FROM (SELECT ROW_NUMBER() OVER(ORDER BY flow_id desc) AS rowid,* ";
            sql += "FROM sys_t_operator_log where convert(varchar(19),oper_date,120)>=@date1 and convert(varchar(19),oper_date,120)<=@date2 " + condition_sql + ") t ";
            sql += "left join sa_t_operator_i m on t.oper_id=m.oper_id ";
            sql += "WHERE t.rowid > " + (page_no - 1) * page_size + " AND t.rowid <= " + page_no * page_size;
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@date1", date1),
                new System.Data.SqlClient.SqlParameter("@date2", date2),
                new System.Data.SqlClient.SqlParameter("@log_type", log_type),
                new System.Data.SqlClient.SqlParameter("@log_level", log_level),
                new System.Data.SqlClient.SqlParameter("@oper_id", oper_id),
                new System.Data.SqlClient.SqlParameter("@keyword1", keyword1),
                new System.Data.SqlClient.SqlParameter("@keyword2", keyword2),
            };
            var dt = db.ExecuteToTable(sql, pars);

            sql = "select count(*) as total from sys_t_operator_log ";
            sql += "where convert(varchar(19),oper_date,120)>=@date1 and convert(varchar(19),oper_date,120)<=@date2 " + condition_sql + " ";
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@date1", date1),
                new System.Data.SqlClient.SqlParameter("@date2", date2),
                new System.Data.SqlClient.SqlParameter("@log_type", log_type),
                new System.Data.SqlClient.SqlParameter("@log_level", log_level),
                new System.Data.SqlClient.SqlParameter("@oper_id", oper_id),
                new System.Data.SqlClient.SqlParameter("@keyword1", keyword1),
                new System.Data.SqlClient.SqlParameter("@keyword2", keyword2),
            };
            var dt2 = db.ExecuteToTable(sql, pars);
            total = Helper.Conv.ToInt(dt2.Rows[0]["total"]);

            return dt;

        }

        /// <summary>
        /// 日志类型
        /// </summary>
        /// <param name="log_tag">log_type; log_level</param>
        /// <returns></returns>
        DataTable ISysLogBLL.GetSysLogType(string log_tag)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sys_t_operator_log_cls where log_tag=@log_tag ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@log_tag", log_tag)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;

        }
    }
}