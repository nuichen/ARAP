using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DB
{
    public interface IDB
    {
        System.Data.DataTable ExecuteToTable(string sql, System.Data.IDbDataParameter[] pars);
        System.Data.DataTable ExecuteToTable(string sql, string sort, System.Data.IDbDataParameter[] pars, int pageSize, int pageIndex, out int total);
        object ExecuteScalar(string sql, System.Data.IDbDataParameter[] pars);
        object ExecuteScalar(string sql, System.Data.IDbDataParameter[] pars, CommandType cmdType);

        T ExecuteToModel<T>(string sql, System.Data.IDbDataParameter[] pars);

        List<T> ExecuteToList<T>(string sql);
        List<T> ExecuteToList<T>(string sql, System.Data.IDbDataParameter[] pars);

        void Insert(object obj);
        void Insert(object obj, string without_fields);

        void BulkCopy<T>(List<T> lis);

        void Update(object obj, string key_fields);
        void Update(object obj, string key_fields, string update_fields);

    }
}
