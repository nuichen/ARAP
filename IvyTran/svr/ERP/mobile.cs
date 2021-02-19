using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class mobile : BaseService
    {
        IMobileBLL bll = new MobileBLL();
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

        public void SearchMobileOperList(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_cls = w.Read("oper_cls");
            string func_id = w.Read("func_id");
            string keyword = w.Read("keyword");
            string is_show_stop = w.Read("is_show_stop");
            var tb = bll.SearchMobileOperList(oper_cls, func_id, keyword, is_show_stop);

            w.Write("data", tb);
        }
        public void GetFuncList(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetFuncList();
            w.Write("data", tb);
        }
        public void GetOperCls(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetOperCls();
            w.Write("data", tb);
        }
        public void GetSupcustList(WebHelper w, Dictionary<string, object> kv)
        {
            string supcust_flag = w.Read("supcust_flag");
            string keyword = w.Read("keyword");
            var tb = bll.GetSupcustList(supcust_flag, keyword);
            w.Write("data", tb);
        }
        public void GetItemCls(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetItemCls();
            w.Write("data", tb);
        }
        public void GetOperDataMain(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            var tb = bll.GetOperDataMain(oper_id);
            w.Write("data", tb);
        }

        public void GetOperDataDetail(WebHelper w, Dictionary<string, object> kv)
        {
            string flow_id = w.Read("flow_id");

            var tb = bll.GetOperDataDetail(flow_id);
            w.Write("data", tb);
        }

        public void SaveMobileOper(WebHelper w, Dictionary<string, object> kv)
        {
            Model.SysModel.sa_t_mobile_oper item = w.GetObject<Model.SysModel.sa_t_mobile_oper>();
            string oper_id = bll.SaveMobileOper( item);

            w.Write("oper_id", oper_id);
        }
        public void SaveMobileOperDataMain(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");

            List<Model.SysModel.sa_t_mobile_data_main> lines = w.GetList<Model.SysModel.sa_t_mobile_data_main>("lines");
            bll.SaveMobileOperDataMain(oper_id, lines);
        }
        public void SaveMobileOperDataDetail(WebHelper w, Dictionary<string, object> kv)
        {
            string flow_id = w.Read("flow_id");
            string item_clsnos = w.Read("item_clsnos");
            bll.SaveMobileOperDataDetail(flow_id, item_clsnos);
        }
        public void DeleteMobileOper(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            
            bll.DeleteMobileOper(oper_id);
        }
    }
}