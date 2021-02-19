using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using Model;
using IvyBack.Helper;
using System.Data;

namespace IvyBack.BLL
{
    public class RegionBLL : IRegion
    {
        public System.Data.DataTable GetDataTable(string is_cs)
        {
            JsonRequest r = new JsonRequest();
            r.Write("is_cs", is_cs);
            r.request("/region?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public List<bi_t_region_info> GetAllList(string is_cs)
        {
            JsonRequest r = new JsonRequest();
            r.Write("is_cs", is_cs);
            r.request("/region?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            List<bi_t_region_info> list = r.GetList<bi_t_region_info>();
            return list;
        }

        public bi_t_region_info GetRegion(string region_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("region_no", region_no);

            r.request("/region?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bi_t_region_info item = r.GetObject<bi_t_region_info>("data");
            return item;
        }

        public void Add(bi_t_region_info region)
        {
            JsonRequest r = new JsonRequest();
            r.Write<bi_t_region_info>(region);

            r.request("/region?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        public void Del(bi_t_region_info region)
        {
            JsonRequest r = new JsonRequest();
            r.Write<bi_t_region_info>(region);

            r.request("/region?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Upload(bi_t_region_info region)
        {
            JsonRequest r = new JsonRequest();
            r.Write<bi_t_region_info>(region);

            r.request("/region?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public string GetMaxCode()
        {
            JsonRequest r = new JsonRequest();

            r.request("/region?t=max_code");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            string code = r.Read("code");
            return code;
        }


    }
}
