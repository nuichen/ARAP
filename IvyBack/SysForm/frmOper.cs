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
using System.Threading;

namespace IvyBack.SysForm
{
    public partial class frmOper : Form
    {
        public frmOper()
        {
            InitializeComponent();

            Helper.GlobalData.InitForm(this);
        }
        DataTable tb;
        private void LoadDataGrid()
        {
            this.dgvOper.AddColumn("oper_id", "操作员编号", "", 80, 1, "");
            this.dgvOper.AddColumn("oper_type", "角色", "", 80, 1, "");
            this.dgvOper.AddColumn("oper_name", "操作员", "", 80, 1, "");
            this.dgvOper.AddColumn("密码", "密码", "", 80, 1, "");
            this.dgvOper.AddColumn("oper_status", "状态", "", 80, 2, "{0:已禁用,1:已启用,2:已禁用}");

            this.dgvOper.DataSource = Conv.Assign<sa_t_operator_i>();
        }

        private void LoadTv()
        {
            this.tv.Nodes.Clear();

            TreeNode tn = new TreeNode("全部操作员");
            tn.Tag = "";

            IBLL.IOper bll = new BLL.OperBLL();
            var list = bll.GetOperTypeList();

            foreach (var type in list)
            {
                TreeNode n = new TreeNode(type.type_name + "[" + type.oper_type + "]");
                n.Tag = type;
                tn.Nodes.Add(n);
            }

            this.tv.Nodes.Add(tn);
            this.tv.ExpandAll();
        }

        public void LoadOper()
        {
            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    IBLL.IOper bll = new BLL.OperBLL();
                    tb = bll.GetOpers();

                    tb.Columns.Add("密码");

                    foreach (DataRow dr in tb.Rows)
                    {
                        var i = dr["oper_pw"].ToString().Length;
                        dr["密码"] = new string('*', i);
                    }

                    this.dgvOper.Invoke((MethodInvoker)delegate
                    {
                        tv_AfterSelect(null, null);
                    });

                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("LoadOper", ex.ToString());
                    MsgForm.ShowFrom(ex);
                }
                Helper.GlobalData.windows.CloseLoad(this);

            });
            th.Start();
        }

        private void frmBranch_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadDataGrid();
                LoadTv();
                LoadOper();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                LogHelper.writeLog("frmBranch_Load=>load", ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (this.tv.SelectedNode == null)
                    return;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                if (tb == null || tb.Rows.Count < 1)
                    LoadOper();

                if (this.tv.SelectedNode.Tag == null ||
                    string.IsNullOrEmpty(this.tv.SelectedNode.Tag.ToString()) ||
                    this.tv.SelectedNode.Text.IndexOf("系统管理员") > -1 ||
                    this.tv.SelectedNode.Text.IndexOf("后台管理员") > -1 ||
                    this.tv.SelectedNode.Text.IndexOf("前台管理员") > -1)
                {
                    this.tsbDelType.Enabled = false;
                    this.tsbChangeType.Enabled = false;
                }
                else
                {
                    this.tsbDelType.Enabled = true;
                    this.tsbChangeType.Enabled = true;
                }

                if (null != this.tv.SelectedNode.Tag && !string.IsNullOrEmpty(this.tv.SelectedNode.Tag.ToString()))
                {
                    sa_t_oper_type type = (sa_t_oper_type)this.tv.SelectedNode.Tag;
                    var rows = tb.Select("oper_type = '" + type.oper_type + "'");
                    if (rows.Length != 0)
                    {
                        DataTable tbs = rows.CopyToDataTable();
                        this.dgvOper.DataSource = tbs;
                    }
                    else
                    {
                        this.dgvOper.DataSource = tb.Clone();
                    }
                }
                else
                {
                    this.dgvOper.DataSource = tb;
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }


        }

        private void tsbUpload_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "14"))
            {

                return;
            }
            DataRow dr = this.dgvOper.CurrentRow();
            if (dr == null) return;
            var oper = DB.ReflectionHelper.DataRowToModel<sa_t_operator_i>(dr);
            var frm = new frmOperUpdate();
            frm.oper = oper;
            frm.ShowDialog();

            tv_AfterSelect(null, null);
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "02"))
            {

                return;
            }
            DataRow dr = this.dgvOper.CurrentRow();
            if (dr == null) return;

            if (YesNoForm.ShowFrom("确认要删除吗？") == DialogResult.Yes)
            {
                IBLL.IOper bll = new BLL.OperBLL();
                var b = DB.ReflectionHelper.DataRowToModel<sa_t_operator_i>(dr);
                bll.Del(b.oper_id);

                tv_AfterSelect(null, null);
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "01"))
            {

                return;
            }
            var frm = new frmOperUpdate();
            frm.ShowDialog();

            tv_AfterSelect(null, null);
        }

        private void dgvBranch_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            tsbUpload_Click(sender, e);
        }

        private void frmBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                tsbAdd_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                tsbUpload_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                tsbDel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                tsbIniPwd_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void tsbIniPwd_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "14"))
            {

                return;
            }
            if (this.dgvOper.CurrentRow() == null) return;

            sa_t_operator_i oper = DB.ReflectionHelper.DataRowToModel<sa_t_operator_i>(dgvOper.CurrentRow());

            if (YesNoForm.ShowFrom("确认要重置操作员:" + oper.oper_name + "的密码吗?") == DialogResult.Yes)
            {
                IBLL.IOper bll = new BLL.OperBLL();
                bll.ResetPWD(oper.oper_id, "1234");
                MsgForm.ShowFrom("重置完成,默认密码:1234");

                LoadOper();
                tv_AfterSelect(null, null);
            }
        }


        #region 角色
        private void tsbAddType_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "01"))
                {

                    return;
                }
                var frm = new frmChangeOperType();
                if (frm.ChangeOperType(null) == DialogResult.Yes)
                {
                    LoadTv();
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbChangeType_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "14"))
                {

                    return;
                }
                var frm = new frmChangeOperType();
                if (frm.ChangeOperType((sa_t_oper_type)this.tv.SelectedNode.Tag) == DialogResult.Yes)
                {
                    LoadTv();
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbDelType_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "02"))
                {

                    return;
                }
                if (YesNoForm.ShowFrom("确认要删除吗?") == DialogResult.Yes)
                {
                    IBLL.IOper bll = new BLL.OperBLL();
                    bll.DelOperType((sa_t_oper_type)this.tv.SelectedNode.Tag);

                    LoadTv();
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }


        #endregion

        private void tsbFlush_Click(object sender, EventArgs e)
        {
            try
            {
                LoadOper();
                tv_AfterSelect(null, null);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }
    }
}
