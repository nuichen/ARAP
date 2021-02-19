using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace b2bclient.Helper
{
    public class aes
    {
        private static string AESKey = "1111111122222222";
        private static string DESKey = "88888888";

        /// <summary> 
        /// AES加密 
        /// </summary>
        public static string AESEncrypt(string value, string _aeskey = null)
        {
            if (string.IsNullOrEmpty(_aeskey))
            {
                _aeskey = AESKey;
            }

            byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(value);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary> 
        /// AES解密 
        /// </summary>
        public static string AESDecrypt(string value, string _aeskey = null)
        {
            try
            {
                if (string.IsNullOrEmpty(_aeskey))
                {
                    _aeskey = AESKey;
                }
                byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
                byte[] toEncryptArray = Convert.FromBase64String(value);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

