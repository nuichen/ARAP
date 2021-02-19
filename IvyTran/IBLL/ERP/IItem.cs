using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    interface IItem
    {
        DataTable GetLikeItem_ChengBen(string sup_id, int sup_type, string item_subno, string type, string branch_no);

        DataTable GetItemSupList(string item_clsno, string sup_no, string item_subno);
        DataTable GetItemClsList();
        DataTable GetSupList(string sup_no);
        DataTable GetSupItemList(string item_sub, string item_cls);
        DataTable GetSupItemPrice();
        void UpdateSupSupItemPrice(List<bi_t_sup_item> bi);
        bool addK3(string item_no, string type);
        bool GetK3(string item_no);
        DataTable ItemPrice(int type);
        System.Data.DataTable GetList(string item_clsno, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        System.Data.DataTable GetItem(string item_no);
        bi_t_item_info GetItemBySubno(string item_subno);
        string MaxCode();
        void Add(bi_t_item_info item);
        void Adds(List<bi_t_item_info> items);
        void Change(bi_t_item_info item);
        void Delete(string item_no);
        System.Data.DataTable GetListShort();
        int IsUse(string item_no);
        System.Data.DataTable GetList_BySup(string sup_no, string keyword, int show_stop, int page_index, int page_size, out int total_count);

        Dictionary<string, decimal> GetItemStock(string branch_no);
        DataTable GetBranchItemList(string item_no, string branch_no);
        DataTable GetSheetItem(string sup_id, int sup_type, string item_no, string item_subno, string barcode, string type);
        DataTable GetLikeItem(string sup_id, int sup_type, string item_subno, string type, string branch_no);
        DataTable QuickItem(string item_subno);
        DataTable QuickSearchList(string keyword);

        DataTable SearchAllItemList(string keyword);

        List<bi_t_item_info> GetALL();

        DataTable GetThemeList();
        DataTable GetItemStock_ChengBen(string branch_no);
    }
}
