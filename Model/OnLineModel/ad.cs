using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ad
    {
        public string ad_id { get; set; }
        public string ad_name { get; set; }
        public int ad_type { get; set; }
        public string status { get; set; }
        public string title_img { get; set; }
        public string detail_img { get; set; }
        public string ad_text { get; set; }
        public DateTime create_date { get; set; }
        public string goods_ids { get; set; }
        public int mc_id { get; set; }
    }
}
