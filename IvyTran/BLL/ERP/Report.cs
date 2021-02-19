using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using Aop.Api.Domain;
using System.Data.SqlTypes;
using Model;

namespace IvyTran.BLL.ERP
{
    public class Report : IReport
    {

        #region 应收应付
        public DataTable GetCusContactDetails(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);//借贷 CASE a.pay_type WHEN 1 THEN a.sheet_amount WHEN -1 THEN '' end 
            string sql = @"SELECT
	datepart(yy,a.oper_date) 年,
    datepart(mm,a.oper_date) 月,
    datepart(dd,a.oper_date) 日,
    b.sheet_name ,
	a.voucher_no 单据号,
	a.trans_no 摘要,
    a.pay_type,
    a.oper_date,
    a.trans_no 单据类型,
    a.sheet_amount as 借方,
    a.sheet_amount as 贷方,
    '借' as 方向,
    0.0 as 余额
FROM
	rp_t_accout_payrec_flow a
left join sys_t_sheet_no b on a.trans_no=b.sheet_id 
WHERE
	a.supcust_flag = 'C'
and Convert(varchar(10), a.oper_date ,20 ) <= '" + end_time.ToString("yyyy-MM-dd") + "'";//BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";

                sql += " AND a.supcust_no = '" + supcust_no + "' ";
            
            DataTable tb = db.ExecuteToTable(sql, "a.oper_date ", null, page_size, page_index, out total_count);
            if (tb.Rows.Count == 0)
            {
                return tb;
            }
            sql = "select * from ot_supcust_beginbalance where supcust_no='" + supcust_no + "' and supcust_flag='C'";
            DataTable tb1 = db.ExecuteToTable(sql, null);
            int mm = Conv.ToInt(tb.Rows[0]["月"]);
            int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
            decimal mmborrow = 0.0000m;//月借累计
            decimal mmloan = 0.0000m;//月贷累计
            decimal yyborrow = 0.0000m;//贷借累计
            decimal yyloan = 0.0000m;//年贷累计
            decimal beginbalance = 0.0000m;
            List<int> ls = new List<int>();
            if (tb1.Rows.Count > 0)
            {
                if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 1)
                    beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"])+ 0.0000m;
                else
                    beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
            }
           decimal begion_amount = 0.0000m;
            bool is_one = false;
            int i = 0;
            int n = tb.Rows.Count;
            int k = 0;
            for (int j = 0; j < n; j++)
            {
               
                if (Conv.ToInt(tb.Rows[i]["年"]) != yyyy)
                {
                    yyyy = Conv.ToInt(tb.Rows[i]["年"]);
                    yyborrow = 0;
                    yyloan = 0;
                }
                if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                {
                    if (is_one == false)
                    {
                        begion_amount = beginbalance;
                        is_one = true;
                    }
                }
                
                if (Conv.ToInt(tb.Rows[i]["月"]) != mm )
                {
                    if (Conv.ToDateTime(tb.Rows[i-1]["oper_date"]) >= start_time)
                    {
                        
                   
                    DataRow r = tb.NewRow();
                    r["年"] = yyyy;
                    r["月"] = mm;
                    r["日"] = DBNull.Value;
                    r["单据类型"] = DBNull.Value;
                    r["单据号"] = DBNull.Value;
                    r["摘要"] = "本月合计";
                    r["借方"] = mmborrow;
                    r["贷方"] = mmloan;
                    if (beginbalance > 0)
                    {
                        r["方向"] = "借";
                    }
                    else if (beginbalance < 0)
                    {
                        r["方向"] = "贷";
                    }
                    else
                    {
                        r["方向"] = "平";
                    }
                    r["余额"] = Math.Abs(beginbalance);
                    tb.Rows.InsertAt(r, i);
                    DataRow rr = tb.NewRow();
                    rr["年"] = yyyy;
                    rr["月"] = mm;
                    rr["日"] = DBNull.Value;
                    rr["单据类型"] = DBNull.Value;
                    rr["单据号"] = DBNull.Value;
                    rr["摘要"] = "本年合计";
                    rr["借方"] = yyborrow;
                    rr["贷方"] = yyloan;
                    if (beginbalance > 0)
                    {
                        rr["方向"] = "借";
                    }
                    else if (beginbalance < 0)
                    {
                        rr["方向"] = "贷";
                    }
                    else
                    {
                        rr["方向"] = "平";
                    }
                    rr["余额"] = Math.Abs(beginbalance);

                    tb.Rows.InsertAt(rr, i+1);
                    i += 2;
                    
                    }
                    mm = Conv.ToInt(tb.Rows[i]["月"]);
                    mmborrow = 0.0000m;
                    mmloan = 0.0000m;

                }
                if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == 1)
                {
                    tb.Rows[i]["贷方"] = DBNull.Value;
                    beginbalance += Conv.ToDecimal(tb.Rows[i]["借方"]);
                    mmborrow+= Conv.ToDecimal(tb.Rows[i]["借方"]);
                    yyborrow+= Conv.ToDecimal(tb.Rows[i]["借方"]);
                    if (beginbalance > 0)
                    {
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                    else if(beginbalance < 0)
                    {
                        tb.Rows[i]["方向"] = "贷";
                        tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                    }
                    else
                    {
                        tb.Rows[i]["方向"] = "平";
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                }else if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == -1)
                {
                    
                    tb.Rows[i]["借方"] = DBNull.Value;
                    beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    yyloan+= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    if (beginbalance > 0)
                    {
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                    else if (beginbalance < 0)
                    {
                        tb.Rows[i]["方向"] = "贷";
                        tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                    }
                    else
                    {
                        tb.Rows[i]["方向"] = "平";
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                }
                if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                {
                    ls.Add(i);
                    //tb.Rows[i].Delete();
                }
                k++;
                i++;
                if (n==k)
                {
                    //mm = Conv.ToInt(tb.Rows[i]["月"]);
                    DataRow r = tb.NewRow();
                    r["年"] = yyyy;
                    r["月"] = mm;
                    r["日"] = DBNull.Value;
                    r["单据类型"] = DBNull.Value;
                    r["单据号"] = DBNull.Value;
                    r["摘要"] = "本月合计";
                    r["借方"] = mmborrow;
                    r["贷方"] = mmloan;
                    if (beginbalance > 0)
                    {
                        r["方向"] = "借";
                    }
                    else if (beginbalance < 0)
                    {
                        r["方向"] = "贷";
                    }
                    else
                    {
                        r["方向"] = "平";
                    }
                    r["余额"] = Math.Abs(beginbalance);
                    tb.Rows.Add(r);
                    DataRow rr = tb.NewRow();
                    rr["年"] = yyyy;
                    rr["月"] = mm;
                    rr["日"] = DBNull.Value;
                    rr["单据类型"] = DBNull.Value;
                    rr["单据号"] = DBNull.Value;
                    rr["摘要"] = "本年合计";
                    rr["借方"] = yyborrow;
                    rr["贷方"] = yyloan;
                    if (beginbalance > 0)
                    {
                        rr["方向"] = "借";
                    }
                    else if (beginbalance < 0)
                    {
                        rr["方向"] = "贷";
                    }
                    else
                    {
                        rr["方向"] = "平";
                    }
                    rr["余额"] = Math.Abs(beginbalance);

                    tb.Rows.Add(rr);
                }


               
            }
            foreach(int j in ls)
            {
                tb.Rows[j].Delete();
            }
            tb.AcceptChanges();
            DataRow s = tb.NewRow();
            s["年"] = DBNull.Value;
            s["月"] = DBNull.Value;
            s["日"] = DBNull.Value;
            s["单据类型"] = DBNull.Value;
            s["单据号"] = DBNull.Value;
            s["摘要"] = "期初金额";
            s["借方"] = DBNull.Value;
            s["贷方"] = DBNull.Value;
            if (begion_amount > 0)
            {
                s["方向"] = "借";
            }
            else if (begion_amount < 0)
            {
                s["方向"] = "贷";
            }
            else
            {
                s["方向"] = "平";
            }
            s["余额"] = Math.Abs(begion_amount);
            tb.Rows.InsertAt(s, 0);
            return tb;
        }

        public DataTable GetCusBalance(DateTime start_time, DateTime end_time, string cust_from, string company_type, int isnull, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);//借贷 CASE a.pay_type WHEN 1 THEN a.sheet_amount WHEN -1 THEN '' end 
            string sql = @"SELECT
	a.supcust_no 编码, 
    b.sup_name 名称,
   datepart(yy,a.oper_date) 年,
    datepart(mm,a.oper_date) 月,
    datepart(dd,a.oper_date) 日,
    a.pay_type,
    a.oper_date,
    a.sheet_amount as 借方,
    a.sheet_amount as 贷方,
    '借' as 方向,
    0.0 as 余额
FROM
	rp_t_accout_payrec_flow a

left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and b.supcust_flag='C'
WHERE
	a.supcust_flag = 'C'
and a.oper_date between '"+start_time+"' and '" + end_time + "'";//BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (cust_from != "")
            {
                sql += " and a.supcust_no='" + cust_from + "' ";
            }
            //if (cust_to != "")
            //{
            //    sql += " and a.supcust_no<='" + cust_to + "' ";
            //}
            if (company_type != "")
            {
                sql += " and b.sup_type='" + company_type + "' ";
            }
            DataTable tbs = db.ExecuteToTable(sql, "a.supcust_no,a.oper_date ", null, page_size, page_index, out total_count);
            DataTable dt = new DataTable();
            dt.Columns.Add("编码");
            dt.Columns.Add("名称");
            dt.Columns.Add("期初金额方向");
            dt.Columns.Add("期初金额");
            dt.Columns.Add("借方");
            dt.Columns.Add("贷方");
            dt.Columns.Add("期末金额方向");
            dt.Columns.Add("期末金额");
            if (tbs.Rows.Count == 0)
            {
                return dt;
            }
            DataTable tb = tbs.Copy();
            tb.Clear();
            string supcust_no = Conv.ToString(tbs.Rows[0]["编码"]);
            string sup_name= Conv.ToString(tbs.Rows[0]["名称"]);
            int ss = 0;
            foreach (DataRow dr in tbs.Rows)
            {



                    DataRow sr = tb.NewRow();
                    sr["编码"] = dr["编码"];
                    sr["名称"] = dr["名称"];
                    sr["年"] = dr["年"];
                    sr["月"] = dr["月"];
                    sr["日"] = dr["日"];
                    sr["pay_type"] = dr["pay_type"];
                    sr["oper_date"] = dr["oper_date"];
                    sr["借方"] = dr["借方"];
                    sr["贷方"] = dr["贷方"];
                    sr["方向"] = dr["方向"];
                    sr["余额"] = dr["余额"];
                    tb.Rows.Add(sr);
                if (supcust_no != Conv.ToString(tb.Rows[tb.Rows.Count-1]["编码"]))
                {

                    sql = "select * from ot_supcust_beginbalance where supcust_no='" + supcust_no + "' and supcust_flag='C'";
                    DataTable tb1 = db.ExecuteToTable(sql, null);
                    int mm = Conv.ToInt(tb.Rows[0]["月"]);
                    int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
                    decimal mmborrow = 0.0000m;//月借累计
                    decimal mmloan = 0.0000m;//月贷累计
                    decimal yyborrow = 0.0000m;//贷借累计
                    decimal yyloan = 0.0000m;//年贷累计
                    decimal beginbalance = 0.0000m;
                    if (tb1.Rows.Count > 0)
                    {
                        if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 1)
                            beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"])+ 0.0000m;
                        else
                            beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
                    }
                    decimal begion_amount = 0.0000m;
                    bool is_one = false;
                    int i = 0;
                    int n = tb.Rows.Count;
                    int k = 0;
                    for (int j = 0; j < n - 1; j++)
                    {

                        //if (Conv.ToInt(tb.Rows[i]["年"]) != yyyy)
                        //{
                        //    yyyy = Conv.ToInt(tb.Rows[i]["年"]);
                        //    //yyborrow = 0;
                        //    //yyloan = 0;
                        //}
                        if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                        {
                            if (is_one == false)
                            {
                                begion_amount = beginbalance;
                                yyborrow = 0;
                                yyloan = 0;
                                is_one = true;
                            }
                        }
                        //if (Conv.ToInt(tb.Rows[i]["月"]) != mm)
                        //{

                        //    DataRow r = tb.NewRow();
                        //    r["年"] = yyyy;
                        //    r["月"] = mm;
                        //    r["日"] = DBNull.Value;
                        //    //r["单据类型"] = DBNull.Value;
                        //    //r["单据号"] = DBNull.Value;
                        //    //r["摘要"] = "本月合计";
                        //    r["借方"] = mmborrow;
                        //    r["贷方"] = mmloan;
                        //    if (beginbalance > 0)
                        //    {
                        //        r["方向"] = "借";
                        //    }
                        //    else if (beginbalance < 0)
                        //    {
                        //        r["方向"] = "贷";
                        //    }
                        //    else
                        //    {
                        //        r["方向"] = "平";
                        //    }
                        //    r["余额"] = Math.Abs(beginbalance);
                        //    tb.Rows.InsertAt(r, i);
                        //    DataRow rr = tb.NewRow();
                        //    rr["年"] = yyyy;
                        //    rr["月"] = mm;
                        //    rr["日"] = DBNull.Value;
                        //    //rr["单据类型"] = DBNull.Value;
                        //    //rr["单据号"] = DBNull.Value;
                        //    //rr["摘要"] = "本年合计";
                        //    rr["借方"] = yyborrow;
                        //    rr["贷方"] = yyloan;
                        //    if (beginbalance > 0)
                        //    {
                        //        rr["方向"] = "借";
                        //    }
                        //    else if (beginbalance < 0)
                        //    {
                        //        rr["方向"] = "贷";
                        //    }
                        //    else
                        //    {
                        //        rr["方向"] = "平";
                        //    }
                        //    rr["余额"] = Math.Abs(beginbalance);

                        //    tb.Rows.InsertAt(rr, i + 1);
                        //   // i += 2;
                        //    mm = Conv.ToInt(tb.Rows[i]["月"]);
                        //    mmborrow = 0.0000m;
                        //    mmloan = 0.0000m;

                        //}
                        if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == 1)
                        {
                            tb.Rows[i]["贷方"] = DBNull.Value;
                            beginbalance += Conv.ToDecimal(tb.Rows[i]["借方"]);
                            mmborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                            yyborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                            if (beginbalance > 0)
                            {
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                            else if (beginbalance < 0)
                            {
                                tb.Rows[i]["方向"] = "贷";
                                tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                            }
                            else
                            {
                                tb.Rows[i]["方向"] = "平";
                                tb.Rows[i]["余额"] = Math.Abs(beginbalance);
                            }
                        }
                        else if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == -1)
                        {

                            tb.Rows[i]["借方"] = DBNull.Value;
                            beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            if (beginbalance > 0)
                            {
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                            else if (beginbalance < 0)
                            {
                                tb.Rows[i]["方向"] = "贷";
                                tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                            }
                            else
                            {
                                tb.Rows[i]["方向"] = "平";
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                        }
                        if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                        {
                            //tb.Rows[i].Delete();
                        }
                        k++;
                        i++;
                        if (n - 1 == k)
                        {

                            //mm = Conv.ToInt(tb.Rows[i]["月"]);
                            DataRow r = dt.NewRow();
                            r["编码"] = supcust_no;
                            r["名称"] = sup_name;
                            r["期初金额"] = Math.Abs(begion_amount);
                            if (begion_amount > 0)
                                r["期初金额方向"] = "借";
                            else if (begion_amount < 0)
                                r["期初金额方向"] = "贷";
                            else
                                r["期初金额方向"] = "平";
                            r["借方"] = yyborrow;
                            r["贷方"] = yyloan;
                            r["期末金额"] = Math.Abs(beginbalance);
                            if (beginbalance > 0)
                                r["期末金额方向"] = "借";
                            else if (beginbalance < 0)
                                r["期末金额方向"] = "贷";
                            else
                                r["期末金额方向"] = "平";
                            dt.Rows.Add(r);
                         
                        }



                    }


                    tb.Clear();
                    supcust_no = Conv.ToString(dr["编码"]);
                    sup_name = Conv.ToString(dr["名称"]);
                    DataRow sr1 = tb.NewRow();
                    sr1["编码"] = dr["编码"];
                    sr1["名称"] = dr["名称"];
                    sr1["年"] = dr["年"];
                    sr1["月"] = dr["月"];
                    sr1["日"] = dr["日"];
                    sr1["pay_type"] = dr["pay_type"];
                    sr1["oper_date"] = dr["oper_date"];
                    sr1["借方"] = dr["借方"];
                    sr1["贷方"] = dr["贷方"];
                    sr1["方向"] = dr["方向"];
                    sr1["余额"] = dr["余额"];
                    tb.Rows.Add(sr1);
                    //yyborrow = 0;
                    //yyloan = 0;

                }



                ss++;
            }
           
            if (true)
            {
                sql = "select * from ot_supcust_beginbalance where supcust_no='" + supcust_no + "' and supcust_flag='C'";
                DataTable tb1 = db.ExecuteToTable(sql, null);
                int mm = Conv.ToInt(tb.Rows[0]["月"]);
                int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
                decimal mmborrow = 0.0000m;//月借累计
                decimal mmloan = 0.0000m;//月贷累计
                decimal yyborrow = 0.0000m;//贷借累计
                decimal yyloan = 0.0000m;//年贷累计
                decimal beginbalance = 0.0000m;
                if (tb1.Rows.Count > 0)
                {
                    if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 1)
                        beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"])+ 0.0000m;
                    else
                        beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
                }
                decimal begion_amount = 0.0000m;
                bool is_one = false;
                int i = 0;
                int n = tb.Rows.Count;
                int k = 0;
                for (int j = 0; j < n; j++)
                {

                    //if (Conv.ToInt(tb.Rows[i]["年"]) != yyyy)
                    //{
                    //    yyyy = Conv.ToInt(tb.Rows[i]["年"]);
                    //    //yyborrow = 0;
                    //    //yyloan = 0;
                    //}
                    if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                    {
                        if (is_one == false)
                        {
                            begion_amount = beginbalance;
                            yyborrow = 0;
                            yyloan = 0;
                            is_one = true;
                        }
                    }
                    //if (Conv.ToInt(tb.Rows[i]["月"]) != mm)
                    //{

                    //    DataRow r = tb.NewRow();
                    //    r["年"] = yyyy;
                    //    r["月"] = mm;
                    //    r["日"] = DBNull.Value;
                    //    //r["单据类型"] = DBNull.Value;
                    //    //r["单据号"] = DBNull.Value;
                    //    //r["摘要"] = "本月合计";
                    //    r["借方"] = mmborrow;
                    //    r["贷方"] = mmloan;
                    //    if (beginbalance > 0)
                    //    {
                    //        r["方向"] = "借";
                    //    }
                    //    else if (beginbalance < 0)
                    //    {
                    //        r["方向"] = "贷";
                    //    }
                    //    else
                    //    {
                    //        r["方向"] = "平";
                    //    }
                    //    r["余额"] = Math.Abs(beginbalance);
                    //    tb.Rows.InsertAt(r, i);
                    //    DataRow rr = tb.NewRow();
                    //    rr["年"] = yyyy;
                    //    rr["月"] = mm;
                    //    rr["日"] = DBNull.Value;
                    //    //rr["单据类型"] = DBNull.Value;
                    //    //rr["单据号"] = DBNull.Value;
                    //    //rr["摘要"] = "本年合计";
                    //    rr["借方"] = yyborrow;
                    //    rr["贷方"] = yyloan;
                    //    if (beginbalance > 0)
                    //    {
                    //        rr["方向"] = "借";
                    //    }
                    //    else if (beginbalance < 0)
                    //    {
                    //        rr["方向"] = "贷";
                    //    }
                    //    else
                    //    {
                    //        rr["方向"] = "平";
                    //    }
                    //    rr["余额"] = Math.Abs(beginbalance);

                    //    tb.Rows.InsertAt(rr, i + 1);
                    //    i += 2;
                    //    mm = Conv.ToInt(tb.Rows[i]["月"]);
                    //    mmborrow = 0.0000m;
                    //    mmloan = 0.0000m;

                    //}
                    if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == 1)
                    {
                        tb.Rows[i]["贷方"] = DBNull.Value;
                        beginbalance += Conv.ToDecimal(tb.Rows[i]["借方"]);
                        mmborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                        yyborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                        if (beginbalance > 0)
                        {
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                        else if (beginbalance < 0)
                        {
                            tb.Rows[i]["方向"] = "贷";
                            tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                        }
                        else
                        {
                            tb.Rows[i]["方向"] = "平";
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                    }
                    else if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == -1)
                    {

                        tb.Rows[i]["借方"] = DBNull.Value;
                        beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        if (beginbalance > 0)
                        {
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                        else if (beginbalance < 0)
                        {
                            tb.Rows[i]["方向"] = "贷";
                            tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                        }
                        else
                        {
                            tb.Rows[i]["方向"] = "平";
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                    }
                    if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                    {
                        //tb.Rows[i].Delete();
                    }
                    k++;
                    i++;
                    if (n == k)
                    {

                        //mm = Conv.ToInt(tb.Rows[i]["月"]);
                        DataRow r = dt.NewRow();
                        r["编码"] = supcust_no;
                        r["名称"] = sup_name;
                        r["期初金额"] = Math.Abs(begion_amount);
                        if (begion_amount > 0)
                            r["期初金额方向"] = "借";
                        else if (begion_amount < 0)
                            r["期初金额方向"] = "贷";
                        else
                            r["期初金额方向"] = "平";
                        r["借方"] = yyborrow;
                        r["贷方"] = yyloan;
                        r["期末金额"] = Math.Abs(beginbalance);
                        if (beginbalance > 0)
                            r["期末金额方向"] = "借";
                        else if (beginbalance < 0)
                            r["期末金额方向"] = "贷";
                        else
                            r["期末金额方向"] = "平";
                        dt.Rows.Add(r);
                    }
                }
               
            }
            if (isnull == 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                   if(Conv.ToDecimal(dt.Rows[i]["期初金额"])==0&& Conv.ToDecimal(dt.Rows[i]["借方"]) == 0 && Conv.ToDecimal(dt.Rows[i]["贷方"]) == 0 && Conv.ToDecimal(dt.Rows[i]["期末金额"]) == 0)
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }
                   
                }
            }
            return dt;
        }


        public DataTable GetSupContactDetails(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);//借贷 CASE a.pay_type WHEN 1 THEN a.sheet_amount WHEN -1 THEN '' end 
            string sql = @"SELECT
	datepart(yy,a.oper_date) 年,
    datepart(mm,a.oper_date) 月,
    datepart(dd,a.oper_date) 日,
    b.sheet_name ,
	a.voucher_no 单据号,
	a.trans_no 摘要,
    a.pay_type,
    a.oper_date,
    a.trans_no 单据类型,
    a.sheet_amount as 借方,
    a.sheet_amount as 贷方,
    '贷' as 方向,
    0.0 as 余额
FROM
	rp_t_accout_payrec_flow a
left join sys_t_sheet_no b on a.trans_no=b.sheet_id 
WHERE
	a.supcust_flag = 'S'
