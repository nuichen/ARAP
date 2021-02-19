using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class salorder : BaseService
    {
        ISaleOrderBLL bll = new SaleOrder();
        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }

        public void get_newsheetno(WebHelper w, Dictionary<string, object> kv)
        {
            var sheet_no = bll.GetNewSheetNo();
            w.Write("sheet_no", sheet_no);
        }
        public void get_rowid(WebHelper w, Dictionary<string, object> kv)
        {
            var rowId = bll.GetRowId();
            w.Write("rowId", rowId);
        }
        public void get_suplist(WebHelper w, Dictionary<string, object> kv)
        {
            var dt = bll.GetSupList();
            w.Write("datas", dt);
        }
        public void get_operlist(WebHelper w, Dictionary<string, object> kv)
        {
            var dt = bll.GetOperList();
            w.Write("datas", dt);
        }
        public void get_itemlist(WebHelper w, Dictionary<string, object> kv)
        {
            if (w.ExistsKeys("item_name", "barcode") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            var item_name = w.Read("item_name");
            var barcode = w.Read("barcode");
            var dt = bll.GetItemList(item_name, barcode);
            w.Write("datas", dt);
        }
        public void add_orders(WebHelper w, Dictionary<string, object> kv)
        {
            if (w.ExistsKeys("branch_no", "jh", "inout_master", "inout_detail") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            var branch_no = ObjectToString(kv, "branch_no");
            var jh = ObjectToString(kv, "jh");
            string json = ObjectToString(kv, "inout_master");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            var ord = new co_t_order_main();
            if (read.Read("sheet_no") != null)
            {
                ord.sheet_no = read.Read("sheet_no");
                ord.p_sheet_no = read.Read("p_sheet_no");
                ord.sup_no = read.Read("sup_no");
                ord.order_man = read.Read("order_man");
                ord.oper_id = read.Read("oper_id");
                ord.valid_date = Conv.ToDateTime(read.Read("valid_date"));
                ord.oper_date = Conv.ToDateTime(read.Read("oper_date"));
                ord.total_amount = Conv.ToDecimal(read.Read("total_amount"));
                ord.paid_amount = Conv.ToDecimal(read.Read("paid_amount"));
                ord.trans_no = read.Read("trans_no");//A采购入库; D销售退货入库
                ord.order_status = read.Read("order_status");
                ord.approve_flag = read.Read("approve_flag");
                ord.agree_inhand = read.Read("agree_inhand");
                ord.coin_code = read.Read("coin_code");
                ord.branch_no = read.Read("branch_no");
                ord.sale_way = read.Read("sale_way");
                ord.memo = read.Read("memo");
                ord.other1 = read.Read("other1");
                ord.other2 = jh;
                ord.cm_branch = ""; //不确定
                ord.approve_man = "";
                ord.approve_date = DateTime.MinValue;
                ord.num1 = 0m;
                ord.num2 = 0m;
                ord.num3 = 0m;
                ord.ask_no = read.Read("ask_no");
                ord.max_change = Conv.ToDecimal(read.Read("max_change"));
                ord.co_sheetno = read.Read("co_sheetno");
                ord.if_promote = read.Read("if_promote");
                ord.update_time = DateTime.MinValue;
            }
            json = ObjectToString(kv, "inout_detail");
            read = new ReadWriteContext.ReadContextByJson(json);
            var lst2 = new List<co_t_order_child>();
            if (read.Read("datas") != null)
            {
                foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                {
                    var item = new co_t_order_child();
                    item.sheet_no = r.Read("sheet_no");
                    item.item_no = r.Read("item_no");
                    item.unit_no = r.Read("unit_no");
                    item.unit_factor = 1m;
                    item.in_price = Conv.ToDecimal(r.Read("in_pric"));
                    item.order_qnty = Conv.ToDecimal(r.Read("order_qnty"));
                    item.sub_amount = item.in_price * item.order_qnty;
                    item.real_qty = Conv.ToDecimal(r.Read("real_qty"));
                    item.tax_rate = Conv.ToDecimal(r.Read("tax_rate"));
                    item.pay_percent = Conv.ToDecimal(r.Read("pay_percent"));
                    item.other1 = "";
                    item.other2 = r.Read("other2");//机号
                    item.other3 = r.Read("other3");//是否赠送
                    item.num1 = 0m;
                    item.num2 = 0m;
                    item.num3 = 0m;
                    item.barcode = r.Read("barcode");
                    item.sheet_sort = Conv.ToInt(r.Read("sheet_sort"));
                    item.discount = 1m;
                    item.voucher_no = "";
                    item.out_qty = Conv.ToDecimal(r.Read("out_qty"));
                    item.packqty = 0;
                    item.sgqty = 0m;
                    item.ordmemo = r.Read("ordmemo");
                    item.month_sale = 0m;
                    item.specin_flag = "0";
                    lst2.Add(item);
                }
            }

            bll.InsertOrderMainAndChilds(ord, lst2);
        }
        public void get_ordermain_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "dtStart", "dtEnd") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            var dtStart = ObjectToDate(kv, "dtStart");
            var dtEnd = ObjectToDate(kv, "dtEnd");
            var dt = bll.GetOrderMainList(dtStart, dtEnd);
            w.Write("datas", dt);
        }
        public void get_orderchild_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "sheet_no") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            var sheet_no = ObjectToString(kv, "sheet_no");
            var dt = bll.GetOrderChildList(sheet_no);
            w.Write("datas", dt);
        }

    }
}