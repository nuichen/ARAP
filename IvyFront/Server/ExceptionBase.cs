using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server
{
    public class ExceptionBase : ApplicationException
    {
        public int errId { get; set; }
        public ExceptionBase(int errId, string msg)
            : base(msg)
        {
            this.errId = errId;
        }

        public ExceptionBase(string msg)
            : base(msg)
        {
            this.errId = -1;
        }

    }
}