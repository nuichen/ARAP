using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tran.svr
{
    
    [System.ServiceModel.ServiceContract()]
    interface IServiceBase
    {

        [System.ServiceModel.OperationContract()]
        void Request(string t,string pars, out string res);

    }

}
