using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.BaseForm;

namespace IvyBack.Helper
{
    public class MyLove
    {
        public static void ShowFrom(cons.WindowsList windows, string oper_type)
        {
            var oper = AppSetting.oper_types.SingleOrDefault(i => i.Value.type_name.Equals(oper_type));
            if (!oper.Equals(default(KeyValuePair<string, Model.sys_t_oper_type>)))
            {
                if (!PermissionsBalidation(oper.Value.type_id, "18")) return;

                Form frm = GetFrom(windows, oper.Value.type_id);
                if (frm.Tag != null && frm.Tag.ToString().Equals("1"))
                {
                    frm.ShowDialog();
                }
                else
                {
                    windows.ShowForm(frm);
                }
            }
        }

        public static Form GetFrom(cons.WindowsList windows, string oper_id)
        {
            Form frm = new Form();
            switch (oper_id)
            {
                /*
                    ----------------------信息档案----------------------
                 */
                //case "010100":
                //    frm = new frmRegion("C");
                //    break;
                //case "010200":
                //    frm = new frmItemCls();
                //    break;
                //case "010300":
                //    frm = new frmBranch(windows);
                //    break;
                //case "010400":
                //    frm = new frmSupcust()
                //    {
                //        runType = 1 // 0 : 供应商     1: 客户
                //    };
                //    break;
                //case "010500":
                //    frm = new frmItem(windows);
                //    break;
                //case "010600":
                //    frm = new frmPeople();
                //    break;
                //case "010700":
                //    frm = new frmSupcust()
                //    {
                //        runType = 0 // 0 : 供应商     1: 客户
                //    };
                //    break;
                //case "010800":
                //    frm = new frmSupcustGroup();
                //    frm.Tag = "1";
                //    break;
                //case "010900":
                //    frm = new frmDept();
                //    break;
                ///*
                //    ----------------------价格管理----------------------
                // */
                //case "020001":
                //    var frmStock = new ReportForm.frmStockReport();
                //    frmStock.GetPmDetail();
                //    frm = frmStock;
                //    break;
                //case "020100":
                //    frm = new PriceForm.frmAdPrice();
                //    break;
                //case "020200":
                //    frm = new PriceForm.frmCustPrice();
                //    break;
                //case "020300":
                //    frm = new PriceForm.frmSupPrice();
                //    break;
                //case "020400":
                //    frm = new VoucherForm.OrderMerge("调价单", new VoucherForm.frmCusPriceOrderList(), new VoucherForm.frmCusPriceOrder());
                //    break;
                ///*
                //    ----------------------采购管理----------------------
                // */
                //case "030001":
                //    var frms2 = new ReportForm.frmPurchaseReport();
                //    frms2.GetCGSum();
                //    frm = frms2;
                //    break;
                //case "030002":
                //    frms2 = new ReportForm.frmPurchaseReport();
                //    frms2.GetCGDetail();
                //    frm = frms2;
                //    break;
                //case "030003":
                //    frms2 = new ReportForm.frmPurchaseReport();
                //    frms2.GetCGOrderSum();
                //    frm = frms2;
                //    break;
                //case "030004":
                //    frms2 = new ReportForm.frmPurchaseReport();
                //    frms2.GetCGMoreSup();
                //    frm = frms2;
                //    break;
                //case "030005":
                //    frms2 = new ReportForm.frmPurchaseReport();
                //    frms2.GetCGItemDetail();
                //    frm = frms2;
                //    break;
                //case "030006":
                //    var frmPurchaseReport = new ReportForm.frmPurchaseReport();
                //    frmPurchaseReport.GetReceiveOrderDetail();
                //    frm = frmPurchaseReport;
                //    break;
                //case "030007":
                //    frmPurchaseReport = new ReportForm.frmPurchaseReport();
                //    frmPurchaseReport.GetAssCGPlanDetail();
                //    frm = frmPurchaseReport;
                //    break;
                //case "030008":
                //    frmPurchaseReport = new ReportForm.frmPurchaseReport();
                //    frmPurchaseReport.GetAssCGPreDetail();
                //    frm = frmPurchaseReport;
                //    break;
                //case "030100":
                //    frm = new OrderForm.frmPHOrderList();
                //    break;
                //case "030200":
                //    frm = new VoucherForm.OrderMerge("采购订单", new VoucherForm.frmCGOrderList(), new VoucherForm.frmCGOrder());
                //    break;
                //case "030300":
                //    frm = new VoucherForm.OrderMerge("采购入库单", new VoucherForm.frmCGInSheetList(), new VoucherForm.frmCGInSheet());
                //    break;
                //case "030400":
                //    frm = new PurchaseFrom.frmCGOrder();
                //    frm.Tag = "1";
                //    break;
                //case "030500":
                //    frm = new PurchaseFrom.frmCGInSheet();
                //    frm.Tag = "1";
                //    break;
                //case "030600":
                //    frm = frm = new VoucherForm.OrderMerge("采购退货单", new VoucherForm.frmCGOutSheetList(), new VoucherForm.frmCGOutSheet());
                //    break;
                ///*
                //   ----------------------销售管理----------------------
                //*/
                //case "040001":
                //    var frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetSaleSum();
                //    frm = frm2;
                //    break;
                //case "040002":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetSaleDetail();
                //    frm = frm2;
                //    break;
                //case "040003":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetSaleOutDetail();
                //    frm = frm2;
                //    break;
                //case "040004":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetCusCredit();
                //    frm = frm2;
                //    break;
                //case "040005":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetNoSaleCus();
                //    frm = frm2;
                //    break;
                //case "040006":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetSheetPayInfo();
                //    frm = frm2;
                //    break;
                //case "040007":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetSaleItemDetail();
                //    frm = frm2;
                //    break;
                //case "040008":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetSaleOrderSum();
                //    frm = frm2;
                //    break;
                //case "040009":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetSaleOrderDetail();
                //    frm = frm2;
                //    break;
                //case "040010":
                //    frm2 = new ReportForm.frmSaleReport();
                //    frm2.GetSaleOrderRealDiff();
                //    frm = frm2;
                //    break;
                //case "040011":
                //    var frmStockReport = new ReportForm.frmStockReport();
                //    frmStockReport.GetPickingDetail();
                //    frm = frmStockReport;
                //    break;
                //case "040012":
                //    frmStockReport = new ReportForm.frmStockReport();
                //    frmStockReport.GetPickingDiff();
                //    frm = frmStockReport;
                //    break;
                //case "040100"://
                //    frm = new VoucherForm.OrderMerge("销售订货单", new VoucherForm.frmSaleSSSheetList(), new VoucherForm.frmSaleSSSheet());
                //    break;
                //case "040200":
                //    frm = new VoucherForm.OrderMerge("批发销售单", new VoucherForm.frmSaleSheetList(), new VoucherForm.frmSaleSheet());
                //    break;
                //case "040300":
                //    frm = new VoucherForm.OrderMerge("客户退货单", new VoucherForm.frmSaleInSheetList(), new VoucherForm.frmSaleInSheet());
                //    break;
                //case "040400":
                //    frm = new SaleForm.frmSaleSSSheet();
                //    frm.Tag = "1";
                //    break;
                //case "040500":
                //    frm = new SaleForm.frmSaleSheet();
                //    frm.Tag = "1";
                //    break;
                ///*
                //----------------------库存管理----------------------
                //*/
                //case "050001":
                //    frmStock = new ReportForm.frmStockReport();
                //    frmStock.GetICSum();
                //    frm = frmStock;
                //    break;
                //case "050002":
                //    frmStock = new ReportForm.frmStockReport();
                //    frmStock.GetICFlow();
                //    frm = frmStock;
                //    break;
                //case "050003":
                //    frmStock = new ReportForm.frmStockReport();
                //    frmStock.GetJXCSum();
                //    frm = frmStock;
                //    break;
                //case "050004":
                //    frmStock = new ReportForm.frmStockReport();
                //    frmStock.GetCheckPlanDetail();
                //    frm = frmStock;
                //    break;
                //case "050005":
                //    frmStock = new ReportForm.frmStockReport();
                //    frmStock.GetICOutDetail();
                //    frm = frmStock;
                //    break;
                //case "050006":
                //    frmStockReport = new ReportForm.frmStockReport();
                //    frmStockReport.GetInventoryCheck();
                //    frm = frmStockReport;
                //    break;
                //case "050100":
                //    frm = new VoucherForm.OrderMerge("盘点初始化", new StockForm.frmStockPDList(), new StockForm.frmStockInit());
                //    break;
                //case "050200":
                //    frm = new VoucherForm.OrderMerge("库存盘点", new VoucherForm.frmCheckSheetList(), new VoucherForm.frmCheckSheet());
                //    break;
                //case "050300":
                //    frmStock = new ReportForm.frmStockReport();
                //    frmStock.GetCheckPlan();
                //    frm = frmStock;
                //    break;
                //case "050400":
                //    frm = new VoucherForm.OrderMerge("盘点结束审核", new StockForm.frmStockPCList(), new StockForm.frmStockPC());
                //    break;
                //case "050500":
                //    frm = new VoucherForm.OrderMerge("其他出入单", new VoucherForm.frmOtherInOutSheetList(), new VoucherForm.frmOtherInOutSheet());
                //    break;
                //case "050600":
                //    frm = new VoucherForm.OrderMerge("调拨单", new VoucherForm.frmIOList(), new VoucherForm.frmIOMaster());
                //    break;
                /*
                ----------------------财务管理----------------------
                */
                //case "060001":
                //    var frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpCusSum();
                //    frm = frmFinanceReport;
                //    break;
                //case "060002":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpCusDetail();
                //    frm = frmFinanceReport;
                //    break;
                //case "060003":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpSupSum();
                //    frm = frmFinanceReport;
                //    break;
                //case "060004":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpSupDetail();
                //    frm = frmFinanceReport;
                //    break;
                //case "060005":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpSupAccount();
                //    frm = frmFinanceReport;
                //    break;
                //case "060006":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpCusAccount();
                //    frm = frmFinanceReport;
                //    break;
                //case "060007":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpTodayInc();
                //    frm = frmFinanceReport;
                //    break;
                //case "060008":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpTodayPay();
                //    frm = frmFinanceReport;
                //    break;
                //case "060009":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpCusFyDetail();
                //    frm = frmFinanceReport;
                //    break;
                //case "060010":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpSupFyDetail();
                //    frm = frmFinanceReport;
                //    break;
                //case "060011":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpCashBank();
                //    frm = frmFinanceReport;
                //    break;
                //case "060012":
                //    frmFinanceReport = new ReportForm.frmFinanceReport();
                //    frmFinanceReport.GetRpAdminCost();
                //    frm = frmFinanceReport;
                //    break;
                //
                //case "060100":
                //    frm = new FinanceForm.frmBank();
                //    break;
                //case "060200":
                //    frm = new FinanceForm.frmPayment();
                //    break;
                //case "060300":
                //    frm = new FinanceForm.frmIncome();
                //    break;
                //case "060400":
                //    frm = new VoucherForm.OrderMerge("客户费用单", new VoucherForm.frmCusFYList(), new VoucherForm.frmCusFY());
                //    break;
                //case "060500":
                //    frm = new VoucherForm.OrderMerge("供应商费用单", new VoucherForm.frmSupFYList(), new VoucherForm.frmSupFY());
                //    break;
                //case "060600":
                //    frm = new VoucherForm.OrderMerge("现金银行转账单", new VoucherForm.frmCashOrderList(), new VoucherForm.frmCashOrder());
                //    break;
                //case "060700":
                //    frm = new VoucherForm.OrderMerge("管理费用单", new VoucherForm.frmFYOrderList(), new VoucherForm.frmFYOrder());
                //    break;
                //case "060800":
                //    frm = new VoucherForm.OrderMerge("供应商结算单", new VoucherForm.frmSupSettleList(), new VoucherForm.frmSupSettle());
                //    break;
                //case "060900":
                //    frm = new VoucherForm.OrderMerge("客户结算单", new VoucherForm.frmCusSettleList(), new VoucherForm.frmCusSettle());
                //    break;
                /*
                ----------------------系统管理----------------------
                */
                //case "070100":
                //    frm = new SysForm.frmOper();
                //    break;
                //case "070200":
                //    frm = new SysForm.frmOperGrant();
                //    break;
                //case "070300":
                //    frm = new SysForm.SysSetting();
                //    frm.Tag = "1";
                //    break;
                //case "070400":
                //    frm = new SysForm.frmUpdatePwd();
                //    frm.Tag = "1";
                //    break;
                //case "070500":
                //    frm = new IvyBack.SysForm.market.AdList();
                //    break;
                //case "070600":
                //    frm = new IvyBack.SysForm.market.AdviceList();
                //    break;
                //case "070700":
                //    frm = new SysForm.frmRegisterList();
                //    frm.Tag = "1";
                //    break;
                //case "070800":
                //    frm = new IvyBack.SysForm.market.frmConfig(true);
                //    frm.Tag = "1";
                //    break;
            }
            return frm;
        }


