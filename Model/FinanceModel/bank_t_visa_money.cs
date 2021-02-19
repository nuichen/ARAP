using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bank_t_visa_money
    {
        public string visa_id { get; set; }
        public string coin_no { get; set; }
        public decimal bank_cash { get; set; }
        public string memo { get; set; }

    }
}
