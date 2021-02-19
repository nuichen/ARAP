using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.customer
{
    public partial class frmAddCus : Form
    {
        public frmAddCus()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
        }

        private void chear_data()
        {
            txt_login_no.Text = "";
            txt_cus_pwd.Text = "";
            txt_cus_name.Text = "";
            txt_cus_tel.Text = "";
            txt_mobile.Text = "";
            txt_contact_addr.Text = "";
            txt_detail_addr.Text = "";
            txt_salesman_id.Text = "";
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string cus_level = "0";
                if (comboBox1.SelectedValue != null) cus_level = comboBox1.SelectedValue.ToString();
                string settle_type = "0";
                var login_no = txt_login_no.Text;
                var is_branch = "1";
                if (!checkBox1.Checked) is_branch = "0";
                BLL.ICustomer bll = new BLL.Customer();
                string cus_pwd = "";
                if (txt_cus_pwd.Text != "") cus_pwd = MD5.ToMD5(txt_cus_pwd.Text);
                bll.AddCustomer(login_no, cus_pwd, txt_cus_name.Text, txt_cus_tel.Text, txt_mobile.Text, "", "", txt_contact_addr.Text,
                txt_detail_addr.Text, cus_level, "", settle_type, txt_salesman_id.Text, DateTime.Now.ToString("yyyy-MM-dd"), "2050-01-01", is_branch);
                Program.frmMsg("操作成功");
                chear_data();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("btn_ok_Click()", ex.ToString(), null);
                Program.frmMsg(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
