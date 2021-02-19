using System;
using System.Data;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public  class People : IPeople
    {
        System.Data.DataTable IPeople.GetList(string dep_no, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "select a.*,b.dept_name from bi_t_people_info a" +
                " left join bi_t_dept_info b on a.dept_no=b.dept_no" +
                " where 1=1";
            if (dep_no != "")
            {
                sql += " and a.dept_no like '" + dep_no + "%'";
            }
            if (keyword != "")
            {
                sql += " and (a.oper_id like '%@%' or a.oper_name like '%@%')".Replace("@", keyword);
            }
            if (show_stop != 1)
            {
                sql += " and a.oper_status='1'";
            }

            var tb = db.ExecuteToTable(sql, "a.oper_id", null, page_size, page_index, out total_count);
            return tb;
        }

        System.Data.DataTable IPeople.GetItem(string oper_id)
        {
            string sql = "select * from bi_t_people_info where oper_id='" + oper_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        string IPeople.MaxCode()
        {
            string sql = "select top 1  oper_id from bi_t_people_info where len(oper_id)=4 order by oper_id desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count == 0)
            {
                return "0001";
            }
            else
            {
                int index = Helper.Conv.ToInt32(tb.Rows[0]["oper_id"].ToString());
                index++;
                return index.ToString().PadLeft(4, '0');
            }
        }

        void IPeople.Add(Model.bi_t_people_info item)
        {
            string sql = "select * from bi_t_people_info where oper_id='" + item.oper_id + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已经存在职员编号" + item.oper_id);
            }
            db.Insert(item);
        }

        void IPeople.Change(Model.bi_t_people_info item)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(item, "oper_id");
        }

        void IPeople.Delete(string oper_id)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string sql = "";
            //
            sql = "select  top 1 * from pm_t_flow_main where deal_man='" + oper_id + "' ";
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("职员已被调价单引用，不能删除!");
            }
            //
            sql = "select  top 1 * from sm_t_salesheet where sale_man='" + oper_id + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("职员已被批发销售单引用，不能删除!");
            }
            //
            sql = "select  top 1 * from ic_t_inout_store_master where deal_man='" + oper_id + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("职员已被出入库单引用，不能删除!");
            }
            //
            sql = "select top 1  * from ic_t_check_master where deal_man='" + oper_id + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("职员已被盘点单引用，不能删除!");
            }
            //
            sql = "delete from bi_t_people_info where oper_id='" + oper_id + "'";

            db.ExecuteScalar(sql, null);
        }

        public DataTable QuickSearchList(string dept_no, string keyword)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            string condition_sql = "";
            if (dept_no != "")
            {
                condition_sql += "and dept_no='" + dept_no + "' ";
            }
            if (keyword != "")
            {
                condition_sql += "and (oper_id like '%'+@keyword+'%' or oper_name like '%'+@keyword+'%') ";
            }

            string sql = "select top 100 oper_id,oper_name from bi_t_people_info where 1=1 " + condition_sql;
            sql += "order by oper_id ";
            var pars = new System.Data.SqlClient.SqlParameter[]
            {
                new System.Data.SqlClient.SqlParameter("@keyword", keyword),
            };
            var dt = db.ExecuteToTable(sql, pars);
            return dt;
        }


    }
}
