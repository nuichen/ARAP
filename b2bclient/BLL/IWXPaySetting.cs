using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.BLL
{
    interface IWXPaySetting
    {
        void read(out string wx_appid_c, out string wx_secret_c,
            out string wx_mcid_c, out string wx_paykey_c);
        void save(string wx_appid, string wx_secret,
            string wx_mcid, string wx_paykey);

    }
}
