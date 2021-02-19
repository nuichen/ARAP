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
    public partial class GoodsList : Form
    {
        BLL.IGoods bll;
        BLL.IGoodsCls bllcls;
        private int pageSize = 20;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        List<body.theme> themeLst = new List<body.theme>();
        DataTable dt;
        public GoodsList()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            LoadDataGrid();
            //
            try
            {
                bll = new BLL.Goods();
                bllcls = new BLL.GoodsCls();
                //显示第一个分类
                var lst = bllcls.GetListForMenu();
                var alllst = new List<Model.goods_cls>();
                alllst.Add(new Model.goods_cls { cls_no="", cls_name="全部"});
                alllst.AddRange(lst);
                comboBox1.DataSource = alllst;

                themeLst = bll.GetThemeList();
                var alllst2 = new List<body.theme>();
                alllst2.Add(new body.theme { theme_code = "0", theme_name = "全部" });
                alllst2.AddRange(themeLst);
                comboBox2.DataSource = alllst2;

                this.btn_first_Click(btn_first, null);
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void LoadDataGrid()
        {
            this.dg_data.AddColumn("goods_id", "编号", "", 120, 2, "");
            this.dg_data.AddColumn("cls_id", "分类编号", "", 80, 2, "");
            this.dg_data.AddColumn("cls_name", "分类名称", "", 120, 1, "");
            this.dg_data.AddColumn("goods_no", "货号", "", 80, 2, "");
            this.dg_data.AddColumn("goods_name", "品名", "", 240, 1, "");
            this.dg_data.AddColumn("status", "状态", "", 80, 2, "{0:停用,1:启用}");
            this.dg_data.AddColumn("themes_str", "主题", "", 240, 1, "");
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
                string cls_no = "";
                if (comboBox1.SelectedValue != null)
                {
                    cls_no = comboBox1.SelectedValue.ToString();
                }
                string theme_no = "0";
                if (comboBox2.SelectedValue != null)
                {
                    theme_no = comboBox2.SelectedValue.ToString();
                }
                string is_no_show_mall = "0";
                if (chkNoShow.Checked) is_no_show_mall = "1";
                dt = bll.GetDt(cls_no, textBox1.Text.ToString(), theme_no, is_no_show_mall, pageSize, pageIndex, out total);
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("themes_str");
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["themes_str"] = get_themes_str(dr["themes"].ToString());
                    }
                }
                this.dg_data.DataSource = dt;
                int n = pageIndex;
                int m = (int)Math.Ceiling((decimal)this.total / (decimal)this.pageSize);
                this.pageCount = m;
                this.label4.Text = string.Format("第{0}页，共{1}页", n.ToString(), m.ToString());
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

        private void pnl_add_Click(object sender, EventArgs e)
        {
            var frm = new frmGoodsChange("");
            frm.ShowDialog();
        }

        private string get_themes_str(string themes) 
        {
            var themes_str = "";
            if (themes == null || themes == "")
            {
                themes_str = "";
            }
            else {
                var arr = themes.Split(',');
                for (var i = 0; i < arr.Length; i++) 
                {
                    if (arr[i] != "") {
                        var item = themeLst.FirstOrDefault(d => d.theme_code == arr[i]);
                        if (item != null) themes_str += "[" + item.theme_name + "] ";
                    }
                }
            }
            return themes_str;
        }

        private void pnl_find_Click(object sender, EventArgs e)
        {
            this.btn_first_Click(btn_first, null);
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                var goods_id = row["goods_id"].ToString();
                var frm = new frmGoodsChange(goods_id);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var item = frm.getItem();
                    var source_row = dt.Select("goods_id='" + goods_id + "'");
                    if (source_row.Length > 0) 
                    {
                        source_row[0]["cls_id"] = item.cls_id;
                        source_row[0]["cls_name"] = item.cls_name;
                        source_row[0]["goods_no"] = item.goods_no;
                        source_row[0]["goods_name"] = item.goods_name;
                        source_row[0]["themes"] = item.themes;
                        source_row[0]["is_show_mall"] = item.is_show_mall;
                    }
                    this.dg_data.DataSource = dt;
                    this.dg_data.Refresh();
                }
            }
        }

        private void dg_data_ClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if (row != null && column_name == "is_show_mall")
            {
                var goods_id = row["goods_id"].ToString();
                var source_row = dt.Select("goods_id='" + goods_id + "'");
                if (row["is_show_mall"].ToString() == "1")
                {
                    bll.Stop(goods_id);
                    if (source_row.Length > 0) source_row[0]["is_show_mall"] = "0";
                }
                else
                {
                    bll.Restart(goods_id);
                    if (source_row.Length > 0) source_row[0]["is_show_mall"] = "1";
                }
                this.dg_data.DataSource = dt;
                this.dg_data.Refresh();
            }
        }

        private void GoodsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            dt = null;
        }


    }
}
