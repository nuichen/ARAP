using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model.PaymentModel;

namespace IvyTran.BLL.ERP
{
    public class CusSettle : ICusSettle
    {
        //public CusSettle()
        //{
        //    if (SessionHelper.oper_id == null || Conv.ToString(SessionHelper.oper_id) == "")
        //    {
        //        throw new Exception("登录超时！");
        //    }
        //}

        System.Data.DataTable ICusSettle.GetList(DateTime date1, DateTime date2, string cus_no, string is_cs)
        {
            string sql = "";
            if (is_cs == "C")
            {
                sql = @"
select a.sheet_no,left(a.sheet_no,2) as sheet_type,a.supcust_no as cus_no,cus.sup_name as cus_name,
(a.supcust_no+'/'+cus.sup_name) as cus_no_a,
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
a.approve_man,man.oper_name,
a.approve_date,
(a.approve_man+'/'+man.oper_name) as approve_man_a,
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
left join (select * from bi_t_supcust_info where supcust_flag='C') cus on a.supcust_no=cus.supcust_no
left join bi_t_payment_info payment on a.pay_way=payment.pay_way
left join sa_t_operator_i oper on a.oper_id=oper.oper_id
left join sa_t_operator_i man on a.approve_man=man.oper_id
left join bi_t_people_info people on a.deal_man=people.oper_id
left join bi_t_bank_info bank on a.visa_id=bank.visa_id
left join bi_t_branch_info branch on a.branch_no=branch.branch_no
 where a.sheet_no like 'CP%'";
            }
            else
            {
                sql = @"
select a.sheet_no,left(a.sheet_no,2) as sheet_type,a.supcust_no as cus_no,sup.sup_name as cus_name,
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
a.approve_date,man.oper_name,
(a.approve_man+'/'+man.oper_name) as approve_man_a,
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
left join sa_t_operator_i man on a.approve_man=man.oper_id
left join bi_t_people_info people on a.deal_man=people.oper_id
left join bi_t_bank_info bank on a.visa_id=bank.visa_id
left join bi_t_branch_info branch on a.branch_no=branch.branch_no";
                sql += " where a.sheet_no like 'RP%'";

            }
            sql += "and a.oper_date>='" + date1.ToString("yyyy-MM-dd ") + "'";
            sql += "and a.oper_date<='" + date2.ToString("yyyy-MM-dd ") + "'";
            if (cus_no != "")
            {
                sql += " and a.supcust_no='" + cus_no + "'";
            }
            sql += "order by a.oper_date desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        DataTable ICusSettle.GetFYList(string supcust_no, string supcust_flag)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"select *,isnull(sum(p.paid_amount),0.00) 已付金额,isnull(sum(p.free_money),0.00) 已免付金额
from rp_t_supcust_fy_master m
left join rp_t_accout_payrec_flow p on p.voucher_no=m.sheet_no
where ((p.paid_amount+p.free_money)<m.total_amount or p.voucher_no is null)
and m.supcust_no='@supcust_no'
and m.supcust_flag='@supcust_flag'
group by m.sheet_no,m.supcust_no,m.supcust_flag,m.pay_type,m.pay_date,m.old_no,m.oper_id,m.oper_date,m.approve_flag,m.approve_man,m.approve_date,m.is_payed,m.sale_man,m.branch_no,m.cm_branch,m.max_change,m.other1,m.other2,m.other3,m.num1,m.num2,m.num3,m.visa_id,m.is_over,m.total_amount,m.paid_amount,m.pay_way,m.pay_name,p.flow_no,p.pay_type,p.voucher_no,p.trans_no,p.sheet_amount,p.discount,p.paid_amount,p.tax_amount,p.pay_way,p.pay_date,p.supcust_no,p.supcust_flag,p.memo,p.other1,p.other2,p.other3,p.num1,p.num2,p.num3,p.branch_no,p.sale_no,p.oper_date,p.free_money
 "
                .Replace("@supcust_flag", supcust_flag)
                .Replace("@supcust_no", supcust_no);

            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        void ICusSettle.GetOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            string sql = "";
            if (is_cs == "C")
            {
                sql = @"
select a.sheet_no,a.supcust_no as cus_no,cus.sup_name as cus_name,
(a.supcust_no+'/'+cus.sup_name) as cus_no_a,
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
o2.oper_name approve_man_name,
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
a.rc_sheet_no,
a.num1,a.num2
from rp_t_recpay_record_info a
left join (select * from bi_t_supcust_info where supcust_flag='C') cus on a.supcust_no=cus.supcust_no
left join bi_t_payment_info payment on a.pay_way=payment.pay_way
left join sa_t_operator_i oper on a.oper_id=oper.oper_id
left join bi_t_people_info people on a.deal_man=people.oper_id
left join sa_t_operator_i o2 on o2.oper_id=a.approve_man
left join bi_t_bank_info bank on a.visa_id=bank.visa_id
left join bi_t_branch_info branch on a.branch_no=branch.branch_no";
            }
            else
            {
                sql = @"
select a.sheet_no,a.supcust_no as cus_no,sup.sup_name as cus_name,
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
            }
            sql += " where a.sheet_no='" + sheet_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            tb1 = db.ExecuteToTable(sql, null);
            //
            sql = @"select a.flow_no,a.sheet_no,a.voucher_no,left(a.voucher_no,2) as voucher_first, a.sheet_amount,
a.paid_amount,a.paid_free,isnull(a.sheet_amount,0)-isnull(a.paid_amount,0)-isnull(a.paid_free,0) as yf_amount,a.pay_amount,a.pay_free,
a.memo,a.other1,a.other2,a.num1,a.num2,a.num3,
a.pay_date,a.path,a.select_flag,a.voucher_type,
a.oper_date,a.voucher_other1,a.voucher_other2,a.order_no
from rp_t_recpay_record_detail a";
            sql += " where a.sheet_no='" + sheet_no + "'";
            sql += " order by a.flow_no";
            tb2 = db.ExecuteToTable(sql, null);
        }

