using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.SysModel
{
    public class sa_t_mobile_oper
    {
        public string oper_id { get; set; }
        public string oper_name { get; set; }
        public string oper_pwd { get; set; }
        public string oper_type { get; set; }
        public string mobile { get; set; }
        public string oper_cls { get; set; }
        public string oper_auth { get; set; }
        public DateTime create_time { get; set; }
        public string display_flag { get; set; }

        public string open_id { get; set; }
        public string union_id { get; set; }
        public DateTime update_time { get; set; }
        public string login_no { get; set; }

        public string oper_auth_str;
    }
}
