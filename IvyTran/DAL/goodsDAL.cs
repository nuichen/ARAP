using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace IvyTran.DAL
{
    public class goodsDAL
    {
        DB.IDB db;
        public goodsDAL(DB.IDB db)
        {
            this.db = db;
        }
        public goods SelectById(string goods_id)
        {
            string sql_s_goods = "select * from goods where goods_id = @goods_id";
            var pars_s_goods = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@goods_id",goods_id)
            };
            var dt = db.ExecuteToModel<goods>(sql_s_goods, pars_s_goods);

            return dt;
        }
        public void DeleteById(string goods_id)
        {
            string sql_d_goods = "delete from goods where goods_id = @goods_id";
            var pars_d_goods = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@goods_id",goods_id)
            };
            db.ExecuteScalar(sql_d_goods, pars_d_goods);
        }

        //通过商品编号获取到商品名称
        //public int GetGoodsId(string goods_no)
        //{
        //    string sql = "select a.goods_id  from goods a left join goods_cls b on a.cls_id = b.cls_id  where a.goods_no = @goods_no and  b.mc_id = @mc_id ";
        //    var pars = new System.Data.SqlClient.SqlParameter[]
        //    {
        //        new System.Data.SqlClient.SqlParameter("@goods_no",goods_no),
        //        new System.Data.SqlClient.SqlParameter("@mc_id",Global.McId)
        //    };
        //    var dt = db.ExecuteToTable(sql, pars);
        //    if (dt.Rows.Count == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return int.Parse(dt.Rows[0]["goods_id"].ToString());
        //    }
        //}
    }
}