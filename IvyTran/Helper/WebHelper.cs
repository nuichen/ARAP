using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReadWriteContext;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Collections;

namespace IvyTran.Helper
{
    public class WebHelper
    {
        public WebHelper() { }
        public WebHelper(IReadContext ReadContext)
        {
            this.ReadContext = ReadContext;
            kv = ReadContext.ToDictionary();
        }
        public WebHelper(string json)
        {
            ReadContext = new ReadWriteContext.ReadContextByJson(json);
            kv = ReadContext.ToDictionary();
        }
        public Dictionary<string, object> kv = new Dictionary<string, object>();
        public IReadContext ReadContext = null;
        private IWriteContext WriteContext = new WriteContextByJson();
        public IWriteContext CreateWriteContext()
        {
            if (WriteContext == null)
            {
                WriteContext = new WriteContextByJson();
            }
            return WriteContext;
        }

        /// <summary>
        /// 写入json
        /// </summary>
        /// <param name="flag">标记</param>
        /// <param name="message">信息</param>
        public void Write(string flag, string message)
        {
            WriteContext.Append(flag, message);
        }
        public void Write(string Key, DataTable dt)
        {
            WriteContext.Append(Key, dt);
        }
        public void Write(string Key, int value)
        {
            WriteContext.Append(Key, value.ToString());
        }
        public void Write(string Key, object value)
        {
            Type t = value.GetType();
            if (t.IsValueType || t == typeof(string))
            {
                WriteContext.Append(Key, value.ToString());
            }
            else
            {
                if (value is DataTable)
                {
                    Write(Key, (DataTable)value);
                }
                else
                {
                    var pis = t.GetProperties();

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

                        dr[j] = pi.GetValue(value, null);
                    }
                    dt.Rows.Add(dr);

                    Write(Key, dt);
                }
            }
        }
        public void Write(DataTable dt)
        {
            WriteContext.Append("data", dt);
        }
        public void WriteTableStruct(DataTable dt)
        {
            WriteContext.AppendTableStruct("data", dt);
        }
        public void WriteTableStruct(string key, DataTable dt)
        {
            WriteContext.AppendTableStruct(key, dt);
        }
        public void Write(string[] keys, string[] values)
        {
            WriteContext.Append("data", keys, values);
        }
        public void Write(string key, string[] keys, string[] values)
        {
            WriteContext.Append(key, keys, values);
        }
        public void WriteObject(string Key, string Value)
        {
            WriteContext.Append(Key, Value);
        }
        public void WriteObject(string Key, int Value)
        {
            WriteContext.Append(Key, Value.ToString());
        }
        public void WriteObject(string Key, object Value)
        {
            if (null == Value) WriteContext.Append(Key, "");
            else WriteContext.Append(Key, Value.ToString());
        }
        public void WriteResult(string value)
        {
            if (null == value) WriteContext.Append("result", "");
            else WriteContext.Append("result", value);
        }
        public void WriteResult(object value)
        {
            if (null == value) WriteContext.Append("result", "");
            else WriteContext.Append("result", value.ToString());
        }
        public void WriteSuccess(string message)
        {
            WriteContext.Append("errId", "0");
            WriteContext.Append("errMsg", message);
        }
        public void WriteSuccess()
        {
            WriteContext.Append("errId", "0");
            WriteContext.Append("errMsg", "成功");
        }
        public void WriteReturnMesg(int errId, string errMsg)
        {
            WriteContext.Append("errId", errId.ToString());
            WriteContext.Append("errMsg", errMsg);
        }
        public void WriteInvalidParameters()
        {
            WriteContext.Append("errId", "-4");
            WriteContext.Append("errMsg", "invalid parameters");
        }
        public void WriteInvalidParameters(string message)
        {
            WriteContext.Append("errId", "-4");
            WriteContext.Append("errMsg", message);
        }
        public void WriteServierTime()
        {
            WriteContext.Append("errId", "-3");
            WriteContext.Append("errMsg", "server time error");
        }
        public void WriteError(string msg)
        {
            WriteContext.Append("errId", "-1");
            WriteContext.Append("errMsg", msg);
        }
        public void WriteError(Exception ex)
        {
            if (ex.InnerException != null)
            {
                LogHelper.writeLog("", ex.InnerException.ToString());

                WriteContext.Append("errId", "-1");
                if (ex.InnerException.InnerException != null)
                {
                    WriteContext.Append("errMsg", ex.InnerException.Message);
                    if (ex.InnerException != null)
                    {
                        WriteContext.Append("detailErrMsg", ex.InnerException.InnerException.Message);
                    }
                }
                else
                {
                    WriteContext.Append("errMsg", ex.InnerException.Message);
                }

            }
            else
            {
                LogHelper.writeLog("", ex.ToString());

                if (ex is ExceptionBase e)
                {
                    WriteContext.Append("errId", e.errId.ToString());
                    WriteContext.Append("errMsg", e.Message);
                }
                else
                {
                    WriteContext.Append("errId", "-1");
                    WriteContext.Append("errMsg", ex.Message);
                }
            }
        }
        public void WriteStrings(string[] keys, string[] values)
        {
            WriteContext.Append(keys, values);
        }
        public void Write<T>(T t)
        {
            var ts = typeof(T);
            var pis = ts.GetProperties();

            for (int j = 0; j < pis.Length; j++)
            {
                var pi = pis[j];
                string colName = pi.Name;
                object nm = pi.GetValue(t, null);
                string value = nm == null ? "" : nm.ToString();

                WriteObject(colName, value);

            }
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
                    object nm = pi.GetValue(t, null);
                    dr[j] = nm == null ? null : nm.ToString();
                }
                dt.Rows.Add(dr);
            }

            Write(dt);

        }
        public void Write<T>(string key, List<T> lis)
        {
            var ts = typeof(T);
            DataTable dt = new DataTable();
            if (ts.IsValueType || ts == typeof(string))
            {
                dt.Columns.Add("value");

                foreach (T t in lis)
                {
                    dt.Rows.Add(t.ToString());
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
                        object nm = pi.GetValue(t, null);
                        dr[j] = nm?.ToString();
                    }
                    dt.Rows.Add(dr);
                }
            }

            Write(key, dt);

        }
        public void Write<T>(string keys, Dictionary<string, T> dic)
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

                    var nm = pi.GetValue(dic[key], null);
                    dr[j + 1] = nm == null ? "" : nm.ToString(); ;
                }
                dt.Rows.Add(dr);
            }

            Write(keys, dt);

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

                    var nm = pi.GetValue(dic[key], null);
                    dr[j + 1] = nm == null ? "" : nm.ToString(); ;
                }
                dt.Rows.Add(dr);
            }

            Write(dt);

        }
        public void Write<T1, T2>(string keys, Dictionary<T1, T2> dic)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key", typeof(T1));
            dt.Columns.Add("value", typeof(T2));

            foreach (T1 key in dic.Keys)
            {
                DataRow dr = dt.NewRow();
                dr["key"] = key;
                dr["value"] = dic[key];
                dt.Rows.Add(dr);
            }

            Write(keys, dt);

        }
        public void Write<T1, T2>(Dictionary<T1, T2> dic)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key", typeof(T1));
            dt.Columns.Add("value", typeof(T2));

            foreach (T1 key in dic.Keys)
            {
                DataRow dr = dt.NewRow();
                dr["key"] = key;
                dr["value"] = dic[key];
                dt.Rows.Add(dr);
            }

            Write(dt);

        }
        public void Write<T1, T2>(Dictionary<T1, List<T2>> dic)
        {
            Write("DateType", "dic");
            Type t1 = typeof(T1);
            Write("KeyType", t1.FullName);

            List<string> keys = new List<string>();
            foreach (var k in dic.Keys)
            {
                if (t1 == typeof(DateTime))
                {
                    keys.Add(k.ToDateTime().Toyyyy_MM_dd_HH_mm_ss());
                }
                else
                {
                    keys.Add(k.ToString());
                }
            }

            Write("Keys", keys);

            foreach (var key in dic)
            {
                if (t1 == typeof(DateTime))
                {
                    Write(key.Key.ToDateTime().Toyyyy_MM_dd_HH_mm_ss(), key.Value);
                }
                else
                {
                    Write(key.Key.ToString(), key.Value);
                }

            }
        }

        public DataTable GetDataTable()
        {
            return GetDataTable("data");
        }
        public DataTable GetDataTable(string cls_key)
        {
            DataTable dt = new DataTable();
            string data_json = Read(cls_key);
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
                foreach (IReadContext r in ReadList(cls_key))
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

        public Dictionary<string, T> GetDic<T>()
        {
            var t = typeof(T);
            var pis = t.GetProperties();
            Dictionary<string, T> dic = new Dictionary<string, T>();

            foreach (IReadContext r in ReadList("data"))
            {
                string key = r.Read("key");
                T nm = GetObject<T>(r);//创建对象

                dic.Add(key, nm);
            }

            return dic;
        }
        public Dictionary<T1, T2> GetDic<T1, T2>()
        {
            Dictionary<T1, T2> dic = new Dictionary<T1, T2>();

            foreach (IReadContext r in ReadList("data"))
            {
                T1 t1 = (T1)(object)r.Read("key");

                T2 t2 = (T2)(object)r.Read("value");
                dic.Add(t1, t2);
            }

            return dic;
        }
        public Dictionary<object, object> GetDic(Type t1, Type t2, string key)
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();

            foreach (IReadContext r in ReadList(key))
            {
                object o1 = r.Read("key");
                object o2 = new object();
                if (t2.IsValueType || t2 == typeof(string))
                {
                    o2 = r.Read("value");
                }
                else
                {
                    o2 = GetObjectClss(t2, r);
                }
                dic.Add(o1, o2);
            }
            return dic;
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
            List<T> nmlis = new List<T>();

            if (t.IsValueType || t == typeof(string))
            {
                foreach (IReadContext r in ReadList(key))
                {
                    T nm = (T)ChangeType(r.Read("obj"), t);
                    nmlis.Add(nm);
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
        public List<object> GetList(Type t, string key)
        {
            List<object> obj_lis = new List<object>();

            foreach (IReadContext r in ReadList(key))
            {
                object nm = GetObjectClss(t, r);//创建对象
                obj_lis.Add(nm);
            }

            return obj_lis;
        }
        public List<T> GetList<T>(IReadContext read, string key)
        {
            var t = typeof(T);
            List<T> nmlis = new List<T>();

            foreach (IReadContext r in read.ReadList(key))
            {
                T nm = GetObject<T>(r);//创建对象

                nmlis.Add(nm);
            }

            return nmlis;
        }
        /// <summary>
        /// 返回json
        /// </summary>
        /// <returns></returns>
        public string NmJson()
        {
            return WriteContext.ToString();
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
            if (t.IsValueType || t == typeof(string))
            {
                string colName = "obj";
                switch (t.FullName)
                {
                    case "System.String":
                        nm = (T)Convert.ChangeType(Read(colName), typeof(T));
                        break;
                    case "System.Int16":
                        nm = (T)Convert.ChangeType(Read(colName).ToInt16(), typeof(T));
                        break;
                    case "System.Int32":
                        nm = (T)Convert.ChangeType(Read(colName).ToInt32(), typeof(T));
                        break;
                    case "System.Int64":
                        nm = (T)Convert.ChangeType(Read(colName).ToInt64(), typeof(T));
                        break;
                    case "System.Decimal":
                        nm = (T)Convert.ChangeType(Read(colName).ToDecimal(), typeof(T));
                        break;
                    case "System.Double":
                        nm = (T)Convert.ChangeType(Read(colName).ToDouble(), typeof(T));
                        break;
                    case "System.Float":
                        nm = (T)Convert.ChangeType(Read(colName).ToSingle(), typeof(T));
                        break;
                    case "System.DateTime":
                        nm = (T)Convert.ChangeType(Read(colName).ToDateTime(), typeof(T));
                        break;
                    case "System.Char":
                        nm = (T)Convert.ChangeType(Read(colName).ToChar(), typeof(T));
                        break;
                }
            }
            else
            {
                foreach (var pi in pis)
                {
                    string colName = pi.Name;
                    switch (pi.PropertyType.FullName)
                    {
                        case "System.String":
                            pi.SetValue(nm, Read(colName), null);
                            break;
                        case "System.Int16":
                            pi.SetValue(nm, Read(colName).ToInt16(), null);
                            break;
                        case "System.Int32":
                            pi.SetValue(nm, Read(colName).ToInt32(), null);
                            break;
                        case "System.Int64":
                            pi.SetValue(nm, Read(colName).ToInt64(), null);
                            break;
                        case "System.Decimal":
                            pi.SetValue(nm, Read(colName).ToDecimal(), null);
                            break;
                        case "System.Double":
                            pi.SetValue(nm, Read(colName).ToDouble(), null);
                            break;
                        case "System.Float":
                            pi.SetValue(nm, Read(colName).ToSingle(), null);
                            break;
                        case "System.DateTime":
                            pi.SetValue(nm, Read(colName).ToDateTime(), null);
                            break;
                        case "System.Char":
                            pi.SetValue(nm, Read(colName).ToChar(), null);
                            break;
                    }
                }
            }

            return nm;
        }
        public T GetObject<T>(string key)
        {
            var t = typeof(T);
            var pis = t.GetProperties();
            T nm = (T)Activator.CreateInstance(t);
            if (t.IsValueType || t == typeof(string))
            {
                string colName = "obj";
                switch (t.FullName)
                {
                    case "System.String":
                        nm = (T)Convert.ChangeType(Read(key, colName), typeof(T));
                        break;
                    case "System.Int16":
                        nm = (T)Convert.ChangeType(Read(key, colName).ToInt16(), typeof(T));
                        break;
                    case "System.Int32":
                        nm = (T)Convert.ChangeType(Read(key, colName).ToInt32(), typeof(T));
                        break;
                    case "System.Int64":
                        nm = (T)Convert.ChangeType(Read(key, colName).ToInt64(), typeof(T));
                        break;
                    case "System.Decimal":
                        nm = (T)Convert.ChangeType(Read(key, colName).ToDecimal(), typeof(T));
                        break;
                    case "System.Double":
                        nm = (T)Convert.ChangeType(Read(key, colName).ToDouble(), typeof(T));
                        break;
                    case "System.Float":
                        nm = (T)Convert.ChangeType(Read(key, colName).ToSingle(), typeof(T));
                        break;
                    case "System.DateTime":
                        nm = (T)Convert.ChangeType(Read(key, colName).ToDateTime(), typeof(T));
                        break;
                    case "System.Char":
                        nm = (T)Convert.ChangeType(Read(key, colName).ToChar(), typeof(T));
                        break;
                }
            }
            else
            {
                foreach (var pi in pis)
                {
                    string colName = pi.Name;
                    switch (pi.PropertyType.FullName)
                    {
                        case "System.String":
                            pi.SetValue(nm, Read(key, colName), null);
                            break;
                        case "System.Int16":
                            pi.SetValue(nm, Read(key, colName).ToInt16(), null);
                            break;
                        case "System.Int32":
                            pi.SetValue(nm, Read(key, colName).ToInt32(), null);
                            break;
                        case "System.Int64":
                            pi.SetValue(nm, Read(key, colName).ToInt64(), null);
                            break;
                        case "System.Decimal":
                            pi.SetValue(nm, Read(key, colName).ToDecimal(), null);
                            break;
                        case "System.Double":
                            pi.SetValue(nm, Read(key, colName).ToDouble(), null);
                            break;
                        case "System.Float":
                            pi.SetValue(nm, Read(key, colName).ToSingle(), null);
                            break;
                        case "System.DateTime":
                            pi.SetValue(nm, Read(key, colName).ToDateTime(), null);
                            break;
                        case "System.Char":
                            pi.SetValue(nm, Read(key, colName).ToChar(), null);
                            break;
                    }
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
            T nm = default(T);
            if (t.IsValueType || t == typeof(string))
            {
                string colName = "obj";
                switch (t.FullName)
                {
                    case "System.String":
                        nm = (T)Convert.ChangeType(Read(r, colName), typeof(T));
                        break;
                    case "System.Int16":
                        nm = (T)Convert.ChangeType(Read(r, colName).ToInt16(), typeof(T));
                        break;
                    case "System.Int32":
                        nm = (T)Convert.ChangeType(Read(r, colName).ToInt32(), typeof(T));
                        break;
                    case "System.Int64":
                        nm = (T)Convert.ChangeType(Read(r, colName).ToInt64(), typeof(T));
                        break;
                    case "System.Decimal":
                        nm = (T)Convert.ChangeType(Read(r, colName).ToDecimal(), typeof(T));
                        break;
                    case "System.Double":
                        nm = (T)Convert.ChangeType(Read(r, colName).ToDouble(), typeof(T));
                        break;
                    case "System.Float":
                        nm = (T)Convert.ChangeType(Read(r, colName).ToSingle(), typeof(T));
                        break;
                    case "System.DateTime":
                        nm = (T)Convert.ChangeType(Read(r, colName).ToDateTime(), typeof(T));
                        break;
                    case "System.Char":
                        nm = (T)Convert.ChangeType(Read(r, colName).ToChar(), typeof(T));
                        break;
                }
            }
            else
            {
                nm = (T)Activator.CreateInstance(t);
                foreach (var pi in pis)
                {
                    string colName = pi.Name;
                    switch (pi.PropertyType.FullName)
                    {
                        case "System.String":
                            pi.SetValue(nm, Read(r, colName), null);
                            break;
                        case "System.Int16":
                            pi.SetValue(nm, Read(r, colName).ToInt16(), null);
                            break;
                        case "System.Int32":
                            pi.SetValue(nm, Read(r, colName).ToInt32(), null);
                            break;
                        case "System.Int64":
                            pi.SetValue(nm, Read(r, colName).ToInt64(), null);
                            break;
                        case "System.Decimal":
                            pi.SetValue(nm, Read(r, colName).ToDecimal(), null);
                            break;
                        case "System.Double":
                            pi.SetValue(nm, Read(r, colName).ToDouble(), null);
                            break;
                        case "System.Float":
                            pi.SetValue(nm, Read(r, colName).ToSingle(), null);
                            break;
                        case "System.DateTime":
                            pi.SetValue(nm, Read(r, colName).ToDateTime(), null);
                            break;
                        case "System.Char":
                            pi.SetValue(nm, Read(r, colName).ToChar(), null);
                            break;
                    }
                }
            }



            return nm;
        }

        public string Read(string key)
        {
            return this.ReadContext.Read(key);
        }
        public List<IReadContext> ReadList(string key)
        {
            return ReadContext.ReadList(key);
        }
        public string Read(IReadContext r, string key)
        {
            return r.Read(key);
        }
        public string Read(string r_key, string key)
        {
            return ReadList(r_key)[0].Read(key);
        }
        /// <summary>
        /// 把字节数组反序列化成对象
        /// </summary>
        public object DeserializeObject(byte[] bytes)
        {
            object nm = null;
            if (bytes == null)
                return nm;
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            nm = formatter.Deserialize(ms);
            ms.Close();
            return nm;
        }

        /// <summary>
        /// 是否包含某些键
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keys"></param>
        public bool ExistsKeys(params string[] keys)
        {
            for (var i = 0; i < keys.Length; i++)
            {
                if (!kv.Keys.Contains(keys[i], StringComparer.OrdinalIgnoreCase))
                {
                    LogHelper.writeLog("ExistsKeys(2)", "错误", keys[i]);
                    return false;
                }
            }
            return true;
        }

        public object ObjectToObject(string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return null;
            var v = kv[k];
            if (v == null || v.ToString() == string.Empty) v = null;
            return v;
        }
        public string ObjectToString(string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return "";
            var v = kv[k].ToString();
            if (v == null || v == string.Empty) v = "";
            return v;
        }
        public char ObjectToChar(string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return char.MinValue;
            var s = kv[k].ToString();
            if (s.Length == 0) return char.MinValue;
            else if (s.Length == 1) return s[0];
            else return char.MinValue;
        }
        public int ObjectToInt(string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return 0;
            if (kv[k] is int) return (int)kv[k];
            if (kv[k] is long) return (int)(long)kv[k];

            int i;
            int.TryParse(kv[k].ToString(), out i);
            return i;
        }
        public long ObjectToLong(string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return 0;
            if (kv[k] is int) return (int)kv[k];
            if (kv[k] is long) return (int)(long)kv[k];

            long i;
            long.TryParse(kv[k].ToString(), out i);
            return i;
        }
        public decimal ObjectToDecimal(string k)
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
        public DateTime ObjectToDate(string k)
        {
            if (!kv.ContainsKey(k) || kv[k] == null) return DateTime.MinValue;
            if (kv[k] is DateTime) return (DateTime)kv[k];

            if ((kv[k] is String) && (string)kv[k] == "") return DateTime.MinValue;
            else if ((kv[k] is String) && (string)kv[k] != "") return DateTime.Parse((string)kv[k]);
            return DateTime.Parse((string)kv[k]);
        }

        public object GetObject(ParameterInfo info)
        {
            object value = null;
            if (info.IsOut)
            {
                string typename = info.ParameterType.FullName.Replace("&", "");
                if (info.ParameterType.FullName.Contains(typeof(DataTable).Name))
                {
                    value = new DataTable();
                }
                else
                {
                    return info.ParameterType.IsValueType ? Activator.CreateInstance(info.ParameterType) : null;
                }
                return value;
            }
            var t = info.ParameterType;
            if (t.IsValueType || (t.Name.Contains("&") && info.ParameterType.FullName.Contains(typeof(string).Name)))
            {
                if (!ExistsKeys(info.Name) && !t.Name.Contains("&")) throw new Exception("没有找到[" + info.Name + "]");

                //值类型
                if (info.ParameterType.FullName.Contains(typeof(string).Name))
                {
                    value = ObjectToString(info.Name);
                }
                else if (info.ParameterType.FullName.Contains(typeof(decimal).Name))
                {
                    value = ObjectToDecimal(info.Name);
                }
                else if (info.ParameterType.FullName.Contains(typeof(long).Name))
                {
                    value = ObjectToLong(info.Name);
                }
                else if (info.ParameterType.FullName.Contains(typeof(int).Name))
                {
                    value = ObjectToInt(info.Name);
                }
                else if (info.ParameterType.FullName.Contains(typeof(DateTime).Name))
                {
                    value = ObjectToDate(info.Name);
                }
                else if (info.ParameterType.FullName.Contains(typeof(char).Name))
                {
                    value = ObjectToChar(info.Name);
                }
                else
                {
                    value = ObjectToObject(info.Name);
                }
            }
            else
            {
                //引用类型
                if (info.ParameterType.FullName.Contains("List"))
                {
                    var type = info.ParameterType.GetGenericArguments()[0];

                    value = GetRefList(info.ParameterType, info.Name);

                }
                else if (info.ParameterType.FullName.Contains("Dictionary"))
                {
                    var types = info.ParameterType.GetGenericArguments();
                    value = GetDic(types[0], types[1], info.Name);
                }
                else if (info.ParameterType.FullName.Contains(typeof(string).Name))
                {
                    value = ObjectToString(info.Name);
                }
                else if (info.ParameterType.FullName.Contains(typeof(DataTable).Name))
                {
                    value = GetDataTable();
                }
                else
                {
                    value = GetObjectClss(info.ParameterType, info.Name);
                }
            }
            return value;
        }
        private object GetObjectClss(Type t)
        {
            var pis = t.GetProperties();
            object nm = t.Assembly.CreateInstance(t.FullName);
            for (int i = 0; i < pis.Length; i++)
            {
                var pi = pis[i];
                string colName = pi.Name;

                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        pi.SetValue(nm, ObjectToString(colName), null);
                        break;
                    case "System.Int16":
                        pi.SetValue(nm, (short)ObjectToObject(colName), null);
                        break;
                    case "System.Int32":
                        pi.SetValue(nm, ObjectToInt(colName), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, (long)ObjectToObject(colName), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, ObjectToDecimal(colName), null);
                        break;
                    case "System.Double":
                        pi.SetValue(nm, (double)ObjectToObject(colName), null);
                        break;
                    case "System.Float":
                        pi.SetValue(nm, (float)ObjectToObject(colName), null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":
                        pi.SetValue(nm, ObjectToDate(colName), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, ObjectToChar(colName), null);
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
        private object GetObjectClss(Type t, string key)
        {
            var pis = t.GetProperties();
            object nm = t.Assembly.CreateInstance(t.FullName);

            for (int i = 0; i < pis.Length; i++)
            {
                var pi = pis[i];
                string colName = pi.Name;

                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        pi.SetValue(nm, ObjectToString(colName), null);
                        break;
                    case "System.Int16":
                        pi.SetValue(nm, (short)ObjectToObject(colName), null);
                        break;
                    case "System.Int32":
                        pi.SetValue(nm, ObjectToInt(colName), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, (long)ObjectToObject(colName), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, ObjectToDecimal(colName), null);
                        break;
                    case "System.Double":
                        pi.SetValue(nm, (double)ObjectToObject(colName), null);
                        break;
                    case "System.Float":
                        pi.SetValue(nm, (float)ObjectToObject(colName), null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":
                        pi.SetValue(nm, ObjectToDate(colName), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, ObjectToChar(colName), null);
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
        private object GetObjectClss(Type t, IReadContext r)
        {
            var pis = t.GetProperties();
            object nm = t.Assembly.CreateInstance(t.FullName);

            for (int i = 0; i < pis.Length; i++)
            {
                var pi = pis[i];
                string colName = pi.Name;

                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        pi.SetValue(nm, ObjectToString(colName), null);
                        break;
                    case "System.Int16":
                        pi.SetValue(nm, (short)ObjectToObject(colName), null);
                        break;
                    case "System.Int32":
                        pi.SetValue(nm, ObjectToInt(colName), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, (long)ObjectToObject(colName), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, ObjectToDecimal(colName), null);
                        break;
                    case "System.Double":
                        pi.SetValue(nm, (double)ObjectToObject(colName), null);
                        break;
                    case "System.Float":
                        pi.SetValue(nm, (float)ObjectToObject(colName), null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":
                        pi.SetValue(nm, ObjectToDate(colName), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, ObjectToChar(colName), null);
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
        private object GetRefList(Type t, string key)
        {
            object obj_lis = Activator.CreateInstance(t);
            var type = t.GetGenericArguments()[0];

            foreach (IReadContext r in ReadList(key))
            {
                dynamic dyn = obj_lis;

                dynamic nm = GetRefObjectClss(type, r);//创建对象

                dyn.Add(nm);
            }


            return obj_lis;
        }
        private dynamic GetRefObjectClss(Type t, IReadContext r)
        {
            var pis = t.GetProperties();
            dynamic nm = t.Assembly.CreateInstance(t.FullName);

            for (int i = 0; i < pis.Length; i++)
            {
                var pi = pis[i];
                string colName = pi.Name;
                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        pi.SetValue(nm, ObjectToString(colName), null);
                        break;
                    case "System.Int16":
                        pi.SetValue(nm, (short)ObjectToObject(colName), null);
                        break;
                    case "System.Int32":
                        pi.SetValue(nm, ObjectToInt(colName), null);
                        break;
                    case "System.Int64":
                        pi.SetValue(nm, (long)ObjectToObject(colName), null);
                        break;
                    case "System.Decimal":
                        pi.SetValue(nm, ObjectToDecimal(colName), null);
                        break;
                    case "System.Double":
                        pi.SetValue(nm, (double)ObjectToObject(colName), null);
                        break;
                    case "System.Float":
                        pi.SetValue(nm, (float)ObjectToObject(colName), null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":
                        pi.SetValue(nm, ObjectToDate(colName), null);
                        break;
                    case "System.Char":
                        pi.SetValue(nm, ObjectToChar(colName), null);
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
        /// 自动反射方法
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="t"></param>
        public void ReflectionMethod(object nm, string t)
        {
            try
            {
                var T = nm.GetType();
                MethodInfo[] methods = T.GetMethods().Where(m => m.Name.Equals(t)).ToArray();
                if (methods == null || methods.Length < 1) throw new Exception("请求地址异常!");

                ParameterInfo[] par = new ParameterInfo[] { };
                Object[] obj_par = new Object[] { };
                Dictionary<int, string> ref_dic = new Dictionary<int, string>();
                foreach (MethodInfo method in methods)
                {
                    if (ref_dic.Count > 0) ref_dic.Clear();
                    par = method.GetParameters();
                    obj_par = new Object[par.Length];
                    int num = 1;
                    for (int i = 0; i < par.Length; i++)
                    {
                        ParameterInfo parinfo = par[i];

                        if (parinfo.IsOut)
                        {
                            ref_dic.Add(i, parinfo.Name);
                        }
                        else if (parinfo.ParameterType.IsValueType)
                        {
                            if (!ExistsKeys(par[i].Name)) continue;
                        }

                        obj_par[i] = GetObject(parinfo);
                        num++;
                    }

                    if (num < par.Length && method == methods[methods.Length - 1])
                        throw new Exception("缺少参数");

                    try
                    {
                        object returnValue = method.Invoke(nm, obj_par);
                        WriteObject(returnValue);
                        WriteRefObject(obj_par, ref_dic);
                        WriteSuccess();
                        break;
                    }
                    catch (Exception e)
                    {
                        LogHelper.writeLog("匹配方法", e.ToString());
                        throw e;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
                throw ex;
            }

        }
        public void ReflectionMethod(object nm, string t, Dictionary<string, object> pars_dic)
        {
            Type type = nm.GetType();
            MethodInfo method = type.GetMethod(t);

            if (method == null || method.GetParameters().Length < 2)
            {
                throw new ExceptionBase(-6, "接口不存在");
            }

            ParameterInfo[] parameters = method.GetParameters();
            object[] objects = new object[parameters.Length];
            for (var index = 0; index < parameters.Length; index++)
            {
                ParameterInfo parameter = parameters[index];

                if (parameter.ParameterType == typeof(WebHelper))
                {
                    objects[index] = this;
                }
                else if (parameter.ParameterType == typeof(Dictionary<string, object>))
                {
                    objects[index] = pars_dic;
                }
                else
                {
                    if (!ExistsKeys(parameter.Name))
                    {
                        throw new Exception($@"缺少参数:[{parameter.Name}]");
                    }

                    objects[index] = GetObject(parameter);
                }

            }

            method.Invoke(nm, objects);
        }

        public void WriteObject(object nm)
        {
            if (nm == null) return;
            Type t = nm.GetType();
            if (t.IsValueType || t == typeof(string))
            {
                WriteResult(nm);
            }
            else
            {
                if (nm is DataTable)
                {
                    Write((DataTable)nm);
                }
                else
                {
                    Write(nm);
                }
            }
        }
        public void Write(object cls)
        {
            var ts = cls.GetType();
            var pis = ts.GetProperties();

            for (int j = 0; j < pis.Length; j++)
            {
                var pi = pis[j];
                string colName = pi.Name;
                object nm = pi.GetValue(cls, null);
                string value = nm == null ? "" : nm.ToString();

                WriteObject(colName, value);

            }
        }
        public void WriteRefObject(Object[] obj_par, Dictionary<int, string> ref_dic)
        {
            if (obj_par == null || obj_par.Length < 1 || ref_dic == null || ref_dic.Count < 1)
                return;

            foreach (int par_index in ref_dic.Keys)
            {
                object nm = obj_par[par_index];
                string key = ref_dic[par_index];
                Write(key, nm);
            }

        }

        /// <summary>
        /// 压缩字节
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] CompressBytes(byte[] bytes)
        {
            using (MemoryStream compressStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(compressStream, CompressionMode.Compress))
                    zipStream.Write(bytes, 0, bytes.Length);
                return compressStream.ToArray();
            }
        }

        /// <summary>
        /// 解压缩字节
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public byte[] Decompress(byte[] bytes)
        {
            using (var compressStream = new MemoryStream(bytes))
            {
                using (var zipStream = new GZipStream(compressStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        zipStream.CopyTo(resultStream);
                        return resultStream.ToArray();
                    }
                }
            }
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