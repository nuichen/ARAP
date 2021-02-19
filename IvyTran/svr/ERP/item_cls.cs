using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class item_cls : BaseService
    {

        IItemCls bll = new ItemCls();
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
        public void get_item(WebHelper w, Dictionary<string, object> kv)
        {
            string item_clsno = w.Read("item_clsno");
            var tb = bll.GetItem(item_clsno);
            w.Write("data", tb);
        }
        public void GetListForMenu(WebHelper w, Dictionary<string, object> kv)
        {
            string is_first_level = w.Read("is_first_level");
            var tb = bll.GetListForMenu(is_first_level);
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
            Model.bi_t_item_cls item = w.GetObject<Model.bi_t_item_cls>();
            bll.Add(item);
        }
        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            Model.bi_t_item_cls item = w.GetObject<Model.bi_t_item_cls>();
            bll.Change(item);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            string item_clsno = w.Read("item_clsno");
            bll.Delete(item_clsno);
        }
        public void SaveGroupCls(WebHelper w, Dictionary<string, object> kv)
        {
            string group_no = w.Read("group_no");
            string item_clsnos = w.Read("item_clsnos");

            bll.SaveGroupCls(group_no, item_clsnos);
        }
        public void GetGroupCls(WebHelper w, Dictionary<string, object> kv)
        {
            string group_no = w.Read("group_no");

            string str = bll.GetGroupCls(group_no);
            w.WriteResult(str);
        }
    }
}