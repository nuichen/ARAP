using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IvyBack.Helper;
using Model;
using Model.PaymentModel;

namespace IvyBack.BLL
{
    class CusSettle : IBLL.ICusSettle
    {
        public DataTable GetDataTable(List<ReadWriteContext.IReadContext> lst)
        {

            DataTable dt = new DataTable();

            foreach (ReadWriteContext.IReadContext r in lst)
            {
                Dictionary<string, object> dic = r.ToDictionary();

                if (dt.Columns.Count < 1)
                {
                    foreach (string key in dic.Keys)
                    {
                        dt.Columns.Add(key);
                    }
                }

                DataRow dr = dt.NewRow();
                foreach (string key in dic.Keys)
                {
                    int i = dt.Columns.IndexOf(key);
                    dr[i] = dic[key];
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        System.Data.DataTable IBLL.ICusSettle.GetList(DateTime date1, DateTime date2, string cus_no,string is_cs)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("cus_no", cus_no);
            w.Append("is_cs", is_cs);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_settle?t=get_list", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("data").Length < 10)
                {
                    return new DataTable();
                }
                var tb = GetDataTable(r.ReadList("data"));

                return tb;
            }
        }

        DataTable IBLL.ICusSettle.GetFYList(string supcust_no, string supcust_flag)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("supcust_no", supcust_no);
            w.Append("supcust_flag", supcust_flag);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_settle?t=GetFYList", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("data").Length < 10)
                {
                    return new DataTable();
                }
                var tb = GetDataTable(r.ReadList("data"));

                return tb;
            }
        }

