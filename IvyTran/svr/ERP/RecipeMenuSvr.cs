
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.BLL.ERP;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class RecipeMenuSvr : BaseService
    {

        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }

        IRecipeMenu bll = new RecipeMenuBll();

        public void GetRecipeMenus(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime startTime = w.ObjectToDate("startTime");
            DateTime endTime = w.ObjectToDate("endTime");
            string cust_no = w.Read("cust_no");
            DataTable tb = bll.GetRecipeMenus(startTime, endTime, cust_no);
            w.Write(tb);
        }

        public void GetRecipeMenusDetails(WebHelper w, Dictionary<string, object> kv)
        {
            if (w.ExistsKeys("sheet_no"))
            {
                string sheet_no = w.Read("sheet_no");
                DataTable tb = bll.GetRecipeMenusDetails(sheet_no);
                w.Write(tb);
            }
            else if (w.ExistsKeys("time", "cust_no"))
            {
                string cust_no = w.Read("cust_no");
                DateTime time = w.ObjectToDate("time");
                decimal income = 0;
                bll.GetRecipeMenusDetails(time, cust_no, out var main, out var details, out income);

                w.Write("main", main);
                w.WriteTableStruct("details", details);
                w.Write("income", income.ToString());
            }
            else
            {
                w.WriteInvalidParameters();
            }

        }

        public void GetCalendarRecipeMenus(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime time = w.ObjectToDate("time");
            string cust_no = w.Read("cust_no");
            var dic = bll.GetCalendarRecipeMenus(time, cust_no);

            w.Write(dic);
        }

        public void SaveRecipeMenu(WebHelper w, Dictionary<string, object> kv)
        {
            co_t_menu_order_main main = w.GetObject<co_t_menu_order_main>("main");
            List<co_t_menu_order_detail> details = w.GetList<co_t_menu_order_detail>("details");
            bll.SaveRecipeMenu(main, details);
        }
        public void CopyRecipeMenu(WebHelper w, Dictionary<string, object> kv)
        {
            List<DateTime> times = w.GetList<DateTime>("times");
            string sourceCustNo = w.Read("sourceCustNo");
            string toCustNo= w.Read("toCustNo");
            string oper_id= w.Read("oper_id");
            bll.CopyRecipeMenu(times, sourceCustNo, toCustNo, oper_id);
        }

        public void DelRecipeMenus(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_nos = w.Read("sheet_nos");
            bll.DelRecipeMenus(sheet_nos);
        }

        public void GeneratePlan(WebHelper w, Dictionary<string, object> kv)
        {
            List<DateTime> times = w.GetList<DateTime>("times");
            string cust_no = w.Read("cust_no");
            string oper_id = w.Read("oper_id");
            bll.GeneratePlan(times, cust_no, oper_id);
        }
    }
}