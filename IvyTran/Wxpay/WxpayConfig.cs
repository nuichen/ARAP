using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace IvyTran.Wxpay
{
    public delegate void GetAccessTokenFromHandler(out string access_token, out DateTime expires);

    /// <summary>
    /// 微信支付配置类
    /// </summary>
    public class WxpayConfig
    {
        internal const string getAccessToken_Url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 统一下单接口Url
        /// </summary>
        internal const string genPrePay_Url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        /// <summary>
        /// 查询交易结果Url
        /// </summary>
        internal const string queryPay_Url = "https://api.mch.weixin.qq.com/pay/orderquery";

        /// <summary>
        /// 微信公众号AppID
        /// </summary>
        private string appId = "";

        /// <summary>
        /// 微信公众号AppSecret
        /// </summary>
        private string appSecret = "";

        /// <summary>
        /// 商户号
        /// </summary>
        private string merchantId = "";

        /// <summary>
        /// 商户密钥
        /// </summary>
        private string merchantKey = "";

        /// <summary>
        /// 访问Token
        /// </summary>
        private string accessToken;

        /// <summary>
        /// access_token有效期
        /// </summary>
        private DateTime accessTokenExpire;

        private GetAccessTokenFromHandler getAccessTokenFrom;

        public string AppId { get { return appId; } }

        public string AppSecret { get { return appSecret; } }

        public string MerchantId { get { return merchantId; } }

        public string MerchantKey { get { return merchantKey; } }

        public WxpayConfig(string appId, string appSecret, string merchantId, string merchantKey)
        {
            this.appId = appId ?? "";
            this.appSecret = appSecret ?? "";
            this.merchantId = merchantId ?? "";
            this.merchantKey = merchantKey ?? "";
        }

        /// <summary>
        /// 设置从外部读取accessToken的方法
        /// </summary>
        /// <param name="m"></param>
        public void Set_GetAccessTokenFrom(GetAccessTokenFromHandler m)
        {
            getAccessTokenFrom = m;
        }

        private object writeAccesstokenLock = new object();
        public string GetAccessToken()
        {
            if (accessTokenExpire < DateTime.Now)
            {
                lock (writeAccesstokenLock) //锁住，后来者在外等
                {
                    if (accessTokenExpire < DateTime.Now) //后来者如果进来后，发现已经更新，则跳过
                    {
                        if (getAccessTokenFrom != null)
                        {
                            string tnk;
                            DateTime exp;
                            getAccessTokenFrom.Invoke(out tnk, out exp);
                            if (!string.IsNullOrEmpty(tnk) && exp != DateTime.MinValue)
                            {
                                accessToken = tnk;
                                accessTokenExpire = exp;
                            }
                        }
                        else
                        {
                            RefreshAccessToken();
                        }
                    }
                }
            }

            return accessToken;
        }

        private void RefreshAccessToken()
        {
            var url = string.Format(getAccessToken_Url, appId, appSecret);

            string result, err;
            if (WxpayCore.Get(url, 5000, out result, out err) && !string.IsNullOrEmpty(result))
            {
                var root = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                if (root.ContainsKey("access_token") && root.ContainsKey("expires_in"))
                {
                    var at = (string)root["access_token"];
                    var ex = root["expires_in"];
                    if (!string.IsNullOrEmpty(at))
                    {
                        accessToken = at;
                        accessTokenExpire = DateTime.Now.AddSeconds(int.Parse(ex.ToString()) - 60 * 10);
                    }
                }
            }
        }
    }
}
