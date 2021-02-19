using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class sm_t_salesheet_detail
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("sm_t_salesheet_detail") == false)
            {
                string sql = "create table sm_t_salesheet_detail(sheet_no varchar(20),item_no varchar(20),item_subno varchar(20),item_name varchar(60),unit_no varchar(20),sale_price decimal(18,4)";
                sql += ",real_price decimal(18,4),cost_price decimal(18,4),sale_qnty decimal(18,4),sale_money decimal(18,4),barcode varchar(20),sheet_sort integer,other3 varchar(60)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);

            }
            //other3:是否赠送：1赠送

        }
    }
}
