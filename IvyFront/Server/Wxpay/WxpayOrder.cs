using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;

namespace Server.Wxpay
{
    /// <summary>
    /// 微信订单
    /// </summary>
    public class WxpayOrder
    {
        private WxpayConfig config;

        public WxpayOrder(WxpayConfig config)
        {
            this.config = config;
        }

        /// <summary>
        /// 为公众号网页提供支付请求，并以键值对形式返回（方便用于转为json）
        /// </summary>
        /// <param name="notifyUrl">到账通知Url</param>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        /// <param name="desc">商品描述</param>
        /// <param name="userOpenId">用户openid</param>
        /// <param name="ip"></param>
        /// <param name="errMsg"></param>
        public Dictionary<string, string>Pay4JSApi(string notifyUrl, string orderId, decimal money, string desc, string ip, string userOpenId, out string errMsg)
        {
            if (notifyUrl.IndexOf("://127.0.0.1") != -1)
            {
                errMsg = "尚未配置微信支付结果通知域名";
                return null;
            }

            string postXml = genPrePay4Jsapi(orderId, money, desc, notifyUrl, DateTime.MinValue, userOpenId, ip);

            //下单
            var url = string.Format(WxpayConfig.genPrePay_Url, config.GetAccessToken());
            string result;
            if (!WxpayCore.Post(url, postXml, out result, out errMsg))
            {
                errMsg = "预支付请求失败";
                return null;
            }
            if (string.IsNullOrEmpty(result) || result.IndexOf("return_code") == -1)
            {
                errMsg = "预支付返回值无效";
                return null;
            }

            var rlsObj = WxpayCore.FromXml(result);
            if (rlsObj["return_code"] != "SUCCESS" || rlsObj["result_code"] != "SUCCESS")
            {
                if (rlsObj["return_code"] != "SUCCESS")
                    errMsg = "预支付失败(return_code=" + rlsObj["return_code"] + ")";
                else
                    errMsg = "预支付失败(result_code=" + rlsObj["result_code"] + ")";
                return null;
            }
            if (!WxpayCore.CheckSign(rlsObj, config.MerchantKey))
            {
                errMsg = "预支付返回值中签名无效";
                return null;
            }
            var preId = (string)rlsObj["prepay_id"];
            if (string.IsNullOrEmpty(preId))
            {
                errMsg = "预支付返回值中prepay_id无效";
                return null;
            }

            errMsg = "";
            string noncestr = Guid.NewGuid().ToString().Replace("-", ""); //32 位内的随机串，防重发
            string timeStamp = WxpayCore.GetUnixDatetime();
            return ToJsapi(orderId, noncestr, timeStamp, preId);
        }
        
        /// <summary>
        /// 生成预支付订单数据包（为微信端Jsapi模式）
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        /// <param name="body"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="expire"></param>
        /// <param name="userOpenId">用户openid</param>
        /// <param name="ip"></param>
        private string genPrePay4Jsapi(string orderId, decimal money, string body, string notifyUrl, DateTime expire, string userOpenId, string ip)
        {
            string noncestr = Guid.NewGuid().ToString().Replace("-", ""); //32 位内的随机串，防重发
            var param = new SortedDictionary<string, object>();
            param.Add("appid", config.AppId);
            param.Add("mch_id", config.MerchantId);
            param.Add("nonce_str", noncestr);
            param.Add("device_info", "WEB");
            param.Add("body", body);
            param.Add("out_trade_no", orderId);
            param.Add("total_fee", Convert.ToInt32(Math.Round(money * 100, 0))); //订单总金额，单位为分
            param.Add("spbill_create_ip", ip);
            param.Add("notify_url", notifyUrl);
            param.Add("openid", userOpenId);
            param.Add("trade_type", "JSAPI");
            if (expire != DateTime.MinValue)
                param.Add("time_expire", expire.ToString("yyyyMMddHHmmss"));

            //生成签名
            string signString = WxpayCore.CreateLinkString(param) + "&key=" + config.MerchantKey;
            string packSign = WxpayCore.MD5(signString, Encoding.UTF8);
            param.Add("sign", packSign);

            return WxpayCore.ToXml(param);
        }

