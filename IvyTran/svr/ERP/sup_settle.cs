using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class sup_settle : BaseService
    {

        ISupSettle bll = new SupSettle();
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
            DateTime date1 = Helper.Conv.ToDateTime(w.Read("date1"));
            DateTime date2 = Helper.Conv.ToDateTime(w.Read("date2"));
            string sup_no = w.Read("sup_no");
            var tb = bll.GetList(date1, date2, sup_no);
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
            Model.rp_t_recpay_record_info ord = w.GetObject<Model.rp_t_recpay_record_info>();
            ord.supcust_flag = "S";
            ord.flag_post = "1";
            ord.coin_no = "RMB";
            ord.coin_rate = 1;
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = System.DateTime.MinValue;
            ord.cm_branch = "00";
            ord.from_date = System.DateTime.MinValue;
            ord.to_date = System.DateTime.MinValue;

            List<Model.rp_t_recpay_record_detail> lines = w.GetList<Model.rp_t_recpay_record_detail>("lines");
            foreach (Model.rp_t_recpay_record_detail line in lines)
            {
                line.sheet_no = ord.sheet_no;
            }
            string sheet_no = "";
            bll.Add(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_recpay_record_info ord = w.GetObject<Model.rp_t_recpay_record_info>();
            ord.supcust_flag = "S";
            ord.flag_post = "1";
            ord.coin_no = "RMB";
            ord.coin_rate = 1;
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = System.DateTime.MinValue;
            ord.cm_branch = "00";
            ord.from_date = System.DateTime.MinValue;
            ord.to_date = System.DateTime.MinValue;

            List<Model.rp_t_recpay_record_detail> lines = w.GetList<Model.rp_t_recpay_record_detail>("lines");
            foreach (Model.rp_t_recpay_record_detail line in lines)
            {
                line.sheet_no = ord.sheet_no;
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