using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_item_cls
    {
        public string item_clsno { get; set; }
        public string item_flag { get; set; }//0:商品分类 A:客商地区 D:部门档案
        public string item_clsname { get; set; }
        public string display_flag { get; set; }
        public DateTime update_time { get; set; }
        public string is_show_mall { get; set; }
        public string cus_group { get; set; }
    }
}
