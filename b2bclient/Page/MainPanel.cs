using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient
{
    public partial class MainPanel : UserControl
    {
        public MainPanel()
        {
            InitializeComponent();
            //
            this.pnl_new_Click(pnl_new, null);
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

        public void ChangePanelControl(Form frm)
        {
            this.pnl.Controls.Clear();
            //frm.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            frm.TopLevel = false; //指示子窗体非顶级窗体
            this.pnl.Controls.Add(frm);//将子窗体载入panel
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void pnl_new_Click(object sender, EventArgs e)
        {
            pnl_new.BackgroundImage = Properties.Resources.菜单_1_2;
            pnl_order.BackgroundImage = Properties.Resources.菜单_2_1;
            pnl_customer.BackgroundImage = Properties.Resources.菜单_3_1;
            pnl_mall.BackgroundImage = Properties.Resources.菜单_4_1;
            pnl_goods.BackgroundImage = Properties.Resources.菜单_5_1;
            pnl_report.BackgroundImage = Properties.Resources.菜单_6_1;
            //
            order_new frm = new order_new();
            ChangePanelControl(frm);
        }

        private void pnl_order_Click(object sender, EventArgs e)
        {
            pnl_new.BackgroundImage = Properties.Resources.菜单_1_1;
            pnl_order.BackgroundImage = Properties.Resources.菜单_2_2;
            pnl_customer.BackgroundImage = Properties.Resources.菜单_3_1;
            pnl_mall.BackgroundImage = Properties.Resources.菜单_4_1;
            pnl_goods.BackgroundImage = Properties.Resources.菜单_5_1;
            pnl_report.BackgroundImage = Properties.Resources.菜单_6_1;

            frmOrderMenu frm = new frmOrderMenu();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string str = frm.ClickCMD();
                if (str == "待处理订单")
                {
                    order_pass frm2 = new order_pass();

                    ChangePanelControl(frm2);
                }
                if (str == "已拒绝订单")
                {
                    order_disable frm2 = new order_disable();

                    ChangePanelControl(frm2);
                }
                if (str == "已完成订单")
                {
                    order_all frm2 = new order_all();

                    ChangePanelControl(frm2);
                }
            }
        }

        private void pnl_customer_Click(object sender, EventArgs e)
        {
            pnl_new.BackgroundImage = Properties.Resources.菜单_1_1;
            pnl_order.BackgroundImage = Properties.Resources.菜单_2_1;
            pnl_customer.BackgroundImage = Properties.Resources.菜单_3_2;
            pnl_mall.BackgroundImage = Properties.Resources.菜单_4_1;
            pnl_goods.BackgroundImage = Properties.Resources.菜单_5_1;
            pnl_report.BackgroundImage = Properties.Resources.菜单_6_1;

            customer.frmCustomer frm = new customer.frmCustomer();

            ChangePanelControl(frm);
        }

        private void pnl_mall_Click(object sender, EventArgs e)
        {
            pnl_new.BackgroundImage = Properties.Resources.菜单_1_1;
            pnl_order.BackgroundImage = Properties.Resources.菜单_2_1;
            pnl_customer.BackgroundImage = Properties.Resources.菜单_3_1;
            pnl_mall.BackgroundImage = Properties.Resources.菜单_4_2;
            pnl_goods.BackgroundImage = Properties.Resources.菜单_5_1;
            pnl_report.BackgroundImage = Properties.Resources.菜单_6_1;

            frmMarketMenu frm = new frmMarketMenu();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string str = frm.ClickCMD();
                if (str == "首页广告")
                {
                    market.AdList frm2 = new market.AdList();

                    ChangePanelControl(frm2);
                }

                if (str == "客户反馈")
                {
                    market.AdviceList frm2 = new market.AdviceList();

                    ChangePanelControl(frm2);
                }

                if (str == "平台设置")
                {
                    market.frmConfig frm2 = new market.frmConfig(true);

                    ChangePanelControl(frm2);
                }
            }
        }

        private void pnl_goods_Click(object sender, EventArgs e)
        {
            pnl_new.BackgroundImage = Properties.Resources.菜单_1_1;
            pnl_order.BackgroundImage = Properties.Resources.菜单_2_1;
            pnl_customer.BackgroundImage = Properties.Resources.菜单_3_1;
            pnl_mall.BackgroundImage = Properties.Resources.菜单_4_1;
            pnl_goods.BackgroundImage = Properties.Resources.菜单_5_2;
            pnl_report.BackgroundImage = Properties.Resources.菜单_6_1;

            goods.frmGoodsMenu frm = new goods.frmGoodsMenu();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string str = frm.ClickCMD();
                if (str == "商品分类")
                {
                    goods.GoodsClsList frm2 = new goods.GoodsClsList();

                    ChangePanelControl(frm2);
                }
                if (str == "商品资料")
                {
                    goods.GoodsList frm2 = new goods.GoodsList();

                    ChangePanelControl(frm2);
                }
                if (str == "客户品类绑定")
                {
                    SetGroupCls frm2 = new SetGroupCls();

                    ChangePanelControl(frm2);
                }
            }
        }

        private void pnl_report_Click(object sender, EventArgs e)
        {
            pnl_new.BackgroundImage = Properties.Resources.菜单_1_1;
            pnl_order.BackgroundImage = Properties.Resources.菜单_2_1;
            pnl_customer.BackgroundImage = Properties.Resources.菜单_3_1;
            pnl_mall.BackgroundImage = Properties.Resources.菜单_4_1;
            pnl_goods.BackgroundImage = Properties.Resources.菜单_5_1;
            pnl_report.BackgroundImage = Properties.Resources.菜单_6_2;

            report.frmReport frm = new report.frmReport();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string str = frm.ClickCMD();
                if (str == "销售明细")
                {
                    report.Rep销售明细 frm2 = new report.Rep销售明细();
                    ChangePanelControl(frm2);
                }
                if (str == "销售汇总表")
                {
                    report.Rep销售汇总表 frm2 = new report.Rep销售汇总表();
                    ChangePanelControl(frm2);
                }
                if (str == "销售日报表")
                {
                    report.Rep销售日报表 frm2 = new report.Rep销售日报表();
                    ChangePanelControl(frm2);
                }
                if (str == "单品汇总表")
                {
                    report.Rep单品汇总表 frm2 = new report.Rep单品汇总表();
                    ChangePanelControl(frm2);
                }
                if (str == "客户对账表")
                {
                    if (Program.op_type == "1000")
                    {
                        report.Rep客户对账表 frm2 = new report.Rep客户对账表();
                        ChangePanelControl(frm2);
                    }
                    else 
                    {
                        Program.frmMsg("无权限");
                    }
                }
            }
        }

    }
}
