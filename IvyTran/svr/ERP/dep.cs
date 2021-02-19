using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class dep:BaseService
    {
        IDep bll = new Dep();
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
            var tb = bll.GetList();
            w.Write("data", tb);
        }
        public void get_listF(WebHelper w, Dictionary<string, object> kv)
        {
            string dep_no = w.Read("name");
            var tb = bll.GetListF(dep_no);
            w.Write("data", tb);
        }
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string dep_no = w.Read("dept_no");
            var tb = bll.GetItem(dep_no);
            w.Write("data", tb);
        }
        public void max_code(WebHelper w, Dictionary<string, object> kv)
        {
            string par_code = w.Read("par_code");
            string code = bll.MaxCode(par_code);
            w.Write("code", code);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_dept_info  item = w.GetObject<Model.bi_t_dept_info >();
            item.update_time = DateTime.Now;
            bll.Add(item);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_dept_info  item = w.GetObject<Model.bi_t_dept_info >();
            item.update_time = DateTime.Now;
            bll.Change(item);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string dep_no = w.Read("dept_no");
            bll.Delete(dep_no);
        }
    }
}