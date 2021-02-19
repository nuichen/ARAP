namespace Model
{
    public class co_t_menu_order_detail
    {
        public int flow_id{get;set;}
        public string sheet_no{get;set;}
        public string formula_no{get;set;}
        /// <summary>
        /// 份数
        /// </summary>
        public decimal qty{get;set;}
        /// <summary>
        /// 餐时段：1早餐,2午餐,3晚餐,4其他
        /// </summary>
        public string menu_type{get;set;}
        /// <summary>
        /// 人群类型：1老师,2学生,3 BB餐,4其他
        /// </summary>
        public string use_type{get;set;}
    }
}