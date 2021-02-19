using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace IvyTran.DAL
{
    public class merchant
    {
        public global::Model.merchant SelectByMobile(string mobile)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql_s_merchant = "select * from merchant where mobile = @mobile";
            var pars_s_merchant = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@mobile",mobile)
            };
            var dt_s_merchant = db.ExecuteToTable(sql_s_merchant, pars_s_merchant);
            if (dt_s_merchant.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                global::Model.merchant merchant = new global::Model.merchant();
                DataRow row = dt_s_merchant.Rows[0];
                int mc_id = 0;
                int.TryParse(row["mc_id"].ToString(), out mc_id);
                merchant.mc_id = mc_id;
                merchant.mc_no = row["mc_no"].ToString();
                merchant.mc_name = row["mc_name"].ToString();
                merchant.contact = row["contact"].ToString();
                merchant.mobile = row["mobile"].ToString();
                merchant.address = row["address"].ToString();
                merchant.pwd = row["pwd"].ToString();
                DateTime time = DateTime.Now;
                DateTime.TryParse(row["reg_time"].ToString(), out time);
                merchant.reg_time = time;
                merchant.status = row["status"].ToString();
                merchant.qrcode = row["qrcode"].ToString();
                return merchant;
            }
        }
    }
}