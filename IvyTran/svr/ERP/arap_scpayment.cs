using System;
using System.Collections.Generic;
using System.Data;
using Aop.Api.Domain;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using Model.BaseModel;
using Model.PaymentModel;

namespace IvyTran.svr.ERP
{
    public class arap_scpayment : BaseService
    {
        IARAP_SCPaymentBLL bll = new ARAP_SCPaymentBLL();
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
        public void get_supcust_beginbalance(WebHelper w, Dictionary<string, object> kv)
        {
            string region_no = w.Read("region_no");
            string keyword = w.Read("keyword");
            string is_cs = w.Read("is_cs");
            int page_index = w.ObjectToInt("page_index");
            int page_size = w.ObjectToInt("page_size");
            int total_count = 0;
            string is_jail = w.Read("is_jail");
            var tb = bll.GetSupcustBeginbalance(region_no, keyword,is_cs, is_jail, page_index, page_size, out total_count);
            w.WriteTableStruct("data", tb);
            w.Write("total_count", total_count.ToString());
        }
        public void get_supcust_beginbalance_list(WebHelper w, Dictionary<string, object> kv)
        {
            string region_no = w.Read("region_no");
            string keyword = w.Read("keyword");
            string is_cs = w.Read("is_cs");
            int page_index = w.ObjectToInt("page_index");
            int page_size = w.ObjectToInt("page_size");
            int total_count = 0;
            string is_jail = w.Read("is_jail");
            var tb = bll.GetSupcustList(region_no, keyword, is_cs, is_jail, page_index, page_size, out total_count);
            w.WriteTableStruct("data", tb);
            w.Write("total_count", total_count.ToString());
        }
        
        public void sava_supcust_initial(WebHelper w, Dictionary<string, object> kv)
        {
            string is_cs = w.Read("is_cs");
            string oper_id = w.Read("oper_id");
            List<ot_supcust_beginbalance> date1 = w.GetList<ot_supcust_beginbalance>("lr");
            bll.SavaSupcustInitial(date1,is_cs,oper_id);
        }

        public void CheckSupcustInitial(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_flag = w.Read("supcust_flag");
            bll.CheckSupcustInitial(supcust_flag);
        }
        public void NotCheckSupcustInitial(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_flag = w.Read("supcust_flag");
            bll.NotCheckSupcustInitial( supcust_flag);
        }

