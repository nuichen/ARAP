using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ic_t_check_bak
    {
        public string sheet_no { get; set; }
        public string branch_no { get; set; }
        public string item_no { get; set; }
        public decimal stock_qty { get; set; }
        public decimal cost_price { get; set; }
        public decimal price { get; set; }
        public decimal sale_price { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }
}
