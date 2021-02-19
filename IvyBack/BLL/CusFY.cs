using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyBack.BLL
{
    class CusFY : IBLL.ICusFY
    {
        public System.Data.DataTable GetCusList()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("region_no", "");
            w.Append("keyword", "");
            w.Append("page_index", "1");
            w.Append("page_size", "20000");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus?t=get_list", w.ToString());
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
                    if (row["pay_flag"].ToString() == "2"|| row["pay_flag"].ToString() == "0")
                    {
                        tb2.Rows.Add(row.ItemArray);
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



        public System.Data.DataTable GetBranchList()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("code_len", "4");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/branch?t=get_list", w.ToString());
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

        public System.Data.DataTable GetPeopleList()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("none", "");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/people?t=get_list", w.ToString());
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


        System.Data.DataTable IBLL.ICusFY.GetList(DateTime date1, DateTime date2, string cus_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("date1", date1.ToString("yyyy-MM-dd"));
            w.Append("date2", date2.ToString("yyyy-MM-dd"));
            w.Append("cus_no", cus_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_fy?t=get_list", w.ToString());
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

        void IBLL.ICusFY.GetOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            Helper.JsonRequest r = new Helper.JsonRequest();
            r.Write("sheet_no", sheet_no);

            r.request("/cus_fy?t=get_order");

            r.WhetherSuccess();

            tb1 = r.GetDataTable("tb1");
            tb2 = r.GetDataTable("tb2");

        }

        string IBLL.ICusFY.MaxCode()
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("none", "");
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_fy?t=max_code", w.ToString());
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

        void IBLL.ICusFY.Add(Model.rp_t_supcust_fy_master ord, List<Model.rp_t_supcust_fy_detail> lines, out string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("supcust_no", ord.supcust_no);
            w.Append("supcust_flag", ord.supcust_flag);
            w.Append("pay_type", ord.pay_type);
            w.Append("pay_date", ord.pay_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Append("old_no", ord.old_no);
            w.Append("oper_id", ord.oper_id);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Append("approve_flag", ord.approve_flag);
            w.Append("approve_man", ord.approve_man);
            w.Append("approve_date", "");
            w.Append("is_payed", ord.is_payed);
            w.Append("sale_man", ord.sale_man);
            w.Append("branch_no", ord.branch_no);
            w.Append("cm_branch", ord.cm_branch);
            w.Append("other1", ord.other1);
            w.Append("other2", ord.other2);
            w.Append("other3", ord.other3);
            w.Append("num1", ord.num1.ToString());
            w.Append("num2", ord.num2.ToString());
            w.Append("num3", ord.num3.ToString());
            w.Append("visa_id", ord.visa_id);
            w.Append("total_amount", ord.total_amount.ToString());
            w.Append("paid_amount", ord.paid_amount.ToString());
            w.Append("pay_way", ord.pay_way);
            w.Append("pay_name", ord.pay_name);
            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("kk_no");
            tb.Columns.Add("kk_cash", typeof(decimal));
            tb.Columns.Add("other1");
            tb.Columns.Add("other2");
            tb.Columns.Add("other3");
            tb.Columns.Add("num1", typeof(decimal));
            tb.Columns.Add("num2", typeof(decimal));
            tb.Columns.Add("num3", typeof(decimal));

            foreach (Model.rp_t_supcust_fy_detail line in lines)
            {
                tb.Rows.Add(
                    line.sheet_no,
                    line.kk_no,
                    line.kk_cash,
                    line.other1,
                    line.other2,
                    line.other3,
                    line.num1,
                    line.num2,
                    line.num3

                    );

            }
            w.Append("lines", tb);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_fy?t=add", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            sheet_no = r.Read("sheet_no");
        }

        void IBLL.ICusFY.Change(Model.rp_t_supcust_fy_master ord, List<Model.rp_t_supcust_fy_detail> lines)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", ord.sheet_no);
            w.Append("supcust_no", ord.supcust_no);
            w.Append("supcust_flag", ord.supcust_flag);
            w.Append("pay_type", ord.pay_type);
            w.Append("pay_date", ord.pay_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Append("old_no", ord.old_no);
            w.Append("oper_id", ord.oper_id);
            w.Append("oper_date", ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Append("approve_flag", ord.approve_flag);
            w.Append("approve_man", ord.approve_man);
            w.Append("approve_date", "");
            w.Append("is_payed", ord.is_payed);
            w.Append("sale_man", ord.sale_man);
            w.Append("branch_no", ord.branch_no);
            w.Append("cm_branch", ord.cm_branch);
            w.Append("other1", ord.other1);
            w.Append("other2", ord.other2);
            w.Append("other3", ord.other3);
            w.Append("num1", ord.num1.ToString());
            w.Append("num2", ord.num2.ToString());
            w.Append("num3", ord.num3.ToString());
            w.Append("visa_id", ord.visa_id);
            w.Append("total_amount", ord.total_amount.ToString());
            w.Append("paid_amount", ord.paid_amount.ToString());
            w.Append("pay_way", ord.pay_way);
            w.Append("pay_name", ord.pay_name);
            var tb = new DataTable();
            tb.Columns.Add("sheet_no");
            tb.Columns.Add("kk_no");
            tb.Columns.Add("kk_cash", typeof(decimal));
            tb.Columns.Add("other1");
            tb.Columns.Add("other2");
            tb.Columns.Add("other3");
            tb.Columns.Add("num1", typeof(decimal));
            tb.Columns.Add("num2", typeof(decimal));
            tb.Columns.Add("num3", typeof(decimal));

            foreach (Model.rp_t_supcust_fy_detail line in lines)
            {
                tb.Rows.Add(
                    line.sheet_no,
                    line.kk_no,
                    line.kk_cash,
                    line.other1,
                    line.other2,
                    line.other3,
                    line.num1,
                    line.num2,
                    line.num3
                    );

            }
            w.Append("lines", tb);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_fy?t=change", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICusFY.Delete(string sheet_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_fy?t=delete", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }

        void IBLL.ICusFY.Check(string sheet_no, string approve_man)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("sheet_no", sheet_no);
            w.Append("approve_man", approve_man);
            Helper.IRequest req = new Helper.Request();
            var json = req.request("/cus_fy?t=check", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
        }
    }
}
