using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class sm_t_salesheet
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("sm_t_salesheet") == false)
            {
                string sql = "create table sm_t_salesheet(sheet_no varchar(20) primary key,branch_no varchar(20),cust_no varchar(20),pay_way varchar(20)";
                sql += ",coin_no varchar(50),real_amount decimal(18,4),total_amount decimal(18,4),paid_amount decimal(18,4),approve_flag varchar(1)";
                sql += ",oper_id varchar(20),sale_man varchar(20),oper_date datetime,pay_date datetime,is_upload varchar(1),discount decimal(18,4)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);

            }
            //discount整单折扣
        }
    }
}
