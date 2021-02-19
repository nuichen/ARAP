using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ic_t_check_master
    {
        public string sheet_no { get; set; }
        public string branch_no { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public string approve_flag { get; set; }
        public string meno { get; set; }
        public string cm_branch { get; set; }
        public string deal_man { get; set; }
        public string check_no { get; set; }
        public decimal max_change { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }
}
