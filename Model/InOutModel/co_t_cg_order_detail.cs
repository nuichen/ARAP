using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class co_t_cg_order_detail
    {
        public long flow_id { get; set; }
        public string ph_sheet_no { get; set; }
        public string sup_no { get; set; }
        public string item_no { get; set; }
        public string item_name { get; set; }
        public string barcode { get; set; }
        public string unit_no { get; set; }
        public decimal unit_factor { get; set; }
        public decimal price { get; set; }
        public string cust_no { get; set; }
        public string show_num { get; set; }
        public decimal order_qnty { get; set; }
        public decimal cg_qty { get; set; }
        public DateTime create_time { get; set; }
        public string is_append { get; set; }
        public string from_sheet_no { get; set; }
    }
}
