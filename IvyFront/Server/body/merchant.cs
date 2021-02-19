using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.body
{
    public class merchant
    {
        public string mer_key { get; set; }
        public string mer_id { get; set; }
        public string mer_name { get; set; }
        public string mer_person { get; set; }
        public string address { get; set; }
        public string pwd { get; set; }
        public string status { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}