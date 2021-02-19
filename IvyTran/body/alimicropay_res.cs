using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.body
{
    public class alimicropay_res
    {
        public alimicropay_res()
        {

        }
        public string code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }

        public alimicropay_res(string context)
        {
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(context);

            code = r.Read("alipay_trade_pay_response/code");
            msg = r.Read("alipay_trade_pay_response/msg");
            sub_code = r.Read("alipay_trade_pay_response/sub_code");
            sub_msg = r.Read("alipay_trade_pay_response/sub_msg");
        }



        public bool is_doing(string sub_code)
        {
            if (sub_code.ToUpper() == "ACQ.SYSTEM_ERROR")
            {
                return true;
            }
            if (sub_code.ToUpper() == "ACQ.TRADE_BUYER_NOT_MATCH")//交易买家不匹配(有时候提示产生这个错误时，其实有付款成功)
            {
                return true;
            }
            return false;
        }



    }
}