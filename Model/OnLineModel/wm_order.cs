using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class wm_order
    {
        public bool is_check { get; set; }
        public string ord_id { get; set; }
        public string create_time { get; set; }
        public string mobile { get; set; }
        public string mobile_is_new { get; set; }
        public string sname { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public string qty { get; set; }
        public string amount { get; set; }
        public string enable_qty { get; set; }
        public string enable_amount { get; set; }
        public string status { get; set; }
        public string build_status { get; set; }
        public string send_status { get; set; }
        //支付方式：0微信支付;1：信用支付;；2：支付宝支付
        public string pay_type { get; set; }
        public string reach_time { get; set; }
        public string cus_remark { get; set; }
        public string cus_no { get; set; }
        public string salesman_id { get; set; }
        public string is_pay { get; set; }
        public decimal take_fee { get; set; }
        public decimal discount_amt { get; set; }
        public string GetStatus()
        {

            if (status == "0")
            {
                return "待审";
            }
            else if (status == "1")
            {
                if (send_status == "0")
                {
                    return "已审";
                }
                else if (send_status == "1")
                {
                    return "已送";
                }
            }
            else if (status == "2")
            {
                return "失效";
            }

            return "";
        }

        public string status_str
        {
            get
            {
                if (status == "0")
                {
                    return "待审";
                }
                else if (status == "1")
                {
                    if (send_status == "0")
                    {
                        return "已审";
                    }
                    else if (send_status == "1")
                    {
                        return "已送";
                    }
                }
                else if (status == "2")
                {
                    return "失效";
                }

                return "";
            }
        }

        public string date_str 
        {
            get 
            {
                return Conv.ToDateTime(create_time).ToString("yyyy-MM-dd");
            }
        }

        public string time_str
        {
            get
            {
                return Conv.ToDateTime(create_time).ToString("HH:mm");
            }
        }
    }
}
