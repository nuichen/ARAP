using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    public class Conv
    {

        public static decimal ToDecimal(object obj)
        {
            if (obj == null || obj.ToString()=="")
            {
                return 0;
            }
            decimal value = 0;
            decimal.TryParse(obj.ToString(), out value);
            return value;
        }

        public static int ToInt(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return 0;
            }
            int value = 0;
            int.TryParse(obj.ToString(), out value);
            return value;
        }

        public static DateTime ToDateTime(object obj)
        {
            if (obj == null || obj.ToString() == "")
            {
                return DateTime.Parse("1990-01-01 00:00:00");
            }
            DateTime dt;
            DateTime.TryParse(obj.ToString(), out dt);
            return dt;
        }
    }
}