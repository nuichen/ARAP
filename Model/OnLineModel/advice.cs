using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
   public class advice
    {
        public string av_id { get; set; }
        public int mc_id { get; set; }
        public string use_ask { get; set; }
        public string mc_reply { get; set; }
        public DateTime ask_date { get; set; }
        public DateTime reply_date { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
    }
}
