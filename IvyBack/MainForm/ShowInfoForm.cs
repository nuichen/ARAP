using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyBack
{
    public partial class ShowInfoForm : Form
    {
        public ShowInfoForm()
        {
            InitializeComponent();
        }
        public ShowInfoForm(string infoMain, string infoDetail)
        {
            InitializeComponent();

            this.lblInfoMain.Text = infoMain;
            this.txtInfoDetail.Text = infoDetail;
        }

        public static DialogResult ShowFrom(string infoMain, string infoDetail)
        {
            ShowInfoForm frm = new ShowInfoForm(infoMain, infoDetail);
            return frm.ShowDialog();
        }
        public static DialogResult ShowYesNoFrom(string infoMain, string infoDetail)
        {
            ShowInfoForm frm = new ShowInfoForm(infoMain, infoDetail);
            frm.btnNo.Visible = true;
            return frm.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void ShowInfoForm_Paint(object sender, PaintEventArgs e)
        {
            Color c = Color.FromArgb(213, 213, 213);
            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawRectangle(new Pen(c),
                    new Rectangle(i, i, this.Width - i * 2 - 1, this.Height - i * 2 - 1));
            }
        }

        private void ShowInfoForm_Shown(object sender, EventArgs e)
        {
            this.btnOK.Focus();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
