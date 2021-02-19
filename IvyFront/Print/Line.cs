using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Print
{
   public  class Line:System.Windows.Forms.Panel 
    {

       public Line()
       {
           InitializeComponent();
           this.Height = 5;
           this.Width = 100;
       }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Line
            // 
            this.SizeChanged += new System.EventHandler(this.Line_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Line_Paint);
            this.ResumeLayout(false);

        }

        private void Line_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(System.Drawing.Pens.Black, new System.Drawing.Point(0, 1), new System.Drawing.Point(this.Width, 1));
        }

        private void Line_SizeChanged(object sender, EventArgs e)
        {
            this.Height = 5;
        }

   }
}
