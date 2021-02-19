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
using IvyBack.BLL;
using IvyBack.IBLL;
using Model.StaticType;
using System.Security.Cryptography.X509Certificates;

namespace IvyBack.PaymentForm
{
    public partial class frmNoticeSelect : Form
    {
        string cus;
        public frmNoticeSelect(string cus)
        {
            InitializeComponent();
            this.cus = cus;
            Helper.GlobalData.InitForm(this);

        }
        private int _runType = 0;//0:供应商 1:客户
        public int runType
        {
            get
            {
                return _runType;
            }
            set
            {
                if (value == 1)
                {
                    //this.Text = "客户档案";
                    //this.cbShowStopSup.Text = "显示已停止往来的客户";
                    //this.tsmiSupBindItem.Visible = false;
                }
                else
                {

                }
                _runType = value;
            }
        }
        Page<ot_supcust_beginbalance> page = new Page<ot_supcust_beginbalance>();
        IBLL.IARAP_SCPaymentBLL bll = new BLL.ARAP_SCPaymentBLL();
        DataTable tb;
        private void LoadDataGrid()
        {
            //dgvSup.AddColumn("approve_flag", "审核状态", "", 60, 2, "{0:未审核 ,1:已审核}");
            dgvSup.AddColumn("sheet_no", "单号", "", 120, 1, "");
            if (runType == 1)
            {
                dgvSup.AddColumn("supcust_no", "客户", "", 100, 2, "");
                dgvSup.AddColumn("sup_name", "客户名称", "", 150, 1, "");//1
            }
            else
            {
                dgvSup.AddColumn("supcust_no", "供应商", "", 100, 2, "");
                dgvSup.AddColumn("sup_name", "供应商名称", "", 150, 1, "");//1
            }

            dgvSup.AddColumn("total_amount", "单据金额", "", 150, 3, "0.00");
            dgvSup.AddColumn("oper_name_a", "操作员", "", 150, 1, "");//1
            dgvSup.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd");
            dgvSup.AddColumn("approve_man_a", "审核人", "", 150, 1, "");//1
            dgvSup.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd");
            //dgvSup.AddColumn("account_due_date", "账期截止日期", "", 100, 1, "yyyy-MM-dd");
            dgvSup.AddColumn("remark", "备注1", "", 200, 1, "");
            dgvSup.IsSelect = true;
            dgvSup.MergeCell = false;
            this.dgvSup.DataSource = new DataTable();
        }
      
        //private List<bi_t_region_info> region_lis;
        public int is_state = 0;//0结算单选择 1账期通知单选择
       
        public DataTable tb1;
        private void LoadSup()
        {
            string is_cs = runType == 1 ? "C" : "S";
            this.dgvSup.DataSource=bll.GetNoticeList(DateTime.Now.AddDays(-30), DateTime.Now, cus, is_cs, "");
            dgvSup.Refresh();
        }

     
     

        private void frmSupcust_Load(object sender, EventArgs e)
        {
            try
            {
                
                LoadDataGrid();
                this.dateTextBox1.Text = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                this.dateTextBox2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                LoadSup();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                LogHelper.writeLog("frmSupcust=>load", ex.ToString());
            }
            finally
            {

            }
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.cb.SelectedValue != null)
            //    LoadTv();
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string is_cs = runType == 1 ? "C" : "S";
            this.dgvSup.DataSource = bll.GetNoticeList(dateTextBox1.Value, dateTextBox2.Value, cus, is_cs, txtKeyword.Text.Trim());
            dgvSup.Refresh();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            page.HomPage();
            LoadSup();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            page.PrePage();
            LoadSup();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page.NextPage();
            LoadSup();
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            page.TraPage();
            LoadSup();
        }



        private void tsbDel_Click(object sender, EventArgs e)
        {
          
        }

        private void tsbUpload_Click(object sender, EventArgs e)
        {
           
        }

        private void cbShowStopSup_CheckedChanged(object sender, EventArgs e)
        {
            LoadSup();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_DoubleClink(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            //tsbUpload_Click(sender, e);
        }

        private void frmSupcust_KeyDown(object sender, KeyEventArgs e)
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
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void tsmiSupBindItem_Click(object sender, EventArgs e)
        {
            //frmSupBindItem frm = new frmSupBindItem();
            //GlobalData.windows.ShowForm(frm);
        }

        private void tsbImportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_runType == 0)
                {
                    ImportSupExcel();
                }
                else
                {
                    ImportCusExcel();
                }

            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }

        private void ImportCusExcel()
        {
           
        }
        private void ImportSupExcel()
        {
            
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
        public DialogResult ShowPayment(out List<DataRow> rowLis)
        {

            this.ShowDialog();
            rowLis = this.dgvSup.GetSelectDatas();
            return this.DialogResult;



        }
    }
}
