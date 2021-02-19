using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyFront.BLL
{
    class XSCK : IBLL.IXSCK
    {
        string IBLL.IXSCK.GetNewOrderCode()
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='SO'";
            object obj = Program.db.ExecuteScalar(sql, null);
            if (obj == null || obj == DBNull.Value)
            {
                return "";
            }
            else
            {
                int index = 0;
                int.TryParse(obj.ToString(), out index);
                if (index >= 9999)
                {
                    index = 0;
                }
                index += 1;
                
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='SO'";
                Program.db.ExecuteScalar(sql, null);
                return "SO00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        void IBLL.IXSCK.InsertOrder(Model.sm_t_salesheet ord, List<Model.sm_t_salesheet_detail> items)
        {
            var db = new DB.SQLiteByHandClose(Program.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select 1 from sm_t_salesheet where sheet_no='" + ord.sheet_no + "' limit 0,1 ";
                var dt = d.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    throw new Exception("已经存在单据号:" + ord.sheet_no);
                }
                //
                foreach (Model.sm_t_salesheet_detail item in items)
                {
                    sql = "insert into sm_t_salesheet_detail values('" + item.sheet_no + "','" + item.item_no + "','" + item.item_subno + "','" + item.item_name + "','" + item.unit_no + "','" + item.sale_price + "','" + item.real_price + "'";
                    sql += ",'" + item.cost_price + "','" + item.sale_qnty + "','" + item.sale_money + "','" + item.barcode + "','" + item.sheet_sort + "','" + item.other3 + "') ";
                    d.ExecuteScalar(sql,null);
                }
                sql = "insert into sm_t_salesheet values('" + ord.sheet_no + "','" + ord.branch_no + "','" + ord.cust_no + "','" + ord.pay_way + "','" + ord.coin_no + "'";
                sql += ",'" + ord.real_amount + "','" + ord.total_amount + "','" + ord.paid_amount + "','" + ord.approve_flag + "','" + ord.oper_id + "','" + ord.sale_man + "'";
                sql += ",'" + ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ord.pay_date.ToString("yyyy-MM-dd HH:mm:ss") + "','0','" + ord.discount + "') ";
                d.ExecuteScalar(sql,null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                Log.writeLog("XSCK.InsertOrder()", ex.ToString(), ord.sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
