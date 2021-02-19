using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IvyBack.BLL;
using IvyBack.cons;
using IvyBack.Helper;
using IvyBack.IBLL;
using IvyBack.MainForm;
using Model;
using DataGrid = System.Windows.Forms.DataGrid;

namespace IvyBack.FinanceForm
{
    public partial class frmIncomeRevenue : Form
    {
        private string curr_month;
        public frmIncomeRevenue()
        {
            InitializeComponent();

            GlobalData.InitForm(this);

            Init(DateTime.Now);
        }

        IFinanceBLL bll = new FinanceBLL();

        private void Init(DateTime month)
        {
            curr_month = month.ToString("yyyy-MM");
            DataTable tb = new DataTable();
            tb.Columns.Add("in_date");
            tb.Columns.Add("in_amount");
            int days = ConvExtension.GetDays(month);
            for (int i = 0; i < days; i++)
            {
                var index = i + 1;
                var dr = tb.NewRow();
                tb.Rows.Add(dr);
                var in_date = month.ToString("yyyy-MM") + "-" + (index > 9 ? index.ToString() : "0" + index.ToString());
                dr["in_date"] = in_date;
                dr["in_amount"] = 0;
            }
            this.dgvItem.AddColumn("in_date", "日期", "", 200, 2, "yyyy-MM-dd", false);
            this.dgvItem.AddColumn("in_amount", "预计收入", "", 160, 3, "0.00", true);
            this.dgvItem.SetTotalColumn("in_amount");
            this.dgvItem.DataSource = tb;

            IBLL.ICommonBLL bll2 = new BLL.CommonBLL();
            var cust = bll2.GetAllCustList();

            txt_cust_id.Bind(cust, 350, 200, "supcust_no", "supcust_no:编号:80,sup_name:名称:200", "supcust_no/sup_name->Text");
            txt_cust_id.GetDefaultValue();
        }
        private void RefreshData()
        {
            DateTime time = this.dateTimePicker1.Value.ToDateTime();
            string cust_no = this.txt_cust_id.Text.Split('/')[0];
            var tb = this.dgvItem.DataSource;
            frmLoad.LoadWait(this,
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        DataTable dt = bll.GetIncomeRevenueList(cust_no, time.ToString("yyyy-MM"));

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in tb.Rows)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (row["in_date"].ToDateTime().Date == Conv.ToDateTime(dr["in_date"]).Date)
                                    {
                                        row["in_amount"] = Conv.ToDecimal(dr["in_amount"]);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow row in tb.Rows)
                            {
                                row["in_amount"] = 0.00;
                            }
                        }
                        this.BeginInvoke(new Action(() =>
                        {
                            dgvItem.DataSource = tb;
                        }));
                    }
                    catch (Exception e)
                    {
                        MsgForm.ShowFrom(e);
                    }
                    finally
                    {
                        this.Invoke(new Action(() => { frmLoad.LoadClose(this); }));
                    }
                }));

        }
        private void Save(bool Refresh = true)
        {
            List<rp_t_supcust_income_revenue> details = new List<rp_t_supcust_income_revenue>();

            Action<DataTable, String> detailAction = (tb, type) =>
             {
                 foreach (DataRow row in tb.Rows)
                 {
                     if (!string.IsNullOrEmpty(row["in_date"].ToString()) && row["in_amount"].ToDecimal() != 0)
                     {
                         rp_t_supcust_income_revenue detail = new rp_t_supcust_income_revenue()
                         {
                             in_date = Conv.ToDateTime(row["in_date"]),
                             in_amount = row["in_amount"].ToDecimal()
                         };
                         details.Add(detail);
                     }
                 }
             };

            detailAction.Invoke(this.dgvItem.DataSource, "1");
            DateTime time = this.dateTimePicker1.Value.ToDateTime();
            string cust_no = this.txt_cust_id.Text.Split('/')[0];
            bll.SaveIncomeRevenue(cust_no, time.ToString("yyyy-MM"), Program.oper.oper_id, details);

            MsgForm.ShowFrom("保存成功");
        }
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Control con = sender as Control;
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(230, 230, 230)), 1, 1, con.Width - 2, con.Height - 2);
        }

        private void txt_cust_id_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    RefreshData();
                    break;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (this.IsHandleCreated)
            {
                if (dateTimePicker1.Value.ToString("yyyy-MM") != curr_month)
                {
                    Init(dateTimePicker1.Value);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void frmIncomeRevenue_Shown(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F5:
                    RefreshData();
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            decimal num = this.txtNums.Text.ToDecimal();
            foreach (DataRow row in this.dgvItem.DataSource.Rows)
            {
                row["in_amount"] = num;
            }

            this.dgvItem.Refresh();
        }
    }
}
