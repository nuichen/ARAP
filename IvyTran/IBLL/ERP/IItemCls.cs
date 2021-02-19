using System.Data;

namespace IvyTran.IBLL.ERP
{
    interface IItemCls
    {
        System.Data.DataTable GetList();
        System.Data.DataTable GetItem(string item_clsno);
        DataTable GetListForMenu(string is_first_level);

        string MaxCode(string par_code);
        void Add(Model.bi_t_item_cls item);
        void Change(Model.bi_t_item_cls item);
        void Delete(string item_clsno);

        string GetGroupCls(string group_no);
        void SaveGroupCls(string group_no, string item_clsnos);
    }
}
