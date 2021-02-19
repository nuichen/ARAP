using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class bi_t_item_info
    {
        public string item_no { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string item_subno { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string item_name { get; set; }

        public string item_subname { get; set; }
        public string item_clsno { get; set; }
        public string item_brand { get; set; }
        public string item_brandname { get; set; }
        public string unit_no { get; set; }
        public string item_size { get; set; }
        public string product_area { get; set; }

        /// <summary>
        /// 参考进价
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        public decimal base_price { get; set; }

        public decimal sale_price { get; set; }

        public string barcode { get; set; }

        public string display_flag { get; set; }

        //0普通商品; 1称重商品   2条码秤商品
        public string item_flag { get; set; }
    }
}