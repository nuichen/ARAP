using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class sa_t_operator_i
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("sa_t_operator_i") == false)
            {
                string sql = "create table sa_t_operator_i(oper_id varchar(20),oper_type varchar(20),oper_name varchar(50),oper_pw varchar(50)";
                sql += ",oper_status varchar(1),update_time datetime,is_branch varchar(1),is_admin varchar(1),branch_no varchar(20)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
