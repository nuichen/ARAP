using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    public class sys_t_system
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("sys_t_system") == false)
            {
                string sql = "create table sys_t_system(sys_var_id varchar(20),sys_var_name varchar(40),sys_var_value varchar(250),is_changed varchar(2)";
                sql += ",sys_var_desc varchar(100),sys_ver_flag varchar(1),update_time datetime) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);

            }

        }
    }
}
