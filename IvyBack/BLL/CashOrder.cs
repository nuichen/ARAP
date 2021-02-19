using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyBack.BLL
{
    class CashOrder : IBLL.ICashOrder
    {

        public System.Data.DataTable GetSZList()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("is_show_stop", "0");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/finance?t=get_sz_list", w.ToString());
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
                var tb2 = tb.Clone();
                foreach (DataRow row in tb.Rows)
                {
                    if (row["pay_flag"].ToString() == "3")
                    {
                        tb2.Rows.Add(row.ItemArray);
                    }
                }
                return tb2;
            }
        }

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

        System.Data.DataTable IBLL.ICashOrder.GetList(DateTime date1, DateTime date2, string visa_id)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("visa_id", visa_id);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cash_order?t=get_list", w.ToString());
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

        void IBLL.ICashOrder.GetOrder(string sheet_no, out System.Data.DataTable tb1)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);

            r.request("/cash_order?t=get_order");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");

        }

        string IBLL.ICashOrder.MaxCode()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("none", "");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cash_order?t=max_code", w.ToString());
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

        void IBLL.ICashOrder.Add(Model.bank_t_cash_master ord, out string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("branch_no", ord.branch_no);
            w.Append("voucher_no", ord.voucher_no);
            w.Append("visa_id", ord.visa_id);
            w.Append("visa_in", ord.visa_in);
            w.Append("pay_way", ord.pay_way);
            w.Append("coin_no", ord.coin_no);
            w.Append("coin_rate", ord.coin_rate.ToString());
            w.Append("deal_man", ord.deal_man);
            w.Append("oper_id", ord.oper_id);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd"));
            w.Append("bill_total", ord.bill_total.ToString());
            w.Append("bill_flag", ord.bill_flag);
            w.Append("cm_branch", ord.cm_branch);
            w.Append("approve_flag", "0");
            w.Append("approve_man", "");
            w.Append("approve_date", "");
            w.Append("other1", ord.other1);
            w.Append("other2", ord.other2);
            w.Append("other3", ord.other3);
            w.Append("num1", ord.num1.ToString());
            w.Append("num2", ord.num2.ToString());
            w.Append("num3", ord.num3.ToString());

            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cash_order?t=add", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            sheet_no = r.Read("sheet_no");
        }

        void IBLL.ICashOrder.Change(Model.bank_t_cash_master ord)
        {

            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("branch_no", ord.branch_no);
            w.Append("voucher_no", ord.voucher_no);
            w.Append("visa_id", ord.visa_id);
            w.Append("visa_in", ord.visa_in);
            w.Append("pay_way", ord.pay_way);
            w.Append("coin_no", ord.coin_no);
            w.Append("coin_rate", ord.coin_rate.ToString());
            w.Append("deal_man", ord.deal_man);
            w.Append("oper_id", ord.oper_id);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd"));
            w.Append("bill_total", ord.bill_total.ToString());
            w.Append("bill_flag", ord.bill_flag);
            w.Append("cm_branch", ord.cm_branch);
            w.Append("approve_flag", "0");
            w.Append("approve_man", "");
            w.Append("approve_date", "");
            w.Append("other1", ord.other1);
            w.Append("other2", ord.other2);
            w.Append("other3", ord.other3);
            w.Append("num1", ord.num1.ToString());
            w.Append("num2", ord.num2.ToString());
            w.Append("num3", ord.num3.ToString());

            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cash_order?t=change", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICashOrder.Delete(string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cash_order?t=delete", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICashOrder.Check(string sheet_no, string approve_man)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("approve_man", approve_man);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cash_order?t=check", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
        void IBLL.ICashOrder.NotCheck(string sheet_no, string approve_man)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("approve_man", approve_man);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cash_order?t=NotCheck", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
    }
}
