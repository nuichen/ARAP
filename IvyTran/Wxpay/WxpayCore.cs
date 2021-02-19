using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Xml;

namespace IvyTran.Wxpay
{
    public class WxpayCore
    {
        internal static bool Post(string url, string content, out string result, out string errMsg)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(content);
                var req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "POST";

                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;

                var stream = req.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                if (rep.StatusCode != HttpStatusCode.OK)
                {
                    result = "";
                    errMsg = "微信支付请求失败 (code=" + rep.StatusCode + ")";
                    return false;
                }

                //以数据流的形式获取response响应的数据
                using (StreamReader myreader = new StreamReader(rep.GetResponseStream(), Encoding.UTF8))
                {
                    result = myreader.ReadToEnd();
                }

                errMsg = "";
                return true;
            }
            catch (Exception e)
            {
                result = "";
                errMsg = "微信支付请求失败 (error=" + e.Message + ")";
                return false;
            }
        }

        internal static bool Get(string url, int timeout, out string result, out string errMsg)
        {
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(url);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                result = strBuilder.ToString();
                errMsg = "";
                return true;
            }
            catch (Exception e)
            {
                result = "";
                errMsg = "微信支付请求失败 (error=" + e.Message + ")";
                return false;
            }
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(SortedDictionary<string, object> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, object> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value.ToString() + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen-1,1);

            return prestr.ToString();
        }

        public static string CreateLinkString(Dictionary<string, object> dicArray)
        {
            var p1 = new SortedDictionary<string, object>();
            foreach (KeyValuePair<string, object> temp in dicArray)
            {
                p1.Add(temp.Key, temp.Value);
            }

            return CreateLinkString(p1);
        }

        public static string CreateLinkString(SortedDictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value.ToString() + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            var p1 = new SortedDictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                p1.Add(temp.Key, temp.Value);
            }

            return CreateLinkString(p1);
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkStringUrlencode(SortedDictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + UrlEncode(temp.Value, Encoding.UTF8) + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        public static string ToXml(SortedDictionary<string, object> param)
        {
            var str = new StringBuilder("<xml>");
            foreach (KeyValuePair<string, object> pair in param)
            {
                if (pair.Value.GetType() == typeof(int))
                {
                    str.Append("<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">");
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    str.Append("<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">");
                }
            }
            str.Append("</xml>");
            return str.ToString();
        }

        public static string UrlEncode(string temp, Encoding encoding)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < temp.Length; i++)
            {
                string t = temp[i].ToString();
                if (t == " ")
                    sb.Append("%20"); //将空格转化为%20而不是+
                else
                {
                    string k = HttpUtility.UrlEncode(t, encoding);
                    if (t == k)
                        sb.Append(t);
                    else
                        sb.Append(k.ToUpper()); //转为大小
                }
            }
            return sb.ToString();
        }

        public static SortedDictionary<string, string> FromXml(string xml)
        {
            var dic = new SortedDictionary<string, string>();
            if (string.IsNullOrEmpty(xml)) return dic;

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode n in nodes)
            {
                XmlElement xe = (XmlElement)n;
                dic[xe.Name] = xe.InnerText;
            }
            return dic;
        }

        public static bool CheckSign(SortedDictionary<string, string> param, string merchantKey)
        {
            string sign = null;
            var param1 = new SortedDictionary<string, string>();
            foreach (KeyValuePair<string, string> p in param)
            {
                if (p.Key != "sign")
                    param1.Add(p.Key, p.Value);
                else
                    sign = p.Value.ToString();
            }
            //string signString = WxpayCore.CreateLinkString(param1) + "&key=" + WxpayConfig.MerchantKey;
            string signString = WxpayCore.CreateLinkString(param1) + "&key=" + merchantKey;
            string mySign = WxpayCore.MD5(signString, Encoding.UTF8);
            return sign == mySign;
        }

        private static DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static string GetUnixDatetime()
        {
            var t = (long)((DateTime.UtcNow - Jan1st1970).TotalMilliseconds) / 1000;
            return t.ToString();
        }

        /// <summary>
        /// 通知结果返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public static string NotifyResponse(string code, string msg)
        {
            var tmp = "<xml><return_code><![CDATA[{0}]]></return_code><return_msg><![CDATA[{1}]]></return_msg></xml>";
            return string.Format(tmp, code, msg);
        }

        /// <summary>
        /// MD5生成签名字符串
        /// </summary>
        /// <param name="content">需要签名的内容</param>
        /// <param name="code">编码</param>
        public static string MD5(string content, Encoding code)
        {
            StringBuilder sb = new StringBuilder(32);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(code.GetBytes(content));
            for (int i = 0; i < t.Length; i++) sb.Append(t[i].ToString("x2"));

            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// SHA1签名
        /// </summary>
        /// <param name="content">需要签名的内容</param>
        /// <param name="code">编码</param>
        public static string SHA1(string content, Encoding code)
        {
            SHA1 sh = new SHA1CryptoServiceProvider();
            byte[] buf = sh.ComputeHash(code.GetBytes(content));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buf.Length; i++) sb.Append(buf[i].ToString("x2"));

            return sb.ToString();
        }
    }
}