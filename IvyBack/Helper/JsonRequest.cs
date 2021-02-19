using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using ReadWriteContext;
using Newtonsoft.Json;

namespace IvyBack.Helper
{
    public class JsonRequest
    {
        private IWriteContext wc;
        private IReadContext rc;

        public JsonRequest()
        {
            if (null == wc)
            {
                wc = new WriteContextByJson();
            }
        }
        private string returnJson = "";//返回Json
        private static bool doing = false;

        public void SetJson(string json)
        {
            returnJson = json;
            rc = new ReadWriteContext.ReadContextByJson(returnJson);
        }

        public void request(string par_url)
        {
            if (string.IsNullOrEmpty(wc.ToString()))
                WriteNone();
            rc = new ReadWriteContext.ReadContextByJson(wc.ToString());
            if (!rc.Exist("oper_id") && Program.oper != null)
            {
                wc.Append("oper_id", Program.oper.oper_id);
            }
            while (1 == 1)
            {
                System.Threading.Thread.Sleep(100);
                if (doing == false)
                {
                    try
                    {
                        doing = true;
                        string url = AppSetting.svr + par_url;
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                        req.KeepAlive = false;
                        req.Timeout = 5 * 60 * 1000;
                        req.ReadWriteTimeout = 5 * 60 * 1000;
                        req.CookieContainer = AppSetting.CookieContainer;
                        //
                        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(wc.ToString());
                        req.Method = "POST";
                        Stream requestStream = req.GetRequestStream();
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                        requestStream.Close();
                        //
                        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                        res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
                        AppSetting.CookieContainer.Add(res.Cookies);
                        Stream stream = res.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        string str = reader.ReadToEnd();
                        stream.Close();
                        reader.Close();
                        //
                        doing = false;
                        //
                        returnJson = str;
                        rc = new ReadWriteContext.ReadContextByJson(returnJson);
                        return;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        doing = false;
                    }
                }
            }
        }
        public void request(string par_url, string context)
        {
            if (string.IsNullOrEmpty(wc.ToString()))
                WriteNone();

            rc = new ReadWriteContext.ReadContextByJson(wc.ToString());
            if (!rc.Exist("oper_id") && Program.oper != null)
            {
                wc.Append("oper_id", Program.oper.oper_id);
            }

            while (1 == 1)
            {
                System.Threading.Thread.Sleep(100);
                if (doing == false)
                {
                    try
                    {
                        doing = true;
                        string url = AppSetting.svr + par_url;
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                        req.KeepAlive = false;
                        req.Timeout = 5 * 60 * 1000;
                        req.ReadWriteTimeout = 5 * 60 * 1000;
                        req.CookieContainer = AppSetting.CookieContainer;
                        //
                        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(context);
                        req.Method = "POST";
                        Stream requestStream = req.GetRequestStream();
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                        requestStream.Close();
                        //
                        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                        res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
                        AppSetting.CookieContainer.Add(res.Cookies);
                        Stream stream = res.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        string str = reader.ReadToEnd();
                        stream.Close();
                        reader.Close();
                        //
                        doing = false;
                        //
                        returnJson = str;
                        rc = new ReadWriteContext.ReadContextByJson(returnJson);
                        return;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        doing = false;
                    }
                }
            }

        }
        public IReadContext GetReadContext()
        {
            if (rc == null)
                rc = new ReadContextByJson(returnJson);
            return rc;
        }

