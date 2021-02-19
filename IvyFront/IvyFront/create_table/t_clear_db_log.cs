using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class t_clear_db_log
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("t_clear_db_log") == false)
            {
                string sql;
                sql = "create table t_clear_db_log(oper_id varchar(20),oper_date datetime) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