        public static bool PermissionsBalidation(string oper_type, string type)
        {
            int flag = -1;//0：成功  -1：没有权限  -2：没有找到加密狗
            if (Program.oper.oper_type.Equals("1000"))
            {
                flag = 0;
            }
            else
            {
                var oper = AppSetting.oper_types.SingleOrDefault(i => i.Value.type_name.Equals(oper_type) || i.Value.type_id == oper_type).Value;
                //新增
                if (oper == null)
                {
                    oper = AppSetting.oper_types.SingleOrDefault(i => i.Value.type_id == type).Value;
                }
                if (oper != null)
                {
                    Model.sa_t_oper_grant gr;
                    if (AppSetting.oper_grants.TryGetValue(oper.type_id, out gr))
                    {
                        //获取对应菜单的权限列表
                        if (gr.grant_string.IndexOf(type, StringComparison.Ordinal) > -1)
                        {
                            //重要业务  （新增 修改 保存 审核 删除） 都需要验证加密狗
                            if (type.Equals("01") || type.Equals("02") || type.Equals("04") || type.Equals("05") || type.Equals("14"))
                            {
                                if (SoftUpdate.VerifySoft() != 1)
                                {
                                    flag = -2;
                                }
                                else
                                {
                                    flag = 0;
                                }
                            }
                            else
                            {
                                flag = 0;
                            }
                        }
                    }
                    else if (string.IsNullOrEmpty(oper.type_value))
                    {
                        //菜单权限为空 不限制
                        flag = 0;
                    }

                }
            }


            switch (flag)
            {
                case 0:
                    return true;
                case -1:
                    MsgForm.ShowFrom("没有权限");
                    return false;
                case -2:
                    return false;
                default:
                    return false;
            }
        }


    }
}
