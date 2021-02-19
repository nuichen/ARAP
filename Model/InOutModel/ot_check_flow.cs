using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ot_check_flow
    {
        public decimal flow_id { get; set; }
        public string task_flow_id { get; set; }
        public string bc_no { get; set; }
        public string item_no { get; set; }
        public string item_name { get; set; }
        public string unit_no { get; set; }
        public decimal qty { get; set; }
        public decimal weight { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public string jh { get; set; }
        public string is_upload { get; set; }
        public string branch_no { get; set; }
        public string is_append { get; set; } 
        public DateTime create_time { get; set; }
    }
}
