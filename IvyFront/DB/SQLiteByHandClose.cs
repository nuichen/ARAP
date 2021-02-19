using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DB
{
  public class SQLiteByHandClose:DB.IDB
    {
        private System.Data.SQLite.SQLiteConnection conn = null;
        private System.Data.SQLite.SQLiteTransaction tran = null;

        
        public SQLiteByHandClose(string connection_string)
        {
            conn = new System.Data.SQLite.SQLiteConnection(connection_string);
        }

        public void Open()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

        }

        public void Close()
        {
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

        }



        public void BeginTran()
        {
            tran = conn.BeginTransaction();
        }

        public void CommitTran()
        {
            if (tran != null)
            {
                tran.Commit();
                tran = null;
            }

        }

        public void RollBackTran()
        {
            if (tran != null)
            {
                tran.Rollback();
                tran = null;
            }

        }

        System.Data.DataTable DB.IDB.ExecuteToTable(string sql, System.Data.IDbDataParameter[] pars)
        {
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;
            cmd.CommandTimeout = 8000;
            System.Data.SQLite.SQLiteDataAdapter da = new System.Data.SQLite.SQLiteDataAdapter();
            da.SelectCommand = cmd;
            cmd.CommandText = sql;
            if (pars != null)
            {
                cmd.Parameters.AddRange(pars);
            }

            //

            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            return dt;
        }

        System.Data.DataTable DB.IDB.ExecuteToTable(string sql, string sort, System.Data.IDbDataParameter[] pars, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }

        object DB.IDB.ExecuteScalar(string sql, System.Data.IDbDataParameter[] pars)
        {
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;
            cmd.CommandTimeout = 8000;
            cmd.CommandText = sql;
            if (pars != null)
            {
                cmd.Parameters.AddRange(pars);
            }
            //

            return cmd.ExecuteScalar();
        }

        object DB.IDB.ExecuteScalar(string sql, System.Data.IDbDataParameter[] pars, System.Data.CommandType cmdType)
        {
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;
            cmd.CommandTimeout = 8000;
            cmd.CommandType = cmdType;
            cmd.CommandText = sql;
            if (pars != null)
            {
                cmd.Parameters.AddRange(pars);
            }
            //

            return cmd.ExecuteScalar();
        }

        T DB.IDB.ExecuteToModel<T>(string sql, System.Data.IDbDataParameter[] pars)
        {
            DB.IDB db = this;
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count == 0) return default(T); else return DB.ReflectionHelper.DataRowToModel<T>(dt.Rows[0]);
        }

        void DB.IDB.Insert(object obj)
        {
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;
            cmd.CommandTimeout = 8000;
            //
            string sql = "";
            string fields = "";
            string values = "";
            foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
            {
                if (fields == "")
                {
                    fields += p.Name;
                    values += "@" + p.Name;
                }
                else
                {
                    fields += "," + p.Name;
                    values += "," + "@" + p.Name;
                }
            }
            sql = "insert into " + DB.ReflectionHelper.GetDataTableNameByModel(obj) + "(" + fields + ")values(" + values + ")";
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(ModelToSqlParameters(obj));
            //

            cmd.ExecuteScalar();
        }

        void DB.IDB.Update(object obj, string key_fields)
        {
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;
            cmd.CommandTimeout = 8000;
            //
            string sql = "";
            string fields = "";
            foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
            {
                if (fields == "")
                {
                    fields += p.Name + "=" + "@" + p.Name;
                }
                else
                {
                    fields += "," + p.Name + "=" + "@" + p.Name;
                }
            }
            string filter = "";
            if (key_fields.Contains(",") == false)
            {
                filter = key_fields + "=" + "@" + key_fields;
            }
            else
            {
                foreach (string field in key_fields.Split(','))
                {
                    if (filter == "")
                    {
                        filter += field + "=" + "@" + field;
                    }
                    else
                    {
                        filter += " and " + field + "=" + "@" + field;
                    }
                }
            }
            sql = "update " + DB.ReflectionHelper.GetDataTableNameByModel(obj) + " set " + fields + " where " + filter;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(ModelToSqlParameters(obj));
            //
            cmd.ExecuteScalar();
        }

        void DB.IDB.Update(object obj, string key_fields, string update_fields)
        {
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;
            cmd.CommandTimeout = 8000;
            //
            string sql = "";
            string fields = "";
            List<string> lst = new List<string>();
            if (update_fields.Contains(",") == false)
            {
                lst.Add(update_fields.ToLower());
            }
            else
            {
                foreach (string field in update_fields.Split(','))
                {
                    lst.Add(field.ToLower());
                }
            }
            foreach (System.Reflection.PropertyInfo p in obj.GetType().GetProperties())
            {
                if (lst.Contains(p.Name.ToLower()) == false)
                {
                    continue;
                }
                if (fields == "")
                {
                    fields += p.Name + "=" + "@" + p.Name;
                }
                else
                {
                    fields += "," + p.Name + "=" + "@" + p.Name;
                }
            }
            string filter = "";
            if (key_fields.Contains(",") == false)
            {
                filter = key_fields + "=" + "@" + key_fields;
            }
            else
            {
                foreach (string field in key_fields.Split(','))
                {
                    if (filter == "")
                    {
                        filter += field + "=" + "@" + field;
                    }
                    else
                    {
                        filter += " and " + field + "=" + "@" + field;
                    }
                }
            }
            sql = "update " + DB.ReflectionHelper.GetDataTableNameByModel(obj) + " set " + fields + " where " + filter;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(ModelToSqlParameters(obj));
            //

            cmd.ExecuteScalar();
        }


        public static System.Data.SQLite.SQLiteParameter[] ModelToSqlParameters(object obj)
        {
            List<System.Data.SQLite.SQLiteParameter> lst = new List<System.Data.SQLite.SQLiteParameter>();
            var t = obj.GetType();
            var pis = t.GetProperties();
            for (int j = 0; j < pis.Length; j++)
            {
                var pi = pis[j];
                string colName = pi.Name.ToUpper();
                System.Data.SQLite.SQLiteParameter par = new System.Data.SQLite.SQLiteParameter();
                par.ParameterName = "@" + pi.Name;
                switch (pi.PropertyType.FullName)
                {
                    case "System.String":
                        lst.Add(par);
                        par.DbType = DbType.String;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Int16":
                        lst.Add(par);
                        par.DbType = DbType.Int16;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Int32":
                        lst.Add(par);
                        par.DbType = DbType.Int32;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Int64":
                        lst.Add(par);
                        par.DbType = DbType.Int64;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Decimal":
                        lst.Add(par);
                        par.DbType = DbType.Decimal;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Double":
                        lst.Add(par);
                        par.DbType = DbType.Double;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    case "System.Float":
                        lst.Add(par);
                        par.DbType = DbType.Double;
                        par.Value = pi.GetValue(obj, null);
                        break;
                    //case "System.Guid":
                    //    if (row[colName] != DBNull.Value) pi.SetValue(r, (Guid)row[colName], null);
                    //    break;
                    case "System.DateTime":

                        lst.Add(par);
                        par.DbType = DbType.DateTime;
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

            return lst.ToArray<System.Data.SQLite.SQLiteParameter>();

        }


        public static void CreateFile(string file)
        {
            System.Data.SQLite.SQLiteConnection.CreateFile(file);
        }

        public bool ExistTable(string tb)
        {
            string sql = "select count(*) from sqlite_master where type='table' and name=@tb";
            System.Data.SQLite.SQLiteParameter[] pars = new System.Data.SQLite.SQLiteParameter[]
            {
                new System.Data.SQLite.SQLiteParameter("@tb",DbType.String)
            };
            pars[0].Value = tb;
            DB.IDB db = this;
            
         var obj=   db.ExecuteScalar(sql, pars);
         
         if (obj == null)
         {
             return false;
         }
         else
         {
             int cnt =0;
             int.TryParse(obj.ToString(), out cnt);
             if (cnt == 0)
             {
                 return false;
             }
             else
             {
                 return true;
             }
         }

        }

        public bool ExistField(string tb, string field)
        {
            try
            {
                string sql = "select " + field + " from " + tb + " Limit 1";
                DB.IDB db = this;
                db.ExecuteScalar(sql, null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        /// <summary>
        /// 执行sql集
        /// </summary>
        /// <param name="sqls">要执行sql集</param>
        public static void ExecuteNonQuery(string conn, List<string> sqls)
        {

            using (System.Data.SQLite.SQLiteConnection Conn = new System.Data.SQLite.SQLiteConnection(conn))
            {
                Conn.Open();
                using (System.Data.SQLite.SQLiteTransaction transaction = Conn.BeginTransaction())
                {
                    using (System.Data.SQLite.SQLiteCommand command = Conn.CreateCommand())
                    {
                        try
                        {
                            foreach (string sql in sqls)
                            {
                                command.CommandText = sql;
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                Conn.Close();
            }
        }
    }
}
