using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyFront.Helper;

namespace IvyFront.Forms
{
    public partial class frmPay : Form
    {
        private string op_type = "";
        public frmPay(string op_type)
        {
            InitializeComponent();
            this.op_type = op_type;
            
            foreach (Control con in this.panel1.Controls)
            {
                cons.AddClickAct.Add(con);
            }

            //退款不允许修改金额
            if (op_type == "2") 
            {
                lbl.ReadOnly = true;
            }
        }
        private bool can_edit = true;
        private decimal ys = 0;
        private string choose_pay_way = "A";
        public bool Cash(decimal ys, out decimal ss, out string pay_way)
        {
            this.ys = Conv.ToDecimal(ys.ToString("F2"));
            this.lblys.Text = this.ys.ToString("0.00");
            this.lbl.Text = this.ys.ToString("0.00");
            this.lblzl.Text = "0.00";
            
            if (this.ShowDialog() == DialogResult.OK)
            {
                ss = Conv.ToDecimal(lbl.Text.Trim());
                pay_way = this.choose_pay_way;
                return true;
            }
            else
            {
                ss = 0;
                pay_way = "";
                return false;
            }
        }

      
        private void label3_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void label15_Click(object sender, EventArgs e)
        {
            contract_str(((Control)sender).Text);
        }

        private void contract_str(string str)
        {
            if (!can_edit) return;
            if (lbl.SelectionLength != 0)
            {
                lbl.Text = lbl.Text.Substring(0, lbl.SelectionStart) + str + lbl.Text.Substring(lbl.SelectionStart + lbl.SelectionLength);
            }
            else
            {
                lbl.Text = lbl.Text + str;
            }

            lbl.SelectionStart = lbl.Text.Length;
        }


        private void label17_Click(object sender, EventArgs e)
        {
            if (!can_edit) return;

            if (lbl.Focused)
            {
                if (lbl.Text.Length > 0)
                {
                    lbl.Text = lbl.Text.Substring(0, lbl.Text.Length - 1);
                    lbl.SelectionStart = lbl.Text.Length;
                }
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (!can_edit) return;

            if (lbl.Focused)
            {
                lbl.Text = "";
                lbl.SelectionStart = lbl.Text.Length;
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ss = Conv.ToDecimal(lbl.Text.Trim());
                if (ss <= 0)
                {
                    throw new Exception("付款金额不正确！");
                }
                if (ss - ys != 0)
                {
                    var frm = new YesNoForm("付款金额与应付金额不一致，是否继续？");
                    if (frm.ShowDialog() == DialogResult.No)
                    {
                        return;
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MsgForm frm = new MsgForm(ex.GetMessage());
                frm.ShowDialog();
            }
         
            
        }

        private void InputNumerForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 255)), 0, 0, this.Width - 1, this.Height - 1);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(68, 68, 68)), 0, panel2.Height - 1, panel2.Width, panel2.Height - 1);
        }

        private void lbl_TextChanged(object sender, EventArgs e)
        {
            cal_amt();
        }

        private void cal_amt()
        {
            decimal ss = Conv.ToDecimal(lbl.Text);
            decimal zl = ys - ss;
            lblzl.Text = zl.ToString("0.00");
        }

        private void pic_cash_Click(object sender, EventArgs e)
        {
            lbl.ReadOnly = false;
            can_edit = true;
            pic_cash.Image = Properties.Resources.cash_pay_1;
            pic_wx.Image = Properties.Resources.wx_pay;
            pic_zfb.Image = Properties.Resources.zfb_pay;
            this.choose_pay_way = "A";
        }

        private void pic_wx_Click(object sender, EventArgs e)
        {
            lbl.ReadOnly = true;
            can_edit = false;
            pic_cash.Image = Properties.Resources.cash_pay;
            pic_wx.Image = Properties.Resources.wx_pay_1;
            pic_zfb.Image = Properties.Resources.zfb_pay;
            lbl.Text = lblys.Text;
            this.choose_pay_way = "W";
        }

        private void pic_zfb_Click(object sender, EventArgs e)
        {
            lbl.ReadOnly = true;
            can_edit = false;
            pic_cash.Image = Properties.Resources.cash_pay;
            pic_wx.Image = Properties.Resources.wx_pay;
            pic_zfb.Image = Properties.Resources.zfb_pay_1;
            lbl.Text = lblys.Text;
            this.choose_pay_way = "Z";
        }

    }
}