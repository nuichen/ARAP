using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    class pf_price : BaseService
    {
        IPFPrice bll = new PFPrice();
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
        public void retail_price(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_nos = w.Read("sheet_nos");
            string is_markup_rate = w.Read("is_markup_rate");
            if (ExistsKeys(kv, "import_tb"))
            {
                DataTable import_tb = w.GetDataTable("import_tb");
                bll.SetPrice(sheet_nos, import_tb, is_markup_rate);
            }
            else
            {
                bll.RetailPrice(sheet_nos, is_markup_rate);
            }

        }
        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            string cus_no = w.Read("cus_no");
            DateTime date1 = w.ObjectToDate("date1");
            DateTime date2 = w.ObjectToDate("date2");

            var tb = bll.GetUnApproveList(date1, date2, cus_no);
            w.Write("data", tb);
        }
        public void set_price(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_nos = w.Read("sheet_nos");
            string is_markup_rate = w.Read("is_markup_rate");
            if (ExistsKeys(kv, "import_tb"))
            {
                DataTable import_tb = w.GetDataTable("import_tb");
                bll.SetPrice(sheet_nos, import_tb, is_markup_rate);
            }
            else
            {
                bll.SetPrice(sheet_nos, is_markup_rate);
            }

        }


    }
}
