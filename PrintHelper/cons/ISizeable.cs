using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface ISizeable
    {
        System.Drawing.Size Size { get; set; }
        System.Drawing.Point Location { get; set; }
    }
}
