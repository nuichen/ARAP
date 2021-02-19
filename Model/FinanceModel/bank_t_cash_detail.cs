using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bank_t_cash_detail
    {
        public Int64 flow_id { get; set; }
        public string sheet_no { get; set; }
        public string type_no { get; set; }
        public decimal bill_cash { get; set; }
        public string memo { get; set; }
    }
}
