using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using IvyTran.Helper;

namespace IvyTran.BLL.API
{
    public class AssApiBLL : IAssApiBLL
    {
        DataTable IAssApiBLL.GetItemInfoList(string last_req, int page_no, out int total)
        {
            var condition_sql = "";
            if (!string.IsNullOrEmpty(AppSetting.sort_cls)) 
            {
                var tmp_cls = "'" + AppSetting.sort_cls.Replace(",", "','") + "'";
                condition_sql += " and substring(item_clsno,1,2) in(" + tmp_cls + ") ";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY update_time) AS rowid,item_no,item_subno,item_subname";
            sql += ",item_name,item_clsno,unit_no,order_unit,item_size,display_flag,sup_no,barcode,item_pack,combine_sta,price,sale_price";
            sql += " FROM bi_t_item_info where convert(varchar(19),update_time,120)>='" + last_req + "' " + condition_sql + ") t ";
            sql += "WHERE t.rowid > " + (page_no - 1) * 5000 + " AND t.rowid <= " + page_no * 5000;

            var dt = db.ExecuteToTable(sql, null);

            sql = "select count(*) as total from bi_t_item_info where convert(varchar(19),update_time,120)>='" + last_req + "' " + condition_sql + " ";
            var dt2 = db.ExecuteToTable(sql, null);
            total = Conv.ToInt(dt2.Rows[0]["total"]);

            return dt;

        }

        DataTable IAssApiBLL.GetItemUnitList(string last_req, int page_no, out int total)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY update_time) AS rowid,item_no,unit,unit_factor ";
            sql += "FROM bi_t_item_unit where convert(varchar(19),update_time,120)>='" + last_req + "') t ";
            sql += "WHERE t.rowid > " + (page_no - 1) * 5000 + " AND t.rowid <= " + page_no * 5000;

            var dt = db.ExecuteToTable(sql, null);

            sql = "select count(*) as total from bi_t_item_unit where convert(varchar(19),update_time,120)>='" + last_req + "' ";
            var dt2 = db.ExecuteToTable(sql, null);
            total = Conv.ToInt(dt2.Rows[0]["total"]);

            return dt;

        }

        DataTable IAssApiBLL.GetItemBarcodeList(string last_req, int page_no, out int total)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY update_time) AS rowid,item_no,barcode ";
            sql += "FROM bi_t_item_barcode where convert(varchar(19),update_time,120)>='" + last_req + "') t ";
            sql += "WHERE t.rowid > " + (page_no - 1) * 5000 + " AND t.rowid <= " + page_no * 5000;

            var dt = db.ExecuteToTable(sql, null);

            sql = "select count(*) as total from bi_t_item_barcode where convert(varchar(19),update_time,120)>='" + last_req + "' ";
            var dt2 = db.ExecuteToTable(sql, null);
            total = Conv.ToInt(dt2.Rows[0]["total"]);

            return dt;

        }

        DataTable IAssApiBLL.GetItemClsList(string last_req)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = "select item_clsno,item_clsname,display_flag from bi_t_item_cls ";
            sql += "where convert(varchar(19),update_time,120)>='" + last_req + "' ";

            var dt = db.ExecuteToTable(sql, null);
            return dt;

        }

        DataTable IAssApiBLL.GetSupList(string last_req, int page_no, out int total)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY update_time) AS rowid,supcust_no,supcust_flag,sup_name,sup_type,display_flag,isnull(show_num,supcust_no) show_num ";
            sql += "FROM bi_t_supcust_info where convert(varchar(19),update_time,120)>='" + last_req + "') t ";
            sql += "WHERE t.rowid > " + (page_no - 1) * 5000 + " AND t.rowid <= " + page_no * 5000;

            var dt = db.ExecuteToTable(sql, null);

            sql = "select count(*) as total from bi_t_supcust_info where convert(varchar(19),update_time,120)>='" + last_req + "' ";
            var dt2 = db.ExecuteToTable(sql, null);
            total = Conv.ToInt(dt2.Rows[0]["total"]);

            return dt;

        }

        DataTable IAssApiBLL.GetItemStock(string branch_no, string keyword)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select item_no from bi_t_item_info where (barcode=@keyword or item_subno=@keyword) ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@keyword", keyword)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count == 0)
                {
                    sql = "select item_no from bi_t_item_barcode where barcode=@keyword ";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@keyword", keyword)
                    };
                    dt = db.ExecuteToTable(sql, pars);
                }
                if (dt.Rows.Count > 0)
                {
                    sql = "select a.item_no,a.stock_qty,b.item_subno,b.unit_no,b.item_size,b.barcode,b.sale_price,b.price,a.stock_qty as stock_qty2 ";
                    sql += "from ic_t_branch_stock a ";
                    sql += "inner join bi_t_item_info b on a.item_no=b.item_no ";
                    sql += "where a.branch_no=@branch_no and a.item_no=@item_no ";
                    pars = new System.Data.SqlClient.SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@branch_no", branch_no),
                        new System.Data.SqlClient.SqlParameter("@item_no", dt.Rows[0]["item_no"].ToString())
                    };
                    dt = db.ExecuteToTable(sql, pars);
                    return dt;
                }
                else
                {
                    throw new Exception("条码无匹配商品");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}