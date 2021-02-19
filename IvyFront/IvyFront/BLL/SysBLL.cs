using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyFront.IBLL;

namespace IvyFront.BLL
{
    class SysBLL:ISysBLL
    {
        List<Model.bt_par_setting> ISysBLL.GetParSettingList()
        {
            string sql = "select * from bt_par_setting ";

            var tb = Program.db.ExecuteToTable(sql, null);
            List<Model.bt_par_setting> lst = new List<Model.bt_par_setting>();
            foreach (System.Data.DataRow row in tb.Rows)
            {
                var item = new Model.bt_par_setting();
                item.par_id = row["par_id"].ToString();
                item.par_val = row["par_val"].ToString();
                lst.Add(item);
            }
            return lst;
        }

        void ISysBLL.UpdateParSetting(List<Model.bt_par_setting> lst)
        {
            foreach (Model.bt_par_setting item in lst)
            {
                string sql = "select * from bt_par_setting where par_id='" + item.par_id + "'";

                var tb = Program.db.ExecuteToTable(sql, null);

                if (tb.Rows.Count > 0)
                {
                    sql = "update bt_par_setting set par_val='" + item.par_val + "' where par_id='" + item.par_id + "'";
                    Program.db.ExecuteScalar(sql, null);
                }
                else
                {
                    sql = "insert into bt_par_setting(par_id,par_val) values('" + item.par_id + "','" + item.par_val + "') ";
                    Program.db.ExecuteScalar(sql, null);
                }
            }
        }

        void ISysBLL.DeleteOldData()
        {
            string conditiion_sql = " where ifnull(is_upload,'0')='1' and oper_date<'" + System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            //删除采购入库明细
            string sql = "delete from ic_t_inout_store_detail where sheet_no in(select sheet_no from ic_t_inout_store_master " + conditiion_sql + ")";
            Program.db.ExecuteScalar(sql, null);
            //删除采购入库表头
            sql = "delete from ic_t_inout_store_master " + conditiion_sql;
            Program.db.ExecuteScalar(sql, null);

            //
            conditiion_sql = " where ifnull(is_upload,'0')='1' and oper_date<'" + System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            //删除销售出库明细
            sql = "delete from sm_t_salesheet_detail where sheet_no in(select sheet_no from sm_t_salesheet " + conditiion_sql + ")";
            Program.db.ExecuteScalar(sql, null);
            //删除支付流水
            sql = "delete from ot_pay_flow where sheet_no in(select sheet_no from sm_t_salesheet " + conditiion_sql + ")";
            Program.db.ExecuteScalar(sql, null);
            //删除销售出库表头
            sql = "delete from sm_t_salesheet " + conditiion_sql;
            Program.db.ExecuteScalar(sql, null);
            
            //
            conditiion_sql = " where oper_date<'" + System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            //删除称重明细
            sql = "delete from t_order_detail " + conditiion_sql;
            Program.db.ExecuteScalar(sql, null);

            //删除打印日志
            conditiion_sql = " where oper_date<'" + System.DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            sql = "delete from t_print_log " + conditiion_sql;
            Program.db.ExecuteScalar(sql, null);

            //删除操作日志
            conditiion_sql = " where oper_date<'" + System.DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            sql = "delete from t_click_log " + conditiion_sql;
            Program.db.ExecuteScalar(sql, null);
        }

        void ISysBLL.WritePrintLog(string sheet_no)
        {
            string sql = "select 1 from t_print_log where sheet_no='" + sheet_no + "'";

            var tb = Program.db.ExecuteToTable(sql, null);

            if (tb.Rows.Count > 0)
            {
                sql = "update t_print_log set print_count=print_count+1 where sheet_no='" + sheet_no + "'";
                Program.db.ExecuteScalar(sql, null);
            }
            else
            {
                sql = "insert into t_print_log(sheet_no,print_count,oper_id,oper_date) values('" + sheet_no + "',1,'" + Program.oper_id + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "') ";
                Program.db.ExecuteScalar(sql, null);
            }
        }

        void ISysBLL.WriteClickLog(string click_num)
        {
            string sql = "select ifnull(max(flow_id),'0') as flow_id from t_click_log ";
            var dt = Program.db.ExecuteToTable(sql, null);
            var flow_id = "1";
            if (dt.Rows.Count > 0)
            {
                flow_id = (Conv.ToInt(dt.Rows[0]["flow_id"]) + 1).ToString();
            }
            sql = "insert into t_click_log(flow_id,jh,click_num,oper_id,oper_date) values('" + flow_id + "','" + Program.cus_id + "','" + Program.jh + "','" + click_num + "','" + Program.oper_id + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "') ";
            Program.db.ExecuteScalar(sql, null);
        }


        void ISysBLL.clear_db()
        {
            Program.db.ExecuteScalar("insert into t_clear_db_log values('" + Program.oper_id + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", null);

            Program.db.ExecuteScalar("delete from bt_sysn_info ", null);
            Program.db.ExecuteScalar("delete from sm_t_salesheet ", null);
            Program.db.ExecuteScalar("delete from sm_t_salesheet_detail ", null);
            Program.db.ExecuteScalar("delete from t_order_detail ", null);

            Program.db.ExecuteScalar("delete from ic_t_inout_store_detail ", null);
            Program.db.ExecuteScalar("delete from ic_t_inout_store_master ", null);
            Program.db.ExecuteScalar("delete from ot_pay_flow ", null);

            Program.db.ExecuteScalar("delete from t_print_log ", null);
            Program.db.ExecuteScalar("delete from sa_t_operator_i ", null);

            Program.db.ExecuteScalar("delete from bi_t_cust_price ", null);
            Program.db.ExecuteScalar("delete from bi_t_sup_item ", null);

            Program.db.ExecuteScalar("delete from bi_t_branch_info ", null);
            Program.db.ExecuteScalar("delete from bi_t_supcust_info ", null);
            Program.db.ExecuteScalar("delete from ic_t_branch_stock ", null);

            Program.db.ExecuteScalar("delete from bi_t_item_cls ", null);
            Program.db.ExecuteScalar("delete from bi_t_item_info ", null);
            

        }
    }
}
