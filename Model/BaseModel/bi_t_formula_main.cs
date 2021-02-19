using System;

namespace Model
{
    public class bi_t_formula_main
    {
        public string formula_no { get; set; }
        public string formula_name { get; set; }
        public string formula_clsno { get; set; } = "00";
        public decimal price { get; set; }
        public string display_flag { get; set; } = "1";
        public string formula_type { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; } = DateTime.Now;
        public string formula_pro { get; set; }
        public string parent_formula_no { get; set; }
        public string is_private { get; set; }
        public string supcust_no { get; set; }
    }
}