using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class finance:BaseService
    {
        IFinanceBLL bll = new FinanceBLL();
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

        public void get_sz_list(WebHelper w, Dictionary<string, object> kv)
        {
            string is_show_stop = w.Read("is_show_stop");
            var tb = bll.GetSZTypeList(is_show_stop);
            w.Write("data", tb);
        }
        public void get_sz_item(WebHelper w, Dictionary<string, object> kv)
        {
            string pay_way = w.Read("pay_way");
            var tb = bll.GetSZTypeItem(pay_way);
            w.Write("data", tb);
        }
        public void add_sz(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_sz_type item = w.GetObject<Model.bi_t_sz_type>();
            bll.InsertSZType(item);
        }
        public void change_sz(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_sz_type item = w.GetObject<Model.bi_t_sz_type>();
            bll.UpdateSZType(item);
        }
        public void delete_sz(WebHelper w, Dictionary<string, object> kv)
        {
            string pay_way = w.Read("pay_way");
            bll.DeleteSZType(pay_way);
        }

        public void get_income_list(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_no = w.Read("supcust_no");
            string month = w.Read("month");
            var tb = bll.GetIncomeRevenueList(supcust_no, month);
            w.Write("data", tb);
        }

        public void SaveIncomeRevenue(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_no = w.Read("supcust_no");
            string month = w.Read("month");
            string oper_id = w.Read("oper_id");
            List<rp_t_supcust_income_revenue> details = w.GetList<rp_t_supcust_income_revenue>("details");
            bll.SaveIncomeRevenue(supcust_no, details, oper_id, month);
        }
    }
}