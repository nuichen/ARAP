using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using IvyBack.Helper;
using System.Data;
using Model;

namespace IvyBack.BLL
{
    public class BranchBLL : IBranch
    {
        public DataTable GetAllList(int code_len)
        {
            JsonRequest r = new JsonRequest();

            r.Write("code_len", code_len);

            r.request("/branch?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

        public System.Data.DataTable GetAllList(string code)
        {
            JsonRequest r = new JsonRequest();

            r.Write("par_code", code);

            r.request("/branch?t=get_list_by_par_code");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            tb.Columns.Add("状态");
            foreach (DataRow dr in tb.Rows)
            {
                if (dr["display_flag"].ToString().Equals("1"))
                {
                    dr["状态"] = "已启用";
                }
                else if (dr["display_flag"].ToString().Equals("0"))
                {
                    dr["状态"] = "已禁用";
                }
            }
            return tb;

        }

        public Model.bi_t_branch_info GetItem(Model.bi_t_branch_info branch)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_branch_info>(branch);

            r.request("/branch?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            branch = r.GetObject<bi_t_branch_info>("data");

            return branch;
        }

        public string GetMaxCode(string code)
        {
            JsonRequest r = new JsonRequest();

            r.Write("par_code", code);

            r.request("/branch?t=max_code");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            code = r.Read("code");

            return code;
        }

        public void Add(Model.bi_t_branch_info branch)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_branch_info>(branch);

            r.request("/branch?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        public void Upload(Model.bi_t_branch_info branch)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_branch_info>(branch);

            r.request("/branch?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Del(Model.bi_t_branch_info branch)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_branch_info>(branch);

            r.request("/branch?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public DataTable QuickSearchList(string keyword)
        {
            JsonRequest r = new JsonRequest();

            r.Write("keyword", keyword);

            r.request("/branch?t=QuickSearchList");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();

            return tb;
        }

    }
}
