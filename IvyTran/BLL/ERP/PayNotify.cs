using System;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class PayNotify : IPayNotify
    {
        void IPayNotify.Success(string pay_no, string ord_id)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select * from ot_pay_record where sheet_no=@sheet_no ";
                var pars = new System.Data.SqlClient.SqlParameter[] 
                {
                    new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count == 0)
                {
                    throw new ExceptionBase("不存在支付记录");
                }

                if (dt.Rows[0]["pay_type"].ToString() != "W" && dt.Rows[0]["pay_type"].ToString() != "Z")//该订单不属于微信支付或者支付宝支付
                {
                    return;
                }
                sql = "update ot_pay_record set trade_no=@pay_no,status='1' where sheet_no=@sheet_no ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                {
                    new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id),
                    new System.Data.SqlClient.SqlParameter("@pay_no",pay_no)
                };
                db.ExecuteToTable(sql, pars);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        void IPayNotify.Fail(string pay_no, string ord_id)
        {
            try
            {
                DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
                string sql = "select * from ot_pay_record where sheet_no=@sheet_no ";
                var pars = new System.Data.SqlClient.SqlParameter[] 
                {
                    new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id)
                };
                var dt = db.ExecuteToTable(sql, pars);
                if (dt.Rows.Count == 0)
                {
                    throw new ExceptionBase("不存在支付记录");
                }

                if (dt.Rows[0]["pay_type"].ToString() != "W" && dt.Rows[0]["pay_type"].ToString() != "Z")//该订单不属于微信支付或者支付宝支付
                {
                    return;
                }
                sql = "update ot_pay_record set status='2' where sheet_no=@sheet_no ";
                pars = new System.Data.SqlClient.SqlParameter[] 
                {
                    new System.Data.SqlClient.SqlParameter("@sheet_no",ord_id)
                };
                db.ExecuteToTable(sql, pars);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}