using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.body
{
    public class vip_pcash
    {
        public int pcash_id { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public decimal start_cash { get; set; }
        public decimal give_cash { get; set; }
        public string is_stop { get; set; }
        public string start_str
        {
            get
            {
                return start_date.ToString("yyyy-MM-dd");
            }
        }
        public string end_str
        {
            get
            {
                return end_date.ToString("yyyy-MM-dd");
            }
        }
        public string stop_str
        {
            get
            {
                if (is_stop == "0") return "待审";
                else if (is_stop == "1") return "已审";
                else return "无效";
            }
        }
    }
}
