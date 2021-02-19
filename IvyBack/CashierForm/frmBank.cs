using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using Model;
using System.Threading;
namespace IvyBack.FinanceForm
{
    public partial class frmBank : Form
    {
        public frmBank()
        {
            InitializeComponent();

            GlobalData.InitForm(this);
        }

        DataTable tb;
        private void LoadDataGrid()
        {
            this.dgvBank.AddColumn("visa_id", "编码", "", 100, 2, "");
            this.dgvBank.AddColumn("visa_nm", "名称", "", 300, 1, "");
            this.dgvBank.AddColumn("状态", "状态", "", 160, 2, "");

            this.dgvBank.DataSource = Conv.Assign<bi_t_bank_info>();
        }
        private void LoadBank()
        {
            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        IBLL.IBank bll = new BLL.BankBLL();
                        tb = bll.GetAllList();

                        this.dgvBank.DataSource = tb;
                        Cursor.Current = Cursors.Default;
                    });
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("frmDept=>LoadItemCls", "获取商品分类出错!");
                    MsgForm.ShowFrom(ex);
                }
                Helper.GlobalData.windows.CloseLoad(this);
            });
            th.Start();
        }

        private void frmBank_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                LoadDataGrid();
                LoadBank();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("frmPayment_Load", ex.ToString());
                MsgForm.ShowFrom(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }



        private void tsbUpload_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "14"))
            {
                
                return;
            }
            DataRow dr = this.dgvBank.CurrentRow();
            if (dr == null) return;
            var bank = DB.ReflectionHelper.DataRowToModel<bi_t_bank_info>(dr);
            var frm = new frmBankUpdate();
            frm.bank = bank;
            frm.ShowDialog();
            LoadBank();
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "02"))
                {
                    
                    return;
                }
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                DataRow dr = this.dgvBank.CurrentRow();
                if (dr == null) return;

                if (YesNoForm.ShowFrom("确认要删除吗？") == DialogResult.Yes)
                {
                    IBLL.IBank bll = new BLL.BankBLL();
                    var bank = DB.ReflectionHelper.DataRowToModel<bi_t_bank_info>(dr);
                    bll.Del(bank);
                    LoadBank();
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

        private void tsbCreate_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "01"))
            {
                
                return;
            }
            var frm = new frmBankUpdate();
            frm.ShowDialog();
            LoadBank();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvBank_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            tsbUpload_Click(sender, e);
        }

        private void frmBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                tsbCreate_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                tsbUpload_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                tsbDel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "18"))
            {

                return;
            }
            if (tb == null || tb.Columns.Count < 1) return;

            if (!tb.Columns.Contains("visa_id")) return;

            var ts = tb.Select("visa_id like '%" + this.txtKeyword.Text + "%' or visa_nm like '%" + this.txtKeyword.Text + "%'");

            DataTable dt = new DataTable();
            if (ts.Count() > 0)
            {
                dt = ts.CopyToDataTable();
            }
            this.dgvBank.DataSource = dt;
        }

    }
}
