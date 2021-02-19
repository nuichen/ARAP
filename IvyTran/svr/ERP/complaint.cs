using IvyTran.Helper;
using Model.BaseModel;
using Model.FinanceModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IvyTran.svr.ERP
{
    public class complaint : BaseService
    {
        IBLL.ERP.IComplaintBLL bll = BLL.ERP.ComplaintBLL.GetComplaintBLL();
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
        public void AddComPlaint(WebHelper w, Dictionary<string, object> kv)
        {
            rp_t_complaint_flow model = w.GetObject<rp_t_complaint_flow>("com_flow");
            bll.AddComPlaint(model);
        }
        public void GetComPlaintInfo(WebHelper w, Dictionary<string, object> kv)
        {
            string type_id = w.Read("type_id");
            DataTable dt = new DataTable();
            bll.GetComPlaintInfo(type_id, out dt);
            w.Write("data", dt);
        }
        public void AddComPlaintType(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_complaint_type model = w.GetObject<bi_t_complaint_type>("com_type");
            bll.AddComPlaintType(model);
        }
        public void EditComPlaint(WebHelper w, Dictionary<string, object> kv)
        {
            rp_t_complaint_flow model = w.GetObject<rp_t_complaint_flow>("com_flow");
            bll.EditComPlaint(model);
        }
        public void GetComplaintType(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.GetComplaintType();
            w.Write("data",dt);
        }
        public void GetCustInfo(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.GetCustInfo();
            w.Write("data", dt);
        }
        public void GetPhs(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.GetPhs();
            w.Write("data", dt);
        }
        public void GetComplaintFlow(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.GetComplaintFlow();
            w.Write("data", dt);
        }
        public void DeleteComplaint(WebHelper w, Dictionary<string, object> kv)
        {
            string flow_id = w.Read("flow_id");
            bll.DeleteComplaint(flow_id);
        }
        public void GetTypeId(WebHelper w, Dictionary<string, object> kv)
        {
            int type_id = bll.GetTypeId();
            w.Write("type_id", type_id);
        }
    }
}