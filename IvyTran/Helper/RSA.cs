using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace IvyTran.Helper
{
    public class RSA
    {

        public static string Sign(string strPrivateKey, string strContent)
        {
            byte[] btContent = Encoding.UTF8.GetBytes(strContent);
          
            RSACryptoServiceProvider rsp = new RSACryptoServiceProvider();
           
            SHA1CryptoServiceProvider r = new SHA1CryptoServiceProvider();

            byte[] signature = rsp.SignData(btContent, r);
            
            return Convert.ToBase64String(signature);
        }  

        

        
             
    }
}