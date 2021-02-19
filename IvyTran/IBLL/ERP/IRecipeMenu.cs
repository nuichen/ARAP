using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace IvyTran.IBLL.ERP
{
    public interface IRecipeMenu
    {
        DataTable GetRecipeMenus(DateTime startTime, DateTime endTime, string cust_no);
        DataTable GetRecipeMenusDetails(string sheet_no);
        void GetRecipeMenusDetails(DateTime time, string cust_no, out co_t_menu_order_main main, out DataTable details, out decimal income);
        Dictionary<DateTime, List<co_t_menu_order_detail>> GetCalendarRecipeMenus(DateTime time, string cust_no);
        void SaveRecipeMenu(co_t_menu_order_main main, List<co_t_menu_order_detail> details);
        void CopyRecipeMenu(List<DateTime> times, string sourceCustNo, string toCustNo, string oper_id);
        void DelRecipeMenus(string sheet_nos);

        void GeneratePlan(List<DateTime> times, string cust_no, string oper_id);
    }
}