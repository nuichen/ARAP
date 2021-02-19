using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace Model
{
    class Conv
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

        public static DateTime ToDateTime(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return DateTime.MinValue;
            }
            DateTime dt;
            DateTime.TryParse(obj.ToString(), out dt);
            return dt;
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

        public static decimal ToDecimal(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                decimal val;
                decimal.TryParse(obj.ToString(), out val);
                return val;
            }
        }

        public static Int16 ToInt16(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                Int16 val;
                Int16.TryParse(obj.ToString(), out val);
                return val;
            }
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

        public static int ToInt(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                if (obj == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    int res = 0;
                    int.TryParse(obj.ToString(), out res);

                    return res;
                }
            }
        }

        public static char ToChar(object obj)
        {
            if (obj == null)
            {
                return ' ';
            }
            else
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    return ' ';
                }
                else
                {
                    return Convert.ToChar(obj);
                }
            }
        }

        public static DataTable Assign<T>()
        {
            DataTable tb = new DataTable();
            var t = typeof(T);
            var pars = t.GetProperties();

            foreach (var item in pars)
            {
                tb.Columns.Add(item.Name, item.PropertyType);
            }
            return tb;
        }

    }
}
