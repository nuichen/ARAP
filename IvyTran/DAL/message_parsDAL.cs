using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.DAL
{
    public class message_parsDAL
    {
        private DB.IDB db;
        public message_parsDAL(DB.IDB db)
        {
            this.db = db;
        }

        public body.message_pars SelectById(int merid, int message_id,string open_type)
        {
            string sql = "select * from message_pars where (merid = @merid or merid = 0) and message_id = @message_id and (open_type = @open_type or isnull(open_type,'0') = '0')";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@merid",merid.ToString()),
                new System.Data.SqlClient.SqlParameter("@message_id",message_id.ToString()),
                new System.Data.SqlClient.SqlParameter("@open_type",open_type)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            return DB.ReflectionHelper.DataRowToModel<body.message_pars>(dt.Rows[0]);
        }
    }
}