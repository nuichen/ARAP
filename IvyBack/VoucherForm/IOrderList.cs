using System;
using System.Collections.Generic;
using System.Text;

namespace IvyBack.VoucherForm
{
  public interface IOrderList
    {
        void SetOrder(IOrder order);
        void SetOrderMerge(IOrderMerge ordermerge);
    }
}
