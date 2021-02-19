using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IvyBack.cons
{
    class MyButton:System.Windows.Forms.Control 
    {
        public MyButton()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.AllPaintingInWmPaint, true);
            //
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
       
            //
            InitializeComponent();
            //
            this.ForeColor = Color.White;
            this.BackColor = Color.Transparent;
            this.ButtonColor = Color.Blue;
        }




        public Color ButtonColor { get; set; }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyButton
            // 
            this.BackColorChanged += new System.EventHandler(this.MyButton_BackColorChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyButton_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyButton_MouseDown);
            this.MouseLeave += new System.EventHandler(this.MyButton_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MyButton_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MyButton_MouseUp);
            this.ResumeLayout(false);

        }


        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.DrawPath(pen, path);
            }
        }

        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        private void MyButton_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            if (1==1)
            {
                Bitmap bmp = new Bitmap(this.Width, this.Height);
                e.Graphics.DrawImage(bmp, 0, 0);
                
            }

           
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            if (cmd_type == 0)
            {
                Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                Helper.DrawHelper.FillRoundRectangle(g, new SolidBrush(ButtonColor), r, 5);
                g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), r, sf);
            }
            else if (cmd_type == 1)
            {

                Rectangle r = new Rectangle(1, 1, this.Width - 1, this.Height - 1);
                Helper.DrawHelper.FillRoundRectangle(g, new SolidBrush(ButtonColor), r, 5);
                g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), r, sf);
              
            }
          



        }
    
        /// <summary>
        /// 0_none;1_mouseDown
        /// </summary>
        private int cmd_type = 0;
        private void MyButton_MouseMove(object sender, MouseEventArgs e)
        {
           
            this.Refresh();
        }

        

        private void MyButton_MouseDown(object sender, MouseEventArgs e)
        {
            cmd_type = 1;
            this.Refresh();
        }

        private void MyButton_MouseUp(object sender, MouseEventArgs e)
        {
            cmd_type = 0;
            this.Refresh();
        }

        private void MyButton_MouseLeave(object sender, EventArgs e)
        {
           
                this.Refresh();
            
        }

        private void MyButton_BackColorChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }


    }
}
