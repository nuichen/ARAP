using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.report
{
    public partial class Rep客户对账表 : Form
    {
        public Rep客户对账表()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            LoadDataGrid();

            BLL.ICustomer bll2 = new BLL.Customer();
            List<body.key_value> all_lst = new List<body.key_value>();
            all_lst.Add(new body.key_value { t_key = "", t_value = "全部" });
            var lst2 = bll2.GetCusGroupList();
            if (lst2.Count > 0) all_lst.AddRange(lst2);
            comboBox1.DataSource = all_lst;

        }

        private void LoadDataGrid()
        {
            this.dg_data.AddColumn("supcust_no", "客户编码", "", 100, 2, "");
            this.dg_data.AddColumn("sup_name", "客户名称", "", 240, 1, "");
            this.dg_data.AddColumn("sum_money", "已用额度", "", 100, 3, "0.00");
            this.dg_data.AddColumn("credit_amt", "信用额度", "", 100, 3, "0.00");
            this.dg_data.AddColumn("balance_amt", "剩余额度", "", 100, 3, "0.00");

            this.dg_data.MergeCell = false;
            this.dg_data.SetTotalColumn("sum_money,credit_amt,balance_amt");
            this.dg_data.DataSource = new DataTable();
        }

        public void do_search()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                BLL.ICustomer bll = new BLL.Customer();
                var supcust_group = "";
                if (comboBox1.SelectedValue != null) supcust_group = comboBox1.SelectedValue.ToString();
                var dt = bll.SearchCusBalance(txt_keyword.Text.Trim(), supcust_group);

                this.dg_data.DataSource = dt;
            }
            catch (Exception e)
            {
                Program.frmMsg(e.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            do_search();
        }

    }
}
