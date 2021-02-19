using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface ISelectContextAlign
    {
        bool Select(int def, out int res);
    }
}
