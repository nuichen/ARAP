namespace IvyTran.IBLL.OnLine
{
    public interface IConfig
    {
        /// <summary>
        /// 客服电话
        /// </summary>
        string kefu_tel { get; set; }

        /// <summary>
        /// 配送电话
        /// </summary>
        string peisong_tel { get; set; }

        /// <summary>
        /// 商城公告
        /// </summary>
        string wx_notice { get; set; }

        /// <summary>
        /// 配送距离
        /// </summary>
        string peisong_jl { get; set; }

        /// <summary>
        /// 免配送费起始金额
        /// </summary>
        string peisong_amt { get; set; }

        /// <summary>
        /// 配送费
        /// </summary>
        string take_fee { get; set; }

        /// <summary>
        /// 客服二维码
        /// </summary>
        string kefu_qrcode { get; set; }

        /// <summary>
        /// 订单打印份数
        /// </summary>
        string print_count { get; set; }

        /// <summary>
        /// 开始下单时间
        /// </summary>
        string start_time { get; set; }

        /// <summary>
        /// 截单时间
        /// </summary>
        string end_time { get; set; }

    }
}