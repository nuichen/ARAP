using System;
using System.Data;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class PriceBLL : IPriceBLL
    {
        DataTable IPriceBLL.GetCustPriceList(string cust_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select sup_name from bi_t_supcust_info where supcust_no=@cust_id and supcust_flag='C' ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cust_id", cust_id)
            };
            var dt = db.ExecuteToTable(sql, pars);

            if (dt.Rows.Count <= 0) throw new Exception("客户不存在[" + cust_id + "]");

            sql = "select a.*,'" + dt.Rows[0]["sup_name"].ToString() + "' as cust_name,b.item_name,b.item_subno,b.item_subname,b.item_clsno,b.unit_no,b.barcode,b.price,b.base_price ";
            sql += "from(select * from bi_t_cust_price where cust_id=@cust_id) a ";
            sql += "left join bi_t_item_info b on a.item_no=b.item_no ";
            sql += "order by a.update_time desc ";
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cust_id", cust_id)
            };
            dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        DataTable IPriceBLL.GetSupPriceList(string sup_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select sup_name from bi_t_supcust_info where supcust_no=@sup_id and supcust_flag='S' ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@sup_id", sup_id)
            };
            var dt = db.ExecuteToTable(sql, pars);

            if (dt.Rows.Count <= 0) throw new Exception("供应商不存在[" + sup_id + "]");

            sql = "select a.*,'" + dt.Rows[0]["sup_name"].ToString() + "' as sup_name,b.item_name,b.item_subno,b.item_subname,b.item_clsno,b.unit_no,b.barcode ,b.base_price ";
            sql += "from(select * from bi_t_sup_item where sup_id=@sup_id) a ";
            sql += "left join bi_t_item_info b on a.item_no=b.item_no ";
            sql += "order by a.update_time desc ";
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@sup_id", sup_id)
            };
            dt = db.ExecuteToTable(sql, pars);
            return dt;
        }

        //取客户商品价格  type: 0：客户批发价 1：客户退货价
        decimal IPriceBLL.GetCusItemPrice(string cust_id, string item_no, string type)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            decimal price = 0;
            //
            string sql = "select isnull(other1,'1') other1,isnull(cust_level,'1') cust_level ";
            sql += "from bi_t_supcust_info where supcust_no='" + cust_id + "' and supcust_flag='C' ";
            var dt1 = db.ExecuteToTable(sql, null);
            string price_type = "1";
            if (type.Equals("0"))
            {
                price_type = db.ExecuteScalar("select sys_var_value from sys_t_system where sys_var_id='cust_pricetype'", null).ToString();
            }
            else if (type.Equals("1"))
            {
                price_type = db.ExecuteScalar("select sys_var_value from sys_t_system where sys_var_id='cust_ri_pricetype'", null).ToString();
            }

            int cust_level = 1;
            if (dt1.Rows.Count > 0)
            {
                cust_level = Helper.Conv.ToInt(dt1.Rows[0]["cust_level"]);
                if (cust_level == 0) cust_level = 1;
            }
            if (price_type == "0")//最后价格
            {
                sql = "select * from bi_t_cust_price where cust_id='" + cust_id + "' and item_no='" + item_no + "' order by  oper_date desc";
                var dt2 = db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0)
                {
                    //bi_t_cust_price.last_price
                    price = Helper.Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                    if (price > 0) return price;

                    //bi_t_cust_price.new_price
                    price = Helper.Conv.ToDecimal(dt2.Rows[0]["new_price"]);
                    if (price > 0) return price;
                }
            }
            else if (price_type == "1")//约定价格
            {
                sql = "select * from sys_t_system where sys_var_id='cust_pricetype' ";
                var dt3 = db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    var temp_price_type = Helper.Conv.ToInt(dt3.Rows[0]["sys_var_value"]);
                    if (temp_price_type == 1) //最后批发价
                    {
                        sql = "select * from bi_t_cust_price where cust_id='" + cust_id + "' and item_no='" + item_no + "' ";
                        var dt2 = db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_cust_price.last_price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                            if (price > 0) return price;

                            //bi_t_cust_price.new_price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["new_price"]);
                            if (price > 0) return price;
                        }
                    }
                    else if (temp_price_type == 3)//最低批发价
                    {
                        sql = "select * from bi_t_cust_price where cust_id='" + cust_id + "' and item_no='" + item_no + "' ";
                        var dt2 = db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_cust_price.last_price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["bottom_price"]);
                            if (price > 0) return price;

                            //bi_t_cust_price.new_price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["new_price"]);
                            if (price > 0) return price;
                        }
                    }
                    else if (temp_price_type == 5) //最后进货价
                    {
                        sql = "select top 1 last_price from bi_t_sup_item where item_no='" + item_no + "'";
                        sql += "and last_sheet_no=(select max(last_sheet_no) from bi_t_sup_item where item_no='" + item_no + "') ";
                        var dt2 = db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_sup_item.last_price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                            if (price > 0) return price;
                        }
                    }
                }
            }

            //以上价格策略无法取到相应价格时，默认取商品档案约定级别价格
            if (1 == 1)
            {
                //bi_t_item_info中约定的商品价格
                sql = "select base_price,base_price2,base_price3 ";
                sql += "from bi_t_item_info where item_no='" + item_no + "' ";
                var dt3 = db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    //取客户级别价格，如无级别则默认取base_price
                    if (dt3.Columns.Count >= cust_level)
                    {
                        price = Helper.Conv.ToDecimal(dt3.Rows[0][cust_level - 1]);
                        if (price > 0) return price;
                    }
                    else
                    {
                        price = Helper.Conv.ToDecimal(dt3.Rows[0][0]);
                        if (price > 0) return price;
                    }
                }
            }
            //

            return price;
        }
        //取供应商商品价格 type 0:供应商进货 1:供应商退货
        decimal IPriceBLL.GetSupItemPrice(string sup_id, string item_no, string type)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            decimal price = 0;
            //
            string sql = "select isnull(other1,'1') other1 ";
            sql += "from bi_t_supcust_info where supcust_no='" + sup_id + "' and supcust_flag='S' ";
            var dt1 = db.ExecuteToTable(sql, null);
            var price_type = "1";
            if (type.Equals("0"))
            {
                price_type = db.ExecuteScalar("select sys_var_value from sys_t_system where sys_var_id='sup_pricetype'", null).ToString();
            }
            else if (type.Equals("1"))
            {
                price_type = db.ExecuteScalar("select sys_var_value from sys_t_system where sys_var_id='sup_ro_pricetype'", null).ToString();
            }

            if (dt1.Rows.Count > 0)
            {

            }
            if (price_type == "0")//最后价格
            {
                sql = "select * from bi_t_sup_item where sup_id='" + sup_id + "' and item_no='" + item_no + "'  ";
                var dt2 = db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0)
                {
                    //bi_t_sup_item.last_price
                    price = Helper.Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                    if (price > 0) return price;

                    //bi_t_sup_item.price
                    price = Helper.Conv.ToDecimal(dt2.Rows[0]["price"]);
                    if (price > 0) return price;
                }
            }
            else if (price_type == "1")//约定价格
            {
                sql = "select * from sys_t_system where sys_var_id='sup_pricetype' ";
                var dt3 = db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    var temp_price_type = Helper.Conv.ToInt(dt3.Rows[0]["sys_var_value"]);
                    if (temp_price_type == 1) //最后进价
                    {
                        sql = "select * from bi_t_sup_item where sup_id='" + sup_id + "' and item_no='" + item_no + "' ";
                        var dt2 = db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_sup_item.last_price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                            if (price > 0) return price;

                            //bi_t_sup_item.price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["price"]);
                            if (price > 0) return price;
                        }
                    }
                    else if (temp_price_type == 2) //约定进价
                    {
                        sql = "select * from bi_t_sup_item where sup_id='" + sup_id + "' and item_no='" + item_no + "' ";
                        var dt2 = db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_sup_item.price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["price"]);
                            if (price > 0) return price;
                        }
                    }
                    else if (temp_price_type == 3)//最低进价
                    {
                        sql = "select * from bi_t_sup_item where sup_id='" + sup_id + "' and item_no='" + item_no + "' ";
                        var dt2 = db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_sup_item.bottom_price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["bottom_price"]);
                            if (price > 0) return price;

                            //bi_t_sup_item.new_price
                            price = Helper.Conv.ToDecimal(dt2.Rows[0]["price"]);
                            if (price > 0) return price;
                        }
                    }
                }
            }

            //以上价格策略无法取到相应价格时，默认取商品档案中的进价
            if (1 == 1)
            {
                //bi_t_item_info中约定的商品价格
                sql = "select price from bi_t_item_info where item_no='" + item_no + "' ";
                var dt3 = db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    //取商品档案中的默认进价
                    price = Helper.Conv.ToDecimal(dt3.Rows[0]["price"]);
                    return price;
                }
            }
            //

            return price;
        }

        decimal IPriceBLL.GetLastInPrice(string item_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            decimal price = 0;

            string sql = "select top 1 last_price from bi_t_sup_item where item_no='" + item_no + "'";
            sql += "and last_sheet_no=(select max(last_sheet_no) from bi_t_sup_item where item_no='" + item_no + "') ";
            var dt2 = db.ExecuteToTable(sql, null);
            if (dt2.Rows.Count > 0)
            {
                //bi_t_sup_item.last_price
                price = Helper.Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                //
                return price;
            }

            //以上价格策略无法取到相应价格时，默认取商品档案中的进价
            if (1 == 1)
            {
                //bi_t_item_info中约定的商品价格
                sql = "select price from bi_t_item_info where item_no='" + item_no + "' ";
                var dt3 = db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    //取商品档案中的默认进价
                    price = Helper.Conv.ToDecimal(dt3.Rows[0]["price"]);
                    return price;
                }
            }

            //
            return price;
        }

    }
}
