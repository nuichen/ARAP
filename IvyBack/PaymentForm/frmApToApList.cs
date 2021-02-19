using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.BLL;
using IvyBack.Helper;
using IvyBack.IBLL;
using Model.StaticType;

namespace IvyBack.VoucherForm
{
    public partial class frmApToApList : Form, IOrderList
    {


        public frmApToApList( )
        {
           
            InitializeComponent();
            //
            Helper.GlobalData.InitForm(this);
            //
            dataGrid1.AddColumn("approve_flag", "审核状态", "", 60, 2, "{0:未审核 ,1:已审核}");
            dataGrid1.AddColumn("sheet_no", "单据号", "", 130, 1, "");
          
            dataGrid1.AddColumn("supcust_from", "转入供应商编号", "", 150, 1, "");
            dataGrid1.AddColumn("supcust_from_name", "转入供应商名称", "", 150, 1, "");//
            dataGrid1.AddColumn("supcust_to", "转出供应商编号", "", 150, 1, "");
            dataGrid1.AddColumn("supcust_to_name", "转出供应商名称", "", 150, 1, ""); //          
            dataGrid1.AddColumn("total_amount", "对冲金额", "", 150, 3, "0.00");
            dataGrid1.AddColumn("approve_man_a", "审核人", "", 110, 1, "");//
            dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd");
            dataGrid1.AddColumn("oper_name_a", "操作员", "", 100, 1, "");//
            dataGrid1.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd");
            dataGrid1.IsSelect = true;
            dataGrid1.MergeCell = false;
            dataGrid1.DataSource = new DataTable();
            //dataGrid1.SetTotalColumn("total_amount");
            //
            //dataGrid2.AddColumn("select_flag", "核销", "", 50, 2, "{0:,1:√}");
            //dataGrid2.AddColumn("path", "方向", "", 50, 2, "{0:-,1:+}");
            //dataGrid2.AddColumn("voucher_no", "业务单号", "", 150, 1, "");
            //dataGrid2.AddColumn("voucher_first", "业务类型", "", 90, 2, IvyTransFunction.all_type_str);
            //dataGrid2.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00");
            //dataGrid2.AddColumn("paid_amount", "已付金额", "", 100, 3, "0.00");
            //dataGrid2.AddColumn("paid_free", "已免付金额", "", 100, 3, "0.00");
            //dataGrid2.AddColumn("yf_amount", "应付金额", "", 100, 3, "0.00");
            //dataGrid2.AddColumn("pay_amount", "本次付款金额", "", 120, 3, "0.00");
            //dataGrid2.AddColumn("pay_free", "免付金额", "", 100, 3, "0.00");
            //dataGrid2.AddColumn("memo", "备注", "", 180, 1, "");
            //dataGrid2.AddColumn("voucher_type", "业务描述", "", 100, 1, "");
            dataGrid2.DataSource = new DataTable();
            //
            this.dateTextBox1.Text = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            this.dateTextBox2.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            IBLL.ISup bll = new BLL.SupBLL();
            int tmp;
            this.myTextBox1.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150",
                "supcust_no/sup_name->Text");
            this.myTextBox2.Bind(paymentbll.GetSupcustList("S"), 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150",
               "supcust_no/sup_name->Text");
        }

        private IOrder order;
        IARAP_SCPaymentBLL paymentbll = new ARAP_SCPaymentBLL();
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
                IBLL.ICusSettle bll = new BLL.CusSettle();
                rowLis.ForEach((r) =>
                {
                    string sheet_no = r["sheet_no"].ToString();
                    try
                    {
                        paymentbll.DeleteArAp(sheet_no, DateTime.Now);
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
                IBLL.ICusSettle bll = new BLL.CusSettle();
                rowLis.ForEach((r) =>
                {
                    string sheet_no = r["sheet_no"].ToString();
                    try
                    {
                        paymentbll.CheckArAp(sheet_no, Program.oper.oper_id, DateTime.Now);
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
                string text = myTextBox1.Text.Split('/')[0];
                string text1 = myTextBox2.Text.Split('/')[0];
                System.Threading.Thread th = new System.Threading.Thread(() =>
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        string is_cs = "S";
                        IBLL.ICusSettle bll = new BLL.CusSettle();

                        if (date1 == DateTime.MinValue)
                        {
                            throw new Exception("期间不正确");
                        }
                        if (date2 == DateTime.MinValue)
                        {
                            throw new Exception("期间不正确");
                        }
                        DataTable tb = paymentbll.GetArApList(date1, date2, text,text1, "PR");


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
                            //if (tb.Rows.Count > 0)
                            //{
                            //    sheet_no = tb.Rows[0]["sheet_no"].ToString();

                            //    System.Data.DataTable tb1;
                            //    System.Data.DataTable tb2;
                            //    bll.GetOrder(sheet_no,is_cs, out tb1, out tb2);


                            //    this.dataGrid2.DataSource = tb2;
                            //    this.dataGrid2.Refresh();
                            //}
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
                            IBLL.ICusSettle bll = new BLL.CusSettle();
                            System.Data.DataTable tb1;
                            System.Data.DataTable tb2;
                            string is_cs = "S";
                            paymentbll.GetArApOrder(sheet_no, out tb2);//GetOrder(,is_cs, out tb1, out tb2);

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
                //IBLL.ICusSettle bll = new BLL.CusSettle();
                rowLis.ForEach((r) =>
                {
                    string sheet_no = r["sheet_no"].ToString();
                    try
                    {
                        paymentbll.NotCheckArAp(sheet_no, Program.oper.oper_id, DateTime.Now);
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
