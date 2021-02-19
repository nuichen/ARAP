using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.body
{
    [Serializable]
    public class ItemPrice
    {
        public string item_no { get; set; }
        public string item_name { get; set; }
        public string base_price { get; set; }//价格
        public string new_price { get; set; }
        public string base_price2 { get; set; }
        public string new_price2 { get; set; }
        public string base_price3 { get; set; }
        public string new_price3 { get; set; }

        public string price { get; set; }
        public string last_price { get; set; }
    }
}
