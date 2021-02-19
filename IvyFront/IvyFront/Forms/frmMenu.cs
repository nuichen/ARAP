using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyFront.Helper;

namespace IvyFront.Forms
{
    public partial class frmMenu : Form
    {
        int cn = 0;
        public frmMenu()
        {
            InitializeComponent();
            this.Text = Program.software_name;
            lbl_title.Text = Program.software_name;
            lbl_version.Text = Program.software_version;
            //
            show_msg_block();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void show_msg_block() 
        {
            if (cn % 2 == 0) lbl_sys_title.Text = "[点击下载更新]";
            else lbl_sys_title.Text = "系统消息";

            var sys_msg = "";
            if (Program.item_count > 0) sys_msg += "商品档案更新: " + Program.item_count + "\r\n";
            if (Program.sup_count > 0) sys_msg += "客商档案更新: " + Program.sup_count + "\r\n";
            if (Program.cus_price_count > 0) sys_msg += "客户价格更新: " + Program.cus_price_count + "\r\n";
            if (Program.sup_price_count > 0) sys_msg += "供应商价格更新: " + Program.sup_price_count + "\r\n";
            if (sys_msg.Length > 0)
            {
                lbl_sys_msg.Text = sys_msg;
                sys_msg_block.Visible = true;
                lbl_sys_msg.Visible = true;
                lbl_sys_title.Visible = true;
            }
            else
            {
                lbl_sys_msg.Text = "";
                sys_msg_block.Visible = false;
                lbl_sys_msg.Visible = false;
                lbl_sys_title.Visible = false;
            }
        }

        private void btn_pos_Click(object sender, EventArgs e)
        {
            if (!SoftUpdate.PermissionsBalidation()) 
            {
                return;
            }

            MainForm frm = new MainForm();
            frm.ShowDialog();
        }

        private void btn_download_Click(object sender, EventArgs e)
        {
            if (!SoftUpdate.PermissionsBalidation()) 
            {
                return;
            }
            frmDownload frm = new frmDownload(2);
            frm.ShowDialog();

            show_msg_block();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            //this.Close();
            YesNoForm frm = new YesNoForm("确定退出系统吗？");
            if (frm.ShowDialog() == DialogResult.Yes) 
            {
                if (Program.ReadWeight != null)
                {
                    Program.ReadWeight.Dis();
                }
                Program.is_run = false;

                save_exit();
            }
        }

        private void save_exit()
        {
            if (1 == 1)
            {
                /*
                Form frmTop = new Form();
                frmTop.FormBorderStyle = FormBorderStyle.None;
                frmTop.WindowState = FormWindowState.Maximized;
                frmTop.BackColor = Color.FromArgb(30, 110, 165);
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.ForeColor = Color.White;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("SimSun", 20);
                lbl.Text = "正在安全退出，请稍候……";
                lbl.Dock = DockStyle.Fill;
                frmTop.Controls.Add(lbl);
                frmTop.Show();
                Application.DoEvents();
                */
                WaitForm frm = new WaitForm("正在上传数据，请稍候", "2");
                frm.ShowDialog();
            }
        }

        //关闭时清理进程
        protected override void OnClosing(CancelEventArgs e)
        {
            YesNoForm frm = new YesNoForm("确定退出系统吗？");
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                if (Program.ReadWeight != null)
                {
                    Program.ReadWeight.Dis();
                }
                Program.is_run = false;
                e.Cancel = false;

                Application.ExitThread();
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
            //System.Environment.Exit(0);
        }

        private void btn_pay_flow_Click(object sender, EventArgs e)
        {
             if (!SoftUpdate.PermissionsBalidation()) 
            {
                return;
            }
            frmSaleSum frm = new frmSaleSum();
            frm.ShowDialog();
        }

        private void btn_change_pwd_Click(object sender, EventArgs e)
        {
             if (!SoftUpdate.PermissionsBalidation()) 
            {
                
                return;
            }
            frmChangePwd frm = new frmChangePwd();
            frm.ShowDialog();
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            frmParSetting frm = new frmParSetting();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK && Program.oper_id == "9999")
            {
                new MsgForm("初始化完成，即将退出重新登录").ShowDialog();
                this.Close();
            }
        }

        private void btn_xsck_Click(object sender, EventArgs e)
        {
             if (!SoftUpdate.PermissionsBalidation()) 
            {
                
                return;
            }
            frmXSOrder frm = new frmXSOrder();
            frm.ShowDialog();
        }

        private void btn_cgrk_Click(object sender, EventArgs e)
        {
             if (!SoftUpdate.PermissionsBalidation()) 
            {
                
                return;
            }
            frmCGOrder frm = new frmCGOrder();
            frm.ShowDialog();
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
             if (!SoftUpdate.PermissionsBalidation()) 
            {
                
                return;
            }
            if (!Program.is_connect)
            {
                (new MsgForm("网络异常")).ShowDialog();
                return;
            }
            frmUpload frm = new frmUpload();
            frm.ShowDialog();
        }

        private void btn_item_cls_Click(object sender, EventArgs e)
        {
            if (!SoftUpdate.PermissionsBalidation())
            {
                return;
            }

            frmGoodsCls frm = new frmGoodsCls();
            frm.ShowDialog();
        }

        private void lbl_sys_msg_Click(object sender, EventArgs e)
        {
             if (!SoftUpdate.PermissionsBalidation()) 
            {
                
                return;
            }
            frmDownload frm = new frmDownload(2);
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++cn;
            if (Program.is_connect)
            {
                pic_network.Image = Properties.Resources.connect;
            }
            else
            {
                pic_network.Image = Properties.Resources.disconnect;
            }
            show_msg_block();
        }

        private void btn_cg_Click(object sender, EventArgs e)
        {
             if (!SoftUpdate.PermissionsBalidation()) 
            {
                
                return;
            }
            CGMainForm frm = new CGMainForm();
            frm.ShowDialog();
        }

        private void btn_th_Click(object sender, EventArgs e)
        {
             if (!SoftUpdate.PermissionsBalidation()) 
            {
                
                return;
            }
            THMainForm frm = new THMainForm();
            frm.ShowDialog();
        }

        private void btn_xsth_Click(object sender, EventArgs e)
        {
            if (!SoftUpdate.PermissionsBalidation()) 
            {
                
                return;
            }
            frmTHOrder frm = new frmTHOrder();
            frm.ShowDialog();
        }

        private void pic_network_Click(object sender, EventArgs e)
        {
            var frm = new NetSvrForm(Appsetting.ws_svr, Appsetting.is_bind_ip);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (frm.svr != Appsetting.ws_svr) 
                {
                    Appsetting.ws_svr = frm.svr;
                }
                if (frm.is_bind_ip != Appsetting.is_bind_ip)
                {
                    Appsetting.is_bind_ip = Appsetting.is_bind_ip;
                }
            }
        }

    }
}
