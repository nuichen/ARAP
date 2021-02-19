using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyFront.Forms
{
    public partial class InputNumerForm : Form
    {
        private decimal num = 0;
        private int decimals = 0;
        public InputNumerForm(string text,int decimals)
        {
            InitializeComponent();

            label2.Text = text;
            this.decimals = decimals;

            //
            foreach (Control con in this.panel1.Controls)
            {
                cons.AddClickAct.Add(con);
            }
        }

        public bool Input(out decimal num)
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                num = this.num;
                return true;
            }
            else
            {
                num = 0;
                return false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label15_Click(object sender, EventArgs e)
        {
            contrat_str(((Control)sender).Text);
        }

        private void label17_Click(object sender, EventArgs e)
        {
            if (lbl.Text.Length == 0)
            {
            }
            else
            {
                lbl.Text = lbl.Text.Substring(0, lbl.Text.Length - 1);
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            lbl.Text = "";
        }

        private void label18_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void contrat_str(string str)
        {
            if (decimals == 0) 
            {
                if (str == ".") return;
            }
            else
            {
                int pos = lbl.Text.IndexOf(".");
                if (pos == 0) 
                {
                    return;
                }
                if (pos > 0)
                { 
                    //不允许两个小数点
                    if(str == ".") return;
                    //计算当前小数点后面剩余的位数
                    int temp_decimals = lbl.Text.Length - (pos + 1);
                    if (temp_decimals >= decimals) return;
                }
            }
            lbl.Text += str;
        }

        private void label14_Click(object sender, EventArgs e)
        {
            decimal.TryParse(lbl.Text, out num);
            this.DialogResult = DialogResult.OK;
            this.Close();
            //}
        }

        private void InputNumerForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(30, 119, 206)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.WhiteSmoke, 0, panel2.Height - 1, panel2.Width, panel2.Height - 1);
        }

        private void InputNumerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}