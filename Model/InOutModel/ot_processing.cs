using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ot_processing
    {
        public decimal local_flow_id { get; set; }
        public decimal task_flow_id { get; set; }
        public string ph_sheet_no { get; set; }
        public string pro_code { get; set; }
        public string pro_bom { get; set; }
        public decimal pro_qty { get; set; }
        public string pro_unit { get; set; }
        public string process_type { get; set; }
        public string bis_type { get; set; }
        public string item_no { get; set; }
        public string unit_no { get; set; }
        public decimal need_qty { get; set; }
        public decimal qty { get; set; }
        public decimal weight { get; set; }
        public DateTime oper_date { get; set; }
        public string oper_id { get; set; }
        public string jh { get; set; }
        public DateTime create_time { get; set; }
    }
}
