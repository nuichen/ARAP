using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class ic_t_branch_stock
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("ic_t_branch_stock") == false)
            {
                string sql;
                sql = "create table ic_t_branch_stock(branch_no varchar(20),item_no varchar(20),stock_qty decimal(18,4)";
                sql += ",cost_price decimal(18,4),display_flag varchar(1),last_price decimal(18,4),fifo_price decimal(18,4)) ";

                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
