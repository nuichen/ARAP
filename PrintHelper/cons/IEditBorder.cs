using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IEditBorder
    {
        bool EditBorder(int BorderLeft, int BorderRight, int BorderTop, int BorderBottom,
            out int BorderLeft2, out int BorderRight2, out int BorderTop2, out int BorderBottom2);
    }
}
