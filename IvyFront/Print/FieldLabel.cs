using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Print
{
    public class FieldLabel:System.Windows.Forms.Label 
    {
        public FieldLabel()
        {
            this.InitializeComponent();
        }
        private System.ComponentModel.IContainer components;


        public FieldLabel(string Field)
        {
            this.InitializeComponent();
            this.Field = Field;
            this.Text = "#"+Field;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FieldLabel
            // 
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FieldLabel_MouseDoubleClick);
            this.ResumeLayout(false);

        }

        public string Field { get; set; }
        public string Format { get; set; }

        private void FieldLabel_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var frm = new frmFieldProperty(this);
            frm.ShowDialog();
        }

    }
}
