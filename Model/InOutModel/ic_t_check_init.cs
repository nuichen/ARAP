using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ic_t_check_init
    {
        public string sheet_no { get; set; }
        public string branch_no { get; set; }
        public string oper_id { get; set; }
        public string approve_flag { get; set; }
        public string check_status { get; set; }
        public DateTime begin_date { get; set; }
        public DateTime end_date { get; set; }
        public string memo { get; set; }
        public string cm_branch { get; set; }
        public decimal max_change { get; set; }
        public string approve_man { get; set; }
        public DateTime approve_date { get; set; }
        public string item_clsno { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }

    }
}
