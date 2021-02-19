using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Reflection;
namespace IvyBack.cons
{
    class MyCheckBox:System.Windows.Forms.Control
    {
        public MyCheckBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.AllPaintingInWmPaint, true);
            //
            this.InitializeComponent();
            base.TabStop = false;
           
        }

        private bool _checked=false;
        public bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                this.Refresh();
            }
        }

         
 


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyCheckBox
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyCheckBox_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MyCheckBox_MouseClick);
            this.ResumeLayout(false);
            

        }

        private void MyCheckBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            string str = this.Text;
            if (str == "")
            {
                str = "测试";
            }
            SizeF s=  g.MeasureString(str, this.Font);
            if (1 == 1)
            {
                LinearGradientBrush bru = new LinearGradientBrush(new Rectangle(0, this.Height - (int)s.Height - 1, (int)s.Height, (int)s.Height), Color.LightGreen  ,
                    Color.Green , LinearGradientMode.Vertical);
                g.FillRectangle(bru, new Rectangle(0, this.Height - (int)s.Height - 1, (int)s.Height, (int)s.Height));
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, this.Height - (int)s.Height, (int)s.Height - 2, (int)s.Height - 2));
            }

            if (_checked == true)
            {
                LinearGradientBrush bru = new LinearGradientBrush(new Rectangle(3, this.Height - (int)s.Height + 2, (int)s.Height - 6, (int)s.Height - 6), Color.LightGreen ,
                     Color.Green, LinearGradientMode.Vertical);
                g.FillRectangle(bru, new Rectangle(3, this.Height - (int)s.Height + 2, (int)s.Height - 6, (int)s.Height - 6));
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

        private void MyCheckBox_MouseClick(object sender, MouseEventArgs e)
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
