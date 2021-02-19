using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    public class bi_t_sup_item
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("bi_t_sup_item") == false)
            {
                string sql = "create table bi_t_sup_item(sup_id varchar(20),item_no varchar(20),price decimal(18,4),top_price decimal(18,4)";
                sql += ",bottom_price decimal(18,4),last_price decimal(18,4),top_sheet_no varchar(20),bottom_sheet_no varchar(20),last_sheet_no varchar(20)";
                sql += ",spec_from datetime,spec_to datetime,spec_price decimal(18,4),item_status varchar(20)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }

        }
    }
}
