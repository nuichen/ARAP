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
    public partial class SetGroupCls : Form
    {
        DataTable dt;
        public SetGroupCls()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            LoadDataGrid();


            BLL.ICustomer bll2 = new BLL.Customer();
            List<body.key_value> all_lst = new List<body.key_value>();
            all_lst.Add(new body.key_value { t_key="", t_value="请选择客户组"});
            var lst2 = bll2.GetCusGroupList();
            if (lst2.Count > 0) all_lst.AddRange(lst2);
            comboBox1.DataSource = all_lst;

            BLL.IGoodsCls bll = new BLL.GoodsCls();
            dt = bll.GetDtForSupCustGroup();
        }

        private void LoadDataGrid()
        {
            this.dg_data.AddColumn("is_check", "选择", "", 60, 2, "{0:,1:√}");
            this.dg_data.AddColumn("cls_no", "分类编号", "", 100, 2, "");
            this.dg_data.AddColumn("cls_name", "分类名称", "", 200, 1, "");

            this.dg_data.MergeCell = false;
            this.dg_data.DataSource = new DataTable();
        }


        private void pageChanged()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //
                this.dg_data.DataSource = dt;

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

        private void dg_data_ClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if (row != null && column_name == "is_check")
            {
                var cls_no = row["cls_no"].ToString();
                var source_row = dt.Select("cls_no='" + cls_no + "'");
                if (row["is_check"].ToString() == "1")
                {
                    if (source_row.Length > 0) source_row[0]["is_check"] = "0";
                }
                else
                {
                    if (source_row.Length > 0) source_row[0]["is_check"] = "1";
                }
                this.dg_data.DataSource = dt;
                this.dg_data.Refresh();
            }
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || comboBox1.SelectedValue.ToString() == "")
            {
                Program.frmMsg("请选择客户组");
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                var item_clsnos = "";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["is_check"].ToString() == "1")
                    {
                        item_clsnos += dr["cls_no"].ToString() + ",";
                    }
                }
                if (item_clsnos.Length > 0) item_clsnos = item_clsnos.Substring(0, item_clsnos.Length - 1);
                BLL.IGoodsCls bll = new BLL.GoodsCls();
                var group_no = comboBox1.SelectedValue.ToString();
                bll.SaveGroupCls(group_no, item_clsnos);
                Program.frmMsg("保存成功");
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null && comboBox1.SelectedValue.ToString() != "")
            {
                BLL.IGoodsCls bll = new BLL.GoodsCls();
                var group_no = comboBox1.SelectedValue.ToString();
                var item_clsnos = bll.GetGroupCls(group_no);
                if(item_clsnos.Length > 0) item_clsnos += ",";
                foreach (DataRow dr in dt.Rows) 
                {
                    if (item_clsnos.Contains(dr["cls_no"].ToString() + ","))
                    {
                        dr["is_check"] = "1";
                    }
                    else 
                    {
                        dr["is_check"] = "0";
                    }
                }
                pageChanged();
            }
        }

        private void SetGroupCls_FormClosed(object sender, FormClosedEventArgs e)
        {
            dt = null;
        }



    }
}