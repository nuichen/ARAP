using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB;

namespace IvyFront.create_table
{
    class ot_pay_flow
    {
        public static void Upgrade(SQLiteByHandClose db)
        {
            if (db.ExistTable("ot_pay_flow") == false)
            {
                string sql;
                sql = "CREATE TABLE ot_pay_flow(sheet_no varchar(50),flow_id INTEGER,cus_no VARCHAR(20),oper_id VARCHAR(20),oper_date DATETIME";
                sql += ",pay_way VARCHAR(50),sale_amount DECIMAL(18, 4),pay_amount DECIMAL(18, 4),old_amount DECIMAL(18, 4),ml DECIMAL(18, 4)";
                sql += ",jh VARCHAR(20),approve_flag VARCHAR(1),remark VARCHAR(50),is_upload char(1)) ";
                DB.IDB d = db;
                d.ExecuteScalar(sql, null);
            }
        }
    }
}
