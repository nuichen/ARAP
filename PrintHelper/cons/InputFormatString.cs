using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintHelper.cons
{
    public partial class InputFormatString : Form, cons.IInput 
    {
        public InputFormatString()
        {
         
            InitializeComponent();
            
        }

        bool IInput.Input(string def, out string res)
        {
            this.textBox1.Text = def;
            if (this.ShowDialog() == DialogResult.OK)
            {
                res = textBox1.Text;
                return true;
            }
            else
            {
                res = "";
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
