using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace b2bclient.control
{
    class MoveHelper
    {
        private System.Windows.Forms.Control con;
        public static void Bind(System.Windows.Forms.Control con)
        {
            var ins = new MoveHelper();
            con.MouseDown += ins.con_MouseDown;
            con.MouseMove += ins.con_MouseMove;
            con.MouseUp += ins.con_MouseUp;
            //
            ins.con = con;
        }

        private Point p = new Point(0, 0);
        private void con_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
        }

        private void con_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int offx = e.X - p.X;
                int offy = e.Y - p.Y;
                con.FindForm().Left += offx;
                con.FindForm().Top += offy;
            }
         
        }

        private void con_MouseUp(object sender, MouseEventArgs e)
        {

        }



    }
}
