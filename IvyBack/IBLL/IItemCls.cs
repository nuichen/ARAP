using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
namespace IvyBack.IBLL
{
    public interface IItemCls
    {
        DataTable GetDataTable();
        List<bi_t_item_cls> GetAllList();
        bi_t_item_cls GetItemCls(bi_t_item_cls itemcls);
        DataTable GetListForMenu(string is_first_level="1");
        void Add(bi_t_item_cls itemcls);
        void Del(bi_t_item_cls itemcls);
        void Upload(bi_t_item_cls itemcls);
        string GetMaxCode(string code);

        string GetGroupCls(string group_no);
        void SaveGroupCls(string group_no, string item_clsnos);
    }
}
