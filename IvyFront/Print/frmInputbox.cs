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
    public partial class frmInputbox : Form
    {
        public frmInputbox()
        {
            InitializeComponent();
        }

        public frmInputbox(string context)
        {
            InitializeComponent();
            this.textBox1.Text = context;
        }
        public string Context()
        {
            return textBox1.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                System.Windows.Forms.MessageBox.Show("内容不能为空");

                //var frm = new frmMsg("内容不能为空");
                //frm.ShowDialog();

            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
