using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IChangeField
    {
        bool Change(string[] Fields, string CurField, out string Field);
    }
}
