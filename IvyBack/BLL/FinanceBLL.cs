using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IvyBack.IBLL;
using Model;

namespace IvyBack.BLL
{
    public class FinanceBLL : IFinanceBLL
    {
        DataTable IFinanceBLL.GetSZTypeList()
        {
            try
            {
                Helper.IRequest req = new Helper.Request();

                var json = req.request("/finance?t=get_sz_list", "{\"is_show_stop\":\"1\"}");
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("pay_way", typeof(string));
                dt.Columns.Add("pay_name", typeof(string));
                dt.Columns.Add("pay_flag", typeof(string));
                dt.Columns.Add("km_code", typeof(string));
                dt.Columns.Add("pay_kind", typeof(string));
                dt.Columns.Add("other1", typeof(string));
                dt.Columns.Add("other2", typeof(string));
                dt.Columns.Add("num1", typeof(decimal));
                dt.Columns.Add("num2", typeof(int));
                dt.Columns.Add("pay_memo", typeof(string));
                dt.Columns.Add("if_acc", typeof(string));
                dt.Columns.Add("path", typeof(string));
                dt.Columns.Add("is_account", typeof(string));
                dt.Columns.Add("account_flag", typeof(string));
                dt.Columns.Add("is_pay", typeof(string));
                dt.Columns.Add("is_profit", typeof(string));
                dt.Columns.Add("profit_type", typeof(string));
                dt.Columns.Add("auto_cashsheet", typeof(string));
                dt.Columns.Add("if_CtFix", typeof(string));
                if (read.Read("data") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                    {
                        Model.bi_t_sz_type item = new Model.bi_t_sz_type();
                        item.pay_way = r.Read("pay_way");
                        item.pay_name = r.Read("pay_name");
                        item.pay_flag = r.Read("pay_flag");
                        item.km_code = r.Read("km_code");
                        item.pay_kind = r.Read("pay_kind");
                        item.other1 = r.Read("other1");
                        item.other2 = r.Read("subject_name");
                        item.num1 = Helper.Conv.ToDecimal(r.Read("num1"));
                        item.num2 = Helper.Conv.ToInt(r.Read("num2"));
                        item.pay_memo = r.Read("pay_memo");
                        item.if_acc = r.Read("if_acc");
                        item.path = r.Read("path");
                        item.is_account = r.Read("is_account");
                        item.account_flag = r.Read("account_flag");
                        item.is_pay = r.Read("is_pay");
                        item.is_profit = r.Read("is_profit");
                        item.profit_type = r.Read("profit_type");
                        item.auto_cashsheet = r.Read("auto_cashsheet");
                        item.if_CtFix = r.Read("if_CtFix");

                        dt.Rows.Add(item.pay_way, item.pay_name, item.pay_flag, item.km_code, item.pay_kind, item.other1, item.other2, item.num1
                            , item.num2, item.pay_memo, item.if_acc, item.path, item.is_account, item.account_flag, item.is_pay, item.is_profit
                            , item.profit_type, item.auto_cashsheet, item.if_CtFix);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("FinanceBLL.GetSZTypeList()", ex.ToString(), null);
                throw ex;
            }
        }

        Model.bi_t_sz_type IFinanceBLL.GetSZTypeItem(string pay_way)
        {
            try
            {
                Helper.IRequest req = new Helper.Request();

                var json = req.request("/finance?t=get_sz_item", "{\"pay_way\":\"" + pay_way + "\"}");
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                if (read.Read("data") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("data"))
                    {
                        Model.bi_t_sz_type item = new Model.bi_t_sz_type();
                        item.pay_way = r.Read("pay_way");
                        item.pay_name = r.Read("pay_name");
                        item.pay_flag = r.Read("pay_flag");
                        item.km_code = r.Read("km_code");
                        item.pay_kind = r.Read("pay_kind");
                        item.other1 = r.Read("other1");
                        item.other2 = r.Read("other2");
                        item.num1 = Helper.Conv.ToDecimal(r.Read("num1"));
                        item.num2 = Helper.Conv.ToInt(r.Read("num2"));
                        item.pay_memo = r.Read("pay_memo");
                        item.if_acc = r.Read("if_acc");
                        item.path = r.Read("path");
                        item.is_account = r.Read("is_account");
                        item.account_flag = r.Read("account_flag");
                        item.is_pay = r.Read("is_pay");
                        item.is_profit = r.Read("is_profit");
                        item.profit_type = r.Read("profit_type");
                        item.auto_cashsheet = r.Read("auto_cashsheet");
                        item.if_CtFix = r.Read("if_CtFix");
                        
                        return item;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("FinanceBLL.GetSZTypeItem()", ex.ToString(), null);
                throw ex;
            }
        }

        void IFinanceBLL.InsertSZType(Model.bi_t_sz_type item)
        {
            try
            {
                Helper.IRequest req = new Helper.Request();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("pay_way", item.pay_way);
                write.Append("pay_name", item.pay_name);
                write.Append("pay_flag", item.pay_flag);
                write.Append("km_code", item.km_code);
                write.Append("pay_kind", item.pay_kind);
                write.Append("other1", item.other1);
                write.Append("other2", item.other2);
                write.Append("num1", item.num1.ToString());
                write.Append("num2", item.num2.ToString());
                write.Append("pay_memo", item.pay_memo);
                write.Append("if_acc", item.if_acc);
                write.Append("path", item.path);
                write.Append("is_account", item.is_account);
                write.Append("account_flag", item.account_flag);
                write.Append("is_pay", item.is_pay);
                write.Append("is_profit", item.is_profit);
                write.Append("profit_type", item.profit_type);
                write.Append("auto_cashsheet", item.auto_cashsheet);
                write.Append("if_CtFix", item.if_CtFix);
                var json = req.request("/finance?t=add_sz", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("FinanceBLL.InsertSZType()", ex.ToString(), null);
                throw ex;
            }
        }

        void IFinanceBLL.UpdateSZType(Model.bi_t_sz_type item)
        {
            try
            {
                Helper.IRequest req = new Helper.Request();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("pay_way", item.pay_way);
                write.Append("pay_name", item.pay_name);
                write.Append("pay_flag", item.pay_flag);
                write.Append("km_code", item.km_code);
                write.Append("pay_kind", item.pay_kind);
                write.Append("other1", item.other1);
                write.Append("other2", item.other2);
                write.Append("num1", item.num1.ToString());
                write.Append("num2", item.num2.ToString());
                write.Append("pay_memo", item.pay_memo);
                write.Append("if_acc", item.if_acc);
                write.Append("path", item.path);
                write.Append("is_account", item.is_account);
                write.Append("account_flag", item.account_flag);
                write.Append("is_pay", item.is_pay);
                write.Append("is_profit", item.is_profit);
                write.Append("profit_type", item.profit_type);
                write.Append("auto_cashsheet", item.auto_cashsheet);
                write.Append("if_CtFix", item.if_CtFix);
                var json = req.request("/finance?t=change_sz", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("FinanceBLL.UpdateSZType()", ex.ToString(), null);
                throw ex;
            }
        }

        void IFinanceBLL.DeleteSZType(string pay_way)
        {
            try
            {
                Helper.IRequest req = new Helper.Request();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("pay_way", pay_way);
                var json = req.request("/finance?t=delete_sz", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("FinanceBLL.DeleteSZType()", ex.ToString(), pay_way);
                throw ex;
            }
        }

        DataTable IFinanceBLL.GetIncomeRevenueList(string supcust_no, string month)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("supcust_no", supcust_no);
            w.Append("month", month);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/finance?t=get_income_list", w.ToString());
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
                IBLL.ICommonBLL bll = new BLL.CommonBLL();
                var tb = bll.GetDataTable(r.ReadList("data"));

                return tb;
            }
        }

        void IFinanceBLL.SaveIncomeRevenue(string supcust_no, string month, string oper_id, List<rp_t_supcust_income_revenue> details)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("supcust_no", supcust_no);
            r.Write("month", month);
            r.Write("oper_id", oper_id);
            r.Write("details", details);
            r.request("/finance?t=SaveIncomeRevenue");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }



    }
}
