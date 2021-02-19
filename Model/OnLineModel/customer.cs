using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class customer
    {
        public string cus_no { get; set; }
        public string login_no { get; set; }
        public string cus_pwd { get; set; }
        public string cus_level { get; set; }
        public DateTime cus_start_date { get; set; }
        public DateTime cus_end_date { get; set; }
        public string cus_name { get; set; }
        public string cus_tel { get; set; }
        public string mobile { get; set; }
        public string cus_sex { get; set; }
        public string cus_idcard { get; set; }
        public string cus_area { get; set; }
        public string contact_address { get; set; }
        public string detail_address { get; set; }
        public string cus_email { get; set; }
        public string remark { get; set; }
        public string settle_type { get; set; }
        public string salesman_id { get; set; }
        public string salesman_name { get; set; }
        public string oper_id { get; set; }
        public string status { get; set; }
        public string img_url { get; set; }
        public DateTime create_time { get; set; }
        public DateTime approve_time { get; set; }
        public string msg_hand { get; set; }
        public string is_branch { get; set; }
        public string supcust_group { get; set; }
        public string create_str { 
            get {
                return create_time.ToString("yyyy-MM-dd");
            } 
        }
        public string status_str {
            get
            {
                if(status == "0") return "未审";
                else if (status == "1") return "已审";
                else return "拒绝";
            } 
        }
    }
}