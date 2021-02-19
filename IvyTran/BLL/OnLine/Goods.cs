using System;
using System.Collections.Generic;
using System.Data;
using DB;
using IvyTran.BLL.ERP;
using IvyTran.DAL;
using IvyTran.Helper;
using IvyTran.IBLL;
using IvyTran.IBLL.ERP;
using IvyTran.IBLL.OnLine;
using Model;

namespace IvyTran.BLL.OnLine
{
    public class Goods : IGoods
    {
        DataTable IGoods.GetList(string cls_no, int pageSize, int pageIndex, out int total)
        {
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;

            var condition_sql = "where status='1' ";
            if (cls_no != "") condition_sql += " and cls_id in (select cls_id from goods_cls where cls_no like ''+@cls_no+'%') ";

            string sql = "select a.goods_id,a.goods_no,a.goods_name,a.cls_id,isnull(b.cls_name,'') cls_name";
            sql += ",0 stock_qty,a.status,isnull(is_show_mall,'0') is_show_mall,isnull(themes,'') themes from (";
            sql += "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY goods_no,goods_id) AS rowid,* ";
            sql += "FROM goods " + condition_sql + ") t ";
            sql += "WHERE t.rowid > " + (pageIndex - 1) * pageSize + " AND t.rowid <= " + pageIndex * pageSize + ") a ";
            sql += "left join goods_cls b on a.cls_id=b.cls_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cls_no",cls_no)
            };
            var dt = d.ExecuteToTable(sql, pars);


