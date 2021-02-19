using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace IvyBack.cons
{
    class DateTextBox:System.Windows.Forms.TextBox 
    {
        Calendar calendar = new Calendar();
        public DateTextBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.AllPaintingInWmPaint, true);
            //
            InitializeComponent();
          
            //
            calendar.Choosed += this.Choosed;
            this.GotFocus += gotFocus;
            //
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void Choosed(object sender)
        {
            this.Text = calendar.Date.ToString("yyyy-MM-dd HH:mm:ss");
            this.calendar.Visible = false;
            this.SelectionStart = this.Text.Length;
            this.Refresh();
        }

        private void gotFocus(object sender, EventArgs e)
        {
            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DateTextBox
            // 
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DateTextBox_MouseClick);
            this.TextChanged += new System.EventHandler(this.DateTextBox_TextChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DateTextBox_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DateTextBox_KeyPress);
            this.Leave += new System.EventHandler(this.DateTextBox_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DateTextBox_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DateTextBox_MouseMove);
            this.ResumeLayout(false);

        }
       
        const int WM_PAINT = 0xF;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT )
            {
                if (this.IsDisposed == true)
                {
                    return;
                }
                try
                {
                    using (Graphics g = this.CreateGraphics())
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.DrawLine(Pens.Gray, 0, this.Height - 1, this.Width, this.Height - 1);
                        if (this.BorderStyle == BorderStyle.None)
                        {
                            int s = Convert.ToInt16(this.Height * 0.6M);
                            Color c = Color.Gray;
                            g.FillPolygon(new SolidBrush(c), new Point[] { new Point(this.Width -s/2-5,s+(this.Height-s)/2-3-2),
                            new Point(this.Width-s-5,(this.Height-s)/2),
                            new Point(this.Width-5,(this.Height-s)/2)});
                        }
                        else if (this.BorderStyle == BorderStyle.Fixed3D)
                        {
                            int s = this.Height / 2;
                            Color c = Color.Gray;
                            g.FillPolygon(new SolidBrush(c), new Point[] { new Point(this.Width -s/2-5,s+(this.Height-s)/2-3-2),
                            new Point(this.Width-s-5,(this.Height-s)/2),
                            new Point(this.Width-5,(this.Height-s)/2)});
                        }
                    }
                }
                catch (Exception)
                {

                }
               
            }
        }

        private void DateTextBox_MouseMove(object sender, MouseEventArgs e)
        {
             
        }

        private void DateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.SendWait("{Tab}");
            }
        }

        private void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Focused == true)
            {
                this.ShowCalendar();
            }
            this.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void DateTextBox_Leave(object sender, EventArgs e)
        {
           
            DateTime dt;
            if (DateTime.TryParse(this.Text.Trim(), out dt) == true)
            {
                this.Text = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                this.Text = "";
            }
            this.calendar.Visible = false;
        }


        private Point getPoint()
        {
            Point p = new Point(this.Left,this.Top);
            var par = this.Parent;
            
            while (par != null && this.FindForm()!=par)
            {
                p = new Point(p.X + par.Left, p.Y + par.Top);
                par = par.Parent;
            }
            return p;
        }

        private void ShowCalendar()
        {
            if (this.FindForm() != null)
            {
                var frm = this.FindForm();
                DateTime dt;
                if (DateTime.TryParse(this.Text.Trim(), out dt) == true)
                {
                    calendar.Date = dt;
                }
                //
                Point p = getPoint();
                int flag = 0;
                if (flag == 0)
                {
                    int x = p.X ;
                    int y =p.Y + this.Height+1;
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + calendar.Width, y + calendar.Height);
                    Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                    if (r.Contains(p1) && r.Contains(p2))
                    {
                        calendar.Left = x;
                        calendar.Top  = y;
                        flag = 1;
                       
                    }
                }
                if (flag == 0)
                {
                    int x = p.X  + this.Width - calendar.Width;
                    int y = p.Y  + this.Height+1;
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + calendar.Width, y + calendar.Height);
                    Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                    if (r.Contains(p1) && r.Contains(p2))
                    {
                        calendar.Left = x;
                        calendar.Top = y;
                        flag = 1;
                    }
                }
              
                if (flag == 0  )
                {
                    int x = p.X ;
                    int y= p.Y -calendar.Height ;
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + calendar.Width, y + calendar.Height);
                    Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                    if (r.Contains(p1) && r.Contains(p2))
                    {
                        calendar.Left = x;
                        calendar.Top = y;
                        flag = 1;
                    }
                }
                if (flag == 0 )
                {
                    int x = p.X +this.Width-calendar.Width;
                    int y =p.Y  - calendar.Height;
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + calendar.Width, y + calendar.Height);
                    Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                    if (r.Contains(p1) && r.Contains(p2))
                    {
                        calendar.Left = x;
                        calendar.Top = y;
                        flag = 1;
                    }
                }
              

                if (flag == 0)
                {
                    calendar.Left =p.X ;
                    calendar.Top =p.Y + this.Height;
                }
               
                //
                calendar.Visible = true;
                frm.Controls.Add(calendar);
                calendar.BringToFront();
                //
                

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.calendar.Visible = false;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DateTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            int s = this.Height;
            Rectangle r = new Rectangle(this.Width - s, 0, s, s);
            if (r.Contains(e.X, e.Y) == true)
            {
                this.ShowCalendar();
            }
             this.Refresh();
          

        }

        private void DateTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.Refresh();
        }

        public static bool IsNum(char str)
        {
            if ((str >= '0' && str <= '9'))
                return true;
            return false;
        }

        private void DateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsNum(e.KeyChar) == true || e.KeyChar == '-' || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
            {

            }
            else
            {
                e.Handled = true;
            }
        }



    }

}
