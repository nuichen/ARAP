using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Server.Wxpay
{
    public class MicroPay
    {
        private string request(string url, string context)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Timeout = 60 * 1000;
            req.ReadWriteTimeout = 60 * 1000;
            //
            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(context);
            req.Method = "POST";
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();
            //
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();
            stream.Close();
            reader.Close();
            return str;
        }

        /// <summary>
        /// 微信扫码支付
        /// </summary>
        /// <param name="mer_key">常春藤商户key</param>
        /// <param name="ord_id">订单号</param>
        /// <param name="pay_amt">支付金额</param>
        /// <param name="barcode">支付码</param>
        /// <param name="branch_no">机构码</param>
        /// <param name="sign">客户端签名</param>
        /// <param name="res"></param>
        /// <param name="errMsg"></param>
        public void WXMicroPay(string mer_id, string sheet_no, decimal pay_amt, string barcode, string jh, out int errId, out string errMsg)
        {
            IBLL.ISysBLL bll = new BLL.SysBLL();

            var wxconfig = bll.GetMerWxpayById(mer_id);
            if (wxconfig == null)
            {
                throw new Exception("未配置微信帐户");
            }
            //
            body.micropay_par par = new body.micropay_par();
            par.appid = wxconfig.wx_appid;
            par.mch_id = wxconfig.wx_mcid;
            par.device_info = jh;
            par.nonce_str = Guid.NewGuid().ToString().Replace("-","");
            par.body = "commongoods";
            par.out_trade_no = sheet_no;
            par.total_fee = Convert.ToInt32(pay_amt * 100);
            par.spbill_create_ip = Appsetting.server_ip;
            par.auth_code = barcode;

            string url = "https://api.mch.weixin.qq.com/pay/micropay";
            
            var context = request(url, par.ToString(wxconfig.wx_paykey));
            var parres = new body.micropay_res(context);
            if (parres.return_code.ToLower() == "success".ToLower())
            {
                if (parres.result_code.ToLower() == "success".ToLower())
                {
                    errId = 0;
                    errMsg = "";
                }
                else
                {
                    if (parres.is_doing(parres.err_code) == true)
                    {
                        errId = 2;
                        errMsg = "";
                    }
                    else
                    {
                        errId = 1;
                        errMsg = parres.err_code + "," + parres.err_code_des;
                    }
                }
            }
            else
            {
                errId = 1;
                errMsg = parres.return_code + "," + parres.return_msg;
            }
            if (errId == 1)
            {
                LogHelper.writeLog("WXMicroPay()", "[" + errId.ToString() + "]" + errMsg, mer_id, barcode, jh, par.ToString(wxconfig.wx_paykey));
            }
        }

        /// <summary>
        /// 微信扫码支付查询订单支付状态
        /// </summary>
        /// <param name="mer_key"></param>
        /// <param name="ord_id"></param>
        /// <param name="sign"></param>
        /// <param name="errId"></param>
        /// <param name="errMsg"></param>
        public void WXMicroPayQuery(string mer_id, string sheet_no, out int errId, out string errMsg)
        {
            IBLL.ISysBLL bll = new BLL.SysBLL();
            var wxconfig = bll.GetMerWxpayById(mer_id);
            if (wxconfig == null)
            {
                throw new Exception("未配置微信帐户");
            }
            //
            body.microquery_par par = new body.microquery_par();
            par.appid = wxconfig.wx_appid;
            par.mch_id = wxconfig.wx_mcid;
            par.nonce_str = Guid.NewGuid().ToString().Replace("-", "");
            par.out_trade_no = sheet_no;

            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            var context = request(url, par.ToString(wxconfig.wx_paykey));
            var parres = new body.microquery_res(context);
            if (parres.return_code.ToLower() == "success".ToLower())
            {
                if (parres.result_code.ToLower() == "success".ToLower())
                {
                    errId = 0;
                    errMsg = "";
                    if (parres.is_doing(parres.trade_state) == true)
                    {
                        errId = 2;
                        errMsg = "";
                    }
                    else if (parres.is_success(parres.trade_state) == true)
                    {
                        errId = 0;
                        errMsg = "";
                    }
                    else
                    {
                        errId = 1;
                        errMsg = parres.trade_state + "," + parres.trade_state_desc;
                    }
                }
                else
                {
                    errId = 2;
                    errMsg = "";
                }
            }
            else
            {
                errId = 2;
                errMsg = "";
            }
            if (errId == 1)
            {
                LogHelper.writeLog("WXMicroPayQuery()", errMsg, mer_id, sheet_no, par.ToString(wxconfig.wx_paykey));
            }
        }


        private string cert_file(string mer_id)
        {
            return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "cert\\" + mer_id + ".p12";
        }

        private string request(string context, string mer_id, string mcid)
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            string cert = cert_file(mer_id);
            string password = mcid;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);


            //X509Certificate cer = new X509Certificate(cert, password);
            X509Certificate2 cer = new X509Certificate2(cert, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.ClientCertificates.Add(cer);
            //
            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(context);
            webrequest.Method = "POST";
            Stream requestStream = webrequest.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();
            //
            HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
            Stream stream = webreponse.GetResponseStream();
            string resp = string.Empty;
            using (StreamReader reader = new StreamReader(stream))
            {
                resp = reader.ReadToEnd();
            }
            return resp;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

        public void WXMicroPayCancel(string mer_id, string sheet_no, out string need_try, out string need_query, out int errId, out string errMsg)
        {
            IBLL.ISysBLL bll = new BLL.SysBLL();
            var wxconfig = bll.GetMerWxpayById(mer_id);
            if (wxconfig == null)
            {
                throw new Exception("未配置微信帐户");
            }
            //
            body.microcancel_par par = new body.microcancel_par();
            par.appid = wxconfig.wx_appid;
            par.mch_id = wxconfig.wx_mcid;
            par.nonce_str = Guid.NewGuid().ToString().Replace("-","");
            par.out_trade_no = sheet_no;

            var context = request(par.ToString(wxconfig.wx_paykey), mer_id, wxconfig.wx_mcid);
            var parres = new body.microcancel_res(context);

            if (parres.return_code.ToLower() == "success".ToLower())
            {
                if (parres.result_code.ToLower() == "success".ToLower())
                {
                    need_try = "0";
                    need_query = "0";
                    errId = 1;
                    errMsg = "";
                }
                else
                {
                    if (parres.recall == "Y")
                    {
                        need_try = "1";
                        need_query = "0";
                        errId = 0;
                        errMsg = parres.err_code + "," + parres.err_code_des;
                    }
                    else
                    {
                        need_try = "0";
                        need_query = "1";
                        errId = 0;
                        errMsg = parres.err_code + "," + parres.err_code_des;
                    }
                }
            }
            else
            {
                need_try = "1";
                need_query = "0";
                errId = 0;
                errMsg = parres.return_code + "," + parres.return_msg;
            }
            if (errId == 1)
            {
                LogHelper.writeLog("WXMicroPayCancel()", errMsg, mer_id, sheet_no, par.ToString(wxconfig.wx_paykey));
            }
        }

    }
}