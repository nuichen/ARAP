using System;
using System.Collections.Generic;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class FYOrder : IFYOrder
    {

        System.Data.DataTable IFYOrder.GetList(DateTime date1, DateTime date2, string visa_id)
        {
            string sql = @"
select a.sheet_no,a.voucher_no,a.branch_no,branch.branch_name,
(a.branch_no +'/'+ branch.branch_name) as branch_no_a,
a.visa_id,bank.visa_nm as visa_name,
(a.visa_id+'/'+bank.visa_nm) as visa_id_a,
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
left join bi_t_payment_info payment on a.pay_way=payment.pay_way
left join bi_t_people_info people on a.deal_man=people.oper_id
left join sa_t_operator_i oper on a.oper_id=oper.oper_id
left join sa_t_operator_i man on a.approve_man=man.oper_id";
            sql += " where sheet_no like 'SR%'";
            sql += " and a.oper_date>='" + date1.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            sql += " and a.oper_date<='" + date2.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            if (visa_id != "")
            {
                sql += " and a.visa_id='" + visa_id + "'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        void IFYOrder.GetOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            string sql = @"
select a.sheet_no,a.voucher_no,a.branch_no,branch.branch_name,
(a.branch_no +'/'+ branch.branch_name) as branch_no_a,
a.visa_id,bank.visa_nm as visa_name,
(a.visa_id+'/'+bank.visa_nm) as visa_id_a,
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
left join bi_t_payment_info payment on a.pay_way=payment.pay_way
left join bi_t_people_info people on a.deal_man=people.oper_id
left join sa_t_operator_i oper on a.oper_id=oper.oper_id
left join sa_t_operator_i man on a.approve_man=man.oper_id";
            sql += " where a.sheet_no='" + sheet_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            tb1 = db.ExecuteToTable(sql, null);

            sql = @"select a.sheet_no,a.flow_id,a.type_no,b.pay_name as type_name,b.account_flag as type_flag,
a.bill_cash,a.memo from bank_t_cash_detail a
left join bi_t_sz_type b on a.type_no=b.pay_way";
            sql += " where a.sheet_no='" + sheet_no + "'";
            sql += " order by flow_id";
            tb2 = db.ExecuteToTable(sql, null);
        }

        string IFYOrder.MaxCode()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string front_str = "SR";
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

        void IFYOrder.Add(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines, out string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            IFYOrder order = this;
            try
            {
                db.Open();
                db.BeginTran();
                //
                sheet_no = ord.sheet_no = order.MaxCode();
                string sql = "select * from bank_t_cash_master where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count != 0)
                {
                    throw new Exception("已存在单号" + ord.sheet_no);
                }
                d.Insert(ord);
                foreach (Model.bank_t_cash_detail line in lines)
                {
                    sql = "select isnull(max(flow_id)+1,1) from bank_t_cash_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = sheet_no;
                    d.Insert(line);
                }
                //
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

        void IFYOrder.Change(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines)
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
                sql = "delete from bank_t_cash_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from bank_t_cash_master where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                d.Insert(ord);
                foreach (Model.bank_t_cash_detail line in lines)
                {
                    sql = "select isnull(max(flow_id)+1,1) from bank_t_cash_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    d.Insert(line);
                }
                //
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

        void IFYOrder.Delete(string sheet_no)
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
                sql = "delete from bank_t_cash_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from bank_t_cash_master where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
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

        void IFYOrder.Check(string sheet_no, string approve_man)
        {
            CheckSheet check = new CheckSheet();
            check.CheckGlSheet(sheet_no, approve_man, DateTime.Now);
        }
    }
}