        public void Write(string key, string value)
        {
            wc.Append(key, value);
        }
        public void Write(string key, DateTime value)
        {
            wc.Append(key, value.Toyyyy_MM_dd_HH_mm_ss());
        }
        public void Write(string[] keys, string[] values)
        {
            wc.Append(keys, values);
        }
        public void Write(string key, int value)
        {
            wc.Append(key, value.ToString());
        }
        public void Write(string key, decimal value)
        {
            wc.Append(key, value.ToString());
        }
        public void Write(string key, DataTable dt)
        {
            wc.Append(key, dt);
        }
        public void Write(string key, string[] subkeys, string[] subvalues)
        {
            wc.Append(key, subkeys, subvalues);
        }
        public void Write(DataTable dt)
        {
            wc.Append("data", dt);
        }
        public void WriteTableStruct(DataTable dt)
        {
            wc.AppendTableStruct("data", dt);
        }
        public void Write<T>(Page<T> page)
        {
            Write("page_index", page.PageIndex);
            Write("page_size", page.PageSize);
        }
        public void Write<T>(T t)
        {
            var ts = typeof(T);
            var pis = ts.GetProperties();

            for (int j = 0; j < pis.Length; j++)
            {
                var pi = pis[j];
                string colName = pi.Name;
                object value = pi.GetValue(t, null);

                Write(colName, value == null ? null : value.ToString());
            }
        }
        public void Write<T>(string key, T t)
        {
            var ts = typeof(T);
            var pis = ts.GetProperties();

            DataTable dt = new DataTable();

            for (int j = 0; j < pis.Length; j++)
            {
                var pi = pis[j];
                string colName = pi.Name;

                dt.Columns.Add(colName);
            }

            DataRow dr = dt.NewRow();
            for (int j = 0; j < pis.Length; j++)
            {
                var pi = pis[j];
                string colName = pi.Name;

                dr[j] = pi.GetValue(t, null);
            }
            dt.Rows.Add(dr);

            Write(key, dt);
        }
        public void Write<T>(List<T> lis)
        {
            var ts = typeof(T);
            DataTable dt = new DataTable();
            if (ts.IsValueType || ts == typeof(string))
            {
                dt.Columns.Add("obj");

                foreach (T t in lis)
                {
                    dt.Rows.Add(t);
                }
            }
            else
            {
                var pis = ts.GetProperties();
                for (int j = 0; j < pis.Length; j++)
                {
                    var pi = pis[j];
                    string colName = pi.Name;

                    dt.Columns.Add(colName);
                }
                foreach (T t in lis)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < pis.Length; j++)
                    {
                        var pi = pis[j];
                        string colName = pi.Name;

                        dr[j] = pi.GetValue(t, null);
                    }
                    dt.Rows.Add(dr);
                }
            }

