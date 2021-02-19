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
    public partial class frmCash : Form
    {
        public frmCash()
        {
            InitializeComponent();

            
            foreach (Control con in this.panel1.Controls)
            {
                cons.AddClickAct.Add(con);
            }
            verify_menu();
        }
        private bool can_edit = true;
        private decimal ys = 0;
        private string choose_pay_way = "A";
        public bool Cash(decimal ys, out decimal ss, out decimal zl, out decimal ml, out decimal zk, out string pay_way)
        {
            this.ys = Conv.ToDecimal(ys.ToString("F2"));//
            this.lblys.Text = this.ys.ToString("0.00");
            decimal temp_ss = Conv.RemoveZero(this.ys);
            this.ml.Text = (this.ys - temp_ss).ToString("0.00");

            lbss.Text = temp_ss.ToString("0.00");
            this.lbl.Text = temp_ss.ToString("0.00");
            this.lblzl.Text = "";
            
            if (this.ShowDialog() == DialogResult.OK)
            {
                ss = Conv.ToDecimal(lbl.Text.Trim());
                ml = Conv.ToDecimal(this.ml.Text);
                zk = Conv.ToDecimal(this.zk.Text);
                zl = Conv.ToDecimal(this.lblzl.Text);
                pay_way = this.choose_pay_way;
                return true;
            }
            else
            {
                ss = 0;
                zl = 0;
                ml = 0;
                zk = 1;
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
            if (lbl.Text.Length > 0)
            {
                lbl.Text = lbl.Text.Substring(0, lbl.Text.Length - 1);
                lbl.SelectionStart = lbl.Text.Length;
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (!can_edit) return;
            lbl.Text = "";
            lbl.SelectionStart = lbl.Text.Length;
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
                decimal sk = Conv.ToDecimal(lbl.Text.Trim());
                decimal ss = Conv.ToDecimal(lbss.Text.Trim());
                decimal zk = Conv.ToDecimal(this.zk.Text);
                decimal zl = Conv.ToDecimal(this.lblzl.Text);
                if (sk <= 0)
                {
                    new MsgForm("收款金额不正确！").ShowDialog();
                    return;
                }
                if (zk < 0 || zk > 1) 
                {
                    new MsgForm("折扣必须在[0,1]之间！").ShowDialog();
                    return;
                }
                if (sk - ss < 0)
                {
                    var frm = new YesNoForm("收款金额小于应收，是否继续？");
                    if (frm.ShowDialog() == DialogResult.No)
                    {
                        return;
                    }
                }
                if (zl >= 100) 
                {
                    new MsgForm("找零必须小于100").ShowDialog();
                    return;
                }
                if (zl >= 0.1M) 
                {
                    ZLForm frm2 = new ZLForm(zl.ToString("0.00"));
                    frm2.ShowDialog();
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
            decimal sk = Conv.ToDecimal(lbl.Text);
            decimal zl = sk - Conv.ToDecimal(lbss.Text);
            if (zl > 0) lblzl.Text = zl.ToString("0.00");
            else lblzl.Text = "";
        }

        private void cal_amt() 
        {
            decimal temp_ys = Conv.ToDecimal((this.ys * Conv.ToDecimal(this.zk.Text)).ToString("F2"));
            decimal ss = (temp_ys - Conv.ToDecimal(ml.Text));
            lbss.Text = ss.ToString("0.00");

            this.lbl.Text = ss.ToString("0.00");
            lbl.SelectionStart = lbl.Text.Length;
            lbl.SelectAll();

            decimal sk = Conv.ToDecimal(lbl.Text);
            decimal zl = sk - ss;
            if (zl > 0) lblzl.Text = zl.ToString("0.00");
            else lblzl.Text = "";
        }


        private void lbl_Click(object sender, EventArgs e)
        {
            lbl.Focus();
            lbl.SelectAll();
        }


        private void frmCash_Load(object sender, EventArgs e)
        {

        }

        private void lbl_show_dz_Click(object sender, EventArgs e)
        {
            InputNumerForm frm = new InputNumerForm("折扣",2);
            decimal qty = 0;
            if (frm.Input(out qty) == true)
            {
                if (qty < 0 || qty > 1)
                {
                    new MsgForm("折扣不正确").ShowDialog();
                    return;
                }
                zk.Text = qty.ToString("0.00");
                //折扣变化，自动抹零也跟着变
                decimal temp_ys = Conv.ToDecimal((this.ys * Conv.ToDecimal(this.zk.Text)).ToString("F2"));
                decimal temp_ss = Conv.RemoveZero(temp_ys);
                this.ml.Text = (temp_ys - temp_ss).ToString("0.00");
                //
                cal_amt();
            }
        }

        private void lbl_show_ml_Click(object sender, EventArgs e)
        {
            InputNumerForm frm = new InputNumerForm("抹零",2);
            decimal qty = 0;
            if (frm.Input(out qty) == true)
            {
                if (qty < 0)
                {
                    new MsgForm("金额不正确").ShowDialog();
                    return;
                }
                ml.Text = qty.ToString("0.00");

                cal_amt();
            }
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
            lbl.Text = lbss.Text;
            this.choose_pay_way = "W";
        }

        private void pic_zfb_Click(object sender, EventArgs e)
        {
            lbl.ReadOnly = true;
            can_edit = false;
            pic_cash.Image = Properties.Resources.cash_pay;
            pic_wx.Image = Properties.Resources.wx_pay;
            pic_zfb.Image = Properties.Resources.zfb_pay_1;
            lbl.Text = lbss.Text;
            this.choose_pay_way = "Z";
        }

        private void verify_menu()
        {
            if (Program.oper_type != "1000")
            {
                zk.Enabled = false;
                ml.Enabled = false;
            }
            else
            {
                zk.Enabled = true;
                ml.Enabled = true;
            }
        }

        
    }
}