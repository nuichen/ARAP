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
    public partial class frmMinExit : Form
    {
        private frmMain frmMain;
        public frmMinExit(frmMain frmMain)
        {
            InitializeComponent();
            //
            this.frmMain = frmMain;
            control.ClickActive.addActive(panel1);
            control.ClickActive.addActive(panel2);
            control.FormEsc.Bind(this);
            control.ClickActive.addActive(pnlexit);
        }

        private frmDiv frm = new frmDiv();
        public new DialogResult ShowDialog()
        {
            frm.Show();
            return base.ShowDialog();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            frm.Visible = false;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            itype = 1;
            this.DialogResult = DialogResult.OK;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            itype = 2;
            this.DialogResult = DialogResult.OK;
        }

        public int itype = 0;

        private void pnlexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
