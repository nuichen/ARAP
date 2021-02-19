using System;
using System.Collections.Generic;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class weight : BaseService
    {
        IWeight bll = new Weight();
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

        //下载销售单单头
        public void get_bc_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req", "page_no", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }

            string last_req = ObjectToString(kv, "last_req");
            var tb = bll.GetCoOrderMain(last_req);
            w.Write("datas", tb);
            w.Write("req_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        //下载销售单单头_追加
        public void get_bc_list_new(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req", "page_no", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }

            string last_req = ObjectToString(kv, "last_req");
            var tb = bll.GetCoOrderMain(last_req);
            w.Write("datas", tb);
            w.Write("req_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        //下载销售订单明细
        public void get_task_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "bc_no", "jh", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }

            string valid_date = ObjectToString(kv, "bc_no");
            var tb = bll.GetCoOrderDetail(valid_date);
            w.Write("datas", tb);
        }

        //下载销售订单明细——追加
        public void get_task_list_new(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "jh", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }

            //  string valid_date = ObjectToString(kv, "bc_no");
            var tb = bll.GetCoOrderDetailNew();
            w.Write("datas", tb);
        }

        //下载系统参数
        public void get_params_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "jh") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }

            string jh = ObjectToString(kv, "jh");
            var tb = bll.GetSystemPars();
            w.Write("datas", tb);
        }

        //下载操作员信息
        public void get_oper_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }

            string oper_id = ObjectToString(kv, "oper_id");
            string last_req = ObjectToString(kv, "last_req");
            var tb = bll.GetOperList();
            w.Write("datas", tb);
            w.Write("req_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        //下载皮重信息
        public void get_piweight_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string last_req = ObjectToString(kv, "last_req");
            var tb = bll.GetPiWeightList();
            w.Write("datas", tb);
            w.Write("req_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        //下载商品品类
        public void get_item_cls_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string last_req = ObjectToString(kv, "last_req");
            string dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var tb = bll.GetItemClsList();
            w.Write("req_time", dtime);
            w.Write("datas", tb);
        }

        //下载商品列表
        public void get_item_info_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req", "page_no", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string last_req = ObjectToString(kv, "last_req");
            string dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var tb = bll.GetItemList(last_req);
            w.Write("req_time", dtime);
            w.Write("datas", tb);
        }

        //下载采配属性
        public void get_item_po_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req", "page_no", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string last_req = ObjectToString(kv, "last_req");
            string dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var tb = bll.GetItemPoList(last_req);
            w.Write("req_time", dtime);
            w.Write("datas", tb);
        }

        //下载客商档案
        public void get_sup_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "last_req", "oper_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string last_req = ObjectToString(kv, "last_req");
            string dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var tb = bll.GetSupCusList(last_req);
            w.Write("req_time", dtime);
            w.Write("datas", tb);
        }

        public void upload_weighting(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "jh", "oper_id", "datas") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            //  string last_req = ObjectToString(kv, "last_req");
            //  string dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            List<ot_weighing> lines = w.GetList<ot_weighing>("datas");

            bll.UploadWeighing(lines);
        }

        public void upload_check(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "jh", "oper_id", "datas") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            //  string last_req = ObjectToString(kv, "last_req");
            //  string dtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            List<ot_check_flow> lines = w.GetList<ot_check_flow>("datas");
            bll.UploadCheck(lines);
        }

        public void connect_server(WebHelper w, Dictionary<string, object> kv)
        {
            w.Write("is_connect", "1");
        }

        public void get_server_time(WebHelper w, Dictionary<string, object> kv)
        {
            w.Write("server_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }





    }
}