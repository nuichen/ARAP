using System;
using System.Data;

namespace IvyTran.IBLL.ERP
{
    interface IPFPrice
    {
        void RetailPrice(string sheet_nos, string is_markup_rate);
        /// <summary>
        /// 获取未审核批销单列表
        /// </summary>
        /// <param name="cus_no">客户编码，可为空</param>
        /// <returns></returns>
        System.Data.DataTable GetUnApproveList(DateTime date1, DateTime date2, string cus_no);

        /// <summary>
        /// 同步采购入库单价格
        /// </summary>
        /// <param name="sheet_nos">批销单号（多个用分号隔开如：xxx;xxx）</param>
        void SetPrice(string sheet_nos, string is_markup_rate);
        void SetPrice(string sheet_nos, DataTable import_tb, string is_markup_rate);



    }
}
