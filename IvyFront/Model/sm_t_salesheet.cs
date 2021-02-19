using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class sm_t_salesheet
    {
        public string sheet_no { get; set; }
        public string voucher_no { get; set; }
        public string branch_no { get; set; }
        public string cust_no { get; set; }
        public string pay_way { get; set; }
        public decimal discount { get; set; }
        public string coin_no { get; set; }
        public decimal real_amount { get; set; }
        public decimal total_amount { get; set; }
        public decimal paid_amount { get; set; }
        public string approve_flag { get; set; }
        public string source_flag { get; set; }
        public string oper_id { get; set; }
        public string sale_man { get; set; }
        public DateTime oper_date { get; set; }
        public DateTime pay_date { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string cm_branch { get; set; }
        public string approve_man { get; set; }
        public DateTime approve_date { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public string payfee_memo { get; set; }
        public string old_no { get; set; }
        public string psheet_no { get; set; }
        public string pay_nowmark { get; set; }
        public string if_back { get; set; }
        public string cust_cls { get; set; }
        public string other4 { get; set; }
    }
}
