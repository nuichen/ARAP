using System;

namespace Model
{
    public class rp_t_batch_flow
    {
        public string branch_no { get; set; }
        public string batch_no { get; set; }
        public string item_no { get; set; }
        public string voucher_no { get; set; }
        public string batch_type { get; set; }
        public string batch_property { get; set; }
        public string area_no { get; set; }
        public string location_no { get; set; }
        public string unit_no { get; set; }
        public decimal unit_factor { get; set; }
        public string item_size { get; set; } = "";
        public decimal in_price { get; set; }
        public decimal cost_price { get; set; }
        public decimal total_qnty { get; set; }
        public decimal box_qnty { get; set; }
        public decimal box_weight { get; set; }
        public decimal all_qnty { get; set; }
        public decimal settle_qnty { get; set; }
        public decimal sub_amount { get; set; }
        public DateTime valid_date { get; set; }
        public DateTime oper_date { get; set; }
        public string oper_id { get; set; }
        public string oper_name { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public decimal max_change { get; set; }
        public DateTime update_time { get; set; }
    }
}