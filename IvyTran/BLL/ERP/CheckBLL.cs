using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using DB;
 
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.BLL.ERP
{
    public class CheckBLL : ICheckBLL
    {
        void ICheckBLL.CheckSupSupItemPrice(string item_no,string sup_no,string oper_id,DateTime date)
        {
            DBByHandClose db = new DBByHandClose(AppSetting.conn);
            IDB d = db;
            string sql = "";
            try
            {
                db.Open();
                db.BeginTran();
                sql = "select is_enabled from bi_t_sup_item where is_supprice='1'" +
                      " and item_no='"+item_no+"' and sup_id='"+sup_no+"'";
                var dt = d.ExecuteToTable(sql, null);
                if (dt.Rows.Count <= 0)
                {
                    sql = "select sup_name from [dbo].[bi_t_supcust_info] where supcust_no='"+sup_no+"'";
                     dt= d.ExecuteToTable(sql, null);
                     if (dt.Rows.Count > 0)
                     {
                         string sup_name = dt.Rows[0][0].ToString();
                         sql = "select item_name from [dbo].[bi_t_item_info] where item_no='" + item_no + "'";
                         dt = d.ExecuteToTable(sql, null);
                         throw new Exception(sup_name+"的"+dt.Rows[0][0].ToString()+"商品不存在！");
                     }
                }

                if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "1")
                {
                    sql = "select sup_name from [dbo].[bi_t_supcust_info] where supcust_no='" + sup_no + "'";
                    dt = d.ExecuteToTable(sql, null);
                    if (dt.Rows.Count > 0)
                    {
                        string sup_name = dt.Rows[0][0].ToString();
                        sql = "select item_name from [dbo].[bi_t_item_info] where item_no='" + item_no + "'";
                        dt = d.ExecuteToTable(sql, null);
                        throw new Exception(sup_name + "的" + dt.Rows[0][0].ToString() + "商品不存在！");
                    }
                }

                sql = "update bi_t_sup_item set is_enabled='1'" +
                      " where item_no='" + item_no + "' and sup_id='" + sup_no + "'" +
                      " and is_supprice='1' and is_enabled='0'";
                d.ExecuteScalar(sql, null);
                //提交
                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                LogHelper.writeLog("CheckSupSupItemPrice->CheckSupSupItemPrice()", ex.ToString());
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        string MaxCode(DB.IDB db, string sheet_type)
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
                index += 1;
                if (index > 9999)
                {
                    index = 1;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + sheet_type + "'";
                db.ExecuteScalar(sql, null);
                return sheet_type + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }

        }

        System.Data.DataTable ICheckBLL.GetCheckSheetList(string date1, string date2)
        {
            string sql = "select a.*,b.branch_name,c.oper_name,e.oper_name as deal_man_name ";
            sql += "from ic_t_check_master a ";
            sql += "left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no ";
            sql += "left join sa_t_operator_i c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_people_info e on a.deal_man=e.oper_id ";
            sql += "where a.oper_date>='" + date1 + " 00:00:00.000' and a.oper_date<='" + date2 + " 23:59:59.999' ";
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        System.Data.DataTable ICheckBLL.GetCheckInitList(string branch_no)//------------------------------
        {
            string sql = "select a.sheet_no,a.branch_no,a.oper_id,a.begin_date,a.memo,a.approve_man,a.approve_date";
            sql += ",a.item_clsno,b.branch_name,c.oper_name,d.item_clsname,e.oper_name as approve_man_name ";
            sql += "from ic_t_check_init a ";
            sql += "left join (select * from bi_t_branch_info) b on a.branch_no=b.branch_no ";
            sql += "left join (select * from sa_t_operator_i) c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_item_cls d on d.item_clsno=isnull(a.item_clsno,'00') ";
            sql += "left join (select * from sa_t_operator_i) e on a.approve_man=e.oper_id ";
            sql += "where a.approve_flag='0' and a.check_status='0' and d.item_flag='0' and b.branch_no = '" + branch_no + "' ";
            sql += "order by a.sheet_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }


        System.Data.DataTable ICheckBLL.GetBranchStockList(string branch_no)
        {
            string sql = "select branch_no,item_no,stock_qty,cost_price,display_flag,last_price,fifo_price ";
            sql += "from ic_t_branch_stock where branch_no=@branch_no ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@branch_no", branch_no)
            };
            var tb = db.ExecuteToTable(sql, pars);

            return tb;
        }

        void ICheckBLL.GetCheckSheet(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,b.branch_name,c.oper_name,e.oper_name as deal_man_name ";
            sql += "from ic_t_check_master a ";
            sql += "left join (select * from bi_t_branch_info ) b on a.branch_no=b.branch_no ";
            sql += "left join sa_t_operator_i c on a.oper_id=c.oper_id ";
            sql += "left join bi_t_people_info e on a.deal_man=e.oper_id ";
            sql += "where a.sheet_no='" + sheet_no + "' ";
            tb1 = db.ExecuteToTable(sql, null);

            sql = "select a.*,b.item_subno,b.item_name,b.barcode,b.item_size,'0' item_pack,b.unit_no,isnull(in_price,0)*isnull(balance_qty,0) as real_amount ";
            sql += "from ic_t_check_detail a ";
            sql += "left join bi_t_item_info b on a.item_no=b.item_no ";
            sql += " where a.sheet_no='" + sheet_no + "' order by a.flow_id";
            tb2 = db.ExecuteToTable(sql, null);
        }

        void ICheckBLL.AddCheckSheet(Model.ic_t_check_master ord, List<Model.ic_t_check_detail> lines, out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                sheet_no = MaxCode(d, "PD");
                ord.sheet_no = sheet_no;
                ord.create_time = DateTime.Now;
                ord.update_time = ord.create_time;
                //
                d.Insert(ord);

                foreach (Model.ic_t_check_detail line in lines)
                {
                    var bak = d.ExecuteToModel<ic_t_check_bak>("select * from ic_t_check_bak where sheet_no='" + ord.check_no + "' and item_no='" + line.item_no + "' ", null);
                    if (bak != null)
                    {
                        line.stock_qty = bak.stock_qty;
                        line.in_price = bak.price;
                        line.sale_price = bak.sale_price;
                        line.balance_qty = line.real_qty - line.stock_qty;
                    }
                    string sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_check_detail ";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    line.sheet_no = sheet_no;
                    d.Insert(line);

                    var finfish = d.ExecuteToModel<ic_t_check_finish>("select * from ic_t_check_finish where sheet_no='" + ord.check_no + "' and item_no='" + line.item_no + "'", null);
                    if (finfish == null)
                    {
                        finfish = new ic_t_check_finish()
                        {
                            sheet_no = ord.check_no,
                            item_no = line.item_no,
                            branch_no = ord.branch_no,
                            change_flag = "1",
                            sale_price = line.sale_price,
                            stock_qty = line.stock_qty,
                            create_time = DateTime.Now,
                            in_price = line.in_price,
                            memo = line.memo,
                            real_qty = line.real_qty,
                            update_time = DateTime.Now,
                        };
                        d.Insert(finfish);
                    }
                    else
                    {
                        finfish.real_qty += line.real_qty;
                        finfish.update_time = DateTime.Now;
                        d.Update(finfish, "sheet_no,item_no");
                    }
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.AddCheckSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void ICheckBLL.ChangeCheckSheet(Model.ic_t_check_master ord, List<Model.ic_t_check_detail> lines)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from ic_t_check_master where sheet_no='" + ord.sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + ord.sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + ord.sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]) > ord.update_time)
                    {
                        throw new Exception("单据已被他人修改[" + ord.sheet_no + "]");
                    }
                }
                DataTable finfishtb = d.ExecuteToTable("select * from ic_t_check_detail where sheet_no='" + ord.sheet_no + "' ", null);

                sql = "delete from ic_t_check_detail where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from ic_t_check_master where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                ord.update_time = DateTime.Now;
                d.Insert(ord);
                foreach (Model.ic_t_check_detail line in lines)
                {
                    sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_check_detail ";
                    line.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    d.Insert(line);

                    var finfish = d.ExecuteToModel<ic_t_check_finish>("select * from ic_t_check_finish where sheet_no='" + ord.check_no + "' and item_no='" + line.item_no + "'", null);
                    if (finfish == null)
                    {
                        finfish = new ic_t_check_finish()
                        {
                            sheet_no = ord.check_no,
                            item_no = line.item_no,
                            branch_no = ord.branch_no,
                            change_flag = "1",
                            sale_price = line.sale_price,
                            stock_qty = line.stock_qty,
                            create_time = DateTime.Now,
                            in_price = line.in_price,
                            memo = line.memo,
                            real_qty = line.real_qty,
                            update_time = DateTime.Now,
                        };
                        d.Insert(finfish);
                    }
                    else
                    {
                        DataRow t = finfishtb.Select().Where(a => a["item_no"].ToString().Equals(finfish.item_no)).SingleOrDefault();
                        finfish.real_qty += line.real_qty;
                        if (t != null)
                            finfish.real_qty -= Conv.ToDecimal(t["real_qty"]);
                        finfish.update_time = DateTime.Now;
                        d.Update(finfish, "sheet_no,item_no");
                    }
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.ChangeCheckSheet()", ex.ToString(), ord.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        void ICheckBLL.DeleteCheckSheet(string sheet_no, DateTime update_time)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();

                db.BeginTran();
                //
                string sql = "select approve_flag,update_time from ic_t_check_master where sheet_no='" + sheet_no + "'";
                var tb = d.ExecuteToTable(sql, null);
                if (tb.Rows.Count == 0)
                {
                    throw new Exception("单据不存在[" + sheet_no + "]");
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核[" + sheet_no + "]");
                    }
                    if (Helper.Conv.ToDateTime(row["update_time"]).ToString("yyyy-MM-dd HH:mm:ss") != update_time.ToString("yyyy-MM-dd HH:mm:ss"))
                    {
                        throw new Exception("单据已被他人修改[" + sheet_no + "]");
                    }
                }
                sql = "delete from ic_t_check_detail where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from ic_t_check_master where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.DeleteCheckSheet()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void AddChectInitSheet(global::Model.ic_t_check_init check_init, out string sheet_no)
        {
            sheet_no = "";
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                string sql = $@"SELECT COUNT(1) 
FROM dbo.ic_t_check_init
WHERE branch_no='{check_init.branch_no}' AND check_status='0'";
                int count = d.ExecuteScalar(sql, null).ToInt32();
                if (count > 0)
                {
                    throw new Exception("该仓库存在未结束盘点单，不可创建新盘点单");
                }

                check_init.sheet_no = sheet_no = MaxCode(d, "PC");
                check_init.create_time = DateTime.Now;
                check_init.update_time = DateTime.Now;

                d.Insert(check_init);
                if (check_init.item_clsno != "" && check_init.item_clsno!="00")
                {
                    sql = $@"select *
from bi_t_item_info i
FULL JOIN(select* from ic_t_branch_stock where branch_no = '{check_init.branch_no}' and display_flag='1' )s on i.item_no = s.item_no
where i.item_clsno = '{check_init.item_clsno}'  or left(i.item_clsno,2) = '{check_init.item_clsno}'  and i.display_flag='1'    ";
                }
                else
                {
                    sql = $@"select *
from bi_t_item_info i
FULL JOIN(select* from ic_t_branch_stock where branch_no = '{check_init.branch_no}' and display_flag='1')s on i.item_no = s.item_no 
 where i.display_flag='1'";
                }
                //备份仓库
                DataTable tb = d.ExecuteToTable(sql, null);

                foreach (DataRow dr in tb.Rows)
                {
                    ic_t_check_bak bak = new ic_t_check_bak()
                    {
                        sheet_no = check_init.sheet_no,
                        branch_no = check_init.branch_no,
                        item_no = dr["item_no"].ToString(),
                        stock_qty = Conv.ToDecimal(dr["stock_qty"]),
                        cost_price = Conv.ToDecimal(dr["cost_price"]) <= 0 ? Conv.ToDecimal(dr["price"]) : Conv.ToDecimal(dr["sale_price"]),
                        price = Conv.ToDecimal(dr["last_price"]),
                        sale_price = Conv.ToDecimal(dr["sale_price"]),
                        create_time = DateTime.Now,
                        update_time = DateTime.Now
                    };
                    d.Insert(bak);
                }


                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.AddChectInitSheet()", ex.ToString(), check_init.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void UpdateChectInitSheet(global::Model.ic_t_check_init init)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select * from ic_t_check_init where sheet_no='" + init.sheet_no + "'";
                var item = d.ExecuteToModel<ic_t_check_init>(sql, null);
                if (item == null || string.IsNullOrEmpty(item.sheet_no))
                {
                    throw new Exception("单据不存在[" + init.sheet_no + "]");
                }
                else
                {
                    if (item.approve_flag.Equals("1"))
                    {
                        throw new Exception("单据已审核[" + init.sheet_no + "]");
                    }
                    if (item.update_time > init.update_time)
                    {
                        throw new Exception("单据已被他人修改[" + init.sheet_no + "]");
                    }
                }
                sql = "delete from ic_t_check_init where sheet_no='" + init.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                init.update_time = DateTime.Now;
                d.Insert(init);

                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.UpdateChectInitSheet()", ex.ToString(), init.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void DeleteChectInitSheet(global::Model.ic_t_check_init init)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select * from ic_t_check_init where sheet_no='" + init.sheet_no + "'";
                var item = d.ExecuteToModel<ic_t_check_init>(sql, null);
                if (item == null || string.IsNullOrEmpty(item.sheet_no))
                {
                    throw new Exception("单据不存在[" + init.sheet_no + "]");
                }
                else
                {
                    if (item.approve_flag.Equals("1"))
                    {
                        throw new Exception("单据已审核[" + init.sheet_no + "]");
                    }
                    if (item.update_time > init.update_time)
                    {
                        throw new Exception("单据已被他人修改[" + init.sheet_no + "]");
                    }
                }
                sql = "delete from ic_t_check_init where sheet_no='" + init.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from ic_t_check_bak where sheet_no='" + init.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.DeleteChectInitSheet()", ex.ToString(), init.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public System.Data.DataTable GetCheckInitSheet(ic_t_check_init init)
        {
            string sql = @"select * ,
(i.oper_id+'/'+s.oper_name) 操作员,
(i.branch_no+'/'+b.branch_name) 仓库,
(i.item_clsno+'/'+c.item_clsname) 商品类别,
(i.approve_man+'/'+o1.oper_name) 审核人
from ic_t_check_init i
left join bi_t_branch_info b on b.branch_no=i.branch_no
left join bi_t_item_cls c on c.item_clsno=i.item_clsno
left join sa_t_operator_i s on s.oper_id=i.oper_id
left join sa_t_operator_i o1 on o1.oper_id=i.approve_man
where 1=1 ";

            if (!string.IsNullOrEmpty(init.sheet_no))
                sql += " and i.sheet_no='" + init.sheet_no + "' ";
            if (!string.IsNullOrEmpty(init.branch_no))
                sql += " and i.branch_no='" + init.branch_no + "' ";
            if (!string.IsNullOrEmpty(init.check_status))
                sql += " and i.check_status='" + init.check_status + "' ";
            if (init.create_time > DateTime.MinValue && init.create_time < DateTime.MaxValue)
                sql += " and Convert(varchar(10),  i.create_time ,20 )   >= '" + init.create_time.ToString("yyyy-MM-dd") + "' ";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DataTable tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        public DataTable GetCheckBak(ic_t_check_bak bak)
        {
            string sql = @"select b.* 
from ic_t_check_bak b
left join bi_t_item_info i on i.item_no=b.item_no
where 1=1 ";

            if (!string.IsNullOrEmpty(bak.sheet_no))
                sql += " and b.sheet_no='" + bak.sheet_no + "' ";
            if (!string.IsNullOrEmpty(bak.item_no))
                sql += " and( i.item_no='" + bak.item_no + "' or i.item_subno='" + bak.item_no + "' or i.barcode='" + bak.item_no + "' )";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DataTable tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        public DataTable GetCheckItemBak(ic_t_check_bak bak)
        {
            string sql = @"select
i.*,
isnull(b.stock_qty,0) stock_qty,
isnull(b.cost_price,0) cost_price
from bi_t_item_info i
left join ic_t_check_bak b on b.item_no=i.item_no and b.branch_no='" + bak.branch_no + "' and b.sheet_no='" + bak.sheet_no + "' where 1=1 ";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            var init = db.ExecuteToModel<ic_t_check_init>("select * from ic_t_check_init where sheet_no='" + bak.sheet_no + "'", null);
            if (init == null)
                throw new Exception("批次单不存在");
            if (!string.IsNullOrEmpty(init.item_clsno))
                sql += " and i.item_clsno like '" + init.item_clsno + "%' ";
            if (!string.IsNullOrEmpty(bak.item_no))
                sql += " and( i.item_no='" + bak.item_no + "' or i.item_subno='" + bak.item_no + "' or i.barcode='" + bak.item_no + "' )";

            DataTable tb = db.ExecuteToTable(sql, null);

            return tb;
        }
        public DataTable GetCheckFinish(ic_t_check_finish finish)
        {
            string sql = @"select  * 
from ic_t_check_finish  f 
left join bi_t_item_info item on f.item_no=item.item_no 
left join bi_t_item_cls c on c.item_clsno=item.item_clsno ";

            if (!string.IsNullOrEmpty(finish.sheet_no))
                sql += "where f.sheet_no='" + finish.sheet_no + "' ";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            DataTable tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        //public void CheckPDSheet(string sheet_no)
        //{
        //    var db = new DB.DBByHandClose(AppSetting.conn);
        //    DB.IDB d = db;
        //    try
        //    {
        //        db.Open();
        //        db.BeginTran();

        //        ic_t_check_master master = d.ExecuteToModel<ic_t_check_master>("select * from ic_t_check_master where sheet_no='" + sheet_no + "'", null);
        //        if (master == null || string.IsNullOrEmpty(master.sheet_no)) throw new Exception("单据:[" + sheet_no + "]不存在");
        //        if (!master.approve_flag.Equals("0")) throw new Exception("单据:[" + sheet_no + "]已审核");

        //        master.approve_flag = "1";
        //        d.Update(master, "sheet_no", "approve_flag");
        //        //
        //        db.CommitTran();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.writeLog("CheckBLL.CheckPDSheet()", ex.ToString(), sheet_no);
        //        db.RollBackTran();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        db.Close();
        //    }
        //}
        public void CheckPDSheet(string sheet_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                ic_t_check_master master = d.ExecuteToModel<ic_t_check_master>("select * from ic_t_check_master where sheet_no='" + sheet_no + "'", null);
                if (master == null || string.IsNullOrEmpty(master.sheet_no)) throw new Exception("单据:[" + sheet_no + "]不存在");
                if (!master.approve_flag.Equals("0")) throw new Exception("单据:[" + sheet_no + "]已审核");

                master.approve_flag = "1";

                d.Update(master, "sheet_no", "approve_flag");
                if (master.meno == "未盘商品单据")
                {
                    string sql = "select a.*, b.item_subno, b.item_name, b.barcode, b.item_size, b.item_pack, b.unit_no, isnull(in_price, 0) * isnull(real_qty, 0) as real_amount " +
           "from ic_t_check_detail a " +
               "left join bi_t_item_info b on a.item_no=b.item_no " +
                  " where a.sheet_no='" + sheet_no + "' order by a.flow_id";
                    DataTable tb = d.ExecuteToTable(sql, null);
                    foreach (DataRow dr in tb.Rows)
                    {

                        //修改库存
                        //写入库存流水、结存
                        sql = "select * from ic_t_branch_stock where branch_no='" + master.branch_no + "'" +
                         " and item_no='" + dr["item_no"] + "' ";
                        Model.ic_t_branch_stock stock = d.ExecuteToModel<Model.ic_t_branch_stock>(sql, null);
                        sql = "select * from bi_t_item_info where item_no='" + dr["item_no"] + "'";
                        Model.bi_t_item_info it = d.ExecuteToModel<Model.bi_t_item_info>(sql, null);


                        //流水
                        sql = "select isnull(max(flow_id)+1,1) from ic_t_flow_dt";
                        Model.ic_t_flow_dt flow = new Model.ic_t_flow_dt();
                        flow.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                        flow.branch_no = master.branch_no;
                        flow.item_no = dr["item_no"].ToString();

                        flow.oper_date = System.DateTime.Now;
                        if (stock == null)
                        {
                            flow.init_qty = 0;
                            flow.init_amt = 0;
                        }
                        else
                        {


                            flow.init_qty = stock.stock_qty;
                            flow.init_amt = stock.stock_qty * stock.cost_price;
                        }
                        flow.new_qty = Conv.ToDecimal(dr["real_qty"]) - Conv.ToDecimal(dr["stock_qty"]);
                        flow.new_amt = flow.new_qty * Conv.ToDecimal(dr["in_price"]);
                        flow.settle_qty = flow.init_qty + flow.new_qty;
                        flow.settle_amt = flow.init_amt + flow.new_qty * Conv.ToDecimal(dr["sale_price"]);
                        flow.cost_price = Conv.ToDecimal(dr["sale_price"]);
                        flow.db_type = Conv.ToDecimal(dr["real_qty"]) > Conv.ToDecimal(dr["stock_qty"]) ? "+" : "-";
                        flow.sheet_no = dr["sheet_no"].ToString();
                        flow.sheet_type = "PC";
                        flow.voucher_no = dr["sheet_no"].ToString();
                        flow.supcust_no = "";
                        flow.supcust_flag = "";
                        flow.oper_day = System.DateTime.Now.ToString("yyyy/MM/dd");
                        flow.adjust_amt = flow.settle_amt;
                        flow.cost_type = it.cost_type;
                        flow.sale_price = Conv.ToDecimal(dr["sale_price"]);
                        d.Insert(flow);
                        //
                        if (flow.new_qty != 0)
                        {
                            Model.ic_t_cost_adjust ad = new Model.ic_t_cost_adjust();
                            sql = "select isnull(max(flow_id)+1,1) from ic_t_cost_adjust";
                            ad.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                            ad.branch_no = master.branch_no;
                            ad.item_no = dr["item_no"].ToString();
                            ad.oper_date = System.DateTime.Now;
                            if (stock == null)
                            {
                                ad.old_price = 0;
                                ad.old_qty = 0;
                            }
                            else
                            {

                                ad.old_price = stock.cost_price;
                                ad.old_qty = stock.stock_qty;
                            }

                            ad.new_price = flow.cost_price;
                            ad.in_qty = flow.new_qty;
                            ad.sheet_no = dr["sheet_no"].ToString();
                            ad.memo = "";
                            ad.type_no = "1";
                            ad.adjust_amt = flow.new_qty;
                            ad.sup_no = "";
                            ad.max_flow_id = flow.flow_id;
                            ad.cost_type = it.cost_type;

                            d.Insert(ad);
                        }
                        //
                        if (stock == null)
                        {//仓库没有就插入

                            var branch_stock_model = DB.ReflectionHelper.DataRowToModel<Model.ic_t_branch_stock>(dr);
                            branch_stock_model.branch_no = master.branch_no;
                            branch_stock_model.stock_qty = Conv.ToDecimal(dr["real_qty"]);
                            branch_stock_model.display_flag = "1";
                            branch_stock_model.cost_price = flow.cost_price;
                            branch_stock_model.last_price = Conv.ToDecimal(dr["in_price"]);
                            d.Insert(branch_stock_model);

                        }

                        else
                        {
                            //有就更细

                            if (it == null)
                            {
                                throw new Exception("不存在商品内码" + dr["item_no"]);
                            }

                            stock.stock_qty = Conv.ToDecimal(dr["real_qty"]);
                            stock.cost_price = flow.cost_price;
                            stock.last_price = Conv.ToDecimal(dr["in_price"]);
                            d.Update(stock, "branch_no,item_no", "stock_qty,cost_price,last_price");
                        }


                    }
                }

                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.CheckPDSheet()", ex.ToString(), sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        //public void CheckPCSheet(ic_t_check_init ini)
        //{
        //    var db = new DB.DBByHandClose(AppSetting.conn);
        //    DB.IDB d = db;
        //    try
        //    {
        //        db.Open();
        //        db.BeginTran();

        //        //
        //        ic_t_check_init master = d.ExecuteToModel<ic_t_check_init>("select * from ic_t_check_init where sheet_no='" + ini.sheet_no + "'", null);
        //        if (master == null || string.IsNullOrEmpty(master.sheet_no)) throw new Exception("单据:[" + ini.sheet_no + "]不存在");
        //        if (!master.approve_flag.Equals("0")) throw new Exception("单据:[" + ini.sheet_no + "]已审核");

        //        master.sheet_no = ini.sheet_no;
        //        master.approve_flag = "1";
        //        master.approve_date = ini.approve_date;
        //        master.approve_man = ini.approve_man;
        //        master.update_time = DateTime.Now;
        //        master.check_status = "2";
        //        master.memo = ini.memo;
        //        master.end_date = DateTime.Now;

        //        d.Update(master, "sheet_no");
        //        //
        //        DataTable tb = d.ExecuteToTable("select * from ic_t_check_finish where sheet_no='" + ini.sheet_no + "'", null);
        //        foreach (DataRow dr in tb.Rows)
        //        {
        //            if ("1".Equals(dr["change_flag"].ToString()))
        //            {
        //                //修改库存
        //                //写入库存流水、结存
        //                string sql = "select * from ic_t_branch_stock where branch_no='" + master.branch_no + "'" +
        //                  " and item_no='" + dr["item_no"] + "' ";
        //                Model.ic_t_branch_stock stock = d.ExecuteToModel<Model.ic_t_branch_stock>(sql, null);
        //                if (stock == null) continue;
        //                //
        //                sql = "select * from bi_t_item_info where item_no='" + dr["item_no"] + "'";
        //                bi_t_item_info it = d.ExecuteToModel<bi_t_item_info>(sql, null);
        //                if (it == null)
        //                {
        //                    throw new Exception("不存在商品内码" + dr["item_no"]);
        //                }
        //                //
        //                sql = "select isnull(max(flow_id)+1,1) from ic_t_flow_dt";
        //                body.ic_t_flow_dt flow = new body.ic_t_flow_dt();
        //                flow.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
        //                flow.branch_no = master.branch_no;
        //                flow.item_no = dr["item_no"].ToString();

        //                flow.oper_date = System.DateTime.Now;
        //                flow.init_qty = stock.stock_qty;
        //                flow.init_amt = stock.stock_qty * stock.cost_price;
        //                flow.new_qty = Conv.ToDecimal(dr["real_qty"]) - Conv.ToDecimal(dr["stock_qty"]);
        //                flow.new_amt = flow.new_qty * Conv.ToDecimal(dr["in_price"]);
        //                flow.settle_qty = flow.init_qty + flow.new_qty;
        //                flow.settle_amt = flow.init_amt + flow.new_qty * Conv.ToDecimal(dr["sale_price"]);
        //                flow.cost_price = Conv.ToDecimal(dr["sale_price"]);
        //                flow.db_type = Conv.ToDecimal(dr["real_qty"]) > Conv.ToDecimal(dr["stock_qty"]) ? "+" : "-";
        //                flow.sheet_no = dr["sheet_no"].ToString();
        //                flow.sheet_type = "PC";
        //                flow.voucher_no = dr["sheet_no"].ToString();
        //                flow.supcust_no = "";
        //                flow.supcust_flag = "";
        //                flow.oper_day = System.DateTime.Now.ToString("yyyy/MM/dd");
        //                flow.adjust_amt = flow.settle_amt;
        //                flow.cost_type = it.cost_type;
        //                flow.sale_price = Conv.ToDecimal(dr["sale_price"]);
        //                d.Insert(flow);
        //                //
        //                if (flow.new_qty != 0)
        //                {
        //                    Model.ic_t_cost_adjust ad = new Model.ic_t_cost_adjust();
        //                    sql = "select isnull(max(flow_id)+1,1) from ic_t_cost_adjust";
        //                    ad.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
        //                    ad.branch_no = dr["branch_no"].ToString();
        //                    ad.item_no = dr["item_no"].ToString();
        //                    ad.oper_date = System.DateTime.Now;
        //                    ad.old_price = stock.cost_price;
        //                    ad.new_price = flow.cost_price;
        //                    ad.in_qty = flow.new_qty;
        //                    ad.sheet_no = dr["sheet_no"].ToString();
        //                    ad.memo = "";
        //                    ad.type_no = "1";
        //                    ad.adjust_amt = flow.new_qty;
        //                    ad.sup_no = "";
        //                    ad.max_flow_id = flow.flow_id;
        //                    ad.cost_type = it.cost_type;
        //                    ad.old_qty = stock.stock_qty;
        //                    d.Insert(ad);
        //                }
        //                //
        //                stock.stock_qty = flow.settle_qty;
        //                stock.cost_price = flow.cost_price;
        //                stock.last_price = Conv.ToDecimal(dr["in_price"]);
        //                d.Update(stock, "branch_no,item_no", "stock_qty,cost_price,last_price");
        //            }
        //        }

        //        //
        //        db.CommitTran();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.writeLog("CheckBLL.CheckPCSheet()", ex.ToString(), ini.sheet_no);
        //        db.RollBackTran();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        db.Close();
        //    }
        //}
        public void CheckPCSheet(ic_t_check_init ini)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();

                //
                ic_t_check_init master = d.ExecuteToModel<ic_t_check_init>("select * from ic_t_check_init where sheet_no='" + ini.sheet_no + "'", null);
                if (master == null || string.IsNullOrEmpty(master.sheet_no)) throw new Exception("单据:[" + ini.sheet_no + "]不存在");
                if (!master.approve_flag.Equals("0")) throw new Exception("单据:[" + ini.sheet_no + "]已审核");

                master.sheet_no = ini.sheet_no;
                master.approve_flag = "1";
                master.approve_date = ini.approve_date;
                master.approve_man = ini.approve_man;
                master.update_time = DateTime.Now;
                master.check_status = "2";
                master.memo = ini.memo;
                master.end_date = DateTime.Now;

                d.Update(master, "sheet_no");
                //
                DataTable tb = d.ExecuteToTable("select * from ic_t_check_finish where sheet_no='" + ini.sheet_no + "'", null);
                foreach (DataRow dr in tb.Rows)
                {
                    if ("1".Equals(dr["change_flag"].ToString()))
                    {
                        //修改库存
                        //写入库存流水、结存
                        string sql = "select * from ic_t_branch_stock where branch_no='" + master.branch_no + "'" +
                          " and item_no='" + dr["item_no"] + "' ";
                        Model.ic_t_branch_stock stock = d.ExecuteToModel<Model.ic_t_branch_stock>(sql, null);
                        sql = "select * from bi_t_item_info where item_no='" + dr["item_no"] + "'";
                        Model.bi_t_item_info it = d.ExecuteToModel<Model.bi_t_item_info>(sql, null);


                        //流水
                        sql = "select isnull(max(flow_id)+1,1) from ic_t_flow_dt";
                        Model.ic_t_flow_dt flow = new Model.ic_t_flow_dt();
                        flow.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                        flow.branch_no = master.branch_no;
                        flow.item_no = dr["item_no"].ToString();

                        flow.oper_date = System.DateTime.Now;
                        if (stock == null)
                        {
                            flow.init_qty = 0;
                            flow.init_amt = 0;
                        }
                        else
                        {


                            flow.init_qty = stock.stock_qty;
                            flow.init_amt = stock.stock_qty * stock.cost_price;
                        }
                        flow.new_qty = Conv.ToDecimal(dr["real_qty"]) - Conv.ToDecimal(dr["stock_qty"]);
                        flow.new_amt = flow.new_qty * Conv.ToDecimal(dr["in_price"]);
                        flow.settle_qty = flow.init_qty + flow.new_qty;
                        flow.settle_amt = flow.init_amt + flow.new_qty * Conv.ToDecimal(dr["sale_price"]);
                        flow.cost_price = Conv.ToDecimal(dr["sale_price"]);
                        flow.db_type = Conv.ToDecimal(dr["real_qty"]) > Conv.ToDecimal(dr["stock_qty"]) ? "+" : "-";
                        flow.sheet_no = dr["sheet_no"].ToString();
                        flow.sheet_type = "PC";
                        flow.voucher_no = dr["sheet_no"].ToString();
                        flow.supcust_no = "";
                        flow.supcust_flag = "";
                        flow.oper_day = System.DateTime.Now.ToString("yyyy/MM/dd");
                        flow.adjust_amt = flow.settle_amt;
                        flow.cost_type = it.cost_type;
                        flow.sale_price = Conv.ToDecimal(dr["sale_price"]);
                        d.Insert(flow);
                        //
                        if (flow.new_qty != 0)
                        {
                            Model.ic_t_cost_adjust ad = new Model.ic_t_cost_adjust();
                            sql = "select isnull(max(flow_id)+1,1) from ic_t_cost_adjust";
                            ad.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                            ad.branch_no = dr["branch_no"].ToString();
                            ad.item_no = dr["item_no"].ToString();
                            ad.oper_date = System.DateTime.Now;
                            if (stock == null)
                            {
                                ad.old_price = 0;
                                ad.old_qty = 0;
                            }
                            else
                            {

                                ad.old_price = stock.cost_price;
                                ad.old_qty = stock.stock_qty;
                            }

                            ad.new_price = flow.cost_price;
                            ad.in_qty = flow.new_qty;
                            ad.sheet_no = dr["sheet_no"].ToString();
                            ad.memo = "";
                            ad.type_no = "1";
                            ad.adjust_amt = flow.new_qty;
                            ad.sup_no = "";
                            ad.max_flow_id = flow.flow_id;
                            ad.cost_type = it.cost_type;

                            d.Insert(ad);
                        }
                        //
                        if (stock == null)
                        {//仓库没有就插入

                            var branch_stock_model = DB.ReflectionHelper.DataRowToModel<Model.ic_t_branch_stock>(dr);
                            branch_stock_model.branch_no = master.branch_no;
                            branch_stock_model.stock_qty = Conv.ToDecimal(dr["real_qty"]);
                            branch_stock_model.display_flag = "1";
                            branch_stock_model.cost_price = flow.cost_price;
                            branch_stock_model.last_price = Conv.ToDecimal(dr["in_price"]);
                            d.Insert(branch_stock_model);

                        }

                        else
                        {
                            //有就更细

                            if (it == null)
                            {
                                throw new Exception("不存在商品内码" + dr["item_no"]);
                            }

                            stock.stock_qty = Conv.ToDecimal(dr["real_qty"]);
                            stock.cost_price = flow.cost_price;
                            stock.last_price = Conv.ToDecimal(dr["in_price"]);
                            d.Update(stock, "branch_no,item_no", "stock_qty,cost_price,last_price");
                        }

                    }
                }

                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.CheckPCSheet()", ex.ToString(), ini.sheet_no);
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
        public void UpdateCheckFinish(List<ic_t_check_finish> finishs)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                foreach (ic_t_check_finish f in finishs)
                {
                    ic_t_check_finish item = d.ExecuteToModel<ic_t_check_finish>("select * from ic_t_check_finish where sheet_no='" + f.sheet_no + "' and item_no='" + f.item_no + "'", null);

                    if (f.update_time <= item.update_time)
                        throw new Exception("库存已被修改");

                    item.change_flag = item.change_flag;
                    item.update_time = f.update_time;
                    item.memo = f.memo;

                    d.Update(item, "sheet_no,item_no", "change_flag,update_time,memo");
                }
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.CheckPDSheet()", ex.ToString());
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        public void CreateUnCheckSheet(string check_no)
        {
            var db = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                string sql = $@"SELECT meno FROM dbo.ic_t_check_master WHERE check_no ='{check_no}' AND meno = '未盘商品单据'";
                DataTable dataTable = d.ExecuteToTable(sql, null);
                if (dataTable.Rows.Count > 0)
                {
                    return;
                }
                sql = $@"SELECT * FROM dbo.ic_t_check_bak WHERE sheet_no = '{check_no}' 
AND item_no NOT IN (SELECT item_no 
FROM dbo.ic_t_check_detail 
WHERE sheet_no IN (SELECT sheet_no FROM dbo.ic_t_check_master WHERE check_no = '{check_no}'))";
                //未盘商品
                List<ic_t_check_bak> lines = d.ExecuteToList<ic_t_check_bak>(sql, null);
                string sheet_no = MaxCode(d, "PD");
                sql = $@"SELECT * FROM dbo.ic_t_check_master WHERE check_no = '{check_no}'";
                DataTable lastDt = d.ExecuteToTable(sql, null);
                ic_t_check_master ord = new ic_t_check_master();//盘点单头
                ord.sheet_no = sheet_no;
                ord.branch_no = lastDt.Rows[0]["branch_no"].ToString();
                ord.oper_id = lastDt.Rows[0]["oper_id"].ToString();
                ord.oper_date = DateTime.Now;
                ord.approve_flag = "0";
                ord.check_no = lastDt.Rows[0]["check_no"].ToString();
                ord.meno = "未盘商品单据";
                ord.create_time = DateTime.Now;
                ord.update_time = ord.create_time;
                d.Insert(ord);
                foreach (ic_t_check_bak line in lines)
                {
                    ic_t_check_detail cd = new ic_t_check_detail();
                    if (line != null)
                    {
                        cd.item_no = line.item_no;

                        cd.stock_qty = line.stock_qty;
                        cd.in_price = line.price;
                        cd.sale_price = line.sale_price;
                        cd.real_qty = 0;
                        cd.balance_qty = cd.real_qty - line.stock_qty;
                        cd.packqty = 0;
                    }
                    sql = "select isnull(max(flow_id),0) + 1 as flow_id from ic_t_check_detail ";
                    cd.flow_id = Helper.Conv.ToInt64(d.ExecuteScalar(sql, null));
                    cd.sheet_no = sheet_no;
                    d.Insert(cd);
                }

                db.CommitTran();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("CheckBLL.CreateUnCheckSheet()", ex.ToString());
                db.RollBackTran();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
