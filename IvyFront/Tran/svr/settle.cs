using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tran.svr
{
    class settle:IServiceBase 
    {

        void IServiceBase.Request(string t, string pars, out string res)
        {
            try
            {
                ReadWriteContext.IReadContext rm = new ReadWriteContext.ReadContextByJson(pars);
                var kv = rm.ToDictionary();
                IBLL.ISettle bll = new BLL.Settle();
                if (t == "upload_fhd")
                {
                    if (CommonHelper.ExistsKeys(kv, "branch_no", "sale_master", "sale_detail", "pay_data") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    var branch_no = rm.Read("branch_no");
                    var json = rm.Read("sale_master");
                    ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                    var lst1 = new List<model.sm_t_salesheet>();
                    if (read.Read("datas") != null)
                    {
                        foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                        {
                            var ord = new model.sm_t_salesheet();
                            ord.sheet_no = r.Read("sheet_no");
                            ord.voucher_no = "";
                            ord.branch_no = r.Read("branch_no");
                            ord.cust_no = r.Read("cust_no");
                            ord.pay_way = r.Read("pay_way");
                            ord.discount = Conv.ToDecimal(r.Read("discount"));
                            ord.coin_no = r.Read("coin_no");
                            ord.real_amount = Conv.ToDecimal(r.Read("total_amount"));
                            ord.total_amount = Conv.ToDecimal(r.Read("total_amount"));
                            ord.paid_amount = Conv.ToDecimal(r.Read("paid_amount"));
                            ord.approve_flag = "0";
                            ord.source_flag = "";
                            ord.oper_id = r.Read("oper_id");
                            ord.sale_man = r.Read("sale_man");
                            ord.oper_date = Conv.ToDateTime(r.Read("oper_date"));
                            ord.pay_date = Conv.ToDateTime(r.Read("pay_date"));
                            ord.other1 = "";
                            ord.other2 = "";
                            ord.other3 = "";
                            ord.cm_branch = "";
                            ord.approve_man = "";
                            ord.approve_date = DateTime.MinValue;
                            ord.num1 = 0m;
                            ord.num2 = 0m;
                            ord.num3 = 0m;
                            ord.payfee_memo = "";
                            ord.old_no = "";
                            ord.psheet_no = "";
                            ord.pay_nowmark = "";
                            ord.if_back = "";
                            ord.cust_cls = "";
                            ord.other4 = "";
                            lst1.Add(ord);
                        }
                    }

                    json = rm.Read("sale_detail");
                    read = new ReadWriteContext.ReadContextByJson(json);
                    var lst2 = new List<model.sm_t_salesheet_detail>();
                    if (read.Read("datas") != null)
                    {
                        foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                        {
                            var item = new model.sm_t_salesheet_detail();
                            item.sheet_no = r.Read("sheet_no");
                            item.item_no = r.Read("item_no");
                            item.item_name = r.Read("item_name");
                            item.unit_no = r.Read("unit_no");
                            item.unit_factor = 1m;
                            item.sale_qnty = Conv.ToDecimal(r.Read("sale_qnty"));
                            item.sale_price = Conv.ToDecimal(r.Read("sale_price"));
                            item.real_price = Conv.ToDecimal(r.Read("real_price"));
                            item.cost_price = Conv.ToDecimal(r.Read("cost_price"));
                            item.sale_money = Conv.ToDecimal(r.Read("sale_money"));
                            item.sale_tax = 0m;
                            item.is_tax = "0";
                            item.other1 = "";
                            item.other2 = "";
                            item.other3 = "";
                            item.other4 = "";
                            item.num1 = 0m;
                            item.num2 = 0m;
                            item.num3 = 0m;
                            item.num4 = 0m;
                            item.num5 = 0m;
                            item.num6 = 0m;
                            item.barcode = r.Read("barcode");
                            item.sheet_sort = Conv.ToInt(r.Read("sheet_sort"));
                            item.ret_qnty = 0m;
                            item.discount = 0m;
                            item.voucher_no = "";
                            item.cost_notax = 0m;
                            item.packqty = 0;
                            item.sgqty = 0m;
                            item.branch_no_d = "";
                            item.ly_sup_no = "";
                            item.ly_rate = 0m;
                            item.num7 = 0m;
                            item.other5 = "";
                            item.num8 = 0m;
                            item.produce_day = DateTime.MinValue;
                            lst2.Add(item);
                        }
                    }
                    json = rm.Read("pay_data");
                    read = new ReadWriteContext.ReadContextByJson(json);
                    var lst3 = new List<model.ot_pay_flow>();
                    if (read.Read("datas") != null)
                    {
                        foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                        {
                            var item = new model.ot_pay_flow();
                            item.sheet_no = r.Read("sheet_no");
                            item.flow_id = Conv.ToInt(r.Read("flow_id"));
                            item.cus_no = r.Read("cus_no");
                            item.oper_id = r.Read("oper_id");
                            item.oper_date = Conv.ToDateTime(r.Read("oper_date"));
                            item.pay_way = r.Read("pay_way");
                            item.sale_amount = Conv.ToDecimal(r.Read("sale_amount"));
                            item.pay_amount = Conv.ToDecimal(r.Read("pay_amount"));
                            item.old_amount = Conv.ToDecimal(r.Read("old_amount"));
                            item.ml = Conv.ToDecimal(r.Read("ml"));
                            item.jh = r.Read("jh");
                            item.remark = r.Read("remark");
                            lst3.Add(item);
                        }
                    }
                    if (lst1.Count > 0 || lst2.Count > 0)
                    {
                        bll.WriteFHD(lst1, lst2, lst3);
                    }
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    res = w.ToString();
                }
                else if (t == "upload_cgrk")
                {
                    if (CommonHelper.ExistsKeys(kv, "branch_no", "inout_master", "inout_detail", "pay_data") == false)
                    {
                        throw new Exception("参数错误");
                    }
                    var branch_no = rm.Read("branch_no");
                    var json = rm.Read("inout_master");
                    ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                    var lst1 = new List<model.ic_t_inout_store_master>();
                    if (read.Read("datas") != null)
                    {
                        foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                        {
                            var ord = new model.ic_t_inout_store_master();
                            ord.sheet_no = r.Read("sheet_no");
                            ord.trans_no = r.Read("trans_no");//A采购入库; D销售退货入库
                            ord.db_no = "+";
                            ord.branch_no = r.Read("branch_no");
                            ord.d_branch_no = "";
                            ord.voucher_no = "";
                            ord.supcust_no = r.Read("supcust_no");
                            ord.total_amount = Conv.ToDecimal(r.Read("total_amount"));
                            ord.inout_amount = Conv.ToDecimal(r.Read("total_amount"));
                            ord.coin_no = "RMB";
                            ord.pay_way = r.Read("pay_way");
                            ord.tax_amount = 0m;
                            ord.discount = 0m;
                            ord.pay_date = DateTime.MinValue;
                            ord.approve_flag = "0";
                            ord.oper_date = Conv.ToDateTime(r.Read("oper_date"));
                            ord.oper_id = r.Read("oper_id");
                            ord.display_flag = "1";
                            ord.other1 = "";
                            ord.other2 = "";
                            ord.other3 = "";
                            ord.cm_branch = ""; //不确定
                            ord.deal_man = "";
                            ord.old_no = "";
                            ord.approve_man = "";
                            ord.approve_date = DateTime.MinValue;
                            ord.num1 = 0m;
                            ord.num2 = 0m;
                            ord.num3 = 0m;
                            ord.max_change = 0m; //不确定
                            ord.sale_no = "A";
                            ord.lock_man = "";
                            ord.lock_date = DateTime.MinValue;
                            ord.if_promote = "";
                            lst1.Add(ord);
                        }
                    }

                    json = rm.Read("inout_detail");
                    read = new ReadWriteContext.ReadContextByJson(json);
                    var lst2 = new List<model.ic_t_inout_store_detail>();
                    if (read.Read("datas") != null)
                    {
                        foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                        {
                            var item = new model.ic_t_inout_store_detail();
                            item.sheet_no = r.Read("sheet_no");
                            item.item_no = r.Read("item_no");
                            item.item_name = r.Read("item_name");
                            item.unit_no = r.Read("unit_no");
                            item.unit_factor = 1m;
                            item.in_qty = Conv.ToDecimal(r.Read("in_qty"));
                            item.orgi_price = Conv.ToDecimal(r.Read("orgi_price"));
                            item.valid_price = Conv.ToDecimal(r.Read("valid_price"));
                            item.cost_price = Conv.ToDecimal(r.Read("cost_price"));
                            item.sub_amount = 0m;
                            item.tax = 0m;
                            item.is_tax = "0";
                            item.valid_date = Conv.ToDateTime(r.Read("valid_date"));
                            item.other1 = "";
                            item.other2 = "";
                            item.other3 = "";
                            item.num1 = 0m;
                            item.num2 = 0m;
                            item.num3 = 0m;
                            item.num4 = 0m;
                            item.num5 = 0m;
                            item.num6 = 0m;
                            item.barcode = r.Read("barcode");
                            item.sheet_sort = Conv.ToInt(r.Read("sheet_sort"));
                            item.ret_qnty = 0m;
                            item.discount = 1m;
                            item.voucher_no = "";
                            item.cost_notax = 0m; //不确定
                            item.packqty = 0;
                            item.sgqty = 0m;
                            item.branch_no_d = "";
                            item.ly_sup_no = "";
                            item.ly_rate = 0m;
                            lst2.Add(item);
                        }
                    }
                    json = rm.Read("pay_data");
                    read = new ReadWriteContext.ReadContextByJson(json);
                    var lst3 = new List<model.ot_pay_flow>();
                    if (read.Read("datas") != null)
                    {
                        foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                        {
                            var item = new model.ot_pay_flow();
                            item.sheet_no = r.Read("sheet_no");
                            item.flow_id = Conv.ToInt(r.Read("flow_id"));
                            item.cus_no = r.Read("cus_no");
                            item.oper_id = r.Read("oper_id");
                            item.oper_date = Conv.ToDateTime(r.Read("oper_date"));
                            item.pay_way = r.Read("pay_way");
                            item.sale_amount = Conv.ToDecimal(r.Read("sale_amount"));
                            item.pay_amount = Conv.ToDecimal(r.Read("pay_amount"));
                            item.old_amount = Conv.ToDecimal(r.Read("old_amount"));
                            item.ml = Conv.ToDecimal(r.Read("ml"));
                            item.jh = r.Read("jh");
                            item.remark = r.Read("remark");
                            lst3.Add(item);
                        }
                    }
                    if (lst1.Count > 0 || lst2.Count > 0)
                    {
                        bll.WriteCGRK(lst1, lst2, lst3);
                    }
                    ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                    w.Append("errId", "0");
                    w.Append("errMsg", "");
                    res = w.ToString();
                }
                else
                {
                    throw new Exception("未找到方法:" + t);
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("settle()", ex.ToString(), t, pars);
                ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
                w.Append("errId", "-1");
                w.Append("errMsg", ex.Message);
                res = w.ToString();
            }
        }

    }
}
