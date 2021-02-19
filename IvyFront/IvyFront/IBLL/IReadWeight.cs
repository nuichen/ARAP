using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.IBLL
{
    public delegate void WeightMsgHandler(decimal weight);
    interface IReadWeight
    {
        bool Ini();
        event WeightMsgHandler WeightMsg;
        bool qupi();
        bool set0();
        bool have_bala();
        bool have_ini();
        void Dis();
    }
}
