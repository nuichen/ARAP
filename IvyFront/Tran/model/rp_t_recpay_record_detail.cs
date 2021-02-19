using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tran.model
{
    public class rp_t_recpay_record_detail
    {
        public string sheet_no { get; set; }
        public string voucher_no { get; set; }
        public decimal  sheet_amount { get; set; }
        public decimal  paid_amount { get; set; }
        public decimal  paid_free { get; set; }
        public decimal  pay_amount { get; set; }
        public decimal  pay_free { get; set; }
        public string memo { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public decimal  num1 { get; set; }
        public decimal  num2 { get; set; }
        public decimal  num3 { get; set; }
        public DateTime  pay_date { get; set; }
        public string item_no { get; set; }
        public string path { get; set; }
        public string select_flag { get; set; }
        public string voucher_type { get; set; }
        public DateTime  oper_date { get; set; }
        public string voucher_other1 { get; set; }
        public string voucher_other2 { get; set; }
        public string order_no { get; set; }
    }
}