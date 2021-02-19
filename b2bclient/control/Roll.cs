using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.control
{
    class Roll
    {
        public static void up(int inter, System.Windows.Forms.Control par)
        {
            int minTop = 0;
            foreach (System.Windows.Forms.Control con in par.Controls)
            {
                if (con.Top < minTop)
                {
                    minTop = con.Top;
                }
            }
            if (minTop < 0)
            {
                foreach (System.Windows.Forms.Control con in par.Controls)
                {
                    con.Top += inter;
                }
            }
        }

        public static void down(int inter, System.Windows.Forms.Control par)
        {
            int maxBottom = 0;
            foreach (System.Windows.Forms.Control con in par.Controls)
            {
                if (maxBottom < con.Bottom)
                {
                    maxBottom = con.Bottom;
                }
            }
            if (maxBottom > par.Height)
            {
                foreach (System.Windows.Forms.Control con in par.Controls)
                {
                    con.Top -= inter;
                }
            }
           
        }


    }
}
