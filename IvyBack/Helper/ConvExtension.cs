using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace IvyBack.Helper
{
    /// <summary>
    /// 常用转换
    /// </summary>
    public static class ConvExtension
    {

        public static Int16 ToInt16(this object obj)
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

        public static Int32 ToInt32(this object obj)
        {
            Int32 result = 0;

            if (obj == null)
            {
                result = 0;
            }
            else if (obj is bool)
            {
                result = Convert.ToInt32(obj);
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

        public static Int64 ToInt64(this object obj)
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

        public static decimal ToDecimal(this object obj)
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

        public static double ToDouble(this object obj)
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

        public static float ToSingle(this object obj)
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

        public static char ToChar(this object obj)
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

        public static bool ToBool(this object obj)
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

                    if (!flag)
                    {
                        flag = Convert.ToBoolean(obj.ToInt32());
                    }

                }
            }

            return flag;
        }
        public static string ToAutoString(this decimal d)
        {
            string str;

            if (d % 1 > 0)
            {
                //小数
                str = d.ToString("0.00");
            }
            else
            {
                //整数
                str = d.ToInt32().ToString();
            }

            return str;
        }
        public static DateTime ToDateTime(this object obj)
        {
            DateTime time = DateTime.MinValue;

            if (obj == null)
            {
                time = DateTime.MinValue;
            }
            else
            {
                if (!DateTime.TryParse(obj.ToString(), out time))
                {
                    time = Convert.ToDateTime(obj.ToString());
                }
            }

            return time;
        }

        public static string Toyyyy_MM_dd_HH_mm_ss(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string Toyyyy_MM_dd(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }
        public static string Toyyyy_MM_ddStart(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd") + " 00:00:00.000";
        }
        public static string Toyyyy_MM_ddEnd(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd") + " 23:59:59.000";
        }

        public static int GetDays(this DateTime time)
        {
            DateTime time2 = time.AddMonths(1);
            return (time2 - time).Days;
        }

        public static T DataRowToModel<T>(this T obj, DataRow dr)
        {
            Type type = typeof(T);

            System.Reflection.PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo info in propertyInfos)
            {
                if (!dr.Table.Columns.Contains(info.Name)) continue;

                switch (info.PropertyType.FullName)
                {
                    case "System.String":
                        info.SetValue(obj, dr[info.Name].ToString(), null);
                        break;
                    case "System.Int16":
                        info.SetValue(obj, dr[info.Name].ToInt16(), null);
                        break;
                    case "System.Int32":
                        info.SetValue(obj, dr[info.Name].ToInt32(), null);
                        break;
                    case "System.Int64":
                        info.SetValue(obj, dr[info.Name].ToInt64(), null);
                        break;
                    case "System.Decimal":
                        info.SetValue(obj, dr[info.Name].ToDecimal(), null);
                        break;
                    case "System.Double":
                        info.SetValue(obj, dr[info.Name].ToDouble(), null);
                        break;
                    case "System.Float":
                        info.SetValue(obj, dr[info.Name].ToSingle(), null);
                        break;
                    case "System.DateTime":
                        info.SetValue(obj, dr[info.Name].ToDateTime(), null);
                        break;
                    case "System.Char":
                        info.SetValue(obj, dr[info.Name].ToChar(), null);
                        break;
                }
            }

            return obj;
        }
        public static void DataModelToRow<T>(this T obj, DataRow dr)
        {
            Type type = typeof(T);

            System.Reflection.PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo info in propertyInfos)
            {
                if (!dr.Table.Columns.Contains(info.Name)) continue;

                object value = info.GetValue(obj, null);
                dr[info.Name] = value;
            }
        }

        /// <summary>
        /// 检查表是否存在字段
        /// </summary>
        /// <param name="tb">数据源</param>
        /// <param name="coloumn">需要检查的字段，多个字段 ","号隔开</param>
        public static bool ExistFileds(this DataTable tb, string coloumns, out string message)
        {
            message = "";
            if (tb.Columns.Count < 1 || string.IsNullOrEmpty(coloumns))
            {
                return false;
            }

            bool flag = false;

            foreach (var coloumn in coloumns.Split(','))
            {
                if (tb.Columns.Contains(coloumn))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    message = "表中不存在列:" + coloumn;
                    break;
                }
            }


            return flag;
        }

        #region 解压缩字节

        /// <summary>
        /// 压缩字节
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] CompressBytes(this byte[] bytes)
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
        public static byte[] Decompress(this byte[] bytes)
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

        #endregion

        #region 序列化
        /// <summary>
        /// 序列化称成文件 保存本地
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">文件名称</param>
        public static byte[] SerializableObject<T>(this T obj)
        {

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, obj);
                byte[] newArray = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(newArray, 0, (int)stream.Length);
                return newArray;
            }
        }
        public static void SerializableObject<T>(this T obj, string name)
        {
            if (!Directory.Exists(Program.path + "\\Resource\\"))
            {
                Directory.CreateDirectory(Program.path + "\\Resource\\");
            }
            string file = Program.path + "\\Resource\\" + name + ".obj";
            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
            }
        }

        /// <summary>
        /// 从本地文件里反序列成文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="name">文件名称</param>
        /// <returns></returns>
        public static T DeSerializableObject<T>(this T obj, byte[] array)
        {
            using (MemoryStream stream = new MemoryStream(array))
            {
                BinaryFormatter bf = new BinaryFormatter();
                obj = (T)bf.Deserialize(stream);
                return obj;
            }
        }
        public static T DeSerializableObject<T>(this T obj, string name)
        {
            if (!Directory.Exists(Program.path + "\\Resource\\"))
            {
                Directory.CreateDirectory(Program.path + "\\Resource\\");
            }
            string file = Program.path + "\\Resource\\" + name + ".obj";
            if (File.Exists(file))
            {
                //不会锁文件
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    obj = (T)bf.Deserialize(fs);
                }
            }
            return obj;

        }

        #endregion

        #region 格式化字符串
        public static string FormatWith(this string format, params object[] objs)
        {
            return string.Format(format, objs);
        }
        public static string FormatWith(this string format, IFormatProvider fp, params object[] objs)
        {
            return string.Format(fp, format, objs);
        }
        #endregion

    }
}
