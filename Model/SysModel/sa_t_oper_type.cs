using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class sa_t_oper_type
    {
        public string oper_type { get; set; }
        public string type_name { get; set; }
        public DateTime update_time { get; set; }
    }
}
