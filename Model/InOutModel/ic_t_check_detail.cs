using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ic_t_check_detail
    {
        public Int64 flow_id { get; set; }
        public string sheet_no { get; set; }
        public string item_no { get; set; }
        public decimal in_price { get; set; }
        public decimal sale_price { get; set; }
        public decimal stock_qty { get; set; }
        public decimal real_qty { get; set; }
        public decimal balance_qty { get; set; }
        public string memo { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public int packqty { get; set; }
        public decimal sgqty { get; set; }
    }
}
