using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class co_t_price_order_main
    {
        public string sheet_no { get; set; }
        public string trans_no { get; set; }
        public string sup_no { get; set; }
        public string order_man { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string coin_code { get; set; }
        public string memo { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string approve_flag { get; set; }
        public string approve_man { get; set; }
        public DateTime? approve_date { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public DateTime update_time { get; set; }
    }
}
