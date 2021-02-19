
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IvyTran.BLL.ERP;
using IvyTran.IBLL.ERP;
using Model;

namespace IvyTran.svr.ERP
{
    public class bom : BaseService
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

        IBom bll = new BomBll();

        public void GetProcessItem(WebHelper w, Dictionary<string, object> kv)
        {
            if (w.ExistsKeys("keyword"))
            {
                string keyword = w.Read("keyword");
                DataTable tb = bll.GetProcessItem(keyword);
                w.Write(tb);
            }
            else
            {
                DataTable tb = bll.GetProcessItem();
                w.Write(tb);
            }
          
        }

        public void GetItemBomDetails(WebHelper w, Dictionary<string, object> kv)
        {
            string bom_no = w.Read("bom_no");
            DataTable tb = bll.GetItemBomDetails(bom_no);
            w.Write(tb);
        }

        public void GetItemBom(WebHelper w, Dictionary<string, object> kv)
        {
            string bom_no = w.Read("bom_no");
            bll.GetItemBom(bom_no, out var item, out var details);

            w.Write("item", item);
            w.Write("details", details);
        }

        public void GetItemBomByItem(WebHelper w, Dictionary<string, object> kv)
        {
            string item_subno = w.Read("item_subno");

            bll.GetItemBomByItem(item_subno, out var item, out var details);

            w.Write("item", item);
            w.Write("details", details);
        }

        public void SaveItemBom(WebHelper w, Dictionary<string, object> kv)
        {
            string oper_id = w.Read("oper_id");
            bi_t_item_info item = w.GetObject<bi_t_item_info>("item");
            List<bi_t_bom_detail> details = w.GetList<bi_t_bom_detail>("details");
            bll.SaveItemBom(oper_id, item, details);
        }

        public void DelBoms(WebHelper w, Dictionary<string, object> kv)
        {
            string bomNos = w.Read("bomNos");
            bll.DelBoms(bomNos);
        }



    }
}