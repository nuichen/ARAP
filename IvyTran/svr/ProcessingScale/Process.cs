
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.BLL.ProcessingScale;
using IvyTran.IBLL.ProcessingScale;
using Model;

namespace IvyTran.svr.ProcessingScale
{
    public class Process : BaseService
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
        IProcess bll = new ProcessBLL();

        public void get_params_list(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.GetParamsList();
            w.Write("datas",dt);
        }
        public void Get_Oper_List(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.GetOperList();
            w.Write("datas",dt);
        }
        public void Getbi_t_bom_master(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            string last_time = ObjectToString(kv, "last_req");
            DataTable dt = bll.Getbi_t_bom_master(last_time);
            w.Write("datas", dt);
        }

        public void Getic_t_pspc_main(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.Getic_t_pspc_main();
            w.Write("datas", dt);
        }

        public void Getbi_t_bom_detail(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.Getbi_t_bom_detail();
            w.Write("datas", dt);
        }

        public void Getbi_t_item_cls(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            string last_time = ObjectToString(kv, "last_req");
            DataTable dt = bll.Getbi_t_item_cls(last_time);
            w.Write("datas", dt);
        }

        public void Getbi_t_item_info(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            string last_time = ObjectToString(kv, "last_req");
            DataTable dt = bll.Getbi_t_item_info(last_time);
            w.Write("datas", dt);
        }

        public void Getot_processing_task(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ph_sheet_no") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            string ph_sheet_no = ObjectToString(kv, "ph_sheet_no");
            DataTable dt = bll.Getot_processing_task(ph_sheet_no);
            w.Write("datas", dt);
        }

        public void InsertProcess(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "dt") == false)
            {
                w.WriteInvalidParameters();
                return;
            }

            List<ot_processing> list = w.GetList<ot_processing>("dt");
            int fh = bll.InsertProcess(list);
            w.Write("datas", fh);
        }

        public void connect_server(WebHelper w, Dictionary<string, object> kv)
        {
            w.Write("is_connect", "1");
        }

        public void get_server_time(WebHelper w, Dictionary<string, object> kv)
        {
            w.Write("server_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public void get_cg_order_list(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = bll.Getco_t_order_main();
            w.Write("datas", tb);
        }

        public void get_cg_order_child(WebHelper w, Dictionary<string, object> kv)
        {
            string sheet_no = w.Read("sheet_no");
            DataTable tb = bll.Getco_t_order_child(sheet_no);
            w.Write("datas", tb);
        }

    }
}