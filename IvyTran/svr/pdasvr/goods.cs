
using IvyTran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IvyTran.BLL.Inventory;
using IvyTran.body.Inventory;
using IvyTran.IBLL.Inventory;

namespace IvyTran.svr.pdasvr
{
    public class goods : BaseService
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
        IGoods bll = new Goods();
        public void clear_goods(WebHelper w, Dictionary<string, object> kv)
        {
            bll.ClearGoods();
        }
        public void insert(WebHelper w, Dictionary<string, object> kv)
        {
            List<pda_bi_t_item_info> lst = new List<pda_bi_t_item_info>();
            foreach (ReadWriteContext.IReadContext r in this.ReadContext.ReadList("data"))
            {
                pda_bi_t_item_info item = new pda_bi_t_item_info();
                item.item_no = r.Read("item_no");
                item.item_subno = r.Read("item_subno");
                item.item_name = r.Read("item_name");
                item.unit_no = r.Read("unit_no");
                item.item_size = r.Read("item_size");
                item.barcode = r.Read("barcode");
                decimal price = 0;
                decimal.TryParse(r.Read("price"), out price);
                item.price = price;
                decimal sale_price = 0;
                decimal.TryParse(r.Read("sale_price"), out sale_price);
                item.sale_price = sale_price;
                item.item_subname = r.Read("item_subname");
                item.item_flag = r.Read("item_flag");
                item.combine_sta = r.Read("combine_sta");
                lst.Add(item);
            }
            bll.Insert(lst);
        }
        public void get_list(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "keyword") == false)
            {
                throw new Exception("par err");
            }
            string keyword = ObjectToString(kv, "keyword");
            var tb = bll.GetList(keyword);

            w.Write("data", tb);
        }
        public void get_list_by_top(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "batch", "top") == false)
            {
                throw new Exception("par err");
            }

            string batch = ObjectToString(kv, "batch");
            int top = ObjectToInt(kv, "top");
            string batch2 = "";
            var tb = bll.GetListByTop(batch, top, out batch2);
            w.Write("batch", batch2);
            w.Write("data", tb);
        }
        public void test(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "val") == false)
            {
                throw new Exception("par err");
            }
            string val = ObjectToString(kv, "val");
            w.Write("val", val);
        }
        public void get_one(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "keyword") == false)
            {
                throw new Exception("par err");
            }
            string keyword = ObjectToString(kv, "keyword");
            var item = bll.GetOne(keyword);
            if (item == null)
            {
                throw new ExceptionBase("不存在商品");
            }
            else
            {
                w.Write("item_no", item.item_no);
                w.Write("item_subno", item.item_subno);
                w.Write("item_name", item.item_name);
                w.Write("unit_no", item.unit_no);
                w.Write("item_size", item.item_size);
                w.Write("barcode", item.barcode);
                w.Write("price", item.price.ToString());
                w.Write("sale_price", item.sale_price.ToString());
                w.Write("item_subname", item.item_subname);
                w.Write("item_flag", item.item_flag);
                w.Write("combine_sta", item.combine_sta);
                w.Write("add_qty", item.add_qty.ToString("0.###"));
                w.Write("master_no", item.master_no);

            }
        }
        public void get_barcode_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetBarCodeList();

            w.Write(tb);
        }
        public void get_pack_list(WebHelper w, Dictionary<string, object> kv)
        {
            var tb = bll.GetPackList();
            w.Write(tb);
        }

        public void clear_barcode(WebHelper w, Dictionary<string, object> kv)
        {
            bll.ClearBarcode();
        }

        public void clear_pack(WebHelper w, Dictionary<string, object> kv)
        {
            bll.ClearPack();
        }

        public void insert_barcode(WebHelper w, Dictionary<string, object> kv)
        {
            List<pda_bi_t_item_barcode> lst = new List<pda_bi_t_item_barcode>();
            foreach (ReadWriteContext.IReadContext r in this.ReadContext.ReadList("data"))
            {
                pda_bi_t_item_barcode item = new pda_bi_t_item_barcode();
                item.item_no = r.Read("item_no");
                item.barcode = r.Read("barcode");

                lst.Add(item);
            }
            bll.Insert(lst);
        }

        public void insert_pack(WebHelper w, Dictionary<string, object> kv)
        {
            List<pda_bi_t_item_pack_detail> lst = new List<pda_bi_t_item_pack_detail>();
            foreach (ReadWriteContext.IReadContext r in this.ReadContext.ReadList("data"))
            {
                pda_bi_t_item_pack_detail item = new pda_bi_t_item_pack_detail();
                item.master_no = r.Read("master_no");
                item.item_no = r.Read("item_no");
                item.pack_num = Conv.ToDecimal(r.Read("pack_num"));

                lst.Add(item);
            }
            bll.Insert(lst);
        }


    }
}