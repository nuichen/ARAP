
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IvyTran.BLL.Inventory;
using IvyTran.body.Inventory;
using IvyTran.IBLL.Inventory;

namespace IvyTran.svr.pdasvr
{
    public class check_bak : BaseService
    {

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
        ICheckBak ins = new CheckBakBll();
        public void clear(WebHelper w, Dictionary<string, object> kv)
        {
            ins.Clear();
        }
        public void insert(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "data") == false)
            {
                throw new Exception("par err");
            }

            List<pda_ot_t_check_bak> lst = w.GetList<pda_ot_t_check_bak>();

            ins.Insert(lst);
        }
        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = ins.GetList();
            w.Write(tb);
        }
        public void get_stock(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "item_no") == false)
            {
                throw new Exception("par err");
            }

            string item_no = ObjectToString(kv, "item_no");
            decimal stock = ins.GetStock(item_no);

            w.Write("stock", stock.ToString());
        }


    }
}