and Convert(varchar(10), a.oper_date ,20 ) <= '" + end_time.ToString("yyyy-MM-dd") + "'";//BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";

            sql += " AND a.supcust_no = '" + supcust_no + "' ";

            DataTable tb = db.ExecuteToTable(sql, "a.oper_date ", null, page_size, page_index, out total_count);
            if (tb.Rows.Count == 0)
            {
                return tb;
            }
            sql = "select * from ot_supcust_beginbalance where supcust_no='" + supcust_no + "' and supcust_flag='S'";
            DataTable tb1 = db.ExecuteToTable(sql, null);
            int mm = Conv.ToInt(tb.Rows[0]["月"]);
            int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
            decimal mmborrow = 0.0000m;//月借累计
            decimal mmloan = 0.0000m;//月贷累计
            decimal yyborrow = 0.0000m;//贷借累计
            decimal yyloan = 0.0000m;//年贷累计
            decimal beginbalance = 0.0000m;
            List<int> ls = new List<int>();
            if (tb1.Rows.Count > 0)
            {
                if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 2)
                    beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"])+ 0.0000m;
                else
                    beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
            }
            decimal begion_amount = 0.0000m;
            bool is_one = false;
            int i = 0;
            int n = tb.Rows.Count;
            int k = 0;
            for (int j = 0; j < n; j++)
            {

                if (Conv.ToInt(tb.Rows[i]["年"]) != yyyy)
                {
                    yyyy = Conv.ToInt(tb.Rows[i]["年"]);
                    yyborrow = 0;
                    yyloan = 0;
                }
                if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                {
                    if (is_one == false)
                    {
                        begion_amount = beginbalance;
                        is_one = true;
                    }
                }

                if (Conv.ToInt(tb.Rows[i]["月"]) != mm)
                {
                    if (Conv.ToDateTime(tb.Rows[i - 1]["oper_date"]) >= start_time)
                    {


                        DataRow r = tb.NewRow();
                        r["年"] = yyyy;
                        r["月"] = mm;
                        r["日"] = DBNull.Value;
                        r["单据类型"] = DBNull.Value;
                        r["单据号"] = DBNull.Value;
                        r["摘要"] = "本月合计";
                        r["借方"] = mmborrow;
                        r["贷方"] = mmloan;
                        if (beginbalance > 0)
                        {
                            r["方向"] = "贷";
                        }
                        else if (beginbalance < 0)
                        {
                            r["方向"] = "借";
                        }
                        else
                        {
                            r["方向"] = "平";
                        }
                        r["余额"] = Math.Abs(beginbalance);
                        tb.Rows.InsertAt(r, i);
                        DataRow rr = tb.NewRow();
                        rr["年"] = yyyy;
                        rr["月"] = mm;
                        rr["日"] = DBNull.Value;
                        rr["单据类型"] = DBNull.Value;
                        rr["单据号"] = DBNull.Value;
                        rr["摘要"] = "本年合计";
                        rr["借方"] = yyborrow;
                        rr["贷方"] = yyloan;
                        if (beginbalance > 0)
                        {
                            rr["方向"] = "贷";
                        }
                        else if (beginbalance < 0)
                        {
                            rr["方向"] = "借";
                        }
                        else
                        {
                            rr["方向"] = "平";
                        }
                        rr["余额"] = Math.Abs(beginbalance);

                        tb.Rows.InsertAt(rr, i + 1);
                        i += 2;

                    }
                    mm = Conv.ToInt(tb.Rows[i]["月"]);
                    mmborrow = 0.0000m;
                    mmloan = 0.0000m;

                }
                if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == -1)
                {
                    tb.Rows[i]["贷方"] = DBNull.Value;
                    beginbalance -= Conv.ToDecimal(tb.Rows[i]["借方"]);
                    mmborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                    yyborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                    if (beginbalance > 0)
                    {
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                    else if (beginbalance < 0)
                    {
                        tb.Rows[i]["方向"] = "借";
                        tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                    }
                    else
                    {
                        tb.Rows[i]["方向"] = "平";
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                }
                else if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == 1)
                {

                    tb.Rows[i]["借方"] = DBNull.Value;
                    beginbalance += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    if (beginbalance > 0)
                    {
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                    else if (beginbalance < 0)
                    {
                        tb.Rows[i]["方向"] = "借";
                        tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                    }
                    else
                    {
                        tb.Rows[i]["方向"] = "平";
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                }
                if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                {
                    ls.Add(i);
                    //tb.Rows[i].Delete();
                }
                k++;
                i++;
                if (n == k)
                {
                    //mm = Conv.ToInt(tb.Rows[i]["月"]);
                    DataRow r = tb.NewRow();
                    r["年"] = yyyy;
                    r["月"] = mm;
                    r["日"] = DBNull.Value;
                    r["单据类型"] = DBNull.Value;
                    r["单据号"] = DBNull.Value;
                    r["摘要"] = "本月合计";
                    r["借方"] = mmborrow;
                    r["贷方"] = mmloan;
                    if (beginbalance > 0)
                    {
                        r["方向"] = "贷";
                    }
                    else if (beginbalance < 0)
                    {
                        r["方向"] = "借";
                    }
                    else
                    {
                        r["方向"] = "平";
                    }
                    r["余额"] = Math.Abs(beginbalance);
                    tb.Rows.Add(r);
                    DataRow rr = tb.NewRow();
                    rr["年"] = yyyy;
                    rr["月"] = mm;
                    rr["日"] = DBNull.Value;
                    rr["单据类型"] = DBNull.Value;
                    rr["单据号"] = DBNull.Value;
                    rr["摘要"] = "本年合计";
                    rr["借方"] = yyborrow;
                    rr["贷方"] = yyloan;
                    if (beginbalance > 0)
                    {
                        rr["方向"] = "贷";
                    }
                    else if (beginbalance < 0)
                    {
                        rr["方向"] = "借";
                    }
                    else
                    {
                        rr["方向"] = "平";
                    }
                    rr["余额"] = Math.Abs(beginbalance);

                    tb.Rows.Add(rr);
                }



            }
            foreach (int j in ls)
            {
                tb.Rows[j].Delete();
            }
            tb.AcceptChanges();
            DataRow s = tb.NewRow();
            s["年"] = DBNull.Value;
            s["月"] = DBNull.Value;
            s["日"] = DBNull.Value;
            s["单据类型"] = DBNull.Value;
            s["单据号"] = DBNull.Value;
            s["摘要"] = "期初金额";
            s["借方"] = DBNull.Value;
            s["贷方"] = DBNull.Value;
            if (begion_amount > 0)
            {
                s["方向"] = "贷";
            }
            else if (begion_amount < 0)
            {
                s["方向"] = "借";
            }
            else
            {
                s["方向"] = "平";
            }
            s["余额"] = Math.Abs(begion_amount);
            tb.Rows.InsertAt(s, 0);
            return tb;
        }

        public DataTable GetSupBalance(DateTime start_time, DateTime end_time, string cust_from, string company_type,int isnull, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);//借贷 CASE a.pay_type WHEN 1 THEN a.sheet_amount WHEN -1 THEN '' end 
            string sql = @"SELECT
	a.supcust_no 编码, 
    b.sup_name 名称,
   datepart(yy,a.oper_date) 年,
    datepart(mm,a.oper_date) 月,
    datepart(dd,a.oper_date) 日,
    a.pay_type,
    a.oper_date,
    a.sheet_amount as 借方,
    a.sheet_amount as 贷方,
    '贷' as 方向,
    0.0 as 余额
FROM
	rp_t_accout_payrec_flow a

left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and b.supcust_flag='S'
WHERE
	a.supcust_flag = 'S'
and Convert(varchar(10), a.oper_date ,20 ) <= '" + end_time.ToString("yyyy-MM-dd") + "'";//BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";
            if (cust_from != "")
            {
                sql += " and a.supcust_no='" + cust_from + "' ";
            }
            //if (cust_to != "")
            //{
            //    sql += " and a.supcust_no<='" + cust_to + "' ";
            //}
            if (company_type != "")
            {
                sql += " and b.sup_type='" + company_type + "' ";
            }

            DataTable tbs = db.ExecuteToTable(sql, "a.supcust_no,a.oper_date ", null, page_size, page_index, out total_count);
            DataTable dt = new DataTable();
            dt.Columns.Add("编码");
            dt.Columns.Add("名称");
            dt.Columns.Add("期初金额方向");
            dt.Columns.Add("期初金额");
            dt.Columns.Add("借方");
            dt.Columns.Add("贷方");
            dt.Columns.Add("期末金额方向");
            dt.Columns.Add("期末金额");
            if (tbs.Rows.Count == 0)
            {
                return dt;
            }
            DataTable tb = tbs.Copy();
            tb.Clear();
            string supcust_no = Conv.ToString(tbs.Rows[0]["编码"]);
            string sup_name = Conv.ToString(tbs.Rows[0]["名称"]);
            int ss = 0;
            foreach (DataRow dr in tbs.Rows)
            {



                DataRow sr = tb.NewRow();
                sr["编码"] = dr["编码"];
                sr["名称"] = dr["名称"];
                sr["年"] = dr["年"];
                sr["月"] = dr["月"];
                sr["日"] = dr["日"];
                sr["pay_type"] = dr["pay_type"];
                sr["oper_date"] = dr["oper_date"];
                sr["借方"] = dr["借方"];
                sr["贷方"] = dr["贷方"];
                sr["方向"] = dr["方向"];
                sr["余额"] = dr["余额"];
                tb.Rows.Add(sr);
                if (supcust_no != Conv.ToString(tb.Rows[tb.Rows.Count - 1]["编码"]))
                {

                    sql = "select * from ot_supcust_beginbalance where supcust_no='" + supcust_no + "' and supcust_flag='S'";
                    DataTable tb1 = db.ExecuteToTable(sql, null);
                    int mm = Conv.ToInt(tb.Rows[0]["月"]);
                    int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
                    decimal mmborrow = 0.0000m;//月借累计
                    decimal mmloan = 0.0000m;//月贷累计
                    decimal yyborrow = 0.0000m;//贷借累计
                    decimal yyloan = 0.0000m;//年贷累计
                    decimal beginbalance = 0.0000m;
                    if (tb1.Rows.Count > 0)
                    {
                        if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 2)
                            beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"])+ 0.0000m;
                        else
                            beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
                    }
                    decimal begion_amount = 0.0000m;
                    bool is_one = false;
                    int i = 0;
                    int n = tb.Rows.Count;
                    int k = 0;
                    for (int j = 0; j < n - 1; j++)
                    {

                        //if (Conv.ToInt(tb.Rows[i]["年"]) != yyyy)
                        //{
                        //    yyyy = Conv.ToInt(tb.Rows[i]["年"]);
                        //    //yyborrow = 0;
                        //    //yyloan = 0;
                        //}
                        if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                        {
                            if (is_one == false)
                            {
                                begion_amount = beginbalance;
                                yyborrow = 0;
                                yyloan = 0;
                                is_one = true;
                            }
                        }
                        //if (Conv.ToInt(tb.Rows[i]["月"]) != mm)
                        //{

                        //    //DataRow r = tb.NewRow();
                        //    //r["年"] = yyyy;
                        //    //r["月"] = mm;
                        //    //r["日"] = DBNull.Value;
                        //    ////r["单据类型"] = DBNull.Value;
                        //    ////r["单据号"] = DBNull.Value;
                        //    ////r["摘要"] = "本月合计";
                        //    //r["借方"] = mmborrow;
                        //    //r["贷方"] = mmloan;
                        //    //if (beginbalance > 0)
                        //    //{
                        //    //    r["方向"] = "贷";
                        //    //}
                        //    //else if (beginbalance < 0)
                        //    //{
                        //    //    r["方向"] = "借";
                        //    //}
                        //    //else
                        //    //{
                        //    //    r["方向"] = "平";
                        //    //}
                        //    //r["余额"] = Math.Abs(beginbalance);
                        //    //tb.Rows.InsertAt(r, i);
                        //    //DataRow rr = tb.NewRow();
                        //    //rr["年"] = yyyy;
                        //    //rr["月"] = mm;
                        //    //rr["日"] = DBNull.Value;
                        //    ////rr["单据类型"] = DBNull.Value;
                        //    ////rr["单据号"] = DBNull.Value;
                        //    ////rr["摘要"] = "本年合计";
                        //    //rr["借方"] = yyborrow;
                        //    //rr["贷方"] = yyloan;
                        //    //if (beginbalance > 0)
                        //    //{
                        //    //    rr["方向"] = "贷";
                        //    //}
                        //    //else if (beginbalance < 0)
                        //    //{
                        //    //    rr["方向"] = "借";
                        //    //}
                        //    //else
                        //    //{
                        //    //    rr["方向"] = "平";
                        //    //}
                        //    //rr["余额"] = Math.Abs(beginbalance);

                        //    //tb.Rows.InsertAt(rr, i + 1);
                        //    // i += 2;
                        //    mm = Conv.ToInt(tb.Rows[i]["月"]);
                        //    mmborrow = 0.0000m;
                        //    mmloan = 0.0000m;

                        //}
                        if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == -1)
                        {
                            tb.Rows[i]["贷方"] = DBNull.Value;
                            beginbalance -= Conv.ToDecimal(tb.Rows[i]["借方"]);
                            mmborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                            yyborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                            if (beginbalance > 0)
                            {
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                            else if (beginbalance < 0)
                            {
                                tb.Rows[i]["方向"] = "贷";
                                tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                            }
                            else
                            {
                                tb.Rows[i]["方向"] = "平";
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                        }
                        else if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == 1)
                        {

                            tb.Rows[i]["借方"] = DBNull.Value;
                            beginbalance += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            if (beginbalance > 0)
                            {
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                            else if (beginbalance < 0)
                            {
                                tb.Rows[i]["方向"] = "借";
                                tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                            }
                            else
                            {
                                tb.Rows[i]["方向"] = "平";
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                        }
                        //if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                        //{
                        //    //tb.Rows[i].Delete();
                        //}
                        k++;
                        i++;
                        if (n - 1 == k)
                        {

                            //mm = Conv.ToInt(tb.Rows[i]["月"]);
                            DataRow r = dt.NewRow();
                            r["编码"] = supcust_no;
                            r["名称"] = sup_name;
                            r["期初金额"] = Math.Abs(begion_amount);
                            if (begion_amount > 0)
                                r["期初金额方向"] = "贷";
                            else if (begion_amount < 0)
                                r["期初金额方向"] = "借";
                            else
                                r["期初金额方向"] = "平";
                            r["借方"] = yyborrow;
                            r["贷方"] = yyloan ;

                            if (beginbalance > 0)
                            {
                                r["期末金额方向"] = "贷";
                                r["期末金额"] = beginbalance;
                            }
                            else if (beginbalance < 0)
                            {
                                r["期末金额方向"] = "借";
                                r["期末金额"] = Math.Abs(beginbalance);
                            }
                            else
                            {
                                r["期末金额方向"] = "平";
                                r["期末金额"] = beginbalance;
                            }
                            dt.Rows.Add(r);

                        }



                    }


                    tb.Clear();
                    supcust_no = Conv.ToString(dr["编码"]);
                    sup_name = Conv.ToString(dr["名称"]);
                    DataRow sr1 = tb.NewRow();
                    sr1["编码"] = dr["编码"];
                    sr1["名称"] = dr["名称"];
                    sr1["年"] = dr["年"];
                    sr1["月"] = dr["月"];
                    sr1["日"] = dr["日"];
                    sr1["pay_type"] = dr["pay_type"];
                    sr1["oper_date"] = dr["oper_date"];
                    sr1["借方"] = dr["借方"];
                    sr1["贷方"] = dr["贷方"];
                    sr1["方向"] = dr["方向"];
                    sr1["余额"] = dr["余额"];
                    tb.Rows.Add(sr1);
                    //yyborrow = 0;
                    //yyloan = 0;

                }



                ss++;
            }

            if (true)
            {
                sql = "select * from ot_supcust_beginbalance where supcust_no='" + supcust_no + "' and supcust_flag='S'";
                DataTable tb1 = db.ExecuteToTable(sql, null);
                int mm = Conv.ToInt(tb.Rows[0]["月"]);
                int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
                decimal mmborrow = 0.0000m;//月借累计
                decimal mmloan = 0.0000m;//月贷累计
                decimal yyborrow = 0.0000m;//贷借累计
                decimal yyloan = 0.0000m;//年贷累计
                decimal beginbalance = 0.0000m;
                if (tb1.Rows.Count > 0)
                {
                    if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 2)
                        beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"])+ 0.0000m;
                    else
                        beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
                }
                decimal begion_amount = 0.0000m;
                bool is_one = false;
                int i = 0;
                int n = tb.Rows.Count;
                int k = 0;
                for (int j = 0; j < n; j++)
                {

                    //if (Conv.ToInt(tb.Rows[i]["年"]) != yyyy)
                    //{
                    //    yyyy = Conv.ToInt(tb.Rows[i]["年"]);
                    //    //yyborrow = 0;
                    //    //yyloan = 0;
                    //}
                    if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                    {
                        if (is_one == false)
                        {
                            begion_amount = beginbalance;
                            yyborrow = 0;
                            yyloan = 0;
                            is_one = true;
                        }
                    }
                    //if (Conv.ToInt(tb.Rows[i]["月"]) != mm)
                    //{

                    //    DataRow r = tb.NewRow();
                    //    r["年"] = yyyy;
                    //    r["月"] = mm;
                    //    r["日"] = DBNull.Value;
                    //    //r["单据类型"] = DBNull.Value;
                    //    //r["单据号"] = DBNull.Value;
                    //    //r["摘要"] = "本月合计";
                    //    r["借方"] = mmborrow;
                    //    r["贷方"] = mmloan;
                    //    if (beginbalance > 0)
                    //    {
                    //        r["方向"] = "贷";
                    //    }
                    //    else if (beginbalance < 0)
                    //    {
                    //        r["方向"] = "借";
                    //    }
                    //    else
                    //    {
                    //        r["方向"] = "平";
                    //    }
                    //    r["余额"] = Math.Abs(beginbalance);
                    //    tb.Rows.InsertAt(r, i);
                    //    DataRow rr = tb.NewRow();
                    //    rr["年"] = yyyy;
                    //    rr["月"] = mm;
                    //    rr["日"] = DBNull.Value;
                    //    //rr["单据类型"] = DBNull.Value;
                    //    //rr["单据号"] = DBNull.Value;
                    //    //rr["摘要"] = "本年合计";
                    //    rr["借方"] = yyborrow;
                    //    rr["贷方"] = yyloan;
                    //    if (beginbalance > 0)
                    //    {
                    //        rr["方向"] = "贷";
                    //    }
                    //    else if (beginbalance < 0)
                    //    {
                    //        rr["方向"] = "借";
                    //    }
                    //    else
                    //    {
                    //        rr["方向"] = "平";
                    //    }
                    //    rr["余额"] = Math.Abs(beginbalance);

                    //    tb.Rows.InsertAt(rr, i + 1);
                    //    // i += 2;
                    //    mm = Conv.ToInt(tb.Rows[i]["月"]);
                    //    mmborrow = 0.0000m;
                    //    mmloan = 0.0000m;

                    //}
                    if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == -1)
                    {
                        tb.Rows[i]["贷方"] = DBNull.Value;
                        beginbalance -= Conv.ToDecimal(tb.Rows[i]["借方"]);
                        mmborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                        yyborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                        if (beginbalance > 0)
                        {
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                        else if (beginbalance < 0)
                        {
                            tb.Rows[i]["方向"] = "贷";
                            tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                        }
                        else
                        {
                            tb.Rows[i]["方向"] = "平";
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                    }
                    else if (Conv.ToDecimal(tb.Rows[i]["pay_type"]) == 1)
                    {

                        tb.Rows[i]["借方"] = DBNull.Value;
                        beginbalance += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        if (beginbalance > 0)
                        {
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                        else if (beginbalance < 0)
                        {
                            tb.Rows[i]["方向"] = "借";
                            tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                        }
                        else
                        {
                            tb.Rows[i]["方向"] = "平";
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                    }
                    //if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                    //{
                    //    //tb.Rows[i].Delete();
                    //}
                  
                    if (n - 1 == k)
                    {

                        //mm = Conv.ToInt(tb.Rows[i]["月"]);
                        DataRow r = dt.NewRow();
                        r["编码"] = supcust_no;
                        r["名称"] = sup_name;
                        r["期初金额"] = Math.Abs(begion_amount);
                        if (begion_amount > 0)
                            r["期初金额方向"] = "贷";
                        else if (begion_amount < 0)
                            r["期初金额方向"] = "借";
                        else
                            r["期初金额方向"] = "平";
                        r["借方"] = yyborrow;
                        r["贷方"] = yyloan;

                        if (beginbalance > 0)
                        {
                            r["期末金额方向"] = "贷";
                            r["期末金额"] = beginbalance;
                        }
                        else if (beginbalance < 0)
                        {
                            r["期末金额方向"] = "借";
                            r["期末金额"] = Math.Abs(beginbalance);
                        }
                        else
                        {
                            r["期末金额方向"] = "平";
                            r["期末金额"] = beginbalance;
                        }
                        dt.Rows.Add(r);

                    }
                    k++;
                    i++;



                }

            }
            if (isnull == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Conv.ToDecimal(dt.Rows[i]["期初金额"]) == 0 && Conv.ToDecimal(dt.Rows[i]["借方"]) == 0 && Conv.ToDecimal(dt.Rows[i]["贷方"]) == 0 && Conv.ToDecimal(dt.Rows[i]["期末金额"]) == 0)
                    {
                        dt.Rows.RemoveAt(i);
                        i--;
                    }

                }
            }
            return dt;
        }


       public DataTable GetCusAgingGroup(DateTime start_time, string cust_from,  string company_type, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);//借贷 CASE a.pay_type WHEN 1 THEN a.sheet_amount WHEN -1 THEN '' end 
            string sql = @"SELECT
	a.supcust_no 编码, 
    b.sup_name 名称,
    a.pay_type,
    a.oper_date,
    a.sheet_amount,
    isnull(a.paid_amount,0.00) paid_amount,
    a.other1,
    a.trans_no
FROM
	rp_t_accout_payrec_flow a

left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and b.supcust_flag='C'
WHERE
	a.supcust_flag = 'C' and (a.paid_amount+a.free_money)*a.pay_type<a.sheet_amount ";
            if (cust_from != "")
            {
                sql += " and a.supcust_no='" + cust_from + "' ";
            }
            //if (cust_to != "")
            //{
            //    sql += " and a.supcust_no<='" + cust_to + "' ";
            //}
            if (company_type != "")
            {
                sql += " and b.sup_type='" + company_type + "' ";
            }
            sql+="and Convert(varchar(10), a.oper_date ,20 ) <= '" + start_time.ToString("yyyy-MM-dd") + "'";//BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";


            DataTable tbs = db.ExecuteToTable(sql, "a.supcust_no,a.oper_date ", null, page_size, page_index, out total_count);
            DataTable dt = new DataTable();
            dt.Columns.Add("客户编码");
            dt.Columns.Add("客户名称");
            sql = "select * from rp_t_aging_group where supcust_flag='C'";
            DataTable dt1 = db.ExecuteToTable(sql, null);
            List<int> start = new List<int>();
            List<int> end = new List<int>();
            List<string> day = new List<string>();
            List<decimal> yfAmount = new List<decimal>();
            //decimal UnCollectionAmount = 0.0000m;
            //decimal UnArApAmount = 0.0000m;
            //decimal Amount = 0.0000m;
            string days = "0-"+Conv.ToString(dt1.Rows[0]["end_days"])+"天";
            day.Add(days);
            yfAmount.Add(0.0000m);
            dt.Columns.Add(days);
            start.Add(0);
            end.Add(Conv.ToInt(dt1.Rows[0]["end_days"]));
            int m;
            for (m=1;m<dt1.Rows.Count-1;m++)
            {
                days = Conv.ToString(dt1.Rows[m]["start_days"]) + "-" + Conv.ToString(dt1.Rows[m]["end_days"]) + "天";
                day.Add(days);
                yfAmount.Add(0.0000m);
                dt.Columns.Add(days);
                start.Add(Conv.ToInt(dt1.Rows[m]["start_days"]));
                end.Add(Conv.ToInt(dt1.Rows[m]["end_days"]));
            }
            days = "大于"+Conv.ToString(dt1.Rows[m]["start_days"]) + "天";
            day.Add(days);
            yfAmount.Add(0.0000m);
            dt.Columns.Add(days);
            start.Add(Conv.ToInt(dt1.Rows[m]["start_days"]));
            end.Add(0);
            //dt.Columns.Add("收款未核销金额");
            //dt.Columns.Add("冲账未核销金额");
            dt.Columns.Add("其他");
            dt.Columns.Add("应收款余额");
            if (tbs.Rows.Count == 0)
                return dt;
            DataTable tb = tbs.Copy();         
            tb.Clear();
            string supcust_no = Conv.ToString(tbs.Rows[0]["编码"]);
            string sup_name = Conv.ToString(tbs.Rows[0]["名称"]);
            int ss = 0;
            foreach (DataRow dr in tbs.Rows)
            {



                DataRow sr = tb.NewRow();
                sr["编码"] = dr["编码"];
                sr["名称"] = dr["名称"];
                sr["pay_type"] = dr["pay_type"];
                sr["oper_date"] = dr["oper_date"];
                sr["sheet_amount"] = dr["sheet_amount"];
                sr["paid_amount"] = dr["paid_amount"];
                sr["other1"] = dr["other1"];
                sr["trans_no"] = dr["trans_no"];
                tb.Rows.Add(sr);
                if (supcust_no != Conv.ToString(tb.Rows[tb.Rows.Count - 1]["编码"]))
                {

                    //

                    //int i = 0;
                    int n = tb.Rows.Count;
                    //int k = 0;
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (Conv.ToString(tb.Rows[j]["trans_no"]) != "CP" && Conv.ToString(tb.Rows[j]["other1"]) != "ARAP")
                        {
                            DateTime oper_date = Conv.ToDateTime(tb.Rows[j]["oper_date"]);
                            TimeSpan sp = start_time.Subtract(oper_date);
                            int nn = 0;
                            if (sp.Days <= end[nn])
                            {
                                yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) *Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                                
                            }
                            for (nn = 1; nn < start.Count-1; nn++)
                            {
                                if (sp.Days >= start[nn] && sp.Days <= end[nn])
                                {
                                    yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                                }
                            }
                            if (sp.Days >= start[nn])
                            {
                                yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                            }
                            //Amount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);


                        }
                        //else if(Conv.ToString(tb.Rows[j]["trans_no"]) == "CP")
                        //{
                        //    UnCollectionAmount+= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        //    Amount -= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        //} else if(Conv.ToString(tb.Rows[j]["other1"]) == "ARAP")
                        //{
                        //    UnArApAmount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        //    Amount -= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        //}
                       // i++;
                        if (n - 2 == j)
                        {

                            ////mm = Conv.ToInt(tb.Rows[i]["月"]); 
                            decimal sum = 0;
                            DataRow r = dt.NewRow();
                            r["客户编码"] = supcust_no;
                            r["客户名称"] = sup_name;
                            for(int i = 0; i < day.Count; i++)
                            {
                                r[day[i]] = yfAmount[i];
                                sum+= yfAmount[i];
                                yfAmount[i] = 0.0000m;

                            }
                            //r["收款未核销金额"] = UnCollectionAmount;
                            //r["冲账未核销金额"] = UnArApAmount;
                            int count;
                           DataTable temp= GetCusBalance(start_time.AddDays(-60), start_time, supcust_no, "", 1, 1, int.MaxValue, out count);
                            decimal amount=0;
                            if(Conv.ToString(temp.Rows[0]["期末金额方向"]).Trim()=="借")
                            amount = Conv.ToDecimal(temp.Rows[0]["期末金额"]);
                            else
                                amount = Conv.ToDecimal(temp.Rows[0]["期末金额"])*-1;
                            r["其他"] = amount - sum;
                            r["应收款余额"] = amount;
                            dt.Rows.Add(r);

                        }



                    }

                    tb.Clear();
                    //UnCollectionAmount = 0.0000m;
                    //UnArApAmount = 0.0000m;
                    //Amount = 0.0000m;
                    supcust_no = Conv.ToString(dr["编码"]);
                    sup_name = Conv.ToString(dr["名称"]);
                    DataRow sr1 = tb.NewRow();
                    sr1["编码"] = dr["编码"];
                    sr1["名称"] = dr["名称"];
                    sr1["pay_type"] = dr["pay_type"];
                    sr1["oper_date"] = dr["oper_date"];
                    sr1["sheet_amount"] = dr["sheet_amount"];
                    sr1["paid_amount"] = dr["paid_amount"];
                    sr1["other1"] = dr["other1"];
                    sr1["trans_no"] = dr["trans_no"];
                    tb.Rows.Add(sr1);

                }



                ss++;
            }
            if (true)
            {
                int n = tb.Rows.Count;
                //int k = 0;
                for (int j = 0; j < n; j++)
                {
                    if (Conv.ToString(tb.Rows[j]["trans_no"]) != "CP" && Conv.ToString(tb.Rows[j]["other1"]) != "ARAP")
                    {
                        DateTime oper_date = Conv.ToDateTime(tb.Rows[j]["oper_date"]);
                        TimeSpan sp = start_time.Subtract(oper_date);
                        int nn = 0;
                        if (sp.Days <= end[nn])
                        {
                            yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);

                        }
                        for (nn = 1; nn < start.Count - 1; nn++)
                        {
                            if (sp.Days >= start[nn] && sp.Days <= end[nn])
                            {
                                yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                            }
                        }
                        if (sp.Days >= start[nn])
                        {
                            yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        }
                        //Amount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);


                    }
                    //else if (Conv.ToString(tb.Rows[j]["trans_no"]) == "CP")
                    //{
                    //    UnCollectionAmount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                    //    Amount -= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                    //}
                    //else if (Conv.ToString(tb.Rows[j]["other1"]) == "ARAP")
                    //{
                    //    UnArApAmount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                    //    Amount -= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                    //}
                    // i++;
                    if (n - 1 == j)
                    {

                        decimal sum = 0;
                        DataRow r = dt.NewRow();
                        r["客户编码"] = supcust_no;
                        r["客户名称"] = sup_name;
                        for (int i = 0; i < day.Count; i++)
                        {
                            r[day[i]] = yfAmount[i];
                            sum += yfAmount[i];
                            yfAmount[i] = 0.0000m;

                        }
                        //r["收款未核销金额"] = UnCollectionAmount;
                        //r["冲账未核销金额"] = UnArApAmount;
                        int count;
                        DataTable temp = GetCusBalance(start_time.AddDays(-60), start_time, supcust_no, "", 1, 1, int.MaxValue, out count);

                        decimal amount = 0;
                        if (Conv.ToString(temp.Rows[0]["期末金额方向"]).Trim() == "借")
                            amount = Conv.ToDecimal(temp.Rows[0]["期末金额"]);
                        else
                            amount = Conv.ToDecimal(temp.Rows[0]["期末金额"]) * -1;
                        r["其他"] = amount - sum;
                        r["应收款余额"] = amount;
                        dt.Rows.Add(r);

                    }



                }

                //tb.Clear();
                //UnCollectionAmount = 0;
                //UnArApAmount = 0;
                //Amount = 0;

            }

            return dt;
        }

        public DataTable GetSupAgingGroup(DateTime start_time, string cust_from,  string company_type, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);//借贷 CASE a.pay_type WHEN 1 THEN a.sheet_amount WHEN -1 THEN '' end 
            string sql = @"SELECT
	a.supcust_no 编码, 
    b.sup_name 名称,
    a.pay_type,
    a.oper_date,
    a.sheet_amount,
    isnull(a.paid_amount,0.00) paid_amount,
    a.other1,
    a.trans_no
