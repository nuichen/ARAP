using System;

namespace IvyTran.body.Inventory
{
    public class pda_ot_t_check_detail
    {
        public string sheet_no { get; set; }
        public int flow_id { get; set; }
        public string counter_no { get; set; }
        public string item_no { get; set; }
        public string input_code { get; set; }
        public decimal qty { get; set; }
        public string jh { get; set; }
        public DateTime oper_time { get; set; }
        public string oper_man { get; set; }
        public string oper_type { get; set; }
        public string master_no { get; set; }
    }
}