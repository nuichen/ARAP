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
    public class cashier : BaseService
    {
        ICashierBLL bll = new CashierBLL();
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
        public void GetBankCashBeginbalance(WebHelper w, Dictionary<string, object> kv)
        {
            string keyword = w.Read("keyword");
            int page_index = w.ObjectToInt("page_index");
            int page_size = w.ObjectToInt("page_size");
            int total_count = 0;
            string is_jail = w.Read("is_jail");
            var tb = bll.GetBankCashBeginbalance( keyword, is_jail, page_index, page_size, out total_count);
            w.WriteTableStruct("data", tb);
            w.Write("total_count", total_count.ToString());
        }
        public void GetBankList(WebHelper w, Dictionary<string, object> kv)
        {
            string keyword = w.Read("keyword");
            int page_index = w.ObjectToInt("page_index");
            int page_size = w.ObjectToInt("page_size");
            int total_count = 0;
            string is_jail = w.Read("is_jail");
            var tb = bll.GetBankList( keyword, is_jail, page_index, page_size, out total_count);
            w.WriteTableStruct("data", tb);
            w.Write("total_count", total_count.ToString());
        }
        
        public void SavaBankCashBeginbalance(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            List<rp_t_bank_beginbalance> date1 = w.GetList<rp_t_bank_beginbalance>("lr");
            bll.SavaBankCashBeginbalance(date1,oper_id);
        }

        public void DeleteBankCashBeginbalance(WebHelper w, Dictionary<string, object> kv)
        {
            string visa_id = w.Read("visa_id");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteBankCashBeginbalance(visa_id, update_time);
        }
        public void CheckBankCashBeginbalance(WebHelper w, Dictionary<string, object> kv)
        {
            bll.CheckBankCashBeginbalance();
        }
        public void NotCheckBankCashBeginbalance(WebHelper w, Dictionary<string, object> kv)
        {
            bll.NotCheckBankCashBeginbalance();
        }
        public void GetCollectionList(WebHelper w, Dictionary<string, object> kv)
        {
            string type = w.Read("type");
            string supcust_no = w.Read("supcust_no");
            string visa_id = w.Read("visa_id");
            string start_date = w.Read("start_date");
            string end_date = w.Read("end_date");
            var tb = bll.GetCollectionList(type, supcust_no, visa_id, start_date, end_date);
            w.Write("data", tb);

        }
        public void GetCollectionWayList(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var tb = bll.GetCollectionWayList(sheet_no);
            w.Write("data", tb);

        }

        public void AddCollectionPay(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_pay_info ord = w.GetObject<Model.rp_t_pay_info>("ord");          
            List<rp_t_pay_detail> lines = w.GetList<rp_t_pay_detail>("lines");         
            string sheet_no = "";
            bll.AddCollectionPay(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }

        public void ChangeCollectionPay(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_pay_info ord = w.GetObject<Model.rp_t_pay_info>("ord");
            //ord.approve_flag = "0";
            //ord.display_flag = "1";
            //ord.old_no = "";
            //ord.approve_date = DateTime.MinValue;
            //ord.max_change = 0m; //不确定
            //ord.lock_man = "";
            //ord.lock_date = DateTime.MinValue;

            List<rp_t_pay_detail> lines = w.GetList<rp_t_pay_detail>("lines");
            //foreach (var item in lines)
            //{
            //    item.cost_notax = 0m; //不确定
            //    item.ly_sup_no = "";
            //    item.ly_rate = 0m;
            //}
            string sheet_no = "";
            bll.ChangeCollectionPay(ord, lines,out sheet_no);
            w.Write("sheet_no", sheet_no);
        }

        public void DeleteCollectionPay(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            var update_time = w.ObjectToDate("update_time");
            bll.DeleteCollectionPay(sheet_no, update_time);
        }

        public void CheckCollectionPay(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.CheckCollectionPay(sheet_no, approve_man, update_time);
        }

        public void NotCheckCollectionPay(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            var update_time = w.ObjectToDate("update_time");
            bll.NotCheckCollectionPay(sheet_no, approve_man, update_time);
        }
        public void GetCollectionPayList(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime date1 = w.ObjectToDate("date1");
            DateTime date2 = w.ObjectToDate("date2");
            string is_cs = w.Read("is_cs");
            string visa_id = w.Read("visa_id");
            
            var tb = bll.GetCollectionPayList(date1, date2, is_cs, visa_id);
            w.Write("data", tb);
        }
        public void GetCollectionPayOrder(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string is_cs = w.Read("is_cs");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetCollectionPayOrder(sheet_no, is_cs, out tb1, out tb2);
            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }

        public void GetOtherList(WebHelper w, Dictionary<string, object> kv)
        {
            DateTime date1 = w.ObjectToDate("date1");
            DateTime date2 = w.ObjectToDate("date2");
            string is_cs = w.Read("is_cs");
            var tb = bll.GetOtherList(date1, date2, is_cs);
            w.Write("data", tb);
        }
        public void GetOtherOrder(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetOtherOrder(sheet_no, out tb1, out tb2);
            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void AddOther(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bank_t_cash_master ord = w.GetObject<Model.bank_t_cash_master>();
            ord.visa_in = "";
            ord.coin_no = "RMB";
            ord.coin_rate = 1;
            ord.cm_branch = "00";
            ord.approve_flag = "0";
            ord.approve_man = "";
            //ord.approve_date = System.DateTime.MinValue;

            List<Model.bank_t_cash_detail> lines = w.GetList<Model.bank_t_cash_detail>("lines");
            foreach (Model.bank_t_cash_detail line in lines)
            {
                line.sheet_no = ord.sheet_no;
            }
            string sheet_no = "";
            bll.AddOther(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void ChangeOther(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bank_t_cash_master ord = w.GetObject<Model.bank_t_cash_master>();
            ord.visa_in = "";
            ord.coin_no = "RMB";
            ord.coin_rate = 1;
            ord.cm_branch = "00";
            ord.approve_flag = "0";
            ord.approve_man = "";
            //ord.approve_date = System.DateTime.MinValue;

            List<Model.bank_t_cash_detail> lines = w.GetList<Model.bank_t_cash_detail>("lines");
            foreach (Model.bank_t_cash_detail line in lines)
            {
                line.sheet_no = ord.sheet_no;
            }
            bll.ChangeOther(ord, lines);
        }
        public void DeleteOther(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            bll.DeleteOther(sheet_no);
        }
        public void CheckOther(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            bll.CheckOther(sheet_no, approve_man);
        }
        public void NotCheckOther(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            bll.NotCheckOther(sheet_no, approve_man);
        }
       

















    }
}