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
using System.Dynamic;

namespace IvyTran.BLL.ERP
{
    public class CashierBLL : ICashierBLL
    {
        //public CashierBLL()
        //{
        //    if (Conv.ToString(SessionHelper.oper_id) == "")
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
        /// 获取现金银行期初
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="is_jail"></param>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        System.Data.DataTable ICashierBLL.GetBankCashBeginbalance(string keyword, string is_jail, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,b.visa_nm,0 as delete_flag from rp_t_bank_beginbalance a left join bi_t_bank_info b on a.visa_id=b.visa_id where 1=1 ";
            //   if (region_no != "")
            //{
            //    sql += " and a.region_no like '" + region_no + "%'";
            //}
            if (keyword != "")
            {
                sql += " and (b.visa_nm like '%@%' or a.visa_id like '%@%')".Replace("@", keyword);
            }
            //if (is_jail != "")
            //{
            //    sql += " and a.is_jail = '1'";
            //}

            var tb = db.ExecuteToTable(sql, "a.visa_id", null, page_size, page_index, out total_count);
            return tb;

        }
        /// <summary>
        /// 获取现金银行信息
        /// </summary>
        /// <param name="region_no"></param>
        /// <param name="keyword"></param>
        /// <param name="is_cs"></param>
        /// <param name="is_jail"></param>
        /// <param name="page_index"></param>
        /// <param name="page_size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        System.Data.DataTable ICashierBLL.GetBankList(string keyword, string is_jail, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,0 as begin_balance,0 as pay_kind,0 as delete_flag from bi_t_bank_info a where display_flag='1' and  a.visa_id not in(select visa_id from rp_t_bank_beginbalance )  ";
            if (keyword != "")
            {
                sql += " and (a.visa_nm like '%@%' or a.visa_id like '%@%')".Replace("@", keyword);
            }
            var tb = db.ExecuteToTable(sql, "a.visa_id", null, page_size, page_index, out total_count);
            return tb;

        }

        /// <summary>
        /// 保存现金银行期初
        /// </summary>
        /// <param name="lr"></param>
        /// <param name="is_cs"></param>
        /// <param name="oper_id"></param>
         void ICashierBLL.SavaBankCashBeginbalance(List<rp_t_bank_beginbalance> lr, string oper_id)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();
                sql = "select * from rp_t_bank_beginbalance ";
                var tb2 = d.ExecuteToTable(sql, null);
                if (tb2.Rows.Count > 0)
                {
                    if(Conv.ToString(tb2.Rows[0]["approve_flag"])=="1")
                    throw new Exception("已经审核，不允许添加或修改数据，如需要添加或修改数据请反审");
                }
                string visa_id_list = "'" + lr[0].visa_id + "'";
            for (int i = 1; i < lr.Count; i++)
            {
                    visa_id_list += ",'" + lr[i].visa_id + "'";
            }
            sql = "select * from rp_t_bank_beginbalance where visa_id in (" + visa_id_list + ") ";
            var tb = d.ExecuteToTable(sql, null);
            foreach (rp_t_bank_beginbalance temp in lr)
            {
                //sql = "select count(*) from ot_supcust_beginbalance where supcust_no='" + temp.supcust_no + "'";
                //var tb = db.ExecuteToTable(sql, null);
                var tb1 = tb.AsEnumerable().Where(item => item["visa_id"].ToString() == temp.visa_id).ToArray();
                if (tb1.Length > 0)
                {
                    sql = "update rp_t_bank_beginbalance set begin_balance=" + temp.begin_balance + ",pay_kind=" + temp.pay_kind + ",oper_id='" + oper_id + "',update_time=GETDATE(),approve_flag='0' where visa_id='" + temp.visa_id + "' ";               
                }
                else
                {
                    sql = "insert into rp_t_bank_beginbalance values('" + temp.visa_id + "'," + temp.begin_balance + ",GETDATE(),'" + oper_id + "'," + temp.pay_kind + ",'0')";
                }
                //sql = "update ot_supcust_beginbalance set begin_balance=" + temp.begin_balance + ",pay_kind="+temp.pay_kind+",oper_id='" + oper_id + "',update_time=GETDATE() where supcust_no='" + temp.supcust_no + "'  and supcust_flag='" + is_cs + "' ";
                d.ExecuteToTable(sql, null);
            }

