using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_dept_info
    {
        public string dept_no { get; set; }
        public string dept_name { get; set; }
        public string f_dept_no { get; set; }
        public string manager { get; set; }
        public string meno { get; set; }
        public DateTime update_time { get; set; }

    }
}
