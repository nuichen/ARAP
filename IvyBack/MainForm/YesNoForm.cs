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
    public partial class YesNoForm : Form
    {
        public YesNoForm()
        {
            InitializeComponent();
        }
        public YesNoForm(string text)
        {
            InitializeComponent();
            label1.Text = text;
        }

        public static DialogResult ShowFrom(string text)
        {
            YesNoForm frm = new YesNoForm(text);
            return frm.ShowDialog();
        }

        private void YesNoForm_Shown(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void YesNoForm_Paint(object sender, PaintEventArgs e)
        {
            Color c = Color.FromArgb(213, 213, 213);
            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawRectangle(new Pen(c),
                    new Rectangle(i, i, this.Width - i * 2 - 1, this.Height - i * 2 - 1));
            }

        }

        private void YesNoForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
