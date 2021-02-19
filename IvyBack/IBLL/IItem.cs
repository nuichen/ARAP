using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using IvyBack.Helper;
using System.Data;
namespace IvyBack.IBLL
{
    public interface IItem
    {
        DataTable GetLikeItem_ChengBen(string sup_id, int sup_type, string item_subno, string type, string branch_no);
        DataTable GetSupItemList(string item_clsno, string item_subno);
        DataTable GetSupList(string sup_no);
        DataTable GetItemClsList();
        DataTable GetSupItemPrice();
        void UpdateSupItemPrice(List<bi_t_sup_item> bi);
        void CheckSupItemPrice(string item_no, string sup_no, string oper_id, DateTime time);
        bool Getk3Item(string item_no);
        bool k3Item(string item_no, string type);
        DataTable ItemPrice(int type);
        DataTable GetDataTable(string item_clsno, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        Page<bi_t_item_info> GetDataTable(string item_clsno, string keyword, int show_stop, Page<bi_t_item_info> page);
        bi_t_item_info GetItem(string item_no);
        bi_t_item_info GetItemBySubno(string item_subno);

        string GetMaxCode();

        void Add(bi_t_item_info item);
        void Adds(List<bi_t_item_info> items);
        void Update(bi_t_item_info item);
        void Del(bi_t_item_info item);
        int IsUse(string item_no);
        DataTable GetDataTable_BySup(string sup_no, string keyword, int show_stop, int page_index, int page_size, out int total_count);
        Page<bi_t_item_info> GetDataTable_BySup(string sup_no, string keyword, int show_stop, Page<bi_t_item_info> page);

        Dictionary<string, decimal> GetItemStock(string branch_no);
        DataTable GetItemStock_ChengBen(string branch_no);
        DataTable GetBranchItemList(string item_no, string branch_no);
        DataTable GetSheetItem(string sup_id, int sup_type, string item_no, string item_subno, string barcode, string type);
        DataTable GetLikeItem(string sup_id, int sup_type, string item_subno, string type, string branch_no);
        DataTable QuickItem(string item_subno);
        DataTable QuickSearchList(string keyword);

        DataTable SearchAllItemList(string keyword);

        List<bi_t_item_info> GetALL();
        DataTable GetThemeList();
    }
}
