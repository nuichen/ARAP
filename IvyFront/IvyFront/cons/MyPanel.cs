using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyFront.Forms
{
  

    public class MyPanel : Panel
    {
        public MyPanel()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint, true);
        }
    }

}
