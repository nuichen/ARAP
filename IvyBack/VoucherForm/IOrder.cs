using System;
using System.Collections.Generic;
using System.Text;

namespace IvyBack.VoucherForm
{
  public interface IOrder
    {
        bool IsEdit();
        void Save();
        void Add();
        void ShowOrder(string sheet_no);
        void SetOrderList(IOrderList orderlist);
        void SetOrderMerge(IOrderMerge ordermerge);
    }
}
