using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.goods
{
    public partial class GoodsClsList : Form
    {
        BLL.IGoodsCls bll;
        private int pageSize = 20;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        private string parent_cls_no = "0";
        private DataTable dt;
        public GoodsClsList()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            //
            LoadDataGrid();
            bll = new BLL.GoodsCls();
            this.btn_first_Click(btn_first, null);
        }

        private void LoadDataGrid()
        {
            this.dg_data.AddColumn("cls_no", "分类编号", "", 80, 2, "");
            this.dg_data.AddColumn("cls_name", "分类名称", "", 160, 1, "");
            this.dg_data.AddColumn("supcust_groupname", "所属客户组", "", 120, 1, "");
            this.dg_data.AddColumn("status", "状态", "", 80, 2, "{0:停用,1:启用}");
            this.dg_data.AddColumn("is_show_mall", "是否显示在B2B平台", "", 160, 2, "{0:,1:√}");

            this.dg_data.MergeCell = false;
            this.dg_data.DataSource = new DataTable();
        }


        private void pageChanged()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //
                var keyword = textBox1.Text;
                dt = bll.GetDt(parent_cls_no, keyword, pageSize, pageIndex, out total);
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

        private void pnl_find_Click(object sender, EventArgs e)
        {
            parent_cls_no = "0";
            lbl_par.Tag = 0;
            lbl_par.Text = "";
            this.btn_first_Click(btn_first, null);
        }

        private void pnl_change_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if(row != null)
            {
                var cls_no = row["cls_no"].ToString();
                var source_row = dt.Select("cls_no='" + cls_no + "'");
                if (source_row.Length > 0) 
                {
                    var item = DB.ReflectionHelper.DataRowToModel<Model.goods_cls>(source_row[0]);
                    var frm = new frmGoodsClsChange(cls_no, item);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.pageChanged();
                    }
                }
            }
            
        }

        private void dg_data_ClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if (row != null && column_name == "is_show_mall") 
            {
                var cls_no = row["cls_no"].ToString();
                var source_row = dt.Select("cls_no='" + cls_no + "'");
                if (row["is_show_mall"].ToString() == "1")
                {
                    bll.Stop(cls_no);
                    if (source_row.Length > 0) source_row[0]["is_show_mall"] = "0";
                }
                else 
                {
                    bll.Restart(cls_no);
                    if (source_row.Length > 0) source_row[0]["is_show_mall"] = "1";
                }
                this.dg_data.DataSource = dt;
                this.dg_data.Refresh();
            }
        }

        private void dg_data_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if (row != null) 
            {
                lbl_par.Tag = row["cls_no"].ToString();
                lbl_par.Text = row["cls_name"].ToString();
                parent_cls_no = row["cls_no"].ToString();
                this.btn_first_Click(btn_first, null);
            }
            
        }

        private void GoodsClsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            dt = null;
        }
    }
}
