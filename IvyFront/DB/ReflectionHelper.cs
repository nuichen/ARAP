using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB
{
  public class ReflectionHelper
    {

      public static  T DataRowToModel<T>( System.Data.DataRow row)
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
                          if (row[colName] != DBNull.Value) pi.SetValue(r, (string)row[colName], null);
                          break;
                      case "System.Int16":
                          if (row[colName] != DBNull.Value) pi.SetValue(r, (short)row[colName], null);
                          break;
                      case "System.Int32":
                          if (row[colName] != DBNull.Value) pi.SetValue(r, (int)row[colName], null);
                          break;
                      case "System.Int64":
                          if (row[colName] != DBNull.Value) pi.SetValue(r, (long)row[colName], null);
                          break;
                      case "System.Decimal":
                          if (row[colName] != DBNull.Value) pi.SetValue(r, (decimal)row[colName], null);
                          break;
                      case "System.Double":
                          if (row[colName] != DBNull.Value) pi.SetValue(r, (double)row[colName], null);
                          break;
                      case "System.Float":
                          if (row[colName] != DBNull.Value) pi.SetValue(r, (float)row[colName], null);
                          break;
                      //case "System.Guid":
                      //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                      //    break;
                      case "System.DateTime":
                          if (row[colName] != DBNull.Value) pi.SetValue(r, (DateTime)row[colName], null);
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
              throw new Exception("1"+ex.Message );
          }

      }


      public static MySql.Data.MySqlClient.MySqlParameter[] ModelToSqlParameters(object obj)
      {
          List<MySql.Data.MySqlClient.MySqlParameter> lst = new List<MySql.Data.MySqlClient.MySqlParameter>();
          var t = obj.GetType();
          var pis = t.GetProperties();
          for (int j = 0; j < pis.Length; j++)
          {
              var pi = pis[j];
              string colName = pi.Name.ToUpper();
              MySql.Data.MySqlClient.MySqlParameter par = new MySql.Data.MySqlClient.MySqlParameter();
              par.ParameterName = "@" + pi.Name;
              switch (pi.PropertyType.FullName)
              {
                  case "System.String":
                      lst.Add(par);
                      par.DbType =System.Data.DbType.String;
                      par.Value = pi.GetValue(obj, null);
                      break;
                  case "System.Int16":
                      lst.Add(par);
                      par.DbType =System.Data.DbType.Int16; 
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
                      if ((System.DateTime )par.Value == System.DateTime.MinValue)
                      {
                          par.Value = DBNull.Value ;
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

          return lst.ToArray<MySql.Data.MySqlClient.MySqlParameter>();

      }


      public static string GetDataTableNameByModel(object obj)
      {
          string tb = obj.GetType().Name.Split('.')[obj.GetType().Name.Split('.').Length - 1];
          return tb;
      }


      
    }
}
