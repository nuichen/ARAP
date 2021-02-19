using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ot_weighing
    {
        public decimal flow_id { get; set; }
        public string bc_no { get; set; }
        public string rec_code { get; set; }
        public string item_no { get; set; }
        public string item_name { get; set; }
        public string unit_no { get; set; }
        public string order_unit { get; set; }
        public string bala_flag { get; set; }
        public decimal need_qty { get; set; }
        public decimal qty { get; set; }
        public decimal weight { get; set; }
        public DateTime oper_date { get; set; }
        public string oper_id { get; set; }
        public string jh { get; set; }
        public string req_sheet_no { get; set; }
        public int req_sheet_sort { get; set; }
        public DateTime create_time { get; set; }
        public string is_approve { get; set; }
    }
}
