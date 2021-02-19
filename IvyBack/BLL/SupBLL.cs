using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using System.Data;
using IvyBack.Helper;
using Model;


namespace IvyBack.BLL
{
    public class SupBLL : ISup
    {
        public DataTable GetFactory()
        {
            JsonRequest r = new JsonRequest();


            r.request("/sup?t=get_factory");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            return r.GetDataTable("data");
        }

        public DataTable GetDataTable(string region_no, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            JsonRequest r = new JsonRequest();

            r.Write("region_no", region_no);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write("page_index", page_index);
            r.Write("page_size", page_size);

            r.request("/sup?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            total_count = r.ReadToInt("total_count");
            return tb;
        }

        public Page<bi_t_supcust_info> GetDataTable(string region_no, string keyword, int show_stop, Page<bi_t_supcust_info> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("region_no", region_no);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write<bi_t_supcust_info>(page);

            r.request("/sup?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page.Tb = r.GetDataTable();
            page.PageCount = r.ReadToInt("total_count");
            return page;
        }

        public bi_t_supcust_info GetItem(string supcust_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("supcust_no", supcust_no);

            r.request("/sup?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bi_t_supcust_info sup = r.GetObject<bi_t_supcust_info>("data");

            return sup;
        }

        public string GetMaxCode()
        {
            JsonRequest r = new JsonRequest();

            r.request("/sup?t=max_code");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            string max_code = r.Read("code");

            return max_code;
        }

        public void Add(bi_t_supcust_info sup)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_supcust_info>(sup);

            r.request("/sup?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Adds(List<bi_t_supcust_info> supcustInfos)
        {
            JsonRequest r = new JsonRequest();

            r.Write(supcustInfos);

            r.request("/sup?t=Adds");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Update(bi_t_supcust_info sup)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_supcust_info>(sup);

            r.request("/sup?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }


        public void Del(Model.bi_t_supcust_info sup)
        {
            JsonRequest r = new JsonRequest();

            r.Write("supcust_no", sup.supcust_no);

            r.request("/sup?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public DataTable QuickSearchList(string keyword)
        {
            JsonRequest r = new JsonRequest();

            r.Write("keyword", keyword);

            r.request("/sup?t=QuickSearchList");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public List<bi_t_supcust_info> GetALL()
        {
            JsonRequest r = new JsonRequest();

            r.request("/sup?t=GetALL");

            r.WhetherSuccess();

            List<bi_t_supcust_info> lis = r.GetList<bi_t_supcust_info>("lis");

            return lis;
        }

        public DataTable GetSupBindItem(string sup_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sup_no",sup_no);

            r.request("/sup?t=GetSupBindItem");

            r.WhetherSuccess();

            DataTable tb = r.GetDataTable();

            return tb;
        }

        public void SaveSupBindItem(bi_t_supcust_bind_item bind_item)
        {
            JsonRequest r = new JsonRequest();

            r.Write("bind_item", bind_item);

            r.request("/sup?t=SaveSupBindItem");

            r.WhetherSuccess();
        }
    }
}