            sql = "select 1 from goods " + condition_sql;
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cls_no",cls_no)
            };

            var dt1 = d.ExecuteToTable(sql, pars);
            total = dt1.Rows.Count;
            return dt;
        }

        DataTable IGoods.SelectKeyword(string cls_no, string keyword, string theme, string is_no_show_mall, int pageSize, int pageIndex, out int total)
        {
            //
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var con_str = "";
            if (keyword != null && keyword != "")
            {
                con_str += " and (goods_name like '%'+@keyword+'%' or goods_no=@keyword or barcode=@keyword) ";
            }
            if (theme != "" && theme != "0")
            {
                con_str += " and ','+themes like '%,'+@theme+',%' ";
            }
            if (cls_no != "")
            {
                con_str += " and cls_id in (select cls_id from goods_cls where cls_no like ''+@cls_no+'%') ";
            }
            if (is_no_show_mall == "1")
            {
                con_str += " and isnull(is_show_mall,'0')='0' ";
            }
            string sql = "select a.goods_id,a.goods_no,a.goods_name,a.cls_id,a.status,isnull(a.is_show_mall,'0') is_show_mall";
            sql += ",isnull(b.cls_name,'') cls_name,isnull(a.themes,'') themes from (";
            sql += "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY isnull(is_show_mall,'0') desc,goods_no,goods_id) AS rowid,* ";
            sql += "FROM goods where status='1' " + con_str + ") t ";
            sql += "WHERE t.rowid > " + (pageIndex - 1) * pageSize + " AND t.rowid <= " + pageIndex * pageSize + ") a ";
            sql += "left join goods_cls b on a.cls_id=b.cls_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cls_no",cls_no),
                new System.Data.SqlClient.SqlParameter("@theme",theme),
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };
            var dt = db.ExecuteToTable(sql, pars);

            sql = "select 1 from goods where status='1' " + con_str;
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cls_no",cls_no),
                new System.Data.SqlClient.SqlParameter("@theme",theme),
                new System.Data.SqlClient.SqlParameter("@keyword",keyword)
            };
            var dt1 = db.ExecuteToTable(sql, pars);
            //

            total = dt1.Rows.Count;
            return dt;
        }

        void IGoods.Add(goods goods, List<goods_std> goods_stds)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                global::DAL.bi_identity identityDAL = new global::DAL.bi_identity(db);
                goods.goods_id = "";// identityDAL.GetId("goods");
                goods.status = "2";
                d.Insert(goods);
                foreach (goods_std goods_std in goods_stds)
                {
                    try
                    {
                        goods_std.goods_id = goods.goods_id;
                        d.Insert(goods_std);
                    }
                    catch (Exception e)
                    {
                        LogHelper.writeLog("Goods ->Add(1)", e.ToString(), goods.goods_id.ToString());
                        throw new ExceptionBase(goods.goods_name + "商品重复请修改后重新保存！");
                    }
                }
                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("Goods ->Add(2)", ex.ToString(), null);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IGoods.SelectById(string goods_id, out goods goods, out goods_cls cls, out DataTable dt)
        {
            try
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DB.IDB d = db;
                goodsDAL goodsDal = new goodsDAL(db);
                //
                var item = goodsDal.SelectById(goods_id);
                if (item == null)
                {
                    throw new ExceptionBase("商品【" + goods_id + "】不存在");
                }
                goods = item;
                //
                var sql = "select * from goods_cls where cls_id = @cls_id";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cls_id",goods.cls_id)
                };
                cls = d.ExecuteToModel<goods_cls>(sql, pars);
                if (cls == null)
                {
                    throw new ExceptionBase("品类【" + goods.cls_id + "】不存在");
                }
                string sql_s_goods_std = "select * from goods_std where goods_id = @goods_id";
                var pars_s_goods_std = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@goods_id",goods_id)
                };
                dt = d.ExecuteToTable(sql_s_goods_std, pars_s_goods_std);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods->SelectById()", ex.ToString(), goods_id);
                throw ex;
            }
        }

        void IGoods.SelectByNo(string goods_no, out goods goods, out goods_cls cls, out DataTable dt)
        {
            try
            {
                var db = new DB.DBByAutoClose(AppSetting.conn);
                DB.IDB d = db;
                string sql = "select * from goods where goods_no=@goods_no or barcode=@goods_no ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@goods_no",goods_no)
                };
                goods = d.ExecuteToModel<goods>(sql, pars);
                if (goods == null)
                {
                    throw new ExceptionBase("商品[" + goods_no + "]不存在");
                }
                sql = "select * from goods_cls where cls_id = @cls_id";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cls_id",goods.cls_id)
                };
                cls = d.ExecuteToModel<goods_cls>(sql, pars);
                if (cls == null)
                {
                    throw new ExceptionBase("类品[" + goods.cls_id + "]不存在");
                }
                sql = "select * from goods_std where goods_id = @goods_id ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@goods_id",goods.goods_id)
                };
                dt = d.ExecuteToTable(sql, pars);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void IGoods.Change(goods goods, List<goods_std> goods_stds)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            //检查分类
            try
            {
                db.Open();
                db.BeginTran();
                var sql = "select * from goods_cls where cls_id = @cls_id";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@cls_id",goods.cls_id)
                };
                var cls = d.ExecuteToModel<goods_cls>(sql, pars);
                if (cls == null)
                {
                    throw new ExceptionBase("不存在分类");
                }
                goodsDAL gDAL = new goodsDAL(db);
                string goods_id = goods.goods_id;
                var goodsD = gDAL.SelectById(goods_id);
                if (goodsD == null)
                {
                    throw new ExceptionBase("不存在该商品！");
                }
                //删除原图片
                IImageClient bll = new ImageClient();
                if (!goods.small_img_url.Equals(goodsD.small_img_url))
                {
                    if (goodsD.small_img_url != null && goodsD.small_img_url.Length != 0)
                    {
                        bll.Delete(goodsD.small_img_url);
                    }
                }
                if (!goods.large_img_url.Equals(goodsD.large_img_url) && goodsD.large_img_url != null && goodsD.large_img_url != "")
                {
                    string[] large = goods.large_img_url.Split(',', '，');
                    string[] largeD = goodsD.large_img_url.Split(',', '，');
                    int length = large.Length > largeD.Length ? largeD.Length : large.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (largeD.Length <= i) break;
                        if (largeD[i] == null || largeD[i].Length == 0)
                        {
                            continue;
                        }
                        if (large[i].Equals(largeD[i]) == false)
                        {
                            bll.Delete(largeD[i]);
                        }
                    }
                }
                if (!goods.detail_img_url.Equals(goodsD.detail_img_url) && goodsD.detail_img_url != null && goodsD.detail_img_url != "")
                {
                    string[] large = goods.detail_img_url.Split(',', '，');
                    string[] largeD = goodsD.detail_img_url.Split(',', '，');
                    int length = large.Length > largeD.Length ? largeD.Length : large.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (largeD.Length <= i) break;
                        if (largeD[i] == null || largeD[i].Length == 0)
                        {
                            continue;
                        }
                        if (large[i].Equals(largeD[i]) == false)
                        {
                            bll.Delete(largeD[i]);
                        }
                    }
                }
                //保存
                sql = "update bi_t_item_info set small_img_url=@small_url,large_img_url=@large_url,detail_img_url=@detail_url";
                sql += ",is_show_mall=@is_show_mall,themes=@themes,update_time=getdate() where item_no=@goods_id ";
                pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@small_url",goods.small_img_url),
                    new System.Data.SqlClient.SqlParameter("@large_url",goods.large_img_url),
                    new System.Data.SqlClient.SqlParameter("@detail_url",goods.detail_img_url),
                    new System.Data.SqlClient.SqlParameter("@is_show_mall",goods.is_show_mall),
                    new System.Data.SqlClient.SqlParameter("@themes",goods.themes),
                    new System.Data.SqlClient.SqlParameter("@goods_id",goods.goods_id)
                };
                d.ExecuteScalar(sql, pars);

                /*
                d.Update(goods, "goods_id", "goods_no,goods_name,long_name,cls_id,small_img_url,large_img_url,detail_img_url,themes,text,is_show_mall");
                
                goods_stdDAL goods_stdDal = new goods_stdDAL(db);
                goods_stdDal.DeleteById(goods_id);
                foreach (goods_std goods_std in goods_stds)
                {
                    try
                    {
                        goods_std.goods_id = goods.goods_id;
                        d.Insert(goods_std);
                    }
                    catch (Exception)
                    {
                        throw new ExceptionBase(goods.goods_name + "商品重复请修改后重新保存！");
                    }
                }
                */
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods ->Change()", ex.ToString(), null);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void IGoods.ChangeStock(goods goods)
        {
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            //检查分类
            global::DAL.goods goodsDAL = new global::DAL.goods(db);
            var item = goodsDAL.SelectById(goods.goods_id);
            if (item == null)
            {
                throw new ExceptionBase("不存在");
            }
            global::DAL.goods_cls clsDAL = new global::DAL.goods_cls(db);
            var cls = clsDAL.SelectById(item.cls_id);
            if (cls == null)
            {
                throw new ExceptionBase("不存在分类");
            }
            d.Update(goods, "goods_id", "stock_qty");
        }

        void IGoods.ChangePriceShort(goods goods)
        {
            var db = new DB.DBByAutoClose(AppSetting.conn);
            DB.IDB d = db;
            //检查分类
            d.Update(goods, "goods_id", "prices");
        }

        void IGoods.Delete(string goods_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            global::DAL.goods goodsDAL = new global::DAL.goods(db);
            //判断是否存在
            var item = goodsDAL.SelectById(goods_id);
            if (item == null)
            {
                throw new ExceptionBase("不存在");
            }
            //判断是否合法
            global::DAL.goods_cls goodsclsDAL = new global::DAL.goods_cls(db);
            var cls = goodsclsDAL.SelectById(item.cls_id);
            if (cls == null)
            {
                throw new ExceptionBase("非法删除");
            }
            //判断是否被其他地方引用
            string sql1 = "select * from tr_order_item where goods_id = '" + goods_id + "' ";
            var order_item = db.ExecuteToTable(sql1, null);
            string sql2 = "select * from tr_shopcart where goods_id = '" + goods_id + "' ";
            var tr_shopcart = db.ExecuteToTable(sql2, null);
            int total = order_item.Rows.Count + tr_shopcart.Rows.Count;
            if (total == 0)
            {
                goodsDAL.DeleteById(goods_id);
            }
            else
            {
                throw new ExceptionBase("存在其他引用不能被删除");
            }

        }

        void IGoods.Stop(string goods_id)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                global::DAL.goods goodsDAL = new global::DAL.goods(db);
                //判断是否存在
                var item = goodsDAL.SelectById(goods_id);
                if (item == null)
                {
                    throw new ExceptionBase("不存在");
                }
                //判断是否合法
                global::DAL.goods_cls goodsclsDAL = new global::DAL.goods_cls(db);
                var cls = goodsclsDAL.SelectById(item.cls_id);
                if (cls == null)
                {
                    throw new ExceptionBase("非法数据");
                }

                db.ExecuteScalar("update bi_t_item_info set is_show_mall='0' where item_no='" + goods_id + "' ", null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods ->Stop()", ex.ToString(), goods_id);
                throw ex;
            }
        }

        void IGoods.Start(string goods_id)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                global::DAL.goods goodsDAL = new global::DAL.goods(db);
                //判断是否存在
                var item = goodsDAL.SelectById(goods_id);
                if (item == null)
                {
                    throw new ExceptionBase("不存在");
                }
                //判断是否合法
                global::DAL.goods_cls goodsclsDAL = new global::DAL.goods_cls(db);
                var cls = goodsclsDAL.SelectById(item.cls_id);
                if (cls == null)
                {
                    throw new ExceptionBase("非法数据");
                }
                //
                db.ExecuteScalar("update bi_t_item_info set is_show_mall='1' where item_no='" + goods_id + "' ", null);
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods ->Start()", ex.ToString(), goods_id);
                throw ex;
            }
        }

        void IGoods.InputSpe(goods_std goods_std)
        {

            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                goods_stdDAL goods_stdDAL = new DAL.goods_stdDAL(db);
                if (goods_stdDAL.SelectById(goods_std.goods_id) == null)
                {
                    goods_std.is_default = "1";
                }
                else
                {
                    goods_std.is_default = "0";
                }
                if (goods_stdDAL.SelectById(goods_std.goods_id) == null)
                {
                    d.Insert(goods_std);
                }
                else
                {
                    d.Update(goods_std, "goods_id", "goods_id,prices");
                }
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

        public static string GetClsId(string cls_name)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql_s_goods_cls = "select cls_id from goods_cls where cls_name = @cls_name";
            var pars_s_goods_cls = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cls_name",cls_name)
            };
            var dt_s_goods_cls = db.ExecuteToTable(sql_s_goods_cls, pars_s_goods_cls);
            if (dt_s_goods_cls.Rows.Count == 0)
            {
                throw new ExceptionBase("该分类：" + cls_name + "不存在！");
            }
            return dt_s_goods_cls.Rows[0]["cls_id"].ToString();
        }

        DataTable IGoods.GetThemeList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select theme_id,theme_code,theme_name from tr_theme where is_choose='1' ";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }


        bool GetSaleSpecialPrice(string branch_no, string item_no, out decimal price)
        {
            price = 0;
            string dDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            string strTime = System.DateTime.Now.ToString("HHmmss");
            string sql = "select * from pos_t_spec_price where " +
            "(branch_no='" + branch_no + "' or branch_no = '*' OR branch_no = '00')" +
            " and convert(varchar(10),start_date,120)<='" + dDate + "'" +
            " and convert(varchar(10),end_date,120)>='" + dDate + "'" +
            " and start_time<='" + strTime + "'" +
            " and end_time>='" + strTime + "'" +
            " and is_stop='0'" +
            " and item_no='" + item_no + "'" +
            " and special_type='0'" +
            " order by update_time desc";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var dt = db.ExecuteToTable(sql, null);
            foreach (DataRow row in dt.Rows)
            {
                if (row["branch_no"].ToString() == branch_no)
                {
                    decimal.TryParse(row["spe_price"].ToString(), out price);
                    return true;
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                if (row["branch_no"].ToString() == "*" || row["branch_no"].ToString() == "00")
                {
                    decimal.TryParse(row["spe_price"].ToString(), out price);
                    return true;
                }
            }
            return false;
        }

        public bool GetStock(string branch_no, string keyword, out bi_t_item_info goods,
            out decimal stock_qty, out decimal sale_price, out decimal spec_price)
        {
            string sql = "select * from bi_t_item_info where barcode='" + keyword + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var item = db.ExecuteToModel<bi_t_item_info>(sql, null);
            if (item == null)
            {
                sql = "select * from bi_t_item_info where item_subno='" + keyword + "'";
                item = db.ExecuteToModel<bi_t_item_info>(sql, null);
                if (item == null)
                {
                    sql = "select item_no from bi_t_item_barcode where barcode='" + keyword + "'";
                    object obj;
                    obj = db.ExecuteScalar(sql, null);
                    if (obj != null)
                    {
                        sql = "select * from bi_t_item_info where item_no='" + obj.ToString() + "'";
                        item = db.ExecuteToModel<bi_t_item_info>(sql, null);
                    }
                }

            }
            //
            if (item != null)
            {
                if (item.combine_sta == "3")
                {
                    sql = "select * from bi_t_item_pack_detail where master_no='" + item.item_no + "'";
                    var tb = db.ExecuteToTable(sql, null);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow row = tb.Rows[0];
                        sql = "select * from bi_t_item_info where item_no='" + row["item_no"].ToString() + "'";
                        item = db.ExecuteToModel<bi_t_item_info>(sql, null);
                    }
                }
            }
            //
            if (item != null)
            {
                goods = item;
                decimal dec = 0;
                if (1 == 1)
                {
                    sql = "select stock_qty from ic_t_branch_stock where item_no='" + item.item_no + "'" +
                    " and branch_no='" + branch_no + "'";
                    object obj = db.ExecuteScalar(sql, null);
                    if (obj != null)
                    {
                        decimal.TryParse(obj.ToString(), out dec);
                    }
                }
                if (1 == 1)
                {
                    sql = @"select sum(case when sell_way='A' then -1*sale_qnty 
when sell_way='B' then 1*sale_qnty else -1*sale_qnty end) as qty from pos_t_saleflow where item_no='" + item.item_no +
 "' and approve_flag='0'";
                    object obj = db.ExecuteScalar(sql, null);
                    decimal val = 0;
                    if (obj != null)
                    {
                        decimal.TryParse(obj.ToString(), out val);
                    }
                    dec += val;
                }
                stock_qty = dec;
                //
                if (1 == 1)
                {
                    dec = 0;
                    sql = "select sale_price from bi_t_item_price where item_no='" + item.item_no + "'" +
                    " and branch_no='" + branch_no + "'";
                    object obj = db.ExecuteScalar(sql, null);

                    if (obj != null)
                    {
                        decimal.TryParse(obj.ToString(), out dec);
                    }
                }

                if (dec == 0)
                {
                    dec = item.sale_price;
                }
                sale_price = dec;
                //
                if (true)
                {
                    if (GetSaleSpecialPrice(branch_no, item.item_no, out dec) == true)
                    {
                        spec_price = dec;
                    }
                    else
                    {
                        spec_price = sale_price;
                    }
                }
                return true;
            }
            else
            {
                goods = null;
                stock_qty = 0;
                sale_price = 0;
                spec_price = 0;
                return false;
            }
        }

        public DataTable GetList()
        {
            try
            {
                string sql = @"select item_no,
item_subno,
item_name,
unit_no,
item_size,
barcode,
price,
sale_price,
item_subname,
item_flag,
combine_sta,
sup_no from bi_t_item_info where display_flag in('1','2','3')";
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var tb = db.ExecuteToTable(sql, null);
                return tb;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods ->GetList()", ex.ToString());
                throw ex;
            }
        }

        public DataTable GetBarcodeList()
        {
            try
            {
                string sql = "select * from bi_t_item_barcode";
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var tb = db.ExecuteToTable(sql, null);
                return tb;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods->GetBarcodeList()", ex.ToString());
                throw ex;
            }


        }

        public DataTable GetPackList()
        {
            try
            {
                string sql = "select * from bi_t_item_pack_detail";
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var tb = db.ExecuteToTable(sql, null);
                return tb;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods->GetPackList()", ex.ToString());
                throw ex;
            }


        }

        public DataTable GetStockList(string branch_no)
        {
            try
            {
                string sql = "select * from ic_t_branch_stock where branch_no='" + branch_no + "'";
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var tb = db.ExecuteToTable(sql, null);
                return tb;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods->GetStockList()", ex.ToString(), branch_no);
                throw ex;
            }

        }


        public string GetStockQty(string item_no)
        {

            try
            {
                string sql = "";
                if (!(string.IsNullOrEmpty(item_no) || item_no == ""))
                {
                    sql = "select sum(stock_qty) from ic_t_branch_stock where item_no='" + item_no + "'";
                }
                IDB db = new DBByAutoClose(AppSetting.conn);
                return db.ExecuteScalar(sql, null).ToString(); //后4位
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods->GetStockQty", ex.ToString(), item_no);
                throw ex;
            }
        }

        public DataTable GetStockQty2(string item_no)
        {

            try
            {
                string sql = "select a.sup_name,b.* from bi_t_supcust_info a left join bi_t_item_info b " +
                    "on a.supcust_no=b.sup_no where b.item_no='" + item_no + "' or b.barcode='" + item_no + "'";
                IDB db = new DBByAutoClose(AppSetting.conn);
                var tb = db.ExecuteToTable(sql, null);
                return tb;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods->GetStockQty", ex.ToString(), item_no);
                throw ex;
            }
        }

        public string GetStockQty1(string item_name)
        {

            try
            {
                string sql = "select top(1) item_no from bi_t_item_info where item_name='" + item_name + "'";
                IDB db = new DBByAutoClose(AppSetting.conn);

                return db.ExecuteScalar(sql, null).ToString(); //后4位
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Goods->GetStockQty", ex.ToString(), item_name);
                throw ex;
            }
        }

        public bi_t_item_info GetOne(string item_no)
        {
            string sql = "select * from bi_t_item_info where item_no='" + item_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToModel<bi_t_item_info>(sql, null);
        }

    }
}