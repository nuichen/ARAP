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
    public partial class frmParSetting : Form
    {
        public frmParSetting()
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
                if (item.par_id == "mo_ling") {
                    if (item.par_val == "1") radioButton2.Checked = true;
                    else if (item.par_val == "2") radioButton3.Checked = true;
                    else if (item.par_val == "3") radioButton4.Checked = true;
                    else radioButton1.Checked = true;
                }
                else if (item.par_id == "weight_model") {
                    if (item.par_val == "2") radioButton6.Checked = true;
                    else radioButton5.Checked = true;
                }
                else if (item.par_id == "branch_no")
                {
                    txt_branch_no.Text = item.par_val;
                    txt_branch_no.SelectionStart = txt_branch_no.Text.Length;
                }
                else if (item.par_id == "jh")
                {
                    txt_jh.Text = item.par_val;
                    txt_jh.SelectionStart = txt_jh.Text.Length;
                }
                else if (item.par_id == "input_cus_model")
                {
                    if (item.par_val == "2") radioButton8.Checked = true;
                    else radioButton7.Checked = true;
                }
                else if (item.par_id == "print_count") 
                {
                    txt_print_count.Text = item.par_val;
                    txt_print_count.SelectionStart = txt_print_count.Text.Length;
                }
                else if (item.par_id == "can_input_qty")
                {
                    if (item.par_val != "1") chk_input_qty.Checked = false;
                }
                else if (item.par_id == "is_continue_weight")
                {
                    if (item.par_val == "1") chk_continue_weight.Checked = true;
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
            else if (txt_print_count.Focused)
            {
                txt_print_count.Text = txt_print_count.Text + str;
                txt_print_count.SelectionStart = txt_print_count.Text.Length;
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
            else if (txt_print_count.Focused)
            {
                if (txt_print_count.Text.Length > 0)
                {
                    txt_print_count.Text = txt_print_count.Text.Substring(0, txt_print_count.Text.Length - 1);
                    txt_print_count.SelectionStart = txt_print_count.Text.Length;
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
            else if (txt_print_count.Focused)
            {
                txt_print_count.Text = "";
                txt_print_count.SelectionStart = txt_print_count.Text.Length;
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
                string mo_ling = "0";
                string weight_model = "1";
                string input_cus_model = "1";
                if (radioButton2.Checked) mo_ling = "1";
                else if (radioButton3.Checked) mo_ling = "2";
                else if (radioButton4.Checked) mo_ling = "3";

                if (radioButton6.Checked) weight_model = "2";

                if (radioButton8.Checked) input_cus_model = "2";

                var can_input_qty = "1";
                if (!chk_input_qty.Checked) can_input_qty = "0";

                var is_continue_weight = "0";
                if (chk_continue_weight.Checked) is_continue_weight = "1";

                IBLL.ISysBLL bll = new BLL.SysBLL();
                var lst = new List<Model.bt_par_setting>();
                lst.Add(new Model.bt_par_setting { par_id = "mo_ling", par_val = mo_ling });
                lst.Add(new Model.bt_par_setting { par_id = "weight_model", par_val = weight_model });
                lst.Add(new Model.bt_par_setting { par_id = "branch_no", par_val = txt_branch_no.Text });
                lst.Add(new Model.bt_par_setting { par_id = "jh", par_val = txt_jh.Text });
                lst.Add(new Model.bt_par_setting { par_id = "input_cus_model", par_val = input_cus_model });
                lst.Add(new Model.bt_par_setting { par_id = "print_count", par_val = txt_print_count.Text });
                lst.Add(new Model.bt_par_setting { par_id = "can_input_qty", par_val = can_input_qty });
                lst.Add(new Model.bt_par_setting { par_id = "is_continue_weight", par_val = is_continue_weight });
                bll.UpdateParSetting(lst);
                Program.mo_ling = mo_ling;
                Program.weight_model = weight_model;
                Program.branch_no = txt_branch_no.Text;
                Program.jh = txt_jh.Text;
                Program.input_cus_model = input_cus_model;
                Program.print_count = Conv.ToInt(txt_print_count.Text);
                Program.can_input_qty = can_input_qty;
                Program.is_continue_weight = is_continue_weight;
                 if (!SoftUpdate.PermissionsBalidation()) 
                {
                    IBLL.IClientBLL bll2 = new BLL.ClientBLL();
                    int errId = 0;
                    string errMsg = "";
                    bll2.DownLoadOper(out errId, out errMsg);
                }

                var frm = new MsgForm("修改参数设置成功");
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