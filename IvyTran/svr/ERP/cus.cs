using System;
using System.Collections.Generic;
using System.Data;
using Aop.Api.Domain;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;
using Model.BaseModel;

namespace IvyTran.svr.ERP
{
    public class cus : BaseService
    {
        ICus bll = new Cus();
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
        public void send_type(WebHelper w, Dictionary<string, object> kv)
        {
            //string flag = w.Read("flag");
            var tb = bll.GetSendType();
            w.Write("data", tb);
        }
        public void supcust_type(WebHelper w, Dictionary<string, object> kv)
        {
            string flag = w.Read("flag");
            var tb = bll.SupCustType(flag);
            w.Write("data", tb);
        }
        public void update_custType(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = w.GetDataTable("dt");
            var tb = bll.UpdateCustType(dt);
            w.Write("b", tb);
        }
        public void cust_type(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_name = w.Read("sup_name");
            string flag = w.Read("flag");
            string type = w.Read("type");
            string region = w.Read("region");
            var tb = bll.CustType(sup_name, type, flag, region);
            w.Write("data", tb);
        }
        public void get_supcar(WebHelper w, Dictionary<string, object> kv)
        {
             string id = w.Read("id");
            var tb = bll.GetCarId(id);
            w.Write("data", tb);
        }
        public void get_car(WebHelper w, Dictionary<string, object> kv)
        {
           // string id = w.Read("id");
            var tb = bll.GetCar();
            w.Write("data", tb);
        }
        public void get_carid(WebHelper w, Dictionary<string, object> kv)
        {
            string id = w.Read("id");
            var tb = bll.GetCar(id);
            w.Write("data", tb);
        }
        public void add_car(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_supcust_car flag = w.GetObject<bi_t_supcust_car>("car");
            bll.AddCar(flag);
            //w.Write("data", tb);
        }
        public void change_car(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_supcust_car flag = w.GetObject<bi_t_supcust_car>("car");
            bll.ChangeCar(flag);
        }
        public void delete_car(WebHelper w, Dictionary<string, object> kv)
        {
            string car_id = w.Read("car_id");
            bll.DeleteCar(car_id);
        }
        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            string region_no = w.Read("region_no");
            string keyword = w.Read("keyword");
            int show_stop = w.ObjectToInt("show_stop");
            int page_index = w.ObjectToInt("page_index");
            int page_size = w.ObjectToInt("page_size");
            int total_count = 0;
            string is_jail = w.Read("is_jail");
            var tb = bll.GetList(region_no, keyword, is_jail, show_stop, page_index, page_size, out total_count);

            w.Write("data", tb);
            w.Write("total_count", total_count.ToString());
        }
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_no = w.Read("supcust_no");
            var tb = bll.GetItem(supcust_no);
            w.Write("data", tb);
        }
        public void max_code(WebHelper w, Dictionary<string, object> kv)
        {
            string code = bll.MaxCode();
            w.Write("code", code);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_supcust_info item = w.GetObject<bi_t_supcust_info>();
            bll.Add(item);
        }
        public void Adds(WebHelper w, Dictionary<string, object> kv)
        {
            List<bi_t_supcust_info> supcustInfos = w.GetList<bi_t_supcust_info>();
            bll.Adds(supcustInfos);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_supcust_info item = w.GetObject<bi_t_supcust_info>();
            bll.Change(item);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_no = w.Read("supcust_no");
            bll.Delete(supcust_no);
        }
        public void QuickSearchList(WebHelper w, Dictionary<string, object> kv)
        {
            string keyword = w.Read("keyword");
            DataTable tb = bll.QuickSearchList(keyword);
            w.Write(tb);
        }

        public void get_custitem_list(WebHelper w, Dictionary<string, object> kv)
        {
            string date1 = w.Read("date1");
            string date2 = w.Read("date2");
            string sup_no = w.Read("sup_no");

            
            var tb = bll.GetCustItemList(date1, date2, sup_no);
            w.Write("data", tb);
        }

        public void get_custitem(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            System.Data.DataTable tb1;
            System.Data.DataTable tb2;
            bll.GetCustItem(sheet_no, out tb1, out tb2);

            w.Write("tb1", tb1);
            w.Write("tb2", tb2);
        }
        public void add_custitem(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_cust_item_master ord = w.GetObject<Model.rp_t_cust_item_master>();
            ord.display_flag = "1";
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;

            List<Model.rp_t_cust_item_detail> lines = w.GetList<Model.rp_t_cust_item_detail>("lines");
            string sheet_no = "";
            bll.AddCustItem(ord, lines, out sheet_no);

            w.Write("sheet_no", sheet_no);
        }
        public void change_custitem(WebHelper w, Dictionary<string, object> kv)
        {
            Model.rp_t_cust_item_master ord = w.GetObject<Model.rp_t_cust_item_master>();
            ord.display_flag = "1";
            ord.approve_flag = "0";
            ord.approve_man = "";
            ord.approve_date = DateTime.MinValue;

            List<Model.rp_t_cust_item_detail> lines = w.GetList<Model.rp_t_cust_item_detail>("lines");
            bll.ChangeCustItem(ord, lines);
        }
        public void delete_custitem(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            bll.DeleteCustItem(sheet_no);
        }
        public void check_custitem(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            string approve_man = w.Read("approve_man");
            bll.CheckCustItem(sheet_no, approve_man);
        }
    }
}