using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IvyFront.Forms
{
    public partial class frmCGOrder : Form
    {
        private int pageSize = 10;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        private DataTable all_dt;
        public frmCGOrder()
        {
            InitializeComponent();
            //设置表格
            this.dataGridView1.Font = new Font("微软雅黑", 11);
            this.dataGridView1.AutoGenerateColumns = false;
            foreach (System.Windows.Forms.DataGridViewColumn col in this.dataGridView1.Columns)
            {
                col.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            //
            cons.ClickActive.addActive(pnl_close, 2);
            cons.ClickActive.addActive(pnl_first, 2);
            cons.ClickActive.addActive(pnl_prev, 2);
            cons.ClickActive.addActive(pnl_next, 2);
            cons.ClickActive.addActive(pnl_last, 2);
            //
            cons.ClickActive.addActive(pnldate1, 2);
            cons.ClickActive.addActive(pnldate2, 2);
            //
            pnldate1.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            pnldate2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            this.pnl_search_Click(btn_search, null);
        }

        private void pageChanged()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                
                this.dataGridView1.Rows.Clear();

                IBLL.ISaleData bll = new BLL.SaleData();
                all_dt = bll.GetInOutSheet(pnldate1.Text, pnldate2.Text, pageIndex, pageSize, out total);
                int m = (int)Math.Ceiling((decimal)this.total / (decimal)this.pageSize);
                this.pageCount = m;

                foreach (DataRow dr in all_dt.Rows) 
                {
                    var index = this.dataGridView1.Rows.Add();
                    var row = this.dataGridView1.Rows[index];
                    row.Tag = dr["sheet_no"].ToString();
                    row.Cells[0].Value = (index + 1).ToString();
                    row.Cells[1].Value = dr["sheet_no"].ToString();
                    row.Cells[2].Value = dr["sup_name"].ToString();

                    var pay_way = dr["pay_way"].ToString();
                    if (pay_way == "A") row.Cells[3].Value = "现金支付";
                    else if (pay_way == "W") row.Cells[3].Value = "微信支付";
                    else if (pay_way == "Z") row.Cells[3].Value = "支付宝支付";
                    else row.Cells[3].Value = "挂账/预付";

                    row.Cells[4].Value = Conv.ToDecimal(dr["total_amount"]).ToString("F2");
                    row.Cells[5].Value = Conv.ToDecimal(dr["inout_amount"]).ToString("F2");
                    row.Cells[6].Value = (dr["is_upload"].ToString() == "1" ? "已上传" : "未上传");
                    row.Cells[7].Value = dr["oper_date"].ToString();
                }
                
                this.Label1.Text = string.Format("第{0}页，共{1}页", this.pageIndex.ToString(), this.pageCount.ToString());
            }
            catch (Exception e)
            {
                new MsgForm(e.Message).ShowDialog();
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void pnl_first_Click(object sender, EventArgs e)
        {
            pageIndex = 1;
            this.pageChanged();
        }

        private void pnl_prev_Click(object sender, EventArgs e)
        {
            if (pageIndex > 1)
            {
                pageIndex -= 1;
            }
            this.pageChanged();
        }

        private void pnl_next_Click(object sender, EventArgs e)
        {
            if (pageIndex < pageCount)
            {
                pageIndex += 1;
            }
            this.pageChanged();
        }

        private void pnl_last_Click(object sender, EventArgs e)
        {
            pageIndex = pageCount;
            this.pageChanged();
        }

        private void pnldate1_Click(object sender, EventArgs e)
        {
            if (pnldate1.Text == "")
            {
                ShowDate(System.DateTime.Now.Year, System.DateTime.Now.Month, pnldate1);
            }
            else
            {
                System.DateTime d = Conv.ToDateTime(pnldate1.Text);
                ShowDate(d.Year, d.Month, pnldate1);
            }
        }

        private void pnldate2_Click(object sender, EventArgs e)
        {
            if (pnldate2.Text == "")
            {
                ShowDate(System.DateTime.Now.Year, System.DateTime.Now.Month, pnldate2);
            }
            else
            {
                System.DateTime d = Conv.ToDateTime(pnldate2.Text);
                ShowDate(d.Year, d.Month, pnldate2);
            }
        }

        private void ShowDate(int year, int month, System.Windows.Forms.Control c)
        {
            pnldate1.TabStop = true;
            pnldate1.Focus();

            pnlboard.BackColor = Color.LightGray;
            pnlboard.Size = new Size(this.Width, 250);
            pnlboard.Top = this.Height - pnlboard.Height;
            this.Controls.Add(pnlboard);
            pnlboard.TabStop = false;
            pnlboard.BringToFront();
            pnlboard.Tag = c;
            pnlboard.Controls.Clear();

            int linecount = 5;
            int columncount = 8;
            int lineheight = (pnlboard.Height - 1) / linecount;
            int columnwidth = (pnlboard.Width - 1) / columncount;
            int itop = 0;
            int ileft = 0;
            int days = DateTime.DaysInMonth(year, month);
            //显示月份
            if (1 == 1)
            {
                cons.ankey con = new cons.ankey();
                con.TabStop = false;
                con.Text = "<";
                con.Tag = year + "-" + month + "-1";
                con.Width = columnwidth - 2;
                con.Height = lineheight - 2;
                con.Top = itop;
                con.Left = ileft;
                con.MouseDown += this.date_click;
                ileft += columnwidth;
                cons.ClickActive.addActive(con, 2);
                pnlboard.Controls.Add(con);
            }
            if (1 == 1)
            {
                System.Windows.Forms.Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("微软雅黑", 12);
                lbl.Text = year + "年" + month + "月";
                lbl.Width = columnwidth * (columncount - 2);
                lbl.Height = lineheight - 2;
                lbl.Top = itop;
                lbl.Left = 1 * columnwidth;
                pnlboard.Controls.Add(lbl);
            }
            if (1 == 1)
            {
                cons.ankey con = new cons.ankey();
                con.TabStop = false;
                con.Text = ">";
                con.Tag = year + "-" + month + "-1";
                con.Width = columnwidth - 2;
                con.Height = lineheight - 2;
                con.Top = itop;
                con.Left = (columncount - 1) * columnwidth;
                con.MouseDown += this.date_click;
                ileft += columnwidth;
                cons.ClickActive.addActive(con, 2);
                pnlboard.Controls.Add(con);
            }

            //
            ileft = 0;
            itop = lineheight;
            for (int i = 1; i <= days; i++)
            {

                cons.ankey con = new cons.ankey();
                con.Tag = year + "-" + month + "-" + i.ToString();
                con.TabStop = false;

                con.Text = i.ToString();

                con.Width = columnwidth - 2;
                con.Height = lineheight - 2;
                con.Top = itop;
                con.Left = ileft;
                con.MouseDown += this.date_click;
                ileft += columnwidth;
                cons.ClickActive.addActive(con, 2);
                pnlboard.Controls.Add(con);
                if (i == columncount)
                {
                    itop += lineheight;
                    ileft = 0;
                }
                else if (i == columncount * 2)
                {
                    itop += lineheight;
                    ileft = 0;
                }
                else if (i == columncount * 3)
                {
                    itop += lineheight;
                    ileft = 0;
                }
                else if (i == columncount * 4)
                {
                    itop += lineheight;
                    ileft = 0;
                }
            }
            pnlboard.Visible = true;
        }

        private void date_click(object sender, EventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            if (con.Text == "<")
            {
                var d = Conv.ToDateTime(con.Tag);
                d = d.AddMonths(-1);
                ShowDate(d.Year, d.Month, (System.Windows.Forms.Control)pnlboard.Tag);
            }
            else if (con.Text == ">")
            {
                var d = Conv.ToDateTime(con.Tag);
                d = d.AddMonths(1);
                ShowDate(d.Year, d.Month, (System.Windows.Forms.Control)pnlboard.Tag);

            }
            else
            {
                System.Windows.Forms.Control c = (System.Windows.Forms.Control)pnlboard.Tag;
                c.Text = Conv.ToDateTime(con.Tag).ToString("yyyy-MM-dd");
                c.Refresh();
                pnlboard.Visible = false;

            }

        }

        Panel pnlboard = new Panel();
        private void con_click(object sender, EventArgs e)
        {
            var con = (System.Windows.Forms.Control)sender;
            string str = con.Text;
            if (pnlboard.Tag != null)
            {
                var con2 = (System.Windows.Forms.Control)pnlboard.Tag;
                con2.Focus();
            }
            if (str == "删除")
            {
                SendKeys.SendWait("{BACKSPACE}");
            }
            else if (str == "查询")
            {
                pnlboard.Visible = false;
                this.pnl_first_Click(pnl_first,null);
            }
            else
            {

                SendKeys.SendWait(con.Text);
            }

        }

        private void pnl_search_Click(object sender, EventArgs e)
        {
            pnlboard.Visible = false;

            IBLL.ISaleData bll = new BLL.SaleData();
            int order_count = 0;
            decimal total_amt = 0;
            decimal fact_amt = 0;
            bll.GetInOutSheetSum(pnldate1.Text, pnldate2.Text, out order_count, out total_amt,out fact_amt);
            lbl_order_count.Text = order_count.ToString();
            lbl_total_amt.Text = total_amt.ToString("F2");
            lbl_fact_amt.Text = fact_amt.ToString("F2");
            this.pnl_first_Click(pnl_first, null);
        }

        private void pnldate1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(pnldate1.Text, new Font("微软雅黑", 12), Brushes.Black, new PointF(5, 7));
        }

        private void pnldate2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(pnldate2.Text, new Font("微软雅黑", 12), Brushes.Black, new PointF(5, 7));
        }

        private void frmSaleFlow_Click(object sender, EventArgs e)
        {
            pnlboard.Visible = false;
        }

        private void pnl_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            pnlboard.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    var row = dataGridView1.CurrentRow;
                    if (row != null && row.Tag != null && row.Tag.ToString() != "")
                    {
                        frmCGOrderDetail frm = new frmCGOrderDetail(row.Tag.ToString(),"A");
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void frmXSOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            all_dt = null;
        }

    }
}
