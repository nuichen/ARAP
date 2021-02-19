using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;

namespace IvyTran.svr.ERP
{
    public class count_detail : BaseService
    {
        ICountDetail bll = new CountDetail();
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

        public void insert(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "branch_no") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            string branch_no = ObjectToString(kv, "branch_no");

            System.Data.DataTable tb = new System.Data.DataTable();
            tb.Columns.Add("flow_id");
            tb.Columns.Add("counter_no");
            tb.Columns.Add("item_no");
            tb.Columns.Add("input_code");
            tb.Columns.Add("qty");
            tb.Columns.Add("jh");
            tb.Columns.Add("oper_time");
            tb.Columns.Add("oper_man");
            if (ReadContext.Read("data").Length < 3)
            {
                throw new Exception("无数据");
            }
            foreach (ReadWriteContext.IReadContext r2 in ReadContext.ReadList("data"))
            {
                var row = tb.NewRow();
                tb.Rows.Add(row);

                row["flow_id"] = r2.Read("flow_id");
                row["counter_no"] = r2.Read("counter_no");
                row["item_no"] = r2.Read("item_no");
                row["input_code"] = r2.Read("input_code");
                row["qty"] = r2.Read("qty");
                row["jh"] = r2.Read("jh");
                row["oper_time"] = r2.Read("oper_time");
                row["oper_man"] = r2.Read("oper_man");
            }
            string sheet_no = "";
            if (!bll.Insert(branch_no, tb, out sheet_no))
            {
                throw new Exception("不存在单据");
            }
        }


    }
}