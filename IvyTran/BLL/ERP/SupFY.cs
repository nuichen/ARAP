using System;
using System.Collections.Generic;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class SupFY : ISupFY
    {
        //public SupFY()
        //{
        //    if (SessionHelper.oper_id == null || Conv.ToString(SessionHelper.oper_id) == "")
        //    {
        //        throw new Exception("登录超时！");
        //    }
        //}

        System.Data.DataTable ISupFY.GetList(DateTime date1, DateTime date2, string sup_no)
        {
            string sql = @"select a.sheet_no,a.supcust_no as sup_no,
cus.sup_name as sup_name,a.supcust_flag,a.pay_type,
a.pay_date,a.old_no,a.oper_id,oper.oper_name,
(a.oper_id +'/'+ oper.oper_name) as oper_id_a,
a.oper_date,a.approve_flag,
a.approve_man,man.oper_name as approve_man_name,
(a.approve_man+'/'+man.oper_name) as approve_man_a,
a.approve_date,a.is_payed,a.branch_no,
branch.branch_name,(a.branch_no +'/'+ branch.branch_name) as branch_no_a,
a.sale_man,people.oper_name as sale_man_name,(a.sale_man +'/'+people.oper_name) as sale_man_a,
a.other1,a.other2,a.other3,a.num1,a.num2,a.num3,
a.visa_id,bank.visa_nm as visa_name,(a.visa_id+'/'+bank.visa_nm) as visa_id_a,
a.is_over,a.total_amount,a.paid_amount,a.pay_way,a.pay_name from rp_t_supcust_fy_master a
left join (select * from bi_t_supcust_info where supcust_flag='S') cus on a.supcust_no=cus.supcust_no
left join (select * from sa_t_operator_i) oper on a.oper_id=oper.oper_id
left join (select * from sa_t_operator_i) man on a.approve_man=man.oper_id
left join (select * from bi_t_branch_info ) branch on a.branch_no=branch.branch_no
left join bi_t_people_info people on a.sale_man=people.oper_id
left join bi_t_bank_info bank on a.visa_id=bank.visa_id" +
 " where sheet_no like 'BM%'" +
                 " and a.oper_date>='" + date1.ToString("yyyy-MM-dd") + " 00:00:00.000'" +
                 " and a.oper_date<='" + date2.ToString("yyyy-MM-dd") + " 23:59:59.999'";
            if (sup_no != "")
            {
                sql += " and a.supcust_no='" + sup_no + "'";
            }
            sql += "order by a.oper_date desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (Conv.ToString(tb.Rows[i]["pay_type"]) == "1")
                    tb.Rows[i]["total_amount"] = 0 - Conv.ToDecimal(tb.Rows[i]["total_amount"]);
            }
            return tb;
        }

        void ISupFY.GetOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            string sql = @"select a.sheet_no,a.supcust_no as sup_no,
cus.sup_name as sup_name,a.supcust_flag,a.pay_type,
a.pay_date,a.old_no,a.oper_id,oper.oper_name,
(a.oper_id +'/'+ oper.oper_name) as oper_id_a,
a.oper_date,a.approve_flag,
a.approve_man,man.oper_name as approve_man_name,
(a.approve_man+'/'+man.oper_name) as approve_man_a,
a.approve_date,a.is_payed,a.branch_no,
branch.branch_name,(a.branch_no +'/'+ branch.branch_name) as branch_no_a,
a.sale_man,people.oper_name as sale_man_name,(a.sale_man +'/'+people.oper_name) as sale_man_a,
a.other1,a.other2,a.other3,a.num1,a.num2,a.num3,
a.visa_id,bank.visa_nm as visa_name,(a.visa_id+'/'+bank.visa_nm) as visa_id_a,
a.is_over,a.total_amount,a.paid_amount,a.pay_way,a.pay_name from rp_t_supcust_fy_master a
left join (select * from bi_t_supcust_info where supcust_flag='C') cus on a.supcust_no=cus.supcust_no
left join (select * from sa_t_operator_i) oper on a.oper_id=oper.oper_id
left join (select * from sa_t_operator_i) man on a.approve_man=man.oper_id
left join (select * from bi_t_branch_info ) branch on a.branch_no=branch.branch_no
left join bi_t_people_info people on a.sale_man=people.oper_id
left join bi_t_bank_info bank on a.visa_id=bank.visa_id" +
 " where sheet_no ='" + sheet_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            tb1 = db.ExecuteToTable(sql, null);

            sql = @"select a.sheet_no,a.flow_id,a.kk_no,b.pay_name as kk_name,b.pay_kind,
a.kk_cash,a.other1,a.other2,a.other3,a.num1,a.num2,a.num3
 from rp_t_supcust_fy_detail a 
