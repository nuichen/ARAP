using ReadWriteContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Server.svr
{
    public class BaseService : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        private System.Text.Encoding coding = Encoding.UTF8;

        private System.Text.Encoding CreateCoding(HttpContext context)
        {

            return Encoding.UTF8;

        }

        private IReadContext ReadContext = null;
        private IReadContext CreateReadContext(HttpContext context)
        {
            var istrm = context.Request.InputStream;
            var ibuf = new byte[istrm.Length];
            istrm.Read(ibuf, 0, ibuf.Length);
            var str = coding.GetString(ibuf, 0, ibuf.Length);

            return new ReadContextByJson(str);

        }

        public IWriteContext WriteContext = null;
        private IWriteContext CreateWriteContext(HttpContext context)
        {
            return new WriteContextByJson();
        }


        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            coding = CreateCoding(context);
            ReadContext = CreateReadContext(context);
            WriteContext = CreateWriteContext(context);
            //
            string type = context.Request.QueryString["t"];
            if (context.Request.RequestType.ToLower() == "post")
            {
                Dictionary<string, object> kv = ReadContext.ToDictionary();
                string hostIP = context.Request.UserHostAddress;
                kv.Add("hostIP", hostIP);
                ProcessRequestPostHandler(type, kv, context);
            }
            else
            {
                ProcessRequestGetHandler(type, context);
            }
        }

        protected virtual void ProcessRequestPostHandler(string t, Dictionary<string, object> kv, HttpContext context)
        {

        }

        protected virtual void ProcessRequestGetHandler(string t, HttpContext context)
        {

        }

        protected void Write(HttpContext context, string content)
        {
            if (content == null || content == string.Empty) content = "";
            context.Response.ContentEncoding = coding;
            var obuf = coding.GetBytes(content);
            var ostrm = context.Response.OutputStream;
            ostrm.Write(obuf, 0, obuf.Length);
        }

        protected void WriteException(HttpContext context, Exception e)
        {
            if (e.GetType() == typeof(ExceptionBase))
            {
                ExceptionBase e1 = (ExceptionBase)e;
                WriteContext.Append("errId", e1.errId.ToString());
                WriteContext.Append("errMsg", e.Message);
                Write(context, WriteContext.ToString());
            }
            else
            {
                WriteContext.Append("errId", "-1");
                WriteContext.Append("errMsg", e.Message);
                Write(context, WriteContext.ToString());
                
            }


        }

        protected void WriteSuccess(HttpContext context)
        {
            WriteContext.Append("errId", "0");
            WriteContext.Append("errMsg", "");
            Write(context, WriteContext.ToString());
        }


        protected void WriteInvalidParameters(HttpContext context)
        {
            WriteContext.Append("errId", "-4");
            WriteContext.Append("errMsg", "invalid parameters");
            Write(context, WriteContext.ToString());
        }


        protected void WriteError(HttpContext context, string msg)
        {
            WriteContext.Append("errId", "-1");
            WriteContext.Append("errMsg", msg);
            Write(context, WriteContext.ToString());
        }

        /// <summary>
        /// 是否包含某些键
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keys"></param>
        protected bool ExistsKeys(Dictionary<string, object> keyValue, params string[] keys)
        {
            for (var i = 0; i < keys.Length; i++)
            {
                if (!keyValue.ContainsKey(keys[i]))
                {
                    LogHelper.writeLog("ExistsKeys(2)", "错误",keys[i]);
                    return false;
                }
            }
            return true;
        }

        public string ObjectToString(Dictionary<string, object> kv, string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return "";
            var v = kv[k].ToString();
            if (v == null || v == string.Empty) v = "";
            return v;
        }

        public char ObjectToChar(Dictionary<string, object> kv, string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return char.MinValue;
            var s = kv[k].ToString();
            if (s.Length == 0) return char.MinValue;
            else if (s.Length == 1) return s[0];
            else return char.MinValue;
        }

        public int ObjectToInt(Dictionary<string, object> kv, string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return 0;
            if (kv[k] is int) return (int)kv[k];
            if (kv[k] is long) return (int)(long)kv[k];

            int i;
            int.TryParse(kv[k].ToString(), out i);
            return i;
        }

        public decimal ObjectToDecimal(Dictionary<string, object> kv, string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return 0;
            if (kv[k] is Decimal) return (decimal)kv[k];
            if (kv[k] is Double || kv[k] is Int64 || kv[k] is Int32 || kv[k] is Int16 || kv[k] is Single || kv[k] is String)
            {
                decimal d;
                decimal.TryParse(kv[k].ToString(), out d);
                return d;
            }
            return 0;
        }

        public DateTime ObjectToDate(Dictionary<string, object> kv, string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return DateTime.MinValue;
            if (kv[k] is DateTime) return (DateTime)kv[k];

            if ((kv[k] is String) && (string)kv[k] == "") return DateTime.MinValue;
            else if ((kv[k] is String) && (string)kv[k] != "") return DateTime.Parse((string)kv[k]);
            return DateTime.Parse((string)kv[k]);
        }

    }
}
