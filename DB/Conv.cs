using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB
{
    class Conv
    {
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

        public static float ToFloat(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                float val;
                float.TryParse(obj.ToString(), out val);
                return val;
            }
        }

        public static double ToDouble(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                double val;
                double.TryParse(obj.ToString(), out val);
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

        public static Int64 ToInt64(object obj)
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
                    Int64 res = 0;
                    Int64.TryParse(obj.ToString(), out res);

                    return res;
                }
            }
        }

        public static long ToLong(object obj)
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
                    long res = 0;
                    long.TryParse(obj.ToString(), out res);

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

    }
}
