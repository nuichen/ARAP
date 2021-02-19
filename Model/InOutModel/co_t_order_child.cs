using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class co_t_order_child
    {
        public decimal flow_id{get;set;}
        public string sheet_no{get;set;}
        public string item_no{get;set;}
        public string unit_no{get;set;}
        public decimal unit_factor{get;set;}
        public decimal in_price{get;set;}
        public decimal order_qnty{get;set;}
        public decimal sub_amount{get;set;}
        public decimal real_qty{get;set;}
        public decimal tax_rate{get;set;}
        public decimal pay_percent{get;set;}
        public string other1{get;set;}
        public string other2{get;set;}
        public string other3{get;set;}
        public decimal num1{get;set;}
        public decimal num2{get;set;}
        public decimal num3{get;set;}
        public string barcode{get;set;}
        public int sheet_sort{get;set;}
        public decimal discount{get;set;}
        public string voucher_no{get;set;}
        public decimal out_qty{get;set;}
        public int packqty{get;set;}
        public decimal sgqty{get;set;}
        public string ordmemo{get;set;}
        public decimal month_sale{get;set;}
        public string specin_flag{get;set;}
        public string pick_barcode{get;set;}
        public string supcust_no{get;set;}
        public string batch_num { get; set; }
    }
}
