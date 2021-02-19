using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ic_t_flow_dt
    {
        public Int64 flow_id { get; set; }
        public string branch_no { get; set; }
        public string item_no { get; set; }
        public DateTime oper_date { get; set; }
        public decimal init_qty { get; set; }
        public decimal init_amt { get; set; }
        public decimal new_qty { get; set; }
        public decimal new_amt { get; set; }
        public decimal settle_qty { get; set; }
        public decimal settle_amt { get; set; }
        public decimal cost_price { get; set; }
        public string db_type { get; set; }
        public string sheet_no { get; set; }
        public string sheet_type { get; set; }
        public string voucher_no { get; set; }
        public string supcust_no { get; set; }
        public string supcust_flag { get; set; }
        public string oper_day { get; set; }
        public decimal adjust_amt { get; set; }
        public string cost_type { get; set; }
        public decimal sale_price { get; set; }
    }
}
