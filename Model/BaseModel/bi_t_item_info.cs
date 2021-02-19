using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_item_info
    {
        public string item_no { get; set; }
        public string item_subno { get; set; }
        public string item_name { get; set; }
        public string item_subname { get; set; }
        public string item_clsno { get; set; }
        public string unit_no { get; set; }
        public string order_unit { get; set; }
        public string formula_unit_no { get; set; }
        public string item_size { get; set; }
        public string product_area { get; set; }
        public string item_brand { get; set; }
        public string item_brandname { get; set; }
        public decimal price { get; set; }
        public decimal base_price { get; set; }
        public decimal sale_price { get; set; }
        public string combine_sta { get; set; } = "0";//包装类型0
        public string item_flag { get; set; } = "1";//商品性质 0:普通商品 1:称重商品 2:条码秤商品
        public string display_flag { get; set; } = "1";
        public string sup_no { get; set; }
        public string barcode { get; set; }
        public decimal base_price2 { get; set; }
        public decimal base_price3 { get; set; }
        public DateTime update_time { get; set; } = DateTime.Now;
        public decimal valid_day { get; set; } = 1;//有效期
        public string cost_type { get; set; }
        public decimal item_pack { get; set; }
        public string bala_flag { get; set; }
        public string small_img_url { get; set; }
        public string large_img_url { get; set; }
        public string detail_img_url { get; set; }
        public string is_show_mall { get; set; } = "1";
        public string themes { get; set; }
        public string group_type { get; set; }
        public string group_main_item_no { get; set; }
        public string child_item_subno { get; set; }
        public decimal min_stock { get; set; }
        public decimal markup_rate { get; set; }
        public decimal item_loss { get; set; }
        public decimal weight_diff { get; set; }
        public decimal rate { get; set; }
        public decimal rate2 { get; set; }
        public decimal rate3 { get; set; }

        public string factory_no { get; set; }
        ///// <summary>
        ///// 加工属性 0:外购,1:自制
        ///// </summary>
        public string item_property { get; set; } = "0";

        /// <summary>
        /// 加工类型：1：多进A出，2：A进多出，3：多进多出，0：无
        /// </summary>
        public string process_type { get; set; } = "0";

        ///// <summary>
        ///// 是否运算物料需求计划
        ///// </summary>
        public string is_mrp { get; set; } = "1";
        public string item_bom { get; set; }
        public string is_bulk_item { get; set; }
        public string is_self_cg { get; set; } = "0";
        public string branch_no { get; set; }
    }
}
