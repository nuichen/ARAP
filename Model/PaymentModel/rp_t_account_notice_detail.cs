using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class rp_t_account_notice_detail
    {
        public string sheet_no { get; set; }
        public string voucher_no { get; set; }
        public DateTime voucher_no_date { get; set; }
        public string sheet_id { get; set; }
        public DateTime update_time { get; set; }
        public DateTime due_date { get; set; }
        public decimal sheet_amount { get; set; }
        public decimal paid_amount { get; set; }
        public decimal unpaid_amount { get; set; }
        public string remark { get; set; }



    }
}
