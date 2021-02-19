using System;
using System.Drawing;
using System.Windows.Forms;

namespace IvyFront.Forms
{
    public partial class CusBalanceForm : Form
    {
        public CusBalanceForm(string cust_id)
        {
            InitializeComponent();

            IBLL.IClientBLL bll = new BLL.ClientBLL();
            decimal bal_amt = 0;
            decimal credit_amt = 0;
            bll.GetCusBalance(cust_id,out bal_amt,out credit_amt);

            IBLL.ISaleData bll2 = new BLL.SaleData();
            decimal no_pay = bll2.GetCusNoPayAmt(cust_id);

            lbl_bal_amt.Text = (bal_amt - no_pay).ToString("F2");
            lbl_credit_amt.Text = credit_amt.ToString("F2");
            lbl_use_amt.Text = (credit_amt + (bal_amt - no_pay)).ToString("F2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CusBalanceForm_Load(object sender, EventArgs e)
        {
        }

        private void CusBalanceForm_Shown(object sender, EventArgs e)
        {
        }

        private void CusBalanceForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 255)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }


    }
}