using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class bi_t_formula_detail
    {
        public int flow_id { get; set; }
        public string formula_no { get; set; }
        public string item_no { get; set; }
        public decimal qty { get; set; }
        public decimal price { get; set; }
        public string unit_no { get; set; }
        public decimal loss_rate { get; set; }
    }
}
