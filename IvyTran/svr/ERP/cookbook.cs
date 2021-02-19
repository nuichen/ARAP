
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Aop.Api.Domain;
using IvyTran.BLL.ERP;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class cookbook : BaseService
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

        ICookbook bll = new CookbookBll();

        public void GetCookbooks(WebHelper w, Dictionary<string, object> kv)
        {
            if (!w.ExistsKeys("formulaName", "itemName", "type", "formulaClsNo"))
            {
                w.WriteInvalidParameters();
                return;
            }

            string formulaName = w.Read("formulaName");
            string itemName = w.Read("itemName");
            string type = w.Read("type");
            string formulaClsNo = w.Read("formulaClsNo");

            DataTable tb = bll.GetCookbooks(formulaName, itemName, type, formulaClsNo);

            w.Write(tb);
        }
        public void GetCookbookDetail(WebHelper w, Dictionary<string, object> kv)
        {
            if (!w.ExistsKeys("formulaNo"))
            {
                w.WriteInvalidParameters();
                return;
            }

            string formulaNo = w.Read("formulaNo");

            DataTable tb = bll.GetCookbookDetail(formulaNo);

            w.Write(tb);
        }
        public void SearchCookbook(WebHelper w, Dictionary<string, object> kv)
        {
            string keyword = w.Read("keyword");
            DataTable tb = bll.SearchCookbook(keyword);
            w.Write(tb);
        }

        public void GetCookbookDetails(WebHelper w, Dictionary<string, object> kv)
        {
            if (!w.ExistsKeys("formulaNo"))
            {
                w.WriteInvalidParameters();
                return;
            }

            string formulaNo = w.Read("formulaNo");

            bll.GetCookbookDetails(formulaNo, out bi_t_formula_main formulaMain, out DataTable formulaDetails);

            w.Write("formulaMain", formulaMain);
            w.Write("formulaDetails", formulaDetails);
        }
        public void SaveCookbook(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_formula_main formulaMain = w.GetObject<bi_t_formula_main>("formulaMain");
            List<bi_t_formula_detail> formulaDetails = w.GetList<bi_t_formula_detail>("formulaDetails");
            bll.SaveCookbook(formulaMain, formulaDetails);
        }
        public void ImportCookbook(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable tb = w.GetDataTable();
            bll.ImportCookbook(tb);
        }
        public void DelCookbook(WebHelper w, Dictionary<string, object> kv)
        {
            if (!w.ExistsKeys("formulaNos"))
            {
                w.WriteInvalidParameters();
                return;
            }

            string formulaNos = w.Read("formulaNos");
            bll.DelCookbook(formulaNos);
        }

        public void GetCookbookClss(WebHelper w, Dictionary<string, object> kv)
        {
            string formulaClsNo = w.Read("formulaClsNo");
            int length = w.ObjectToInt("length");
            DataTable tb = bll.GetCookbookClss(formulaClsNo, length);
            w.Write(tb);
        }
        public void GetCookbookClsLis(WebHelper w, Dictionary<string, object> kv)
        {
            string formulaClsNo = w.Read("formulaClsNo");
            int length = w.ObjectToInt("length");
            var lis = bll.GetCookbookClsLis(formulaClsNo, length);
            w.Write(lis);
        }
        public void GetCookbookCls(WebHelper w, Dictionary<string, object> kv)
        {
            string formulaClsNo = w.Read("formulaClsNo");
            var item = bll.GetCookbookCls(formulaClsNo);
            w.Write(item);
        }
        public void MaxCookbookClsCode(WebHelper w, Dictionary<string, object> kv)
        {
            string par_code = w.Read("par_code");
            string result = bll.MaxCookbookClsCode(par_code);
            w.WriteResult(result);
        }
        public void Add(WebHelper w, Dictionary<string, object> kv)
        {
            string parCode = w.Read("parCode");
            bi_t_formula_cls item = w.GetObject<bi_t_formula_cls>("item");
            item = bll.Add(parCode, item);
            w.Write(item);
        }
        public void Change(WebHelper w, Dictionary<string, object> kv)
        {
            bi_t_formula_cls item = w.GetObject<bi_t_formula_cls>("item");
            item = bll.Change(item);
            w.Write(item);
        }
        public void Delete(WebHelper w, Dictionary<string, object> kv)
        {
            string formula_clsno = w.Read("formula_clsno");
            bll.Delete(formula_clsno);
        }
    }
}