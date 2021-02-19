using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class cus_price_order : BaseService
    {
        ICusPriceOrder bll = new CusPriceOrder();
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

        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime date1 = w.ObjectToDate("date1");
            DateTime date2 = w.ObjectToDate("date2");

            var tb = bll.GetList(date1, date2);

            w.Write("data", tb);
        }
        public void get_order(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetOrder(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void max_code(WebHelper w, Dictionary<string, object> kv)
        {
            string code = bll.MaxCode();
            w.Write("code", code);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            Model.pm_t_flow_main ord = w.GetObject<Model.pm_t_flow_main>();
            ord.price_type = "1";
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = System.DateTime.MinValue;

            List<Model.pm_t_price_flow_detial> lines = w.GetList<Model.pm_t_price_flow_detial>("lines");
            foreach (Model.pm_t_price_flow_detial line in lines)
            {
                line.sheet_no = ord.sheet_no;
                line.price_type = ord.price_type;
                line.start_date = ord.start_date;
                line.start_time = ord.start_time;
                line.end_time = ord.end_time;
            }

            string sheet_no = "";
            bll.Add(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.pm_t_flow_main ord = w.GetObject<Model.pm_t_flow_main>();
            ord.price_type = "1";
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = System.DateTime.MinValue;

            List<Model.pm_t_price_flow_detial> lines = w.GetList<Model.pm_t_price_flow_detial>("lines");
            foreach (Model.pm_t_price_flow_detial line in lines)
            {
                line.sheet_no = ord.sheet_no;
                line.price_type = ord.price_type;
                line.start_date = ord.start_date;
                line.start_time = ord.start_time;
                line.end_time = ord.end_time;
            }

            bll.Change(ord, lines);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            bll.Delete(sheet_no);
        }
        public void check(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            bll.Check(sheet_no, approve_man);
        }

    }
}