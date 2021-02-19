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
    public partial class frmCusHand : Form
    {
        BLL.ICustomer bll = new BLL.Customer();
        private body.customer cus =new body.customer();
        public frmCusHand(body.customer c, string header, int un_read_num)
        {
            InitializeComponent();
            this.cus = c;
            this.Text = header;
            this.lbl_un_read_num.Text = un_read_num.ToString();
            if (cus.msg_hand == "0")
            {
                pnl_new.Visible = true;
            }
            else
            {
                pnl_new.Visible = false;
            }
            var lst = bll.GetCusGroupList();
            comboBox2.DataSource = lst;

            this.refreshData();
        }
       
        private void refreshData()
        {
            try
            {
                lblsalesman_id.Text = cus.salesman_id;
                lblcreate_time.Text = Conv.ToDateTime(cus.cus_start_date).ToString("yyyy-MM-dd HH:mm:ss");
                lblcus_name.Text = cus.cus_name;
                lblcontact_addr.Text = cus.contact_address;
                lblcus_area.Text = cus.cus_area;
                lblcus_idcard.Text = cus.cus_idcard;
                lblcus_tel.Text = cus.cus_tel;
                lbldetail_addr.Text = cus.detail_address;
                lblmobile.Text = cus.mobile;
                txt_remark.Text = cus.remark;
                
                //展示处理按键
                if (cus.status == "0")
                {
                    pnl_pass.Visible = true;
                    pnl_disable.Visible = true;
                }
                else
                {
                    pnl_pass.Visible = false ;
                    pnl_disable.Visible =  false ;
                }
                if (cus.img_url != null && cus.img_url != "")
                {
                    var urls = cus.img_url.Split(',', '，');
                    if (urls.Length > 0 && urls[0] != "")
                    {
                        pnl_img1.Text = urls[0];
                        pnl_img1.BackgroundImage = ImageHelper.getImage(AppSetting.imgsvr + urls[0]);
                    }
                    if (urls.Length > 1 && urls[1] != "")
                    {
                        pnl_img2.Text = urls[1];
                        pnl_img2.BackgroundImage = ImageHelper.getImage(AppSetting.imgsvr + urls[1]);
                    }
                }
            }
            catch (Exception e)
            {
                Program.frmMsg(e.Message);
            }
          
        }

        private void pnl_pass_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string status = "1";
                string oper_id = Program.oper_id;
                string cus_level = "0";
                if (comboBox1.SelectedValue != null) cus_level = comboBox1.SelectedValue.ToString();
                string supcust_group = "";
                if (comboBox2.SelectedValue != null) supcust_group = comboBox2.SelectedValue.ToString();
                string remark = txt_remark.Text;
                bll.Approve(this.cus.cus_no, status, oper_id, cus_level, supcust_group, remark);
                Program.frmMsg("操作成功");
                this.Close();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("pnl_pass_Click()", ex.ToString(), null);
                Program.frmMsg(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void pnl_disable_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string status = "2";
                string oper_id = Program.oper_id;
                string cus_level = "0";
                if (comboBox1.SelectedValue != null) cus_level = comboBox1.SelectedValue.ToString();
                string settle_type = "0";
                if (comboBox2.SelectedValue != null) settle_type = comboBox2.SelectedValue.ToString();
                string remark = txt_remark.Text;
                bll.Approve(this.cus.cus_no, status, oper_id, cus_level, settle_type, remark);
                Program.frmMsg("操作成功");
                this.Close();
            }
            catch (Exception ex)
            {
                LogHelper.writeLog("pnl_disable_Click()", ex.ToString(), null);
                Program.frmMsg(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void pnl_later_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.cus.status == "0")
                {
                    bll.SignRead(this.cus.cus_no);
                }
                //
                var new_cus = new body.customer();
                int un_read_num = 0;
                if (bll.GetFirstNewCustomer(out new_cus,out un_read_num ) == true)
                {
                    this.DialogResult = DialogResult.No;
                    var frm = new frmCusHand(new_cus, "新货商审核",un_read_num);
                    frm.ShowDialog();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
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

        private void pnl_img2_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, pnl_img2.Width - 1, pnl_img2.Height - 1));
        }

        private void pnl_img1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, pnl_img1.Width - 1, pnl_img1.Height - 1));
        }

        private void pnl_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
