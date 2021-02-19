using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.DAL
{
    public class merchantDAL
    {
        DB.IDB db;
        public merchantDAL(DB.IDB db)
        {
            this.db = db;
        }
        public merchant SelectById(int mc_id)
        {
            string sql_s_merchant = "select * from merchant where mc_id = @mc_id";
            var pars_s_merchant = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_id",mc_id.ToString())
            };
            var dt_s_merchant = db.ExecuteToTable(sql_s_merchant,pars_s_merchant);
            merchant merchant = DB.ReflectionHelper.DataRowToModel<merchant>(dt_s_merchant.Rows[0]);
            return merchant;
        }
    }
}