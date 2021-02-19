using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.cons;
using IvyBack.BaseForm;
using IvyBack.Helper;
using IvyBack.PaymentForm;

namespace IvyBack.NavForm
{
    public partial class frmNavPayment : Form
    {
        WindowsList windowsList;
        public frmNavPayment(WindowsList windowsList)
        {
            InitializeComponent();

            this.windowsList = windowsList;
        }

        //private void myIconButton3_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton2_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton1_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton4_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton5_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton6_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton7_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton8_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton9_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void label1_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpCusSum();
        //    windowsList.ShowForm(frm);
        //}

        //private void label2_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpCusDetail();
        //    windowsList.ShowForm(frm);
        //}

        //private void label3_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpSupSum();
        //    windowsList.ShowForm(frm);
        //}

        //private void label4_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpSupDetail();
        //    windowsList.ShowForm(frm);
        //}

        //private void label5_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpSupAccount();
        //    windowsList.ShowForm(frm);
        //}

        //private void label10_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpCusAccount();
        //    windowsList.ShowForm(frm);
        //}

        //private void label9_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpTodayInc();
        //    windowsList.ShowForm(frm);
        //}

        //private void label8_Click(object sender, EventArgs e)
        //{

        //}

        //private void label7_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpCusFyDetail();
        //    windowsList.ShowForm(frm);
        //}

        //private void label6_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpSupFyDetail();
        //    windowsList.ShowForm(frm);
        //}

        //private void label15_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpCashBank();
        //    windowsList.ShowForm(frm);
        //}

        //private void label14_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetRpAdminCost();
        //    windowsList.ShowForm(frm);
        //}

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            c.ForeColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            Control c = sender as Control;
            c.ForeColor = Color.Black;
        }


        public string oper_type { get; set; }

