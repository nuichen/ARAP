using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class bt_sysn_info
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("bt_sysn_info") == false)
            {
                string sql;
                sql = "CREATE TABLE bt_sysn_info(table_name VARCHAR(50), sysn_time DATETIME) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
