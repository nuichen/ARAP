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
    public partial class frmIncome : Form
    {
        public frmIncome()
        {
            InitializeComponent();

        }

        DataTable tb;
        private void init_datagrid()
        {
            this.dgvPay.AddColumn("pay_way", "编码", "", 60, 1, "");
            this.dgvPay.AddColumn("pay_name", "名称", "", 200, 1, "");
            this.dgvPay.AddColumn("pay_flag", "类型", "", 100, 2, "{0:不定,1:供应商,2:客户,3:系统自动生成,4:其他收入,5:其他支出}");
            this.dgvPay.AddColumn("other2", "科目名称", "", 200, 1, "");
            //this.dgvPay.AddColumn("if_CtFix", "合同扣项", "", 90, 2, "{0:否,1:是}");
            //this.dgvPay.AddColumn("is_account", "现金收支", "", 90, 2, "{0:否,1:是}");
            //this.dgvPay.AddColumn("account_flag", "现金收支方向", "", 100, 2, "{0:收入,1:支出}");
            //this.dgvPay.AddColumn("is_pay", "参与应收应付", "", 150, 2, "{0:不参与,1:应收,2:应付}");
            //this.dgvPay.AddColumn("pay_kind", "应收应付增减", "", 150, 2, "{0:减少,1:增加}");
            //this.dgvPay.AddColumn("is_profit", "参与利润核算", "", 120, 2, "{0:否,1:是}");
            //this.dgvPay.AddColumn("profit_type", "利润增减方向", "", 150, 2, "{0:减少,1:增加}");
            //this.dgvPay.AddColumn("auto_cashsheet", "自动生成现金流水", "", 150, 2, "{0:否,1:是}");
            //this.dgvPay.AddColumn("path", "锁定", "", 60, 2, "{0:否,1:是}");
            this.dgvPay.AddColumn("pay_memo", "备注", "", 200, 2, "");
            this.dgvPay.DataSource = Conv.Assign<bi_t_sz_type>();
        }

        private void init_data()
        {
            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        IBLL.IFinanceBLL bll = new BLL.FinanceBLL();
                        tb = bll.GetSZTypeList();

                        this.dgvPay.DataSource = tb;
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
        private void init()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                init_data();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom("初始化收支类型异常[" + ex.Message + "]");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "14"))
            {
                
                return;
            }
            DataRow dr = this.dgvPay.CurrentRow();
            if (dr == null) return;
            var item = DB.ReflectionHelper.DataRowToModel<bi_t_sz_type>(dr);
            if (item != null)
            {
                var frm = new frmIncomeEdit(item.pay_way, item);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Yes) init_data();
            }

        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "02"))
            {
                
                return;
            }
            DataRow dr = this.dgvPay.CurrentRow();
            if (dr == null) return;

            if (YesNoForm.ShowFrom("确认要删除吗？") == DialogResult.Yes)
            {
                IBLL.IFinanceBLL bll = new BLL.FinanceBLL();
                var item = DB.ReflectionHelper.DataRowToModel<bi_t_sz_type>(dr);
                if (item != null)
                {
                    try
                    {
                        bll.DeleteSZType(item.pay_way);
                        init_data();
                    }
                    catch (Exception ex)
                    {
                        MsgForm.ShowFrom("删除收支类型异常[" + ex.Message + "]");
                    }
                }
            }
        }


        private void tsbInI_Click(object sender, EventArgs e)
        {
            //初始化
            if (YesNoForm.ShowFrom("确认要初始化吗?") == DialogResult.Yes) init();
        }

        private void tsbCreate_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "01"))
            {
                
                return;
            }
            var frm = new frmIncomeEdit("");
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.Yes) init_data();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPay_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            tsbEdit_Click(sender, e);
        }

        private void frmPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                tsbCreate_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                tsbEdit_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                tsbDel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                tsbInI_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmIncome_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                init_datagrid();
                init_data();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("frmIncome_Load", ex.ToString());
                MsgForm.ShowFrom(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

    }
}
