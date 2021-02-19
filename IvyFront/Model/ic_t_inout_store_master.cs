namespace Model
{
    public class ic_t_inout_store_master
    {
        public string sheet_no { get; set; }
        public string trans_no { get; set; }
        public string db_no { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string branch_no { get; set; }
        public string d_branch_no { get; set; }
        public string voucher_no { get; set; }
        public string supcust_no { get; set; }
        public decimal total_amount { get; set; }
        public decimal inout_amount { get; set; }
        public string coin_no { get; set; }
        public string pay_way { get; set; }
        public decimal tax_amount { get; set; }
        public decimal discount { get; set; }
        public System.DateTime pay_date { get; set; }
        public string approve_flag { get; set; }
        public System.DateTime oper_date { get; set; }
        public string oper_id { get; set; }
        public string display_flag { get; set; }
        public string other1 { get; set; }
        public string other2 { get; set; }
        public string other3 { get; set; }
        public string cm_branch { get; set; }
        public string deal_man { get; set; }
        public string old_no { get; set; }
        public string approve_man { get; set; }
        public System.DateTime approve_date { get; set; }
        public decimal num1 { get; set; }
        public decimal num2 { get; set; }
        public decimal num3 { get; set; }
        public decimal max_change { get; set; }
        public string sale_no { get; set; }
        public string lock_man { get; set; }
        public System.DateTime lock_date { get; set; }
        public string if_promote { get; set; }
    }
}