        public void GetAccountFlows(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_accout_payrec_flow flow = w.GetObject<Model.rp_t_accout_payrec_flow>("flow");
            DataTable tb = bll.GetAccountFlows(flow);
            w.Write(tb);
        }
        public void GetNoticeList(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime date1 = w.ObjectToDate("date1");
            DateTime date2 = w.ObjectToDate("date2");
            string cus_no = w.Read("cus_no");
            string is_cs = w.Read("is_cs");
            string sheet_no = w.Read("sheet_no");
            var tb = bll.GetNoticeList(date1, date2, cus_no, is_cs, sheet_no);
            w.Write("data", tb);
        }
        public void GetCollectionNotice(WebHelper w, Dictionary<string, object> kv)
        {
            List<rp_t_account_notice> lr = w.GetList <rp_t_account_notice>("lr");
            //List<DataRow> lr = w.GetObject<List<DataRow>>("lr");
            var tb = bll.GetCollectionNotice(lr);
            w.Write("data", tb);
        }
        public void get_supcust_list(WebHelper w, Dictionary<string, object> kv)
        {
            string is_cs = w.Read("is_cs");
            var tb = bll.GetSupcustList( is_cs);
            w.Write("data", tb);

        }
        public void get_supcust_info_list(WebHelper w, Dictionary<string, object> kv)
        {
            string is_cs = w.Read("is_cs");
            var tb = bll.GetSupcustInfoList(is_cs);
            w.WriteTableStruct("data", tb);

        }
        public void get_supcust_beginbalance_model(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_no = w.Read("supcust_no");
            var tb = bll.GetSupcustBeginbalanceModel(supcust_no);
            w.Write("data", tb);

        }
        public void add_notice(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_account_notice ord = w.GetObject<Model.rp_t_account_notice>("ord");
            //ord.approve_flag = "0";
            //ord.display_flag = "1";
            //ord.old_no = "";
            //ord.approve_date = DateTime.MinValue;
            //ord.max_change = 0m; //不确定
            //ord.lock_man = "";
            //ord.lock_date = DateTime.MinValue;

            List<rp_t_account_notice_detail> lines = w.GetList<rp_t_account_notice_detail>("lines");
            //foreach (var item in lines)
            //{
            //    item.cost_notax = 0m; //不确定
            //    item.ly_sup_no = "";
            //    item.ly_rate = 0m;
            //}
            string sheet_no = "";
            bll.AddNotice(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change_notice(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_account_notice ord = w.GetObject<Model.rp_t_account_notice>("ord");
            //ord.approve_flag = "0";
            //ord.display_flag = "1";
            //ord.old_no = "";
            //ord.approve_date = DateTime.MinValue;
            //ord.max_change = 0m; //不确定
            //ord.lock_man = "";
            //ord.lock_date = DateTime.MinValue;

            List<rp_t_account_notice_detail> lines = w.GetList<rp_t_account_notice_detail>("lines");
            //foreach (var item in lines)
            //{
            //    item.cost_notax = 0m; //不确定
            //    item.ly_sup_no = "";
            //    item.ly_rate = 0m;
            //}
            string sheet_no = "";
            bll.ChangeNotice(ord, lines);
            w.Write("sheet_no", sheet_no);
        }
        
        public void delete_notice(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteNotice(sheet_no, update_time);
        }
        public void GetArApList(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime date1 = w.ObjectToDate("date1");
            DateTime date2 = w.ObjectToDate("date2");
            string supcust_form = w.Read("supcust_form");
            string supcust_to = w.Read("supcust_to");
            string sheet_id= w.Read("sheet_id");
            var tb = bll.GetArApList(date1, date2, supcust_form, supcust_to, sheet_id);
            w.Write("data", tb);
        }
        public void GetArApOrder(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            bll.GetArApOrder(sheet_no, out tb1);
            w.Write("tb1", tb1);
        }
        public void AddArAp(WebHelper w, Dictionary<string, object> kv)
        {
            rp_t_arap_transformation ord = w.GetObject<rp_t_arap_transformation>("ord");

            //List<ot_account_notice_flow> lines = w.GetList<ot_account_notice_flow>("lines");

            string sheet_no = "";
            bll.AddArAp(ord, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void DeleteArAp(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteArAp(sheet_no, update_time);
        }
        public void CheckArAp(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.CheckArAp(sheet_no, approve_man, update_time);
        }
        public void NotCheckArAp(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.NotCheckArAp(sheet_no, approve_man, update_time);
        }
        public void ChangeArAp(WebHelper w, Dictionary<string, object> kv)
        {
            rp_t_arap_transformation ord = w.GetObject<rp_t_arap_transformation>("ord");


            //List<ot_account_notice_flow> lines = w.GetList<ot_account_notice_flow>("lines");

            string sheet_no = "";
            bll.ChangeArAp(ord, out sheet_no);
            w.Write("sheet_no", sheet_no);
        }
        public void delete_initial(WebHelper w, Dictionary<string, object> kv)
        {
            string keyword = w.Read("keyword");
            string is_cs = w.Read("is_cs");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteInitial(keyword, is_cs, update_time);
        }
        
        public void check_notice(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.CheckNotice(sheet_no, approve_man, update_time);
        }
        public void add_supcust_initial(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust = w.Read("supcust");
            string is_cs = w.Read("is_cs");
            string oper_id = w.Read("oper_id");
            string type = w.Read("type");
            bll.AddSupcustInitial(supcust, is_cs, oper_id, type);
        }
        public void get_payment_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetPaymentList();
            w.Write("data", tb);
        }
        public void get_payment_info(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var tb = bll.GetPaymentList(sheet_no);
            w.Write("data", tb);
        }
        
        public void get_recpay_record_model(WebHelper w, Dictionary<string, object> kv)
        {
            string is_cs = w.Read("is_cs");
            string supcust_no = w.Read("supcust_no");
            var tb = bll.GetRecpayRecordModel(supcust_no,is_cs);
            w.Write("data", tb);

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

        public void SaveAgingGroup(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = w.GetDataTable("dt");
             bll.SaveAgingGroup(dt);

        }
        public void GetAgingGroup(WebHelper w, Dictionary<string, object> kv)
        {
            string is_cs = w.Read("is_cs");
            DataTable tb=  bll.GetAgingGroup(is_cs);
            w.Write("data", tb);
        }
        
    }
}