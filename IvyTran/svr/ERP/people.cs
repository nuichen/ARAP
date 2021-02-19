using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class people : BaseService
    {

        IPeople bll = new People();
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
            string dep_no = w.Read("dep_no");
            string keyword = w.Read("keyword");
            int show_stop = Helper.Conv.ToInt32(w.Read("show_stop"));
            int page_index = Helper.Conv.ToInt32(w.Read("page_index"));
            int page_size = Helper.Conv.ToInt32(w.Read("page_size"));
            int total_count = 0;

            var tb = bll.GetList(dep_no, keyword, show_stop, page_index, page_size, out total_count);
            w.Write("data", tb);
            w.Write("total_count", total_count.ToString());
        }
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            var tb = bll.GetItem(oper_id);
            w.Write("data", tb);
        }
        public void max_code(WebHelper w, Dictionary<string, object> kv)
        {
            string code = bll.MaxCode();
            w.Write("code", code);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_people_info item = w.GetObject<Model.bi_t_people_info>();
            if (w.Read("birthday") != "")
            {
                item.birthday = Helper.Conv.ToDateTime(w.Read("birthday"));
            }
            if (w.Read("in_date") != "")
            {
                item.in_date = Helper.Conv.ToDateTime(w.Read("in_date"));
            }
            bll.Add(item);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_people_info item = w.GetObject<Model.bi_t_people_info>();
            if (w.Read("birthday") != "")
            {
                item.birthday = Helper.Conv.ToDateTime(w.Read("birthday"));
            }
            if (w.Read("in_date") != "")
            {
                item.in_date = Helper.Conv.ToDateTime(w.Read("in_date"));
            }
            bll.Change(item);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            bll.Delete(oper_id);
        }
        public void QuickSearchList(WebHelper w, Dictionary<string, object> kv)
        {
            string dept_no = w.Read("dept_no");
            string keyword = w.Read("keyword");
            DataTable tb = bll.QuickSearchList(dept_no, keyword);
            w.Write(tb);
        }

    }
}