using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using NPinyin;

namespace IvyTran.BLL.ERP
{
    public class Item : IItem
    {
        #region 供应商报价

        /// <summary>
        /// Gets the item sup list.
        /// </summary>一商多供
        /// <param name="item_clsno">The item clsno.</param>
        /// <param name="sup_no">The sup no.</param>
        /// <param name="item_subno">The item subno.</param>
        /// <returns></returns>
        public DataTable GetItemSupList(string item_clsno,string sup_no,string item_subno)
        {
            string sql = @"select a.item_no,a.sup_no,b.item_subno,b.item_name
,c.sup_name,d.is_enabled,d.price
from [dbo].[bi_t_item_sup] a left join 
[dbo].[bi_t_item_info] b on a.item_no=b.item_no
left join [dbo].[bi_t_supcust_info] c on a.sup_no=c.supcust_no
left join [dbo].[bi_t_sup_item] d on a.item_no=d.item_no and d.sup_id=a.sup_no
left join [dbo].[bi_t_item_cls] e on e.item_clsno=b.item_clsno
where  1=1";
            if (Regex.IsMatch(sup_no, @"[\u4e00-\u9fa5]+"))
            {
                sql += " and c.sup_name like '%" + sup_no + "%'";
            }
            else
            {
                sql += " and c.supcust_no like '%" + sup_no + "%'";
            }
            if (Regex.IsMatch(item_clsno, @"[\u4e00-\u9fa5]+"))
            {
                sql += " and e.item_clsno like '%" + item_clsno + "%'";
            }
            else
            {
                sql += " and e.item_clsname like '%" + item_clsno + "%'";
            }
            if (Regex.IsMatch(item_subno, @"[\u4e00-\u9fa5]+"))
            {
                sql += " and b.item_subno like '%" + item_subno + "%'";
            }
            else
            {
                sql += " and b.item_name like '%" + item_subno + "%'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        public DataTable GetSupList(string sup_no)
        {
            string sql = "select * from bi_t_supcust_info where supcust_flag='S'";
            if (Regex.IsMatch(sup_no, @"[\u4e00-\u9fa5]+"))
            {
                sql += " and sup_name like '%" + sup_no + "%'";
            }
            else
            {
                sql += " and supcust_no like '%" + sup_no + "%'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        public DataTable GetSupItemList(string item_sub,string item_cls)
        {
            string sql = @"select * from [dbo].[bi_t_item_info] a left join 
                [dbo].[bi_t_item_cls] b on a.item_clsno = b.item_clsno where 1=1";
            if (Regex.IsMatch(item_sub, @"[\u4e00-\u9fa5]+"))
            {
                sql += " and a.item_subno like '%" + item_sub + "%'";
            }
            else
            {
                sql += " and a.item_name like '%" + item_sub + "%'";
            }
            if (Regex.IsMatch(item_cls, @"[\u4e00-\u9fa5]+"))
            {
                sql += " and b.item_clsno like '%" + item_sub + "%'";
            }
            else
            {
                sql += " and b.item_clsname like '%" + item_sub + "%'";
            }
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetItemClsList()
        {
            string sql = @"select * from bi_t_item_cls where len(item_clsno)=4";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        DataTable IItem.GetSupItemPrice()
        {
            string sql = "select * from bi_t_sup_item where is_supprice='1'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DataTable dt = db.ExecuteToTable(sql, null);
            return dt;
        }
        void IItem.UpdateSupSupItemPrice(List<bi_t_sup_item> bi)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();
                sql = "delete from bi_t_sup_item where is_supprice='1'";
                d.ExecuteScalar(sql, null);
                foreach (var item in bi)
                {
                    sql = "select isnull(Max(flow_id),0)+1 flow_id from bi_t_sup_item";
                    DataTable flow_id = d.ExecuteToTable(sql, null);
                    item.flow_id = flow_id.Rows[0][0].ToInt32();
                    d.Insert(item);
                }
                //提交
                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("UpdateSupSupItemPrice->UpdateSupSupItemPrice()", ex.ToString());
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        #endregion

        DataTable IItem.GetLikeItem_ChengBen(string sup_id, int sup_type, string item_subno, string type, string branch_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"select   *,0.00 valid_price,branch_stock.cost_price from bi_t_item_info  item_info
                            left join ic_t_branch_stock branch_stock on branch_stock.item_no=item_info.item_no
                        where 1=1 ";

            if (!string.IsNullOrEmpty(item_subno))
                sql += " and (item_info. item_subno like '%" + item_subno + "%' or item_info.item_no like '%" + item_subno + "%'   or item_info.barcode like '%" + item_subno + "%' )";

            if (!string.IsNullOrEmpty(branch_no))
                sql += " and branch_stock.branch_no='" + branch_no + " '";

            DataTable tb = db.ExecuteToTable(sql, null);

            IPriceBLL bll = new PriceBLL();

            foreach (DataRow dr in tb.Rows)
            {
                if (sup_type == 0)
                {
                    dr["valid_price"] = bll.GetCusItemPrice(sup_id, dr["item_no"].ToString(), type);
                }
                else if (sup_type == 1)
                {
                    dr["valid_price"] = bll.GetSupItemPrice(sup_id, dr["item_no"].ToString(), type);
                }
                else
                {
                    dr["valid_price"] = bll.GetLastInPrice(dr["item_no"].ToString());
                }
            }

            return tb;
        }

        DataTable IItem.GetItemStock_ChengBen(string branch_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);



            string sql = "";
            if (!string.IsNullOrEmpty(branch_no))
            {
                sql = @" SELECT branch_stock.*,item_info.price FROM ic_t_branch_stock branch_stock left join bi_t_item_info  
                item_info  on item_info .item_no=   branch_stock.item_no    
                WHERE branch_stock. branch_no = '" + branch_no + "' ";


            }
            else
            {
                sql = @" SELECT branch_stock.*,item_info.price FROM ic_t_branch_stock branch_stock left join bi_t_item_info  
                item_info  on item_info .item_no=   branch_stock.item_no    ";


            }
            DataTable dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        /// <summary>
        /// 商品近15天价格
        /// </summary>
        /// <param name="type">1 入库  0销售</param>
        /// <returns></returns>
        DataTable IItem.ItemPrice(int type)
        {
            string sql = "";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            if (type == 1)
            {
                 sql = $@"select a.item_no,c.item_subno,c.item_name,valid_price,oper_date from [dbo].[ic_t_inout_store_detail] a left join 
[dbo].[ic_t_inout_store_master] b on a.sheet_no=b.sheet_no
left join [dbo].[bi_t_item_info] c on a.item_no=c.item_no
where a.sheet_no like 'PI%' and (oper_date between '" + DateTime.Now.AddDays(-15) + "' and '" + DateTime.Now + "') order by oper_date desc";
            }

            if (type == 0)
            {
                 sql = $@"select a.item_no,c.item_subno,c.item_name,a.real_price,a.sale_price,a.cost_price,oper_date from [dbo].[sm_t_salesheet_detail] a left join 
[dbo].[sm_t_salesheet] b on a.sheet_no=b.sheet_no
left join [dbo].[bi_t_item_info] c on a.item_no=c.item_no
where a.sheet_no like 'SO%' and (oper_date between '" + DateTime.Now.AddDays(-15) + "' and '" + DateTime.Now + "') order by oper_date desc";
            }
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        System.Data.DataTable IItem.GetList(string item_clsno, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT a.item_no,
       a.item_subno,
       a.item_name,
       a.item_subname,
       a.item_clsno,
  
	   a.branch_no,
	   bo.branch_name,
       a.unit_no,
       a.item_size,
       a.product_area,
       a.item_brand,
       a.item_brandname,
       a.price,
       a.base_price,
       a.sale_price,
       a.combine_sta,
       a.item_flag,
       a.display_flag,
       a.sup_no,
       a.barcode,
       a.base_price2,
       a.base_price3,
       a.update_time,
       a.valid_day,
       a.cost_type,
       a.item_pack,
       a.bala_flag,
       a.order_unit,
       a.small_img_url,
       a.large_img_url,
       a.detail_img_url,
       a.is_show_mall,
       a.themes,
       a.group_type,
       a.group_main_item_no,
       a.child_item_subno,
       a.min_stock,
       a.markup_rate,
       a.item_loss,
       a.weight_diff,
       a.rate,
       a.rate2,
       a.rate3,
       a.formula_unit_no,
       a.item_property,
       a.process_type,
       a.is_mrp,
       a.item_bom,
a.factory_no+'/'+fac.sup_name factory_no,
       ie.is_batch,
	   CASE WHEN bi.item_no IS NULL THEN '0' ELSE '1' END is_bulk_item,
       b.item_clsname,
       c.sup_name
FROM bi_t_item_info a
    LEFT JOIN
    (SELECT * FROM bi_t_item_cls WHERE item_flag = '0') b
        ON a.item_clsno = b.item_clsno
    LEFT JOIN
    (SELECT * FROM bi_t_supcust_info WHERE supcust_flag = 'S') c
        ON a.sup_no = c.supcust_no
		LEFT JOIN dbo.bi_t_bulk_item bi ON bi.item_no = a.item_no
		LEFT JOIN dbo.bi_t_branch_info bo ON bo.branch_no=a.branch_no
        LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = a.item_no
LEFT JOIN
    (SELECT * FROM bi_t_supcust_info WHERE is_factory = '1') fac
        ON a.factory_no = fac.supcust_no
WHERE 1 = 1";
            if (item_clsno != "")
            {
                sql += " and a.item_clsno like '" + item_clsno + "%'";
            }
            if (keyword != "")
            {
                sql += " and (a.item_subno like '%@%' or a.item_name like '%@%' or a.barcode like '%@%')".Replace("@", keyword);
            }
            if (show_stop != 1)
            {
                sql += " and a.display_flag='1'";
            }

            var tb = db.ExecuteToTable(sql, "a.item_no", null, page_size, page_index, out total_count);
            return tb;
        }

        System.Data.DataTable IItem.GetItem(string item_no)
        {
            string sql = "select * from bi_t_item_info where item_no='" + item_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        bi_t_item_info IItem.GetItemBySubno(string item_subno)
        {
            string sql = "select * from bi_t_item_info where item_subno='" + item_subno + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            bi_t_item_info item = null;
            if (tb.Rows.Count > 0)
            {
                item = DB.ReflectionHelper.DataRowToModel<bi_t_item_info>(tb.Rows[0]);
            }
            return item;
        }

        string IItem.MaxCode()
        {
            string sql = "select  top 1 item_subno from bi_t_item_info where len(item_subno)=6 AND PATINDEX('%[^0-9]%', item_subno)=0 order by item_subno desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count == 0)
            {
                return "000001";
            }
            else
            {
                int index = Helper.Conv.ToInt32(tb.Rows[0]["item_subno"].ToString());
                index++;
                return index.ToString().PadLeft(6, '0');
            }
        }

        private string get_item_no()
        {
            string sql = "select max(item_no) as item_no from bi_t_item_info where len(item_no)=13 ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count == 0 || string.IsNullOrWhiteSpace(tb.Rows[0]["item_no"].ToString()))
            {
                return "1000000000001";
            }
            else
            {
                if (tb.Rows[0][0] == "" || string.IsNullOrEmpty(tb.Rows[0][0].ToString()))
                {
                    return "1000000000001";
                }
                else
                {
                    string item_no = tb.Rows[0]["item_no"].ToString();
                    char a = item_no[0];
                    if (a=='0')
                    {
                        item_no="1"+item_no.Substring(1, 12);
                    }
                    decimal index = item_no.ToDecimal();
                    index++;
                    return index.ToString();
                }
                
            }
        }

        void IItem.Add(bi_t_item_info item)
        {
            item.item_no = get_item_no();
            item.update_time = System.DateTime.Now;
            string sql = "select * from bi_t_item_info where item_subno='" + item.item_subno + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已存在货号" + item.item_subno);
            }
            db.Insert(item);

            //大宗商品
            db.ExecuteScalar($@"DELETE FROM dbo.bi_t_bulk_item WHERE item_no='{item.item_no}';", null);
            if ("1".Equals(item.is_bulk_item))
            {
                db.ExecuteScalar($@"INSERT INTO dbo.bi_t_bulk_item(item_no,create_time) VALUES('{item.item_no}',GETDATE());", null);
            }
        }

        public void Adds(List<bi_t_item_info> items)
        {
            string sql = "";
            var db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                int item_no = get_item_no().ToInt32();

                HashSet<string> itemSet = new HashSet<string>();
                var supNoInfos = new Dictionary<string, bi_t_supcust_info>();//key:编号
                var supNameInfos = new Dictionary<string, bi_t_supcust_info>();//key:名称
                var itemClsNos = new Dictionary<string, bi_t_item_cls>();//key:编号
                var itemClsNames = new Dictionary<string, bi_t_item_cls>();//key:名称
                {
                    var tb = d.ExecuteToTable("SELECT item_subno FROM dbo.bi_t_item_info", null);
                    foreach (DataRow row in tb.Rows)
                    {
                        itemSet.Add(row["item_subno"].ToString());
                    }

                    tb = d.ExecuteToTable("SELECT * FROM dbo.bi_t_supcust_info WHERE supcust_flag='S'", null);
                    foreach (DataRow row in tb.Rows)
                    {
                        var sup = ReflectionHelper.DataRowToModel<bi_t_supcust_info>(row);
                        supNoInfos.Add(sup.supcust_no, sup);
                        if (!supNameInfos.TryGetValue(sup.sup_name, out var value))
                        {
                            supNameInfos.Add(sup.sup_name, sup);
                        }
                    }

                    tb = d.ExecuteToTable("SELECT * FROM dbo.bi_t_item_cls", null);
                    foreach (DataRow row in tb.Rows)
                    {
                        var itemCls = ReflectionHelper.DataRowToModel<bi_t_item_cls>(row);
                        itemClsNos.Add(itemCls.item_clsno, itemCls);
                        if (!itemClsNames.TryGetValue(itemCls.item_clsname, out var value))
                        {
                            itemClsNames.Add(itemCls.item_clsname, itemCls);
                        }
                    }
                }


                foreach (var item in items)
                {
                    //货号
                    if (itemSet.Contains(item.item_subno))
                    {
                        throw new Exception($"货号:[{item.item_subno}]，已存在");
                    }

                    //供应商
                    if (supNoInfos.TryGetValue(item.sup_no, out var sup) || supNameInfos.TryGetValue(item.sup_no, out sup))
                    {
                        item.sup_no = sup.supcust_no;
                    }
                    else
                    {
                        item.sup_no = "00";
                    }

                    //分类
                    if (itemClsNos.TryGetValue(item.item_clsno, out var itemCls) || itemClsNames.TryGetValue(item.item_clsno, out itemCls))
                    {
                        item.item_clsno = itemCls.item_clsno;
                    }
                    else
                    {
                        item.item_clsno = "00";
                    }

                    item.item_no = (item_no++).ToString().PadLeft(13, '0');
                    item.item_subname = Pinyin.GetInitials(item.item_name);
                    item.update_time = DateTime.Now;
                }

                d.BulkCopy(items);

                db.CommitTran();
            }
            catch (Exception)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        void IItem.Change(bi_t_item_info item)
        {
            item.update_time = System.DateTime.Now;
            string sql = "select * from bi_t_item_info where item_subno='" + item.item_subno + "' and item_no<>'" + item.item_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已存在货号" + item.item_subno);
            }
            db.Update(item, "item_no");

            //大宗商品
            db.ExecuteScalar($@"DELETE FROM dbo.bi_t_bulk_item WHERE item_no='{item.item_no}';", null);
            if ("1".Equals(item.is_bulk_item))
            {
                db.ExecuteScalar($@"INSERT INTO dbo.bi_t_bulk_item(item_no,create_time) VALUES('{item.item_no}',GETDATE());", null);
            }
        }

        void IItem.Delete(string item_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "";
            //
            sql = "select  top 1 * from pm_t_price_flow_detial where item_no='" + item_no + "' ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("商品已被调价单引用，不能删除!");
            }
            //
            sql = "select  top 1 * from sm_t_salesheet_detail where item_no='" + item_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("商品已被批发销售单引用，不能删除!");
            }
            //
            sql = "select  top 1 * from ic_t_inout_store_detail where item_no='" + item_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("商品已被出入库单引用，不能删除!");
            }
            //
            sql = "select  top 1 * from ic_t_check_detail where item_no='" + item_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("商品已被盘点单引用，不能删除!");
            }
            //
            sql = "delete from bi_t_item_info where item_no='" + item_no + "'";

            db.ExecuteScalar(sql, null);
        }

        System.Data.DataTable IItem.GetListShort()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.item_no,a.item_subno,a.item_name,a.item_subname,a.item_size,a.unit_no,b.item_clsname from bi_t_item_info a" +
                " left join (select * from bi_t_item_cls where item_flag='0') b on a.item_clsno=b.item_clsno " +
                " where display_flag='1' order by a.item_no";
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        int IItem.IsUse(string item_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select  top 1 * from ic_t_branch_stock where item_no='" + item_no + "' ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                return 1;
            }
            sql = "select  top 1 * from ic_t_inout_store_detail where item_no='" + item_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                return 1;
            }
            return 0;
        }

