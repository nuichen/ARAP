using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class sys_t_system
    {
        public string sys_var_id { get; set; }
        public string sys_var_name { get; set; }
        public string sys_var_value { get; set; }
        public string is_changed { get; set; }
        public string sys_var_desc { get; set; }
        public string sys_ver_flag { get; set; }
        public DateTime update_time { get; set; }
    }
}
