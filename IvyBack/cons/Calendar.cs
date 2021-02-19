using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using IvyBack.Helper;
using System.Runtime.InteropServices;

namespace IvyBack.cons
{
    class Calendar:System.Windows.Forms.Control 
    {

        public delegate void ChoosedHandler(object sender);

        public event ChoosedHandler Choosed;

        private aa a;
        public Calendar()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.ResizeRedraw |
             ControlStyles.AllPaintingInWmPaint, true);
            //
            this.InitializeComponent();
            //
            this.TabStop = false;
            //
            this.Size = new Size(200, 200);
            //
            this.BackColor = Color.White;

            //
            a = new aa();
            a.Click += click;
            Application.AddMessageFilter(a);
        }

        private void click()
        {
            Point p = this.PointToClient(System.Windows.Forms.Cursor.Position);
            Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
            if (r.Contains(p) == false)
            {
                this.Visible = false;
            }
        }


        protected override void Dispose(bool disposing)
        {
            Application.RemoveMessageFilter(a);
            //
            base.Dispose(disposing);
        }

        public class aa : IMessageFilter
        {
            public delegate void ClickHandler();
            private ClickHandler click;
            public event ClickHandler Click
            {
                add { click += value; }
                remove { click -= value; }
            }

            bool IMessageFilter.PreFilterMessage(ref Message m)
            {
                switch (m.Msg)
                {
                    case 513://鼠标左键Down
                        if (click != null)
                        {
                            click.Invoke();
                        }
                        break;
                    case 516://鼠标右键Down
                        if (click != null)
                        {
                            click.Invoke();
                        }
                        break;
                }
                return false;
            }
        }

        
        private  void mh_MouseDownEvent(object sender, MouseEventArgs e)
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(this.Width, this.Height);
            p1 = this.PointToScreen(p1);
            p2 = this.PointToScreen(p2);
            Rectangle r = new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            if (r.Contains(new Point(e.X, e.Y)) == false)
            {
                this.Visible = false;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Calendar
            // 
            this.VisibleChanged += new System.EventHandler(this.Calendar_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Calendar_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Calendar_MouseDown);
            this.MouseLeave += new System.EventHandler(this.Calendar_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Calendar_MouseMove);
            this.Validated += new System.EventHandler(this.Calendar_Validated);
            this.ResumeLayout(false);

        }

        private DateTime date = DateTime.Now;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                this.Refresh();
            }
        }

        private class CellObject
        {
            public Rectangle r { get; set; }
            /// <summary>
            /// 1_数据;101_左;102_右;1_今天
            /// </summary>
            public int type { get; set; }
            public DateTime value { get; set; }
        }

        

        List<CellObject> lstobj = new List<CellObject>();
        private void Calendar_Paint(object sender, PaintEventArgs e)
        {
            List<CellObject> lstobj = new List<CellObject>();
            int rowHeight = this.Height / 7;
            int columnWidth = this.Width / 7;
            //
            Graphics g = e.Graphics;
            g.DrawRectangle(Pens.Gray, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            int x = 0;
            int y = 0;
            int d = 1;
            int days = DateTime.DaysInMonth(date.Year, date.Month);
            Point p = this.PointToClient(System.Windows.Forms.Cursor.Position);
            for(int i=0;i<7;i++)
            {
                
                if (i == 0)
                {
                    if (1 == 1)
                    {
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(0, 0, rowHeight, rowHeight);
                        if (r.Contains(p) == true)
                        {
                            g.FillRectangle(Brushes.Blue, r);
                            g.DrawString("<", this.Font, Brushes.White, r, sf);
                        }
                        else
                        {
                            g.DrawString("<", this.Font, Brushes.Blue, r, sf);
                        }
                       
                        //
                        CellObject item = new CellObject();
                        item.r = r;
                        item.type = 101;
                        lstobj.Add(item);
                    }
                    if (1 == 1)
                    {
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center ;
                        sf.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(this.Width-rowHeight, 0, rowHeight, rowHeight);
                        if (r.Contains(p) == true)
                        {
                            g.FillRectangle(Brushes.Blue, r);
                            g.DrawString(">", this.Font, Brushes.White , r, sf);
                        }
                        else
                        {
                            g.DrawString(">", this.Font, Brushes.Blue, r, sf);
                        }
                      
                        //
                        CellObject item = new CellObject();
                        item.r = r;
                        item.type = 102;
                        lstobj.Add(item);
                    }
                    if (1 == 1)
                    {
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(  rowHeight, 0, this.Width-rowHeight-rowHeight , rowHeight);
                        g.DrawString(date.ToString("yyyy年MM月"), this.Font, Brushes.Black, r, sf);
                     
                    }
                }
                else if (i == 6)
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    Rectangle r = new Rectangle(0, y, this.Width, rowHeight);
                   
                    if (r.Contains(p) == true)
                    {
                        g.FillRectangle(Brushes.Blue, r);
                        g.DrawString(System.DateTime.Now.ToString("yyyy-MM-dd"), this.Font, Brushes.White, r, sf);
                    }
                    
                    else
                    {
                        g.DrawString(System.DateTime.Now.ToString("yyyy-MM-dd"), this.Font, Brushes.Blue, r, sf);
                    }
                    
                    //
                    CellObject item = new CellObject();
                    item.r = r;
                    item.type = 1;
                    item.value = System.DateTime.Now;
                    lstobj.Add(item);
                }
                else
                {
                    x = 0;
                   
                    for (int j = 0; j < 7; j++)
                    {

                        if (d > days)
                        {
                            break;
                        }
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        Rectangle r = new Rectangle(x, y, columnWidth, rowHeight);
                        DateTime d1 = Helper.Conv.ToDateTime(date.Year.ToString() + "-" + date.Month.ToString() + "-" + d.ToString());
                        CellObject item = new CellObject();
                        item.r = r;
                        item.type = 1;
                        item.value = d1;
                        lstobj.Add(item);
                        //
                        r = new Rectangle(r.X + 1, r.Y + 1, r.Width - 2, r.Height - 2);
                      
                        if (r.Contains(p) == true)
                        {
                            g.FillRectangle(Brushes.Blue, r);
                            g.DrawString(d.ToString(), this.Font, Brushes.White, r, sf);
                        }
                        else if (date.ToString("yyyy-MM-dd") == d1.ToString("yyyy-MM-dd"))
                        {
                            g.FillRectangle(Brushes.Blue, r);
                            g.DrawString(d.ToString(), this.Font, Brushes.White, r, sf);
                        }
                        else
                        {
                            g.DrawString(d.ToString(), this.Font, Brushes.Black, r, sf);
                        }
                  
                        //
                      
                        d++;
                        x += columnWidth;
                    }
                }
                y += rowHeight;
            }

            this.lstobj = lstobj;

        }

        private void Calendar_MouseMove(object sender, MouseEventArgs e)
        {
            this.Refresh();
        }

        private void Calendar_MouseDown(object sender, MouseEventArgs e)
        {
            
            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    if (obj.type == 1)
                    {
                        date = obj.value;
                        this.Visible = false;
                        if (Choosed != null)
                        {
                            Choosed.Invoke(this);
                        }
                    }
                    else if (obj.type == 101)
                    {
                       date= date.AddMonths(-1);
                    }
                    else if (obj.type == 102)
                    {
                       date= date.AddMonths(1);
                    }
                    this.Refresh();
                    break;
                }
            }
        }

        private void Calendar_MouseLeave(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Calendar_Validated(object sender, EventArgs e)
        {

        }

        
        private void Calendar_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        



    }
}
