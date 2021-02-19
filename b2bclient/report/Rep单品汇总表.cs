using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.report
{
    public partial class Rep单品汇总表 : Form
    {
        private string key = "";
        private int pageSize = 20;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        public Rep单品汇总表()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            LoadDataGrid();
            
            //
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void LoadDataGrid()
        {
            this.dg_data.AddColumn("编号", "编号", "", 100, 2, "");
            this.dg_data.AddColumn("品名", "品名", "", 200, 1, "");
            this.dg_data.AddColumn("单位", "单位", "", 100, 1, "");
            this.dg_data.AddColumn("规格", "规格", "", 100, 1, "");
            this.dg_data.AddColumn("单数", "单数", "", 100, 3, "");
            this.dg_data.AddColumn("数量", "数量", "", 100, 3, "0.00");
            this.dg_data.AddColumn("金额", "金额", "", 100, 3, "0.00");

            this.dg_data.MergeCell = false;
            //this.dg_data.SetTotalColumn("单数,数量,金额");
            this.dg_data.DataSource = new DataTable();

        }

        public void refreshData()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                BLL.IRep单品汇总表 bll = new BLL.Rep单品汇总表();
                string k = "";
                bll.GetKey(key, dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"), txtgoodsname.Text, out k);
                key = k;
                //
                this.btn_first_Click(btn_first, null);
            }
            catch (Exception e)
            {
                Program.frmMsg(e.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }

        }

        private void pageChanged()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                BLL.IRep单品汇总表 bll = new BLL.Rep单品汇总表();
                var dt = bll.GetData(key, pageSize, pageIndex, out total,"编号","单数,数量,金额");
                //
                dt_excel = dt;
                this.dg_data.DataSource = dt;

                int n = pageIndex;
                int m = (int)Math.Ceiling((decimal)this.total / (decimal)this.pageSize);
                this.pageCount = m;
                this.Label1.Text = string.Format("第{0}页，共{1}页", n.ToString(), m.ToString());
            }
            catch (Exception e)
            {
                Program.frmMsg(e.Message);

            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            pageIndex = pageCount;
            this.pageChanged();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (pageIndex < pageCount)
            {
                pageIndex += 1;
            }
            this.pageChanged();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (pageIndex > 1)
            {
                pageIndex -= 1;
            }
            this.pageChanged();
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            pageIndex = 1;
            this.pageChanged();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            this.refreshData();
        }

        DataTable dt_excel = null;
        private void btn_export_Click(object sender, EventArgs e)
        {
            var f = new SaveFileDialog();
            f.Filter = "*.xls|*.xls";
            if (dt_excel == null)
            {
                MessageBox.Show("请先查询再导出！");
            }
            else if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BLL.IRep单品汇总表 bll = new BLL.Rep单品汇总表();
                dt_excel = bll.GetData(key, 10000, 1, out total, "编号", "单数,数量,金额");
                ToExcel.toExcel(dt_excel, f.FileName);
            }
        }

        private void Rep单品汇总表_FormClosed(object sender, FormClosedEventArgs e)
        {
            dt_excel = null;
        }

    }
}
