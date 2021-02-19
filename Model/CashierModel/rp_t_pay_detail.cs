using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class rp_t_pay_detail
    {

        public int flow_no { get; set; }
        public string sheet_no { get; set; } //单号
        public string voucher_no { get; set; }//业务单号
        public string supcust_no { get; set; }//客户供应商编号
        public string supcust_flag { get; set; }//客户供应商标志
        public string visa_id { get; set; } //账户
        public DateTime oper_date { get; set; } //结算单日期
        public string pay_way { get; set; }//结算方式
        public decimal pay_amount { get; set; }//结算金额
        public string other1 { get; set; }  //备注1
        public string other2 { get; set; } //备注2
        public string other3 { get; set; } //备注3
        public decimal num1 { get; set; } //备用num1
        public decimal num2 { get; set; } //备用num2
        public decimal num3 { get; set; } //备用num3
       



    }
}
