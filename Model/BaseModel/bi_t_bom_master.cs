using System;

namespace Model
{
    [Serializable]
    public class bi_t_bom_master
    {
        public string bom_no{get;set;}
        public string bom_name{get;set;}
        public string approve_flag{get;set;}
        public string oper_id{get;set;}
        public DateTime oper_date{get;set;}
        public DateTime update_time{get;set;}
    }
}