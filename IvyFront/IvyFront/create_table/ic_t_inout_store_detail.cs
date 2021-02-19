using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class ic_t_inout_store_detail
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("ic_t_inout_store_detail") == false)
            {
                string sql = "create table ic_t_inout_store_detail(sheet_no varchar(20),item_no varchar(20),item_subno varchar(20),item_name varchar(60),unit_no varchar(20),in_qty decimal(18,4)";
                sql += ",orgi_price decimal(18,4),valid_price decimal(18,4),cost_price decimal(18,4),valid_date datetime,barcode varchar(20),sheet_sort integer,other3 varchar(60)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);

            }

            //other3: 是否赠送：1赠送

        }
    }
}
