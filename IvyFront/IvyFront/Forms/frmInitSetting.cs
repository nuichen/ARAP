﻿using System;
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
    public partial class frmInitSetting : Form
    {
        public frmInitSetting()
        {
            InitializeComponent();
            //
            foreach (Control con in this.panel1.Controls)
            {
                cons.AddClickAct.Add(con);
            }
            IBLL.ISysBLL bll = new BLL.SysBLL();
            var lst = bll.GetParSettingList();
            txt_branch_no.Focus();
            foreach (Model.bt_par_setting item in lst)
            {
                if (item.par_id == "branch_no")
                {
                    txt_branch_no.Text = item.par_val;
                    txt_branch_no.SelectionStart = txt_branch_no.Text.Length;
                }
                else if (item.par_id == "jh")
                {
                    txt_jh.Text = item.par_val;
                    txt_jh.SelectionStart = txt_jh.Text.Length;
                }
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
            if (txt_branch_no.Focused)
            {
                txt_branch_no.Text = txt_branch_no.Text + str;
                txt_branch_no.SelectionStart = txt_branch_no.Text.Length;
            }
            else if (txt_jh.Focused)
            {
                txt_jh.Text = txt_jh.Text + str;
                txt_jh.SelectionStart = txt_jh.Text.Length;
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            if (txt_branch_no.Focused)
            {
                if (txt_branch_no.Text.Length > 0)
                {
                    txt_branch_no.Text = txt_branch_no.Text.Substring(0, txt_branch_no.Text.Length - 1);
                    txt_branch_no.SelectionStart = txt_branch_no.Text.Length;
                }
            }
            else if (txt_jh.Focused)
            {
                if (txt_jh.Text.Length > 0)
                {
                    txt_jh.Text = txt_jh.Text.Substring(0, txt_jh.Text.Length - 1);
                    txt_jh.SelectionStart = txt_jh.Text.Length;
                }
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (txt_branch_no.Focused)
            {
                txt_branch_no.Text = "";
                txt_branch_no.SelectionStart = txt_branch_no.Text.Length;
            }
            else if (txt_jh.Focused)
            {
                txt_jh.Text = "";
                txt_jh.SelectionStart = txt_jh.Text.Length;
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
                Cursor.Current = Cursors.WaitCursor;
                if (txt_branch_no.Text.Trim().Length == 0)
                {
                    new MsgForm("机构编码不能为空").ShowDialog();
                    return;
                }
                IBLL.ISysBLL bll = new BLL.SysBLL();
                var lst = new List<Model.bt_par_setting>();
                lst.Add(new Model.bt_par_setting { par_id = "branch_no", par_val = txt_branch_no.Text });
                lst.Add(new Model.bt_par_setting { par_id = "jh", par_val = txt_jh.Text });
                bll.UpdateParSetting(lst);
                Program.branch_no = txt_branch_no.Text;
                Program.jh = txt_jh.Text;

                var frm = new MsgForm("初始化设置成功");
                frm.ShowDialog();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                new MsgForm(ex.GetMessage()).ShowDialog();
            }
            finally 
            {
                Cursor.Current = Cursors.Default;
            }
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