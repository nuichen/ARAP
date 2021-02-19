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
    public partial class frmChangePwd : Form
    {
        public frmChangePwd()
        {
            InitializeComponent();
            //
            foreach (Control con in this.panel1.Controls)
            {
                cons.AddClickAct.Add(con);
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
            if (txt_pwd1.Focused)
            {
                txt_pwd1.Text = txt_pwd1.Text + str;
                txt_pwd1.SelectionStart = txt_pwd1.Text.Length;
            }
            else if (txt_pwd2.Focused)
            {
                txt_pwd2.Text = txt_pwd2.Text + str;
                txt_pwd2.SelectionStart = txt_pwd2.Text.Length;
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            if (txt_pwd1.Focused)
            {
                if (txt_pwd1.Text.Length > 0)
                {
                    txt_pwd1.Text = txt_pwd1.Text.Substring(0, txt_pwd1.Text.Length - 1);
                    txt_pwd1.SelectionStart = txt_pwd1.Text.Length;
                }
            }
            else if (txt_pwd2.Focused)
            {
                if (txt_pwd2.Text.Length > 0)
                {
                    txt_pwd2.Text = txt_pwd2.Text.Substring(0, txt_pwd2.Text.Length - 1);
                    txt_pwd2.SelectionStart = txt_pwd2.Text.Length;
                }
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (txt_pwd1.Focused)
            {
                txt_pwd1.Text = "";
                txt_pwd1.SelectionStart = txt_pwd1.Text.Length;
            }
            else if (txt_pwd2.Focused)
            {
                txt_pwd2.Text = "";
                txt_pwd2.SelectionStart = txt_pwd2.Text.Length;
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            if (txt_pwd2.Text.Length == 0)
            {
                var frm = new MsgForm("请正确输入密码");
                frm.ShowDialog();
                txt_pwd2.Focus();
            }
            else
            {
                try
                {
                    IBLL.IClientBLL bll = new BLL.ClientBLL();
                    string old_pwd = txt_pwd1.Text;
                    string pwd = txt_pwd2.Text;
                    Helper.sec sec = new Helper.sec();
                    if (old_pwd != "")
                    {
                        old_pwd = sec.des(old_pwd);
                    }
                    if (pwd != "")
                    {
                        pwd = sec.des(pwd);
                    }
                    var res = bll.ChangePwd(Program.branch_no, Program.oper_id, old_pwd, pwd);
                    if (res)
                    {
                        IBLL.IOper bll2 = new BLL.Oper();
                        bll2.UpdatePwd(Program.branch_no, Program.oper_id, pwd);

                        var frm = new MsgForm("修改密码成功");
                        frm.ShowDialog();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    var frm = new MsgForm(ex.GetMessage());
                    frm.ShowDialog();
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.FromArgb(68,68,68)), 0, panel2.Height - 1, panel2.Width, panel2.Height - 1);
        }

        private void frmChangePwd_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(30, 119, 206)), 0, 0, this.Width - 1, this.Height - 1);
        }

        private void frmChangePwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}