using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient
{
    public partial class order_new : Form
    {
        BLL.IOrder bll;
        private string key = "";
        private int pageSize = 20;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        public order_new()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            LoadDataGrid();
            bll = new BLL.Order();
            //
            control.ClickActive.addActive(btn_first, 2);
            control.ClickActive.addActive(btn_prev, 2);
            control.ClickActive.addActive(btn_next, 2);
            control.ClickActive.addActive(btn_last, 2);

            refreshData();
        }

        // 防止闪屏
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void LoadDataGrid()
        {
            this.dg_data.AddColumn("is_check", "", "", 80, 2, "{0:,1:√}");
            this.dg_data.AddColumn("ord_id", "订单号", "", 130, 2, "");
            this.dg_data.AddColumn("create_time", "下单时间", "", 140, 2, "yyyy-MM-dd HH:mm");
            this.dg_data.AddColumn("reach_time", "送达时间", "", 100, 2, "");
            this.dg_data.AddColumn("mobile", "手机", "", 120, 1, "");
            this.dg_data.AddColumn("sname", "联系人", "", 120, 1, "");
            this.dg_data.AddColumn("enable_qty", "数量", "", 100, 3, "0.00");
            this.dg_data.AddColumn("enable_amount", "金额", "", 100, 3, "0.00");
            this.dg_data.AddColumn("status_str", "状态", "", 80, 2, "");

            this.dg_data.MergeCell = false;
            this.dg_data.DataSource = new DataTable();

        }

        private string GetStatus(string status,string send_status)
        {
            if (status == "0")
            {
                return "待审";
            }
            else if (status == "1")
            {
                if (send_status == "0")
                {
                    return "已审";
                }
                else if (send_status == "1")
                {
                    return "已送";
                }
            }
            else if (status == "2")
            {
                return "失效";
            }

            return "";
        }

        public void refreshData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //
                string k = "";
                bll.GetOrderNew(key, out k);
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
                Cursor.Current = Cursors.Default;
            }
        }

        private void pageChanged()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //
                var dt = bll.GetOrderNewDt(key, pageSize, pageIndex, out total);
                dt.Columns.Add("is_check");
                dt.Columns.Add("status_str");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["is_check"] = "1";
                        dr["status_str"] = GetStatus(dr["status"].ToString(), dr["send_status"].ToString());
                    }
                }
                this.dg_data.DataSource = dt;

                int n = pageIndex;
                int m = (int)Math.Ceiling((decimal)this.total / (decimal)this.pageSize);
                this.pageCount = m;
                this.Label1.Text = string.Format("第{0}页，共{1}页", n.ToString(), m.ToString());
                //
            }
            catch (Exception e)
            {
                Program.frmMsg(e.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
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

        private void btn_view_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                string ord_id = row["ord_id"].ToString();
                var ord = new body.wm_order();
                var lines = new DataTable();
                int un_read_num = 0;
                bll.GetOrder(ord_id, out ord, out lines, out un_read_num);
                var frm = new frmHand(ord, lines, "订单明细", un_read_num);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.refreshData();
                }
            }
        }

        private void btn_approve_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                string ord_id = row["ord_id"].ToString();
                bll.Pass(ord_id);
                this.refreshData();
            }
        }

        private void btn_all_approve_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.dg_data.DataSource != null)
                {
                    foreach (DataRow dr in this.dg_data.DataSource.Rows)
                    {
                        if (dr["is_check"].ToString() == "1") 
                        {
                            bll.Pass(dr["ord_id"].ToString());
                        }
                    }
                    this.refreshData();
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btn_reject_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                string ord_id = row["ord_id"].ToString();
                bll.Disable(ord_id);
                this.refreshData();
            }
        }

        private void dg_data_ClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if (row != null && column_name == "is_check") 
            {
                if (row["is_check"].ToString() == "1")
                {
                    row["is_check"] = "0";
                }
                else 
                {
                    row["is_check"] = "1";
                }
                this.dg_data.Refresh();
            }
        }

        private void dg_data_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if (row != null)
            {
                string ord_id = row["ord_id"].ToString();
                var ord = new body.wm_order();
                var lines = new DataTable();
                int un_read_num = 0;
                bll.GetOrder(ord_id, out ord, out lines, out un_read_num);
                var frm = new frmHand(ord, lines, "订单明细", un_read_num);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.refreshData();
                }
            }
        }
    }
}