using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using IvyBack.Helper;
using System.Data;
using System.Drawing;

namespace IvyBack.BLL
{
    public class ReportBLL : IReport
    {

        #region 应收应付
        public Page<object> GetCusContactDetails(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            page.PageSize = int.MaxValue;
            r.Write<object>(page);

            r.request("/report?t=GetCusContactDetails");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetCusBalance(DateTime start_time, DateTime end_time,string cust_from, string company_type,bool isnull, Page<object> page)
        {
            JsonRequest r = new JsonRequest();
            if (DateTime.Compare(start_time,end_time)>0)
            {
                throw new Exception("开始日期不能大于结束日期");
            }
            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("cust_from", cust_from);
            r.Write("company_type", company_type);
            r.Write("isnull", Convert.ToInt32(isnull));
            page.PageSize = int.MaxValue;
            r.Write<object>(page);

            r.request("/report?t=GetCusBalance");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetSupContactDetails(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            page.PageSize = int.MaxValue;
            r.Write<object>(page);

            r.request("/report?t=GetSupContactDetails");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetSupBalance(DateTime start_time, DateTime end_time, string cust_from, string company_type, bool isnull, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("cust_from", cust_from);
            r.Write("company_type", company_type);
            r.Write("isnull", Convert.ToInt32(isnull));
            page.PageSize = int.MaxValue;
            r.Write<object>(page);

            r.request("/report?t=GetSupBalance");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetCusAgingGroup(DateTime start_time, string cust_from, string company_type, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("cust_from", cust_from);
            r.Write("company_type", company_type);
            page.PageSize = int.MaxValue;
            r.Write<object>(page);

            r.request("/report?t=GetCusAgingGroup");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetSupAgingGroup(DateTime start_time, string cust_from , string company_type, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("cust_from", cust_from);
            r.Write("company_type", company_type);
            page.PageSize = int.MaxValue;
            r.Write<object>(page);

            r.request("/report?t=GetSupAgingGroup");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        #endregion

            #region 出纳管理

            public Page<object> GetBankCashDetailed(DateTime start_time, DateTime end_time, string visa_id, Page<object> page)
            {
                JsonRequest r = new JsonRequest();

                r.Write("start_time", start_time);
                r.Write("end_time", end_time);
                r.Write("visa_id", visa_id);
                page.PageSize = int.MaxValue;
                r.Write<object>(page);

                r.request("/report?t=GetBankCashDetailed");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                page = r.ReadPage(page);
            //DataTable dt = page.Tb;
            page.Tb.Columns.Add("row_color",typeof(Color));
            //for (int i = 0; i < page.Tb.Rows.Count; i++)
            //{
            //    if (Conv.ToString(page.Tb.Rows[i]["方向"]) == "贷")
            //        page.Tb.Rows[i]["row_color"] = Color.Red;
            //    else
            //        page.Tb.Rows[i]["row_color"] = Color.White;

            //}
            foreach (DataRow rr in page.Tb.Rows)
            {
                if (Conv.ToString(rr["方向"]) == "贷")
                {
                    rr["row_color"] = Color.Red;
                }
            }
            return page;
            }
            public Page<object> GetBankCashBalance(DateTime start_time, DateTime end_time, string visa_id1, Page<object> page)
            {
                JsonRequest r = new JsonRequest();

                r.Write("start_time", start_time);
                r.Write("end_time", end_time);
                r.Write("visa_id1", visa_id1);
                page.PageSize = int.MaxValue;
                r.Write<object>(page);

                r.request("/report?t=GetBankCashBalance");

                if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

                page = r.ReadPage(page);

                return page;
            }
            #endregion
            public Page<object> GetCGSum(DateTime start_time, DateTime end_time, string branch_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write<object>(page);

            r.request("/report?t=GetCGSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetCGDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string supcust_no, string trans_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("sheet_no", sheet_no);
            r.Write("supcust_no", supcust_no);
            r.Write("trans_no", trans_no);
            r.Write<object>(page);

            r.request("/report?t=GetCGDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetCGOrderSum(DateTime start_time, DateTime end_time, string supcust_no, string barcode, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            r.Write("barcode", barcode);
            r.Write<object>(page);

            r.request("/report?t=GetCGOrderSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetCGMoreSup(DateTime start_time, DateTime end_time, string supcust_no, string barcode, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            r.Write("barcode", barcode);
            r.Write<object>(page);

            r.request("/report?t=GetCGMoreSup");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetCGItemDetail(DateTime start_time, DateTime end_time, string supcust_no, string keyword, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            r.Write("keyword", keyword);
            r.Write<object>(page);

            r.request("/report?t=GetCGItemDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
       

        public Page<object> GetOrderInLoss(DateTime start_time, DateTime end_time, string keyword, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();
            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("keyword", keyword);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetOrderInLoss");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetOrderInLoss_D(DateTime start_time, DateTime end_time, string keyword, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();
            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("keyword", keyword);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetOrderInLoss_D");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetSaleSum(DateTime start_time, DateTime end_time, string branch_no, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetSaleSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetSaleDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string cust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("sheet_no", sheet_no);
            r.Write("cust_no", cust_no);
            r.Write<object>(page);

            r.request("/report?t=GetSaleDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetSaleItemDetail(DateTime start_time, DateTime end_time, string branch_no, string cust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("cust_no", cust_no);
            r.Write<object>(page);

            r.request("/report?t=GetSaleItemDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetSaleOutDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("sheet_no", sheet_no);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetSaleOutDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetCusCredit(string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetCusCredit");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetNoSaleCus(DateTime start_time, DateTime end_time, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write<object>(page);

            r.request("/report?t=GetNoSaleCus");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetSheetPayInfo(DateTime start_time, DateTime end_time, string trans_no, string supcust_no, string sheet_no, string type, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("trans_no", trans_no);
            r.Write("supcust_no", supcust_no);
            r.Write("sheet_no", sheet_no);
            r.Write("type", type);
            r.Write<object>(page);

            r.request("/report?t=GetSheetPayInfo");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetSaleOrderSum(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetSaleOrderSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetSaleOrderDetail(DateTime start_time, DateTime end_time, string sheet_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("sheet_no", sheet_no);
            r.Write<object>(page);

            r.request("/report?t=GetSaleOrderDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetInOutDiff(DateTime start_time, DateTime end_time, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write<object>(page);

            r.request("/report?t=GetInOutDiff");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetICSum(string branch_no, string item_clsno, string item_name, string barcode, string sup_no, string stock_qty, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("branch_no", branch_no);
            r.Write("item_clsno", item_clsno);
            r.Write("item_name", item_name);
            r.Write("barcode", barcode);
            r.Write("sup_no", sup_no);
            r.Write("stock_qty", stock_qty);
            r.Write<object>(page);

            r.request("/report?t=GetICSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetICFlow(DateTime start_time, DateTime end_time, string branch_no, string str, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("str", str);
            r.Write<object>(page);

            r.request("/report?t=GetICFlow");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetICOutDetail(DateTime start_time, DateTime end_time, string branch_no, string barcode, string item_name, string sheet_no, string item_clsno, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("barcode", barcode);
            r.Write("item_name", item_name);
            r.Write("sheet_no", sheet_no);
            r.Write("item_clsno", item_clsno);
            r.Write<object>(page);

            r.request("/report?t=GetICOutDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetJXCSum(DateTime start_time, DateTime end_time, string branch_no, string item_clsno, string item_name, string barcode, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("barcode", barcode);
            r.Write("item_name", item_name);
            r.Write("item_clsno", item_clsno);
            r.Write<object>(page);

            r.request("/report?t=GetJXCSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetPmDetail(DateTime start_time, DateTime end_time, string barcode, string item_name, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("barcode", barcode);
            r.Write("item_name", item_name);
            r.Write<object>(page);

            r.request("/report?t=GetPmDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetCheckPlan(DateTime start_time, DateTime end_time, string branch_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write<object>(page);

            r.request("/report?t=GetCheckPlan");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetCheckPlanDetail(DateTime start_time, DateTime end_time, string sheet_no, string barcode, string item_clsno, string branch_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("branch_no", branch_no);
            r.Write("barcode", barcode);
            r.Write("item_clsno", item_clsno);
            r.Write("sheet_no", sheet_no);
            r.Write<object>(page);

            r.request("/report?t=GetCheckPlanDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetRpCusSum(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);

            r.Write<object>(page);

            r.request("/report?t=GetRpCusSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetRpCusDetail(DateTime start_time, DateTime end_time, string supcust_no, string deal_man, string sheet_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            r.Write("deal_man", deal_man);
            r.Write("sheet_no", sheet_no);
            r.Write<object>(page);

            r.request("/report?t=GetRpCusDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;

        }
        public Page<object> GetRpSupSum(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetRpSupSum");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;

        }
        public Page<object> GetRpSupDetail(DateTime start_time, DateTime end_time, string sheet_no, string deal_man, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("sheet_no", sheet_no);
            r.Write("supcust_no", supcust_no);
            r.Write("deal_man", deal_man);
            r.Write<object>(page);

            r.request("/report?t=GetRpSupDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;

        }
        public Page<object> GetRpSupAccount(DateTime start_time, DateTime end_time, string sheet_no, string oper_type, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("sheet_no", sheet_no);
            r.Write("oper_type", oper_type);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetRpSupAccount");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;

        }
        public Page<object> GetRpCusAccount(DateTime start_time, DateTime end_time, string sheet_no, string oper_type, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("sheet_no", sheet_no);
            r.Write("supcust_no", supcust_no);
            r.Write("oper_type", oper_type);
            r.Write<object>(page);

            r.request("/report?t=GetRpCusAccount");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetRpTodayInc(string sheet_no, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetRpTodayInc");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetRpTodayPay(string sheet_no, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sheet_no", sheet_no);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetRpTodayPay");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetRpCusFyDetail(DateTime start_time, DateTime end_time, string supcust_no, string kk_no, string sheet_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("sheet_no", sheet_no);
            r.Write("supcust_no", supcust_no);
            r.Write("kk_no", kk_no);
            r.Write<object>(page);

            r.request("/report?t=GetRpCusFyDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetRpSupFyDetail(DateTime start_time, DateTime end_time, string supcust_no, string kk_no, string sheet_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("sheet_no", sheet_no);
            r.Write("supcust_no", supcust_no);
            r.Write("kk_no", kk_no);
            r.Write<object>(page);

            r.request("/report?t=GetRpSupFyDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetRpCashBank(DateTime start_time, DateTime end_time, string visa_id, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("visa_id", visa_id);
            r.Write<object>(page);

            r.request("/report?t=GetRpCashBank");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetRpAdminCost(DateTime start_time, DateTime end_time, string sheet_no, string type_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("sheet_no", sheet_no);
            r.Write("type_no", type_no);
            r.Write<object>(page);

            r.request("/report?t=GetRpAdminCost");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }


        public Page<object> GetAssCGFlow(DateTime start_time, DateTime end_time, string supcust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("supcust_no", supcust_no);
            r.Write<object>(page);

            r.request("/report?t=GetAssCGFlow");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public Page<object> GetAssCGPlanDetail(DateTime start_time, DateTime end_time, string keyword, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("keyword", keyword);
            r.Write<object>(page);

            r.request("/report?t=GetAssCGPlanDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
        public DataTable GetAssCGPlanDetailExport(DateTime start_time, DateTime end_time, string keyword)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("keyword", keyword);

            r.request("/report?t=GetAssCGPlanDetailExport");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }
        public Page<object> GetAssCGPreDetail(DateTime start_time, DateTime end_time, string keyword, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("keyword", keyword);
            r.Write<object>(page);

            r.request("/report?t=GetAssCGPreDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetReceiveOrderDetail(DateTime start_time, DateTime end_time, string item_no, string is_build, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("item_no", item_no);
            r.Write("is_build", is_build);
            r.Write<object>(page);

            r.request("/report?t=GetReceiveOrderDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        //实拣
        public Page<object> GetPickingDetail(DateTime start_time, DateTime end_time, string item, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("item", item);
            r.Write<object>(page);

            r.request("/report?t=GetPickingDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetPickingDiff(DateTime start_time, DateTime end_time, string item, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("item", item);
            r.Write<object>(page);

            r.request("/report?t=GetPickingDiff");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetInventoryCheck(DateTime start_time, DateTime end_time, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write<object>(page);

            r.request("/report?t=GetInventoryCheck");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetOperatingStatement(DateTime start_time, DateTime end_time, Page<object> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write<object>(page);

            r.request("/report?t=GetOperatingStatement");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetProcessDetail(DateTime start_time, DateTime end_time, string ph_sheet_no, string oper_type, Page<object> page)
        {
            JsonRequest r = new JsonRequest();
            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("ph_sheet_no", ph_sheet_no);
            r.Write("oper_type", oper_type);
            r.Write<object>(page);

            r.request("/report?t=GetProcessDetail");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetProcessLoss(DateTime start_time, DateTime end_time, Page<object> page)
        {
            JsonRequest r = new JsonRequest();
            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write<object>(page);

            r.request("/report?t=GetProcessLoss");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }

        public Page<object> GetMenuTotalCost(DateTime start_time, DateTime end_time, string cust_no, Page<object> page)
        {
            JsonRequest r = new JsonRequest();
            r.Write("start_time", start_time);
            r.Write("end_time", end_time);
            r.Write("cust_no", cust_no);
            r.Write<object>(page);

            r.request("/report?t=GetMenuTotalCost");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page = r.ReadPage(page);

            return page;
        }
    }
}
