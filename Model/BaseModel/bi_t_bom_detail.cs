using System;

namespace Model
{
    [Serializable]
    public class bi_t_bom_detail
    {
        public long flow_id{get;set;}
        public string item_no{get;set;}
        public decimal qty{get;set;}
        public string unit_no{get;set;}
        public decimal loss_rate{get;set;}
        public string bom_no{get;set;}
    }
}