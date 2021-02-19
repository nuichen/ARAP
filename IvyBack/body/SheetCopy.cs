using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.body
{
    /// <summary>
    /// 复制单据 model
    /// </summary>
    [Serializable]
    public class SheetCopy
    {
        public string sheet_no { get; set; }
        public string voucher_no { get; set; }
        public string tran_no { get; set; }
        public string branch_no { get; set; }
        public string supcust_no { get; set; }
        public string supcust_flag { get; set; }
        public string order_man { get; set; }
        public decimal sub_amount { get; set; }
        public decimal paid_amount { get; set; }
        public decimal total_amount { get; set; }
        public DateTime oper_date { get; set; }
        public DateTime valid_date { get; set; }
        public string memo { get; set; }
        public string other2 { get; set; }

        public List<SheetDetail> details { get; set; }

        [Serializable]
        public class SheetDetail
        {
      
            public string item_no { get; set; }
            public string item_subno { get; set; }
            public string item_name { get; set; }
            public string unit_no { get; set; }
            public string sup_no { get; set; }
            public string sup_name { get; set; }
            public string barcode { get; set; }
            public decimal unit_factor { get; set; }
            public decimal discount { get; set; }

            public string item_size { get; set; }
            public decimal price { get; set; }
            public decimal in_price { get; set; }
            public decimal sale_price { get; set; }
            public decimal real_price { get; set; }
            public decimal cost_price { get; set; }

            public decimal qty { get; set; }
            public decimal order_qty { get; set; }
            public decimal order_qnty { get; set; }
            public decimal real_qty { get; set; }
            public decimal out_qty { get; set; }
            public decimal pack_qty { get; set; }
            public decimal sgqty { get; set; }

            public decimal sale_money { get; set; }
            public decimal sub_amount { get; set; }

            public decimal sheet_sort { get; set; }
            public string other1 { get; set; }
            public string other5 { get; set; }
        }

    }
}
