using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using System.Threading;
using Model;

namespace IvyBack.VoucherForm
{
    public partial class frmCashOrder : Form, IOrder
    {
        private IOrderList orderlist;
        private IOrderMerge ordermerge;
        public frmCashOrder()
        {
            InitializeComponent();

            Helper.GlobalData.InitForm(this);

            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                Thread th = new Thread(() =>
                {
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        IBLL.IPayment paymentBLL = new BLL.PaymentBLL();
                        //var pay_way_tb = paymentBLL.GetAllList();
                        //this.txtpay_way.Invoke((MethodInvoker)delegate
                        //{
                        //    this.txtpay_way.Bind(pay_way_tb, 300, 200, "pay_way", "pay_way:类型编码:100,pay_name:付款类型:150",
                        //    "pay_way/pay_name->Text");
                        //});
                        IBLL.IBank bankBLL = new BLL.BankBLL();
                        var bank_tb = bankBLL.GetAllList();
                        this.txtvisa.Invoke((MethodInvoker)delegate
                        {
                            this.txtvisa.Bind(bank_tb, 300, 200, "visa_id", "visa_id:编号:100,visa_nm:名称:130",
                            "visa_id/visa_nm->Text");
                        });
                        var bank_tb2 = bankBLL.GetAllList();
                        this.txtvisa2.Invoke((MethodInvoker)delegate
                        {
                            txtvisa2.Bind(bank_tb2, 300, 200, "visa_id", "visa_id:编号:100,visa_nm:名称:130",
                            "visa_id/visa_nm->Text");
                        });
                        IBLL.IBranch branchBLL = new BLL.BranchBLL();
                        //var branch_tb = branchBLL.GetAllList(4);
                        //this.txtbranch.Invoke((MethodInvoker)delegate
                        //{
                        //    txtbranch.Bind(branch_tb, 300, 200, "branch_no", "branch_no:机构号:80,branch_name:机构名:140", "branch_no/branch_name->Text");
                        //});
                        IBLL.IPeople peopleBLL = new BLL.PeopleBLL();
                        int tmp;
                        //var people_tb = peopleBLL.GetDataTable("", "", 1, 1, 20000, out tmp);
                        //this.txtpeople.Invoke((MethodInvoker)delegate
                        //{
                        //    txtpeople.Bind(people_tb, 250, 200, "oper_id", "oper_id:职员编号:80,oper_name:姓名:80", "oper_id/oper_name->Text");
                        //});

                        this.Invoke((MethodInvoker)delegate
                        {
                            IOrder ins = this;
                            ins.Add();
                        });
                    }
                    catch (Exception ex)
                    {
                        LogHelper.writeLog("frmCashOrder", ex.ToString());
                        MsgForm.ShowFrom(ex);
                    }
                    Helper.GlobalData.windows.CloseLoad(this);
                    Cursor.Current = Cursors.Default;
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

        void IOrder.Save()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                //if (txtpay_way.Text.Trim().Contains("/") == false)
                //{
                //    throw new Exception("付款方式必填!");
                //}
                //if (txtbranch.Text.Trim().Contains("/") == false)
                //{
                //    throw new Exception("机构必填!");
                //}
                if (txtoper_date.Text.Trim() == "")
                {
                    throw new Exception("单据日期必填!");
                }
                if (this.txtvisa.Text.Trim().Contains("/") == false)
                {
                    throw new Exception("转出账户必填!");
                }
                if (this.txtvisa2.Text.Trim().Contains("/") == false)
                {
                    throw new Exception("转入账户必填!");
                }
                if (Helper.Conv.ToDecimal(txtcash.Text.Trim()) <= 0)
                {
                    throw new Exception("转账金额不正确!");
                }
                Model.bank_t_cash_master ord = new Model.bank_t_cash_master();
                ord.sheet_no = txtsheet_no.Text.Trim();
                ord.branch_no = "";//txtbranch.Text.Trim().Split('/')[0];
                ord.voucher_no = "";// txtold_no.Text.Trim();
                ord.visa_id = txtvisa.Text.Trim().Split('/')[0];
                ord.visa_in = txtvisa2.Text.Trim().Split('/')[0];
                ord.pay_way = "";//txtpay_way.Text.Split('/')[0];
                ord.coin_no = "RMB";
                ord.coin_rate = 1;
                ord.deal_man = "";// txtpeople.Text.Trim().Split('/')[0];
                ord.oper_id = txtoper_man.Text.Trim().Split('/')[0];
                ord.oper_date = Helper.Conv.ToDateTime(txtoper_date.Text);
                ord.bill_total = Helper.Conv.ToDecimal(txtcash.Text.Trim());
                ord.bill_flag = "B";
                ord.cm_branch = "00";
                ord.approve_flag = "0";
                ord.approve_man = "";
                ord.approve_date = System.DateTime.MinValue;
                ord.other1 = txtmemo.Text.Trim();
                ord.other2 = "";
                ord.other3 = "";
                ord.num1 = 0;
                ord.num2 = 0;
                ord.num3 = 0;

                if (runType == 1)
                {

                    IBLL.ICashOrder bll = new BLL.CashOrder();
                    string sheet_no = "";
                    bll.Add(ord, out sheet_no);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                }
                else if (runType == 2)
                {
                    IBLL.ICashOrder bll = new BLL.CashOrder();
                    bll.Change(ord);

                    IOrder ins = this;
                    ins.ShowOrder(ord.sheet_no);
                }

                Dictionary<string, object> dic = this.Tag as Dictionary<string, object>;
                this.Tag = Conv.ControlsAdds(this, dic);

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
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
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

                //txtpay_way.Text = "";
                //this.txtbranch.GetDefaultValue();
                this.txtsheet_no.Text = "";
                this.txtoper_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                //this.txtpeople.Text = "";
                this.txtapprove_man.Text = "";
                this.txtoper_man.Text = Program.oper.oper_id + "/" + Program.oper.oper_name;
                this.txtmemo.Text = "";
                this.txtapprove_date.Text = "";
                //txtold_no.Text = "";
                txtvisa.Text = "";
                txtvisa2.Text = "";
                txtcash.Text = "";
                //

                Dictionary<string, object> dic = this.Tag as Dictionary<string, object>;
                this.Tag = Conv.ControlsAdds(this, dic);
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

                IBLL.ICashOrder bll = new BLL.CashOrder();
                DataTable tb1;

                bll.GetOrder(sheet_no, out tb1);
                //

                //
                var r1 = tb1.Rows[0];
                //if (r1["pay_way"].ToString() == "")
                //{
                //    txtpay_way.Text = "";
                //}
                //else
                //{
                //    txtpay_way.Text = r1["pay_way"].ToString() + "/" + r1["pay_name"].ToString();
                //}
                //if (r1["branch_no"].ToString() == "")
                //{
                //    txtbranch.Text = "";
                //}
                //else
                //{
                //    txtbranch.Text = r1["branch_no"].ToString() + "/" + r1["branch_name"].ToString();
                //}
                txtsheet_no.Text = r1["sheet_no"].ToString();
                txtoper_date.Text = Helper.Conv.ToDateTime(r1["oper_date"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                //if (r1["deal_man"].ToString() == "")
                //{
                //    txtpeople.Text = "";
                //}
                //else
                //{
                //    txtpeople.Text = r1["deal_man"].ToString() + "/" + r1["deal_man_name"].ToString();
                //}
                if (r1["approve_man"].ToString() == "")
                {
                    txtapprove_man.Text = "";
                }
                else
                {
                    txtapprove_man.Text = r1["approve_man"].ToString() + "/" + r1["approve_man_name"].ToString();
                }
                if (r1["oper_id"].ToString() == "")
                {
                    txtoper_man.Text = "";
                }
                else
                {
                    txtoper_man.Text = r1["oper_id"] + "/" + r1["oper_name"];
                }
                txtmemo.Text = r1["other1"].ToString();
                DateTime dt;
                if (DateTime.TryParse(r1["approve_date"].ToString(), out dt) == true)
                {
                    txtapprove_date.Text = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    txtapprove_date.Text = "";
                }
                //txtold_no.Text = r1["voucher_no"].ToString();
                if (r1["visa_id"].ToString() == "")
                {
                    txtvisa.Text = "";
                }
                else
                {
                    txtvisa.Text = r1["visa_id"].ToString() + "/" + r1["visa_name"].ToString();
                }
                if (r1["visa_in"].ToString() == "")
                {
                    txtvisa2.Text = "";
                }
                else
                {
                    txtvisa2.Text = r1["visa_in"].ToString() + "/" + r1["bank_in_name"].ToString();
                }
                txtcash.Text = Helper.Conv.ToDecimal(r1["bill_total"].ToString()).ToString("0.00");
                //
                Dictionary<string, object> dic = this.Tag as Dictionary<string, object>;
                this.Tag = Conv.ControlsAdds(this, dic);
            }
            catch (Exception ex)
            {
                //MsgForm.ShowFrom(ex);
                throw;
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
                if (txtsheet_no.Text.Trim() != "")
                {
                    string sheet_no = txtsheet_no.Text.Trim();

                    if (YesNoForm.ShowFrom("确认删除单据" + sheet_no + "?") == DialogResult.Yes)
                    {
                        IBLL.ICashOrder bll = new BLL.CashOrder();
                        bll.Delete(sheet_no);
                        IOrder ins = this;
                        ins.Add();
                    }

                }

            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
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
                if (txtsheet_no.Text.Trim() != "")
                {
                    string sheet_no = txtsheet_no.Text.Trim();
                    IBLL.ICashOrder bll = new BLL.CashOrder();
                    bll.Check(sheet_no, Program.oper.oper_id);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                    MsgForm.ShowFrom("审核成功");
                }
                
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {


            ordermerge.Close2();

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

        public string method_id = "ap013";
        public void GetPrintTb()
        {
            //if (string.IsNullOrEmpty(this.txtsheet_no.Text))
            //    throw new Exception("请选择单据!");

            IBLL.ICashOrder bll = new BLL.CashOrder();
            DataTable tb1;

            bll.GetOrder(this.txtsheet_no.Text, out tb1);

            Dictionary<string, string> file_dic = new Dictionary<string, string>()
            {
                {"sheet_no","单据号"},



                {"visa_id","转出帐户编码"},
                {"visa_name","转出帐户名称"},
                {"visa_in","转入帐户编码"},
                {"bank_in_name","转入帐户名称"},
                {"oper_name","操作员"},
                {"bill_total","金额"},
                {"approve_man_name","审核人"},
                {"approve_date","审核日期"},
                {"other1","摘要"},
            };

            DataTable print_tb = new DataTable();

            foreach (var k in file_dic.Keys)
            {
                var c = file_dic[k];
                if (tb1.Columns.Contains(k))
                {
                    print_tb.Columns.Add(c, tb1.Columns[k].DataType);
                }
                else
                {
                    print_tb.Columns.Add(c);
                }
            }

            foreach (DataRow dr in tb1.Rows)
            {
                DataRow row = print_tb.NewRow();
                foreach (var key in file_dic.Keys)
                {
                    row[file_dic[key]] = dr[key];
                }
                print_tb.Rows.Add(row);
            }

            PrintForm.PrintHelper.tb_main = print_tb;
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

        private void txtvisa_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            string visa = this.txtvisa.Text.Split('/')[0];
            string visa2 = this.txtvisa2.Text.Split('/')[0];
            if (visa == visa2)
            {
                MsgForm.ShowFrom("账户不能一样");
                this.txtvisa.Text = "";
            }
        }

        private void txtvisa2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtvisa2_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            string visa = this.txtvisa.Text.Split('/')[0];
            string visa2 = this.txtvisa2.Text.Split('/')[0];
            if (visa == visa2)
            {
                MsgForm.ShowFrom("账户不能一样");
                this.txtvisa2.Text = "";
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MyLove.PermissionsBalidation(this.Text, "05"))
                {

                    return;
                }
                if (txtsheet_no.Text.Trim() != "")
                {
                    string sheet_no = txtsheet_no.Text.Trim();
                    IBLL.ICashOrder bll = new BLL.CashOrder();
                    bll.NotCheck(sheet_no, Program.oper.oper_id);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                    MsgForm.ShowFrom("反审成功");
                }
                
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }
        }
    }
}
