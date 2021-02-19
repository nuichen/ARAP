﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using Model.StaticType;

namespace IvyBack.VoucherForm
{
    public partial class frmCusCollectionPayList : Form, IOrderList
    {
        private int _runType1 = 0;//0:供应商 1:客户
        public int runType1
        {
            get
            {
                return _runType1;
            }
            set
            {
                if (value == 1)
                {

                }
                else
                {
                   
                }
                _runType1 = value;
            }
        }
        IBLL.ICashierBLL bll = new BLL.CashierBLL();
        public frmCusCollectionPayList(int run)
        {
           
            InitializeComponent();
            //
            runType1 = run;
            Helper.GlobalData.InitForm(this);
            //
            dataGrid1.AddColumn("approve_flag", "审核状态", "", 60, 2, "{0:未审核 ,1:已审核}");
            dataGrid1.AddColumn("sheet_no", "单据号", "", 130, 1, "");
            if (runType1 == 1)
            {
                dataGrid1.AddColumn("supcust_no", "客户编号", "", 80, 1, "");
                dataGrid1.AddColumn("sup_name", "客户名称", "", 150, 1, "");
            }
            else
            {
                dataGrid1.AddColumn("supcust_no", "供应商编号", "", 80, 1, "");
                dataGrid1.AddColumn("sup_name", "供应商名称", "", 150, 1, "");

            }
            dataGrid1.AddColumn("visa_id", "账户", "", 120, 1, "");
           
            dataGrid1.AddColumn("total_amount", "结算总金额", "", 90, 3, "0.00");
            
            dataGrid1.AddColumn("approve_man", "审核人", "", 110, 1, "");
            dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd");
            dataGrid1.AddColumn("oper_id", "出纳", "", 100, 1, "");
            dataGrid1.AddColumn("oper_date", "单据日期", "", 100, 1, "yyyy-MM-dd");
            dataGrid1.AddColumn("other1", "备注", "", 120, 1, "");
            dataGrid1.IsSelect = true;
            dataGrid1.MergeCell = false;
            dataGrid1.DataSource = new DataTable();
            dataGrid1.SetTotalColumn("total_amount");
           
            dataGrid2.AddColumn("voucher_no", "业务单号", "", 150, 1, "");
            if (runType1 == 1)
            {
                dataGrid2.AddColumn("supcust_no", "客户编号", "", 80, 1, "");
                dataGrid2.AddColumn("sup_name", "客户名称", "", 150, 1, "");
            }
            else
            {
                dataGrid2.AddColumn("supcust_no", "供应商编号", "", 80, 1, "");
                dataGrid2.AddColumn("sup_name", "供应商名称", "", 150, 1, "");

            }
            dataGrid2.AddColumn("visa_id", "账户", "", 120, 1, "");
            dataGrid2.AddColumn("oper_date", "结算单日期", "", 100, 1, "yyyy-MM-dd");
            dataGrid2.AddColumn("pay_way", "结算方式", "", 120, 1, "");
            dataGrid2.AddColumn("pay_amount", "结算金额", "", 100, 3, "0.00");
            dataGrid2.DataSource = new DataTable();
            //
            this.dateTextBox1.Text = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            this.dateTextBox2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            //if (runType1 == 1)
            //{
            //    IBLL.ICus bll = new BLL.CusBLL();
            //    int tmp;

            //    this.myTextBox1.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:客户编码:100,sup_name:客户名称:150",
            //        "supcust_no/sup_name->Text");
            //}
            //else
            //{
            //    IBLL.ISup bll = new BLL.SupBLL();
            //    int tmp;
            //    this.myTextBox1.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150",
            //        "supcust_no/sup_name->Text");
            //}
            IBLL.IBank bll = new BLL.BankBLL();
            var tb_bank = bll.GetAllList();
                this.txtbank.Bind(tb_bank, 300, 200, "visa_id", "visa_id:现金银行编码:120,visa_nm:现金银行名称:150", "visa_id/visa_nm->Text");
        }

        private IOrder order;
        void IOrderList.SetOrder(IOrder order)
        {
            this.order = order;
        }

