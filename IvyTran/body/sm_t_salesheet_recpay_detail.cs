using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.body
{
    public class sm_t_salesheet_recpay_detail
    {
        public long flow_id { get; set; }
        public string sheet_no { get; set; }
        public string sheet_sort { get; set; }
        public string voucher_no { get; set; }
        public long task_flow_id { get; set; }
        public string item_no { get; set; }
        public decimal real_qnty { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string other4 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public decimal num4 { get; set; }
        public string batch_num { get; set; }
    }
}