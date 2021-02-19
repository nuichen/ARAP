using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyTran.body
{
    public class item
    {
        public string sup_no { get; set; }
        public string item_no { get; set; }
        public string item_name { get; set; }
        public string cust_no { get; set; }
        public string show_num { get; set; }
        public string unit_no { get; set; }
        public decimal unit_factor { get; set; }
        public decimal order_qnty { get; set; }
        public string barcode { get; set; }
        public decimal price { get; set; }
        public decimal cg_qty { get; set; }
        public decimal stock_qty { get; set; }
        public decimal min_stock { get; set; }
    }
}
