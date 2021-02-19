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
    public partial class frmPayment : Form
    {
        public frmPayment()
        {
            InitializeComponent();

        }

        DataTable tb;
        private void LoadDataGrid()
        {
            this.dgvPay.AddColumn("pay_way", "编码", "", 100, 2, "");
            this.dgvPay.AddColumn("pay_name", "名称", "", 300, 1, "");
            this.dgvPay.AddColumn("visa_id", "现金银行代码", "", 150, 1, "");
            this.dgvPay.AddColumn("visa_nm", "现金银行名称", "", 160, 2, "");

            this.dgvPay.DataSource = Conv.Assign<bi_t_payment_info>();
        }
        private void LoadPay()
        {
            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        IBLL.IPayment bll = new BLL.PaymentBLL();
                        tb = bll.GetAllList();

                        this.dgvPay.DataSource = tb;
                        Cursor.Current = Cursors.Default;
                    });
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("SysSetting_Load", ex.ToString());
                    MsgForm.ShowFrom(ex);
                }
                Helper.GlobalData.windows.CloseLoad(this);
            });
            th.Start();

        }

        private void InI()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<bi_t_payment_info> pays = new List<bi_t_payment_info>()
            {
                new bi_t_payment_info(){pay_name="现金",pay_way="A",display="1",pay_flag="1"},
                new bi_t_payment_info(){pay_name="支票",pay_way="B",display="1",pay_flag="1"},
                new bi_t_payment_info(){pay_name="信用卡",pay_way="C",display="1",pay_flag="1"},
                new bi_t_payment_info(){pay_name="金卡",pay_way="D",display="1",pay_flag="1"},
                new bi_t_payment_info(){pay_name="现金收支",pay_way="E",display="1",pay_flag="1"},
                new bi_t_payment_info(){pay_name="购物券",pay_way="F",display="1",pay_flag="1"},
                new bi_t_payment_info(){pay_name="支付宝",pay_way="G",display="1",pay_flag="1"},
                new bi_t_payment_info(){pay_name="自动抹零",pay_way="H",display="1",pay_flag="1"},
                new bi_t_payment_info(){pay_name="手动抹零",pay_way="H",display="1",pay_flag="1"},
            };
            IBLL.IPayment bll = new BLL.PaymentBLL();
            foreach (bi_t_payment_info p in pays)
            {
                bll.Del(p);
                bll.Add(p);
            }
            LoadPay();
            Cursor.Current = Cursors.Default;
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadDataGrid();
                LoadPay();
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
            DataRow dr = this.dgvPay.CurrentRow();
            if (dr == null) return;
            var item = DB.ReflectionHelper.DataRowToModel<bi_t_payment_info>(dr);
            var frm = new frmPaymentUpdate();
            frm.payment = item;
            frm.ShowDialog();
            LoadPay();
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
                IBLL.IPayment bll = new BLL.PaymentBLL();
                var item = DB.ReflectionHelper.DataRowToModel<bi_t_payment_info>(dr);
                if (item.pay_flag.Equals("1"))
                {
                    MsgForm.ShowFrom("系统预留方式不可删除");
                    return;
                }
                bll.Del(item);
                LoadPay();
            }
        }

        private void tsbInI_Click(object sender, EventArgs e)
        {
            //初始化
            if (YesNoForm.ShowFrom("确认要初始化吗?") == DialogResult.Yes)
                InI();
        }

        private void tsbCreate_Click(object sender, EventArgs e)
        {

            if (!MyLove.PermissionsBalidation(this.Text, "01"))
            {
                
                return;
            }
            var frm = new frmPaymentUpdate();
            frm.ShowDialog();
            LoadPay();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPay_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            tsbUpload_Click(sender, e);
        }

        private void frmPayment_KeyDown(object sender, KeyEventArgs e)
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
            else if (e.KeyCode == Keys.F5)
            {
                tsbInI_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (tb == null || tb.Columns.Count < 1) return;

            if (!tb.Columns.Contains("pay_way")) return;

            var ts = tb.Select("pay_way like '%" + this.txtKeyword.Text + "%' or pay_name like '%" + this.txtKeyword.Text + "%'");

            DataTable dt = new DataTable();
            if (ts.Count() > 0)
            {
                dt = ts.CopyToDataTable();
            }
            this.dgvPay.DataSource = dt;
        }

    }
}
