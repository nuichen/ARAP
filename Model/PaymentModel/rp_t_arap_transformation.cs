using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PaymentModel
{
   public class rp_t_arap_transformation
    {
        public string sheet_no { set; get; }
        public string supcust_from { set; get; }
        public string supcust_to { set; get; }
        public string sheet_id { set; get; }
        public string oper_id { set; get; }
        public string approve_man { set; get; }
        public string approve_flag { set; get; }
        public string remark { set; get; }
        public DateTime oper_date { set; get; }
        public DateTime update_time { set; get; }
        public DateTime approve_date { set; get; }
        public decimal total_amount { set; get; }
    }
}
