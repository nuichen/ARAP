using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_supcust_bind_item
    {
        public string supcust_no { get; set; }
        public string bind_item { get; set; }
        public DateTime update_time { get; set; }
    }
}
