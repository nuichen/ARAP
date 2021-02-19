using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient
{
    public partial class frmTop : Form
    {
        private Form frm;
        public frmTop(Form frm)
        {
            InitializeComponent();
            this.panel1.Hide();
            this.label1.Hide();
            //
            this.frm = frm;
        }

        private void frmTop_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
            frm.Visible = true;
        }

        private void frmTop_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.panel1.BackgroundImage, new Rectangle(0, 0, this.panel1.Width, this.panel1.Height));
            e.Graphics.DrawString(this.label1.Text, this.label1.Font, Brushes.Black, this.label1.Location);
        }

        private void frmTop_Load(object sender, EventArgs e)
        {
            this.Left = 0;
            this.Top = 0;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
        }

    }
}
