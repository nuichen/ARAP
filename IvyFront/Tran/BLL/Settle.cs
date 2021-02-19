using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tran.BLL
{
    public class Settle : IBLL.ISettle
    {

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


        void IBLL.ISettle.WriteFHD(List<model.sm_t_salesheet> lst1, List<model.sm_t_salesheet_detail> lst2, List<model.ot_pay_flow> lst3)
        {
            var db = new DB.MySqlByHandClose(Appsetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //提取付款方式
                string sql = "select * from bi_t_payment_info";
                var tb = d.ExecuteToTable(sql, null);
                Dictionary<string, model.bi_t_payment_info> dic_payment = new Dictionary<string, model.bi_t_payment_info>();
                foreach (System.Data.DataRow row in tb.Rows)
                {
                    string pay_way = row["pay_way"].ToString().Trim();
                    dic_payment.Add(pay_way, DB.ReflectionHelper.DataRowToModel<model.bi_t_payment_info>(row));
                }
                //重取单号
                foreach (model.sm_t_salesheet item in lst1)
                {
                    string offline_sheet_no = item.sheet_no;
                    item.sheet_no = GetSheetCode(db, "SO");
                    item.offline_sheet_no = offline_sheet_no;
                }
                //
                Dictionary<string, model.sm_t_salesheet> dic = new Dictionary<string, model.sm_t_salesheet>();
                foreach (model.sm_t_salesheet item in lst1)
                {
                    dic.Add(item.offline_sheet_no, item);
                }
                //生成明细行号,单号
                foreach (model.sm_t_salesheet item in lst1)
                {
                    item.temp1 = 0;
                }
                foreach (model.sm_t_salesheet_detail item in lst2)
                {
                    model.sm_t_salesheet it;
                    if (dic.TryGetValue(item.sheet_no, out it) == true)
                    {
                        it.temp1 += 1;
                        item.sheet_no = it.sheet_no;

                    }
                }
                //生成付款明细行号,单号
                foreach (model.sm_t_salesheet item in lst1)
                {
                    item.temp1 = 0;
                }
                foreach (model.ot_pay_flow item in lst3)
                {
                    model.sm_t_salesheet it;
                    if (dic.TryGetValue(item.sheet_no, out it) == true)
                    {
                        it.temp1 += 1;
                        item.flow_id = it.temp1;
                        item.sheet_no = it.sheet_no;
                    }
                }

                //

                //写入单头
                foreach (model.sm_t_salesheet item in lst1)
                {
                    d.Insert(item);
                }
                //写入单体
                foreach (model.sm_t_salesheet_detail item in lst2)
                {
                    d.Insert(item);
                }
                //写入支付
                foreach (model.ot_pay_flow item in lst3)
                {
                    d.Insert(item);
                }
                //批处理
                foreach (model.sm_t_salesheet item in lst1)
                {
                    //审核发货单
                    item.approve_flag = "1";
                    item.approve_man = "1001";
                    item.approve_date = System.DateTime.Now;
                    d.Update(item, "sheet_no", "approve_flag,approve_man,approve_date");
                    //提取付款方式
                    string pay_way = "";
                    decimal pay_amount = 0;
                    string ml_pay_way = "";
                    decimal ml_amount = 0;
                    foreach (model.ot_pay_flow payflow in lst3)
                    {
                        if (item.sheet_no == payflow.sheet_no)
                        {
                            if (payflow.pay_way != "Y")
                            {
                                if (pay_way == "")//除了抹零，只处理一种支付方式 
                                {
                                    pay_way = payflow.pay_way;
                                    pay_amount += payflow.pay_amount;
                                }
                                else
                                {
                                    pay_amount += payflow.pay_amount;
                                }
                            }
                            else
                            {
                                ml_pay_way = "Y";
                                ml_amount += payflow.pay_amount;
                            }
                        }
                    }

                    //写入结算单
                    string settle_sheet_no = "";
                    string visa_id = "";
                    if (pay_way != "")
                    {
                        model.rp_t_recpay_record_info it = new model.rp_t_recpay_record_info();
                        it.sheet_no = GetSheetCode(db, "CP");
                        settle_sheet_no = it.sheet_no;
                        it.supcust_no = item.cust_no;
                        it.supcust_flag = "C";
                        it.flag_post = "";
                        it.total_amount = pay_amount;
                        if (ml_pay_way != "")
                        {
                            it.free_money = ml_amount;
                        }
                        it.coin_no = "RMB";
                        it.coin_rate = 1;
                        it.pay_way = pay_way;
                        it.approve_flag = "0";
                        it.oper_id = "1001";
                        it.oper_date = System.DateTime.Now;
                        it.deal_man = "";
                        it.approve_man = "";
                        it.approve_date = System.DateTime.MinValue;
                        it.other1 = "";
                        it.other2 = "";
                        it.other3 = "";
                        model.bi_t_payment_info payment;
                        if (dic_payment.TryGetValue(pay_way, out payment) == true)
                        {
                            if (payment.visa_id == null || payment.visa_id == "")
                            {
                                throw new Exception("付款方式" + pay_way + "找不到默认现金银行帐户!");
                            }
                            else
                            {
                                it.visa_id = payment.visa_id;
                            }

                        }
                        else
                        {
                            throw new Exception("付款方式" + pay_way + "找不到默认现金银行帐户!");
                        }


                        visa_id = it.visa_id;

                        it.num1 = 0;
                        it.num2 = 0;
                        it.num3 = 0;
                        it.cm_branch = "00";
                        it.branch_no = item.branch_no;
                        it.from_date = System.DateTime.Now;
                        it.to_date = System.DateTime.Now;
                        it.rc_sheet_no = "";
                        it.pay_memo = "";
                        it.money_date = System.DateTime.Now;
                        d.Insert(it);
                        //
                        model.rp_t_recpay_record_detail line = new model.rp_t_recpay_record_detail();
                        line.sheet_no = it.sheet_no;
                        line.voucher_no = item.sheet_no;
                        line.sheet_amount = item.total_amount;
                        line.paid_amount = 0;
                        line.paid_free = 0;
                        line.pay_amount = pay_amount;
                        line.pay_free = ml_amount;
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
                    //写入收支流水
                    if (pay_way != "")
                    {
                        model.bank_t_cash_master it = new model.bank_t_cash_master();
                        it.sheet_no = GetSheetCode(d, "SR");
                        it.branch_no = item.branch_no;
                        it.voucher_no = settle_sheet_no;
                        it.visa_id = visa_id;
                        it.visa_in = "";
                        it.pay_way = pay_way;
                        it.coin_no = "RMB";
                        it.coin_rate = 1;
                        it.deal_man = "";
                        it.oper_id = "1001";
                        it.oper_date = System.DateTime.Now;
                        it.bill_total = pay_amount;
                        it.bill_flag = "S";
                        it.cm_branch = "00";
                        it.approve_flag = "0";
                        it.approve_man = "";
                        it.approve_date = System.DateTime.MinValue;
                        it.other1 = "";
                        it.other2 = "";
                        it.other3 = "";
                        it.num1 = 0;
                        it.num2 = 0;
                        it.num3 = 0;
                        d.Insert(it);
                        model.bank_t_cash_detail line = new model.bank_t_cash_detail();
                        line.sheet_no = it.sheet_no;
                        line.type_no = "101";
                        line.bill_cash = pay_amount;
                        line.memo = "";
                        d.Insert(line);
                        //
                        it.approve_date = System.DateTime.Now;
                        it.approve_flag = "1";
                        it.approve_man = "1001";
                        d.Update(it, "sheet_no", "approve_date,approve_flag,approve_man");
                    }

                }


                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("", ex.ToString());
                throw;
            }
            finally
            {
                db.Close();
            }


            //
        }

        void IBLL.ISettle.WriteCGRK(List<model.ic_t_inout_store_master> lst1, List<model.ic_t_inout_store_detail> lst2, List<model.ot_pay_flow> lst3)
        {
            var db = new DB.MySqlByHandClose(Appsetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //提取付款方式
                string sql = "select * from bi_t_payment_info";
                var tb = d.ExecuteToTable(sql, null);
                Dictionary<string, model.bi_t_payment_info> dic_payment = new Dictionary<string, model.bi_t_payment_info>();
                foreach (System.Data.DataRow row in tb.Rows)
                {
                    string pay_way = row["pay_way"].ToString().Trim();
                    dic_payment.Add(pay_way, DB.ReflectionHelper.DataRowToModel<model.bi_t_payment_info>(row));
                }
                //重取单号
                foreach (model.ic_t_inout_store_master item in lst1)
                {
                    string offline_sheet_no = item.sheet_no;
                    item.sheet_no = GetSheetCode(db, "PI");
                    item.offline_sheet_no = offline_sheet_no;
                }
                //
                Dictionary<string, model.ic_t_inout_store_master> dic = new Dictionary<string, model.ic_t_inout_store_master>();
                foreach (model.ic_t_inout_store_master item in lst1)
                {
                    dic.Add(item.offline_sheet_no, item);
                }
                //生成明细行号,单号
                foreach (model.ic_t_inout_store_master item in lst1)
                {
                    item.temp1 = 0;
                }
                foreach (model.ic_t_inout_store_detail item in lst2)
                {
                    model.ic_t_inout_store_master it;
                    if (dic.TryGetValue(item.sheet_no, out it) == true)
                    {
                        it.temp1 += 1;
                        item.sheet_no = it.sheet_no;

                    }
                }
                //生成付款明细行号,单号
                foreach (model.ic_t_inout_store_master item in lst1)
                {
                    item.temp1 = 0;
                }
                foreach (model.ot_pay_flow item in lst3)
                {
                    model.ic_t_inout_store_master it;
                    if (dic.TryGetValue(item.sheet_no, out it) == true)
                    {
                        it.temp1 += 1;
                        item.flow_id = it.temp1;
                        item.sheet_no = it.sheet_no;
                    }
                }

                //

                //写入单头
                foreach (model.ic_t_inout_store_master item in lst1)
                {
                    d.Insert(item);
                }
                //写入单体
                foreach (model.ic_t_inout_store_detail item in lst2)
                {
                    d.Insert(item);
                }
                //写入支付
                foreach (model.ot_pay_flow item in lst3)
                {
                    d.Insert(item);
                }
                //批处理
                foreach (model.ic_t_inout_store_master item in lst1)
                {
                    //审核入库单
                    item.approve_flag = "1";
                    item.approve_man = "1001";
                    item.approve_date = System.DateTime.Now;
                    d.Update(item, "sheet_no", "approve_flag,approve_man,approve_date");
                    //提取付款方式
                    string pay_way = "";
                    decimal pay_amount = 0;
                    string ml_pay_way = "";
                    decimal ml_amount = 0;
                    foreach (model.ot_pay_flow payflow in lst3)
                    {
                        if (item.sheet_no == payflow.sheet_no)
                        {
                            if (payflow.pay_way != "Y")
                            {
                                if (pay_way == "")//除了抹零，只处理一种支付方式 
                                {
                                    pay_way = payflow.pay_way;
                                    pay_amount += payflow.pay_amount;
                                }
                                else
                                {
                                    pay_amount += payflow.pay_amount;
                                }
                            }
                            else
                            {
                                ml_pay_way = "Y";
                                ml_amount += payflow.pay_amount;
                            }
                        }
                    }

                    //写入结算单
                    string settle_sheet_no = "";
                    string visa_id = "";
                    if (pay_way != "")
                    {
                        model.rp_t_recpay_record_info it = new model.rp_t_recpay_record_info();
                        it.sheet_no = GetSheetCode(db, "RP");
                        settle_sheet_no = it.sheet_no;
                        it.supcust_no = item.supcust_no;
                        it.supcust_flag = "S";
                        it.flag_post = "";
                        it.total_amount = pay_amount;
                        if (ml_pay_way != "")
                        {
                            it.free_money = ml_amount;
                        }
                        it.coin_no = "RMB";
                        it.coin_rate = 1;
                        it.pay_way = pay_way;
                        it.approve_flag = "0";
                        it.oper_id = "1001";
                        it.oper_date = System.DateTime.Now;
                        it.deal_man = "";
                        it.approve_man = "";
                        it.approve_date = System.DateTime.MinValue;
                        it.other1 = "";
                        it.other2 = "";
                        it.other3 = "";
                        model.bi_t_payment_info payment;
                        if (dic_payment.TryGetValue(pay_way, out payment) == true)
                        {
                            if (payment.visa_id == null || payment.visa_id == "")
                            {
                                throw new Exception("付款方式" + pay_way + "找不到默认现金银行帐户!");
                            }
                            else
                            {
                                it.visa_id = payment.visa_id;
                            }

                        }
                        else
                        {
                            throw new Exception("付款方式" + pay_way + "找不到默认现金银行帐户!");
                        }

                        visa_id = it.visa_id;
                        it.num1 = 0;
                        it.num2 = 0;
                        it.num3 = 0;
                        it.cm_branch = "00";
                        it.branch_no = item.branch_no;
                        it.from_date = System.DateTime.Now;
                        it.to_date = System.DateTime.Now;
                        it.rc_sheet_no = "";
                        it.pay_memo = "";
                        it.money_date = System.DateTime.Now;
                        d.Insert(it);
                        //
                        model.rp_t_recpay_record_detail line = new model.rp_t_recpay_record_detail();
                        line.sheet_no = it.sheet_no;
                        line.voucher_no = item.sheet_no;
                        line.sheet_amount = item.total_amount;
                        line.paid_amount = 0;
                        line.paid_free = 0;
                        line.pay_amount = pay_amount;
                        line.pay_free = ml_amount;
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
                    //写入收支流水
                    if (pay_way != "")
                    {
                        model.bank_t_cash_master it = new model.bank_t_cash_master();
                        it.sheet_no = GetSheetCode(d, "KZ");
                        it.branch_no = item.branch_no;
                        it.voucher_no = settle_sheet_no;
                        it.visa_id = visa_id;
                        it.visa_in = "";
                        it.pay_way = pay_way;
                        it.coin_no = "RMB";
                        it.coin_rate = 1;
                        it.deal_man = "";
                        it.oper_id = "1001";
                        it.oper_date = System.DateTime.Now;
                        it.bill_total = pay_amount;
                        it.bill_flag = "K";
                        it.cm_branch = "00";
                        it.approve_flag = "0";
                        it.approve_man = "";
                        it.approve_date = System.DateTime.MinValue;
                        it.other1 = "";
                        it.other2 = "";
                        it.other3 = "";
                        it.num1 = 0;
                        it.num2 = 0;
                        it.num3 = 0;
                        d.Insert(it);
                        model.bank_t_cash_detail line = new model.bank_t_cash_detail();
                        line.sheet_no = it.sheet_no;
                        line.type_no = "103";
                        line.bill_cash = pay_amount;
                        line.memo = "";
                        d.Insert(line);
                        //
                        it.approve_date = System.DateTime.Now;
                        it.approve_flag = "1";
                        it.approve_man = "1001";
                        d.Update(it, "sheet_no", "approve_date,approve_flag,approve_man");
                    }

                }


                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("", ex.ToString());
                throw;
            }
            finally
            {
                db.Close();
            }
        }



    }
}
