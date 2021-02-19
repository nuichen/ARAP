using System;
using System.Collections.Generic;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class CusPriceOrder : ICusPriceOrder
    {

        System.Data.DataTable ICusPriceOrder.GetList(DateTime date1, DateTime date2)
        {
            string sql = @"select  a.*,branch.branch_name,(a.branch_no +'/'+ branch.branch_name) as branch_no_a,
oper.oper_name, (a.oper_id +'/'+ oper.oper_name) as oper_id_a,
people.oper_name as deal_man_name,(a.deal_man +'/'+people.oper_name) as deal_man_a,
man.oper_name as approve_man_name,(a.approve_man+'/'+man.oper_name) as approve_man_a
 from pm_t_flow_main a
left join (select * from bi_t_branch_info ) branch on a.branch_no=branch.branch_no
left join (select * from sa_t_operator_i) oper on a.oper_id=oper.oper_id
left join bi_t_people_info people on a.deal_man=people.oper_id
left join (select * from sa_t_operator_i) man on a.approve_man=man.oper_id
where a.price_type='1'" +
                " and a.oper_date>='" + date1.ToString("yyyy-MM-dd") + " 00:00:00.000'" +
                " and a.oper_date<='" + date2.ToString("yyyy-MM-dd") + " 23:59:59.999'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);

            return tb;
        }

        void ICusPriceOrder.GetOrder(string sheet_no, out System.Data.DataTable tb1, out System.Data.DataTable tb2)
        {
            string sql = @"select  a.*,branch.branch_name,a.branch_no +' '+ branch.branch_name as branch_no_a,
oper.oper_name, a.oper_id +' '+ oper.oper_name as oper_id_a,
people.oper_name as deal_man_name,a.deal_man +' '+people.oper_name as deal_man_a,
man.oper_name as approve_man_name,a.approve_man+' '+man.oper_name as approve_man_a
 from pm_t_flow_main a
left join (select * from bi_t_branch_info ) branch on a.branch_no=branch.branch_no
left join (select * from sa_t_operator_i) oper on a.oper_id=oper.oper_id
left join bi_t_people_info people on a.deal_man=people.oper_id
left join (select * from sa_t_operator_i) man on a.approve_man=man.oper_id
where a.sheet_no='" + sheet_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            tb1 = db.ExecuteToTable(sql, null);
            sql = "select a.*,b.item_subno,b.item_name,b.barcode,b.item_size,b.unit_no,b.price from pm_t_price_flow_detial a " +
                " left join bi_t_item_info b on a.item_no=b.item_no" +
                " where a.sheet_no='" + sheet_no + "' order by a.flow_id";
            tb2 = db.ExecuteToTable(sql, null);
        }

        string ICusPriceOrder.MaxCode()
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string front_str = "TP";
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
                    index = 0;
                }
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='" + front_str + "'";
                db.ExecuteScalar(sql, null);
                return front_str + "00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }

        }

        void ICusPriceOrder.Add(Model.pm_t_flow_main ord, List<Model.pm_t_price_flow_detial> lines, out string sheet_no)
        {
            ICusPriceOrder ins = this;
            ord.sheet_no = ins.MaxCode();
            //
            string sql = "select * from pm_t_flow_main where sheet_no='" + ord.sheet_no + "'";
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
                    throw new Exception("已存在单号" + ord.sheet_no);
                }
                d.Insert(ord);

                sql = "SELECT ISNULL(MAX(flow_id),0) FROM dbo.pm_t_price_flow_detial";
                foreach (Model.pm_t_price_flow_detial line in lines)
                {
                    int flow_id = d.ExecuteScalar(sql, null).ToInt32();
                    line.flow_id = flow_id + 1;
                    line.sheet_no = ord.sheet_no;
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
            //
            sheet_no = ord.sheet_no;
        }

        void ICusPriceOrder.Change(Model.pm_t_flow_main ord, List<Model.pm_t_price_flow_detial> lines)
        {
            string sql = "select * from pm_t_flow_main where sheet_no='" + ord.sheet_no + "'";
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
                    throw new Exception("单据不存在" + ord.sheet_no);
                }
                else
                {
                    var row = tb.Rows[0];
                    if (row["approve_flag"].ToString() == "1")
                    {
                        throw new Exception("单据已审核" + ord.sheet_no);
                    }
                }
                sql = "delete from pm_t_price_flow_detial where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from pm_t_flow_main where sheet_no='" + ord.sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                d.Insert(ord);
                foreach (Model.pm_t_price_flow_detial line in lines)
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

        void ICusPriceOrder.Delete(string sheet_no)
        {
            string sql = "select * from pm_t_flow_main where sheet_no='" + sheet_no + "'";
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
                sql = "delete from pm_t_price_flow_detial where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                sql = "delete from pm_t_flow_main where sheet_no='" + sheet_no + "'";
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

        void ICusPriceOrder.Check(string sheet_no, string approve_man)
        {
            string sql = "select * from pm_t_flow_main where sheet_no='" + sheet_no + "'";
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
                sql = "update pm_t_flow_main set approve_flag='1',approve_man='" + approve_man +
                    "',approve_date='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' where sheet_no='" + sheet_no + "'";
                d.ExecuteScalar(sql, null);
                //
                sql = "select * from pm_t_price_flow_detial where sheet_no='" + sheet_no + "'";
                tb = d.ExecuteToTable(sql, null);
                foreach (System.Data.DataRow row in tb.Rows)
                {
                    string item_no = row["item_no"].ToString();
                    decimal new_price = Helper.Conv.ToDecimal(row["new_price"].ToString());
                    decimal new_price2 = Helper.Conv.ToDecimal(row["new_price2"].ToString());
                    decimal new_price3 = Helper.Conv.ToDecimal(row["new_price3"].ToString());
                    sql = "update bi_t_item_info set base_price=" + new_price.ToString() +
                        ",base_price2=" + new_price2.ToString() +
                        ",base_price3=" + new_price3.ToString() +
                        " where item_no='" + item_no + "'";
                    d.ExecuteScalar(sql, null);
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

    }
}
