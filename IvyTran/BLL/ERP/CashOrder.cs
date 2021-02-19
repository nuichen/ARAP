using System;
using System.Data;
using System.Dynamic;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
   public class CashOrder : ICashOrder
    {
        System.Data.DataTable ICashOrder.GetList(DateTime date1, DateTime date2, string visa_id)
        {
            string sql = @"
select a.sheet_no,a.voucher_no,a.branch_no,branch.branch_name,
(a.branch_no +'/'+ branch.branch_name) as branch_no_a,
a.visa_id,bank.visa_nm as visa_name,
(a.visa_id+'/'+bank.visa_nm) as visa_id_a,
a.visa_in,bank2.visa_nm as bank_in_name,
(a.visa_in+'/'+bank2.visa_nm) as visa_in_a,
a.pay_way,payment.pay_name,
(a.pay_way+'/'+payment.pay_name),
a.deal_man,people.oper_name as deal_man_name,
(a.deal_man+'/'+people.oper_name) as deal_man_a,
a.oper_id,oper.oper_name,
(a.oper_id+'/'+oper.oper_name) as oper_id_a,
a.oper_date,a.bill_total,
a.approve_flag,
a.approve_man,man.oper_name as approve_man_name,
(a.approve_man+'/'+man.oper_name) as approve_man_a,
a.approve_date,
a.other1,a.other2,a.other3,
a.num1,a.num2,a.num3
from bank_t_cash_master a
left join bi_t_branch_info branch on a.branch_no=branch.branch_no
left join bi_t_bank_info bank on a.visa_id=bank.visa_id
left join bi_t_bank_info bank2 on a.visa_in=bank2.visa_id
left join bi_t_payment_info payment on a.pay_way=payment.pay_way
left join bi_t_people_info people on a.deal_man=people.oper_id
left join sa_t_operator_i oper on a.oper_id=oper.oper_id
left join sa_t_operator_i man on a.approve_man=man.oper_id";
            sql += " where sheet_no like 'BK%'";
            sql += " and a.oper_date>='" + date1.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            sql += " and a.oper_date<='" + date2.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            if (visa_id != "")
            {
                sql += " and (a.visa_id='" + visa_id + "' or a.visa_in='" + visa_id + "')";
            }
            sql += " order by a.oper_date desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        void ICashOrder.GetOrder(string sheet_no, out System.Data.DataTable tb1)
        {
            string sql = @"
select top(1) a.sheet_no,a.voucher_no,a.branch_no,branch.branch_name,
(a.branch_no +'/'+ branch.branch_name) as branch_no_a,
a.visa_id,bank.visa_nm as visa_name,
(a.visa_id+'/'+bank.visa_nm) as visa_id_a,
a.visa_in,bank2.visa_nm as bank_in_name,
(a.visa_in+'/'+bank2.visa_nm) as visa_in_a,
a.pay_way,payment.pay_name,
(a.pay_way+'/'+payment.pay_name),
a.deal_man,people.oper_name as deal_man_name,
(a.deal_man+'/'+people.oper_name) as deal_man_a,
a.oper_id,oper.oper_name,
(a.oper_id+'/'+oper.oper_name) as oper_id_a,
a.oper_date,a.bill_total,
a.approve_flag,
a.approve_man,man.oper_name as approve_man_name,
(a.approve_man+'/'+man.oper_name) as approve_man_a,
a.approve_date,
a.other1,a.other2,a.other3,
a.num1,a.num2,a.num3
from bank_t_cash_master a
left join bi_t_branch_info branch on a.branch_no=branch.branch_no
left join bi_t_bank_info bank on a.visa_id=bank.visa_id
left join bi_t_bank_info bank2 on a.visa_in=bank.visa_id
left join bi_t_payment_info payment on a.pay_way=payment.pay_way
left join bi_t_people_info people on a.deal_man=people.oper_id
left join sa_t_operator_i oper on a.oper_id=oper.oper_id
left join sa_t_operator_i man on a.approve_man=man.oper_id";
            sql += " where sheet_no='" + sheet_no + "'";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            tb1 = db.ExecuteToTable(sql, null);

        }

        string ICashOrder.MaxCode()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string front_str = "BK";
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + front_str + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                index += 1;
                if (index > 9999)
                {
                    index = 0;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + front_str + "'";
                db.ExecuteScalar(sql, null);
                return front_str + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        void ICashOrder.Add(Model.bank_t_cash_master ord, out string sheet_no)
        {
            ICashOrder ins = this;
            ord.sheet_no = ins.MaxCode();
            //
            string sql = "select * from bank_t_cash_master where sheet_no='" + ord.sheet_no + "'";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count != 0)
                {
                    throw new Exception("已存在单号" + ord.sheet_no);
                }
                d.Insert(ord);

                //
                db.CommitTran();
                LogHelper.writeSheetLog("CashOrder ->Add()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING", ord.sheet_no);
                //LogHelper.writeSheetLog("CashOrder ->Add()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashOrder ->Add()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
            sheet_no = ord.sheet_no;
        }

        void ICashOrder.Change(Model.bank_t_cash_master ord)
        {
            string sql = "select * from bank_t_cash_master where sheet_no='" + ord.sheet_no + "'";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + ord.sheet_no);
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核" + ord.sheet_no);
                    }
                }

                sql = "delete from bank_t_cash_master where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                d.Insert(ord);
                //
                db.CommitTran();
                LogHelper.writeSheetLog("CashOrder ->Change()", "更改成功！", SessionHelper.oper_id, "操作日志", "WARNING", ord.sheet_no);
                //LogHelper.writeSheetLog("CashOrder ->Change()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashOrder ->Change()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ICashOrder.Delete(string sheet_no)
        {
            string sql = "select * from bank_t_cash_master where sheet_no='" + sheet_no + "'";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核" + sheet_no);
                    }
                }

                sql = "delete from bank_t_cash_master where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
                LogHelper.writeSheetLog("CashOrder ->Delete()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashOrder ->Delete()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashOrder ->Delete()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ICashOrder.Check(string sheet_no, string approve_man)
        {
            //CheckSheet check = new CheckSheet();
            //check.CheckBankSheet(sheet_no, approve_man, DateTime.Now);
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();

                //
                sql = "select * from bank_t_cash_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                else if (tb.Rows[0]["approve_flag"].ToString() == "1")
                {
                    throw new Exception("单据已审核" + sheet_no);
                }
                sql = "update bank_t_cash_master set approve_flag='1',approve_man='" + approve_man +
                    "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //bank_t_cash_master master = d.ExecuteToModel<bank_t_cash_master>("select * from bank_t_cash_master where sheet_no='" + sheet_no + "'", null);
                //DataTable tbDetail = d.ExecuteToTable("select * from bank_t_cash_detail where sheet_no='" + sheet_no + "' ", null);

                //dynamic dyn = new ExpandoObject();

                //dyn.visa_id = master.visa_id;
                //dyn.pay_amount = master.bill_total;
                //dyn.memo = master.other1;

                ////转出
                ////CashBalance(d, dyn);

                ////转入
                //dyn.visa_id = master.visa_in;
                //dyn.pay_amount = -master.bill_total;
                ////CashBalance(d, dyn);


                db.CommitTran();
                LogHelper.writeSheetLog("CashOrder ->Check()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashOrder ->Check()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashOrder ->Check()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void ICashOrder.NotCheck(string sheet_no, string approve_man)
        {
            //CheckSheet check = new CheckSheet();
            //check.CheckBankSheet(sheet_no, approve_man, DateTime.Now);
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();

                //
                sql = "select * from bank_t_cash_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                sql = "update bank_t_cash_master set approve_flag='0',approve_man='',approve_date=null where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //bank_t_cash_master master = d.ExecuteToModel<bank_t_cash_master>("select * from bank_t_cash_master where sheet_no='" + sheet_no + "'", null);
                //DataTable tbDetail = d.ExecuteToTable("select * from bank_t_cash_detail where sheet_no='" + sheet_no + "' ", null);

                //dynamic dyn = new ExpandoObject();

                //dyn.visa_id = master.visa_id;
                //dyn.pay_amount = master.bill_total;
                //dyn.memo = master.other1;

                ////转出
                ////CashBalance(d, dyn);

                ////转入
                //dyn.visa_id = master.visa_in;
                //dyn.pay_amount = -master.bill_total;
                ////CashBalance(d, dyn);


                db.CommitTran();
                LogHelper.writeSheetLog("CashOrder ->NotCheck()", "反审成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashOrder ->Check()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashOrder ->NotCheck()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