        System.Data.DataTable IItem.GetList_BySup(string sup_no, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT a.item_no,
       a.item_subno,
       a.item_name,
       a.item_subname,
       a.item_clsno,
	   a.branch_no,
	   bo.branch_name,
       a.unit_no,
       a.item_size,
       a.product_area,
       a.item_brand,
       a.item_brandname,
       a.price,
       a.base_price,
       a.sale_price,
       a.combine_sta,
       a.item_flag,
       a.display_flag,
       a.sup_no,
       a.barcode,
       a.base_price2,
       a.base_price3,
       a.update_time,
       a.valid_day,
       a.cost_type,
       a.item_pack,
       a.bala_flag,
       a.order_unit,
       a.small_img_url,
       a.large_img_url,
       a.detail_img_url,
       a.is_show_mall,
       a.themes,
       a.group_type,
       a.group_main_item_no,
       a.child_item_subno,
       a.min_stock,
       a.markup_rate,
       a.item_loss,
       a.weight_diff,
       a.rate,
       a.rate2,
       a.rate3,
       a.formula_unit_no,
       a.item_property,
       a.process_type,
       a.is_mrp,
       a.item_bom,
       ie.is_batch,
	   CASE WHEN bi.item_no IS NULL THEN '0' ELSE '1' END is_bulk_item,
       b.item_clsname,
       c.sup_name
FROM bi_t_item_info a
    LEFT JOIN
    (SELECT * FROM bi_t_item_cls WHERE item_flag = '0') b
        ON a.item_clsno = b.item_clsno
    LEFT JOIN
    (SELECT * FROM bi_t_supcust_info WHERE supcust_flag = 'S') c
        ON a.sup_no = c.supcust_no
		LEFT JOIN dbo.bi_t_bulk_item bi ON bi.item_no = a.item_no
		LEFT JOIN dbo.bi_t_branch_info bo ON bo.branch_no=a.branch_no
        LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = a.item_no
WHERE 1 = 1 ";
            if (sup_no != "")
            {
                sql += " and a.sup_no='" + sup_no + "'";
            }
            if (keyword != "")
            {
                sql += " and (a.item_subno like '%@%' or a.item_name like '%@%' or a.barcode like '%@%')".Replace("@", keyword);
            }
            if (show_stop != 1)
            {
                sql += " and a.display_flag='1'";
            }


            var tb = db.ExecuteToTable(sql, "a.item_no", null, page_size, page_index, out total_count);
            return tb;
        }

