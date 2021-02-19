using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IvyBack.Properties;
using IvyBack.Helper;

namespace IvyBack.VoucherForm
{
    public partial class OrderMerge : Form, IOrderMerge
    {
        IOrderList orderlist;
        IOrder order;
        Form frm1;
        Form frm2;
        public OrderMerge(string text, IOrderList orderlist, IOrder order)
        {
            InitializeComponent();
            this.Text = text;
            this.orderlist = orderlist;
            this.order = order;
            frm1 = (Form)orderlist;
            frm2 = (Form)order;
            //
            orderlist.SetOrder(order);
            orderlist.SetOrderMerge(this);
            order.SetOrderList(orderlist);
            order.SetOrderMerge(this);
            //
            frm1.FormBorderStyle = FormBorderStyle.None;
            frm1.Dock = DockStyle.Fill;
            frm1.TopLevel = false;
            frm1.Show();
            this.panel1.Controls.Add(frm1);
            frm2.FormBorderStyle = FormBorderStyle.None;
            frm2.Dock = DockStyle.Fill;
            frm2.TopLevel = false;
            frm2.Tag = new Dictionary<string, object>();
            frm2.Show();
            this.panel1.Controls.Add(frm2);
            //
            RunType = 2;
        }
        public OrderMerge(string text, string btnText1, string btnText2, Form frm1, Form frm2)
        {
            InitializeComponent();
            this.Text = text;
            this.myButton1.Text = btnText1;
            this.myButton2.Text = btnText2;
            this.frm1 = frm1;
            this.frm2 = frm2;
            //
            frm1.FormBorderStyle = FormBorderStyle.None;
            frm1.Dock = DockStyle.Fill;
            frm1.TopLevel = false;
            frm1.Show();
            this.panel1.Controls.Add(frm1);
            frm2.FormBorderStyle = FormBorderStyle.None;
            frm2.Dock = DockStyle.Fill;
            frm2.TopLevel = false;
            frm2.Tag = new Dictionary<string, object>();
            frm2.Show();
            this.panel1.Controls.Add(frm2);
            //
            RunType = 2;
        }

        private int runType = 2;
        private int RunType
        {
            get
            {
                return runType;
            }
            set
            {
                runType = value;
                if (runType == 1)
                {
                    myPanel1.BackgroundImage = Properties.Resources.单据Tab1;
                    //
                    frm1.BringToFront();
                }
                else if (runType == 2)
                {
                    myPanel1.BackgroundImage = Properties.Resources.单据Tab2;
                    //
                    frm2.BringToFront();
                }
            }
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            RunType = 1;
        }

        private void myButton2_Click(object sender, EventArgs e)
        {
            RunType = 2;
        }


        void IOrderMerge.ShowForm1()
        {
            RunType = 1;
        }

        void IOrderMerge.ShowForm2()
        {
            RunType = 2;
        }


        void IOrderMerge.Close1()
        {
            this.Close();

        }

        void IOrderMerge.Close2()
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Dictionary<string, object> dic = frm2.Tag as Dictionary<string, object>;
            if (order != null && !Conv.ControlsCom((Form)order, dic))
            {
                switch (YesNoSaveForm.ShowFrom("数据已修改,是否保存?"))
                {
                    case DialogResult.Yes:
                        order.Save();
                        e.Cancel = true;
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }

            base.OnClosing(e);
        }
        private void OrderMerge_Shown(object sender, EventArgs e)
        {
            Helper.GlobalData.windows.CloseLoad(this);
        }

        private void OrderMerge_Load(object sender, EventArgs e)
        {
        }

    }
}
