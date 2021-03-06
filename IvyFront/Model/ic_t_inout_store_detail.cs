﻿namespace Model
{
    public class ic_t_inout_store_detail
    {
        //public decimal flow_id { get; set; }
        public string sheet_no { get; set; }
        public string item_no { get; set; }
        public string item_subno { get; set; }
        public string item_name { get; set; }
        public string unit_no { get; set; }
        public decimal unit_factor { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal in_qty { get; set; }
        public decimal orgi_price { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal valid_price { get; set; }
        public decimal cost_price { get; set; }
        public decimal sub_amount { get; set; }
        public decimal tax { get; set; }
        public string is_tax { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public System.DateTime valid_date { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public decimal num4 { get; set; }
        public decimal num5 { get; set; }
        public decimal num6 { get; set; }
        public string barcode { get; set; }
        public int sheet_sort { get; set; }
        public decimal ret_qnty { get; set; }
        public decimal discount { get; set; }
        public string voucher_no { get; set; }
        public decimal cost_notax { get; set; }
        public int packqty { get; set; }
        public decimal sgqty { get; set; }
        public string branch_no_d { get; set; }
        public string ly_sup_no { get; set; }
        public decimal ly_rate { get; set; }
    }
}
