
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using IvyBack.Helper;

namespace IvyBack.FinanceForm
{
    public partial class frmBankUpdate : Form
    {
        public frmBankUpdate()
        {
            InitializeComponent();
            
        }

        public bi_t_bank_info bank;

        private void LoadBank()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = c as TextBox;
                    if (tb.TextAlign == HorizontalAlignment.Right)
                        c.Text = "0.0000";
                    else
                        c.Text = "";
                }
            }

            if (bank != null)
            {
                txtBankName.Text = bank.visa_nm;
                txtBankNo.Text = bank.visa_id;
                txtBankNo.ReadOnly = true;
                //checkIsStop.Checked = bank.display_flag.Equals("0");

            }
            else
            {
                txtBankName.Text = "";
                txtBankNo.Text = "";
                txtBankNo.ReadOnly = false;
                //checkIsStop.Checked = false;
            }
        }
        private void OK()
        {
            IBLL.IBank bll = new BLL.BankBLL();
            if (bank == null || string.IsNullOrEmpty(bank.visa_id))
            {
                //添加
                bi_t_bank_info banks = new bi_t_bank_info()
                {
                    display_flag = "1",//checkIsStop.Checked ? "0" : "1",
                    visa_nm = txtBankName.Text,
                    visa_id = txtBankNo.Text,
                };
                bll.Add(banks);
            }
            else
            {
                //修改
                bank.display_flag = "1";//this.checkIsStop.Checked ? "0" : "1";
                bank.visa_nm = this.txtBankName.Text;
                bll.Upload(bank);
            }

        }

        private void frmBankUpload_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBank();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("frmPaymentUpload_Load=>load", ex.ToString());
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "14"))
            {

                return;
            }
            try
            {
                if (string.IsNullOrEmpty(this.txtBankNo.Text))
                {
                    txtBankNo.Focus();
                    MsgForm.ShowFrom("请输入编码");
                    return;
                }
                if (string.IsNullOrEmpty(this.txtBankName.Text))
                {
                    txtBankName.Focus();
                    MsgForm.ShowFrom("请输入名称");
                    return;
                }

                OK();
                if (bank != null)
                    this.Close();
                this.txtBankNo.Focus();
                this.LoadBank();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("btnOk_Click=>OK", ex.ToString());
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control c = sender as Control;
                if (c.TabIndex == this.txtBankName.TabIndex)
                {
                    btnOk_Click(sender, e);
                }
                else
                {
                    SendKeys.Send("{Tab}");
                }
            }
        }

        private void frmBankUpload_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtBankName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!KeyPressJudge.IsStr(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmBankUpload_Shown(object sender, EventArgs e)
        {
            if (bank == null)
                this.txtBankNo.Focus();
            else
                this.txtBankName.Focus();
        }

        private void txtBankNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Helper.KeyPressJudge.IsNum(e.KeyChar) == true ||
                Helper.KeyPressJudge.IsEnglish(e.KeyChar) == true ||
                e.KeyChar == (char)Keys.Back ||
                e.KeyChar == (char)Keys.Delete)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

    }
}
