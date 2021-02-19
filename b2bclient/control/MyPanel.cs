using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.control
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
