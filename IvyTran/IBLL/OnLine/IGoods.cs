using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.OnLine
{
    public interface IGoods
    {
        DataTable GetList(string cls_no, int pageSize, int pageIndex, out int total);
        DataTable SelectKeyword(string cls_no, string keyword, string theme, string is_no_show_mall, int pageSize, int pageIndex, out int total);
        void Add(goods goods, List<goods_std> goods_stds);
        void SelectById(string goods_id, out goods goods, out goods_cls cls, out DataTable dt);
        void SelectByNo(string goods_no, out goods goods, out goods_cls cls, out DataTable dt);
        void Change(goods goods, List<goods_std> goods_stds);
        void ChangePriceShort(goods goods);
        void ChangeStock(goods goods);
        void Delete(string goods_id);
        void Stop(string goods_id);
        void Start(string goods_id);
        void InputSpe(goods_std goods_std);
        DataTable GetThemeList();


        string GetStockQty1(string item_name);
        DataTable GetStockQty2(string item_no);
        //  string GetStockQty(string item_no);
        string GetStockQty(string item_no);
        bool GetStock(string branch_no, string keyword, out bi_t_item_info goods, out decimal stock_qty, out decimal sale_price, out decimal spec_price);
        System.Data.DataTable GetList();
        System.Data.DataTable GetBarcodeList();
        System.Data.DataTable GetPackList();
        System.Data.DataTable GetStockList(string branch_no);
        bi_t_item_info GetOne(string item_no);

    }
}