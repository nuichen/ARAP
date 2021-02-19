using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParSetting
{
    class Conv
    {
        public static decimal ToDecimal(object obj)
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
                    decimal res = 0;
                    decimal.TryParse(obj.ToString(), out res);
                    return res;
                }
            }
        }

        public static string ToString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

    }
}
