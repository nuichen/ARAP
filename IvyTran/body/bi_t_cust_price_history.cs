using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.body
{
    public class bi_t_cust_price_history
    {
        public decimal id { get; set; }
        public string cust_id { get; set; }
        public string item_no { get; set; }
        public string price_type { get; set; }
        public decimal new_price { get; set; }
        public decimal discount { get; set; }
        public DateTime oper_date { get; set; }
        public string oper_id { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public decimal top_price { get; set; }
        public decimal bottom_price { get; set; }
        public decimal last_price { get; set; }
        public string top_sheet_no { get; set; }
        public string bottom_sheet_no { get; set; }
        public string last_sheet_no { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public decimal max_change { get; set; }
        public DateTime create_time { get; set; }
    }
}