using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
   public  class bi_t_region_info
    {
       public string region_no { get; set; }
       public string region_name { get; set; }
       public string display_flag { get; set; }
       public string region_parent { get; set; }
       public DateTime update_time { get; set; }
        public string supcust_flag { get; set; }
    }
}
