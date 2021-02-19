using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface IOrder
    {
        DataTable GetPHSheets(DateTime startTime, DateTime endTime);
        DataTable GetDic1();
        DataTable GetDic();
        Dictionary<string, string> GetItem();
        Dictionary<string, string> GetItemDic(string item_nos);
        /// <summary>
        /// 下载出货单 -订单
        /// </summary>
        /// <returns></returns>
        bool GetOutPut(string sheet_no, out DataTable tbMain, out DataTable tbDetail);
        bool GetCoOrder(string sheet_no, out System.Data.DataTable tbMain, out System.Data.DataTable tbDetail);
        /// <summary>
        /// 获取子表 
        /// </summary>
        /// <param name="sheet_no"></param>
        /// <param name="tb"></param>
        /// <returns></returns>
        DataTable GetOrderDetail(string sheet_no);

        /// <summary>
        /// 获取货商协拣明细
        /// </summary>
        /// <param name="ph_sheet_no">要导出的批次号</param>
        /// <returns></returns>
        DataTable GetSupPickOrderDetail(string ph_sheet_no);

        /// <summary>
        /// 获取单据号
        /// 销售单主表
        /// </summary>
        /// <returns></returns>
        string GetSheetNo(DB.IDB db);

        /// <summary>
        /// 获取全部销售订子单
        /// </summary>
        /// <returns></returns>
        Dictionary<string, sm_t_salesheet_detail> GetAllSale(string sheet_no);

        /// <summary>
        /// 添加 销售单
        /// </summary>
        /// <param name="main">主表</param>
        /// <param name="details">子表</param>
        /// <returns></returns>
        void AddOrder(sm_t_salesheet main, List<sm_t_salesheet_detail> details);

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="item_no"></param>
        /// <returns></returns>
        bi_t_item_info GetItem(string item_no);

        /// <summary>
        /// 获取退货单
        /// 单据号
        /// </summary>
        /// <returns></returns>
        string GetReOrderNo(DB.IDB db);

        /// <summary>
        /// 添加退货单
        /// </summary>
        /// <param name="main">退货单主表</param>
        /// <param name="dels">子表</param>
        /// <returns></returns>
        void AddReOrder(ic_t_inout_store_master main, List<ic_t_inout_store_detail> dels);

        /// <summary>
        /// 获取采购单 
        /// 单据号
        /// </summary>
        /// <returns></returns>
        string GetColOrderNo(DB.IDB db);

        /// <summary>
        /// 添加采购单
        /// </summary>
        /// <param name="mian">主表</param>
        /// <param name="childs">子表</param>
        void AddColOrder(ic_t_inout_store_master main, List<ic_t_inout_store_detail> childs);

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="item_no">商品编号</param>
        /// <param name="item_cls">商品类型</param>
        /// <param name="item_name">商品名称</param>
        /// <param name="sup_no">供应商编号</param>
        /// <param name="sup_name">供应商名称</param>
        /// <returns></returns>
        DataTable GetItemTable(string item_no, string item_cls, string item_name, string sup_no, string sup_name);


        /// <summary>
        /// 获取采购出库 残次品
        /// 单据号
        /// </summary>
        /// <returns></returns>
        string GetBadOrderNo(DB.IDB db);

        /// <summary>
        /// 获取采购出库 残次品
        /// </summary>
        void bad_goods(ic_t_inout_store_master main, List<ic_t_inout_store_detail> details);

        #region 采购统单
        DataTable GetReqOrderList(string date1, string date2, string out_date);
        DataTable GetPHReqOrderList(string ph_sheet_no);
        DataTable GetPHOrderList(string date1, string date2);
        DataTable GetReqOrderDetail(string sheet_no);
        DataTable GetReqOrderStockDiff(string ph_sheet_no,string is_bulk);
        void CreateCGOrder(string ph_sheet_no, string op_type, DataTable item_dt, string is_min_stock, string oper_id);
        void CreatePHOrder(string sheet_nos, string oper_id, string flag);
        DataTable GetSupReqOrderDetail(string ph_sheet_no);
        DataTable GetClsReqOrderDetail(string ph_sheet_no);
        DataTable GetItemReqOrderSum(string ph_sheet_no);
        DataTable GetSupPickList(string ph_sheet_no, string sup_no, string item_no,DateTime startTime,DateTime endTime);
        #endregion

        #region 大宗统单

        DataTable GetBulikItems();
        void SaveBulkItems(List<bi_t_bulk_item> items);
        DataTable GetBulkOrderList(string date1, string date2);
        DataTable GetBulkReqOrderList(string cb_sheet_no);
        DataTable GetBulkReqOrderStockDiff(string cb_sheet_no);

        DataTable GetBulkSupReqOrderDetail(string ph_sheet_no);
        DataTable GetBulkClsReqOrderDetail(string ph_sheet_no);
        DataTable GetBulkItemReqOrderSum(string ph_sheet_no);
        #endregion

        #region 智能定价
        DataTable GetItemCls();
        DataTable GetLeftTable(string item_clsno);
        DataTable GetItemPrice(string item_no);
        DataTable GetPriceHistory(string item_name);
        DataTable GetItemList();
        DataTable GetItemList(List<string> item_nos);
        #endregion

        #region 快速开单—追加采购助手单 
        void AddCGOrder(List<Model.co_t_cg_order_detail> lines);
        #endregion


    }
}