using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.BLL
{
    class WXPaySetting:IWXPaySetting 
    {

        void IWXPaySetting.read(out string wx_appid_c, out string wx_secret_c, out string wx_mcid_c, out string wx_paykey_c)
        {
            var req = new Request();
            var json = req.request("/acc?t=read","");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
            //
            wx_appid_c = read.Read("wx_appid_c");
            wx_secret_c = read.Read("wx_secret_c");
            wx_mcid_c = read.Read("wx_mcid_c");
            wx_paykey_c = read.Read("wx_paykey_c");
        }

        void IWXPaySetting.save(string wx_appid, string wx_secret, string wx_mcid, string wx_paykey)
        {
            var req = new Request();
            var json = req.request("/acc?t=save", "{\"wx_appid\":\"" + wx_appid + "\",\"wx_secret\":\"" + wx_secret +
                "\",\"wx_mcid\":\"" + wx_mcid  + "\",\"wx_paykey\":\""+wx_paykey + "\"}");
            ReadWriteContext.IReadContext read = new ReadWriteContext.ReadContextByJson(json);
            if (read.Read("errId") != "0")
            {
                throw new Exception(read.Read("errMsg"));
            }
        }

    }
}
