using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tran.svr
{
    class ServiceBase:IServiceBase 
    {
        void IServiceBase.Request(string t, string pars, out string res)
        {
            ReadWriteContext.IWriteContext w = new ReadWriteContext.WriteContextByJson();
            w.Append("errId", "0");
            w.Append("errMsg", "");
            res = w.ToString();
        }

    }
}
