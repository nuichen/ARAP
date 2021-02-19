using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    public class bi_t_cust_price
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("bi_t_cust_price") == false)
            {
                string sql = "create table bi_t_cust_price(cust_id varchar(20),item_no varchar(20),price_type varchar(1)";
                sql += ",new_price decimal(18,4),top_price decimal(18,4),bottom_price decimal(18,4),last_price decimal(18,4)";
                sql += ",top_sheet_no varchar(20),bottom_sheet_no varcahr(20),last_sheet_no varchar(20)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }

        }
    }
}
