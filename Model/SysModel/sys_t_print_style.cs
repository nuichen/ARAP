using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class sys_t_print_style
    {
        public string report_id { get; set; }
        public string style_id { get; set; }
        public string style_name { get; set; }
        public string oper_id { get; set; }
        public string is_share { get; set; }
        public string style_data { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public string is_base { get; set; }
    }
}
