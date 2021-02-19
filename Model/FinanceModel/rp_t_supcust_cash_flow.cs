using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class rp_t_supcust_cash_flow
    {
        public Int64 flow_no { get; set; }
        public string supcust_no { get; set; }
        public string supcust_flag { get; set; }
        public string oper_type { get; set; }
        public string voucher_no { get; set; }
        public string sheet_no { get; set; }
        public DateTime oper_date { get; set; }
        public decimal old_money { get; set; }
        public decimal oper_money { get; set; }
        public decimal free_money { get; set; }
        public decimal new_money { get; set; }
        public DateTime pay_date { get; set; }
    }
}
