using System;
using System.Collections.Generic;
using IvyTran.BLL.OnLine;
using IvyTran.Helper;
using IvyTran.IBLL.OnLine;

namespace IvyTran.svr.OnLine
{
    public class ad : BaseService
    {
        IAd bll = new Ad();

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

        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "pageSize", "pageIndex") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            int total = 0;
            int pageSize = ObjectToInt(kv, "pageSize");
            int pageIndex = ObjectToInt(kv, "pageIndex");
            var dt = bll.GetList(pageSize, pageIndex, out total);

            w.Write("datas", dt);
            w.Write("total", total);
        }

        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ad_type", "ad_name", "title_img", "detail_img", "ad_text", "goods_ids") == false)
            {
                throw new ExceptionBase(-4, "参数错误!");
            }

            global::Model.ad ad = new global::Model.ad
            {
                ad_type = ObjectToInt(kv, "ad_type"),
                ad_name = ObjectToString(kv, "ad_name"),
                title_img = ObjectToString(kv, "title_img"),
                detail_img = ObjectToString(kv, "detail_img"),
                ad_text = ObjectToString(kv, "ad_text"),
                goods_ids = ObjectToString(kv, "goods_ids")
            };
            bll.Add(ad);

        }

        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ad_id", "ad_name", "title_img", "detail_img", "ad_text", "goods_ids") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }

            global::Model.ad ad = new global::Model.ad
            {
                ad_id = ObjectToString(kv, "ad_id"),
                ad_name = ObjectToString(kv, "ad_name"),
                title_img = ObjectToString(kv, "title_img"),
                detail_img = ObjectToString(kv, "detail_img"),
                ad_text = ObjectToString(kv, "ad_text"),
                goods_ids = ObjectToString(kv, "goods_ids")
            };
            bll.Change(ad);
        }

        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ad_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ad_id = ObjectToString(kv, "ad_id");
            bll.Delete(ad_id);
        }

        public void select(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ad_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误！");
            }
            string ad_id = ObjectToString(kv, "ad_id");
            var item = bll.SelectById(ad_id);

            w.Write("ad_id", item.ad_id);
            w.Write("ad_type", item.ad_type.ToString());
            w.Write("ad_text", item.ad_text);
            w.Write("ad_name", item.ad_name);
            w.Write("title_img", item.title_img);
            w.Write("detail_img", item.detail_img);
            w.Write("goods_ids", item.goods_ids);
        }

        public void add_member_ad(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "ad_href", "ad_img_url") == false)
            {
                throw new ExceptionBase(-4, "参数错误!");
            }
            var ad_href = ObjectToString(kv, "ad_href");
            var ad_img_url = ObjectToString(kv, "ad_img_url");
            bll.AddMemberAd(ad_href, ad_img_url);
        }


    }
}