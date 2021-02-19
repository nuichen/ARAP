using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyFront.Helper;

namespace IvyFront.Forms
{
    public partial class frmGoodsCls : Form
    {
        private int pageSize = 10;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        private List<Model.bi_t_item_cls> all_list = new List<Model.bi_t_item_cls>();
        public frmGoodsCls()
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
        }

        private void pageChanged()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                this.dataGridView1.Rows.Clear();
                var lst = all_list.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();

                foreach (Model.bi_t_item_cls item in lst)
                {
                    var index = this.dataGridView1.Rows.Add();
                    var row = this.dataGridView1.Rows[index];
                    row.Tag = item.item_clsno;
                    row.Cells[0].Value = (index + 1).ToString();
                    row.Cells[1].Value = item.item_clsno;
                    row.Cells[2].Value = item.item_clsname;
                    row.Cells[3].Value = (item.is_stop == "1" ? "已停用" : "已启用");
                    if (item.is_stop == "1") 
                    {
                        row.Cells[3].Style.ForeColor = Color.Red;
                    }
                }
                
                this.Label1.Text = string.Format("第{0}页，共{1}页", pageIndex.ToString(), pageCount.ToString());
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

        private void pnl_search_Click(object sender, EventArgs e)
        {
            IBLL.IGoods bll = new BLL.Goods();
            all_list = bll.GetAllClsList();
            this.total = all_list.Count;
            int m = (int)Math.Ceiling((decimal)this.total / (decimal)this.pageSize);
            this.pageCount = m;

            this.pnl_first_Click(pnl_first, null);
        }

        private void pnl_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            var row = this.dataGridView1.CurrentRow;
            if (row != null && row.Tag != null && row.Tag.ToString() != "") 
            {
                try
                {
                    var cls_no = row.Tag.ToString();
                    var item = all_list.FirstOrDefault(d => d.item_clsno == cls_no);
                    item.is_stop = "0";
                    IBLL.IGoods bll = new BLL.Goods();
                    bll.StartCls(cls_no);
                    this.pageChanged();
                }
                catch (Exception ex)
                {
                    Log.writeLog("frmGoodsCls->btn_start_Click()", ex.ToString(), null);
                    new MsgForm(ex.GetMessage()).ShowDialog();
                }
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            var row = this.dataGridView1.CurrentRow;
            if (row != null && row.Tag != null && row.Tag.ToString() != "")
            {
                try
                {
                    var cls_no = row.Tag.ToString();
                    var item = all_list.FirstOrDefault(d => d.item_clsno == cls_no);
                    item.is_stop = "1";
                    IBLL.IGoods bll = new BLL.Goods();
                    bll.StopCls(cls_no);
                    this.pageChanged();
                }
                catch (Exception ex)
                {
                    Log.writeLog("frmGoodsCls->btn_stop_Click()", ex.ToString(), null);
                    new MsgForm(ex.GetMessage()).ShowDialog();
                }
            }
        }

        private void frmGoodsCls_FormClosed(object sender, FormClosedEventArgs e)
        {
            all_list = null;
        }

    }
}
