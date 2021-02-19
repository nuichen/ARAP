using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.cons
{
    public class ToolStripClick
    {
        public static void AddClick(System.Windows.Forms.Control con)
        {
            ToolStripClick ins = new ToolStripClick();
            con.Click += ins.toolStrip_Click;
        }


        private void toolStrip_Click(object sender, EventArgs e)
        {
            dynamic toolStrip = sender;
            toolStrip.Focus();
        }
    }
}
