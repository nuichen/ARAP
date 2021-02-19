using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace IvyTran.DAL
{
    public class goods_stdDAL
    {
        DB.IDB db;
        public goods_stdDAL(DB.IDB db)
        {
            this.db = db;
        }
        public void DeleteById(string goods_id)
        {
            string sql_d_goods_std = "delete from goods_std where goods_id = @goods_id";
            var pars_d_goods_std = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@goods_id",goods_id)
            };
            db.ExecuteScalar(sql_d_goods_std,pars_d_goods_std);
        }

        public goods_std SelectById(string goods_id)
        {
            string sql_s_goods_std = "select * from goods_std where goods_id = @goods_id";
            var pars_s_goods_std = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@goods_id",goods_id)
            };
            var dt_s_goods_std = db.ExecuteToModel<goods_std>(sql_s_goods_std, pars_s_goods_std);
            return dt_s_goods_std;
        }

    }
}