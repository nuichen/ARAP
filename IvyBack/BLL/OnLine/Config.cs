using System;
using IvyBack.Helper;
using IvyBack.IBLL.OnLine;

namespace IvyBack.BLL.OnLine
{
    class Config:IConfig 
    {
        void IConfig.read(out string kefu_tel, out string peisong_tel, out string wx_notice, out string peisong_jl
            , out string peisong_amt, out string take_fee, out string kefu_qrcode, out string full_kefu_qrcode
            ,out string print_count, out string start_time, out string end_time)
        {
           
            var req = new Request();
            var json = req.request("/OnLine/config?t=read", "");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            kefu_tel = read.Read("kefu_tel");
            peisong_tel = read.Read("peisong_tel");
            wx_notice = read.Read("wx_notice");

            wx_notice = read.Read("wx_notice");
            peisong_jl = read.Read("peisong_jl");
            peisong_amt = read.Read("peisong_amt");
            take_fee = read.Read("take_fee");
            kefu_qrcode = read.Read("kefu_qrcode");
            full_kefu_qrcode = read.Read("full_kefu_qrcode");
            print_count = read.Read("print_count");

            start_time = read.Read("start_time");
            end_time = read.Read("end_time");
        }

        void IConfig.save(string kefu_tel,string peisong_tel,string wx_notice,string peisong_jl,string peisong_amt
            ,string take_fee,string kefu_qrcode,string print_count, string start_time, string end_time)
        {
            ReadWriteContext.IWriteContext write = new ReadWriteContext.WriteContextByJson();
            write.Append("kefu_tel", kefu_tel);
            write.Append("peisong_tel", peisong_tel);
            write.Append("wx_notice", wx_notice);
            write.Append("peisong_jl", peisong_jl);
            write.Append("peisong_amt", peisong_amt);
            write.Append("take_fee", take_fee);
            write.Append("kefu_qrcode", kefu_qrcode);
            write.Append("print_count", print_count);
            write.Append("start_time", start_time);
            write.Append("end_time", end_time);
            var req = new Request();
            var json = req.request("/OnLine/config?t=save", write.ToString());
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

    }
}
