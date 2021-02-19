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
    public partial class WaitForm : Form
    {
        private int count = 0;
        private string title = "";
        private string op_type = "";
        public WaitForm(string title,string op_type)
        {
            InitializeComponent();
            this.title = title;
            this.op_type = op_type;

            this.Size = new System.Drawing.Size(1024, 768);
            this.FormBorderStyle = FormBorderStyle.None;
            if(op_type == "1")
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 110, 165);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;

            lbl.AutoSize = false;
            lbl.ForeColor = Color.White;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("SimSun", 20);
            lbl.Text = title + "…… " + count.ToString() + " s";
            lbl.Dock = DockStyle.Fill;
            timer1.Start();
        }

        public bool ShowWait()
        {
            if (this.ShowDialog() == DialogResult.OK)
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            ++count;
            lbl.Text = this.title + "…… " + count.ToString() + " s";
            
        }

        private void WaitForm_Shown(object sender, EventArgs e)
        {
            if (this.op_type == "1")
            {
                try
                {
                    IBLL.IClientBLL bll = new BLL.ClientBLL();
                    if (1 == 1)
                    {
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadBranch(out errId, out errMsg);
                        if (errId != 0)
                        {
                            new MsgForm("下载机构档案异常:" + errMsg).ShowDialog();
                            Log.writeLog("WaitForm()", "下载机构档案异常:" + errMsg, null);
                        }
                        Application.DoEvents();
                    }
                    if (1 == 1)
                    {
                        
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadOper(out errId, out errMsg);
                        if (errId != 0)
                        {
                            new MsgForm("下载操作员档案异常:" + errMsg).ShowDialog();
                            Log.writeLog("WaitForm()", "下载操作员档案异常:" + errMsg, null);
                        }
                        Application.DoEvents();
                    }

                    if (1 == 1)
                    {
                        
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadSystemPars(out errId, out errMsg);
                        if (errId != 0)
                        {
                            new MsgForm("下载系统参数异常:" + errMsg).ShowDialog();
                            Log.writeLog("WaitForm()", "下载系统参数异常:" + errMsg, null);
                        }
                        Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    new MsgForm("初始化数据异常:" + ex.GetMessage()).ShowDialog();
                    Log.writeLog("WaitForm()", "初始化数据异常:" + ex.GetMessage(), null);
                }
                finally
                {
                    timer1.Stop();
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            else if (this.op_type == "2")
            { 
            
            }
        }

    }
}
