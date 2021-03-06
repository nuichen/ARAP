﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;

namespace b2bclient
{
    public class Conv
    {
        public static string ToMD5(string value)
        {
            if (value == null || value == "")
            {
                return "";
            }
            byte[] data = System.Text.Encoding.Default.GetBytes(value);
            var md = new MD5CryptoServiceProvider();
            var data2 = md.ComputeHash(data);
            var v = BitConverter.ToString(data2);
            v = v.Replace("-", "");
            v = v.ToLower();
            return v;
        }

        public static string ToString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        public static string DaXie(string money)
        {


            //将小写金额转换成大写金额  
            money = ToDecimal(money).ToString("0.##");
            String[] MyScale = { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟", "兆", "拾", "佰", "仟" };
            String[] MyBase = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            String M = "";
            bool isPoint = false;
            if (money.IndexOf(".") != -1)
            {
                money = money.Remove(money.IndexOf("."), 1);
                isPoint = true;
            }
            for (int i = money.Length; i > 0; i--)
            {
                int MyData = Convert.ToInt16(money[money.Length - i].ToString());//?
                M += MyBase[MyData];//?
                if (isPoint == true)
                {
                    M += MyScale[i - 1];//?
                }
                else
                {
                    M += MyScale[i + 1];//?
                }
            }
            return M;
        }

        public static DataTable Group(DataTable tb, string group_fields, string sum_fields)
        {
            var dt = tb.Copy();
            tb = tb.Copy();
            string[] g_fields = group_fields.Split(',');
            string[] s_fields = sum_fields.Split(',');
            tb.DefaultView.Sort = group_fields;
            tb = tb.DefaultView.ToTable();
            tb.Columns.Add("rows", typeof(List<DataRow>));
            var tb2 = tb.Clone();
            DataRow dr = null;
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                DataRow row = tb2.NewRow();
                row.ItemArray = tb.Rows[i].ItemArray;
                int flag = 0;
                if (i == 0)
                {
                    flag = 1;
                }
                else
                {
                    int flag2 = 0;
                    foreach (string field in g_fields)
                    {
                        if (dr[field].ToString() != row[field].ToString())
                        {
                            flag2 = 1;
                            break;
                        }
                    }
                    if (flag2 == 0)
                    {
                        if (sum_fields != "")
                        {
                            foreach (string field in s_fields)
                            {
                                dr[field] = Conv.ToDecimal(dr[field]) + Conv.ToDecimal(row[field]);
                            }
                        }
                        var lst = (List<DataRow>)dr["rows"];
                        lst.Add(row);
                    }
                    else
                    {
                        flag = 1;
                    }
                }
                if (flag == 1)
                {
                    dr = tb2.NewRow();
                    var lst = new List<DataRow>();
                    lst.Add(row);
                    dr["rows"] = lst;
                    foreach (string field in g_fields)
                    {
                        dr[field] = row[field];
                    }
                    if (sum_fields != "")
                    {
                        foreach (string field in s_fields)
                        {
                            dr[field] = row[field];
                        }
                    }

                    tb2.Rows.Add(dr);
                }

            }
            return tb2;
        }


        public static decimal ToDecimal(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return 0;
            }
            decimal value=0;
            try
            {
                decimal.TryParse(obj.ToString(), out value);
            }
            catch (Exception)
            { 
            
            }
            return value;
        }

        public static int ToInt(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return 0;
            }
            int value = 0;
            try
            {
                int.TryParse(obj.ToString(), out value);
            }
            catch (Exception)
            { }
            return value;
        }

        public static float ToFloat(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return 0;
            }
            float value = 0;
            try
            {
                float.TryParse(obj.ToString(), out value);
            }
            catch (Exception)
            { }
            return value;
        }

        public static DateTime ToDateTime(object obj)
        {
            if (obj == null)
            {
                return System.DateTime.MinValue;
            }
            DateTime dt;
            DateTime.TryParse(obj.ToString(), out dt);
            return dt;
        }


        public static DataTable GetDataTable(List<ReadWriteContext.IReadContext> lst)
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

    }
}