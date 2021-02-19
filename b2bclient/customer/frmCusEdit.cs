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
    public partial class frmCusEdit : Form
    {
        BLL.ICustomer bll = new BLL.Customer();
        private string cus_no = "";
        body.customer cus = new body.customer();
        public frmCusEdit(string cus_no, string opt_type)
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            this.cus_no = cus_no;

            var lst = bll.GetCusGroupList();
            comboBox1.DataSource = lst;

            cus = bll.GetCustomer(cus_no);

            if (opt_type == "view" || cus.status == "2") 
            {
                btn_ok.Visible = false;
                btn_approve.Visible = false;
                btn_reject.Visible = false;
            }
            else if (opt_type == "edit")
            {
                btn_ok.Visible = true;
                btn_approve.Visible = false;
                btn_reject.Visible = false;
            }
            else if (opt_type == "approve" && cus.status == "0")
            {
                btn_ok.Visible = false;
                btn_approve.Visible = true;
                btn_reject.Visible = true;
            }

            txt_cus_no.Text = cus.cus_no;
            txt_cus_name.Text = cus.cus_name;
            txt_cus_tel.Text = cus.cus_tel;
            txt_mobile.Text = cus.mobile;
            txt_contact_addr.Text = cus.contact_address;
            txt_detail_addr.Text = cus.detail_address;
            txt_salesman_id.Text = cus.salesman_id;
            txt_login_no.Text = cus.login_no;
            if (cus.is_branch == "1")
            {
                checkBox1.Checked = true;
            }
           
            comboBox1.SelectedValue = cus.supcust_group;

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string cus_level = "0";
                if (comboBox1.SelectedValue != null) cus_level = comboBox1.SelectedValue.ToString();
                var login_no = txt_login_no.Text;
                var is_branch = "1";
                if (!checkBox1.Checked) is_branch = "0";
                string supcust_group = "";
                if (comboBox1.SelectedValue != null) supcust_group = comboBox1.SelectedValue.ToString();
                bll.UpdateCustomer(this.cus_no,login_no, txt_cus_name.Text, txt_cus_tel.Text, txt_mobile.Text, "", "", txt_contact_addr.Text,
                txt_detail_addr.Text, cus_level, "", supcust_group, txt_salesman_id.Text, DateTime.Now.ToString("yyyy-MM-dd"), "2050-01-01", is_branch);
                Program.frmMsgYesNo("操作成功");
                this.Close();
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

        private void btn_approve_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string status = "1";
                string oper_id = Program.oper_id;
                string cus_level = "0";
                if (comboBox1.SelectedValue != null) cus_level = comboBox1.SelectedValue.ToString();
                string supcust_group = "";
                if (comboBox1.SelectedValue != null) supcust_group = comboBox1.SelectedValue.ToString();
                string remark = "";
                bll.Approve(this.cus_no,status,oper_id,cus_level,supcust_group,remark);
                Program.frmMsg("操作成功");
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("btn_approve_Click()", ex.ToString(), null);
                Program.frmMsg(ex.Message);
            }
            finally 
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btn_reject_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string status = "2";
                string oper_id = Program.oper_id;
                string cus_level = "0";
                if (comboBox1.SelectedValue != null) cus_level = comboBox1.SelectedValue.ToString();
                string supcust_group = "";
                if (comboBox1.SelectedValue != null) supcust_group = comboBox1.SelectedValue.ToString();
                string remark = "";
                bll.Approve(this.cus_no, status, oper_id, cus_level, supcust_group, remark);
                Program.frmMsg("操作成功");
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("btn_reject_Click()", ex.ToString(), null);
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
