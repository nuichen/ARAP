using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IvyBack.IBLL;
using Model;
using IvyBack.Helper;
using System.Data;
using bi_t_sup_item = IvyBack.body.bi_t_sup_item;

namespace IvyBack.BLL
{
    public class ItemBLL : IItem
    {
        #region 供应商报价
        public DataTable GetSupItemList(string item_clsno,string item_subno)
        {
            JsonRequest r = new JsonRequest();
            r.Write("item_clsno", item_clsno);
            r.Write("item_subno", item_subno);
            r.request("/item?t=get_supitemlist");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable dt = r.GetDataTable("tb");
            return dt;
        }
        public DataTable GetSupList(string sup_no)
        {
            JsonRequest r = new JsonRequest();
            r.Write("sup_no", sup_no);
            
            r.request("/item?t=get_suplist");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable dt = r.GetDataTable("tb");
            return dt;
        }
        public DataTable GetItemClsList()
        {
            JsonRequest r = new JsonRequest();
            r.request("/item?t=get_supitemclslist");
            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable dt = r.GetDataTable("tb");
            return dt;
        }
        public DataTable GetSupItemPrice()
        {
            JsonRequest r = new JsonRequest();
            r.request("/item?t=get_supitemprice");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable dt = r.GetDataTable("tb");
            return dt;
        }
        public void UpdateSupItemPrice(List<Model.bi_t_sup_item> bi)
        {
            JsonRequest r = new JsonRequest();
            r.Write("bi", bi);

            r.request("/item?t=update_supitemprice");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }
        public void CheckSupItemPrice(string item_no, string sup_no, string oper_id, DateTime time)
        {
            JsonRequest r = new JsonRequest();
            r.Write("item_no", item_no);
            r.Write("sup_no", sup_no);
            r.Write("oper_id", oper_id);
            r.Write("time", time);

            r.request("/item?t=check_supitemprice");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        #endregion
        public DataTable GetLikeItem_ChengBen(string sup_id, int sup_type, string item_subno, string type, string branch_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sup_id", sup_id);
            r.Write("sup_type", sup_type);
            r.Write("item_subno", item_subno);
            r.Write("type", type);
            r.Write("branch_no", branch_no);

            r.request("/item?t=GetLikeItem_ChengBen");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }
        public DataTable GetItemStock_ChengBen(string branch_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("branch_no", branch_no);

            r.request("/item?t=GetItemStock_ChengBen");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }
        public bool Getk3Item(string item_no)
        {
            JsonRequest r = new JsonRequest();
            r.Write("item_no", item_no);

            r.request("/item?t=get_k3item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bool i = Convert.ToBoolean(r.Read("b"));
            return i;
        }
        public bool k3Item(string item_no,string type)
        {
            JsonRequest r = new JsonRequest();
            r.Write("item_no", item_no);
            r.Write("type", type);

            r.request("/item?t=add_k3item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bool i = Convert.ToBoolean(r.Read("b"));
            return i;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0是销售 1是采购</param>
        /// <returns></returns>
        /// 
        public System.Data.DataTable ItemPrice(int type)
        {
            JsonRequest r = new JsonRequest();
            r.Write("type",type);

            r.request("/item?t=item_price");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }
        public System.Data.DataTable GetDataTable(string item_clsno, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            JsonRequest r = new JsonRequest();

            r.Write("item_clsno", item_clsno);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write("page_size", page_size);
            r.Write("page_index", page_index);

            r.request("/item?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            total_count = r.ReadToInt("total_count");
            return tb;
        }

        public Helper.Page<Model.bi_t_item_info> GetDataTable(string item_clsno, string keyword, int show_stop, Helper.Page<Model.bi_t_item_info> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("item_clsno", item_clsno);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write<bi_t_item_info>(page);

            r.request("/item?t=get_list");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page.Tb = r.GetDataTable();
            page.PageCount = r.ReadToInt("total_count");
            return page;
        }

        public Model.bi_t_item_info GetItem(string item_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("item_no", item_no);

            r.request("/item?t=get_item");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bi_t_item_info item = r.GetObject<bi_t_item_info>();

            return item;
        }

        public bi_t_item_info GetItemBySubno(string item_subno)
        {
            JsonRequest r = new JsonRequest();

            r.Write("item_subno", item_subno);

            r.request("/item?t=GetItemBySubno");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            bi_t_item_info item = r.GetObject<bi_t_item_info>();

            return item;
        }

        public string GetMaxCode()
        {
            JsonRequest r = new JsonRequest();

            r.request("/item?t=max_code");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            string max_code = r.Read("code");

            return max_code;
        }

        public void Add(Model.bi_t_item_info item)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_item_info>(item);

            r.request("/item?t=add");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Adds(List<bi_t_item_info> items)
        {
            JsonRequest r = new JsonRequest();
            r.Write("items", items);

            r.request("/item?t=Adds");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Update(Model.bi_t_item_info item)
        {
            JsonRequest r = new JsonRequest();

            r.Write<bi_t_item_info>(item);

            r.request("/item?t=change");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        public void Del(Model.bi_t_item_info item)
        {
            JsonRequest r = new JsonRequest();

            r.Write("item_no", item.item_no);

            r.request("/item?t=delete");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());
        }

        int IItem.IsUse(string item_no)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("item_no", item_no);
            IRequest req = new Request();
            var json = req.request("/item?t=is_use", w.ToString());
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(json);
            if (r.Read("errId") != "0")
            {
                throw new Exception(r.Read("errMsg"));
            }
            else
            {
                if (r.Read("value") == "1")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public DataTable GetDataTable_BySup(string sup_no, string keyword, int show_stop, int page_index, int page_size, out int total_count)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sup_no", sup_no);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write("page_size", page_size);
            r.Write("page_index", page_index);

            r.request("/item?t=get_list_by_sup");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            total_count = r.ReadToInt("total_count");
            return tb;
        }

        public Page<bi_t_item_info> GetDataTable_BySup(string sup_no, string keyword, int show_stop, Page<bi_t_item_info> page)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sup_no", sup_no);
            r.Write("show_stop", show_stop);
            r.Write("keyword", keyword);
            r.Write<bi_t_item_info>(page);

            r.request("/item?t=get_list_by_sup");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            page.Tb = r.GetDataTable();
            page.PageCount = r.ReadToInt("total_count");
            return page;
        }

        public Dictionary<string, decimal> GetItemStock(string branch_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("branch_no", branch_no);

            r.request("/item?t=GetItemStock");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            Dictionary<string, decimal> dic = r.GetDic<string, decimal>("dic");

            return dic;
        }
        public DataTable GetBranchItemList(string item_no, string branch_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("item_no", item_no);
            r.Write("branch_no", branch_no);

            r.request("/item?t=GetBranchItemList");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public DataTable GetSheetItem(string sup_id, int sup_type, string item_no, string item_subno, string barcode, string type)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sup_id", sup_id);
            r.Write("sup_type", sup_type);
            r.Write("item_no", item_no);
            r.Write("item_subno", item_subno);
            r.Write("barcode", barcode);
            r.Write("type", type);

            r.request("/item?t=GetSheetItem");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }


        public DataTable GetLikeItem(string sup_id, int sup_type, string item_subno, string type, string branch_no)
        {
            JsonRequest r = new JsonRequest();

            r.Write("sup_id", sup_id);
            r.Write("sup_type", sup_type);
            r.Write("item_subno", item_subno);
            r.Write("type", type);
            r.Write("branch_no", branch_no);

            r.request("/item?t=GetLikeItem");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public DataTable QuickItem(string item_subno)
        {
            JsonRequest r = new JsonRequest();
            r.Write("item_subno", item_subno);

            r.request("/item?t=QuickItem");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }


        public DataTable QuickSearchList(string keyword)
        {
            JsonRequest r = new JsonRequest();

            r.Write("keyword", keyword);

            r.request("/item?t=QuickSearchList");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public DataTable SearchAllItemList(string keyword)
        {
            JsonRequest r = new JsonRequest();

            r.Write("keyword", keyword);

            r.request("/item?t=SearchAllItemList");

            if (!r.ReadSuccess()) throw new Exception(r.ReadMessage());

            DataTable tb = r.GetDataTable();
            return tb;
        }

        public List<bi_t_item_info> GetALL()
        {
            JsonRequest r = new JsonRequest();

            r.request("/item?t=GetALL");

            r.WhetherSuccess();

            List<bi_t_item_info> lis = r.GetList<bi_t_item_info>("lis");

            return lis;
        }

        public DataTable GetThemeList()
        {
            JsonRequest r = new JsonRequest();

            r.request("/item?t=GetThemeList");

            r.WhetherSuccess();

            DataTable tb = r.GetDataTable();
            return tb;
        }
    }
}
