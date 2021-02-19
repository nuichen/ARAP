using System;

namespace Model
{
    public class ic_t_batch_branch_stock
    {
        public string branch_no { get; set; }
        public string item_no { get; set; }
        public string batch_no { get; set; }
        public string area_no { get; set; }
        public string unit_no { get; set; }
        public decimal stock_qty { get; set; }
        public decimal cost_price { get; set; }
        public decimal unit_factor { get; set; }
        public decimal unpack_qnty { get; set; }
        public string display_flag { get; set; }
        public decimal last_price { get; set; }
        public decimal fifo_price { get; set; }
        public decimal max_change { get; set; }
        public DateTime update_time { get; set; }
    }
}