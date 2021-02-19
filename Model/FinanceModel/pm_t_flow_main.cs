using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [Serializable]
    public class pm_t_flow_main
    {
        public string sheet_no { get; set; }
        public string branch_no { get; set; }
        public string vip_type { get; set; }
        public string price_type { get; set; }
        public DateTime oper_date { get; set; }
        public string oper_id { get; set; }
        public string approve_flag { get; set; }
        public string memo { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string cm_branch { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public decimal buy_amt { get; set; }
        public decimal add_amt { get; set; }
        public string deal_man { get; set; }
        public string approve_man { get; set; }
        public DateTime approve_date { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }

    }
}
