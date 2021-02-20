using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using IvyBack.Helper;
using System.Threading;
using IvyBack.BLL;
using Model.StaticType;
using IvyBack.PaymentForm;

namespace IvyBack.VoucherForm
{
    public partial class frmSupcustNotice : Form, IOrder
    {

        private IOrderList orderlist;
        private IOrderMerge ordermerge;
        private DateTime update_time;
        private string sheet_no;
        private int _runType1 = 0;//0:供应商 1:客户
        IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
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
                    this.label1.Text = "供应商账期通知单";
                    this.label2.Text = "供 应 商：";

                }
                _runType1 = value;
            }
        }
        public DataTable sup_tb;
        public frmSupcustNotice(int runType2)
        {
            InitializeComponent();
            this.runType1 = runType2;
            Helper.GlobalData.InitForm(this);
            //
            var tb = new DataTable();
            tb.Columns.Add("voucher_no");
            tb.Columns.Add("voucher_no_date", typeof(DateTime));
            tb.Columns.Add("sheet_id");
            tb.Columns.Add("due_date", typeof(DateTime));
            tb.Columns.Add("sheet_amount", typeof(decimal));
            tb.Columns.Add("paid_amount", typeof(decimal));
            tb.Columns.Add("unpaid_amount", typeof(decimal));

            editGrid1.AddColumn("voucher_no", "单号", "", 100, 1, "", true);
            editGrid1.AddColumn("voucher_no_date", "单据日期", "", 150, 1, "yyyy-MM-dd", false);
            editGrid1.AddColumn("sheet_id", "类型", "", 150, 2, IvyTransFunction.all_type_str, false);
            editGrid1.AddColumn("due_date", "应结日期", "", 80, 1, "yyyy-MM-dd", false);
            editGrid1.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00", false);
            editGrid1.AddColumn("paid_amount", "已核销", "", 100, 3, "0.00", false);
            editGrid1.AddColumn("unpaid_amount", "未核销", "", 100, 3, "0.00", false);
            editGrid1.SetTotalColumn("sheet_amount,paid_amount,unpaid_amount");//合计项
            editGrid1.DataSource = tb;
            editGrid1.IsShowIco = true;
            //
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //

                Thread th = new Thread(() =>
                {
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        IBLL.ICommonBLL bll2 = new BLL.CommonBLL();
                        IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
                        var branch_tb = bll2.GetBranchList();
                        //this.txtbranch.Invoke((MethodInvoker)delegate
                        //{
                        //    txtbranch.Bind(branch_tb, 300, 200, "branch_no", "branch_no:编号:80,branch_name:名称:140", "branch_no/branch_name->Text");
                        //});
                        //DataTable dt2 = new DataTable();
                        //dt2.Columns.Add("sale_no");
                        //dt2.Columns.Add("sale_name");
                        //dt2.Rows.Add("A", "购销");
                        //dt2.Rows.Add("B", "代销");
                        //dt2.Rows.Add("C", "联营");
                        //dt2.Rows.Add("E", "联营进货");
                        //dt2.Rows.Add("Z", "租赁");

                        //this.txt_sale_no.Invoke((MethodInvoker)delegate
                        //{
                        //    txt_sale_no.Bind(dt2, 250, 200, "sale_no", "sale_no:编码:80,sale_name:代销方式:100", "sale_no/sale_name->Text");
                        //});
                        
                        if ( runType1== 1)
                        {
                            //sup_tb = paymentbll.GetSupcustList("C");
                            IBLL.ICus bll = new BLL.CusBLL();
                            int tmp;
                            this.txt_sup.Invoke((MethodInvoker)delegate
                            {
                                txt_sup.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 350, 200, "supcust_no", "supcust_no:客户编号:80,sup_name:客户名称:200", "supcust_no/sup_name->Text");
                            });
                        }
                        else
                        {
                            //sup_tb = paymentbll.GetSupcustList("S");
                            IBLL.ISup bll = new BLL.SupBLL();
                            int tmp;
                            this.txt_sup.Invoke((MethodInvoker)delegate
                            {
                                txt_sup.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 350, 200, "supcust_no", "supcust_no:供应商编号:80,sup_name:供应商名称:200", "supcust_no/sup_name->Text");
                            });
                        }
                        

                        
                        //var deal_tb = bll2.GetPeopleList();
                        //this.txt_deal_man.Invoke((MethodInvoker)delegate
                        //{
                        //    txt_deal_man.Bind(deal_tb, 250, 200, "oper_id", "oper_id:编号:80,oper_name:姓名:100", "oper_id/oper_name->Text");
                        //});

                        //IBLL.IInOutBLL bll3 = new BLL.InOutBLL();
                        //var order = bll3.GetImportCGOrder();
                        //this.txt_voucher_no.Invoke((MethodInvoker)delegate
                        //{
                        //    this.txt_voucher_no.Bind(order, 350, 200, "sheet_no", "sheet_no:单据号:110,supcust:供应商:100,branch:仓库:100,oper_date:日期:150", "sheet_no->Text");
                        //});

                        this.Invoke((MethodInvoker)delegate
                        {
                            IOrder ins = this;
                            ins.Add();
                        });
                    }
                    catch (Exception ex)
                    {
                        LogHelper.writeLog("frmSupcustNotice", ex.ToString());
                        MsgForm.ShowFrom(ex);
                    }
                    Helper.GlobalData.windows.CloseLoad(this);
                });
                th.Start();
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
                Helper.LogHelper.writeLog("frmSupcustNotice()", ex.ToString());
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private int runType = 0;
        public int RunType
        {
            get
            {
                return runType;
            }
            set
            {
                runType = value;
            }
        }

        private bool isEdit = false;
        bool IOrder.IsEdit()
        {
            return isEdit;
        }

        void Save1()
        {
            rp_t_account_notice ord = new rp_t_account_notice();
            List<rp_t_account_notice_detail> lines = new List<rp_t_account_notice_detail>();
            ord.sheet_no = txtsheet_no.Text.Trim();
            ord.supcust_no = txt_sup.Text.Trim().Split('/')[0];
            ord.oper_date = Helper.Conv.ToDateTime(txtoper_date.Text.Trim());
            //审核人
            //审核日期
            ord.oper_id = txtoper_man.Text.Split('/')[0];
            ord.remark = txt_other1.Text.Trim();
            ord.approve_flag = "0";
            ord.update_time = DateTime.Now;
            ord.total_amount = Helper.Conv.ToDecimal(txt_account.Text);
            ord.account_due_date = Conv.ToDateTime(txtNoticeEndDate.Text);
            ord.supcust_flag = runType1 == 1 ? "C" : "S";
            ord.recpay_record_info_num2 = 0;
            ord.this_receivable = 0;
            //已收
            //应收

            int flag = 0;
            int index = 0;
            foreach (DataRow row in editGrid1.DataSource.Rows)
            {
                ++index;
                if (row["voucher_no"].ToString() != "")
                {
                    rp_t_account_notice_detail line = DB.ReflectionHelper.DataRowToModel<rp_t_account_notice_detail>(row);
                    //string s = line.batch_num;
                    line.update_time = DateTime.Now;


                    line.voucher_no = Conv.ToString(row["voucher_no"]);
                    line.voucher_no_date = Conv.ToDateTime(row["voucher_no_date"]);
                    line.sheet_id = Conv.ToString(row["sheet_id"]);
                    line.due_date = Conv.ToDateTime(row["due_date"]);
                    line.sheet_amount = Conv.ToDecimal(row["sheet_amount"]);
                    line.paid_amount = Conv.ToDecimal(row["paid_amount"]);
                    line.unpaid_amount = Conv.ToDecimal(row["unpaid_amount"]);
                    lines.Add(line);
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                throw new Exception("无数据！");
            }
            var sheet_no = "";
            if (runType == 1)
                {
                   
                    paymentbll.AddNotice(ord, lines, out sheet_no);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                }
                else if (runType == 2)
                {
                paymentbll.ChangeNotice(ord, lines,out sheet_no);
                IOrder ins = this;
                ins.ShowOrder(ord.sheet_no);
            }
            txtsheet_no.Text = sheet_no;
            Dictionary<string, object> dic = this.Tag as Dictionary<string, object>;
            this.Tag = Helper.Conv.ControlsAdds(this, dic);

            IBLL.ISys sys = new BLL.SysBLL();
            string isApprove = sys.Read("approve_at_ones");
            if ("1".Equals(isApprove))
            {
                if (YesNoForm.ShowFrom("保存成功!是否立即审核") == DialogResult.Yes)
                {
                    tsbCheck_Click(new object(), new EventArgs());
                }
            }
        }
        void IOrder.Save()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                if (txt_sup.Text.Trim().Contains("/") == false)
                {
                    MsgForm.ShowFrom("供应商必填!");
                    return;
                }
                //if (txtbranch.Text.Trim().Contains("/") == false)
                //{
                //    MsgForm.ShowFrom("机构必填!");
                //    return;
                //}
                if (txtoper_date.Text.Trim() == "")
                {
                    MsgForm.ShowFrom("单据日期必填!");
                    return;
                }
                Save1();
                //if (MsgHelper.TraverseAlType(editGrid1.DataSource, 1))
                //{
                //    Save1();
                //}
            }
            catch (Exception ex)
            {
                Helper.LogHelper.writeLog("frmSupcustNotice->Save()", ex.ToString(), txtsheet_no.Text);
                MsgForm.ShowFrom("账期通知单保存异常[" + ex.Message + "]");

            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }

        }

        void IOrder.Add()
        {
            try
            {
                RunType = 1;

                foreach (Control c in this.panel1.Controls)
                {
                    c.Enabled = true;
                }

                //IBLL.IInOutBLL bll3 = new BLL.InOutBLL();
                //var order = bll3.GetImportCGOrder();
                //this.txt_voucher_no.Invoke((MethodInvoker)delegate
                //{
                //    this.txt_voucher_no.Bind(order, 350, 200, "sheet_no", "sheet_no:单据号:110,supcust:供应商:100,branch:仓库:100,oper_date:日期:150", "sheet_no->Text");
                //});
                isAll = false;
                this.txt_sup.Text = "";
                //this.txtbranch.GetDefaultValue();
                this.txtsheet_no.Text = "";
                this.txtoper_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                this.txtNoticeEndDate.Text= System.DateTime.Now.ToString("yyyy-MM-dd");
                this.txtNoticeStartDate.Text = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                //this.txt_pay_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //this.txt_sale_no.Text = "A/购销";
                //this.txtapprove_man.Text = "";
                this.txtoper_man.Text = Program.oper.oper_id + "/" + Program.oper.oper_name;
                this.txt_account.Text = "";
                txtSupInitial.Text = "";
                txtQuota.Text = "";
                txt_other1.Text = "";
                txtapprove_man.Text = "";
                txtapprove_date.Text = "";
                txt_account.Text = "";
                //this.txtapprove_date.Text = "";
                //this.txt_voucher_no.Text = "";

                //
                var tb = editGrid1.DataSource;
                tb.Clear();
                editGrid1.Refresh();

                Dictionary<string, object> dic = this.Tag as Dictionary<string, object>;
                this.Tag = Helper.Conv.ControlsAdds(this, dic);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }

        }

        void IOrder.ShowOrder(string sheet_no)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                RunType = 2;

                //IBLL.IInOutBLL bll = new BLL.InOutBLL();
                IBLL.IARAP_SCPaymentBLL scbll = new BLL.ARAP_SCPaymentBLL();
                DataTable tb1;
                DataTable tb2;
                string is_cs = runType1 == 1 ? "C" : "S";
                scbll.GetOrder(sheet_no,is_cs,out tb1,out tb2);
               // bll.GetInOut(sheet_no, "A", out tb1, out tb2);
                //

                //
                var r1 = tb1.Rows[0];
                update_time = Helper.Conv.ToDateTime(r1["update_time"]);
                this.sheet_no = sheet_no;
                this.txt_sup.Text = Conv.ToString(r1["supcust_no"]) + "/" + Conv.ToString(r1["sup_name"]);
                //this.txtbranch.GetDefaultValue();
                this.txtsheet_no.Text = sheet_no;
                this.txtoper_date.Text = Helper.Conv.ToDateTime(r1["oper_date"]).ToString("yyyy-MM-dd");
                this.txtNoticeEndDate.Text = Conv.ToDateTime(r1["account_due_date"]).ToString("yyyy -MM-dd");
                //this.txt_pay_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //this.txt_sale_no.Text = "A/购销";
                //this.txtapprove_man.Text = "";
                this.txtoper_man.Text = Program.oper.oper_id + "/" + Program.oper.oper_name;
                this.txt_account.Text = Conv.ToString(r1["total_amount"]);
                txtSupInitial.Text = "";
                txtQuota.Text = "";
                txt_other1.Text = Conv.ToString(r1["remark"]);
                txtapprove_man.Text = Conv.ToString(r1["approve_man_name"]);
                txtapprove_date.Text = Helper.Conv.ToDateTime(r1["approve_date"]).ToString("yyyy-MM-dd");
                //if (r1["oper_id"].ToString() == "")
                //{
                //    txtoper_man.Text = "";
                //}
                //else
                //{
                //    txtoper_man.Text = r1["oper_id"] + "/" + r1["oper_name"];
                //}
                //txtapprove_date.Text = Helper.Conv.ToDateTime(r1["approve_date"]).ToString("yyyy-MM-dd HH:mm:ss");
                //txt_pay_date.Text = Helper.Conv.ToDateTime(r1["pay_date"]).ToString("yyyy-MM-dd HH:mm:ss");
                //txt_voucher_no.Text = r1["voucher_no"].ToString();
                //txt_sup.Text = r1["supcust_no"].ToString() + "/" + r1["sup_name"].ToString();
                //var sale_name = "";
                //var sale_no = r1["sale_no"].ToString();
                //if (sale_no == "A") sale_name = "购销";
                //else if (sale_no == "B") sale_name = "代销";
                //else if (sale_no == "C") sale_name = "联营";
                //else if (sale_no == "E") sale_name = "联营进货";
                //else if (sale_no == "Z") sale_name = "租赁";
                //txt_sale_no.Text = sale_no + "/" + sale_name;

                //txt_other1.Text = r1["other1"].ToString();
                //txt_account.Text = r1["other3"].ToString();
                //txt_old_no.Text = r1["old_no"].ToString();
                //
                editGrid1.DataSource = tb2;

                Dictionary<string, object> dic = this.Tag as Dictionary<string, object>;
                this.Tag = Helper.Conv.ControlsAdds(this, dic);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom("加载账户通知单异常[" + ex.Message + "]");
                Helper.LogHelper.writeLog("frmSupcustNotice->ShowOrder()", ex.ToString(), sheet_no);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        void IOrder.SetOrderList(IOrderList orderlist)
        {
            this.orderlist = orderlist;
        }

        void IOrder.SetOrderMerge(IOrderMerge ordermerge)
        {
            this.ordermerge = ordermerge;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "01"))
            {

                return;
            }
            IOrder ins = this;
            ins.Add();
        }

        private void tsbDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "02"))
                {

                    return;
                }
                if (!string.IsNullOrEmpty(sheet_no))
                {
                    if (YesNoForm.ShowFrom("确认删除单据[" + sheet_no + "]?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        IBLL.IARAP_SCPaymentBLL scbll = new ARAP_SCPaymentBLL();

                        IBLL.IInOutBLL bll = new BLL.InOutBLL();
                        scbll.DeleteNotice(sheet_no,update_time);
                /*        bll.DeleteInOut(sheet_no, update_time);*/
                        IOrder ins = this;
                        ins.Add();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom("删除单据异常[" + ex.Message + "]");
                Helper.LogHelper.writeLog("frmSupcustNotice->tsbDel_Click()", ex.ToString(), sheet_no);
            }
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "04"))
            {

                return;
            }
            IOrder ins = this;
            ins.Save();
        }

        private void tsbCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "05"))
                {
                    return;
                }
                if (!string.IsNullOrEmpty(txtsheet_no.Text))
                {
                    //IBLL.IInOutBLL bll = new BLL.InOutBLL();
                    IBLL.IARAP_SCPaymentBLL scpaymentbll = new ARAP_SCPaymentBLL();
                    scpaymentbll.CheckNotice(sheet_no, Program.oper.oper_id, update_time);
                   // bll.CheckNotice(sheet_no, Program.oper.oper_id, update_time);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                    MsgForm.ShowFrom("审核成功");
                }
                
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom("审核单据异常[" + ex.Message + "]");
                Helper.LogHelper.writeLog("frmSupcustNotice->tsbCheck_Click()", ex.ToString(), sheet_no);
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            ordermerge.Close2();
        }

        private void frmCGInSheet_Shown(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txtapprove_man.Text.Trim() != "")
            {
               tsbDel.Enabled = false;
               tsbSave.Enabled = false;
                tsbCheck.Enabled = false;
            }
            else
            {
                tsbDel.Enabled = true;
                tsbSave.Enabled = true;
                tsbCheck.Enabled = true;
            }
        }

        bool isSelect = false;
        private void editGrid1_CellEndEdit(object sender, string column_name, DataRow row)
        {
            //if (isSelect) return;

            //if (column_name == "item_subno")
            //{
            //    if (string.IsNullOrEmpty(row[column_name].ToString())) return;

            //    IBLL.IPriceBLL pricebll = new BLL.PriceBLL();
            //    IBLL.IItem itembll = new BLL.ItemBLL();
            //    var sup_id = txt_sup.Text.Trim().Split('/')[0];
            //    DataTable tb = itembll.GetLikeItem(sup_id, 1, row["item_subno"].ToString(), "0", this.txtbranch.Text.Split('/')[0]);

            //    if (tb.Rows.Count > 1)
            //    {
            //        SelectRow(row, row["item_subno"].ToString());
            //    }
            //    else
            //    {
            //        foreach (DataRow dr in tb.Rows)
            //        {
            //            Conv.CopyDataRow(row, dr);
            //            row["branch_no_d"] = dr["branch_no"].ToString();
            //            row["discount"] = "1";
            //            row["valid_price"] = pricebll.GetSupItemPrice(sup_id, dr["item_no"].ToString(), "0");
            //            row["sub_amount"] = Helper.Conv.ToDecimal(row["in_qty"]) * Helper.Conv.ToDecimal(row["valid_price"]) * Helper.Conv.ToDecimal(row["discount"]);
            //            row["valid_date"] = DateTime.Now.AddDays(dr["valid_day"].ToInt32());
            //            break;
            //        }
            //        if (tb.Rows.Count < 1)
            //        {
            //            row[column_name] = "";
            //        }
            //    }

            //    if (!string.IsNullOrEmpty(this.editGrid1.DataSource.Rows[this.editGrid1.DataSource.Rows.Count - 1]["item_subno"].ToString()))
            //    {
            //        this.editGrid1.DataSource.Rows.Add();
            //    }
            //}
            //else if (column_name == "in_qty" || column_name == "valid_price" || column_name == "discount")
            //{
            //    row["sub_amount"] = Helper.Conv.ToDecimal(row["in_qty"]) * Helper.Conv.ToDecimal(row["valid_price"]) * Helper.Conv.ToDecimal(row["discount"]);
            //    editGrid1.Refresh();
            //}
        }

        private void editGrid1_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            //if (column_name.Equals("item_subno"))
            //{
            //    isSelect = true;
            //    SelectRow(row);
            //    isSelect = false;
            //}
            //else if (column_name.Equals("branch_name"))
            //{
            //    SelectBranch(row);
            //}
        }

        public void SelectRow(DataRow row, string condition = "")
        {

            //frmSelect frm = new frmSelect();
            //frm.dgv.AddColumn("选择", "选择", "", 40, 2, "{0:,1:√}");
            //frm.dgv.AddColumn("item_subno", "货号", "", 110, 1, "");
            //frm.dgv.AddColumn("barcode", "条码", "", 110, 1, "");
            //frm.dgv.AddColumn("item_name", "商品名称", "", 160, 1, "");
            //frm.dgv.AddColumn("item_subname", "拼音码", "", 90, 1, "");
            //frm.dgv.AddColumn("unit_no", "单位", "", 60, 1, "");
            //frm.dgv.AddColumn("item_size", "规格", "", 60, 1, "");
            //frm.dgv.AddColumn("price", "价格", "", 100, 3, "0.00");
            //frm.dgv.AddColumn("product_area", "产地", "", 150, 1, "");

            //frm.Text = "选择商品";
            //frm.condition = condition;

            //IBLL.IPriceBLL pricebll = new BLL.PriceBLL();
            //var sup_id = txt_sup.Text.Trim().Split('/')[0];

            //List<DataRow> lis = frm.SelectDataRow("货号/品名:", "item_subno,item_name");

            //int index = 0;
            //for (int i = 0; i < this.editGrid1.DataSource.Rows.Count; i++)
            //{
            //    if (this.editGrid1.DataSource.Rows[i] == row)
            //        index = i;
            //}
            //if (lis.Count > 1)
            //    for (int i = 0; i < lis.Count - 1; i++)
            //    {
            //        this.editGrid1.DataSource.Rows.InsertAt(this.editGrid1.DataSource.NewRow(), index);
            //    }

            //foreach (DataRow dr in lis)
            //{
            //    var dgv_tb = this.editGrid1.DataSource.Copy();
            //    dgv_tb.ImportRow(dr);
            //    var dgv_dr = dgv_tb.Rows[dgv_tb.Rows.Count - 1];

            //    dgv_dr["discount"] = "1";
            //    dgv_dr["branch_no_d"] = dr["branch_no"];
            //    dgv_dr["valid_price"] = pricebll.GetSupItemPrice(sup_id, dr["item_no"].ToString(), "0");
            //    dgv_dr["sub_amount"] = Helper.Conv.ToDecimal(dgv_dr["in_qty"]) * Helper.Conv.ToDecimal(dgv_dr["valid_price"]) * Helper.Conv.ToDecimal(dgv_dr["discount"]);
            //    dgv_dr["valid_date"] = DateTime.Now.AddDays(dr["valid_day"].ToInt32());

            //    Conv.CopyDataRow(this.editGrid1.DataSource, index, dgv_dr);
            //    index++;
            //}

            //if (!string.IsNullOrEmpty(this.editGrid1.DataSource.Rows[this.editGrid1.DataSource.Rows.Count - 1]["item_subno"].ToString()))
            //{
            //    this.editGrid1.DataSource.Rows.Add();
            //}

            //this.editGrid1.Editing = false;
            //this.editGrid1.Refresh();
        }

        public void SelectBranch(DataRow row)
        {
            //SaleForm.frmSelect frm = new SaleForm.frmSelect("5", "");
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    row["branch_name"] = frm.itemRow["branch_name"];
            //    row["branch_no_d"] = frm.itemRow["branch_no"];

            //    this.editGrid1.Editing = false;
            //    this.editGrid1.Refresh();
            //}
        }

        private void LoadOrder()
        {
            //string order_sheet_no = this.txt_voucher_no.Text.Trim();
            //if (string.IsNullOrEmpty(order_sheet_no)) return;
            //this.editGrid1.DataSource.Clear();

            //IBLL.IInOutBLL bll3 = new BLL.InOutBLL();

            //DataTable tbmain;
            //DataTable tbdetail;
            //bll3.GetCGOrder(order_sheet_no, out tbmain, out tbdetail);

            //DataRow row = tbmain.Rows[0];
            //txt_sup.GetValue("supcust_no", row["sup_no"].ToString());
            //txtbranch.GetValue("branch_no", row["branch_no"].ToString());
            //txt_deal_man.GetValue("oper_id", row["order_man"].ToString());
            //txt_other1.Text = row["memo"].ToString();

            //this.editGrid1.DataSource.ClearTable();
            //foreach (DataRow dr in tbdetail.Rows)
            //{
            //    this.editGrid1.DataSource.ImportRow(dr);

            //    DataRow r = this.editGrid1.DataSource.Rows[this.editGrid1.DataSource.Rows.Count - 1];
            //    r["item_no"] = dr["item_no"];
            //    r["item_subno"] = dr["item_subno"];
            //    r["barcode"] = dr["barcode"];
            //    r["item_name"] = dr["item_name"];
            //    r["unit_no"] = dr["unit_no"];
            //    r["in_qty"] = dr["order_qnty"];
            //    r["valid_price"] = dr["in_price"];
            //    r["sub_amount"] = dr["sub_amount"];
            //    r["other1"] = dr["other1"];
            //    r["discount"] = dr["discount"];
            //    r["branch_no_d"] = dr["branch_no"];
            //    r["valid_date"] = row["valid_date"].ToDateTime();
            //    //r["batch_num"] = row["batch_num"];
            //}

            //txt_sup.Enabled = false;
            //txt_voucher_no.Enabled = false;

            //this.editGrid1.DataSource.Rows.Add();
            //this.editGrid1.Refresh();
        }
        private void txt_voucher_no_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    LoadOrder();
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.writeLog(ex);
            //    MsgForm.ShowFrom(ex);
            //}
        }

        public string method_id = "ap007";
        public void GetPrintTb()
        {
            if (string.IsNullOrEmpty(this.txtsheet_no.Text))
                throw new Exception("请选择单据!");

            IBLL.IARAP_SCPaymentBLL scbll = new BLL.ARAP_SCPaymentBLL();
            DataTable tb1;
            DataTable tb2;
            string is_cs = runType1 == 1 ? "C" : "S";
            if (runType1==1)
            {
                method_id = "ap007";
            }
            else
            {
                method_id = "ap008";
            }
            scbll.GetOrder(sheet_no, is_cs, out tb1, out tb2);
            //DataTable tb1;
            //DataTable tb2;
            //bll.GetInOut(this.txtsheet_no.Text, "A", out tb1, out tb2);

            DataTable print_tb_m = new DataTable();
            DataTable print_tb_d = new DataTable();
            //表头
            {
                Dictionary<string, string> file_dic = new Dictionary<string, string>()
                {
                    {"sheet_no","单据号"},
                    {"total_amount","本单金额"},
                    {"oper_id","操作人编号"},
                    {"oper_name","制单人"},
                    {"oper_date","操作日期"},
                    {"account_due_date","账期截止日期"},
                    {"remark","备注"},
                };
                if (runType1 == 1)
                {
                    file_dic.Add("supcust_no", "客户编号");
                    file_dic.Add("sup_name", "客户名称");
                }
                else
                {
                    file_dic.Add("supcust_no", "供应商编号");
                    file_dic.Add("sup_name", "供应商名称");
                }
                foreach (var k in file_dic.Keys)
                {
                    var c = file_dic[k];
                    if (tb1.Columns.Contains(k))
                    {
                        print_tb_m.Columns.Add(c, tb1.Columns[k].DataType);
                    }
                    else
                    {
                        print_tb_m.Columns.Add(c);
                    }
                }

                foreach (DataRow dr in tb1.Rows)
                {
                    DataRow row = print_tb_m.NewRow();
                    foreach (var key in file_dic.Keys)
                    {
                        row[file_dic[key]] = dr[key];
                    }
                    print_tb_m.Rows.Add(row);
                }
            }
            //明细
            {
                Dictionary<string, string> file_dic = new Dictionary<string, string>()
                {
                    {"sheet_no","业务单号"},
                    {"voucher_no_date","单据日期"},
                    {"due_date","应结日期"},
                    {"sheet_id","业务类型"},
                    {"sheet_amount","单据金额"},
                    {"paid_amount","已付金额"},
                    {"unpaid_amount","未核销金额"},
            };

                foreach (var k in file_dic.Keys)
                {
                    var c = file_dic[k];
                    if (tb2.Columns.Contains(k))
                    {
                        print_tb_d.Columns.Add(c, tb2.Columns[k].DataType);
                    }
                    else
                    {
                        print_tb_d.Columns.Add(c);
                    }
                }

                foreach (DataRow dr in tb2.Rows)
                {
                    DataRow row = print_tb_d.NewRow();
                    foreach (var key in file_dic.Keys)
                    {
                        row[file_dic[key]] = dr[key];
                    }
                    print_tb_d.Rows.Add(row);
                }
            }

            PrintForm.PrintHelper.tb_main = print_tb_m;
            PrintForm.PrintHelper.tb_detail = print_tb_d;
        }
        private void tsmiPrintStyle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "07"))
                {

                    return;
                }
                GetPrintTb();
                PrintForm.frmPrintManager frm = new PrintForm.frmPrintManager();

                frm.PrintStyle(method_id);
            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }
        private void tsmiPrintV_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "07"))
                {

                    return;
                }
                GetPrintTb();
                Model.sys_t_print_style_default styleDefault;
                Model.sys_t_print_style sys_T_Print_;
                if (PrintForm.PrintHelper.myDefault.TryGetValue(method_id, out styleDefault))
                {
                    if (PrintForm.PrintHelper.myStyle.TryGetValue(styleDefault.style_id, out sys_T_Print_))
                    {
                        global::PrintHelper.IBLL.IPrint print = new global::PrintHelper.BLL.PrintV();
                        print.Print(sys_T_Print_.style_data,
                            PrintForm.PrintHelper.GetStyle(sys_T_Print_.style_data),
                            PrintForm.PrintHelper.tb_main,
                            PrintForm.PrintHelper.tb_detail);
                    }
                    else
                    {
                        MsgForm.ShowFrom("重新选择默认打印样式!");

                        PrintForm.frmPrintManager frm = new PrintForm.frmPrintManager();
                        frm.PrintStyle(method_id);
                    }
                }
                else
                {
                    MsgForm.ShowFrom("请选择默认打印样式!");

                    PrintForm.frmPrintManager frm = new PrintForm.frmPrintManager();
                    frm.PrintStyle(method_id);
                }
            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }
            private void tsmiPrint_Click(object sender, EventArgs e)
            {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "07"))
                {
                    return;
                }
                GetPrintTb();
                Model.sys_t_print_style_default styleDefault;
                Model.sys_t_print_style sys_T_Print_;
                if (PrintForm.PrintHelper.myDefault.TryGetValue(method_id, out styleDefault))
                {
                    if (PrintForm.PrintHelper.myStyle.TryGetValue(styleDefault.style_id, out sys_T_Print_))
                    {
                        global::PrintHelper.IBLL.IPrint print = new global::PrintHelper.BLL.PrintV();
                        print.Print(sys_T_Print_.style_data,
                            PrintForm.PrintHelper.GetStyle(sys_T_Print_.style_data),
                            PrintForm.PrintHelper.tb_main,
                            PrintForm.PrintHelper.tb_detail);
                    }
                    else
                    {
                        MsgForm.ShowFrom("重新选择默认打印样式!");

                        PrintForm.frmPrintManager frm = new PrintForm.frmPrintManager();
                        frm.PrintStyle(method_id);
                    }
                }
                else
                {
                    MsgForm.ShowFrom("请选择默认打印样式!");

                    PrintForm.frmPrintManager frm = new PrintForm.frmPrintManager();
                    frm.PrintStyle(method_id);
                }
            }
            catch (Exception exception)
            {
                MsgForm.ShowFrom(exception);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if (txtsheet_no.Text != null)
            //{
            //    string strpath = Environment.CurrentDirectory;
            //    string path = strpath + @"\acc\IvyBack.exe";
            //    string password = "";
            //    if (!string.IsNullOrEmpty(Program.oper.oper_pw))
            //    {
            //        password = Program.oper.oper_pw;
            //    }
            //    else
            //    {
            //        password = "123456";
            //    }
            //    string batch_num = "";

            //    if (txtsheet_no.Text.Trim()!=null&&txtsheet_no.Text.Trim()!="")
            //    {
                   
            //        string[] arr = batch_num.Split('_');
            //        batch_num = "";
            //        if (arr.Length == 3)
            //        {
            //            batch_num = arr[1];
            //        }

            //        if (arr.Length == 4)
            //        {
            //            batch_num = arr[2];
            //        }
            //    }
            
                

            //    string str = Program.oper.oper_id + " " + password + " " + batch_num;
            //    OpenPress(path, str);
            //}
            //else
            //{
            //    YesNoForm.ShowFrom("当前没有订单，无法添加！");
            //}
        }
        public static void OpenPress(string FileName, string Arguments)
        {
            //try
            //{
            //    Process pro = new Process();
            //    if (System.IO.File.Exists(FileName))
            //    {
            //        pro.StartInfo.FileName = FileName;
            //        pro.StartInfo.Arguments = Arguments;
            //        pro.Start();
            //    }
            //    else
            //    {
            //        YesNoForm.ShowFrom("未找到应用程序");
            //    }
            //}
            //catch (Exception e)
            //{
            //    MsgForm.ShowFrom(e.ToString());
            //}

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //string batch_num = this.editGrid1.DataSource.Rows[0]["batch_num"].ToString();
            //string[] arr = batch_num.Split('_');
            //batch_num = "";
            //if (arr.Length == 3)
            //{
            //    batch_num = arr[1];
            //}

            //if (arr.Length == 4)
            //{
            //    batch_num = arr[2];
            //}
            //string strpath = Environment.CurrentDirectory;
            //string path = strpath + @"\printer\IvyPrinter.exe";
            //OpenPress(path, $"1 {batch_num}");
        }

        private void txt_sup_TextChanged(object sender, EventArgs e)
        {
            //IBLL.ISCPaymentBLL paymentbll = new BLL.SCPaymentBLL();
            //string supcust_no = txt_sup.Text.Trim().Split('/')[0];
            //var supcust= sup_tb.AsEnumerable().Where(item1 => item1["supcust_no"].ToString() == supcust_no).ToArray();
            //txtSupInitial.Text = Helper.Conv.ToString(supcust[0]["account_period"]);
            //txtQuota.Text = Helper.Conv.ToString(supcust[0]["credit_amt"]);
            //DataTable dt= paymentbll.GetSupcustBeginbalanceModel(supcust_no);
            // //if(dt.Rows.Count>0)
            // //txt_account.Text = dt.Rows[0]["begin_balance"].ToString();
            // //else
            // //    txt_account.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(this.txtsheet_no.Text)) return;

                //if (this.editGrid1.DataSource.Rows.Count == 1)
                //{
                //    this.editGrid1.DataSource.Rows.Clear();
                //}
                //else 
                if (this.editGrid1.DataSource.Rows.Count > 0)
                {
                    if (YesNoForm.ShowFrom("确认要清空覆盖吗?") == DialogResult.Yes)
                    {
                        this.editGrid1.DataSource.Rows.Clear();
                    }
                    else
                    {
                        return;
                    }
                }
                isAll = true;
                string cus = this.txt_sup.Text.Trim().Split('/')[0];
                System.Threading.Thread th = new System.Threading.Thread(() =>
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        //IBLL.ICusSettle bll = new BLL.CusSettle();
                        DataTable tb;
                        if (runType1 == 1)
                        {
                            tb = paymentbll.GetAccountFlows(new Model.rp_t_accout_payrec_flow()
                            {
                                supcust_no = cus,
                                supcust_flag = "C",
                            });
                        }
                        else
                        {
                            tb = paymentbll.GetAccountFlows(new Model.rp_t_accout_payrec_flow()
                            {
                                supcust_no = cus,
                                supcust_flag = "S",
                            });
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow dr in tb.Rows)
                            {
                                if (Conv.ToDateTime(Conv.ToDateTime(dr["oper_date"]).ToString("yyyy-MM-dd")).AddDays(Conv.ToInt(txtSupInitial.Text)) > Conv.ToDateTime(txtNoticeEndDate.Text)&& Conv.ToDateTime(Conv.ToDateTime(dr["oper_date"]).ToString("yyyy-MM-dd")).AddDays(Conv.ToInt(txtSupInitial.Text)) < Conv.ToDateTime(txtNoticeStartDate.Text))
                                    continue;
                                var r = this.editGrid1.DataSource.NewRow();
                                //r["select_flag"] = "1";
                                //r["path"] = dr["pay_type"];
                                r["voucher_no"] = dr["voucher_no"];//单号
                                r["voucher_no_date"] = dr["oper_date"];//单据日期
                                r["sheet_id"] = dr["trans_no"];//单据类型
                                if (dr["pay_type"].ToString() == "-1" || dr["pay_type"].ToString() == "-")
                                {
                                    r["sheet_amount"] = 0 - Conv.ToDecimal(dr["sheet_amount"]);//单据金额
                                }
                                else
                                    r["sheet_amount"] = dr["sheet_amount"];
                                r["due_date"] = Conv.ToDateTime(dr["oper_date"]).AddDays(Conv.ToInt(txtSupInitial.Text)).ToString("yyyy-MM-dd");//应结日期
                                r["paid_amount"] = Conv.ToDecimal(dr["已核销金额"]);
                                //r["paid_free"] = Conv.ToDecimal(dr["已免付金额"]);
                                //r["yf_amount"] = Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["已免付金额"]) - Conv.ToDecimal(dr["已核销金额"]);
                                r["unpaid_amount"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);//未核销
                                //r["pay_free"] = 0.00;
                                //r["memo"] = dr["memo"];
                                //r["pay_amount"] = 0.00;//Conv.ToDecimal(r["yf_amount"]);

                                this.editGrid1.DataSource.Rows.Add(r);
                            }
                            //editGrid1.AddColumn("voucher_no", "单号", "", 100, 1, "", true);
                            //editGrid1.AddColumn("voucher_no_date", "单据日期", "", 150, 1, "", false);
                            //editGrid1.AddColumn("sheet_id", "类型", "", 60, 2, "", false);
                            //editGrid1.AddColumn("due_date", "应结日期", "", 80, 1, "", false);
                            //editGrid1.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00", true);
                            //editGrid1.AddColumn("paid_amount", "已核销", "", 100, 3, "0.00", true);
                            //editGrid1.AddColumn("unpaid_amount", "未核销", "", 100, 3, "0.00", true);
                            //editGrid1.SetTotalColumn("sheet_amount,paid_amount_amount,unpaid_amount_amount");//合计项

                            //editGrid1.AddColumn("select_flag", "核销", "", 50, 2, "", false);
                            //editGrid1.AddColumn("path", "方向", "", 50, 2, "{-1:-,1:+}", false);
                            //editGrid1.AddColumn("voucher_no", "单号", "", 150, 1, "", false);
                            //editGrid1.AddColumn("oper_date", "单据日期", "", 100, 3, "yyyy-MM-dd", false);
                            //editGrid1.AddColumn("voucher_first", "业务类型", "", 90, 2, IvyTransFunction.all_type_str, false);
                            //editGrid1.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("paid_amount", "已核销金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("paid_free", "已免付金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("yf_amount", "待核销金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("pay_amount", "本次核销金额", "", 120, 3, "0.00", true);
                            //editGrid1.AddColumn("num1", "未核销金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("pay_free", "免付金额", "", 100, 3, "0.00", true);
                            //editGrid1.AddColumn("pay_date", "限付日期", "", 100, 3, "yyyy-MM-dd", false);
                            //editGrid1.AddColumn("memo", "备注", "", 180, 1, "", true);
                            //editGrid1.AddColumn("voucher_type", "业务描述", "", 100, 1, "", false);
                            //editGrid1.SetTotalColumn("pay_amount,pay_free");
                            var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                            {
                                int path = 1;

                                //if (r.Field<object>("path").ToString().Equals("-") || r.Field<object>("path").ToString().Equals("-1"))
                                //{
                                //    path = -1;
                                //}
                                return Conv.ToDecimal(r.Field<object>("unpaid_amount"));
                            });
                            this.txt_account.Text = pay_amount.ToString("0.00");


                            this.editGrid1.DataSource = this.editGrid1.DataSource;

                            this.editGrid1.Refresh();

                        });
                    }
                    catch (Exception ex)
                    {
                        IvyBack.Helper.LogHelper.writeLog("myButton1_Click", ex.ToString());
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
        }

        private void txt_sup_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
            string supcust_no = txt_sup.Text.Trim().Split('/')[0];
            if (runType1==1)
            {
                sup_tb = paymentbll.GetSupcustList("C");
            }
            else if (runType1==0)
            {
                sup_tb = paymentbll.GetSupcustList("S");
            }

            var supcust = sup_tb.AsEnumerable().Where(item1 => item1["supcust_no"].ToString() == supcust_no).ToArray();
            txtSupInitial.Text = Helper.Conv.ToString(supcust[0]["account_period"]);
            txtQuota.Text = Helper.Conv.ToString(supcust[0]["credit_amt"]);
            isAll = false;
            var tb = editGrid1.DataSource;
            tb.Clear();
            editGrid1.Refresh();
            //string is_cs = runType1 == 1 ? "C" : "S";
            //DataTable dt = paymentbll.GetRecpayRecordModel(supcust_no, is_cs);
            //if (dt.Rows.Count > 0)
            //{
            //    txtReceived.Text = Conv.ToString(dt.Rows[0]["num2"]);
            //}
            //else
            //{
            //    txtReceived.Text = "0.00";
            //}





        }

        private void txt_account_TextChanged(object sender, EventArgs e)
        {
            //txtReceivable.Text = Conv.ToString(Conv.ToDecimal(txt_account.Text) - Conv.ToDecimal(txtReceived.Text));
        }

        private void txtReceived_TextChanged(object sender, EventArgs e)
        {
            //txtReceivable.Text = Conv.ToString(Conv.ToDecimal(txt_account.Text) - Conv.ToDecimal(txtReceived.Text));
        }
        public bool isAll = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (isAll)
            {
                MsgForm.ShowFrom("请已经生成全部数据，无需再手动添加。");
                return;
            }
            if (string.IsNullOrEmpty(this.txt_sup.Text)) return;
            //if (!string.IsNullOrEmpty(this.txtsheet_no.Text)) return;
            //editGrid1.AddColumn("voucher_no", "单号", "", 100, 1, "", true);
            //editGrid1.AddColumn("voucher_no_date", "单据日期", "", 150, 1, "yyyy-MM-dd", false);
            //editGrid1.AddColumn("sheet_id", "类型", "", 150, 2, IvyTransFunction.all_type_str, false);
            //editGrid1.AddColumn("due_date", "应结日期", "", 80, 1, "yyyy-MM-dd", false);
            //editGrid1.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00", false);
            //editGrid1.AddColumn("paid_amount", "已核销", "", 100, 3, "0.00", false);
            //editGrid1.AddColumn("unpaid_amount", "未核销", "", 100, 3, "0.00", false);
            //editGrid1.SetTotalColumn("sheet_amount,paid_amount,unpaid_amount");//合计项

            //this.dgvSup.AddColumn("voucher_no", "单号", "", 200, 1, "");
            //this.dgvSup.AddColumn("oper_date", "单据日期", "", 240, 1, "");
            //this.dgvSup.AddColumn("voucher_first", "业务类型", "", 100, 1, IvyTransFunction.all_type_str);
            //this.dgvSup.AddColumn("sheet_amount", "单据金额", "", 100, 1, "");
            //this.dgvSup.AddColumn("paid_amount", "已核销金额", "", 150, 2, "");
            //this.dgvSup.AddColumn("yf_amount", "待核销金额", "", 150, 1, "");
            //this.dgvSup.AddColumn("pay_amount", "本次核销金额", "", 80, 3, "0.0");
            //this.dgvSup.AddColumn("num1", "未核销金额", "", 150, 1, "");
            //this.dgvSup.AddColumn("memo", "memo", "", 80, 3, "0.0");
            List<DataRow> list = new List<DataRow>();
            string cus = this.txt_sup.Text.Trim().Split('/')[0];
            var frm = new frmCollectionSelect(cus);
            frm.runType = runType1;
            frm.is_state = 1;
            if (frm.ShowPayment(out list) == DialogResult.Yes)
            {
                foreach (DataRow row in list)
                {
                    DataRow row1 = editGrid1.DataSource.NewRow();
                    row1["voucher_no"] = row["voucher_no"];
                    row1["voucher_no_date"] = row["oper_date"];
                    row1["sheet_id"] = row["voucher_first"];
                    row1["due_date"] = Conv.ToDateTime(row["oper_date"]).AddDays(Conv.ToInt(txtSupInitial.Text)); ;
                    row1["sheet_amount"] = row["sheet_amount"];
                    row1["paid_amount"] = row["paid_amount"];
                    row1["unpaid_amount"] = row["yf_amount"];
                    editGrid1.DataSource.Rows.Add(row1);
                }
                //this.editGrid1.DataSource = this.editGrid1.DataSource;
                
               
                editGrid1.Refresh();
                
                var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                {
                    int path = 1;

                    //if (r.Field<object>("path").ToString().Equals("-") || r.Field<object>("path").ToString().Equals("-1"))
                    //{
                    //    path = -1;
                    //}
                    return Conv.ToDecimal(r.Field<object>("unpaid_amount"));
                });
                this.txt_account.Text = pay_amount.ToString("0.00");
                //var dt = this.editGrid1.DataSource;
                //decimal sum1 = 0;
                //string type = runType1 == 1 ? "CP" : "RP";
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (dt.Rows[i]["select_flag"].ToString() == "1" && Conv.ToDecimal(dt.Rows[i]["yf_amount"]) < 0 && dt.Rows[i]["voucher_first"].ToString() == type)
                //    {
                //        sum1 -= Conv.ToDecimal(dt.Rows[i]["pay_amount"]);

                //    }
                //}
                //txtSurplus.Text = Conv.ToString(sum1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(this.txtsheet_no.Text)) return;

                //if (this.editGrid1.DataSource.Rows.Count == 1)
                //{
                //    this.editGrid1.DataSource.Rows.Clear();
                //}
                //else 
                if (this.editGrid1.DataSource.Rows.Count > 0)
                {
                    if (YesNoForm.ShowFrom("确认要清空覆盖吗?") == DialogResult.Yes)
                    {
                        this.editGrid1.DataSource.Rows.Clear();
                    }
                    else
                    {
                        return;
                    }
                }
                isAll = true;
                string cus = this.txt_sup.Text.Trim().Split('/')[0];
                System.Threading.Thread th = new System.Threading.Thread(() =>
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        //IBLL.ICusSettle bll = new BLL.CusSettle();
                        DataTable tb;
                        if (runType1 == 1)
                        {
                            tb = paymentbll.GetAccountFlows(new Model.rp_t_accout_payrec_flow()
                            {
                                supcust_no = cus,
                                supcust_flag = "C",
                            });
                        }
                        else
                        {
                            tb = paymentbll.GetAccountFlows(new Model.rp_t_accout_payrec_flow()
                            {
                                supcust_no = cus,
                                supcust_flag = "S",
                            });
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow dr in tb.Rows)
                            {
                                if (Conv.ToDateTime(Conv.ToDateTime(dr["oper_date"]).ToString("yyyy-MM-dd"))> Conv.ToDateTime(txtNoticeEndDate.Text) && Conv.ToDateTime(Conv.ToDateTime(dr["oper_date"]).ToString("yyyy-MM-dd"))< Conv.ToDateTime(txtNoticeStartDate.Text))
                                    continue;
                                var r = this.editGrid1.DataSource.NewRow();
                                //r["select_flag"] = "1";
                                //r["path"] = dr["pay_type"];
                                r["voucher_no"] = dr["voucher_no"];//单号
                                r["voucher_no_date"] = dr["oper_date"];//单据日期
                                r["sheet_id"] = dr["trans_no"];//单据类型
                                if (dr["pay_type"].ToString() == "-1" || dr["pay_type"].ToString() == "-")
                                {
                                    r["sheet_amount"] = 0 - Conv.ToDecimal(dr["sheet_amount"]);//单据金额
                                }
                                else
                                    r["sheet_amount"] = dr["sheet_amount"];
                                r["due_date"] = Conv.ToDateTime(dr["oper_date"]).AddDays(Conv.ToInt(txtSupInitial.Text)).ToString("yyyy-MM-dd");//应结日期
                                r["paid_amount"] = Conv.ToDecimal(dr["已核销金额"]);
                                //r["paid_free"] = Conv.ToDecimal(dr["已免付金额"]);
                                //r["yf_amount"] = Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["已免付金额"]) - Conv.ToDecimal(dr["已核销金额"]);
                                r["unpaid_amount"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);//未核销
                                //r["pay_free"] = 0.00;
                                //r["memo"] = dr["memo"];
                                //r["pay_amount"] = 0.00;//Conv.ToDecimal(r["yf_amount"]);

                                this.editGrid1.DataSource.Rows.Add(r);
                            }
                            //editGrid1.AddColumn("voucher_no", "单号", "", 100, 1, "", true);
                            //editGrid1.AddColumn("voucher_no_date", "单据日期", "", 150, 1, "", false);
                            //editGrid1.AddColumn("sheet_id", "类型", "", 60, 2, "", false);
                            //editGrid1.AddColumn("due_date", "应结日期", "", 80, 1, "", false);
                            //editGrid1.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00", true);
                            //editGrid1.AddColumn("paid_amount", "已核销", "", 100, 3, "0.00", true);
                            //editGrid1.AddColumn("unpaid_amount", "未核销", "", 100, 3, "0.00", true);
                            //editGrid1.SetTotalColumn("sheet_amount,paid_amount_amount,unpaid_amount_amount");//合计项

                            //editGrid1.AddColumn("select_flag", "核销", "", 50, 2, "", false);
                            //editGrid1.AddColumn("path", "方向", "", 50, 2, "{-1:-,1:+}", false);
                            //editGrid1.AddColumn("voucher_no", "单号", "", 150, 1, "", false);
                            //editGrid1.AddColumn("oper_date", "单据日期", "", 100, 3, "yyyy-MM-dd", false);
                            //editGrid1.AddColumn("voucher_first", "业务类型", "", 90, 2, IvyTransFunction.all_type_str, false);
                            //editGrid1.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("paid_amount", "已核销金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("paid_free", "已免付金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("yf_amount", "待核销金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("pay_amount", "本次核销金额", "", 120, 3, "0.00", true);
                            //editGrid1.AddColumn("num1", "未核销金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("pay_free", "免付金额", "", 100, 3, "0.00", true);
                            //editGrid1.AddColumn("pay_date", "限付日期", "", 100, 3, "yyyy-MM-dd", false);
                            //editGrid1.AddColumn("memo", "备注", "", 180, 1, "", true);
                            //editGrid1.AddColumn("voucher_type", "业务描述", "", 100, 1, "", false);
                            //editGrid1.SetTotalColumn("pay_amount,pay_free");
                            var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                            {
                                int path = 1;

                                //if (r.Field<object>("path").ToString().Equals("-") || r.Field<object>("path").ToString().Equals("-1"))
                                //{
                                //    path = -1;
                                //}
                                return Conv.ToDecimal(r.Field<object>("unpaid_amount"));
                            });
                            this.txt_account.Text = pay_amount.ToString("0.00");


                            this.editGrid1.DataSource = this.editGrid1.DataSource;

                            this.editGrid1.Refresh();

                        });
                    }
                    catch (Exception ex)
                    {
                        IvyBack.Helper.LogHelper.writeLog("myButton1_Click", ex.ToString());
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
        }
    }
}
