using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class ic_t_check_finish
    {
        public string sheet_no { get; set; }
        public string item_no { get; set; }
        public string branch_no { get; set; }
        public string change_flag { get; set; }
        public decimal stock_qty { get; set; }
        public decimal real_qty { get; set; }
        public decimal in_price { get; set; }
        public decimal sale_price { get; set; }
        public string memo { get; set; }
        public string diff_duty { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }

    }
}
