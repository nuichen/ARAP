using System;
using System.Drawing;
using System.Windows.Forms;

namespace IvyFront.Forms
{
    public partial class ZLForm : Form
    {
        public ZLForm(string text)
        {
            InitializeComponent();
            label1.Text = Conv.ToDecimal(text).ToString("0.##") + "元";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ZLForm_Load(object sender, EventArgs e)
        {
        }

        private void ZLForm_Shown(object sender, EventArgs e)
        {
        }

        private void ZLForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 255)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}