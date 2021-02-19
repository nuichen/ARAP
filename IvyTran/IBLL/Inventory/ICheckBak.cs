using System.Collections.Generic;
 
using IvyTran.body.Inventory;

namespace IvyTran.IBLL.Inventory
{
    public interface ICheckBak
    {
        void Clear();
        void Insert(List<pda_ot_t_check_bak> lst);
        System.Data.DataTable GetList();
        decimal GetStock(string item_no);
    }
}