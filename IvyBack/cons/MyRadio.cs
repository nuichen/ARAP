using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace IvyBack.cons
{
    class MyRadio:System.Windows.Forms.Control 
    {
        public MyRadio()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.ResizeRedraw |
             ControlStyles.AllPaintingInWmPaint, true);
          //
            this.InitializeComponent();
            base.TabStop = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyRadio
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyRadio_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MyRadio_MouseClick);
            this.ResumeLayout(false);
            //
        

        }

        private bool _checked = false;
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                if (this.Parent != null)
                {
                    foreach (Control con in this.Parent.Controls)
                    {
                        if (con.GetType() == typeof(MyRadio))
                        {
                            if (con != this)
                            {
                                if (value == true)
                                {
                                    var rdo=(MyRadio)con;
                                    rdo.Checked=false;
                                }
                             
                            }
                        }
                    }
                }
                this.Refresh();
            }
        }

        private void MyRadio_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            string str = this.Text;
            if (str == "")
            {
                str = "测试";
            }
            SizeF s = g.MeasureString(str, this.Font);
            s.Height += 1;
            s.Width += 1;
            if (1 == 1)
            {
                LinearGradientBrush bru = new LinearGradientBrush(new Rectangle(0, this.Height-(int)s.Height-1, (int)s.Height, (int)s.Height), Color.LightGreen  ,
                    Color.Green , LinearGradientMode.Vertical);
                g.FillEllipse(bru, new Rectangle(0, this.Height-(int)s.Height-1, (int)s.Height, (int)s.Height));
                g.FillEllipse(new SolidBrush(Color.White), new Rectangle(1,  this.Height-(int)s.Height , (int)s.Height - 2, (int)s.Height - 2));
            }

            if (_checked == true)
            {
                LinearGradientBrush bru = new LinearGradientBrush(new Rectangle(3, this.Height-(int)s.Height + 2, (int)s.Height - 6, (int)s.Height - 6), Color.LightGreen  ,
                     Color.Green  , LinearGradientMode.Vertical);
                g.FillEllipse(bru, new Rectangle(3,  this.Height-(int)s.Height + 2, (int)s.Height - 6, (int)s.Height - 6));
            }
            else
            {

            }
            if (1 == 1)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Far;
                g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor),
                    new Rectangle((int)s.Height + 3, 1, this.Width - (int)s.Height - 3, this.Height), sf);
            }
        }

        private void MyRadio_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Checked == true)
            {
                this.Checked = false;
            }
            else
            {
                this.Checked = true;
            }
        }

    }
}
