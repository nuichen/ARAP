using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using Aop.Api.Domain;
using IvyTran;
using IvyTran.BLL.API;
using IvyTran.Helper;
using IvyTran.svr;

namespace IvyTran.svr.pdasvr
{
    public class ass_api : BaseService
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
        IAssApiBLL bll = new AssApiBLL();
        public void get_server_time(WebHelper w, Dictionary<string, object> kv)
        {

            string errMsg = "";
            int errId = 0;
            w.Write("errId", errId.ToString());
            w.Write("errMsg", errMsg);
            w.Write("server_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        public void connect_server(WebHelper w, Dictionary<string, object> kv)
        {

            string errMsg = "";
            int errId = 0;
            w.Write("errId", errId.ToString());
            w.Write("errMsg", errMsg);
            w.Write("is_connect", "1");
        }
        public void login(WebHelper w, Dictionary<string, object> kv)
        {

            
            if (ExistsKeys(kv, "oper_id", "pwd", "branch_no") == false)
            {
                throw new Exception("参数异常");
            }
            var oper_id = ObjectToString(kv, "oper_id");
            var branch_no = ObjectToString(kv, "branch_no");
            var pwd = ObjectToString(kv, "pwd").ToLower();
            if (pwd != "") pwd = IvyTran.Helper.sec.des(pwd);
            Model.sa_t_operator_i item = new Model.sa_t_operator_i();
            IOperBLL bll2 = new OperBLL();
            string errMsg = "";
            var res = bll2.Login(oper_id, pwd, out errMsg, out item);
            if (res)
            {
                w.Write("errId", "0");
                w.Write("errMsg", errMsg);
               w.Write("oper_id", item.oper_id);
               w.Write("oper_name", item.oper_name);
               w.Write("oper_type", item.oper_type);
               w.Write("can_see_price", "1");
            }
            else
            {
                //string errMsg = "";
                int errId = 0;
                w.Write("errId", errId.ToString());
                w.Write("errMsg", errMsg);
               // Write(context, WriteContext.ToString());
            }
        }
        public void get_item_info_list(WebHelper w, Dictionary<string, object> kv)
        {

            if (ExistsKeys(kv, "last_req", "page_no", "oper_id") == false)
            {
                throw new Exception("参数异常");
            }
            string oper_id = ObjectToString(kv, "oper_id");
            var last_req = ObjectToString(kv, "last_req");
            var page_no = ObjectToInt(kv, "page_no");
            int total = 0;
            var req_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var dt = bll.GetItemInfoList(last_req, page_no, out total);
            w.Write("req_time", req_time);
            w.Write("total", total.ToString());
            w.Write("datas", dt); 
            string errMsg = "";
            int errId = 0;
            w.Write("errId", errId.ToString());
            w.Write("errMsg", errMsg);
            //Write(context, WriteContext.ToString());
        }
        public void get_item_unit_list(WebHelper w, Dictionary<string, object> kv)
        {

            if (ExistsKeys(kv, "last_req", "page_no", "oper_id") == false)
            {
                throw new Exception("参数异常");
            }
           string oper_id = ObjectToString(kv, "oper_id");
           string last_req = ObjectToString(kv, "last_req");
           int page_no = ObjectToInt(kv, "page_no");
           int total = 0;
           string req_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
          DataTable  dt = bll.GetItemUnitList(last_req, page_no, out total);
          string errMsg = "";
          int errId = 0;
          w.Write("errId", errId.ToString());
          w.Write("errMsg", errMsg);
            w.Write("req_time", req_time);
          w.Write("total", total.ToString());
          w.Write("datas", dt);
        }
        public void get_item_barcode_list(WebHelper w, Dictionary<string, object> kv)
        {

            if (ExistsKeys(kv, "last_req", "page_no", "oper_id") == false)
            {
                throw new Exception("参数异常");
            }
          string  oper_id = ObjectToString(kv, "oper_id");
          string last_req = ObjectToString(kv, "last_req");
           int page_no = ObjectToInt(kv, "page_no");
           int total = 0;
          string  req_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
          DataTable  dt = bll.GetItemBarcodeList(last_req, page_no, out total);
          string errMsg = "";
          int errId = 0;
          w.Write("errId", errId.ToString());
          w.Write("errMsg", errMsg);
            w.Write("req_time", req_time);
            w.Write("total", total.ToString());
            w.Write("datas", dt);
        }
        public void get_item_cls_list(WebHelper w, Dictionary<string, object> kv)
        {

            if (ExistsKeys(kv, "last_req", "oper_id") == false)
            {
                throw new Exception("参数异常");
            }
           string oper_id = ObjectToString(kv, "oper_id");
           string last_req = ObjectToString(kv, "last_req");
           string req_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
           DataTable dt = bll.GetItemClsList(last_req);
           string errMsg = "";
           int errId = 0;
           w.Write("errId", errId.ToString());
           w.Write("errMsg", errMsg);
            w.Write("req_time", req_time);
           w.Write("datas", dt);
        }
        public void get_sup_list(WebHelper w, Dictionary<string, object> kv)
        {

            if (ExistsKeys(kv, "last_req", "page_no", "oper_id") == false)
            {
                throw new Exception("参数异常");
            }
          string  oper_id = ObjectToString(kv, "oper_id");
          string last_req = ObjectToString(kv, "last_req");
           int page_no = ObjectToInt(kv, "page_no");
          int  total = 0;
          string  req_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
          DataTable  dt = bll.GetSupList(last_req, page_no, out total);
          string errMsg = "";
          int errId = 0;
          w.Write("errId", errId.ToString());
          w.Write("errMsg", errMsg);
          w.Write("total", total);
            w.Write("req_time", req_time);
            w.Write("datas", dt);
        }
        public void get_stock(WebHelper w, Dictionary<string, object> kv)
        {

            if (ExistsKeys(kv, "branch_no", "keyword") == false)
            {
                throw new Exception("参数异常");
            }
            string branch_no = ObjectToString(kv, "branch_no");
            string keyword = ObjectToString(kv, "keyword");
            DataTable  dt = bll.GetItemStock(branch_no, keyword);
            string errMsg = "";
            int errId = 0;
            w.Write("errId", errId.ToString());
            w.Write("errMsg", errMsg);
            w.Write("datas", dt);
        }
    }
}