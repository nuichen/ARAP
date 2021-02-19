using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyFront.BLL
{
    class SaleData:IBLL.ISaleData 
    {
        void IBLL.ISaleData.Insert(Model.t_order_detail item,out int flow_id)
        {
            try
            {
                var sql = "select ifnull(max(flow_id),0)+1 as flow_id from t_order_detail where sheet_no='" + item.sheet_no + "' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                flow_id = 1;
                if (dt.Rows.Count > 0) flow_id = Conv.ToInt(dt.Rows[0]["flow_id"]);

                sql = "insert into t_order_detail values('" + flow_id + "','" + item.sheet_no + "','" + item.item_no + "','" + item.item_subno + "','" + item.item_name + "','" + item.unit_no + "'";
                sql += ",'" + item.oper_id + "','" + item.oper_date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + item.qty + "','" + item.price + "','" + item.amt + "'";
                sql += ",'" + item.jh + "','" + item.cost_price + "','" + item.branch_no + "','" + item.cus_no + "','" + item.sup_no + "','0','0','" + item.oper_date.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                sql += ",'" + item.oper_id + "','" + item.source_price + "','" + item.discount + "','" + item.is_give + "') ";
                Program.db.ExecuteScalar(sql, null);
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->Insert()", ex.ToString(), null);
                throw ex;
            }
        }

        //订单结算
        void IBLL.ISaleData.SubmitOrder(string sheet_no, decimal discount, string oper_id)
        {
            try
            {
                var sql = "select ifnull(is_upload,'0') is_upload from t_order_detail where sheet_no='" + sheet_no + "' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count >= 0)
                {
                    if (dt.Rows[0]["is_upload"].ToString() == "1") throw new Exception("订单已提交");

                    //销售流水
                    var condition_sql = "";
                    if (discount > 0 && discount < 1) 
                    {
                        condition_sql = ",discount='" + discount + "',price=source_price*" + discount + ",amt=qty*source_price*" + discount + " ";
                    }
                    sql = "update t_order_detail set approve_flag='1',update_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',update_oper='" + oper_id + "' " + condition_sql;
                    sql += "where sheet_no='" + sheet_no + "' and approve_flag='0' ";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->SubmitOrder()", ex.ToString(), sheet_no);
                throw ex;
            }
        }

        void IBLL.ISaleData.InsertPayFlow(Model.ot_pay_flow item, out int flow_id)
        {
            try
            {
                var sql = "select ifnull(max(flow_id),0)+1 as flow_id from ot_pay_flow where sheet_no='" + item.sheet_no + "' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                flow_id = 1;
                if (dt.Rows.Count > 0) flow_id = Conv.ToInt(dt.Rows[0]["flow_id"]);

                sql = "insert into ot_pay_flow values('" + item.sheet_no + "','" + flow_id + "','" + item.cus_no + "','" + item.oper_id + "','" + item.oper_date.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                sql += ",'" + item.pay_way + "','" + item.sale_amount + "','" + item.pay_amount + "','" + item.old_amount + "','" + item.ml + "','" + item.jh + "','1','','0') ";
                Program.db.ExecuteScalar(sql, null);

            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->InsertPayFlow()", ex.ToString(), null);
                throw ex;
            }
        }

        //删除流水
        void IBLL.ISaleData.DeleteSaleFlow(string sheet_no, int flow_id, string oper_id)
        {
            string sql = "update t_order_detail set approve_flag='2',update_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',update_oper='" + oper_id + "' ";
            sql += "where sheet_no='" + sheet_no + "' and flow_id=" + flow_id + " ";
            Program.db.ExecuteScalar(sql, null);
        }

        //改数量
        void IBLL.ISaleData.UpdateSaleFlowQty(string sheet_no, int flow_id, decimal qty, string oper_id)
        {
            string sql = "update t_order_detail set qty=" + qty + ",amt=" + qty + "*price,update_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',update_oper='" + oper_id + "' ";
            sql += "where sheet_no='" + sheet_no + "' and flow_id=" + flow_id + " ";
            Program.db.ExecuteScalar(sql, null);
        }

        //改价格
        void IBLL.ISaleData.UpdateSaleFlowPrice(string sheet_no, int flow_id, decimal sale_price, string oper_id)
        {
            string sql = "update t_order_detail set price=" + sale_price + ",amt=qty*" + sale_price + ",update_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',update_oper='" + oper_id + "' ";
            sql += "where sheet_no='" + sheet_no + "' and flow_id=" + flow_id + " ";
            Program.db.ExecuteScalar(sql, null);
        }

        //赠送
        void IBLL.ISaleData.GiveSaleFlow(string sheet_no, int flow_id, string oper_id)
        {
            string sql = "update t_order_detail set is_give='1',price=0,amt=0,update_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',update_oper='" + oper_id + "' ";
            sql += "where sheet_no='" + sheet_no + "' and flow_id=" + flow_id + " ";
            Program.db.ExecuteScalar(sql, null);
        }

        //销售流水
        List<Model.t_order_detail> IBLL.ISaleData.GetSaleFlow(string date1, string date2,string order_key, int page_no, int page_size, out int total)
        {
            total = 0;
            try
            {
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                var sql = "select * from t_order_detail where ifnull(approve_flag,'0')='1' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' and sheet_no like '" + order_key + "%' ";
                sql += "order by oper_date,sheet_no,flow_id asc limit " + (page_no - 1) * page_size + "," + page_size;

                var dt = Program.db.ExecuteToTable(sql, null);
                var lst = new List<Model.t_order_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new Model.t_order_detail();
                    item.flow_id = Conv.ToInt(dr["flow_id"].ToString());
                    item.sheet_no = dr["sheet_no"].ToString();
                    item.amt = Conv.ToDecimal(dr["amt"].ToString());
                    item.item_name = dr["item_name"].ToString();
                    item.item_no = dr["item_no"].ToString();
                    item.item_subno = dr["item_subno"].ToString();
                    item.unit_no = dr["unit_no"].ToString();
                    item.branch_no = dr["branch_no"].ToString();
                    item.cus_no = dr["cus_no"].ToString();
                    item.oper_date = Conv.ToDateTime(dr["oper_date"].ToString());
                    item.qty = Conv.ToDecimal(dr["qty"].ToString());
                    item.price = Conv.ToDecimal(dr["price"].ToString());
                    item.cost_price = Conv.ToDecimal(dr["cost_price"].ToString());
                    item.approve_flag = dr["approve_flag"].ToString();
                    item.source_price = Conv.ToDecimal(dr["source_price"].ToString());
                    item.discount = Conv.ToDecimal(dr["discount"].ToString());
                    item.is_give = dr["is_give"].ToString();
                    lst.Add(item);
                }

                sql = "select count(*) total from t_order_detail where ifnull(approve_flag,'0')='1' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' and sheet_no like '" + order_key + "%' ";
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) total = Conv.ToInt(dt.Rows[0]["total"]);
                return lst;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetSaleFlow()", ex.ToString());
                throw ex;
            }
        }

        //销售汇总
        void IBLL.ISaleData.GetSaleFlowSum(string date1, string date2, string order_key, out int order_count, out decimal total_amt, out decimal give_amt)
        {
            order_count = 0;
            total_amt = 0;
            give_amt = 0;
            try
            {
                //订单数
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                var sql = "select sheet_no from t_order_detail where ifnull(approve_flag,'0')='1' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' and sheet_no like '" + order_key + "%' group by sheet_no ";
                var dt = Program.db.ExecuteToTable(sql, null);
                order_count = dt.Rows.Count;

                //订单应收金额
                sql = "select sum(amt) total_amt from t_order_detail where ifnull(approve_flag,'0')='1' and ifnull(is_give,'0')='0' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' and sheet_no like '" + order_key + "%' ";
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) total_amt = Conv.ToDecimal(dt.Rows[0]["total_amt"]);
                
                //赠送金额
                sql = "select sum(source_price*qty) total_amt from t_order_detail where ifnull(approve_flag,'0')='1' and ifnull(is_give,'0')='1' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' and sheet_no like '" + order_key + "%' ";
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) give_amt = Conv.ToDecimal(dt.Rows[0]["total_amt"]);
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetSaleFlow()", ex.ToString());
                throw ex;
            }
        }

        //销售对账
        List<Model.ot_pay_flow> IBLL.ISaleData.GetSaleSum(string date1, string date2, string order_key, out int order_count, out decimal total_amt, out decimal give_amt)
        {
            order_count = 0;
            total_amt = 0;
            give_amt = 0;
            try
            {
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                string condition_str = " and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' and ifnull(approve_flag,'0')='1' ";
                condition_str += " and sheet_no like '" + order_key + "%' ";

                var sql = "select sheet_no from t_order_detail where 1=1 " + condition_str + " group by sheet_no ";
                var dt = Program.db.ExecuteToTable(sql, null);
                order_count = dt.Rows.Count;

                //过滤赠送金额
                sql = "select ifnull(sum(amt),0) total_amt from t_order_detail where ifnull(is_give,'0')='0' " + condition_str;
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) total_amt = Conv.ToDecimal(dt.Rows[0]["total_amt"]);

                //赠送金额
                sql = "select ifnull(sum(source_price*qty),0) total_amt from t_order_detail where ifnull(is_give,'0')='1' " + condition_str;
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) give_amt = Conv.ToDecimal(dt.Rows[0]["total_amt"]);

                sql = "select sheet_no,flow_id,pay_way,sale_amount,pay_amount,ml from ot_pay_flow ";
                sql += "where ifnull(approve_flag,'0')='1' and sheet_no in (select distinct sheet_no from t_order_detail where 1=1 " + condition_str + ") ";
                sql += "order by sheet_no,flow_id asc ";

                dt = Program.db.ExecuteToTable(sql, null);
                var lst = new List<Model.ot_pay_flow>();
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new Model.ot_pay_flow();
                    item.flow_id = Conv.ToInt(dr["flow_id"].ToString());
                    item.sheet_no = dr["sheet_no"].ToString();
                    item.sale_amount = Conv.ToDecimal(dr["sale_amount"].ToString());
                    item.pay_way = dr["pay_way"].ToString();
                    item.pay_amount = Conv.ToDecimal(dr["pay_amount"].ToString());
                    item.ml = Conv.ToDecimal(dr["ml"].ToString());
                    lst.Add(item);
                }
                return lst;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetSaleSum()", ex.ToString());
                throw ex;
            }
        }

        //收银流水
        List<Model.ot_pay_flow> IBLL.ISaleData.GetPayFlow(string date1, string date2, string order_key, int page_no, int page_size, out int total)
        {
            total = 0;
            try
            {
                var sup_flag = "C";
                if (order_key == "PI") sup_flag = "S";
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                string condition_str = " and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' and ifnull(approve_flag,'0')='1' ";
                condition_str += " and sheet_no like '" + order_key + "%' ";

                string sql = "select a.*,b.sup_name from ot_pay_flow a ";
                sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='" + sup_flag + "') b on a.cus_no=b.supcust_no ";
                sql += "where ifnull(a.approve_flag,'0')='1' and a.sheet_no in (select distinct sheet_no from t_order_detail where 1=1 " + condition_str + ") ";
                sql += "order by a.sheet_no,a.flow_id asc  limit " + (page_no - 1) * page_size + "," + page_size;

                var dt = Program.db.ExecuteToTable(sql, null);
                var lst = new List<Model.ot_pay_flow>();
                foreach (DataRow dr in dt.Rows)
                {
                    var item = new Model.ot_pay_flow();
                    item.flow_id = Conv.ToInt(dr["flow_id"].ToString());
                    item.sheet_no = dr["sheet_no"].ToString();
                    item.sale_amount = Conv.ToDecimal(dr["sale_amount"].ToString());
                    item.pay_way = dr["pay_way"].ToString();
                    item.pay_amount = Conv.ToDecimal(dr["pay_amount"].ToString());
                    item.oper_date = Conv.ToDateTime(dr["oper_date"].ToString());
                    item.oper_id = dr["oper_id"].ToString();
                    item.approve_flag = dr["approve_flag"].ToString();
                    item.cus_no = dr["cus_no"].ToString();
                    item.sup_name = dr["sup_name"].ToString();
                    lst.Add(item);
                }

                sql = "select count(*) total from ot_pay_flow where ifnull(approve_flag,'0')='1' and sheet_no in (select distinct sheet_no from t_order_detail where 1=1 " + condition_str + ")  ";
                
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) total = Conv.ToInt(dt.Rows[0]["total"]);

                return lst;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetPayFlow()", ex.ToString());
                throw ex;
            }
        }


        //销售出库单
        DataTable IBLL.ISaleData.GetSaleSheet(string date1, string date2, int page_no, int page_size, out int total)
        {
            total = 0;
            try
            {
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                var sql = "select a.sheet_no,a.branch_no,a.cust_no,a.pay_way,a.real_amount,a.total_amount,a.paid_amount,ifnull(a.is_upload,'0') is_upload,a.oper_date,b.sup_name ";
                sql += "from sm_t_salesheet a ";
                sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C' and display_flag='1') b on a.cust_no=b.supcust_no ";
                sql += "where a.oper_date>='" + date1 + "' and a.oper_date<='" + d2 + "' order by a.oper_date desc ";
                sql += "limit " + (page_no - 1) * page_size + "," + page_size;
                var dt = Program.db.ExecuteToTable(sql, null);

                sql = "select count(*) total from sm_t_salesheet where oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                var dt2 = Program.db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0) total = Conv.ToInt(dt2.Rows[0]["total"]);

                return dt;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetSaleSheet()", ex.ToString());
                throw ex;
            }
        }

        //销售出库单汇总
        void IBLL.ISaleData.GetSaleSheetSum(string date1, string date2, out int order_count, out decimal total_amt, out decimal fact_amt)
        {
            order_count = 0;//出库单数
            total_amt = 0;//出库单实际金额
            fact_amt = 0;//出库单预付金额
            try
            {
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                var sql = "select count(*) total from sm_t_salesheet where oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) order_count = Conv.ToInt(dt.Rows[0]["total"]);

                sql = "select ifnull(sum(total_amount),0) total_amt,ifnull(sum(real_amount),0) fact_amt ";
                sql += "from sm_t_salesheet where oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) 
                {
                    total_amt = Conv.ToDecimal(dt.Rows[0]["total_amt"]);
                    fact_amt = Conv.ToDecimal(dt.Rows[0]["fact_amt"]);
                }

            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetSaleSheetSum()", ex.ToString());
                throw ex;
            }
        }

        //销售出库单明细
        DataTable IBLL.ISaleData.GetSaleSheetDetail(string sheet_no)
        {
            try
            {
                var sql = "select a.sheet_sort,a.item_no,a.unit_no,a.real_price,a.cost_price,a.sale_qnty,a.sale_money,ifnull(other3,'0') other3,a.item_subno,a.item_name ";
                sql += "from sm_t_salesheet_detail a ";
                sql += "where a.sheet_no='" + sheet_no + "' order by a.sheet_sort asc ";

                var dt = Program.db.ExecuteToTable(sql, null);
                return dt;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetSaleSheetDetail()", ex.ToString(),sheet_no);
                throw ex;
            }
        }

        //销售出库单明细
        Model.sm_t_salesheet IBLL.ISaleData.GetSaleSheetMasterDetail(string sheet_no,out DataTable lines,out decimal ml)
        {
            ml = 0;
            try
            {
                var sql = "select a.sheet_sort,a.item_no,a.unit_no,a.real_price,a.sale_qnty,a.sale_money,a.item_subno,a.item_name,b.item_size ";
                sql += "from sm_t_salesheet_detail a ";
                sql += "left join bi_t_item_info b on a.item_no=b.item_no ";
                sql += "where a.sheet_no='" + sheet_no + "' order by a.sheet_sort asc ";

                lines = Program.db.ExecuteToTable(sql, null);

                sql = "select a.sheet_no,a.branch_no,a.cust_no,a.pay_way,a.real_amount,a.total_amount,a.paid_amount,a.discount,a.oper_id,a.oper_date,b.sup_name ";
                sql += "from sm_t_salesheet a ";
                sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C') b on a.cust_no=b.supcust_no ";
                sql += "where a.sheet_no='" + sheet_no + "' ";
                var dt2 = Program.db.ExecuteToTable(sql, null);
                Model.sm_t_salesheet item = new Model.sm_t_salesheet();
                if (dt2.Rows.Count > 0) 
                {
                    item.sheet_no = dt2.Rows[0]["sheet_no"].ToString();
                    item.branch_no = dt2.Rows[0]["branch_no"].ToString();
                    item.cust_no = dt2.Rows[0]["cust_no"].ToString() + "/" + dt2.Rows[0]["sup_name"].ToString();
                    item.pay_way = dt2.Rows[0]["pay_way"].ToString();
                    item.real_amount = Conv.ToDecimal(dt2.Rows[0]["real_amount"].ToString());
                    item.total_amount = Conv.ToDecimal(dt2.Rows[0]["total_amount"].ToString());
                    item.paid_amount = Conv.ToDecimal(dt2.Rows[0]["paid_amount"].ToString());
                    item.discount = Conv.ToDecimal(dt2.Rows[0]["discount"].ToString());
                    item.oper_date = Conv.ToDateTime(dt2.Rows[0]["oper_date"].ToString());
                    item.oper_id = dt2.Rows[0]["oper_id"].ToString();
                }

                sql = "select ifnull(sum(pay_amount),0) as ml from ot_pay_flow where sheet_no='" + sheet_no + "' and pay_way='Y' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) ml = Conv.ToDecimal(dt.Rows[0]["ml"]);

                return item;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetSaleSheetMasterDetail()", ex.ToString(), sheet_no);
                throw ex;
            }
        }


        //采购入库单
        DataTable IBLL.ISaleData.GetInOutSheet(string date1, string date2, int page_no, int page_size, out int total)
        {
            total = 0;
            try
            {
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                var sql = "select a.sheet_no,a.branch_no,a.supcust_no,a.total_amount,a.inout_amount,ifnull(a.is_upload,'0') is_upload,a.oper_date,b.sup_name,a.pay_way ";
                sql += "from ic_t_inout_store_master a ";
                sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='S' and display_flag='1') b on a.supcust_no=b.supcust_no ";
                sql += "where a.trans_no='A' and a.oper_date>='" + date1 + "' and a.oper_date<='" + d2 + "' order by a.oper_date desc ";
                sql += "limit " + (page_no - 1) * page_size + "," + page_size;
                var dt = Program.db.ExecuteToTable(sql, null);

                sql = "select count(*) total from ic_t_inout_store_master where trans_no='A' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                var dt2 = Program.db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0) total = Conv.ToInt(dt2.Rows[0]["total"]);

                return dt;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetInOutSheet()", ex.ToString());
                throw ex;
            }
        }

        //采购入库单汇总
        void IBLL.ISaleData.GetInOutSheetSum(string date1, string date2, out int order_count, out decimal total_amt, out decimal fact_amt)
        {
            order_count = 0;
            total_amt = 0;
            fact_amt = 0;
            try
            {
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                var sql = "select count(*) total from ic_t_inout_store_master where trans_no='A' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) order_count = Conv.ToInt(dt.Rows[0]["total"]);

                sql = "select ifnull(sum(total_amount),0) total_amt,ifnull(sum(inout_amount),0) fact_amt ";
                sql += "from ic_t_inout_store_master where trans_no='A' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    total_amt = Conv.ToDecimal(dt.Rows[0]["total_amt"]);
                    fact_amt = Conv.ToDecimal(dt.Rows[0]["fact_amt"]);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetInOutSheetSum()", ex.ToString());
                throw ex;
            }
        }

        //采购入库单明细
        DataTable IBLL.ISaleData.GetInOutSheetDetail(string sheet_no)
        {
            try
            {
                var sql = "select a.sheet_sort,a.item_no,a.unit_no,a.valid_price,a.cost_price,a.in_qty,a.item_subno,a.item_name ";
                sql += "from ic_t_inout_store_detail a ";
                sql += "where a.sheet_no='" + sheet_no + "' order by a.sheet_sort asc ";

                var dt = Program.db.ExecuteToTable(sql, null);
                return dt;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetInOutSheetDetail()", ex.ToString(), sheet_no);
                throw ex;
            }
        }

        //销售退货入库单
        DataTable IBLL.ISaleData.GetTHInOutSheet(string date1, string date2, int page_no, int page_size, out int total)
        {
            total = 0;
            try
            {
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                var sql = "select a.sheet_no,a.branch_no,a.supcust_no,a.total_amount,a.inout_amount,ifnull(a.is_upload,'0') is_upload,a.oper_date,b.sup_name,a.pay_way ";
                sql += "from ic_t_inout_store_master a ";
                sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C' and display_flag='1') b on a.supcust_no=b.supcust_no ";
                sql += "where a.trans_no='D' and a.oper_date>='" + date1 + "' and a.oper_date<='" + d2 + "' order by a.oper_date desc ";
                sql += "limit " + (page_no - 1) * page_size + "," + page_size;
                var dt = Program.db.ExecuteToTable(sql, null);

                sql = "select count(*) total from ic_t_inout_store_master where trans_no='D' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                var dt2 = Program.db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0) total = Conv.ToInt(dt2.Rows[0]["total"]);

                return dt;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetTHInOutSheet()", ex.ToString());
                throw ex;
            }
        }

        //销售退货入库单汇总
        void IBLL.ISaleData.GetTHInOutSheetSum(string date1, string date2, out int order_count, out decimal total_amt, out decimal fact_amt)
        {
            order_count = 0;
            total_amt = 0;
            fact_amt = 0;
            try
            {
                var d2 = Conv.ToDateTime(date2).AddDays(1).ToString("yyyy-MM-dd");
                var sql = "select count(*) total from ic_t_inout_store_master where trans_no='D' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0) order_count = Conv.ToInt(dt.Rows[0]["total"]);

                sql = "select ifnull(sum(total_amount),0) total_amt,ifnull(sum(inout_amount),0) fact_amt ";
                sql += "from ic_t_inout_store_master where trans_no='D' and oper_date>='" + date1 + "' and oper_date<='" + d2 + "' ";
                dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    total_amt = Conv.ToDecimal(dt.Rows[0]["total_amt"]);
                    fact_amt = Conv.ToDecimal(dt.Rows[0]["fact_amt"]);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetTHInOutSheetSum()", ex.ToString());
                throw ex;
            }
        }


        //取客户商品价格
        decimal IBLL.ISaleData.GetCusItemPrice(string cust_id, string item_no)
        {
            decimal price = 0;
            //
            string sql = "select ifnull(other1,'1') other1,ifnull(cust_level,'1') cust_level ";
            sql += "from bi_t_supcust_info where supcust_no='" + cust_id + "' and supcust_flag='C' ";
            var dt1 = Program.db.ExecuteToTable(sql,null);
            var price_type = "1";
            int cust_level = 1;
            if (dt1.Rows.Count > 0)
            {
                price_type = dt1.Rows[0]["other1"].ToString();
                cust_level = Conv.ToInt(dt1.Rows[0]["cust_level"]);
                if (cust_level == 0) cust_level = 1;
            }
            if (price_type == "2")//最后价格
            {
                sql = "select * from bi_t_cust_price where cust_id='" + cust_id + "' and item_no='" + item_no + "' ";
                var dt2 = Program.db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0)
                {
                    //bi_t_cust_price.last_price
                    price = Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                    if (price > 0) return price;

                    //bi_t_cust_price.new_price
                    price = Conv.ToDecimal(dt2.Rows[0]["new_price"]);
                    if (price > 0) return price;
                }
            }
            else if (price_type == "1")//约定价格
            {
                sql = "select * from sys_t_system where sys_var_id='cust_pricetype' ";
                var dt3 = Program.db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0) 
                {
                    var temp_price_type = Conv.ToInt(dt3.Rows[0]["sys_var_value"]);
                    if (temp_price_type == 1) //最后批发价
                    {
                        sql = "select * from bi_t_cust_price where cust_id='" + cust_id + "' and item_no='" + item_no + "' ";
                        var dt2 = Program.db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_cust_price.last_price
                            price = Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                            if (price > 0) return price;

                            //bi_t_cust_price.new_price
                            price = Conv.ToDecimal(dt2.Rows[0]["new_price"]);
                            if (price > 0) return price;
                        }
                    }
                    else if (temp_price_type == 3)//最低批发价
                    {
                        sql = "select * from bi_t_cust_price where cust_id='" + cust_id + "' and item_no='" + item_no + "' ";
                        var dt2 = Program.db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_cust_price.last_price
                            price = Conv.ToDecimal(dt2.Rows[0]["bottom_price"]);
                            if (price > 0) return price;

                            //bi_t_cust_price.new_price
                            price = Conv.ToDecimal(dt2.Rows[0]["new_price"]);
                            if (price > 0) return price;
                        }
                    }
                    else if (temp_price_type == 5) //最后进货价
                    {
                        sql = "select top 1 last_price from bi_t_sup_item where item_no='" + item_no + "'";
                        sql += "and last_sheet_no=(select max(last_sheet_no) from bi_t_sup_item where item_no='" + item_no + "') ";
                        var dt2 = Program.db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0) 
                        {
                            //bi_t_sup_item.last_price
                            price = Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                            if (price > 0) return price;
                        }
                    }
                }
            }

            //以上价格策略无法取到相应价格时，默认取商品档案约定级别价格
            if(1 == 1)
            {
                //bi_t_item_info中约定的商品价格
                sql = "select base_price,base_price2,base_price3,base_price4,base_price5 ";
                sql += "from bi_t_item_info where item_no='" + item_no + "' ";
                var dt3 = Program.db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    //取客户级别价格，如无级别则默认取base_price
                    if (dt3.Columns.Count >= cust_level)
                    {
                        price = Conv.ToDecimal(dt3.Rows[0][cust_level - 1]);
                        if (price > 0) return price;
                    }
                    else
                    {
                        price = Conv.ToDecimal(dt3.Rows[0][0]);
                        if (price > 0) return price;
                    }
                }
            }
            //

            return price;
        }

        //取供应商商品价格
        decimal IBLL.ISaleData.GetSupItemPrice(string sup_id, string item_no)
        {
            decimal price = 0;
            //
            string sql = "select ifnull(other1,'1') other1 ";
            sql += "from bi_t_supcust_info where supcust_no='" + sup_id + "' and supcust_flag='S' ";
            var dt1 = Program.db.ExecuteToTable(sql, null);
            var price_type = "1";
            if (dt1.Rows.Count > 0)
            {
                price_type = dt1.Rows[0]["other1"].ToString();
            }
            if (price_type == "2")//最后价格
            {
                sql = "select * from bi_t_sup_item where sup_id='" + sup_id + "' and item_no='" + item_no + "' ";
                var dt2 = Program.db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0)
                {
                    //bi_t_sup_item.last_price
                    price = Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                    if (price > 0) return price;

                    //bi_t_sup_item.price
                    price = Conv.ToDecimal(dt2.Rows[0]["price"]);
                    if (price > 0) return price;
                }
            }
            else if (price_type == "1")//约定价格
            {
                sql = "select * from sys_t_system where sys_var_id='sup_pricetype' ";
                var dt3 = Program.db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    var temp_price_type = Conv.ToInt(dt3.Rows[0]["sys_var_value"]);
                    if (temp_price_type == 1) //最后进价
                    {
                        sql = "select * from bi_t_sup_item where sup_id='" + sup_id + "' and item_no='" + item_no + "' ";
                        var dt2 = Program.db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_sup_item.last_price
                            price = Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                            if (price > 0) return price;

                            //bi_t_sup_item.price
                            price = Conv.ToDecimal(dt2.Rows[0]["price"]);
                            if (price > 0) return price;
                        }
                    }
                    else if (temp_price_type == 2) //约定进价
                    {
                        sql = "select * from bi_t_sup_item where sup_id='" + sup_id + "' and item_no='" + item_no + "' ";
                        var dt2 = Program.db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_sup_item.price
                            price = Conv.ToDecimal(dt2.Rows[0]["price"]);
                            if (price > 0) return price;
                        }
                    }
                    else if (temp_price_type == 3)//最低进价
                    {
                        sql = "select * from bi_t_sup_item where sup_id='" + sup_id + "' and item_no='" + item_no + "' ";
                        var dt2 = Program.db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_sup_item.bottom_price
                            price = Conv.ToDecimal(dt2.Rows[0]["bottom_price"]);
                            if (price > 0) return price;

                            //bi_t_sup_item.new_price
                            price = Conv.ToDecimal(dt2.Rows[0]["price"]);
                            if (price > 0) return price;
                        }
                    }
                }
            }

            //以上价格策略无法取到相应价格时，默认取商品档案中的进价
            if (1 == 1)
            {
                //bi_t_item_info中约定的商品价格
                sql = "select price from bi_t_item_info where item_no='" + item_no + "' ";
                var dt3 = Program.db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    //取商品档案中的默认进价
                    price = Conv.ToDecimal(dt3.Rows[0]["price"]);
                    return price;
                }
            }
            //

            return price;
        }

        bool IBLL.ISaleData.CheckCusIsRetail(string cust_id) 
        {
            string sql = "select * from bi_t_supcust_info where supcust_no='" + cust_id + "' and supcust_flag='C'";
            var dt = Program.db.ExecuteToTable(sql, null);
            if (dt.Rows.Count > 0 && dt.Rows[0]["is_retail"].ToString() == "1") 
            {
                return true;
            }
            return false;
        }

        //未上传客户欠款
        decimal IBLL.ISaleData.GetCusNoPayAmt(string cust_id)
        {
            decimal no_pay = 0;
            try
            {
                var sql = "select cust_no,ifnull(sum(real_amount),0) sum_real_amt,ifnull(sum(total_amount),0) sum_total_amt ";
                sql += "from sm_t_salesheet where cust_no='" + cust_id + "' and ifnull(is_upload,'0')='0' ";
                var dt = Program.db.ExecuteToTable(sql, null);

                if (dt.Rows.Count > 0)
                {
                    var sum_real_amt = Conv.ToDecimal(dt.Rows[0]["sum_real_amt"]);
                    var sum_total_amt = Conv.ToDecimal(dt.Rows[0]["sum_total_amt"]);
                    no_pay = sum_total_amt - sum_real_amt;
                }

                return no_pay;
            }
            catch (Exception ex)
            {
                Log.writeLog("SaleData->GetSaleSheet()", ex.ToString());
                throw ex;
            }
        }


    }

}
