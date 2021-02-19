using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace b2bclient.BLL
{
    interface IOrder
    {
        void GetOrderNew(string clear_key, out string key);
        void GetOrderPass(string date1, string date2, string keyword, string clear_key, out string key);
        void GetOrderDisable(string date1, string date2, string keyword, string clear_key, out string key);
        void GetOrderComplete(string date1, string date2, string keyword, string only_show_no_receive, string clear_key, out string key);
        List<body.wm_order> GetOrderNew(string key, int pageSize, int pageIndex, out int total);
        List<body.wm_order> GetOrderPass(string key, int pageSize, int pageIndex, out int total);
        List<body.wm_order> GetOrderDisable(string key, int pageSize, int pageIndex, out int total);
        List<body.wm_order> GetOrderComplete(string key, int pageSize, int pageIndex, out int total);
        DataTable GetOrderNewDt(string key, int pageSize, int pageIndex, out int total);
        DataTable GetOrderPassDt(string key, int pageSize, int pageIndex, out int total);
        DataTable GetOrderDisableDt(string key, int pageSize, int pageIndex, out int total);
        DataTable GetOrderCompleteDt(string key, int pageSize, int pageIndex, out int total);
        void Pass(string ord_id);
        void Preview(string ord_id);
        void Disable(string ord_id);
        void DisableRow(string ord_id, string row_index);
        void Send(string ord_id);
        void GetOrder(string ord_id,out body.wm_order ord,out List<body.wm_order_item> lines,out int un_read_num);
        void GetOrder(string ord_id, out body.wm_order ord, out DataTable lines, out int un_read_num);
        List<string> GetNewOrderCode();
        bool GetFirstNewOrder(out body.wm_order ord, out List<body.wm_order_item> lines,out int un_read_num);
        bool GetFirstNewOrder(out body.wm_order ord, out DataTable lines, out int un_read_num);
        void SignRead(string ord_id);
    }
}
