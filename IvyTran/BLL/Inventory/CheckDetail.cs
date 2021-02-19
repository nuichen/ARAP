using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IvyTran.body.Inventory;
using IvyTran.Helper;
using IvyTran.IBLL.Inventory;

namespace IvyTran.BLL.Inventory
{
    public class CheckDetail : ICheckDetail
    {
        public DataTable GetList(DateTime date1, DateTime date2, string sheet_no, string counter_no, string jh, string oper)
        {
            string sql = "select a.*,goods.item_subno,goods.item_name,goods.unit_no,oper.oper_name as oper_man_name from " +
                         "pda_ot_t_check_detail a left join pda_bi_t_item_info goods on a.item_no=goods.item_no " +
                         "left join pda_st_t_oper_info oper on a.oper_man=oper.oper_id where 1=1 ";
            if (1 == 1)
            {
                sql += " and convert(varchar(50), a.oper_time ,120)>='" + date1.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                sql += " and convert(varchar(50), a.oper_time ,120)<='" + date2.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            if (sheet_no != "")
            {
                sql += " and a.sheet_no='" + sheet_no + "'";
            }
            if (counter_no != "")
            {
                sql += " and a.counter_no='" + counter_no + "'";
            }
            if (jh != "")
            {
                sql += " and a.jh='" + jh + "'";
            }
            if (oper != "")
            {
                sql += " and (a.oper_man='" + oper + "' or oper.oper_name='" + oper + "')";
            }
            sql += " order by a.oper_time";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        public bool Insert(List<pda_ot_t_check_detail> lst)
        {
            var d = new DB.DBByHandClose(AppSetting.conn);
            DB.IDB db = d;
            string sheet_no = "P" + System.DateTime.Now.ToString("yyMMddHHmmssfff");
            string msg = "";

            try
            {
                d.Open();
                d.BeginTran();
                //
                string str1 = "insert into pda_ot_t_check_detail(sheet_no,flow_id,counter_no," +
                    "item_no,input_code,qty,jh,oper_time,oper_man,oper_type,master_no)values";
                for (int i = 0; i < lst.Count; i++)
                {

                    pda_ot_t_check_detail item = lst[i];
                    item.sheet_no = sheet_no;
                    item.flow_id = i + 1;
                    StringBuilder sb = new StringBuilder();
                    sb.Append(str1);
                    sb.Append("('");
                    sb.Append(item.sheet_no);
                    sb.Append("',");
                    sb.Append(item.flow_id.ToString());
                    sb.Append(",'");
                    sb.Append(item.counter_no);
                    sb.Append("','");
                    sb.Append(item.item_no);
                    sb.Append("','");
                    sb.Append(item.input_code);
                    sb.Append("',");
                    sb.Append(item.qty.ToString());
                    sb.Append(",'");
                    sb.Append(item.jh);
                    sb.Append("','");
                    sb.Append(item.oper_time.ToString("yyyy-MM-dd HH:mm:ss"));
                    sb.Append("','");
                    sb.Append(item.oper_man);
                    sb.Append("','");
                    sb.Append(item.oper_type);
                    sb.Append("','");
                    sb.Append(item.master_no);
                    sb.Append("')");
                    msg = sb.ToString();
                    db.ExecuteScalar(sb.ToString(), null);
                    sb = null;
                }

                //
                d.CommitTran();
                //
                return true;
            }
            catch (Exception ex)
            {
                d.RollBackTran();
                LogHelper.writeLog(msg, ex.Message + ex.StackTrace);

                throw new Exception(ex.Message);
            }
            finally
            {
                d.Close();
            }
        }

        public decimal GetQtySum(string item_no)
        {
            string sql = "select sum(qty) from pda_ot_t_check_detail where item_no='" + item_no + "'";
            DB.IDB db = new DB.DBByAutoClose();
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

        public void Clear()
        {
            string sql = "delete from pda_ot_t_check_detail";
            DB.IDB db = new DB.DBByAutoClose();
            db.ExecuteScalar(sql, null);
        }
    }
}