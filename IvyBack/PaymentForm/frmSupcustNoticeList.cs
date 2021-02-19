using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using System.Threading;
using IvyBack.MainForm;
using IvyBack.BLL;
using Model.StaticType;

namespace IvyBack.VoucherForm
{
    public partial class frmSupcustNoticeList : Form, IOrderList
    {
        private IOrder order;
        private IOrderMerge ordermerge;
        private int _runType = 0;//0:供应商 1:客户
        public int runType
        {
            get
            {
                return _runType;
            }
            set
            {
                if (value == 1)
                {
                    //this.Text = "客户期初";
                    //this.cbShowStopSup.Text = "显示已停止往来的客户";
                    //this.tsmiSupBindItem.Visible = false;
                }
                else
                {
                    label3.Text = "供 应 商：";
                }
                _runType = value;
            }
        }
        public frmSupcustNoticeList(int is_cs)
        {
            InitializeComponent();
            runType = is_cs;
            //
            Helper.GlobalData.InitForm(this);
            //
            dataGrid1.AddColumn("approve_flag", "审核状态", "", 60, 2, "{0:未审核 ,1:已审核}");
            //dataGrid1.AddColumn("trans_no", "单据类型", "", 90, 2, "{A:进货入库,D:退货入库,F:退货出库}");
            //dataGrid1.AddColumn("sale_no", "经销方式", "", 90, 2, "{A:购销,B:代销,C:联营,E:联营进货,Z:租赁}");
            dataGrid1.AddColumn("sheet_no", "货号", "", 120, 1, "");
            if (runType == 1)
            {
                dataGrid1.AddColumn("supcust_no", "客户", "", 100, 2, "");
                dataGrid1.AddColumn("sup_name", "客户名称", "", 150, 1, "");//1
            }
            else
            {
                dataGrid1.AddColumn("supcust_no", "供应商", "", 100, 2, "");
                dataGrid1.AddColumn("sup_name", "供应商名称", "", 150, 1, "");//1
            }
           
            dataGrid1.AddColumn("total_amount", "单据金额", "", 150, 3, "0.00");
            dataGrid1.AddColumn("oper_name_a", "操作员", "", 150, 1, "");//1
            dataGrid1.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd");
            dataGrid1.AddColumn("approve_man_a", "审核人", "", 150, 1, "");//1
            dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd");
            //dataGrid1.AddColumn("account_due_date", "账期截止日期", "", 100, 1, "yyyy-MM-dd");
            dataGrid1.AddColumn("remark", "备注1", "", 200, 1, "");
            dataGrid1.IsSelect = true;
            dataGrid1.MergeCell = false;
            this.dataGrid1.DataSource = new DataTable();
            //
            dataGrid2.AddColumn("voucher_no", "单号", "", 100, 1, "");
            dataGrid2.AddColumn("voucher_no_date", "单据日期", "", 150, 1, "yyyy-MM-dd");
            dataGrid2.AddColumn("sheet_id", "类型", "", 100, 2, IvyTransFunction.all_type_str);
            dataGrid2.AddColumn("due_date", "应结日期", "", 80, 1, "yyyy-MM-dd");
            dataGrid2.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00");
            dataGrid2.AddColumn("paid_amount", "已核销", "", 100, 3, "0.00");
            dataGrid2.AddColumn("unpaid_amount", "未核销", "", 100, 3, "0.00");
            dataGrid2.MergeCell = false;
            this.dataGrid2.DataSource = new DataTable();
            //
            this.dateTextBox1.Text = System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            this.dateTextBox2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
            if (runType == 1)
            {
                //var cus = paymentbll.GetSupcustList("C");
                IBLL.ICus bll = new BLL.CusBLL();
                int tmp;
                this.txt_sup_id.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:客户编码:100,sup_name:客户名称:150", "supcust_no/sup_name->Text");

            }
            else
            {
                //var cus = paymentbll.GetSupcustList("S");
                IBLL.ISup bll = new BLL.SupBLL();
                int tmp;
                this.txt_sup_id.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150", "supcust_no/sup_name->Text");
            }
        }