        /// <summary>
        /// 为PC网页提供充值支付请求
        /// </summary>
        /// <param name="notifyUrl">到账通知Url</param>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        /// <param name="desc">商品描述</param>
        /// <param name="userOpenId">用户openid</param>
        /// <param name="ip"></param>
        /// <param name="errMsg"></param>
        /// <param name="prepay_id">预支付订单号</param>
        /// <param name="qrcode_url">二维码Url内容</param>
        public bool Pay4Qrcode(string notifyUrl, string orderId, decimal money, string desc, string ip, out string errMsg, out string prepay_id, out string qrcode_url)
        {
            prepay_id = null;
            qrcode_url = null;
            if (notifyUrl.IndexOf("://127.0.0.1") != -1)
            {
                errMsg = "尚未配置微信支付结果通知域名";
                return false;
            }

            string postXml = genPrePay4Qrcode(orderId, money, desc, notifyUrl, DateTime.MinValue, ip);

            //下单
            var url = string.Format(WxpayConfig.genPrePay_Url, config.GetAccessToken());
            string result;
            if (!WxpayCore.Post(url, postXml, out result, out errMsg))
            {
                errMsg = "预支付请求失败";
                return false;
            }
            if (string.IsNullOrEmpty(result) || result.IndexOf("return_code") == -1)
            {
                errMsg = "预支付返回值无效";
                return false;
            }

            var rlsObj = WxpayCore.FromXml(result);
            LogHelper.writeLog("测试2",result,null);
            if (rlsObj["return_code"] != "SUCCESS" || rlsObj["result_code"] != "SUCCESS")
            {
                if (rlsObj["return_code"] != "SUCCESS")
                    errMsg = "预支付失败(return_code=" + rlsObj["return_code"] + ")";
                else
                    errMsg = "预支付失败(result_code=" + rlsObj["result_code"] + ")";
                return false;
            }
            if (!WxpayCore.CheckSign(rlsObj, config.MerchantKey))
            {
                errMsg = "预支付返回值中签名无效";
                return false;
            }
            prepay_id = (string)rlsObj["prepay_id"];
            if (string.IsNullOrEmpty(prepay_id))
            {
                errMsg = "预支付返回值中prepay_id无效";
                return false;
            }
            qrcode_url = (string)rlsObj["code_url"];
            if (string.IsNullOrEmpty(qrcode_url))
            {
                errMsg = "预支付返回值中code_url无效";
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 生成统一下单交易数据包（为pc端网页扫码模式）
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="money"></param>
        /// <param name="body"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="expire"></param>
        /// <param name="userOpenId">用户openid</param>
        /// <param name="ip"></param>
        private string genPrePay4Qrcode(string orderId, decimal money, string body, string notifyUrl, DateTime expire, string ip)
        {
            string noncestr = Guid.NewGuid().ToString().Replace("-", ""); //32 位内的随机串，防重发
            var param = new SortedDictionary<string, object>();
            param.Add("appid", config.AppId);
            param.Add("mch_id", config.MerchantId);
            param.Add("nonce_str", noncestr);
            param.Add("device_info", "WEB");
            param.Add("body", body);
            param.Add("out_trade_no", orderId);
            param.Add("total_fee", Convert.ToInt32(Math.Round(money * 100, 0))); //订单总金额，单位为分
            param.Add("spbill_create_ip", ip);
            param.Add("notify_url", notifyUrl);
            param.Add("product_id", orderId);
            param.Add("trade_type", "NATIVE");
            if (expire != DateTime.MinValue)
                param.Add("time_expire", expire.ToString("yyyyMMddHHmmss"));

            //生成签名
            string signString = WxpayCore.CreateLinkString(param) + "&key=" + config.MerchantKey;
            string packSign = WxpayCore.MD5(signString, Encoding.UTF8);
            param.Add("sign", packSign);

            return WxpayCore.ToXml(param);
        }

        /// <summary>
        /// 生成订单查询XML
        /// </summary>
        /// <param name="transaction_id"></param>
        private string generateOrderQueryByTransactionId(string transaction_id)
        {
            string noncestr = Guid.NewGuid().ToString().Replace("-", ""); //32 位内的随机串，防重发
            var param = new SortedDictionary<string, object>();
            param.Add("appid", config.AppId);
            param.Add("mch_id", config.MerchantId);
            param.Add("nonce_str", noncestr);
            param.Add("transaction_id", transaction_id);

            //生成签名
            string signString = WxpayCore.CreateLinkString(param) + "&key=" + config.MerchantKey;
            string packSign = WxpayCore.MD5(signString, Encoding.UTF8);
            param.Add("sign", packSign);

            return WxpayCore.ToXml(param);
        }

        /// <summary>
        /// 生成订单查询XML
        /// </summary>
        /// <param name="orderId"></param>
        private string generateOrderQueryByOrderId(string orderId)
        {
            string noncestr = Guid.NewGuid().ToString().Replace("-", ""); //32 位内的随机串，防重发
            var param = new SortedDictionary<string, object>();
            param.Add("appid", config.AppId);
            param.Add("mch_id", config.MerchantId);
            param.Add("nonce_str", noncestr);
            param.Add("out_trade_no", orderId);

            //生成签名
            string signString = WxpayCore.CreateLinkString(param) + "&key=" + config.MerchantKey;
            string packSign = WxpayCore.MD5(signString, Encoding.UTF8);
            param.Add("sign", packSign);

            return WxpayCore.ToXml(param);
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="transaction_id"></param>
        /// <param name="errMsg"></param>
        public SortedDictionary<string, string> OrderQueryTransactionId(string transaction_id, out string errMsg)
        {
            string postXml = generateOrderQueryByTransactionId(transaction_id);

            //下单
            string result;
            if (!WxpayCore.Post(WxpayConfig.queryPay_Url, postXml, out result, out errMsg))
            {
                errMsg = "查询订单失败";
                return null;
            }
            if (string.IsNullOrEmpty(result) || result.IndexOf("return_code") == -1)
            {
                errMsg = "查询订单失败";
                return null;
            }

            var order = WxpayCore.FromXml(result);
            if (order["return_code"] != "SUCCESS")
            {
                errMsg = "查询订单失败(return_code=" + order["return_code"] + "；return_msg=" + order["return_msg"] + ")";
                return null;
            }
            errMsg = "";
            return order;
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="errMsg"></param>
        public SortedDictionary<string, string> OrderQueryOrderId(string orderId, out string errMsg)
        {
            string postXml = generateOrderQueryByOrderId(orderId);

            //下单
            string result;
            if (!WxpayCore.Post(WxpayConfig.queryPay_Url, postXml, out result, out errMsg))
            {
                errMsg = "查询订单失败";
                return null;
            }
            if (string.IsNullOrEmpty(result) || result.IndexOf("return_code") == -1)
            {
                errMsg = "查询订单失败";
                return null;
            }

            var order = WxpayCore.FromXml(result);
            if (order["return_code"] != "SUCCESS")
            {
                errMsg = "查询订单失败(return_code=" + order["return_code"] + "；return_msg=" + order["return_msg"] + ")";
                return null;
            }
            errMsg = "";
            return order;
        }

        /// <summary>
        /// 检测订单是否支付完成
        /// </summary>
        /// <param name="transaction_id"></param>
        /// <param name="errMsg">错误</param>
        public bool OrderIsPayByTransactionId(string transaction_id, out string errMsg)
        {
            var order = OrderQueryTransactionId(transaction_id, out errMsg);
            if (string.IsNullOrEmpty(errMsg))
            {
                return order["trade_state"] == "SUCCESS";
            }
            else
                return false;
        }

        /// <summary>
        /// 检测订单是否支付完成
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="errMsg">错误</param>
        public bool OrderIsPayByOrderId(string orderId, out string errMsg)
        {
            var order = OrderQueryOrderId(orderId, out errMsg);
            if (string.IsNullOrEmpty(errMsg))
            {
                return order["trade_state"] == "SUCCESS";
            }
            else
                return false;
        }

        /// <summary>
        /// 检测订单支付状态（SUCCESS:支付成功;REFUND:转入退款;NOTPAY:未支付;CLOSED:已关闭;REVOKED:已撤销（刷卡支付）;USERPAYING:用户支付中;PAYERROR:支付失败(其他原因，如银行返回失败)）
        /// </summary>
        /// <param name="transaction_id"></param>
        /// <param name="errMsg">错误</param>
        public string QueryOrderStatusByTransactionId(string transaction_id, out string errMsg)
        {
            var order = OrderQueryTransactionId(transaction_id, out errMsg);
            if (string.IsNullOrEmpty(errMsg))
            {
                return order["trade_state"];
            }
            else
                return "";
        }

        /// <summary>
        /// 检测订单支付状态（SUCCESS:支付成功;REFUND:转入退款;NOTPAY:未支付;CLOSED:已关闭;REVOKED:已撤销（刷卡支付）;USERPAYING:用户支付中;PAYERROR:支付失败(其他原因，如银行返回失败)）
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="errMsg">错误</param>
        public string QueryOrderStatusByOrderId(string orderId, out string errMsg)
        {
            var order = OrderQueryOrderId(orderId, out errMsg);
            if (string.IsNullOrEmpty(errMsg))
            {
                return order["trade_state"];
            }
            else
                return "";
        }

        private Dictionary<string, string> ToJsapi(string orderId, string noncestr, string timeStamp, string prepayId)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("appId", config.AppId);
            dic.Add("timeStamp", timeStamp);    
            dic.Add("nonceStr", noncestr);
            dic.Add("package", "prepay_id=" + prepayId);
            dic.Add("signType", "MD5");                     

            //生成签名
            string signString = WxpayCore.CreateLinkString(dic) + "&key=" + config.MerchantKey;
            string sign = WxpayCore.MD5(signString, Encoding.UTF8);

            dic.Add("paySign", sign);
            return dic;
        }

        /// <summary>
        /// 统一下单返回值
        /// </summary>
        private class unifiedOrderResult
        {
            public string return_code { get; set; }
            public string return_msg { get; set; }
            //以下字段在return_code为SUCCESS的时候有返回
            public string appid { get; set; }
            public string mch_id { get; set; }
            public string nonce_str { get; set; }
            public string sign { get; set; }
            public string result_code { get; set; }
            public string err_code { get; set; }
            public string err_code_des { get; set; }
            //以下字段在return_code 和result_code都为SUCCESS的时候有返回
            public string trade_type { get; set; }
            public string prepay_id { get; set; }
            public string code_url { get; set; }
        }

        public class ErrorResult
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
        }
    }
}

