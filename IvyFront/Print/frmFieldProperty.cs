using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Print
{
    public partial class frmFieldProperty : Form
    {
        private FieldLabel lbl;
        public frmFieldProperty(FieldLabel lbl)
        {
            InitializeComponent();
            this.lbl = lbl;
            //
            this.comboBox1.Text = lbl.Format;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl.Format = this.comboBox1.Text;
        }

    }
}
