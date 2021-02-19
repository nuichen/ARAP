using System.Collections.Generic;
using System.Data;
using DB;
using Model;

namespace IvyTran.IBLL.OnLine
{
    public interface IOrder
    {
        void GetOrderNew(out string key);
        void GetOrderPass(string date1, string date2, string keyword, out string key);
        void GetOrderDisable(string date1, string date2, string keyword, out string key);
        void GetOrderComplete(string date1, string date2, string keyword, string only_show_no_receice, out string key);
        void Pass(string ord_id, string oper_id);
        void Pass(tr_order tr_order, string oper_id, IDB d);
        void Preview(string ord_id);
        void Disable(string ord_id);
        void DisableRow(string ord_id, int row_index);
        void Send(string ord_id, string oper_id);
        void GetOrder(string ord_id, out Model.tr_order ord, out DataTable dtLine, out int un_read_num);
        List<Model.tr_order> GetNewMsg();
        void GetFirstNewOrder(out Model.tr_order ord, out DataTable dtLine, out int un_read_num);
        void SignRead(string ord_id);
    }
}