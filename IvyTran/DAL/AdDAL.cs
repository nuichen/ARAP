using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace IvyTran.DAL
{
    public class AdDAL
    {
        private DB.IDB db;
        public AdDAL(DB.IDB db)
        {
            this.db = db;
        }

        public ad SelectById(string ad_id)
        {
            string sql = "select ad_id,isnull(ad_type,'0') ad_type,ad_name,title_img,detail_img,ad_text,goods_ids from ad where ad_id=@ad_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ad_id",ad_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            return DB.ReflectionHelper.DataRowToModel<ad>(dt.Rows[0]);
        }
    }
}