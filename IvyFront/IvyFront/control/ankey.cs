using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IvyFront.control
{
    public partial class ankey : UserControl
    {
        public ankey()
        {
            InitializeComponent();
            //
        }

        private void ankey_Paint(object sender, PaintEventArgs e)
        {
            FillRoundRectangle(e.Graphics, new SolidBrush(Color.FromArgb(30, 119, 206)), new Rectangle(0, 0, this.Width - 1, this.Height - 1), 10);
            DrawRoundRectangle(e.Graphics, new Pen(Color.White), new Rectangle(0, 0, this.Width - 1, this.Height - 1), 10);
            System.Drawing.StringFormat sf = new StringFormat();
            sf.Alignment = System.Drawing.StringAlignment.Center;
            sf.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString(this.Text, new Font("微软雅黑", 13),
            new SolidBrush(Color.White), new Rectangle(0, 0, this.Width - 3, this.Height), sf);
            //
        }

        private void ankey_Load(object sender, EventArgs e)
        {
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

        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
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
    }
}
