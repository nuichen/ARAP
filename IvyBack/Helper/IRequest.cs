using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.Helper
{
    interface IRequest
    {
        string request(string par_url);
        string request(string par_url, string context);
    }
}
