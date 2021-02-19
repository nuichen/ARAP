using System;
using System.Collections.Generic;

namespace IvyFront.BLL
{
    public class CGRK : IBLL.ICGRK
    {
        string IBLL.ICGRK.GetNewTHOrderCode()
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='RI'";
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
                
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='RI'";
                Program.db.ExecuteScalar(sql, null);
                return "RI00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        string IBLL.ICGRK.GetNewOrderCode()
        {
            string sql = "select sheet_value from sys_t_sheet_no where sheet_id='PI'";
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
                
                sql = "update sys_t_sheet_no set sheet_value=" + index + " where sheet_id='PI'";
                Program.db.ExecuteScalar(sql, null);
                return "PI00" + System.DateTime.Now.ToString("yyMMdd") + index.ToString().PadLeft(4, '0');
            }
        }

        void IBLL.ICGRK.InsertOrder(Model.ic_t_inout_store_master ord, List<Model.ic_t_inout_store_detail> items)
        {
            var db = new DB.SQLiteByHandClose(Program.conn);
            DB.IDB d = db;
            try
            {
                db.Open();
                db.BeginTran();
                //
                string sql = "select 1 from ic_t_inout_store_master where sheet_no='" + ord.sheet_no + "' limit 0,1 ";
                var dt = d.ExecuteToTable(sql, null);
                if (dt.Rows.Count > 0)
                {
                    throw new Exception("已经存在单据号:" + ord.sheet_no);
                }
                //
                foreach (Model.ic_t_inout_store_detail item in items)
                {
                    sql = "insert into ic_t_inout_store_detail values('" + item.sheet_no + "','" + item.item_no + "','" + item.item_subno + "','" + item.item_name + "','" + item.unit_no + "','" + item.in_qty + "','" + item.orgi_price + "'";
                    sql += ",'" + item.valid_price + "','" + item.cost_price + "','" + item.valid_date + "','" + item.barcode + "','" + item.sheet_sort + "','" + item.other3 + "') ";
                    d.ExecuteScalar(sql, null);
                }
                //
                sql = "insert into ic_t_inout_store_master values('" + ord.sheet_no + "','" + ord.trans_no + "','" + ord.branch_no + "','" + ord.supcust_no + "','" + ord.total_amount + "'";
                sql += ",'" + ord.inout_amount + "','" + ord.approve_flag + "','" + ord.oper_date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + ord.oper_id + "','0','" + ord.pay_way + "') ";
                d.ExecuteScalar(sql, null);
                //
                db.CommitTran();
            }
            catch (Exception ex)
            {
                db.RollBackTran();
                Log.writeLog("CGRK.InsertOrder()", ex.ToString(), ord.sheet_no);
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
