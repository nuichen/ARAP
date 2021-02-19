﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using ReadWriteContext;
using System.Runtime.Serialization;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace IvyTran.Helper
{
    public class Request
    {
        private IWriteContext wc;
        private IReadContext rc;
        //private static CookieContainer cookie = new CookieContainer();

        public Request()
        {
            if (null == wc)
            {
                wc = new WriteContextByJson();
            }
        }

        private string returnJson = "";//返回Json
        private static bool doing = false;

        private static CookieContainer cookie = new CookieContainer();

        public void request(string par_url)
        {
            int num = 1;
            while (1 == 1)
            {
                System.Threading.Thread.Sleep(100);
                if (doing == false)
                {
                    try
                    {
                        doing = true;
                        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(par_url);
                        req.KeepAlive = false;
                        req.Timeout = 10 * 1000;
                        req.ReadWriteTimeout = 10 * 1000;
                        req.CookieContainer = cookie;
                        //
                        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(wc.ToString());

                        req.Method = "POST";
                        Stream requestStream = req.GetRequestStream();
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                        requestStream.Close();
                        //
                        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                        res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
                        cookie = new CookieContainer();
                        cookie.Add(res.Cookies);
                        Stream stream = res.GetResponseStream();
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
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
                        if (num == 3)
                            throw;
                    }
                    finally
                    {
                        doing = false;
                        num++;
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
            wc.Append(key, value.ToString());
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
            var pis = ts.GetProperties();

            DataTable dt = new DataTable();

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
            lis = null;
            GC.Collect();
            Write(dt);
        }
        public void Write<T>(string key, List<T> lis)
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
            lis = null;
            GC.Collect();
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
                        pi.SetValue(nm, Convert.ToInt32(Read(colName)), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, Convert.ToInt64(Read(colName)), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, Convert.ToDecimal(Read(colName)), null);
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
                        pi.SetValue(nm, Convert.ToDateTime(Read(colName)), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, Convert.ToChar(Read(colName)), null);
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
                        pi.SetValue(nm, Convert.ToInt32(Read(r, colName)), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, Convert.ToInt64(Read(r, colName)), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, Convert.ToDecimal(Read(r, colName)), null);
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
                        pi.SetValue(nm, Convert.ToDateTime(Read(r, colName)), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, Convert.ToChar(Read(r, colName)), null);
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
                        pi.SetValue(nm, Convert.ToInt32(Read(key, colName)), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, Convert.ToInt64(Read(key, colName)), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, Convert.ToDecimal(Read(key, colName)), null);
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
                        pi.SetValue(nm, Convert.ToDateTime(Read(key, colName)), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, Convert.ToChar(Read(key, colName)), null);
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
        /// <summary>
        /// 模型 获取DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable GetDataTable(DataTable dt)
        {
            List<string> colName = new List<string>();


            foreach (IReadContext r in ReadList("data"))
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string key = dt.Columns[i].ColumnName;
                    dr[i] = Read(r, key);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public DataTable GetDataTable(string tb_key)
        {
            DataTable dt = new DataTable();

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

            return dt;
        }
        public DataTable GetDataTable(string tb_key, List<string> col_lis)
        {
            DataTable dt = new DataTable();

            foreach (IReadContext r in ReadList(tb_key))
            {
                Dictionary<string, object> dic = r.ToDictionary();

                if (dt.Columns.Count < 1)
                {
                    foreach (string key in dic.Keys)
                    {
                        if (col_lis.Contains(key, StringComparer.OrdinalIgnoreCase))
                        {
                            dt.Columns.Add(key);
                        }
                    }
                }

                DataRow dr = dt.NewRow();
                foreach (string key in dic.Keys)
                {
                    if (col_lis.Contains(key, StringComparer.OrdinalIgnoreCase))
                    {
                        int i = dt.Columns.IndexOf(key);
                        dr[i] = dic[key];
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        public DataTable GetDataTable(string tb_key, List<string> col_lis, string key_col)
        {
            DataTable dt = new DataTable();

            foreach (IReadContext r in ReadList(tb_key))
            {
                Dictionary<string, object> dic = r.ToDictionary();

                if (dt.Columns.Count < 1)
                {
                    foreach (string key in dic.Keys)
                    {
                        if (col_lis.Contains(key, StringComparer.OrdinalIgnoreCase))
                        {
                            dt.Columns.Add(key);
                        }
                    }
                }

                DataRow dr = dt.NewRow();
                foreach (string key in dic.Keys)
                {
                    if (col_lis.Contains(key, StringComparer.OrdinalIgnoreCase))
                    {
                        if (key.Equals(key_col))
                        {
                            if (dt.Select(key_col + "='" + dic[key] + "'").Count() > 0)
                            {
                                dr = null;
                                break;
                            }
                        }

                        int i = dt.Columns.IndexOf(key);
                        dr[i] = dic[key];
                    }
                }
                if (dr != null)
                    dt.Rows.Add(dr);
            }

            return dt;
        }
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();

            foreach (IReadContext r in ReadList("data"))
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

            return dt;
        }

        /// <summary>
        /// 获取List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>()
        {
            var t = typeof(T);
            var pis = t.GetProperties();
            List<T> nmlis = new List<T>();

            foreach (IReadContext r in ReadList("data"))
            {
                T nm = GetObject<T>(r);//创建对象

                nmlis.Add(nm);
            }

            return nmlis;
        }
        /// <summary>
        /// 获取List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetList<T>(string key)
        {
            var t = typeof(T);
            var pis = t.GetProperties();
            List<T> nmlis = new List<T>();

            foreach (IReadContext r in ReadList(key))
            {
                T nm = GetObject<T>(r);//创建对象

                nmlis.Add(nm);
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
            return Convert.ToInt32(rc.Read(key));
        }
        public int ReadToInt(IReadContext r, string key)
        {
            return Convert.ToInt32(r.Read(key));
        }
        public long ReadToLong(IReadContext r, string key)
        {
            return Convert.ToInt64(r.Read(key));
        }
        public string ReadMessage()
        {
            return rc.Read("msg");
        }
        public string ReadFlag()
        {
            return rc.Read("recode");
        }
        public bool ReadSuccess()
        {
            return ReadFlag().Equals("0");
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

            obj = formatter.Deserialize(ms);
            ms.Close();
            return obj;
        }

        public class UBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Assembly ass = Assembly.GetExecutingAssembly();
                Type t = ass.GetType(typeName);
                return t;
            }
        }

    }
}
