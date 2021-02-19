using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class bi_t_item_info
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("bi_t_item_info") == false)
            {
                string sql = "create table bi_t_item_info(item_no varchar(20) PRIMARY KEY,item_subno varchar(20),item_subname varchar(100),item_clsno varchar(20)";
                sql += ",item_name varchar(100),item_brand varchar(20),item_brandname varchar(100),unit_no varchar(20),item_size varchar(20),product_area varchar(100)";
                sql += ",barcode varchar(20),price decimal(18,4),base_price decimal(18, 4),sale_price decimal(18, 4),display_flag varchar(1),item_flag varchar(1)";
                sql += ",base_price2 decimal(18, 4),base_price3 decimal(18, 4),base_price4 decimal(18, 4),base_price5 decimal(18, 4)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }

        }
    }
}
