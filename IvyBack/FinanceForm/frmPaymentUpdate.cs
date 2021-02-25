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
    public partial class frmPaymentUpdate : Form
    {
        System.Data.DataTable tb_bank;
        public frmPaymentUpdate()
        {
            InitializeComponent();
            //
            IBLL.IBank bll = new BLL.BankBLL();
            tb_bank = bll.GetAllList();
            txtvisa.Bind(tb_bank, 300, 200, "visa_id", "visa_id:现金银行编码:120,visa_nm:现金银行名称:150", "visa_id/visa_nm->Text");
        }

        public bi_t_payment_info payment;

        private void LoadPay()
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

            if (payment != null)
            {
                txtPayName.Text = payment.pay_name;
                txtPaynum.Text = payment.pay_way;
                
                var dr = this.tb_bank.Select().Where(r => r["visa_id"].Equals(payment.visa_id)).SingleOrDefault();
                if (dr != null)
                {
                    this.txtvisa.Text = payment.visa_id + "/" + dr["visa_nm"].ToString();
                    
                }
                if (payment.is_sup_default=="1")
                {
                    this.myCheckBox1.Checked = true;
                }
                else
                {
                    this.myCheckBox1.Checked = false;
                }
                txtPaynum.ReadOnly = true;
                //checkIsStop.Checked = payment.display.Equals("0");
            
            }
            else
            {
                txtPayName.Text = "";
                txtPaynum.Text = "";
                txtPaynum.ReadOnly = false;
               // checkIsStop.Checked = false;
            }
        }
        private void OK()
        {
            IBLL.IPayment bll = new BLL.PaymentBLL();
            if (payment == null || string.IsNullOrEmpty(payment.pay_way))
            {
                //添加
                bi_t_payment_info sup = new bi_t_payment_info()
                {
                    display = "1",//checkIsStop.Checked ? "0" : "1",
                    pay_flag = "0",
                    pay_name = txtPayName.Text,
                    pay_way = txtPaynum.Text,                  
                    visa_id = txtvisa.Text.Split('/')[0]//visa_id=txtvisa.Text.Split('/')[0]
                };
                if (this.myCheckBox1.Checked == true)
                {
                    sup.is_sup_default = "1";
                }
                else
                {
                    sup.is_sup_default = "0";
                }
                bll.Add(sup);
            }
            else
            {
                //修改
                payment.display = "1";//this.checkIsStop.Checked ? "0" : "1";
                payment.pay_name = this.txtPayName.Text;
                payment.visa_id = txtvisa.Text.Split('/')[0];
                if (this.myCheckBox1.Checked == true)
                {
                    payment.is_sup_default = "1";
                }
                else
                {
                    payment.is_sup_default = "0";
                }
                bll.Upload(payment);
            }

        }

        private void frmPaymentUpload_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPay();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("frmPaymentUpload_Load=>load", ex.ToString());
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtPaynum.Text))
                {
                    txtPaynum.Focus();
                    MsgForm.ShowFrom("请输入编码");
                    return;
                }
                if (string.IsNullOrEmpty(this.txtPayName.Text))
                {
                    txtPayName.Focus();
                    MsgForm.ShowFrom("请输入名称");
                    return;
                }
                if (string.IsNullOrEmpty(this.txtvisa.Text))
                {
                    txtPayName.Focus();
                    MsgForm.ShowFrom("请选择现金银行");
                    return;
                }
                OK();
                if ( payment != null)
                    this.Close();
                this.txtPaynum.Focus();
                this.LoadPay();
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
                if (c.TabIndex == this.txtPayName.TabIndex)
                {
                    btnOk_Click(sender, e);
                }
                else
                {
                    SendKeys.Send("{Tab}");
                }
            }
        }

        private void frmPaymentUpload_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmPaymentUpload_Shown(object sender, EventArgs e)
        {
            if (payment == null)
                this.txtPaynum.Focus();
            else
                this.txtPayName.Focus();
        }

        private void txtPaynum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Delete)
            {

            }
            else if (e.KeyChar == (char)Keys.Back)
            {

            }
            else if (Helper.KeyPressJudge.IsNum(e.KeyChar) || Helper.KeyPressJudge.IsEnglish(e.KeyChar))
            {


            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPayName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Delete)
            {

            }
            else if (e.KeyChar == (char)Keys.Back)
            {

            }
            else if (Helper.StringHelper.GetLength(txtPayName.Text) > 20)
            {
                e.Handled = true;
            }
            else if (Helper.KeyPressJudge.IsStr(e.KeyChar) == true)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

    }
}
