using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class t_click_log
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("t_click_log") == false)
            {
                string sql;
                sql = "create table t_click_log(flow_id varchar(10),cus_id varchar(10),jh varchar(10),click_num varchar(10),oper_id varchar(20),oper_date datetime) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
