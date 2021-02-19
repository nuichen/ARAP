using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IvyFront.IBLL
{
    public interface ICGRK
    {
        string GetNewTHOrderCode();
        string GetNewOrderCode();
        void InsertOrder(Model.ic_t_inout_store_master ord, System.Collections.Generic.List<Model.ic_t_inout_store_detail> items);

    }
}
