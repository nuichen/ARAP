using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class ModifyCSPriceBLL : IModifyCSPriceBLL
    {
        private static ModifyCSPriceBLL modifyPriceBLL = null;
        public static ModifyCSPriceBLL GetModifyCSPriceBLL()
        {
            if (modifyPriceBLL == null)
            {
                modifyPriceBLL = new ModifyCSPriceBLL();
            }
            return modifyPriceBLL;
        }

        public void Delete(string sheet_no)
        {
            string sql = "select * from co_t_price_order_main where sheet_no='" + sheet_no + "'";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核" + sheet_no);
                    }
                }
                sql = "delete from co_t_price_order_child where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from co_t_price_order_main where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
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

        public void Check(string sheet_no, string approve_man)
        {
            string sql = "select * from co_t_price_order_main where sheet_no='" + sheet_no + "'";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + sheet_no);
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核" + sheet_no);
                    }
                }
                sql = "update co_t_price_order_main set approve_flag='1',approve_man='" + approve_man +
                    "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',update_time='"+ System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                sql = "select * from co_t_price_order_child where sheet_no='" + sheet_no + "'";
                tb = d.ExecuteToTable(sql, null);
                //sql = "update " + cs == "CV" ? "bi_t_cust_price" : "bi_t_sup_item";
                foreach (DataRow row in tb.Rows)
                {
                    sql = "update bi_t_cust_price set new_price=" + row["price"] + " where item_no='" + row["item_no"] + "'";
                    d.ExecuteScalar(sql, null);
                }
                sql = "select * from co_t_price_order_main where sheet_no='" + sheet_no + "'";
                co_t_price_order_main main = d.ExecuteToModel<co_t_price_order_main>(sql, null);
                sql = "select * from co_t_price_order_child where sheet_no='" + sheet_no + "'";
                List<co_t_price_order_child> child_lst = d.ExecuteToList<co_t_price_order_child>(sql);
                DataTable dt = null;
                if (main.trans_no == "CV")
                {
                    bi_t_cust_price cust_price = null;
                    foreach (Model.co_t_price_order_child line in child_lst)
                    {
                        sql = "select * from bi_t_cust_price where cust_id='" + main.sup_no + "' and item_no='" + line.item_no + "'";
                        cust_price = d.ExecuteToModel<bi_t_cust_price>(sql, null);
                        if (cust_price != null)
                        {
                            cust_price.cust_id = main.sup_no;
                            cust_price.top_sheet_no = cust_price.top_price > line.price ? cust_price.top_sheet_no : main.sheet_no;
                            cust_price.bottom_sheet_no = cust_price.bottom_price > line.price ? main.sheet_no : cust_price.bottom_sheet_no;
                            cust_price.top_price = cust_price.top_price > line.price ? cust_price.top_price : line.price;
                            cust_price.bottom_price = cust_price.bottom_price > line.price ? line.price : cust_price.bottom_price;
                            cust_price.last_price = line.price;
                            cust_price.new_price = line.price;
                            cust_price.last_sheet_no = main.sheet_no;
                            cust_price.oper_date = DateTime.Now;
                            cust_price.update_time = DateTime.Now;
                            d.Update(cust_price, "cust_id,item_no", "cust_id,top_sheet_no,bottom_sheet_no,top_price,bottom_price,last_price,last_sheet_no,oper_date,update_time");
                        }
                        else
                        {
                            cust_price = new bi_t_cust_price();
                            cust_price.cust_id = main.sup_no;
                            cust_price.item_no = line.item_no;
                            cust_price.price_type = "0";
                            cust_price.discount = 1;
                            cust_price.oper_id = main.oper_id;
                            cust_price.top_sheet_no = main.sheet_no;
                            cust_price.new_price = line.price;
                            cust_price.bottom_sheet_no = main.sheet_no;
                            cust_price.top_price = line.price;
                            cust_price.bottom_price = line.price;
                            cust_price.last_price = line.price;
                            cust_price.last_sheet_no = main.sheet_no;
                            cust_price.oper_date = DateTime.Now;
                            cust_price.update_time = DateTime.Now;
                            d.Insert(cust_price);
                        }
                    }
                }
                else if (main.trans_no == "SV")
                {
                    bi_t_sup_item sup_price = null;
                    foreach (Model.co_t_price_order_child line in child_lst)
                    {
                        sql = "select * from bi_t_sup_item where sup_id='" + main.sup_no + "' and item_no='" + line.item_no + "'";
                        sup_price = d.ExecuteToModel<bi_t_sup_item>(sql, null);
                        if (sup_price != null)
                        {
                            sup_price.sup_id = main.sup_no;
                            sup_price.top_sheet_no = sup_price.top_price > line.price ? sup_price.top_sheet_no : main.sheet_no;
                            sup_price.bottom_sheet_no = sup_price.bottom_price > line.price ? main.sheet_no : sup_price.bottom_sheet_no;
                            sup_price.top_price = sup_price.top_price > line.price ? sup_price.top_price : line.price;
                            sup_price.bottom_price = sup_price.bottom_price > line.price ? line.price : sup_price.bottom_price;
                            sup_price.last_price = line.price;
                            sup_price.last_sheet_no = main.sheet_no;
                            sup_price.update_time = DateTime.Now;
                            d.Update(sup_price, "sup_id,item_no", "sup_id,top_sheet_no,bottom_sheet_no,top_price,bottom_price,last_price,last_sheet_no,update_time");
                        }
                        else
                        {
                            sup_price = new bi_t_sup_item();
                            sup_price.sup_id = main.sup_no;
                            sup_price.item_no = line.item_no;
                            sup_price.price = line.price;
                            sup_price.top_sheet_no = main.sheet_no;
                            sup_price.bottom_sheet_no = main.sheet_no;
                            sup_price.top_price = line.price;
                            sup_price.bottom_price = line.price;
                            sup_price.last_price = line.price;
                            sup_price.last_sheet_no = main.sheet_no;
                            sup_price.update_time = DateTime.Now;
                            d.Insert(sup_price);
                        }
                    }
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

        public void GetOrder(string sheet_no, string cs, out System.Data.DataTable main_dt, out System.Data.DataTable child_dt)
        {
            var supcust_flag = "C";
            if (cs == "SV") supcust_flag = "S";
            string sql = "select a.*,d.sup_name,c.oper_name,man.oper_name as approve_man_name,b.oper_name as order_man_name ";
            sql += "from co_t_price_order_main a ";
            sql += "left join bi_t_people_info b on a.order_man=b.oper_id ";
            sql += "left join sa_t_operator_i c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_supcust_info d on a.sup_no=d.supcust_no and d.supcust_flag='" + supcust_flag + "' ";
            sql += "left join (select * from sa_t_operator_i) man on a.approve_man=man.oper_id ";
            sql += "where a.trans_no='" + cs + "' and a.sheet_no='" + sheet_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            main_dt = db.ExecuteToTable(sql, null);
            sql = "select a.sheet_no,a.item_no,a.sheet_sort,a.price as new_price,b.item_subno,b.item_name,b.barcode,b.unit_no,b.price ";
            sql += "from co_t_price_order_child a ";
            sql += "left join bi_t_item_info b on a.item_no=b.item_no ";
            sql += "where a.sheet_no='" + sheet_no + "' order by a.sheet_sort";
            child_dt = db.ExecuteToTable(sql, null);
        }

        string MaxCode(string cs)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='" + cs + "'";

            object obj = db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = Helper.Conv.ToInt(obj);
                index += 1;
                if (index > 9999)
                {
                    index = 0;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + cs + "'";
                db.ExecuteScalar(sql, null);
                return cs + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }
        //客户调价
        public void AddModifyPrice(co_t_price_order_main main, List<co_t_price_order_child> child_lst,string cs, out string sheet_no)
        {
            IInOutBLL get_sheet_no = new InOutBLL();
            main.sheet_no = MaxCode(cs);

            //
            string sql = "select * from co_t_price_order_main where sheet_no='" + main.sheet_no + "'";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count != 0)
                {
                    throw new Exception("已存在单号" + main.sheet_no);
                }

                if (main.start_date.ToString().Contains("1950"))
                {
                    main.start_date = null;
                }
                if (main.end_date.ToString().Contains("1950"))
                {
                    main.end_date = null;
                }
                if (main.approve_date.ToString().Contains("1950"))
                {
                    main.approve_date = null;
                }
                d.Insert(main);

                sql = "SELECT ISNULL(MAX(sheet_sort),0) FROM dbo.co_t_price_order_child";
                foreach (Model.co_t_price_order_child line in child_lst)
                {
                    int sheet_sort = d.ExecuteScalar(sql, null).ToInt32();
                    line.sheet_sort = sheet_sort + 1;
                    line.sheet_no = main.sheet_no;
                    d.Insert(line);
                }
                
                //
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
            sheet_no = main.sheet_no;

            //

        }

        public void Change(co_t_price_order_main main, List<co_t_price_order_child> child_lst, string cs)
        {
            string sql = "select * from co_t_price_order_main where trans_no='" + cs + "' and sheet_no='" + main.sheet_no + "'";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在" + main.sheet_no);
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核" + main.sheet_no);
                    }
                }
                sql = "delete from co_t_price_order_child where sheet_no='" + main.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from co_t_price_order_main where trans_no='" + cs + "' and sheet_no='" + main.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                d.Insert(main);
                foreach (Model.co_t_price_order_child line in child_lst)
                {
                    d.Insert(line);
                }
                //
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

        public DataTable GetList(string cs,DateTime begin_time, DateTime end_time)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from co_t_price_order_main where trans_no='" + cs + "' and oper_date>='" + begin_time.ToString("yyyy-MM-dd") + " 00:00:00.000'" + " and oper_date<='" + end_time.ToString("yyyy-MM-dd") + " 23:59:59.999'";
            DataTable dt = db.ExecuteToTable(sql, null);
            return dt;
        }
    }
}