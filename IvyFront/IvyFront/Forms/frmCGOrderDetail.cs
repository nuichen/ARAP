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
    public partial class frmCGOrderDetail : Form
    {
        private int pageSize = 10;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        private DataTable all_dt;
        public frmCGOrderDetail(string sheet_no, string trans_no)
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
            cons.ClickActive.addActive(pnlclose, 2);
            cons.ClickActive.addActive(pnl_first, 2);
            cons.ClickActive.addActive(pnl_prev, 2);
            cons.ClickActive.addActive(pnl_next, 2);
            cons.ClickActive.addActive(pnl_last, 2);
            //
            if (trans_no == "D")
            {
                lbl_title.Text = "采购入库单-" + sheet_no;
            }
            else 
            {
                lbl_title.Text = "销售退货入库单-" + sheet_no;
            }
            IBLL.ISaleData bll = new BLL.SaleData();
            all_dt = bll.GetSaleSheetDetail(sheet_no);
            int m = (int)Math.Ceiling((decimal)this.total / (decimal)this.pageSize);
            this.pageCount = m;
            lbl_order_count.Text = all_dt.Rows.Count.ToString();
            lbl_total_amt.Text = all_dt.Compute("sum(in_qty*valid_price)", "").ToString();

            this.pageChanged();
        }

        private void pageChanged()
        {
            if ((pageIndex - 1) * pageSize > all_dt.Rows.Count) 
            {
                return;
            }
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                this.dataGridView1.Rows.Clear();
                for (int i = (pageIndex - 1) * pageSize; i < pageIndex * pageSize; i++) 
                {
                    if (i < all_dt.Rows.Count) 
                    {
                        var index = this.dataGridView1.Rows.Add();
                        var row = this.dataGridView1.Rows[index];
                        row.Tag = all_dt.Rows[i]["sheet_sort"].ToString();
                        row.Cells[0].Value = all_dt.Rows[i]["sheet_sort"].ToString();
                        row.Cells[1].Value = all_dt.Rows[i]["item_subno"].ToString();
                        row.Cells[2].Value = all_dt.Rows[i]["item_name"].ToString();
                        row.Cells[3].Value = all_dt.Rows[i]["unit_no"].ToString();

                        row.Cells[4].Value = all_dt.Rows[i]["in_qty"].ToString();
                        row.Cells[5].Value = all_dt.Rows[i]["valid_price"].ToString();
                        row.Cells[6].Value = (Conv.ToDecimal(all_dt.Rows[i]["in_qty"]) * Conv.ToDecimal(all_dt.Rows[i]["valid_price"])).ToString("F2");
                    }
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

        private void pnl_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmXSOrderDetail_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 255)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void frmXSOrderDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            all_dt = null;
        }

    }
}
