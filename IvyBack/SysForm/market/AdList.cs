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
    public partial class AdList : Form
    {
        private int pageSize = 20;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        public AdList()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            LoadDataGrid();
            try
            {
                //
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
            this.dg_data.AddColumn("ad_id", "广告流水号", "", 160, 2, "");
            this.dg_data.AddColumn("ad_name", "广告标题", "", 400, 2, "");
            this.dg_data.AddColumn("ad_type", "广告类型", "", 100, 2, "{0:微商城广告,1:PC商城广告}");

            this.dg_data.MergeCell = false;
            this.dg_data.DataSource = new DataTable();
        }

        private void pageChanged()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                IAd bll = new Ad();
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

        private void pnl_add_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "01"))
            {

                return;
            }
            var frm = new AdEdit();
            frm.ShowDialog();
            pageChanged();
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


        private void pnl_member_ad_Click(object sender, EventArgs e)
        {
            MemberAdEdit ad = new MemberAdEdit();
            ad.ShowDialog();
        }

        private void btn_pc_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "01"))
            {

                return;
            }
            PCAdEdit ad = new PCAdEdit();
            ad.ShowDialog();
            this.btn_first_Click(btn_first, null);
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
                var ad_id = row["ad_id"].ToString();
                if (row["ad_type"].ToString() == "1")
                {
                    var frm = new PCAdEdit(ad_id);
                    frm.ShowDialog();
                }
                else
                {
                    var frm = new AdEdit(ad_id);
                    frm.ShowDialog();
                }
                pageChanged();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "02"))
            {

                return;
            }
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                var frm = new YesNoForm("确认要删除该行?");
                if (frm.ShowDialog() == DialogResult.Yes)
                {
                    var ad_id = row["ad_id"].ToString();
                    IAd bll = new Ad();
                    bll.Delete(ad_id);
                    pageChanged();
                }
            }

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            pageIndex = 1;
            this.pageChanged();
        }

    }
}
