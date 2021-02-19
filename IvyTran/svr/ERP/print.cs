using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.ERP;
using IvyTran.Helper;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class print : BaseService
    {

        IPrint bll = new PrintBLL();
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

        public void GetAll(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style style = w.GetObject<sys_t_print_style>("style");
            DataTable tb = bll.GetAll(style);
            w.Write(tb);
        }
        public void Add(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style style = w.GetObject<sys_t_print_style>("style");
            bll.Add(style);
        }
        public void Update(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style style = w.GetObject<sys_t_print_style>("style");
            bll.Update(style);
        }
        public void Del(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style style = w.GetObject<sys_t_print_style>("style");
            bll.Del(style);
        }
        public void GetPrintStyleDefault(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style_default style = w.GetObject<sys_t_print_style_default>("style");
            DataTable tb = bll.GetPrintStyleDefault(style);
            w.Write(tb);
        }
        public void AddDefault(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style_default style = w.GetObject<sys_t_print_style_default>("style");
            bll.AddDefault(style);
        }
        public void UpdateDefault(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style_default style = w.GetObject<sys_t_print_style_default>("style");
            bll.UpdateDefault(style);
        }
        public void DelDefault(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style_default style = w.GetObject<sys_t_print_style_default>("style");
            bll.DelDefault(style);
        }
        public void GetPrintStyleData(WebHelper w, Dictionary<string, object> kv)
        {
            string style_data = w.Read("style_data");
            DataTable tb = bll.GetPrintStyleData(style_data);
            w.Write(tb);
        }
        public void UpdateStyleData(WebHelper w, Dictionary<string, object> kv)
        {
            List<sys_t_print_style_data> lis = w.GetList<sys_t_print_style_data>("lis");
            bll.UpdateStyleData(lis);
        }
        #region 客户打印样式配对

        public void GetStyleGroupInfo(WebHelper w, Dictionary<string, object> kv)
        {
            if (!kv.ContainsKey("type_name"))
            {
                throw new Exception("缺少参数type_name");
            }
            string type_name = w.Read("type_name");
            DataTable styleInfo = bll.GetStyleGroupInfo(type_name);

            w.Write("data", styleInfo);
        }

        public void GetStyleType(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable styleTypeInfo = bll.GetStyleTypeInfo();

            w.Write("data", styleTypeInfo);
        }

        public void JoinCustomerAndStyle(WebHelper w, Dictionary<string, object> kv)
        {
            sys_t_print_style_default style_default = w.GetObject<sys_t_print_style_default>("style");
            bll.JoinCustomerAndStyle(style_default);
        }

        public void GetStyleById(WebHelper w, Dictionary<string, object> kv)
        {
            if (!kv.ContainsKey("style_id"))
            {
                throw new Exception("缺少参数style_id");
            }

            var style = new sys_t_print_style
            {
                style_id = w.Read("style_id"),
            };

            var result = bll.GetStyleById(style);
            w.WriteObject(result);
        }

        #endregion
    }
}