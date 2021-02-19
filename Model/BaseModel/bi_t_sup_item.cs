using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_sup_item
    {
        public int flow_id { get; set; }
        public string item_no { get; set; }
        public string sup_id { get; set; }
        public decimal price { get; set; }//价格
        public decimal top_price { get; set; }
        public decimal bottom_price { get; set; }
        public decimal last_price { get; set; }
        public string top_sheet_no { get; set; }
        public string bottom_sheet_no { get; set; }
        public string last_sheet_no { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public DateTime spec_from { get; set; }
        public DateTime spec_to { get; set; }
        public decimal spec_price { get; set; }
        public string Item_Status { get; set; }
        public decimal Ct_Ly_rate { get; set; }
        public decimal Ct_Ly_Spec_rate { get; set; }
        public string Spec_sheet_no { get; set; }
        public string Ct_no { get; set; }
        public DateTime update_time { get; set; }
        public string is_supprice { get; set; }
        public string is_enabled { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public DateTime approve_date { get; set; }
        public string approve_flag { get; set; }
    }
}
