using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Print
{
   public  class TextLabel:System.Windows.Forms.Label 
    {
       public TextLabel()
       {
           InitializeComponent();
       }

       private void InitializeComponent()
       {
           this.SuspendLayout();
           // 
           // TextLabel
           // 
           this.DoubleClick += new System.EventHandler(this.TextLabel_DoubleClick);
           this.ResumeLayout(false);

       }

       private void TextLabel_DoubleClick(object sender, EventArgs e)
       {
           var frm = new frmInputbox(this.Text);
           if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
           {
               this.Text = frm.Context();
           }
       }

    }
}
