using System;
using System.Collections.Generic;
using System.Text;

namespace PrintHelper.cons
{
    interface IInput
    {
        bool Input(string def, out string res);
    }
}
