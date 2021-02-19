using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace IvyBack.MainForm
{
    public partial class frmFindServer : Form
    {
        public frmFindServer()
        {
            InitializeComponent();
        }
        private string[] ips;
        private int cycleNum = 10;
        private int cycleIndex = -1;

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

        private void FindServer()
        {
            Thread th = new Thread(() =>
            {
                try
                {
                    AutoResetEvent myEvent = new AutoResetEvent(false);

                    StringBuilder ls_ip = new StringBuilder(1024);
                    IvyGetValue.GetIP(ls_ip);
                    int ll_len = IvyGetValue.F_0001_0016(ls_ip);
                    StringBuilder ls_computers = new StringBuilder(ll_len);
                    IvyGetValue.F_0001_0017(ls_computers); //返回 ip + (tab) + 机器名,多部机分多行
                    ips = ls_computers.ToString().Split(',');

                    //foreach (string ip in ips)
                    //{
                    //    IBLL.ICommonBLL bll = new BLL.CommonBLL();
                    //    string isServer = bll.IsServer(ips[cycleIndex], "8383");

                    //    if (isServer.Equals("1"))
                    //    {
                    //        //成功
                    //        //Helper.AppSetting.ip = ips[cycleIndex];
                    //        //Helper.AppSetting.port = "8383";

                    //        this.Invoke((MethodInvoker)delegate
                    //        {
                    //            this.Close();
                    //        });
                    //    }
                    //}

                }
                catch (Exception e)
                {
                    Helper.LogHelper.writeLog("findServer", e.ToString());
                    MsgForm.ShowFrom(e);
                    Application.Exit();
                }
            });
            th.IsBackground = true;
            th.Start();
        }

        private void frmFindServer_Shown(object sender, EventArgs e)
        {
            FindServer();
        }

        private string info = "正在查找服务器";
        private DateTime startTime = DateTime.Now;
        private int num = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (num <= 10)
                num++;
            else
                num = 0;

            this.lblInfo.Text = info + new string('.', num);
            TimeSpan ts = DateTime.Now - startTime;
            this.lblTime.Text = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
        }

    }

    public class IvyGetValue
    {
        [DllImport("IVYUpload.dll")]
        public static extern int GetIP(StringBuilder ip);
        [DllImport("IVYGetValue.dll")]
        public static extern int F_0001_0016(StringBuilder MyIP);
        [DllImport("IVYGetValue.dll")]
        public static extern int F_0001_0017(StringBuilder buf);

    }
}
