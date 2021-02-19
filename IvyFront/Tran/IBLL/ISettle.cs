using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Tran.IBLL
{
    public interface ISettle
    {
        void WriteFHD(List<model.sm_t_salesheet> lst1, List<model.sm_t_salesheet_detail> lst2, List<model.ot_pay_flow> lst3);
        void WriteCGRK(List<model.ic_t_inout_store_master> lst1, List<model.ic_t_inout_store_detail> lst2, List<model.ot_pay_flow> lst3);
    }
}
