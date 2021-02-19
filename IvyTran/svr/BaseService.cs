using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Text;
using ReadWriteContext;
using System.Reflection;
using IvyTran.Helper;

namespace IvyTran.svr
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseService : IHttpHandler, IRequiresSessionState
    {
        internal HttpContext context;
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

        public IReadContext ReadContext = null;

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
            this.context = context;
            context.Response.ContentType = "text/plain";
            coding = CreateCoding(context);
            ReadContext = CreateReadContext(context);
            WriteContext = CreateWriteContext(context);
            string type = context.Request.QueryString["t"];
            if (context.Request.RequestType.ToLower() == "post")
            {
                try
                {
                    Dictionary<string, object> kv = ReadContext.ToDictionary();
                    string str = ProcessRequestPostHandler(type, kv);
                    Write(context, str);
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("ProcessRequest", ex.ToString(), type);
                }
            }
            else
            {
                string str = ProcessRequestGetHandler(type);
                Write(context, str);
            }
        }

        protected virtual string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            return "";
        }

        protected virtual string ProcessRequestGetHandler(string t)
        {
            return "";
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

        public void WriteModel<T>(T t)
        {
            Type type = t.GetType();
            PropertyInfo[] infos = type.GetProperties();

            foreach (PropertyInfo info in infos)
            {
                object obj = info.GetValue(t, null);
                if (obj != null)
                {
                    WriteContext.Append(info.Name, obj.ToString());
                }

            }

        }

        /// <summary>
        /// 是否包含某些键
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        protected bool ExistsKeys(Dictionary<string, object> keyValue, params string[] keys)
        {
            for (var i = 0; i < keys.Length; i++)
            {
                if (!keyValue.ContainsKey(keys[i]))
                {
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

            if ((kv[k] is String) && (string)kv[k] == "") return DateTime.Parse("1900-01-01");
            else if ((kv[k] is String) && (string)kv[k] != "") return DateTime.Parse((string)kv[k]);
            return DateTime.Parse((string)kv[k]);
        }

        public T ObjectToModel<T>(Dictionary<string, object> kv)
        {
            Type type = typeof(T);
            T t2 = (T)type.Assembly.CreateInstance(type.FullName);

            PropertyInfo[] infos = type.GetProperties();

            foreach (PropertyInfo info in infos)
            {
                dynamic dyn = info.PropertyType.IsValueType ? Activator.CreateInstance(info.PropertyType) : null;

                switch (info.PropertyType.FullName)
                {
                    case "System.String":
                        dyn = ObjectToString(kv, info.Name);
                        break;
                    case "System.Double":
                        dyn = ObjectToDecimal(kv, info.Name);
                        break;
                    case "System.Decimal":
                        dyn = ObjectToDecimal(kv, info.Name);
                        break;
                    case "System.Int32":
                        dyn = ObjectToInt(kv, info.Name);
                        break;
                    case "System.DateTime":
                        dyn = ObjectToDate(kv, info.Name);
                        break;
                    case "System.Char":
                        dyn = ObjectToChar(kv, info.Name);
                        break;
                }

                info.SetValue(t2, dyn, null);
            }
            return t2;
        }

        public List<T> ObjectToModel<T>(string key)
        {
            List<T> lis = new List<T>();
            List<IReadContext> rlis = ReadContext.ReadList(key);

            Type type = typeof(T);
            T t2 = (T)type.Assembly.CreateInstance(type.FullName);
            PropertyInfo[] infos = type.GetProperties();

            foreach (IReadContext r in rlis)
            {
                t2 = (T)type.Assembly.CreateInstance(type.FullName);
                Dictionary<string, object> kv = r.ToDictionary();
                foreach (PropertyInfo info in infos)
                {
                    dynamic dyn = info.PropertyType.IsValueType ? Activator.CreateInstance(info.PropertyType) : null;

                    switch (info.PropertyType.FullName)
                    {
                        case "System.String":
                            dyn = ObjectToString(kv, info.Name);
                            break;
                        case "System.Double":
                            dyn = ObjectToDecimal(kv, info.Name);
                            break;
                        case "System.Decimal":
                            dyn = ObjectToDecimal(kv, info.Name);
                            break;
                        case "System.Int32":
                            dyn = ObjectToInt(kv, info.Name);
                            break;
                        case "System.DateTime":
                            dyn = ObjectToDate(kv, info.Name);
                            break;
                        case "System.Char":
                            dyn = ObjectToChar(kv, info.Name);
                            break;
                    }

                    info.SetValue(t2, dyn, null);
                }
                lis.Add(t2);
            }

            return lis;
        }

    }

}