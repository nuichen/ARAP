using System;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public   class Payment:IPayment 
    {
        System.Data.DataTable IPayment.GetList()
        {
            string sql = "select a.*,b.visa_nm from bi_t_payment_info a left join bi_t_bank_info b on a.visa_id=b.visa_id order by a.pay_way  ";
            //string sql = "select a.*,b.* from bi_t_payment_info a left join bi_t_subject_info b on a.subject_no=b.subject_no order by a.pay_way ";
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }

        System.Data.DataTable IPayment.GetItem(string pay_way)
        {
            string sql = "select a.*,b.visa_nm from bi_t_payment_info a left join bi_t_bank_info b on a.visa_id=b.visa_id where a.pay_way='" + pay_way + "'";
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            return db.ExecuteToTable(sql, null);
        }

        void IPayment.Add(Model.bi_t_payment_info item)
        {
            string sql = "select * from bi_t_payment_info where pay_way='" + item.pay_way + "'";
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已经存在付款编码"+item.pay_way );
            }
            db.Insert(item);
        }

        void IPayment.Change(Model.bi_t_payment_info item)
        {
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(item,"pay_way");
        }

        void IPayment.Delete(string pay_way)
        {
            DB.IDB db  = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "";
            //
            sql = "select top 1  * from rp_t_recpay_record_info where pay_way='" + pay_way + "' ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("付款方式已被结算单引用，不能删除!");
            }
            //
            sql = "select  top 1 * from sm_t_salesheet where pay_way='" + pay_way + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("付款方式已被批发销售单引用，不能删除!");
            }
            //
            sql = "select top 1  * from ic_t_inout_store_master where pay_way='" + pay_way + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("付款方式已被出入库单引用，不能删除!");
            }
            //
            sql = "select  top 1 * from bank_t_cash_master where pay_way='" + pay_way + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("付款方式已被单据引用，不能删除!");
            }
            //
            sql = "delete from bi_t_payment_info where pay_way='" + pay_way + "'";
           
            db.ExecuteScalar(sql, null);
        }
    }
}
