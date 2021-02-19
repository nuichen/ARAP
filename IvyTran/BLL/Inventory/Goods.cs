using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IvyTran.body.Inventory;
using IvyTran.Helper;
using IvyTran.IBLL.Inventory;

namespace IvyTran.BLL.Inventory
{
    public class Goods : IGoods
    {
        private static Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();
        public void ClearGoods()
        {
            string sql = "delete from pda_bi_t_item_info";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.ExecuteScalar(sql, null);
        }

        public void Insert(List<pda_bi_t_item_info> lstgoods)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            StringBuilder sb = new StringBuilder();
            try
            {
                d.Open();
                d.BeginTran();
                //

                string str1 = "insert into pda_bi_t_item_info(item_no,item_subno,item_name,unit_no,item_size," +
                "barcode,price,sale_price,item_subname,item_flag,combine_sta)values";
                foreach (pda_bi_t_item_info item in lstgoods)
                {
                    sb.Clear();

                    sb.Append(str1);
                    sb.Append("('");
                    sb.Append(item.item_no);
                    sb.Append("','");
                    sb.Append(item.item_subno);
                    sb.Append("','");
                    sb.Append(item.item_name);
                    sb.Append("','");
                    sb.Append(item.unit_no);
                    sb.Append("','");
                    sb.Append(item.item_size);
                    sb.Append("','");
                    sb.Append(item.barcode);
                    sb.Append("',");
                    sb.Append(item.price);
                    sb.Append(",");
                    sb.Append(item.sale_price);
                    sb.Append(",'");
                    sb.Append(item.item_subname);
                    sb.Append("','");
                    sb.Append(item.item_flag);
                    sb.Append("','");
                    sb.Append(item.combine_sta);
                    sb.Append("')");
                    db.ExecuteScalar(sb.ToString(), null);
                }

                //
                d.CommitTran();
            }
            catch (Exception)
            {
                LogHelper.writeLog("", sb.ToString(), "");
                d.RollBackTran();
                throw;
            }
            finally
            {
                d.Close();
            }
        }

        public DataTable GetList(string keyword)
        {
            string sql;
            if (keyword == "")
            {
                sql = "select * from pda_bi_t_item_info";
            }
            else
            {
                sql = "select * from pda_bi_t_item_info where (item_no like '%@a%' or item_name like '%@a%' or barcode like '%@a%')".Replace("@a", keyword);
            }
            sql += " order by item_no";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetListByTop(string batch, int top, out string batch2)
        {
            if (batch == "")
            {
                string sql = "select * from pda_bi_t_item_info order by item_no";
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var tb = db.ExecuteToTable(sql, null);
                batch2 = System.Guid.NewGuid().ToString().Replace("-", "");

                //
                var dt1 = tb.Clone();
                var dt2 = tb.Clone();
                int cnt = 0;
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    cnt++;
                    if (cnt <= top)
                    {
                        dt1.Rows.Add(tb.Rows[i].ItemArray);
                    }
                    else
                    {
                        dt2.Rows.Add(tb.Rows[i].ItemArray);
                    }
                }
                dic.Add(batch2, dt2);
                return dt1;
            }
            else
            {
                var tb = new DataTable();
                if (dic.TryGetValue(batch, out tb) == true)
                {
                    var dt1 = tb.Clone();
                    var dt2 = tb.Clone();
                    int cnt = 0;
                    for (int i = 0; i < tb.Rows.Count; i++)
                    {
                        cnt++;
                        if (cnt <= top)
                        {
                            dt1.Rows.Add(tb.Rows[i].ItemArray);
                        }
                        else
                        {
                            dt2.Rows.Add(tb.Rows[i].ItemArray);
                        }
                    }
                    batch2 = batch;
                    tb.Clear();
                    foreach (DataRow row in dt2.Rows)
                    {
                        tb.Rows.Add(row.ItemArray);
                    }
                    return dt1;
                }
                else
                {
                    throw new Exception("未找到缓存" + this.GetType().ToString());
                }
            }
        }

