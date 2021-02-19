using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.body
{
    public class alimicrocancel_res
    {
        public alimicrocancel_res()
        {

        }
        public string code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public string retry_flag { get; set; }

        public alimicrocancel_res(string context)
        {
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(context);
            code = r.Read("alipay_trade_cancel_response/code");
            msg = r.Read("alipay_trade_cancel_response/msg");
            sub_code = r.Read("alipay_trade_cancel_response/sub_code");
            sub_msg = r.Read("alipay_trade_cancel_response/sub_msg");
            retry_flag = r.Read("alipay_trade_cancel_response/retry_flag");
        }



        public bool need_query()
        {
            return true;
        }

    }
}