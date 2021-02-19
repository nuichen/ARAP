using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyFront.BLL
{
    class Branch:IBLL.IBranch 
    {
        Dictionary<string, Model.bi_t_branch_info> IBLL.IBranch.GetList()
        {
            string sql = "select * from bi_t_branch_info";
            var tb = Program.db.ExecuteToTable(sql, null);
            Dictionary<string, Model.bi_t_branch_info> dic = new Dictionary<string, Model.bi_t_branch_info>();
            foreach (DataRow row in tb.Rows)
            {
                dic.Add(row["branch_no"].ToString(), DB.ReflectionHelper.DataRowToModel<Model.bi_t_branch_info>(row));
            }
            return dic;
        }

        string IBLL.IBranch.GetBranch(string branch_no) 
        {
            string sql = "select * from bi_t_branch_info where branch_no='" + branch_no + "'";
            var tb = Program.db.ExecuteToTable(sql, null);
            if (tb.Rows.Count > 0)
            {
                return tb.Rows[0]["branch_name"].ToString();
            }
            else 
            {
                return "";
            }
        }
    }

}
