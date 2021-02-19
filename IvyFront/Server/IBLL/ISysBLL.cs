using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Server.IBLL
{
    public interface ISysBLL
    {
        body.merchant GetMerchantByKey(string key);
        body.merchant GetMerchantById(string id);
        body.wxpay GetMerWxpayById(string id);
        body.alipay GetMerAlipayById(string id);
    }
}