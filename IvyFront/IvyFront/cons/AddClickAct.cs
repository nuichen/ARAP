using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IvyFront.cons
{
    public class AddClickAct
    {
        private System.Windows.Forms.Control con;
        public static void Add(System.Windows.Forms.Control con)
        {
            AddClickAct ins = new AddClickAct();
            ins.con = con;
            con.MouseDown += ins.con_mouseDown;
            con.MouseUp += ins.con_mouseUp;
        }

        public static void Add2(System.Windows.Forms.Control con)
        {
            AddClickAct ins = new AddClickAct();
            ins.con = con;
            con.MouseDown += ins.con_mouseDown2;
            con.MouseUp += ins.con_mouseUp2;
            
        }

        private Color c;
        public static void Add3(System.Windows.Forms.Control con)
        {
            AddClickAct ins = new AddClickAct();
            ins.con = con;
            con.MouseDown += ins.con_mouseDown3;
            con.MouseUp += ins.con_mouseUp3;
            ins.c = ins.con.BackColor;
        }

        

        private void con_mouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.con.Left += 1;
            this.con.Top += 1;
        }

        private void con_mouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.con.Left -= 1;
            this.con.Top -= 1;
        }

        private void con_mouseDown2(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.con.Font=new Font(this.con.Font.Name,this.con.Font.Size,FontStyle.Bold);
        }

        private void con_mouseUp2(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.con.Font = new Font(this.con.Font.Name, this.con.Font.Size);
        }

        private void con_mouseDown3(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.con.BackColor = Color.FromArgb((int)(this.con.BackColor.R * 0.6M),
                (int)( this.con.BackColor.G * 0.6M),(int)( this.con.BackColor.B * 0.6M));
        }

        private void con_mouseUp3(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.con.BackColor = this.c;
        }

    }
}
