using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.Interfaces
{
    public interface IValueChanged
    {
        void Set(object sender, string value);
    }
}