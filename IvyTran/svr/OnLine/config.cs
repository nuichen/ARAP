using System;
using System.Collections.Generic;
using IvyTran.BLL.OnLine;
using IvyTran.Helper;
using IvyTran.IBLL.OnLine;

namespace IvyTran.svr.OnLine
{
    public class config : BaseService
    {
        IConfig bll = new Config();
        protected override string ProcessRequestGetHandler(string t)
        {
            return base.ProcessRequestGetHandler(t);
        }
        protected override string ProcessRequestPostHandler(string t, Dictionary<string, object> kv)
        {
            WebHelper web = new WebHelper(base.ReadContext);

            try
            {
                web.ReflectionMethod(this, t, kv);
                web.WriteSuccess();
            }
            catch (Exception ex)
            {
                web.WriteError(ex);
            }

            return web.NmJson();
        }

        public void read(WebHelper w, Dictionary<string, object> kv)
        {
            string kefu_tel = bll.kefu_tel;
            string peisong_tel = bll.peisong_tel;
            string wx_notice = bll.wx_notice;
            string peisong_jl = bll.peisong_jl;
            string peisong_amt = bll.peisong_amt;
            string take_fee = bll.take_fee;
            string kefu_qrcode = bll.kefu_qrcode;
            string print_count = bll.print_count;
            string full_kefu_qrcode = "";
            if (!string.IsNullOrEmpty(kefu_qrcode)) full_kefu_qrcode = AppSetting.imgsvr + kefu_qrcode;

            string start_time = bll.start_time;
            string end_time = bll.end_time;

            w.Write("kefu_tel", kefu_tel);
            w.Write("peisong_tel", peisong_tel);
            w.Write("wx_notice", wx_notice);
            w.Write("peisong_jl", peisong_jl);
            w.Write("peisong_amt", peisong_amt);
            w.Write("take_fee", take_fee);
            w.Write("kefu_qrcode", kefu_qrcode);
            w.Write("full_kefu_qrcode", full_kefu_qrcode);
            w.Write("print_count", print_count);
            w.Write("start_time", start_time);
            w.Write("end_time", end_time);
        }


        public void save(WebHelper w, Dictionary<string, object> kv)
        {
            if (ExistsKeys(kv, "kefu_tel", "peisong_tel", "wx_notice", "peisong_jl", "peisong_amt", "take_fee", "kefu_qrcode", "print_count") == false)
            {
                throw new ExceptionBase(-4, "参数错误");
            }

            var kefu_tel = ObjectToString(kv, "kefu_tel");
            var peisong_tel = ObjectToString(kv, "peisong_tel");
            var wx_notice = ObjectToString(kv, "wx_notice");
            var peisong_jl = ObjectToString(kv, "peisong_jl");
            var peisong_amt = ObjectToString(kv, "peisong_amt");
            var take_fee = ObjectToString(kv, "take_fee");
            var kefu_qrcode = ObjectToString(kv, "kefu_qrcode");
            var print_count = ObjectToString(kv, "print_count");
            var start_time = ObjectToString(kv, "start_time");
            var end_time = ObjectToString(kv, "end_time");
            bll.kefu_tel = kefu_tel;
            bll.peisong_tel = peisong_tel;
            bll.wx_notice = wx_notice;
            bll.peisong_jl = peisong_jl;
            bll.peisong_amt = peisong_amt;
            bll.take_fee = take_fee;
            bll.kefu_qrcode = kefu_qrcode;
            bll.print_count = print_count;
            bll.start_time = start_time;
            bll.end_time = end_time;

            AppSetting.print_count = Conv.ToInt(print_count);
        }

    }
}