        Dictionary<string, decimal> IItem.GetItemStock(string branch_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            Dictionary<string, decimal> item_dic = new Dictionary<string, decimal>();

            string sql = "";
            if (!string.IsNullOrEmpty(branch_no))
            {
                sql = @" SELECT * FROM ic_t_branch_stock WHERE branch_no = '" + branch_no + "' ";

                foreach (DataRow row in db.ExecuteToTable(sql, null).Rows)
                {
                    item_dic.Add(row["item_no"].ToString(), row["stock_qty"].ToDecimal());
                }
            }
            else
            {
                sql = @" SELECT * FROM ic_t_branch_stock  ";

                foreach (DataRow row in db.ExecuteToTable(sql, null).Rows)
                {
                    item_dic.Add(row["branch_no"].ToString() + "-" + row["item_no"].ToString(), row["stock_qty"].ToDecimal());
                }
            }

            return item_dic;
        }

        System.Data.DataTable IItem.GetBranchItemList(string item_no, string branch_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"select *,
(s.branch_no+'/'+b.branch_name) 仓库
from ic_t_branch_stock s
left join bi_t_branch_info b on b.branch_no=s.branch_no where 1=1 ";
            if (!string.IsNullOrEmpty(item_no))
                sql += " and s.item_no='" + item_no + "'  ";
            if (!string.IsNullOrEmpty(branch_no))
                sql += " and branch_no='" + branch_no + "' ";
            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        DataTable IItem.GetSheetItem(string sup_id, int sup_type, string item_no, string item_subno, string barcode, string type)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = @"select *,0.00 valid_price from bi_t_item_info where 1=1 ";

            if (!string.IsNullOrEmpty(item_no))
                sql += " and item_no = '" + item_no + "' ";
            if (!string.IsNullOrEmpty(item_subno))
                sql += " and item_subno = '" + item_subno + "' ";
            if (!string.IsNullOrEmpty(barcode))
                sql += " and barcode = '" + barcode + "' ";

            DataTable tb = db.ExecuteToTable(sql, null);

            IPriceBLL bll = new PriceBLL();

            foreach (DataRow dr in tb.Rows)
            {
                if (sup_type == 0)
                {
                    dr["valid_price"] = bll.GetCusItemPrice(sup_id, dr["item_no"].ToString(), type);
                }
                else if (sup_type == 1)
                {
                    dr["valid_price"] = bll.GetSupItemPrice(sup_id, dr["item_no"].ToString(), type);
                }
                else
                {
                    dr["valid_price"] = bll.GetLastInPrice(dr["item_no"].ToString());
                }
            }

            return tb;
        }

