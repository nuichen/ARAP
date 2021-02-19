using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient
{
   public  interface IRequest
    {
       string request(string par_url, string context);

    }
}
