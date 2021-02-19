using System;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.BLL.ERP
{
    public class Dep : IDep
    {

        System.Data.DataTable IDep.GetList()
        {
            string sql = "select * from bi_t_dept_info order by dept_no";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        System.Data.DataTable IDep.GetListF(string name)
        {
            string sql = "select f_dept_no from bi_t_dept_info where dept_name='"+name+"'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }
        System.Data.DataTable IDep.GetItem(string dep_no)
        {
            string sql = "select * from bi_t_dept_info where dept_no='" + dep_no + "'";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            return tb;
        }

        string IDep.MaxCode(string par_code)
        {
            string sql = "select  top 1 dept_no from bi_t_dept_info where dept_no like '" + par_code + "%' and len(dept_no)=" + (par_code.Length + 2) +
            " order by dept_no desc ";
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                var val = tb.Rows[0]["dept_no"].ToString();
                val = val.Substring(par_code.Length);
                int index = Helper.Conv.ToInt16(val);
                index++;
                return par_code + index.ToString().PadLeft(2, '0');
            }
            else
            {
                return par_code + "01";
            }
        }

        void IDep.Add(Model.bi_t_dept_info item)
        {
            string sql = "select * from bi_t_dept_info where dept_no='" + item.dept_no + "'";

            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已经存在部门编号" + item.dept_no);
            }
            db.Insert(item);
        }

        void IDep.Change(Model.bi_t_dept_info item)
        {
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            db.Update(item, "dept_no");
        }

        void IDep.Delete(string dep_no)
        {
            string sql = "select * from bi_t_dept_info where dept_no like '" +
              dep_no + "%' and dept_no<>'" + dep_no + "'";
            LogHelper.writeLog("", sql);
            DB.IDB db = new DB.DBByAutoClose(AppSetting.conn);
            var tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("存在子分类，不能删除!");
            }
            //
            sql = "select  top 1 * from bi_t_people_info where dept_no='" + dep_no + "' ";
            tb = db.ExecuteToTable(sql, null);
            if (tb.Rows.Count != 0)
            {
                throw new Exception("已经存在职员，不能删除!");
            }
            //
            sql = "delete from bi_t_dept_info where dept_no='" + dep_no + "'";
            db.ExecuteScalar(sql, null);
        }

    }

}
