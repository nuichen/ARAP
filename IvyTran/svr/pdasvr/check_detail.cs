
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
    public class check_detail : BaseService
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

        ICheckDetail cntBLL = new CheckDetail();
        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "date1", "date2", "sheet_no", "counter_no", "jh", "oper") == false)
            {
                throw new Exception("par err");
            }
            var date1 = ObjectToDate(kv, "date1");
            var date2 = ObjectToDate(kv, "date2");
            var sheet_no = ObjectToString(kv, "sheet_no");
            var counter_no = ObjectToString(kv, "counter_no");
            var jh = ObjectToString(kv, "jh");
            var oper = ObjectToString(kv, "oper");
            var tb = cntBLL.GetList(date1, date2, sheet_no, counter_no, jh, oper);

            w.Write(tb);
        }
        public void insert(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "data") == false)
            {
                throw new Exception("par err");
            }

            List<pda_ot_t_check_detail> lst = w.GetList<pda_ot_t_check_detail>();

            if (!cntBLL.Insert(lst))
            {
                throw new Exception("服务端写入盘点数据失败");
            }
        }
        public void get_qty_sum(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "item_no") == false)
            {
                throw new Exception("par err");
            }

            string item_no = ObjectToString(kv, "item_no");
            decimal qty = cntBLL.GetQtySum(item_no);

            w.Write("qty", qty);

        }
        public void clear(WebHelper w, Dictionary<string, object> kv)
        {
            cntBLL.Clear();
        }


    }
}