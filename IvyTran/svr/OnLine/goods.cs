using System;
using System.Collections.Generic;
using System.Data;
using IvyTran.BLL.OnLine;
using IvyTran.Helper;
using IvyTran.IBLL.OnLine;
using Model;

namespace IvyTran.svr.OnLine
{
    public class goods : BaseService
    {
        IGoods bll = new Goods();

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
                var tb = bll.GetList();
                w.Write("data", tb);
            }
            else
            {
                int pageSize = ObjectToInt(kv, "pageSize");
                int pageIndex = ObjectToInt(kv, "pageIndex");
                var cls_no = ObjectToString(kv, "cls_no");
                //
                int total;
                var dt = bll.GetList(cls_no, pageSize, pageIndex, out total);

                w.Write("pageSize", pageSize.ToString());
                w.Write("pageIndex", pageIndex.ToString());
                w.Write("total", total.ToString());
                w.Write("datas", dt);
            }

        }
        public void get_server_time(WebHelper w, Dictionary<string, object> kv)
        {
            w.Write("server_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public void select_key(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "theme", "keyword", "cls_no", "is_no_show_mall", "pageSize", "pageIndex") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var pageSize = ObjectToInt(kv, "pageSize");
            var pageIndex = ObjectToInt(kv, "pageIndex");
            var cls_no = ObjectToString(kv, "cls_no");
            string keyword = ObjectToString(kv, "keyword");
            string theme = ObjectToString(kv, "theme");
            string is_no_show_mall = ObjectToString(kv, "is_no_show_mall");
            //
            int total;
            var dt = bll.SelectKeyword(cls_no, keyword, theme, is_no_show_mall, pageSize, pageIndex, out total);

            w.Write("total", total.ToString());
            w.Write("datas", dt);
        }

        public void add(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_no", "goods_name", "long_name", "cls_id", "small_img_url",
                    "large_img_url", "detail_img_url", "themes", "text", "is_show_mall", "datas") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            List<global::Model.goods_std> goods_stds = new List<global::Model.goods_std>();
            foreach (ReadWriteContext.IReadContext r in ReadContext.ReadList("datas"))
            {
                global::Model.goods_std goods_std = new global::Model.goods_std();
                goods_std.prices = r.Read("prices");
                goods_std.is_default = r.Read("is_default");
                float qty = Conv.ToFloat(r.Read("qty"));
                goods_std.qty = qty;
                goods_stds.Add(goods_std);
            }
            global::Model.goods goods = new global::Model.goods();
            goods.goods_no = ObjectToString(kv, "goods_no");
            goods.goods_name = ObjectToString(kv, "goods_name");
            goods.long_name = ObjectToString(kv, "long_name");
            goods.cls_id = ObjectToString(kv, "cls_id");
            goods.small_img_url = ObjectToString(kv, "small_img_url");
            goods.large_img_url = ObjectToString(kv, "large_img_url");
            goods.detail_img_url = ObjectToString(kv, "detail_img_url");
            goods.themes = ObjectToString(kv, "themes");
            goods.text = ObjectToString(kv, "text");
            goods.is_show_mall = ObjectToString(kv, "is_show_mall");

            //
            bll.Add(goods, goods_stds);

        }

        public void change(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_id", "goods_no", "goods_name", "long_name", "cls_id", "small_img_url",
                    "large_img_url", "detail_img_url", "themes", "text", "is_show_mall", "datas") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var goods_stds = new List<goods_std>();
            foreach (ReadWriteContext.IReadContext r in ReadContext.ReadList("datas"))
            {
                goods_std goods_std = new goods_std();
                goods_std.prices = r.Read("prices");
                goods_std.is_default = r.Read("is_default");
                float qty = 0;
                float.TryParse(r.Read("qty"), out qty);
                goods_std.qty = qty;
                goods_stds.Add(goods_std);
            }
            var goods = new global::Model.goods();
            goods.goods_id = ObjectToString(kv, "goods_id");
            goods.goods_no = ObjectToString(kv, "goods_no");
            goods.goods_name = ObjectToString(kv, "goods_name");
            goods.long_name = ObjectToString(kv, "long_name");
            goods.cls_id = ObjectToString(kv, "cls_id");
            goods.small_img_url = ObjectToString(kv, "small_img_url");
            goods.large_img_url = ObjectToString(kv, "large_img_url");
            goods.detail_img_url = ObjectToString(kv, "detail_img_url");
            goods.themes = ObjectToString(kv, "themes");
            goods.text = ObjectToString(kv, "text");
            goods.is_show_mall = ObjectToString(kv, "is_show_mall");
            bll.Change(goods, goods_stds);
        }

        public void change_stock(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_id", "stock_qty") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var goods = new global::Model.goods();
            goods.goods_id = ObjectToString(kv, "goods_id");
            goods.stock_qty = ObjectToInt(kv, "stock_qty");

            bll.ChangeStock(goods);
        }

        public void change_price_short(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_id", "prices") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var goods = new global::Model.goods();
            goods.goods_id = ObjectToString(kv, "goods_id");
            goods.prices = ObjectToString(kv, "prices");
            bll.ChangePriceShort(goods);
        }
        public void delete(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var goods_id = ObjectToString(kv, "goods_id");
            //
            bll.Delete(goods_id);
        }
        public void get_info(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var goods_id = ObjectToString(kv, "goods_id");
            //
            var goods = new global::Model.goods();
            var cls = new global::Model.goods_cls();
            DataTable dt = null;
            bll.SelectById(goods_id, out goods, out cls, out dt);
            string large = goods.large_img_url;
            string large_str = "";
            int large_id = 0;
            if (large != null && large.Equals("") == false)
            {
                foreach (var item in large.Split(',', '，'))
                {
                    if (large_id == 0)
                    {
                        large_str += (AppSetting.imgsvr + item);
                    }
                    else
                    {
                        large_str += ("," + AppSetting.imgsvr + item);
                    }
                    large_id++;
                }
            }
            string detail = goods.detail_img_url;
            string detail_str = "";
            int detail_id = 0;
            if (detail != null && detail.Equals("") == false)
            {
                foreach (var item in detail.Split(',', '，'))
                {
                    if (detail_id == 0)
                    {
                        detail_str += (AppSetting.imgsvr + item);
                    }
                    else
                    {
                        detail_str += ("," + AppSetting.imgsvr + item);
                    }
                    detail_id++;
                }
            }
            w.Write("goods_id", goods.goods_id);
            w.Write("goods_no", goods.goods_no);
            w.Write("goods_name", goods.goods_name);
            w.Write("long_name", goods.long_name);
            w.Write("cls_id", goods.cls_id);
            w.Write("cls_name", cls.cls_name);
            w.Write("small_img_url", goods.small_img_url);
            w.Write("small_img_full_url", AppSetting.imgsvr + goods.small_img_url);
            w.Write("large_img_url", large);
            w.Write("brand_id", goods.brand_id);
            w.Write("is_show_mall", goods.is_show_mall);
            w.Write("detail_img_full_url", detail_str);
            w.Write("detail_img_url", detail);
            w.Write("large_img_full_url", large_str);
            w.Write("themes", goods.themes);
            w.Write("text", goods.text);
            w.Write("status", goods.status);
            w.Write("datas", dt);
        }
        public void get_info_by_no(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_no") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var goods_no = ObjectToString(kv, "goods_no");
            //
            var goods = new global::Model.goods();
            var cls = new global::Model.goods_cls();
            DataTable dt = null;
            bll.SelectByNo(goods_no, out goods, out cls, out dt);
            var large_str = "";
            var large_id = 0;
            var large = goods.large_img_url;
            if (large != null && large.Equals("") == false)
            {
                foreach (var item in large.Split(',', '，'))
                {
                    if (large_id == 0)
                    {
                        large_str += (AppSetting.imgsvr + item);
                    }
                    else
                    {
                        large_str += ("," + AppSetting.imgsvr + item);
                    }
                    large_id++;
                }
            }
            var detail = goods.detail_img_url;
            var detail_str = "";
            var detail_id = 0;
            if (detail != null && detail.Equals("") == false)
            {
                foreach (var item in detail.Split(',', '，'))
                {
                    if (detail_id == 0)
                    {
                        detail_str += (AppSetting.imgsvr + item);
                    }
                    else
                    {
                        detail_str += ("," + AppSetting.imgsvr + item);
                    }
                    detail_id++;
                }
            }
            w.Write("goods_id", goods.goods_id.ToString());
            w.Write("goods_no", goods.goods_no);
            w.Write("goods_name", goods.goods_name);
            w.Write("long_name", goods.long_name);
            w.Write("cls_id", goods.cls_id);
            w.Write("cls_name", cls.cls_name);
            w.Write("small_img_url", goods.small_img_url);
            w.Write("small_img_full_url", AppSetting.imgsvr + goods.small_img_url);
            w.Write("large_img_url", large);
            w.Write("brand_id", goods.brand_id);
            w.Write("is_show_mall", goods.is_show_mall);
            w.Write("large_img_full_url", large_str);
            w.Write("detail_img_url", detail);
            w.Write("detail_img_full_url", detail_str);
            w.Write("themes", goods.themes);
            w.Write("text", goods.text);
            w.Write("status", goods.status);
            w.Write("datas", dt);
        }

        public void stop(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var goods_id = ObjectToString(kv, "goods_id");
            //
            bll.Stop(goods_id);
        }
        public void start(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_id") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            var goods_id = ObjectToString(kv, "goods_id");
            //
            bll.Start(goods_id);
        }

        public void input_spe(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "goods_id", "color", "size", "prices") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }
            global::Model.goods_std goods_stdp = new global::Model.goods_std();
            goods_stdp.goods_id = ObjectToString(kv, "goods_id");
            goods_stdp.prices = ObjectToString(kv, "prices");

            bll.InputSpe(goods_stdp);
        }

        public void get_theme_list(WebHelper w, Dictionary<string, object> kv)
        {
            DataTable dt = bll.GetThemeList();

            w.Write("datas", dt);
        }

        public void get_barcode_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetBarcodeList();
            w.Write("data", tb);
        }
        public void get_pack_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetPackList();
            w.Write("data", tb);
        }

        public void get_stock_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "branch_no") == false)
            {
                w.WriteInvalidParameters();
                return;
            }
            string branch_no = ObjectToString(kv, "branch_no");
            var tb = bll.GetStockList(branch_no);
            w.Write("data", tb);
        }

        public void get_stock(WebHelper w, Dictionary<string, object> kv)
        {
            var item_no = ObjectToString(kv, "keyword");
            var tb = bll.GetStockQty2(item_no);
            if (tb.Rows.Count < 1)
            {
                throw new Exception("商品不存在");
            }

            var stock_qty = bll.GetStockQty(tb.Rows[0]["item_no"].ToString());
            var sup_name = tb.Rows[0]["sup_name"].ToString();
            var item_subno = tb.Rows[0]["item_subno"].ToString();
            var item_name = tb.Rows[0]["item_name"].ToString();
            var unit_no = tb.Rows[0]["unit_no"].ToString();
            var item_size = tb.Rows[0]["item_size"].ToString();
            var barcode = tb.Rows[0]["barcode"].ToString();
            var price = tb.Rows[0]["price"].ToString();
            var sale_price = tb.Rows[0]["sale_price"].ToString();
            w.Write("stock_qty", stock_qty);
            w.Write("item_no", item_no);
            w.Write("sup_name", sup_name);
            w.Write("item_subno", item_subno);
            w.Write("item_name", item_name);
            w.Write("unit_no", unit_no);
            w.Write("item_size", item_size);
            w.Write("barcode", barcode);
            w.Write("sup_price", price);
            w.Write("sale_price", sale_price);
            w.Write("stock_qty2", stock_qty);
            w.Write("spec_price", price);

        }

    }
}