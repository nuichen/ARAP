using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyBack.cons
{
    class MyPanel:System.Windows.Forms.Panel 
    {
        public MyPanel()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.AllPaintingInWmPaint, true);
        }

    }
}
