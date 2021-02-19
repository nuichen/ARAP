using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyFront.IBLL
{
    interface IXSCK
    {
        string GetNewOrderCode();
        void InsertOrder(Model.sm_t_salesheet ord, List<Model.sm_t_salesheet_detail> items);
    }
}
