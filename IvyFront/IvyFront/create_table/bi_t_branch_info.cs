using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class bi_t_branch_info
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("bi_t_branch_info") == false)
            {
                string sql;
                sql = "create table bi_t_branch_info(branch_no varchar(20) PRIMARY KEY,branch_name varchar(200)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
