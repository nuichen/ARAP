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

namespace IvyBack.SysForm
{
    public partial class frmOperUpdate : Form
    {
        public frmOperUpdate()
        {
            InitializeComponent();

        }

        public sa_t_operator_i oper;

        private void LoadBranch()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = c as TextBox;
                    c.Text = "";
                }
            }

            if (oper != null)
            {
                txtOperId.Text = oper.oper_id;
                txtName.Text = oper.oper_name;
                txtPw.Text = oper.oper_pw;
                txtBranch.Text = oper.branch_no;
                cbIsBanch.Checked = oper.is_branch.Equals("0");
                checkIsStop.Checked = oper.oper_status.Equals("0");
                cbType.SelectedValue = oper.oper_type;
            }
            else
            {
                txtOperId.Text = new BLL.OperBLL().GetMaxCode();
                checkIsStop.Checked = false;
            }


        }
        private void OK()
        {
            IBLL.IOper bll = new BLL.OperBLL();
            if (oper == null || string.IsNullOrEmpty(oper.oper_id))
            {
                //添加
                sa_t_operator_i o = new sa_t_operator_i()
                {
                    oper_status = this.checkIsStop.Checked ? "0" : "1",
                    branch_no = string.IsNullOrEmpty(this.txtBranch.Text) ? "" : this.txtBranch.Text.Split('/')[0],
                    is_branch = this.cbIsBanch.Checked ? "1" : "0",
                    is_admin = "0",
                    oper_id = txtOperId.Text,
                    oper_name = txtName.Text,
                    oper_pw = sec.des(txtPw.Text),
                    update_time = DateTime.Now,
                    oper_type = this.cbType.SelectedValue.ToString()
                };
                bll.Add(o);
            }
            else
            {
                //修改
                oper.oper_name = txtName.Text;
                oper.oper_pw = !txtPw.Text.Equals(oper.oper_pw) ? sec.des(txtPw.Text) : txtPw.Text;
                oper.update_time = DateTime.Now;
                oper.oper_type = this.cbType.SelectedValue.ToString();
                oper.oper_status = this.checkIsStop.Checked ? "0" : "1";
                oper.branch_no = string.IsNullOrEmpty(this.txtBranch.Text) ? "" : this.txtBranch.Text.Split('/')[0];
                oper.is_branch = this.cbIsBanch.Checked ? "1" : "0";

                bll.Upload(oper);
            }

        }
        public void LoadCb()
        {
            //角色
            IBLL.IOper bll = new BLL.OperBLL();
            var lis = bll.GetOperTypeList();

            this.cbType.DisplayMember = "type_name";
            this.cbType.ValueMember = "oper_type";
            this.cbType.DataSource = lis;

            //机构
            IBLL.IBranch brbll = new BLL.BranchBLL();
            var branch = brbll.GetAllList("");

            this.txtBranch.Bind(branch, 300, 150, "branch_no", "branch_no:机构编码:80,branch_name:机构名称:150", "branch_no/branch_name->Text");
        }

        private void frmBranchUpload_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBranch();
                LoadCb();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("frmBranchUpload_Load=>load", ex.ToString());
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtName.Text))
                {
                    txtName.Focus();
                    MsgForm.ShowFrom("请输入名称");
                    return;
                }


                OK();
                if (oper != null)
                    this.Close();

                this.txtName.Focus();
                this.LoadBranch();
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
                if (c.TabIndex == this.txtBranch.TabIndex)
                {
                    btnOk_Click(sender, e);
                }
                else
                {
                    SendKeys.Send("{Tab}");
                }
            }
        }

        private void frmBranchUpload_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmBranchUpload_Shown(object sender, EventArgs e)
        {
            this.txtName.Focus();
        }

    }
}
