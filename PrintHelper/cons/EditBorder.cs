using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PrintHelper.cons
{
    public partial class EditBorder : Form,IEditBorder
    {
        public EditBorder()
        {
            InitializeComponent();
        }

        bool IEditBorder.EditBorder(int BorderLeft, int BorderRight, int BorderTop, int BorderBottom, out int BorderLeft2, out int BorderRight2, out int BorderTop2, out int BorderBottom2)
        {
            if (BorderLeft == 1)
            {
                checkBox1.Checked = true;
            }
            if (BorderRight == 1)
            {
                checkBox2.Checked = true;
            }
            if (BorderTop == 1)
            {
                checkBox3.Checked = true;
            }
            if (BorderBottom == 1)
            {
                checkBox4.Checked = true;
            }
            if (this.ShowDialog() == DialogResult.OK)
            {
                if (checkBox1.Checked == true)
                {
                    BorderLeft2 = 1;
                }
                else
                {
                    BorderLeft2 = 0;
                }
                if (checkBox2.Checked == true)
                {
                    BorderRight2 = 1;
                }
                else
                {
                    BorderRight2 = 0;
                }
                if (checkBox3.Checked == true)
                {
                    BorderTop2 = 1;
                }
                else
                {
                    BorderTop2 = 0;
                }
                if (checkBox4.Checked == true)
                {
                    BorderBottom2 = 1;
                }
                else
                {
                    BorderBottom2 = 0;
                }
               
               
              
               
                return  true ;
            }
            else
            {
                BorderLeft2 = 0;
                BorderRight2 = 0;
                BorderTop2 = 0;
                BorderBottom2 = 0;
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