        private void refreshData()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                string is_cs = runType == 1 ? "C" : "S";
                DateTime date1 = Helper.Conv.ToDateTime(dateTextBox1.Text.Trim());
                DateTime date2 = Helper.Conv.ToDateTime(dateTextBox2.Text.Trim());
                string txt_sup = txt_sup_id.Text.Split('/')[0].Trim();
                Thread th = new Thread(() =>
                {
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        //IBLL.IInOutBLL bll = new BLL.InOutBLL();
                        IBLL.IARAP_SCPaymentBLL scbll = new BLL.ARAP_SCPaymentBLL();
                        if (date1 == DateTime.MinValue)
                        {
                            throw new Exception("期间不正确");
                        }
                        if (date2 == DateTime.MinValue)
                        {
                            throw new Exception("期间不正确");
                        }
                        var tb = scbll.GetList(date1, date2, txt_sup, is_cs);

                        this.dataGrid1.Invoke((MethodInvoker)delegate
                        {
                            tb.Columns.Add("row_color", typeof(Color));
                            foreach (DataRow r in tb.Rows)
                            {
                                if (!"1".Equals(r["approve_flag"].ToString()))
                                {
                                    r["row_color"] = Color.Red;
                                }
                            }
                            this.dataGrid1.DataSource = tb;
                            if (tb.Rows.Count > 0)
                            {
                                sheet_no = tb.Rows[0]["sheet_no"].ToString();
                                trans_no = "";// tb.Rows[0]["trans_no"].ToString();

                                System.Data.DataTable tb1;
                                System.Data.DataTable tb2;
                                scbll.GetOrder(sheet_no, is_cs, out tb1, out tb2);


                                this.dataGrid2.DataSource = tb2;

                                update_time = Helper.Conv.ToDateTime(tb1.Rows[0]["update_time"]);
                            }

                        });
                    }
                    catch (Exception ex)
                    {
                        LogHelper.writeLog("refreshData", ex.ToString());
                        MsgForm.ShowFrom(ex);
                    }
                    Helper.GlobalData.windows.CloseLoad(this);
                });
                th.Start();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                Helper.LogHelper.writeLog("frmSupcustNoticeList->refreshData()", ex.ToString());
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        void IOrderList.SetOrder(IOrder order)
        {
            this.order = order;
        }

