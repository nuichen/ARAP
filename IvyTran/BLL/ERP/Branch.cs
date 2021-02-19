using System;
using System.Data;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class Branch : IBranch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branch_no"> ''值 </param>
        /// <param name="batch_no"></param>
        /// <param name="item_no"></param>
        /// <param name="type">1：显示有库存的  0：都显示</param>
        /// <param name="is_message">1：不存在凭证 2：存在凭证</param>
        /// <returns></returns>
         DataTable IBranch.GetBranch(string branch_no, string batch_no, string item_no, int type, int? is_message)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = $@"select distinct 
a.branch_no+'/'+d.branch_name branch_no,a.batch_no
,c.valid_date vaild_date,c.produce_date,b.item_no,b.item_name,b.unit_no,a.stock_qty
from dbo.ic_t_batch_branch_stock a 
left join dbo.bi_t_item_info b on a.item_no = b.item_no
left join ic_t_batch_info c on a.branch_no=c.branch_no and a.batch_no=c.batch_no
left join [dbo].[bi_t_branch_info] d on a.branch_no=d.branch_no
where a.branch_no like '%{branch_no}%'
 and a.batch_no like '%{batch_no}%' and (b.item_subno like '%{item_no}%' or b.item_name like '%{item_no}%'
 or a.item_no like '%{item_no}%')";
                if (type == 1)
                {
                    sql += " and stock_qty>0";
                }

                if (is_message == 1)
                {
                    sql += " and  c.branch_no is null";
                }

                if (is_message == 2)
                {
                    sql += " and  c.branch_no is not null";
                }

               DataTable dt = db.ExecuteToTable(sql, null);
            

            return dt;
        }
        System.Data.DataTable IBranch.GetBranchStock(string item_no,string batch_no)
        {
            string sql = $@"select a.item_no,b.item_name,a.batch_no
,a.stock_qty from ic_t_batch_branch_stock a
left join bi_t_item_info b on a.item_no = b.item_no
where a.display_flag = '1' and a.stock_qty > 0
and a.item_no = '{item_no}' and batch_no = '{batch_no}'
order by a.batch_no
";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        System.Data.DataTable IBranch.GetList()
        {
            string sql = "select * from bi_t_branch_info order by branch_no";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        System.Data.DataTable IBranch.GetListByParCode(string par_code)
        {
            string sql = "select * from bi_t_branch_info where branch_no like '" + par_code + "%' order by branch_no";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        System.Data.DataTable IBranch.GetItem(string branch_no)
        {

            string sql = "select * from bi_t_branch_info where branch_no='" + branch_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        string IBranch.MaxCode(string par_code)
        {

            string sql = "select top 1 branch_no from bi_t_branch_info where branch_no like '" + par_code + "%' and len(branch_no)=" + (par_code.Length + 2) +
                " order by branch_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                var val = tb.Rows[0]["branch_no"].ToString();
                val = val.Substring(par_code.Length);
                int index = Helper.Conv.ToInt16(val);
                index++;
                return par_code + index.ToString().PadLeft(2, '0');
            }
            else
            {

                if (par_code == "")
                {
                    return "00";
                }
                else
                {
                    return par_code + "01";
                }

            }
        }

        void IBranch.Add(Model.bi_t_branch_info item)
        {
            item.update_time = System.DateTime.Now;
            string sql = "select * from bi_t_branch_info where branch_no='" + item.branch_no + "'";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已存在机构号" + item.branch_no);
            }
            db.Insert(item);
        }

        void IBranch.Change(Model.bi_t_branch_info item)
        {
            item.update_time = System.DateTime.Now;
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(item, "branch_no");
        }

        void IBranch.Delete(string branch_no)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            var sql = "select top 1 * from ic_t_inout_store_master where branch_no='" + branch_no + "' ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("仓库被出入库单引用，不能删除！");
            }
            //
            sql = "select top 1 * from sm_t_salesheet where branch_no='" + branch_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("仓库被批发销售单引用，不能删除！");
            }
            //
            sql = "delete from bi_t_branch_info where branch_no='" + branch_no + "'";


            db.ExecuteScalar(sql, null);
        }

        public DataTable QuickSearchList(string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);

            string sql = $@"
SELECT *
FROM dbo.bi_t_branch_info
WHERE display_flag='1'
AND (branch_no LIKE '%{keyword}%' OR branch_name LIKE '%{keyword}%')";

            DataTable tb = db.ExecuteToTable(sql, null);

            return tb;
        }

    }

}
