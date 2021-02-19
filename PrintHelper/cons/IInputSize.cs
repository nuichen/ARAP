using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IInputSize
    {
        bool Input(System.Drawing.Size OriSize, out System.Drawing.Size Size);
    }
}
