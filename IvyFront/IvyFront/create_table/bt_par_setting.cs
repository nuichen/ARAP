using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    public class bt_par_setting
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("bt_par_setting") == false)
            {
                string sql;
                sql = "create table bt_par_setting(par_id varchar(20), par_val varchar(50)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}


