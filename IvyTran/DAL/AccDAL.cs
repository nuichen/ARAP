using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IvyTran.Helper;
using Model;

namespace IvyTran.DAL
{
    public class AccDAL
    {
        DB.IDB db;
        public AccDAL(DB.IDB db)
        {
            this.db = db;
        }

        public merchant_acc SelectById(int mc_id)
        {
            string sql = "select top 1 * from merchant ";
            var dt = db.ExecuteToTable(sql, null);
            merchant_acc item = null;
            if (dt.Rows.Count <= 0)
            {
                return item;
            }
            else 
            {
                item = new merchant_acc();
                item.wx_appid = dt.Rows[0]["wx_appid"].ToString();
                item.wx_secret = dt.Rows[0]["wx_secret"].ToString();
                item.wx_mcid = dt.Rows[0]["wx_mcid"].ToString();
                item.wx_paykey = dt.Rows[0]["wx_paykey"].ToString();
                item.mc_id = Conv.ToInt(dt.Rows[0]["mc_id"]);
            }
            return item;
        }
    }
}