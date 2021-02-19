using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.body
{
    public class alimicroquery_res
    {
        public alimicroquery_res()
        {

        }
        public string code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public string trade_status { get; set; }

        public alimicroquery_res(string context)
        {
            ReadWriteContext.IReadContext r = new ReadWriteContext.ReadContextByJson(context);

            code = r.Read("alipay_trade_query_response/code");
            msg = r.Read("alipay_trade_query_response/msg");
            sub_code = r.Read("alipay_trade_query_response/sub_code");
            sub_msg = r.Read("alipay_trade_query_response/sub_msg");
            trade_status = r.Read("alipay_trade_query_response/trade_status");

        }

        public bool is_doing()
        {
            if (sub_code.ToUpper() == "ACQ.TRADE_NOT_EXIST")//查询的交易不存在
            {
                return false;
            }
            else
            {
                return true;
            }
            if (trade_status.ToUpper() == "WAIT_BUYER_PAY")
            {
                return true;
            }
            return false;
        }

        public bool is_success()
        {
            if (trade_status.ToUpper() == "TRADE_SUCCESS")
            {
                return true;
            }
            return false;
        }


    }
}