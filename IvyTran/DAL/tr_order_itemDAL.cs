using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Model;

namespace IvyTran.DAL
{
    public class tr_order_itemDAL
    {
        private DB.IDB db;
        public tr_order_itemDAL(DB.IDB db)
        {
            this.db = db;
        }
        public tr_order_item SelectById(string ord_id, int row_index)
        {
            string sql = "select * from tr_order_item where ord_id=@ord_id and row_index=@row_index";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ord_id",ord_id),
                new System.Data.SqlClient.SqlParameter("@row_index",row_index)
            };
            var dt = db.ExecuteToTable(sql, pars);
            tr_order_item item = new tr_order_item();
            if (dt.Rows.Count == 0)
            {
                item = null;
            }
            else
            {
                DataRow row = dt.Rows[0];
                item = DB.ReflectionHelper.DataRowToModel<tr_order_item>(row);
            }
            return item;
        }

        public List<tr_order_item> SelectById(string ord_id)
        {
            string sql = "select * from tr_order_item where ord_id = @ord_id and enable = '1' ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ord_id",ord_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            List<tr_order_item> lst = new List<tr_order_item>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows) {
                    var item = new tr_order_item();
                    item = DB.ReflectionHelper.DataRowToModel<tr_order_item>(row);
                    lst.Add(item);
                }
            }
            return lst;
        }
    }
}