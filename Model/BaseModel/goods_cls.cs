using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class goods_cls
    {
        public string cls_id { get; set; }
        public string cls_no { get; set; }
        public string cls_name { get; set; }
        public string icon_img_url { get; set; }
        public string cus_group { get; set; }
        public string status { get; set; }
        public string is_show_mall { get; set; }
        public string supcust_groupname { get; set; }
    }
}
