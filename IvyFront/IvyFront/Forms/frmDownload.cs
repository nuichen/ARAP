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
    public partial class frmDownload : Form
    {
        private bool is_finish = true;

        public frmDownload(int op_type)
        {
            InitializeComponent();
        }

        private bool c1
        {
            get
            {
                if (chk1.Tag == null)
                {
                    return false;
                }
                else
                {
                    return (bool)chk1.Tag;
                }
            }
        }

        private bool c2
        {
            get
            {
                if (chk2.Tag == null)
                {
                    return false;
                }
                else
                {
                    return (bool)chk2.Tag;
                }
            }
        }

        private bool c3
        {
            get
            {
                if (chk3.Tag == null)
                {
                    return false;
                }
                else
                {
                    return (bool)chk3.Tag;
                }
            }
        }

        private bool c4
        {
            get
            {
                if (chk4.Tag == null)
                {
                    return false;
                }
                else
                {
                    return (bool)chk4.Tag;
                }
            }
        }

        private bool c5
        {
            get
            {
                if (chk5.Tag == null)
                {
                    return false;
                }
                else
                {
                    return (bool)chk5.Tag;
                }
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (is_finish)
            {
                if (!Program.is_connect)
                {
                    (new MsgForm("网络异常")).ShowDialog();
                    return;
                }
                is_finish = false;
                pro.Visible = true;
                downThread ins = new downThread();
                ins.chk1 = c1;
                ins.chk2 = c2;
                ins.chk3 = c3;
                ins.chk4 = c4;
                ins.chk5 = c5;
                ins.mainCon = this;
                ins.deleg = ThreadEnd;
                System.Threading.Thread th = new System.Threading.Thread(ins.down);
                th.Start();
            }
            else {
                MsgForm frm = new MsgForm("正在下载数据，请稍候……");
                frm.ShowDialog();
            }
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (is_finish)
            {
                this.Close();
            }
            else {
                MsgForm frm = new MsgForm("正在下载数据，请稍候……");
                frm.ShowDialog();
            }
        }

        private void chk1_Click(object sender, EventArgs e)
        {
            chk_click("chk1");
        }

        private void chk2_Click(object sender, EventArgs e)
        {
            chk_click("chk2");
        }

        private void chk3_Click(object sender, EventArgs e)
        {
            chk_click("chk3");
        }

        private void chk4_Click(object sender, EventArgs e)
        {
            chk_click("chk4");
        }

        private void chk5_Click(object sender, EventArgs e)
        {
            chk_click("chk5");
        }

        private void ThreadEnd(int errId, string errMsg)
        {
            if (errId == 0 || errId == -1)
            {
                Program.item_count = 0;
                Program.sup_count = 0;
                Program.cus_price_count = 0;
                Program.sup_price_count = 0;

                is_finish = true;
                pro.Visible = false;

                if (errId == 0)
                {
                    errMsg = "下载成功";
                }
                MsgForm frm = new MsgForm(errMsg);
                frm.ShowDialog();
                this.Close();
            }
            else 
            {
                lbl_msg.Text = errMsg;
                if (errId == -2)
                {
                    lbl_msg.ForeColor = Color.Red;
                }
                else {
                    lbl_msg.ForeColor = Color.Gray;
                }
            }

        }

        private class downThread
        {
            public bool chk1 = false;
            public bool chk2 = false;
            public bool chk3 = false;
            public bool chk4 = false;
            public bool chk5 = false;
            public System.Windows.Forms.Control mainCon;
            public delegate void ThreadEndHandler(int errId, string errMsg);
            public ThreadEndHandler deleg;
            public void down()
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
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载机构档案异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载机构档案完成");
                        }
                    }

                    if (chk1 == true)
                    {
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadItemCls(out errId, out errMsg);
                        if (errId != 0)
                        {
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载品类档案异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else 
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载品类档案完成");
                        }

                        bll.DownLoadItem(out errId, out errMsg);
                        if (errId != 0) 
                        {
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载商品档案异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载商品档案完成");
                        }
                    }
                    if (chk2 == true)
                    {
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadOper(out errId, out errMsg);
                        if (errId != 0)
                        {
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载操作员档案异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载操作员档案完成");
                        }
                    }
                    if (chk3 == true)
                    {
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadStock(out errId, out errMsg);
                        if (errId != 0)
                        {
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载库存/成本异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载库存/成本完成");
                        }
                    }
                    if (chk4 == true)
                    {
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadSupCus(out errId, out errMsg);
                        if (errId != 0)
                        {
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载供应商/客户档案异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载供应商/客户档案完成");
                        }
                    }
                    if (chk5 == true)
                    {
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadSupPrice("", "", out errId, out errMsg);
                        if (errId != 0)
                        {
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载供应商价格异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载供应商价格完成");
                        }

                        errId = 0;
                        errMsg = "";
                        bll.DownLoadCusPrice("", "", out errId, out errMsg);
                        if (errId != 0)
                        {
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载客户价格异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载客户价格完成");
                        }
                    }
                    if (1 == 1)
                    {
                        int errId = 0;
                        string errMsg = "";
                        bll.DownLoadSystemPars(out errId, out errMsg);
                        if (errId != 0)
                        {
                            if (deleg != null) mainCon.Invoke(deleg, -2, "下载系统参数异常:" + errMsg);
                            else throw new Exception(errMsg);
                        }
                        else
                        {
                            if (deleg != null) mainCon.Invoke(deleg, 1, "下载系统参数完成");
                        }
                    }

                    if (deleg != null)
                    {
                        mainCon.Invoke(deleg, 0, "");
                    }
                }
                catch (Exception ex)
                {
                    Log.writeLog("frmDownload ->downThread()", ex.GetMessage(), ex.StackTrace);
                    if (deleg != null)
                    {
                        mainCon.Invoke(deleg, -1, ex.GetMessage());
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pro2.Left += 10;
            if (pro2.Left > pro.Width)
            {
                pro2.Left = 0;
            }
        }

        private void pro2_Paint(object sender, PaintEventArgs e)
        {
            int width = 7;
            int w = pro2.Width - width * 6;
            int w2 = w / 5;
            int left = 0;
            for (int i = 0; i < 6; i++)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 119, 206)), left, 0, width, pro2.Height);
                left += w2;
            }
        }

        private void frmDownload_Load(object sender, EventArgs e)
        {
            chk1.Tag = false;
            chk2.Tag = false;
            chk3.Tag = false;
            chk4.Tag = false;
            chk5.Tag = false;
            chk_click("chk1");
            //chk_click("chk2");
            chk_click("chk3");
            chk_click("chk4");
            chk_click("chk5");
        }

        private void chk_click(string control) 
        {
            switch (control) {
                case "chk1":
                    if (c1 == true)
                    {
                        chk1.BackgroundImage = picUncheck.Image;
                        chk1.Tag = false;
                    }
                    else
                    {
                        chk1.BackgroundImage = picCheck.Image;
                        chk1.Tag = true;
                    }
                    break;
                case "chk2":
                    if (c2 == true)
                    {
                        chk2.BackgroundImage = picUncheck.Image;
                        chk2.Tag = false;
                    }
                    else
                    {
                        chk2.BackgroundImage = picCheck.Image;
                        chk2.Tag = true;
                    }
                    break;
                case "chk3":
                    if (c3 == true)
                    {
                        chk3.BackgroundImage = picUncheck.Image;
                        chk3.Tag = false;
                    }
                    else
                    {
                        chk3.BackgroundImage = picCheck.Image;
                        chk3.Tag = true;
                    }
                    break;
                case "chk4":
                    if (c4 == true)
                    {
                        chk4.BackgroundImage = picUncheck.Image;
                        chk4.Tag = false;
                    }
                    else
                    {
                        chk4.BackgroundImage = picCheck.Image;
                        chk4.Tag = true;
                    }
                    break;
                case "chk5":
                    if (c5 == true)
                    {
                        chk5.BackgroundImage = picUncheck.Image;
                        chk5.Tag = false;
                    }
                    else
                    {
                        chk5.BackgroundImage = picCheck.Image;
                        chk5.Tag = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            chk_click("chk4");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            chk_click("chk3");
        }

        private void label7_Click(object sender, EventArgs e)
        {
            chk_click("chk2");
        }

        private void label6_Click(object sender, EventArgs e)
        {
            chk_click("chk1");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            chk_click("chk5");
        }

        private void frmDownload_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 255)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (new YesNoForm("确定执行清库操作吗？").ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                var frm = new InputNumerForm("清库密码", 0);
                decimal pwd = 0;
                if (frm.Input(out pwd) && pwd == 8686)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        IBLL.ISysBLL bll = new BLL.SysBLL();
                        bll.clear_db();
                        new MsgForm("清库完成,将退出系统").ShowDialog();
                        System.Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        Log.writeLog("frmDownload ->btn_clear_Click()", ex.ToString(), Program.oper_id);
                        new MsgForm(ex.GetMessage()).ShowDialog();
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

    }
}
