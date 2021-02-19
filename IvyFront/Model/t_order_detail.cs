using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class t_order_detail
    {
        public int flow_id { get; set; }
        public string sheet_no { get; set; }
        public string item_no { get; set; }
        public string item_subno { get; set; }
        public string item_name { get; set; }
        public string unit_no { get; set; }
        public decimal qty { get; set; }
        public decimal price { get; set; }
        public decimal amt { get; set; }
        public decimal cost_price { get; set; }
        public string branch_no { get; set; }
        public string cus_no { get; set; }
        public string sup_no { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public string jh { get; set; }
        public string approve_flag { get; set; }
        public decimal source_price { get; set; }
        public decimal discount { get; set; }
        public string is_give { get; set; }
        public string approve_flag_str
        {
            get
            {
                if (approve_flag == "0")
                {
                    return "待结算";
                }
                else if (approve_flag == "1")
                {
                    return "已结算";
                }
                else if (approve_flag == "2")
                {
                    return "已删除";
                }
                else
                {
                    return "未知项";
                }
            }
        }

    }
}
