using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PaymentModel
{
   public class rp_t_aging_group
    {
        public int start_days { get; set; }
        public int end_days { get; set; }
        public  string supcust_flag { get; set; }
    }
}
