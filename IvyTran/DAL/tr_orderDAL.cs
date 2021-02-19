using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Model;
using Conv = IvyTran.Helper.Conv;

namespace IvyTran.DAL
{
    public class tr_orderDAL
    {
        private DB.IDB d;
        public tr_orderDAL(DB.IDB db)
        {
            d = db;
        }
        public tr_order SelectById(string ord_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql_s_tr_order = "select * from tr_order where ord_id = @ord_id";
            var pars_s_tr_order = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@ord_id",ord_id)
            };
            var dt_s_tr_order = db.ExecuteToTable(sql_s_tr_order, pars_s_tr_order);
            tr_order tr_order = new tr_order();
            if (dt_s_tr_order.Rows.Count == 0)
            {
                tr_order = null;
            }
            else
            {
                DataRow row = dt_s_tr_order.Rows[0];
                tr_order.ord_id = row["ord_id"].ToString();
                tr_order.status = row["status"].ToString();
                tr_order.qty = Conv.ToDecimal(row["qty"].ToString());
                tr_order.amount = Conv.ToDecimal(row["amount"].ToString());
                tr_order.enable_qty = Conv.ToDecimal(row["enable_qty"].ToString());
                tr_order.enable_amount = Conv.ToDecimal(row["enable_amount"].ToString());
                tr_order.create_time = Conv.ToDateTime(row["create_time"]);
                tr_order.check_oper_id = row["check_oper_id"].ToString();
                tr_order.company = row["company"].ToString();
                tr_order.sname = row["sname"].ToString();
                tr_order.mobile = row["mobile"].ToString();
                tr_order.address = row["address"].ToString();
                tr_order.openid = row["openid"].ToString();
                tr_order.pay_type = row["pay_type"].ToString();
                tr_order.mc_id = Conv.ToInt(row["mc_id"].ToString());
                tr_order.send_status = row["send_status"].ToString();
                tr_order.build_status = row["build_status"].ToString();
                tr_order.open_type = row["open_type"].ToString();
                tr_order.reach_time = row["reach_time"].ToString();
                tr_order.cus_remark = row["cus_remark"].ToString();
                tr_order.to_the_code = row["to_the_code"].ToString();
                tr_order.distribution_type = row["distribution_type"].ToString();
                tr_order.discount_amt = Conv.ToDecimal(row["discount_amt"].ToString());
                tr_order.take_fee = Conv.ToDecimal(row["take_fee"].ToString());
                tr_order.pay_weixin = Conv.ToDecimal(row["pay_weixin"].ToString());
                tr_order.cus_no = row["cus_no"].ToString();
                tr_order.salesman_id = row["salesman_id"].ToString();
            }
            return tr_order;
        }
    }
}