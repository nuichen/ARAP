using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyFront.cons
{
    class EnterToTab
    {

        public static void Bind(System.Windows.Forms.Control txt)
        {
            EnterToTab ins = new EnterToTab();
            txt.KeyDown += ins.txt_KeyDown;
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

    }
}
