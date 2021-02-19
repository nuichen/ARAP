using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IvyTran.IBLL.ERP
{
    public interface IModifyCSPriceBLL
    {
        void Delete(string sheet_no);
        void Check(string sheet_no, string approve_man);
        void AddModifyPrice(Model.co_t_price_order_main main, List<Model.co_t_price_order_child> child_lst, string cs,out string sheet_no);
        void Change(Model.co_t_price_order_main main, List<Model.co_t_price_order_child> child_lst,string cs);
        void GetOrder(string sheet_no, string cs, out DataTable main_dt, out DataTable child_dt);
        DataTable GetList(string cs,DateTime begin_time, DateTime end_time);
    }
}
