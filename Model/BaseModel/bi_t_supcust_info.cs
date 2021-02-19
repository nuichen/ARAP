using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_supcust_info
    {
        public string supcust_no { get; set; }
        public string supcust_flag { get; set; }
        public string sup_name { get; set; }
        public string sup_fullname { get; set; }
        public string region_no { get; set; }
        public string sup_type { get; set; }
        public string sup_man { get; set; }
        public string sup_addr { get; set; }
        public string sup_email { get; set; }
        public string sup_tel { get; set; }
        public string display_flag { get; set; }
        public decimal credit_amt { get; set; }
        public string sale_man { get; set; }
        public string sup_pyname { get; set; }
        public DateTime update_time { get; set; } = DateTime.Now;
        public string other1 { get; set; }
        public string cust_level { get; set; }
        public string is_retail { get; set; }
        public string mobile { get; set; }
        public string car_id { get; set; }
        public string open_id { get; set; }
        public string img_url { get; set; }
        public string sup_acct_name { get; set; }
        public string invoice_title { get; set; }
        public string is_bind { get; set; }
        public string pwd { get; set; }
        public string aff_settle { get; set; }
        public string is_branch { get; set; }
        public string supcust_group { get; set; }
        public string login_no { get; set; }
        public DateTime create_time { get; set; } = DateTime.Now;
        public string show_num { get; set; }
        public string reach_time { get; set; }
        public string is_self_picking { get; set; } = "0";
        public string is_factory { get; set; }
        public string is_jail { get; set; }
        public int account_period { get; set; }
    }
}