        void IOrderList.SetOrderMerge(IOrderMerge ordermerge)
        {
            this.ordermerge = ordermerge;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "01"))
                {

                    return;
                }
                if (order.IsEdit() == true)
                {
                    var res = YesNoForm.ShowFrom("数据已修改，是否保存?");
                    if (res == DialogResult.Yes)
                    {
                        order.Save();
                        order.Add();
                        ordermerge.ShowForm2();
                    }
                    else if (res == DialogResult.No)
                    {
                        order.Add();
                        ordermerge.ShowForm2();
                    }
                    else if (res == DialogResult.Cancel)
                    {

                    }
                }
                else
                {
                    order.Add();
                    ordermerge.ShowForm2();
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                Helper.LogHelper.writeLog("frmSupcustNoticeList->tsbAdd_Click()", ex.ToString());
            }

        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "14"))
                {

                    return;
                }
                if (this.dataGrid1.CurrentRow() != null)
                {
                    if (order.IsEdit() == true)
                    {
                        var res = YesNoForm.ShowFrom("数据已修改，是否保存?");
                        if (res == DialogResult.Yes)
                        {
                            order.Save();
                            order.ShowOrder(this.dataGrid1.CurrentRow()["sheet_no"].ToString());
                            ordermerge.ShowForm2();
                        }
                        else if (res == DialogResult.No)
                        {
                            order.ShowOrder(this.dataGrid1.CurrentRow()["sheet_no"].ToString());
                            ordermerge.ShowForm2();
                        }
                        else if (res == DialogResult.Cancel)
                        {

                        }
                    }
                    else
                    {
                        order.ShowOrder(this.dataGrid1.CurrentRow()["sheet_no"].ToString());
                        ordermerge.ShowForm2();
                    }
                }

            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                Helper.LogHelper.writeLog("frmSupcustNoticeList->tsbEdit_Click()", ex.ToString());
            }

        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "02"))
                {

                    return;
                }

                List<DataRow> rowLis = this.dataGrid1.GetSelectDatas();
                if (rowLis == null || rowLis.Count < 1)
                {
                    return;
                }
                if (YesNoForm.ShowFrom("确认删除单据?") != DialogResult.Yes)
                {
                    return;
                }

                int ok_num = 0;
                int err_num = 0;
                StringBuilder sb = new StringBuilder("详细信息:" + Environment.NewLine);
                //IBLL.IInOutBLL bll = new BLL.InOutBLL();
                IBLL.IARAP_SCPaymentBLL bll = new ARAP_SCPaymentBLL();
                rowLis.ForEach((r) =>
                {
                    string sheet_no = r["sheet_no"].ToString();
                    try
                    {
                        bll.DeleteNotice(sheet_no, update_time);
                        var tb = this.dataGrid1.DataSource;
                        tb.Rows.Remove(r);
                        this.dataGrid2.DataSource = new DataTable();
                        ok_num++;
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine("单据：[" + sheet_no + "]，出现了异常，错误信息：" + ex.GetMessage());
                        err_num++;
                    }
                });
                string infoMain = "共删除" + rowLis.Count + "张单据,成功:" + ok_num + "张,失败:" + err_num + "张";
                ShowInfoForm.ShowFrom(infoMain, sb.ToString());
                this.dataGrid1.RowUnSelect();
                this.dataGrid1.Refresh();

            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                Helper.LogHelper.writeLog("frmSupcustNoticeList->tsbDel_Click()", ex.ToString());
            }
        }

        private void tsbCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "05"))
                {

                    return;
                }
                List<DataRow> rowLis = this.dataGrid1.GetSelectDatas();
                if (rowLis == null || rowLis.Count < 1)
                {
                    return;
                }

                int ok_num = 0;
                int err_num = 0;
                StringBuilder sb = new StringBuilder("详细信息:" + Environment.NewLine);
                IBLL.IInOutBLL bll = new BLL.InOutBLL();
                IBLL.IARAP_SCPaymentBLL scbll = new BLL.ARAP_SCPaymentBLL();
                rowLis.ForEach((r) =>
                {
                    string sheet_no = r["sheet_no"].ToString();
                    try
                    {
                        scbll.CheckNotice(sheet_no, Program.oper.oper_id, DateTime.Now);
                        r["approve_flag"] = "1";
                        r["approve_man"] = Program.oper.oper_id;
                        r["approve_date"] = System.DateTime.Now;
                        ok_num++;
                    }
                    catch (Exception ex)
                    {
                        string str = ex.GetMessage();
                        sb.AppendLine("单据：[" + sheet_no + "]，出现了异常. 错误信息：" + ex.Message);
                        err_num++;
                    }
                });
                string infoMain = "共审核" + rowLis.Count + "张单据,成功:" + ok_num + "张,失败:" + err_num + "张";
                ShowInfoForm.ShowFrom(infoMain, sb.ToString());
                this.dataGrid1.RowUnSelect();
                this.dataGrid1.Refresh();

            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                Helper.LogHelper.writeLog("frmSupcustNoticeList->tsbDel_Click()", ex.ToString());
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            ordermerge.Close1();
        }



        private void dataGrid1_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            this.tsbEdit_Click(null, null);
        }

        private string sheet_no = "";
        private string trans_no = "";
        private DateTime update_time;
        private void dataGrid1_CurrentCellChange(object sender, string column_name, DataRow row)
        {
            try
            {
                string is_cs = runType == 1 ? "C" : "S";
                if (sheet_no != row["sheet_no"].ToString())
                {
                    Thread th = new Thread(() =>
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Helper.GlobalData.windows.ShowLoad(this);
                        try
                        {
                            sheet_no = row["sheet_no"].ToString();
                            trans_no = "";//row["trans_no"].ToString();

                           // IBLL.IInOutBLL bll = new BLL.InOutBLL();
                            IBLL.IARAP_SCPaymentBLL scbll = new BLL.ARAP_SCPaymentBLL();
                            System.Data.DataTable tb1;
                            System.Data.DataTable tb2;
                            scbll.GetOrder(sheet_no, is_cs, out tb1, out tb2);

                            this.dataGrid2.Invoke((MethodInvoker)delegate
                            {
                                this.dataGrid2.DataSource = tb2;
                            });
                            update_time = Helper.Conv.ToDateTime(tb1.Rows[0]["update_time"]);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.writeLog("dataGrid1_CurrentCellChange", ex.ToString());
                            MsgForm.ShowFrom(ex);
                        }
                        Cursor.Current = Cursors.Default;
                        Helper.GlobalData.windows.CloseLoad(this);
                    });
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                Helper.LogHelper.writeLog("frmSupcustNoticeList->dataGrid1_CurrentCellChange()", ex.ToString());
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.refreshData();
        }

    }
}
