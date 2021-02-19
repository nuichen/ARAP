using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using Model;
using IvyBack.Helper;
using System.Data;
using Model.BaseModel;
using Model.PaymentModel;

namespace IvyBack.BLL
{
    public class CashierBLL : ICashierBLL
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
         Page<rp_t_bank_beginbalance> ICashierBLL.GetBankCashBeginbalance(string keyword, Page<rp_t_bank_beginbalance> page)
        {
            {
                JsonRequest r = new JsonRequest();

                r.Write("keyword", keyword);
                r.Write<rp_t_bank_beginbalance>(page);

                r.request("/cashier?t=GetBankCashBeginbalance");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                page.Tb = r.GetDataTable();
                page.PageCount = r.ReadToInt("total_count");
                return page;
            }
            throw new NotImplementedException();
        }
        public Page<rp_t_bank_beginbalance> GetBankList(string keyword, Page<rp_t_bank_beginbalance> page)
        {
            {
                JsonRequest r = new JsonRequest();

                r.Write("keyword", keyword);
                r.Write<rp_t_bank_beginbalance>(page);

                r.request("/cashier?t=GetBankList");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                page.Tb = r.GetDataTable();
                page.PageCount = r.ReadToInt("total_count");
                return page;
            }
            throw new NotImplementedException();
        }
        void IBLL.ICashierBLL.SavaBankCashBeginbalance(List<rp_t_bank_beginbalance> lr, string oper_id)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("oper_id", oper_id);
            r.Write("lr", lr);
            r.request("/cashier?t=SavaBankCashBeginbalance");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
        void IBLL.ICashierBLL.DeleteBankCashBeginbalance(string visa_id, DateTime update_time)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("visa_id", visa_id);
            w.Append("update_time", update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=DeleteBankCashBeginbalance", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
        void IBLL.ICashierBLL.CheckBankCashBeginbalance()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
           
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=CheckBankCashBeginbalance", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
        void IBLL.ICashierBLL.NotCheckBankCashBeginbalance()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();

            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=NotCheckBankCashBeginbalance", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        DataTable ICashierBLL.GetCollectionList(string type, string supcust_no, string visa_id, string start_date, string end_date)
        {
            try
            {
                JsonRequest r = new JsonRequest();
                r.Write("type", type);
                r.Write("supcust_no", supcust_no);
                r.Write("visa_id", visa_id);
                r.Write("start_date", start_date);
                r.Write("end_date", end_date);

                r.request("/cashier?t=GetCollectionList");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
                DataTable dt = r.GetDataTable();

                return dt;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CashierBLL.GetCollectionList()", ex.ToString(), null);
                throw ex;
            }
        }

        DataTable ICashierBLL.GetCollectionWayList(string sheet_no)
        {
            try
            {
                JsonRequest r = new JsonRequest();
                r.Write("sheet_no", sheet_no);
               
                r.request("/cashier?t=GetCollectionWayList");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
                DataTable dt = r.GetDataTable();

                return dt;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("CashierBLL.GetCollectionWayList()", ex.ToString(), null);
                throw ex;
            }
        }
        /// <summary>
        /// 保存客户通知单
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="lines"></param>
        /// <param name="sheet_no"></param>
        void IBLL.ICashierBLL.AddCollectionPay(Model.rp_t_pay_info ord, List<Model.rp_t_pay_detail> lines, out string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);
            r.Write("lines", lines);

            r.request("/cashier?t=AddCollectionPay");
            r.WhetherSuccess();
            sheet_no = r.Read("sheet_no");
        }
      

