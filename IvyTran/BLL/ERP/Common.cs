using System;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
  public  class Common : ICommon
    {
        //获取商品列表
        DataTable ICommon.GetItemList(string sysn_time)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

                string sql = "select item_no,item_subno,item_subname,item_clsno,item_name,item_brand,item_brandname,unit_no,item_size";
                sql += ",product_area,barcode,price,base_price,base_price2,base_price3,0 base_price4,0 base_price5,sale_price,display_flag,item_flag ";
                sql += "from bi_t_item_info where update_time>=@sysn_time ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time)
                };
                var dt = db.ExecuteToTable(sql, pars);
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetItemList()", ex.ToString(), sysn_time);
                throw new Exception(ex.Message);
            }
        }

        //获取商品更新数目
        int ICommon.GetItemCount(string sysn_time)
        {
            try
            {
                int count = 0;
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

                string sql = "select count(item_no) total_count from bi_t_item_info where update_time>=@sysn_time ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0) count = Conv.ToInt(dt.Rows[0]["total_count"]);
                return count;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetItemCount()", ex.ToString(), sysn_time);
                throw new Exception(ex.Message);
            }
        }

        //获取品类列表
        DataTable ICommon.GetItemClsList(string sysn_time)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select item_clsno,item_clsname,item_flag,display_flag ";
                sql += "from bi_t_item_cls where item_flag='0' and len(item_clsno)=2 and 1=1";//"update_time>=@sysn_time ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetItemClsList()", ex.ToString(), sysn_time);
                throw new Exception(ex.Message);
            }
        }

        //获取商品库存
        DataTable ICommon.GetBranchStockList(string sysn_time, string branch_no)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

                string sql = "select branch_no,item_no,stock_qty,cost_price,display_flag,last_price,fifo_price ";
                sql += "from ic_t_branch_stock where branch_no=@branch_no and update_time>=@sysn_time ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@branch_no",branch_no),
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;

            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetBranchStockList()", ex.ToString(), branch_no);
                throw new Exception(ex.Message);
            }
        }

        //获取操作员列表
        DataTable ICommon.GetOperList(string sysn_time, string branch_no)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select oper_id,oper_type,oper_name,oper_pw,oper_status,update_time,is_branch,is_admin,branch_no ";
                sql += "from sa_t_operator_i where 1=1 ";//update_time>=@sysn_time ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time),
                    new System.Data.SqlClient.SqlParameter("@branch_no",branch_no)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetOperList()", ex.ToString(), sysn_time);
                throw new Exception(ex.Message);
            }
        }

        //获取供应商/客户列表
        DataTable ICommon.GetSupCusList(string sysn_time)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select supcust_no,supcust_flag,sup_name,display_flag,sup_tel,sup_pyname,credit_amt,other1,cust_level,is_retail ";
                sql += "from bi_t_supcust_info where update_time>=@sysn_time ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetSupCusList()", ex.ToString(), sysn_time);
                throw new Exception(ex.Message);
            }
        }

        //获取供应商/客户数目
        int ICommon.GetSupCusCount(string sysn_time)
        {
            try
            {
                int count = 0;
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select count(supcust_no) total_count from bi_t_supcust_info where update_time>=@sysn_time ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0) count = Conv.ToInt(dt.Rows[0]["total_count"]);
                return count;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetSupCusCount()", ex.ToString(), sysn_time);
                throw new Exception(ex.Message);
            }
        }

        //批销客户价格表
        DataTable ICommon.GetCusPriceList(string sysn_time, string cust_id, string item_no)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var condition_sql = "";
                if (cust_id != "") condition_sql += " and cust_id=@cust_id ";
                if (item_no != "") condition_sql += " and item_no=@item_no ";
                string sql = "select cust_id,item_no,price_type,new_price,discount,top_price,bottom_price,last_price";
                sql += ",top_sheet_no,bottom_sheet_no,last_sheet_no from bi_t_cust_price where update_time>=@sysn_time " + condition_sql;

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time),
                    new System.Data.SqlClient.SqlParameter("@cust_id",cust_id),
                    new System.Data.SqlClient.SqlParameter("@item_no",item_no)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetCusPriceList()", ex.ToString(), null);
                throw new Exception(ex.Message);
            }
        }

        //批销客户价格数目
        int ICommon.GetCusPriceCount(string sysn_time, string cust_id, string item_no)
        {
            try
            {
                int count = 0;
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var condition_sql = "";
                if (cust_id != "") condition_sql += " and cust_id=@cust_id ";
                if (item_no != "") condition_sql += " and item_no=@item_no ";
                string sql = "select count(*) total_count from bi_t_cust_price where update_time>=@sysn_time " + condition_sql;

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time),
                    new System.Data.SqlClient.SqlParameter("@cust_id",cust_id),
                    new System.Data.SqlClient.SqlParameter("@item_no",item_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0) count = Conv.ToInt(dt.Rows[0]["total_count"]);
                return count;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetCusPriceCount()", ex.ToString(), null);
                throw new Exception(ex.Message);
            }
        }

        //供应商价格表
        DataTable ICommon.GetSupPriceList(string sysn_time, string sup_id, string item_no)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var condition_sql = "";
                if (sup_id != "") condition_sql += " and sup_id=@sup_id ";
                if (item_no != "") condition_sql += " and item_no=@item_no ";
                string sql = "select item_no,sup_id,price,top_price,bottom_price,last_price,spec_from,spec_to,spec_price,item_status";
                sql += ",top_sheet_no,bottom_sheet_no,last_sheet_no from bi_t_sup_item where update_time>=@sysn_time " + condition_sql;

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time),
                    new System.Data.SqlClient.SqlParameter("@sup_id",sup_id),
                    new System.Data.SqlClient.SqlParameter("@item_no",item_no)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetSupPriceList()", ex.ToString(), null);
                throw new Exception(ex.Message);
            }
        }

        //供应商价格表
        int ICommon.GetSupPriceCount(string sysn_time, string sup_id, string item_no)
        {
            try
            {
                int count = 0;
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                var condition_sql = "";
                if (sup_id != "") condition_sql += " and sup_id=@sup_id ";
                if (item_no != "") condition_sql += " and item_no=@item_no ";
                string sql = "select count(*) total_count from bi_t_sup_item where update_time>=@sysn_time " + condition_sql;

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time),
                    new System.Data.SqlClient.SqlParameter("@sup_id",sup_id),
                    new System.Data.SqlClient.SqlParameter("@item_no",item_no)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count > 0) count = Conv.ToInt(dt.Rows[0]["total_count"]);
                return count;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetSupPriceCount()", ex.ToString(), null);
                throw new Exception(ex.Message);
            }
        }

        //价格系统参数表
        DataTable ICommon.GetSystemPars(string sysn_time)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select * from sys_t_system where 1=1";// "update_time>=@sysn_time ";
                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetSystemPars()", ex.ToString(), null);
                throw new Exception(ex.Message);
            }
        }

        //获取仓库
        DataTable ICommon.GetBranchList(string sysn_time)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

                string sql = "select branch_no,branch_name from bi_t_branch_info where 1=1";//"update_time>=@sysn_time ";

                var pars = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@sysn_time",sysn_time)
                };
                var dt = db.ExecuteToTable(sql, pars);

                return dt;

            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetBranchList()", ex.ToString(), sysn_time);
                throw new Exception(ex.Message);
            }
        }

        //获取支付方式列表
        DataTable ICommon.GetPayWayList()
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select * from bt_payment ";
                var dt = db.ExecuteToTable(sql, null);
                return dt;
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Common.GetPayWayList()", ex.ToString(), null);
                throw new Exception(ex.Message);
            }
        }

        void ICommon.UpdatePwd(string branch_no, string oper_id, string pwd, string new_pwd)
        {
            pwd = pwd.ToUpper();
            new_pwd = new_pwd.ToUpper();
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from sa_t_operator_i where branch_no=@branch_no and oper_id=@oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[] 
            { 
                new System.Data.SqlClient.SqlParameter("@oper_id",oper_id),
                new System.Data.SqlClient.SqlParameter("@branch_no",branch_no)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                var temp_pwd = dt.Rows[0]["oper_pw"].ToString();
                if (temp_pwd.ToUpper() != pwd.ToUpper())
                {
                    throw new Exception("旧密码不正确");
                }
                sql = "update sa_t_operator_i set oper_pw=@new_pwd where branch_no=@branch_no and oper_id=@oper_id ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                { 
                    new System.Data.SqlClient.SqlParameter("@oper_id",oper_id),
                    new System.Data.SqlClient.SqlParameter("@new_pwd",new_pwd),
                    new System.Data.SqlClient.SqlParameter("@branch_no",branch_no)
                };
                db.ExecuteScalar(sql, pars);
            }
            else
            {
                throw new Exception("操作员不存在");
            }

        }

        //取客户商品价格
        decimal ICommon.GetCusItemPrice(string cust_id, string item_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            decimal price = 0;
            //
            string sql = "select isnull(other1,'1') other1,isnull(cust_level,'1') cust_level ";
            sql += "from bi_t_supcust_info where supcust_no='" + cust_id + "' and supcust_flag='C' ";
            var dt1 = db.ExecuteToTable(sql, null);
            var price_type = "1";
            int cust_level = 1;
            if (dt1.Rows.Count > 0)
            {
                price_type = dt1.Rows[0]["other1"].ToString();
                cust_level = Conv.ToInt(dt1.Rows[0]["cust_level"]);
                if (cust_level == 0) cust_level = 1;
            }
            if (price_type == "2")//最后价格
            {
                sql = "select * from bi_t_cust_price where cust_id='" + cust_id + "' and item_no='" + item_no + "' ";
                var dt2 = db.ExecuteToTable(sql, null);
                if (dt2.Rows.Count > 0)
                {
                    //bi_t_cust_price.last_price
                    price = Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                    if (price > 0) return price;

                    //bi_t_cust_price.new_price
                    price = Conv.ToDecimal(dt2.Rows[0]["new_price"]);
                    if (price > 0) return price;
                }
            }
            else if (price_type == "1")//约定价格
            {
                sql = "select * from sys_t_system where sys_var_id='cust_pricetype' ";
                var dt3 = db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    var temp_price_type = Conv.ToInt(dt3.Rows[0]["sys_var_value"]);
                    if (temp_price_type == 1) //最后批发价
                    {
                        sql = "select * from bi_t_cust_price where cust_id='" + cust_id + "' and item_no='" + item_no + "' ";
                        var dt2 = db.ExecuteToTable(sql, null);
                        if (dt2.Rows.Count > 0)
                        {
                            //bi_t_cust_price.last_price
                            price = Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                            if (price > 0) return price;

                            //bi_t_cust_price.new_price
                            price = Conv.ToDecimal(dt2.Rows[0]["new_price"]);
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
                            price = Conv.ToDecimal(dt2.Rows[0]["bottom_price"]);
                            if (price > 0) return price;

                            //bi_t_cust_price.new_price
                            price = Conv.ToDecimal(dt2.Rows[0]["new_price"]);
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
                            price = Conv.ToDecimal(dt2.Rows[0]["last_price"]);
                            if (price > 0) return price;
                        }
                    }
                }
            }

            //以上价格策略无法取到相应价格时，默认取商品档案约定级别价格
            if (1 == 1)
            {
                //bi_t_item_info中约定的商品价格
                sql = "select base_price,base_price2,base_price3,base_price4,base_price5 ";
                sql += "from bi_t_item_info where item_no='" + item_no + "' ";
                var dt3 = db.ExecuteToTable(sql, null);
                if (dt3.Rows.Count > 0)
                {
                    //取客户级别价格，如无级别则默认取base_price
                    if (dt3.Columns.Count >= cust_level)
                    {
                        price = Conv.ToDecimal(dt3.Rows[0][cust_level - 1]);
                        if (price > 0) return price;
                    }
                    else
                    {
                        price = Conv.ToDecimal(dt3.Rows[0][0]);
                        if (price > 0) return price;
                    }
                }
            }
            //

            return price;
        }

        //客户已用额度
        void ICommon.GetBalance(string cus_no, out decimal balance, out decimal credit_amt)
        {
            balance = 0;
            credit_amt = 0;
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            //已用额度
            string sql = "select isnull(sum(a.pay_type*a.sheet_amount - a.pay_type*a.paid_amount - a.pay_type*a.free_money),0) as sum_money ";
            sql += "from rp_t_accout_payrec_flow a ";
            sql += "inner join bi_t_supcust_info b on a.supcust_no = b.supcust_no ";
            sql += "where (a.supcust_flag = 'c') and b.supcust_flag = 'c' and a.supcust_no=cus_no ";
            sql += "and (a.pay_type * a.sheet_amount <> a.pay_type * (a.paid_amount + a.free_money )) ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cus_no", cus_no)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt != null && dt.Rows.Count > 0) balance = Conv.ToDecimal(dt.Rows[0]["sum_money"]);
            balance = 0 - balance;

            //信用额度
            sql = "select isnull(credit_amt,0) credit_amt from bi_t_supcust_info where supcust_flag='C' and supcust_no=@cus_no ";
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@cus_no", cus_no)
            };
            dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0) credit_amt = Conv.ToDecimal(dt.Rows[0]["credit_amt"]);
        }

        void ICommon.AddJH(Model.netsetup ns, out string jh, out int err_id, out string msg)
        {
            jh = "";
            err_id = 0;
            msg = "";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from netsetup where other=@macid and softpos='0'";
            System.Data.SqlClient.SqlParameter[] pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@macid", ns.other)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                jh = dt.Rows[0]["jh"].ToString();
                return;
            }
            //
            sql = "select * from netsetup where jh=@jh and softpos='0'";
            pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@jh", ns.jh)
            };
            dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                err_id = -1;
                msg = "已存在相同机号";
                return;
            }
            db.Insert(ns);
        }

        void ICommon.DelJH(Model.netsetup ns)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var count = Convert.ToInt32(db.ExecuteScalar("select count(*) from netsetup where jh='" + ns.jh + "' and softpos='" + ns.softpos + "'", null));
            if (count > 0)
            {
                db.ExecuteScalar("delete netsetup where jh='" + ns.jh + "' and softpos='" + ns.softpos + "' ", null);
            }
            else
            {
                throw new Exception("机号[" + ns.jh + "]不存在");
            }
        }

        DataTable ICommon.GetJHList()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var dt = db.ExecuteToTable("select * from netsetup  ", null);
            return dt;
        }

        string ICommon.GetMacJH(string mac_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select * from netsetup where other=@mac_id and softpos='0' ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mac_id", mac_id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                var jh = dt.Rows[0]["jh"].ToString();
                sql = "select * from netsetup where softpos='0' and jh='" + jh + "' ";
                dt = db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 1)
                {
                    throw new Exception("存在重复机号[" + jh + "]");
                }
                return jh;
            }
            else
            {
                return "";
            }
        }
    }
}
