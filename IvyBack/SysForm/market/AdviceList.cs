using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.BLL.OnLine;
using IvyBack.Helper;
using IvyBack.IBLL.OnLine;

namespace IvyBack.SysForm.market
{
    public partial class AdviceList : Form
    {
        private int pageSize = 20;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;

        public AdviceList()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            LoadDataGrid();
            try
            {
                this.btn_search_Click(btn_search, null);
            }
            catch (Exception ex)
            {
                var frm = new MsgForm(ex.Message);
                frm.ShowDialog();
            }
        }

        private void LoadDataGrid()
        {
            this.dg_data.AddColumn("av_id", "流水号", "", 120, 2, "");
            this.dg_data.AddColumn("nickname", "顾客昵称", "", 120, 1, "");
            this.dg_data.AddColumn("ask_date", "反馈日期", "", 100, 2, "yyyy-MM-dd");
            this.dg_data.AddColumn("use_ask", "反馈内容", "", 300, 1, "");
            this.dg_data.AddColumn("mc_reply", "答复内容", "", 300, 1, "");

            this.dg_data.MergeCell = false;
            this.dg_data.DataSource = new DataTable();
        }

        private void pageChanged()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                IAdvice bll = new Advice();
                var dt = bll.GetDt(pageSize, pageIndex, out total);
                this.dg_data.DataSource = dt;
                int n = pageIndex;
                int m = (int)Math.Ceiling((decimal)this.total / (decimal)this.pageSize);
                this.pageCount = m;
                this.Label1.Text = string.Format("第{0}页，共{1}页", n.ToString(), m.ToString());
            }
            catch (Exception e)
            {
                var frm = new MsgForm(e.Message);
                frm.ShowDialog();
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
            btn_first_Click(btn_first,null);
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "14"))
            {

                return;
            }
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                var av_id = row["av_id"].ToString();
                var frm = new AdviceReply(av_id);
                frm.ShowDialog();
                pageChanged();
            }
        }
    }
}