FROM
	rp_t_accout_payrec_flow a

left join bi_t_supcust_info b on a.supcust_no=b.supcust_no and b.supcust_flag='S'
WHERE
	a.supcust_flag = 'S' and (a.paid_amount+a.free_money)*a.pay_type<a.sheet_amount ";
            if (cust_from != "")
            {
                sql += " and a.supcust_no='" + cust_from + "' ";
            }
          
            if (company_type != "")
            {
                sql += " and b.sup_type='" + company_type + "' ";
            }
            sql += "and Convert(varchar(10), a.oper_date ,20 ) <= '" + start_time.ToString("yyyy-MM-dd") + "'";//BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";


            DataTable tbs = db.ExecuteToTable(sql, "a.supcust_no,a.oper_date ", null, page_size, page_index, out total_count);
            DataTable dt = new DataTable();
            dt.Columns.Add("供应商编码");
            dt.Columns.Add("供应商名称");
            sql = "select * from rp_t_aging_group where supcust_flag='S'";
            DataTable dt1 = db.ExecuteToTable(sql, null);
            List<int> start = new List<int>();
            List<int> end = new List<int>();
            List<string> day = new List<string>();
            List<decimal> yfAmount = new List<decimal>();
            //decimal UnCollectionAmount = 0.0000m;
            //decimal UnArApAmount = 0.0000m;
            //decimal Amount = 0.0000m;
            string days = "0-" + Conv.ToString(dt1.Rows[0]["end_days"]) + "天";
            day.Add(days);
            yfAmount.Add(0.0000m);
            dt.Columns.Add(days);
            start.Add(0);
            end.Add(Conv.ToInt(dt1.Rows[0]["end_days"]));
            int m;
            for (m = 1; m < dt1.Rows.Count - 1; m++)
            {
                days = Conv.ToString(dt1.Rows[m]["start_days"]) + "-" + Conv.ToString(dt1.Rows[m]["end_days"]) + "天";
                day.Add(days);
                yfAmount.Add(0.0000m);
                dt.Columns.Add(days);
                start.Add(Conv.ToInt(dt1.Rows[m]["start_days"]));
                end.Add(Conv.ToInt(dt1.Rows[m]["end_days"]));
            }
            days = "大于" + Conv.ToString(dt1.Rows[m]["start_days"]) + "天";
            day.Add(days);
            yfAmount.Add(0.0000m);
            dt.Columns.Add(days);
            start.Add(Conv.ToInt(dt1.Rows[m]["start_days"]));
            end.Add(0);
            //dt.Columns.Add("付款未核销金额");
            //dt.Columns.Add("冲账未核销金额");
            dt.Columns.Add("其他");
            dt.Columns.Add("应付款余额");
            if (tbs.Rows.Count == 0)
                return dt;
            DataTable tb = tbs.Copy();
            tb.Clear();
            string supcust_no = Conv.ToString(tbs.Rows[0]["编码"]);
            string sup_name = Conv.ToString(tbs.Rows[0]["名称"]);
            int ss = 0;
            foreach (DataRow dr in tbs.Rows)
            {



                DataRow sr = tb.NewRow();
                sr["编码"] = dr["编码"];
                sr["名称"] = dr["名称"];
                sr["pay_type"] = dr["pay_type"];
                sr["oper_date"] = dr["oper_date"];
                sr["sheet_amount"] = dr["sheet_amount"];
                sr["paid_amount"] = dr["paid_amount"];
                sr["other1"] = dr["other1"];
                sr["trans_no"] = dr["trans_no"];
                tb.Rows.Add(sr);
                if (supcust_no != Conv.ToString(tb.Rows[tb.Rows.Count - 1]["编码"]))
                {

                    //

                    //int i = 0;
                    int n = tb.Rows.Count;
                    //int k = 0;
                    for (int j = 0; j < n - 1; j++)
                    {
                        if (Conv.ToString(tb.Rows[j]["trans_no"]) != "RP" && Conv.ToString(tb.Rows[j]["other1"]) != "ARAP")
                        {
                            DateTime oper_date = Conv.ToDateTime(tb.Rows[j]["oper_date"]);
                            TimeSpan sp = start_time.Subtract(oper_date);
                            int nn = 0;
                            if (sp.Days <= end[nn])
                            {
                                yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);

                            }
                            for (nn = 1; nn < start.Count - 1; nn++)
                            {
                                if (sp.Days >= start[nn] && sp.Days <= end[nn])
                                {
                                    yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                                }
                            }
                            if (sp.Days >= start[nn])
                            {
                                yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                            }
                            //Amount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);


                        }
                        //else if (Conv.ToString(tb.Rows[j]["trans_no"]) == "RP")
                        //{
                        //    UnCollectionAmount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        //    Amount -= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        //}
                        //else if (Conv.ToString(tb.Rows[j]["other1"]) == "ARAP")
                        //{
                        //    UnArApAmount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        //    Amount -= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        //}
                        // i++;
                        if (n - 2 == j)
                        {

                            ////mm = Conv.ToInt(tb.Rows[i]["月"]);    
                            decimal sum = 0;
                            DataRow r = dt.NewRow();
                            r["供应商编码"] = supcust_no;
                            r["供应商名称"] = sup_name;
                            for (int i = 0; i < day.Count; i++)
                            {
                                r[day[i]] = yfAmount[i];
                                sum += yfAmount[i];
                                yfAmount[i] = 0.0000m;
                                

                            }
                            int count;
                            DataTable temp = GetSupBalance(start_time.AddDays(-60), start_time, supcust_no, "", 1, 1, int.MaxValue, out count);
                            decimal amount = 0;
                            if (Conv.ToString(temp.Rows[0]["期末金额方向"]).Trim() == "贷")
                                amount = Conv.ToDecimal(temp.Rows[0]["期末金额"]);
                            else
                                amount = Conv.ToDecimal(temp.Rows[0]["期末金额"]) * -1;
                            r["其他"] = amount - sum;
                            r["应付款余额"] = amount;
                            dt.Rows.Add(r);

                        }



                    }

                    tb.Clear();
                    //UnCollectionAmount = 0.0000m;
                    //UnArApAmount = 0.0000m;
                    //Amount = 0.0000m;
                    supcust_no = Conv.ToString(dr["编码"]);
                    sup_name = Conv.ToString(dr["名称"]);
                    DataRow sr1 = tb.NewRow();
                    sr1["编码"] = dr["编码"];
                    sr1["名称"] = dr["名称"];
                    sr1["pay_type"] = dr["pay_type"];
                    sr1["oper_date"] = dr["oper_date"];
                    sr1["sheet_amount"] = dr["sheet_amount"];
                    sr1["paid_amount"] = dr["paid_amount"];
                    sr1["other1"] = dr["other1"];
                    sr1["trans_no"] = dr["trans_no"];
                    tb.Rows.Add(sr1);

                }



                ss++;
            }
            if (true)
            {
                int n = tb.Rows.Count;
                //int k = 0;
                for (int j = 0; j < n; j++)
                {
                    if (Conv.ToString(tb.Rows[j]["trans_no"]) != "RP" && Conv.ToString(tb.Rows[j]["other1"]) != "ARAP")
                    {
                        DateTime oper_date = Conv.ToDateTime(tb.Rows[j]["oper_date"]);
                        TimeSpan sp = start_time.Subtract(oper_date);
                        int nn = 0;
                        if (sp.Days <= end[nn])
                        {
                            yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);

                        }
                        for (nn = 1; nn < start.Count - 1; nn++)
                        {
                            if (sp.Days >= start[nn] && sp.Days <= end[nn])
                            {
                                yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                            }
                        }
                        if (sp.Days >= start[nn])
                        {
                            yfAmount[nn] += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                        }
                        //Amount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);


                    }
                    //else if (Conv.ToString(tb.Rows[j]["trans_no"]) == "RP")
                    //{
                    //    UnCollectionAmount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                    //    Amount -= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                    //}
                    //else if (Conv.ToString(tb.Rows[j]["other1"]) == "ARAP")
                    //{
                    //    UnArApAmount += Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                    //    Amount -= Conv.ToDecimal(tb.Rows[j]["pay_type"]) * Conv.ToDecimal(tb.Rows[j]["sheet_amount"]) - Conv.ToDecimal(tb.Rows[j]["paid_amount"]);
                    //}
                    // i++;
                    if (n - 1 == j)
                    {

                        decimal sum = 0;
                        DataRow r = dt.NewRow();
                        r["供应商编码"] = supcust_no;
                        r["供应商名称"] = sup_name;
                        for (int i = 0; i < day.Count; i++)
                        {
                            r[day[i]] = yfAmount[i];
                            sum += yfAmount[i];
                            yfAmount[i] = 0.0000m;


                        }
                        int count;
                        DataTable temp = GetSupBalance(start_time.AddDays(-60), start_time, supcust_no, "", 1, 1, int.MaxValue, out count);
                        decimal amount = 0;
                        if (Conv.ToString(temp.Rows[0]["期末金额方向"]).Trim() == "贷")
                            amount = Conv.ToDecimal(temp.Rows[0]["期末金额"]);
                        else
                            amount = Conv.ToDecimal(temp.Rows[0]["期末金额"]) * -1;
                        r["其他"] = amount - sum;
                        r["应付款余额"] = amount;
                        dt.Rows.Add(r);

                    }



                }

                //tb.Clear();
                //UnCollectionAmount = 0;
                //UnArApAmount = 0;
                //Amount = 0;

            }

            return dt;
        }
        #endregion
        #region 出纳管理
        public DataTable GetBankCashDetailed(DateTime start_time, DateTime end_time, string visa_id, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);//借贷 CASE a.pay_type WHEN 1 THEN a.sheet_amount WHEN -1 THEN '' end 
            string sql = @"select * from (SELECT
    datepart(yy,a.oper_date) 年,
    datepart(mm,a.oper_date) 月,
    datepart(dd,a.oper_date) 日,
    a.bill_flag 单据类型,
    a.sheet_no 单据号,
    a.other1 摘要,
    a.oper_date,
    a.bill_total as 借方,
    a.bill_total as 贷方,
    '借' as 方向,
    0.0 as 余额,
    a.visa_id
    FROM
    bank_t_cash_master a
WHERE  a.approve_flag='1' and
 Convert(varchar(10), a.oper_date ,20 ) <= '" + end_time.ToString("yyyy-MM-dd") + "'";
            sql += " AND a.visa_id = '" + visa_id + "' ";
            sql += @"  union all
    SELECT
	datepart(yy,a.oper_date) 年,
    datepart(mm,a.oper_date) 月,
    datepart(dd,a.oper_date) 日,
    a.bill_flag 单据类型,
    a.sheet_no 单据号,
    a.other1 摘要,
    a.oper_date,
    -1*a.bill_total as 借方,
    -1*a.bill_total as 贷方,
    '借' as 方向,
    0.0 as 余额,
    a.visa_in visa_id
    
FROM
	bank_t_cash_master a
