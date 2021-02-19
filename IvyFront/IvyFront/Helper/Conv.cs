using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyFront
{
    class Conv
    {
        public static decimal ToDecimal(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return 0;
            }
            decimal value = 0;
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

        public static double ToDouble(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return 0;
            }
            double value = 0;
            try
            {
                double.TryParse(obj.ToString(), out value);
            }
            catch (Exception)
            { }
            return value;
        }

        public static DateTime ToDateTime(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return DateTime.Parse("1900-01-01 00:00:00");
            }
            DateTime dt;
            DateTime.TryParse(obj.ToString(), out dt);
            return dt;
        }

        public static DateTime ToDateTime2(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return DateTime.Now;
            }
            DateTime dt;
            DateTime.TryParse(obj.ToString(), out dt);
            return dt;
        }

        //两位小数点
        public static string ToFixed2(object obj)
        {
            try
            {
                if (obj == null || obj.ToString() == "")
                {
                    return "0.00";
                }
                else
                {
                    return Convert.ToDouble(obj.ToString()).ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                return obj.ToString();
            }
        }

        public static long get_timespan()
        {
            long timespan = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return timespan;
        }

        public static decimal RemoveZero(object obj)
        {
            decimal value = ToDecimal(obj);
            try
            {
                if (Program.mo_ling == "1")
                {
                    value = Math.Floor(value);
                }
                else if (Program.mo_ling == "2")
                {
                    var str = value.ToString();
                    if (str.IndexOf(".") > 0)
                    {
                        value = ToDecimal(str.Substring(0, str.IndexOf(".") + 2));
                    }
                }
                else if (Program.mo_ling == "3")
                {
                    var int1 = Math.Floor(value);
                    var dec = value - int1;
                    if (dec > 0.5M)
                    {
                        value = int1 + 0.5M;
                    }
                    else
                    {
                        value = int1;
                    }
                }
            }
            catch (Exception)
            {

            }
            return value;
        }
    }
}
