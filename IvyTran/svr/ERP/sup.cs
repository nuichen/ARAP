using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class sup : BaseService
    {

        ISup bll = new Sup();
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
            string region_no = w.Read("region_no");
            string keyword = w.Read("keyword");
            int show_stop = Helper.Conv.ToInt32(w.Read("show_stop"));
            int page_index = Helper.Conv.ToInt32(w.Read("page_index"));
            int page_size = Helper.Conv.ToInt32(w.Read("page_size"));
            int total_count = 0;

            var tb = bll.GetList(region_no, keyword, show_stop, page_index, page_size, out total_count);
            w.Write("data", tb);
            w.Write("total_count", total_count.ToString());
        }
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_no = w.Read("supcust_no");
            var tb = bll.GetItem(supcust_no);
            w.Write("data", tb);
        }
        public void get_factory(WebHelper w, Dictionary<string, object> kv)
        {
            //string supcust_no = w.Read("supcust_no");
            var tb = bll.GetCJ();
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
        public void GetALL(WebHelper w, Dictionary<string, object> kv)
        {
            List<bi_t_supcust_info> lis = bll.GetALL();
            w.Write("lis", lis);
        }

        public void GetSupBindItem(WebHelper w, Dictionary<string, object> kv)
        {
            string sup_no = w.Read("sup_no");
            DataTable tb = bll.GetSupBindItem(sup_no);
            w.Write(tb);
        }
        public void SaveSupBindItem(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_supcust_bind_item bind_item = w.GetObject<bi_t_supcust_bind_item>("bind_item");
            bll.SaveSupBindItem(bind_item);
        }
    }
}