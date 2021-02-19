using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class rp_t_supcust_fy_detail
    {
        public string sheet_no { get; set; }
        public Int64 flow_id { get; set; }
        public string kk_no { get; set; }
        public decimal kk_cash { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
    }
}
