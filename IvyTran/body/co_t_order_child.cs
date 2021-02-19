using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IvyTran.body
{
    public class co_t_order_child : Model.co_t_order_child
    {
        public decimal real_transaction_sum { get; set; }
        public decimal sale_price { get; set; }
    }
}