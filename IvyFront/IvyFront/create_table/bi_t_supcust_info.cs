using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class bi_t_supcust_info
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("bi_t_supcust_info") == false)
            {
                string sql = "create table bi_t_supcust_info(supcust_no varchar(20),supcust_flag varchar(1),sup_name varchar(50),sup_tel varchar(20),sup_pyname varchar(50)";
                sql += ",display_flag varchar(1),credit_amt decimal(18,4),other1 varchar(60),cust_level varchar(1),is_retail varchar(10),primary key (supcust_no,supcust_flag)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }

        }
    }
}