        public pda_bi_t_item_info GetOne(string keyword)
        {
            string sql = "select * from pda_bi_t_item_info where barcode='" + keyword + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var item = db.ExecuteToModel<pda_bi_t_item_info>(sql, null);
            if (item == null)
            {
                sql = "select * from  pda_bi_t_item_info where item_subno='" + keyword + "'";
                item = db.ExecuteToModel<pda_bi_t_item_info>(sql, null);
                if (item == null)
                {
                    sql = "select item_no from pda_bi_t_item_barcode where barcode='" + keyword + "'";
                    object obj;
                    obj = db.ExecuteScalar(sql, null);
                    if (obj != null)
                    {
                        sql = "select * from pda_bi_t_item_info where item_no='" + obj.ToString() + "'";
                        item = db.ExecuteToModel<pda_bi_t_item_info>(sql, null);
                    }
                }

            }
            //
            if (item != null)
            {
                if (item.combine_sta == "3")
                {
                    sql = "select * from pda_bi_t_item_pack_detail where master_no='" + item.item_no + "'";
                    var tb = db.ExecuteToTable(sql, null);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow row = tb.Rows[0];
                        sql = "select * from pda_bi_t_item_info where item_no='" + row["item_no"].ToString() + "'";
                        item = db.ExecuteToModel<pda_bi_t_item_info>(sql, null);
                        if (item != null)
                        {
                            item.add_qty = Conv.ToDecimal(row["pack_num"].ToString());
                            item.master_no = row["master_no"].ToString();
                        }
                    }
                }
            }
            //
            if (item != null)
            {
                if (item.add_qty == 0)
                {
                    item.add_qty = 1;
                }
            }

            return item;
        }

        public DataTable GetBarCodeList()
        {
            string sql;

            sql = "select * from pda_bi_t_item_barcode";


            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public DataTable GetPackList()
        {
            string sql;

            sql = "select * from pda_bi_t_item_pack_detail";


            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public void ClearBarcode()
        {
            string sql = "delete from pda_bi_t_item_barcode";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.ExecuteScalar(sql, null);
        }

        public void Insert(List<pda_bi_t_item_barcode> lstbarcode)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            try
            {
                d.Open();
                d.BeginTran();
                //

                string str1 = "insert into pda_bi_t_item_barcode(item_no,barcode)values";
                foreach (pda_bi_t_item_barcode item in lstbarcode)
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append(str1);
                    sb.Append("('");
                    sb.Append(item.item_no);
                    sb.Append("','");
                    sb.Append(item.barcode);
                    sb.Append("')");
                    db.ExecuteScalar(sb.ToString(), null);
                    sb = null;
                }


                //
                d.CommitTran();
            }
            catch (Exception)
            {
                d.RollBackTran();
                throw;
            }
            finally
            {
                d.Close();
            }
        }

        public void ClearPack()
        {
            string sql = "delete from pda_bi_t_item_pack_detail";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.ExecuteScalar(sql, null);
        }

        public void Insert(List<pda_bi_t_item_pack_detail> lstpack)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            try
            {
                d.Open();
                d.BeginTran();
                //

                string str1 = "insert into pda_bi_t_item_pack_detail(master_no,item_no,pack_num)values";
                foreach (pda_bi_t_item_pack_detail item in lstpack)
                {

                    StringBuilder sb = new StringBuilder();
                    sb.Append(str1);
                    sb.Append("('");
                    sb.Append(item.master_no);
                    sb.Append("','");
                    sb.Append(item.item_no);
                    sb.Append("',");
                    sb.Append(item.pack_num);
                    sb.Append(")");
                    db.ExecuteScalar(sb.ToString(), null);
                    sb = null;
                }


                //
                d.CommitTran();
            }
            catch (Exception)
            {
                d.RollBackTran();
                throw;
            }
            finally
            {
                d.Close();
            }
        }
    }
}