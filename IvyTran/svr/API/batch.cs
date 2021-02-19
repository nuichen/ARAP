
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.svr.API
{
    public class batch : BaseService
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
        //IAssApiBLL bll = new AssApiBLL();
        public void get_branch_stock(WebHelper w, Dictionary<string, object> kv)
        {
            IBranch branch = new Branch();
            //System.Data.DataTable IBranch.GetBranchStock(string item_no, string batch_no)
            string item_no= w.Read("item_no");
            string batch_no= w.Read("batch_no");
            string errMsg = "";
            int errId = 0;
            w.Write("errId", errId.ToString());
            w.Write("errMsg", errMsg);
            w.Write("server_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            w.Write("data", branch.GetBranchStock(item_no,batch_no));
        }
    }
}