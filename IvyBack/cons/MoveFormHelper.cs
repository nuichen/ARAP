using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IvyBack.cons
{
   public class MoveFormHelper
    {
       private System.Windows.Forms.Control con;
       public static void Bind(System.Windows.Forms.Control con)
       {
           MoveFormHelper ins = new MoveFormHelper();
           ins.con = con;
           con.MouseDown += ins.con_MouseDown;
           con.MouseMove += ins.con_MouseMove;
           con.MouseUp += ins.con_MouseUp;
       }

       private System.Drawing.Point p = new System.Drawing.Point();
       private void con_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
       {
           p = new System.Drawing.Point(e.X, e.Y);
       }

       private void con_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
       {
           if (e.Button == System.Windows.Forms.MouseButtons.Left)
           {
               con.FindForm().Left += e.X  - p.X;
               con.FindForm().Top += e.Y - p.Y;
           }
       }

       private void con_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
       {

       }

 
    }
}
