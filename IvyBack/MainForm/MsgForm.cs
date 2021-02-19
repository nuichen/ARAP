using System;
using System.Drawing;
using System.Windows.Forms;
using IvyBack.Helper;
namespace IvyBack
{
    public partial class MsgForm : Form
    {
        public MsgForm(string text)
        {
            InitializeComponent();
            label1.Text = text;
        }

        public static void ShowFrom(string text)
        {
            MsgForm msg = new MsgForm(text);
            msg.ShowDialog();
        }
        public static void ShowFrom(Form baseFrm, string text)
        {
            MsgForm msg = new MsgForm(text);
            msg.ShowDialog(baseFrm);
        }

        public static void ShowFrom(Exception e)
        {
            Type type = typeof(Exception);
            if (e.InnerException != null)
            {
                type = e.InnerException.GetType();
            }
            else if (e != null)
            {
                type = e.GetType();
            }

            if (type == typeof(Exception))
            {
                MsgForm.ShowFrom(e.Message);
            }
            else
            {
                ShowInfoForm.ShowFrom(e.GetMessage(), "详细错误:" + Environment.NewLine + e.ToString());
            }

        }
        public static void ShowFrom(Form baseFrm, Exception e)
        {
            Type type = typeof(Exception);
            if (e.InnerException != null)
            {
                type = e.InnerException.GetType();
            }
            else if (e != null)
            {
                type = e.GetType();
            }

            if (type == typeof(Exception))
            {
                MsgForm.ShowFrom(baseFrm, e.Message);
            }
            else
            {
                ShowInfoForm.ShowFrom(e.GetMessage(), "详细错误:" + Environment.NewLine + e.ToString());
            }

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
            Color c = Color.FromArgb(213, 213, 213);
            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawRectangle(new Pen(c),
                    new Rectangle(i, i, this.Width - i * 2 - 1, this.Height - i * 2 - 1));
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Action action = new Action(() =>
            {
                string text = this.label1.Text;
                Clipboard.SetDataObject(text);
            });

            GlobalData.mainFrm.Invoke(action);
        }

        private void MsgForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}