using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using System.Data.SqlClient;

namespace IvyFront.BLL
{
    class Goods : IBLL.IGoods
    {
        List<Model.bi_t_item_info> IBLL.IGoods.GetList(string item_clsno, string keyword)
        {
            string sql = "select * from bi_t_item_info where display_flag<>'0' and display_flag<>'4' ";

            if (item_clsno != "")
            {
                sql += " and item_clsno like '" + item_clsno + "%'";
            }
            if (keyword != "")
            {
                sql += " and (item_subno like '%!%' or item_name like '%!%' or barcode like '%!%')".Replace("!", keyword);
            }
            var tb = Program.db.ExecuteToTable(sql, null);
            List<Model.bi_t_item_info> lst = new List<Model.bi_t_item_info>();
            foreach (System.Data.DataRow row in tb.Rows)
            {
                var it = DB.ReflectionHelper.DataRowToModel<Model.bi_t_item_info>(row);
                lst.Add(it);
            }
            return lst;
        }

        List<Model.bi_t_item_cls> IBLL.IGoods.GetClsList()
        {
            string sql = "select * from bi_t_item_cls where display_flag='1' and ifnull(is_stop,'0')='0' ";
            var tb = Program.db.ExecuteToTable(sql, null);
            List<Model.bi_t_item_cls> lst = new List<Model.bi_t_item_cls>();
            foreach (System.Data.DataRow row in tb.Rows)
            {
                var it = DB.ReflectionHelper.DataRowToModel<Model.bi_t_item_cls>(row);
                lst.Add(it);
            }
            return lst;
        }

        List<Model.bi_t_item_cls> IBLL.IGoods.GetAllClsList()
        {
            string sql = "select * from bi_t_item_cls where display_flag='1' ";
            var tb = Program.db.ExecuteToTable(sql, null);
            List<Model.bi_t_item_cls> lst = new List<Model.bi_t_item_cls>();
            foreach (System.Data.DataRow row in tb.Rows)
            {
                var it = DB.ReflectionHelper.DataRowToModel<Model.bi_t_item_cls>(row);
                lst.Add(it);
            }
            return lst;
        }

        void IBLL.IGoods.StopCls(string item_clsno)
        {
            string sql = "update bi_t_item_cls set is_stop='1' where item_clsno='" + item_clsno + "' ";
            Program.db.ExecuteScalar(sql, null);
        }

        void IBLL.IGoods.StartCls(string item_clsno)
        {
            string sql = "update bi_t_item_cls set is_stop='0' where item_clsno='" + item_clsno + "' ";
            Program.db.ExecuteScalar(sql, null);
        }

        Dictionary<string, Model.bi_t_item_info> IBLL.IGoods.GetDic()
        {
            string sql = "select * from bi_t_item_info where display_flag<>'0' and display_flag<>'4' ";
            var tb = Program.db.ExecuteToTable(sql, null);
            Dictionary<string, Model.bi_t_item_info> dic = new Dictionary<string, Model.bi_t_item_info>();
            foreach (DataRow row in tb.Rows)
            {
                dic.Add(row["item_no"].ToString(), DB.ReflectionHelper.DataRowToModel<Model.bi_t_item_info>(row));
            }
            return dic;
        }

        decimal IBLL.IGoods.GetCost(string item_no, string branch_no)
        {
            string sql = "select * from ic_t_branch_stock where item_no='" + item_no + "' and branch_no='" + branch_no + "'";
            var tb = Program.db.ExecuteToTable(sql, null);
            if (tb.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                var row = tb.Rows[0];
                decimal cost_price = Conv.ToDecimal(row["cost_price"].ToString());
                if (cost_price < 0)
                {
                    return 0;
                }
                else
                {
                    return cost_price;
                }
            }
        }

        Model.bi_t_item_info IBLL.IGoods.GetItem(string item_no)
        {
            string sql = "select * from bi_t_item_info where item_no='" + item_no + "'";
            return Program.db.ExecuteToModel<Model.bi_t_item_info>(sql, null);
        }

        Model.bi_t_item_info IBLL.IGoods.GetItemByItemSubNo(string item_subno)
        {
            string sql = "select * from bi_t_item_info where item_subno='" + item_subno + "' ";
            var item = Program.db.ExecuteToModel<Model.bi_t_item_info>(sql, null);
            if (item == null)
            {
                sql = "select * from bi_t_item_info where barcode='" + item_subno + "' ";
                item = Program.db.ExecuteToModel<Model.bi_t_item_info>(sql, null);
            }
            return item;
        }

    }
}
