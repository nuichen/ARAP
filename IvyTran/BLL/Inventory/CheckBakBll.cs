using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DB;
using IvyTran.BLL.ERP;
 
using IvyTran.body.Inventory;
using IvyTran.IBLL.ERP;
using IvyTran.IBLL.Inventory;

namespace IvyTran.BLL.Inventory
{
    public class CheckBakBll : ICheckBak
    {

        public void Clear()
        {
            string sql = "delete from pda_ot_t_check_bak";
            IDB db = new DBByAutoClose(AppSetting.conn);
            db.ExecuteScalar(sql, null);
        }

        public void Insert(List<pda_ot_t_check_bak> lst)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            try
            {
                d.Open();
                d.BeginTran();
                //

                string str1 = "insert into pda_ot_t_check_bak(sheet_no,branch_no,item_no," +
                              "stock_qty,cost_price,price,sale_price)values";
                foreach (pda_ot_t_check_bak item in lst)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(str1);
                    sb.Append("('");
                    sb.Append(item.sheet_no);
                    sb.Append("','");
                    sb.Append(item.branch_no);
                    sb.Append("','");
                    sb.Append(item.item_no);
                    sb.Append("',");
                    sb.Append(item.stock_qty);
                    sb.Append(",");
                    sb.Append(item.cost_price);
                    sb.Append(",");
                    sb.Append(item.price);
                    sb.Append(",");
                    sb.Append(item.sale_price);
                    sb.Append(")");
                    db.ExecuteScalar(sb.ToString(), null);
                    sb = null;
                }
                //
                d.CommitTran();
            }
            catch (Exception)
            {
                d.RollBackTran();
                throw;
            }
            finally
            {
                d.Close();
            }
        }

        public DataTable GetList()
        {
            ISys par = new Sys();

            var check_back_sheet = par.Read("check_back_sheet");

            string sql = "select * from pda_ot_t_check_bak where sheet_no='" + check_back_sheet + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public decimal GetStock(string item_no)
        {
            string sql = "select stock_qty from pda_ot_t_check_bak where item_no='" + item_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            object obj = db.ExecuteScalar(sql, null);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                decimal val = 0;
                decimal.TryParse(obj.ToString(), out val);
                return val;
            }
        }

    }
}