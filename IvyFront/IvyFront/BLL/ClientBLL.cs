using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IvyFront.IBLL;

namespace IvyFront.BLL
{
    public class ClientBLL:IClientBLL
    {
        int IClientBLL.GetItemCount(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                int count = 0;
                var sql = "select * from bt_sysn_info where table_name='bi_t_item_info' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                var json = req.request("/common?t=get_item_count", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.GetItemCount()", errMsg, "/common?t=get_item_count");
                    return 0;
                }
                count = Conv.ToInt(read.Read("total_count"));
                return count;
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->GetItemCount()", ex.ToString(), null);
                throw ex;
            }
        }

        void IClientBLL.DownLoadItem(out int errId,out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='bi_t_item_info' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                var json = req.request("/common?t=get_item_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadItem()", errMsg, "/common?t=get_item_list");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string item_no = r.Read("item_no");
                        string item_subno = r.Read("item_subno");
                        string item_subname = r.Read("item_subname");
                        string item_clsno = r.Read("item_clsno");
                        string item_name = r.Read("item_name");
                        string item_brand = r.Read("item_brand");
                        string item_brandname = r.Read("item_brandname");
                        string unit_no = r.Read("unit_no");
                        string item_size = r.Read("item_size");
                        string product_area = r.Read("product_area");
                        string barcode = r.Read("barcode");
                        string sale_price = r.Read("sale_price");
                        string price = r.Read("price");
                        string base_price = r.Read("base_price");
                        string display_flag = r.Read("display_flag");
                        string item_flag = r.Read("item_flag");
                        string base_price2 = r.Read("base_price2");
                        string base_price3 = r.Read("base_price3");
                        string base_price4 = r.Read("base_price4");
                        string base_price5 = r.Read("base_price5");
                        //
                        string sql2 = "select 1 from bi_t_item_info where item_no='" + item_no + "' ";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update bi_t_item_info set item_subno='" + item_subno + "',item_subname='" + item_subname + "',item_clsno='" + item_clsno + "'";
                            sql += ",item_name='" + item_name + "',item_brand='" + item_brand + "',item_brandname='" + item_brandname + "',unit_no='" + unit_no + "'";
                            sql += ",item_size='" + item_size + "',product_area='" + product_area + "',barcode='" + barcode + "',price='" + price + "'";
                            sql += ",base_price='" + base_price + "',sale_price='" + sale_price + "',display_flag='" + display_flag + "',item_flag='" + item_flag + "'";
                            sql += ",base_price2='" + base_price3 + "',base_price3='" + base_price3 + "',base_price4='" + base_price4 + "',base_price5='" + base_price5 + "' ";
                            sql += "where item_no='" + item_no + "' ";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into bi_t_item_info values('" + item_no + "','" + item_subno + "','" + item_subname + "','" + item_clsno + "','" + item_name + "','" + item_brand + "'";
                            sql += ",'" + item_brandname + "','" + unit_no + "','" + item_size + "','" + product_area + "','" + barcode + "','" + price + "','" + base_price + "'";
                            sql += ",'" + sale_price + "','" + display_flag + "','" + item_flag + "','" + base_price2 + "','" + base_price3 + "','" + base_price4 + "','" + base_price5 + "')";
                            Program.db.ExecuteScalar(sql,null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='bi_t_item_info' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('bi_t_item_info','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex) {
                Log.writeLog("ClientBLL ->DownLoadItem()", ex.ToString(), null);
                throw ex;
            }
        }

        void IClientBLL.DownLoadItemCls(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='bi_t_item_cls' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                var json = req.request("/common?t=get_itemcls_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadItemCls()", errMsg, "/common?t=get_itemcls_list");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string item_clsno = r.Read("item_clsno");
                        string item_clsname = r.Read("item_clsname");
                        string item_flag = r.Read("item_flag");
                        string display_flag = r.Read("display_flag");
                        //
                        string sql2 = "select 1 from bi_t_item_cls where item_clsno='" + item_clsno + "' ";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update bi_t_item_cls set item_clsname='" + item_clsname + "',item_flag='" + item_flag + "',display_flag='" + display_flag + "' where item_clsno='" + item_clsno + "' ";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into bi_t_item_cls values('" + item_clsno + "','" + item_flag + "','" + item_clsname + "','" + display_flag + "','0')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='bi_t_item_cls' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('bi_t_item_cls','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadItemCls()", ex.ToString(), null);
                throw ex;
            }
        }

        void IClientBLL.DownLoadStock(out int errId, out string errMsg) 
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='ic_t_branch_stock' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                write.Append("branch_no", Program.branch_no);
                var json = req.request("/common?t=get_stock_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadStock()", errMsg, "/common?t=get_stock_list");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    sql = "delete from ic_t_branch_stock where branch_no='" + Program.branch_no + "' ";
                    Program.db.ExecuteScalar(sql, null);

                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string branch_no = r.Read("branch_no");
                        string item_no = r.Read("item_no");
                        string stock_qty = r.Read("stock_qty");
                        string cost_price = r.Read("cost_price");
                        string display_flag = r.Read("display_flag");
                        string last_price = r.Read("last_price");
                        string fifo_price = r.Read("fifo_price");
                        //
                        string sql2 = "select 1 from ic_t_branch_stock where branch_no='" + branch_no + "' and item_no='" + item_no + "' ";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update ic_t_branch_stock set stock_qty='" + stock_qty + "',cost_price='" + cost_price + "',display_flag='" + display_flag + "'";
                            sql += ",last_price='" + last_price + "',fifo_price='" + fifo_price + "' where branch_no='" + branch_no + "' and item_no='" + item_no + "'";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into ic_t_branch_stock values('" + branch_no + "','" + item_no + "','" + stock_qty + "','" + cost_price + "','" + display_flag + "','" + last_price + "','" + fifo_price + "')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='ic_t_branch_stock' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('ic_t_branch_stock','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadStock()", ex.ToString(), null);
                throw ex;
            }
        }

        void IClientBLL.DownLoadOper(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='sa_t_operator_i' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                write.Append("branch_no", Program.branch_no);
                var json = req.request("/common?t=get_oper_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadOper()", errMsg, "/common?t=get_oper_list");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string oper_id = r.Read("oper_id");
                        string oper_type = r.Read("oper_type");
                        string oper_name = r.Read("oper_name");
                        string oper_pw = r.Read("oper_pw");
                        string oper_status = r.Read("oper_status");
                        string update_time = sysn_time;
                        string is_branch = r.Read("is_branch");
                        string is_admin = r.Read("is_admin");
                        string branch_no = r.Read("branch_no");
                        //
                        string sql2 = "select 1 from sa_t_operator_i where oper_id='" + oper_id + "' and branch_no='" + branch_no + "'";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update sa_t_operator_i set oper_type='" + oper_type + "',oper_name='" + oper_name + "',oper_pw='" + oper_pw + "',oper_status='" + oper_status + "'";
                            sql += ",update_time='" + update_time + "',is_branch='" + is_branch + "',is_admin='" + is_admin + "' where oper_id='" + oper_id + "' and branch_no='" + branch_no + "'";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into sa_t_operator_i values('" + oper_id + "','" + oper_type + "','" + oper_name + "','" + oper_pw + "','" + oper_status + "'";
                            sql += ",'" + update_time + "','" + is_branch + "','" + is_admin + "','" + branch_no + "')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='sa_t_operator_i' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('sa_t_operator_i','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadOper()", ex.ToString(), null);
                throw ex;
            }
        }

        int IClientBLL.GetSupCusCount(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                int count = 0;
                var sql = "select * from bt_sysn_info where table_name='bi_t_supcust_info' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                var json = req.request("/common?t=get_sup_count", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.GetSupCusCount()", errMsg, "/common?t=get_sup_count");
                    return 0;
                }
                count = Conv.ToInt(read.Read("total_count"));
                return count;
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->GetSupCusCount()", ex.ToString(), null);
                throw ex;
            }

        }

        void IClientBLL.DownLoadSupCus(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='bi_t_supcust_info' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                var json = req.request("/common?t=get_sup_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadSupCus()", errMsg, "/common?t=get_sup_list");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string supcust_no = r.Read("supcust_no");
                        string supcust_flag = r.Read("supcust_flag");
                        string sup_name = r.Read("sup_name");
                        string display_flag = r.Read("display_flag");
                        string sup_tel = r.Read("sup_tel");
                        string sup_pyname = r.Read("sup_pyname");
                        string credit_amt = r.Read("credit_amt");
                        string other1 = r.Read("other1");
                        string cust_level = r.Read("cust_level");
                        string is_retail = r.Read("is_retail");
                        //
                        string sql2 = "select 1 from bi_t_supcust_info where supcust_no='" + supcust_no + "' and supcust_flag='" + supcust_flag + "'";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update bi_t_supcust_info set sup_name='" + sup_name + "',display_flag='" + display_flag + "',sup_tel='" + sup_tel + "',sup_pyname='" + sup_pyname + "'";
                            sql += ",credit_amt='" + credit_amt + "',other1='" + other1 + "',cust_level='" + cust_level + "',is_retail='" + is_retail + "' ";
                            sql += "where supcust_no='" + supcust_no + "' and supcust_flag='" + supcust_flag + "'";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into bi_t_supcust_info values('" + supcust_no + "','" + supcust_flag + "','" + sup_name + "','" + sup_tel + "','" + sup_pyname + "'";
                            sql += ",'" + display_flag + "','" + credit_amt + "','" + other1 + "','" + cust_level + "','" + is_retail + "')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='bi_t_supcust_info' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('bi_t_supcust_info','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadSupCus()", ex.ToString(), null);
                throw ex;
            }
        
        }

        void IClientBLL.DownLoadBranch(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='bi_t_branch_info' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                var json = req.request("/common?t=get_branch_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadBranch()", errMsg, "/common?t=get_branch_list");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string branch_no = r.Read("branch_no");
                        string branch_name = r.Read("branch_name");
                        //
                        string sql2 = "select 1 from bi_t_branch_info where branch_no='" + branch_no + "'";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update bi_t_branch_info set branch_name='" + branch_name + "' where branch_no='" + branch_no + "'";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into bi_t_branch_info values('" + branch_no + "','" + branch_name + "')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='bi_t_branch_info' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('bi_t_branch_info','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadBranch()", ex.ToString(), null);
                throw ex;
            }

        }

        int IClientBLL.GetCusPriceCount(string cust_id, string item_no, out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                int count = 0;
                var sql = "select * from bt_sysn_info where table_name='bi_t_cust_price' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                write.Append("cust_id", cust_id);
                write.Append("item_no", item_no);
                var json = req.request("/common?t=get_cus_price_count", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.GetCusPriceCount()", errMsg, "/common?t=get_cus_price_count");
                    return 0;
                }
                count = Conv.ToInt(read.Read("total_count"));
                return count;
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->GetCusPriceCount()", ex.ToString(), null);
                throw ex;
            }

        }

        void IClientBLL.DownLoadCusPrice(string cust_id, string item_no,out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='bi_t_cust_price' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                write.Append("cust_id", cust_id);
                write.Append("item_no", item_no);
                var json = req.request("/common?t=get_cus_price_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadCusPrice()", errMsg, "/common?t=get_cus_price_list");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string t_cust_id = r.Read("cust_id");
                        string t_item_no = r.Read("item_no");
                        string price_type = r.Read("price_type");
                        decimal new_price = Conv.ToDecimal(r.Read("new_price"));
                        decimal top_price = Conv.ToDecimal(r.Read("top_price"));
                        decimal bottom_price = Conv.ToDecimal(r.Read("bottom_price"));
                        decimal last_price = Conv.ToDecimal(r.Read("last_price"));
                        string top_sheet_no = r.Read("top_sheet_no");
                        string bottom_sheet_no = r.Read("bottom_sheet_no");
                        string last_sheet_no = r.Read("last_sheet_no");
                        //
                        string sql2 = "select 1 from bi_t_cust_price where cust_id='" + t_cust_id + "' and item_no='" + t_item_no + "'";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update bi_t_cust_price set price_type='" + price_type + "',new_price='" + new_price + "',top_price='" + top_price + "'";
                            sql += ",bottom_price='" + bottom_price + "',last_price='" + last_price + "',top_sheet_no='" + top_sheet_no + "'";
                            sql += ",bottom_sheet_no='" + bottom_sheet_no + "',last_sheet_no='" + last_sheet_no + "' where cust_id='" + t_cust_id + "' and item_no='" + t_item_no + "'";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into bi_t_cust_price values('" + t_cust_id + "','" + t_item_no + "','" + price_type + "','" + new_price + "','" + top_price + "'";
                            sql += ",'" + bottom_price + "','" + last_price + "','" + top_sheet_no + "','" + bottom_sheet_no + "','" + last_sheet_no + "')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='bi_t_cust_price' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('bi_t_cust_price','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadCusPrice()", ex.ToString(), null);
                throw ex;
            }

        }

        int IClientBLL.GetSupPriceCount(string sup_id, string item_no, out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                int count = 0;
                var sql = "select * from bt_sysn_info where table_name='bi_t_sup_item' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                write.Append("sup_id", sup_id);
                write.Append("item_no", item_no);
                var json = req.request("/common?t=get_sup_price_count", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.GetSupPriceCount()", errMsg, "/common?t=get_sup_price_count");
                    return 0;
                }
                count = Conv.ToInt(read.Read("total_count"));
                return count;
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->GetSupPriceCount()", ex.ToString(), null);
                throw ex;
            }

        }

        void IClientBLL.DownLoadSupPrice(string sup_id, string item_no, out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='bi_t_sup_item' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                write.Append("sup_id", sup_id);
                write.Append("item_no", item_no);
                var json = req.request("/common?t=get_sup_price_list", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadSupPrice()", errMsg, "/common?t=get_sup_price_list");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string t_sup_id = r.Read("sup_id");
                        string t_item_no = r.Read("item_no");
                        decimal price = Conv.ToDecimal(r.Read("price"));
                        decimal top_price = Conv.ToDecimal(r.Read("top_price"));
                        decimal bottom_price = Conv.ToDecimal(r.Read("bottom_price"));
                        decimal last_price = Conv.ToDecimal(r.Read("last_price"));
                        string top_sheet_no = r.Read("top_sheet_no");
                        string bottom_sheet_no = r.Read("bottom_sheet_no");
                        string last_sheet_no = r.Read("last_sheet_no");
                        string spec_from = r.Read("spec_from");
                        string spec_to = r.Read("spec_to");
                        decimal spec_price = Conv.ToDecimal(r.Read("spec_price"));
                        string item_status = r.Read("item_status");
                        //
                        string sql2 = "select 1 from bi_t_sup_item where sup_id='" + t_sup_id + "' and item_no='" + t_item_no + "'";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update bi_t_sup_item set price='" + price + "',top_price='" + top_price + "',bottom_price='" + bottom_price + "',last_price='" + last_price + "'";
                            sql += ",top_sheet_no='" + top_sheet_no + "',bottom_sheet_no='" + bottom_sheet_no + "',last_sheet_no='" + last_sheet_no + "',spec_from='" + spec_from + "'";
                            sql += ",spec_to='" + spec_to + "',spec_price='" + spec_price + "',item_status='" + item_status + "' ";
                            sql += "where sup_id='" + t_sup_id + "' and item_no='" + t_item_no + "'";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into bi_t_sup_item values('" + t_sup_id + "','" + t_item_no + "','" + price + "','" + top_price + "','" + bottom_price + "','" + last_price + "'";
                            sql += ",'" + top_sheet_no + "','" + bottom_sheet_no + "','" + last_sheet_no + "','" + spec_from + "','" + spec_to + "','" + spec_price + "','" + item_status + "')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='bi_t_sup_item' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('bi_t_sup_item','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadSupPrice()", ex.ToString(), null);
                throw ex;
            }

        }

        void IClientBLL.DownLoadSystemPars(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var sql = "select * from bt_sysn_info where table_name='sys_t_system' ";
                var dt = Program.db.ExecuteToTable(sql, null);
                var sysn_time = "1990-01-01";
                if (dt != null && dt.Rows.Count > 0) sysn_time = dt.Rows[0]["sysn_time"].ToString();
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("sysn_time", sysn_time);
                var json = req.request("/common?t=get_system_pars", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadSystemPars()", errMsg, "/common?t=get_system_pars");
                    return;
                }
                sysn_time = read.Read("sysn_time");
                int change_count = 0;
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        ++change_count;
                        string sys_var_id = r.Read("sys_var_id");
                        string sys_var_name = r.Read("sys_var_name");
                        string sys_var_value = r.Read("sys_var_value");
                        string is_changed = r.Read("is_changed");
                        string sys_var_desc = r.Read("sys_var_desc");
                        string sys_ver_flag = r.Read("sys_ver_flag");
                        string update_time = sysn_time;
                        //
                        string sql2 = "select 1 from sys_t_system where sys_var_id='" + sys_var_id + "' ";
                        dt = Program.db.ExecuteToTable(sql2, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update sys_t_system set sys_var_name='" + sys_var_name + "',sys_var_value='" + sys_var_value + "',is_changed='" + is_changed + "'";
                            sql += ",sys_var_desc='" + sys_var_desc + "',sys_ver_flag='" + sys_ver_flag + "',update_time='" + update_time + "' ";
                            sql += "where sys_var_id='" + sys_var_id + "' ";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into sys_t_system values('" + sys_var_id + "','" + sys_var_name + "','" + sys_var_value + "','" + is_changed + "'";
                            sql += ",'" + sys_var_desc + "','" + sys_ver_flag + "','" + update_time + "')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
                if (change_count > 0)
                {
                    sql = "delete from bt_sysn_info where table_name='sys_t_system' ";
                    Program.db.ExecuteScalar(sql, null);

                    sql = "insert into bt_sysn_info values('sys_t_system','" + sysn_time + "')";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadSystemPars()", ex.ToString(), null);
                throw ex;
            }

        }

        void IClientBLL.DownLoadPayWay(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                IRequest req = new WebServiceRequest();
                var json = req.request("/common?t=get_payway_list", "");
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    errId = 1;
                    errMsg = read.Read("errMsg");
                    Log.writeLog("ClientBLL.DownLoadPayWay()", errMsg, "/common?t=get_payway_list");
                    return;
                }
                var lst = new List<string>();
                if (read.Read("datas") != null)
                {
                    foreach (ReadWriteContext.IReadContext r in read.ReadList("datas"))
                    {
                        string pay_way = r.Read("pay_way");
                        string pay_name = r.Read("pay_name");
                        string s_property = r.Read("s_property");
                        string display = r.Read("display");
                        //
                        string sql = "select 1 from bt_payment where pay_way='" + pay_way + "' ";
                        var dt = Program.db.ExecuteToTable(sql, null);
                        if (dt.Rows.Count > 0)
                        {
                            sql = "update bt_payment set pay_name='" + pay_name + "',s_property='" + s_property + "',display='" + display + "' where pay_way='" + pay_way + "' ";
                            Program.db.ExecuteScalar(sql, null);
                        }
                        else
                        {
                            sql = "insert into bt_payment values('" + pay_way + "','" + pay_name + "','" + s_property + "','" + display + "','0')";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->DownLoadPayWay()", ex.ToString(), null);
                throw ex;
            }
        }
        
        void IClientBLL.UpLoadSale(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var in_data = "";
                var in_data2 = "";
                var in_data3 = "";
                var sql = "select * from sm_t_salesheet where ifnull(is_upload,'0')='0' order by oper_date asc limit 100 ";
                var dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    var sheet_nos = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        sheet_nos += "'" + dr["sheet_no"].ToString() + "',";
                        in_data += "{";
                        in_data += "\"sheet_no\":\"" + dr["sheet_no"].ToString() + "\",";
                        in_data += "\"branch_no\":\"" + dr["branch_no"].ToString() + "\",";
                        in_data += "\"cust_no\":\"" + dr["cust_no"].ToString() + "\",";
                        in_data += "\"pay_way\":\"" + dr["pay_way"].ToString() + "\",";
                        in_data += "\"coin_no\":\"" + dr["coin_no"].ToString() + "\",";
                        in_data += "\"real_amount\":\"" + dr["real_amount"].ToString() + "\",";
                        in_data += "\"total_amount\":\"" + dr["total_amount"].ToString() + "\",";
                        in_data += "\"paid_amount\":\"" + dr["paid_amount"].ToString() + "\",";
                        in_data += "\"approve_flag\":\"" + dr["approve_flag"].ToString() + "\",";
                        in_data += "\"oper_id\":\"" + dr["oper_id"].ToString() + "\",";
                        in_data += "\"sale_man\":\"" + dr["sale_man"].ToString() + "\",";
                        in_data += "\"oper_date\":\"" + dr["oper_date"].ToString() + "\",";
                        in_data += "\"pay_date\":\"" + dr["pay_date"].ToString() + "\",";
                        in_data += "\"discount\":\"" + dr["discount"].ToString() + "\"";
                        in_data += "},";
                    }
                    if (in_data.Length > 0)
                    {
                        in_data = in_data.Substring(0, in_data.Length - 1);
                        //in_data = "[" + in_data + "]";
                    }

                    if (sheet_nos.Length > 0) sheet_nos = sheet_nos.Substring(0, sheet_nos.Length - 1);

                    if (sheet_nos.Length > 0)
                    {
                        sql = "select * from sm_t_salesheet_detail where sheet_no in (" + sheet_nos + ") order by sheet_no,sheet_sort";
                        dt = Program.db.ExecuteToTable(sql, null);
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            in_data2 += "{";
                            in_data2 += "\"sheet_no\":\"" + dr["sheet_no"].ToString() + "\",";
                            in_data2 += "\"item_no\":\"" + dr["item_no"].ToString() + "\",";
                            in_data2 += "\"item_name\":\"" + dr["item_name"].ToString() + "\",";
                            in_data2 += "\"unit_no\":\"" + dr["unit_no"].ToString() + "\",";
                            in_data2 += "\"sale_price\":\"" + dr["sale_price"].ToString() + "\",";
                            in_data2 += "\"real_price\":\"" + dr["real_price"].ToString() + "\",";
                            in_data2 += "\"cost_price\":\"" + dr["cost_price"].ToString() + "\",";
                            in_data2 += "\"sale_qnty\":\"" + dr["sale_qnty"].ToString() + "\",";
                            in_data2 += "\"sale_money\":\"" + dr["sale_money"].ToString() + "\",";
                            in_data2 += "\"barcode\":\"" + dr["barcode"].ToString() + "\",";
                            in_data2 += "\"sheet_sort\":\"" + dr["sheet_sort"].ToString() + "\",";
                            in_data2 += "\"other3\":\"" + dr["other3"].ToString() + "\"";
                            in_data2 += "},";

                        }
                        
                        sql = "select * from ot_pay_flow where sheet_no in (" + sheet_nos + ") and ifnull(is_upload,'0')='0' order by sheet_no,flow_id";
                        dt = Program.db.ExecuteToTable(sql, null);

                        foreach (DataRow dr in dt.Rows)
                        {
                            in_data3 += "{";
                            in_data3 += "\"sheet_no\":\"" + dr["sheet_no"].ToString() + "\",";
                            in_data3 += "\"flow_id\":\"" + dr["flow_id"].ToString() + "\",";
                            in_data3 += "\"cus_no\":\"" + dr["cus_no"].ToString() + "\",";
                            in_data3 += "\"oper_id\":\"" + dr["oper_id"].ToString() + "\",";
                            in_data3 += "\"oper_date\":\"" + dr["oper_date"].ToString() + "\",";
                            in_data3 += "\"pay_way\":\"" + dr["pay_way"].ToString() + "\",";
                            in_data3 += "\"sale_amount\":\"" + dr["sale_amount"].ToString() + "\",";
                            in_data3 += "\"pay_amount\":\"" + dr["pay_amount"].ToString() + "\",";
                            in_data3 += "\"old_amount\":\"" + dr["old_amount"].ToString() + "\",";
                            in_data3 += "\"ml\":\"" + dr["ml"].ToString() + "\",";
                            in_data3 += "\"jh\":\"" + dr["jh"].ToString() + "\",";
                            in_data3 += "\"remark\":\"" + dr["remark"].ToString() + "\"";
                            in_data3 += "},";
                
                        }
                        
                        if (in_data2.Length > 0)
                        {
                            in_data2 = in_data2.Substring(0, in_data2.Length - 1);
                            //in_data2 = "[" + in_data2 + "]";
                        }

                        if (in_data3.Length > 0)
                        {
                            in_data3 = in_data3.Substring(0, in_data3.Length - 1);
                            //in_data3 = "[" + in_data3 + "]";
                        }
                    }
                    if (in_data.Length > 0 || in_data2.Length > 0)
                    {

                        string pars = "{\"branch_no\":\"" + Program.branch_no + "\",\"sale_master\":{\"datas\":[" + in_data + "]},\"sale_detail\":{\"datas\":[" + in_data2 + "]},\"pay_data\":{\"datas\":[" + in_data3 + "]}}";
                        IRequest req = new WebServiceRequest();
                        var json = req.request("/settle?t=upload_fhd", pars);
                        ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                        if (read.Read("errId") != "0")
                        {
                            errId = 1;
                            errMsg = read.Read("errMsg");
                            Log.writeLog("ClientBLL.UpLoadSale()", errMsg, "/settle?t=upload_fhd");
                        }
                        else
                        {
                            sql = "update sm_t_salesheet set is_upload='1' where sheet_no in (" + sheet_nos + ") ";
                            Program.db.ExecuteScalar(sql, null);

                            sql = "update ot_pay_flow set is_upload='1' where sheet_no in (" + sheet_nos + ") ";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                    else
                    {
                        errId = 0;
                        errMsg = "无数据";
                    }
                }
                else
                {
                    errId = 0;
                    errMsg = "无数据";
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("Client ->UpLoadSale()", ex.ToString(), null);
                throw ex;
            }
        }

        void IClientBLL.UpLoadInOut(out int errId, out string errMsg)
        {
            errId = 0;
            errMsg = "";
            try
            {
                var in_data = "";
                var in_data2 = "";
                var in_data3 = "";
                var sql = "select * from ic_t_inout_store_master where ifnull(is_upload,'0')='0' order by oper_date asc limit 100 ";
                var dt = Program.db.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    var sheet_nos = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        sheet_nos += "'" + dr["sheet_no"].ToString() + "',";
                        in_data += "{";
                        in_data += "\"sheet_no\":\"" + dr["sheet_no"].ToString() + "\",";
                        in_data += "\"trans_no\":\"" + dr["trans_no"].ToString() + "\",";
                        in_data += "\"branch_no\":\"" + dr["branch_no"].ToString() + "\",";
                        in_data += "\"supcust_no\":\"" + dr["supcust_no"].ToString() + "\",";
                        in_data += "\"total_amount\":\"" + dr["total_amount"].ToString() + "\",";
                        in_data += "\"inout_amount\":\"" + dr["inout_amount"].ToString() + "\",";
                        in_data += "\"approve_flag\":\"" + dr["approve_flag"].ToString() + "\",";
                        in_data += "\"oper_date\":\"" + dr["oper_date"].ToString() + "\",";
                        in_data += "\"oper_id\":\"" + dr["oper_id"].ToString() + "\",";
                        in_data += "\"pay_way\":\"" + dr["pay_way"].ToString() + "\"";
                        in_data += "},";

                    }
                    if (in_data.Length > 0)
                    {
                        in_data = in_data.Substring(0, in_data.Length - 1);
                        //in_data = "[" + in_data + "]";
                    }

                    if (sheet_nos.Length > 0) sheet_nos = sheet_nos.Substring(0, sheet_nos.Length - 1);

                    if (sheet_nos.Length > 0)
                    {
                        sql = "select * from ic_t_inout_store_detail where sheet_no in (" + sheet_nos + ") order by sheet_no,sheet_sort";
                        dt = Program.db.ExecuteToTable(sql, null);

                        foreach (DataRow dr in dt.Rows)
                        {
                            in_data2 += "{";
                            in_data2 += "\"sheet_no\":\"" + dr["sheet_no"].ToString() + "\",";
                            in_data2 += "\"item_no\":\"" + dr["item_no"].ToString() + "\",";
                            in_data2 += "\"item_name\":\"" + dr["item_name"].ToString() + "\",";
                            in_data2 += "\"unit_no\":\"" + dr["unit_no"].ToString() + "\",";
                            in_data2 += "\"in_qty\":\"" + dr["in_qty"].ToString() + "\",";
                            in_data2 += "\"orgi_price\":\"" + dr["orgi_price"].ToString() + "\",";
                            in_data2 += "\"valid_price\":\"" + dr["valid_price"].ToString() + "\",";
                            in_data2 += "\"cost_price\":\"" + dr["cost_price"].ToString() + "\",";
                            in_data2 += "\"valid_date\":\"" + dr["valid_date"].ToString() + "\",";
                            in_data2 += "\"barcode\":\"" + dr["barcode"].ToString() + "\",";
                            in_data2 += "\"sheet_sort\":\"" + dr["sheet_sort"].ToString() + "\",";
                            in_data2 += "\"other3\":\"" + dr["other3"].ToString() + "\"";
                            in_data2 += "},";
                        }

                        sql = "select * from ot_pay_flow where sheet_no in (" + sheet_nos + ") and ifnull(is_upload,'0')='0' order by sheet_no,flow_id";
                        dt = Program.db.ExecuteToTable(sql, null);

                        foreach (DataRow dr in dt.Rows)
                        {
                            in_data3 += "{";
                            in_data3 += "\"sheet_no\":\"" + dr["sheet_no"].ToString() + "\",";
                            in_data3 += "\"flow_id\":\"" + dr["flow_id"].ToString() + "\",";
                            in_data3 += "\"cus_no\":\"" + dr["cus_no"].ToString() + "\",";
                            in_data3 += "\"oper_id\":\"" + dr["oper_id"].ToString() + "\",";
                            in_data3 += "\"oper_date\":\"" + dr["oper_date"].ToString() + "\",";
                            in_data3 += "\"pay_way\":\"" + dr["pay_way"].ToString() + "\",";
                            in_data3 += "\"sale_amount\":\"" + dr["sale_amount"].ToString() + "\",";
                            in_data3 += "\"pay_amount\":\"" + dr["pay_amount"].ToString() + "\",";
                            in_data3 += "\"old_amount\":\"" + dr["old_amount"].ToString() + "\",";
                            in_data3 += "\"ml\":\"" + dr["ml"].ToString() + "\",";
                            in_data3 += "\"jh\":\"" + dr["jh"].ToString() + "\",";
                            in_data3 += "\"remark\":\"" + dr["remark"].ToString() + "\"";
                            in_data3 += "},";

                        }

                        if (in_data2.Length > 0)
                        {
                            in_data2 = in_data2.Substring(0, in_data2.Length - 1);
                        }
                        if (in_data3.Length > 0)
                        {
                            in_data3 = in_data3.Substring(0, in_data3.Length - 1);
                        }
                    }
                    if (in_data.Length > 0 || in_data2.Length > 0)
                    {

                        string pars = "{\"branch_no\":\"" + Program.branch_no + "\",\"inout_master\":{\"datas\":[" + in_data + "]},\"inout_detail\":{\"datas\":[" + in_data2 + "]},\"pay_data\":{\"datas\":[" + in_data3 + "]}}";
                        IRequest req = new WebServiceRequest();
                        var json = req.request("/settle?t=upload_cgrk", pars);
                        ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                        if (read.Read("errId") != "0")
                        {
                            errId = 1;
                            errMsg = read.Read("errMsg");
                            Log.writeLog("ClientBLL.UpLoadInOut()", errMsg, "/settle?t=upload_cgrk");
                        }
                        else
                        {
                            sql = "update ic_t_inout_store_master set is_upload='1' where sheet_no in (" + sheet_nos + ") ";
                            Program.db.ExecuteScalar(sql, null);

                            sql = "update ot_pay_flow set is_upload='1' where sheet_no in (" + sheet_nos + ") ";
                            Program.db.ExecuteScalar(sql, null);
                        }
                    }
                    else
                    {
                        errId = 0;
                        errMsg = "无数据";
                    }
                }
                else
                {
                    errId = 0;
                    errMsg = "无数据";
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("Client ->UpLoadInOut()", ex.ToString(), null);
                throw ex;
            }
        }

        int IClientBLL.GetNoUpLoadSaleCount() 
        {
            var sql = "select count(sheet_no) total_count from sm_t_salesheet where ifnull(is_upload,'0')='0' ";
            var dt = Program.db.ExecuteToTable(sql, null);
            int count = 0;
            if (dt.Rows.Count > 0)
            {
                count = Conv.ToInt(dt.Rows[0]["total_count"]);
            }
            return count;
        }

        int IClientBLL.GetNoUpLoadInOutCount()
        {
            var sql = "select count(sheet_no) total_count from ic_t_inout_store_master where ifnull(is_upload,'0')='0' ";
            var dt = Program.db.ExecuteToTable(sql, null);
            int count = 0;
            if (dt.Rows.Count > 0)
            {
                count = Conv.ToInt(dt.Rows[0]["total_count"]);
            }
            return count;
        }

        bool IClientBLL.ChangePwd(string branch_no, string oper_id, string pwd, string new_pwd)
        {
            try
            {
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("branch_no", branch_no);
                write.Append("oper_id", oper_id);
                write.Append("pwd", pwd);
                write.Append("new_pwd", new_pwd);
                var json = req.request("/common?t=update_pwd", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->ChangePwd()", ex.ToString(), branch_no, oper_id);
                throw ex;
            }
        }

        bool IClientBLL.CheckConnect()
        {
            try
            {
                bool is_connect = false;
                IRequest req = new WebServiceRequest();
                var json = req.request("/common?t=connect_server", "");
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                if (read.Read("is_connect") == "1") 
                {
                    is_connect = true;
                }
                return is_connect;
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->CheckConnect()", ex.ToString(), null);
                return false;
            }
        }

        decimal IClientBLL.GetCusItemPrice(string cust_id, string item_no)
        {
            try
            {
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("cust_id", cust_id);
                write.Append("item_no", item_no);
                var json = req.request("/common?t=get_cus_item_price", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                return Conv.ToDecimal(read.Read("price"));
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->GetCusItemPrice()", ex.ToString(), null);
                return 0;
            }
        }

        void IClientBLL.GetCusBalance(string cust_id, out decimal balance, out decimal credit_amt)
        {
            balance = 0;
            credit_amt = 0;
            try
            {
                IRequest req = new WebServiceRequest();
                ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
                write.Append("cust_id", cust_id);
                var json = req.request("/common?t=get_cus_balance", write.ToString());
                ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
                if (read.Read("errId") != "0")
                {
                    throw new Exception(read.Read("errMsg"));
                }
                balance = Conv.ToDecimal(read.Read("balance"));
                credit_amt = Conv.ToDecimal(read.Read("credit_amt"));
            }
            catch (Exception ex)
            {
                Log.writeLog("ClientBLL ->GetCusBalance()", ex.ToString(), null);
            }
        }

    }
}