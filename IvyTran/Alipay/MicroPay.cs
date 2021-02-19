using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using IvyTran.Helper;
using IvyTran.IBLL;
using IvyTran;
using IvyTran.BLL;
using IvyTran.BLL.ERP;
using IvyTran.IBLL.ERP;

namespace Server.Alipay
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
        /// <param name="mer_key">常春藤商户id</param>
        /// <param name="ord_id">订单号</param>
        /// <param name="pay_amt">支付金额</param>
        /// <param name="barcode">支付码</param>
        /// <param name="branch_no">机构码</param>
        /// <param name="sign">客户端签名</param>
        /// <param name="res"></param>
        /// <param name="errMsg"></param>
        void AliMicroPay(string mer_id, string sheet_no, decimal pay_amt, string barcode, string jh, out int errId, out string errMsg)
        {
            ISysBLL bll = new SysBLL();
            var config = bll.GetMerAlipayById(mer_id);
            if (config == null)
            {
                throw new Exception("未配置支付宝帐户");
            }
            //
            IvyTran.body.alimicropay_par par = new IvyTran.body.alimicropay_par();
            par.app_id = config.app_id;
            par.method = "alipay.trade.pay";
            par.charset = "utf-8";
            par.sign_type = "RSA";
            par.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            par.version = "1.0";
            par.out_trade_no = sheet_no;
            par.scene = "bar_code";
            par.auth_code = barcode;
            par.subject = "commongoods";
            par.total_amount = pay_amt.ToString("0.00");
            par.store_id = jh;
            string url = "https://openapi.alipay.com/gateway.do";
            Aop.Api.IAopClient client = new Aop.Api.DefaultAopClient(url, par.app_id, config.rsa1_private);
            Aop.Api.Request.AlipayTradePayRequest req = new Aop.Api.Request.AlipayTradePayRequest();
            req.BizContent = par.biz_content();
            var response = client.Execute<Aop.Api.Response.AlipayTradePayResponse>(req);
            var context = response.Body;
            var parres = new IvyTran.body.alimicropay_res(context);
            if (parres.code == "10000")
            {
                errId = 0;
                errMsg = "";
            }
            else if (parres.code == "20000" || parres.code == "20001" || parres.code == "40001" || parres.code == "40002" || parres.code == "40006")//出错
            {
                errId = 1;
                errMsg = parres.code + "," + parres.msg + "," + parres.sub_code + "," + parres.sub_msg;
            }
            else if (parres.code == "40004")
            {
                if (parres.is_doing(parres.sub_code) == true)
                {
                    errId = 2;
                    errMsg = "";
                }
                else
                {
                    errId = 1;
                    errMsg = parres.sub_code + "," + parres.sub_msg;
                }
            }
            else//未知错误
            {
                errId = 2;
                errMsg = "";
            }
            if (errId == 1)
            {
                LogHelper.writeLog("AliMicroPay()", errMsg, mer_id, sheet_no, par.biz_content());
            }
        }

        void AliMicroQuery(string mer_id, string sheet_no, out int errId, out string errMsg)
        {
            ISysBLL bll = new SysBLL();
            var config = bll.GetMerAlipayById(mer_id);
            if (config == null)
            {
                throw new Exception("未配置支付宝帐户");
            }
            //
            IvyTran.body.alimicroquery_par par = new IvyTran.body.alimicroquery_par();
            par.app_id = config.app_id;
            par.method = "alipay.trade.query";
            par.charset = "utf-8";
            par.sign_type = "RSA";
            par.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            par.version = "1.0";
            par.out_trade_no = sheet_no;
            string url = "https://openapi.alipay.com/gateway.do";
            Aop.Api.IAopClient client = new Aop.Api.DefaultAopClient(url, par.app_id, config.rsa1_private);
            Aop.Api.Request.AlipayTradeQueryRequest req = new Aop.Api.Request.AlipayTradeQueryRequest();
            req.BizContent = par.biz_content();
            var response = client.Execute<Aop.Api.Response.AlipayTradeQueryResponse>(req);
            var context = response.Body;
            var parres = new IvyTran.body.alimicroquery_res(context);
            if (parres.code == "10000")
            {
                if (parres.is_success() == true)
                {
                    errId = 0;
                    errMsg = "";
                }
                else if (parres.is_doing() == true)
                {
                    errId = 2;
                    errMsg = parres.sub_code + "," + parres.sub_msg;
                }
                else
                {
                    errId = 1;
                    errMsg = parres.sub_code + ",," + parres.sub_msg + ",," + parres.trade_status;
                }
            }
            else if (parres.code == "40004")
            {
                if (parres.is_doing() == true)
                {
                    errId = 2;
                    errMsg = parres.sub_code + "," + parres.sub_msg;
                }
                else
                {
                    errId = 1;
                    errMsg = parres.sub_code + ",," + parres.sub_msg;
                }
            }
            else
            {
                errId = 2;
                errMsg = parres.msg;
            }
            if (errId != 0)
            {
                LogHelper.writeLog("AliMicroQuery()", errMsg, errId.ToString(), mer_id, sheet_no, par.biz_content());
            }

        }

        void AliMicroCancel(string mer_id, string sheet_no, out string need_try, out string need_query, out int errId, out string errMsg)
        {
            ISysBLL bll = new SysBLL();
            var config = bll.GetMerAlipayById(mer_id);
            if (config == null)
            {
                throw new Exception("未配置支付宝帐户");
            }
            //
            IvyTran.body.alimicrocancel_par par = new IvyTran.body. alimicrocancel_par();
            par.app_id = config.app_id;
            par.method = "alipay.trade.cancel";
            par.charset = "utf-8";
            par.sign_type = "RSA";
            par.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            par.version = "1.0";
            par.out_trade_no = sheet_no;
            string url = "https://openapi.alipay.com/gateway.do";
            Aop.Api.IAopClient client = new Aop.Api.DefaultAopClient(url, par.app_id, config.rsa1_private);
            Aop.Api.Request.AlipayTradeCancelRequest req = new Aop.Api.Request.AlipayTradeCancelRequest();
            req.BizContent = par.biz_content();
            var response = client.Execute<Aop.Api.Response.AlipayTradeCancelResponse>(req);
            var context = response.Body;
            var parres = new IvyTran.body.alimicrocancel_res(context);
            if (parres.code == "10000")
            {
                if (parres.retry_flag == "Y")
                {
                    need_try = "1";
                    need_query = "0";
                    errId = 0;
                    errMsg = parres.sub_msg;
                }
                else
                {
                    need_try = "0";
                    need_query = "1";
                    errId = 0;
                    errMsg = parres.sub_msg;
                }

            }
            else
            {
                need_try = "1";
                need_query = "0";
                errId = 0;
                errMsg = parres.msg;
            }

            if (errId != 0)
            {
                LogHelper.writeLog("AliMicroCancel()", errMsg, errId.ToString(), mer_id, sheet_no, par.biz_content());
            }
        }
    }
}