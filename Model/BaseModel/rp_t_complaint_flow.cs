using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.FinanceModel
{
    public class rp_t_complaint_flow
    {
        public decimal flow_id { get; set; }
        public int complaint_type { get; set; }
        public string supcust_no { get; set; }
        public string ph_sheet_no { get; set; }
        public string ms_other { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public string anser { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
    }
}
