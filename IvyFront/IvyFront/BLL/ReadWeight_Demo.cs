using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.BLL
{
    class ReadWeight_Demo:IBLL.IReadWeight 
    {
        public bool Ini()
        {
            return true;
        }

        public event IBLL.WeightMsgHandler WeightMsg;

        public bool qupi()
        {
            return true;
        }

        public bool set0()
        {
            return true;
        }

        public bool have_bala()
        {
            return true;
        }

        public bool have_ini()
        {
            return true;
        }

        public void Dis()
        {
            
        }
    }
}
