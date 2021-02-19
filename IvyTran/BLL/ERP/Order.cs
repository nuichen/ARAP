using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DB;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class Order : IOrder
    {
        /// <summary>
        /// 下载出货单-订单
        /// </summary>
        /// <param name="sheet">单据号</param>
        /// <param name="tbMain">单头</param>
        /// <param name="tbDetail">单详细</param>
        /// <returns>bool</returns>
        /// <returns>bool</returns>
        public bool GetOutPut(string sheet_no, out System.Data.DataTable tbMain, out System.Data.DataTable tbDetail)
        {
            IDB db = new DBByAutoClose(AppSetting.conn);
            string sql = @" SELECT co_t_order_main.sheet_no,     
    co_t_order_main.sup_no,   
    co_t_order_main.branch_no,  
    co_t_order_main.oper_date, 
    co_t_order_main.trans_no     
    FROM co_t_order_main,   
         bi_t_branch_info,   
         bi_t_supcust_info  
   WHERE ( co_t_order_main.branch_no = bi_t_branch_info.branch_no ) and
         ( co_t_order_main.sup_no = bi_t_supcust_info.supcust_no ) and
			( bi_t_supcust_info.supcust_flag = 'C' ) and
         ( co_t_order_main.trans_no = 'S' and co_t_order_main.approve_flag='1' ) and oper_date> '" + System.DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") + "'";


            //if (!string.IsNullOrEmpty(sheet_no))
            //    sql += "  and co_t_order_main.sheet_no ='" + sheet_no + "'";

            tbMain = db.ExecuteToTable(sql, null);
            //  

            tbDetail = new DataTable();
            tbDetail.Columns.Add("flow_id");
            tbDetail.Columns.Add("sheet_no");
            tbDetail.Columns.Add("item_no");
            tbDetail.Columns.Add("unit_no");
            tbDetail.Columns.Add("unit_factor");
            tbDetail.Columns.Add("sale_qnty");

            if (null == tbMain || tbMain.Rows.Count < 1)
                throw new Exception("没有数据");

            foreach (DataRow r in tbMain.Rows)
            {
                string sheet = r["sheet_no"].ToString();
                sql = @"SELECT  a.flow_id , a.sheet_no ,
           a.item_no ,
           a.unit_no ,
           a.unit_factor ,
           a.order_qnty as sale_qnty 
        FROM 	co_t_order_child a 
        WHERE	( a.sheet_no = '???')  
        ORDER BY a.flow_id          ASC  ".Replace("???", sheet);

                System.Data.DataTable tb2 = db.ExecuteToTable(sql, null);

                if (1 == 1)
                {
                    List<object[]> lst = new List<object[]>();
                    foreach (DataRow row in tb2.Rows)
                    {
                        string item_no = row["item_no"].ToString();
                        sql = "select top 1 * from bi_t_item_info where item_no='" + item_no + "'";
                        var tb = db.ExecuteToTable(sql, null);
                        if (tb.Rows.Count != 0)
                        {
                            DataRow dr = tb.Rows[0];
                            DataRow new_row = tb2.NewRow();

                            //  int sale_qnty = Conv.ToInt(row["sale_qnty"].ToString());
                            int sale_qnty = Convert.ToInt32(Convert.ToDecimal(row["sale_qnty"]));
                            new_row["sheet_no"] = row["sheet_no"].ToString();
                            new_row["item_no"] = dr["item_no"].ToString();
                            new_row["unit_no"] = dr["unit_no"].ToString();
                            new_row["unit_factor"] = Conv.ToDecimal(row["unit_factor"]);
                            //  decimal str =Convert.ToDecimal(row["unit_factor"]);
                            //  int i =Convert.ToInt32(str);
                            if (Convert.ToInt32(Convert.ToDecimal(row["unit_factor"])) == 0)
                            {
                                row["unit_factor"] = 1;
                            }
                            new_row["sale_qnty"] = sale_qnty / Convert.ToInt32(Convert.ToDecimal(row["unit_factor"]));
                            lst.Add(new_row.ItemArray);

                            if (sale_qnty % Convert.ToInt32(Convert.ToDecimal(row["unit_factor"])) != 0)
                            {
                                new_row = tb2.NewRow();
                                new_row["sheet_no"] = row["sheet_no"].ToString();
                                new_row["item_no"] = item_no;
                                new_row["unit_no"] = row["unit_no"];
                                new_row["unit_factor"] = 1;

                                new_row["sale_qnty"] = sale_qnty % Convert.ToInt32(Convert.ToDecimal(row["unit_factor"]));
                                lst.Add(new_row.ItemArray);
                            }

                        }
                        else
                        {
                            DataRow new_row = tb2.NewRow();
                            new_row.ItemArray = row.ItemArray;
                            row["unit_factor"] = 1;
                            lst.Add(new_row.ItemArray);
                        }
                    }


                    foreach (object[] objs in lst)
                    {
                        tbDetail.Rows.Add(objs);
                    }

                }

            }


            tbDetail.Columns.Add("order_qnty", typeof(decimal));
            foreach (DataRow row in tbDetail.Rows)
            {
                row["order_qnty"] = row["sale_qnty"];
            }
            tbDetail.Columns["sale_qnty"].ColumnName = "real_qnty";
            //

            return true;
        }

        /// <summary>
        /// 下载出库单
        /// </summary>
        /// <param name="sheet_no">单据号</param>
        /// <param name="tbMain">单头</param>
        /// <param name="tbDetail">单详细</param>
        /// <returns></returns>
        public bool GetCoOrder(string sheet_no, out System.Data.DataTable tbMain, out System.Data.DataTable tbDetail)
        {
            try
            {
                IDB db = new DBByAutoClose(AppSetting.conn);
                string sql = @" SELECT co_t_order_main.sheet_no,     
    co_t_order_main.sup_no,   
    co_t_order_main.branch_no,  
    co_t_order_main.oper_date,
    co_t_order_main.trans_no     
    FROM co_t_order_main,   
         bi_t_branch_info,   
         bi_t_supcust_info  
   WHERE ( co_t_order_main.branch_no = bi_t_branch_info.branch_no ) and
         ( co_t_order_main.sup_no = bi_t_supcust_info.supcust_no ) and
			( bi_t_supcust_info.supcust_flag = 'S' ) and
         ( co_t_order_main.trans_no = 'P' and co_t_order_main.approve_flag='1' ) and
          oper_date> '" + System.DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") + "'";

                //if (!(sheet_no==""))
                //    sql += "  and co_t_order_main.sheet_no ='" + sheet_no + "'";

                tbMain = db.ExecuteToTable(sql, null);
                // approve_flag='1' and oper_date>'" + System.DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd") + "'";

                tbDetail = new DataTable();
                tbDetail.Columns.Add("flow_id");
                tbDetail.Columns.Add("sheet_no");
                tbDetail.Columns.Add("item_no");
                tbDetail.Columns.Add("unit_no");
                tbDetail.Columns.Add("unit_factor");
                tbDetail.Columns.Add("sale_qnty");

                if (null == tbMain || tbMain.Rows.Count < 1)
                    throw new Exception("没有数据");

                foreach (DataRow r in tbMain.Rows)
                {
                    string sheet = r["sheet_no"].ToString();
                    sql = @"SELECT  a.flow_id , a.sheet_no ,
           a.item_no ,
           a.unit_no ,
           a.unit_factor ,
           a.order_qnty as sale_qnty 
        FROM 	co_t_order_child a 
        WHERE	( a.sheet_no = '???')  
        ORDER BY a.flow_id          ASC  ".Replace("???", sheet);

                    System.Data.DataTable tb2 = db.ExecuteToTable(sql, null);

                    if (1 == 1)
                    {
                        List<object[]> lst = new List<object[]>();
                        foreach (DataRow row in tb2.Rows)
                        {
                            string item_no = row["item_no"].ToString();
                            sql = "select top 1 * from bi_t_item_info where item_no='" + item_no + "'";
                            var tb = db.ExecuteToTable(sql, null);
                            if (tb.Rows.Count != 0)
                            {
                                DataRow dr = tb.Rows[0];
                                DataRow new_row = tb2.NewRow();

                                //  int sale_qnty = Conv.ToInt(row["sale_qnty"].ToString());
                                int sale_qnty = Convert.ToInt32(Convert.ToDecimal(row["sale_qnty"]));
                                new_row["sheet_no"] = row["sheet_no"].ToString();
                                new_row["item_no"] = dr["item_no"].ToString();
                                new_row["unit_no"] = dr["unit_no"].ToString();
                                new_row["unit_factor"] = Conv.ToDecimal(row["unit_factor"]);
                                //  decimal str =Convert.ToDecimal(row["unit_factor"]);
                                //  int i =Convert.ToInt32(str);
                                if (Convert.ToInt32(Convert.ToDecimal(row["unit_factor"])) == 0)
                                {
                                    row["unit_factor"] = 1;
                                }
                                new_row["sale_qnty"] = sale_qnty / Convert.ToInt32(Convert.ToDecimal(row["unit_factor"]));
                                lst.Add(new_row.ItemArray);

                                if (sale_qnty % Convert.ToInt32(Convert.ToDecimal(row["unit_factor"])) != 0)
                                {
                                    new_row = tb2.NewRow();
                                    new_row["sheet_no"] = row["sheet_no"].ToString();
                                    new_row["item_no"] = item_no;
                                    new_row["unit_no"] = row["unit_no"];
                                    new_row["unit_factor"] = 1;

                                    new_row["sale_qnty"] = sale_qnty % Convert.ToInt32(Convert.ToDecimal(row["unit_factor"]));
                                    lst.Add(new_row.ItemArray);
                                }

                            }
                            else
                            {
                                DataRow new_row = tb2.NewRow();
                                new_row.ItemArray = row.ItemArray;
                                row["unit_factor"] = 1;
                                lst.Add(new_row.ItemArray);
                            }
                        }


                        foreach (object[] objs in lst)
                        {
                            tbDetail.Rows.Add(objs);
                        }

                    }

                }


                tbDetail.Columns.Add("order_qnty", typeof(decimal));
                foreach (DataRow row in tbDetail.Rows)
                {
                    row["order_qnty"] = row["sale_qnty"];
                }
                tbDetail.Columns["sale_qnty"].ColumnName = "real_qnty";
                //

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("orders->GetCoOrder()", ex.ToString(), "下载采购订单");
                throw ex;
            }

        }

        /// <summary>
        /// 获取子表 
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        //   public DataTable GetOrderDetail(string sheet_no)
        //   {
        //       IDB db = new DBByAutoClose(AppSetting.conn);
        //       string sql = @"SELECT *
        //   FROM 	co_t_order_child a 
        //left join   view_item_info  b  on (a.item_no = b.item_no)
        //left join   view_branch_stock_canuse d on a.item_no = d.item_no      
        //left join ( select	 item_no, sum(sale_qnty)  as month_sale 
        //				from view_pf_total 
        //				where oper_date >=dateadd(dd,-31,getdate() ) 
        //				group by item_no  ) c on a.item_no = c.item_no
        //left join ( select	 item_no, sum(sale_qnty)  as season_sale 
        //				from view_pf_total 
        //				where oper_date >=dateadd(dd,-91,getdate() )  
        //				group by item_no ) e on a.item_no = e.item_no ";
        //       if (!string.IsNullOrEmpty(sheet_no))
        //           sql += " WHERE	( a.sheet_no = '" + sheet_no + "')  ";
        //       sql += " ORDER BY a.flow_id          ASC  ";

        //       var tb = db.ExecuteToTable(sql, null);

        //       return tb;
        //   }

        public DataTable GetOrderDetail(string sheet_no)
        {
            IDB db = new DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT a.*,b.item_size 
        FROM 	co_t_order_child a left join   bi_t_item_info  b  on (a.item_no = b.item_no)";
            if (!string.IsNullOrEmpty(sheet_no))
                sql += " WHERE	( a.sheet_no = '" + sheet_no + "')  ";
            sql += " ORDER BY a.flow_id          ASC  ";

            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        /// <summary>
        /// 获取单据号
        /// 销售单 主表单据号
        /// </summary>
        /// <returns></returns>
        public string GetSheetNo(DB.IDB db)
        {

            string front_str = "SO";
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + front_str + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                index += 1;
                if (index > 9999)
                {
                    index = 1;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + front_str + "'";
                db.ExecuteScalar(sql, null);
                return front_str + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        /// <summary>
        /// 获取全部销售订子单
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, sm_t_salesheet_detail> GetAllSale(string sheet_no)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select * from sm_t_salesheet_detail where sheet_no='" + sheet_no + "'";
            DataTable tb = d.ExecuteToTable(sql, null);

            Dictionary<string, sm_t_salesheet_detail> dic = new Dictionary<string, sm_t_salesheet_detail>();

            foreach (DataRow r in tb.Rows)
            {
                var item = ReflectionHelper.DataRowToModel<sm_t_salesheet_detail>(r);

                dic.Add(item.item_no, item);
            }

            return dic;
        }

        public Dictionary<string, string> GetItem()
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select item_no,item_name from bi_t_item_info";
            DataTable tb = d.ExecuteToTable(sql, null);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (DataRow r in tb.Rows)
            {
                dic.Add(r["item_no"].ToString(), r["item_name"].ToString());
            }

            return dic;
        }
        public Dictionary<string, string> GetItemDic(string item_nos)
        {
            item_nos = "'" + item_nos.Replace(",", "','") + "'";
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = "select item_no,item_name from bi_t_item_info where item_no in(" + item_nos + ")";
            DataTable tb = d.ExecuteToTable(sql, null);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (DataRow r in tb.Rows)
            {
                dic.Add(r["item_no"].ToString(), r["item_name"].ToString());
            }

            return dic;
        }

        public DataTable GetDic()
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            var sql = "select sheet_no,cust_no,oper_date,count(*) sum from sm_t_salesheet group by sheet_no,cust_no,oper_date";
            var dt = d.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetPHSheets(DateTime startTime, DateTime endTime)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT *
FROM dbo.ic_t_pspc_main
WHERE oper_date
BETWEEN '{startTime.Toyyyy_MM_ddStart()}' AND '{endTime.Toyyyy_MM_ddEnd()}'
ORDER BY sheet_no DESC;";
            var dt = d.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetDic1()
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            var sql = "select sheet_no,supcust_no,oper_date,count(*) sum from ic_t_inout_store_master group by sheet_no,supcust_no,oper_date";
            var dt = d.ExecuteToTable(sql, null);
            return dt;
        }

        /// <summary>
        /// 添加 销售单
        /// </summary>
        /// <param name="main">主表</param>
        /// <param name="details">子表</param>
        /// <returns></returns>
        public void AddOrder(sm_t_salesheet main, List<sm_t_salesheet_detail> details)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            try
            {
                Dictionary<string, string> dic1 = new Dictionary<string, string>();
                db.Open();
                db.BeginTran();
                IDB d = db;

                IOrder ins = this;
                #region  判断

                DataTable table1 = ins.GetDic();
                string num = "";
                foreach (DataRow row in table1.Rows)
                {
                    if (!dic1.TryGetValue(row["cust_no"].ToString() + row["oper_date"].ToString().Substring(0, 10), out num))
                    {
                        dic1.Add(row["cust_no"].ToString() + row["oper_date"].ToString().Substring(0, 10), row["sheet_no"].ToString());
                    }
                }

                //if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 6)
                //{
                //    other1 = DateTime.Now.AddDays(-1).ToString().Substring(0, 10) + "0606";
                //}
                //else
                //{
                //    // valid_data = DateTime.Now.AddDays(-1).ToString().Substring(0, 10) + "0606";
                //    other1 = DateTime.Now.ToString().Substring(0, 10) + "0606";
                //}

                if (!dic1.ContainsKey(main.cust_no + main.oper_date.ToString().Substring(0, 10)))
                {
                    string sm_sheet_no = ins.GetSheetNo(d);
                    dic1.Add(main.cust_no + main.oper_date.ToString().Substring(0, 10), sm_sheet_no);
                    main.sheet_no = sm_sheet_no;
                }
                else
                {
                    main.sheet_no = dic1[main.cust_no + main.oper_date.ToString().Substring(0, 10)];
                }
                #endregion

                main.sale_man = "0";
                var sql1 = "select 1 from sm_t_salesheet where sheet_no='" + main.sheet_no + "' ";
                var dt = d.ExecuteToTable(sql1, null);
                if (dt.Rows.Count == 0)
                {
                    d.Insert(main);
                }
                else
                {
                    sql1 = "update sm_t_salesheet set total_amount=total_amount+" + main.total_amount + ",paid_amount=paid_amount+" + main.paid_amount + " " +
                            "where sheet_no='" + main.sheet_no + "'";
                    DB.IDB db1 = new DB.DBByAutoClose(AppSetting.conn);
                    db1.ExecuteScalar(sql1, null);
                }

                Dictionary<string, sm_t_salesheet_detail> dic = new Dictionary<string, sm_t_salesheet_detail>();
                foreach (var item in details)
                {
                    item.sheet_no = main.sheet_no;
                    if (item.item_no.StartsWith("PK") == true)
                    {
                        var sql = "select * from bi_t_item_info where item_no='" + item.item_no + "'";
                        var tb = d.ExecuteToTable(sql, null);
                        if (tb.Rows.Count == 0)
                        {
                            throw new Exception("匹配不到主商品" + item.item_no);
                        }
                        var row = tb.Rows[0];
                        sm_t_salesheet_detail temp;
                        if (dic.TryGetValue(item.item_no, out temp) == true)
                        {
                            temp.sale_qnty += item.sale_qnty * Conv.ToInt("1");
                        }
                        else
                        {
                            sql = "select unit_no from bi_t_item_info where item_no='" + row["item_no"].ToString() + "'";
                            item.item_no = row["item_no"].ToString();
                            item.sale_qnty = item.sale_qnty * Conv.ToInt("1");
                            //  item.unit_no = Conv.ToString(d.ExecuteScalar(sql, null));
                            item.unit_no = d.ExecuteScalar(sql, null).ToString();
                            item.unit_factor = 1;
                            dic.Add(item.item_no, item);
                        }
                    }
                    else
                    {
                        sm_t_salesheet_detail temp;
                        if (dic.TryGetValue(item.item_no, out temp) == true)
                        {
                            temp.sale_qnty += item.sale_qnty;
                        }
                        else
                        {
                            dic.Add(item.item_no, item);
                        }
                    }
                }

                foreach (KeyValuePair<string, sm_t_salesheet_detail> kv in dic)
                {
                    kv.Value.ret_qnty = kv.Value.sale_qnty;
                    //   var sql = "select * from bi_t_item_unit where item_no='" + kv.Value.item_no + "' and memo like 'PK%'";
                    var sql = "select * from bi_t_item_info where item_no='" + kv.Value.item_no + "'";
                    var tb = d.ExecuteToTable(sql, null);
                    if (tb.Rows.Count == 0)
                    {
                        kv.Value.packqty = 0;
                        kv.Value.sgqty = kv.Value.sale_qnty;
                    }
                    else
                    {
                        var row = tb.Rows[0];
                        kv.Value.packqty = Conv.ToInt(kv.Value.sale_qnty) / Conv.ToInt("1");
                        kv.Value.sgqty = Conv.ToInt(kv.Value.sale_qnty) % Conv.ToInt("1");
                    }
                    sql = "select top(1) flow_id from sm_t_salesheet_detail order by flow_id desc";
                    object obj = d.ExecuteScalar(sql, null);
                    kv.Value.flow_id = Conv.ToInt64(obj) + 1;
                    d.Insert(kv.Value);
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

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="item_no"></param>
        /// <returns></returns>
        public bi_t_item_info GetItem(string item_no)
        {
            IDB db = new DBByAutoClose(AppSetting.conn);

            string sql = @"select * 
from bi_t_item_info ";
            if (!string.IsNullOrEmpty(item_no))
                sql += "where item_no='" + item_no + "'";

            return db.ExecuteToModel<bi_t_item_info>(sql, null);
        }


        /// <summary>
        /// 获取退货单
        /// 单据号
        /// </summary>
        /// <returns></returns>
        public string GetReOrderNo(DB.IDB db)
        {
            string front_str = "RI";
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + front_str + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                index += 1;
                if (index > 9999)
                {
                    index = 1;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + front_str + "'";
                db.ExecuteScalar(sql, null);
                return front_str + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        /// <summary>
        /// 添加退货单
        /// </summary>
        /// <param name="main">退货单主表</param>
        /// <param name="dels">子表</param>
        /// <returns></returns>
        public void AddReOrder(ic_t_inout_store_master main, List<ic_t_inout_store_detail> dels)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            try
            {
                IDB d = db;
                db.Open();
                db.CommitTran();

                IOrder ins = this;
                main.sheet_no = ins.GetReOrderNo(d);
                main.deal_man = "0";
                d.Insert(main);

                Dictionary<string, ic_t_inout_store_detail> dic = new Dictionary<string, ic_t_inout_store_detail>();
                foreach (var item in dels)
                {
                    item.sheet_no = main.sheet_no;
                    if (item.item_no.StartsWith("PK") == true)
                    {
                        var sql = "select * from bi_t_item_info where item_no='" + item.item_no + "'";
                        var tb = d.ExecuteToTable(sql, null);
                        if (tb.Rows.Count == 0)
                        {
                            throw new Exception("匹配不到主商品" + item.item_no);
                        }
                        var row = tb.Rows[0];
                        ic_t_inout_store_detail temp;
                        if (dic.TryGetValue(item.item_no, out temp) == true)
                        {
                            //  temp.in_qty += item.in_qty * Conv.ToInt(row["unit_factor"].ToString());
                            temp.in_qty += item.in_qty * Conv.ToInt("1");
                        }
                        else
                        {
                            sql = "select unit_no from bi_t_item_info where item_no='" + row["item_no"].ToString() + "'";
                            item.item_no = row["item_no"].ToString();
                            //  item.in_qty = item.in_qty * Conv.ToInt(row["unit_factor"].ToString());
                            item.in_qty = item.in_qty * Conv.ToInt("1");
                            //  item.unit_no = Conv.ToString(d.ExecuteScalar(sql, null));
                            item.unit_no = d.ExecuteScalar(sql, null).ToString();
                            item.unit_factor = 1;
                            dic.Add(item.item_no, item);
                        }
                    }
                    else
                    {
                        ic_t_inout_store_detail temp;
                        if (dic.TryGetValue(item.item_no, out temp) == true)
                        {
                            temp.in_qty += item.in_qty;
                        }
                        else
                        {
                            dic.Add(item.item_no, item);
                        }
                    }
                }

                foreach (KeyValuePair<string, ic_t_inout_store_detail> kv in dic)
                {
                    kv.Value.ret_qnty = kv.Value.in_qty;
                    //  var sql = "select * from bi_t_item_unit where item_no='" + kv.Value.item_no + "' and memo like 'PK%'";
                    var sql = "select * from bi_t_item_info where item_no='" + kv.Value.item_no + "'";
                    var tb = d.ExecuteToTable(sql, null);
                    if (tb.Rows.Count == 0)
                    {
                        kv.Value.packqty = 0;
                        kv.Value.sgqty = kv.Value.in_qty;
                    }
                    else
                    {
                        var row = tb.Rows[0];
                        kv.Value.packqty = Conv.ToInt(kv.Value.in_qty) / Conv.ToInt("1");
                        kv.Value.sgqty = Conv.ToInt(kv.Value.in_qty) % Conv.ToInt("1");
                    }
                    sql = "select top(1) flow_id from ic_t_inout_store_detail order by flow_id desc";
                    object obj = d.ExecuteScalar(sql, null);
                    kv.Value.flow_id = Conv.ToInt64(obj) + 1;
                    d.Insert(kv.Value);
                }

                db.CommitTran();
            }
            catch (Exception e)
            {
                db.RollBackTran();
                throw;
            }
            finally
            {
                db.Close();
            }

        }

        /// <summary>
        /// 获取采购单 
        /// 单据号
        /// </summary>
        /// <returns></returns>
        public string GetColOrderNo(DB.IDB db)
        {
            string front_str = "PI";
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + front_str + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                index += 1;
                if (index > 9999)
                {
                    index = 1;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + front_str + "'";
                db.ExecuteScalar(sql, null);
                return front_str + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        /// <summary>
        /// 添加采购单
        /// </summary>
        /// <param name="mian">主表</param>
        /// <param name="childs">子表</param>
        public void AddColOrder(ic_t_inout_store_master main, List<ic_t_inout_store_detail> childs)
        {

            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            try
            {
                Dictionary<string, string> dic1 = new Dictionary<string, string>();
                IDB d = db;
                db.Open();
                db.BeginTran();

                IOrder ins = this;

                DataTable table1 = ins.GetDic1();
                string num1 = "";
                foreach (DataRow row in table1.Rows)
                {
                    if (!dic1.TryGetValue(row["supcust_no"].ToString() + row["oper_date"].ToString().Substring(0, 10), out num1))
                    {
                        dic1.Add(row["supcust_no"].ToString() + row["oper_date"].ToString().Substring(0, 10), row["sheet_no"].ToString());
                    }
                }
                if (!dic1.ContainsKey(main.supcust_no + main.oper_date.ToString().Substring(0, 10)))
                {
                    string sm_sheet_no = ins.GetColOrderNo(d);
                    dic1.Add(main.supcust_no + main.oper_date.ToString().Substring(0, 10), sm_sheet_no);
                    main.sheet_no = sm_sheet_no;
                }
                else
                {
                    main.sheet_no = dic1[main.supcust_no + main.oper_date.ToString().Substring(0, 10)];
                }



                //   main.sheet_no = ins.GetColOrderNo(d);
                main.deal_man = "0";

                var sql1 = "select 1 from ic_t_inout_store_master where sheet_no='" + main.sheet_no + "' ";
                var dt = d.ExecuteToTable(sql1, null);
                if (dt.Rows.Count == 0)
                {
                    d.Insert(main);
                }
                else
                {
                    sql1 = "update ic_t_inout_store_master set total_amount=total_amount+" + main.total_amount + ",inout_amount=inout_amount+" + main.inout_amount + " " +
                            "where sheet_no='" + main.sheet_no + "'";
                    DB.IDB db1 = new DB.DBByAutoClose(AppSetting.conn);
                    db1.ExecuteScalar(sql1, null);
                }



                //    d.Insert(main);

                Dictionary<string, ic_t_inout_store_detail> dic = new Dictionary<string, ic_t_inout_store_detail>();
                foreach (var item in childs)
                {
                    item.sheet_no = main.sheet_no;
                    if (item.item_no.StartsWith("PK") == true)
                    {
                        var sql = "select * from bi_t_item_info where item_no='" + item.item_no + "'";
                        var tb = d.ExecuteToTable(sql, null);
                        if (tb.Rows.Count == 0)
                        {
                            throw new Exception("匹配不到主商品" + item.item_no);
                        }
                        var row = tb.Rows[0];
                        ic_t_inout_store_detail temp;
                        if (dic.TryGetValue(item.item_no, out temp) == true)
                        {
                            temp.in_qty += item.in_qty * Conv.ToInt("1");
                        }
                        else
                        {
                            sql = "select unit_no from bi_t_item_info where item_no='" + row["item_no"].ToString() + "'";
                            item.item_no = row["item_no"].ToString();
                            item.in_qty = item.in_qty * Conv.ToInt("1");
                            //  item.unit_no = Conv.ToString(d.ExecuteScalar(sql,null)) ;
                            item.unit_no = d.ExecuteScalar(sql, null).ToString();
                            item.unit_factor = 1;
                            dic.Add(item.item_no, item);
                        }
                    }
                    else
                    {
                        ic_t_inout_store_detail temp;
                        if (dic.TryGetValue(item.item_no, out temp) == true)
                        {
                            temp.in_qty += item.in_qty;
                        }
                        else
                        {
                            dic.Add(item.item_no, item);
                        }
                    }
                }

                foreach (KeyValuePair<string, ic_t_inout_store_detail> kv in dic)
                {
                    kv.Value.ret_qnty = kv.Value.in_qty;
                    //  var sql = "select * from bi_t_item_unit where item_no='"+kv.Value.item_no+"' and memo like 'PK%'";
                    var sql = "select * from bi_t_item_info where item_no='" + kv.Value.item_no + "'";
                    var tb = d.ExecuteToTable(sql, null);
                    if (tb.Rows.Count == 0)
                    {
                        kv.Value.packqty = 0;
                        kv.Value.sgqty = kv.Value.in_qty;
                    }
                    else
                    {
                        var row = tb.Rows[0];
                        kv.Value.packqty = Conv.ToInt(kv.Value.in_qty) / Conv.ToInt("1");
                        kv.Value.sgqty = Conv.ToInt(kv.Value.in_qty) % Conv.ToInt("1");
                    }
                    sql = "select top(1) flow_id from ic_t_inout_store_detail order by flow_id desc";
                    object obj = d.ExecuteScalar(sql, null);
                    kv.Value.flow_id = Conv.ToInt64(obj) + 1;
                    d.Insert(kv.Value);
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


        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="item_no">商品编号</param>
        /// <param name="item_cls">商品类型</param>
        /// <param name="item_name">商品名称</param>
        /// <param name="sup_no">供应商编号</param>
        /// <param name="sup_name">供应商名称</param>
        /// <returns></returns>
        public DataTable GetItemTable(string item_no, string item_cls, string item_name, string sup_no, string sup_name)
        {
            IDB d = new DBByAutoClose(AppSetting.conn);
            string sql = @"select *
from bi_t_item_info  b
left join bi_t_supcust_info s on b.sup_no=s.supcust_no
 where 1=1 ";
            if (!string.IsNullOrEmpty(item_no))
                sql += " and b.item_no='" + item_no + "'";
            if (!string.IsNullOrEmpty(item_cls))
                sql += "  and b.item_clsno like   '" + item_cls + "%' ";
            if (!string.IsNullOrEmpty(item_name))
                sql += " and b.item_name like '%" + item_name + "%' ";
            if (!string.IsNullOrEmpty(sup_no))
                sql += " and b.sup_no = '" + sup_no + "' ";
            if (!string.IsNullOrEmpty(sup_name))
                sql += " and s.sup_name like '%" + sup_name + "%' ";
            return d.ExecuteToTable(sql, null);
        }

        /// <summary>
        /// 获取采购出库 残次品
        /// 单据号
        /// </summary>
        /// <returns></returns>
        public string GetBadOrderNo(DB.IDB db)
        {
            string front_str = "RO";
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + front_str + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                index += 1;
                if (index > 9999)
                {
                    index = 1;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + front_str + "'";
                db.ExecuteScalar(sql, null);
                return front_str + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }


        public void bad_goods(ic_t_inout_store_master main, List<ic_t_inout_store_detail> details)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            try
            {
                IDB d = db;
                db.Open();
                db.BeginTran();

                IOrder ins = this;
                main.sheet_no = ins.GetBadOrderNo(d);

                d.Insert(main);

                Dictionary<string, ic_t_inout_store_detail> dic = new Dictionary<string, ic_t_inout_store_detail>();
                foreach (var item in details)
                {
                    if (item.item_no.StartsWith("PK") == true)
                    {
                        var sql = "select * from bi_t_item_unit where memo='" + item.item_no + "'";
                        var tb = d.ExecuteToTable(sql, null);
                        if (tb.Rows.Count == 0)
                        {
                            throw new Exception("匹配不到主商品" + item.item_no);
                        }
                        var row = tb.Rows[0];
                        ic_t_inout_store_detail temp;
                        if (dic.TryGetValue(item.item_no, out temp) == true)
                        {
                            temp.in_qty += item.in_qty * Conv.ToInt(row["unit_factor"].ToString());
                        }
                        else
                        {
                            sql = "select unit_no from bi_t_item_info where item_no='" + row["item_no"].ToString() + "'";
                            item.item_no = row["item_no"].ToString();
                            item.in_qty = item.in_qty * Conv.ToInt(row["unit_factor"].ToString());
                            //     item.unit_no = Conv.ToString(d.ExecuteScalar(sql, null));
                            item.unit_no = d.ExecuteScalar(sql, null).ToString();
                            item.unit_factor = 1;
                            dic.Add(item.item_no, item);
                        }
                    }
                    else
                    {
                        ic_t_inout_store_detail temp;
                        if (dic.TryGetValue(item.item_no, out temp) == true)
                        {
                            temp.in_qty += item.in_qty;
                        }
                        else
                        {
                            dic.Add(item.item_no, item);
                        }
                    }
                }

                foreach (KeyValuePair<string, ic_t_inout_store_detail> kv in dic)
                {
                    d.Insert(kv.Value, "flow_id");
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


        #region 采购统单

        /// <summary>
        /// 创建拣货批次-要货单
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetReqOrderList(string date1, string date2, string out_date)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var condition_sql = "";
                if (out_date != "")
                {
                    condition_sql += " and convert(varchar(10),m.valid_date,120)=@out_date ";
                }
                else
                {
                    condition_sql += " and convert(varchar(10),m.valid_date,120)>=@date1 ";
                }
                string sql = "select m.*,n.sup_name from co_t_order_main m ";
                sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C') n on m.sup_no=n.supcust_no ";
                sql += "where (m.trans_no='S'  or trans_no='PP') and isnull(m.order_status,'0')='0' and isnull(m.ph_sheet_no,'')='' " + condition_sql;
                sql += "and convert(varchar(10),m.oper_date,120)>=@date1 and convert(varchar(10),m.oper_date,120)<=@date2 ";
                sql += "order by sheet_no desc ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@date1",date1),
                    new System.Data.SqlClient.SqlParameter("@date2",date2),
                    new System.Data.SqlClient.SqlParameter("@out_date",out_date)
                };
                var dt = db.ExecuteToTable(sql, pars);
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("OrderBLL.GetReqOrderList(2)", ex.ToString(), date1, date2);
                throw ex;
            }
        }
        /// <summary>
        /// 批次-要货单
        /// </summary>
        /// <param name="ph_sheet_no">批次号</param>
        /// <returns></returns>
        public DataTable GetPHReqOrderList(string ph_sheet_no)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

                string sql = "select m.*,n.sup_name from co_t_order_main m ";
                sql += "left join (select supcust_no,sup_name from bi_t_supcust_info where supcust_flag='C') n on m.sup_no=n.supcust_no ";
                sql += "where (m.trans_no='S' or m.trans_no='PP' or m.trans_no='P') and m.ph_sheet_no=@ph_sheet_no ";
                sql += "order by m.sheet_no ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@ph_sheet_no",ph_sheet_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("OrderBLL.GetPHReqOrderList()", ex.ToString(), ph_sheet_no);
                throw ex;
            }
        }

        /// <summary>
        /// 送货日期批次列表
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public DataTable GetPHOrderList(string date1, string date2)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var condition_sql = "";
                string sql = "select a.*,b.oper_name from dbo.ic_t_pspc_main a ";
                sql += "left join sa_t_operator_i b on a.oper_id=b.oper_id ";
                sql += "where convert(varchar(10),a.oper_date,120)>=@date1 and convert(varchar(10),a.oper_date,120)<=@date2 " + condition_sql;
                sql += "order by a.sheet_no desc ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@date1",date1),
                    new System.Data.SqlClient.SqlParameter("@date2",date2)
                };
                var dt = db.ExecuteToTable(sql, pars);
                dt.Columns.Add("color");
                foreach (DataRow row in dt.Rows)
                {
                    sql= @"select ph_sheet_no from [dbo].[ic_t_pspc_detail] where  sheet_no='"+row["sheet_no"]+"'";
                    var d = db.ExecuteToTable(sql, null);
                    string sheets = "";
                    foreach (DataRow dataRow in d.Rows)
                    {
                        sheets += "'" + dataRow["ph_sheet_no"] + "',";
                    }

                    sql = @"select is_batch_num from co_t_order_main where sheet_no in (" + sheets.Substring(0,sheets.Length-1) + ")";
                    d = db.ExecuteToTable(sql,null);
                    DataRow[] dr = d.Select("is_batch_num='1'");
                    if (d.Rows.Count != dr.Length)
                    {
                        row["color"] = "1";
                    }

                }
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("OrderBLL.GetPHOrderList()", ex.ToString(), date1, date2);
                throw ex;
            }
        }

        //销售订单明细
        public DataTable GetReqOrderDetail(string sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = "select a.*,b.item_name,b.item_size,b.item_subno,b.barcode ";
            sql += "from co_t_order_child a inner join bi_t_item_info b on a.item_no=b.item_no ";
            sql += "where sheet_no=@sheet_no order by sheet_sort ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@sheet_no",sheet_no)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }


        // 创建拣货批次-库存差异（生成采购订单）
        public DataTable GetReqOrderStockDiff(string ph_sheet_no, string is_bulk)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string where_sql = "1=1";
            if ("1".Equals(is_bulk))
            {
                where_sql += $@" and f.item_no NOT IN
      (
          SELECT item_no FROM dbo.bi_t_bulk_item
      ) ";
            }
            where_sql += $@" and f.ph_sheet_no = '{ph_sheet_no}'";

            string sql = $@"SELECT f.item_no,
       f.unit_no,
       i.item_name,
       s.supcust_no,
       s.sup_name,
       i.item_size,
       i.item_subno,
       i.barcode,
       SUM(f.order_qnty) order_qnty,
       ISNULL(c.stock_qty, 0) stock_qty,
       ISNULL(c.stock_qty, 0) - SUM(f.order_qnty) AS diff_qty,
       ISNULL(i.item_loss, 0) cg_rate
FROM dbo.ot_temp_mrp_flow f
    LEFT JOIN dbo.bi_t_item_info i ON i.item_no = f.item_no
    LEFT JOIN (SELECT item_no,stock_qty FROM dbo.ic_t_branch_stock WHERE branch_no = '0001') c ON c.item_no = f.item_no
left join bi_t_supcust_info s on s.supcust_no=f.sup_no and s.supcust_flag='S' 
WHERE 
{where_sql}
GROUP BY f.item_no,f.unit_no,i.item_name,s.supcust_no,s.sup_name,i.item_size,i.item_subno,i.barcode,c.stock_qty,
         ISNULL(i.item_loss, 0) ORDER BY f.item_no;";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        /// <summary>
        /// 确认生成采购订单
        /// 生成思路：获取批次内所有批销单，循环乘损耗率，得到最终要货数
        /// 取库存数量（如果按库存指标，就直接减去库存指标）
        /// 如果要按差异生成，判断库存>要货 就不生成采购
        /// </summary>
        public void CreateCGOrder(string ph_sheet_no, string op_type, DataTable item_dt, string is_min_stock, string oper_id)
        {
            string sql = "";
            var d = new DBByHandClose(AppSetting.conn);
            IDB db = d;
            try
            {
                d.Open();
                d.BeginTran();

                //写采购损耗率日志
                Dictionary<string, decimal> item_dic = new Dictionary<string, decimal>();
                string item_nos = "";
                string log_sql = "";
                foreach (DataRow dr in item_dt.Rows)
                {
                    if (!item_dic.ContainsKey(dr["item_no"].ToString()))
                    {
                        item_dic.Add(dr["item_no"].ToString(), 1 + Conv.ToDecimal(dr["cg_rate"]));
                    }
                }

                {
                    //按要货数量生成采购订单
                    sql = $@"SELECT b.sup_no,
       a.item_no,
       b.item_name,
       a.cust_no,
       a.unit_no,
       a.unit_factor,
       a.order_qnty,
       b.barcode,
       ISNULL( b.price, 0) price,
       a.order_qnty AS cg_qty,
       b.min_stock,
       c.stock_qty,
       a.pick_barcode,
       '' show_num
FROM
(
    SELECT a.item_no,
           a.unit_no,
           a.unit_factor,
           b.sup_no AS cust_no,
           ISNULL(a.order_qnty, 0) order_qnty,
           a.pick_barcode
    FROM co_t_order_child a
        INNER JOIN co_t_order_main b
            ON a.sheet_no = b.sheet_no
    WHERE b.ph_sheet_no = '{ph_sheet_no}'
          AND LEFT(b.sheet_no, 2) = 'SS'
) a
    INNER JOIN bi_t_item_info b
        ON a.item_no = b.item_no
    LEFT JOIN ic_t_branch_stock c
        ON c.item_no = a.item_no
           AND c.branch_no = '0001'
    LEFT JOIN
    (
        SELECT supcust_no,
               sup_name
        FROM bi_t_supcust_info
        WHERE supcust_flag = 'C'
    ) d
        ON a.cust_no = d.supcust_no
ORDER BY b.sup_no,
         a.item_no,
         a.cust_no;";
                }
                var dt = db.ExecuteToTable(sql, null);

                if (dt.Rows.Count > 0)
                {
                    #region 采购订单
                    Dictionary<string, decimal> stockDic = new Dictionary<string, decimal>();
                    var groupBy = dt.AsEnumerable().GroupBy(s => s["sup_no"].ToString());
                    foreach (var g in groupBy)
                    {
                        List<co_t_order_child> lines = new List<co_t_order_child>();
                        co_t_order_main ord = new co_t_order_main
                        {
                            sheet_no = MaxCode(db, "PO"),
                            p_sheet_no = "",
                            ph_sheet_no = ph_sheet_no,
                            sup_no = g.First()["sup_no"].ToString(),
                            order_man = "0001",
                            oper_id = oper_id,
                            valid_date = DateTime.Now.AddDays(1),
                            oper_date = DateTime.Now,
                            update_time = DateTime.Now,
                            branch_no = "0001",
                            coin_code = "RMB",
                            paid_amount = 0,
                            trans_no = "P",
                            order_status = "0",
                            sale_way = "A",
                            approve_flag = "0",
                            other1 = "",
                            cm_branch = "00",
                            approve_man = "",
                            approve_date = System.DateTime.MinValue,
                            num1 = 0,
                            num2 = 0,
                            num3 = 0,
                            memo = ""
                        };

                        int index = 0;
                        decimal total_amt = 0;
                        foreach (var row in g)
                        {
                            ++index;
                            decimal stock_qty = row["stock_qty"].ToDecimal();
                            decimal min_stock = row["min_stock"].ToDecimal();

                            co_t_order_child line = new co_t_order_child();
                            line.sheet_no = ord.sheet_no;
                            line.item_no = row["item_no"].ToString();
                            line.unit_no = row["unit_no"].ToString();
                            line.barcode = row["barcode"].ToString();
                            line.unit_factor = row["unit_factor"].ToDecimal();
                            line.discount = 1;
                            line.in_price = row["price"].ToDecimal();

                            if (stockDic.TryGetValue(line.item_no, out decimal value))
                            {
                                stock_qty = value;
                            }
                            else
                            {
                                if ("1".Equals(is_min_stock))
                                {
                                    stock_qty -= min_stock;
                                }
                                stockDic.Add(line.item_no, stock_qty);
                            }

                            decimal order_qnty = row["cg_qty"].ToDecimal();
                            if (item_dic.TryGetValue(line.item_no, out decimal rate))
                            {
                                order_qnty *= rate;
                            }
                            line.order_qnty = order_qnty;

                            if ("2".Equals(op_type))
                            {
                                //判断要货数量跟库存的差异
                                if (stock_qty >= line.order_qnty)
                                {
                                    //不需要生成采购
                                    continue;
                                }
                                else
                                {
                                    line.order_qnty -= stock_qty;
                                    stockDic[line.item_no] = 0;
                                }
                            }

                            line.sub_amount = line.in_price * line.order_qnty;
                            line.discount = 1;
                            line.other1 = "";
                            line.other2 = "";
                            line.voucher_no = "";
                            line.sheet_sort = index;
                            line.num1 = 0;
                            line.num2 = 0;
                            line.num3 = 0;
                            line.packqty = 0;
                            line.sgqty = 0;
                            line.pick_barcode = row["pick_barcode"].ToString();
                            line.supcust_no = row["cust_no"].ToString();

                            total_amt += line.order_qnty * line.in_price;
                            lines.Add(line);
                        }
                        ord.total_amount = total_amt;

                        if (lines.Count > 0)
                        {
                            db.Insert(ord);
                            foreach (co_t_order_child line in lines)
                            {
                                line.sheet_no = ord.sheet_no;
                                db.Insert(line, "flow_id");
                            }
                            db.ExecuteScalar($@"INSERT dbo.ic_t_pspc_detail(sheet_no,ph_sheet_no,sheet_type) VALUES('{ph_sheet_no}','{ord.sheet_no}','PO')", null);
                            lines.Clear();
                        }
                    }
                    #endregion

                    #region 生产co_t_cg_order_detail表数据
                    {
                        sql = "delete from co_t_cg_order_detail where ph_sheet_no=@ph_sheet_no ";
                        var pars = new System.Data.SqlClient.SqlParameter[]
                        {
                        new System.Data.SqlClient.SqlParameter("@ph_sheet_no", ph_sheet_no)
                        };
                        db.ExecuteScalar(sql, pars);

                        StringBuilder sb = new StringBuilder();
                        int index = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (index > 5000)
                            {
                                db.ExecuteScalar(sb.ToString(), null);
                                sb.Clear();
                                index = 0;
                            }
                            decimal cg_rate2 = 1;
                            if (!item_dic.TryGetValue(dr["item_no"].ToString(), out cg_rate2))
                            {
                                cg_rate2 = 1;
                            }
                            decimal qty = Math.Abs(Conv.ToDecimal(dr["cg_qty"])) * cg_rate2;

                            sb.Append("insert into co_t_cg_order_detail(ph_sheet_no,sup_no,item_no,item_name,barcode,unit_no,unit_factor,price,cust_no,show_num,order_qnty,cg_qty,create_time) ");
                            sb.Append("values(");
                            sb.Append("'" + ph_sheet_no + "'");
                            sb.Append(",'" + dr["sup_no"].ToString() + "'");
                            sb.Append(",'" + dr["item_no"].ToString() + "'");
                            sb.Append(",'" + dr["item_name"].ToString() + "'");
                            sb.Append(",'" + dr["barcode"].ToString() + "'");
                            sb.Append(",'" + dr["unit_no"].ToString() + "'");
                            sb.Append(",'" + dr["unit_factor"].ToString() + "'");
                            sb.Append(",'" + dr["price"].ToString() + "'");
                            sb.Append(",'" + dr["cust_no"].ToString() + "'");
                            sb.Append(",'" + dr["show_num"].ToString() + "'");
                            sb.Append(",'" + dr["order_qnty"].ToString() + "'");
                            sb.Append(",'" + qty + "'");
                            sb.Append(",getdate()");
                            sb.Append(");\r\n");

                            ++index;
                        }
                        if (sb.Length > 0)
                        {
                            db.ExecuteScalar(sb.ToString(), null);
                            sb.Clear();
                            index = 0;
                        }
                    }
                    #endregion
                }

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


        public string MaxCode(DB.IDB db, string sheet_type)
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + sheet_type + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = Helper.Conv.ToInt(obj);
                if (index > 9999)
                {
                    index = 0;
                }
                index += 1;
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + sheet_type + "'";
                db.ExecuteScalar(sql, null);
                return sheet_type + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        /// <summary>
        /// 创建拣货批次
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public void CreatePHOrder(string sheet_nos, string oper_id,string flag)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            try
            {
                d.Open();
                d.BeginTran();
                sheet_nos = "'" + sheet_nos.Replace(",", "','") + "'";
                var sheet_no = MaxCode(d, "PH");

                string sql = $@"insert into dbo.ic_t_pspc_main( sheet_no,oper_id,oper_date,out_date,ms_other1,update_time)
select '{sheet_no}','{oper_id}',getdate(),min(valid_date),convert(varchar(10),min(valid_date),120)+'配送批次',GETDATE()
from dbo.co_t_order_main
WHERE sheet_no IN ({sheet_nos})";
                db.ExecuteScalar(sql, null);

                sql = $@"insert into dbo.ic_t_pspc_detail(sheet_no,ph_sheet_no,sheet_type)
select '{sheet_no}',sheet_no,LEFT(sheet_no,2) from dbo.co_t_order_main
WHERE sheet_no in({sheet_nos})";
                db.ExecuteScalar(sql, null);

                sql = $"update co_t_order_main set ph_sheet_no='{sheet_no}',update_time='{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}' where sheet_no in({sheet_nos}) ";
                db.ExecuteScalar(sql, null);

                //创建销售订单拣货码
                var dt_str = DateTime.Now.ToString("yyMMdd");
                sql = "select max(right(pick_barcode ,4)) barcode_index from co_t_order_child where left(pick_barcode,6)='" + dt_str + "' ";
                var dt = db.ExecuteToTable(sql, null);
                int barcode_index = 0;
                if (dt.Rows.Count > 0) barcode_index = Conv.ToInt(dt.Rows[0]["barcode_index"]);
                if (barcode_index >= 9999) barcode_index = 0;
                if (flag=="0")
                {
                    //创建条码
                    sql = $@"SELECT c.flow_id,m.sup_no
FROM dbo.co_t_order_main m
LEFT JOIN dbo.co_t_order_child c ON c.sheet_no = m.sheet_no
WHERE (LEFT(m.sheet_no,2)='SS' or LEFT(m.sheet_no,2)='PP'or LEFT(m.sheet_no,2)='PO') AND  c.sheet_no IN ({sheet_nos})";
                }

                if (flag == "1")
                {
                    sql = "select flow_id from co_t_order_child ";
                    sql += "where sheet_no =" + sheet_nos + "";
                }
                DataTable tb = db.ExecuteToTable(sql, null);
                StringBuilder sb = new StringBuilder();
                int index = 0;
                foreach (DataRow row in tb.Rows)
                {
                    ++barcode_index;
                    ++index;
                    string flow_id = row["flow_id"].ToString();
                    string pick_barcode = dt_str + barcode_index.ToString().PadLeft(4, '0');

                    sb.Append("update co_t_order_child set pick_barcode='" + pick_barcode + "' where flow_id='" + flow_id + "';\r\n");

                    if (index > 3000)
                    {
                        LogHelper.writeLog("CreatePHOrder(1)", sb.ToString(), sheet_nos, sheet_no);
                        db.ExecuteScalar(sb.ToString(), null);
                        sb.Clear();
                        index = 0;
                    }
                }
                if (sb.Length > 0)
                {
                    LogHelper.writeLog("CreatePHOrder(2)", sb.ToString(), sheet_nos, sheet_no);
                    db.ExecuteScalar(sb.ToString(), null);
                    sb.Clear();
                    index = 0;
                }
                d.CommitTran();
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog("OrderBLL.CreatePHOrder()", ex.ToString(), sheet_nos);
                throw ex;
            }
            finally
            {
                d.Close();
            }
        }

        // 按供应商导出
        public DataTable GetSupReqOrderDetail(string ph_sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT f.sup_no,
       s.sup_name sup_name,
       f.item_no,
	   i.item_subno,
       f.item_name,
       f.barcode,
       f.unit_no,
       f.unit_factor,
       f.price,
       f.cust_no,
       c.sup_name cust_name,
       f.show_num,
       f.order_qnty,
       f.cg_qty,
       f.out_date,
       f.create_time,
       f.pick_barcode,
       f.cg_rate,
       f.co_flow_id,
	   i.item_size,
i.min_stock,isnull(bs.stock_qty,0) stock_qty,
ot1.qty,
(case when (isnull(bs.stock_qty,0)-ot1.qty)<isnull(i.min_stock,0)
then isnull(i.min_stock,0)-abs((isnull(bs.stock_qty,0)-isnull(ot1.qty,0))) else 0 end) cg_qnty
FROM dbo.ot_temp_mrp_flow f
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = f.item_no
    LEFT JOIN dbo.bi_t_supcust_info c
        ON c.supcust_no = f.cust_no
           AND c.supcust_flag = 'C'
    LEFT JOIN dbo.bi_t_supcust_info s
        ON s.supcust_no = f.sup_no
           AND s.supcust_flag = 'S'
left join ic_t_branch_stock bs on bs.item_no=i.item_no and bs.branch_no = '0001'
left join (select ph_sheet_no,item_no,sum(order_qnty) qty from  ot_temp_mrp_flow 
group by item_no,ph_sheet_no) ot1
on ot1.item_no=f.item_no and f.ph_sheet_no=ot1.ph_sheet_no
WHERE f.ph_sheet_no = '{ph_sheet_no}'
ORDER BY f.sup_no,
         f.item_no,
         f.cust_no;";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }
        /// <summary>
        /// 按供应商协拣明细导出
        /// </summary>
        /// <param name="ph_sheet_no"></param>
        /// <returns></returns>
        public DataTable GetSupPickOrderDetail(string ph_sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT f.sup_no,
       s.sup_name sup_name,
       f.item_no,
	   i.item_subno,
       f.item_name,
       f.barcode,
       f.unit_no,
       f.unit_factor,
       f.price,
       f.cust_no,
       c.sup_name cust_name,
       f.show_num,
       f.order_qnty,
       f.cg_qty,
       f.out_date,
       f.create_time,
       f.pick_barcode,
       f.cg_rate,
       f.co_flow_id,
	   i.item_size,
       f.is_pick
FROM dbo.ot_temp_mrp_flow f
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = f.item_no
    LEFT JOIN dbo.bi_t_supcust_info c
        ON c.supcust_no = f.cust_no
           AND c.supcust_flag = 'C'
    LEFT JOIN dbo.bi_t_supcust_info s
        ON s.supcust_no = f.sup_no
           AND s.supcust_flag = 'S'
WHERE f.ph_sheet_no = '{ph_sheet_no}' and f.is_pick = '1' 
ORDER BY f.sup_no,
         f.item_no,
         f.cust_no;";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }



        // 按分类导出
        public DataTable GetClsReqOrderDetail(string ph_sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT f.sup_no,
       s.sup_name sup_name,
       f.item_no,
	   i.item_subno,
       f.item_name,
       f.barcode,
       f.unit_no,
       f.unit_factor,
       f.price,
       f.cust_no,
       c.sup_name cust_name,
       f.show_num,
       f.order_qnty,
       f.cg_qty,
       f.out_date,
       f.create_time,
       f.pick_barcode,
       f.cg_rate,
       f.co_flow_id,
       i.item_size,
        SUBSTRING(i.item_clsno, 1, 2) item_clsno,
       cls.item_clsname,
i.min_stock,isnull(bs.stock_qty,0) stock_qty,
ot1.qty,
(case when (isnull(bs.stock_qty,0)-ot1.qty)<isnull(i.min_stock,0)
then isnull(i.min_stock,0)-abs((isnull(bs.stock_qty,0)-isnull(ot1.qty,0))) else 0 end) cg_qnty
FROM dbo.ot_temp_mrp_flow f
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = f.item_no
    LEFT JOIN dbo.bi_t_item_cls cls
        ON cls.item_clsno = SUBSTRING(i.item_clsno, 1, 2)
    LEFT JOIN dbo.bi_t_supcust_info c
        ON c.supcust_no = f.cust_no
           AND c.supcust_flag = 'C'
    LEFT JOIN dbo.bi_t_supcust_info s
        ON s.supcust_no = f.sup_no
           AND s.supcust_flag = 'S'
left join ic_t_branch_stock bs on bs.item_no=i.item_no and bs.branch_no = '0001'
left join (select ph_sheet_no,item_no,sum(order_qnty) qty from  ot_temp_mrp_flow 
group by item_no,ph_sheet_no) ot1
on ot1.item_no=f.item_no and f.ph_sheet_no=ot1.ph_sheet_no
WHERE f.ph_sheet_no = '{ph_sheet_no}'
ORDER BY i.item_clsno,
         f.item_no,
         f.cust_no;";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }


        // 按商品导出
        public DataTable GetItemReqOrderSum(string ph_sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT f.sup_no,
       s.sup_name sup_name,
       f.item_no,
	   i.item_subno,
       f.item_name,
       f.barcode,
       f.unit_no,
       f.unit_factor,
       f.price,
       f.cust_no,
       c.sup_name cust_name,
       f.show_num,
       f.order_qnty,
       f.cg_qty,
       f.out_date,
       f.create_time,
       f.pick_barcode,
       f.cg_rate,
       f.co_flow_id,
       i.item_size,
        SUBSTRING(i.item_clsno, 1, 2) item_clsno,
       cls.item_clsname,
i.min_stock,isnull(bs.stock_qty,0) stock_qty,
ot1.qty,
(case when (isnull(bs.stock_qty,0)-ot1.qty)<isnull(i.min_stock,0)
then isnull(i.min_stock,0)-abs((isnull(bs.stock_qty,0)-isnull(ot1.qty,0))) else 0 end) cg_qnty
FROM dbo.ot_temp_mrp_flow f
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = f.item_no
    LEFT JOIN dbo.bi_t_item_cls cls
        ON cls.item_clsno = SUBSTRING(i.item_clsno, 1, 2)
    LEFT JOIN dbo.bi_t_supcust_info c
        ON c.supcust_no = f.cust_no
           AND c.supcust_flag = 'C'
    LEFT JOIN dbo.bi_t_supcust_info s
        ON s.supcust_no = f.sup_no
           AND s.supcust_flag = 'S'
left join ic_t_branch_stock bs on bs.item_no=i.item_no and bs.branch_no = '0001'
left join (select ph_sheet_no,item_no,sum(order_qnty) qty from  ot_temp_mrp_flow 
group by item_no,ph_sheet_no) ot1
on ot1.item_no=f.item_no and f.ph_sheet_no=ot1.ph_sheet_no
WHERE f.ph_sheet_no = '{ph_sheet_no}'
ORDER BY i.item_clsno,
         f.item_no;";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetSupPickList(string ph_sheet_no, string sup_no, string item_no, DateTime startTime, DateTime endTime)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.ph_sheet_no,a.sup_no,a.item_no,i.item_subno,a.item_name,a.unit_no,a.cust_no,a.show_num";
            sql += ",a.order_qnty,a.cg_qty,a.out_date,a.pick_barcode,b.sup_name as cust_name,b.supcust_group,b.reach_time,c.sup_name ";
            sql += "from ot_temp_mrp_flow a ";
            sql += "left join bi_t_supcust_info b on a.cust_no=b.supcust_no and b.supcust_flag='C' ";
            sql += "left join bi_t_supcust_info c on a.sup_no=c.supcust_no and c.supcust_flag='S' ";
            sql += "LEFT JOIN dbo.bi_t_item_info i ON i.item_no = a.item_no ";
            sql += "where a.item_no in(select item_no from bi_t_item_info where isnull(is_self_cg,'0')='0') ";
            sql += $@" AND CONVERT(VARCHAR(10),a.out_date,120) BETWEEN '{startTime.Toyyyy_MM_dd()}' AND '{endTime.Toyyyy_MM_dd()}' ";
            if (sup_no != "")
            {
                sql += "and a.sup_no=@sup_no ";
            }
            if (item_no != "")
            {
                sql += "and a.item_no=@item_no ";
            }
            sql += "order by a.ph_sheet_no,a.sup_no,a.item_no,a.cust_no ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@sup_no", sup_no),
                new System.Data.SqlClient.SqlParameter("@item_no", item_no),
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }


        public void UpdateSheetStatus(string sheet_nos)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "update co_t_order_main set order_status='3' where sheet_no in (" + sheet_nos + ")";
                db.ExecuteScalar(sql, null);
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
        #endregion

        #region 大宗统单

        public DataTable GetBulikItems()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@" SELECT b.flow_id,
       b.item_no,
       i.item_name,
       i.item_subno,
       i.unit_no,
       b.create_time
FROM dbo.bi_t_bulk_item b
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = b.item_no; ";

            DataTable tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public void SaveBulkItems(List<bi_t_bulk_item> items)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            db.ExecuteScalar("DELETE FROM dbo.bi_t_bulk_item ", null);

            foreach (var item in items)
            {
                db.Insert(item, "flow_id");
            }
        }

        public DataTable GetBulkOrderList(string date1, string date2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@" SELECT	m.sheet_no,
        m.start_date,
        m.end_date,
        m.ms_other1,
        m.oper_id,
		i.oper_name,
        m.oper_date,
        m.approve_flag
FROM dbo.ot_cg_bulk_master m
LEFT JOIN  dbo.sa_t_operator_i i ON i.oper_id = m.oper_id
WHERE m.oper_date BETWEEN '{date1}' AND '{date2}'
order by m.oper_date desc,m.sheet_no desc";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetBulkReqOrderList(string cb_sheet_no)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

                string sql = $@" SELECT m.*,
       n.sup_name
FROM co_t_order_main m
    LEFT JOIN
    (
        SELECT supcust_no,
               sup_name
        FROM bi_t_supcust_info
        WHERE supcust_flag = 'C'
    ) n
        ON m.sup_no = n.supcust_no
WHERE m.trans_no = 'S'
      AND m.sheet_no IN
          (
              SELECT sheet_no FROM dbo.ot_cg_bulk_sheet WHERE cb_sheet_no = '{cb_sheet_no}'
          )
ORDER BY m.sheet_no desc; ";

                var dt = db.ExecuteToTable(sql, null);
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("OrderBLL.GetBulkReqOrderList()", ex.ToString(), cb_sheet_no);
                throw ex;
            }
        }

        public DataTable GetBulkReqOrderStockDiff(string cb_sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT d.item_no,
       i.unit_no,
       i.item_name,
       s.supcust_no,
       s.sup_name,
       i.item_size,
       i.item_subno,
       i.barcode,
       SUM(d.order_qnty) order_qnty,
       ISNULL(c.stock_qty, 0) stock_qty,
       ISNULL(lock.qty, 0) lock_qty,
       ISNULL(c.stock_qty, 0) - ISNULL(lock.qty, 0) - SUM(d.order_qnty) AS diff_qty,
       ISNULL(i.item_loss, 0) cg_rate
FROM dbo.ot_cg_bulk_detail d
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = d.item_no
    LEFT JOIN
    (
        SELECT item_no,
               stock_qty
        FROM dbo.ic_t_branch_stock
        WHERE branch_no = '0001'
    ) c
        ON c.item_no = d.item_no
    INNER JOIN dbo.bi_t_bulk_item bi
        ON bi.item_no = d.item_no
    LEFT JOIN bi_t_supcust_info s
        ON s.supcust_no = i.sup_no
    LEFT JOIN
    (
        SELECT item_no,
               SUM(lock_qty) qty
        FROM dbo.ot_temp_lock_inventory
        GROUP BY item_no
    ) lock
        ON lock.item_no = d.item_no
WHERE d.cb_sheet_no = '{cb_sheet_no}'
GROUP BY d.item_no,
         i.unit_no,
         i.item_name,
         s.supcust_no,
         s.sup_name,
         i.item_size,
         i.item_subno,
         i.barcode,
         c.stock_qty,
         lock.qty,
         ISNULL(i.item_loss, 0)
ORDER BY d.item_no;";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetBulkSupReqOrderDetail(string ph_sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT bd.sup_no,
       s.sup_name,
       bd.item_no,
       i.item_subno,
       i.item_name,
       SUBSTRING(i.item_clsno, 1, 2) item_clsno,
       cl.item_clsname,
       i.item_size,
       i.barcode,
       bd.unit_no,
       bd.price,
       bd.order_qnty,
       bd.cg_qty
FROM dbo.ot_cg_bulk_detail bd
    LEFT JOIN dbo.bi_t_supcust_info s
        ON s.supcust_no = bd.sup_no
           AND s.supcust_flag = 'S'
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = bd.item_no
    LEFT JOIN dbo.bi_t_item_cls cl
        ON cl.item_clsno = SUBSTRING(i.item_clsno, 1, 2)
WHERE bd.cb_sheet_no = '{ph_sheet_no}'
ORDER BY bd.sup_no,
         bd.item_no;";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetBulkClsReqOrderDetail(string ph_sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT bd.sup_no,
       s.sup_name,
       bd.item_no,
       i.item_subno,
       i.item_name,
       SUBSTRING(i.item_clsno, 1, 2) item_clsno,
       cl.item_clsname,
       i.item_size,
       i.barcode,
       bd.unit_no,
       bd.price,
       bd.order_qnty,
       bd.cg_qty
FROM dbo.ot_cg_bulk_detail bd
    LEFT JOIN dbo.bi_t_supcust_info s
        ON s.supcust_no = bd.sup_no
           AND s.supcust_flag = 'S'
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = bd.item_no
    LEFT JOIN dbo.bi_t_item_cls cl
        ON cl.item_clsno = SUBSTRING(i.item_clsno, 1, 2)
WHERE bd.cb_sheet_no = '{ph_sheet_no}'
ORDER BY cl.item_clsno,
         bd.item_no;";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetBulkItemReqOrderSum(string ph_sheet_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"SELECT bd.sup_no,
       s.sup_name,
       bd.item_no,
       i.item_subno,
       i.item_name,
       SUBSTRING(i.item_clsno, 1, 2) item_clsno,
       cl.item_clsname,
       i.item_size,
       i.barcode,
       bd.unit_no,
       bd.price,
       bd.order_qnty,
       bd.cg_qty
FROM dbo.ot_cg_bulk_detail bd
    LEFT JOIN dbo.bi_t_supcust_info s
        ON s.supcust_no = bd.sup_no
           AND s.supcust_flag = 'S'
    LEFT JOIN dbo.bi_t_item_info i
        ON i.item_no = bd.item_no
    LEFT JOIN dbo.bi_t_item_cls cl
        ON cl.item_clsno = SUBSTRING(i.item_clsno, 1, 2)
WHERE bd.cb_sheet_no = '{ph_sheet_no}'
ORDER BY cl.item_clsno,
         bd.item_no;";

            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        #endregion

        #region 智能调价
        public DataTable GetItemCls()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from bi_t_item_cls where item_flag='0' order by item_clsno ";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetLeftTable(string item_clsno)
        {
            string consql = "";
            if (item_clsno != "所有类别")
            {
                consql = " AND LEFT(i.item_clsno, 2) = @item_clsno ";
            }

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DateTime endTime =
                db.ExecuteScalar("SELECT TOP 1 create_time FROM dbo.bi_t_sup_item_history ORDER BY last_sheet_no DESC",
                    null).ToDateTime();
            endTime = endTime.AddMilliseconds(-1);
            string sql = $@"SELECT  i.item_no ,
                                    i.item_name ,
                                    ISNULL(t.last_price, i.price) price,
		                            CASE WHEN h.create_time > '{endTime}' THEN ISNULL(h.is_change, 0) ELSE 0 END is_change
                            FROM    dbo.bi_t_item_info i
                                    LEFT JOIN ( SELECT  t.item_no ,
                                                        t.last_price,
							                            t.update_time,
							                            t.last_sheet_no
                                                FROM    ( SELECT    ROW_NUMBER() OVER ( PARTITION BY item_no ORDER BY update_time DESC ) rowId ,
                                                                    item_no ,
                                                                    last_price ,
                                                                    update_time,
										                            last_sheet_no
                                                          FROM      dbo.bi_t_sup_item
                                                        ) t
                                                WHERE   t.rowId = 1
                                              ) t ON t.item_no = i.item_no
		                            LEFT JOIN dbo.bi_t_sup_item_history h ON h.last_sheet_no = t.last_sheet_no AND h.item_no = t.item_no 
                            WHERE 1 = 1 
                                  {consql}
                            ORDER BY t.update_time DESC;";

            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                    new System.Data.SqlClient.SqlParameter("@item_clsno",item_clsno)
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        public DataTable GetItemPrice(string item_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT  item_name ,item_no,
        base_price ,
        rate ,
        base_price2 ,
        rate2 ,
        base_price3 ,
        rate3,
		price,
		ISNULL((SELECT TOP 1 last_price FROM dbo.bi_t_sup_item_history WHERE item_no = i.item_no ORDER BY create_time DESC), 0) last_price 
FROM    dbo.bi_t_item_info i 
  where item_no=@item_name ";

            var pars = new System.Data.SqlClient.SqlParameter[]
           {
              new System.Data.SqlClient.SqlParameter("@item_name",item_no)
           };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        public DataTable GetPriceHistory(string item_name)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "SELECT TOP 5 h.price FROM dbo.bi_t_sup_item_history h LEFT JOIN dbo.bi_t_item_info i ON i.item_no = h.item_no WHERE i.item_no=@item_name ORDER BY h.create_time DESC";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@item_name",item_name)
                };

                var dt = db.ExecuteToTable(sql, pars);

                return dt;
            }
            catch (Exception e)
            {
                LogHelper.writeLog("Order>GetPriceHistory", e.ToString());
                throw e;
            }
        }

        public DataTable GetItemList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from bi_t_item_info ";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }

        public DataTable GetItemList(List<string> item_nos)
        {
            string nos = "";
            foreach (string itemNo in item_nos)
            {
                nos += itemNo;
            }

            nos = nos.Remove(nos.Length - 1, 1);
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = @"SELECT  
        item_no      ,  
        item_name ,
        base_price ,
        rate ,
        base_price2 ,
        rate2 ,
        base_price3 ,
        rate3,
		price,
		ISNULL((SELECT TOP 1 last_price FROM dbo.bi_t_sup_item_history WHERE item_no = i.item_no ORDER BY create_time DESC), 0) last_price 
FROM    dbo.bi_t_item_info i  where item_no in (" + nos + ")";
            var dt = db.ExecuteToTable(sql, null);
            return dt;
        }
        #endregion

        #region 快速开单—追加采购助手单 
        public void AddCGOrder(List<Model.co_t_cg_order_detail> lines)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select ph_sheet_no,from_sheet_no from co_t_cg_order_detail ";
            sql += "where ph_sheet_no=(select max(ph_sheet_no) from co_t_cg_order_detail) ";
            var dt = db.ExecuteToTable(sql, null);
            string ph_sheet_no = "";
            string from_sheet_no = "";
            if (dt.Rows.Count > 0)
            {
                ph_sheet_no = dt.Rows[0]["ph_sheet_no"].ToString();
                from_sheet_no = dt.Rows[0]["from_sheet_no"].ToString();
            }
            else
            {
                ph_sheet_no = MaxCode(db, "PH");
                from_sheet_no = "";
            }

            foreach (var item in lines)
            {
                item.ph_sheet_no = ph_sheet_no;
                item.from_sheet_no = from_sheet_no;
                db.Insert(item, "flow_id");
            }
        }

        #endregion

    }
}