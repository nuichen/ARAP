using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    [Serializable]
    public class pm_t_price_flow_detial
    {
        public Int64 flow_id { get; set; }
        public string sheet_no { get; set; }
        public string item_no { get; set; }
        public string price_type { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public decimal old_price { get; set; }
        public decimal new_price { get; set; }
        public decimal discount { get; set; }
        public decimal buy_qnty { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public decimal stock_qty { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public decimal old_price2 { get; set; }
        public decimal old_price3 { get; set; }

        public decimal new_price2 { get; set; }
        public decimal new_price3 { get; set; }

    }
}
