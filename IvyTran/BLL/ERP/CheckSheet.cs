using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using Aop.Api.Domain;
using DB;

using IvyTran.Helper;
using Model;
using ic_t_inout_store_detail = Model.ic_t_inout_store_detail;
using ic_t_inout_store_master = Model.ic_t_inout_store_master;
using rp_t_supcust_fy_master = Model.rp_t_supcust_fy_master;
using sm_t_salesheet = Model.sm_t_salesheet;
using sm_t_salesheet_detail = Model.sm_t_salesheet_detail;

namespace IvyTran.BLL.ERP
{
    public class CheckSheet
    {
        public void CheckZT(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select approve_flag,update_time from sm_t_salesheet_recpay_main where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    sql = "update sm_t_salesheet_recpay_main set approve_flag='1',approve_man='" + approve_man + "'" +
                          ",approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                    sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                    d.ExecuteScalar(sql, null);

                    #region 通过batch_num

                    sql = @"select a.flow_id sflow_id,a.sheet_no ssheet_no,b.batch_num,a.item_no sitem_no
,a.real_qnty qty
,a.sheet_sort ssheet_sort,c.sheet_sort psheet_sort,c.sheet_no psheet_no
,c.flow_id pflow_id,c.voucher_no
,c.branch_no_d pbraanch_no,a.item_no
,c.barcode pbarcode,b.batch_num,c.batch_num pbatch_num,sm.cust_no,d.supcust_no
 from [dbo].[sm_t_salesheet_recpay_detail] a left join 
[dbo].[sm_t_salesheet_detail] b 
on a.task_flow_id=b.flow_id and b.sheet_no=a.sheet_no 
left join [dbo].[ic_t_inout_store_detail] c on c.batch_num=b.batch_num and c.sheet_no like '%PI%'
left join [dbo].[ic_t_inout_store_master] d on d.sheet_no=c.sheet_no
left join [dbo].[sm_t_salesheet] sm on sm.sheet_no=b.sheet_no  where a.sheet_no='" + sheet_no + "'";
                    DataTable dt = d.ExecuteToTable(sql, null);
                    if (dt.Rows.Count > 0)
                    {
                        ArrayList al1 = new ArrayList();
                        ArrayList al = new ArrayList();
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!al1.Contains(row["psheet_no"].ToString()))
                            {
                                al1.Add(row["psheet_no"].ToString());
                                al.Add(row["psheet_no"].ToString() + '/' + row["supcust_no"].ToString() + '/' + row["pbraanch_no"].ToString());
                            }
                        }
                        foreach (var a in al)
                        {
                            string[] arr = a.ToString().Split('/');
                            sql = @"select * from [dbo].[ic_t_inoutstore_recpay_main] where sheet_no='" + arr[0] + "'";
                            if (d.ExecuteToTable(sql, null).Rows.Count <= 0)
                            {
                                IvyTran.body.ic_t_inoutstore_recpay_main main = new IvyTran.body.ic_t_inoutstore_recpay_main();
                                main.sheet_no = arr[0];
                                main.cust_no = arr[1];
                                main.branch_no = arr[2];
                                main.oper_id = approve_man;
                                main.approve_flag = "0";
                                main.approve_date = DateTime.MinValue;
                                main.create_time = DateTime.Now;
                                main.update_time = DateTime.Now;
                                main.memo = "";
                                main.num1 = 0;
                                main.num2 = 0;
                                main.num3 = 0;
                                main.pay_date = DateTime.MinValue;
                                main.oper_date = DateTime.Now;
                                main.other1 = "";
                                main.other2 = "";
                                main.other3 = "";
                                d.Insert(main);
                            }


                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            DataRow[] dr = dt.Select("pflow_id='" + row["pflow_id"].ToDecimal() + "'");
                            sql = "select * from ic_t_inoutstore_recpay_detail where task_flow_id='" + row["pflow_id"].ToDecimal() + "'";
                            DataTable t = d.ExecuteToTable(sql, null);
                            if (t.Rows.Count <= 0 && dr.Length == 1)
                            {
                                IvyTran.body.ic_t_inoutstore_recpay_detail detail = new IvyTran.body.ic_t_inoutstore_recpay_detail();
                                detail.sheet_no = row["psheet_no"].ToString();
                                detail.batch_num = row["batch_num"].ToString();
                                sql = @"select Max(isnull(flow_id,0))+1 flow_id from ic_t_inoutstore_recpay_detail";
                                detail.flow_id = d.ExecuteToTable(sql, null).Rows[0]["flow_id"].ToDecimal();
                                detail.item_no = row["item_no"].ToString();
                                detail.sheet_sort = Convert.ToInt32(row["psheet_sort"]);
                                detail.task_flow_id = row["pflow_id"].ToDecimal();
                                detail.real_qnty = row["qty"].ToDecimal();
                                detail.voucher_no = row["voucher_no"].ToString();
                                detail.num1 = 0;
                                detail.num2 = 0;
                                detail.num3 = 0;
                                d.Insert(detail);
                            }
                            else if (dr.Length > 1)
                            {
                                decimal qty = 0;
                                foreach (var dataRow in dr)
                                {
                                    qty += dataRow["qty"].ToDecimal();
                                }
                                if (t.Rows.Count <= 0)
                                {
                                    IvyTran.body.ic_t_inoutstore_recpay_detail detail = new IvyTran.body.ic_t_inoutstore_recpay_detail();
                                    detail.sheet_no = row["psheet_no"].ToString();
                                    detail.batch_num = row["batch_num"].ToString();
                                    sql = @"select Max(isnull(flow_id,0))+1 flow_id from ic_t_inoutstore_recpay_detail";
                                    detail.flow_id = d.ExecuteToTable(sql, null).Rows[0]["flow_id"].ToDecimal();
                                    detail.item_no = row["item_no"].ToString();
                                    detail.sheet_sort = Convert.ToInt32(row["psheet_sort"]);
                                    detail.task_flow_id = row["pflow_id"].ToDecimal();
                                    detail.real_qnty = qty;
                                    detail.voucher_no = row["voucher_no"].ToString();
                                    detail.num1 = 0;
                                    detail.num2 = 0;
                                    detail.num3 = 0;
                                    d.Insert(detail);
                                }
                                else
                                {
                                    sql = "update ic_t_inoutstore_recpay_detail set real_qnty='" + qty + "' where task_flow_id='" + row["pflow_id"].ToDecimal() + "'";
                                    d.ExecuteScalar(sql, null);
                                }
                            }
                            else
                            {
                                sql = "update ic_t_inoutstore_recpay_detail set real_qnty='" + row["qty"].ToDecimal() + "' where task_flow_id='" + row["pflow_id"].ToDecimal() + "'";
                                d.ExecuteScalar(sql, null);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("未能找到匹配项，无法创建采购入库调整单订单");
                    }

                    #endregion
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckSSSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        public void CheckInOutZT(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select approve_flag,update_time from ic_t_inoutstore_recpay_main where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    sql = "update ic_t_inoutstore_recpay_main set approve_flag='1',approve_man='" + approve_man + "'" +
                          ",approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                    sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                    d.ExecuteScalar(sql, null);

                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckSSSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
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
        /// 应结明细
        /// </summary>
        private void ShouldKnotDetail(IDB d, dynamic item)
        {
            Model.rp_t_accout_payrec_flow it = new Model.rp_t_accout_payrec_flow();
            string sql = "select isnull(max(flow_no),0)+1 from rp_t_accout_payrec_flow";
            it.flow_no = IvyTran.Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
            it.pay_type = item.pay_type;
            it.voucher_no = item.sheet_no;
            it.trans_no = item.trans_no;
            it.sheet_amount = item.total_amount;
            it.discount = 1;
            it.paid_amount = 0;
            it.tax_amount = 0;
            it.pay_way = item.pay_way == null ? "" : item.pay_way;
            it.pay_date = System.DateTime.Now;
            it.supcust_no = item.supcust_no;
            it.supcust_flag = item.supcust_flag;
            it.free_money = item.free_money;
            it.memo = item.memo;
            it.other1 = "";
            it.other2 = "";
            it.other3 = "";
            it.num1 = 0;
            it.num2 = 0;
            it.num3 = 0;
            it.branch_no = "00";
            it.sale_no = item.sale_no;
            it.oper_date = item.oper_date;

            d.Insert(it);
        }

        /// <summary>
        /// 客户流水(应收)
        /// </summary>
        private void CustReceivableFlow(IDB d, dynamic item)
        {
            string sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no=@supcust_no and supcust_flag='C' order by flow_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_no",item.supcust_no)
            };
            var dt1 = d.ExecuteToTable(sql, pars);
            decimal old_money = 0;
            if (dt1.Rows.Count > 0)
            {
                old_money = Conv.ToDecimal(dt1.Rows[0]["new_money"]);
            }

            Model.rp_t_supcust_cash_flow it = new Model.rp_t_supcust_cash_flow();

            sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
            it.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
            it.supcust_no = item.supcust_no;
            it.supcust_flag = "C";
            it.oper_type = item.oper_type;
            it.voucher_no = item.sheet_no;
            it.sheet_no = "";
            it.oper_date = item.oper_date;
            it.old_money = old_money;
            it.oper_money = item.total_amount;
            it.free_money = 0;
            it.new_money = it.old_money + (it.oper_money - it.free_money) * item.pay_type;
            it.pay_date = System.DateTime.Now;
            d.Insert(it);
        }

        /// <summary>
        /// 客户流水(收款)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="item"></param>
        private void CustPayFlow(IDB d, dynamic item)
        {
            string sql = "select  top 1 * from rp_t_supcust_cash_flow where supcust_no=@supcust_no and supcust_flag='C' order by flow_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_no", item.supcust_no)
            };
            var dt1 = d.ExecuteToTable(sql, pars);
            decimal old_money = 0;
            if (dt1.Rows.Count > 0)
            {
                old_money = Helper.Conv.ToDecimal(dt1.Rows[0]["new_money"]);
            }
            Model.rp_t_supcust_cash_flow it = new Model.rp_t_supcust_cash_flow();

            sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
            it.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
            it.supcust_no = item.supcust_no;
            it.supcust_flag = "C";
            it.oper_type = item.oper_type;
            it.voucher_no = item.settle_sheet_no;
            it.sheet_no = "";
            it.oper_date = item.oper_date;
            it.old_money = old_money;
            it.oper_money = item.pay_amount;
            it.free_money = item.ml_amount;
            it.new_money = it.old_money - it.oper_money - it.free_money;
            it.pay_date = System.DateTime.Now;
            d.Insert(it, "");
        }

        /// <summary>
        /// 客户价格
        /// </summary>
        private void CustPrice(IDB d, dynamic item)
        {
            string sql = "select * from bi_t_cust_price where cust_id='" + item.cust_no + "'" +
                      " and item_no='" + item.detail.item_no + "'";
            Model.bi_t_cust_price it = d.ExecuteToModel<Model.bi_t_cust_price>(sql, null);
            if (it == null)
            {
                it = new Model.bi_t_cust_price();
                it.cust_id = item.cust_no;
                it.item_no = item.detail.item_no;
                it.price_type = "0";
                it.new_price = item.detail.sale_price;
                it.discount = 1;
                it.oper_date = System.DateTime.Now;
                it.oper_id = item.oper_id;
                it.other1 = "";
                it.other2 = "";
                it.other3 = "";
                it.top_price = item.detail.sale_price;
                it.bottom_price = item.detail.sale_price;
                it.last_price = item.detail.sale_price;
                it.top_sheet_no = item.sheet_no;
                it.bottom_sheet_no = item.sheet_no;
                it.last_sheet_no = item.sheet_no;
                it.num1 = 0;
                it.num2 = 0;
                it.num3 = 0;
                it.update_time = System.DateTime.Now;
                d.Insert(it);
            }
            else
            {
                if (it.top_price < item.detail.sale_price)
                {

                    it.new_price = item.detail.sale_price;
                    it.oper_date = System.DateTime.Now;
                    it.oper_id = item.oper_id;
                    it.top_price = item.detail.sale_price;
                    it.bottom_price = it.bottom_price;
                    it.last_price = item.detail.sale_price;
                    it.top_sheet_no = item.sheet_no;
                    it.bottom_sheet_no = it.bottom_sheet_no;
                    it.last_sheet_no = item.sheet_no;
                    it.update_time = System.DateTime.Now;
                }
                else if (it.bottom_price > item.detail.sale_price)
                {
                    it.new_price = item.detail.sale_price;
                    it.oper_date = System.DateTime.Now;
                    it.oper_id = item.oper_id;
                    it.top_price = it.top_price;
                    it.bottom_price = item.detail.sale_price;
                    it.last_price = item.detail.sale_price;
                    it.top_sheet_no = it.top_sheet_no;
                    it.bottom_sheet_no = item.sheet_no;
                    it.last_sheet_no = item.sheet_no;
                    it.update_time = System.DateTime.Now;
                }
                else
                {
                    it.new_price = item.detail.sale_price;
                    it.oper_date = System.DateTime.Now;
                    it.oper_id = item.oper_id;
                    it.top_price = it.top_price;
                    it.bottom_price = it.bottom_price;
                    it.last_price = item.detail.sale_price;
                    it.top_sheet_no = it.top_sheet_no;
                    it.bottom_sheet_no = it.bottom_sheet_no;
                    it.last_sheet_no = item.sheet_no;
                    it.update_time = System.DateTime.Now;
                }
                d.Update(it, "cust_id,item_no", "new_price,oper_date,oper_id,top_price,bottom_price,last_price,top_sheet_no,bottom_sheet_no,last_sheet_no,update_time");
            }
            body.bi_t_cust_price_history ctHistory = new body.bi_t_cust_price_history
            {
                cust_id = it.cust_id,
                item_no = it.item_no,
                price_type = "0",
                new_price = it.new_price,
                discount = 1,
                oper_date = System.DateTime.Now,
                oper_id = it.oper_id,
                other1 = "",
                other2 = "",
                other3 = "",
                top_price = it.top_price,
                bottom_price = it.bottom_price,
                last_price = it.last_price,
                top_sheet_no = it.top_sheet_no,
                bottom_sheet_no = it.bottom_sheet_no,
                last_sheet_no = it.last_sheet_no,
                num1 = 0,
                num2 = 0,
                num3 = 0,
                create_time = System.DateTime.Now
            };
            d.Insert(ctHistory, "id");
        }

        /// <summary>
        /// 供应商流水(应收)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="item"></param>
        private void SupcustReceivabelFlow(IDB d, dynamic item)
        {
            string sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no=@supcust_no and supcust_flag='S' order by flow_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_no", item.supcust_no)
            };
            var dt1 = d.ExecuteToTable(sql, pars);
            decimal old_money = 0;
            if (dt1.Rows.Count > 0)
            {
                old_money = Helper.Conv.ToDecimal(dt1.Rows[0]["new_money"]);
            }
            Model.rp_t_supcust_cash_flow it = new Model.rp_t_supcust_cash_flow();

            sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
            it.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
            it.supcust_no = item.supcust_no;
            it.supcust_flag = "S";
            it.oper_type = item.oper_type;
            it.voucher_no = item.sheet_no;
            it.sheet_no = "";
            it.oper_date = item.oper_date;
            it.old_money = old_money;
            it.oper_money = item.total_amount;
            it.free_money = item.free_money;
            it.new_money = it.old_money + (it.oper_money - it.free_money) * item.pay_type;
            it.pay_date = System.DateTime.Now;
            d.Insert(it);
        }

        /// <summary>
        /// 供应商流水(付款)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="item"></param>
        private void SupcustPayFlow(IDB d, dynamic item)
        {
            //写入供应商流水(付款)
            string sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no=@supcust_no and supcust_flag='S' order by flow_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@supcust_no", item.supcust_no)
            };
            var dt1 = d.ExecuteToTable(sql, pars);
            decimal old_money = 0;
            if (dt1.Rows.Count > 0)
            {
                old_money = Helper.Conv.ToDecimal(dt1.Rows[0]["new_money"]);
            }
            Model.rp_t_supcust_cash_flow it = new Model.rp_t_supcust_cash_flow();

            sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
            it.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
            it.supcust_no = item.supcust_no;
            it.supcust_flag = "S";
            it.oper_type = item.oper_type;
            it.voucher_no = item.settle_sheet_no;
            it.sheet_no = "";
            it.oper_date = item.oper_date;
            it.old_money = old_money;
            it.oper_money = item.pay_amount;
            it.free_money = item.ml_amount;
            it.new_money = it.old_money - it.oper_money - it.free_money;
            it.pay_date = System.DateTime.Now;
            d.Insert(it);
        }

        /// <summary>
        /// 供应商价格
        /// </summary>
        private void SupcustPrice(IDB d, dynamic item)
        {
            string sql = "select * from bi_t_sup_item where sup_id='" + item.supcust_no + "'" +
                      " and item_no='" + item.detail.item_no + "'";
            Model.bi_t_sup_item it = d.ExecuteToModel<Model.bi_t_sup_item>(sql, null);
            int isChange = 0;
            if (it == null)
            {
                sql = @"select isnull(Max(flow_id),0)+1 flow_id from bi_t_sup_item";
                DataTable dt = d.ExecuteToTable(sql, null);


                it = new Model.bi_t_sup_item();
                if (dt.Rows.Count > 0)
                {
                    it.flow_id = dt.Rows[0]["flow_id"].ToInt32();
                }
                it.item_no = item.detail.item_no;
                it.sup_id = item.supcust_no;
                it.price = item.detail.valid_price;
                it.top_price = item.detail.valid_price;
                it.bottom_price = item.detail.valid_price;
                it.last_price = item.detail.valid_price;
                it.top_sheet_no = item.sheet_no;
                it.bottom_sheet_no = item.sheet_no;
                it.last_sheet_no = item.sheet_no;
                it.other1 = "";
                it.other2 = "";
                it.other3 = "";
                it.num1 = 0;
                it.num2 = 0;
                it.num3 = 0;
                it.spec_from = System.DateTime.MinValue;
                it.spec_to = System.DateTime.MinValue;
                it.spec_price = 0;
                it.Item_Status = "";
                it.Ct_Ly_rate = 0;
                it.Ct_Ly_Spec_rate = 0;
                it.Spec_sheet_no = "";
                it.Ct_no = "";
                it.update_time = System.DateTime.Now;
                d.Insert(it);
                isChange = 1;
            }
            else
            {
                if (it.price != item.detail.valid_price)
                    isChange = 1;
                if (it.top_price < item.detail.valid_price)
                {
                    it.price = item.detail.valid_price;
                    it.top_price = item.detail.valid_price;
                    it.bottom_price = it.bottom_price;
                    it.last_price = item.detail.valid_price;
                    it.top_sheet_no = item.sheet_no;
                    it.bottom_sheet_no = it.bottom_sheet_no;
                    it.last_sheet_no = item.sheet_no;
                    it.update_time = System.DateTime.Now;
                    d.Update(it, "item_no,sup_id", "price,top_price,bottom_price,last_price," +
                        "top_sheet_no,bottom_sheet_no,last_sheet_no,oper_id,update_time");
                }
                else if (it.bottom_price > item.detail.valid_price)
                {
                    it.price = item.detail.valid_price;
                    it.top_price = it.top_price;
                    it.bottom_price = item.detail.valid_price;
                    it.last_price = item.detail.valid_price;
                    it.top_sheet_no = it.top_sheet_no;
                    it.bottom_sheet_no = item.sheet_no;
                    it.last_sheet_no = item.sheet_no;
                    it.update_time = System.DateTime.Now;
                    d.Update(it, "item_no,sup_id", "price,top_price,bottom_price,last_price," +
                        "top_sheet_no,bottom_sheet_no,last_sheet_no,oper_id,update_time");
                }
                else
                {
                    it.price = item.detail.valid_price;
                    it.top_price = it.top_price;
                    it.bottom_price = it.bottom_price;
                    it.last_price = item.detail.valid_price;
                    it.top_sheet_no = it.top_sheet_no;
                    it.bottom_sheet_no = it.bottom_sheet_no;
                    it.last_sheet_no = item.sheet_no;
                    it.update_time = System.DateTime.Now;
                    d.Update(it, "item_no,sup_id", "price,top_price,bottom_price,last_price," +
                        "top_sheet_no,bottom_sheet_no,last_sheet_no,oper_id,update_time");
                }
            }

            body.bi_t_sup_item_history itHistory = new body.bi_t_sup_item_history
            {
                item_no = it.item_no,
                sup_id = it.sup_id,
                price = it.price,
                top_price = it.top_price,
                bottom_price = it.bottom_price,
                last_price = it.last_price,
                top_sheet_no = it.top_sheet_no,
                bottom_sheet_no = it.bottom_sheet_no,
                last_sheet_no = it.last_sheet_no,
                other1 = "",
                other2 = "",
                other3 = "",
                num1 = 0,
                num2 = 0,
                num3 = 0,
                spec_from = DateTime.MinValue,
                spec_to = DateTime.MinValue,
                spec_price = 0,
                Item_Status = "",
                Ct_Ly_rate = 0,
                Ct_Ly_Spec_rate = 0,
                Spec_sheet_no = "",
                Ct_no = "",
                create_time = DateTime.Now,
                is_change = isChange
            };
            d.Insert(itHistory, "id");
        }

        /// <summary>
        /// 结算单
        /// </summary>
        /// <param name="d"></param>
        /// <param name="item"></param>
        private void SettlementSheet(IDB d, dynamic item)
        {
            //提取付款方式
            string sql = "select * from bi_t_payment_info";
            var tb = d.ExecuteToTable(sql, null);
            Dictionary<string, Model.bi_t_payment_info> dic_payment = new Dictionary<string, Model.bi_t_payment_info>();
            foreach (System.Data.DataRow row in tb.Rows)
            {
                string pay_way = row["pay_way"].ToString().Trim();
                dic_payment.Add(pay_way, DB.ReflectionHelper.DataRowToModel<Model.bi_t_payment_info>(row));
            }

            if (item.pay_way != "")
            {
                Model.rp_t_recpay_record_info it = new Model.rp_t_recpay_record_info();
                it.sheet_no = GetSheetCode(d, "CP");
                item.settle_sheet_no = it.sheet_no;
                it.supcust_no = item.cust_no;
                it.supcust_flag = item.supcust_flag;
                it.flag_post = "";
                it.total_amount = item.pay_amount;
                if (item.ml_pay_way != "")
                {
                    it.free_money = item.ml_amount;
                }
                it.coin_no = "RMB";
                it.coin_rate = 1;
                it.pay_way = item.pay_way;
                it.approve_flag = "0";
                it.oper_id = "1001";
                it.oper_date = System.DateTime.Now;
                it.deal_man = "";
                it.approve_man = "";
                it.approve_date = System.DateTime.MinValue;
                it.other1 = "";
                it.other2 = "";
                it.other3 = "";
                Model.bi_t_payment_info payment;
                if (dic_payment.TryGetValue(item.pay_way, out payment) == true)
                {
                    if (payment.visa_id == null || payment.visa_id == "")
                    {
                        throw new Exception("付款方式" + item.pay_way + "找不到默认现金银行帐户!");
                    }
                    else
                    {
                        it.visa_id = payment.visa_id;
                    }
                }
                else
                {
                    throw new Exception("付款方式" + item.pay_way + "找不到默认现金银行帐户!");
                }

                item.visa_id = it.visa_id;
                it.num1 = 0;
                it.num2 = 0;
                it.num3 = 0;
                it.cm_branch = "00";
                it.branch_no = item.branch_no;
                it.from_date = System.DateTime.Now;
                it.to_date = System.DateTime.Now;
                it.rc_sheet_no = "";

                d.Insert(it);
                //
                Model.rp_t_recpay_record_detail line = new Model.rp_t_recpay_record_detail();
                sql = "select isnull(max(flow_no),0)+1 from rp_t_recpay_record_detail";
                line.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                line.sheet_no = it.sheet_no;
                line.voucher_no = item.sheet_no;
                line.sheet_amount = item.total_amount;
                line.paid_amount = 0;
                line.paid_free = 0;
                line.pay_amount = item.pay_amount;
                line.pay_free = item.ml_amount;
                line.memo = "";
                line.other1 = "";
                line.other2 = "";
                line.other3 = "";
                line.num1 = 0;
                line.num2 = 0;
                line.num3 = 0;
                line.pay_date = System.DateTime.Now;
                line.item_no = "";
                line.path = "+";
                line.select_flag = "1";
                line.voucher_type = "";
                line.oper_date = System.DateTime.Now;
                line.voucher_other1 = "";
                line.voucher_other2 = "";
                line.order_no = "";
                d.Insert(line);
                //
                it.approve_date = System.DateTime.Now;
                it.approve_flag = "1";
                it.approve_man = "1001";
                d.Update(it, "sheet_no", "approve_date,approve_flag,approve_man");
            }
        }

        /// <summary>
        /// 回填应结明细
        /// </summary>
        /// <param name="d"></param>
        /// <param name="item"></param>
        private void BackFilPayDetail(IDB d, dynamic item)
        {
            string sql = "select * from rp_t_accout_payrec_flow where supcust_flag='" + item.supcust_flag + "' and voucher_no='" + item.sheet_no + "'";
            var it = d.ExecuteToModel<Model.rp_t_accout_payrec_flow>(sql, null);
            if (it != null)
            {
                it.paid_amount = it.paid_amount + item.pay_amount;
                it.free_money = it.free_money + item.ml_amount;
                it.pay_way = item.pay_way;
                d.Update(it, "trans_no,voucher_no", "paid_amount,free_money,pay_way");
            }

        }

        /// <summary>
        /// 现金收支
        /// </summary>
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
            it.bill_flag = "K";
            it.cm_branch = "00";
            it.approve_flag = "1";
            it.approve_man = item.approve_man;
            it.approve_date = System.DateTime.Now;
            it.other1 = "";
            it.other2 = "";
            it.other3 = "";
            it.num1 = 0;
            it.num2 = 0;
            it.num3 = 0;
            d.Insert(it);
            Model.bank_t_cash_detail line = new Model.bank_t_cash_detail();
            string sql = "select isnull(max(flow_id),0)+1 from bank_t_cash_detail ";
            line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
            line.sheet_no = it.sheet_no;
            line.type_no = item.type_no;
            line.bill_cash = item.pay_amount;
            line.memo = "";
            d.Insert(line);
            //
            //it.approve_date = System.DateTime.Now;
            //it.approve_flag = "1";
            // it.approve_man = item.approve_man;
            // d.Update(it, "sheet_no", "approve_date,approve_flag,approve_man");

        }

        /// <summary>
        /// 现金结存
        /// </summary>
        /// <param name="d"></param>
        /// <param name="item"></param>
        private void CashBalance(IDB d, dynamic item)
        {

            string sql = "select * from bank_t_visa_money where visa_id='" + item.visa_id + "'";
            var dt1 = d.ExecuteToTable(sql, null);
            if (dt1.Rows.Count == 0)
            {
                Model.bank_t_visa_money it = new Model.bank_t_visa_money();
                it.visa_id = item.visa_id;
                it.coin_no = "RMB";
                it.bank_cash = 0;
                it.memo = item.memo;
                d.Insert(it);
            }
            else
            {
                var row1 = dt1.Rows[0];
                Model.bank_t_visa_money it = new Model.bank_t_visa_money();
                it.visa_id = item.visa_id;
                it.coin_no = "RMB";
                it.bank_cash = Helper.Conv.ToDecimal(row1["bank_cash"].ToString()) + item.pay_amount;
                it.memo = item.memo;
                d.Update(it, "visa_id", "bank_cash");
            }

        }

        /// <summary>
        /// 库存流水、结存
        /// </summary>
        /// <param name="d"></param>
        /// <param name="item"></param>
        private void StockFlowBalance(IDB d, dynamic item)
        {
            ComputeCost com = new ComputeCost();
            decimal price;
            decimal end_price;
            decimal adjust_amt;
            com.Compute(d, item.sheet_property, item.db_type, item.branch_no, item.detail.item_no, item.detail.in_qty, item.detail.valid_price / item.detail.unit_factor, out price, out end_price, out adjust_amt);
            //
            string sql = "select * from ic_t_branch_stock where branch_no='" + item.branch_no + "'" +
              " and item_no='" + item.detail.item_no + "'";
            global::Model.ic_t_branch_stock stock = d.ExecuteToModel<global::Model.ic_t_branch_stock>(sql, null);
            //
            sql = "select * from bi_t_item_info where item_no='" + item.detail.item_no + "'";
            bi_t_item_info it = d.ExecuteToModel<bi_t_item_info>(sql, null);
            if (it == null)
            {
                throw new Exception("不存在商品内码" + item.detail.item_no);
            }
            //
            sql = "select isnull(max(flow_id),0)+1 from ic_t_flow_dt";
            global::Model.ic_t_flow_dt flow = new global::Model.ic_t_flow_dt();
            flow.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
            flow.branch_no = item.branch_no;
            flow.item_no = item.detail.item_no;
            flow.oper_date = DateTime.Now;
            flow.init_qty = stock.stock_qty;
            flow.init_amt = stock.stock_qty * stock.cost_price;
            flow.new_qty = item.detail.in_qty;
            flow.new_amt = item.detail.in_qty * price;
            if ("+".Equals(item.db_type))
            {
                flow.settle_qty = flow.init_qty + flow.new_qty;
                flow.settle_amt = flow.init_amt + adjust_amt + flow.new_amt;
            }
            else if ("-".Equals(item.db_type))
            {
                flow.settle_qty = flow.init_qty - flow.new_qty;
                flow.settle_amt = flow.init_amt + adjust_amt - flow.new_amt;
            }
            else if ("09".Equals(item.db_type))
            {
                if (item.detail.in_qty > flow.init_qty)
                {
                    item.sheet_property = "1";
                    item.db_type = "+";
                }
                else
                {
                    item.sheet_property = "0";
                    item.db_type = "-";
                }
                flow.new_qty = Math.Abs(item.detail.in_qty - flow.init_qty);
                flow.new_amt = Math.Abs(item.detail.in_qty * price - flow.init_amt);
                flow.settle_qty = item.detail.in_qty;
                flow.settle_amt = item.detail.in_qty * price;
            }
            flow.cost_price = end_price;
            flow.db_type = item.db_type;
            flow.sheet_no = item.sheet_no;
            flow.voucher_no = item.sheet_no;
            flow.supcust_no = item.supcust_no;
            flow.supcust_flag = item.supcust_flag;
            flow.oper_day = System.DateTime.Now.ToString("yyyy/MM/dd");
            flow.adjust_amt = adjust_amt;
            flow.cost_type = it.cost_type;
            flow.sale_price = item.detail.sale_price / item.detail.unit_factor;
            flow.sheet_type = item.sheet_type;

            d.Insert(flow);
            //
            if (adjust_amt != 0)
            {
                global::Model.ic_t_cost_adjust ad = new global::Model.ic_t_cost_adjust();
                sql = "select isnull(max(flow_id),0)+1 from ic_t_cost_adjust";
                ad.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                ad.branch_no = item.branch_no;
                ad.item_no = item.detail.item_no;
                ad.oper_date = System.DateTime.Now;
                ad.old_price = stock.cost_price;
                ad.new_price = flow.cost_price;
                ad.in_qty = flow.new_qty;
                ad.sheet_no = item.sheet_no;
                ad.memo = item.detail.memo;
                ad.type_no = "1";
                ad.adjust_amt = adjust_amt;
                ad.sup_no = item.supcust_no;
                ad.max_flow_id = flow.flow_id;
                ad.cost_type = it.cost_type;
                ad.old_qty = stock.stock_qty;
                d.Insert(ad);
            }
            //
            stock.stock_qty = flow.settle_qty;
            stock.cost_price = flow.cost_price;
            stock.last_price = price;
            d.Update(stock, "branch_no,item_no", "stock_qty,cost_price,last_price");
        }

        /// <summary>
        /// 销售订货单
        /// </summary>
        public void CheckSSSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + sheet_no + "'";
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
                sql = "update co_t_order_main set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckSSSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 批发销售单
        /// </summary>
        public void CheckSOSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select approve_flag,update_time from sm_t_salesheet where sheet_no='" + sheet_no + "'";
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

                BatchProcessing.CheckSaleSheetBatch(d, sheet_no);

                sql = @"select sale_qnty from [dbo].[sm_t_salesheet_detail] where sheet_no='" + sheet_no + "'";
                DataTable d1 = d.ExecuteToTable(sql, null);
                foreach (DataRow row in d1.Rows)
                {
                    if (row["sale_qnty"].ToDecimal() < 0)
                    {
                        throw new Exception("" + sheet_no + "的商品数量异常");
                    }
                }
                sql = "update sm_t_salesheet set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sm_t_salesheet item = d.ExecuteToModel<sm_t_salesheet>("select * from sm_t_salesheet where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from sm_t_salesheet_detail where sheet_no='" + sheet_no + "'", null);
                sql = "select * from bi_t_supcust_info where supcust_no='" + item.cust_no + "' and supcust_flag='C'";
                var tb1 = d.ExecuteToTable(sql, null);
                if (Conv.ToString(tb1.Rows[0]["is_cust_ar"]) == "" || Conv.ToString(tb1.Rows[0]["acc_no"]) == "")
                {
                    throw new Exception("客户" + item.cust_no + "的结算账号为空，请重新维护下该客户档案！");
                }
                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.oper_date = item.oper_date;
                dyn.branch_no = item.branch_no;
                dyn.db_type = "-";
                dyn.branch_no = item.branch_no;
                if (Conv.ToString(tb1.Rows[0]["is_cust_ar"]) == "1")
                    dyn.supcust_no = item.cust_no;
                else
                    dyn.supcust_no = Conv.ToString(tb1.Rows[0]["acc_no"]);
                dyn.oper_id = item.oper_id;
                dyn.supcust_flag = "C";
                dyn.trans_no = "I";
                dyn.oper_type = "I";
                dyn.sheet_property = "0";
                dyn.sheet_type = "SO";
                dyn.pay_type = 1;
                dyn.free_money = 0;
                dyn.memo = "";
                dyn.sale_no = "A";
                dyn.cust_no = item.cust_no;

                ShouldKnotDetail(d, dyn);
                CustReceivableFlow(d, dyn);
                dyn.supcust_no = item.cust_no;
                foreach (DataRow dr in tbdetail.Rows)
                {
                    var detail = DB.ReflectionHelper.DataRowToModel<sm_t_salesheet_detail>(dr);

                    dyn.detail = new ExpandoObject();
                    dyn.detail.in_qty = detail.sale_qnty;
                    dyn.detail.item_no = detail.item_no;
                    dyn.detail.valid_price = detail.sale_price;
                    dyn.detail.unit_factor = detail.unit_factor;
                    dyn.detail.sale_price = detail.sale_price;
                    dyn.detail.memo = "";

                    StockFlowBalance(d, dyn);
                    CustPrice(d, dyn);//列名无效
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckSOSheet()", ex.ToString(), sheet_no);
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 客户退货单
        /// </summary>
        public void CheckRISheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select trans_no,approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
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

                BatchProcessing.CheckReturnInSheetBatch(d, sheet_no);

                sql = "update ic_t_inout_store_master set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ic_t_inout_store_master item = d.ExecuteToModel<ic_t_inout_store_master>("select * from ic_t_inout_store_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.oper_date = item.oper_date;
                dyn.branch_no = item.branch_no;
                dyn.db_type = "+";
                dyn.branch_no = item.branch_no;
                dyn.supcust_no = item.supcust_no;
                dyn.oper_id = item.oper_id;
                dyn.supcust_flag = "C";
                dyn.oper_type = "D";
                dyn.sheet_property = "0";
                dyn.trans_no = "D";
                dyn.sheet_type = "RI";
                dyn.pay_type = -1;
                dyn.free_money = 0;
                dyn.memo = "";
                dyn.sale_no = "A";

                ShouldKnotDetail(d, dyn);
                CustReceivableFlow(d, dyn);

                foreach (DataRow dr in tbdetail.Rows)
                {
                    var detail = DB.ReflectionHelper.DataRowToModel<ic_t_inout_store_detail>(dr);

                    dyn.detail = new ExpandoObject();
                    dyn.detail.in_qty = detail.in_qty;
                    dyn.detail.item_no = detail.item_no;
                    dyn.detail.valid_price = detail.valid_price;
                    dyn.detail.unit_factor = detail.unit_factor;
                    dyn.detail.sale_price = detail.valid_price;
                    dyn.detail.memo = "";

                    StockFlowBalance(d, dyn);
                }


                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckRISheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 销售订货单
        /// </summary>
        public void CheckCGOrder(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + sheet_no + "'";
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
                sql = "update co_t_order_main set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckCGOrder()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 采购入库
        /// </summary>
        public void CheckPISheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //

                string sql = "select trans_no,approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
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
                BatchProcessing.CheckInSheetBatch(d, sheet_no);
                sql = @"select in_qty from [dbo].[ic_t_inout_store_detail] where sheet_no='" + sheet_no + "'";
                DataTable d1 = d.ExecuteToTable(sql, null);
                foreach (DataRow row in d1.Rows)
                {
                    if (row["in_qty"].ToDecimal() < 0)
                    {
                        throw new Exception("" + sheet_no + "的商品数量异常");
                    }
                }
                sql = "update ic_t_inout_store_master set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = @"select batch_num from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'";
                DataTable t1 = d.ExecuteToTable(sql, null);
                string batch_nums = "";
                foreach (DataRow dataRow in t1.Rows)
                {
                    batch_nums += "'" + dataRow["batch_num"].ToString() + "',";

                }

                sql = @"select * from   [dbo].[sm_t_salesheet_recpay_detail] where batch_num in(" +
                      batch_nums.Substring(0, batch_nums.Length - 1) + ") and " +
                      "batch_num !='' and batch_num is not null";
                t1 = d.ExecuteToTable(sql, null);
                if (t1.Rows.Count > 0)
                {
                    sql = "select * from ic_t_inout_store_master where sheet_no = '" + sheet_no + "'";
                    DataTable t_main = d.ExecuteToTable(sql, null);
                    IvyTran.body.ic_t_inoutstore_recpay_main main = new IvyTran.body.ic_t_inoutstore_recpay_main();
                    main.sheet_no = sheet_no;
                    main.approve_flag = "1";
                    main.approve_date = DateTime.Now;
                    main.update_time = DateTime.Now;
                    main.approve_man = approve_man;
                    main.create_time = DateTime.Now;
                    main.oper_id = approve_man;
                    main.oper_date = DateTime.Now;
                    main.branch_no = t_main.Rows[0]["branch_no"].ToString();
                    main.cust_no = t_main.Rows[0]["supcust_no"].ToString();
                    main.voucher_no = t_main.Rows[0]["voucher_no"].ToString();
                    d.Insert(main);
                    sql = "";
                    DataTable t_detail = d.ExecuteToTable("select * from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'", null);
                    foreach (DataRow dataRow in t_detail.Rows)
                    {
                        IvyTran.body.ic_t_inoutstore_recpay_detail detail = new IvyTran.body.ic_t_inoutstore_recpay_detail();
                        sql = "select  isnull(Max(flow_id),0)+1 flow_id from ic_t_inoutstore_recpay_detail";

                        detail.flow_id = long.Parse(d.ExecuteToTable(sql, null).Rows[0]["flow_id"].ToString());
                        detail.sheet_no = sheet_no;
                        detail.item_no = dataRow["item_no"].ToString();
                        detail.batch_num = dataRow["batch_num"].ToString();
                        detail.sheet_sort = dataRow["sheet_sort"].ToInt32();
                        detail.task_flow_id = long.Parse(dataRow["flow_id"].ToString());
                        detail.real_qnty = dataRow["in_qty"].ToDecimal();
                        detail.voucher_no = sheet_no;
                        sql = "select isnull(sum(real_qnty),0.00) real_qnty from [dbo].[sm_t_salesheet_recpay_detail] where batch_num='" + dataRow["batch_num"] + "'";
                        detail.num2 = d.ExecuteToTable(sql, null).Rows[0]["real_qnty"].ToDecimal();
                        if (detail.num2 == 0)
                        {
                            detail.num2 = dataRow["in_qty"].ToDecimal();
                        }
                        d.Insert(detail);
                    }


                }
                //
                ic_t_inout_store_master item = d.ExecuteToModel<ic_t_inout_store_master>("select * from ic_t_inout_store_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.oper_date = item.oper_date;
                dyn.branch_no = item.branch_no;
                dyn.supcust_no = item.supcust_no;
                dyn.oper_id = item.oper_id;
                dyn.oper_type = "A";
                dyn.sheet_property = "1";
                dyn.trans_no = "A";//trans_no
                dyn.supcust_flag = "S";
                dyn.db_type = "+";
                dyn.sheet_type = "PI";
                dyn.pay_type = 1;
                dyn.free_money = 0;
                dyn.sale_no = "A";
                dyn.memo = "";

                ShouldKnotDetail(d, dyn);
                SupcustReceivabelFlow(d, dyn);

                foreach (DataRow dr in tbdetail.Rows)
                {
                    var detail = DB.ReflectionHelper.DataRowToModel<ic_t_inout_store_detail>(dr);

                    dyn.detail = new ExpandoObject();
                    sql = "select real_qnty from ic_t_inoutstore_recpay_detail " +
                          "where batch_num='" + detail.batch_num + "'";
                    DataTable d12 = d.ExecuteToTable(sql, null);

                    if (d12.Rows.Count > 0)
                    {
                        dyn.detail.in_qty = d12.Rows[0]["real_qnty"].ToDecimal();
                    }
                    else
                    {
                        dyn.detail.in_qty = detail.in_qty;
                    }
                    dyn.detail.item_no = detail.item_no;
                    dyn.detail.valid_price = detail.valid_price;
                    dyn.detail.unit_factor = detail.unit_factor;
                    dyn.detail.sale_price = detail.valid_price;
                    dyn.detail.memo = "";

                    StockFlowBalance(d, dyn);
                    SupcustPrice(d, dyn);
                }

                //sql = "select * from  where ";
                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckPISheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 采购退货
        /// </summary>
        public void CheckROSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select trans_no,approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
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

                BatchProcessing.CheckReturnOutSheetBatch(d, sheet_no);

                sql = "update ic_t_inout_store_master set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ic_t_inout_store_master item = d.ExecuteToModel<ic_t_inout_store_master>("select * from ic_t_inout_store_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.total_amount = item.total_amount;
                dyn.oper_date = item.oper_date;
                dyn.branch_no = item.branch_no;
                dyn.db_type = "-";
                dyn.branch_no = item.branch_no;
                dyn.supcust_no = item.supcust_no;
                dyn.oper_id = item.oper_id;
                dyn.supcust_flag = "S";
                dyn.trans_no = "F";//trans_no
                dyn.oper_type = "F";
                dyn.sheet_type = "RO";
                dyn.pay_type = -1;
                dyn.free_money = 0;
                dyn.sale_no = "A";
                dyn.sheet_property = "1";
                dyn.memo = "";

                ShouldKnotDetail(d, dyn);
                SupcustReceivabelFlow(d, dyn);

                foreach (DataRow dr in tbdetail.Rows)
                {
                    var detail = DB.ReflectionHelper.DataRowToModel<ic_t_inout_store_detail>(dr);

                    dyn.detail = new ExpandoObject();
                    dyn.detail.in_qty = detail.in_qty;
                    dyn.detail.item_no = detail.item_no;
                    dyn.detail.valid_price = detail.valid_price;
                    dyn.detail.unit_factor = detail.unit_factor;
                    dyn.detail.sale_price = detail.valid_price;
                    dyn.detail.memo = detail.other1;

                    StockFlowBalance(d, dyn);
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckROSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 其他出入单
        /// </summary>
        public void CheckOOSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select trans_no,approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
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
                sql = "update ic_t_inout_store_master set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ic_t_inout_store_master item = d.ExecuteToModel<ic_t_inout_store_master>("select * from ic_t_inout_store_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.oper_date = item.oper_date;
                dyn.branch_no = item.branch_no;
                dyn.supcust_no = item.supcust_no;
                dyn.oper_id = item.oper_id;
                dyn.sheet_type = "OO";
                dyn.supcust_flag = "";
                dyn.memo = "";

                //("01", "其它入库")  ("02", "归还") ("03", "其它出库") ("04", "领用出库") ("05", "报损出库") ("06", "借出") ("07", "报溢入库") ("09", "库存调整")
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    {"01","+"},
                    {"02","+"},
                    {"03","-"},
                    {"04","-"},
                    {"05","-"},
                    {"06","-"},
                    {"07","+"},
                    {"09","09"},
                };
                string str = dic[item.trans_no];
                if ("+".Equals(str))
                {
                    dyn.sheet_property = "1";
                    dyn.db_type = "+";
                    BatchProcessing.CheckInSheetBatch(d, sheet_no);
                }
                else if ("-".Equals(str))
                {
                    dyn.sheet_property = "0";
                    dyn.db_type = "-";
                    BatchProcessing.CheckOutSheetBatch(d, sheet_no);
                }
                else if ("09".Equals(str))
                {
                    dyn.sheet_property = "";
                    dyn.db_type = "09";
                }



                foreach (DataRow dr in tbdetail.Rows)
                {
                    var detail = DB.ReflectionHelper.DataRowToModel<ic_t_inout_store_detail>(dr);

                    dyn.detail = new ExpandoObject();
                    dyn.detail.in_qty = detail.in_qty;
                    dyn.detail.item_no = detail.item_no;
                    dyn.detail.valid_price = detail.valid_price;
                    dyn.detail.unit_factor = detail.unit_factor;
                    dyn.detail.sale_price = detail.valid_price;
                    dyn.detail.memo = "";

                    StockFlowBalance(d, dyn);
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckOOSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        public void CheckOOSheet(IDB d, ic_t_inout_store_master master, List<body.ic_t_inout_store_detail> lines, string approve_man)
        {
            string sql = "";
            sql = "update ic_t_inout_store_master set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + DateTime.Now.Toyyyy_MM_dd_HH_mm_ss() + "'";
            sql += ",update_time='" + DateTime.Now.Toyyyy_MM_dd_HH_mm_ss() + "' where sheet_no='" + master.sheet_no + "'";
            d.ExecuteScalar(sql, null);

            dynamic dyn = new ExpandoObject();
            dyn.sheet_no = master.sheet_no;
            dyn.total_amount = master.total_amount;
            dyn.pay_way = master.pay_way;
            dyn.oper_date = master.oper_date;
            dyn.branch_no = master.branch_no;
            dyn.supcust_no = master.supcust_no;
            dyn.oper_id = master.oper_id;
            dyn.sheet_type = "OO";
            dyn.supcust_flag = "";
            dyn.memo = "";

            //("01", "其它入库")  ("02", "归还") ("03", "其它出库") ("04", "领用出库") ("05", "报损出库") ("06", "借出") ("07", "报溢入库") ("09", "库存调整")
            // ("11", "盘盈");("12", "盘亏");("13", "调拨入库");("14", "调拨出库");
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"01","+"},
                {"02","+"},
                {"03","-"},
                {"04","-"},
                {"05","-"},
                {"06","-"},
                {"07","+"},
                {"09","09"},
                {"10","-"},
                {"11","+"},
                {"12","-"},
                {"13","+"},
                {"14","-"},
            };
            string str = dic[master.trans_no];
            if ("+".Equals(str))
            {
                dyn.sheet_property = "1";
                dyn.db_type = "+";
                BatchProcessing.CheckInSheetBatch(d, master.sheet_no);
            }
            else if ("-".Equals(str))
            {
                dyn.sheet_property = "0";
                dyn.db_type = "-";
                BatchProcessing.CheckOutSheetBatch(d, master.sheet_no);
            }
            else if ("09".Equals(str))
            {
                dyn.sheet_property = "";
                dyn.db_type = "09";
            }

            foreach (var detail in lines)
            {
                dyn.detail = new ExpandoObject();
                dyn.detail.in_qty = detail.in_qty;
                dyn.detail.item_no = detail.item_no;
                dyn.detail.valid_price = detail.valid_price;
                dyn.detail.unit_factor = detail.unit_factor;
                dyn.detail.sale_price = detail.valid_price;
                dyn.detail.memo = "";

                StockFlowBalance(d, dyn);

            }

        }

        /// <summary>
        /// 调拨单
        /// </summary>
        public void CheckIOSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select trans_no,approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
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
                sql = "update ic_t_inout_store_master set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ic_t_inout_store_master item = d.ExecuteToModel<ic_t_inout_store_master>("select * from ic_t_inout_store_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.oper_date = item.oper_date;
                dyn.supcust_no = item.supcust_no;
                dyn.oper_id = item.oper_id;
                dyn.sheet_type = "IO";
                dyn.trans_no = "G";
                dyn.supcust_flag = "";
                dyn.memo = "";

                foreach (DataRow dr in tbdetail.Rows)
                {
                    var detail = DB.ReflectionHelper.DataRowToModel<ic_t_inout_store_detail>(dr);

                    dyn.detail = new ExpandoObject();
                    dyn.detail.in_qty = detail.in_qty;
                    dyn.detail.item_no = detail.item_no;
                    dyn.detail.valid_price = detail.valid_price;
                    dyn.detail.unit_factor = detail.unit_factor;
                    dyn.detail.sale_price = detail.valid_price;
                    dyn.sheet_property = "0";
                    dyn.db_type = "-";
                    dyn.detail.memo = "";
                    dyn.branch_no = item.branch_no;

                    StockFlowBalance(d, dyn);

                    dyn.branch_no = item.d_branch_no;
                    dyn.sheet_property = "1";
                    dyn.db_type = "+";
                    dyn.detail.memo = "";
                    StockFlowBalance(d, dyn);
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckIOSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 加工生产
        /// </summary>
        public void CheckProcessSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select trans_no,approve_flag,update_time from ic_t_inout_store_master where sheet_no='" + sheet_no + "'";
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

                BatchProcessing.CheckInOutSheet(d, sheet_no);

                sql = "update ic_t_inout_store_master set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ic_t_inout_store_master item = d.ExecuteToModel<ic_t_inout_store_master>("select * from ic_t_inout_store_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from ic_t_inout_store_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.oper_date = item.oper_date;
                dyn.supcust_no = item.supcust_no;
                dyn.oper_id = item.oper_id;
                dyn.sheet_type = item.trans_no;
                dyn.trans_no = item.trans_no;
                dyn.supcust_flag = "";
                dyn.memo = "";
                dyn.sheet_property = "+".Equals(item.db_no) ? "1" : "0";
                dyn.db_type = item.db_no;
                dyn.branch_no = item.branch_no;

                foreach (DataRow dr in tbdetail.Rows)
                {
                    var detail = DB.ReflectionHelper.DataRowToModel<ic_t_inout_store_detail>(dr);

                    dyn.detail = new ExpandoObject();
                    dyn.detail.in_qty = detail.in_qty;
                    dyn.detail.item_no = detail.item_no;
                    dyn.detail.valid_price = detail.valid_price;
                    dyn.detail.unit_factor = detail.unit_factor;
                    dyn.detail.sale_price = detail.valid_price;
                    dyn.detail.memo = "";

                    StockFlowBalance(d, dyn);
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckIOSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 客户费用单
        /// </summary>
        public void CheckCustFYSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select * from rp_t_supcust_fy_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                else if (tb.Rows[0]["approve_flag"].ToString() == "1")
                {
                    throw new Exception("单据已审核" + sheet_no);
                }
                sql = "update rp_t_supcust_fy_master set approve_flag='1',approve_man='" + approve_man +
                    "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                rp_t_supcust_fy_master item = d.ExecuteToModel<rp_t_supcust_fy_master>("select * from rp_t_supcust_fy_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from rp_t_supcust_fy_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.oper_date = item.oper_date;
                dyn.branch_no = item.branch_no;
                dyn.supcust_no = item.supcust_no;
                dyn.oper_id = item.oper_id;
                dyn.oper_type = "CM";
                dyn.trans_no = "CM";//trans_no
                dyn.supcust_flag = "C";
                if (item.pay_type == "0" || item.pay_type == "2")
                {
                    dyn.pay_type = 1;
                }
                else
                {
                    dyn.pay_type = -1;
                }
                dyn.free_money = 0;
                dyn.memo = item.other1;
                dyn.sale_no = "A";
                //sql = "update rp_t_accout_payrec_flow set approve_flag='1',approve_man='" + approve_man +
                //    "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where voucher_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                ShouldKnotDetail(d, dyn);
                CustReceivableFlow(d, dyn);


                db.CommitTran();
                LogHelper.writeSheetLog("CheckSheet ->CheckCustFYSheet()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CusFY ->Delete()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CheckSheet ->CheckCustFYSheet()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 供应商费用单
        /// </summary>
        public void CheckSupFYSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select * from rp_t_supcust_fy_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                else if (tb.Rows[0]["approve_flag"].ToString() == "1")
                {
                    throw new Exception("单据已审核" + sheet_no);
                }
                sql = "update rp_t_supcust_fy_master set approve_flag='1',approve_man='" + approve_man +
                    "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                rp_t_supcust_fy_master item = d.ExecuteToModel<rp_t_supcust_fy_master>("select * from rp_t_supcust_fy_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbdetail = d.ExecuteToTable("select * from rp_t_supcust_fy_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.sheet_no = item.sheet_no;
                dyn.total_amount = item.total_amount;
                dyn.pay_way = item.pay_way;
                dyn.oper_date = item.oper_date;
                dyn.branch_no = item.branch_no;
                dyn.supcust_no = item.supcust_no;
                dyn.oper_id = item.oper_id;
                dyn.oper_type = "GM";
                dyn.trans_no = "GM";//trans_no
                dyn.supcust_flag = "S";
                if (item.pay_type == "0" || item.pay_type == "2")
                {
                    dyn.pay_type = 1;
                }
                else
                {
                    dyn.pay_type = -1;
                }

                dyn.free_money = 0;
                dyn.memo = "";
                dyn.sale_no = "A";
                //sql = "update rp_t_accout_payrec_flow set approve_flag='1',approve_man='" + approve_man +
                //   "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where voucher_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                ShouldKnotDetail(d, dyn);
                SupcustReceivabelFlow(d, dyn);

                db.CommitTran();
                LogHelper.writeSheetLog("CheckSheet ->CheckSupFYSheet()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CheckSheet ->CheckSupFYSheet()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CheckSheet ->CheckSupFYSheet()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 现金银行转账单
        /// </summary>
        public void CheckBankSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select * from bank_t_cash_master where sheet_no='" + sheet_no + "'";
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
                bank_t_cash_master master = d.ExecuteToModel<bank_t_cash_master>("select * from bank_t_cash_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbDetail = d.ExecuteToTable("select * from bank_t_cash_detail where sheet_no='" + sheet_no + "' ", null);

                dynamic dyn = new ExpandoObject();

                dyn.visa_id = master.visa_id;
                dyn.pay_amount = master.bill_total;
                dyn.memo = master.other1;

                //转出
                CashBalance(d, dyn);

                //转入
                dyn.visa_id = master.visa_in;
                dyn.pay_amount = -master.bill_total;
                CashBalance(d, dyn);


                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckBankSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 管理费用单
        /// </summary>
        public void CheckGlSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select * from bank_t_cash_master where sheet_no='" + sheet_no + "'";
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
                bank_t_cash_master master = d.ExecuteToModel<bank_t_cash_master>("select * from bank_t_cash_master where sheet_no='" + sheet_no + "'", null);
                DataTable tbDetail = d.ExecuteToTable("select * from bank_t_cash_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();

                dyn.visa_id = master.visa_id;
                dyn.pay_amount = -master.bill_total;
                dyn.memo = master.other1;
                CashBalance(d, dyn);

                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckGlSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 供应商结算单
        /// </summary>
        public void CheckSupJSSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select * from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                else if (tb.Rows[0]["approve_flag"].ToString() == "1")
                {
                    throw new Exception("单据已审核" + sheet_no);
                }
                decimal num = 0;
                sql = "select * from rp_t_recpay_record_detail where sheet_no='" + sheet_no + "'";
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
                    num += Conv.ToDecimal(dr["pay_amount"]);
                    //sql = "update rp_t_recpay_record_detail set paid_amount='" + Conv.ToDecimal(tb2.Rows[0]["已核销金额"]) + "',sheet_amount='" + Conv.ToDecimal(tb2.Rows[0]["sheet_amount"]) + "'  where sheet_no='" + sheet_no + "' and voucher_no='" + Conv.ToString(dr["voucher_no"]) + "'";
                    //d.ExecuteScalar(sql, null);

                }
                //sql = "select * from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'";
                //var tb3 = d.ExecuteToTable(sql, null);
                //decimal num1 = 0;
                //if (num < Conv.ToDecimal(tb.Rows[0]["num1"]))
                //    num1 = Conv.ToDecimal(tb.Rows[0]["num1"]) - num;
                //else
                //    num1 = 0;
                sql = "update rp_t_recpay_record_info set approve_flag='1',approve_man='" + approve_man +
                   "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "update rp_t_accout_payrec_flow set approve_flag='1',approve_man='" + approve_man +
                //  "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',sheet_amount=" + Conv.ToDecimal(tb.Rows[0]["total_amount"]) + ",paid_amount=" + (0 - (num - Conv.ToDecimal(tb.Rows[0]["free_money"]))) + " where voucher_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                if (Conv.ToDecimal(tb.Rows[0]["free_money"]) > 0)
                {
                    Model.rp_t_supcust_fy_master ord = new Model.rp_t_supcust_fy_master();
                    ord.sheet_no = MaxCode();
                    ord.supcust_no = tb.Rows[0]["supcust_no"].ToString();
                    ord.supcust_flag = "S";

                    ord.pay_date = System.DateTime.Now;
                    ord.old_no = tb.Rows[0]["sheet_no"].ToString();
                    ord.oper_id = approve_man;
                    ord.oper_date = DateTime.Now;
                    ord.approve_flag = "0";
                    ord.approve_man = "";
                    ord.approve_date = System.DateTime.MinValue;
                    ord.is_payed = "0";
                    ord.sale_man = approve_man;
                    ord.branch_no = "";
                    ord.cm_branch = "";
                    ord.other1 = "供应商收款单免收金额";
                    ord.other2 = "";
                    ord.other3 = "";
                    ord.num1 = 0;
                    ord.num2 = 0;
                    ord.num3 = 0;
                    ord.visa_id = "";
                    ord.is_over = "0";
                    ord.total_amount = 0;
                    ord.paid_amount = 0;
                    ord.pay_way = "";
                    ord.pay_name = "";

                    Model.rp_t_supcust_fy_detail line = new Model.rp_t_supcust_fy_detail();
                    line.sheet_no = ord.sheet_no;
                    line.kk_no = "";
                    line.kk_cash = Conv.ToDecimal(tb.Rows[0]["free_money"]);
                    line.other1 = "供应商收款单免收金额";
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.other2 = "RP";
                    line.other3 = "";
                    line.num1 = 0;
                    line.num2 = 0;
                    line.num3 = 0;
                    ord.total_amount = line.kk_cash;
                    ord.pay_type = "0";
                    d.Insert(ord);
                    d.Insert(line);

                    //生成流水
                    sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no='" + ord.supcust_no + "' and supcust_flag='S' order by flow_no ";
                    var dt2 = d.ExecuteToTable(sql, null);
                    decimal old_money1 = 0;
                    if (dt2.Rows.Count > 0)
                    {
                        old_money1 = Conv.ToDecimal(dt2.Rows[0]["new_money"]);
                    }

                    Model.rp_t_supcust_cash_flow it1 = new Model.rp_t_supcust_cash_flow();

                    sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
                    it1.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
                    it1.supcust_no = ord.supcust_no;
                    it1.supcust_flag = "S";
                    decimal pay_type1;
                    if (ord.pay_type == "0" || ord.pay_type == "2")
                    {
                        pay_type1 = 1;
                    }
                    else
                    {
                        pay_type1 = -1;
                    }
                    it1.oper_type = "GM";
                    it1.voucher_no = ord.sheet_no;
                    it1.sheet_no = "";
                    it1.oper_date = ord.oper_date;
                    it1.old_money = old_money1;
                    it1.oper_money = ord.total_amount;
                    it1.free_money = 0;
                    it1.new_money = it1.old_money + (it1.oper_money - it1.free_money) * pay_type1;
                    it1.pay_date = System.DateTime.Now;
                    d.Insert(it1);
                    //为客户生成应结明细
                    rp_t_accout_payrec_flow ord1 = new rp_t_accout_payrec_flow();
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_accout_payrec_flow";
                    ord1.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    ord1.pay_type = pay_type1;
                    ord1.voucher_no = ord.sheet_no;
                    ord1.trans_no = "GM";
                    ord1.sheet_amount = ord.total_amount;
                    ord1.discount = 1;
                    ord1.pay_way = "";
                    ord1.paid_amount = 0;
                    ord1.tax_amount = 0;
                    ord1.pay_date = DateTime.Now;
                    ord1.supcust_no = ord.supcust_flag;
                    ord1.supcust_flag = "S";
                    ord1.free_money = 0;
                    ord1.oper_date = DateTime.Now;
                    //ord1.approve_flag = "0";
                    d.Insert(ord1);


                }
                //生成收款单的应结明细
                Model.rp_t_accout_payrec_flow it = new Model.rp_t_accout_payrec_flow();
                sql = "select isnull(max(flow_no),0)+1 from rp_t_accout_payrec_flow";
                it.flow_no = IvyTran.Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                it.pay_type = -1;
                it.voucher_no = sheet_no;
                //if ((tb.Rows[0]["supcust_flag"].ToString() == "C"))
                //    it.trans_no = "CP";
                //else
                it.trans_no = "RP";
                it.sheet_amount = Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                it.discount = 1;
                it.paid_amount = 0 - (num - Conv.ToDecimal(tb.Rows[0]["free_money"]));
                it.tax_amount = 0;
                it.pay_way = "";
                it.pay_date = System.DateTime.Now;
                it.supcust_no = Conv.ToString(tb.Rows[0]["supcust_no"]);
                it.supcust_flag = Conv.ToString(tb.Rows[0]["supcust_flag"]);
                it.free_money = 0;
                it.memo = "";
                it.other1 = "";
                it.other2 = "";
                it.other3 = "";
                it.num1 = 0;
                it.num2 = 0;
                it.num3 = 0;
                it.branch_no = "";
                it.sale_no = "A";
                it.oper_date = DateTime.Now;
                d.Insert(it);
                rp_t_recpay_record_info recpay = d.ExecuteToModel<rp_t_recpay_record_info>("select * from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'", null);
                DataTable detail = d.ExecuteToTable("select * from rp_t_recpay_record_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                dyn.pay_way = recpay.pay_way;
                dyn.branch_no = recpay.branch_no;
                dyn.sheet_no = recpay.sheet_no;
                dyn.settle_sheet_no = recpay.sheet_no;
                dyn.visa_id = recpay.visa_id;
                dyn.pay_amount = recpay.total_amount;
                dyn.ml_amount = recpay.free_money;
                dyn.type_no = "102";
                dyn.oper_id = recpay.oper_id;
                dyn.approve_man = approve_man;
                dyn.supcust_no = recpay.supcust_no;
                dyn.memo = recpay.other1;
                dyn.supcust_flag = "S";

                //CashMaster(d, dyn);

                dyn.pay_amount = -recpay.total_amount;

                //CashBalance(d, dyn);


                dyn.oper_type = "P";
                dyn.total_amount = recpay.total_amount;
                dyn.oper_date = recpay.oper_date;

                //SupcustPayFlow(d, dyn);

                foreach (DataRow dr in detail.Rows)
                {
                    dyn.pay_amount = Conv.ToDecimal(dr["pay_amount"]);
                    dyn.ml_amount = Conv.ToDecimal(dr["pay_free"]);
                    dyn.sheet_no = dr["voucher_no"];
                    BackFilPayDetail(d, dyn);
                }

                db.CommitTran();
                LogHelper.writeSheetLog("CheckSheet ->CheckCustJSSheet()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CusSettle ->Delete()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CheckSheet ->CheckSupJSSheet()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
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
        /// <summary>
        /// 客户结算单
        /// </summary>
        public void CheckCustJSSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select * from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);

                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                else if (tb.Rows[0]["approve_flag"].ToString() == "1")
                {
                    throw new Exception("单据已审核" + sheet_no);
                }
                decimal num = 0;
                sql = "select * from rp_t_recpay_record_detail where sheet_no='" + sheet_no + "'";
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
                    //decimal sheet_amount = 0;
                    //if (Conv.ToDecimal(tb2.Rows[0]["pay_type"]) == 1)
                    //{
                    //    sheet_amount = Conv.ToDecimal(tb2.Rows[0]["sheet_amount"]);
                    //}
                    //else
                    //{
                    //    sheet_amount =0- Conv.ToDecimal(tb2.Rows[0]["sheet_amount"]);
                    //}
                    if (Conv.ToDecimal(dr["sheet_amount"]) != Conv.ToDecimal(tb2.Rows[0]["sheet_amount"]) * Conv.ToDecimal(tb2.Rows[0]["pay_type"]) || Conv.ToDecimal(dr["paid_amount"]) != Conv.ToDecimal(tb2.Rows[0]["paid_amount"]))
                    {
                        throw new Exception("应结明细表中数据已经被别人修改，请重新生成数据，被修改的业务单号是" + Conv.ToString(dr["voucher_no"]));
                    }
                    num += Conv.ToDecimal(dr["pay_amount"]);
                    //sql = "update rp_t_recpay_record_detail set paid_amount='" + Conv.ToDecimal(tb2.Rows[0]["已核销金额"]) + "',sheet_amount='" + Conv.ToDecimal(tb2.Rows[0]["sheet_amount"]) + "'  where sheet_no='" + sheet_no + "' and voucher_no='" + Conv.ToString(dr["voucher_no"]) + "'";
                    //d.ExecuteScalar(sql, null);

                }
                //sql = "select * from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'";
                //var tb3 = d.ExecuteToTable(sql, null);
                //decimal num1 = 0;
                //if (num < Conv.ToDecimal(tb.Rows[0]["num1"]))
                //    num1 = Conv.ToDecimal(tb.Rows[0]["num1"]) - num;
                //else
                //    num1 = 0;
                sql = "update rp_t_recpay_record_info set approve_flag='1',approve_man='" + approve_man +
                   "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //sql = "update rp_t_accout_payrec_flow set approve_flag='1',approve_man='" + approve_man +
                //  "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',sheet_amount=" + Conv.ToDecimal(tb.Rows[0]["total_amount"]) + ",paid_amount=" + (0 - (num - Conv.ToDecimal(tb.Rows[0]["free_money"]))) + " where voucher_no='" + sheet_no + "'";
                //d.ExecuteScalar(sql, null);
                if (Conv.ToDecimal(tb.Rows[0]["free_money"]) > 0)
                {
                    Model.rp_t_supcust_fy_master ord = new Model.rp_t_supcust_fy_master();
                    ord.sheet_no = MaxCode();
                    ord.supcust_no = tb.Rows[0]["supcust_no"].ToString();
                    ord.supcust_flag = "C";
                    ord.pay_date = System.DateTime.Now;
                    ord.old_no = tb.Rows[0]["sheet_no"].ToString();
                    ord.oper_id = approve_man;
                    ord.oper_date = DateTime.Now;
                    ord.approve_flag = "1";
                    ord.approve_man = approve_man;
                    ord.approve_date = System.DateTime.Now;
                    ord.is_payed = "0";
                    ord.sale_man = approve_man;
                    ord.branch_no = "";
                    ord.cm_branch = "00";
                    ord.other1 = "客户收款单免收金额";
                    ord.other2 = "CP";
                    ord.other3 = "";
                    ord.num1 = 0;
                    ord.num2 = 0;
                    ord.num3 = 0;
                    ord.visa_id = "";
                    ord.is_over = "0";
                    //ord.total_amount = 0;
                    ord.paid_amount = 0;
                    ord.pay_way = "";
                    ord.pay_name = "";
                    //editGrid1.AddColumn("kk_no", "代码", "", 90, 1, "", true);
                    //editGrid1.AddColumn("kk_name", "名称", "", 140, 1, "", false);
                    //editGrid1.AddColumn("pay_kind", "方向", "", 75, 2, "{0:借,1:贷,2:平}", false);
                    //editGrid1.AddColumn("kk_cash", "金额", "", 100, 3, "0.00", true);
                    //editGrid1.AddColumn("other1", "备注", "", 250, 1, "", true);
                    Model.rp_t_supcust_fy_detail line = new Model.rp_t_supcust_fy_detail();
                    line.sheet_no = ord.sheet_no;
                    line.kk_no = "";
                    line.kk_cash = Conv.ToDecimal(tb.Rows[0]["free_money"]);
                    line.other1 = "客户收款单免收金额";
                    sql = "select isnull(max(flow_id)+1,1) from rp_t_supcust_fy_detail";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.other2 = "";
                    line.other3 = "";
                    line.num1 = 1;
                    line.num2 = 0;
                    line.num3 = 0;
                    ord.total_amount = line.kk_cash;
                    ord.pay_type = "1";
                    d.Insert(ord);
                    d.Insert(line);

                    //转出客户流水(其他应收)

                    sql = "select top 1 * from rp_t_supcust_cash_flow where supcust_no='" + ord.supcust_no + "' and supcust_flag='C' order by flow_no ";
                    var dt2 = d.ExecuteToTable(sql, null);
                    decimal old_money1 = 0;
                    if (dt2.Rows.Count > 0)
                    {
                        old_money1 = Conv.ToDecimal(dt2.Rows[0]["new_money"]);
                    }

                    Model.rp_t_supcust_cash_flow it1 = new Model.rp_t_supcust_cash_flow();

                    sql = "select isnull(max(flow_no),0)+1 from rp_t_supcust_cash_flow";
                    it1.flow_no = Conv.ToInt64(d.ExecuteScalar(sql, null));
                    it1.supcust_no = ord.supcust_no;
                    it1.supcust_flag = "C";
                    decimal pay_type1;
                    if (ord.pay_type == "0" || ord.pay_type == "2")
                    {
                        pay_type1 = 1;
                    }
                    else
                    {
                        pay_type1 = -1;
                    }
                    it1.oper_type = "CM";
                    it1.voucher_no = ord.sheet_no;
                    it1.sheet_no = "";
                    it1.oper_date = ord.oper_date;
                    it1.old_money = old_money1;
                    it1.oper_money = ord.total_amount;
                    it1.free_money = 0;
                    it1.new_money = it1.old_money + (it1.oper_money - it1.free_money) * pay_type1;
                    it1.pay_date = System.DateTime.Now;
                    d.Insert(it1);
                    //为转入客户生成应结明细
                    rp_t_accout_payrec_flow ord1 = new rp_t_accout_payrec_flow();
                    sql = "select isnull(max(flow_no)+1,1) from rp_t_accout_payrec_flow";
                    ord1.flow_no = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    ord1.pay_type = pay_type1;
                    ord1.voucher_no = ord.sheet_no;
                    ord1.trans_no = "CM";
                    ord1.sheet_amount = ord.total_amount;
                    ord1.discount = 1;
                    ord1.pay_way = "";
                    ord1.paid_amount = 0;
                    ord1.tax_amount = 0;
                    ord1.pay_date = DateTime.Now;
                    ord1.supcust_no = ord.supcust_flag;
                    ord1.supcust_flag = "C";
                    ord1.free_money = 0;
                    ord1.oper_date = DateTime.Now;
                    //ord1.approve_flag = "0";
                    d.Insert(ord1);
                }
                Model.rp_t_accout_payrec_flow it = new Model.rp_t_accout_payrec_flow();
                sql = "select isnull(max(flow_no),0)+1 from rp_t_accout_payrec_flow";
                it.flow_no = IvyTran.Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                it.pay_type = -1;
                it.voucher_no = sheet_no;
                //if ((tb.Rows[0]["supcust_flag"].ToString() == "C"))
                it.trans_no = "CP";
                //else
                //    it.trans_no = "RP";
                it.sheet_amount = Conv.ToDecimal(tb.Rows[0]["total_amount"]);
                it.discount = 1;
                it.paid_amount = 0 - (num - Conv.ToDecimal(tb.Rows[0]["free_money"]));
                it.tax_amount = 0;
                it.pay_way = "";
                it.pay_date = System.DateTime.Now;
                it.supcust_no = Conv.ToString(tb.Rows[0]["supcust_no"]);
                it.supcust_flag = Conv.ToString(tb.Rows[0]["supcust_flag"]);
                it.free_money = 0;
                it.memo = "";
                it.other1 = "";
                it.other2 = "";
                it.other3 = "";
                it.num1 = 0;
                it.num2 = 0;
                it.num3 = 0;
                it.branch_no = "";
                it.sale_no = "A";
                it.oper_date = DateTime.Now;
                d.Insert(it);



                rp_t_recpay_record_info recpay = d.ExecuteToModel<rp_t_recpay_record_info>("select * from rp_t_recpay_record_info where sheet_no='" + sheet_no + "'", null);
                DataTable detail = d.ExecuteToTable("select * from rp_t_recpay_record_detail where sheet_no='" + sheet_no + "'", null);

                dynamic dyn = new ExpandoObject();
                if (recpay.pay_way == null)
                    dyn.pay_way = "";
                else
                    dyn.pay_way = recpay.pay_way;
                dyn.branch_no = recpay.branch_no;
                dyn.sheet_no = recpay.sheet_no;
                dyn.settle_sheet_no = recpay.sheet_no;
                dyn.visa_id = recpay.visa_id;
                dyn.pay_amount = recpay.total_amount;
                dyn.ml_amount = recpay.free_money;
                dyn.type_no = "101";
                dyn.oper_id = recpay.oper_id;
                dyn.approve_man = approve_man;
                dyn.memo = recpay.other1;
                dyn.supcust_no = recpay.supcust_no;
                dyn.supcust_flag = "C";



                // CashMaster(d, dyn);

                dyn.pay_amount = recpay.total_amount;
                //CashBalance(d, dyn);

                dyn.oper_type = "P";
                dyn.total_amount = recpay.total_amount;
                dyn.oper_date = recpay.oper_date;
                CustPayFlow(d, dyn);

                foreach (DataRow dr in detail.Rows)
                {
                    dyn.pay_amount = Conv.ToDecimal(dr["pay_amount"]);
                    dyn.ml_amount = Conv.ToDecimal(dr["pay_free"]);
                    dyn.sheet_no = dr["voucher_no"];
                    BackFilPayDetail(d, dyn);
                }

                db.CommitTran();
                LogHelper.writeSheetLog("CheckSheet ->CheckCustJSSheet()", "审核成功！", SessionHelper.oper_id, "操作日志", "WARNING", sheet_no);
                //LogHelper.writeSheetLog("CusSettle ->Delete()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR",sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeSheetLog("CheckSheet ->CheckCustJSSheet()", ex.ToString(), SessionHelper.oper_id, "异常日志", "ERROR", sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 销售计划单
        /// </summary>
        public void CheckSPSheet(string sheet_no, string approve_man, DateTime update_time)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                string sql = "select approve_flag,update_time from co_t_order_main where sheet_no='" + sheet_no + "'";
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
                sql = "update co_t_order_main set approve_flag='1',approve_man='" + approve_man + "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                sql += ",update_time='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                db.CommitTran();

                InOutBLL inOutBLL = new InOutBLL();
                inOutBLL.CreateInStorage(sheet_no);
                inOutBLL.CreateSO(sheet_no);
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSheet->CheckSPSheet()", ex.ToString(), sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }

}
