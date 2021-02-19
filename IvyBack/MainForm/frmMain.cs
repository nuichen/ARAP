using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using IvyBack.BaseForm;
using IvyBack.MainForm;
using System.Diagnostics;
using IvyBack.NavForm;

namespace IvyBack
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            this.Text = Helper.GlobalData.soft_name + Helper.GlobalData.version;

            cons.AddClickAct.Add(this.pnlClose);
            cons.ResourcesHelper.Clear();

            Helper.GlobalData.windows = this.windowsList1;
            Helper.GlobalData.mainFrm = this;

            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            if ("0".Equals(Helper.AppSetting.isMax))
                this.WindowState = FormWindowState.Normal;
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmNav frm = new frmNav(this.windowsList1);
            windowsList1.ShowForm(frm);
        }

        //关闭时清理进程
        protected override void OnClosing(CancelEventArgs e)
        {
            YesNoForm frm = new YesNoForm("确定退出系统吗？");
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                e.Cancel = false;

                Application.ExitThread();
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //AutoForm(this.panTopTitle);
        }

        private void AutoForm(Control c)
        {
            int x = (int)(0.5 * (this.Width - c.Width));
            int y = (int)(0.5 * (c.Parent.Height - c.Height));
            c.Location = new Point(x, y);
        }

        private void pnlClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void pnlClose_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                windowsList1.SortKey(e.KeyCode);
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
        }


    }
}
