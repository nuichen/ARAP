using IvyTran.IBLL.ERP;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DB;
using IvyTran.Helper;
using IvyTran.svr.ERP;
using Model.BaseModel;
using NPinyin;
using System.Linq;
using Model.PaymentModel;

namespace IvyTran.BLL.ERP
{
    public class ARAP_SCPaymentBLL : IARAP_SCPaymentBLL
    {
        //public ARAP_SCPaymentBLL()
        //{
        //    if (SessionHelper.oper_id == null||Conv.ToString(SessionHelper.oper_id)=="")
        //    {
        //        throw new Exception("登录超时！");
        //    }
        //}

        /// <summary>
        /// 生成单号
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sheet_type"></param>
        /// <returns></returns>
        public string MaxCode(DB.IDB db, string sheet_type)
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + sheet_type + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = Helper.Conv.ToInt(obj);
                index += 1;
                if (index > 9999)
                {
                    index = 1;
                }

                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + sheet_type + "'";
                db.ExecuteScalar(sql, null);
                return sheet_type + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }

        }
        /// <summary>
        /// 获取客户或供应商期初
        /// </summary>
        /// <param name="region_no"></param>
        /// <param name="keyword"></param>
        /// <param name="is_cs"></param>
        /// <param name="is_jail"></param>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        System.Data.DataTable IARAP_SCPaymentBLL.GetSupcustBeginbalance(string region_no, string keyword, string is_cs, string is_jail, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,b.region_name,c.begin_balance,c.pay_kind,0 as delete_flag from (select * from bi_t_supcust_info where supcust_flag='" + is_cs + "') a left join bi_t_region_info b on a.region_no=b.region_no left join ot_supcust_beginbalance c on c.supcust_no=a.supcust_no  and b.supcust_flag='" + is_cs + "'   where  c.supcust_flag='" + is_cs + "' ";
            //   if (region_no != "")
            //{
            //    sql += " and a.region_no like '" + region_no + "%'";
            //}
            if (keyword != "")
            {
                sql += " and (c.supcust_no like '%@%' or sup_name like '%@%')".Replace("@", keyword);
            }
            //if (is_jail != "")
            //{
            //    sql += " and a.is_jail = '1'";
            //}

            var tb = db.ExecuteToTable(sql, "c.supcust_no", null, page_size, page_index, out total_count);
            return tb;

        }
        /// <summary>
        /// 获取供应商客户信息
        /// </summary>
        /// <param name="region_no"></param>
        /// <param name="keyword"></param>
        /// <param name="is_cs"></param>
        /// <param name="is_jail"></param>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        System.Data.DataTable IARAP_SCPaymentBLL.GetSupcustList(string region_no, string keyword, string is_cs, string is_jail, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,b.region_name,0 as begin_balance,0 as pay_kind,0 as delete_flag from (select * from bi_t_supcust_info where supcust_flag='" + is_cs + "') a left join bi_t_region_info b on a.region_no=b.region_no and b.supcust_flag='"+is_cs+"'  where a.supcust_no not in(select supcust_no from ot_supcust_beginbalance where supcust_flag='" + is_cs + "' ) ";
            if (region_no != "")
            {
                sql += " and a.region_no like '" + region_no + "%'";
            }
            if (keyword != "")
            {
                sql += " and (a.supcust_no like '%@%' or a.sup_name like '%@%')".Replace("@", keyword);
            }
            //if (is_jail != "")
            //{
            //    sql += " and a.is_jail = '1'";
            //}

            var tb = db.ExecuteToTable(sql, "a.supcust_no", null, page_size, page_index, out total_count);
            return tb;

        }
        System.Data.DataTable IARAP_SCPaymentBLL.GetSupcustList(string is_cs)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select b.supcust_no,b.sup_name,b.account_period,b.credit_amt from  bi_t_supcust_info b  where b.supcust_flag='" + is_cs + "'";
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        /// <summary>
        /// 获取客户/供应商信息
        /// </summary>
        /// <param name="is_cs"></param>
        /// <returns></returns>
        System.Data.DataTable IARAP_SCPaymentBLL.GetSupcustInfoList(string is_cs)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,b.region_name,c.begin_balance,c.pay_kind from (select * from bi_t_supcust_info where supcust_flag='" + is_cs + "') a left join bi_t_region_info b on a.region_no=b.region_no left join ot_supcust_beginbalance c on c.supcust_no=a.supcust_no  and c.supcust_flag='" + is_cs + "'  where 1=1";
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        System.Data.DataTable IARAP_SCPaymentBLL.GetSupcustBeginbalanceModel(string supcust_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from ot_supcust_beginbalance where supcust_no='" + supcust_no + "'";
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }/// <summary>
         /// 保存客户供应商期初
         /// </summary>
         /// <param name="lr"></param>
         /// <param name="is_cs"></param>
         /// <param name="oper_id"></param>
        public void SavaSupcustInitial(List<ot_supcust_beginbalance> lr, string is_cs, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();

                //    db.CommitTran();
                //}
                //catch (Exception ex)
                //{
                //    db.RollBackTran();
                //    throw;
                //}
                //finally
                //{
                //    db.Close();
                //}
                sql = "select * from ot_supcust_beginbalance ";
                var tb2 = d.ExecuteToTable(sql, null);
                if (tb2.Rows.Count > 0)
                {
                    if (Conv.ToString(tb2.Rows[0]["approve_flag"]) == "1")
                        throw new Exception("已经审核，不允许删除数据，如需要删除数据请反审");
                }
                string supcust_no_list = "'" + lr[0].supcust_no + "'";
                for (int i = 1; i < lr.Count; i++)
                {
                    supcust_no_list += ",'" + lr[i].supcust_no + "'";
                }
                sql = "select * from ot_supcust_beginbalance where supcust_no in (" + supcust_no_list + ") and supcust_flag='" + is_cs + "' ";
                var tb = d.ExecuteToTable(sql, null);
                foreach (ot_supcust_beginbalance temp in lr)
                {
                    //sql = "select count(*) from ot_supcust_beginbalance where supcust_no='" + temp.supcust_no + "'";
                    //var tb = db.ExecuteToTable(sql, null);
                    var tb1 = tb.AsEnumerable().Where(item => item["supcust_no"].ToString() == temp.supcust_no).ToArray();
                    if (tb1.Length > 0)
                    {
                        sql = "update ot_supcust_beginbalance set begin_balance=" + temp.begin_balance + ",pay_kind=" + temp.pay_kind + ",oper_id='" + oper_id + "',update_time=GETDATE(),approve_flag='0' where supcust_no='" + temp.supcust_no + "'  and supcust_flag='" + is_cs + "' ";
                        //sql = "update ot_supcust_beginbalance set begin_balance=" + temp.begin_balance + ",oper_id='" + oper_id + "',update_time=GETDATE() where supcust_no='" + temp.supcust_no + "'  and supcust_flag='" + is_cs + "' ";
                    }
                    else
                    {
                        sql = "insert into ot_supcust_beginbalance values('" + temp.supcust_no + "','" + is_cs + "'," + temp.begin_balance + ",GETDATE(),'" + oper_id + "'," + temp.pay_kind + ",'0')";
                    }
                    //sql = "update ot_supcust_beginbalance set begin_balance=" + temp.begin_balance + ",pay_kind="+temp.pay_kind+",oper_id='" + oper_id + "',update_time=GETDATE() where supcust_no='" + temp.supcust_no + "'  and supcust_flag='" + is_cs + "' ";
                    d.ExecuteToTable(sql, null);
                }

                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->SavaSupcustInitial()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING");
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->SavaSupcustInitial()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 删除期初
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="is_cs"></param>
        /// <param name="update_time"></param>
        void IARAP_SCPaymentBLL.DeleteInitial(string keyword, string is_cs, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //
                sql = "select * from ot_supcust_beginbalance ";
                var tb2 = d.ExecuteToTable(sql, null);
                if (tb2.Rows.Count > 0)
                {
                    if (Conv.ToString(tb2.Rows[0]["approve_flag"]) == "1")
                        throw new Exception("已经审核，不允许删除数据，如需要删除数据请反审");
                }
                sql = "delete from ot_supcust_beginbalance where supcust_no in(" + keyword + ") and supcust_flag='" + is_cs + "'";
                d.ExecuteScalar(sql, null);

                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->DeleteInitial()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING", keyword);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->DeleteInitial()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", keyword);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 锁定客户供应商期初
        /// </summary>
        void IARAP_SCPaymentBLL.CheckSupcustInitial(string supcust_flag)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //

                sql = "select * from ot_supcust_beginbalance where supcust_flag='"+supcust_flag+"'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("无数据不允许审核");
                }
                sql = "select * from ot_supcust_beginbalance where supcust_flag='" + supcust_flag + "' and approve_flag='0' ";
                var table = d.ExecuteToTable(sql, null);
                sql = "update ot_supcust_beginbalance set approve_flag='1' where supcust_flag='" + supcust_flag + "'";
             /*   List<rp_t_accout_payrec_flow> lst = new List<rp_t_accout_payrec_flow>();*/
                foreach (DataRow row in table.Rows)
                {
                    rp_t_accout_payrec_flow flow = new rp_t_accout_payrec_flow();
                    flow.flow_no = d.ExecuteScalar("select max(isnull(flow_no,0)) +1 from rp_t_accout_payrec_flow", null).ToInt64();
                    flow.pay_type = 1;
                    flow.voucher_no = row["supcust_no"].ToString();
                    if (supcust_flag=="S")
                    {
                        flow.trans_no = "Q2";
                    }
                    else
                    {
                        flow.trans_no = "Q1";
                    }
                
                    flow.tax_amount = 0;
                    flow.supcust_no = row["supcust_no"].ToString();
                    flow.supcust_flag = supcust_flag;
                    flow.sheet_amount = row["begin_balance"].ToString().ToDecimal();
                    flow.sale_no = "A";
                    flow.pay_way = "";
                    flow.pay_date = DateTime.Now;
                    flow.paid_amount = 0;
                    flow.other3 = "";
                    flow.other2 = "";
                    flow.other1 = "";
                    flow.oper_date = DateTime.Now;
                    flow.num3 = 0;
                    flow.num2 = 0;
                    flow.num1 = 0;
                    flow.memo = "期初";
                    flow.free_money = 0;
                    flow.discount = 1;
                    flow.branch_no = "00";
                    d.Insert(flow);

                }
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("IARAP_SCPaymentBLL ->CheckSupcustInitial()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING");
                //LogHelper.writeSheetLog("CashierBLL ->DeleteBankCashBeginbalance()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",visa_id);
            }
            catch (Exception ex)
            {

                db.RollBackTran();
                LogHelper.writeSheetLog("IARAP_SCPaymentBLL ->CheckSupcustInitial()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 取消锁定客户供应商期初
        /// </summary>
        void IARAP_SCPaymentBLL.NotCheckSupcustInitial( string supcust_flag)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //

                sql = "select * from ot_supcust_beginbalance where supcust_flag='"+supcust_flag+"' ";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("无数据不允许反审");
                }
   /*             sql = " select * from rp_t_accout_payrec_flow where paid_amount >0 and supcust_flag='"+supcust_flag+"' ";*/
                
                sql = "update ot_supcust_beginbalance set approve_flag='0' where supcust_flag='"+supcust_flag+ "' AND supcust_no not in (select voucher_no from rp_t_accout_payrec_flow where paid_amount >0 and supcust_flag='"+supcust_flag+"' and trans_no in ('Q1','Q2'))";
                d.ExecuteScalar(sql, null);
                sql = " delete from rp_t_accout_payrec_flow where trans_no in ('Q1','Q2') and paid_amount=0 and supcust_flag='"+supcust_flag+ "' and supcust_no not in (select voucher_no from rp_t_accout_payrec_flow where paid_amount >0 and supcust_flag='" + supcust_flag + "' and trans_no in ('Q1','Q2'))";
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("IARAP_SCPaymentBLL ->NotCheckSupcustInitial()", "反审成功！", SessionHelper.oper_id, "操作日志", "WARNING");
                //LogHelper.writeSheetLog("CashierBLL ->DeleteBankCashBeginbalance()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",visa_id);
            }
            catch (Exception ex)
            {

                db.RollBackTran();
                LogHelper.writeSheetLog("IARAP_SCPaymentBLL ->NotCheckSupcustInitial()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 获取不包含已保存在账期通知单中的应结明细
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
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
            sql += " and p.voucher_no  not in(select voucher_no from rp_t_account_notice_detail)";
            sql += " order by p.oper_date ";



            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        /// <summary>
        /// 获取指定客户或供应商的账期通知单
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="cus_no"></param>
        /// <param name="is_cs"></param>
        /// <param name="sheet_no"></param>
        /// <returns></returns>
        System.Data.DataTable IARAP_SCPaymentBLL.GetNoticeList(DateTime date1, DateTime date2, string cus_no, string is_cs, string sheet_no)
        {
            string sql = "select a.*,b.sup_name,c.oper_name,(a.oper_id+'/'+c.oper_name) as oper_name_a,d.oper_name as approve_man_name,(a.approve_man+'/'+d.oper_name) as approve_man_a from rp_t_account_notice a left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and b.supcust_flag='" + is_cs + "' left join sa_t_operator_i c on c.oper_id=a.oper_id left join sa_t_operator_i d on d.oper_id=a.approve_man  where a.supcust_flag='" + is_cs + "' and a.approve_flag='1' ";
            sql += "and a.oper_date>='" + date1.ToString("yyyy-MM-dd ") + "'";
            sql += "and a.oper_date<='" + date2.ToString("yyyy-MM-dd ") + "'";
            sql += " and a.supcust_no='" + cus_no + "'";
            if (sheet_no != "")
            {
                sql += " and a.sheet_no like '%" + sheet_no + "%'";
            }
            sql += " order by a.oper_date desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }
        /// <summary>
        /// 根据账期通知单获取应结明细
        /// </summary>
        /// <param name="lr"></param>
        /// <returns></returns>
        System.Data.DataTable IARAP_SCPaymentBLL.GetCollectionNotice(List<rp_t_account_notice> lr)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "";
            string sheet_no = "";
            bool is_one = false;
            foreach (rp_t_account_notice dr in lr)
            {
                sql = "select * from rp_t_account_notice_detail where sheet_no='" + dr.sheet_no + "'";
                DataTable dt = db.ExecuteToTable(sql, null);
                foreach (DataRow d in dt.Rows)
                {
                    if (is_one == false)
                    {
                        sheet_no += "'" + Conv.ToString(d["voucher_no"]) + "'";
                        is_one = true;
                    }
                    else
                    {
                        sheet_no += ",'" + Conv.ToString(d["voucher_no"]) + "'";
                    }
                }

                sql = @"select *,isnull(p.paid_amount,0.00) 已核销金额,isnull(p.free_money,0.00) 已免付金额
from rp_t_accout_payrec_flow p
where  p.voucher_no  in(" + sheet_no + ") ";
                sql += " order by p.oper_date ";

            }
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        void IARAP_SCPaymentBLL.AddSupcustInitial(string supcust, string is_cs, string oper_id, string type)
        {
            string sql = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                if (type == "1")
                {

                    sql = "insert into ot_supcust_beginbalance values('" + supcust + "','" + is_cs + "',0,GETDATE(),'" + oper_id + "'," + ",1)";
                    d.ExecuteToTable(sql, null);
                }
                else if (type == "2")
                {
                    sql = "select supcust_no from bi_t_supcust_info where supcust_flag='" + is_cs + "' and region_no='" + supcust + "'  and supcust_no not in (select supcust_no from ot_supcust_beginbalance where supcust_flag='" + is_cs + "')";
                    DataTable dt = d.ExecuteToTable(sql, null);
                    foreach (DataRow dr in dt.Rows)
                    {
                        sql = "insert into ot_supcust_beginbalance values('" + dr["supcust_no"].ToString() + "','" + is_cs + "',0,GETDATE(),'" + oper_id + "',1)";
                        d.ExecuteToTable(sql, null);
                    }
                }
                else
                {
                    sql = "select supcust_no from bi_t_supcust_info where supcust_flag='" + is_cs + "'  and supcust_no not in (select supcust_no from ot_supcust_beginbalance where supcust_flag='" + is_cs + "')";
                    DataTable dt = d.ExecuteToTable(sql, null);
                    foreach (DataRow dr in dt.Rows)
                    {
                        sql = "insert into ot_supcust_beginbalance values('" + dr["supcust_no"].ToString() + "','" + is_cs + "',0,GETDATE(),'" + oper_id + "',1)";
                        d.ExecuteToTable(sql, null);
                    }
                }

                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->SavaSupcustInitial()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING");
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->SavaSupcustInitial()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
                throw;
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// 保存客户账期通知
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="lines"></param>
        /// <param name="sheet_no"></param>
        void IARAP_SCPaymentBLL.AddNotice(Model.rp_t_account_notice ord, List<rp_t_account_notice_detail> lines, out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                sheet_no = MaxCode(d, "ZQ");
                ord.sheet_no = sheet_no;
                d.Insert(ord);


                foreach (rp_t_account_notice_detail line in lines)
                {
                    line.sheet_no = sheet_no;
                    d.Insert(line);

                }
                //
                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING", ord.sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IARAP_SCPaymentBLL.ChangeNotice(Model.rp_t_account_notice ord, List<Model.rp_t_account_notice_detail> lines)
        {
            string sql = "select * from rp_t_account_notice where sheet_no='" + ord.sheet_no + "'";
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
                sql = "delete from rp_t_account_notice where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_account_notice_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //string sheet_no;
                //sheet_no = MaxCode(d, "ZQ");
                //ord.sheet_no = sheet_no;
                d.Insert(ord);
                foreach (Model.rp_t_account_notice_detail line in lines)
                {
                    line.sheet_no = ord.sheet_no;
                    d.Insert(line);
                }
                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->ChangeNotice()", "更改成功！", SessionHelper.oper_id, "操作日志", "WARNING", ord.sheet_no);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->ChangeNotice()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 删除客户通知
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="update_time"></param>
        void IARAP_SCPaymentBLL.DeleteNotice(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //
                sql = "select * from rp_t_account_notice where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                sql = "delete from rp_t_account_notice where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_account_notice_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->DeleteNotice()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->DeleteNotice()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        System.Data.DataTable IARAP_SCPaymentBLL.GetArApList(DateTime date1, DateTime date2, string supcust_form, string supcust_to, string sheet_id)
        {
            string is_cs_supcust_from;
            string is_cs_supcust_to;
            if (sheet_id == "OR")
            {
                is_cs_supcust_from = "C";
                is_cs_supcust_to = "S";
            }else if (sheet_id == "RR")
            {
                is_cs_supcust_from = "C";
                is_cs_supcust_to = "C";
            }
            else
            {
                is_cs_supcust_from = "S";
                is_cs_supcust_to = "S";
            }
            string sql = "select a.*,b.sup_name as supcust_from_name,c.sup_name as supcust_to_name,d.oper_name as oper_name,(a.oper_id+'/'+d.oper_name) as oper_name_a,e.oper_name as approve_man_name,(a.approve_man+'/'+e.oper_name) as approve_man_a from rp_t_arap_transformation a left join bi_t_supcust_info b on a.supcust_from=b.supcust_no and b.supcust_flag='"+ is_cs_supcust_from + "'  left join bi_t_supcust_info c on a.supcust_to=c.supcust_no and c.supcust_flag='"+ is_cs_supcust_to + "' left join sa_t_operator_i d on d.oper_id=a.oper_id left join sa_t_operator_i e on e.oper_id=a.approve_man  where a.sheet_id='" + sheet_id + "'";
            sql += "and a.oper_date>='" + date1.Toyyyy_MM_ddStart() + "'";
            sql += "and a.oper_date<='" + date2.Toyyyy_MM_ddEnd() + "'";
            if (supcust_to != "")
            {
                sql += " and a.supcust_from='" + supcust_to + "'";
            }
            if (supcust_form != "")
            {
                sql += " and a.supcust_to='" + supcust_form + "'";
            }
            sql += " order by a.oper_date desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }
        void IARAP_SCPaymentBLL.GetArApOrder(string sheet_no, out System.Data.DataTable tb1)
        {
            string sheet_id = sheet_no.Substring(0, 2);
            string is_cs_supcust_from;
            string is_cs_supcust_to;
            if (sheet_id == "OR")
            {
                is_cs_supcust_from = "C";
                is_cs_supcust_to = "S";
            }
            else if (sheet_id == "RR")
            {
                is_cs_supcust_from = "C";
                is_cs_supcust_to = "C";
            }
            else
            {
                is_cs_supcust_from = "S";
                is_cs_supcust_to = "S";
            }
            string sql = "select a.*,b.sup_name as supcust_from_name,c.sup_name as supcust_to_name,d.oper_name as oper_name,e.oper_name as approve_man_name from rp_t_arap_transformation a left join bi_t_supcust_info b on a.supcust_from=b.supcust_no and b.supcust_flag='"+ is_cs_supcust_from + "'  left join bi_t_supcust_info c on a.supcust_to=c.supcust_no and c.supcust_flag='"+ is_cs_supcust_to + "' left join sa_t_operator_i d on d.oper_id=a.oper_id left join sa_t_operator_i e on e.oper_id=a.approve_man  where a.sheet_no='" + sheet_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            tb1 = db.ExecuteToTable(sql, null);
        }
        void IARAP_SCPaymentBLL.AddArAp(rp_t_arap_transformation ord, out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                sheet_no = MaxCode(d, ord.sheet_id);
                ord.sheet_no = sheet_no;
                d.Insert(ord);
                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddArAp()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddArAp()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IARAP_SCPaymentBLL.DeleteArAp(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //
                sql = "select * from rp_t_arap_transformation where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                //sql = "select * from rp_t_recpay_record_detail where voucher_no='" + sheet_no + "'";
                //var tb1 = d.ExecuteToTable(sql, null);
                //if (tb1.Rows.Count > 0)
                //{
                //    throw new Exception("收款单已经使用，需要先删收款单" + sheet_no);
                //}
                sql = "delete from rp_t_arap_transformation where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "delete from rp_t_arap_transformation where sheet_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                //sql = "delete from rp_t_accout_payrec_flow where voucher_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->DeleteArAp()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->DeleteArAp()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        string MaxCode()
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
        string MaxCode1()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string front_str = "HM";
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
        void IARAP_SCPaymentBLL.CheckArAp(string sheet_no, string approve_man, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();

                sql = "select * from rp_t_arap_transformation where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }




                sql = "update rp_t_arap_transformation set approve_man='" + approve_man + "',approve_date=GETDATE(),approve_flag='1' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                if (Conv.ToString(tb.Rows[0]["sheet_id"]) == "OR")
                {
                    //生成其他应收
                    Model.rp_t_supcust_fy_master ord = new Model.rp_t_supcust_fy_master();
                    ord.sheet_no = MaxCode1();
                    ord.supcust_no = Conv.ToString(tb.Rows[0]["supcust_from"]);
                    ord.supcust_flag = "C";

                    ord.pay_date = System.DateTime.Now;
                    ord.old_no = Conv.ToString(tb.Rows[0]["sheet_no"]);
                    ord.oper_id = approve_man;
                    ord.oper_date = System.DateTime.Now;
                    ord.approve_flag = "1";
                    ord.approve_man = approve_man;
                    ord.approve_date = System.DateTime.Now;
                    ord.is_payed = "0";
                    ord.sale_man = approve_man;
                    ord.branch_no = "";
                    ord.cm_branch = "00";
                    ord.other1 = "应收应付冲账";
                    ord.other2 = "ARAP";
                    ord.other3 = "";
                    ord.num1 = 0;
                    ord.num2 = 0;
                    ord.num3 = 0;
                    ord.visa_id = "";
                    ord.is_over = "0";
                    ord.total_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord.paid_amount = 0;
                    ord.pay_way = "";
                    ord.pay_name = "";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord.pay_type = "0";
                    else
                        ord.pay_type = "1";
                    Model.rp_t_supcust_fy_detail line = new Model.rp_t_supcust_fy_detail();
                    line.sheet_no = ord.sheet_no;
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.kk_no = "03";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        line.kk_cash = Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                    else
                        line.kk_cash = 0 - Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                    line.other1 = "应收应付冲账";

                    line.other2 = "";
                    line.other3 = "";
                    line.num1 = 0;
                    line.num2 = 0;
                    line.num3 = 0;
                    d.Insert(ord);
                    d.Insert(line);
                    //客户流水(应收)
                    sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no='" + ord.supcust_no + "' and supcust_flag='C' order by flow_no ";
                    var dt1 = d.ExecuteToTable(sql, null);
                    decimal old_money = 0;
                    if (dt1.Rows.Count > 0)
                    {
                        old_money = Conv.ToDecimal(dt1.Rows[0]["new_money"]);
                    }

                    Model.rp_t_supcust_cash_flow it = new Model.rp_t_supcust_cash_flow();

                    sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
                    it.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
                    it.supcust_no = ord.supcust_no;
                    it.supcust_flag = "C";
                    decimal pay_type;
                    if (ord.pay_type == "0" || ord.pay_type == "2")
                    {
                        pay_type = 1;
                    }
                    else
                    {
                        pay_type = -1;
                    }
                    it.oper_type = "CM";
                    it.voucher_no = ord.sheet_no;
                    it.sheet_no = "";
                    it.oper_date = ord.oper_date;
                    it.old_money = old_money;
                    it.oper_money = ord.total_amount;
                    it.free_money = 0;
                    it.new_money = it.old_money + (it.oper_money - it.free_money) * pay_type;
                    it.pay_date = System.DateTime.Now;
                    d.Insert(it);

                    //生成其他应付
                    Model.rp_t_supcust_fy_master ord3 = new Model.rp_t_supcust_fy_master();
                    ord3.sheet_no = MaxCode();
                    ord3.supcust_no = Conv.ToString(tb.Rows[0]["supcust_to"]);
                    ord3.supcust_flag = "S";

                    ord3.pay_date = System.DateTime.Now;
                    ord3.old_no = Conv.ToString(tb.Rows[0]["sheet_no"]);
                    ord3.oper_id = approve_man;
                    ord3.oper_date = System.DateTime.Now;
                    ord3.approve_flag = "1";
                    ord3.approve_man = approve_man;
                    ord3.approve_date = System.DateTime.Now;
                    ord3.is_payed = "0";
                    ord3.sale_man = approve_man;
                    ord3.branch_no = "";
                    ord3.cm_branch = "00";
                    ord3.other1 = "应收应付冲账";
                    ord3.other2 = "ARAP";
                    ord3.other3 = "";
                    ord3.num1 = 0;
                    ord3.num2 = 0;
                    ord3.num3 = 0;
                    ord3.visa_id = "";
                    ord3.is_over = "0";
                    ord3.total_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord3.paid_amount = 0;
                    ord3.pay_way = "";
                    ord3.pay_name = "";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord3.pay_type = "0";
                    else
                        ord3.pay_type = "1";
                    Model.rp_t_supcust_fy_detail line1 = new Model.rp_t_supcust_fy_detail();
                    line1.sheet_no = ord3.sheet_no;
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line1.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line1.kk_no = "03";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        line1.kk_cash = Helper.Conv.ToDecimal(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    else
                        line1.kk_cash = 0 - Helper.Conv.ToDecimal(Conv.ToDecimal(tb.Rows[0]["total_amount"]));

                    line1.other1 = "应收应付冲账";

                    line1.other2 = "";
                    line1.other3 = "";
                    line1.num1 = 0;
                    line1.num2 = 0;
                    line1.num3 = 0;
                    d.Insert(ord3);
                    d.Insert(line1);
                    //供应商流水(应付)

                    sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no='" + ord3.supcust_no + "' and supcust_flag='S' order by flow_no ";
                    var dt2 = d.ExecuteToTable(sql, null);
                    decimal old_money1 = 0;
                    if (dt2.Rows.Count > 0)
                    {
                        old_money1 = Conv.ToDecimal(dt2.Rows[0]["new_money"]);
                    }

                    Model.rp_t_supcust_cash_flow it1 = new Model.rp_t_supcust_cash_flow();

                    sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
                    it1.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
                    it1.supcust_no = ord3.supcust_no;
                    it1.supcust_flag = "S";
                    decimal pay_type1;
                    if (ord3.pay_type == "0" || ord3.pay_type == "2")
                    {
                        pay_type1 = 1;
                    }
                    else
                    {
                        pay_type1 = -1;
                    }
                    it1.oper_type = "GM";
                    it1.voucher_no = ord3.sheet_no;
                    it1.sheet_no = "";
                    it1.oper_date = ord3.oper_date;
                    it1.old_money = old_money1;
                    it1.oper_money = ord3.total_amount;
                    it1.free_money = 0;
                    it1.new_money = it1.old_money + (it1.oper_money - it1.free_money) * pay_type1;
                    it1.pay_date = System.DateTime.Now;
                    d.Insert(it1);
                    //生成其他应收的应结明细
                    rp_t_accout_payrec_flow ord1 = new rp_t_accout_payrec_flow();
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_accout_payrec_flow";
                    ord1.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord1.pay_type = 1;
                    else
                        ord1.pay_type = -1;
                    ord1.voucher_no = ord.sheet_no;
                    ord1.trans_no = "CM";//待确定
                    ord1.sheet_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord1.discount = 1;
                    ord1.paid_amount = 0;
                    ord1.tax_amount = 0;
                    ord1.pay_date = DateTime.Now;
                    ord1.supcust_no = Conv.ToString(tb.Rows[0]["supcust_from"]);
                    ord1.supcust_flag = "C";
                    ord1.pay_way = "";
                    ord1.free_money = 0;
                    ord1.oper_date = DateTime.Now;
                    ord1.other1 = "ARAP";
                    //ord1.approve_flag = "0";
                    d.Insert(ord1);


                    //应成其他应付的应结明细
                    rp_t_accout_payrec_flow ord2 = new rp_t_accout_payrec_flow();
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_accout_payrec_flow";
                    ord2.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord2.pay_type = 1;
                    else
                        ord2.pay_type = -1;
                    ord2.voucher_no = ord3.sheet_no;
                    ord2.trans_no = "GM";//待确定
                    ord2.sheet_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord2.discount = 1;
                    ord2.paid_amount = 0;
                    ord2.pay_way = "";
                    ord2.tax_amount = 0;
                    ord2.pay_date = DateTime.Now;
                    ord2.supcust_no = Conv.ToString(tb.Rows[0]["supcust_to"]);
                    ord2.supcust_flag = "S";
                    ord2.free_money = 0;
                    ord2.oper_date = DateTime.Now;
                    ord2.other1 = "ARAP";
                    d.Insert(ord2);



                }
                else if (Conv.ToString(tb.Rows[0]["sheet_id"]) == "RR")
                {
                    //为转入客户生成其他应收
                    Model.rp_t_supcust_fy_master ord = new Model.rp_t_supcust_fy_master();
                    ord.sheet_no = MaxCode1();
                    ord.supcust_no = Conv.ToString(tb.Rows[0]["supcust_from"]);
                    ord.supcust_flag = "C";

                    ord.pay_date = System.DateTime.Now;
                    ord.old_no = Conv.ToString(tb.Rows[0]["sheet_no"]);
                    ord.oper_id = approve_man;
                    ord.oper_date = System.DateTime.Now;
                    ord.approve_flag = "1";
                    ord.approve_man = approve_man;
                    ord.approve_date = System.DateTime.Now;
                    ord.is_payed = "0";
                    ord.sale_man = approve_man;
                    ord.branch_no = "";
                    ord.cm_branch = "00";
                    ord.other1 = "应收转应收";
                    ord.other2 = "ARAP";
                    ord.other3 = "";
                    ord.num1 = 0;
                    ord.num2 = 0;
                    ord.num3 = 0;
                    ord.visa_id = "";
                    ord.is_over = "0";
                    ord.total_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord.paid_amount = 0;
                    ord.pay_way = "";
                    ord.pay_name = "";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord.pay_type = "0";
                    else
                        ord.pay_type = "1";
                    Model.rp_t_supcust_fy_detail line = new Model.rp_t_supcust_fy_detail();
                    line.sheet_no = ord.sheet_no;
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.kk_no = "04";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        line.kk_cash = Helper.Conv.ToDecimal(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    else
                        line.kk_cash = 0 - Helper.Conv.ToDecimal(Conv.ToDecimal(tb.Rows[0]["total_amount"]));

                    line.other1 = "应收转应收";

                    line.other2 = "";
                    line.other3 = "";
                    line.num1 = 0;
                    line.num2 = 0;
                    line.num3 = 0;
                    d.Insert(ord);
                    d.Insert(line);

                    //转入客户流水(其他应收)
                    sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no='" + ord.supcust_no + "' and supcust_flag='C' order by flow_no ";
                    var dt1 = d.ExecuteToTable(sql, null);
                    decimal old_money = 0;
                    if (dt1.Rows.Count > 0)
                    {
                        old_money = Conv.ToDecimal(dt1.Rows[0]["new_money"]);
                    }

                    Model.rp_t_supcust_cash_flow it = new Model.rp_t_supcust_cash_flow();

                    sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
                    it.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
                    it.supcust_no = ord.supcust_no;
                    it.supcust_flag = "C";
                    decimal pay_type;
                    if (ord.pay_type == "0" || ord.pay_type == "2")
                    {
                        pay_type = 1;
                    }
                    else
                    {
                        pay_type = -1;
                    }
                    it.oper_type = "CM";
                    it.voucher_no = ord.sheet_no;
                    it.sheet_no = "";
                    it.oper_date = ord.oper_date;
                    it.old_money = old_money;
                    it.oper_money = ord.total_amount;
                    it.free_money = 0;
                    it.new_money = it.old_money + (it.oper_money - it.free_money) * pay_type;
                    it.pay_date = System.DateTime.Now;
                    d.Insert(it);
                    //为转出客户生成其他应收
                    Model.rp_t_supcust_fy_master ord3 = new Model.rp_t_supcust_fy_master();
                    ord3.sheet_no = MaxCode1();
                    ord3.supcust_no = Conv.ToString(tb.Rows[0]["supcust_to"]);
                    ord3.supcust_flag = "C";

                    ord3.pay_date = System.DateTime.Now;
                    ord3.old_no = Conv.ToString(tb.Rows[0]["sheet_no"]);
                    ord3.oper_id = approve_man;
                    ord3.oper_date = System.DateTime.Now;
                    ord3.approve_flag = "1";
                    ord3.approve_man = approve_man;
                    ord3.approve_date = System.DateTime.Now;
                    ord3.is_payed = "0";
                    ord3.sale_man = approve_man;
                    ord3.branch_no = "";
                    ord3.cm_branch = "00";
                    ord3.other1 = "应收转应收";
                    ord3.other2 = "ARAP";
                    ord3.other3 = "";
                    ord3.num1 = 0;
                    ord3.num2 = 0;
                    ord3.num3 = 0;
                    ord3.visa_id = "";
                    ord3.is_over = "0";
                    ord3.total_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord3.paid_amount = 0;
                    ord3.pay_way = "";
                    ord3.pay_name = "";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord3.pay_type = "1";
                    else
                        ord3.pay_type = "0";
                    Model.rp_t_supcust_fy_detail line1 = new Model.rp_t_supcust_fy_detail();
                    line1.sheet_no = ord3.sheet_no;
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line1.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line1.kk_no = "04";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        line1.kk_cash = 0 - Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                    else
                        line1.kk_cash = Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                    line1.other1 = "应收转应收";

                    line1.other2 = "";
                    line1.other3 = "";
                    line1.num1 = 0;
                    line1.num2 = 0;
                    line1.num3 = 0;
                    d.Insert(ord3);
                    d.Insert(line1);

                    //转出客户流水(其他应收)

                    sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no='" + ord3.supcust_no + "' and supcust_flag='C' order by flow_no ";
                    var dt2 = d.ExecuteToTable(sql, null);
                    decimal old_money1 = 0;
                    if (dt2.Rows.Count > 0)
                    {
                        old_money1 = Conv.ToDecimal(dt2.Rows[0]["new_money"]);
                    }

                    Model.rp_t_supcust_cash_flow it1 = new Model.rp_t_supcust_cash_flow();

                    sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
                    it1.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
                    it1.supcust_no = ord3.supcust_no;
                    it1.supcust_flag = "C";
                    decimal pay_type1;
                    if (ord3.pay_type == "0" || ord3.pay_type == "2")
                    {
                        pay_type1 = 1;
                    }
                    else
                    {
                        pay_type1 = -1;
                    }
                    it1.oper_type = "CM";
                    it1.voucher_no = ord3.sheet_no;
                    it1.sheet_no = "";
                    it1.oper_date = ord3.oper_date;
                    it1.old_money = old_money1;
                    it1.oper_money = ord3.total_amount;
                    it1.free_money = 0;
                    it1.new_money = it1.old_money + (it1.oper_money - it1.free_money) * pay_type1;
                    it1.pay_date = System.DateTime.Now;
                    d.Insert(it1);
                    //为转入客户生成应结明细
                    rp_t_accout_payrec_flow ord1 = new rp_t_accout_payrec_flow();
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_accout_payrec_flow";
                    ord1.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord1.pay_type = 1;
                    else
                        ord1.pay_type = -1;
                    ord1.voucher_no = ord.sheet_no;
                    ord1.trans_no = "CM";//待确定
                    ord1.sheet_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord1.discount = 1;
                    ord1.pay_way = "";
                    ord1.paid_amount = 0;
                    ord1.tax_amount = 0;
                    ord1.pay_date = DateTime.Now;
                    ord1.supcust_no = Conv.ToString(tb.Rows[0]["supcust_from"]);
                    ord1.supcust_flag = "C";
                    ord1.free_money = 0;
                    ord1.oper_date = DateTime.Now;
                    ord1.other1 = "ARAP";
                    //ord1.approve_flag = "0";
                    d.Insert(ord1);
                    //为转出客户生成其他应收的应结明细
                    rp_t_accout_payrec_flow ord2 = new rp_t_accout_payrec_flow();
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_accout_payrec_flow";
                    ord2.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord2.pay_type = -1;
                    else
                        ord2.pay_type = 1;
                    ord2.voucher_no = ord3.sheet_no;
                    ord2.trans_no = "CM";//待确定
                    ord2.sheet_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord2.discount = 1;
                    ord2.paid_amount = 0;
                    ord2.pay_way = "";
                    ord2.tax_amount = 0;
                    ord2.pay_date = DateTime.Now;
                    ord2.supcust_no = Conv.ToString(tb.Rows[0]["supcust_to"]);
                    ord2.supcust_flag = "C";
                    ord2.free_money = 0;
                    ord2.oper_date = DateTime.Now;
                    ord2.other1 = "ARAP";
                    d.Insert(ord2);
                }
                else if (Conv.ToString(tb.Rows[0]["sheet_id"]) == "PR")
                {
                    //生成转入供应商的其他应付
                    Model.rp_t_supcust_fy_master ord = new Model.rp_t_supcust_fy_master();
                    ord.sheet_no = MaxCode();
                    ord.supcust_no = Conv.ToString(tb.Rows[0]["supcust_from"]);
                    ord.supcust_flag = "S";

                    ord.pay_date = System.DateTime.Now;
                    ord.old_no = Conv.ToString(tb.Rows[0]["sheet_no"]);
                    ord.oper_id = approve_man;
                    ord.oper_date = System.DateTime.Now;
                    ord.approve_flag = "1";
                    ord.approve_man = approve_man;
                    ord.approve_date = System.DateTime.Now;
                    ord.is_payed = "0";
                    ord.sale_man = approve_man;
                    ord.branch_no = "";
                    ord.cm_branch = "00";
                    ord.other1 = "应付转应付";
                    ord.other2 = "ARAP";
                    ord.other3 = "";
                    ord.num1 = 0;
                    ord.num2 = 0;
                    ord.num3 = 0;
                    ord.visa_id = "";
                    ord.is_over = "0";
                    ord.total_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord.paid_amount = 0;
                    ord.pay_way = "";
                    ord.pay_name = "";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord.pay_type = "0";
                    else
                        ord.pay_type = "1";
                    Model.rp_t_supcust_fy_detail line = new Model.rp_t_supcust_fy_detail();
                    line.sheet_no = ord.sheet_no;
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.kk_no = "05";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        line.kk_cash = Helper.Conv.ToDecimal(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    else
                        line.kk_cash = 0 - Helper.Conv.ToDecimal(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    line.other1 = "应付转应付";

                    line.other2 = "";
                    line.other3 = "";
                    line.num1 = 0;
                    line.num2 = 0;
                    line.num3 = 0;
                    d.Insert(ord);
                    d.Insert(line);
                    //转入供应商流水(其他应付)
                    sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no='" + ord.supcust_no + "' and supcust_flag='S' order by flow_no ";
                    var dt1 = d.ExecuteToTable(sql, null);
                    decimal old_money = 0;
                    if (dt1.Rows.Count > 0)
                    {
                        old_money = Conv.ToDecimal(dt1.Rows[0]["new_money"]);
                    }

                    Model.rp_t_supcust_cash_flow it = new Model.rp_t_supcust_cash_flow();

                    sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
                    it.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
                    it.supcust_no = ord.supcust_no;
                    it.supcust_flag = "S";
                    decimal pay_type;
                    if (ord.pay_type == "0" || ord.pay_type == "2")
                    {
                        pay_type = 1;
                    }
                    else
                    {
                        pay_type = -1;
                    }
                    it.oper_type = "GM";
                    it.voucher_no = ord.sheet_no;
                    it.sheet_no = "";
                    it.oper_date = ord.oper_date;
                    it.old_money = old_money;
                    it.oper_money = ord.total_amount;
                    it.free_money = 0;
                    it.new_money = it.old_money + (it.oper_money - it.free_money) * pay_type;
                    it.pay_date = System.DateTime.Now;
                    d.Insert(it);

                    //生成转出供应商的其他应付
                    Model.rp_t_supcust_fy_master ord3 = new Model.rp_t_supcust_fy_master();
                    ord3.sheet_no = MaxCode();
                    ord3.supcust_no = Conv.ToString(tb.Rows[0]["supcust_to"]);
                    ord3.supcust_flag = "S";

                    ord3.pay_date = System.DateTime.Now;
                    ord3.old_no = Conv.ToString(tb.Rows[0]["sheet_no"]);
                    ord3.oper_id = approve_man;
                    ord3.oper_date = System.DateTime.Now;
                    ord3.approve_flag = "1";
                    ord3.approve_man = approve_man;
                    ord3.approve_date = System.DateTime.Now;
                    ord3.is_payed = "0";
                    ord3.sale_man = approve_man;
                    ord3.branch_no = "";
                    ord3.cm_branch = "00";
                    ord3.other1 = "应付转应付";
                    ord3.other2 = "ARAP";
                    ord3.other3 = "";
                    ord3.num1 = 0;
                    ord3.num2 = 0;
                    ord3.num3 = 0;
                    ord3.visa_id = "";
                    ord3.is_over = "0";
                    ord3.total_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord3.paid_amount = 0;
                    ord3.pay_way = "";
                    ord3.pay_name = "";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord3.pay_type = "1";
                    else
                        ord3.pay_type = "0";
                    Model.rp_t_supcust_fy_detail line1 = new Model.rp_t_supcust_fy_detail();
                    line1.sheet_no = ord3.sheet_no;
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line1.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line1.kk_no = "05";
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        line1.kk_cash = 0 - Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                    else
                        line1.kk_cash = Conv.ToDecimal(tb.Rows[0]["total_amount"]);

                    line1.other1 = "应付转应付";

                    line1.other2 = "";
                    line1.other3 = "";
                    line1.num1 = 0;
                    line1.num2 = 0;
                    line1.num3 = 0;
                    d.Insert(ord3);
                    d.Insert(line1);

                    //转出供应商流水(其他应付)

                    sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no='" + ord3.supcust_no + "' and supcust_flag='S' order by flow_no ";
                    var dt2 = d.ExecuteToTable(sql, null);
                    decimal old_money1 = 0;
                    if (dt2.Rows.Count > 0)
                    {
                        old_money1 = Conv.ToDecimal(dt2.Rows[0]["new_money"]);
                    }

                    Model.rp_t_supcust_cash_flow it1 = new Model.rp_t_supcust_cash_flow();

                    sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
                    it1.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
                    it1.supcust_no = ord3.supcust_no;
                    it1.supcust_flag = "S";
                    decimal pay_type1;
                    if (ord3.pay_type == "0" || ord3.pay_type == "2")
                    {
                        pay_type1 = 1;
                    }
                    else
                    {
                        pay_type1 = -1;
                    }
                    it1.oper_type = "GM";
                    it1.voucher_no = ord3.sheet_no;
                    it1.sheet_no = "";
                    it1.oper_date = ord3.oper_date;
                    it1.old_money = old_money1;
                    it1.oper_money = ord3.total_amount;
                    it1.free_money = 0;
                    it1.new_money = it1.old_money + (it1.oper_money - it1.free_money) * pay_type1;
                    it1.pay_date = System.DateTime.Now;
                    d.Insert(it1);
                    //生成转入供应商的应结明细
                    rp_t_accout_payrec_flow ord1 = new rp_t_accout_payrec_flow();
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_accout_payrec_flow";
                    ord1.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord1.pay_type = 1;
                    else
                        ord1.pay_type = -1;
                    ord1.voucher_no = ord.sheet_no;
                    ord1.trans_no = "GM";//待确定
                    ord1.sheet_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord1.discount = 1;
                    ord1.paid_amount = 0;
                    ord1.pay_way = "";
                    ord1.tax_amount = 0;
                    ord1.pay_date = DateTime.Now;
                    ord1.supcust_no = Conv.ToString(tb.Rows[0]["supcust_from"]);
                    ord1.supcust_flag = "S";
                    ord1.free_money = 0;
                    ord1.oper_date = DateTime.Now;
                    ord1.other1 = "ARAP";
                    //ord1.approve_flag = "0";
                    d.Insert(ord1);

                    //生成转出供应商的应结明细
                    rp_t_accout_payrec_flow ord2 = new rp_t_accout_payrec_flow();
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_accout_payrec_flow";
                    ord2.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    if (Conv.ToDecimal(tb.Rows[0]["total_amount"]) >= 0)
                        ord2.pay_type = -1;
                    else
                        ord2.pay_type = 1;
                    ord2.voucher_no = ord3.sheet_no;
                    ord2.trans_no = "GM";//待确定
                    ord2.sheet_amount = Math.Abs(Conv.ToDecimal(tb.Rows[0]["total_amount"]));
                    ord2.discount = 1;
                    ord2.pay_way = "";
                    ord2.paid_amount = 0;
                    ord2.tax_amount = 0;
                    ord2.pay_date = DateTime.Now;
                    ord2.supcust_no = Conv.ToString(tb.Rows[0]["supcust_to"]);
                    ord2.supcust_flag = "S";
                    ord2.free_money = 0;
                    ord2.oper_date = DateTime.Now;
                    ord2.other1 = "ARAP";
                    d.Insert(ord2);
                }
                //sql = "update rp_t_accout_payrec_flow set approve_flag='1',approve_man='" + approve_man +
                //  "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where voucher_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);

                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->CheckArAp()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->CheckArAp()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void IARAP_SCPaymentBLL.NotCheckArAp(string sheet_no, string approve_man, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                sql = "select * from rp_t_arap_transformation where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                string supcust_from_flag;
                string supcust_to_flag;
                if (Conv.ToString(tb.Rows[0]["sheet_id"]) == "OR")
                {
                    supcust_from_flag = "C";
                    supcust_to_flag = "S";
                }
                else if (Conv.ToString(tb.Rows[0]["sheet_id"]) == "RR")
                {
                    supcust_from_flag = "C";
                    supcust_to_flag = "C";
                }
                else
                {
                    supcust_from_flag = "S";
                    supcust_to_flag = "S";
                }
                sql = "select * from rp_t_supcust_fy_master where supcust_no='" + Conv.ToString(tb.Rows[0]["supcust_from"]) + "' and supcust_flag='" + supcust_from_flag + "' and old_no ='" + sheet_no + "'";
                var tb2 = d.ExecuteToTable(sql, null);
                if (tb2.Rows.Count<1)
                {
                    throw new Exception("单据不存在rp_t_supcust_fy_master表中");
                }
                sql = "select * from rp_t_supcust_fy_master where supcust_no='" + Conv.ToString(tb.Rows[0]["supcust_to"]) + "' and supcust_flag='" + supcust_to_flag + "' and old_no ='" + sheet_no + "'";
                var tb3 = d.ExecuteToTable(sql, null);
                if (tb3.Rows.Count < 1)
                {
                    throw new Exception("单据不存在rp_t_supcust_fy_master表中");
                }
                sql = "select * from rp_t_recpay_record_detail  where voucher_no='" + Conv.ToString(tb2.Rows[0]["sheet_no"]) + "'";
                var tb1 = d.ExecuteToTable(sql, null);
/*                if (tb1.Rows.Count < 1)
                {
                    throw new Exception("单据不存在rp_t_recpay_record_detail表中");
                }*/
                sql = "select * from rp_t_recpay_record_detail  where voucher_no='" + Conv.ToString(tb3.Rows[0]["sheet_no"]) + "'";
                var tb4 = d.ExecuteToTable(sql, null);
/*                if (tb4.Rows.Count < 1)
                {
                    throw new Exception("单据不存在rp_t_recpay_record_detail表中");
                }*/
                if (tb1.Rows.Count > 0 || tb4.Rows.Count > 0)
                {
                    throw new Exception("收款单已经使用，请先删除收款单" + Conv.ToString(tb1.Rows[0]["sheet_no"]) + "或" + Conv.ToString(tb4.Rows[0]["sheet_no"]));
                }
                sql = "update rp_t_arap_transformation set approve_man='',approve_date=null,approve_flag='0' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_fy_master where sheet_no = '" + Conv.ToString(tb2.Rows[0]["sheet_no"]) + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_fy_master where sheet_no = '" + Conv.ToString(tb3.Rows[0]["sheet_no"]) + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_fy_detail where sheet_no = '" + Conv.ToString(tb2.Rows[0]["sheet_no"]) + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_fy_detail where sheet_no = '" + Conv.ToString(tb3.Rows[0]["sheet_no"]) + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_accout_payrec_flow where voucher_no = '" + Conv.ToString(tb2.Rows[0]["sheet_no"]) + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_accout_payrec_flow where voucher_no = '" + Conv.ToString(tb3.Rows[0]["sheet_no"]) + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_cash_flow where voucher_no = '" + Conv.ToString(tb2.Rows[0]["sheet_no"]) + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_supcust_cash_flow where voucher_no = '" + Conv.ToString(tb3.Rows[0]["sheet_no"]) + "'";
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->NotCheckArAp()", "反审成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
            }
            catch (Exception ex)
            {
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->NotCheckArAp()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                db.RollBackTran();

                throw;
            }
            finally
            {
                db.Close();
            }
        }
        void IARAP_SCPaymentBLL.ChangeArAp(rp_t_arap_transformation ord, out string sheet_no)
        {
            string sql = "select * from rp_t_arap_transformation where sheet_no='" + ord.sheet_no + "'";
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
                sql = "delete from rp_t_arap_transformation where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "delete from rp_t_accout_payrec_flow where voucher_no='" + ord.sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                //sql = "delete from rp_t_arap_transformation where sheet_no='" + ord.sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                //sheet_no = MaxCode(d, ord.sheet_id);
                sheet_no = ord.sheet_no;
                d.Insert(ord);


                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->ChangeArAp()", "更改成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->ChangeArAp()", sql + ":" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }


        /// <summary>
        /// 审核客户账期通知
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="approve_man"></param>
        /// <param name="update_time"></param>
        void IARAP_SCPaymentBLL.CheckNotice(string sheet_no, string approve_man, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                sql = "select * from rp_t_account_notice where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                decimal num = 0;
                sql = "select * from rp_t_account_notice_detail where sheet_no='" + sheet_no + "'";
                var tb1 = d.ExecuteToTable(sql, null);
                foreach (DataRow dr in tb1.Rows)
                {
                    sql = @"select * 
from rp_t_accout_payrec_flow p
where  p.voucher_no = '@voucher_no' "
           .Replace("@voucher_no", Conv.ToString(dr["voucher_no"]));
                    var tb2 = d.ExecuteToTable(sql, null);
                    //if (tb2.Rows.Count == 0)
                    //{
                    //    throw new Exception("应结明细中有单据已删除：" + Conv.ToString(dr["voucher_no"]));
                    //}
                    //if (Conv.ToString(tb2.Rows[0]["approve_flag"]) == "0")
                    //{
                    //    throw new Exception("应结明细中有单据未审核：" + Conv.ToString(dr["voucher_no"]));
                    //}
                    if (Conv.ToDecimal(dr["sheet_amount"]) != Conv.ToDecimal(tb2.Rows[0]["sheet_amount"]) * Conv.ToDecimal(tb2.Rows[0]["pay_type"]) || Conv.ToDecimal(dr["paid_amount"]) != Conv.ToDecimal(tb2.Rows[0]["paid_amount"]))
                    {
                        throw new Exception("应结明细表中数据已经被别人修改，请重新生成数据，被修改的业务单号是" + Conv.ToString(dr["voucher_no"]));
                    }
                    //num += Conv.ToDecimal(dr["paid_amount"]);
                    //sql = "update ot_account_notice_flow set wrltten_off='" + Conv.ToDecimal( tb2.Rows[0]["已核销金额"]) + "',sheet_amount='" + Conv.ToDecimal(tb2.Rows[0]["sheet_amount"]) + "' ,not_wrltten_off='" + (Conv.ToDecimal(tb2.Rows[0]["sheet_amount"])- Conv.ToDecimal(tb2.Rows[0]["已核销金额"])) + "', update_date=GETDATE() where sheet_no='"+sheet_no+ "'and blz_doc_no='" + Conv.ToString(dr["blz_doc_no"]) + "'";
                    //d.ExecuteScalar(sql, null);

                }
                //sql = "select top 1 * from rp_t_recpay_record_info where supcust_no='" + tb.Rows[0]["supcust_no"].ToString() + "' and supcust_flag='" + tb.Rows[0]["supcust_flag"].ToString() + "'  and approve_flag='1' ORDER BY  sheet_no DESC";
                //var tb3 = d.ExecuteToTable(sql, null);

                sql = "update rp_t_account_notice set approve_man='" + approve_man + "',approve_date=GETDATE(),approve_flag='1' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->CheckNotice()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->AddNotice()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->CheckNotice()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }

        }

        System.Data.DataTable IARAP_SCPaymentBLL.GetPaymentList()
        {
            //string sql = "select a.*,(a.visa_id+'/'+b.visa_nm) as visa_id_a,b.visa_nm as visa_name from bi_t_payment_info a left join bi_t_bank_info b on a.visa_id=b.visa_id order by a.pay_way left join ";
            string sql = "select a.*,0.00 as total_amount from bi_t_payment_info a  order by a.pay_way ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }
        /// <summary>
        /// 获取支付明细
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <returns></returns>
        DataTable IARAP_SCPaymentBLL.GetPaymentList(string sheet_no)
        {
            string sql = "select a.total_amount,(a.pay_way+'/'+b.pay_name) as pay_name from rp_t_collection_way a left join bi_t_payment_info b on b.pay_way=a.pay_way where a.sheet_no='" + sheet_no + "'  order by a.pay_way ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }
        /// <summary>
        /// 获取用户上次多收金额
        /// </summary>
        /// <param name="supcust_no"></param>
        /// <param name="is_cs"></param>
        /// <returns></returns>
        System.Data.DataTable IARAP_SCPaymentBLL.GetRecpayRecordModel(string supcust_no, string is_cs)
        {

            string sql = "select top 1 * from rp_t_recpay_record_info where supcust_no='" + supcust_no + "' and supcust_flag='" + is_cs + "'  and approve_flag='1' ORDER BY  sheet_no DESC";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }
        /// <summary>
        /// 获取账期通知单列表
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="cus_no"></param>
        /// <param name="is_cs"></param>
        /// <returns></returns>
        System.Data.DataTable IARAP_SCPaymentBLL.GetList(DateTime date1, DateTime date2, string cus_no, string is_cs)
        {
            string sql = "select a.*,b.sup_name,c.oper_name,(a.oper_id+'/'+c.oper_name) as oper_name_a,d.oper_name as approve_man_name,(a.approve_man+'/'+d.oper_name) as approve_man_a from rp_t_account_notice a left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and b.supcust_flag='" + is_cs + "' left join sa_t_operator_i c on c.oper_id=a.oper_id left join sa_t_operator_i d on d.oper_id=a.approve_man  where a.supcust_flag='" + is_cs + "' ";
            sql += "and a.oper_date>='" + date1.ToString("yyyy-MM-dd ") + "'";
            sql += "and a.oper_date<='" + date2.ToString("yyyy-MM-dd ") + "'";
            if (cus_no != "")
            {
                sql += " and a.supcust_no='" + cus_no + "'";
            }
            sql += " order by a.oper_date desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }
        /// <summary>
        /// 获取账期通知单和明细
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="is_cs"></param>
        /// <param name="tb1"></param>
        /// <param name="tb2"></param>
        void IARAP_SCPaymentBLL.GetOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            string sql = "";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            sql = "select a.*,b.sup_name,c.oper_name,d.oper_name as approve_man_name from rp_t_account_notice a left join bi_t_supcust_info b on a.supcust_no=b.supcust_no left join sa_t_operator_i c on c.oper_id=a.oper_id left join sa_t_operator_i d on d.oper_id=a.approve_man  where a.supcust_flag='" + is_cs + "' and a.sheet_no='" + sheet_no + "'";
            tb1 = db.ExecuteToTable(sql, null);
            sql = "select a.* from rp_t_account_notice_detail a where a.sheet_no='" + sheet_no + "'";

            tb2 = db.ExecuteToTable(sql, null);

        }

        void IARAP_SCPaymentBLL.SaveAgingGroup(DataTable dt)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //
                d.ExecuteScalar("delete from rp_t_aging_group where supcust_flag='" + Conv.ToString(dt.Rows[0]["supcust_flag"]) + "'", null);
                foreach (DataRow dr in dt.Rows)
                {
                    rp_t_aging_group line = new rp_t_aging_group();
                    line.start_days = Conv.ToInt(dr["start_days"]);
                    line.end_days = Conv.ToInt(dr["end_days"]);
                    line.supcust_flag = Conv.ToString(dr["supcust_flag"]);
                    d.Insert(line);
                }

                db.CommitTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->SaveAgingGroup()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING");
                //LogHelper.writeSheetLog("APAP_SCPaymentBLL ->SaveAgingGroup()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("APAP_SCPaymentBLL ->SaveAgingGroup()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        DataTable IARAP_SCPaymentBLL.GetAgingGroup(string is_cs)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from rp_t_aging_group where supcust_flag='" + is_cs + "'";

            DataTable dt = db.ExecuteToTable(sql, null);
            if (dt.Rows.Count == 0)
            {
                return dt;
            }
            dt.Rows[0][0] = DBNull.Value;
            dt.Rows[dt.Rows.Count - 1][1] = DBNull.Value;
            return dt;
        }
    }
}
