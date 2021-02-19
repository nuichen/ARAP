using System;
using System.Drawing;
using System.Windows.Forms;

namespace IvyFront.Forms
{
    public partial class MsgForm : Form
    {
        public MsgForm(string text)
        {
            InitializeComponent();
            label1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void MsgForm_Load(object sender, EventArgs e)
        {
        }

        private void MsgForm_Shown(object sender, EventArgs e)
        {
        }

        private void MsgForm_Paint(object sender, PaintEventArgs e)
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