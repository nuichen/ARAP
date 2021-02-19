using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    [Serializable]
    public class rp_t_accout_payrec_flow
    {
        public Int64 flow_no { get; set; }
        public decimal  pay_type { get; set; }
        public string voucher_no { get; set; }
        public string trans_no { get; set; }
        public decimal  sheet_amount { get; set; }
        public decimal  discount { get; set; }
        public decimal  paid_amount { get; set; }
        public decimal  tax_amount { get; set; }
        public string pay_way { get; set; }
        public DateTime  pay_date { get; set; }
        public string supcust_no { get; set; }
        public string supcust_flag { get; set; }
        public decimal  free_money { get; set; }
        public string memo { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public decimal  num1 { get; set; }
        public decimal  num2 { get; set; }
        public decimal  num3 { get; set; }
        public string branch_no { get; set; }
        public string sale_no { get; set; }
        public DateTime  oper_date { get; set; }
    }
}