using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class t_order_detail
    {
        public static void Upgrade(SQLiteByHandClose db)
        {

            if (db.ExistTable("t_order_detail") == false)
            {
                string sql = "CREATE TABLE t_order_detail(flow_id INTEGER,sheet_no VARCHAR(20),item_no VARCHAR(20),item_subno VARCHAR(20),item_name VARCHAR(50)";
                sql += ",unit_no varchar(20),oper_id VARCHAR(20),oper_date DATETIME,qty DECIMAL(18, 4),price DECIMAL(18, 4),amt DECIMAL(18, 4),jh VARCHAR(20)";
                sql += ",cost_price decimal(18,4),branch_no varchar(20),cus_no varchar(20),sup_no varchar(20),approve_flag varchar(1),is_upload VARCHAR(1)";
                sql += ",update_time datetime,update_oper varchar(20),source_price decimal(18, 4),discount decimal(18, 4),is_give varchar(1))";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }

        }

    }
}
