using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class rp_t_pay_info  
    {

        public string sheet_no { get; set; }//单号
        public string supcust_no { get; set; }//客户供应商编号
        public string supcust_flag { get; set; }//客户供应商标志
        public decimal total_amount { get; set; } //结算总金额
        public string coin_no { get; set; }//货币单位
        public string coin_rate { get; set; }
        public string approve_flag { get; set; }//审核标志
        public string approve_man { get; set; }//审核人
        public DateTime approve_date { get; set; }//审核时间
        public string oper_id { get; set; }//出纳人ID
        public DateTime oper_date { get; set; }//操作时间
        public string visa_id { get; set; } // 账号
        public DateTime start_date { get; set; }//开始日期
        public DateTime end_date { get; set; } //结束日期
        public string other1 { get; set; }  ///备注1
        public string other2 { get; set; } //备注2
        public string other3 { get; set; } //备注3
        public decimal num1 { get; set; }//备用num1
        public decimal num2 { get; set; }//备用num2
        public decimal num3 { get; set; } //备用num3  


    }
}
