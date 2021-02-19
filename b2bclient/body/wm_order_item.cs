using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.body
{
   public  class wm_order_item
    {
        public string ord_id { get; set; }
        public string row_index { get; set; }
        public string goods_id { get; set; }
        public string goods_no { get; set; }
        public string goods_name { get; set; }
        public string price { get; set; }
        public string qty { get; set; }
        public string amount { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string enable { get; set; }
    }
}