        string ICusSettle.MaxCode()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string front_str = "CP";
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
        public string MaxCode1()
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
        void ICusSettle.Add(Model.rp_t_recpay_record_info ord, List<Model.rp_t_recpay_record_detail> lines, DataTable dt, string is_cs, out string sheet_no)
        {
            ICusSettle ins = this;
            //

            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();
                if (is_cs == "C")
                    ord.sheet_no = sheet_no = ins.MaxCode();
                else
                    ord.sheet_no = sheet_no = MaxCode1();
                //
                sql = "select * from rp_t_recpay_record_info where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count != 0)
                {
                    throw new Exception("已存在单号" + ord.sheet_no);
                }
                d.Insert(ord);
                decimal num = 0;
                foreach (Model.rp_t_recpay_record_detail line in lines)
                {
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_recpay_record_detail";
                    line.sheet_no = sheet_no;
                    line.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    num += line.pay_amount;
                    d.Insert(line);
                }
                foreach (DataRow l in dt.Rows)
                {

                    if (Helper.Conv.ToDecimal(l["total_amount"]) > 0 && !string.IsNullOrEmpty(Helper.Conv.ToString(l["pay_name"])))
                    {
                        rp_t_collection_way cw = new rp_t_collection_way();

                        cw.sheet_no = sheet_no;
                        cw.pay_way = Helper.Conv.ToString(l["pay_name"]).Trim().Split('/')[0];
                        cw.total_amount = Helper.Conv.ToDecimal(l["total_amount"]);
                        d.Insert(cw);
                    }

                }
                //
                db.CommitTran();
                LogHelper.writeSheetLog("CusSettle ->Add()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CusSettle ->SaveAgingGroup()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CusSettle ->Add()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ICusSettle.Change(Model.rp_t_recpay_record_info ord, List<Model.rp_t_recpay_record_detail> lines, DataTable dt)
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
                sql = "delete from rp_t_collection_way where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "delete from rp_t_accout_payrec_flow where voucher_no='" + ord.sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                //
                d.Insert(ord);
                //decimal num = 0;
                foreach (Model.rp_t_recpay_record_detail line in lines)
                {
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_recpay_record_detail";
                    line.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    // num += line.pay_amount;
                    d.Insert(line);
                }



                foreach (DataRow l in dt.Rows)
                {

                    if (Helper.Conv.ToDecimal(l["total_amount"]) > 0 && !string.IsNullOrEmpty(Helper.Conv.ToString(l["pay_name"])))
                    {
                        rp_t_collection_way cw = new rp_t_collection_way();

                        cw.sheet_no = ord.sheet_no;
                        cw.pay_way = Helper.Conv.ToString(l["pay_name"]).Trim().Split('/')[0];
                        cw.total_amount = Helper.Conv.ToDecimal(l["total_amount"]);
                        d.Insert(cw);
                    }

                }
                //Model.rp_t_accout_payrec_flow it = new Model.rp_t_accout_payrec_flow();
                //sql = "select isnull(max(flow_no),0)+1 from rp_t_accout_payrec_flow";
                //it.flow_no = IvyTran.Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));

                //it.pay_type = -1;
                //it.voucher_no = ord.sheet_no;
                //if (ord.supcust_flag == "C")
                //    it.trans_no = "CP";
                //else
                //    it.trans_no = "RP";
                //it.sheet_amount = ord.total_amount;
                //it.discount = 1;
                //it.paid_amount = 0 - (num - ord.free_money);
                //it.tax_amount = 0;
                //it.pay_way = ord.pay_way == null ? "" : ord.pay_way;
                //it.pay_date = System.DateTime.Now;
                //it.supcust_no = ord.supcust_no;
                //it.supcust_flag = ord.supcust_flag;
                //it.free_money = 0;
                //it.memo = ord.other1;
                //it.other1 = "";
                //it.other2 = "";
                //it.other3 = "";
                //it.num1 = 0;
                //it.num2 = 0;
                //it.num3 = 0;
                //it.branch_no = "00";
                //it.sale_no = "A";
                //it.oper_date = ord.oper_date;
                //it.approve_flag = "0";
                //d.Insert(it);
                //
                db.CommitTran();
                LogHelper.writeSheetLog("CusSettle ->Change()", "更改成功！", SessionHelper.oper_id, "操作日志", "WARNING", ord.sheet_no);
                //LogHelper.writeSheetLog("CusSettle ->SaveAgingGroup()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CusSettle ->Change()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ICusSettle.Delete(string sheet_no)
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
                sql = "delete from rp_t_collection_way where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "delete from rp_t_accout_payrec_flow where voucher_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
                LogHelper.writeSheetLog("CusSettle ->Delete()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CusSettle ->Delete()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CusSettle ->Delete()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ICusSettle.Check(string sheet_no, string approve_man, string is_cs)
        {
            CheckSheet check = new CheckSheet();
            if (is_cs == "C")
                check.CheckCustJSSheet(sheet_no, approve_man, DateTime.Now);
            else
                check.CheckSupJSSheet(sheet_no, approve_man, DateTime.Now);
        }
        void ICusSettle.NotCheck(string sheet_no, string approve_man, string is_cs)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();
                sql = "select * from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                sql = "select * from rp_t_recpay_record_detail  where voucher_no='" + sheet_no + "'";
                var tb1 = d.ExecuteToTable(sql, null);
                if (tb1.Rows.Count > 0)
                {
                    string sheet = "";
                    foreach (DataRow dr in tb1.Rows)
                    {
                        sheet += Conv.ToString(dr["sheet_no"]) + "  ";
                    }
                    throw new Exception("本收款单在这些收款单：" + sheet_no + "中已经使用，请先让这些收款单删除。");
                }
                else
                {
                    sql = "update rp_t_recpay_record_info  set approve_flag='0',approve_man='',approve_date=null  where sheet_no='" + sheet_no + "'";
                    d.ExecuteScalar(sql, null);
                    if (Conv.ToDecimal(tb.Rows[0]["free_money"]) > 0)
                    {

                        sql = "select * from rp_t_supcust_fy_master where supcust_no='" + Conv.ToString(tb.Rows[0]["supcust_no"]) + "' and supcust_flag='" + Conv.ToString(tb.Rows[0]["supcust_flag"]) + "' and old_no ='" + sheet_no + "'";
                        var tb2 = d.ExecuteToTable(sql, null);
                        sql = "delete from rp_t_supcust_fy_master   where supcust_no='" + Conv.ToString(tb.Rows[0]["supcust_no"]) + "' and supcust_flag='" + Conv.ToString(tb.Rows[0]["supcust_flag"]) + "' and old_no ='" + sheet_no + "'";
                        d.ExecuteScalar(sql, null);
                        sql = "delete from rp_t_supcust_fy_detail   where sheet_no='" + Conv.ToString(tb2.Rows[0]["sheet_no"]) + "'";
                        d.ExecuteScalar(sql, null);
                        sql = "delete from rp_t_accout_payrec_flow where voucher_no = '" + Conv.ToString(tb2.Rows[0]["sheet_no"]) + "'";
                        d.ExecuteScalar(sql, null);
                        sql = "delete from rp_t_supcust_cash_flow where voucher_no = '" + Conv.ToString(tb2.Rows[0]["sheet_no"]) + "'";
                        d.ExecuteScalar(sql, null);
                    }
                    sql = "delete from rp_t_accout_payrec_flow   where voucher_no='" + sheet_no + "'";
                    d.ExecuteScalar(sql, null);

                    //sql = "select * from bank_t_cash_master where voucher_no='" + sheet_no + "'";
                    //var tb3 = d.ExecuteToTable(sql, null);
                    //sql = "delete from bank_t_cash_master   where voucher_no='" + sheet_no + "'";
                    //d.ExecuteScalar(sql, null);
                    //sql = "delete from bank_t_cash_detail   where sheet_no='" + Conv.ToString(tb3.Rows[0]["sheet_no"]) + "'";
                    //d.ExecuteScalar(sql, null);
                    //sql = "delete from rp_t_supcust_cash_flow   where voucher_no='" + sheet_no + "'";
                    //d.ExecuteScalar(sql, null);
                    sql = "select * from rp_t_recpay_record_detail  where sheet_no='" + sheet_no + "'";
                    var tb4 = d.ExecuteToTable(sql, null);
                    foreach (DataRow dr in tb4.Rows)
                    {
                        sql = "update rp_t_accout_payrec_flow  set paid_amount=paid_amount-" + Conv.ToDecimal(dr["pay_amount"]) + "  where voucher_no='" + Conv.ToString(dr["voucher_no"]) + "'";
                        d.ExecuteScalar(sql, null);
                    }
                }
                db.CommitTran();
                LogHelper.writeSheetLog("CusSettle ->NotCheck()", "反审成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CusSettle ->Delete()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CusSettle ->NotCheck()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }


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

        public DataTable GetAccountFlows(Model.rp_t_accout_payrec_flow flow)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"select *,isnull(p.paid_amount,0.00) 已核销金额,isnull(p.free_money,0.00) 已免付金额
            from rp_t_accout_payrec_flow p
            where (p.paid_amount+p.free_money)*p.pay_type<p.sheet_amount 
            and p.supcust_no='@supcust_no' 
            and p.supcust_flag='@supcust_flag' "
            .Replace("@supcust_no", flow.supcust_no)
             .Replace("@supcust_flag", flow.supcust_flag);

            sql += " order by p.oper_date ";
            /*            string sql = $@"select *,isnull(p.paid_amount,0.00) 已核销金额,isnull(p.free_money,0.00) 已免付金额
            from rp_t_accout_payrec_flow p
            where (p.paid_amount+p.free_money)*p.pay_type<p.sheet_amount 
                        and p.supcust_no='{flow.supcust_no}' 
                        and p.supcust_flag='{flow.supcust_flag}' 
            union all
            select  p.flow_no,p.pay_type,b.sheet_no,b.trans_no,p.sheet_amount *-1,p.discount,p.paid_amount,p.tax_amount,p.pay_way,p.pay_date,p.supcust_no,
            p.supcust_flag,p.free_money,p.memo,p.other1,p.other2,p.other3,p.num1,p.num2,p.num3,p.branch_no,p.sale_no,
            p.oper_date, isnull(p.paid_amount,0.00) 已核销金额,isnull(p.free_money,0.00) 已免付金额
            from rp_t_accout_payrec_flow p 
            left join ic_t_inout_store_master as b on b.voucher_no=p.voucher_no 
            where  
                         p.supcust_no='{flow.supcust_no}' 
                        and p.supcust_flag='{flow.supcust_flag}' 
             and b.trans_no='F'
             order by p.oper_date  ";*/

            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

    }
}
