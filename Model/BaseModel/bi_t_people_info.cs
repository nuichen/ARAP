using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_people_info
    {
        public string oper_id { get; set; }
        public string oper_name { get; set; }
        public string oper_status { get; set; }
        public string dept_no { get; set; }
        public DateTime birthday { get; set; }
        public string tel { get; set; }
        public DateTime in_date { get; set; }
    }
}
