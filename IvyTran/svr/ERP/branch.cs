using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class branch : BaseService
    {
        IBranch bll = new Branch();
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
        public void get_branch(WebHelper w, Dictionary<string, object> kv)
        {
            
                string branch_no = w.Read("branch_no");
                string item_no = w.Read("item_no");
                int type = w.Read("type").ToInt32();
                string batch_no = w.Read("batch_no");
                int is_message = w.Read("is_message").ToInt32();
                var tb = bll.GetBranch(branch_no, batch_no, item_no, type, is_message);
                w.Write(tb);
           
        }
        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetList();
            w.Write(tb);
        }
        public void get_list_by_par_code(WebHelper w, Dictionary<string, object> kv)
        {
            string par_code = w.Read("par_code");
            var tb = bll.GetListByParCode(par_code);
            w.Write(tb);
        }
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            var tb = bll.GetItem(branch_no);
            w.Write(tb);
        }
        public void max_code(WebHelper w, Dictionary<string, object> kv)
        {
            string par_code = w.Read("par_code");
            string code = bll.MaxCode(par_code);
            w.Write("code", code);
        }
        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_branch_info item = w.GetObject<Model.bi_t_branch_info>();
            bll.Add(item);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_branch_info item = w.GetObject<Model.bi_t_branch_info>();
            bll.Change(item);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string branch_no = w.Read("branch_no");
            bll.Delete(branch_no);
        }

        public void QuickSearchList(WebHelper w, Dictionary<string, object> kv)
        {
            string keyword = w.Read("keyword");
            DataTable tb = bll.QuickSearchList(keyword);
            w.Write(tb);
        }

    }
}