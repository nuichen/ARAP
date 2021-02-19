using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ot_pay_flow
    {
        public Int64 flow_id { get; set; }
        public string sheet_no { get; set; }
        public string cus_no { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public string pay_way { get; set; }
        public decimal sale_amount { get; set; }
        public decimal pay_amount { get; set; }
        public decimal old_amount { get; set; }
        public decimal ml { get; set; }
        public string jh { get; set; }
        //1结算；2退货；3整单取消
        public string approve_flag { get; set; }
        public string remark { get; set; }
    }
}
