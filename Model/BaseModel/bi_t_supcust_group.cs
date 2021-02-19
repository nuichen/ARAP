using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_supcust_group
    {
        public string SupCust_GroupNo { get; set; }
        public string SupCust_GroupName { get; set; }
        public string SupCust_Flag { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
    }
}
