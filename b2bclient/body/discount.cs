using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.body
{
    public class discount
    {
        public string dis_id { get; set; }
        public string dis_name { get; set; }
        public string dis_type { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public decimal dis_amt { get; set; }
        public decimal condition_amt { get; set; }
        public string dis_count { get; set; }
        public string status { get; set; }
        public DateTime create_time { get; set; }
        public string create_str
        {
            get
            {
                return create_time.ToString("yyyy-MM-dd");
            }
        }
        public string status_str
        {
            get
            {
                if (status == "0") return "无效";
                else if (status == "1") return "待审";
                else return "已审";
            }
        }

        public string type_str
        {
            get
            {
                if (dis_type == "1") return "整单满减";
                else if (dis_type == "2") return "首单满减";
                else return "";
            }
        }
    }
}
