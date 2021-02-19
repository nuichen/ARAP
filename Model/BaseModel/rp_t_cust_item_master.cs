using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class rp_t_cust_item_master
    {
        public string sheet_no { get; set; }
        public string sup_no { get; set; }
        public string display_flag { get; set; }
        public string approve_flag { get; set; }
        public string approve_man { get; set; }
        public DateTime approve_date { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public string memo { get; set; }
    }
}
