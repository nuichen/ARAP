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
    public class ItemClsBLL : IItemCls
    {
        System.Data.DataTable IItemCls.GetDataTable()
        {
            JsonRequest r = new JsonRequest();

            r.request("/item_cls?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public List<bi_t_item_cls> GetAllList()
        {
            JsonRequest r = new JsonRequest();

            r.request("/item_cls?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            List<bi_t_item_cls> list = r.GetList<bi_t_item_cls>();
            return list;
        }

        public bi_t_item_cls GetItemCls(bi_t_item_cls itemcls)
        {
            JsonRequest r = new JsonRequest();

            r.request("/item_cls?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bi_t_item_cls list = r.GetObject<bi_t_item_cls>("data");
            return list;
        }

        public DataTable GetListForMenu(string is_first_level = "1")
        {
            JsonRequest r = new JsonRequest();
            r.Write("is_first_level", is_first_level);

            r.request("/item_cls?t=GetListForMenu");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            if (tb.Rows.Count > 0)
            {
                tb.Columns.Add("is_check");
                foreach (DataRow dr in tb.Rows)
                {
                    dr["is_check"] = "0";
                }
            }
            return tb;

        }

        public void Add(bi_t_item_cls itemcls)
        {
            JsonRequest r = new JsonRequest();
            r.Write<bi_t_item_cls>(itemcls);

            r.request("/item_cls?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

        }

        public void Del(bi_t_item_cls itemcls)
        {
            JsonRequest r = new JsonRequest();
            r.Write<bi_t_item_cls>(itemcls);

            r.request("/item_cls?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Upload(bi_t_item_cls itemcls)
        {
            JsonRequest r = new JsonRequest();
            r.Write<bi_t_item_cls>(itemcls);

            r.request("/item_cls?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public string GetMaxCode(string code)
        {
            JsonRequest r = new JsonRequest();
            r.Write("par_code", code);

            r.request("/item_cls?t=max_code");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            code = r.Read("code");
            return code;
        }

        public string GetGroupCls(string group_no)
        {
            JsonRequest r = new JsonRequest();
            r.Write("group_no", group_no);

            r.request("/item_cls?t=GetGroupCls");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            string result = r.ReadResult();
            return result;
        }

        public void SaveGroupCls(string group_no, string item_clsnos)
        {
            JsonRequest r = new JsonRequest();
            r.Write("group_no", group_no);
            r.Write("item_clsnos", item_clsnos);

            r.request("/item_cls?t=SaveGroupCls");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
    }
}
