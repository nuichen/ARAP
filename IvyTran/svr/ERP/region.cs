using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class region:BaseService
    {

        IRegion bll = new Region();
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
            string is_cs = w.Read("is_cs");
            var tb = bll.GetList(is_cs);
            w.Write("data", tb);
        }
        public void GetNodeList(WebHelper w, Dictionary<string, object> kv)
        {
            string is_cs = w.Read("is_cs");
            var tb = bll.GetNodeList(is_cs);
            w.Write("data", tb);
        }
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string region_no = w.Read("region_no");
            var tb = bll.GetItem(region_no);
            w.Write("data", tb);
        }
        public void max_code(WebHelper w, Dictionary<string, object> kv)
        {
            string code = bll.MaxCode();
            w.Write("code", code);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_region_info item = w.GetObject<Model.bi_t_region_info>();
            item.update_time = DateTime.Now;
            bll.Add(item);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_region_info item = w.GetObject<Model.bi_t_region_info>();
            item.update_time = DateTime.Now;
            bll.Change(item);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string region_no = w.Read("region_no");
            bll.Delete(region_no);
        }

    }
}