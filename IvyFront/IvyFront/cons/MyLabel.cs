using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyFront.Forms
{
  

    public class MyLabel : Label
    {
        public MyLabel()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint, true);
        }
    }


}
