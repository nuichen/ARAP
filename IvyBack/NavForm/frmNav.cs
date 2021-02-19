using System;
using System.Windows.Forms;
using IvyBack.MainForm;
using IvyBack.cons;
using IvyBack.BaseForm;
using IvyBack.Properties;

namespace IvyBack.NavForm
{
    public partial class frmNav : Form
    {
        cons.WindowsList windowsList;
        public frmNav(cons.WindowsList windowsList)
        {
            InitializeComponent();
            //
            this.windowsList = windowsList;
            Helper.GlobalData.windows = this.windowsList;
            //
            Helper.GlobalData.modeulelist = moduleList1;
            
            if (1 == 1)
            {
                //frmNavPayment frm = new frmNavPayment(windowsList);
                //moduleList1.ShowForm(frm, ResourcesHelper.GetImage("导航_应收应付_1"), ResourcesHelper.GetImage("导航_应收应付_2"));
                frmNavCommon frm = new frmNavCommon(windowsList);
                //功能
                frm.SetMenu("初始信息,客户期初,供应商期初,结算方式,收支类型");
                frm.SetMenu("其他应收应付,其他应收,其他应付,客户核销单,供应商核销单");
                frm.SetMenu("结算管理,客户结算单,供应商结算单,客户账期通知单,供应商账期通知单");
                frm.SetMenu("冲账管理,应收应付冲账,应收转应收,应付转应付");
                //报表
                frm.SetReportABC("客户往来明细账,供应商往来明细账");
                frm.SetReportABC("客户往来余额表,供应商往来余额表");
                frm.SetReportABC("客户账龄分析表,供应商账龄分析表");
                frm.ItemClick += this.arap_item_click;
                //moduleList1.ShowForm(frm, ResourcesHelper.GetImage("应收应付-未选中"), ResourcesHelper.GetImage("应收应付-选中"));
                moduleList1.ShowForm(frm, Resources.应收应付_未选中, Resources.应收应付_选中);
            }
            if (1 == 1)
            {
                //frmNavCashier frm = new frmNavCashier(windowsList);
                //moduleList1.ShowForm(frm, ResourcesHelper.GetImage("导航_出纳管理_1"), ResourcesHelper.GetImage("导航_出纳管理_2"));
                frmNavCommon frm = new frmNavCommon(windowsList);
                //功能
                frm.SetMenu("初始信息,现金银行档案,现金银行期初");
                frm.SetMenu("收付款单,客户收款,供应商付款,其他收入单,其他支出单,现金银行转账单");
                //报表
                frm.SetReportABC("现金银行明细账");
                frm.SetReportABC("现金银行余额表");
                frm.ItemClick += this.cashier_item_click;
                moduleList1.ShowForm(frm, ResourcesHelper.GetImage("出纳_未选中"), ResourcesHelper.GetImage("出纳_选中"));
            }


            moduleList1.ShowForm(1);

        }
        private void arap_item_click(string item)
        {
            #region 功能模块
            //初始信息
            if (item == "客户期初")
            {
                frmSupcustInitial frm = new frmSupcustInitial();
            frm.runType = 1;
            windowsList.ShowForm(frm);
            }
            if (item == "供应商期初")
            {
                frmSupcustInitial frm = new frmSupcustInitial();
                frm.runType = 0;
                windowsList.ShowForm(frm);
            }
            if (item == "结算方式")
            {
                var frm = new FinanceForm.frmPayment();
                windowsList.ShowForm(frm);
            }
            if (item == "收支类型")
            {
                var frm = new FinanceForm.frmIncome();
                windowsList.ShowForm(frm);
            }
            //其他应收应付
            if (item == "其他应收")
            {
                var frm = new VoucherForm.OrderMerge("其他应收", new VoucherForm.frmCusReceivableList(), new VoucherForm.frmCusReceivable(windowsList));
                windowsList.ShowForm(frm);
            }
            if (item == "其他应付")
            {
                var frm = new VoucherForm.OrderMerge("其他应付", new VoucherForm.frmSupPayableList(), new VoucherForm.frmSupPayable(windowsList));
                windowsList.ShowForm(frm);
            }
            if (item == "客户核销单")
            {
                var frm = new VoucherForm.OrderMerge("客户核销单", new VoucherForm.frmCusCollectionARAPList(1), new VoucherForm.frmCusCollectionARAP(1));
                windowsList.ShowForm(frm);
            }
            if (item == "供应商核销单")
            {
                var frm = new VoucherForm.OrderMerge("供应商核销单", new VoucherForm.frmCusCollectionARAPList(0), new VoucherForm.frmCusCollectionARAP(0));
                windowsList.ShowForm(frm);
            }
            //结算管理
            if (item == "客户结算单")
            {
                var frm = new VoucherForm.OrderMerge("客户结算单", new VoucherForm.frmCusCollectionList(1), new VoucherForm.frmCusCollection(1));
                windowsList.ShowForm(frm);
            }
            if (item == "供应商结算单")
            {
                var frm = new VoucherForm.OrderMerge("供应商结算单", new VoucherForm.frmCusCollectionList(0), new VoucherForm.frmCusCollection(0));
                windowsList.ShowForm(frm);
            }
            if (item == "客户账期通知单")
            {
                var frm = new VoucherForm.OrderMerge("客户账期通知单", new VoucherForm.frmSupcustNoticeList(1), new VoucherForm.frmSupcustNotice(1));
                windowsList.ShowForm(frm);
            }
            if (item == "供应商账期通知单")
            {
                var frm = new VoucherForm.OrderMerge("供应商账期通知单", new VoucherForm.frmSupcustNoticeList(0), new VoucherForm.frmSupcustNotice(0));
                windowsList.ShowForm(frm);
            }
            //冲账管理
            if (item == "应收应付冲账")
            {
                var frm = new VoucherForm.OrderMerge("应收应付冲账", new VoucherForm.frmArToApList(), new VoucherForm.frmArToAp());
                windowsList.ShowForm(frm);
            }
            if (item == "应收转应收")
            {
                var frm = new VoucherForm.OrderMerge("应收转应收", new VoucherForm.frmArToArList(), new VoucherForm.frmArToAr());
                windowsList.ShowForm(frm);
            }
            if (item == "应付转应付")
            {
                var frm = new VoucherForm.OrderMerge("应付转应付", new VoucherForm.frmApToApList(), new VoucherForm.frmApToAp());
                windowsList.ShowForm(frm);
            }
           
            #endregion

            #region 报表
            if (item == "客户往来明细账")
            {
                var frm = new ReportForm.frmFinanceReport();
                frm.GetCusContactDetails();
                windowsList.ShowForm(frm);
            }
            if (item == "供应商往来明细账")
            {
                var frm = new ReportForm.frmFinanceReport();
                frm.GetSupContactDetails();
                windowsList.ShowForm(frm);
            }
            if (item == "客户往来余额表")
            {
                var frm = new ReportForm.frmFinanceReport();
                frm.GetCusBalance();
                windowsList.ShowForm(frm);
            }
            if (item == "供应商往来余额表")
            {
                var frm = new ReportForm.frmFinanceReport();
                frm.GetSupBalance();
                windowsList.ShowForm(frm);
            }
            if (item == "客户账龄分析表")
            {
                var frm = new ReportForm.frmFinanceReport();
                frm.GetCusAgingGroup();
                windowsList.ShowForm(frm);
            }
            if (item == "供应商账龄分析表")
            {
                var frm = new ReportForm.frmFinanceReport();
                frm.GetSupAgingGroup();
                windowsList.ShowForm(frm);
            }
            #endregion

        }
        private void cashier_item_click(string item)
        {
            #region 功能模块
            //初始信息
            if (item == "现金银行档案")
            {
                var frm = new FinanceForm.frmBank();
                windowsList.ShowForm(frm);
            }
            if (item == "现金银行期初")
            {
                frmBankCashInitial frm = new frmBankCashInitial();
                //frm.runType = 1;
                windowsList.ShowForm(frm);
            }
            //收付款单
            if (item == "客户收款")
            {
                var frm = new VoucherForm.OrderMerge("客户收款单", new VoucherForm.frmCusCollectionPayList(1), new VoucherForm.frmCusCollectionPay(1));
                windowsList.ShowForm(frm);
            }
            if (item == "供应商付款")
            {
                var frm = new VoucherForm.OrderMerge("供应商付款单", new VoucherForm.frmCusCollectionPayList(0), new VoucherForm.frmCusCollectionPay(0));
                windowsList.ShowForm(frm);
            }
            if (item == "其他收入单")
            {
                var frm = new VoucherForm.OrderMerge("其他收入单", new VoucherForm.frmOtherIncomeList("C"), new VoucherForm.frmOtherIncome(windowsList, "C"));
                windowsList.ShowForm(frm);
            }
            if (item == "其他支出单")
            {
                var frm = new VoucherForm.OrderMerge("其他支出单", new VoucherForm.frmOtherIncomeList("S"), new VoucherForm.frmOtherIncome(windowsList, "S"));
                windowsList.ShowForm(frm);
            }
            if (item == "现金银行转账单")
            {
                var frm = new VoucherForm.OrderMerge("现金银行转账单", new VoucherForm.frmCashOrderList(), new VoucherForm.frmCashOrder());
                windowsList.ShowForm(frm);
            }
            ////结算管理
            //if (item == "客户结算单")
            //{
            //    var frm = new VoucherForm.OrderMerge("客户结算单", new VoucherForm.frmCusCollectionList(1), new VoucherForm.frmCusCollection(1));
            //    windowsList.ShowForm(frm);
            //}
            //if (item == "供应商结算单")
            //{
            //    var frm = new VoucherForm.OrderMerge("供应商结算单", new VoucherForm.frmCusCollectionList(0), new VoucherForm.frmCusCollection(0));
            //    windowsList.ShowForm(frm);
            //}
            //if (item == "客户账期通知单")
            //{
            //    var frm = new VoucherForm.OrderMerge("客户账期通知单", new VoucherForm.frmSupcustNoticeList(1), new VoucherForm.frmSupcustNotice(1));
            //    windowsList.ShowForm(frm);
            //}
            //if (item == "供应商账期通知单")
            //{
            //    var frm = new VoucherForm.OrderMerge("供应商账期通知单", new VoucherForm.frmSupcustNoticeList(0), new VoucherForm.frmSupcustNotice(0));
            //    windowsList.ShowForm(frm);
            //}
            ////冲账管理
            //if (item == "应收应付冲账")
            //{
            //    var frm = new VoucherForm.OrderMerge("应收应付冲账", new VoucherForm.frmArToApList(), new VoucherForm.frmArToAp());
            //    windowsList.ShowForm(frm);
            //}
            //if (item == "应收转应收")
            //{
            //    var frm = new VoucherForm.OrderMerge("应收转应收", new VoucherForm.frmArToArList(), new VoucherForm.frmArToAr());
            //    windowsList.ShowForm(frm);
            //}
            //if (item == "应付转应付")
            //{
            //    var frm = new VoucherForm.OrderMerge("应付转应付", new VoucherForm.frmApToApList(), new VoucherForm.frmApToAp());
            //    windowsList.ShowForm(frm);
            //}

            #endregion

            #region 报表
            if (item == "现金银行明细账")
            {
                var frm = new ReportForm.frmFinanceReport();
                frm.GetBankCashDetailed();
                windowsList.ShowForm(frm);
            }
            if (item == "现金银行余额表")
            {
                var frm = new ReportForm.frmFinanceReport();
                frm.GetBankCashBalance();
                windowsList.ShowForm(frm);
            }
            //if (item == "客户往来余额表")
            //{
            //    var frm = new ReportForm.frmFinanceReport();
            //    frm.GetCusBalance();
            //    windowsList.ShowForm(frm);
            //}
            //if (item == "供应商往来余额表")
            //{
            //    var frm = new ReportForm.frmFinanceReport();
            //    frm.GetSupBalance();
            //    windowsList.ShowForm(frm);
            //}
            //if (item == "客户账龄分析表")
            //{
            //    var frm = new ReportForm.frmFinanceReport();
            //    frm.GetCusAgingGroup();
            //    windowsList.ShowForm(frm);
            //}
            //if (item == "供应商账龄分析表")
            //{
            //    var frm = new ReportForm.frmFinanceReport();
            //    frm.GetSupAgingGroup();
            //    windowsList.ShowForm(frm);
            //}
            #endregion

        }
        private void moduleList1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmNav_Shown(object sender, EventArgs e)
        {
            moduleList1.Flush();
        }




    }
}
