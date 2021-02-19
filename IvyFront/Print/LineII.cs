using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Print
{
    public class LineII : System.Windows.Forms.Panel
    {
        public LineII()
        {
            InitializeComponent();
            this.Height = 100;
            this.Width = 5;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LineII
            // 
            this.SizeChanged += new System.EventHandler(this.LineII_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LineII_Paint);
            this.ResumeLayout(false);
        }

        private void LineII_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(System.Drawing.Pens.Black, new System.Drawing.Point(0, 0),
                new System.Drawing.Point(0, this.Height));
        }

        private void LineII_SizeChanged(object sender, EventArgs e)
        {
            this.Width = 5;
        }
    }
}