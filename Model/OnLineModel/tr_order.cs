using System;

namespace Model
{
    public class tr_order
    {
        public string ord_id { get; set; }
        public string status { get; set; }
        public int mc_id { get; set; }
        public decimal qty { get; set; }
        public decimal amount { get; set; }
        public decimal enable_qty { get; set; }
        public decimal enable_amount { get; set; }
        public DateTime create_time { get; set; }
        public string check_oper_id { get; set; }
        public string company { get; set; }
        public string sname { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string openid { get; set; }
        public int cus_id { get; set; }
        public string msg_hand { get; set; }
        public string send_status { get; set; }
        public string pay_type { get; set; }
        public string build_status { get; set; }
        public decimal card_pay { get; set; }
        public string flow_no { get; set; }
        public string distribution_type { get; set; }//配送类型：0：配送；1：自提；
        public string to_the_code { get; set; }//自提码
        public string open_type { get; set; }
        public decimal discount_amt { get; set; }//优惠金额
        public decimal take_fee { get; set; }//配送费
        public string cus_remark { get; set; }//备注
        public string reach_time { get; set; }//预约配送时间
        public decimal wm_fee_bag { get; set; }//包装费
        public decimal refund_amt { get; set; }//退款
        public decimal pay_weixin { get; set; }//微信支付金额
        public string cus_no { get; set; }
        public string salesman_id { get; set; }
        public string sheet_no { get; set; }
    }
}
