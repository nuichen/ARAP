using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Model;
using IvyBack.Helper;
using System.Diagnostics;
namespace IvyBack
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            this.lblSoftName.Text = Helper.GlobalData.soft_name;

            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            if ("0".Equals(Helper.AppSetting.isMax))
                this.WindowState = FormWindowState.Normal;

            cons.MoveFormHelper.Bind(this);
        }

        // 防止闪屏
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }


        public DialogResult GoLogin()
        {
            this.ShowDialog();

            return this.DialogResult;
        }

        private void Login()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                sa_t_operator_i oper = new sa_t_operator_i()
                {
                    oper_id = this.txtOperId.Text,
                    oper_pw = this.txtPwd.Text
                };

                IBLL.IOper bll = new BLL.OperBLL();

                if (bll.Login(oper))
                {
                    Program.oper  = oper;

                    InI.Writue("app", "oper_id", oper.oper_id);

                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    new MsgForm("账号/密码错误").ShowDialog();
                }
            }
            catch (Exception e)
            {
                LogHelper.writeLog("frmLogin", e.ToString());
                MsgForm.ShowFrom(e);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }


        }

        private void myPanel3_Click(object sender, EventArgs e)
        {
            Control c = sender as Control;
            foreach (Control con in c.Controls)
            {
                if (con is TextBox)
                {
                    TextBox txt = con as TextBox;
                    txt.Focus();
                }
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Control con = sender as Control;

            if ("退格".Equals(con.Tag))
            {
                SendKeys.Send("{BACKSPACE}");
            }
            else if ("清空".Equals(con.Tag))
            {
                SendKeys.Send("^A");
                SendKeys.Send("{BACKSPACE}");
            }
            else
            {
                SendKeys.Send("{" + con.Tag + "}");
            }

        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (sender == txtPwd)
                {
                    Login();
                }

                SendKeys.Send("{TAB}");
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            this.txtOperId.Text = InI.ReadValue("app", "oper_id");
            this.txtOperId.SelectionStart = this.txtOperId.TextLength;
        }
    }
}