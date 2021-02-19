using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront
{
    public class Par_aes
    {
        public static string Read(string class_name, string par_name)
        {
            Par par = new Par();
            var val = par.Read(class_name, par_name);
            val = val.Trim();
            if (val == "")
            {
                return "";
            }
            else
            {
                val = Helper.aes.AESDecrypt(val);
                return val;
            }
        }

        public static void Write(string class_name, string par_name, string par_value)
        {
            Par par = new Par();
            par_value = Helper.aes.AESEncrypt(par_value);
            par.Write(class_name, par_name, par_value);
        }

    }
}
