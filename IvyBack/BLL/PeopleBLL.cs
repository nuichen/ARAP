using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using IvyBack.Helper;
using Model;
using System.Data;

namespace IvyBack.BLL
{
    public class PeopleBLL : IPeople
    {
        public System.Data.DataTable GetDataTable(string dep_no, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            JsonRequest r = new JsonRequest();

            r.Write("dep_no", dep_no);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write("page_size", page_size);
            r.Write("page_index", page_index);

            r.request("/people?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            total_count = r.ReadToInt("total_count");
            return tb;
        }

        public Helper.Page<Model.bi_t_people_info> GetDataTable(string dep_no, string keyword, int show_stop, Helper.Page<Model.bi_t_people_info> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("dep_no", dep_no);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write<bi_t_people_info>(page);

            r.request("/people?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page.Tb = r.GetDataTable();

            Conv.AddColorTable(page.Tb, "oper_name");

            page.PageCount = r.ReadToInt("total_count");
            return page;
        }

        public Model.bi_t_people_info GetItem(string oper_id)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", oper_id);

            r.request("/people?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bi_t_people_info peo = r.GetObject<bi_t_people_info>();

            return peo;
        }

        public string GetMaxCode()
        {
            JsonRequest r = new JsonRequest();

            r.request("/people?t=max_code");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            string max_code = r.Read("code");

            return max_code;
        }

        public void Add(Model.bi_t_people_info peo)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_people_info>(peo);

            r.request("/people?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Update(Model.bi_t_people_info peo)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_people_info>(peo);

            r.request("/people?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Del(Model.bi_t_people_info peo)
        {
            JsonRequest r = new JsonRequest();

            r.Write("oper_id", peo.oper_id);

            r.request("/people?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public DataTable QuickSearchList(string dept_no, string keyword)
        {
            JsonRequest r = new JsonRequest();

            r.Write("dept_no", dept_no);
            r.Write("keyword", keyword);

            r.request("/people?t=QuickSearchList");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }
    }
}
