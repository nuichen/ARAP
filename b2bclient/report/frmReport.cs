using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.report
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
            //
            foreach (System.Windows.Forms.Control con in pnl.Controls)
            {
                con.BackColor = Color.FromArgb(240,240,240);
                control.ClickActive.addActive(con, 2);
                con.Click += this.con_click;
                con.Paint += this.lbl_Paint;
            }
            //
            control.FormEsc.Bind(this);
        }

        private string cmd = "";
        private void con_click(object sender, EventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            cmd = con.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public string ClickCMD()
        {
            return cmd;
        }

        private void pnlexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(0, 0, con.Width - 1, con.Height - 1));
        }

    }
}
