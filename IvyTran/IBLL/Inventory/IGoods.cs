using System.Collections.Generic;
using System.Data;
using IvyTran.body.Inventory;

namespace IvyTran.IBLL.Inventory
{
    public interface IGoods
    {
        void ClearGoods();
        void Insert(List<pda_bi_t_item_info> lstgoods);
        DataTable GetList(string keyword);
        DataTable GetListByTop(string batch, int top, out string batch2);
        pda_bi_t_item_info GetOne(string keyword);
        DataTable GetBarCodeList();
        DataTable GetPackList();
        void ClearBarcode();
        void Insert(List<pda_bi_t_item_barcode> lstbarcode);
        void ClearPack();
        void Insert(List<pda_bi_t_item_pack_detail> lstpack);
    }
}