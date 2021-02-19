using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.body
{
    [Serializable]
    public class bi_t_sup_item
    {
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

        public DateTime oper_date { get; set; }
        public string oper_id { get; set; }
        public decimal max_change { get; set; }
        public string sup_name { get; set; }

        public string item_subno { get; set; }//货号
        public string barcode { get; set; }//主条码
        public string item_name { get; set; }
        public string unit_no { get; set; }//单位
        public decimal base_price { get; set; }//批发价

    }
}