WHERE  isnull(a.visa_in,'')<>'' and  a.approve_flag='1' and
 Convert(varchar(10), a.oper_date ,20 ) <= '" + end_time.ToString("yyyy-MM-dd") + "'";
            sql += " AND a.visa_in = '" + visa_id + "' ";
            sql += ")t1  ORDER BY oper_date asc";


            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = int.MaxValue;
            if (tb.Rows.Count == 0)
            {
                return tb;
            }
            sql = "select * from rp_t_bank_beginbalance where visa_id='" + visa_id + "' ";
            DataTable tb1 = db.ExecuteToTable(sql, null);
            int mm = Conv.ToInt(tb.Rows[0]["月"]);
            int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
            decimal mmborrow = 0.0000m;//月借累计
            decimal mmloan = 0.0000m;//月贷累计
            decimal yyborrow = 0.0000m;//贷借累计
            decimal yyloan = 0.0000m;//年贷累计
            decimal beginbalance = 0.0000m;
            List<int> ls = new List<int>();
            if (tb1.Rows.Count > 0)
            {
                if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 1)
                    beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"]) + 0.0000m;
                else
                    beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
            }
            decimal begion_amount = 0.0000m;
            bool is_one = false;
            int i = 0;
            int n = tb.Rows.Count;
            int k = 0;
            for (int j = 0; j < n; j++)
            {

                if (Conv.ToInt(tb.Rows[i]["年"]) != yyyy)
                {
                    yyyy = Conv.ToInt(tb.Rows[i]["年"]);
                    yyborrow = 0.0000m;
                    yyloan = 0.0000m;
                }
                if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                {
                    if (is_one == false)
                    {
                        begion_amount = beginbalance;
                        is_one = true;
                    }
                }

                if (Conv.ToInt(tb.Rows[i]["月"]) != mm)
                {
                    if (Conv.ToDateTime(tb.Rows[i - 1]["oper_date"]) >= start_time)
                    {


                        DataRow r = tb.NewRow();
                        r["年"] = yyyy;
                        r["月"] = mm;
                        r["日"] = DBNull.Value;
                        r["单据类型"] = DBNull.Value;
                        r["单据号"] = DBNull.Value;
                        r["摘要"] = "本月合计";
                        r["借方"] = mmborrow;
                        r["贷方"] = mmloan;
                        if (beginbalance > 0)
                        {
                            r["方向"] = "借";
                        }
                        else if (beginbalance < 0)
                        {
                            r["方向"] = "贷";
                        }
                        else
                        {
                            r["方向"] = "平";
                        }
                        r["余额"] = Math.Abs(beginbalance);
                        tb.Rows.InsertAt(r, i);
                        DataRow rr = tb.NewRow();
                        rr["年"] = yyyy;
                        rr["月"] = mm;
                        rr["日"] = DBNull.Value;
                        rr["单据类型"] = DBNull.Value;
                        rr["单据号"] = DBNull.Value;
                        rr["摘要"] = "本年合计";
                        rr["借方"] = yyborrow;
                        rr["贷方"] = yyloan;
                        if (beginbalance > 0)
                        {
                            rr["方向"] = "借";
                        }
                        else if (beginbalance < 0)
                        {
                            rr["方向"] = "贷";
                        }
                        else
                        {
                            rr["方向"] = "平";
                        }
                        rr["余额"] = Math.Abs(beginbalance);

                        tb.Rows.InsertAt(rr, i + 1);
                        i += 2;

                    }
                    mm = Conv.ToInt(tb.Rows[i]["月"]);
                    mmborrow = 0.0000m;
                    mmloan = 0.0000m;

                }
                if (Conv.ToDecimal(tb.Rows[i]["借方"]) >= 0)
                {
                    if (Conv.ToString(tb.Rows[i]["单据类型"]) == "S")
                    {
                        tb.Rows[i]["借方"] = DBNull.Value;
                        tb.Rows[i]["贷方"] = Math.Abs(Conv.ToDecimal(tb.Rows[i]["贷方"]));
                        beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    }
                    else
                    {
                        tb.Rows[i]["贷方"] = DBNull.Value;
                        beginbalance += Conv.ToDecimal(tb.Rows[i]["借方"]);
                        mmborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                        yyborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                    }
                    switch (Conv.ToString(tb.Rows[i]["单据类型"]))
                    {
                        case "K":
                            tb.Rows[i]["单据类型"] = "客户收款单";
                            break;
                        case "G":
                            tb.Rows[i]["单据类型"] = "供应商付款单";
                            break;
                        case "C":
                            tb.Rows[i]["单据类型"] = "其他收入单";
                            break;
                        case "S":
                            tb.Rows[i]["单据类型"] = "其他支出单";
                            break;
                        case "B":
                            tb.Rows[i]["单据类型"] = "现金银行转账单";
                            break;
                    }

                    if (beginbalance > 0)
                    {
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                    else if (beginbalance < 0)
                    {
                        tb.Rows[i]["方向"] = "贷";
                        tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                    }
                    else
                    {
                        tb.Rows[i]["方向"] = "平";
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                }
                else if (Conv.ToDecimal(tb.Rows[i]["借方"]) < 0)
                {

                    tb.Rows[i]["借方"] = DBNull.Value;
                    tb.Rows[i]["贷方"] = Math.Abs(Conv.ToDecimal(tb.Rows[i]["贷方"]));
                    beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                    switch (Conv.ToString(tb.Rows[i]["单据类型"]))
                    {
                        case "K":
                            tb.Rows[i]["单据类型"] = "客户收款单";
                            break;
                        case "G":
                            tb.Rows[i]["单据类型"] = "供应商付款单";
                            break;
                        case "C":
                            tb.Rows[i]["单据类型"] = "其他收入单";
                            break;
                        case "S":
                            tb.Rows[i]["单据类型"] = "其他支出单";
                            break;
                        case "B":
                            tb.Rows[i]["单据类型"] = "现金银行转账单";
                            break;
                    }
                    if (beginbalance > 0)
                    {
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                    else if (beginbalance < 0)
                    {
                        tb.Rows[i]["方向"] = "贷";
                        tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                    }
                    else
                    {
                        tb.Rows[i]["方向"] = "平";
                        tb.Rows[i]["余额"] = beginbalance;
                    }
                }
                if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                {
                    ls.Add(i);
                    //tb.Rows[i].Delete();
                }
                k++;
                i++;
                if (n == k)
                {
                    //mm = Conv.ToInt(tb.Rows[i]["月"]);
                    DataRow r = tb.NewRow();
                    r["年"] = yyyy;
                    r["月"] = mm;
                    r["日"] = DBNull.Value;
                    r["单据类型"] = DBNull.Value;
                    r["单据号"] = DBNull.Value;
                    r["摘要"] = "本月合计";
                    r["借方"] = mmborrow;
                    r["贷方"] = mmloan;
                    if (beginbalance > 0)
                    {
                        r["方向"] = "借";
                    }
                    else if (beginbalance < 0)
                    {
                        r["方向"] = "贷";
                    }
                    else
                    {
                        r["方向"] = "平";
                    }
                    r["余额"] = Math.Abs(beginbalance);
                    tb.Rows.Add(r);
                    DataRow rr = tb.NewRow();
                    rr["年"] = yyyy;
                    rr["月"] = mm;
                    rr["日"] = DBNull.Value;
                    rr["单据类型"] = DBNull.Value;
                    rr["单据号"] = DBNull.Value;
                    rr["摘要"] = "本年合计";
                    rr["借方"] = yyborrow;
                    rr["贷方"] = yyloan;
                    if (beginbalance > 0)
                    {
                        rr["方向"] = "借";
                    }
                    else if (beginbalance < 0)
                    {
                        rr["方向"] = "贷";
                    }
                    else
                    {
                        rr["方向"] = "平";
                    }
                    rr["余额"] = Math.Abs(beginbalance);

                    tb.Rows.Add(rr);
                }



            }
            foreach (int j in ls)
            {
                tb.Rows[j].Delete();
            }
            tb.AcceptChanges();
            DataRow s = tb.NewRow();
            s["年"] = DBNull.Value;
            s["月"] = DBNull.Value;
            s["日"] = DBNull.Value;
            s["单据类型"] = DBNull.Value;
            s["单据号"] = DBNull.Value;
            s["摘要"] = "期初金额";
            s["借方"] = DBNull.Value;
            s["贷方"] = DBNull.Value;
            if (begion_amount > 0)
            {
                s["方向"] = "借";
            }
            else if (begion_amount < 0)
            {
                s["方向"] = "贷";
            }
            else
            {
                s["方向"] = "平";
            }
            s["余额"] = Math.Abs(begion_amount);
            tb.Rows.InsertAt(s, 0);
            return tb;
        }

        public DataTable GetBankCashBalance(DateTime start_time, DateTime end_time, string visa_id1, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);//借贷 CASE a.pay_type WHEN 1 THEN a.sheet_amount WHEN -1 THEN '' end 
            string sql = @"select * from (SELECT
    
    datepart(yy,a.oper_date) 年,
    datepart(mm,a.oper_date) 月,
    datepart(dd,a.oper_date) 日,
    a.bill_flag 单据类型,
    a.sheet_no 单据号,
    a.other1 摘要,
    a.oper_date,
    a.bill_total as 借方,
    a.bill_total as 贷方,
    '借' as 方向,
    0.0 as 余额,
    a.visa_id 编码,
    b.visa_nm 名称
    FROM
    bank_t_cash_master a
    left join bi_t_bank_info b on a.visa_id=b.visa_id
WHERE  a.approve_flag='1' and
 Convert(varchar(10), a.oper_date ,20 ) <= '" + end_time.ToString("yyyy-MM-dd") + "'";
            if (visa_id1 != "")
            {
                sql += " and a.visa_id='" + visa_id1 + "' ";
            }
            sql += @"  union all
    SELECT
    
	datepart(yy,a.oper_date) 年,
    datepart(mm,a.oper_date) 月,
    datepart(dd,a.oper_date) 日,
    a.bill_flag 单据类型,
    a.sheet_no 单据号,
    a.other1 摘要,
    a.oper_date,
    -1*a.bill_total as 借方,
    -1*a.bill_total as 贷方,
    '借' as 方向,
    0.0 as 余额,
    a.visa_in 编码,
    b.visa_nm 名称
    
FROM
	bank_t_cash_master a
left join bi_t_bank_info b on a.visa_in=b.visa_id
WHERE  isnull(a.visa_in,'')<>'' and  a.approve_flag='1' and
 Convert(varchar(10), a.oper_date ,20 ) <= '" + end_time.ToString("yyyy-MM-dd") + "'";
            if (visa_id1 != "")
            {
                sql += " and a.visa_in='" + visa_id1 + "' ";
            }
            sql += ")t1  ORDER BY 编码 asc ,oper_date asc";

            DataTable tbs = db.ExecuteToTable(sql, null);
            total_count = int.MaxValue;
            DataTable dt = new DataTable();
            dt.Columns.Add("编码");
            dt.Columns.Add("名称");
            dt.Columns.Add("期初金额方向");
            dt.Columns.Add("期初金额");
            dt.Columns.Add("借方");
            dt.Columns.Add("贷方");
            dt.Columns.Add("期末金额方向");
            dt.Columns.Add("期末金额");
            if (tbs.Rows.Count == 0)
            {
                return dt;
            }
            DataTable tb = tbs.Copy();
            tb.Clear();
            string visa_id = Conv.ToString(tbs.Rows[0]["编码"]);
            string visa_nm = Conv.ToString(tbs.Rows[0]["名称"]);
            int ss = 0;
            foreach (DataRow dr in tbs.Rows)
            {



                DataRow sr = tb.NewRow();
                sr["编码"] = dr["编码"];
                sr["名称"] = dr["名称"];
                sr["年"] = dr["年"];
                sr["月"] = dr["月"];
                sr["日"] = dr["日"];
                sr["oper_date"] = dr["oper_date"];
                sr["借方"] = dr["借方"];
                sr["贷方"] = dr["贷方"];
                sr["方向"] = dr["方向"];
                sr["余额"] = dr["余额"];
                sr["单据类型"] = dr["单据类型"];
                tb.Rows.Add(sr);
                if (visa_id != Conv.ToString(tb.Rows[tb.Rows.Count - 1]["编码"]))
                {

                    sql = "select * from rp_t_bank_beginbalance where visa_id='" + visa_id + "' ";
                    DataTable tb1 = db.ExecuteToTable(sql, null);
                    int mm = Conv.ToInt(tb.Rows[0]["月"]);
                    int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
                    decimal mmborrow = 0.0000m;//月借累计
                    decimal mmloan = 0.0000m;//月贷累计
                    decimal yyborrow = 0.0000m;//贷借累计
                    decimal yyloan = 0.0000m;//年贷累计
                    decimal beginbalance = 0.0000m;
                    if (tb1.Rows.Count > 0)
                    {
                        if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 1)
                            beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"]) + 0.0000m;
                        else
                            beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
                    }
                    decimal begion_amount = 0.0000m;
                    bool is_one = false;
                    int i = 0;
                    int n = tb.Rows.Count;
                    int k = 0;
                    for (int j = 0; j < n - 1; j++)
                    {

                        if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                        {
                            if (is_one == false)
                            {
                                begion_amount = beginbalance;
                                is_one = true;
                            }
                        }

                        if (Conv.ToDecimal(tb.Rows[i]["借方"]) >= 0)
                        {
                            if (Conv.ToString(tb.Rows[i]["单据类型"]) == "S")
                            {
                                tb.Rows[i]["借方"] = DBNull.Value;
                                tb.Rows[i]["贷方"] = Math.Abs(Conv.ToDecimal(tb.Rows[i]["贷方"]));
                                beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                                mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                                yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            }
                            else
                            {
                                tb.Rows[i]["贷方"] = DBNull.Value;
                                beginbalance += Conv.ToDecimal(tb.Rows[i]["借方"]);
                                mmborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                                yyborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                            }
                            switch (Conv.ToString(tb.Rows[i]["单据类型"]))
                            {
                                case "K":
                                    tb.Rows[i]["单据类型"] = "客户收款单";
                                    break;
                                case "G":
                                    tb.Rows[i]["单据类型"] = "供应商付款单";
                                    break;
                                case "C":
                                    tb.Rows[i]["单据类型"] = "其他收入单";
                                    break;
                                case "S":
                                    tb.Rows[i]["单据类型"] = "其他支出单";
                                    break;
                                case "B":
                                    tb.Rows[i]["单据类型"] = "现金银行转账单";
                                    break;
                            }

                            if (beginbalance > 0)
                            {
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                            else if (beginbalance < 0)
                            {
                                tb.Rows[i]["方向"] = "贷";
                                tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                            }
                            else
                            {
                                tb.Rows[i]["方向"] = "平";
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                        }
                        else if (Conv.ToDecimal(tb.Rows[i]["借方"]) < 0)
                        {

                            tb.Rows[i]["借方"] = DBNull.Value;
                            tb.Rows[i]["贷方"] = Math.Abs(Conv.ToDecimal(tb.Rows[i]["贷方"]));
                            beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            switch (Conv.ToString(tb.Rows[i]["单据类型"]))
                            {
                                case "K":
                                    tb.Rows[i]["单据类型"] = "客户收款单";
                                    break;
                                case "G":
                                    tb.Rows[i]["单据类型"] = "供应商付款单";
                                    break;
                                case "C":
                                    tb.Rows[i]["单据类型"] = "其他收入单";
                                    break;
                                case "S":
                                    tb.Rows[i]["单据类型"] = "其他支出单";
                                    break;
                                case "B":
                                    tb.Rows[i]["单据类型"] = "现金银行转账单";
                                    break;
                            }
                            if (beginbalance > 0)
                            {
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                            else if (beginbalance < 0)
                            {
                                tb.Rows[i]["方向"] = "贷";
                                tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                            }
                            else
                            {
                                tb.Rows[i]["方向"] = "平";
                                tb.Rows[i]["余额"] = beginbalance;
                            }
                        }
                        if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                        {
                            //tb.Rows[i].Delete();
                        }
                        k++;
                        i++;
                        if (n - 1 == k)
                        {

                            //mm = Conv.ToInt(tb.Rows[i]["月"]);
                            DataRow r = dt.NewRow();
                            r["编码"] = visa_id;
                            r["名称"] = visa_nm;
                            r["期初金额"] = begion_amount;
                            if (begion_amount > 0)
                                r["期初金额方向"] = "借";
                            else if (begion_amount < 0)
                                r["期初金额方向"] = "贷";
                            else
                                r["期初金额方向"] = "平";
                            r["借方"] = yyborrow;
                            r["贷方"] = yyloan;
                            r["期末金额"] = Math.Abs(beginbalance);
                            if (beginbalance > 0)
                                r["期末金额方向"] = "借";
                            else if (beginbalance < 0)
                                r["期末金额方向"] = "贷";
                            else
                                r["期末金额方向"] = "平";
                            dt.Rows.Add(r);

                        }



                    }


                    tb.Clear();
                    visa_id = Conv.ToString(dr["编码"]);
                    visa_nm = Conv.ToString(dr["名称"]);
                    DataRow sr1 = tb.NewRow();
                    sr1["编码"] = dr["编码"];
                    sr1["名称"] = dr["名称"];
                    sr1["年"] = dr["年"];
                    sr1["月"] = dr["月"];
                    sr1["日"] = dr["日"];
                    sr1["oper_date"] = dr["oper_date"];
                    sr1["借方"] = dr["借方"];
                    sr1["贷方"] = dr["贷方"];
                    sr1["方向"] = dr["方向"];
                    sr1["余额"] = dr["余额"];
                    tb.Rows.Add(sr1);
                    //yyborrow = 0;
                    //yyloan = 0;

                }



                ss++;
            }

            if (true)
            {
                sql = "select * from rp_t_bank_beginbalance where visa_id='" + visa_id + "' ";
                DataTable tb1 = db.ExecuteToTable(sql, null);
                int mm = Conv.ToInt(tb.Rows[0]["月"]);
                int yyyy = Conv.ToInt(tb.Rows[0]["年"]);
                decimal mmborrow = 0.0000m;//月借累计
                decimal mmloan = 0.0000m;//月贷累计
                decimal yyborrow = 0.0000m;//贷借累计
                decimal yyloan = 0.0000m;//年贷累计
                decimal beginbalance = 0.0000m;
                if (tb1.Rows.Count > 0)
                {
                    if (Conv.ToInt(tb1.Rows[0]["pay_kind"]) == 1)
                        beginbalance = Conv.ToDecimal(tb1.Rows[0]["begin_balance"]) + 0.0000m;
                    else
                        beginbalance = 0.0000m - Conv.ToDecimal(tb1.Rows[0]["begin_balance"]);
                }
                decimal begion_amount = 0.0000m;
                bool is_one = false;
                int i = 0;
                int n = tb.Rows.Count;
                int k = 0;
                for (int j = 0; j < n; j++)
                {

                    //if (Conv.ToInt(tb.Rows[i]["年"]) != yyyy)
                    //{
                    //    yyyy = Conv.ToInt(tb.Rows[i]["年"]);
                    //    //yyborrow = 0;
                    //    //yyloan = 0;
                    //}
                    if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) > start_time)
                    {
                        if (is_one == false)
                        {
                            begion_amount = beginbalance;
                            is_one = true;
                        }
                    }
                    if (Conv.ToDecimal(tb.Rows[i]["借方"]) >= 0)
                    {
                        if (Conv.ToString(tb.Rows[i]["单据类型"]) == "S")
                        {
                            tb.Rows[i]["借方"] = DBNull.Value;
                            tb.Rows[i]["贷方"] = Math.Abs(Conv.ToDecimal(tb.Rows[i]["贷方"]));
                            beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                            yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        }
                        else
                        {
                            tb.Rows[i]["贷方"] = DBNull.Value;
                            beginbalance += Conv.ToDecimal(tb.Rows[i]["借方"]);
                            mmborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                            yyborrow += Conv.ToDecimal(tb.Rows[i]["借方"]);
                        }
                        switch (Conv.ToString(tb.Rows[i]["单据类型"]))
                        {
                            case "K":
                                tb.Rows[i]["单据类型"] = "客户收款单";
                                break;
                            case "G":
                                tb.Rows[i]["单据类型"] = "供应商付款单";
                                break;
                            case "C":
                                tb.Rows[i]["单据类型"] = "其他收入单";
                                break;
                            case "S":
                                tb.Rows[i]["单据类型"] = "其他支出单";
                                break;
                            case "B":
                                tb.Rows[i]["单据类型"] = "现金银行转账单";
                                break;
                        }

                        if (beginbalance > 0)
                        {
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                        else if (beginbalance < 0)
                        {
                            tb.Rows[i]["方向"] = "贷";
                            tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                        }
                        else
                        {
                            tb.Rows[i]["方向"] = "平";
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                    }
                    else if (Conv.ToDecimal(tb.Rows[i]["借方"]) < 0)
                    {

                        tb.Rows[i]["借方"] = DBNull.Value;
                        tb.Rows[i]["贷方"] = Math.Abs(Conv.ToDecimal(tb.Rows[i]["贷方"]));
                        beginbalance -= Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        mmloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        yyloan += Conv.ToDecimal(tb.Rows[i]["贷方"]);
                        switch (Conv.ToString(tb.Rows[i]["单据类型"]))
                        {
                            case "K":
                                tb.Rows[i]["单据类型"] = "客户收款单";
                                break;
                            case "G":
                                tb.Rows[i]["单据类型"] = "供应商付款单";
                                break;
                            case "C":
                                tb.Rows[i]["单据类型"] = "其他收入单";
                                break;
                            case "S":
                                tb.Rows[i]["单据类型"] = "其他支出单";
                                break;
                            case "B":
                                tb.Rows[i]["单据类型"] = "现金银行转账单";
                                break;
                        }
                        if (beginbalance > 0)
                        {
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                        else if (beginbalance < 0)
                        {
                            tb.Rows[i]["方向"] = "贷";
                            tb.Rows[i]["余额"] = Math.Abs(beginbalance);

                        }
                        else
                        {
                            tb.Rows[i]["方向"] = "平";
                            tb.Rows[i]["余额"] = beginbalance;
                        }
                    }
                    if (Conv.ToDateTime(tb.Rows[i]["oper_date"]) < start_time)
                    {
                        //tb.Rows[i].Delete();
                    }
                    k++;
                    i++;
                    if (n == k)
                    {

                        //mm = Conv.ToInt(tb.Rows[i]["月"]);
                        DataRow r = dt.NewRow();
                        r["编码"] = visa_id;
                        r["名称"] = visa_nm;
                        r["期初金额"] = begion_amount;
                        if (begion_amount > 0)
                            r["期初金额方向"] = "借";
                        else if (begion_amount < 0)
                            r["期初金额方向"] = "贷";
                        else
                            r["期初金额方向"] = "平";
                        r["借方"] = yyborrow;
                        r["贷方"] = yyloan;
                        r["期末金额"] = Math.Abs(beginbalance);
                        if (beginbalance > 0)
                            r["期末金额方向"] = "借";
                        else if (beginbalance < 0)
                            r["期末金额方向"] = "贷";
                        else
                            r["期末金额方向"] = "平";
                        dt.Rows.Add(r);
                    }
                }

            }
            return dt;
        }

        #endregion


        #region 采购 PI F
        public System.Data.DataTable GetCGSum(DateTime start_time, DateTime end_time, string branch_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string start = start_time.ToString("yyyy-MM-dd hh:mm:ss");
            string sql = @"select 
b.branch_name 机构仓库,
isnull(count(distinct m.sheet_no),0) 合计单数,
isnull(sum(case WHEN m.trans_no='A' THEN isnull(isnull(f.real_qnty,d.in_qty),0.00) else 0 end  ),0.00) 采购数量,
isnull(sum(case WHEN m.trans_no='A' then d.valid_price*isnull(isnull(f.real_qnty,d.in_qty),0.00) else 0 end),0.00)  采购金额,
isnull(sum(case WHEN m.trans_no='F' THEN d.in_qty else 0 end  ),0.00)  退货数量,
isnull(sum(case WHEN m.trans_no='F' THEN d.valid_price*d.in_qty else 0 end  ),0.00)  退货金额,
isnull(sum(case when d.other3='1' then d.in_qty else 0 end),0.00)  赠送数量,
isnull(isnull(sum(case WHEN m.trans_no='A' THEN isnull(isnull(f.real_qnty,d.in_qty),0.00) else 0 end  ),0.00)-isnull(sum(case WHEN m.trans_no='F' THEN d.in_qty else 0 end  ),0.00),0.00)  实际数量,
isnull(isnull(sum(case WHEN m.trans_no='A' then d.valid_price*isnull(isnull(f.real_qnty,d.in_qty),0.00) else 0 end),0.00)-isnull(sum(case WHEN m.trans_no='F' THEN d.valid_price*d.in_qty else 0 end  ),0.00),0.00)  实际金额
from bi_t_branch_info b
INNER JOIN ic_t_inout_store_master m on b.branch_no=m.branch_no
INNER JOIN ic_t_inout_store_detail d on d.sheet_no=m.sheet_no 
 left join [dbo].[bi_t_supcust_info] e on m.supcust_no=e.supcust_no 
left join [dbo].[ic_t_inoutstore_recpay_detail] f on f.sheet_no=d.sheet_no and f.item_no=d.item_no and f.task_flow_id=d.flow_id
 where m.trans_no in ('A','F') and m.approve_man is not null and supcust_flag='S'  
   and  m.oper_date BETWEEN '" + start_time.ToString() + "' and '" + end_time.ToString() + "' ";

            if (!string.IsNullOrEmpty(branch_no))
                sql += " and m.branch_no='" + branch_no + "' ";
            sql += " group by b.branch_name";

            DataTable tb = db.ExecuteToTable(sql, "b.branch_name", null, page_size, page_index, out total_count);
            //DataTable tt=new DataTable();
            //tt.Columns.Add("机构仓库");
            //tt.Columns.Add("合计单数",typeof(decimal));
            //tt.Columns.Add("采购数量", typeof(decimal));
            //tt.Columns.Add("采购金额", typeof(decimal));
            //tt.Columns.Add("退货数量", typeof(decimal));
            //tt.Columns.Add("退货金额", typeof(decimal));
            //tt.Columns.Add("赠送数量", typeof(decimal));
            //tt.Columns.Add("实际数量", typeof(decimal));
            //tt.Columns.Add("实际金额", typeof(decimal));
            //ArrayList al=new ArrayList();
            //foreach (DataRow row in tb.Rows)
            //{
            //    if (!al.Contains(row["机构仓库"]))
            //    {
            //        DataRow dr = tt.NewRow();
            //        dr["机构仓库"] = row["机构仓库"];
            //        tt.Rows.Add(dr);
            //        al.Add(row["机构仓库"]);
            //    }
            //}

            //foreach (DataRow row in tt.Rows)
            //{
            //    DataRow[] r = tb.Select("机构仓库='"+row["机构仓库"]+"'");
            //    decimal dc = 0;
            //    decimal Amount = 0.0000m;
            //    decimal tuihuo = 0;
            //    decimal ta = 0;
            //    decimal zs = 0;
            //    foreach (var dataRow in r)
            //    {
            //        if (dataRow["实收数量"].ToDecimal() == 0)
            //        {
            //            dc += dataRow["采购数量"].ToDecimal();
            //            amount += dataRow["采购金额"].ToDecimal();
            //        }
            //        else
            //        {
            //            dc+= dataRow["实收数量"].ToDecimal();
            //            amount += dataRow["单价"].ToDecimal() * dataRow["实收数量"].ToDecimal();
            //        }

            //        tuihuo += dataRow["退货数量"].ToDecimal();
            //        ta+= dataRow["退货金额"].ToDecimal();
            //        if (row["合计单数"].ToDecimal() == 0 && row["合计单数"] == null)
            //        {
            //            row["合计单数"] = dataRow["合计单数"];
            //        }
            //    }
            //    row["采购数量"] = dc;
            //    row["采购金额"] = amount;
            //    row["赠送数量"] = zs;
            //    row["退货数量"] = tuihuo;
            //    row["退货金额"] = ta;
            //    row["实际数量"] = dc -tuihuo;
            //    row["实际金额"] = amount - ta;
               
            //}
            return tb;
        }
        public System.Data.DataTable GetCGDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string supcust_no, string trans_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select 
case when m.trans_no='A' then '采购入库' 
		 when m.trans_no='F' then '退货出库' 
     end 业务类型,
m.sheet_no 单据号,
Convert(varchar(10), m.approve_date ,20 ) 审核日期,
(b.branch_no+'/'+b.branch_name) 仓库,
(s.supcust_no+'/'+s.sup_name) 供应商,
o1.oper_name 操作人,
o2.oper_name 审核人,
Convert(varchar(10), m.oper_date ,20 )  操作时间,
isnull(sum(isnull(e.real_qnty,d.in_qty)),0.00) 数量,
isnull(sum(d.in_qty*d.valid_price),0.00) 金额
from ic_t_inout_store_master m 
INNER JOIN ic_t_inout_store_detail d on d.sheet_no=m.sheet_no
INNER JOIN bi_t_branch_info b on b.branch_no=m.branch_no
left JOIN bi_t_supcust_info s on m.supcust_no=s.supcust_no and s.supcust_flag='S'
left join [dbo].[ic_t_inoutstore_recpay_detail] e on e.sheet_no=d.sheet_no and e.item_no=d.item_no and e.task_flow_id=d.flow_id
left JOIN sa_t_operator_i o1 on o1.oper_id=m.oper_id
left JOIN sa_t_operator_i o2 on o2.oper_id=m.approve_man ";
            sql += " where  m.trans_no in ('A','F')  and   Convert(varchar(10), m.approve_date ,20 ) BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "' and '" + end_time.ToString("yyyy-MM-dd") + "' ";
            if (!string.IsNullOrEmpty(branch_no))
                sql += " and m.branch_no='" + branch_no + "' ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " and m.sheet_no like  '%" + sheet_no + "%'";
            if (!string.IsNullOrEmpty(supcust_no))
                sql += " and m.supcust_no = '" + supcust_no + "' ";
            if (!string.IsNullOrEmpty(trans_no))
                sql += " and m.trans_no='" + trans_no + "' ";
            sql += @"group by m.trans_no,
m.sheet_no ,
m.approve_date,
(b.branch_no+'/'+b.branch_name) ,
(s.supcust_no+'/'+s.sup_name) ,
o1.oper_name ,
o2.oper_name ,
m.oper_date ";

            DataTable tb = db.ExecuteToTable(sql, "m.sheet_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetCGOrderSum(DateTime start_time, DateTime end_time, string supcust_no, string barcode, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string where_sql = "";
            if (!string.IsNullOrEmpty(supcust_no))
            {
                where_sql += $"  AND c.sup_no = '{supcust_no}' ";
            }
            if (!string.IsNullOrEmpty(barcode))
            {
                where_sql += $"   AND (c.item_no LIKE '%{barcode}%' OR c.barcode LIKE '%{barcode}%')";
            }

            string sql = $@"SELECT c.sup_no 供应商编号,
       s.sup_name 供应商名称,
       c.item_no 商品编号,
       c.item_name 商品名称,
       c.barcode 条码,
       c.unit_no 商品单位,
       SUM(cg_qty) 采购数量
FROM dbo.co_t_cg_order_detail c
    LEFT JOIN dbo.bi_t_supcust_info s
        ON c.sup_no = s.supcust_no
           AND s.supcust_flag = 'S'
WHERE c.create_time
      BETWEEN '{start_time.Toyyyy_MM_dd_HH_mm_ss()}' AND '{end_time.Toyyyy_MM_dd_HH_mm_ss()}'
{where_sql}
GROUP BY c.sup_no,
         s.sup_name,
         c.item_no,
         c.item_name,
         c.barcode,
         c.unit_no";

            DataTable tb = db.ExecuteToTable(sql, "c.sup_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetCGMoreSup(DateTime start_time, DateTime end_time, string supcust_no, string barcode, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string where_sql = "";
            if (!string.IsNullOrEmpty(supcust_no))
            {
                where_sql += $"  AND c.sup_no = '{supcust_no}' ";
            }
            if (!string.IsNullOrEmpty(barcode))
            {
                where_sql += $"   AND (c.item_no LIKE '%{barcode}%' OR c.barcode LIKE '%{barcode}%')";
            }

            string sql = $@"SELECT c.sup_no 供应商编号,
       s.sup_name 供应商名称,
       c.cust_no 客户编号,
       cs.sup_name 客户名称,
       c.item_no 商品编号,
       c.item_name 商品名称,
       d.barcode 条码,
       c.unit_no 商品单位,
       SUM(cg_qty) 采购数量
FROM dbo.co_t_cg_order_detail c
    LEFT JOIN dbo.bi_t_supcust_info s
        ON c.sup_no = s.supcust_no
           AND s.supcust_flag = 'S'
    LEFT JOIN dbo.bi_t_supcust_info cs
        ON cs.supcust_no = c.cust_no
           AND cs.supcust_flag = 'C'
LEFT JOIN dbo.bi_t_item_info d
on d.item_no=c.item_no
WHERE  c.create_time 
      BETWEEN '{start_time.Toyyyy_MM_dd_HH_mm_ss()}' AND '{end_time.Toyyyy_MM_dd_HH_mm_ss()}'
{where_sql}
GROUP BY c.sup_no,
         s.sup_name,
         c.cust_no,
         cs.sup_name,
         c.item_no,
         c.item_name,
         c.barcode,
         c.unit_no,
d.barcode";

            DataTable tb = db.ExecuteToTable(sql, "c.sup_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetCGItemDetail(DateTime start_time, DateTime end_time, string supcust_no, string keyword, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT m.supcust_no  供应商编号,s.sup_name 供应商名称,d.item_no 商品编号,i.item_subno 货号,i.item_name 商品名称
,i.barcode 条码,i.unit_no 单位,i.item_size 规格,
SUM(isnull(e.real_qnty,d.in_qty))  数量,
SUM(d.sub_amount)/SUM(CASE WHEN d.in_qty=0 THEN 1 ELSE d.in_qty end) 价格,
SUM(isnull(e.real_qnty,d.in_qty)*valid_price) 金额
FROM dbo.ic_t_inout_store_master m
LEFT JOIN dbo.ic_t_inout_store_detail d ON d.sheet_no = m.sheet_no
LEFT JOIN dbo.bi_t_item_info i ON d.item_no=i.item_no
LEFT JOIN dbo.bi_t_supcust_info s ON s.supcust_no=m.supcust_no AND s.supcust_flag='S'
left join [dbo].[ic_t_inoutstore_recpay_detail] e on e.sheet_no=d.sheet_no and e.item_no=d.item_no and e.task_flow_id=d.flow_id
WHERE m.trans_no='A' AND m.oper_date BETWEEN '" + start_time.Toyyyy_MM_dd_HH_mm_ss() + "' AND '" + end_time.Toyyyy_MM_dd_HH_mm_ss() + " '";

            if (!string.IsNullOrEmpty(supcust_no))
            {
                sql += " AND m.supcust_no='" + supcust_no + "' ";
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (d.item_no LIKE '%" + keyword + "%' OR i.item_subno LIKE '%" + keyword + "%' OR i.item_name LIKE '%" + keyword + "%') ";
            }

            sql += " GROUP BY m.supcust_no,s.sup_name,d.item_no,i.item_subno,i.item_name,i.barcode,i.unit_no,i.item_size ";

            DataTable tb = db.ExecuteToTable(sql, "m.supcust_no", null, page_size, page_index, out total_count);

            return tb;
        }

        public DataTable GetOrderInLoss(DateTime start_time, DateTime end_time, string keyword, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

//            string sql = $@"SELECT o.sup_no 供应商编码,
//       s.sup_name 供应商,
//       o.item_no 商品编号,
//       e.item_subno 货号,
//       e.item_name 商品名称,
//       e.unit_no 单位,
//       SUM ( o.order_qnty ) 订单数量,
//       SUM ( ISNULL ( o.in_qty, 0 )) 收货数量,
//       SUM ( ISNULL ( o.in_qty, 0 ))- SUM ( o.order_qnty ) 实收差异,
//       SUM ( ISNULL ( o.in_qty, 0 ))- SUM ( o.order_qnty ) 损耗数量,
//(SUM ( ISNULL ( o.in_qty, 0 ))- SUM ( o.order_qnty ) )/(case when SUM(ISNULL( o.in_qty, 0 ))=0 then 1 else SUM ( ISNULL ( o.in_qty, 0 )) end) 损耗率
//FROM
//    (
//        SELECT m.sup_no, d.item_no, SUM ( d.order_qnty ) order_qnty, '0' in_qty, '0' loss_qty
//        FROM dbo.co_t_order_main m
//        LEFT JOIN dbo.co_t_order_child d
//          ON d.sheet_no = m.sheet_no
//        WHERE m.trans_no = 'P'
//              AND m.approve_date BETWEEN '{start_time.Toyyyy_MM_ddStart()}' AND '{end_time.Toyyyy_MM_ddEnd()}'
//              {(string.IsNullOrEmpty(supcust_no) ? "" : $@"AND m.sup_no = '{supcust_no}'")}
//        GROUP BY m.sup_no, d.item_no
//        UNION ALL
//        SELECT m.supcust_no, d.item_no, '0', SUM ( d.in_qty ) in_qty, SUM ( d.num5 )
//        FROM dbo.ic_t_inout_store_master m
//        LEFT JOIN dbo.ic_t_inout_store_detail d
//          ON d.sheet_no = m.sheet_no
//        WHERE m.trans_no = 'A'
//              AND m.approve_date BETWEEN '{start_time.Toyyyy_MM_ddStart()}' AND '{end_time.Toyyyy_MM_ddEnd()}'
//              {(string.IsNullOrEmpty(supcust_no)?"":$@"AND m.supcust_no = '{supcust_no}'")}
//        GROUP BY m.supcust_no, d.item_no
//    ) o
//LEFT JOIN dbo.bi_t_item_info e
//  ON e.item_no = o.item_no
//LEFT JOIN dbo.bi_t_supcust_info s
//  ON s.supcust_no = o.sup_no AND s.supcust_flag = 'S'
//WHERE e.item_name LIKE '%{keyword}%' OR e.item_subno LIKE '%{keyword}%'
//GROUP BY o.sup_no, s.sup_name, o.item_no, e.item_subno, e.item_name, e.unit_no
//ORDER BY o.item_no;";
            string sql = @"select c.supcust_no 供应商编码,
       c.sup_name 供应商,
       d.item_no 商品编号,
       d.item_subno 货号,
       d.item_name 商品名称,
       d.unit_no 单位,
	   sum(isnull(a.in_qty,0.00)) 订单数量,
	   sum(isnull(a.order_qnty,0.00)) 收货数量,
	    sum(isnull(a.order_qnty,0.00))-sum(isnull(a.in_qty,0.00)) 实收差异
		,'0' 损耗数量
		,'0' 损耗率
	   from 
dbo.ic_t_inout_store_detail a left join
dbo.ic_t_inout_store_master b  on a.sheet_no=b.sheet_no
left join [dbo].[bi_t_supcust_info] c on c.supcust_no=b.supcust_no
left join [dbo].[bi_t_item_info] d on a.item_no=d.item_no
where b.trans_no='A'
and  b.approve_date BETWEEN '" + start_time.Toyyyy_MM_ddStart()+"' AND '"+end_time.Toyyyy_MM_ddEnd()+"'" +
                "and   (c.supcust_no like '%" + supcust_no + "%' or c.sup_name like '%" + supcust_no + "%') " +
                "and(d.item_subno like '%"+keyword+"%' or d.item_name like '%"+keyword+"%')" +
                         "group by c.supcust_no,c.sup_name,d.item_no,d.item_subno,d.item_name,d.unit_no";
            total_count = 9999999;
            DataTable dt= db.ExecuteToTable(sql, null);
            ArrayList al=new ArrayList();
            DataTable d=new DataTable();
            d.Columns.Add("供应商编码");
            d.Columns.Add("供应商");
            d.Columns.Add("商品编号");
            d.Columns.Add("货号");
            d.Columns.Add("商品名称");
            d.Columns.Add("单位");
            d.Columns.Add("订单数量", typeof(decimal));
            d.Columns.Add("收货数量", typeof(decimal));
            d.Columns.Add("实收差异", typeof(decimal));
            d.Columns.Add("损耗数量", typeof(decimal));
            d.Columns.Add("损耗率", typeof(decimal));
            foreach (DataRow dataRow in dt.Rows)
            {
                //if (!al.Contains(dataRow["供应商编码"] + "/" + dataRow["货号"]))
                //{
                    DataRow dr = d.NewRow();
                    dr["供应商编码"] =dataRow["供应商编码"];
                    dr["供应商"] = dataRow["供应商"];
                    dr["商品编号"] = dataRow["商品编号"];
                    dr["货号"] = dataRow["货号"];
                    dr["商品名称"] = dataRow["商品名称"];
                    dr["单位"] = dataRow["单位"];
                    //DataRow[] dingdan_row =
                    //    dt.Select("订单数量='" + dataRow["订单数量"] + "' " +
                    //              "and 供应商编码='" + dataRow["供应商编码"] + "'");
                    //decimal dingdan = 0;
                    //foreach (var row in dingdan_row)
                    //{
                    //    dingdan += row["订单数量"].ToDecimal();
                    //}
                    //dr["订单数量"] = dingdan;
                    dr["订单数量"] = dataRow["订单数量"];
                    //DataRow[] shouhuo_row =
                    //    dt.Select("收货数量='" + dataRow["收货数量"] + "' " +
                    //              "and 供应商编码='" + dataRow["供应商编码"] + "'");
                    //decimal shouhuo = 0;
                    //foreach (var row in shouhuo_row)
                    //{
                    //    shouhuo += row["收货数量"].ToDecimal();
                    //}
                    //dr["收货数量"] = shouhuo;
                    dr["收货数量"] = dataRow["收货数量"];
                    //dr["实收差异"] = shouhuo-dingdan;
                    dr["实收差异"] = dataRow["收货数量"].ToDecimal()-dataRow["订单数量"].ToDecimal();
                    sql = @"select sum(isnull(receive_qty,0.00)) qty from [dbo].[co_t_receive_order_detail] where  trans_no='4' and sup_no='" + dataRow["供应商编码"] + "' and item_no='" + dataRow["商品编号"] + "' and oper_date BETWEEN '" + start_time.Toyyyy_MM_ddStart() + "' AND '" + end_time.Toyyyy_MM_ddEnd() + "'";
                    decimal sunhao = db.ExecuteToTable(sql, null).Rows[0]["qty"].ToDecimal();
                    dr["损耗数量"] = sunhao;
                    //if (dingdan == null||dingdan==0)
                    //{
                    //    dingdan = 1;
                   // }
                   decimal dingdan = dataRow["订单数量"].ToDecimal();
                    if (dingdan == null || dingdan == 0)
                    {
                        dingdan = 1;
                    }
                    dr["损耗率"] = sunhao / dingdan;
                    d.Rows.Add(dr);
                    al.Contains(dataRow["供应商编码"] + "/" + dataRow["货号"]);
               // }
            }
            return d;
        }
        public DataTable GetOrderInLoss_D(DateTime start_time, DateTime end_time, string keyword, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            //         
            string sql = @"select a.sup_no,a.po_sheet_no,b.item_no,b.item_name,b.item_subno
,sum(isnull(order_qnty,0.00)) order_qty,sum(isnull(receive_qty,0.00)) qty
,c.sup_name,b.unit_no  from [dbo].[co_t_receive_order_detail] a 
left join [dbo].[bi_t_item_info] b on a.item_no=b.item_no
left join [dbo].[bi_t_supcust_info] c on a.sup_no=c.supcust_no and c.supcust_flag='S' 
left join [dbo].[bi_t_branch_info] d on d.branch_no=b.branch_no
where  a.trans_no='4' and a.sup_no like'%" + supcust_no + "%' and (b.item_subno like '%" + keyword + "%' or b.item_name like '%" + keyword + "%')and a.oper_date BETWEEN '" + start_time.Toyyyy_MM_ddStart() + "' AND '" + end_time.Toyyyy_MM_ddEnd() + "' group by b.item_no,b.item_name,b.item_subno,a.sup_no,a.po_sheet_no,c.sup_name,b.unit_no order by a.po_sheet_no";
            total_count = 9999999;
            DataTable dt = db.ExecuteToTable(sql, null);
            return dt;
        }
        #endregion

        #region 销售
        public DataTable GetSaleSum(DateTime start_time, DateTime end_time, string branch_no, string supcust_no, int page_index, int page_size, out int total_count)
        {
            //销售汇总
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT t1.机构仓库,t1.合计单数,t1.销售数量,t1.销售金额,
ISNULL(t2.退货数量,0.00) 退货数量,
ISNULL(t2.退货金额,0.00) 退货金额,
(t1.销售数量-t2.退货数量) 实际数量,
(t1.销售金额-t2.退货金额) 实际金额
FROM 
(
SELECT
b.branch_name 机构仓库,
isnull(count(distinct m.sheet_no), 0) 合计单数,
isnull(sum(isnull(e.real_qnty,d.sale_qnty)), 0) 销售数量,
isnull(sum(isnull(e.real_qnty,d.sale_qnty)*d.real_price), 0.00) 销售金额
from bi_t_branch_info b
INNER JOIN sm_t_salesheet m on m.branch_no = b.branch_no
INNER JOIN sm_t_salesheet_detail d on d.sheet_no = m.sheet_no
left join [dbo].[ic_t_inoutstore_recpay_detail] e on e.sheet_no=d.sheet_no and e.item_no=d.item_no and e.task_flow_id=d.flow_id
where  m.approve_date  BETWEEN '" + start_time.Toyyyy_MM_dd_HH_mm_ss() + "' and '" + end_time.Toyyyy_MM_dd_HH_mm_ss() + "'";
            if (!string.IsNullOrEmpty(branch_no))
            {
                sql += " AND  b.branch_no='" + branch_no + "' ";
            }
            if (!string.IsNullOrEmpty(supcust_no))
            {
                sql += " AND  m.cust_no='" + supcust_no + "' ";
            }

            sql += @"
group by b.branch_name
) t1
LEFT JOIN
(select
b.branch_name 机构仓库,
isnull(sum(case when m1.trans_no = 'D' then d1.in_qty else 0 end), 0) 退货数量,
isnull(sum(case when m1.trans_no = 'D' then d1.valid_price * d1.in_qty else 0 end), 0.00) 退货金额
from bi_t_branch_info b
INNER JOIN ic_t_inout_store_master m1 on m1.branch_no = b.branch_no
INNER JOIN ic_t_inout_store_detail d1 on d1.sheet_no = m1.sheet_no
where  m1.approve_date  BETWEEN '" + start_time.Toyyyy_MM_dd_HH_mm_ss() + "' and '" + end_time.Toyyyy_MM_dd_HH_mm_ss() + "' ";
            if (!string.IsNullOrEmpty(branch_no))
            {
                sql += " AND  b.branch_no='" + branch_no + "' ";
            }
            if (!string.IsNullOrEmpty(supcust_no))
            {
                sql += " AND  m1.supcust_no='" + supcust_no + "' ";
            }

            sql += @"group by b.branch_name) t2 ON t2.机构仓库 = t1.机构仓库 ";

            DataTable tb = db.ExecuteToTable(sql, "t1.机构仓库", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetSaleDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string cust_no, int page_index, int page_size, out int total_count)
        {
            //批发销售明细
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select 
m.sheet_no 单据号,
m.approve_date 审核日期,
o1.oper_name 审核人,
m.oper_date 操作日期,
o2.oper_name 操作人,
(b.branch_no+'/'+b.branch_name) 仓库,
(s.supcust_no+'/'+s.sup_name) 客户,
isnull(sum(isnull(e.real_qnty,d.sale_qnty)),0.00) 数量,
isnull(sum(isnull(e.real_qnty,d.sale_qnty)*real_price),0.00) 金额
from  sm_t_salesheet m 
INNER JOIN sm_t_salesheet_detail d on d.sheet_no=m.sheet_no
INNER JOIN sa_t_operator_i o1 on o1.oper_id=m.approve_man
INNER JOIN sa_t_operator_i o2 on o2.oper_id=m.oper_id
INNER JOIN bi_t_branch_info b on b.branch_no = m.branch_no
INNER JOIN bi_t_supcust_info s on m.cust_no=s.supcust_no and s.supcust_flag='C'
left join [dbo].[ic_t_inoutstore_recpay_detail] e on e.sheet_no=d.sheet_no and e.item_no=d.item_no and e.task_flow_id=d.flow_id
where m.approve_date  BETWEEN '" + start_time.ToString() + "' and '" + end_time.ToString() + "' ";

            if (!string.IsNullOrEmpty(branch_no))
                sql += " and m.branch_no='" + branch_no + "' ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " and m.sheet_no like '%" + sheet_no + "%' ";
            if (!string.IsNullOrEmpty(cust_no))
                sql += " and m.cust_no = '" + cust_no + "' ";

            sql += "group by m.sheet_no,m.approve_date,o1.oper_name,m.oper_date" +
                   ",o2.oper_name,(b.branch_no+'/'+b.branch_name),(s.supcust_no+'/'+s.sup_name)";


            DataTable tb = db.ExecuteToTable(sql, "m.sheet_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetSaleItemDetail(DateTime start_time, DateTime end_time, string branch_no, string cust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT b.branch_no 仓库编码 ,b.branch_name 仓库名称,c.supcust_no 客户编码,c.sup_name 客户名称,d.item_no 商品编号,i.item_subno 货号,
i.item_name 商品名称,d.unit_no 单位,i.item_size 规格,
SUM(isnull(g.real_qnty,d.sale_qnty)) 销售数量,
SUM(isnull(g.real_qnty,d.sale_qnty)*real_price)/SUM(CASE WHEN isnull(g.real_qnty,d.sale_qnty)*real_price=0 THEN 1 else  isnull(g.real_qnty,d.sale_qnty)*real_price end )  销售价格,
SUM(isnull(g.real_qnty,d.sale_qnty)*real_price) 销售金额,
SUM(d.cost_price*d.sale_qnty)
/SUM(CASE WHEN d.sale_qnty=0 THEN 1 ELSE d.sale_qnty END )  成本价格,
SUM(d.cost_price*d.sale_qnty) 成本金额,
(SUM(isnull(g.real_qnty,d.sale_qnty)*real_price)-SUM(d.cost_price*d.sale_qnty)) 利润
FROM dbo.sm_t_salesheet s
LEFT JOIN  dbo.sm_t_salesheet_detail d ON s.sheet_no=d.sheet_no
LEFT JOIN dbo.bi_t_item_info i ON i.item_no=d.item_no
LEFT JOIN dbo.bi_t_branch_info b ON b.branch_no=s.branch_no
LEFT JOIN dbo.bi_t_supcust_info c ON c.supcust_no=s.cust_no AND c.supcust_flag='C'
left join [dbo].[ic_t_inoutstore_recpay_detail] g on g.sheet_no=d.sheet_no and g.item_no=d.item_no and g.task_flow_id=d.flow_id
WHERE s.approve_date BETWEEN '" + start_time.Toyyyy_MM_dd_HH_mm_ss() + "' AND '" + end_time.Toyyyy_MM_dd_HH_mm_ss() + "'";

            if (!string.IsNullOrEmpty(branch_no))
            {
                sql += " AND b.branch_no='" + branch_no + "' ";
            }
            if (!string.IsNullOrEmpty(cust_no))
            {
                sql += " AND s.cust_no='" + cust_no + "' ";
            }

            sql += " GROUP BY b.branch_no ,b.branch_name,c.supcust_no,c.sup_name,d.item_no,i.item_subno,i.item_name,d.unit_no,i.item_size ";

            DataTable tb = db.ExecuteToTable(sql, "b.branch_no,c.supcust_no,d.item_no", null, page_size, page_index, out total_count);
            return tb;
        }
        public DataTable GetSaleOutDetail(DateTime start_time, DateTime end_time, string branch_no, string sheet_no, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
			m.sheet_no 单据号,
			Convert(varchar(10),  m.approve_date,20 )  审核日期,
			(
				b.branch_no +'/' +b.branch_name
			) 仓库,
			(
				s.supcust_no + '/' + s.sup_name
			) 客户,
			o1.oper_name 操作人,
			o2.oper_name 审核人,
			Convert(varchar(10),m.oper_date  ,20 ) 操作时间,
			isnull(sum(d.in_qty), 0.00) 数量,
			isnull(
				m.total_amount,
				0.00
			) 金额
		FROM
			ic_t_inout_store_master m
		INNER JOIN ic_t_inout_store_detail d ON d.sheet_no = m.sheet_no
		INNER JOIN bi_t_branch_info b ON b.branch_no = m.branch_no
		INNER JOIN bi_t_supcust_info s ON m.supcust_no = s.supcust_no and s.supcust_flag = 'C' 
		INNER JOIN sa_t_operator_i o1 ON o1.oper_id = m.oper_id
		INNER JOIN sa_t_operator_i o2 ON o2.oper_id = m.approve_man 
where  m.approve_date BETWEEN '" + start_time.ToString("yyyy-MM-dd hh:mm:ss") + "' and '" + end_time.ToString("yyyy-MM-dd hh:mm:ss") + "' ";

            if (!string.IsNullOrEmpty(branch_no))
                sql += " and m.branch_no='" + branch_no + "' ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " and m.sheet_no like '%" + sheet_no + "%' ";
            if (!string.IsNullOrEmpty(supcust_no))
                sql += " and m.supcust_no = '" + supcust_no + "' ";

            sql += " and m.trans_no='D' ";
            sql += @"group by m.sheet_no,m.approve_date,(b.branch_no +'/' +b.branch_name), (s.supcust_no + '/' + s.sup_name),
o1.oper_name,o2.oper_name,m.oper_date,m.total_amount";

            DataTable tb = db.ExecuteToTable(sql, "m.sheet_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetCusCredit(string supcust_no, int page_index, int page_size, out int total_count)
        {

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select 
s.supcust_no 客户编号,
s.sup_name 客户名称,
f.new_money 欠款金额,
s.credit_amt 信誉额度,
s.sup_addr 客户地址,
s.sup_tel 客户电话,
s.sup_email 客户EMail
from bi_t_supcust_info s
INNER JOIN (select s.supcust_no ,sum( s.oper_money) new_money
from rp_t_supcust_cash_flow s
group by s.supcust_no  ) f on f.supcust_no = s.supcust_no and f.new_money<s.credit_amt
where s.supcust_flag = 'C'  ";

            if (!string.IsNullOrEmpty(supcust_no))
                sql += "and s.supcust_no='" + supcust_no + "' ";


            DataTable tb = db.ExecuteToTable(sql, "s.supcust_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetNoSaleCus(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select 
s.supcust_no 客户编号,
s.sup_name 客户名称,
s.credit_amt 信誉额度,
s.sup_addr 客户地址,
s.sup_tel 客户电话,
s.sup_email 客户EMail
from bi_t_supcust_info s
left join sm_t_salesheet m on m.cust_no=s.supcust_no and  m.approve_date BETWEEN '@start_time' and '@end_time' 
where m.sheet_no is null  and  s.supcust_flag = 'C' "
                .Replace("@start_time", start_time.ToString("yyyy-MM-dd hh:mm:ss"))
                .Replace("@end_time", end_time.ToString("yyyy-MM-dd hh:mm:ss"));

            DataTable tb = db.ExecuteToTable(sql, "s.supcust_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetSheetPayInfo(DateTime start_time, DateTime end_time, string trans_no, string supcust_no, string sheet_no, string type, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.voucher_no 单号,
	a.trans_no 单据类型,
	a.sheet_amount 单据金额,
	a.paid_amount 已付款金额,
	a.pay_date 限收日期,
	(s.supcust_no+'/'+s.sup_name )客户,
	a.free_money 免付金额,
	(
		a.sheet_amount - a.paid_amount - a.free_money
	) 尚欠金额 
FROM rp_t_accout_payrec_flow a 
LEFT OUTER JOIN sm_t_salesheet b ON a.voucher_no = b.sheet_no 
LEFT JOIN bi_t_supcust_info s ON s.supcust_no = a.supcust_no  and s.supcust_flag='C'
where  a.pay_date   BETWEEN '" + start_time.ToString("yyyy-MM-dd hh:mm:ss") + "' and '" + end_time.ToString("yyyy-MM-dd hh:mm:ss") + "' ";

            if (!string.IsNullOrEmpty(supcust_no))
                sql += " and a.supcust_no='" + supcust_no + "' ";

            if (!string.IsNullOrEmpty(sheet_no))
                sql += " and a.voucher_no like '%" + sheet_no + "%' ";
            if (!string.IsNullOrEmpty(trans_no))
                sql += " and a.trans_no IN (" + trans_no + ") ";

            if (type.Equals("全部"))
                sql += " and (1=1) ";
            else if (type.Equals("完全没付"))
                sql += " and (a.paid_amount + a.free_money = 0) ";
            else if (type.Equals("部分付款"))
                sql += " and (abs(a.pay_type * a.sheet_amount - a.paid_amount - a.free_money) >= 0.01) ";
            else if (type.Equals("没有付清"))
                sql += " and (abs(a.pay_type * a.sheet_amount - a.paid_amount - a.free_money) >= 0.01) ";
            else if (type.Equals("完全付清"))
                sql += " and (abs(a.pay_type * a.sheet_amount - a.paid_amount - a.free_money) <= 0.01) ";

            DataTable tb = db.ExecuteToTable(sql, "a.voucher_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetSaleOrderSum(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT	 c.item_no 商品编号,i.item_subno 货号,i.item_name 商品名称,i.barcode 条码,i.unit_no 单位,i.item_size 规格,
(SUM(c.sub_amount)/SUM(CASE WHEN  c.order_qnty = 0 OR c.order_qnty IS NULL THEN 1 ELSE c.order_qnty END)) 价格,
SUM(c.order_qnty) 订单数量,SUM(c.sub_amount) 金额
FROM dbo.co_t_order_child c
LEFT JOIN dbo.co_t_order_main m ON c.sheet_no=m.sheet_no
LEFT JOIN dbo.bi_t_item_info i ON c.item_no=i.item_no
WHERE m.sheet_no LIKE 'SS%'
AND m.approve_date BETWEEN '" + start_time.Toyyyy_MM_dd_HH_mm_ss() + "' AND '" + end_time.Toyyyy_MM_dd_HH_mm_ss() + " '";

            if (!string.IsNullOrEmpty(supcust_no))
            {
                sql += " AND m.sup_no='" + supcust_no + "' ";
            }

            sql += " GROUP BY c.item_no,i.item_subno,i.item_name,i.barcode,i.unit_no,i.item_size ";

            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }
        public DataTable GetSaleOrderDetail(DateTime start_time, DateTime end_time, string sheet_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT c.sheet_no 单号,c.item_no 商品编号,i.item_subno 货号,i.item_name 商品名称,c.barcode 条码,c.unit_no 单位
,i.item_size 规格,c.in_price 价格,c.order_qnty 数量,c.sub_amount 金额
FROM dbo.co_t_order_child c
LEFT JOIN dbo.co_t_order_main m ON c.sheet_no=m.sheet_no
LEFT JOIN dbo.bi_t_item_info i ON c.item_no=i.item_no
WHERE m.sheet_no LIKE 'SS%'
AND m.approve_date BETWEEN '" + start_time.Toyyyy_MM_dd_HH_mm_ss() + "' AND '" + end_time.Toyyyy_MM_dd_HH_mm_ss() + " '";

            if (!string.IsNullOrEmpty(sheet_no))
            {
                sql += " AND m.sheet_no LIKE '%" + sheet_no + "%' ";
            }

            sql += " ORDER BY c.sheet_no,c.item_no,i.item_subno ";

            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }
        public DataTable GetInOutDiff(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@" SELECT i.item_no 商品编号,i.item_subno 货号,i.item_name 商品名称,i.unit_no 单位,i.item_size 规格,
CASE WHEN  t1.in_qty IS NULL THEN  0 ELSE t1.in_qty END  入库数量,
CASE WHEN  t1.sub_amount IS NULL THEN  0 ELSE t1.sub_amount END  入库金额,
CASE WHEN  t2.sale_qnty IS NULL THEN  0 ELSE t2.sale_qnty END  出库数量,
CASE WHEN  t2.sale_money IS NULL THEN  0 ELSE t2.sale_money END  出库金额,
((CASE WHEN  t2.sale_qnty IS NULL THEN  0 ELSE t2.sale_qnty END)-(CASE WHEN  t1.in_qty IS NULL THEN  0 ELSE t1.in_qty END)) 数量差异,
((CASE WHEN  t2.sale_money IS NULL THEN  0 ELSE t2.sale_money END)-(CASE WHEN  t1.sub_amount IS NULL THEN  0 ELSE t1.sub_amount END)) 金额差异
FROM dbo.bi_t_item_info i
LEFT JOIN(
SELECT c.item_no, SUM(isnull(g.real_qnty,c.in_qty)) in_qty, SUM(isnull(g.real_qnty,c.in_qty)*valid_price) sub_amount
FROM dbo.ic_t_inout_store_master m
LEFT JOIN dbo.ic_t_inout_store_detail c ON m.sheet_no= c.sheet_no
left join [dbo].[ic_t_inoutstore_recpay_detail] g on g.sheet_no=c.sheet_no and g.item_no=c.item_no and g.task_flow_id=c.flow_id
WHERE m.approve_flag= '1' AND  (m.approve_date BETWEEN '{start_time.Toyyyy_MM_dd_HH_mm_ss()}' AND '{end_time.Toyyyy_MM_dd_HH_mm_ss()}')
AND m.sheet_no LIKE 'PI%'
GROUP BY c.item_no) t1 ON t1.item_no = i.item_no
LEFT JOIN(
SELECT d.item_no, SUM(isnull(g.real_qnty,d.sale_qnty)) sale_qnty, SUM(isnull(g.real_qnty,d.sale_qnty)*real_price) sale_money
FROM dbo.sm_t_salesheet s
LEFT JOIN dbo.sm_t_salesheet_detail d ON d.sheet_no = s.sheet_no
left join [dbo].[sm_t_salesheet_recpay_detail] g on g.sheet_no=d.sheet_no and g.item_no=d.item_no and g.task_flow_id=d.flow_id
WHERE s.approve_flag = '1' AND  (s.approve_date BETWEEN '{start_time.Toyyyy_MM_dd_HH_mm_ss()}' AND '{end_time.Toyyyy_MM_dd_HH_mm_ss()}')
GROUP BY d.item_no) t2 ON t2.item_no = i.item_no
WHERE((t1.in_qty IS NOT  NULL  AND   t2.sale_qnty IS NOT  NULL)
OR(t1.in_qty IS NOT NULL OR t2.sale_qnty IS NOT NULL)) ";
            //int a = 0;
            //if (int.TryParse(item_name, out a) == false)
            //{
            //    sql += " and i.item_name like '%" + item_name + "%'";
            //}
            //else
            //{
            //    sql += " and i.item_subno like '%" + item_name + "%'";
            //}

            //sql += " and i.barcode like '%" + barcode + "%'";
            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("商品编号");
            dt.Columns.Add("货号");
            dt.Columns.Add("商品名称");
            dt.Columns.Add("单位");
            dt.Columns.Add("规格");
            dt.Columns.Add("入库数量", typeof(decimal));
            dt.Columns.Add("入库金额", typeof(decimal));
            dt.Columns.Add("出库数量", typeof(decimal));
            dt.Columns.Add("出库金额", typeof(decimal));
            dt.Columns.Add("数量差异", typeof(decimal));
            dt.Columns.Add("金额差异", typeof(decimal));
            //decimal cks = 0;
            //decimal ckj = 0;
            //decimal rkj = 0;
            //decimal rks = 0;
            //decimal cys = 0;
            //decimal cyj = 0;

            foreach (DataRow item in tb.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["商品编号"] = item["商品编号"];
                dr["货号"] = item["货号"];
                dr["商品名称"] = item["商品名称"];
                dr["单位"] = item["单位"];
                dr["规格"] = item["规格"];
                dr["入库数量"] = item["入库数量"].ToDecimal();
                // rks += item["入库数量"].ToDecimal();
                dr["入库金额"] = item["入库金额"].ToDecimal();
                //rkj += item["入库金额"].ToDecimal();
                dr["出库数量"] = item["出库数量"].ToDecimal();
                //cks += item["出库数量"].ToDecimal();
                dr["出库金额"] = item["出库金额"].ToDecimal();
                //ckj += item["出库金额"].ToDecimal();
                dr["数量差异"] = item["数量差异"].ToDecimal();
                //cys += item["数量差异"].ToDecimal();
                dr["金额差异"] = item["金额差异"].ToDecimal();
                // cyj += item["金额差异"].ToDecimal();
                dt.Rows.Add(dr);
            }
            //DataRow drr = dt.NewRow();
            //drr["货号"] = "合计：";
            //drr["入库数量"] = rks.ToDecimal();
            //drr["入库金额"] = rkj.ToDecimal();
            //drr["出库数量"] = cks.ToDecimal();
            //drr["出库金额"] = ckj.ToDecimal();
            //drr["数量差异"] = cys.ToDecimal();
            //drr["金额差异"] = cyj.ToDecimal();
            //dt.Rows.Add(drr);
            return dt;
        }

      
        #endregion

        #region 库存
        public DataTable GetICSum(string branch_no, string item_clsno, string item_name, string barcode, string sup_no, string stock_qty, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
				a.item_subno 货号,
				a.barcode 条码,
				a.item_name 商品名称,
				a.item_size 规格,
				a.unit_no 单位,
				a.price 进价,
				a.sale_price 零售价,
				a.product_area 产地,
				(a.item_clsno+'/'+ic.item_clsname) 分类,
				(a.sup_no+'/'+s.sup_name) 主供应商,
				isnull(sum(b.stock_qty), 0) 库存数量,
				ISNULL(a.min_stock,0) 库存指标,
				(ISNULL(sum(b.stock_qty), 0)-ISNULL(a.min_stock,0)) 库存差异,
				isnull(
					sum(
						CASE a.cost_type
						WHEN 'a' THEN
							b.cost_price * b.stock_qty
						WHEN 'b' THEN
							b.fifo_price * b.stock_qty
						WHEN 'c' THEN
							b.last_price * b.stock_qty
						END
					),
					0
				) AS 售价金额,
				a.base_price 批发价,
				a.item_no 商品编码,
				isnull(b.cost_price, 0) 库存金额
			FROM
				bi_t_item_info a
			LEFT JOIN ic_t_branch_stock b ON a.item_no = b.item_no
			LEFT JOIN bi_t_branch_info c ON b.branch_no = c.branch_no
	    LEFT JOIN bi_t_supcust_info s on s.supcust_no=a.sup_no and s.supcust_flag='S'
      LEFT JOIN bi_t_item_cls ic on ic.item_clsno=a.item_clsno and ic.item_flag = '0'
			WHERE
				a.display_flag <> '0'		";

            if (!string.IsNullOrEmpty(item_clsno))
                sql += " AND a.item_clsno like '" + item_clsno + "%' ";
            if (!string.IsNullOrEmpty(item_name))
                sql += " and a.item_name like '%" + item_name + "%' ";
            if (!string.IsNullOrEmpty(stock_qty))
                sql += "      and isnull(b.stock_qty,0)>=" + stock_qty;
            if (!string.IsNullOrEmpty(sup_no))
                sql += "      and a.sup_no = '" + sup_no + "'";
            if (!string.IsNullOrEmpty(barcode))
                sql += "      and a.barcode like '%" + barcode + "%' ";
            if (!string.IsNullOrEmpty(branch_no))
                sql += " AND b.branch_no like '" + branch_no + "%' ";

            sql += @" GROUP BY  a.item_subno ,
				a.barcode ,
				a.item_name ,
				a.item_size ,
				a.unit_no ,
				a.price ,
				a.sale_price ,
				a.product_area ,
				(a.item_clsno+'/'+ic.item_clsname) ,
				(a.sup_no+'/'+s.sup_name) ,
				a.min_stock,
				a.base_price ,
				a.item_no ,
				b.cost_price ";

            DataTable tb = db.ExecuteToTable(sql, "a.item_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetICFlow(DateTime start_time, DateTime end_time, string branch_no, string str, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select  f.oper_date 发生时间,
f.sheet_type 业务类型,
f.sheet_no 单号,
(f.branch_no+'/'+bi.branch_name) 仓库,
f.db_type '+/-',
f.init_qty 期初数量,
f.init_amt 期初金额,
f.new_qty 发生数量,
f.new_amt 发生金额,
f.settle_qty 结存数量,
f.settle_amt 结存金额,
f.cost_price 成本价,
f.adjust_amt 调账金额,
f.sale_price 业务单价,
i.unit_no 单位,
i.item_size 规格,
i.item_subno 货号,
i.item_name 商品名称,
i.barcode 条码,
(f.supcust_no+'/'+s.sup_name) '供应商/客商',
f.voucher_no 关联单号,
f.flow_id 流水号
from ic_t_flow_dt f
LEFT JOIN bi_t_item_info i on i.item_no = f.item_no
LEFT JOIN ic_t_branch_stock b on b.item_no=i.item_no
LEFT JOIN bi_t_branch_info bi on bi.branch_no=b.branch_no
LEFT JOIN bi_t_item_cls ic on ic.item_clsno=i.item_clsno and ic.item_flag='0'
LEFT JOIN bi_t_supcust_info s on s.supcust_no=f.supcust_no and s.supcust_flag='S'
where Convert(varchar(10),f.oper_date  ,20 ) BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(branch_no))
                sql += " and b.branch_no like '" + branch_no + "%'";
            if (!string.IsNullOrEmpty(str))
                sql += " and (i.item_no like '%" + str + "%' or i.barcode like '%" + str + "%' or i.item_subno like '%" + str + "%' or i.item_name like '%" + str + "%' )";

            DataTable tb = db.ExecuteToTable(sql, " f.oper_date", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetICOutDetail(DateTime start_time, DateTime end_time, string branch_no, string barcode, string item_name, string sheet_no, string item_clsno, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
a.sheet_no 单号,
 a.voucher_no 原始单号,
 b.item_subno 货号,
 a.item_name 商品名称,
 a.barcode 条码,
 (c.branch_no+'/'+bi.branch_name) 仓库,
 (c.supcust_no+'/'+s.sup_name) 供应商,
 (c.oper_id+'/'+o1.oper_name) 操作人,
 (c.approve_man+'/'+o2.oper_name) 审核人,
 c.oper_date 操作时间,
 c.approve_date 审核时间,
 isnull(a.in_qty,0) 数量,
 isnull(a.in_qty*a.orgi_price,0.00) 金额
FROM
  ic_t_inout_store_detail a
 left join 	ic_t_inout_store_master c on a.sheet_no=c.sheet_no
 left join 	bi_t_item_info b on a.item_no = b.item_no
 left join bi_t_branch_info bi  on bi.branch_no=c.branch_no
 left join bi_t_item_cls ic on ic.item_clsno=b.item_clsno 
 left join bi_t_supcust_info s  on s.supcust_no=c.supcust_no and s.supcust_flag='S'
 left join sa_t_operator_i o1 on o1.oper_id=c.oper_id 
 left join sa_t_operator_i o2 on o2.oper_id=c.approve_man
WHERE	  c.trans_no in ('01','03')
AND c.approve_flag = '1'
AND Convert(varchar(10), c.approve_date ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(branch_no))
                sql += " AND c.branch_no = '" + branch_no + "' ";
            if (!string.IsNullOrEmpty(barcode))
                sql += " AND b.barcode like '%" + barcode + "%' ";
            if (!string.IsNullOrEmpty(item_name))
                sql += " and b.item_name like '%" + item_name + "%' ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " and c.sheet_no like '%" + sheet_no + "%'";
            if (!string.IsNullOrEmpty(item_clsno))
                sql += "and b.item_clsno like  '" + item_clsno + "%'";



            DataTable tb = db.ExecuteToTable(sql, "a.sheet_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetJXCSum(DateTime start_time, DateTime end_time, string branch_no, string item_clsno, string item_name, string barcode, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"
select 
f.branch_no 仓库,
i.item_subno 货号,
i.barcode 条码,
i.item_name 商品名称,
i.unit_no 单位,
i.item_size 规格,
f1.init_qty 期初数量,
f1.init_amt 期初金额,
isnull(sum(case when f.db_type='+' then f.new_qty end ),0.00) 入库总数量,
isnull(sum(case when f.db_type='+' then f.new_amt end),0.00)  入库总金额,
isnull(sum(case when f.db_type='-' then f.new_qty end ),0.00)  出库总数量,
isnull(sum(case when f.db_type='-' then f.new_amt end),0.00)  出库总金额,
f2.settle_qty 结存数量,
f2.settle_amt 结存余额,
i.item_clsno 商品类别
from bi_t_item_info i
LEFT JOIN  ic_t_flow_dt f on f.item_no=i.item_no
left join  
(
	select t.* from 
	ic_t_flow_dt t
	inner join (
	select branch_no,item_no,min(oper_date) oper_date
	from ic_t_flow_dt 
	group by branch_no,item_no
	) f on f.branch_no=t.branch_no and t.item_no=f.item_no and t.oper_date = f.oper_date
) f1 on f1.branch_no=f.branch_no and f1.item_no=f.item_no
left join 
(
	select t.* from 
	ic_t_flow_dt t
	inner join (
	select branch_no,item_no,max(oper_date) oper_date
	from ic_t_flow_dt 
	group by branch_no,item_no
	) f on f.branch_no=t.branch_no and t.item_no=f.item_no and t.oper_date = f.oper_date
) f2 on f2.branch_no=f.branch_no and f2.item_no=f.item_no
where f.flow_id is not null
AND Convert(varchar(10),  f.oper_date,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(branch_no))
                sql += " AND f.branch_no = '" + branch_no + "' ";
            if (!string.IsNullOrEmpty(item_clsno))
                sql += " and i.item_clsno like  '" + item_clsno + "%' ";
            if (!string.IsNullOrEmpty(item_name))
                sql += " and i.item_name like '%" + item_name + "%' ";
            if (!string.IsNullOrEmpty(barcode))
                sql += " and i.barcode like '%" + barcode + "%' ";


            sql += @"GROUP BY f.branch_no ,
i.item_subno ,
i.barcode ,
i.item_name ,
i.unit_no ,
i.item_size ,
f1.init_qty,
f1.init_amt,
f2.settle_qty ,
f2.settle_amt ,
i.item_clsno ";

            DataTable tb = db.ExecuteToTable(sql, "i.item_subno", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetPmDetail(DateTime start_time, DateTime end_time, string barcode, string item_name, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select 
m.sheet_no 调价单号,
m.approve_man 审核人,
m.approve_date 审核日期,
m.oper_id 操作员,
m.oper_date 操作日期 ,
m.deal_man 经手人,
i.item_subno 货号,
i.barcode 条码,
i.item_name 商品名称,
i.unit_no 单位,
i.item_size 规格,
d.old_price 原批发价1,
d.new_price 新批发价1,
d.old_price2 原批发价2,
d.new_price2 新批发价2,
d.old_price3 原批发价3,
d.new_price3 新批发价3,
(i.item_clsno+'/'+ic.item_clsname )类别,
i.product_area 产地
from pm_t_flow_main m
INNER JOIN pm_t_price_flow_detial d on m.sheet_no=d.sheet_no
INNER JOIN bi_t_item_info i on i.item_no = d.item_no
INNER JOIN bi_t_item_cls ic on ic.item_clsno=i.item_clsno and ic.item_flag='0'
INNER JOIN sa_t_operator_i o1 on o1.oper_id=m.oper_id
INNER JOIN sa_t_operator_i o2 on o2.oper_id=m.approve_man 
LEFT JOIN bi_t_people_info o3 on o3.oper_id=m.deal_man
where Convert(varchar(10),m.approve_date  ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(barcode))
                sql += " and i.barcode like '%" + barcode + "%' ";
            if (!string.IsNullOrEmpty(item_name))
                sql += " and i.item_name like '%" + item_name + "%' ";


            DataTable tb = db.ExecuteToTable(sql, "m.sheet_no", null, page_size, page_index, out total_count);

            return tb;
        }
        #endregion

        #region 盘点进度报告
        public DataTable GetCheckPlan(DateTime start_time, DateTime end_time, string branch_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select 
b.sheet_no 盘点批次,
(i.branch_no+'/'+branch.branch_name) 仓库,
(i.item_clsno+'/'+c.item_clsname) 商品类别,
(i.oper_id+'/'+oi.oper_name) 操作员,
case when i.approve_flag='0' then '未审核'
else '已审核' end 审核情况,
case when i.check_status='0' then '开始盘点'
else '盘点结束' end 盘点情况,
i.begin_date 开始时间,
i.end_date 结束时间,
sum(case when f.real_qty=b.stock_qty then 1 else 0 end) 盘对品种,
sum(case when f.real_qty=b.stock_qty then f.real_qty else 0 end) 盘对数量,
sum(case when f.real_qty>b.stock_qty then 1 else 0 end) 盘盈品种,
sum(case when f.real_qty>b.stock_qty then f.real_qty-b.stock_qty else 0 end) 盘盈数量,
sum(case when f.real_qty<b.stock_qty then 1 else 0 end) 盘亏品种,
sum(case when f.real_qty<b.stock_qty then b.stock_qty-f.real_qty else 0 end) 盘亏数量,
f1.多盘品种,
f1.多盘数量,
sum(case when f.item_no is NULL then 1 else 0 end) 未盘品种
from ic_t_check_bak b
left join ic_t_check_finish f on f.sheet_no=b.sheet_no and f.item_no=b.item_no
left join 
(select f.sheet_no,count(1) 多盘品种,sum(f.real_qty) 多盘数量 from ic_t_check_bak b 
RIGHT JOIN ic_t_check_finish f on f.sheet_no = b.sheet_no and f.item_no=b.item_no 
where b.sheet_no is null
group by f.sheet_no ) 
f1 on f1.sheet_no=b.sheet_no
left join ic_t_check_init i on i.sheet_no=b.sheet_no
left join bi_t_branch_info branch on branch.branch_no=b.branch_no
left join bi_t_item_cls c on c.item_clsno=i.item_clsno and c.item_flag='0'
left join sa_t_operator_i oi on oi.oper_id=i.oper_id
where i.check_status!='2' and Convert(varchar(10), i.create_time ,20 )   BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "' and '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(branch_no))
                sql += " and i.branch_no='" + branch_no + "' ";

            sql += @" group by b.sheet_no ,
(i.item_clsno+'/'+c.item_clsname) ,
(i.branch_no+'/'+branch.branch_name) ,
(i.oper_id+'/'+oi.oper_name) ,
i.approve_flag,
i.check_status,
i.begin_date ,
i.end_date ,
f1.多盘品种,
f1.多盘数量 ";

            DataTable tb = db.ExecuteToTable(sql, "b.sheet_no", null, page_size, page_index, out total_count);
            return tb;
        }
        public DataTable GetCheckPlanDetail(DateTime start_time, DateTime end_time, string sheet_no, string barcode, string item_clsno, string branch_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select * from ((select 
i.begin_date 开始时间,
b.sheet_no 批次号,
b.branch_no,
(b.branch_no+'/'+branch.branch_name) 仓库,
case when f.real_qty=b.stock_qty then '盘对'
	   when f.real_qty>b.stock_qty then '盘盈'
	   when f.real_qty<b.stock_qty then '盘亏'
     when f.item_no is null then '未盘'
	   end 盈亏状况,
item.item_no 商品编码,
item.item_subno 货号,
item.barcode 主条码,
item.item_name 商品名称,
f.real_qty 实盘数量,
item.item_size 规格,
item.unit_no 单位,
item.price 进价,
b.cost_price 库存价,
item.sale_price 零售价,
b.stock_qty 帐面数量,
isnull((f.real_qty *b.cost_price ),0.00) 盘点金额,
isnull((f.real_qty *b.price ),0.00) 进价额,
isnull((f.real_qty *b.sale_price ),0.00) 售价额,
isnull((f.real_qty-b.stock_qty),0.00) 盈亏数量,
isnull(((f.real_qty-b.stock_qty)*b.cost_price),0.00)  盈亏进额,
isnull(((f.real_qty-b.stock_qty)*b.sale_price),0.00)  盈亏售额,
isnull((((f.real_qty-b.stock_qty)*b.sale_price)-((f.real_qty-b.stock_qty)*b.cost_price)),0.00) 盈亏进销差,
item.item_clsno,
(item.item_clsno+'/'+c.item_clsname) 商品类别
from  ic_t_check_bak b
left join ic_t_check_finish f on f.sheet_no=b.sheet_no and f.item_no=b.item_no
left join ic_t_check_init i on i.sheet_no=b.sheet_no
left join bi_t_branch_info branch on branch.branch_no=b.branch_no
left join bi_t_item_info item on item.item_no=b.item_no
left join bi_t_item_cls c on c.item_clsno=item.item_clsno and c.item_flag='0'
left join sa_t_operator_i oi on oi.oper_id=i.oper_id
where  i.check_status!='2' 
)
union 
(select 
i.begin_date 开始时间,
f.sheet_no 批次号,
f.branch_no,
(f.branch_no+'/'+branch.branch_name) 仓库,
'多盘' 盈亏状况,
item.item_no 商品编码,
item.item_subno 货号,
item.barcode 主条码,
item.item_name 商品名称,
f.real_qty 实盘数量,
item.item_size 规格,
item.unit_no 单位,
item.price 进价,
b.cost_price 库存价,
item.sale_price 零售价,
b.stock_qty 帐面数量,
isnull((f.real_qty *b.cost_price ),0.00) 盘点金额,
isnull((f.real_qty *b.price ),0.00) 进价额,
isnull((f.real_qty *b.sale_price ),0.00) 售价额,
isnull((f.real_qty-b.stock_qty),0.00) 盈亏数量,
isnull(((f.real_qty-b.stock_qty)*b.cost_price),0.00)  盈亏进额,
isnull(((f.real_qty-b.stock_qty)*b.sale_price),0.00)  盈亏售额,
isnull((((f.real_qty-b.stock_qty)*b.sale_price)-((f.real_qty-b.stock_qty)*b.cost_price)),0.00) 盈亏进销差,
item.item_clsno,
(item.item_clsno+'/'+c.item_clsname) 商品类别
from  ic_t_check_bak b
right join ic_t_check_finish f on f.sheet_no=b.sheet_no and f.item_no=b.item_no
left join ic_t_check_init i on i.sheet_no=f.sheet_no
left join bi_t_branch_info branch on branch.branch_no=f.branch_no
left join bi_t_item_info item on item.item_no=f.item_no
left join bi_t_item_cls c on c.item_clsno=item.item_clsno and c.item_flag='0'
left join sa_t_operator_i oi on oi.oper_id=i.oper_id
where   i.check_status!='2' and  b.item_no is null ))t
where Convert(varchar(10), t.开始时间 ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "' and '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(sheet_no))
                sql += " and t.批次号 like '%" + sheet_no + "%' ";
            if (!string.IsNullOrEmpty(barcode))
                sql += " and t.主条码 like '%" + barcode + "%' ";
            if (!string.IsNullOrEmpty(item_clsno))
                sql += " and t.item_clsno = '" + item_clsno + "' ";
            if (!string.IsNullOrEmpty(branch_no))
                sql += " AND t.branch_no = '" + branch_no + "' ";

            DataTable tb = db.ExecuteToTable(sql, "t.盈亏状况", null, page_size, page_index, out total_count);

            return tb;
        }
        #endregion

        #region 财务
        public DataTable GetRpCusSum(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.supcust_no 客户编号,
	a.sheet_no 单号,
	b.sup_name 客户名称,
	a.total_amount 收款金额,
	a.free_money 免收款金额,
	a.approve_flag 生效,
	a.oper_date 制单日期,
	a.approve_date  生效时间,
	(a.branch_no+'/'+br.branch_name) 机构
FROM
	rp_t_recpay_record_info a,
	bi_t_supcust_info b,
  bi_t_branch_info br
WHERE
	a.supcust_no = b.supcust_no
AND a.supcust_flag = b.supcust_flag
and br.branch_no=a.branch_no
AND a.supcust_flag = 'c'
and Convert(varchar(10), a.approve_date ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (a.supcust_no LIKE '%" + supcust_no + "%')  ";

            DataTable tb = db.ExecuteToTable(sql, "a.supcust_no ", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpCusDetail(DateTime start_time, DateTime end_time, string supcust_no, string deal_man, string sheet_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.pay_date 限付日期,
	a.sheet_no 单号,
	a.voucher_no 业务单号,
	a.sheet_amount 本次应收金额,
	a.paid_amount 本次收款金额,
	a.pay_amount 已收金额,
	a.pay_free 已免收金额,
	b.supcust_no 客户编号,
	b.total_amount 单据金额,
	b.free_money 免收金额,
	b.approve_date 审核日期,
	b.oper_id 操作员,
	b.oper_date 操作日期,
	b.deal_man 经手人,
	b.approve_man 审核人,
	a.sheet_amount - a.paid_amount - a.paid_free AS should_money,
	substring(a.voucher_no, 1, 2)  业务类型,
	c.sup_name 客户名称
FROM
	rp_t_recpay_record_info b
LEFT OUTER JOIN rp_t_recpay_record_detail a ON b.sheet_no = a.sheet_no,
 bi_t_supcust_info c
WHERE
	c.supcust_no = b.supcust_no
AND b.supcust_flag = 'c'
and c.supcust_flag='C'
and Convert(varchar(10), b.approve_date ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (b.supcust_no = '" + supcust_no + "') ";
            if (!string.IsNullOrEmpty(deal_man))
                sql += " AND (b.deal_man =  '" + deal_man + "') ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " AND (a.sheet_no LIKE '%" + sheet_no + "%') ";


            DataTable tb = db.ExecuteToTable(sql, "a.sheet_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpSupSum(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.supcust_no 供应商编码,
	a.sheet_no 单据号,
	a.total_amount 付款金额,
	a.free_money 免付款金额,
	b.sup_name 供应商名称,
	a.approve_flag 生效,
	a.approve_date 生效日期,
	a.oper_date 制单日期
FROM
	rp_t_recpay_record_info a,
	bi_t_supcust_info b
WHERE
	a.supcust_no = b.supcust_no
AND a.supcust_flag = b.supcust_flag
AND a.supcust_flag = 's'
and Convert(varchar(10), a.approve_date ,20 ) BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (a.supcust_no = '" + supcust_no + "') ";

            DataTable tb = db.ExecuteToTable(sql, "a.supcust_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpSupDetail(DateTime start_time, DateTime end_time, string sheet_no, string deal_man, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.pay_date 限付日期,
	a.sheet_no 单号,
	a.voucher_no 业务单号,
 a.memo 备注,
 b.supcust_no 供应商编号,
 b.total_amount 单据金额,
 b.oper_id 操作员,
 b.oper_date 操作日期,
 b.deal_man 经办人,
 b.approve_man 审核人,
 b.approve_date 审核日期,
 a.sheet_amount - a.paid_amount - a.paid_free AS 本次应付款金额,
 substring(a.voucher_no, 1, 2) 业务类型,
 c.sup_name 供应商名称,
 CASE a.path
WHEN '-' THEN
	- 1
ELSE
	1
END * a.paid_amount 已付金额,
 CASE a.path
WHEN '-' THEN
	- 1
ELSE
	1
END * a.paid_free  已免付金额
FROM
	rp_t_recpay_record_info b
LEFT OUTER JOIN rp_t_recpay_record_detail a ON b.sheet_no = a.sheet_no,
 bi_t_supcust_info c
WHERE
	b.supcust_flag = 's'
AND c.supcust_no = b.supcust_no
and Convert(varchar(10),  b.approve_date,20 )   BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(deal_man))
                sql += " AND (b.deal_man = '" + deal_man + "') ";
            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (b.supcust_no LIKE '%" + supcust_no + "%')";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " AND (a.sheet_no LIKE '%" + sheet_no + "%') ";

            DataTable tb = db.ExecuteToTable(sql, "a.sheet_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpSupAccount(DateTime start_time, DateTime end_time, string sheet_no, string oper_type, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT 
	a.supcust_no 供应商编号, 
	a.oper_type 业务类型,
	a.voucher_no 单号,
	a.oper_date 操作日期,
	a.old_money 期初金额,
	a.new_money 结余金额,
	a.oper_money 本期发生额,
	a.free_money 本期免付额,
	b.sup_name 供应商名称,
	e.oper_date 制单日期,
  d.pay_name 费用名称
FROM
	rp_t_supcust_cash_flow a
LEFT OUTER JOIN bi_t_supcust_info b ON a.supcust_no = b.supcust_no
LEFT JOIN rp_t_supcust_fy_detail c ON a.voucher_no = c.sheet_no
LEFT JOIN bi_t_sz_type d ON c.kk_no = d.pay_way
LEFT JOIN ic_t_inout_store_master e ON e.sheet_no = a.voucher_no
WHERE
	a.supcust_flag IN ('s', 'b')
AND a.supcust_flag = 's'
AND b.supcust_flag = 's'
and Convert(varchar(10), a.oper_date ,20 )   BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "'";

            if (!string.IsNullOrEmpty(oper_type))
                sql += " AND (a.oper_type like  '%" + oper_type + "%') ";
            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (a.supcust_no = '" + supcust_no + "' ) ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " AND (a.voucher_no LIKE '%" + sheet_no + "%') ";

            DataTable tb = db.ExecuteToTable(sql, "a.supcust_no,	a.flow_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpCusAccount(DateTime start_time, DateTime end_time, string sheet_no, string oper_type, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @" SELECT
	a.supcust_no 客户编号, 
	a.oper_type 业务类型,
	a.voucher_no 单号,
	a.oper_date 操作日期,
	a.old_money 期初金额,
	a.new_money 结余金额,
	a.oper_money 本期发生额,
	a.free_money 本期免付额,
	b.sup_name 客户名称,
  d.pay_name 费用名称 
FROM 
	rp_t_supcust_cash_flow a 
LEFT OUTER JOIN bi_t_supcust_info b ON a.supcust_no = b.supcust_no 
LEFT JOIN rp_t_supcust_fy_detail c ON a.voucher_no = c.sheet_no 
LEFT JOIN bi_t_sz_type d ON c.kk_no = d.pay_way  
WHERE 	a.supcust_flag IN ('c', 'h')
AND b.supcust_flag = 'c'
AND a.supcust_flag = 'c'
and Convert(varchar(10), a.oper_date ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "'	AND '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(oper_type))
                sql += " AND (a.oper_type like  '%" + oper_type + "%') ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " AND (a.voucher_no LIKE '%" + sheet_no + "%') ";
            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (a.supcust_no = '" + supcust_no + "' ) ";


            DataTable tb = db.ExecuteToTable(sql, "a.supcust_no,	a.flow_no ", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpTodayInc(string sheet_no, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
  a.voucher_no 单据号,
	a.pay_type * a.sheet_amount   单据金额,
	a.pay_type * a.paid_amount  已收金额,
	a.pay_type * a.free_money 免收金额,
	a.pay_type * a.sheet_amount - a.pay_type * a.paid_amount - a.pay_type * a.free_money AS 欠款金额,
	datediff(
       dd,
		isnull(a.pay_date, getdate()),
		getdate()
	) 逾期天数,
	a.supcust_no 客户编号,
	isnull(a.pay_date, getdate()) 限定日期,
	b.sup_name 客户名称
FROM
	rp_t_supcust_fy_detail d
INNER JOIN rp_t_supcust_fy_master c ON d.sheet_no = c.sheet_no
INNER JOIN bi_t_sz_type e ON d.kk_no = e.pay_way
RIGHT OUTER JOIN rp_t_accout_payrec_flow a ON c.sheet_no = a.voucher_no
INNER JOIN bi_t_supcust_info b ON a.supcust_no = b.supcust_no and b.supcust_flag='C'
WHERE
	(a.supcust_flag = 'c')
AND (
	a.pay_type * a.sheet_amount <> a.pay_type * (a.paid_amount + a.free_money)
)
and Convert(varchar(10), a.oper_date ,20 )=Convert(varchar(10), getdate() ,20 )";

            if (!string.IsNullOrEmpty(sheet_no))
                sql += " AND (a.voucher_no LIKE '%" + sheet_no + "%') ";
            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (a.supcust_no ='" + supcust_no + "' ) ";

            DataTable tb = db.ExecuteToTable(sql, "a.supcust_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpTodayPay(string sheet_no, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.voucher_no 单据号,
	a.pay_type * a.sheet_amount 单据金额,
	a.pay_type * a.paid_amount 已付金额,
	a.pay_type * a.free_money 免付金额,
	a.pay_type * a.sheet_amount - a.pay_type * a.paid_amount - a.pay_type * a.free_money 欠款金额,
	datediff(
dd,
		isnull(a.pay_date, getdate()),
		getdate()
	) 逾期天数,
	a.supcust_no 供应商编号,
	isnull(a.pay_date, getdate()) 限定日期,
	a.memo 备注,
	b.sup_name 供应商名称
FROM
	rp_t_supcust_fy_detail d
INNER JOIN rp_t_supcust_fy_master c ON d.sheet_no = c.sheet_no
INNER JOIN bi_t_sz_type e ON d.kk_no = e.pay_way
RIGHT OUTER JOIN rp_t_accout_payrec_flow a ON c.sheet_no = a.voucher_no
INNER JOIN bi_t_supcust_info b ON a.supcust_no = b.supcust_no  and b.supcust_flag='C'
WHERE
	(a.supcust_flag = 's')
AND b.supcust_flag = 's'
AND (
	a.pay_type * a.sheet_amount <> a.pay_type * (a.paid_amount + a.free_money)
)
and Convert(varchar(10), a.oper_date ,20 )=Convert(varchar(10), getdate() ,20 ) ";

            if (!string.IsNullOrEmpty(sheet_no))
                sql += " and c.sheet_no like '%" + sheet_no + "%' ";
            if (!string.IsNullOrEmpty(supcust_no))
                sql += " and c.supcust_no = '" + supcust_no + "' ";


            DataTable tb = db.ExecuteToTable(sql, "a.supcust_no", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpCusFyDetail(DateTime start_time, DateTime end_time, string supcust_no, string kk_no, string sheet_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.sheet_no 单据号,
	a.supcust_no 客户编号,
	c.sup_name 客户名称,
	a.pay_type 应收增减,
	a.approve_man 审核人,
	b.kk_no 费用代码,
  z.pay_name 费用名称,
	b.kk_cash 费用金额,
	a.approve_date 审核日期,
	a.oper_date 操作日期,
	a.branch_no 仓库
FROM
	rp_t_supcust_fy_detail b,
	rp_t_supcust_fy_master a,
	bi_t_supcust_info c,
  bi_t_sz_type z
WHERE
	a.sheet_no = b.sheet_no
and z.pay_way=b.kk_no
AND a.supcust_no = c.supcust_no
AND c.supcust_flag = a.supcust_flag
AND a.supcust_flag = 'c'
AND c.supcust_flag = 'c'
and Convert(varchar(10), a.oper_date ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "' and '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(kk_no))
                sql += " AND (b.kk_no = '" + kk_no + "') ";
            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (a.supcust_no = '" + supcust_no + "') ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " AND (a.sheet_no LIKE '%" + sheet_no + "%') ";


            DataTable tb = db.ExecuteToTable(sql, "a.supcust_no ASC,a.sheet_no ASC,b.flow_id ASC", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpSupFyDetail(DateTime start_time, DateTime end_time, string supcust_no, string kk_no, string sheet_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.sheet_no 单据号,
	a.supcust_no 供应商编号,
	c.sup_name 供应商名称,
	a.pay_type 应收增减,
	a.approve_man 审核人,
	b.kk_no 费用代码,
  z.pay_name 费用名称,
	b.kk_cash 费用金额,
	a.approve_date 审核日期,
	a.oper_date 操作日期,
	a.branch_no 仓库
FROM
	rp_t_supcust_fy_detail b,
	rp_t_supcust_fy_master a,
	bi_t_supcust_info c,
  bi_t_sz_type z
WHERE
	(a.sheet_no = b.sheet_no)
 and z.pay_way=b.kk_no
AND (a.supcust_no = c.supcust_no)
AND (
	c.supcust_flag = a.supcust_flag
)
AND (
	(a.supcust_flag = 's')
	AND (c.supcust_flag = 's')
)
and  Convert(varchar(10), a.oper_date ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "' and '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(supcust_no))
                sql += " AND (	a.supcust_no = '" + supcust_no + "') ";
            if (!string.IsNullOrEmpty(kk_no))
                sql += " AND (b.kk_no IN('" + kk_no + "')) ";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " AND (a.sheet_no LIKE '%" + sheet_no + "%') ";

            DataTable tb = db.ExecuteToTable(sql, "a.supcust_no ASC,a.sheet_no ASC,b.flow_id ASC", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpCashBank(DateTime start_time, DateTime end_time, string visa_id, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select * from (
select '前结' 费用代码,'前结' 费用名称,'' 收支时间,
 isnull(sum((CASE WHEN s.account_flag = '0' THEN 1 ELSE -1 END) * d.bill_cash),0.00) 期初,
0.00 收入,0.00 付出,0.00 期末,'' 收支账户编号,'' 收支帐户名称,'' 单据号,'' 操作时间,'' 机构仓库
from bank_t_cash_master  m
INNER JOIN bank_t_cash_detail d on m.sheet_no = d.sheet_no  
INNER JOIN bi_t_sz_type s on  s.pay_way = d.type_no  
where   Convert(varchar(10), m.approve_date ,20 ) <= '@start_time'
AND s.is_account = '1' and m.visa_id='@visa_id'
group by m.visa_id

union all

select 
d.type_no 费用代码,
z.pay_name 费用名称,
m.approve_date 收支时间,
0.00 期初,
isnull(sum((CASE WHEN s.account_flag = '0' THEN 1  END) * d.bill_cash),0.00) 收入,
isnull(sum((CASE WHEN s.account_flag = '1' THEN -1  END) * d.bill_cash),0.00) 付出,
0.00 期末,
m.visa_id 收支账户编号,
b.visa_nm 收支帐户名称,
m.sheet_no 单据号,
m.oper_date 操作时间,
( m.branch_no+'/'+br.branch_name) 机构仓库
from 
bank_t_cash_master m
left join  bank_t_cash_detail d on d.sheet_no=m.sheet_no
left join  bi_t_bank_info b on b.visa_id=m.visa_id
left join  bi_t_sz_type z on z.pay_way=d.type_no
LEFT JOIN  bi_t_branch_info br on br.branch_no=m.branch_no
LEFT JOIN bi_t_sz_type s on  s.pay_way = d.type_no  
where  Convert(varchar(10),m.approve_date  ,20 ) BETWEEN '@start_time' and '@end_time' 
and m.visa_id ='@visa_id'
group by d.type_no ,
z.pay_name ,
m.approve_date ,
m.visa_id ,
b.visa_nm ,
m.sheet_no ,
m.oper_date ,
( m.branch_no+'/'+br.branch_name) 

union all

select '结余','结余','',0.00,0.00,0.00, 
isnull(sum((CASE WHEN s.account_flag = '0' THEN 1 ELSE -1 END) * d.bill_cash) ,0.00) 结余,
'','','','',''
from bank_t_cash_master  m
INNER JOIN bank_t_cash_detail d on m.sheet_no = d.sheet_no  
INNER JOIN bi_t_sz_type s on  s.pay_way = d.type_no  
where  Convert(varchar(10), m.approve_date ,20 )  >= '@end_time'
AND s.is_account = '1' and m.visa_id='@visa_id' 
group by m.visa_id) n
"
                .Replace("@start_time", start_time.ToString("yyyy-MM-dd"))
                .Replace("@end_time", end_time.ToString("yyyy-MM-dd"))
                .Replace("@visa_id", visa_id);

            DataTable tb = db.ExecuteToTable(sql, "n.费用代码", null, page_size, page_index, out total_count);

            return tb;
        }
        public DataTable GetRpAdminCost(DateTime start_time, DateTime end_time, string sheet_no, string type_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT
	a.sheet_no 单号,
(	a.branch_no +'/'+br.branch_name)仓库,
  a.bill_total 金额,
	a.pay_way 费用代码,
	(a.deal_man+'/'+p.oper_name) 经办人,
	(a.oper_id +'/'+o1.oper_name)制单人,
	a.oper_date 制单时间,
	c.pay_name 费用名称,
	b.memo 摘要,
	(a.approve_man +'/'+o2.oper_name)审核人,
	a.approve_date 审核时间
FROM
	bank_t_cash_detail b,
	bank_t_cash_master a,
  sa_t_operator_i o1,
  sa_t_operator_i o2,
  bi_t_people_info p,
  bi_t_branch_info br,
	bi_t_sz_type c
WHERE
	(a.sheet_no = b.sheet_no)
and br.branch_no=a.branch_no
and p.oper_id=a.deal_man
and o1.oper_id=a.oper_id
and o2.oper_id=a.approve_man
AND (b.type_no = c.pay_way)
AND c.is_account = '1'
AND isnull(c.pay_kind, 'a') = 'a'
and Convert(varchar(10), a.oper_date ,20 )  BETWEEN '" + start_time.ToString("yyyy-MM-dd") + "' and '" + end_time.ToString("yyyy-MM-dd") + "' ";

            if (!string.IsNullOrEmpty(sheet_no))
                sql += " and a.sheet_no like '%" + sheet_no + "%' ";
            if (!string.IsNullOrEmpty(type_no))
                sql += " AND (b.type_no IN('" + type_no + "')) ";

            DataTable tb = db.ExecuteToTable(sql, "a.oper_date ASC,a.sheet_no ASC", null, page_size, page_index, out total_count);

            return tb;
        }
        #endregion

        #region 采购助手
        public DataTable GetAssCGFlow(DateTime start_time, DateTime end_time, string supcust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT d.sup_no 供应商编码,s.sup_name 供应商名称,d.item_no 商品编号,i.item_subno 货号,d.item_name 商品名称,d.barcode 条码,d.unit_no 单位,
d.price 价格,d.cg_qty 采购数量,d.create_time 时间
FROM dbo.co_t_cg_order_detail d
LEFT JOIN dbo.bi_t_supcust_info s ON d.sup_no=s.supcust_no AND s.supcust_flag='S'
LEFT JOIN dbo.bi_t_item_info i ON i.item_no=d.item_no
WHERE d.create_time BETWEEN '" + start_time.Toyyyy_MM_dd() + " 00:00:00.000' AND '" + end_time.Toyyyy_MM_dd() + " 23:59:59.000'";

            if (!string.IsNullOrEmpty(supcust_no))
            {
                sql += " AND d.sup_no='" + supcust_no + "' ";
            }

            sql += " ORDER BY d.create_time desc ";

            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }
        public DataTable GetAssCGPlanDetail(DateTime start_time, DateTime end_time, string keyword, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT d.flow_id 行号,s.supcust_no 供应商编号,s.sup_name 供应商名称,d.item_no 商品编号,d.item_name 商品名称,
d.barcode 条码,d.unit_no 单位,d.price 价格,d.cg_qty 数量,d.create_time  操作时间,
(CASE WHEN i.flow_id IS NULL THEN 0 ELSE 1 END ) 是否生成
FROM dbo.co_t_cg_order_app_detail d
LEFT JOIN dbo.bi_t_supcust_info s ON d.sup_no=s.supcust_no AND s.supcust_flag='S'
LEFT JOIN dbo.ic_t_inout_store_detail i ON i.other2='Ass-'+CONVERT(VARCHAR(50),d.flow_id) 
WHERE (d.ph_sheet_no != '' AND d.ph_sheet_no IS NOT NULL)
AND (CONVERT(VARCHAR(10), d.create_time, 20) BETWEEN '" + start_time.Toyyyy_MM_dd() + "' AND '" + end_time.Toyyyy_MM_dd() + "')";

            if (!string.IsNullOrEmpty(keyword))
                sql += " AND (d.item_no LIKE '%" + keyword + "%' OR d.item_name LIKE '%" + keyword + "%' OR d.barcode LIKE '%" + keyword + "%') ";

            sql += " ORDER BY s.supcust_no,d.item_no ";

            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }
        public DataTable GetAssCGPlanDetailExport(DateTime start_time, DateTime end_time, string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT c.item_clsno,c.item_clsname,s.supcust_no,s.sup_name,d.item_no,d.item_name,
d.barcode,d.unit_no,d.price,SUM(d.cg_qty) cg_qty,d.price*SUM(d.cg_qty) sub_amount
FROM dbo.co_t_cg_order_app_detail d
LEFT JOIN dbo.bi_t_supcust_info s ON d.sup_no=s.supcust_no AND s.supcust_flag='S'
LEFT JOIN dbo.bi_t_item_info i ON i.item_no=d.item_no
LEFT JOIN dbo.bi_t_item_cls c ON c.item_clsno=i.item_clsno
WHERE (d.ph_sheet_no != '' AND d.ph_sheet_no IS NOT NULL)
AND (CONVERT(VARCHAR(10), d.create_time, 20) BETWEEN '" + start_time.Toyyyy_MM_dd() + "' AND '" + end_time.Toyyyy_MM_dd() + "')";

            if (!string.IsNullOrEmpty(keyword))
                sql += " AND (d.item_no LIKE '%" + keyword + "%' OR d.item_name LIKE '%" + keyword + "%' OR d.barcode LIKE '%" + keyword + "%') ";

            sql += @" GROUP BY c.item_clsno,c.item_clsname,s.supcust_no,s.sup_name,d.item_no,d.item_name,d.barcode,d.unit_no,d.price
ORDER BY c.item_clsno,s.supcust_no,d.item_no ";

            DataTable tb = db.ExecuteToTable(sql, null);

            return tb;
        }
        public DataTable GetAssCGPreDetail(DateTime start_time, DateTime end_time, string keyword, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT d.flow_id 行号,s.supcust_no 供应商编号,s.sup_name 供应商名称,d.item_no 商品编号,d.item_name 商品名称,
d.barcode 条码,d.unit_no 单位,d.price 价格,d.cg_qty 数量,d.create_time  操作时间,
(CASE WHEN i.flow_id IS NULL THEN 0 ELSE 1 END ) 是否生成
FROM dbo.co_t_cg_order_app_detail d
LEFT JOIN dbo.bi_t_supcust_info s ON d.sup_no=s.supcust_no AND s.supcust_flag='S'
LEFT JOIN dbo.ic_t_inout_store_detail i ON i.other2='AssPre-'+CONVERT(VARCHAR(50),d.flow_id) 
WHERE (d.ph_sheet_no IS NULL OR d.ph_sheet_no='')
AND (CONVERT(VARCHAR(10), d.create_time, 20) BETWEEN '" + start_time.Toyyyy_MM_dd() + "' AND '" + end_time.Toyyyy_MM_dd() + "')";

            if (!string.IsNullOrEmpty(keyword))
                sql += " AND (d.item_no LIKE '%" + keyword + "%' OR d.item_name LIKE '%" + keyword + "%' OR d.barcode LIKE '%" + keyword + "%') ";

            sql += " ORDER BY s.supcust_no,d.item_no ";

            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }

        #endregion

        #region 收获明细
        public DataTable GetReceiveOrderDetail(DateTime start_time, DateTime end_time,string item_no,string is_build, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"  SELECT d.flow_id 行号,
       d.po_sheet_no,
       d.sup_no 供应商编号,
s.sup_name 供应商名称,
d.batch_num ,
	   d.trans_no,
       d.item_no 商品编号,
       d.item_name 商品名称,
       d.barcode 条码,
       d.unit_no 单位,
       d.unit_factor,
       d.price 价格,
       d.order_qnty,
       d.receive_qty 数量,
       d.oper_date,
       d.oper_id,
       d.create_time 操作时间,
       s.sup_name 供应商名称,
       d.is_build 是否生成
FROM dbo.co_t_receive_order_detail d
LEFT JOIN dbo.bi_t_supcust_info s

  ON d.sup_no = s.supcust_no AND s.supcust_flag = 'S'
  left join bi_t_item_info ii on d.item_no=ii.item_no
WHERE CONVERT(VARCHAR(10),d.create_time,20) BETWEEN '" + start_time.Toyyyy_MM_dd() + "' AND '" + end_time.Toyyyy_MM_dd() + "' and  d.is_build like '%"+is_build+"%'";
            int a = 0;
            if (int.TryParse(item_no, out a))
            {
                sql += @" and ii.item_subno like '%"+item_no+"%'";
            }
            else
            {
                sql += @" and ii.item_name like '%" + item_no + "%'";
            }
            
            
                DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }

        #endregion

        #region 实拣报表

        public DataTable GetPickingDetail(DateTime start_time, DateTime end_time, string item, int page_index, int page_size,
            out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = string.Format(
                @"SELECT w.flow_id,w.rec_code  客户编号,s.sup_name 客户,i.item_subno 货号,i.barcode 条码,i.item_name 商品名称,i.unit_no 单位,
w.need_qty 要货数量,w.qty 实拣数量,w.bc_no   配送日期,o.oper_name 操作人,w.oper_date 操作日期,w.is_approve 审核状态
FROM dbo.ot_weighing w
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = w.item_no
LEFT JOIN dbo.sa_t_operator_i o ON o.oper_id = w.oper_id
LEFT JOIN dbo.bi_t_supcust_info s ON s.supcust_no=w.rec_code AND s.supcust_flag='C'
WHERE (w.oper_date BETWEEN '{0}' AND '{1}')
AND (w.item_no like '%{2}%' OR i.item_name LIKE '%{2}%' OR i.barcode LIKE '%{2}%') 
ORDER BY w.rec_code,s.sup_name,i.item_subno", start_time.Toyyyy_MM_dd_HH_mm_ss(), end_time.Toyyyy_MM_dd_HH_mm_ss(),
                item);
            // sql += " and s.supcust_no like '%" + cust_no + "%' ";
            DataTable tb = db.ExecuteToTable(sql, null);
            decimal TotalYQuan = 0;
            decimal TotalSQuan = 0;
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("客户编号");
            dt1.Columns.Add("客户");
            dt1.Columns.Add("货号");
            dt1.Columns.Add("条码");
            dt1.Columns.Add("商品名称");
            dt1.Columns.Add("单位");
            dt1.Columns.Add("要货数量", typeof(decimal));
            dt1.Columns.Add("实拣数量", typeof(decimal));
            dt1.Columns.Add("配送日期");
            dt1.Columns.Add("操作人");
            dt1.Columns.Add("操作日期");

            dt1.Columns.Add("审核状态");
            foreach (DataRow item1 in tb.Rows)
            {
                DataRow dr1 = dt1.NewRow();
                dr1["客户编号"] = item1["客户编号"].ToString();
                dr1["客户"] = item1["客户"].ToString();
                dr1["货号"] = item1["货号"].ToString();
                dr1["条码"] = item1["条码"].ToString();
                dr1["商品名称"] = item1["商品名称"].ToString();
                dr1["单位"] = item1["单位"].ToString();
                dr1["要货数量"] = item1["要货数量"].ToString().ToDecimal();
                dr1["实拣数量"] = item1["实拣数量"].ToString().ToDecimal();
                dr1["配送日期"] = item1["配送日期"].ToString();
                dr1["操作人"] = item1["操作人"].ToString();
                dr1["操作日期"] = item1["操作日期"].ToString();
                //TotalYQuan += item1["要货数量"].ToDecimal();
                //TotalSQuan += item1["实拣数量"].ToDecimal();
                dt1.Rows.Add(dr1);
            }

            //DataRow dr = dt1.NewRow();
            //dr["客户编号"] = "合计：";
            //dr["要货数量"] = TotalYQuan.ToDecimal();
            //dr["实拣数量"] = TotalSQuan.ToDecimal();
            //dr["操作日期"] = "";
           // dt1.Rows.Add(dr);
            total_count = 0;
            return dt1;
        }

        public DataTable GetPickingDiff(DateTime start_time, DateTime end_time, string item, int page_index, int page_size,
            out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = string.Format(
                @"SELECT w.rec_code  客户编号,s.sup_name 客户,i.item_subno 货号,i.barcode 条码,i.item_name 商品名称,i.unit_no 单位,
SUM(w.need_qty)要货数量汇总,MAX(w.need_qty)要货数量,SUM(w.qty )实拣数量,(SUM(w.qty)-MAX(w.need_qty)) 实拣差异,
MAX(w.bc_no) 配送日期,MAX(o.oper_name) 操作人,convert(varchar(10),w.oper_date,120) 操作日期
FROM dbo.ot_weighing w
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = w.item_no
LEFT JOIN dbo.sa_t_operator_i o ON o.oper_id = w.oper_id
LEFT JOIN dbo.bi_t_supcust_info s ON s.supcust_no=w.rec_code AND s.supcust_flag='C'
WHERE (w.oper_date BETWEEN '{0}' AND '{1}')
AND (w.item_no like '%{2}%' OR i.item_name LIKE '%{2}%' OR i.barcode LIKE '%{2}%')
GROUP BY w.rec_code,s.sup_name,i.item_subno,i.barcode,i.item_name,i.unit_no,convert(varchar(10),w.oper_date,120)
ORDER BY convert(varchar(10),w.oper_date,120),w.rec_code,s.sup_name,i.item_subno", start_time.Toyyyy_MM_dd_HH_mm_ss(), end_time.Toyyyy_MM_dd_HH_mm_ss(),
                item);

            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }
        #endregion

        #region 盘点

        public DataTable GetInventoryCheck(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"SELECT f.flow_id,f.item_no,i.item_subno 货号,i.item_name 商品名称,i.unit_no 单位,f.qty 实盘数量,f.oper_date 时间,f.is_append 已生成
FROM dbo.ot_check_flow f
LEFT JOIN dbo.bi_t_item_info i ON i.item_no = f.item_no
WHERE f.oper_date BETWEEN '" + start_time.Toyyyy_MM_dd() + " 00:00:00.00' AND '" + end_time.Toyyyy_MM_dd() + " 23:59:59.00'";

            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }


        #endregion

        #region 个人空间
        public DataTable GetOperatingStatement(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DataTable tb = new DataTable();
            tb.Columns.Add("客户销售");
            tb.Columns.Add("客户退货");
            tb.Columns.Add("销售品种");
            tb.Columns.Add("未销品种");
            tb.Columns.Add("采购进货");
            tb.Columns.Add("采购退货");
            tb.Columns.Add("采购品种");
            tb.Columns.Add("收货款");
            tb.Columns.Add("付货款");
            tb.Columns.Add("费用开支");
            tb.Columns.Add("逾期应付");
            tb.Columns.Add("逾期应收");
            tb.Columns.Add("信誉超限客户");
            tb.Columns.Add("低库存品种");

            DataRow row = tb.NewRow();
            string date_sql = $@" BETWEEN '{start_time.Toyyyy_MM_ddStart()}' AND '{end_time.Toyyyy_MM_ddEnd()}' ";
            string sql = $@" SELECT CONVERT(VARCHAR(20),COUNT( DISTINCT m.sheet_no))+'单 '+Convert(VARCHAR(20),CONVERT(DECIMAL(18,2),ISNULL(SUM(d.sale_qnty),0)))
FROM dbo.sm_t_salesheet m
LEFT JOIN dbo.sm_t_salesheet_detail d ON d.sheet_no = m.sheet_no
WHERE m.approve_flag='1' and  m.oper_date  " + date_sql;
            row["客户销售"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT CONVERT(VARCHAR(20),COUNT( DISTINCT m.sheet_no))+'单 '+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),ISNULL(SUM(d.in_qty),0))) 
FROM dbo.ic_t_inout_store_master m
LEFT JOIN dbo.ic_t_inout_store_detail d ON d.sheet_no = m.sheet_no
WHERE m.approve_flag='1' and  m.trans_no='D' AND m.oper_date " + date_sql;
            row["客户退货"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT COUNT(DISTINCT d.item_no) 品种
FROM dbo.sm_t_salesheet m
LEFT JOIN dbo.sm_t_salesheet_detail d ON d.sheet_no = m.sheet_no
WHERE m.approve_flag='1' and m.oper_date " + date_sql;
            row["销售品种"] = db.ExecuteScalar(sql, null);

            sql = $@" SELECT COUNT(DISTINCT i.item_no) 未销品种
FROM dbo.bi_t_item_info i
LEFT JOIN (
SELECT DISTINCT d.item_no 
FROM dbo.sm_t_salesheet_detail d
LEFT JOIN dbo.sm_t_salesheet m ON d.sheet_no = m.sheet_no
WHERE m.approve_flag='1' and m.oper_date  {date_sql} ) t ON t.item_no = i.item_no
WHERE t.item_no IS NULL ";
            row["未销品种"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT CONVERT(VARCHAR(20),COUNT(DISTINCT m.sheet_no))+'单 '+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),ISNULL(SUM(d.in_qty),0)))
FROM dbo.ic_t_inout_store_master m
LEFT JOIN dbo.ic_t_inout_store_detail d ON d.sheet_no = m.sheet_no
WHERE m.approve_flag='1' and   m.trans_no='A' AND m.oper_date " + date_sql;
            row["采购进货"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT CONVERT(VARCHAR(20),COUNT(DISTINCT m.sheet_no))+'单 '+CONVERT(VARCHAR(20),CONVERT(DECIMAL(18,2),ISNULL(SUM(d.in_qty),0)))
FROM dbo.ic_t_inout_store_master m
LEFT JOIN dbo.ic_t_inout_store_detail d ON d.sheet_no = m.sheet_no
WHERE m.approve_flag='1' and m.trans_no='F' AND m.oper_date " + date_sql;
            row["采购退货"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT COUNT(DISTINCT d.item_no) 品种
FROM dbo.ic_t_inout_store_master m
LEFT JOIN dbo.ic_t_inout_store_detail d ON d.sheet_no = m.sheet_no
WHERE m.trans_no='A' AND m.oper_date " + date_sql;
            row["采购品种"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT CONVERT(DECIMAL(18,2), ISNULL(SUM(d.pay_amount),0)) 收货款
FROM dbo.rp_t_recpay_record_info m
LEFT JOIN dbo.rp_t_recpay_record_detail d ON d.sheet_no = m.sheet_no
WHERE d.voucher_no LIKE 'SO%' AND m.supcust_flag='C' 
AND m.approve_flag='1' AND m.oper_date " + date_sql;
            row["收货款"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT CONVERT(DECIMAL(18,2), ISNULL(SUM(d.pay_amount),0)) 付货款
FROM dbo.rp_t_recpay_record_info m
LEFT JOIN dbo.rp_t_recpay_record_detail d ON d.sheet_no = m.sheet_no
WHERE d.voucher_no LIKE 'PI%' AND m.supcust_flag='S'
AND m.approve_flag='1' AND m.oper_date " + date_sql;
            row["付货款"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT CONVERT(DECIMAL(18,2), ISNULL(SUM(d.bill_cash),0)) 费用开支 
FROM dbo.bank_t_cash_master m
LEFT JOIN dbo.bank_t_cash_detail d ON d.sheet_no = m.sheet_no
WHERE m.sheet_no LIKE 'SR%' AND m.oper_date " + date_sql;
            row["费用开支"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT CONVERT(DECIMAL(18,2), ISNULL(SUM(sheet_amount-paid_amount-free_money),0)) 应付
FROM dbo.rp_t_accout_payrec_flow
WHERE trans_no IN ('A','F') AND oper_date " + date_sql;
            row["逾期应付"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT CONVERT(DECIMAL(18,2), ISNULL(SUM(sheet_amount-paid_amount-free_money),0)) 应收
FROM dbo.rp_t_accout_payrec_flow
WHERE trans_no IN ('I','D') AND oper_date " + date_sql;
            row["逾期应收"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT COUNT(1) 客户数
FROM dbo.bi_t_supcust_info
WHERE ISNULL(credit_amt, 0) = 0 ";
            row["信誉超限客户"] = db.ExecuteScalar(sql, null);

            sql = $@"SELECT COUNT(DISTINCT item_no) 低库存品种 
FROM dbo.ic_t_branch_stock
WHERE stock_qty<0 ";
            row["低库存品种"] = db.ExecuteScalar(sql, null);

            tb.Rows.Add(row);
            total_count = 0;
            return tb;
        }
        #endregion

        #region 加工
        public DataTable GetProcessDetail(DateTime start_time, DateTime end_time, string ph_sheet_no, string oper_type, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT p.flow_id,
       p.ph_sheet_no 批次号,
       i.process_type 加工类型,
       p.bis_type 单据类型,
       p.item_no 商品编号,
       i.item_subno 货号,
       i.item_name 品名,
       p.unit_no 单位,
       p.need_qty 需求数量,
       p.qty 实际数量,
       p.weight 称重重量,
       p.oper_date 操作日期,
       p.jh 机号,
       p.is_approve 审核状态
FROM dbo.ot_processing p
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = p.item_no
WHERE 1 = 1
      AND p.oper_date BETWEEN '{start_time.Toyyyy_MM_ddStart()}' AND '{end_time.Toyyyy_MM_ddEnd()}' ";

            if (!string.IsNullOrWhiteSpace(ph_sheet_no))
            {
                sql += $@" AND p.ph_sheet_no = '{ph_sheet_no}' ";
            }

            if (!string.IsNullOrWhiteSpace(oper_type))
            {
                sql += $@" AND p.bis_type = '{oper_type}' ";
            }

            DataTable tb = db.ExecuteToTable(sql, null);
            total_count = 0;
            return tb;
        }

        public DataTable GetProcessLoss(DateTime start_time, DateTime end_time, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
//            string sql = $@"SELECT i.item_no 商品编号,
//       i.item_subno 货号,
//       i.item_name 商品名称,
//	   i.unit_no 单位,
//       t.in_qty 领料数量,
//       t.rt_qty 退料数量,
//       t.out_qty 出成数量,
//       t.in_qty - t.out_qty - t.rt_qty 损耗数量,
//       (t.in_qty - t.out_qty - t.rt_qty) / ISNULL(   CASE
//                                                         WHEN t.in_qty = 0 THEN
//                                                             1
//                                                         ELSE
//                                                             t.in_qty
//                                                     END,
//                                                     1
//                                                 ) 损耗率
//FROM
//(
//    SELECT d.item_no,
//           SUM(d.in_qty) in_qty,
//           '0' rt_qty,
//           '0' out_qty
//    FROM dbo.ic_t_inout_store_master m
//        LEFT JOIN dbo.ic_t_inout_store_detail d
//            ON d.sheet_no = m.sheet_no
//    WHERE m.trans_no = 'OM'
//          AND m.oper_date
//          BETWEEN '{start_time.Toyyyy_MM_ddStart()}' AND '{end_time.Toyyyy_MM_ddEnd()}'
//    GROUP BY d.item_no
//    UNION ALL
//    SELECT d.item_no,
//           0,
//           SUM(d.in_qty),
//           0
//    FROM dbo.ic_t_inout_store_master m
//        LEFT JOIN dbo.ic_t_inout_store_detail d
//            ON d.sheet_no = m.sheet_no
//    WHERE m.trans_no = 'RM'
//          AND m.oper_date
//          BETWEEN '{start_time.Toyyyy_MM_ddStart()}' AND '{end_time.Toyyyy_MM_ddEnd()}'
//    GROUP BY d.item_no
//    UNION ALL
//    SELECT d.item_no,
//           0,
//           0,
//           SUM(d.in_qty)
//    FROM dbo.ic_t_inout_store_master m
//        LEFT JOIN dbo.ic_t_inout_store_detail d
//            ON d.sheet_no = m.sheet_no
//    WHERE m.trans_no = 'PE'
//          AND m.oper_date
//          BETWEEN '{start_time.Toyyyy_MM_ddStart()}' AND '{end_time.Toyyyy_MM_ddEnd()}'
//    GROUP BY d.item_no
//) t
//    LEFT JOIN dbo.bi_t_item_info i
//        ON i.item_no = t.item_no";
            string ass = "oper_date between '"+start_time.Toyyyy_MM_ddStart()+"' and '"+ end_time.Toyyyy_MM_ddEnd() + "'";
            //            string sql = @"select   a.bom_no,b.item_no 商品编号,b.item_subno 货号,b.item_name 商品名称,b.unit_no 单位
            //,isnull(t1.in_qty,0.00) 退料入库,isnull(t2.in_qty,0.00) 领料出库
            //,isnull(t3.in_qty,0.00) ,isnull(t4.in_qty,0.00) 出成入库 
            //,(isnull(t3.in_qty,0.00)-isnull(t2.in_qty,0.00)+isnull(t1.in_qty,0.00))/isnull(t2.in_qty,1) 损耗率
            //from [dbo].[bi_t_bom_detail] a 
            //left join  [dbo].[bi_t_item_info] b on a.item_no=b.item_no
            //left join (select item_no,sum(in_qty) in_qty from ic_t_inout_store_detail c 
            //left join  ic_t_inout_store_master d 
            //on c.sheet_no=d.sheet_no where trans_no='RM' and " + ass + "";
            //            sql += @"  group by item_no) t1 on t1.item_no=a.item_no
            //left join (select item_no,sum(in_qty) in_qty from ic_t_inout_store_detail c 
            //left join  ic_t_inout_store_master d 
            //on c.sheet_no=d.sheet_no where trans_no='OM' and " + ass + "";

            //            sql +=
            //                @"  group by item_no) t2 on t2.item_no = a.item_no left join (select item_no,sum(in_qty) in_qty from ic_t_inout_store_detail c 
            //            left join  ic_t_inout_store_master d 
            //            on c.sheet_no=d.sheet_no where trans_no='PE' and " + ass + "";

            //            sql += @"group by item_no) t3 on t3.item_no=a.item_no

            //            left join (select item_no,sum(in_qty) in_qty from ic_t_inout_store_detail c 
            //            left join  ic_t_inout_store_master d 
            //            on c.sheet_no=d.sheet_no where trans_no='OM' and " + ass + " group by item_no) t4 on t4.item_no=a.bom_no";


            //            string sql1 =
            //                @"select a.item_no 商品编号,d.item_name 商品名称,d.item_subno 货号,d.unit_no 单位 from bi_t_bom_detail a 
            //left join ic_t_inout_store_detail b on a.item_no=b.item_no
            //left join  ic_t_inout_store_master c on b.sheet_no=c.sheet_no
            //left join bi_t_item_info d on d.item_no=a.item_no
            //left join 
            //(select item_no,sum(in_qty) qty from  ic_t_inout_store_detail b1 
            //left join  ic_t_inout_store_master c1 on b1.sheet_no=c1.sheet_no
            //where trans_no='RM' and " + ass + " ";
            //            sql1 += @"group by item_no)t1 on t1.item_no=a.item_no
            //where trans_no='OM' and " + ass + " group by a.item_no,d.item_name,d.item_subno,d.unit_no,t1.qty";

            //            DataTable dt = db.ExecuteToTable(sql1, null);
            //            dt.Columns.Add("领料数量", typeof(decimal));
            //            dt.Columns.Add("退料数量", typeof(decimal));
            //            dt.Columns.Add("出成数量", typeof(decimal));
            //            dt.Columns.Add("损耗数量", typeof(decimal));
            //            dt.Columns.Add("损耗率", typeof(decimal));
            //            DataTable dt1 = new DataTable();
            //            DataTable dt2 = new DataTable();
            //            foreach (DataRow row in dt.Rows)
            //            {
            //                dt1 = new DataTable();
            //                dt2 = new DataTable();
            //                sql1 = @"select isnull(sum(in_qty),0.00) in_qty from  ic_t_inout_store_detail b1 
            //               left join  ic_t_inout_store_master c1 on b1.sheet_no = c1.sheet_no
            //               where trans_no = 'OM' and item_no='" + row["商品编号"].ToString() + "' and " + ass + " ";
            //                decimal s = 0;
            //                foreach (DataRow item in db.ExecuteToTable(sql1, null).Rows)
            //                {
            //                    s += item["in_qty"].ToDecimal();
            //                }

            //                row["领料数量"] = s;
            //                sql1 = @"select isnull(sum(in_qty),0.00) in_qty from  ic_t_inout_store_detail b1 
            //               left join  ic_t_inout_store_master c1 on b1.sheet_no = c1.sheet_no
            //               where trans_no = 'RM' and item_no='" + row["商品编号"].ToString() + "' and " + ass + " ";
            //                decimal s1 = 0;
            //                foreach (DataRow item in db.ExecuteToTable(sql1, null).Rows)
            //                {
            //                    s1 += item["in_qty"].ToDecimal();
            //                }

            //                row["退料数量"] = s1;
            //                sql1 = "select bom_no from [dbo].[bi_t_bom_detail] where item_no='" + row["商品编号"].ToString() + "'";
            //                dt1 = db.ExecuteToTable(sql1, null);
            //                string bom_nos = "";
            //                foreach (DataRow dataRow in dt1.Rows)
            //                {
            //                    bom_nos += "'" + dataRow["bom_no"].ToString() + "',";
            //                }
            //                sql1 = @"select isnull(sum(in_qty),0.00) in_qty from  ic_t_inout_store_detail b1 
            //                left join  ic_t_inout_store_master c1 on b1.sheet_no = c1.sheet_no
            //                where trans_no = 'PE' and item_no in (" + bom_nos.Substring(0, bom_nos.Length - 1) + ") and " + ass + " ";
            //                dt2 = db.ExecuteToTable(sql1, null);
            //                decimal sum = 0;
            //                foreach (DataRow dataRow in dt2.Rows)
            //                {
            //                    sum += dataRow["in_qty"].ToDecimal();
            //                }

            //                row["出成数量"] = sum;
            //                decimal sunhao = (row["退料数量"].ToDecimal() + sum);
            //                row["损耗数量"] = sunhao;
            //                decimal lingliao = 1;
            //                if (row["领料数量"] != null && row["领料数量"].ToDecimal() != 0)
            //                {
            //                    lingliao = row["领料数量"].ToDecimal();
            //                }
            //                //System.Math.Abs()绝对值
            //                row["损耗率"] = (sum - lingliao + s1) / lingliao;
            //            }


            string sql = @"select b.item_name,a.*,b.unit_no from (
select ph_sheet_no,pro_code,bis_type,max(need_qty) need_qty,sum(qty) qty 
from ot_processing where ph_sheet_no like '%%' and " + ass + " group by ph_sheet_no,pro_code,bis_type)a left join bi_t_item_info b on a.pro_code=b.item_no order by pro_code";
            DataTable dt = db.ExecuteToTable(sql, null);

            DataTable d = new DataTable();
            d.Columns.Add("商品编号");
            d.Columns.Add("商品名称");
            d.Columns.Add("单位");
            d.Columns.Add("出成数量", typeof(decimal));
            d.Columns.Add("领料数量", typeof(decimal));
            d.Columns.Add("退料数量", typeof(decimal));
            d.Columns.Add("损耗率", typeof(decimal));
            ArrayList al = new ArrayList();
            DataRow[] dr = dt.Select("bis_type='PE'");
            foreach (DataRow dataRow in dr)
            {
                if (!al.Contains(dataRow["pro_code"]))
                {
                    DataRow dr1 = d.NewRow();
                    dr1["商品编号"] = dataRow["pro_code"];
                    dr1["商品名称"] = dataRow["item_name"];
                    dr1["单位"] = dataRow["unit_no"];
                    DataRow[] dr2 = dt.Select("bis_type='PE' and pro_code='" + dataRow["pro_code"] + "'");
                    decimal pe_qty = 0;
                    foreach (var row in dr2)
                    {
                        pe_qty += row["qty"].ToDecimal();
                    }

                    dr1["出成数量"] = pe_qty;
                    DataRow[] dr3 = dt.Select("bis_type='RM' and pro_code='" + dataRow["pro_code"] + "'");
                    decimal rm_qty = 0;
                    foreach (var row in dr3)
                    {
                        rm_qty += row["qty"].ToDecimal();
                    }
                    //if (dataRow["bis_type"].ToString().Trim() == "RM" &&
                    //    dataRow["pro_code"].ToString().Trim() == dataRow["pro_code"])
                    //{
                    //    rm_qty += dataRow["qty"].ToDecimal();
                    //}

                    dr1["退料数量"] = rm_qty;
                    DataRow[] dr4 = dt.Select("bis_type='OM' and pro_code='" + dataRow["pro_code"] + "'");
                    decimal om_qty = 0;
                    foreach (var row in dr4)
                    {
                        om_qty += row["qty"].ToDecimal();
                    }
                    //if (dataRow["bis_type"].ToString().Trim() == "OM" &&
                    //    dataRow["pro_code"].ToString().Trim() == dataRow["pro_code"])
                    //{
                    //    om_qty += dataRow["qty"].ToDecimal();
                    //}

                    dr1["领料数量"] = om_qty;
                    decimal a = om_qty - rm_qty;
                    if ((om_qty - rm_qty) == 0)
                    {
                        a = 1;
                    }

                    //损耗率 = 1 -（出成数量 /（领料数量 - 退料数量））
                    dr1["损耗率"] = 1 - (pe_qty / a);
                    d.Rows.Add(dr1);
                    al.Add(dataRow["pro_code"]);
                }
                
            }

            total_count = 9999999;
            return d;
        }

        #endregion

        //菜单
        public DataTable GetMenuTotalCost(DateTime start_time, DateTime end_time, string cust_no, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT m.cus_no 编号,
       s.sup_name 客户名称,
       d.ord_id 订单编号,
       CONVERT(VARCHAR(10), m.reach_time, 120) 配送日期,
       SUM(d.qty) * AVG(p.price) 金额,
       AVG(ISNULL(ri.in_amount, 0)) 预估收入,
       SUM(d.qty) * AVG(p.price) / AVG(ISNULL(ri.in_amount, 1)) 成本占比
FROM dbo.tr_order m
    INNER JOIN dbo.tr_order_item d
        ON d.ord_id = m.ord_id
    LEFT JOIN dbo.bi_t_supcust_info s
        ON m.cus_no = s.supcust_no
           AND s.supcust_flag = 'C'
    LEFT JOIN
    (
        SELECT i.item_no,
               CASE
                   WHEN s.last_price IS NULL THEN
                       i.price
                   ELSE
                       s.last_price
               END price
        FROM dbo.bi_t_item_info i
            LEFT JOIN dbo.bi_t_sup_item s
                ON i.item_no = s.item_no
    ) p
        ON p.item_no = d.goods_id
    LEFT JOIN dbo.rp_t_supcust_income_revenue ri
        ON ri.supcust_no = s.supcust_no
           AND CONVERT(VARCHAR(10), ri.in_date, 120) = CONVERT(VARCHAR(10), m.reach_time, 120)
WHERE m.reach_time
      BETWEEN '{start_time.Toyyyy_MM_ddStart()}' AND '{end_time.Toyyyy_MM_ddEnd()}'
{cust_no.ToWhereSql("  AND s.supcust_no = '?'")}
GROUP BY CONVERT(VARCHAR(10), m.reach_time, 120),
         m.cus_no,
         s.sup_name,
         d.ord_id
ORDER BY m.cus_no,
         s.sup_name;";
            DataTable tb = db.ExecuteToTable(sql, null);
            tb.Columns.Add("总成本占比", typeof(decimal));

            var groupBy = tb.AsEnumerable().GroupBy(d => d["客户名称"]);
            foreach (var g in groupBy)
            {
                var amount = g.Sum(d => d["金额"].ToDecimal());
                var incomeAmount = g.Sum(d => d["预估收入"].ToDecimal());
                incomeAmount = incomeAmount == 0 ? 1 : incomeAmount;
                foreach (var row in g)
                {
                    row["总成本占比"] = amount / incomeAmount;
                }
            }

            total_count = 0;
            return tb;
        }

    }
}
