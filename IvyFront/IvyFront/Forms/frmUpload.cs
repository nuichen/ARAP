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
    public partial class frmUpload : Form
    {
        public frmUpload()
        {
            InitializeComponent();
        }

       
        public void Upload()
        {
            this.ShowDialog();
        }


        private int flag = 0;
        int dotnum = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            dotnum++;
            if (dotnum > 20)
            {
                dotnum = 1;
            }
            label1.Text = new string('.', dotnum);
            if (flag == 1)
            {

                timer1.Enabled = false;
                this.Close();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (flag == 0)
            {
                e.Cancel = true;
            }
        }

        private void frmDownLoad_Load(object sender, EventArgs e)
        {
            MyTh ins = new MyTh();
            ins.mainThreadCon = this;
            ins.deleg = this.EndCallHandler;
            System.Threading.Thread th = new System.Threading.Thread(ins.doing);
            th.Start();
        }

        public void EndCallHandler(int errId, string errMsg)
        {
            try
            {
                if (errId == 99)
                {
                    lbl_tip.Text = errMsg;
                }
                else if (errId == 0)
                {
                    var frm = new MsgForm("上传单据成功");
                    frm.ShowDialog();
                    this.flag = 1;
                }
                else
                {
                    var frm = new MsgForm(errMsg);
                    frm.ShowDialog();
                    this.flag = 1;
                }
            }
            catch (Exception ex)
            {
                Log.writeLog("frmDownLoad ->EndCallHandler()", ex.ToString(), null);
            }
        }

        public delegate void EndCall(int errId, string errMsg);
    
        public class MyTh
        {
            public System.Windows.Forms.Control mainThreadCon;
            public EndCall deleg;
            public void doing()
            {
                try
                {
                    int eId = 99;
                    string eMsg = "";
                    IBLL.IClientBLL bll = new BLL.ClientBLL();
                    //上传销售出库单
                    decimal count = Conv.ToDecimal(bll.GetNoUpLoadSaleCount());
                    for (var i = 0; i < count / 100; i++) 
                    {
                        bll.UpLoadSale(out eId, out eMsg);
                        if (eId == 0)
                        {
                            int temp_i = (int)count;
                            if (count > 100) temp_i = (i + 1) * 100;
                            mainThreadCon.Invoke(deleg, 99, "销售出库单上传完成: " + temp_i);
                        }
                        else
                        {
                            throw new Exception("销售出库单上传异常:" + eMsg);
                            //mainThreadCon.Invoke(deleg, 99, eMsg);
                        }
                    }
                    
                    //上传采购入库单
                    count = Conv.ToDecimal(bll.GetNoUpLoadInOutCount());
                    for (var i = 0; i < count / 100; i++)
                    {
                        bll.UpLoadInOut(out eId, out eMsg);
                        if (eId == 0)
                        {
                            int temp_i = (int)count;
                            if (count > 100) temp_i = (i + 1) * 100;
                            mainThreadCon.Invoke(deleg, 99, "采购入库单上传完成: " + temp_i);
                        }
                        else
                        {
                            throw new Exception("销售出库单上传异常:" + eMsg);
                            //mainThreadCon.Invoke(deleg, 99, eMsg);
                        }
                    }
                    
                    mainThreadCon.Invoke(deleg, 0, "");
                }
                catch (Exception ex)
                {
                    mainThreadCon.Invoke(deleg, -1, ex.GetMessage());
                    Log.writeLog("frmUpload ->MyTh.doing()", ex.ToString(), null);
                }
              
            }
        }

        private void frmDownLoad_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 255)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void pnl_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