            Write(dt);
        }
        public void Write<T>(string key, List<T> lis)
        {
            var ts = typeof(T);
            DataTable dt = new DataTable();
            if (ts.IsValueType || ts == typeof(string))
            {
                dt.Columns.Add("obj");

                foreach (T t in lis)
                {
                    dt.Rows.Add(t);
                }
            }
            else
            {
                var pis = ts.GetProperties();
                for (int j = 0; j < pis.Length; j++)
                {
                    var pi = pis[j];
                    string colName = pi.Name;

                    dt.Columns.Add(colName);
                }
                foreach (T t in lis)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < pis.Length; j++)
                    {
                        var pi = pis[j];
                        string colName = pi.Name;

                        dr[j] = pi.GetValue(t, null);
                    }
                    dt.Rows.Add(dr);
                }
            }

            Write(key, dt);
        }
        public void Write<T1, T2>(Dictionary<T1, T2> dic)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key");
            dt.Columns.Add("value");

            foreach (T1 key in dic.Keys)
            {
                DataRow dr = dt.NewRow();
                dr["key"] = key;
                dr["value"] = dic[key];
                dt.Rows.Add(dr);
            }

            Write(dt);

        }
        public void Write<T>(Dictionary<string, T> dic)
        {
            var ts = typeof(T);
            var pis = ts.GetProperties();

            DataTable dt = new DataTable();
            dt.Columns.Add("key");
            for (int j = 0; j < pis.Length; j++)
            {
                var pi = pis[j];
                string colName = pi.Name;
                dt.Columns.Add(colName);
            }


            foreach (string key in dic.Keys)
            {
                DataRow dr = dt.NewRow();
                dr["key"] = key;
                for (int j = 0; j < pis.Length; j++)
                {
                    var pi = pis[j];
                    string colName = pi.Name;

                    var obj = pi.GetValue(dic[key], null);
                    dr[j + 1] = obj == null ? "" : obj.ToString(); ;
                }
                dt.Rows.Add(dr);
            }

            Write(dt);
        }
        public void WriteNone()
        {
            wc.Append("none", "");
            wc.Append("oper_id", Program.oper.oper_id);
        }

        public Dictionary<T1, T2> GetDic<T1, T2>()
        {
            Dictionary<T1, T2> dic = new Dictionary<T1, T2>();

            foreach (IReadContext r in ReadList("data"))
            {

                T1 t1 = (T1)Convert.ChangeType(Read(r, "key"), typeof(T1));
                T2 t2 = (T2)Convert.ChangeType(r.Read("value"), typeof(T2));

                T2 value;
                if (!dic.TryGetValue(t1, out value))
                    dic.Add(t1, t2);
            }

            return dic;
        }
        public Dictionary<T1, T2> GetDic<T1, T2>(string keys) where T2 : struct
        {
            Dictionary<T1, T2> dic = new Dictionary<T1, T2>();

            foreach (IReadContext r in ReadList(keys))
            {

                T1 t1 = (T1)Convert.ChangeType(Read(r, "key"), typeof(T1));
                T2 t2 = (T2)Convert.ChangeType(r.Read("value"), typeof(T2));

                T2 value;
                if (!dic.TryGetValue(t1, out value))
                    dic.Add(t1, t2);
            }

            return dic;
        }
        public Dictionary<T1, T2> GetDic<T1, T2>(string keys, string key) where T2 : new()
        {
            Dictionary<T1, T2> dic = new Dictionary<T1, T2>();

            foreach (IReadContext r in ReadList(keys))
            {

                T1 t1 = (T1)Convert.ChangeType(Read(r, key), typeof(T1));
                T2 t2 = GetObject<T2>(r);

                T2 value;
                if (!dic.TryGetValue(t1, out value))
                    dic.Add(t1, t2);
            }

            return dic;
        }
        public Dictionary<T1, List<T2>> GetDicList<T1, T2>()
        {
            Dictionary<T1, List<T2>> dic = new Dictionary<T1, List<T2>>();

            if ("dic".Equals(Read("DateType")))
            {
                List<string> keys = GetList<string>("Keys");
                foreach (var key in keys)
                {
                    List<T2> lis2 = GetList<T2>(key);
                    dic.Add((T1)ChangeType(key, typeof(T1)), lis2);
                }
            }
            else
            {
                throw new Exception("格式错误");
            }

            return dic;
        }
        public Dictionary<T1, T2> GetDicOfTable<T1, T2>(string dic_key) where T2 : new()
        {
            Dictionary<T1, T2> dic = new Dictionary<T1, T2>();

            DataTable tb = GetDataTable();

            int index = 0;
            foreach (DataRow dr in tb.Rows)
            {
                T1 t1 = (T1)Convert.ChangeType(dr[dic_key], typeof(T1));
                T2 t2 = GetTableObject<T2>(index);

                dic.Add(t1, t2);
                index++;
            }

            return dic;
        }
        public Dictionary<T1, T2> GetDicOfTable<T1, T2>(string dic_key, string key) where T2 : new()
        {
            Dictionary<T1, T2> dic = new Dictionary<T1, T2>();

            DataTable tb = GetDataTable(key);

            int index = 0;
            foreach (DataRow dr in tb.Rows)
            {
                T1 t1 = (T1)Convert.ChangeType(dr[dic_key], typeof(T1));
                T2 t2 = GetTableObject<T2>(index, key);

                dic.Add(t1, t2);
                index++;
            }

            return dic;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        public T GetObject<T>()
        {
            var t = typeof(T);
            var pis = t.GetProperties();
            T nm = (T)Activator.CreateInstance(t);
            for (int i = 0; i < pis.Length; i++)
            {
                var pi = pis[i];
                string colName = pi.Name;

                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        pi.SetValue(nm, Read(colName), null);
                        break;
                    case "System.Int16":
                        pi.SetValue(nm, Convert.ToInt16(Read(colName)), null);
                        break;
                    case "System.Int32":
                        pi.SetValue(nm, Conv.ToInt(Read(colName)), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, Convert.ToInt64(Read(colName)), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, Conv.ToDecimal(Read(colName)), null);
                        break;
                    case "System.Double":
                        pi.SetValue(nm, Convert.ToDouble(Read(colName)), null);
                        break;
                    case "System.Float":
                        pi.SetValue(nm, Convert.ToSingle(Read(colName)), null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":
                        pi.SetValue(nm, Conv.ToDateTime(Read(colName)), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, Conv.ToChar(Read(colName)), null);
                        break;
                        //case "System.Boolean":
                        //    if (row[colName] != DBNull.Value) pi.SetValue(r, (bool)row[colName], null);
                        //    break;
                        //case "System.Byte":
                        //    if (row[colName] != DBNull.Value) pi.SetValue(r, (byte)row[colName], null);
                        //    break;
                }
            }

            return nm;
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        public T GetObject<T>(IReadContext r)
        {
            var t = typeof(T);
            var pis = t.GetProperties();
            T nm = (T)Activator.CreateInstance(t);
            for (int i = 0; i < pis.Length; i++)
            {
                var pi = pis[i];
                string colName = pi.Name;

                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        pi.SetValue(nm, Read(r, colName), null);
                        break;
                    case "System.Int16":
                        pi.SetValue(nm, Convert.ToInt16(Read(r, colName)), null);
                        break;
                    case "System.Int32":
                        pi.SetValue(nm, Conv.ToInt(Read(r, colName)), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, Convert.ToInt64(Read(r, colName)), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, Conv.ToDecimal(Read(r, colName)), null);
                        break;
                    case "System.Double":
                        pi.SetValue(nm, Convert.ToDouble(Read(r, colName)), null);
                        break;
                    case "System.Float":
                        pi.SetValue(nm, Convert.ToSingle(Read(r, colName)), null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":
                        pi.SetValue(nm, Conv.ToDateTime(Read(r, colName)), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, Conv.ToChar(Read(r, colName)), null);
                        break;
                        //case "System.Boolean":
                        //    if (row[colName] != DBNull.Value) pi.SetValue(r, (bool)row[colName], null);
                        //    break;
                        //case "System.Byte":
                        //    if (row[colName] != DBNull.Value) pi.SetValue(r, (byte)row[colName], null);
                        //    break;
                }
            }

            return nm;
        }
        public T GetObject<T>(string key)
        {
            var t = typeof(T);
            var pis = t.GetProperties();
            T nm = (T)Activator.CreateInstance(t);
            for (int i = 0; i < pis.Length; i++)
            {
                var pi = pis[i];
                string colName = pi.Name;

                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        pi.SetValue(nm, Read(key, colName), null);
                        break;
                    case "System.Int16":
                        pi.SetValue(nm, Convert.ToInt16(Read(key, colName)), null);
                        break;
                    case "System.Int32":
                        pi.SetValue(nm, Conv.ToInt(Read(key, colName)), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, Convert.ToInt64(Read(key, colName)), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, Conv.ToDecimal(Read(key, colName)), null);
                        break;
                    case "System.Double":
                        pi.SetValue(nm, Convert.ToDouble(Read(key, colName)), null);
                        break;
                    case "System.Float":
                        pi.SetValue(nm, Convert.ToSingle(Read(key, colName)), null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":
                        pi.SetValue(nm, Conv.ToDateTime(Read(key, colName)), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, Conv.ToChar(Read(key, colName)), null);
                        break;
                        //case "System.Boolean":
                        //    if (row[colName] != DBNull.Value) pi.SetValue(r, (bool)row[colName], null);
                        //    break;
                        //case "System.Byte":
                        //    if (row[colName] != DBNull.Value) pi.SetValue(r, (byte)row[colName], null);
                        //    break;
                }
            }

            return nm;
        }
        /// <summary>
        /// 在datatable 取出 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetTableObject<T>()
        {
            T t;
            foreach (IReadContext r in ReadList("data"))
            {
                t = GetObject<T>(r);
                return t;
            }
            return default(T);
        }
        public T GetTableObject<T>(int index)
        {
            IReadContext r = ReadList("data")[index];

            T t = GetObject<T>(r);
            return t;
        }
        public T GetTableObject<T>(int index, string key)
        {
            IReadContext r = ReadList(key)[index];

            T t = GetObject<T>(r);
            return t;
        }

        public DataTable GetDataTable(string tb_key)
        {
            DataTable dt = new DataTable();
            string data_json = Read(tb_key);
            IReadContext rc2 = new ReadContextByJson(data_json);
            if (!rc2.IsArray())
            {
                Dictionary<string, Type> col_dic = new Dictionary<string, Type>();

                foreach (IReadContext r in rc2.ReadList("Columns"))
                {
                    DataColumn dc = new DataColumn
                    {
                        ColumnName = r.Read("Name"),
                        DataType = Type.GetType(r.Read("DataType"))
                    };

                    dt.Columns.Add(dc);
                    col_dic.Add(dc.ColumnName, dc.DataType);
                }

                foreach (IReadContext r in rc2.ReadList("Rows"))
                {
                    Dictionary<string, object> dic = r.ToDictionary();

                    DataRow row = dt.NewRow();
                    foreach (string key in dic.Keys)
                    {
                        row[key] = ChangeType(dic[key], col_dic[key]);
                    }
                    dt.Rows.Add(row);
                }
            }
            else
            {
                foreach (IReadContext r in ReadList(tb_key))
                {
                    Dictionary<string, object> dic = r.ToDictionary();

                    if (dt.Columns.Count < 1)
                    {
                        foreach (string key in dic.Keys)
                        {
                            dt.Columns.Add(key);
                        }
                    }

                    DataRow dr = dt.NewRow();
                    foreach (string key in dic.Keys)
                    {
                        int i = dt.Columns.IndexOf(key);
                        dr[i] = dic[key];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public DataTable GetDataTable()
        {
            return GetDataTable("data");
        }

        /// <summary>
        /// 获取List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>()
        {
            return GetList<T>("data");
        }
        /// <summary>
        /// 获取List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>(string key)
        {
            var t = typeof(T);
            List<T> nmlis = new List<T>();

            if (t.IsValueType || t == typeof(string))
            {
                foreach (IReadContext r in ReadList(key))
                {
                    string value = r.Read("value");
                    nmlis.Add((T)ChangeType(value, t));
                }
            }
            else
            {
                foreach (IReadContext r in ReadList(key))
                {
                    T nm = GetObject<T>(r);//创建对象

                    nmlis.Add(nm);
                }
            }

            return nmlis;
        }

        /// <summary>
        /// 获取 dic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Dictionary<string, T> GetDic<T>()
        {
            var t = typeof(T);
            var pis = t.GetProperties();

            Dictionary<string, T> dic = new Dictionary<string, T>();

            foreach (IReadContext r in ReadList("data"))
            {
                T nm = GetObject<T>(r);//创建对象

                T value;
                if (!dic.TryGetValue(Read(r, "key"), out value))
                    dic.Add(Read(r, "key"), nm);
            }

            return dic;
        }
        /// <summary>
        /// 获取 dic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Dictionary<string, T> GetDic<T>(string keys)
        {
            var t = typeof(T);
            var pis = t.GetProperties();

            Dictionary<string, T> dic = new Dictionary<string, T>();

            foreach (IReadContext r in ReadList(keys))
            {
                T nm = GetObject<T>(r);//创建对象

                T value;
                if (!dic.TryGetValue(Read(r, "key"), out value))
                    dic.Add(Read(r, "key"), nm);
            }

            return dic;
        }

        public byte[] WriteByte<T>(List<T> lis)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, lis);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }

        public byte[] WriteByte<T>(Dictionary<int, List<T>> lis)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, lis);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }

        public string Read(string key)
        {
            return rc.Read(key);
        }
        public List<IReadContext> ReadList(string key)
        {
            return rc.ReadList(key);
        }
        public string Read(IReadContext r, string key)
        {
            return r.Read(key);
        }
        public string Read(string r_key, string key)
        {
            var lis = ReadList(r_key);
            if (lis == null || lis.Count < 1) return "";
            return ReadList(r_key)[0].Read(key);
        }
        public int ReadToInt(string key)
        {
            return Conv.ToInt(rc.Read(key));
        }
        public int ReadToInt(IReadContext r, string key)
        {
            return Conv.ToInt(r.Read(key));
        }
        public long ReadToLong(IReadContext r, string key)
        {
            return Convert.ToInt64(r.Read(key));
        }
        public string ReadMessage()
        {
            return rc.Read("errMsg");
        }
        public string ReadDetailMessage()
        {
            return rc.Read("detailErrMsg");
        }
        public string ReadFlag()
        {
            return rc.Read("errId");
        }
        public bool ReadSuccess()
        {
            return ReadFlag().Equals("0");
        }
        public bool WhetherSuccess()
        {
            bool flag = ReadSuccess();
            if (!flag)
            {
                throw new Exception(ReadMessage(), new Exception(ReadDetailMessage()));
            }
            return flag;
        }
        public bool ReadLogin()
        {
            try
            {
                return ReadFlag().Equals("");
            }
            catch (Exception)
            {
                return true;
            }

        }
        public string ReadResult()
        {
            return rc.Read("result");
        }
        public T ReadResult<T>()
        {
            object result = rc.Read("result");
            T t = (T)Convert.ChangeType(result, typeof(T));
            return t;
        }
        public Page<T> ReadPage<T>(Page<T> page)
        {
            page.Tb = GetDataTable();
            page.PageCount = ReadToInt("total_count");
            return page;
        }

        private object ChangeType(object data, Type type)
        {
            object obj = null;
            switch (type.FullName)
            {
                case "System.String":
                    obj = data.ToString();
                    break;
                case "System.Int16":
                    obj = ToInt16(data);
                    break;
                case "System.Int32":
                    obj = ToInt32(data);
                    break;
                case "System.Int64":
                    obj = ToInt64(data);
                    break;
                case "System.Decimal":
                    obj = ToDecimal(data);
                    break;
                case "System.Double":
                    obj = ToDouble(data);
                    break;
                case "System.Float":
                    obj = ToSingle(data);
                    break;
                case "System.DateTime":
                    obj = ToDateTime(data);
                    break;
                case "System.Char":
                    obj = ToChar(data);
                    break;
            }

            return obj;
        }

        /// <summary>
        /// 把字节数组反序列化成对象
        /// </summary>
        public object DeserializeObject(byte[] bytes)
        {
            object obj = null;
            if (bytes == null)
                return obj;
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Binder = new UBinder();

            try
            {
                obj = formatter.Deserialize(ms);
            }
            catch (Exception ex)
            {

                throw;
            }
            ms.Close();
            return obj;
        }


        public Int16 ToInt16(object obj)
        {
            Int16 result = 0;
            if (obj == null)
            {
                result = 0;
            }
            else
            {
                if (!Int16.TryParse(obj.ToString(), out result))
                {
                    decimal dec = obj.ToDecimal();

                    result = Decimal.ToInt16(dec);
                }
            }
            return result;
        }
        public Int32 ToInt32(object obj)
        {
            Int32 result = 0;

            if (obj == null)
            {
                result = 0;
            }
            else
            {
                if (!Int32.TryParse(obj.ToString(), out result))
                {
                    decimal dec = obj.ToDecimal();

                    result = Decimal.ToInt32(dec);
                }
            }

            return result;
        }
        public Int64 ToInt64(object obj)
        {
            Int64 result = 0;

            if (obj == null)
            {
                result = 0;
            }
            else
            {
                if (!Int64.TryParse(obj.ToString(), out result))
                {
                    decimal dec = obj.ToDecimal();

                    result = dec.ToInt64();
                }
            }


            return result;
        }
        public decimal ToDecimal(object obj)
        {
            decimal result = 0;

            if (obj == null)
            {
                result = 0;
            }
            else
            {
                decimal.TryParse(obj.ToString(), out result);
            }

            return result;
        }
        public double ToDouble(object obj)
        {
            double value = 0.0;
            if (obj == null)
            {
                value = 0.0;
            }
            else
            {
                double.TryParse(obj.ToString(), out value);
            }
            return value;
        }
        public float ToSingle(object obj)
        {
            float value = 0;
            if (obj == null)
            {
                value = 0;
            }
            else
            {
                float.TryParse(obj.ToString(), out value);
            }
            return value;
        }
        public char ToChar(object obj)
        {
            char result = ' ';

            if (obj == null)
            {
                result = ' ';
            }
            else
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    result = ' ';
                }
                else
                {
                    result = Convert.ToChar(obj.ToString());
                }
            }

            return result;
        }
        public bool ToBool(object obj)
        {
            bool flag = false;

            if (obj == null)
            {
                flag = false;
            }
            else
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    flag = false;
                }
                else
                {
                    bool.TryParse(obj.ToString(), out flag);
                }
            }

            return flag;
        }
        public DateTime ToDateTime(object obj)
        {
            DateTime time = DateTime.MinValue;
            DateTime minTime = new DateTime(1950, 1, 1);
            DateTime maxTime = new DateTime(2500, 1, 1);

            if (obj == null)
            {
                time = minTime;
            }
            else if (obj != null && string.IsNullOrEmpty(obj.ToString()))
            {
                time = minTime;
            }
            else
            {
                if (!DateTime.TryParse(obj.ToString(), out time))
                {
                    time = Convert.ToDateTime(obj.ToString());
                }

                if (time < minTime)
                {
                    time = minTime;
                }
                if (time > maxTime)
                {
                    time = maxTime;
                }
            }

            return time;
        }

    }
}
