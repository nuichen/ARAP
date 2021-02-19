namespace IvyBack.IBLL.OnLine
{
    interface IConfig
    {
        void read(out string kefu_tel, out string peisong_tel, out string wx_notice, out string peisong_jl
            , out string peisong_amt, out string take_fee, out string kefu_qrcode, out string full_kefu_qrcode
            , out string print_count, out string start_time, out string end_time);

        void save(string kefu_tel, string peisong_tel, string wx_notice, string peisong_jl, string peisong_amt
            , string take_fee, string kefu_qrcode, string print_count, string start_time, string end_time);
    }
}