        DataTable IItem.GetLikeItem(string sup_id, int sup_type, string item_subno, string type, string branch_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT i.*,0.00 valid_price,isnull(s.stock_qty,0.00) stock_qty,b.branch_name,ie.is_batch
FROM dbo.bi_t_item_info i
LEFT JOIN dbo.ic_t_branch_stock  s ON i.item_no=s.item_no AND s.branch_no='{branch_no}' 
LEFT JOIN dbo.bi_t_branch_info b ON b.branch_no = i.branch_no
LEFT JOIN dbo.k3_t_item_info_expand ie ON ie.item_no = i.item_no
WHERE 1=1 ";

            if (!string.IsNullOrEmpty(item_subno))
                sql += " and ( i.item_subno like '%" + item_subno + "%'  or i.barcode like '%" + item_subno + "%' )";

            DataTable tb = db.ExecuteToTable(sql, null);

            IPriceBLL bll = new PriceBLL();

            foreach (DataRow dr in tb.Rows)
            {
                if (sup_type == 0)
                {
                    dr["valid_price"] = bll.GetCusItemPrice(sup_id, dr["item_no"].ToString(), type);
                }
                else if (sup_type == 1)
                {
                    dr["valid_price"] = bll.GetSupItemPrice(sup_id, dr["item_no"].ToString(), type);
                }
                else
                {
                    dr["valid_price"] = bll.GetLastInPrice(dr["item_no"].ToString());
                }
            }

            return tb;
        }

