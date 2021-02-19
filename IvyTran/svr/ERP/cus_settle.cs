using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model.PaymentModel;

namespace IvyTran.svr.ERP
{
    public class cus_settle : BaseService
    {
        ICusSettle bll = new CusSettle();
        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }

        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                if (SessionHelper.oper_id == "")
                {
                    throw new Exception("登录超时");
                }
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
            string cus_no = w.Read("cus_no");
            string is_cs = w.Read("is_cs");
            var tb = bll.GetList(date1, date2, cus_no, is_cs);
            w.Write("data", tb);
        }
        public void GetFYList(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_no = w.Read("supcust_no");
            string supcust_flag = w.Read("supcust_flag");
            var tb = bll.GetFYList(supcust_no, supcust_flag);
            w.Write("data", tb);
        }
        public void get_order(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string is_cs = w.Read("is_cs");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetOrder(sheet_no, is_cs, out tb1, out tb2);
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
            //ord.supcust_flag = "C";
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
            DataTable dt= w.GetDataTable("dt");
            string is_cs = w.Read("is_cs");
            //List<rp_t_collection_way> lr = w.GetList<rp_t_collection_way>("dt");
            string sheet_no = "";
            bll.Add(ord, lines,dt,is_cs, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_recpay_record_info ord = w.GetObject<Model.rp_t_recpay_record_info>();
            //ord.supcust_flag = "C";
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
            DataTable dt = w.GetDataTable("dt");
            bll.Change(ord, lines,dt);
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
            string is_cs = w.Read("is_cs");
            bll.Check(sheet_no, approve_man,is_cs);
        }
        public void not_check(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            string is_cs = w.Read("is_cs");
            bll.NotCheck(sheet_no, approve_man, is_cs);
        }
        public void GetAccountFlows(WebHelper w, Dictionary<string, object> kv)
        {
             Model.rp_t_accout_payrec_flow flow = w.GetObject< Model.rp_t_accout_payrec_flow>("flow");
            DataTable tb = bll.GetAccountFlows(flow);
            w.Write(tb);
        }

    }
}