using System;

namespace IvyTran.body
{
    public class bi_t_supcust_info : Model.bi_t_supcust_info
    {
        public string zip { get; set; }
        public string sup_fax { get; set; }
        public string sup_acct_back { get; set; }
        public string sup_acct_no { get; set; }
        public string sup_tax_no { get; set; }
        public string check_out_flag { get; set; }
        public decimal check_out_date { get; set; }
        public decimal check_out_day { get; set; }
        public string fp { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public string data_lock { get; set; }
        public decimal init_money { get; set; }
        public string sale_no { get; set; }
        public string return_type { get; set; }
        public decimal month_rate { get; set; }
        public string tk_txt { get; set; }
        public decimal batch { get; set; }
        public string CT_SP_Ctrl { get; set; }
        public string affiliate_branch { get; set; }
        public string aff_pur { get; set; }
        public string sup_plan { get; set; }
        public string if_sxsj { get; set; }
        public string ctrl_branch { get; set; }
    }
}