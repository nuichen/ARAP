using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Server.IBLL;

namespace Server.BLL
{
    public class SysBLL : ISysBLL
    {
        body.merchant IBLL.ISysBLL.GetMerchantByKey(string key)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            string sql = "select top 1 * from merchant where lower(mer_key)=@key ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@key", key.ToLower())
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                var item = new body.merchant();
                item.mer_key = dt.Rows[0]["mer_key"].ToString();
                item.mer_id = dt.Rows[0]["mer_id"].ToString();
                item.mer_name = dt.Rows[0]["mer_name"].ToString();
                item.mer_person = dt.Rows[0]["mer_person"].ToString();
                item.pwd = dt.Rows[0]["pwd"].ToString();
                item.status = dt.Rows[0]["status"].ToString();
                item.start_time = Conv.ToDateTime(dt.Rows[0]["start_time"]);
                item.end_time = Conv.ToDateTime(dt.Rows[0]["end_time"]);
                return item;
            }
            else 
            {
                return null;
            }
        }

        body.merchant IBLL.ISysBLL.GetMerchantById(string id)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            string sql = "select top 1 * from merchant where mer_id=@id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@id", id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                var item = new body.merchant();
                item.mer_key = dt.Rows[0]["mer_key"].ToString();
                item.mer_id = dt.Rows[0]["mer_id"].ToString();
                item.mer_name = dt.Rows[0]["mer_name"].ToString();
                item.mer_person = dt.Rows[0]["mer_person"].ToString();
                item.pwd = dt.Rows[0]["pwd"].ToString();
                item.status = dt.Rows[0]["status"].ToString();
                item.start_time = Conv.ToDateTime(dt.Rows[0]["start_time"]);
                item.end_time = Conv.ToDateTime(dt.Rows[0]["end_time"]);
                return item;
            }
            else
            {
                return null;
            }
        }

        body.wxpay IBLL.ISysBLL.GetMerWxpayById(string id)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            string sql = "select * from wxpay where mer_id=@id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@id", id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                var item = new body.wxpay();
                item.mer_id = dt.Rows[0]["mer_id"].ToString();
                item.wx_appid = dt.Rows[0]["wx_appid"].ToString();
                item.wx_mcid = dt.Rows[0]["wx_mcid"].ToString();
                item.wx_paykey = dt.Rows[0]["wx_paykey"].ToString();
                item.wx_secret = dt.Rows[0]["wx_secret"].ToString();
                return item;
            }
            else
            {
                return null;
            }
        }

        body.alipay IBLL.ISysBLL.GetMerAlipayById(string id)
        {
            DB.IDB db = new DB.DBByAutoClose(Appsetting.conn);
            string sql = "select * from alipay where mer_id=@id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@id", id)
            };
            var dt = db.ExecuteToTable(sql, pars);
            if (dt.Rows.Count > 0)
            {
                var item = new body.alipay();
                item.mer_id = dt.Rows[0]["mer_id"].ToString();
                item.rsa1 = dt.Rows[0]["rsa1"].ToString();
                item.rsa2 = dt.Rows[0]["rsa2"].ToString();
                item.aes = dt.Rows[0]["aes"].ToString();
                item.app_id = dt.Rows[0]["app_id"].ToString();
                item.rsa1_private = dt.Rows[0]["rsa1_private"].ToString();
                item.pid = dt.Rows[0]["pid"].ToString();
                return item;
            }
            else
            {
                return null;
            }
        }

    }
}