using Com.Alipay.Business;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using IvyTran;
using IvyTran.Helper;
using System;

namespace Com.Alipay
{
    /// <summary>
    /// 功能：预下单接口接入页
    /// 日期：2016-12-27
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    ///
    /// /////////////////注意///////////////////////////////////////////////////////////////
    /// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
    /// 1、支持中心（https://support.open.alipay.com/alipay/support/index.htm），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
    /// 2、开发者论坛（https://openclub.alipay.com/）
    /// </summary>
    public class AliPay
    {
        public AliPay()
        {
        }

        /// <summary>
        /// 【统一收单线下交易预创建】构造支付请求数据
        /// </summary>
        /// <returns>请求数据集</returns>
        private AlipayTradePrecreateContentBuilder BuildPrecreateContent(string ord_id, string ord_name, decimal total_amount, string partner_id)
        {
            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
            //收款账号
            builder.seller_id = partner_id;
            //订单编号
            builder.out_trade_no = ord_id;
            //订单总金额
            builder.total_amount = total_amount.ToString();
            //订单名称
            builder.subject = ord_name;
            //自定义超时时间：m表示分钟
            builder.timeout_express = "5m";
            //订单描述
            //builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            //builder.store_id = "";
            //操作员编号，很重要的参数，可以用作之后的营销
            //builder.operator_id = "";

            //传入商品信息详情
            /*
            List<GoodsInfo> gList = new List<GoodsInfo>();
            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = "goods id";
            goods.goods_name = "goods name";
            goods.price = "0.01";
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;
            */
            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;

            return builder;
        }

        /// <summary>
        /// 【统一收单线下交易退款】构造退款请求数据
        /// </summary>
        /// <returns>请求数据集</returns>
        private AlipayTradeRefundContentBuilder BuildContent(string out_trade_no, string out_request_no, decimal refund_amount)
        {
            AlipayTradeRefundContentBuilder builder = new AlipayTradeRefundContentBuilder();

            //支付宝交易号与商户网站订单号不能同时为空
            builder.out_trade_no = out_trade_no;

            //退款请求单号保持唯一性。
            builder.out_request_no = out_request_no;

            //退款金额
            builder.refund_amount = refund_amount.ToString();

            builder.refund_reason = "订单退款";

            return builder;
        }

        /// <summary>
        /// 【统一收单线下交易条码支付】构造支付请求数据
        /// </summary>
        /// <returns>请求数据集</returns>
        private AlipayTradePayContentBuilder BuildPayContent(string ord_id, string ord_name, string auth_coe, decimal total_amount, string partner_id)
        {
            string out_trade_no = ord_id;

            //扫码枪扫描到的用户手机钱包中的付款条码
            AlipayTradePayContentBuilder builder = new AlipayTradePayContentBuilder();

            //收款账号
            builder.seller_id = partner_id;
            //订单编号
            builder.out_trade_no = out_trade_no;
            //支付场景，无需修改
            builder.scene = "bar_code";
            //支付授权码,付款码
            builder.auth_code = auth_coe;
            //订单总金额
            builder.total_amount = total_amount.ToString();
            //订单名称
            builder.subject = ord_name;
            //自定义超时时间
            builder.timeout_express = "5m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            //builder.store_id = "test store id";
            //操作员编号，很重要的参数，可以用作之后的营销
            //builder.operator_id = "test";

            return builder;
        }

