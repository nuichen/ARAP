using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.customer
{
    public partial class frmCustomer : Form
    {
        BLL.ICustomer bll;
        private int pageSize = 20;
        private int pageIndex = 1;
        private int total = 0;
        private int pageCount = 0;
        public frmCustomer()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            //
            bll = new BLL.Customer();
            LoadDataGrid();

            control.ClickActive.addActive(btn_first, 2);
            control.ClickActive.addActive(btn_prev, 2);
            control.ClickActive.addActive(btn_next, 2);
            control.ClickActive.addActive(btn_last, 2);

            dateTimePicker1.Value = DateTime.Now.AddDays(-7);
            dateTimePicker2.Value = DateTime.Now;

            var s_lst = new List<body.key_value>();
            s_lst.Add(new body.key_value { t_key = "-1", t_value = "全部" });
            s_lst.Add(new body.key_value { t_key = "0", t_value = "待审" });
            s_lst.Add(new body.key_value { t_key = "1", t_value = "已审" });
            s_lst.Add(new body.key_value { t_key = "2", t_value = "拒绝" });
            comboBox1.DataSource = s_lst;

            this.btn_first_Click(btn_first, null);
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
            this.dg_data.AddColumn("cus_no", "编号", "", 80, 2, "");
            this.dg_data.AddColumn("cus_name", "客户名称", "", 160, 1, "");
            this.dg_data.AddColumn("mobile", "手机", "", 120, 2, "");
            this.dg_data.AddColumn("contact_address", "联系人", "", 100, 2, "");
            this.dg_data.AddColumn("login_no", "登录账号", "", 120, 2, "");
            this.dg_data.AddColumn("salesman_id", "归属业务员", "", 100, 1, "");
            this.dg_data.AddColumn("detail_address", "详细地址", "", 160, 1, "");
            this.dg_data.AddColumn("create_time", "注册时间", "", 100, 2, "yyyy-MM-dd");
            this.dg_data.AddColumn("status", "状态", "", 80, 2, "{0:未审,1:已审,2:拒绝}");

            this.dg_data.MergeCell = false;
            this.dg_data.DataSource = new DataTable();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            this.btn_first_Click(btn_first, null);
        }

        private void pageChanged()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                var status = comboBox1.SelectedValue.ToString();
                var salesman_id = "";
                var keyword = textBox1.Text;
                var dt = bll.GetCusDt(date1, date2, status, salesman_id, keyword, pageSize, pageIndex, out total);

                this.dg_data.DataSource = dt;

                int n = pageIndex;
                int m = (int)Math.Ceiling((decimal)this.total / (decimal)this.pageSize);
                this.pageCount = m;
                this.label6.Text = string.Format("第{0}页，共{1}页", n.ToString(), m.ToString());
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


        private void btn_view_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if (row != null) 
            {
                var cus_no = row["cus_no"].ToString();
                frmCusEdit frm = new frmCusEdit(cus_no,"view");
                frm.ShowDialog();
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                var cus_no = row["cus_no"].ToString();
                frmCusEdit frm = new frmCusEdit(cus_no, "edit");
                frm.ShowDialog();
            }
        }

        private void btn_approve_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                var cus_no = row["cus_no"].ToString();
                frmCusEdit frm = new frmCusEdit(cus_no, "approve");
                frm.ShowDialog();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            var row = this.dg_data.CurrentRow();
            if (row != null)
            {
                var cus_no = row["cus_no"].ToString();
                if (row["status"].ToString() != "1")
                {
                    bll.DeleteCustomer(cus_no);
                    this.pageChanged();
                }
                else {
                    Program.frmMsg("已审核客户无法删除");
                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            frmAddCus frm = new frmAddCus();
            frm.ShowDialog();
            this.btn_first_Click(btn_first, null);
        }

    }
}