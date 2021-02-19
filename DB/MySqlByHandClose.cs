using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace DB
{
    public class MySqlByHandClose : IDB
    {
        private MySql.Data.MySqlClient.MySqlConnection conn = null;
        private MySql.Data.MySqlClient.MySqlTransaction tran = null;
        public MySqlByHandClose()
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["conn"]);
        }

        public MySqlByHandClose(string connection_string)
        {
            conn = new MySql.Data.MySqlClient.MySqlConnection(connection_string);
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


        System.Data.DataTable IDB.ExecuteToTable(string sql, System.Data.IDbDataParameter[] pars)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;
            cmd.CommandTimeout = 8000;

            MySql.Data.MySqlClient.MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter();
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

        System.Data.DataTable IDB.ExecuteToTable(string sql, string sort, System.Data.IDbDataParameter[] pars, int pageSize, int pageIndex, out int total)
        {
            throw new NotImplementedException();
        }

        object IDB.ExecuteScalar(string sql, System.Data.IDbDataParameter[] pars)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
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

        object IDB.ExecuteScalar(string sql, System.Data.IDbDataParameter[] pars, System.Data.CommandType cmdType)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
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

        T IDB.ExecuteToModel<T>(string sql, System.Data.IDbDataParameter[] pars)
        {
            IDB db = this;
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count == 0) return default(T); else return ReflectionHelper.DataRowToModel<T>(dt.Rows[0]);
        }

        void IDB.Insert(object obj)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
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
            sql = "insert into " + ReflectionHelper.GetDataTableNameByModel(obj) + "(" + fields + ")values(" + values + ")";
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(ReflectionHelper.ModelToSqlParameters(obj));
            //

            cmd.ExecuteScalar();
        }

        public void BulkCopy<T>(List<T> lis)
        {
            throw new NotImplementedException();
        }

        void IDB.Update(object obj, string key_fields)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
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
            sql = "update " + ReflectionHelper.GetDataTableNameByModel(obj) + " set " + fields + " where " + filter;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(ReflectionHelper.ModelToSqlParameters(obj));
            //
            cmd.ExecuteScalar();
        }

        void IDB.Update(object obj, string key_fields, string update_fields)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
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
            sql = "update " + ReflectionHelper.GetDataTableNameByModel(obj) + " set " + fields + " where " + filter;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(ReflectionHelper.ModelToSqlParameters(obj));
            //

            cmd.ExecuteScalar();
        }


        List<T> IDB.ExecuteToList<T>(string sql)
        {
            throw new NotImplementedException();
        }

        List<T> IDB.ExecuteToList<T>(string sql, IDbDataParameter[] pars)
        {
            throw new NotImplementedException();
        }

        void IDB.Insert(object obj, string without_fields)
        {
            throw new NotImplementedException();
        }
    }
}
