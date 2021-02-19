using System;

namespace Model.BaseModel
{
    public class bi_t_company_type
    {
        public string type_no { get; set; }
        public string type_name { get; set; }
        public string display { get; set; }
        public string memo { get; set; }
        public DateTime update_time { get; set; }
        public decimal max_change { get; set; }
    }
}
