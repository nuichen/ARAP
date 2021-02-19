using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DB
{
    public class ReflectionHelper
    {

        public static T DataRowToModel<T>(System.Data.DataRow row)
        {
            try
            {

                if (row == null) return default(T);

                var t = typeof(T);
                var pis = t.GetProperties();
                T r = (T)Activator.CreateInstance(t);
                for (int j = 0; j < pis.Length; j++)
                {
                    var pi = pis[j];
                    string colName = pi.Name.ToUpper();
                    if (!row.Table.Columns.Contains(colName)) continue;
                    switch (pi.PropertyType.FullName)
                    {
                        case "System.String":
                            if (row[colName] != DBNull.Value &&
                                row[colName] != null &&
                                !string.IsNullOrEmpty(row[colName].ToString()))
                                pi.SetValue(r, row[colName].ToString(), null);
                            break;
                        case "System.Int16":
                            if (row[colName] != DBNull.Value &&
                                row[colName] != null &&
                                !string.IsNullOrEmpty(row[colName].ToString()))
                                pi.SetValue(r, Conv.ToInt16(row[colName]), null);
                            break;
                        case "System.Int32":
                            if (row[colName] != DBNull.Value &&
                                row[colName] != null &&
                                !string.IsNullOrEmpty(row[colName].ToString()))
                                pi.SetValue(r, Conv.ToInt(row[colName]), null);
                            break;
                        case "System.Int64":
                            if (row[colName] != DBNull.Value &&
                                row[colName] != null &&
                                !string.IsNullOrEmpty(row[colName].ToString()))
                                pi.SetValue(r, Conv.ToInt64(row[colName]), null);
                            break;
                        case "System.Decimal":
                            if (row[colName] != DBNull.Value &&
                                row[colName] != null &&
                                !string.IsNullOrEmpty(row[colName].ToString()))
                                pi.SetValue(r, Conv.ToDecimal(row[colName]), null);
                            break;
                        case "System.Double":
                            if (row[colName] != DBNull.Value &&
                                row[colName] != null &&
                                !string.IsNullOrEmpty(row[colName].ToString()))
                                pi.SetValue(r, Conv.ToDouble(row[colName]), null);
                            break;
                        case "System.Float":
                            if (row[colName] != DBNull.Value &&
                                row[colName] != null &&
                                !string.IsNullOrEmpty(row[colName].ToString()))
                                pi.SetValue(r, Conv.ToFloat(row[colName]), null);
                            break;
                        //case "System.Guid":
                        //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                        //    break;
                        case "System.DateTime":
                            if (row[colName] != DBNull.Value &&
                                row[colName] != null &&
                                !string.IsNullOrEmpty(row[colName].ToString()))
                                pi.SetValue(r, Conv.ToDateTime(row[colName]), null);
                            break;

                            //case "System.Boolean":
                            //    if (row[colName] != DBNull.Value) pi.SetValue(r, (bool)row[colName], null);
                            //    break;
                            //case "System.Byte":
                            //    if (row[colName] != DBNull.Value) pi.SetValue(r, (byte)row[colName], null);
                            //    break;
                    }
                }

                return r;
            }
            catch (Exception ex)
            {
                throw new Exception("1" + ex.Message);
            }

        }

        public static DataTable ModelToDataTable<T>(List<T> lis)
        {
            DataTable tb = new DataTable();

            Type t = typeof(T);
            PropertyInfo[] infos = t.GetProperties();
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();

            foreach (PropertyInfo propertyInfo in infos)
            {
                tb.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
                propertyInfos.Add(propertyInfo);
            }

            foreach (T li in lis)
            {
                DataRow dr = tb.NewRow();
                foreach (PropertyInfo info in propertyInfos)
                {
                    object value = GetPropertInfoValue(info, li);
                    dr[info.Name] = value;
                }

                tb.Rows.Add(dr);
            }


            return tb;
        }

        public static System.Data.SqlClient.SqlParameter[] ModelToSqlParameters(object obj)
        {
            List<System.Data.SqlClient.SqlParameter> lst = new List<System.Data.SqlClient.SqlParameter>();
            var t = obj.GetType();
            var pis = t.GetProperties();
            for (int j = 0; j < pis.Length; j++)
            {
                var pi = pis[j];
                string colName = pi.Name.ToUpper();
                System.Data.SqlClient.SqlParameter par = new System.Data.SqlClient.SqlParameter();
                par.ParameterName = "@" + pi.Name;
                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        lst.Add(par);
                        par.DbType = System.Data.DbType.String;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Int16":
                        lst.Add(par);
                        par.DbType = System.Data.DbType.Int16;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Int32":
                        lst.Add(par);
                        par.DbType = System.Data.DbType.Int32;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Int64":
                        lst.Add(par);
                        par.DbType = System.Data.DbType.Int64;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Decimal":
                        lst.Add(par);
                        par.DbType = System.Data.DbType.Decimal;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Double":
                        lst.Add(par);
                        par.DbType = System.Data.DbType.Double;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Float":
                        lst.Add(par);
                        par.DbType = System.Data.DbType.Single;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":

                        lst.Add(par);
                        par.DbType = System.Data.DbType.DateTime;
                        par.Value = pi.GetValue(obj, null);
                        if ((System.DateTime)par.Value == System.DateTime.MinValue)
                        {
                            par.Value = DBNull.Value;
                        }
                        break;
                    //case "System.Boolean":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (bool)row[colName], null);
                    //    break;
                    //case "System.Byte":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (byte)row[colName], null);
                    //    break;
                    default:
                        lst.Add(par);
                        par.Value = pi.GetValue(obj, null);
                        break;
                }
                if (par.Value == null)
                {
                    par.Value = DBNull.Value;
                }
            }

            return lst.ToArray<System.Data.SqlClient.SqlParameter>();

        }


        public static string GetDataTableNameByModel(object obj)
        {
            string tb = obj.GetType().Name.Split('.')[obj.GetType().Name.Split('.').Length - 1];
            return tb;
        }
        public static string GetDataTableNameByModel<T>()
        {
            string tb = typeof(T).Name.Split('.')[typeof(T).Name.Split('.').Length - 1];
            return tb;
        }

        public static string GetParem(PropertyInfo p, object obj)
        {
            string dyn = "";
            switch (p.PropertyType.FullName)
            {
                case "System.String":
                    dyn = "'" + Convert.ToString(p.GetValue(obj, null)) + "'";
                    break;
                case "System.Int16":
                    dyn = Convert.ToInt16(p.GetValue(obj, null)).ToString();
                    break;
                case "System.Int32":
                    dyn = Convert.ToInt32(p.GetValue(obj, null)).ToString();
                    break;
                case "System.Int64":
                    dyn = Convert.ToInt64(p.GetValue(obj, null)).ToString();
                    break;
                case "System.Decimal":
                    dyn = Convert.ToDecimal(p.GetValue(obj, null)).ToString();
                    break;
                case "System.Double":
                    dyn = Convert.ToDouble(p.GetValue(obj, null)).ToString();
                    break;
                case "System.Float":
                    dyn = Convert.ToSingle(p.GetValue(obj, null)).ToString();
                    break;
                case "System.DateTime":
                    dyn = "'" + Convert.ToDateTime(p.GetValue(obj, null)).ToString() + "'";
                    break;
                case "System.Boolean":
                    dyn = "'" + Convert.ToBoolean(p.GetValue(obj, null)).ToString() + "'";
                    break;
                case "System.Byte":
                    dyn = Convert.ToByte(p.GetValue(obj, null)).ToString();
                    break;
                case "System.Char":
                    dyn = "'" + Convert.ToChar(p.GetValue(obj, null)) + "'";
                    break;
                default:
                    dyn = "NULL";
                    break;
            }
            if (string.IsNullOrEmpty(dyn))
            {
                dyn = "''";
            }
            return dyn;
        }

        public static object GetPropertInfoValue(PropertyInfo p, object obj)
        {
            object dyn = null;
            switch (p.PropertyType.FullName)
            {
                case "System.String":
                    dyn = Convert.ToString(p.GetValue(obj, null));
                    break;
                case "System.Int16":
                    dyn = Convert.ToInt16(p.GetValue(obj, null));
                    break;
                case "System.Int32":
                    dyn = Convert.ToInt32(p.GetValue(obj, null));
                    break;
                case "System.Int64":
                    dyn = Convert.ToInt64(p.GetValue(obj, null));
                    break;
                case "System.Decimal":
                    dyn = Convert.ToDecimal(p.GetValue(obj, null));
                    break;
                case "System.Double":
                    dyn = Convert.ToDouble(p.GetValue(obj, null));
                    break;
                case "System.Float":
                    dyn = Convert.ToSingle(p.GetValue(obj, null));
                    break;
                case "System.DateTime":
                    dyn = Convert.ToDateTime(p.GetValue(obj, null));
                    break;
                case "System.Boolean":
                    dyn = Convert.ToBoolean(p.GetValue(obj, null));
                    break;
                case "System.Byte":
                    dyn = Convert.ToByte(p.GetValue(obj, null)).ToString();
                    break;
                case "System.Char":
                    dyn = Convert.ToChar(p.GetValue(obj, null));
                    break;
                default:
                    dyn = null;
                    break;
            }
            return dyn;
        }

        public static string GetSql(object obj, string no_key = "")
        {
            string sql = "";
            string fields = "";
            string values = "";
            foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
            {
                if (fields == "")
                {
                    if (!no_key.Contains(p.Name))
                    {
                        fields += p.Name;
                        values += GetParem(p, obj);
                    }
                }
                else
                {
                    if (!no_key.Contains(p.Name))
                    {
                        fields += "," + p.Name;
                        values += "," + GetParem(p, obj);
                    }
                }

            }

            return sql = "insert into " + ReflectionHelper.GetDataTableNameByModel(obj) + "(" + fields + ")values(" + values + ")";




        }

    }
}
