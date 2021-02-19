
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.BLL.OnLine;
using IvyTran.IBLL.OnLine;

namespace IvyTran.svr.OnLine
{
    public class pager : BaseService
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
        IPager bll = new Pager();
        public void clear_data(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "key") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            string key = ObjectToString(kv, "key");
            //
            bll.ClearData(key);
        }

        public void get_data(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "key", "pageSize", "pageIndex") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var key = ObjectToString(kv, "key");
            int pageSize = ObjectToInt(kv, "pageSize");
            int pageIndex = ObjectToInt(kv, "pageIndex");
            //
            DataTable dt = new DataTable();
            int total = 0;
            bll.GetData(key, out dt, pageSize, pageIndex, out total);
            w.Write("total", total.ToString());
            w.Write("data", dt);
        }
        public void get_data_with_total(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "key", "pageSize", "pageIndex", "field", "fields") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var key = ObjectToString(kv, "key");
            var pageSize = ObjectToInt(kv, "pageSize");
            var pageIndex = ObjectToInt(kv, "pageIndex");
            var field = ObjectToString(kv, "field");
            var fields = ObjectToString(kv, "fields");
            //
            var dt = new DataTable();
            var total = 0;
            bll.GetDataWithTotal(key, out dt, pageSize, pageIndex, out total, field, fields);
            w.Write("total", total.ToString());
            w.Write("data", dt);
        }

    }
}