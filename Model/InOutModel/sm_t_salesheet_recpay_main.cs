using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.InOutModel
{
   public class sm_t_salesheet_recpay_main
    {
        public string sheet_no { get; set; }
        public string voucher_no { get; set; }
        public string cust_no { get; set; }
        public string branch_no { get; set; }
        public string approve_flag { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public DateTime pay_date { get; set; }
        public string memo { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string approve_man { get; set; }
        public DateTime approve_date { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }

    }
}