            db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->SavaBankCashBeginbalance()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING");
                //LogHelper.writeSheetLog("CashierBLL ->SavaBankCashBeginbalance()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->SavaBankCashBeginbalance()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 删除现金银行期初
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="is_cs"></param>
        /// <param name="update_time"></param>
        void ICashierBLL.DeleteBankCashBeginbalance(string visa_id, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //
                sql = "select * from rp_t_bank_beginbalance ";
                var tb2 = d.ExecuteToTable(sql, null);
                if (tb2.Rows.Count > 0)
                {
                    if (Conv.ToString(tb2.Rows[0]["approve_flag"]) == "1")
                        throw new Exception("已经审核，不允许删除数据，如需要删除数据请反审");
                }
                sql = "delete from rp_t_bank_beginbalance where visa_id in(" + visa_id + ") ";
                d.ExecuteScalar(sql, null);

                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->DeleteBankCashBeginbalance()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING",visa_id);
                //LogHelper.writeSheetLog("CashierBLL ->DeleteBankCashBeginbalance()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",visa_id);
            }
            catch (Exception ex)
            {
               
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->DeleteBankCashBeginbalance()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", visa_id);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void ICashierBLL.CheckBankCashBeginbalance()
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //

                sql = "select * from rp_t_bank_beginbalance ";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("无数据不允许审核");
                }
                sql = "update rp_t_bank_beginbalance set approve_flag='1'";
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->CheckBankCashBeginbalance()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING");
                //LogHelper.writeSheetLog("CashierBLL ->DeleteBankCashBeginbalance()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",visa_id);
            }
            catch (Exception ex)
            {

                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->CheckBankCashBeginbalance()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        void ICashierBLL.NotCheckBankCashBeginbalance()
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //

                sql = "select * from rp_t_bank_beginbalance ";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("无数据不允许反审");
                }
                sql = "update rp_t_bank_beginbalance set approve_flag='0'";
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->NotCheckBankCashBeginbalance()", "反审成功！", SessionHelper.oper_id, "操作日志", "WARNING");
                //LogHelper.writeSheetLog("CashierBLL ->DeleteBankCashBeginbalance()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",visa_id);
            }
            catch (Exception ex)
            {

                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->NotCheckBankCashBeginbalance()", sql + "/n" + ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR");
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 获取收付款单列表
        /// </summary>
        /// <param name="is_cs"></param>
        /// <returns></returns>
        System.Data.DataTable ICashierBLL.GetCollectionList(string type, string supcust_no, string visa_id, string start_date, string end_date)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select (a.visa_id+'/'+b.visa_nm) as visa_id,a.supcust_no,c.sup_name,a.sheet_no as voucher_no,a.oper_date,'' as pay_way,a.total_amount as  pay_amount from  rp_t_recpay_record_info a  left join bi_t_bank_info b on a.visa_id=b.visa_id left join bi_t_supcust_info c on a.supcust_no=c.supcust_no and a.supcust_flag=c.supcust_flag where left(a.sheet_no,2)='" + type+"' and other3='0' and approve_flag='1' ";
            if (supcust_no != "")
            {
                string is_cs = type == "CP" ? "C" : "S";
                sql += " and a.supcust_no='" + supcust_no + "' and a.supcust_flag='"+is_cs+"'";
            }
            if (visa_id != "")
            {
                sql += " and a.visa_id='" + visa_id + "' ";
            }
            if (start_date != "")
            {
                sql += " and Convert(varchar(10), a.oper_date ,20 ) >= '" + start_date + "'";
            }
            if (end_date != "")
            {
                sql += " and Convert(varchar(10), a.oper_date ,20 ) <= '" + end_date + "'";
            }

            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        /// <summary>
        /// 获取收款方式列表
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <returns></returns>
        DataTable ICashierBLL.GetCollectionWayList(string sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.sheet_no,(a.pay_way+'/'+b.pay_name) as pay_way,a.total_amount from rp_t_collection_way a left join bi_t_payment_info b on a.pay_way=b.pay_way  where 1=1 ";
            if (sheet_no != "")
            {
                sql += "a.sheet_no='"+sheet_no+"'";
            }
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        /// <summary>
        /// 保存客户供应商收付款
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="lines"></param>
        /// <param name="sheet_no"></param>
        void ICashierBLL.AddCollectionPay(Model.rp_t_pay_info ord, List<Model.rp_t_pay_detail> lines, out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();

                sheet_no = MaxCode(d, "SF");
                ord.sheet_no = sheet_no;
                d.Insert(ord);


                foreach (rp_t_pay_detail line in lines)
                {
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_pay_detail";
                    line.sheet_no = sheet_no;
                    line.flow_no = Helper.Conv.ToInt(d.ExecuteScalar(sql, null));

                    d.Insert(line);

                }
                //
                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->AddCollectionPay()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->AddCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->AddCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
      /// <summary>
      /// 修改收付款单
      /// </summary>
      /// <param name="ord"></param>
      /// <param name="lines"></param>
      /// <param name="sheet_no"></param>
        void ICashierBLL.ChangeCollectionPay(Model.rp_t_pay_info ord, List<Model.rp_t_pay_detail> lines, out string sheet_no)
        {
            string sql = "select * from rp_t_pay_info where sheet_no='" + ord.sheet_no + "'";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                sheet_no = ord.sheet_no;
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
                sql = "delete from rp_t_pay_info where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_pay_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //string sheet_no;
                //sheet_no = MaxCode(d, "ZQ");
                //ord.sheet_no = sheet_no;
                d.Insert(ord);
                foreach (Model.rp_t_pay_detail line in lines)
                {
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_pay_detail";
                    line.sheet_no = ord.sheet_no;
                    line.flow_no = Helper.Conv.ToInt(d.ExecuteScalar(sql, null));

                    d.Insert(line);
                }
                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->ChangeCollectionPay()", "修改成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->AddCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->ChangeCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 删除收付款单
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="update_time"></param>
        void ICashierBLL.DeleteCollectionPay(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                db.Open();

                db.BeginTran();
                //
                sql = "select * from rp_t_pay_info where sheet_no='" + sheet_no + "'";
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
                    if (Helper.Conv.ToDateTime(row["oper_date"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                sql = "delete from rp_t_pay_info where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from rp_t_pay_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->DeleteCollectionPay()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->AddCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->DeleteCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void CashMaster(IDB d, dynamic item)
        {
            Model.bank_t_cash_master it = new Model.bank_t_cash_master();
            it.sheet_no = GetSheetCode(d, "KZ");
            it.branch_no = item.branch_no;
            it.voucher_no = item.settle_sheet_no;
            it.visa_id = item.visa_id;
            it.visa_in = "";
            it.pay_way = item.pay_way;
            it.coin_no = "RMB";
            it.coin_rate = 1;
            it.deal_man = "";
            it.oper_id = item.oper_id;
            it.oper_date = System.DateTime.Now;
            it.bill_total = item.pay_amount;
            it.bill_flag = item.type_no;
            it.cm_branch = "00";
            it.approve_flag = "1";
            it.approve_man = item.approve_man;
            it.approve_date = System.DateTime.Now;
            it.other1 = item.memo;
            it.other2 = "";
            it.other3 = "";
            it.num1 = 0;
            it.num2 = 0;
            it.num3 = 0;
            d.Insert(it);
            //Model.bank_t_cash_detail line = new Model.bank_t_cash_detail();
            //string sql = "select isnull(max(flow_id),0)+1 from bank_t_cash_detail ";
            //line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
            //line.sheet_no = it.sheet_no;
            //line.type_no = item.type_no;
            //line.bill_cash = item.pay_amount;
            //line.memo = "";
            //d.Insert(line);
            //
            //it.approve_date = System.DateTime.Now;
            //it.approve_flag = "1";
            // it.approve_man = item.approve_man;
            // d.Update(it, "sheet_no", "approve_date,approve_flag,approve_man");

        }
        /// <summary>
        /// 获取单据编码
        /// </summary>
        /// <param name="db"></param>
        /// <param name="front_str"></param>
        /// <returns></returns>
        private string GetSheetCode(DB.IDB db, string front_str)
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
                    index = 1;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + front_str + "'";
                db.ExecuteScalar(sql, null);
                return front_str + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }

        }
        /// <summary>
        /// 审核收付款单
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="approve_man"></param>
        /// <param name="update_time"></param>
        void ICashierBLL.CheckCollectionPay(string sheet_no, string approve_man, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
           string sql = "";
            try
            {
                //
                db.Open();
                db.BeginTran();
                sql = "select * from rp_t_pay_info where sheet_no='" + sheet_no + "'";
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
                    if (Helper.Conv.ToDateTime(row["oper_date"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                dynamic dyn = new ExpandoObject();
                //if (Conv.ToString(dr["pay_way"]) == "")
                //    dyn.pay_way = "";
                //else
                //    
                dyn.pay_way = "";
                dyn.branch_no = "";
                dyn.sheet_no = sheet_no;
                dyn.settle_sheet_no = sheet_no;
                dyn.visa_id = Conv.ToString(tb.Rows[0]["visa_id"]);
                if(Conv.ToString(tb.Rows[0]["supcust_flag"])=="C")
                dyn.pay_amount = Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                else
                    dyn.pay_amount = 0-Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                dyn.ml_amount = 0;//待商讨
                if (Conv.ToString(tb.Rows[0]["supcust_flag"]) == "C")
                    dyn.type_no = "K";
                else
                    dyn.type_no = "G";

                dyn.oper_id = approve_man;//待商讨
                dyn.approve_man = approve_man;
                dyn.memo = Conv.ToString(tb.Rows[0]["other1"]);
                dyn.supcust_no = "";
                dyn.supcust_flag = "";


                //CheckSheet check = new CheckSheet();
                //check.CashMaster(d, dyn);
                CashMaster(d, dyn);
                sql = "select * from rp_t_pay_detail where sheet_no='" + sheet_no + "'";
                var tb1 = d.ExecuteToTable(sql, null);
                decimal num = 0;
                int count = tb1.Rows.Count;
                int index = 0;
                string voucher_no = Conv.ToString(tb1.Rows[0]["voucher_no"]);

                foreach (DataRow dr in tb1.Rows)
                {
                   
                   
                    if (Conv.ToString(dr["voucher_no"]) == voucher_no)
                    {
                        num += Conv.ToDecimal(dr["pay_amount"]);
                    }else
                    {
                        sql = @"select * 
                        from rp_t_recpay_record_info p
                        where  p.sheet_no = '@sheet_no' "
               .Replace("@sheet_no", voucher_no);
                        var tb2 = d.ExecuteToTable(sql, null);
                        if (num != Conv.ToDecimal(tb2.Rows[0]["total_amount"])|| Conv.ToString(tb2.Rows[0]["other3"])=="1")
                        {
                            throw new Exception("结算单表中数据已经被别人修改，请重新生成数据，被修改的业务单号是" + voucher_no);
                        }
                        num = 0;
                        num+= Conv.ToDecimal(dr["pay_amount"]);                     
                        sql = "update rp_t_recpay_record_info set other3='1' where sheet_no='" + voucher_no + "'";
                        d.ExecuteScalar(sql, null);
                        voucher_no = Conv.ToString(dr["voucher_no"]);
                        
                    }
                    if (index == count - 1)
                    {
                        sql = @"select * 
                        from rp_t_recpay_record_info p
                        where  p.sheet_no = '@sheet_no' "
               .Replace("@sheet_no", Conv.ToString(dr["voucher_no"]));
                        var tb2 = d.ExecuteToTable(sql, null);
                        if (num != Conv.ToDecimal(tb2.Rows[0]["total_amount"]) || Conv.ToString(tb2.Rows[0]["other3"]) == "1")
                        {
                            throw new Exception("结算单表中数据已经被别人修改，请重新生成数据，被修改的业务单号是" + Conv.ToString(dr["voucher_no"]));
                        }
                        sql = "update rp_t_recpay_record_info set other3='1' where sheet_no='" + Conv.ToString(dr["voucher_no"]) + "'";
                        d.ExecuteScalar(sql, null);
                    }
                        index++;                  
                }
                
                sql = "update rp_t_pay_info set approve_man='" + approve_man + "',approve_date=GETDATE(),approve_flag='1' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->CheckCollectionPay()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->CheckCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);

            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->CheckCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// 反审收付款单
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="approve_man"></param>
        /// <param name="update_time"></param>
        void ICashierBLL.NotCheckCollectionPay(string sheet_no, string approve_man, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            string sql = "";
            try
            {
                //
                db.Open();
                db.BeginTran();
                sql = "select * from rp_t_pay_info where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (Helper.Conv.ToDateTime(row["oper_date"]) > update_time)
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                sql = "delete from bank_t_cash_master   where voucher_no='" +sheet_no+"' ";
                d.ExecuteScalar(sql, null);
                sql = "select * from rp_t_pay_detail where sheet_no='" + sheet_no + "'";
                var tb1 = d.ExecuteToTable(sql, null);
                for (int i=0;i<tb1.Rows.Count;i++)
                {
                    sql = "update rp_t_recpay_record_info set other3='0' where sheet_no='" + Conv.ToString(tb1.Rows[i]["voucher_no"]) + "'";
                    d.ExecuteScalar(sql, null);                   
                    //sql = "select * from bank_t_cash_master where voucher_no='" + Conv.ToString(tb1.Rows[i]["voucher_no"]) + "'";
                    //var tb3 = d.ExecuteToTable(sql, null);
                    //if (tb3.Rows.Count == 1)
                    //{
                    //    sql = "delete from bank_t_cash_master   where voucher_no='" + Conv.ToString(tb1.Rows[i]["voucher_no"]) + "'";
                    //    d.ExecuteScalar(sql, null);
                    //    sql = "delete from bank_t_cash_detail   where sheet_no='" + Conv.ToString(tb3.Rows[0]["sheet_no"]) + "'";
                    //    d.ExecuteScalar(sql, null);
                    //}else if (tb3.Rows.Count > 1)
                    //{
                    //    for (int j = 0; j < tb3.Rows.Count; j++)
                    //    {
                    //        sql = "delete from bank_t_cash_detail   where sheet_no='" + Conv.ToString(tb3.Rows[j]["sheet_no"]) + "'";
                    //        d.ExecuteScalar(sql, null);
                    //    }
                    //    sql = "delete from bank_t_cash_master   where voucher_no='" + Conv.ToString(tb1.Rows[i]["voucher_no"]) + "'";
                    //    d.ExecuteScalar(sql, null);
                    //    i += tb3.Rows.Count - 1;
                    //}



                }

                sql = "update rp_t_pay_info set approve_man='',approve_date=null,approve_flag='0' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->NotCheckCollectionPay()", "反审成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->CheckCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);

            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->NotCheckCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw new Exception(ex.ToString());
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// 获取收付款单列表
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="is_cs"></param>
        /// <returns></returns>
        System.Data.DataTable ICashierBLL.GetCollectionPayList(DateTime date1, DateTime date2, string is_cs, string visa_id)
        {
           
            string sql = "select a.approve_flag,a.sheet_no,a.supcust_no,b.sup_name,(a.visa_id+'/'+e.visa_nm) as visa_id,a.total_amount,(a.approve_man+'/'+d.oper_name) as approve_man,a.approve_date,(a.oper_id+'/'+c.oper_name) as oper_id,a.oper_date,a.other1  from rp_t_pay_info a left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and b.supcust_flag='" + is_cs + "' left join sa_t_operator_i c on c.oper_id=a.oper_id left join sa_t_operator_i d on d.oper_id=a.approve_man   left join bi_t_bank_info e on a.visa_id=e.visa_id   where a.supcust_flag='" + is_cs + "' ";
            sql += "and a.oper_date>='" + date1.ToString("yyyy-MM-dd ") + "'";
            sql += "and a.oper_date<='" + date2.ToString("yyyy-MM-dd ") + "'";
            if (visa_id != "")
            {
                sql += " and a.visa_id='" + visa_id + "'";
            }
            sql += " order by a.oper_date desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            //LogHelper.writeSheetLog("CashierBLL ->GetCollectionPayList()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING");
            //LogHelper.writeSheetLog("CashierBLL ->CheckCollectionPay()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            return db.ExecuteToTable(sql, null);
        }
        /// <summary>
        /// 获取收付款单和明细
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="is_cs"></param>
        /// <param name="tb1"></param>
        /// <param name="tb2"></param>
        void ICashierBLL.GetCollectionPayOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            string sql = "";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            sql = "select a.start_date,a.end_date,a.approve_flag,a.sheet_no,a.supcust_no,b.sup_name,a.visa_id as visa_id_a,e.visa_nm,(a.visa_id+'/'+e.visa_nm) as visa_id,a.total_amount,a.approve_man as approve_man_a,d.oper_name as approve_man_name,(a.approve_man+'/'+d.oper_name) as approve_man,a.approve_date,a.oper_id as oper_id_a,c.oper_name,(a.oper_id+'/'+c.oper_name) as oper_id,a.oper_date,a.other1  from rp_t_pay_info a left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and b.supcust_flag='" + is_cs + "' left join sa_t_operator_i c on c.oper_id=a.oper_id left join sa_t_operator_i d on d.oper_id=a.approve_man   left join bi_t_bank_info e on a.visa_id=e.visa_id   where a.supcust_flag='" + is_cs + "'  and a.sheet_no='" + sheet_no + "'";
            tb1 = db.ExecuteToTable(sql, null);
          
            sql = "select a.visa_id as visa_id_a,b.visa_nm,(a.visa_id+'/'+b.visa_nm) as visa_id,a.supcust_no,c.sup_name,a.sheet_no as voucher_no,a.oper_date,a.pay_way as pay_way_a,f.pay_name,(a.pay_way+'/'+f.pay_name) as pay_way,a.pay_amount from  rp_t_pay_detail a  left join bi_t_bank_info b on a.visa_id=b.visa_id left join bi_t_supcust_info c on a.supcust_no=c.supcust_no and a.supcust_flag=c.supcust_flag left join bi_t_payment_info f on a.pay_way=f.pay_way  where a.sheet_no='" + sheet_no + "'";

            tb2 = db.ExecuteToTable(sql, null);

        }
        /// <summary>
        /// 获取其他收入支出列表
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="is_cs"></param>
        /// <returns></returns>
        System.Data.DataTable ICashierBLL.GetOtherList(DateTime date1, DateTime date2, string is_cs)
        {
            string code= is_cs == "C" ? "CR" : "SR";
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
            sql += " where sheet_no like '"+ code + "%'";
            sql += " and a.oper_date>='" + date1.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            sql += " and a.oper_date<='" + date2.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
            //if (visa_id != "")
            //{
            //    sql += " and a.visa_id='" + visa_id + "'";
            //}
            sql += " order by a.oper_date desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }
        /// <summary>
        /// 获取其他收入支出单明细
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="tb1"></param>
        /// <param name="tb2"></param>
        void ICashierBLL.GetOtherOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
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

        string  MaxCodes(string code)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string front_str = code;//"SR";
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
        /// <summary>
        /// 保存其他收入支出
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="lines"></param>
        /// <param name="sheet_no"></param>
        void ICashierBLL.AddOther(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines, out string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            ICashierBLL order = this;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();
                //
                if (ord.bill_flag == "S")
                {
                    sheet_no = ord.sheet_no = MaxCodes("SR");
                }
                else
                {
                    sheet_no = ord.sheet_no = MaxCodes("CR");
                }
                sql = "select * from bank_t_cash_master where sheet_no='" + ord.sheet_no + "'";
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
                LogHelper.writeSheetLog("CashierBLL ->AddOther()", "添加成功！", SessionHelper.oper_id, "操作日志", "WARNING",ord.sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->AddOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->AddOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 修改其他收入支出
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="lines"></param>
        void ICashierBLL.ChangeOther(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines)
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
                LogHelper.writeSheetLog("CashierBLL ->ChangeOther()", "更改成功！", SessionHelper.oper_id, "操作日志", "WARNING", ord.sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->ChangeOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->ChangeOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", ord.sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 删除其他收入支出
        /// </summary>
        /// <param name="sheet_no"></param>
        void ICashierBLL.DeleteOther(string sheet_no)
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
                LogHelper.writeSheetLog("CashierBLL ->DeleteOther()", "删除成功！", SessionHelper.oper_id, "操作日志", "WARNING",sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->DeleteOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->DeleteOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 审核其他收入支出
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="approve_man"></param>
        void ICashierBLL.CheckOther(string sheet_no, string approve_man)
        {
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
                //CashBalance(d, dyn);

                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->CheckOther()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->CheckOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->CheckOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// 反审其他收入支出
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="approve_man"></param>
        void ICashierBLL.NotCheckOther(string sheet_no, string approve_man)
        {
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
                sql = "update bank_t_cash_master set approve_flag='0',approve_man='',approve_date='' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //CashBalance(d, dyn);

                db.CommitTran();
                LogHelper.writeSheetLog("CashierBLL ->NotCheckOther()", "反审成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CashierBLL ->CheckOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CashierBLL ->NotCheckOther()",sql+"/n"+ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
