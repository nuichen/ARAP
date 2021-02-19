using IvyTran.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IvyTran.svr.ERP
{
    public class modify_price: BaseService
    {
        IBLL.ERP.IModifyCSPriceBLL bll = BLL.ERP.ModifyCSPriceBLL.GetModifyCSPriceBLL();
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
        public void Delete(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            bll.Delete(sheet_no);
        }
        public void Check(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            //string cs = w.Read("cs");
            string approve_man = w.Read("approve_man");
            bll.Check(sheet_no, /*cs,*/ approve_man);
        }
        public void GetList(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime begin_time = w.ObjectToDate("begin_time");
            DateTime end_time = w.ObjectToDate("end_time");
            string cs = w.Read("cs");
            DataTable main_dt = bll.GetList(cs, begin_time, end_time);
            w.Write("data", main_dt);
        }
        public void GetOrder(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string cs = w.Read("cs");
            System.Data.DataTable main_dt;
            System.Data.DataTable child_dt;
            bll.GetOrder(sheet_no, cs, out main_dt, out child_dt);
            w.Write("main_dt", main_dt);
            w.Write("child_dt", child_dt);
        }
        public void AddModifyPrice(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = "";
            co_t_price_order_main main = w.GetObject<co_t_price_order_main>("main");
            List<co_t_price_order_child> child_lst = w.GetList<co_t_price_order_child>("tb");
            string cs = w.Read("cs");
            bll.AddModifyPrice(main, child_lst, cs, out sheet_no);
            w.Write("sheet_no", sheet_no);
        }
        public void Change(WebHelper w, Dictionary<string, object> kv)
        {
            co_t_price_order_main main = w.GetObject<co_t_price_order_main>("main");
            List<co_t_price_order_child> child_lst = w.GetList<co_t_price_order_child>("tb");
            string cs = w.Read("cs");
            bll.Change(main, child_lst, cs);
        }
    }
}