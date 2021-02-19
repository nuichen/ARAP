using System;
using System.Data;
using System.Collections.Generic;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class FinanceBLL : IFinanceBLL
    {
        DataTable IFinanceBLL.GetSZTypeList(string is_show_stop)
        {
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            //var condition_sql = " and other2='1'";
            //if (is_show_stop == "1") condition_sql = "";
            string sql = "select bi_t_sz_type.*,b.subject_name from bi_t_sz_type left join bi_t_subject_info b on b.subject_no=bi_t_sz_type.other1  order by pay_way ";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        DataTable IFinanceBLL.GetSZTypeItem(string pay_way)
        {
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@pay_way", pay_way)
            };
            string sql = "select top 1 * from bi_t_sz_type where pay_way=@pay_way  ";
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        void IFinanceBLL.InsertSZType(Model.bi_t_sz_type item)
        {
            try
            {
                DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select 1 from bi_t_sz_type where pay_way=@pay_way ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@pay_way", item.pay_way)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0)
                {
                    throw new Exception("收支类型编号已存在");
                }
                sql = "select 1 from bi_t_sz_type where pay_name=@pay_name ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@pay_name", item.pay_name)
                };
                dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0)
                {
                    throw new Exception("收支类型名称已存在");
                }
                sql = "insert into bi_t_sz_type(pay_way,pay_name,pay_flag,km_code,pay_kind,other1,other2,num1,num2,pay_memo,max_change,if_acc,path,is_account";
                sql += ",account_flag,is_pay,is_profit,profit_type,auto_cashsheet,if_CtFix,update_time) values(@pay_way,@pay_name,@pay_flag,@km_code,@pay_kind,@other1";
                sql += ",@other2,@num1,@num2,@pay_memo,0,@if_acc,@path,@is_account,@account_flag,@is_pay,@is_profit,@profit_type,@auto_cashsheet,@if_CtFix,@update_time)";

                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@pay_way", item.pay_way),
                    new System.Data.SqlClient.SqlParameter("@pay_name", item.pay_name),
                    new System.Data.SqlClient.SqlParameter("@pay_flag", item.pay_flag),
                    new System.Data.SqlClient.SqlParameter("@km_code", item.km_code),
                    new System.Data.SqlClient.SqlParameter("@pay_kind", item.pay_kind),
                    new System.Data.SqlClient.SqlParameter("@other1", item.other1),
                    new System.Data.SqlClient.SqlParameter("@other2", item.other2),
                    new System.Data.SqlClient.SqlParameter("@num1", item.num1),
                    new System.Data.SqlClient.SqlParameter("@num2", item.num2),
                    new System.Data.SqlClient.SqlParameter("@pay_memo", item.pay_memo),
                    new System.Data.SqlClient.SqlParameter("@if_acc", item.if_acc),
                    new System.Data.SqlClient.SqlParameter("@path", item.path),
                    new System.Data.SqlClient.SqlParameter("@is_account", item.is_account),
                    new System.Data.SqlClient.SqlParameter("@account_flag", item.account_flag),
                    new System.Data.SqlClient.SqlParameter("@is_pay", item.is_pay),
                    new System.Data.SqlClient.SqlParameter("@is_profit", item.is_profit),
                    new System.Data.SqlClient.SqlParameter("@profit_type", item.profit_type),
                    new System.Data.SqlClient.SqlParameter("@auto_cashsheet", item.auto_cashsheet),
                    new System.Data.SqlClient.SqlParameter("@if_CtFix", item.if_CtFix),
                    new System.Data.SqlClient.SqlParameter("@update_time", DateTime.Now)
                };
                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("FinanceBLL.InsertSZType()", ex.ToString(), item.pay_way, item.pay_name);
                throw new Exception("新建收支类型异常[" + ex.Message + "]");
            }
        }

        void IFinanceBLL.UpdateSZType(Model.bi_t_sz_type item)
        {
            try
            {
                DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select * from bi_t_sz_type where pay_way ='" + item.pay_way + "' and num2=1 ";
                DataTable dt2 = db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0)
                {
                    throw new Exception("该收支类型是系统内部引用的，不可删除。");
                }
                sql = "select 1 from bi_t_sz_type where pay_name=@pay_name and pay_way<>@pay_way ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@pay_way", item.pay_way),
                    new System.Data.SqlClient.SqlParameter("@pay_name", item.pay_name)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0)
                {
                    throw new Exception("收支类型名称已存在");
                }
                sql = "update bi_t_sz_type set pay_name=@pay_name,pay_flag=@pay_flag,km_code=@km_code,pay_kind=@pay_kind,other1=@other1,other2=@other2";
                sql += ",num1=@num1,num2=@num2,pay_memo=@pay_memo,if_acc=@if_acc,path=@path,is_account=@is_account,account_flag=@account_flag,is_pay=@is_pay";
                sql += ",is_profit=@is_profit,profit_type=@profit_type,auto_cashsheet=@auto_cashsheet,if_CtFix=@if_CtFix,update_time=@update_time where pay_way=@pay_way ";

                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@pay_way", item.pay_way),
                    new System.Data.SqlClient.SqlParameter("@pay_name", item.pay_name),
                    new System.Data.SqlClient.SqlParameter("@pay_flag", item.pay_flag),
                    new System.Data.SqlClient.SqlParameter("@km_code", item.km_code),
                    new System.Data.SqlClient.SqlParameter("@pay_kind", item.pay_kind),
                    new System.Data.SqlClient.SqlParameter("@other1", item.other1),
                    new System.Data.SqlClient.SqlParameter("@other2", item.other2),
                    new System.Data.SqlClient.SqlParameter("@num1", item.num1),
                    new System.Data.SqlClient.SqlParameter("@num2", item.num2),
                    new System.Data.SqlClient.SqlParameter("@pay_memo", item.pay_memo),
                    new System.Data.SqlClient.SqlParameter("@if_acc", item.if_acc),
                    new System.Data.SqlClient.SqlParameter("@path", item.path),
                    new System.Data.SqlClient.SqlParameter("@is_account", item.is_account),
                    new System.Data.SqlClient.SqlParameter("@account_flag", item.account_flag),
                    new System.Data.SqlClient.SqlParameter("@is_pay", item.is_pay),
                    new System.Data.SqlClient.SqlParameter("@is_profit", item.is_profit),
                    new System.Data.SqlClient.SqlParameter("@profit_type", item.profit_type),
                    new System.Data.SqlClient.SqlParameter("@auto_cashsheet", item.auto_cashsheet),
                    new System.Data.SqlClient.SqlParameter("@if_CtFix", item.if_CtFix),
                    new System.Data.SqlClient.SqlParameter("@update_time", DateTime.Now)
                };
                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("FinanceBLL.UpdateSZType()", ex.ToString(), item.pay_way, item.pay_name);
                throw new Exception("修改收支类型异常[" + ex.Message + "]");
            }
        }

        void IFinanceBLL.DeleteSZType(string pay_way) 
        {
            try
            {
               
                DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select * from bi_t_sz_type where pay_way ='" + pay_way + "' and num2=1 ";
                DataTable dt2 = db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0)
                {
                    throw new Exception("该收支类型是系统内部引用的，不可删除。");
                }
                sql = "select * from bank_t_cash_detail where type_no ='"+pay_way+"'";
                DataTable dt = db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    throw new Exception("该收支类型已经在其他收入支出中引用，不可删除。");
                }
                sql = "select * from rp_t_supcust_fy_detail where kk_no ='" + pay_way + "'";
                DataTable dt1 = db.ExecuteToTable(sql, null);
                if (dt1.Rows.Count > 0)
                {
                    throw new Exception("该收支类型已经在其他应收应付中引用，不可删除。");
                }
                sql = "delete from bi_t_sz_type where pay_way=@pay_way ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@pay_way", pay_way),
                    new System.Data.SqlClient.SqlParameter("@update_time", DateTime.Now)
                };
                db.ExecuteScalar(sql, pars);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("FinanceBLL.DeleteSZType()", ex.ToString(), pay_way);
                throw new Exception("删除收支类型异常[" + ex.Message + "]");
            }
        }

        DataTable IFinanceBLL.GetIncomeRevenueList(string supcust_no, string month)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from rp_t_supcust_income_revenue where supcust_no=@supcust_no and convert(varchar(7),in_date,120)=@month order by in_date asc ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_no", supcust_no),
                new System.Data.SqlClient.SqlParameter("@month", month)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        void IFinanceBLL.SaveIncomeRevenue(string supcust_no, List<Model.rp_t_supcust_income_revenue> details, string oper_id, string month)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                string sql = "delete from rp_t_supcust_income_revenue where supcust_no=@supcust_no and convert(varchar(7),in_date,120)=@month ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@supcust_no", supcust_no),
                    new System.Data.SqlClient.SqlParameter("@month", month)
                };
                d.ExecuteScalar(sql, pars);

                string sql2 = "insert into rp_t_supcust_income_revenue(flow_id,supcust_no,in_date,in_amount,oper_id,oper_date) ";
                sql2 += "values(@flow_id,@supcust_no,@in_date,@in_amount,@oper_id,getdate()) ";
                int index = 0;
                foreach (var detail in details)
                {
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@flow_id", ++index),
                        new System.Data.SqlClient.SqlParameter("@supcust_no", supcust_no),
                        new System.Data.SqlClient.SqlParameter("@in_date", detail.in_date.ToString("yyyy-MM-dd")),
                        new System.Data.SqlClient.SqlParameter("@in_amount", detail.in_amount),
                        new System.Data.SqlClient.SqlParameter("@oper_id", oper_id)
                    };
                    d.ExecuteScalar(sql2, pars);
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("SaveIncomeRevenue()", ex.ToString(), supcust_no, month);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
