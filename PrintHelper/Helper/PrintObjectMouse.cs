using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PrintHelper.cons;

namespace PrintHelper.Helper
{
    class PrintObjectMouse
    {
        cons.IPrintObject ins;
        cons.IDesign select;
        System.Windows.Forms.Control con;
       
        private Size sizebk = new Size(0, 0);
        private Point locationbk = new Point(0, 0);
        public static void Bind(cons.IPrintObject ins,cons.IDesign select)
        {
            PrintObjectMouse m = new PrintObjectMouse();
            m.ins = ins;
            m.select = select;
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)ins;
            m.con = con;
            con.MouseDown += m.PrintObject1_MouseDown;
            con.MouseMove += m.PrintObject1_MouseMove;
            con.MouseUp += m.PrintObject1_MouseUp;
            //
           
            m.sizebk = con.Size;
            m.locationbk = con.Location;
        }

        Point p = new Point(0, 0);
        private void PrintObject1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                MouseOverType = Helper.PrintObjectHelper.MouseOverType(new Point(e.X, e.Y), con);
                con.Cursor = Helper.PrintObjectHelper.GetCursor(MouseOverType);
                //
               
               sizebk = con.Size;
                locationbk = con.Location;
            }
            p = new Point(e.X, e.Y);
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
            {
                if (ins.Selected == true)
                {
                    ins.Selected = false;
                }
                else
                {
                    ins.Selected = true;
                }

            }
            else
            {
                if (ins.Selected == true)
                {
                    if (select.Count > 1)
                    {

                        ins.Selected = true;
                    }
                    else
                    {
                        select.RemoveAll();
                        ins.Selected = true;
                    }


                }
                else
                {
                    select.RemoveAll();
                    ins.Selected = true;
                }

            }

        }

        int MouseOverType = 0;
        private void PrintObject1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (MouseOverType == 0)
                {
                    int offsetX = e.X - p.X;
                    int offsetY = e.Y - p.Y;
                    select.OffSetX(offsetX);
                    select.OffSetY(offsetY);

                }
                else if (MouseOverType == 1)
                {
                    int offsetX = e.X - p.X;
                    select.OffSetX(offsetX);
                    select.OffSetWidth(-1 * offsetX);
                }
                else if (MouseOverType == 2)
                {
                    int offsetX = e.X - p.X;
                    select.OffSetWidth(offsetX);
                    p = new Point(e.X, p.Y);
                }
                else if (MouseOverType == 3)
                {
                    int offsetY = e.Y - p.Y;
                    select.OffSetY(offsetY);
                    select.OffSetHeight(-1 * offsetY);
                }
                else if (MouseOverType == 4)
                {
                    int offsetY = e.Y - p.Y;
                    select.OffSetHeight(offsetY);
                    p = new Point(p.X, e.Y);
                }
                else if (MouseOverType == 5)
                {
                    int offsetX = e.X - p.X;
                    int offsetY = e.Y - p.Y;
                    select.OffSetX(offsetX);
                    select.OffSetY(offsetY);
                    select.OffSetWidth(-1 * offsetX);
                    select.OffSetHeight(-1 * offsetY);
                }
                else if (MouseOverType == 6)
                {
                    int offsetX = e.X - p.X;
                    int offsetY = e.Y - p.Y;
                    select.OffSetY(offsetY);
                    select.OffSetWidth(offsetX);
                    select.OffSetHeight(-1 * offsetY);
                    p = new Point(e.X, p.Y);
                }
                else if (MouseOverType == 7)
                {
                    int offsetX = e.X - p.X;
                    int offsetY = e.Y - p.Y;
                    select.OffSetX(offsetX);

                    select.OffSetWidth(-1 * offsetX);
                    select.OffSetHeight(offsetY);
                    p = new Point(p.X, e.Y);
                }
                else if (MouseOverType == 8)
                {
                    int offsetX = e.X - p.X;
                    int offsetY = e.Y - p.Y;

                    select.OffSetWidth(offsetX);
                    select.OffSetHeight(offsetY);
                    p = new Point(e.X, e.Y);
                }

            }
            else
            {
                var MouseOverType = Helper.PrintObjectHelper.MouseOverType(new Point(e.X, e.Y), con);
                con.Cursor = Helper.PrintObjectHelper.GetCursor(MouseOverType);
            }

        }

        private void PrintObject1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                select.ShowMenu(con.PointToScreen(new Point(e.X, e.Y)));
            }
            if (e.Button == MouseButtons.Left)
            {
                if (this.sizebk != con.Size || this.locationbk != con.Location)
                {
                    select.Record();
                }
            }

        }


    }
}
