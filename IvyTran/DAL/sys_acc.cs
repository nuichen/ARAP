using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.DAL
{
    public class sys_acc
    {
        private DB.IDB db;
        public sys_acc(DB.IDB db)
        {
            this.db = db;
        }

        public Model.sys_acc Select()
        {
            string sql = "select top 1 * from merchant ";
            var dt = db.ExecuteToTable(sql, null);
            Model.sys_acc item = null;
            if (dt.Rows.Count <= 0)
            {
                return item;
            }
            else
            {
                item = new Model.sys_acc();
                item.wx_appid = dt.Rows[0]["wx_appid"].ToString();
                item.wx_secret = dt.Rows[0]["wx_secret"].ToString();
                item.wx_mcid = dt.Rows[0]["wx_mcid"].ToString();
                item.wx_paykey = dt.Rows[0]["wx_paykey"].ToString();
            }
            return item;
        }
    }
}