        private IOrderMerge ordermerge;
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
            }
        }

        private void tsbUpload_Click(object sender, EventArgs e)
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
                rowLis.ForEach((r) =>
                {
                    string sheet_no = r["sheet_no"].ToString();
                    try
                    {
                        bll.DeleteCollectionPay(sheet_no,DateTime.Now);
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
                rowLis.ForEach((r) =>
                {
                    string sheet_no = r["sheet_no"].ToString();
                    try
                    {
                        string is_cs = runType1 == 1 ? "C" : "S";
                        bll.CheckCollectionPay(sheet_no, Program.oper.oper_id,DateTime.Now);
                        r["approve_flag"] = "1";
                        r["approve_man"] = Program.oper.oper_id;
                        r["approve_date"] = System.DateTime.Now;
                        ok_num++;
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine("单据：[" + sheet_no + "]，出现了异常，错误信息：" + ex.GetMessage());
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
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            ordermerge.Close1();
        }


        private void refreshData()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                DateTime date1;
                DateTime date2;
                DateTime.TryParse(dateTextBox1.Text.Trim(), out date1);
                DateTime.TryParse(dateTextBox2.Text.Trim(), out date2);
               string visa_id = txtbank.Text.Split('/')[0];
                System.Threading.Thread th = new System.Threading.Thread(() =>
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        string is_cs = runType1 == 1 ? "C" : "S";

                        if (date1 == DateTime.MinValue)
                        {
                            throw new Exception("期间不正确");
                        }
                        if (date2 == DateTime.MinValue)
                        {
                            throw new Exception("期间不正确");
                        }
                        DataTable tb = bll.GetCollectionPayList(date1, date2,is_cs, visa_id);


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

                                System.Data.DataTable tb1;
                                System.Data.DataTable tb2;
                                bll.GetCollectionPayOrder(sheet_no,is_cs, out tb1, out tb2);


                                this.dataGrid2.DataSource = tb2;
                                this.dataGrid2.Refresh();
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        IvyBack.Helper.LogHelper.writeLog("refreshData", ex.ToString());
                        MsgForm.ShowFrom(ex);
                    }
                    Cursor.Current = Cursors.Default;
                    Helper.GlobalData.windows.CloseLoad(this);
                });
                th.Start();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        string sheet_no = "";
        private void dataGrid1_CurrentCellChange(object sender, string column_name, DataRow row)
        {
            try
            {
                if (sheet_no != row["sheet_no"].ToString())
                {
                    System.Threading.Thread th = new System.Threading.Thread(() =>
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Helper.GlobalData.windows.ShowLoad(this);
                        try
                        {
                            sheet_no = row["sheet_no"].ToString();
                            System.Data.DataTable tb1;
                            System.Data.DataTable tb2;
                            string is_cs = runType1 == 1 ? "C" : "S";
                            bll.GetCollectionPayOrder(sheet_no,is_cs, out tb1, out tb2);

                            this.dataGrid2.Invoke((MethodInvoker)delegate
                            {
                                this.dataGrid2.DataSource = tb2;
                                this.dataGrid2.Refresh();
                            });
                        }
                        catch (Exception ex)
                        {
                            IvyBack.Helper.LogHelper.writeLog("dataGrid1_CurrentCellChange", ex.ToString());
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
            }
        }

        private void dataGrid1_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            this.tsbUpload_Click(null, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.refreshData();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
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
                rowLis.ForEach((r) =>
                {
                    string sheet_no = r["sheet_no"].ToString();
                    try
                    {
                        string is_cs = runType1 == 1 ? "C" : "S";
                        bll.NotCheckCollectionPay(sheet_no, Program.oper.oper_id, DateTime.Now);
                        r["approve_flag"] = "0";
                        r["approve_man"] = "";
                        r["approve_date"] = null;
                        ok_num++;
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine("单据：[" + sheet_no + "]，出现了异常，错误信息：" + ex.GetMessage());
                        err_num++;
                    }
                });
                string infoMain = "共反审" + rowLis.Count + "张单据,成功:" + ok_num + "张,失败:" + err_num + "张";
                ShowInfoForm.ShowFrom(infoMain, sb.ToString());
                this.dataGrid1.RowUnSelect();
                this.dataGrid1.Refresh();

            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }
    }
}
