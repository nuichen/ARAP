using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ic_t_cost_adjust
    {
        public Int64 flow_id { get; set; }
        public string branch_no { get; set; }
        public string item_no { get; set; }
        public DateTime oper_date { get; set; }
        public decimal old_price { get; set; }
        public decimal new_price { get; set; }
        public decimal in_qty { get; set; }
        public string sheet_no { get; set; }
        public string memo { get; set; }
        public string type_no { get; set; }
        public decimal adjust_amt { get; set; }
        public string sup_no { get; set; }
        public decimal max_flow_id { get; set; }
        public string cost_type { get; set; }
        public decimal old_qty { get; set; }
    }
}
