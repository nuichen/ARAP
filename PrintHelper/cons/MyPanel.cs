using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PrintHelper.cons
{
    class MyPanel : System.Windows.Forms.Panel
    {
        public MyPanel()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.ResizeRedraw |
               ControlStyles.AllPaintingInWmPaint, true);
        }

    }
}
