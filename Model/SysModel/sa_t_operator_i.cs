using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class sa_t_operator_i
    {
        public string oper_id { get; set; }
        public string oper_type { get; set; }
        public string oper_name { get; set; }
        public string oper_pw { get; set; }
        public string oper_status { get; set; }
        public DateTime update_time { get; set; }
        public decimal max_change { get; set; }
        public string is_branch { get; set; }
        public string is_admin { get; set; }
        public string branch_no { get; set; }
    }
}