left join bi_t_sz_type b on a.kk_no=b.pay_way" +
                                               " where a.sheet_no='" + sheet_no + "' order by flow_id";
            tb2 = db.ExecuteToTable(sql, null);
        }

        string ISupFY.MaxCode()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string front_str = "BM";
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

        void ISupFY.Add(Model.rp_t_supcust_fy_master ord, List<Model.rp_t_supcust_fy_detail> lines, out string sheet_no)
        {
            ISupFY supfy = this;
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();
                //
                ord.sheet_no = supfy.MaxCode();
                sheet_no = ord.sheet_no;
                sql = "select * from rp_t_supcust_fy_master where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count != 0)
                {
                    throw new Exception("已存在单号" + ord.sheet_no);
                }
                d.Insert(ord);
                foreach (Model.rp_t_supcust_fy_detail line in lines)
                {
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = ord.sheet_no;
                    d.Insert(line);
                }
                //Model.rp_t_accout_payrec_flow it = new Model.rp_t_accout_payrec_flow();
                //sql = "select isnull(max(flow_no),0)+1 from rp_t_accout_payrec_flow";
                //it.flow_no = IvyTran.Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                //if (ord.pay_type == "0")
                //    it.pay_type = 1;
                //else
                //    it.pay_type = -1;
                //it.voucher_no = ord.sheet_no;
                //it.trans_no = "GM";
                //it.sheet_amount = ord.total_amount;
                //it.discount = 1;
                //it.paid_amount = 0;
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
                LogHelper.writeSheetLog("SupFY ->Add()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("SupFY ->Add()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("SupFY ->Add()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ISupFY.Change(Model.rp_t_supcust_fy_master ord, List<Model.rp_t_supcust_fy_detail> lines)
        {
            string sql = "select * from rp_t_supcust_fy_master where sheet_no='" + ord.sheet_no + "'";
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
                sql = "delete from rp_t_supcust_fy_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_fy_master where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "delete from rp_t_accout_payrec_flow where voucher_no='" + ord.sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                //
                d.Insert(ord);
                foreach (Model.rp_t_supcust_fy_detail line in lines)
                {
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    d.Insert(line);
                }
                //Model.rp_t_accout_payrec_flow it = new Model.rp_t_accout_payrec_flow();
                //sql = "select isnull(max(flow_no),0)+1 from rp_t_accout_payrec_flow";
                //it.flow_no = IvyTran.Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                //if (ord.pay_type == "0")//需要更新
                //    it.pay_type = 1;
                //else
                //    it.pay_type = -1;
                //it.voucher_no = ord.sheet_no;
                //it.trans_no = "CM";
                //it.sheet_amount = ord.total_amount;//需要更新
                //it.discount = 1;
                //it.paid_amount = 0;
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
                //it.oper_date = ord.oper_date;//更新
                //it.approve_flag = "0";
                //d.Insert(it);
                //
                db.CommitTran();
                LogHelper.writeSheetLog("SupFY ->Change()", "更改成功！", SessionHelper.oper_id, "操作日志", "WARNING", ord.sheet_no);
                //LogHelper.writeSheetLog("SupFY ->Add()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("SupFY ->Change()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ISupFY.Delete(string sheet_no)
        {
            string sql = "select * from rp_t_supcust_fy_master where sheet_no='" + sheet_no + "'";
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
                //sql = "select * from rp_t_recpay_record_detail where voucher_no='" + sheet_no + "'";
                //var tb1 = d.ExecuteToTable(sql, null);
                //if (tb1.Rows.Count > 0)
                //{
                //    throw new Exception("收款单已经使用，需要先删收款单" + sheet_no);
                //}
                sql = "delete from rp_t_supcust_fy_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_fy_master where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "delete from rp_t_accout_payrec_flow where voucher_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                //
                //
                db.CommitTran();
                LogHelper.writeSheetLog("SupFY ->Delete()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("SupFY ->Delete()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("SupFY ->Delete()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void ISupFY.Check(string sheet_no, string approve_man)
        {
            CheckSheet check = new CheckSheet();
            check.CheckSupFYSheet(sheet_no, approve_man, DateTime.Now);
        }
        void ISupFY.NotCheck(string sheet_no, string approve_man)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();
                sql = "select * from rp_t_supcust_fy_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                if (Conv.ToString(tb.Rows[0]["other2"]) == "ARAP" || Conv.ToString(tb.Rows[0]["other2"]) == "CP" || Conv.ToString(tb.Rows[0]["other2"]) == "RP")
                {
                    throw new Exception("该单据是在收付款单生的免收金额或是在应收冲应付生成的，你无权在此处反审，请在对应的功能模块中反审。" + sheet_no);
                }
                sql = "select * from rp_t_recpay_record_detail  where voucher_no='" + sheet_no + "'";
                var tb1 = d.ExecuteToTable(sql, null);
                if (tb1.Rows.Count > 0)
                {
                    throw new Exception("收款单已经使用，请先删除收款单" + Conv.ToString(tb1.Rows[0]["sheet_no"]));
                }
                sql = "update rp_t_supcust_fy_master  set approve_flag='0' ,approve_man='',approve_date=null  where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "update rp_t_accout_payrec_flow  set approve_flag='0' ,approve_man='" + approve_man +
                //  "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'  where voucher_no='" + sheet_no + "'";
                sql = "delete from rp_t_accout_payrec_flow  where voucher_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_cash_flow where voucher_no = '" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("SupFY ->NotCheck()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("SupFY ->NotCheck()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("SupFY ->NotCheck()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
