using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class goods
    {
        public string goods_id { get; set; }
        public string goods_no { get; set; }
        public string goods_name { get; set; }
        public string long_name { get; set; }
        public string cls_id { get; set; }
        public string cls_name { get; set; }
        public string unit_no { get; set; }
        public string small_img_url { get; set; }
        public string large_img_url { get; set; }
        public string detail_img_url { get; set; }
        public string themes { get; set; }
        public string status { get; set; }
        public string text { get; set; }
        public Decimal stock_qty { get; set; }
        public string brand_id { get; set; }
        public string is_show_mall { get; set; }
        public string barcode { get; set; }
        public string goods_size { get; set; }
        public string product_area { get; set; }
        public decimal item_pack { get; set; }
        public string group_type { get; set; }
        public string group_main_item_no { get; set; }
        public decimal base_price { get; set; }
        public decimal base_price2 { get; set; }
        public decimal base_price3 { get; set; }
        public string prices { get; set; }
    }
}
