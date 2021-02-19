using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class sa_t_oper_grant
    {
        public string oper_id { get; set; }
        public string func_id { get; set; }
        public string grant_string { get; set; }
        public string other { get; set; }
        public DateTime update_time { get; set; }
        public decimal max_change { get; set; }
    }
}
