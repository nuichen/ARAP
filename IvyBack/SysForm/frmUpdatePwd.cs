using IvyBack.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyBack.SysForm
{
    public partial class frmUpdatePwd : Form
    {
        public frmUpdatePwd()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in this.Controls)
                {
                    if (c is TextBox)
                    {
                        var txt = c as TextBox;
                        if (!txt.Name.Equals(txtOldPwd.Name) && string.IsNullOrEmpty(c.Text))
                        {
                            txt.Focus();
                            MsgForm.ShowFrom("请补全信息！");
                            return;
                        }
                    }
                }

                if (!this.txtNewPwd.Text.Equals(this.txtNewPwd2.Text))
                {
                    MsgForm.ShowFrom("两次密码不一样！");
                    return;
                }

                IBLL.IOper bll = new BLL.OperBLL();
                bll.UploadPw(Program.oper.oper_id, this.txtOldPwd.Text, this.txtNewPwd2.Text);

                MsgForm.ShowFrom("修改成功，请重新登录");
                Application.Restart();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void txtOldPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Helper.KeyPressJudge.IsStr(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
