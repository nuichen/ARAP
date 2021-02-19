using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.InOutModel
{
   public class ic_t_inoutstore_recpay_detail
    {
        public decimal flow_id { get; set; }
        public decimal task_flow_id { get; set; }
        public string voucher_no { get; set; }
        public decimal real_qnty { get; set; }
        public string sheet_no { get; set; }
        public string item_no { get; set; }
        public int sheet_sort { get; set; }

        public string batch_num { get; set; }
        public decimal num1 { get; set; } = 0;
        public decimal num2 { get; set; } = 0;
        public decimal num3 { get; set; } = 0;
        public decimal num4 { get; set; } = 0;
    }
}
