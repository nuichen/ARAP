using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace b2bclient
{
   public  class MD5
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
    }
}
