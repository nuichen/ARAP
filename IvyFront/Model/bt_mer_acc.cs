using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class bt_mer_acc
    {
        /// <summary>
        /// 支付方式：0系统代收；1支付到自己公众号
        /// </summary>
        public string pay_mode { get; set; }
        public string wx_appid { get; set; }
        public string wx_secret { get; set; }
        public string wx_mcid { get; set; }
        public string wx_paykey { get; set; }
    }
}
