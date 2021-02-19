using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.DAL
{
    public class merchant_configDAL
    {
        DB.IDB db;
        public merchant_configDAL(DB.IDB db)
        {
            this.db = db;
        }

        public string SelectValue(int mc_id, string ckey)
        {
            string sql = "select cvalue from merchant_config where mc_id=@mc_id and ckey=@ckey";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_id",mc_id),
                new System.Data.SqlClient.SqlParameter("@ckey",ckey)
            };
            object obj = db.ExecuteScalar(sql, pars);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }

        }

        public void SaveValue(int mc_id, string ckey, string cvalue)
        {
            bool exist = false;
            string sql = "select count(*) from merchant_config where mc_id=@mc_id and ckey=@ckey";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_id",mc_id),
                new System.Data.SqlClient.SqlParameter("@ckey",ckey)
            };
            if ((int)db.ExecuteScalar(sql, pars) == 0) exist = false;
            else exist = true;

            if (exist == true)
            {
                sql = "update merchant_config set cvalue=@cvalue where mc_id=@mc_id and ckey=@ckey";
            }
            else
            {
                sql = "insert merchant_config(mc_id,ckey,cvalue) values(@mc_id,@ckey,@cvalue)";
            }
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mc_id",mc_id),
                new System.Data.SqlClient.SqlParameter("@ckey",ckey),
                new System.Data.SqlClient.SqlParameter("@cvalue",cvalue)
            };
            db.ExecuteScalar(sql, pars);
        }
    }
}