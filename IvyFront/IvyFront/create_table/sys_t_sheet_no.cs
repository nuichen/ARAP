using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class sys_t_sheet_no
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("sys_t_sheet_no") == false)
            {
                string sql;
                sql = "create table sys_t_sheet_no(sheet_id varchar(20),sheet_value integer) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);

                sql = "insert into sys_t_sheet_no values('PI',0)";
                d.ExecuteScalar(sql, null);

                sql = "insert into sys_t_sheet_no values('SO',0)";
                d.ExecuteScalar(sql, null);

                sql = "insert into sys_t_sheet_no values('RI',0)";
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
