using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_sz_type
    {
        //varchar(3)
        public string pay_way { get; set; } 
        //varchar(30)
        public string pay_name { get; set; }
        //varchar(1)
        public string pay_flag { get; set; }
        //varchar(15)
        public string km_code { get; set; }
        //varchar(10)
        public string pay_kind { get; set; }
        //varchar(50)
        public string other1 { get; set; }//存放科目代码
        //varchar(50)
        public string other2 { get; set; }
        public decimal num1 { get; set; }
        public int num2 { get; set; }
        //varchar(255)
        public string pay_memo { get; set; }
        public decimal max_change { get; set; }
        //char(1)
        public string if_acc { get; set; }
        //char(1)
        public string path { get; set; }
        //char(1)
        public string is_account { get; set; }
        //char(1)
        public string account_flag { get; set; }
        //char(1)
        public string is_pay { get; set; }
        //char(1)
        public string is_profit { get; set; }
        //char(1)
        public string profit_type { get; set; }
        //char(1)
        public string auto_cashsheet { get; set; }
        //char(1)
        public string if_CtFix { get; set; }
    }
}
