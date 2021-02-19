using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class rp_t_account_notice
    {
        public string sheet_no { get; set; }
        public string supcust_no { get; set; }
        public DateTime oper_date { get; set; }
        public DateTime update_time { get; set; }
        public DateTime approve_date { get; set; }
        public string oper_id { get; set; }
        public string approve_man { get; set; }
        public decimal total_amount { get; set; }//金额
        public string approve_flag { get; set; }
        public string remark { get; set; }//备注
        public DateTime account_due_date { get; set; }
        public string supcust_flag { get; set; }

        public decimal recpay_record_info_num2 { get; set; }//已收金额
        public decimal this_receivable { get; set; }//应收金额


    }
}
