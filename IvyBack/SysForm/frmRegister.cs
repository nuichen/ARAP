using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using System.Net;
using System.Net.Sockets;
namespace IvyBack.SysForm
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
            int x = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2 - 200;

            this.Location = new Point(x, y);
            this.dgv.AutoGenerateColumns = false;
        }
        IBLL.ISys bll = new BLL.SysBLL();

        DataTable tb;

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            try
            {
                this.dgv.DataSource = this.tb;
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt.Text))
                {
                    MsgForm.ShowFrom("请输入机号！");
                    this.txt.Focus();
                    return;
                }

                Model.netsetup ns = new Model.netsetup()
                {
                    jh = this.txt.Text,
                    softpos = "1",
                    comp_name = Dns.GetHostName(),
                    other = Program.com_guid,
                    ip = GetLocalIP(),
                    memo = ""
                };

                bll.AddJH(ns);

                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Helper.KeyPressJudge.IsStr(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public DialogResult RegisterJH()
        {
            tb = bll.GetJH();

            if (tb != null && tb.Rows.Count > 0)
            {
                var result = this.tb.Select("other = '" + Program.com_guid + "'");

                if (result != null && result.Count() > 0)
                {
                    return DialogResult.Yes;
                }
            }

            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"1","后台"},
                {"0","前台"}
            };
           
            this.机号类型.ValueMember = "Key";
            this.机号类型.DisplayMember = "Value";
            this.机号类型.DataSource = new BindingSource(dic, null);

            return this.ShowDialog();
        }

        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns>本机IP地址</returns>
        public string GetLocalIP()
        {

            string HostName = Dns.GetHostName(); //得到主机名
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return IpEntry.AddressList[i].ToString();
                }
            }
            return "";
        }

    }
}
