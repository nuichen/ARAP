using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class ic_t_inout_store_master
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("ic_t_inout_store_master") == false)
            {
                string sql = "create table ic_t_inout_store_master(sheet_no varchar(20) primary key,trans_no varchar(2),branch_no varchar(20)";
                sql += ",supcust_no varchar(20),total_amount decimal(18,4),inout_amount decimal(18,4),approve_flag varchar(1),oper_date datetime";
                sql += ",oper_id varchar(20),is_upload varchar(1),pay_way varchar(20)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
