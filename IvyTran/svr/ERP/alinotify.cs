using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    /// <summary>
    /// 功能：服务器异步通知页面
    /// 日期：2016-12-28
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// ///////////////////页面功能说明///////////////////
    /// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
    /// 该页面不能在本机电脑测试，请到服务器上做测试。请确保外部可以访问该页面。
    /// 如果没有收到该页面返回的 success 信息，支付宝会在24小时内按一定的时间策略重发通知
    /// </summary>
    public class alinotify : IHttpHandler, IRequiresSessionState
    {

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                SortedDictionary<string, string> sPara = GetRequestPost(context.Request);

                if (sPara.Count > 0)//判断是否有带返回参数
                {
                    //商户订单号
                    string out_trade_no = context.Request.Form["out_trade_no"];
                    //支付宝交易号
                    string trade_no = context.Request.Form["trade_no"];
                    //订单支付金额
                    decimal total_amount = Conv.ToDecimal(context.Request.Form["total_amount"]);

                    IPayNotify bll = new PayNotify();
                    //交易状态
                    //在支付宝的业务通知中，只有交易通知状态为TRADE_SUCCESS或TRADE_FINISHED时，才是买家付款成功。
                    string trade_status = context.Request.Form["trade_status"];
                    if (trade_status == "TRADE_SUCCESS" || trade_status == "TRADE_FINISHED")
                    {
                        bll.Success(trade_no, out_trade_no);
                    }
                    else
                    {
                        bll.Fail(trade_no, out_trade_no);
                    }

                    context.Response.Write("success");  //请不要修改或删除
                }
                else
                {
                    context.Response.Write("无通知参数");
                }

            }
            catch (Exception e)
            {
                LogHelper.writeLog("alinotify ->ProcessRequest()", e.ToString(), null);
                //Write(e.Message);
            }

        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost(HttpRequest request)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], request.Form[requestItem[i]]);
            }

            return sArray;
        }


    }
}