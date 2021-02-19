using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.SessionState;
using System.IO;

namespace Server.svr
{
    public class paynotify : IHttpHandler, IRequiresSessionState
    {

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        private string GetRequestText(HttpRequest request)
        {
            System.IO.Stream str = request.InputStream;
            int count = 0;
            byte[] buffer = new byte[1024];
            StringBuilder builder = new StringBuilder();
            while ((count = str.Read(buffer, 0, 1024)) > 0)
            {
                builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
            }
            str.Flush();
            str.Close();
            str.Dispose();
            return builder.ToString();
        }

        protected void Write(HttpContext context, string content)
        {
            if (content == null || content == string.Empty) content = "";
            context.Response.ContentEncoding = Encoding.UTF8;
            var obuf = Encoding.UTF8.GetBytes(content);
            var ostrm = context.Response.OutputStream;
            ostrm.Write(obuf, 0, obuf.Length);
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                IBLL.IPayNotify bll = new BLL.PayNotify();
                string con = GetRequestText(context.Request);
                //LogHelper.writeLog("paynotify ->ProcessRequest()", con, null);
                ReadXML read = new ReadXML(con);
                if (read.Read("return_code") == "FAIL")
                {
                    return;
                }
                string out_trade_no = read.Read("out_trade_no");//商户订单号
                string transaction_id = read.Read("transaction_id");//微信交易号
                //签名验证

                //
                if (read.Read("result_code") == "FAIL")
                {
                    bll.Fail(transaction_id, out_trade_no);
                }
                else
                {
                    bll.Success(transaction_id, out_trade_no);
                }

                StringBuilderForXML sb = new StringBuilderForXML();
                sb.Append("return_code", "SUCCESS");
                sb.Append("return_msg", "OK");
                Write(context, sb.ToString());

            }
            catch (Exception e)
            {
                LogHelper.writeLog("paynotify ->ProcessRequest()", e.ToString(),null);
                //Write(e.Message);
            }
            
        }

    }

}