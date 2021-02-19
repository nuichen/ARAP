using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.IBLL
{
    public interface IUpdate
    {
        string GetServerVer();
        void Update();
        bool UpdateState();
    }
}