        //alipay.trade.precreate(统一收单线下交易预创建)
        public bool Alipay_PreCreate(string app_id, string merchant_private_key, string alipay_public_key, string ord_id, string ord_name, decimal total_amt
            , string partner_id, out string barcode, out string msg)
        {
            try
            {
                IAlipayTradeService sc = F2FBiz.CreateClientInstance(Config.serverUrl, app_id, merchant_private_key, Config.version,
                            Config.sign_type, alipay_public_key, Config.charset);

                AlipayTradePrecreateContentBuilder builder = BuildPrecreateContent(ord_id, ord_name, total_amt, partner_id);
                string out_trade_no = builder.out_trade_no;

                //如果需要接收扫码支付异步通知，那么请把下面两行注释代替本行。
                //推荐使用轮询撤销机制，不推荐使用异步通知,避免单边账问题发生。
                //AlipayF2FPrecreateResult precreateResult = serviceClient.tradePrecreate(builder);
                string notify_url = AppSetting.ali_notify_url;  //商户接收异步通知的地址
                AlipayF2FPrecreateResult precreateResult = sc.tradePrecreate(builder, notify_url);

                //以下返回结果的处理供参考。
                //payResponse.QrCode即二维码对于的链接
                //将链接用二维码工具生成二维码打印出来，顾客可以用支付宝钱包扫码支付。
                msg = "";
                barcode = "";
                if (precreateResult.Status == ResultEnum.SUCCESS)
                {
                    barcode = precreateResult.response.QrCode;
                    return true;
                }
                else if (precreateResult.Status == ResultEnum.FAILED)
                {
                    msg = precreateResult.response.Body;
                    return false;
                }
                else
                {
                    if (precreateResult.response == null)
                    {
                        msg = "配置或网络异常，请检查后重试";
                    }
                    else
                    {
                        msg = "系统异常，请更新外部订单后重新发起请求";
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PreCreate ->Alipay_PreCreate()", ex.ToString(), ord_id, total_amt.ToString());
                throw ex;
            }
        }

        //alipay.trade.query(统一收单线下交易查询)
        public bool Alipay_Query(string app_id, string merchant_private_key, string alipay_public_key, string ord_id, out string msg)
        {
            try
            {
                IAlipayTradeService sc = F2FBiz.CreateClientInstance(Config.serverUrl, app_id, merchant_private_key, Config.version,
                            Config.sign_type, alipay_public_key, Config.charset);
                //商户订单号
                string out_trade_no = ord_id;

                //商户网站订单系统中唯一订单号，必填

                AlipayF2FQueryResult queryResult = sc.tradeQuery(out_trade_no);

                msg = "";
                if (queryResult.Status == ResultEnum.SUCCESS)
                {
                    return true;
                }
                else if (queryResult.Status == ResultEnum.FAILED)
                {
                    msg = queryResult.response.Body;
                    return false;
                }
                else
                {
                    if (queryResult.response == null)
                    {
                        msg = "配置或网络异常，请检查后重试";
                    }
                    else
                    {
                        msg = "系统异常，请重试";
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PreCreate ->Alipay_Query()", ex.ToString(), ord_id);
                throw ex;
            }
        }

        //alipay.trade.refund(统一收单线下交易退款)
        public bool Alipay_Refund(string app_id, string merchant_private_key, string alipay_public_key, string ord_id, string req_id, decimal refund_amt, out string msg)
        {
            try
            {
                IAlipayTradeService sc = F2FBiz.CreateClientInstance(Config.serverUrl, app_id, merchant_private_key, Config.version,
                            Config.sign_type, alipay_public_key, Config.charset);

                AlipayTradeRefundContentBuilder builder = BuildContent(ord_id, req_id, refund_amt);

                AlipayF2FRefundResult refundResult = sc.tradeRefund(builder);

                msg = "";
                if (refundResult.Status == ResultEnum.SUCCESS)
                {
                    return true;
                }
                else if (refundResult.Status == ResultEnum.FAILED)
                {
                    msg = refundResult.response.Body;
                    return false;
                }
                else
                {
                    if (refundResult.response == null)
                    {
                        msg = "配置或网络异常，请检查后重试";
                    }
                    else
                    {
                        msg = "系统异常，请走人工退款流程";
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PreCreate ->Alipay_Query()", ex.ToString(), ord_id);
                throw ex;
            }
        }

        //alipay.trade.pay(统一收单线下交易条码支付)
        public bool Alipay_BarcodePay(string app_id, string merchant_private_key, string alipay_public_key, string ord_id, string ord_name, decimal total_amt
            , string auth_code, string partner_id, out string msg)
        {
            try
            {
                IAlipayTradeService sc = F2FBiz.CreateClientInstance(Config.serverUrl, app_id, merchant_private_key, Config.version,
                            Config.sign_type, alipay_public_key, Config.charset);

                AlipayTradePayContentBuilder builder = BuildPayContent(ord_id, ord_name, auth_code, total_amt, partner_id);
                string out_trade_no = builder.out_trade_no;

                AlipayF2FPayResult payResult = sc.tradePay(builder);

                msg = "";
                if (payResult.Status == ResultEnum.SUCCESS)
                {
                    return true;
                }
                else if (payResult.Status == ResultEnum.FAILED)
                {
                    msg = payResult.response.Body;
                    return false;
                }
                else
                {
                    if (payResult.response == null)
                    {
                        msg = "配置或网络异常，请检查后重试";
                    }
                    else
                    {
                        msg = "网络异常，请检查网络配置后重试";
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("PreCreate ->Alipay_BarcodePay()", ex.ToString(), ord_id, total_amt.ToString());
                throw ex;
            }
        }
    }
}