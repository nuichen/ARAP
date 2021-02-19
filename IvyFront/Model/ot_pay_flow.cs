using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ot_pay_flow
    {
        public int flow_id { get; set; }
        public string sheet_no { get; set; }
        public string cus_no { get; set; }
        public string sup_name { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public string pay_way { get; set; }
        public decimal sale_amount { get; set; }
        public decimal pay_amount { get; set; }
        public decimal old_amount { get; set; }
        public decimal ml { get; set; }
        public string jh { get; set; }
        //1结算；2退货；3整单取消
        public string approve_flag { get; set; }
        public string remark { get; set; }

        public string approve_flag_str
        {
            get
            {
                if (approve_flag == "0")
                {
                    return "草稿";
                }
                else if (approve_flag == "1")
                {
                    return "结算";
                }
                else if (approve_flag == "2")
                {
                    return "退货";
                }
                else if (approve_flag == "3")
                {
                    return "整单取消";
                }
                else
                {
                    return "未知项";
                }
            }
        }

        public string pay_way_str
        {
            get
            {
                if (pay_way == "A")
                {
                    return "现金";
                }
                else if (pay_way == "D")
                {
                    return "储值卡";
                }
                else if (pay_way == "W")
                {
                    return "微信";
                }
                else if (pay_way == "Z")
                {
                    return "支付宝";
                }
                else if (pay_way == "S")
                {
                    return "扫呗";
                }
                else if (pay_way == "Y")
                {
                    return "抹零";
                }
                else
                {
                    return "其他";
                }
            }
        }
    }
}
