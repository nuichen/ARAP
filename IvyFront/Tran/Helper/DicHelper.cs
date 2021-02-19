using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tran.Helper
{
   public  class DicHelper
    {
        public static  bool Exist(Dictionary<string, object> kv, string ks)
        {
            if (ks == null || ks == "")
            {
                return true;
            }
            if (ks.Contains(",") == false)
            {
                if (kv.ContainsKey(ks) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                int flag = 0;
                foreach (string k in ks.Split(','))
                {
                    if (kv.ContainsKey(k) == false)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
