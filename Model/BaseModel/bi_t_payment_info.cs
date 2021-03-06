﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class bi_t_payment_info
    {
        public string pay_way { get; set; }
        public string pay_name { get; set; }
        public string pay_flag { get; set; }//1:系统 0:人工
        public string display { get; set; }
        public string visa_id { get; set; }
        //public string subject_no { get; set; }
        //是否供应商默认结算方式
        public string is_sup_default { get; set; }//0:否 1：是
    }
}
