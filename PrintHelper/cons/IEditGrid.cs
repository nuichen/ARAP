using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IEditGrid
    {
        bool Edit(System.Data.DataTable tb, GridStyleInfo info, out System.Data.DataTable tb2, out GridStyleInfo info2);
    }
}