        /// <summary>
        /// 删除客户通知单
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="update_time"></param>
        void IBLL.ICashierBLL.DeleteCollectionPay(string sheet_no, DateTime update_time)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("update_time", update_time.ToString("yyyy-MM-dd HH:mm:ss"));
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=DeleteCollectionPay", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }

        }
        void IBLL.ICashierBLL.ChangeCollectionPay(Model.rp_t_pay_info ord, List<Model.rp_t_pay_detail> lines, out string sheet_no)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("ord", ord);
            r.Write("lines", lines);


            r.request("/cashier?t=ChangeCollectionPay");

            r.WhetherSuccess();
            sheet_no = r.Read("sheet_no");
        }
       
        /// <summary>
        /// 审核用户账期通知
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="approve_man"></param>
        /// <param name="update_time"></param>
        void IBLL.ICashierBLL.CheckCollectionPay(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/cashier?t=CheckCollectionPay");
            r.WhetherSuccess();
        }
        void IBLL.ICashierBLL.NotCheckCollectionPay(string sheet_no, string approve_man, DateTime update_time)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("approve_man", approve_man);
            r.Write("update_time", update_time);

            r.request("/cashier?t=NotCheckCollectionPay");
            r.WhetherSuccess();
        }
        System.Data.DataTable IBLL.ICashierBLL.GetCollectionPayList(DateTime date1, DateTime date2, string is_cs, string visa_id)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("is_cs", is_cs);
            w.Append("visa_id", visa_id);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=GetCollectionPayList", w.ToString());
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
        void IBLL.ICashierBLL.GetCollectionPayOrder(string sheet_no, string is_cs, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            Helper.JsonRequest r = new JsonRequest();
            r.Write("sheet_no", sheet_no);
            r.Write("is_cs", is_cs);
            r.request("/cashier?t=GetCollectionPayOrder");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }

       

        System.Data.DataTable IBLL.ICashierBLL.GetOtherList(DateTime date1, DateTime date2,string is_cs)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("is_cs", is_cs);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=GetOtherList", w.ToString());
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

        void IBLL.ICashierBLL.GetOtherOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);

            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=GetOtherOrder", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {

                tb1 = GetDataTable(r.ReadList("tb1"));
                tb2 = GetDataTable(r.ReadList("tb2"));


            }
        }

       

        void IBLL.ICashierBLL.AddOther(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines, out string sheet_no)
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
            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("type_no");
            tb.Columns.Add("bill_cash", typeof(decimal));
            tb.Columns.Add("memo");


            foreach (Model.bank_t_cash_detail line in lines)
            {
                tb.Rows.Add(
                    line.sheet_no,
                    line.type_no,
                    line.bill_cash,
                    line.memo

                    );

            }
            w.Append("lines", tb);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=AddOther", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            sheet_no = r.Read("sheet_no");
        }

        void IBLL.ICashierBLL.ChangeOther(Model.bank_t_cash_master ord, List<Model.bank_t_cash_detail> lines)
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
            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("type_no");
            tb.Columns.Add("bill_cash", typeof(decimal));
            tb.Columns.Add("memo");


            foreach (Model.bank_t_cash_detail line in lines)
            {
                tb.Rows.Add(
                    line.sheet_no,
                    line.type_no,
                    line.bill_cash,
                    line.memo

                    );

            }
            w.Append("lines", tb);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=ChangeOther", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICashierBLL.DeleteOther(string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=DeleteOther", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICashierBLL.CheckOther(string sheet_no, string approve_man)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("approve_man", approve_man);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=CheckOther", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICashierBLL.NotCheckOther(string sheet_no, string approve_man)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("approve_man", approve_man);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cashier?t=NotCheckOther", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        public System.Data.DataTable GetSZList(int type)
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
                    if (type == 0)
                    {
                        if (row["pay_flag"].ToString() == "4" || row["pay_flag"].ToString() == "0")
                        {
                            tb2.Rows.Add(row.ItemArray);
                        }
                    }
                    else
                    {
                        if (row["pay_flag"].ToString() == "4" || row["pay_flag"].ToString() == "0")
                        {
                            tb2.Rows.Add(row.ItemArray);
                        }
                    }
                }
                return tb2;
            }
        }

        public System.Data.DataTable GetBankList()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("none", "");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/bank?t=get_list", w.ToString());
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

    }
}
