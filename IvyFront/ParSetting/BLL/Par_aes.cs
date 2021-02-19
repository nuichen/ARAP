using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParSetting.BLL
{
    class Par_aes:IBLL.IPar 
    {
        string IBLL.IPar.Read(string class_name, string par_name)
        {
            IBLL.IPar par = new BLL.Par();
           var val= par.Read(class_name, par_name);
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

        void IBLL.IPar.Write(string class_name, string par_name, string par_value)
        {
            IBLL.IPar par = new BLL.Par();
            par_value = Helper.aes.AESEncrypt(par_value);
            par.Write(class_name, par_name, par_value);
        }

    }
}
