using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class sm_t_salesheet_detail
    {
        //public decimal flow_id { get; set; }
        public string sheet_no { get; set; }
        public string item_no { get; set; }
        public string item_subno { get; set; }
        public string item_name { get; set; }
        public string unit_no { get; set; }
        public decimal unit_factor { get; set; }
        public decimal sale_price { get; set; }

        /// <summary>
        /// 实际价
        /// </summary>
        public decimal real_price { get; set; }

        public decimal cost_price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sale_qnty { get; set; }

        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal sale_money { get; set; }

        public decimal sale_tax { get; set; }
        public string is_tax { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string other4 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public decimal num4 { get; set; }
        public decimal num5 { get; set; }
        public decimal num6 { get; set; }
        public string barcode { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int sheet_sort { get; set; }

        public decimal ret_qnty { get; set; }
        public decimal discount { get; set; }
        public string voucher_no { get; set; }
        public decimal cost_notax { get; set; }
        public int packqty { get; set; }
        public decimal sgqty { get; set; }
        public string branch_no_d { get; set; }
        public string ly_sup_no { get; set; }
        public decimal ly_rate { get; set; }
        public decimal num7 { get; set; }
        public string other5 { get; set; }
        public decimal num8 { get; set; }
        public DateTime produce_day { get; set; }
    }

}
