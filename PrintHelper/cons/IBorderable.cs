using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IBorderable
    {
        int BorderLeft { get; set; }
        int BorderRight { get; set; }
        int BorderTop { get; set; }
        int BorderBottom { get; set; }
    }
}
