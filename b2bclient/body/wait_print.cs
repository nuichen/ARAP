using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.body
{
    class wait_print
    {
        public int id { get; set; }
        public string ord_id { get; set; }
        public int goods_id { get; set; }
        public string goods_name { get; set; }
        public int qty { get; set; }
        public decimal price { get; set; }
        public decimal amount { get; set; }
        public DateTime create_time { get; set; }
        public string remark { get; set; }
        public int mc_id { get; set; }
        public DateTime disable_time { get; set; }
        public string print_port1 { get; set; }
        public string print_port2 { get; set; }
        public string desk_print_port { get; set; }
        public string wm_print_port { get; set; }
        public string reprint { get; set; }
        public string sname { get; set; }
        public string sex { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string reach_time { get; set; }
        public decimal discount_amt { get; set; }
    }
}
