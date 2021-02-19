using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace b2bclient
{
    public partial class frmMain : Form
    {
        MainPanel mainpanel ;
        private BLL.IOrder bll = new BLL.Order();
        private BLL.ICustomer bll2 = new BLL.Customer();
        System.Threading.Thread th;
        System.Threading.Thread th2;

        public frmMain()
        {
            InitializeComponent();
            //
            mainpanel = new MainPanel();
            this.pnl.Controls.Add(mainpanel);
            mainpanel.Dock = DockStyle.Fill;
            mainpanel.BringToFront();
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
            control.FormEsc.Bind(this);
            //
            th = new System.Threading.Thread(Tick);
            th.Start();
            //
            th2 = new System.Threading.Thread(Tick2);
            th2.Start();
        }
         
        private void Tick()
        {
            while (true)
            {
                try
                {
                    Program.lstNewOrder = bll.GetNewOrderCode();
                    Program.lstNewCus = bll2.GetNewCustomerNo();
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("frmMain->Tick()", ex.ToString());
                }
                System.Threading.Thread.Sleep(5 * 1000);
            }
        }

        private void Tick2()
        {
            PlaySound.IPlaySound p = new PlaySound.PlaySound();
            while (true)
            {
                try
                {
                    p.PlaySoundForNewOrder();
                    p.PlaySoundForNewCus();
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("frmMain->Tick2()", ex.ToString());
                }
                System.Threading.Thread.Sleep(300);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (th != null)
            {
                th.Abort();
            }
            if (th2 != null)
            {
                th2.Abort();
            }

            Application.ExitThread();
            System.Environment.Exit(0);
        }

        private void panel3_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            var frm = new frmTop(this);
            frm.ShowDialog();
        }

        private void newOrderMsg1_Click(object sender, EventArgs e)
        {
            try
            {
                 
                var ord = new body.wm_order();
                var lines = new DataTable();
                int un_read_num = 0;
                if (bll.GetFirstNewOrder(out ord, out lines,out un_read_num ) == true)
                {
                    var frm = new frmHand(ord, lines, "订单处理",un_read_num);
                    frm.ShowDialog();
                }
                else
                {
                    throw new Exception("不存在未阅读的订单");
                }
                
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);

            }
            
        }

        private void newCusMsg1_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.ICustomer cbll = new BLL.Customer();
                var cus = new body.customer();
                int un_read_num = 0;
                if (cbll.GetFirstNewCustomer(out cus, out un_read_num) == true)
                {
                    var frm = new customer.frmCusHand(cus, "新客户审核", un_read_num);
                    frm.ShowDialog();
                }
                else
                {
                    throw new Exception("不存在未阅读的货商信息");
                }

            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                var f = new frmIcon(this);
                f.Show();
            }
        }

    }
}