        void IBLL.ICusSettle.GetOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("is_cs", is_cs);
            r.request("/cus_settle?t=get_order");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }

        string IBLL.ICusSettle.MaxCode()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("none", "");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/sup_settle?t=max_code", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                return r.Read("code");
            }
        }

        void IBLL.ICusSettle.Add(Model.rp_t_recpay_record_info ord, List<Model.rp_t_recpay_record_detail> lines, DataTable dt, string is_cs, out string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("supcust_no", ord.supcust_no);
            w.Append("supcust_flag", ord.supcust_flag);
            w.Append("flag_post", ord.flag_post);
            w.Append("total_amount", ord.total_amount.ToString());
            w.Append("free_money", ord.free_money.ToString());
            w.Append("coin_no", ord.coin_no);
            w.Append("coin_rate", ord.coin_rate.ToString());
            w.Append("pay_way", ord.pay_way);
            w.Append("approve_flag", ord.approve_flag);
            w.Append("oper_id", ord.oper_id);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Append("deal_man", ord.deal_man);
            w.Append("approve_man", ord.approve_man);
            w.Append("approve_date", "");
            w.Append("other1", ord.other1);
            w.Append("other2", ord.other2);
            w.Append("other3", ord.other3);
            w.Append("visa_id", ord.visa_id);
            w.Append("num1", ord.num1.ToString());
            w.Append("num2", ord.num2.ToString());
            w.Append("num3", ord.num3.ToString());
            w.Append("cm_branch", ord.cm_branch);
            w.Append("branch_no", ord.branch_no);
            w.Append("from_date", "");
            w.Append("to_date", "");
            w.Append("rc_sheet_no", ord.rc_sheet_no);
            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("voucher_no");
            tb.Columns.Add("sheet_amount", typeof(decimal));
            tb.Columns.Add("paid_amount", typeof(decimal));
            tb.Columns.Add("paid_free", typeof(decimal));
            tb.Columns.Add("pay_amount", typeof(decimal));
            tb.Columns.Add("pay_free", typeof(decimal));
            tb.Columns.Add("memo");
            tb.Columns.Add("other1");
            tb.Columns.Add("other2");
            tb.Columns.Add("other3");
            tb.Columns.Add("num1");
            tb.Columns.Add("num2");
            tb.Columns.Add("num3");
            tb.Columns.Add("pay_date", typeof(DateTime));
            tb.Columns.Add("item_no");
            tb.Columns.Add("path");
            tb.Columns.Add("select_flag");
            tb.Columns.Add("voucher_type");
            tb.Columns.Add("oper_date", typeof(DateTime));
            tb.Columns.Add("voucher_other1");
            tb.Columns.Add("voucher_other2");
            tb.Columns.Add("order_no");
            var tb1 = new DataTable();
            tb1.Columns.Add("sheet_no");
            tb1.Columns.Add("collection_type");
            tb1.Columns.Add("collection_amount", typeof(decimal));
            //          public string sheet_no { get; set; }
            //public string collection_type { get; set; }
            //public decimal collection_amount { get; set; }
            foreach (Model.rp_t_recpay_record_detail line in lines)
            {
                tb.Rows.Add(
                    line.sheet_no,
                    line.voucher_no,
                    line.sheet_amount,
                    line.paid_amount,
                    line.paid_free,
                    line.pay_amount,
                    line.pay_free,
                    line.memo,
                    line.other1,
                    line.other2,
                    line.other3,
                    line.num1,
                    line.num2,
                    line.num3,
                    line.pay_date,
                    line.item_no,
                    line.path,
                    line.select_flag,
                    line.voucher_type,
                    line.oper_date,
                    line.voucher_other1,
                    line.voucher_other2,
                    line.order_no

                    );
            }
            w.Append("lines", tb);
            w.Append("dt", dt);
            w.Append("is_cs", is_cs);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_settle?t=add", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
           
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            sheet_no = r.Read("sheet_no");
        }

        void IBLL.ICusSettle.Change(Model.rp_t_recpay_record_info ord, List<Model.rp_t_recpay_record_detail> lines, DataTable dt)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("supcust_no", ord.supcust_no);
            w.Append("supcust_flag", ord.supcust_flag);
            w.Append("flag_post", ord.flag_post);
            w.Append("total_amount", ord.total_amount.ToString());
            w.Append("free_money", ord.free_money.ToString());
            w.Append("coin_no", ord.coin_no);
            w.Append("coin_rate", ord.coin_rate.ToString());
            w.Append("pay_way", ord.pay_way);
            w.Append("approve_flag", ord.approve_flag);
            w.Append("oper_id", ord.oper_id);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Append("deal_man", ord.deal_man);
            w.Append("approve_man", ord.approve_man);
            w.Append("approve_date", "");
            w.Append("other1", ord.other1);
            w.Append("other2", ord.other2);
            w.Append("other3", ord.other3);
            w.Append("visa_id", ord.visa_id);
            w.Append("num1", ord.num1.ToString());
            w.Append("num2", ord.num2.ToString());
            w.Append("num3", ord.num3.ToString());
            w.Append("cm_branch", ord.cm_branch);
            w.Append("branch_no", ord.branch_no);
            w.Append("from_date", "");
            w.Append("to_date", "");
            w.Append("rc_sheet_no", ord.rc_sheet_no);
            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("voucher_no");
            tb.Columns.Add("sheet_amount", typeof(decimal));
            tb.Columns.Add("paid_amount", typeof(decimal));
            tb.Columns.Add("paid_free", typeof(decimal));
            tb.Columns.Add("pay_amount", typeof(decimal));
            tb.Columns.Add("pay_free", typeof(decimal));
            tb.Columns.Add("memo");
            tb.Columns.Add("other1");
            tb.Columns.Add("other2");
            tb.Columns.Add("other3");
            tb.Columns.Add("num1");
            tb.Columns.Add("num2");
            tb.Columns.Add("num3");
            tb.Columns.Add("pay_date", typeof(DateTime));
            tb.Columns.Add("item_no");
            tb.Columns.Add("path");
            tb.Columns.Add("select_flag");
            tb.Columns.Add("voucher_type");
            tb.Columns.Add("oper_date", typeof(DateTime));
            tb.Columns.Add("voucher_other1");
            tb.Columns.Add("voucher_other2");
            tb.Columns.Add("order_no");

            foreach (Model.rp_t_recpay_record_detail line in lines)
            {
                tb.Rows.Add(
                    line.sheet_no,
                    line.voucher_no,
                    line.sheet_amount,
                    line.paid_amount,
                    line.paid_free,
                    line.pay_amount,
                    line.pay_free,
                    line.memo,
                    line.other1,
                    line.other2,
                    line.other3,
                    line.num1,
                    line.num2,
                    line.num3,
                    line.pay_date,
                    line.item_no,
                    line.path,
                    line.select_flag,
                    line.voucher_type,
                    line.oper_date,
                    line.voucher_other1,
                    line.voucher_other2,
                    line.order_no

                    );
            }
            w.Append("lines", tb);
            w.Append("dt", dt);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_settle?t=change", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICusSettle.Delete(string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_settle?t=delete", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICusSettle.Check(string sheet_no, string approve_man, string is_cs)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("approve_man", approve_man);
            w.Append("is_cs", is_cs);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_settle?t=check", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
        void IBLL.ICusSettle.NotCheck(string sheet_no, string approve_man, string is_cs)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("approve_man", approve_man);
            w.Append("is_cs", is_cs);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_settle?t=not_check", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        public DataTable GetAccountFlows(Model.rp_t_accout_payrec_flow flow)
        {
            JsonRequest r = new JsonRequest();

            r.Write<rp_t_accout_payrec_flow>("flow", flow);

            r.request("/cus_settle?t=GetAccountFlows");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

    }
}
