using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.body
{
    public class micropay_par
    {
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string device_info { get; set; }
        public string nonce_str { get; set; }
        public string body { get; set; }
        public string out_trade_no { get; set; }
        public int total_fee { get; set; }
        public string spbill_create_ip { get; set; }
        public string auth_code { get; set; }

        public string getSign(string key)
        {
            string sign = "";
            SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
            dic.Add("appid", appid);
            dic.Add("mch_id", mch_id);
            dic.Add("device_info", device_info);
            dic.Add("nonce_str", nonce_str);
            dic.Add("body", body);
            dic.Add("out_trade_no", out_trade_no);
            dic.Add("total_fee", total_fee.ToString());
            dic.Add("spbill_create_ip", spbill_create_ip);
            dic.Add("auth_code", auth_code);
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
            if (1 == 1)
            {
                sign += "&key=" + key;
            }
            sign = MD5Helper.ToMD5(sign).ToUpper();
            return sign;
        }

        public string ToString(string key)
        {
            StringBuilderForXML sb = new StringBuilderForXML();
            sb.Append("<xml>");
            sb.Append("appid", appid);
            sb.Append("mch_id", mch_id);
            sb.Append("device_info", device_info);
            sb.Append("nonce_str", nonce_str);
            sb.Append("body", body);
            sb.Append("out_trade_no", out_trade_no);
            sb.Append("total_fee", total_fee.ToString());
            sb.Append("spbill_create_ip", spbill_create_ip);
            sb.Append("auth_code", auth_code);
            sb.Append("sign", getSign(key));
            sb.Append("</xml>");
            return sb.ToString();
        }

    }
}