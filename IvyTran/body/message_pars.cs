using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.body
{
    public class message_pars
    {
        public int merid { get; set; }
        public int message_id { get; set; }
        public string template_id { get; set; }
        public string title { get; set; }
        public DateTime create_date { get; set; }
    }
}