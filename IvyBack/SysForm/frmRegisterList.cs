using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;

namespace IvyBack.SysForm
{
    public partial class frmRegisterList : Form
    {
        public frmRegisterList()
        {
            InitializeComponent();

            this.dgv.AutoGenerateColumns = false;
        }
        IBLL.ISys bll = new BLL.SysBLL();

        private void LoadDGV()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"1","后台"},
                {"0","前台"}
            };

            this.机号类型.ValueMember = "Key";
            this.机号类型.DisplayMember = "Value";
            this.机号类型.DataSource = new BindingSource(dic, null);

            DataTable tb = bll.GetJH();

            this.dgv.DataSource = tb;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "14"))
                {
                    
                    return;
                }
                if (this.dgv.SelectedRows.Count < 1) return;

                if (YesNoForm.ShowFrom("确认要注销机号吗?") == DialogResult.Yes)
                {

                    Model.netsetup ns = new Model.netsetup()
                    {
                        jh = dgv.CurrentRow.Cells["机号"].Value.ToString(),
                        ip = dgv.CurrentRow.Cells["Ip地址"].Value.ToString(),
                        softpos = dgv.CurrentRow.Cells["机号类型"].Value.ToString(),
                    };

                    bll.DelJH(ns);

                    LoadDGV();
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegisterList_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDGV();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }

        }


    }
}
