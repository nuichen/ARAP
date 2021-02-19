using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace IvyBack.cons
{
    class MyIconButton : System.Windows.Forms.Control
    {
        public MyIconButton()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.ResizeRedraw |
          ControlStyles.AllPaintingInWmPaint, true);
            //
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);


            //
            this.InitializeComponent();
            //
            this.BackColor = Color.Transparent;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyIconButton
            // 
            this.BackColorChanged += new System.EventHandler(this.MyIconButton_BackColorChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyIconButton_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyIconButton_MouseDown);
            this.MouseLeave += new System.EventHandler(this.MyIconButton_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MyIconButton_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MyIconButton_MouseUp);
            this.ResumeLayout(false);

        }

        private Image image;
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                this.Refresh();
            }
        }

        private Color _MoveColor = Color.Black;
        public Color MoveColor
        {
            get
            {
                return _MoveColor;
            }
            set
            {
                _MoveColor = value;
            }
        }

        private void MyIconButton_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated)
                return;

            if (1 == 1)
            {
                Bitmap bmp = new Bitmap(this.Width, this.Height);
                e.Graphics.DrawImage(bmp, 0, 0);
            }
            if (cmd_type == 0)
            {
                Point p = this.PointToClient(System.Windows.Forms.Cursor.Position);
                Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
                if (r.Contains(p) == true)
                {
                    Graphics g = e.Graphics;
                    SizeF s = g.MeasureString("测试", this.Font);
                    int h = this.Height - (int)s.Height - 10;
                    int w = this.Width;
                    if (h > w)
                    {
                        h = w;
                    }
                    else
                    {
                        w = h;
                    }

                    if (Image != null)
                    {
                        g.DrawImage(Image, new Rectangle((this.Width - w) / 2, 0, w, h));
                    }
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Far;
                    g.DrawString(this.Text, this.Font, new SolidBrush(this.MoveColor), new Rectangle(0, 0, this.Width - 1, this.Height - 1), sf);

                }
                else
                {
                    Graphics g = e.Graphics;
                    SizeF s = g.MeasureString("测试", this.Font);
                    int h = this.Height - (int)s.Height - 10;
                    int w = this.Width;
                    if (h > w)
                    {
                        h = w;
                    }
                    else
                    {
                        w = h;
                    }

                    if (Image != null)
                    {
                        g.DrawImage(Image, new Rectangle((this.Width - w) / 2, 0, w, h));
                    }
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Far;
                    g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), new Rectangle(0, 0, this.Width - 1, this.Height - 1), sf);

                }


            }

            else if (cmd_type == 1)
            {
                Graphics g = e.Graphics;
                SizeF s = g.MeasureString("测试", this.Font);
                int h = this.Height - (int)s.Height - 10;
                int w = this.Width;
                if (h > w)
                {
                    h = w;
                }
                else
                {
                    w = h;
                }

                if (Image != null)
                {
                    g.DrawImage(Image, new Rectangle((this.Width - w) / 2 + 1, 1, w, h));
                }
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Far;
                g.DrawString(this.Text, this.Font, new SolidBrush(this.MoveColor), new Rectangle(1, 1, this.Width - 1, this.Height - 1), sf);



            }

        }


        /// <summary>
        /// 0_none;1_mouseDown
        /// </summary>
        private int cmd_type = 0;

        private void MyIconButton_MouseDown(object sender, MouseEventArgs e)
        {
            cmd_type = 1;
            this.Refresh();
        }

        private void MyIconButton_MouseUp(object sender, MouseEventArgs e)
        {
            cmd_type = 0;
            this.Refresh();
        }

        private void MyIconButton_MouseLeave(object sender, EventArgs e)
        {
            cmd_type = 0;
            this.Refresh();
        }

        private void MyIconButton_MouseMove(object sender, MouseEventArgs e)
        {
            cmd_type = 0;
            this.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void MyIconButton_BackColorChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }



    }
}
