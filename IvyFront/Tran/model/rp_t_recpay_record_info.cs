using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tran.model
{
    public class rp_t_recpay_record_info
    {
        public string sheet_no { get; set; }
        public string supcust_no { get; set; }
        public string supcust_flag { get; set; }
        public string flag_post { get; set; }
        public decimal  total_amount { get; set; }
        public decimal  free_money { get; set; }
        public string coin_no { get; set; }
        public decimal  coin_rate { get; set; }
        public string pay_way { get; set; }
        public string approve_flag { get; set; }
        public string oper_id { get; set; }
        public DateTime  oper_date { get; set; }
        public string deal_man { get; set; }
        public string approve_man { get; set; }
        public DateTime  approve_date { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string visa_id { get; set; }
        public decimal  num1 { get; set; }
        public decimal    num2 { get; set; }
        public decimal  num3 { get; set; }
        public string cm_branch { get; set; }
    
        public string branch_no { get; set; }
        public DateTime  from_date { get; set; }
        public DateTime  to_date { get; set; }
        public string rc_sheet_no { get; set; }
        public string pay_memo { get; set; }
        public DateTime   money_date { get; set; }
    }
}