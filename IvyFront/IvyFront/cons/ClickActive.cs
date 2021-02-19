using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyFront.cons
{
    class ClickActive
    {
        private int inter=1;
        public static void addActive(System.Windows.Forms.Control con)
        {
            ClickActive ins = new ClickActive();
            con.MouseDown += ins.con_mouseDown;
            con.MouseUp += ins.con_mouseUp;
        }

        public static void addActive(System.Windows.Forms.Control con,int inter)
        {
            ClickActive ins = new ClickActive();
            con.MouseDown += ins.con_mouseDown;
            con.MouseUp += ins.con_mouseUp;
            ins.inter = inter;
        }

        private void con_mouseDown(object sender, EventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            con.Left += 2;
            con.Top += 2;
        }

        private void con_mouseUp(object sender, EventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            con.Left -= 2;
            con.Top -= 2;
        }

    }
}
