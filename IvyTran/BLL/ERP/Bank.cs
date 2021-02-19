using System;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public  class Bank : IBank
    {
        System.Data.DataTable IBank.GetList()
        {
            string sql = "select * from bi_t_bank_info where display_flag='1' order by visa_id";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        System.Data.DataTable IBank.GetItem(string visa_id)
        {
            string sql = "select * from bi_t_bank_info where visa_id='" + visa_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        System.Data.DataTable IBank.GetSubjectList()
        {
            string sql = "select * from bi_t_subject_info ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        void IBank.Add(Model.bi_t_bank_info item)
        {
            string sql = "select * from bi_t_bank_info where visa_id='" + item.visa_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已存在银行编号" + item.visa_id);
            }
            db.Insert(item);
        }

        void IBank.Change(Model.bi_t_bank_info item)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(item, "visa_id");
        }

        void IBank.Delete(string visa_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select top 1 * from bi_t_payment_info where visa_id='" + visa_id + "'  ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("现金银行账户已被付款方式引用，不能删除!");
            }
            //
            sql = "select top 1 * from bank_t_cash_master where visa_id='" + visa_id + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("现金银行账户已被单据引用，不能删除!");
            }
            //
            sql = "select top 1 * from rp_t_recpay_record_info where visa_id='" + visa_id + "'  ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("现金银行账户已被单据引用，不能删除!");
            }
            //
            sql = "delete from bi_t_bank_info where visa_id='" + visa_id + "'";

            db.ExecuteScalar(sql, null);
        }

    }
}
