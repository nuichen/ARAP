﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.body
{
    public class alimicropay_par
    {
        public string app_id { get; set; }
        public string method { get; set; }
        public string charset { get; set; }
        public string sign_type { get; set; }
        public string timestamp { get; set; }
        public string version { get; set; }
        public string out_trade_no { get; set; }
        public string scene { get; set; }
        public string auth_code { get; set; }
        public string subject { get; set; }
        public string total_amount { get; set; }
        public string store_id { get; set; }
        public string alipay_store_id { get; set; }
        public string biz_content()
        {

            string str = "";
            str += "{";
            str += "\"out_trade_no\":\"" + out_trade_no + "\"";
            str += ",";
            str += "\"scene\":\"" + scene + "\"";
            str += ",";
            str += "\"auth_code\":\"" + auth_code + "\"";
            str += ",";
            str += "\"subject\":\"" + subject + "\"";
            str += ",";
            str += "\"total_amount\":\"" + total_amount + "\"";
            str += ",";
            str += "\"store_id\":\"" + store_id + "\"";
            str += ",";
            str += "\"alipay_store_id\":\"" + alipay_store_id + "\"";
            str += "}";

            return str;
        }

        private string getSign(string key)
        {
            string sign = "";
            System.Collections.Generic.SortedDictionary<string, string> dic =
            new SortedDictionary<string, string>();
            dic.Add("app_id", app_id);
            dic.Add("method", method);
            dic.Add("charset", charset);
            dic.Add("sign_type", sign_type);
            dic.Add("timestamp", timestamp);
            dic.Add("version", version);

            dic.Add("biz_content", biz_content());
            foreach (KeyValuePair<string, string> item in dic)
            {
                if (sign == "")
                {
                    sign = sign + item.Key + "=" + item.Value;
                }
                else
                {
                    sign = sign + "&" + item.Key + "=" + item.Value;
                }
            }


            sign = RSAFromPkcs8.sign(sign, key, "utf-8");
            return sign;
        }

        public string ToString(string key)
        {

            string str = "";
            str += "app_id=" + app_id;
            str += "&method=" + method;
            str += "&charset=" + charset;
            str += "&sign_type=" + sign_type;
            str += "&timestamp=" + timestamp;
            str += "&version=" + version;
            str += "&biz_content=" + biz_content();
            str += "&sign=" + getSign(key);

            return str;
        }
    }
}