using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class item : BaseService
    {

        IItem bll = new Item();
        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }
        public void get_suplist(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_no = w.Read("sup_no");
            var tb = bll.GetSupList(sup_no);
            w.Write("tb", tb);
        }
        public void get_supitemlist(WebHelper w, Dictionary<string, object> kv)
        {
            string item_subno = w.Read("item_subno");
            string item_clsno = w.Read("item_clsno");
            var tb = bll.GetSupItemList(item_subno, item_clsno);
            w.Write("tb", tb);
        }
        public void get_supitemclslist(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetItemClsList();
            w.Write("tb", tb);
        }
        public void get_supitemprice(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetSupItemPrice();
            w.Write("tb", tb);
        }
        public void update_supitemprice(WebHelper w, Dictionary<string, object> kv)
        {
            List<bi_t_sup_item> list = w.GetList<bi_t_sup_item>("bi");
           
             bll.UpdateSupSupItemPrice(list);
        }
        public void check_supitemprice(WebHelper w, Dictionary<string, object> kv)
        {
            ICheckBLL check=new CheckBLL();
            string item_no = w.Read("item_no");
            string sup_no = w.Read("sup_no");
            string oper_id = w.Read("oper_id");
            string datetime = w.Read("datetime");
            check.CheckSupSupItemPrice(item_no,sup_no,oper_id,Convert.ToDateTime(datetime));
            
        }
        public void add_k3item(WebHelper w, Dictionary<string, object> kv)
        {
            string item_no = w.Read("item_no");
            string type = w.Read("type");
            var tb = bll.addK3(item_no,type);
            w.Write("b", tb);
        }
        public void get_k3item(WebHelper w, Dictionary<string, object> kv)
        {
            string item_no = w.Read("item_no");
            var tb = bll.GetK3(item_no);
            w.Write("b", tb);
        }
        public void item_price(WebHelper w, Dictionary<string, object> kv)
        {
            int type = Convert.ToInt32(w.Read("type"));
            var tb = bll.ItemPrice(type);
            w.Write("data", tb);
        }
        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            string item_clsno = w.Read("item_clsno");
            string keyword = w.Read("keyword");
            int show_stop = Helper.Conv.ToInt32(w.Read("show_stop"));
            int page_index = Helper.Conv.ToInt32(w.Read("page_index"));
            int page_size = Helper.Conv.ToInt32(w.Read("page_size"));
            int total_count = 0;

            var tb = bll.GetList(item_clsno, keyword, show_stop, page_index, page_size, out total_count);
            w.Write("data", tb);
            w.Write("total_count", total_count.ToString());
        }
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string item_no = w.Read("item_no");
            var tb = bll.GetItem(item_no);
            w.Write("data", tb);
        }
        public void max_code(WebHelper w, Dictionary<string, object> kv)
        {
            string code = bll.MaxCode();
            w.Write("code", code);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_item_info item = w.GetObject<bi_t_item_info>();
            bll.Add(item);
        }
        public void Adds(WebHelper w, Dictionary<string, object> kv)
        {
            List<bi_t_item_info> items = w.GetList<bi_t_item_info>("items");
            bll.Adds(items);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_item_info item = w.GetObject<bi_t_item_info>();
            bll.Change(item);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string item_no = w.Read("item_no");
            bll.Delete(item_no);
        }
        public void get_list_short(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetListShort();
            w.Write("datas", tb);
        }
        public void is_use(WebHelper w, Dictionary<string, object> kv)
        {
            string item_no = w.Read("item_no");
            var val = bll.IsUse(item_no);
            w.Write("value", val.ToString());
        }
        public void get_list_by_sup(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_no = w.Read("sup_no");
            string keyword = w.Read("keyword");
            int show_stop = Helper.Conv.ToInt32(w.Read("show_stop"));
            int page_index = Helper.Conv.ToInt32(w.Read("page_index"));
            int page_size = Helper.Conv.ToInt32(w.Read("page_size"));
            int total_count = 0;

            var tb = bll.GetList_BySup(sup_no, keyword, show_stop, page_index, page_size, out total_count);
            w.Write("data", tb);
            w.Write("total_count", total_count.ToString());
        }

        public void GetItemStock(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            Dictionary<string, decimal> dic = bll.GetItemStock(branch_no);

            w.Write<string, decimal>("dic", dic);
        }
        public void GetItemBySubno(WebHelper w, Dictionary<string, object> kv)
        {
            string item_subno = w.Read("item_subno");
            bi_t_item_info item_info = bll.GetItemBySubno(item_subno);
            if (item_info != null)
            {
                w.Write(item_info);
            }
        }
        public void GetBranchItemList(WebHelper w, Dictionary<string, object> kv)
        {
            string item_no = w.Read("item_no");
            string branch_no = w.Read("branch_no");
            var tb = bll.GetBranchItemList(item_no, branch_no);
            w.Write(tb);
        }
        public void GetSheetItem(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_id = w.Read("sup_id");
            int sup_type = w.ObjectToInt("sup_type");
            string item_no = w.Read("item_no");
            string item_subno = w.Read("item_subno");
            string barcode = w.Read("barcode");
            string type = w.Read("type");
            var tb = bll.GetSheetItem(sup_id, sup_type, item_no, item_subno, barcode, type);
            w.Write(tb);
        }
        public void GetLikeItem(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_id = w.Read("sup_id");
            int sup_type = w.ObjectToInt("sup_type");
            string item_subno = w.Read("item_subno");
            string type = w.Read("type");
            string branch_no = w.Read("branch_no");
            var tb = bll.GetLikeItem(sup_id, sup_type, item_subno, type, branch_no);
            w.Write(tb);
        }
        public void QuickItem(WebHelper w, Dictionary<string, object> kv)
        {
            string item_subno = w.Read("item_subno");
            var tb = bll.QuickItem(item_subno);
            w.Write(tb);
        }
        public void QuickSearchList(WebHelper w, Dictionary<string, object> kv)
        {
            string keyword = w.Read("keyword");
            var tb = bll.QuickSearchList(keyword);
            w.Write(tb);
        }

        public void SearchAllItemList(WebHelper w, Dictionary<string, object> kv)
        {
            string keyword = w.Read("keyword");
            var tb = bll.SearchAllItemList(keyword);
            w.Write(tb);
        }
        public void GetALL(WebHelper w, Dictionary<string, object> kv)
        {
            List<bi_t_item_info> lis = bll.GetALL();
            w.Write("lis", lis);
        }

        public void GetThemeList(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = bll.GetThemeList();
            w.Write(tb);
        }

        public void GetItemStock_ChengBen(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            DataTable dt = bll.GetItemStock_ChengBen(branch_no);

            w.Write(dt);
        }
        public void GetLikeItem_ChengBen(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_id = w.Read("sup_id");
            int sup_type = w.ObjectToInt("sup_type");
            string item_subno = w.Read("item_subno");
            string type = w.Read("type");
            string branch_no = w.Read("branch_no");
            var tb = bll.GetLikeItem_ChengBen(sup_id, sup_type, item_subno, type, branch_no);
            w.Write(tb);
        }
    }
}