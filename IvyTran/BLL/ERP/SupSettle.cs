using System;
using System.Collections.Generic;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public  class SupSettle : ISupSettle
    {

        System.Data.DataTable ISupSettle.GetList(DateTime date1, DateTime date2, string sup_no)
        {
            string sql = @"
select a.sheet_no,left(a.sheet_no,2) as sheet_type,a.supcust_no as sup_no,sup.sup_name,
(a.supcust_no+'/'+sup.sup_name) as sup_no_a,
a.flag_post,a.total_amount,a.free_money,
a.coin_no,a.coin_rate,
a.pay_way,payment.pay_name,
(a.pay_way+'/'+payment.pay_name) as pay_way_a,
a.approve_flag,
a.oper_id,oper.oper_name,
(a.oper_id+'/'+oper.oper_name) as oper_id_a,
a.oper_date,
a.deal_man,
people.oper_name as deal_man_name,
(a.deal_man+'/'+people.oper_name) as deal_man_a,
a.approve_man,
a.approve_date,
a.other1,a.other2,a.other3,
a.visa_id,
bank.visa_nm as visa_name,
(a.visa_id+'/'+bank.visa_nm) as visa_id_a,
a.num1,a.num2,a.num3,a.cm_branch,
a.branch_no,
branch.branch_name,
(a.branch_no+'/'+branch.branch_name) as branch_no_a,
a.from_date,a.to_date,
a.rc_sheet_no
from rp_t_recpay_record_info a
left join (select * from bi_t_supcust_info where supcust_flag='S') sup on a.supcust_no=sup.supcust_no
left join bi_t_payment_info payment on a.pay_way=payment.pay_way
left join sa_t_operator_i oper on a.oper_id=oper.oper_id
left join bi_t_people_info people on a.deal_man=people.oper_id
left join bi_t_bank_info bank on a.visa_id=bank.visa_id
left join bi_t_branch_info branch on a.branch_no=branch.branch_no";
            sql += " where a.sheet_no like 'RP%'";
            sql += " and a.oper_date>='" + date1.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            sql += " and a.oper_date<='" + date2.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            if (sup_no != "")
            {
                sql += " and a.supcust_no='" + sup_no + "'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        void ISupSettle.GetOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            string sql = @"
select a.sheet_no,a.supcust_no as sup_no,sup.sup_name,
(a.supcust_no+'/'+sup.sup_name) as sup_no_a,
a.flag_post,a.total_amount,a.free_money,
a.coin_no,a.coin_rate,
a.pay_way,payment.pay_name,
(a.pay_way+'/'+payment.pay_name) as pay_way_a,
a.approve_flag,
a.oper_id,oper.oper_name,
(a.oper_id+'/'+oper.oper_name) as oper_id_a,
a.oper_date,
a.deal_man,
people.oper_name as deal_man_name,
(a.deal_man+'/'+people.oper_name) as deal_man_a,
a.approve_man,
o2.oper_name AS approve_man_name,
a.approve_date,
a.other1,a.other2,a.other3,
a.visa_id,
bank.visa_nm as visa_name,
(a.visa_id+'/'+bank.visa_nm) as visa_id_a,
a.num1,a.num2,a.num3,a.cm_branch,
a.branch_no,
branch.branch_name,
(a.branch_no+'/'+branch.branch_name) as branch_no_a,
a.from_date,a.to_date,
a.rc_sheet_no
from rp_t_recpay_record_info a
left join (select * from bi_t_supcust_info where supcust_flag='S') sup on a.supcust_no=sup.supcust_no
left join bi_t_payment_info payment on a.pay_way=payment.pay_way
left join sa_t_operator_i oper on a.oper_id=oper.oper_id
LEFT JOIN dbo.sa_t_operator_i o2 ON a.approve_man=o2.oper_id
left join bi_t_people_info people on a.deal_man=people.oper_id
left join bi_t_bank_info bank on a.visa_id=bank.visa_id
left join bi_t_branch_info branch on a.branch_no=branch.branch_no";
            sql += " where a.sheet_no ='" + sheet_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            tb1 = db.ExecuteToTable(sql, null);
            if (tb1.Rows.Count == 0)
            {
                throw new Exception("不存在供应商结算单" + sheet_no);
            }
            //
            sql = @"select a.flow_no,a.sheet_no,a.voucher_no,left(a.voucher_no,2) as voucher_first,a.sheet_amount,
a.paid_amount,a.paid_free,isnull(a.sheet_amount,0)-isnull(a.paid_amount,0)-isnull(a.paid_free,0) as yf_amount, a.pay_amount,a.pay_free,
a.memo,a.other1,a.other2,a.num1,a.num2,a.num3,
a.pay_date,a.path,a.select_flag,a.voucher_type,
a.oper_date,a.voucher_other1,a.voucher_other2,a.order_no
from rp_t_recpay_record_detail a";
            sql += " where a.sheet_no='" + sheet_no + "'";
            sql += " order by a.flow_no";
            tb2 = db.ExecuteToTable(sql, null);
        }

        string ISupSettle.MaxCode()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string front_str = "RP";
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

        void ISupSettle.Add(Model.rp_t_recpay_record_info ord, List<Model.rp_t_recpay_record_detail> lines, out string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                ISupSettle supbll = this;
                sheet_no = ord.sheet_no = supbll.MaxCode();
                //
                string sql = "select * from rp_t_recpay_record_info where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count != 0)
                {
                    throw new Exception("已存在单号" + ord.sheet_no);
                }
                d.Insert(ord);
                foreach (Model.rp_t_recpay_record_detail line in lines)
                {
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_recpay_record_detail";
                    line.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
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

        void ISupSettle.Change(Model.rp_t_recpay_record_info ord, List<Model.rp_t_recpay_record_detail> lines)
        {
            string sql = "select * from rp_t_recpay_record_info where sheet_no='" + ord.sheet_no + "'";
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
                sql = "delete from rp_t_recpay_record_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_recpay_record_info where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                d.Insert(ord);
                foreach (Model.rp_t_recpay_record_detail line in lines)
                {
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_recpay_record_detail";
                    line.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
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

        void ISupSettle.Delete(string sheet_no)
        {
            string sql = "select * from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'";
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
                sql = "delete from rp_t_recpay_record_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ISupSettle.Check(string sheet_no, string approve_man)
        {
            CheckSheet check = new CheckSheet();
            check.CheckSupJSSheet(sheet_no, approve_man, DateTime.Now);
        }

        string GetSheetCode(DB.IDB db, string front_str)
        {
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

    }

}
