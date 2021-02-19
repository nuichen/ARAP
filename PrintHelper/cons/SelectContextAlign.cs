using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintHelper.cons
{
    public partial class SelectContextAlign : Form,ISelectContextAlign 
    {
        public SelectContextAlign()
        {
            InitializeComponent();
        }

        bool ISelectContextAlign.Select(int def, out int res)
        {
            if (def == 1)
            {
                radioButton1.Checked = true;
            }
            else if (def == 2)
            {
                radioButton2.Checked = true;
            }
            else if (def == 3)
            {
                radioButton3.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
            if (this.ShowDialog() == DialogResult.OK)
            {
                if (radioButton1.Checked == true)
                {
                    res = 1;
                }
                else if (radioButton2.Checked == true)
                {
                    res = 2;
                }
                else if (radioButton3.Checked == true)
                {
                    res = 3;
                }
                else
                {
                    res = 1;
                }
                return true;
            }
            else
            {
                res = 1;
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
