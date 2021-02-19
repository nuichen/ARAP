using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ic_t_branch_stock
    {
        public string branch_no { get; set; }
        public string item_no { get; set; }
        public decimal stock_qty { get; set; }
        public decimal cost_price { get; set; }
        public string display_flag { get; set; }
        public decimal last_price { get; set; }
        public decimal fifo_price { get; set; }

        public DateTime update_time { get; set; }
    }
}
