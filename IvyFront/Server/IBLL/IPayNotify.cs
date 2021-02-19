using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.IBLL
{
    public interface IPayNotify
    {
        void Success(string pay_no, string ord_id);
        void Fail(string pay_no, string ord_id);
    }
}