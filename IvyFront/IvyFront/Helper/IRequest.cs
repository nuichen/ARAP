using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront
{
    interface IRequest
    {
        string request(string par_url, string context);
    }
}
