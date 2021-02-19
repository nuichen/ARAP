using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class rp_t_bank_beginbalance
    {
        public string visa_id { get; set; }     
        public string visa_nm { get; set; }
        public decimal begin_balance { get; set; }
        public int pay_kind { get; set; }
        public string oper_id { get; set; }
        public DateTime update_time { get; set; }
        public string approve_flag { get;set; }


    }
}
