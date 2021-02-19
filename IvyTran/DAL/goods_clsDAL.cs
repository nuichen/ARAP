using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace IvyTran.DAL
{
    public class goods_clsDAL
    {
        DB.IDB db;
        public goods_clsDAL(DB.IDB db)
        {
            this.db = db;
        }
        public goods_cls SelectById(string cls_id)
        {
            string sql_s_goods_cls = "select * from goods_cls where cls_id = @cls_id";
            var pars_s_goods_cls = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cls_id",cls_id)
            };
            var dt = db.ExecuteToModel<goods_cls>(sql_s_goods_cls, pars_s_goods_cls);
            return dt;
        }
    }
}