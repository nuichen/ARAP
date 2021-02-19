using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParSetting.IBLL
{
    interface IPar
    {
        string Read(string class_name, string par_name);
        void Write(string class_name, string par_name, string par_value);
    }
}