        public DataTable QuickItem(string item_subno)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT i.*
FROM dbo.bi_t_item_info i
WHERE 1=1 ";

            if (!string.IsNullOrEmpty(item_subno))
                sql += " and ( i.item_subno like '%" + item_subno + "%'  or i.barcode like '%" + item_subno + "%' )";

            DataTable tb = db.ExecuteToTable(sql, null);

            IPriceBLL bll = new PriceBLL();

            foreach (DataRow dr in tb.Rows)
            {
                dr["price"] = bll.GetLastInPrice(dr["item_no"].ToString());
            }

            return tb;
        }

        DataTable IItem.QuickSearchList(string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select top 200 item_no,item_subno,item_name from bi_t_item_info ";
            sql += "where display_flag<>'0' AND display_flag<>'4' and (item_subno like '%'+@keyword+'%' or item_name like '%'+@keyword+'%') ";
            sql += "order by item_subno ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@keyword", keyword),
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        DataTable IItem.SearchAllItemList(string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select item_no,item_subno,item_name,item_size,unit_no,item_clsno,barcode ";
            sql += "from bi_t_item_info ";
            sql += "where display_flag<>'0' AND display_flag<>'4' ";
            if (keyword != "")
            {
                sql += "and (item_subno like '%'+@keyword+'%' or item_name like '%'+@keyword+'%') ";
            }
            sql += "order by item_subno ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@keyword", keyword),
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        List<bi_t_item_info> IItem.GetALL()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            List<bi_t_item_info> lis =
                db.ExecuteToList<bi_t_item_info>("SELECT * FROM dbo.bi_t_item_info WHERE display_flag='1'", null);
            return lis;
        }

        DataTable IItem.GetThemeList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select theme_id,theme_code,theme_name from tr_theme where is_choose='1' ";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public bool addK3(string item_no, string type)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            try
            {
                string sql = "select item_no from[dbo].[bi_t_item_info] where item_subno='" + item_no + "' ";
                var d = db.ExecuteToTable(sql, null);
                sql = "select * from k3_t_item_info_expand where item_no='" + d.Rows[0]["item_no"] + "' ";

                var dt = db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    sql = "update k3_t_item_info_expand set is_batch='" + type + "' where  item_no='" + d.Rows[0]["item_no"] + "' ";
                    db.ExecuteScalar(sql, null);
                }
                else
                {
                    sql = "insert into k3_t_item_info_expand values('" + d.Rows[0]["item_no"] + "','','" + type + "')";
                    db.ExecuteScalar(sql, null);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        public bool GetK3(string item_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            try
            {
                string sql = "select item_no from[dbo].[bi_t_item_info] where item_subno='" + item_no + "' ";
                var d = db.ExecuteToTable(sql, null);
                sql = "select isnull(is_batch,0) is_batch from k3_t_item_info_expand where item_no='" + d.Rows[0]["item_no"] + "' ";

                var dt = db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["is_batch"].ToString() == "1")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
