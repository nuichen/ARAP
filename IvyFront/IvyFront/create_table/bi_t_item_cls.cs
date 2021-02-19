using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class bi_t_item_cls
    {
        public static void Upgrade(SQLiteByHandClose db)
        {

            if (db.ExistTable("bi_t_item_cls") == false)
            {
                string sql;
                sql = "create table bi_t_item_cls(item_clsno varchar(20),item_flag varchar(1),item_clsname varchar(100),display_flag varchar(1),is_stop varchar(1)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }

        }

    }
}
