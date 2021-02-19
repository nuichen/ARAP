using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class co_t_order_main
    {
        public string sheet_no { get; set; }
        public string p_sheet_no { get; set; }
        public string sup_no { get; set; }
        public string order_man { get; set; }
        public string oper_id { get; set; }
        public DateTime valid_date { get; set; }
        public DateTime oper_date { get; set; }
        public decimal total_amount { get; set; }
        public decimal paid_amount { get; set; }
        public string trans_no { get; set; }
        public string order_status { get; set; }
        public string approve_flag { get; set; }
        public string agree_inhand { get; set; }
        public string coin_code { get; set; }
        public string branch_no { get; set; }
        public string sale_way { get; set; }
        public string memo { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string cm_branch { get; set; }
        public string approve_man { get; set; }
        public DateTime approve_date { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public string ask_no { get; set; }
        public decimal max_change { get; set; }
        public string co_sheetno { get; set; }
        public string if_promote { get; set; }
        public DateTime update_time { get; set; }
        public string sname { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string openid { get; set; }
        public decimal qty { get; set; }
        public string send_status { get; set; }
        public string ph_sheet_no { get; set; }
        public string is_gen_pc { get; set; } = "1";
    }
}