        private void tsmiOperFnc_Click(object sender, EventArgs e)
        {
            Helper.MyLove.ShowFrom(windowsList, oper_type);
        }
        private void tsmiAddMyLove_Click(object sender, EventArgs e)
        {
            try
            {
                var oper = AppSetting.oper_types.SingleOrDefault(i => i.Value.type_name.Equals(oper_type));
                IBLL.IMyDestop bll = new BLL.MyDestop();
                bll.AddMyLove(new Model.sys_t_oper_mylove()
                {
                    oper_id = Program.oper.oper_id,
                    oper_type = oper.Value.type_id,
                    oper_date = DateTime.Now,
                });
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }
        private void myIconButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is MyIconButton)
            {
                MyIconButton btn = sender as MyIconButton;
                oper_type = btn.Text;
            }
            else if (sender is Label)
            {
                Label lbl = sender as Label;
                oper_type = lbl.Text.Trim();
            }
        }

        private void myIconButton10_Click(object sender, EventArgs e)
        {
            
        }

        //private void label11_Click(object sender, EventArgs e)
        //{
        //    var frm = new ReportForm.frmFinanceReport();
        //    frm.GetMenuTotalCost();
        //    windowsList.ShowForm(frm);
        //}

        //private void myIconButton14_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton13_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void myIconButton15_Click(object sender, EventArgs e)
        //{
            
        //}

        private void btn_stock_sum_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {
                return;
            }

            var frm = new VoucherForm.OrderMerge("应付转应付", new VoucherForm.frmApToApList(), new VoucherForm.frmApToAp());
            windowsList.ShowForm(frm);
        }

        //private void btn_stock_flow_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {
        //        return;
        //    }
        //    var frm = new ReportForm.frmStockReport();
        //    frm.GetICFlow();
        //    GlobalData.windows.ShowForm(frm);
        //}

        //private void btn_jxc_sum_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {
        //        return;
        //    }
        //    var frm = new ReportForm.frmStockReport();
        //    frm.GetJXCSum();
        //    GlobalData.windows.ShowForm(frm);
        //}

        private void btn_cust_settle2_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("客户结算单", new VoucherForm.frmCusCollectionList(1), new VoucherForm.frmCusCollection(1));
            windowsList.ShowForm(frm);
        }

        private void btn_sup_settle2_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("供应商结算单", new VoucherForm.frmCusCollectionList(0), new VoucherForm.frmCusCollection(0));
            windowsList.ShowForm(frm);
        }

        private void btn_manager_fee_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("应收应付冲账", new VoucherForm.frmArToApList(), new VoucherForm.frmArToAp());
            windowsList.ShowForm(frm);
        }

        private void btn_bank_sheet_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("应收转应收", new VoucherForm.frmArToArList(), new VoucherForm.frmArToAr());
            windowsList.ShowForm(frm);
        }

        private void btn_cust_fee_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("客户账期通知单", new VoucherForm.frmSupcustNoticeList(1), new VoucherForm.frmSupcustNotice(1));
            windowsList.ShowForm(frm);
        }

        //private void btn_sup_settle1_Click(object sender, EventArgs e)
        //{
        //    if (!MyLove.PermissionsBalidation(oper_type, "18"))
        //    {

        //        return;
        //    }
        //    var frm = new VoucherForm.OrderMerge("供应商结算单", new VoucherForm.frmSupSettleList(), new VoucherForm.frmSupSettle());
        //    windowsList.ShowForm(frm);
        //}

        private void btn_bank_info_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            frmSupcustInitial frm = new frmSupcustInitial();
            frm.runType = 1;
            windowsList.ShowForm(frm);
        }

        private void btn_pay_way_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            frmSupcustInitial frm = new frmSupcustInitial();
            frm.runType = 0;
            windowsList.ShowForm(frm);
        }

        //private void btn_sz_type_Click(object sender, EventArgs e)
        //{

        //}

        private void btn_cust_receive_Click(object sender, EventArgs e)
        {
            var frm = new FinanceForm.frmIncomeRevenue();
            windowsList.ShowForm(frm);
        }

        private void btn_other_receivable_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("其他应收", new VoucherForm.frmCusReceivableList(), new VoucherForm.frmCusReceivable(windowsList));
            windowsList.ShowForm(frm);
        }

        private void btn_other_payable_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("其他应付", new VoucherForm.frmSupPayableList(), new VoucherForm.frmSupPayable(windowsList));
            windowsList.ShowForm(frm);
        }

        private void btn_cust_s_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("供应商账期通知单", new VoucherForm.frmSupcustNoticeList(0), new VoucherForm.frmSupcustNotice(0));
            windowsList.ShowForm(frm);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new ReportForm.frmFinanceReport();
            frm.GetCusContactDetails();
            windowsList.ShowForm(frm);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new ReportForm.frmFinanceReport();
            frm.GetCusBalance();
            windowsList.ShowForm(frm);
        }

        private void label20_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new ReportForm.frmFinanceReport();
            frm.GetSupContactDetails();
            windowsList.ShowForm(frm);
        }

        private void label21_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new ReportForm.frmFinanceReport();
            frm.GetSupBalance();
            windowsList.ShowForm(frm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void label22_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new ReportForm.frmFinanceReport();
            frm.GetCusAgingGroup();
            windowsList.ShowForm(frm);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new ReportForm.frmFinanceReport();
            frm.GetSupAgingGroup();
            windowsList.ShowForm(frm);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("客户核销单", new VoucherForm.frmCusCollectionARAPList(1), new VoucherForm.frmCusCollectionARAP(1));
            windowsList.ShowForm(frm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new VoucherForm.OrderMerge("供应商核销单", new VoucherForm.frmCusCollectionARAPList(0), new VoucherForm.frmCusCollectionARAP(0));
            windowsList.ShowForm(frm);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new FinanceForm.frmPayment();
            windowsList.ShowForm(frm);
        }

        private void btn_sz_type_Click_1(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(oper_type, "18"))
            {

                return;
            }
            var frm = new FinanceForm.frmIncome();
            windowsList.ShowForm(frm);
        }
    }
}
