using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_branch_info
    {
        public string branch_no { get; set; }
        public string branch_name { get; set; }
        public string branch_man { get; set; }
        public string address { get; set; }
        public DateTime update_time { get; set; }
        public string display_flag { get; set; }
    }
}
