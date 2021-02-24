using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using Model.StaticType;
using IvyBack;
using IvyBack.FinanceForm;
using IvyBack.PaymentForm;
using Model.PaymentModel;
using IvyBack.BLL;
using IvyBack.BaseForm;
using Model;

namespace IvyBack.VoucherForm
{
    public partial class frmCusCollection : Form, IOrder
    {
        private IOrderList orderlist;
        private IOrderMerge ordermerge;
       
        private DataTable pay_dt;
        IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
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
                    this.label11.Text = "还款金额：";
                    this.label13.Text = "结算金额：";
                }
                else
                {
                    this.label1.Text = "供应商结算单";
                    this.label2.Text = "供应商：";

                    this.label11.Text = "付款金额：";
                    this.label13.Text = "结算金额：";

                }
                _runType1 = value;
            }
        }
        public frmCusCollection(int run)
        {
           
            InitializeComponent();
            runType1 = run;
            //
            Helper.GlobalData.InitForm(this);
            //
            var tb = new DataTable();
            tb.Columns.Add("select_flag");
            tb.Columns.Add("path");
            tb.Columns.Add("voucher_no");
            tb.Columns.Add("oper_date");
            tb.Columns.Add("voucher_first");
            tb.Columns.Add("sheet_amount", typeof(decimal));
            tb.Columns.Add("paid_amount", typeof(decimal));
            tb.Columns.Add("paid_free", typeof(decimal));
            tb.Columns.Add("yf_amount", typeof(decimal));
            tb.Columns.Add("num1", typeof(decimal));
            tb.Columns.Add("pay_amount", typeof(decimal));
            tb.Columns.Add("pay_free", typeof(decimal));
            tb.Columns.Add("pay_date", typeof(DateTime));
            tb.Columns.Add("memo");
            tb.Columns.Add("voucher_type");

            editGrid1.AddColumn("select_flag", "核销", "", 50, 2, "", false);
            //editGrid1.AddColumn("path", "方向", "", 50, 2, "{-1:-,1:+}", false);
            editGrid1.AddColumn("voucher_no", "单号", "", 150, 1, "", false);
            editGrid1.AddColumn("oper_date", "单据日期", "", 100, 3, "yyyy-MM-dd", false);
            editGrid1.AddColumn("voucher_first", "业务类型", "", 120, 2, IvyTransFunction.all_type_str, false);
            editGrid1.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00", false);
            editGrid1.AddColumn("paid_amount", "已核销金额", "", 100, 3, "0.00", false);
            //editGrid1.AddColumn("paid_free", "已免付金额", "", 100, 3, "0.00", false);
            editGrid1.AddColumn("yf_amount", "待核销金额", "", 100, 3, "0.00", false);
            editGrid1.AddColumn("pay_amount", "本次核销金额", "", 120, 3, "0.00", true);
            editGrid1.AddColumn("num1", "未核销金额", "", 100, 3, "0.00", false);
            //editGrid1.AddColumn("pay_free", "免付金额", "", 100, 3, "0.00", true);
            //editGrid1.AddColumn("pay_date", "限付日期", "", 100, 3, "yyyy-MM-dd", false);
            editGrid1.AddColumn("memo", "备注", "", 180, 1, "", true);
            //editGrid1.AddColumn("voucher_type", "业务描述", "", 100, 1, "", false);
            editGrid1.SetTotalColumn("sheet_amount,paid_amount,yf_amount,pay_amount");
            editGrid1.DataSource = tb;

            editGrid1.BindCheck("select_flag");
            
            //
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                System.Threading.Thread th = new System.Threading.Thread(() =>
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        IBLL.ICus bll1 = new BLL.CusBLL();
                        int tmp;
                        //DataTable cus;
                        if (runType1 == 1)
                        {
                            //cus = paymentbll.GetSupcustList("C");
                            
                            this.txtcus.Invoke((MethodInvoker)delegate
                            {
                                this.txtcus.Bind(bll1.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:客户编码:100,sup_name:客户名称:150", "supcust_no/sup_name->Text");
                            });
                        }
                        else
                        {
                            // cus = paymentbll.GetSupcustList("S");
                            IBLL.ISup bll2 = new BLL.SupBLL();
                            this.txtcus.Invoke((MethodInvoker)delegate
                            {
                                this.txtcus.Bind(bll2.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150", "supcust_no/sup_name->Text");
                            });
                        }
                        //BLL.CusFY bll = new BLL.CusFY();
                        //var cus = bll.GetCusList();
                        
                        
                        IBLL.IBank bll = new BLL.BankBLL();
                        var tb_bank = bll.GetAllList();
                        this.txtbank.Invoke((MethodInvoker)delegate
                        {
                            this.txtbank.Bind(tb_bank, 300, 200, "visa_id", "visa_id:现金银行编码:120,visa_nm:现金银行名称:150", "visa_id/visa_nm->Text");
                        });
                        
                        IBLL.IPayment paymentBLL = new BLL.PaymentBLL();
                        //var pay_way = paymentBLL.GetAllList();
                        //this.txtpay_way.Invoke((MethodInvoker)delegate
                        //{
                        //    txtpay_way.Bind(pay_way, 300, 200, "pay_way", "pay_way:付款代码:100,pay_name:付款方式:100", "pay_way/pay_name->Text");
                        //    txtpay_way.GetDefaultValue();
                        //});

                        //var visa = bll.GetBankList();
                        //this.txtvisa.Invoke((MethodInvoker)delegate
                        //{
                        //    txtvisa.Bind(visa, 300, 200, "visa_id", "visa_id:编号:100,visa_nm:名称:130", "visa_id/visa_nm->Text");
                        //    txtvisa.GetDefaultValue();
                        //});

                        //var branch = bll.GetBranchList();
                        //this.txtbranch.Invoke((MethodInvoker)delegate
                        //{
                        //    txtbranch.Bind(branch, 300, 200, "branch_no", "branch_no:机构号:80,branch_name:机构名:140", "branch_no/branch_name->Text");
                        //});

                        IBLL.IPeople peopleBLL = new BLL.PeopleBLL();
                        var people = peopleBLL.GetDataTable("", "", 1, 1, 20000, out tmp);

                        this.txtpeople.Invoke((MethodInvoker)delegate
                        {
                            txtpeople.Bind(people, 250, 200, "oper_id", "oper_id:职员编号:80,oper_name:姓名:80", "oper_id/oper_name->Text");
                            //txtpeople.GetDefaultValue();
                        });

                        this.Invoke((MethodInvoker)delegate
                        {
                            IOrder ins = this;
                            ins.Add();
                        });
                    }
                    catch (Exception ex)
                    {
                        IvyBack.Helper.LogHelper.writeLog("frmCusSettle", ex.ToString());
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
                if (txtcus.Text.Trim().Contains("/") == false)
                {
                    throw new Exception("客户必填!");
                }
                //if (txtbranch.Text.Trim().Contains("/") == false)
                //{
                //    throw new Exception("机构必填!");
                //}
                if (txtoper_date.Text.Trim() == "")
                {
                    throw new Exception("单据日期必填!");
                }
                if (Helper.Conv.ToDecimal(txttotal_amount.Text.Trim()) == 0)
                {
                    throw new Exception("收款金额不能为0!");
                }
                //if (txtpay_way.Text.Trim().Contains("/") == false)
                //{
                //    throw new Exception("付款方式必填");
                //}
                //if (txtvisa.Text.Trim().Contains("/") == false)
                //{
                //    throw new Exception("账户必填");
                //}
                Model.rp_t_recpay_record_info ord = new Model.rp_t_recpay_record_info();
                ord.sheet_no = txtsheet_no.Text.Trim();
                ord.supcust_no = txtcus.Text.Split('/')[0];


                ord.supcust_flag =runType1==1? "C":"S";
                ord.flag_post = "1";
                ord.total_amount = Helper.Conv.ToDecimal(txttotal_amount.Text.Trim());
                ord.free_money = Helper.Conv.ToDecimal(txtfree_money.Text.Trim());
                ord.coin_no = "RMB";
                ord.coin_rate = 1;
                //ord.pay_way = txtpay_way.Text.Split('/')[0];
                ord.approve_flag = "0";
                ord.oper_id = txtoper_man.Text.Trim().Split('/')[0];
                ord.oper_date = Helper.Conv.ToDateTime(txtoper_date.Text.Trim());
                ord.deal_man = txtpeople.Text.Split('/')[0];
                ord.visa_id=txtbank.Text.Split('/')[0];
                ord.approve_man = "";
                //ord.approve_date = DateTime.MinValue;
                ord.other1 = txtmemo.Text.Trim();
                ord.other2 = "";
                ord.other3 = "0";
                //ord.visa_id = txtvisa.Text.Split('/')[0];
                ord.num1 = Conv.ToDecimal(txtCumulative.Text);//本次累计可核销金额
                ord.num2 = Conv.ToDecimal(txtThisSurplus.Text);//本次剩余未核销收款额
                ord.num3 = 0;
                //ord.cm_branch = "00";
                //ord.branch_no = txtbranch.Text.Split('/')[0];
                ord.from_date = DateTime.MinValue;
                ord.to_date = DateTime.MinValue;
                ord.rc_sheet_no = "";
                int flag = 0;
                List<Model.rp_t_recpay_record_detail> lines = new List<Model.rp_t_recpay_record_detail>();
                int index = 0;
                //decimal pay_amt = ord.total_amount;
                //decimal free_money = ord.free_money;
                //decimal get_amt = 0;
                //应还客户金额
                //foreach (DataRow row in editGrid1.DataSource.Rows)
                //{
                //    if (row["voucher_no"].ToString() != "" && row["select_flag"].ToString() == "1" && row["path"].ToString() == "-1")
                //    {
                //        get_amt += Conv.ToDecimal(row["pay_amount"]);
                //    }
                //}
                //pay_amt += get_amt;
                //结算
                decimal sum = 0;
                foreach (DataRow row in editGrid1.DataSource.Rows)
                {
                    index++;
                    if (row["voucher_no"].ToString() != "" &&  row["select_flag"].ToString() == "1")
                    {
                     
                        sum += Conv.ToDecimal(row["pay_amount"]);
                        Model.rp_t_recpay_record_detail line = new Model.rp_t_recpay_record_detail();
                       
                        line.sheet_no = ord.sheet_no;
                        line.voucher_no = row["voucher_no"].ToString();
                        line.sheet_amount = Helper.Conv.ToDecimal(row["sheet_amount"].ToString());
                        line.pay_amount = Conv.ToDecimal(row["pay_amount"]);
                        line.paid_amount = Helper.Conv.ToDecimal(row["paid_amount"].ToString());
                        line.pay_free = Helper.Conv.ToDecimal(row["pay_free"].ToString());
                        line.memo = row["memo"].ToString();
                        line.other1 = "";
                        line.other2 = "";
                        line.other3 = "";
                        line.num1 = Conv.ToDecimal(row["num1"]);//未核销金额
                        line.num2 = 0;
                        line.num3 = 0;
                        line.pay_date = System.DateTime.Now;
                        line.item_no = "";
                        line.path = ((row["path"].ToString() == "1" || row["path"].ToString() == "+") ? "+" : "-");
                        line.select_flag = row["select_flag"].ToString();
                        line.voucher_type = row["voucher_first"].ToString();
                        line.oper_date = Conv.ToDateTime(row["oper_date"]);
                        line.voucher_other1 = "";
                        line.voucher_other2 = "";
                        line.order_no = "";
                        flag = 1;
                        lines.Add(line);
                    }

                }
                //for(int i = 0; i < pay_dt.Rows.Count; i++)
                //{
                //    lr[i].sheet_no = ord.sheet_no;
                //}
                //if (flag == 0)
                //{
                //    throw new Exception("无可结算明细！");
                //}
                if ( sum<0)
                {
                    MsgForm.ShowFrom("待核销金额中负值累加的绝对值大于正值累加，无法保存。");
                    return;
                }

                if (runType == 1)
                {
                    IBLL.ICusSettle bll = new BLL.CusSettle();
                    string sheet_no = "";
                    string is_cs = runType1 == 1 ? "C" : "S";
                    bll.Add(ord, lines,pay_dt,is_cs, out sheet_no);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                }
                else if (runType == 2)
                {
                    IBLL.ICusSettle bll = new BLL.CusSettle();
                    bll.Change(ord, lines,pay_dt);
                    IOrder ins = this;
                    ins.ShowOrder(ord.sheet_no);
                }

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
                isAll = false;
                txtcus.Text = "";
                //this.txtbranch.GetDefaultValue();
                this.txtsheet_no.Text = "";
                this.txtoper_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                this.txtpeople.Text="";
                this.txtapprove_man.Text = "";
                this.txtoper_man.Text = Program.oper.oper_id + "/" + Program.oper.oper_name;
                this.txtmemo.Text = "";
                this.txtapprove_date.Text = "";
                txttotal_amount.Text = "0.00";
                txtfree_money.Text = "0.00";
                if(pay_dt!=null)
                pay_dt.Clear();
                txttotal_amount.Text = "0.00";
                txtfree_money.Text = "0.00";
                txtSurplus.Text = "0.00";
                txtCumulative.Text = "0.00";
                txtThisSurplus.Text = "0.00";

                //txtpay_way.GetDefaultValue();
                //txtvisa.GetDefaultValue();
                //
                var tb = editGrid1.DataSource;
                tb.Clear();
                //pay_dt = paymentbll.GetPaymentList();
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

                IBLL.ICusSettle bll = new BLL.CusSettle();
                DataTable tb1;
                DataTable tb2;
                string is_cs = runType1 == 1 ? "C" : "S";
                bll.GetOrder(sheet_no,is_cs, out tb1, out tb2);
                //IBLL.ISCPaymentBLL scbll = new BLL.SCPaymentBLL();
                pay_dt =paymentbll.GetPaymentList(sheet_no);
                
                //for(int i = 0; i < pay_dt.Rows.Count; i++)
                //{
                //    for(int j = 0; j < dt1.Rows.Count; j++)
                //    {
                //        if (pay_dt.Rows[i]["pay_way"].ToString() == dt1.Rows[j]["pay_way"].ToString())
                //        {
                //            pay_dt.Rows[i]["collection_amount"] = dt1.Rows[j]["total_amount"];
                //        }
                //    }

                //    DataRow[] dr = pay_dt.AsEnumerable().Where(item => item["pay_way"].ToString() == dt1.Rows[i]["pay_way"].ToString()).ToArray();

                //}
               

                //

                //
                var r1 = tb1.Rows[0];
                if (r1["cus_no"].ToString() == "")
                {
                    txtcus.Text = "";
                }
                else
                {
                    txtcus.Text = r1["cus_no"].ToString() + "/" + r1["cus_name"].ToString();
                }
                //if (r1["branch_no"].ToString() == "")
                //{
                //    txtbranch.Text = "";
                //}
                //else
                //{
                //    txtbranch.Text = r1["branch_no"].ToString() + "/" + r1["branch_name"].ToString();
                //}
                txtsheet_no.Text = r1["sheet_no"].ToString();
                txtoper_date.Text = Helper.Conv.ToDateTime(r1["oper_date"].ToString()).ToString("yyyy-MM-dd");
                txtThisSurplus.Text = Helper.Conv.ToString(r1["num2"]);
                if (r1["deal_man"].ToString() == "")
                {
                    txtpeople.Text = "";
                }
                else
                {
                    txtpeople.Text = r1["deal_man"].ToString() + "/" + r1["deal_man_name"].ToString();
                }
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
                txttotal_amount.Text = Helper.Conv.ToDecimal(r1["total_amount"].ToString()).ToString("0.00");
                txtfree_money.Text = Helper.Conv.ToDecimal(r1["free_money"].ToString()).ToString("0.00");
                //if (r1["pay_way"].ToString() == "")
                //{
                //    txtpay_way.Text = "";
                //}
                //else
                //{
                //    txtpay_way.Text = r1["pay_way"].ToString() + "/" + r1["pay_name"].ToString();
                //}
                //if (r1["visa_id"].ToString() == "")
                //{
                //    txtvisa.Text = "";
                //}
                //else
                //{
                //    txtvisa.Text = r1["visa_id"].ToString() + "/" + r1["visa_name"].ToString();
                //}
                //
                editGrid1.DataSource = tb2;


                Dictionary<string, object> dic = this.Tag as Dictionary<string, object>;
                this.Tag = Helper.Conv.ControlsAdds(this, dic);
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
                        IBLL.ICusSettle bll = new BLL.CusSettle();
                       
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
                    IBLL.ICusSettle bll = new BLL.CusSettle();
                    string is_cs = runType1 == 1 ? "C" : "S";
                    bll.Check(sheet_no, Program.oper.oper_id, is_cs);
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

        private void editGrid1_ClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if ("select_flag".Equals(column_name))
            {
                var select_flag = row["select_flag"].ToString();
                if ("0".Equals(select_flag))
                {
                    row["select_flag"] = "1";
                    //row["pay_amount"] = Conv.ToDecimal(row["yf_amount"]);
                    //row["pay_free"] = 0.00;
                }
                else
                {
                    row["select_flag"] = "0";
                    if (Conv.ToDecimal(txtThisSurplus.Text) + Conv.ToDecimal(row["pay_amount"]) < 0)
                        txtThisSurplus.Text = "0.00";
                    else
                    {
                        txtThisSurplus.Text = Conv.ToString(Conv.ToDecimal(txtThisSurplus.Text) + Conv.ToDecimal(row["pay_amount"]));
                    }
                    row["pay_amount"] = 0.00;
                    //row["pay_free"] = 0.00;
                }

                //var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                //{
                //    int path = 1;

                //    //if ("-".Equals(r.Field<object>("path").ToString()) || "-1".Equals(r.Field<object>("path").ToString()))
                //    //{
                //    //    path = -1;
                //    //}
                //    return Conv.ToDecimal(r.Field<object>("pay_amount")) * path;
                //});
                //this.txttotal_amount.Text = pay_amount.ToString("0.00");

                //pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                //{
                //    int path = 1;

                //    //if ("-".Equals(r.Field<object>("path").ToString()) || "-1".Equals(r.Field<object>("path").ToString()))
                //    //{
                //    //    path = -1;
                //    //}
                //    return Conv.ToDecimal(r.Field<object>("pay_free")) * path;
                //});
                //this.txtfree_money.Text = pay_amount.ToString("0.00");

                //this.editGrid1.DataSource = this.editGrid1.DataSource;
                this.editGrid1.Refresh();

            }
        }
       public bool isAll = false;
        private void myButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtcus.Text)) return;
                //if (!string.IsNullOrEmpty(this.txtsheet_no.Text)) return;

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
                string cus = this.txtcus.Text.Trim().Split('/')[0];
                System.Threading.Thread th = new System.Threading.Thread(() =>
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Helper.GlobalData.windows.ShowLoad(this);
                    try
                    {
                        IBLL.ICusSettle bll = new BLL.CusSettle();
                        DataTable tb;
                        if (runType1 == 1)
                        {
                             tb = bll.GetAccountFlows(new Model.rp_t_accout_payrec_flow()
                            {
                                supcust_no = cus,
                                supcust_flag = "C",
                            });
                        }
                        else
                        {
                             tb = bll.GetAccountFlows(new Model.rp_t_accout_payrec_flow()
                            {
                                supcust_no = cus,
                                supcust_flag = "S",
                            });
                        }

                        this.Invoke((MethodInvoker)delegate
                        {
                            foreach (DataRow dr in tb.Rows)
                            {
                                var r = this.editGrid1.DataSource.NewRow();
                                r["select_flag"] = "1";
                                r["path"] = dr["pay_type"];
                                r["voucher_no"] = dr["voucher_no"];
                                r["oper_date"] = dr["oper_date"];
                                r["voucher_first"] = dr["trans_no"];
                                if(dr["pay_type"].ToString()=="-1"|| dr["pay_type"].ToString() == "-")
                                {
                                    r["sheet_amount"] = 0-Conv.ToDecimal(dr["sheet_amount"]);
                                }else
                                r["sheet_amount"] = dr["sheet_amount"];
                                r["pay_date"] = dr["pay_date"];
                                r["paid_amount"] = dr["已核销金额"];
                                r["paid_free"] = "0.00";
                                r["yf_amount"] = Conv.ToDecimal(r["sheet_amount"])  - Conv.ToDecimal(dr["已核销金额"]);
                                r["pay_free"] = 0.00;
                                r["memo"] = dr["memo"];
                                r["pay_amount"] = "0.00";
                                r["num1"]= Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);

                                this.editGrid1.DataSource.Rows.Add(r);
                            }
                            //editGrid1.AddColumn("select_flag", "核销", "", 50, 2, "", false);
                            ////editGrid1.AddColumn("path", "方向", "", 50, 2, "{-1:-,1:+}", false);
                            //editGrid1.AddColumn("voucher_no", "单号", "", 150, 1, "", false);
                            //editGrid1.AddColumn("oper_date", "单据日期", "", 100, 3, "yyyy-MM-dd", false);
                            //editGrid1.AddColumn("voucher_first", "业务类型", "", 90, 2, IvyTransFunction.all_type_str, false);
                            //editGrid1.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("paid_amount", "已核销金额", "", 100, 3, "0.00", false);
                            ////editGrid1.AddColumn("paid_free", "已免付金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("yf_amount", "待核销金额", "", 100, 3, "0.00", false);
                            //editGrid1.AddColumn("pay_amount", "本次核销金额", "", 120, 3, "0.00", true);
                            //editGrid1.AddColumn("num1", "未核销金额", "", 100, 3, "0.00", false);
                            //var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                            //{
                            //    int path = 1;

                            //    if (r.Field<object>("path").ToString().Equals("-") || r.Field<object>("path").ToString().Equals("-1"))
                            //    {
                            //        path = -1;
                            //    }
                            //    return Conv.ToDecimal(r.Field<object>("pay_amount")) * path;
                            //});
                            //this.txttotal_amount.Text = pay_amount.ToString("0.00");

                            //pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                            //{
                            //    int path = 1;

                            //    if (r.Field<object>("path").ToString().Equals("-") || r.Field<object>("path").ToString().Equals("-1"))
                            //    {
                            //        path = -1;
                            //    }
                            //    return Conv.ToDecimal(r.Field<object>("pay_free")) * path;
                            //});
                            //this.txtfree_money.Text = pay_amount.ToString("0.00");

                            this.editGrid1.DataSource = this.editGrid1.DataSource;
                            var tb4 = this.editGrid1.DataSource;
                            decimal num_ber = 0;
                            if (runType1 == 1)
                                foreach (DataRow dr in tb4.Rows)
                                {
                                    if (Conv.ToString(dr["voucher_first"]) == "CP" && (Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"]) < 0))
                                        num_ber += Math.Abs((Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"])));
                                }
                            else
                            {
                                foreach (DataRow dr in tb4.Rows)
                                {
                                    if (Conv.ToString(dr["voucher_first"]) == "RP" && (Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"]) < 0))
                                        num_ber += Math.Abs((Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"])));
                                }
                            }

                            //all_type_dic.Add("RP", "供应商结算单"); all_type_dic.Add("CP", "客户结算单");
                            txtSurplus.Text = Conv.ToString(num_ber);
                            this.editGrid1.Refresh();
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

        private void editGrid1_CellEndEdit(object sender, string column_name, DataRow row)
        {
            if (column_name.Equals("pay_amount"))
            {
                if (row["select_flag"].ToString() != "1")
                {
                    MsgForm.ShowFrom("请不要填写未勾选核销的行！");
                    row["pay_amount"] = 0.00;
                    return;
                }
                if (row["yf_amount"].ToDecimal() > 0)
                {
                    if (row["pay_amount"].ToDecimal() < 0)
                    {
                        MsgForm.ShowFrom("当待核销金额>0时，本次核销金额必须>=0");
                        row["pay_amount"] = 0.00;
                        return;
                    }
                    if (row["paid_amount"].ToDecimal() + row["pay_amount"].ToDecimal() > row["sheet_amount"].ToDecimal())
                    {
                        MsgForm.ShowFrom("当待核销金额<0时，本次核销金额必须<=0，金额必须<=已核销金额+本次核销金额");
                        row["pay_amount"] = 0.00;
                        return;
                    }
                }
                else if (row["yf_amount"].ToDecimal() < 0)
                {
                    if (row["pay_amount"].ToDecimal() > 0)
                    {
                        MsgForm.ShowFrom("当待核销金额<0时，本次核销金额必须<=0");
                        row["pay_amount"] = 0.00;
                        return;
                    }
                    if (row["paid_amount"].ToDecimal() + row["pay_amount"].ToDecimal() < row["sheet_amount"].ToDecimal())
                    {
                        MsgForm.ShowFrom("当待核销金额<0时，本次核销金额必须<=0，金额必须<=已核销金额+本次核销金额");
                        row["pay_amount"] = 0.00;
                        return;
                    }

                }
                else
                {
                    if (row["pay_amount"].ToDecimal() != 0)
                    {
                        MsgForm.ShowFrom("当金额=0时，本次核销金额必须=0");
                        row["pay_amount"] = 0.00;
                        return;
                    }
                }
                if (Math.Abs(row["pay_amount"].ToDecimal()) > Math.Abs(row["yf_amount"].ToDecimal()))
                {
                    row["pay_amount"] = 0;
                    //row["num1"] = 0;

                }
                else
                {
                    row["num1"] = row["yf_amount"].ToDecimal() - row["pay_amount"].ToDecimal();
                }



                DataTable dt = editGrid1.DataSource;
                decimal cum = Conv.ToDecimal(txtCumulative.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (cum >= Conv.ToDecimal(dt.Rows[i]["yf_amount"]))
                    {
                        //dt.Rows[i]["pay_amount"] = dt.Rows[i]["yf_amount"];
                        //dt.Rows[i]["num1"] = 0;
                        cum -= Conv.ToDecimal(dt.Rows[i]["pay_amount"]);

                    }
                    else if ( cum < Conv.ToDecimal(dt.Rows[i]["yf_amount"]))
                    {
                        //dt.Rows[i]["pay_amount"] = cum;
                        //dt.Rows[i]["num1"] = Conv.ToDecimal(dt.Rows[i]["yf_amount"]) - cum;
                        cum = 0;
                    }
                    else
                    {
                        break;
                    }
                }
                txtThisSurplus.Text = Conv.ToString(cum);
            }

        }

        string select_flag = "1";
        private void editGrid1_DoubleClick(object sender, EventArgs e)
        {
            //foreach (DataRow dr in this.editGrid1.DataSource.Rows)
            //{
            //    if ("0".Equals(select_flag))
            //    {
            //        dr["select_flag"] = "1";
            //        dr["pay_amount"] = Conv.ToDecimal(dr["yf_amount"]);
            //    }
            //    else
            //    {
            //        dr["select_flag"] = "0";
            //        dr["pay_amount"] = 0.00;
            //    }
            //}
            //select_flag = "0".Equals(select_flag) ? "1" : "0";

            //var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum(r => Conv.ToDecimal(r.Field<object>("path")) * Conv.ToDecimal(r.Field<object>("pay_amount")));
            //this.txttotal_amount.Text = pay_amount.ToString("0.00");

            //pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum(r => Conv.ToDecimal(r.Field<object>("pay_free")));
            //this.txtfree_money.Text = pay_amount.ToString("0.00");

            //this.editGrid1.DataSource = this.editGrid1.DataSource;
            //this.editGrid1.Refresh();
        }

        private void txttotal_amount_TextChanged(object sender, EventArgs e)
        {
            //this.txtcus.Enabled = "0.00".Equals(this.txttotal_amount.Text);
            this.editGrid1.SetTotalColumn("pay_amount", this.txttotal_amount.Text.ToDecimal());
            SetCumulative();
        }
        private void txttotal_amount_Leave(object sender, EventArgs e)
        {
            //decimal money = this.txttotal_amount.Text.ToDecimal();

            //foreach (DataRow row in this.editGrid1.DataSource.Rows)
            //{
            //    if (row["select_flag"].ToDecimal() == 1)
            //    {
            //        decimal yf_amount = row["yf_amount"].ToDecimal();

            //        if (money > yf_amount)
            //        {
            //            row["pay_amount"] = yf_amount;
            //            money -= yf_amount;
            //        }
            //        else
            //        {
            //            row["pay_amount"] = money;
            //            money = 0;
            //        }
            //    }
            //}
            //this.editGrid1.Refresh();
        }

        private void txtfree_money_TextChanged(object sender, EventArgs e)
        {
           
            //this.editGrid1.SetTotalColumn("pay_free", this.txtfree_money.Text.ToDecimal());
            //SetCumulative();

        }

        public string method_id = "ap005";
        public void GetPrintTb()
        {
            //if (string.IsNullOrEmpty(this.txtsheet_no.Text))
            //    throw new Exception("请选择单据!");

            IBLL.ICusSettle bll = new BLL.CusSettle();
            DataTable tb1;
            DataTable tb2;
            string is_cs = runType1 == 1 ? "C" : "S";
            if (runType1==1)
            {
                method_id = "ap005";
            }
            else
            {
                method_id = "ap006";
            }
            bll.GetOrder(this.txtsheet_no.Text,is_cs, out tb1, out tb2);

            DataTable print_tb_m = new DataTable();
            DataTable print_tb_d = new DataTable();
            //表头
            {

                Dictionary<string, string> file_dic = new Dictionary<string, string>()
                {
                    {"sheet_no","单据号"},       
                    {"total_amount","金额"},
                    {"free_money","免收金额"},
                    {"num1","本次累计可核销金额"},
                    {"num2","本次剩余未核销收款额"},
                    {"oper_id","操作人编号"},
                    {"oper_name","制单人"},
                    {"oper_date","操作日期"},
                    {"approve_man","审核人编码"},
                    {"approve_man_name","审核人"},
                    {"deal_man","业务员编码"},
                    {"deal_man_name","业务员名称"},
                    {"other1","备注"},
                };
                if (runType1 == 1)
                {
                    file_dic.Add("cus_no", "客户编号");
                    file_dic.Add("cus_name", "客户名称");
                }
                else
                {
                    file_dic.Add("cus_no", "供应商编号");
                    file_dic.Add("cus_name", "供应商名称");
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
                    {"voucher_first","业务类型"},
                    {"sheet_amount","单据金额"},
                    {"paid_amount","已付金额"},
                    {"yf_amount","应付金额"},
                    {"pay_amount","本次核销金额"},
                    {"num1","未核销金额"},
                    {"memo","备注"},
                };
                //Dictionary<string, string> convert_dic = new Dictionary<string, string>()
                //{
                //    {"path","{-1:-,1:+}"},
                //    {"voucher_first",IvyTransFunction.all_type_str},
                //};

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
                //foreach (DataRow dr in tb2.Rows)
                //{
                //    DataRow row = print_tb_d.NewRow();
                //    foreach (DataColumn dc in tb2.Columns)
                //    {
                //        string value;
                //        if (file_dic.TryGetValue(dc.ColumnName, out value))
                //        {
                //            string format;
                //            //convert_dic.TryGetValue(dc.ColumnName, out format);
                //            row[file_dic[key]] = dr[key];
                //            //row[value] = Conv.Format(dc, dr[dc], format);
                //        }
                //    }

                //    print_tb_d.Rows.Add(row);
                //}

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
        public List<rp_t_collection_way> lr;
        private void txttotal_amount_Click(object sender, EventArgs e)
        {
            decimal num=0.00m;
            var frm = new frmPaymentDetailed(pay_dt);
            //frm.runType = runType1;
            string visa_id;
            if(frm.ShowPayment(out pay_dt,out visa_id) == DialogResult.Yes)
            {               
               for(int i = 0; i < pay_dt.Rows.Count; i++)
                {
                    if(Conv.ToDecimal(pay_dt.Rows[i]["total_amount"])>0)
                    num += Conv.ToDecimal(pay_dt.Rows[i]["total_amount"]);
                }
                txttotal_amount.Text = Conv.ToString(num);
                txtbank.Text = visa_id;
            }
           
        }

        private void txtcus_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            //string supcust_no= txtcus.Text.Split('/')[0];
            // string is_cs = runType1 == 1 ? "C" : "S";
            //DataTable dt= paymentbll.GetRecpayRecordModel(supcust_no, is_cs);
            // if (dt.Rows.Count > 0)
            // {
            //     txtSurplus.Text = Conv.ToString(dt.Rows[0]["num2"]);
            // }
            // else
            // {
            //     txtSurplus.Text = "0.00";
            // }
            var tb = editGrid1.DataSource;
            tb.Clear();
            editGrid1.Refresh();
            isAll = false;
        }

        public void SetCumulative()
        {
            txtCumulative.Text = Conv.ToString(Conv.ToDecimal(txttotal_amount.Text) + Conv.ToDecimal(txtfree_money.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt1 = editGrid1.DataSource;
            DataTable dt = dt1.Copy();
           decimal cum= Conv.ToDecimal(txtCumulative.Text);
            decimal sum = 0;
            decimal sum1 = 0;
            decimal sum2 = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["select_flag"].ToString() == "1"&& Conv.ToDecimal(dt.Rows[i]["yf_amount"])<0)
                {                   
                        dt.Rows[i]["pay_amount"] = dt.Rows[i]["yf_amount"];
                        dt.Rows[i]["num1"] = 0;
                        cum -= Conv.ToDecimal(dt.Rows[i]["pay_amount"]);
                        sum1-= Conv.ToDecimal(dt.Rows[i]["pay_amount"]);
                        sum += Conv.ToDecimal(dt.Rows[i]["pay_amount"]);

                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["select_flag"].ToString() == "1" && Conv.ToDecimal(dt.Rows[i]["yf_amount"]) >= 0)
                {
                    if (cum >= Conv.ToDecimal(dt.Rows[i]["yf_amount"]))
                    {
                        dt.Rows[i]["pay_amount"] = dt.Rows[i]["yf_amount"];
                        dt.Rows[i]["num1"] = 0;
                        cum -= Conv.ToDecimal(dt.Rows[i]["pay_amount"]);
                        sum += Conv.ToDecimal(dt.Rows[i]["pay_amount"]);
                        sum2+= Conv.ToDecimal(dt.Rows[i]["pay_amount"]);
                    }
                    else if ( cum < Conv.ToDecimal(dt.Rows[i]["yf_amount"]))
                    {
                        dt.Rows[i]["pay_amount"] = cum;
                        dt.Rows[i]["num1"] = Conv.ToDecimal(dt.Rows[i]["yf_amount"]) - cum;
                        cum = 0;
                        sum += Conv.ToDecimal(dt.Rows[i]["pay_amount"]);
                        sum2 += Conv.ToDecimal(dt.Rows[i]["pay_amount"]);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
            if (sum1 > sum2)
            {
                MsgForm.ShowFrom("待核销金额中负值累加的绝对值大于正值累加，请手动核销。");
                return;
            }
            txtThisSurplus.Text = Conv.ToString(cum);
            //if (Conv.ToDecimal(txtfree_money.Text) > sum)
            //{
            //    MsgForm.ShowFrom("免收金额不可大于总核销金额");
            //    txtfree_money.Text = "0.00";
            //    return;
            //}
            editGrid1.DataSource = dt;
        }

        private void txtfree_money_Leave(object sender, EventArgs e)
        {

            if (Conv.ToDecimal(txtfree_money.Text) < 0)
            {
                MsgForm.ShowFrom("免收金额不可小于0");
                txtfree_money.Text = "0.00";
            }
            SetCumulative();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isAll)
            {
                MsgForm.ShowFrom("已经生成全部数据或引用账期通知单，禁止再手动添加。");
                return;
            }
            if (string.IsNullOrEmpty(this.txtcus.Text)) return;
            //if (!string.IsNullOrEmpty(this.txtsheet_no.Text)) return;
            List<DataRow> list = new List<DataRow>();
            string cus = this.txtcus.Text.Trim().Split('/')[0];
            var frm = new frmCollectionSelect(cus);
            frm.runType = runType1;
            if (frm.ShowPayment(out list) == DialogResult.Yes)
            {
                foreach (DataRow row in list)
                {
                    //DataRow row1 = row;

                    editGrid1.DataSource.ImportRow(row);
                }
                this.editGrid1.DataSource = this.editGrid1.DataSource;
                var tb4 = this.editGrid1.DataSource;
                decimal num_ber = 0;
                if (runType1 == 1)
                    foreach (DataRow dr in tb4.Rows)
                    {
                        if (Conv.ToString(dr["voucher_first"]) == "CP" && (Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"]) < 0))
                            num_ber += Math.Abs((Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"])));
                    }
                else
                {
                    foreach (DataRow dr in tb4.Rows)
                    {
                        if (Conv.ToString(dr["voucher_first"]) == "RP" && (Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"]) < 0))
                            num_ber += Math.Abs((Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"])));
                    }
                }

                //all_type_dic.Add("RP", "供应商结算单"); all_type_dic.Add("CP", "客户结算单");all_type_dic.Add("RP", "供应商结算单"); all_type_dic.Add("CP", "客户结算单");
                txtSurplus.Text = Conv.ToString(num_ber);
                editGrid1.Refresh();
                //var dt = this.editGrid1.DataSource;
                //decimal sum1 = 0;
                //string type = runType1 == 1 ? "CP" : "RP";
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (dt.Rows[i]["select_flag"].ToString() == "1" && Conv.ToDecimal(dt.Rows[i]["yf_amount"]) < 0&& dt.Rows[i]["voucher_first"].ToString()==type)
                //    {
                //        sum1 -= Conv.ToDecimal(dt.Rows[i]["pay_amount"]);

                //    }
                //}
                //txtSurplus.Text = Conv.ToString(sum1);
            }
        }

        private void txtCumulative_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = editGrid1.DataSource;
            decimal cum = Conv.ToDecimal(txtCumulative.Text);
            decimal sum = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["select_flag"].ToString() == "1")
                {               
                    sum += Conv.ToDecimal(dt.Rows[i]["pay_amount"]);

                }
            }
            if (cum <= sum)
            {
                txtThisSurplus.Text = "0.00";
            }
            else
            {
                txtThisSurplus.Text = Conv.ToString(cum - sum);
            }
        }

        private void txttotal_amount_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {

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
                    IBLL.ICusSettle bll = new BLL.CusSettle();
                    string is_cs = runType1 == 1 ? "C" : "S";
                    bll.NotCheck(sheet_no, Program.oper.oper_id, is_cs);
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

        private void txtapprove_man_TextChanged(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {           
            if (string.IsNullOrEmpty(this.txtcus.Text)) return;
            List<DataRow> list = new List<DataRow>();
            List<rp_t_account_notice> lr = new List<rp_t_account_notice>();
            string cus = this.txtcus.Text.Trim().Split('/')[0];
            var frm = new frmNoticeSelect(cus);
            frm.runType = runType1;
            if (frm.ShowPayment(out list) == DialogResult.Yes)
            {
                isAll = true;
                int index = 0;
                foreach(DataRow dr in list)
                {
                    rp_t_account_notice line = new rp_t_account_notice();
                    line.sheet_no = Conv.ToString(dr["sheet_no"]);
                    lr.Add(line);
                    index++;
                }
                if (index == 0)
                {
                    return;
                }
                var tb = paymentbll.GetCollectionNotice(lr);
                var tb1 = editGrid1.DataSource;
                tb1.Clear();
                foreach (DataRow dr in tb.Rows)
                {
                    var r = this.editGrid1.DataSource.NewRow();
                    r["select_flag"] = "1";
                    r["path"] = dr["pay_type"];
                    r["voucher_no"] = dr["voucher_no"];
                    r["oper_date"] = dr["oper_date"];
                    r["voucher_first"] = dr["trans_no"];
                    if (dr["pay_type"].ToString() == "-1" || dr["pay_type"].ToString() == "-")
                    {
                        r["sheet_amount"] = 0 - Conv.ToDecimal(dr["sheet_amount"]);
                    }
                    else
                        r["sheet_amount"] = dr["sheet_amount"];
                    r["pay_date"] = dr["pay_date"];
                    r["paid_amount"] = dr["已核销金额"];
                    r["paid_free"] = "0.00";
                    r["yf_amount"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);
                    r["pay_free"] = 0.00;
                    r["memo"] = dr["memo"];
                    r["pay_amount"] = "0.00";
                    r["num1"] = Conv.ToDecimal(r["sheet_amount"]) - Conv.ToDecimal(dr["已核销金额"]);

                    this.editGrid1.DataSource.Rows.Add(r);
                }

                this.editGrid1.DataSource = this.editGrid1.DataSource;
                var tb4 = this.editGrid1.DataSource;
                decimal num_ber = 0;
                if (runType1 == 1)
                    foreach (DataRow dr in tb4.Rows)
                    {
                        if (Conv.ToString(dr["voucher_first"]) == "CP" && (Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"]) < 0))
                            num_ber += Math.Abs((Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"])));
                    }
                else
                {
                    foreach (DataRow dr in tb4.Rows)
                    {
                        if (Conv.ToString(dr["voucher_first"]) == "RP" && (Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"]) < 0))
                            num_ber += Math.Abs((Conv.ToDecimal(dr["sheet_amount"]) - Conv.ToDecimal(dr["paid_amount"])));
                    }
                }

                //all_type_dic.Add("RP", "供应商结算单"); all_type_dic.Add("CP", "客户结算单");all_type_dic.Add("RP", "供应商结算单"); all_type_dic.Add("CP", "客户结算单");
                txtSurplus.Text = Conv.ToString(num_ber);
                editGrid1.Refresh();
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

        private void editGrid1_DoubleClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if(column_name== "voucher_no")
            {
                string type = Conv.ToString(row["voucher_first"]);
               
                var frm = new frmSheetTypeInfo();
                System.Data.DataTable tb1;
                System.Data.DataTable tb2;
                if (type == "CP")//客户结算单
                {
                    frm.dataGrid1.AddColumn("sheet_no", "单据号", "", 130, 1, "");
                    frm.dataGrid1.AddColumn("cus_no", "客户编号", "", 80, 1, "");
                    frm.dataGrid1.AddColumn("cus_name", "客户名称", "", 150, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "结算金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("free_money", "免付金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("deal_man_a", "经办人", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("approve_man_a", "审核人", "", 110, 1, "");
                    frm.dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd");
                    frm.dataGrid1.AddColumn("oper_id_a", "操作员", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd");
                    frm.dataGrid2.AddColumn("voucher_no", "业务单号", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("oper_date", "单据日期", "", 100, 1, "yyyy-MM-dd");
                    frm.dataGrid2.AddColumn("voucher_first", "业务类型", "", 120, 2, IvyTransFunction.all_type_str);
                    frm.dataGrid2.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("paid_amount", "已核销金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("yf_amount", "应付金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("pay_amount", "本次付款金额", "", 120, 3, "0.00");
                    frm.dataGrid2.AddColumn("num1", "未核销金额", "", 120, 3, "0.00");
                    frm.dataGrid2.AddColumn("memo", "备注", "", 180, 1, "");
                    frm.dataGrid2.AddColumn("voucher_type", "业务描述", "", 100, 1, "");
                    IBLL.ICusSettle bll = new BLL.CusSettle();
                    

                    bll.GetOrder(Conv.ToString(row["voucher_no"]), "C", out tb1, out tb2);
                    frm.tb1 = tb1;
                    frm.tb2 = tb2;
                    frm.ShowDialog();

                } else if (type == "RP")//供应商结算单
                {
                    frm.dataGrid1.AddColumn("sheet_no", "单据号", "", 130, 1, "");
                    frm.dataGrid1.AddColumn("cus_no", "供应商编号", "", 80, 1, "");
                    frm.dataGrid1.AddColumn("cus_name", "供应商名称", "", 150, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "结算金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("free_money", "免付金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("deal_man_a", "经办人", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("approve_man_a", "审核人", "", 110, 1, "");
                    frm.dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd");
                    frm.dataGrid1.AddColumn("oper_id_a", "操作员", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd");
                    frm.dataGrid2.AddColumn("voucher_no", "业务单号", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("oper_date", "单据日期", "", 100, 1, "yyyy-MM-dd");
                    frm.dataGrid2.AddColumn("voucher_first", "业务类型", "", 120, 2, IvyTransFunction.all_type_str);
                    frm.dataGrid2.AddColumn("sheet_amount", "单据金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("paid_amount", "已核销金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("yf_amount", "应付金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("pay_amount", "本次付款金额", "", 120, 3, "0.00");
                    frm.dataGrid2.AddColumn("num1", "未核销金额", "", 120, 3, "0.00");
                    frm.dataGrid2.AddColumn("memo", "备注", "", 180, 1, "");
                    frm.dataGrid2.AddColumn("voucher_type", "业务描述", "", 100, 1, "");
                    IBLL.ICusSettle bll = new BLL.CusSettle();


                    bll.GetOrder(Conv.ToString(row["voucher_no"]), "S", out tb1, out tb2);
                    frm.tb1 = tb1;
                    frm.tb2 = tb2;
                    frm.ShowDialog();
                }
                else if (type == "CM")//其他应收单
                {
                    frm.dataGrid1.AddColumn("sheet_no", "费用单号", "", 130, 1, "");
                    frm.dataGrid1.AddColumn("cus_no", "客户编号", "", 80, 1, "");
                    frm.dataGrid1.AddColumn("cus_name", "客户名称", "", 150, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "金额", "", 150, 3, "");
                    frm.dataGrid1.AddColumn("branch_no_a", "发生机构", "", 130, 1, "");
                    frm.dataGrid1.AddColumn("old_no", "原始单号", "", 130, 1, "");
                    frm.dataGrid1.AddColumn("sale_man_a", "经办人", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("approve_man_a", "审核人", "", 110, 1, "");
                    frm.dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("oper_id_a", "操作员", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid2.AddColumn("kk_no", "费用代码", "", 90, 1, "");
                    frm.dataGrid2.AddColumn("kk_name", "费用名称", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("kk_cash", "费用(金额)", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("pay_kind", "应收增减", "", 80, 2, "{0:-,1:+}");
                    frm.dataGrid2.AddColumn("other1", "备注", "", 150, 1, "");
                    IBLL.ICusFY bll = new BLL.CusFY();


                    bll.GetOrder(Conv.ToString(row["voucher_no"]), out tb1, out tb2);
                    frm.tb1 = tb1;
                    frm.tb2 = tb2;
                    frm.ShowDialog();
                }
                else if (type == "GM")//其他应付单
                {
                    frm.dataGrid1.AddColumn("sheet_no", "费用单号", "", 130, 1, "");
                    frm.dataGrid1.AddColumn("sup_no", "供应商编号", "", 80, 1, "");
                    frm.dataGrid1.AddColumn("sup_name", "供应商名称", "", 150, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "金额", "", 150, 3, "");
                    frm.dataGrid1.AddColumn("branch_no_a", "发生机构", "", 130, 1, "");
                    frm.dataGrid1.AddColumn("old_no", "原始单号", "", 130, 1, "");
                    frm.dataGrid1.AddColumn("sale_man_a", "经办人", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("approve_man_a", "审核人", "", 110, 1, "");
                    frm.dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("oper_id_a", "操作员", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    //
                    frm.dataGrid2.AddColumn("kk_no", "费用代码", "", 90, 1, "");
                    frm.dataGrid2.AddColumn("kk_name", "费用名称", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("kk_cash", "费用(金额)", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("pay_kind", "应付增减", "", 80, 2, "{0:-,1:+}");
                    frm.dataGrid2.AddColumn("other1", "备注", "", 150, 1, "");
                    IBLL.ISupFY bll = new BLL.SupFY();


                    bll.GetOrder(Conv.ToString(row["voucher_no"]), out tb1, out tb2);
                    frm.tb1 = tb1;
                    frm.tb2 = tb2;
                    frm.ShowDialog();
                }
                else if (type == "I")//销售出库单
                {
                    frm.dataGrid1.AddColumn("remark", "签收状态", "", 60, 2, "{0:签收 ,1:未签收}");
                    frm.dataGrid1.AddColumn("sheet_no", "销售单号", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("branch_name", "仓库", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("sup_name", "客户", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("voucher_no", "订单号", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "订单金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("sale_man_name", "业务员", "", 80, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "单据金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("real_amount", "折后金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("pay_date", "结账日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("oper_date", "日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("oper_name", "操作员", "", 90, 1, "");
                    frm.dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("approve_man_name", "审核人", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("coin_no", "币种", "", 60, 1, "");
                    frm.dataGrid1.AddColumn("payfee_memo", "备注", "", 200, 1, "");
                    //
                    frm.dataGrid2.AddColumn("item_subno", "货号", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("barcode", "条码", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("item_name", "商品名称", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("other3", "赠送", "", 60, 2, "{0:否,1:是}");
                    frm.dataGrid2.AddColumn("unit_no", "单位", "", 60, 2, "");
                    frm.dataGrid2.AddColumn("dinghuo", "订货数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("shifa", "实发数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("yingjie", "验收数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("tiaozheng", "调整数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("real_price", "实际价", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("sale_money", "实际金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("branch_name", "分仓", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("batch_num", "批次", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("other5", "备注明细", "", 200, 1, "");
                    IBLL.IInOutBLL bll = new BLL.InOutBLL();


                    bll.GetSaleSheet(Conv.ToString(row["voucher_no"]), out tb1, out tb2);
                    frm.tb1 = tb1;
                    frm.tb2 = tb2;
                    frm.ShowDialog();
                }
                else if (type == "D")//销售退货单
                {
                    frm.dataGrid1.AddColumn("trans_no", "业务类型", "", 90, 2, "{A:进货入库,D:退货入库,F:退货出库}");
                    frm.dataGrid1.AddColumn("sheet_no", "退货单号", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("branch_name", "仓库", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("sup_name", "客户名称", "", 150, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "单据总金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("deal_man_name", "业务员", "", 80, 1, "");
                    frm.dataGrid1.AddColumn("voucher_no", "销售单号", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("oper_name", "操作员", "", 90, 1, "");
                    frm.dataGrid1.AddColumn("oper_date", "日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("approve_man_name", "审核人", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("pay_date", "限付款日", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("old_no", "内部单号", "", 120, 1, "");
                    //
                    frm.dataGrid2.AddColumn("item_subno", "货号", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("item_name", "商品名称", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("unit_no", "单位", "", 60, 2, "{0:否,1:是}");
                    frm.dataGrid2.AddColumn("item_size", "规格", "", 80, 2, "");
                    frm.dataGrid2.AddColumn("in_qty", "数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("valid_price", "单价", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("sub_amount", "金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("branch_name", "分仓", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("other1", "备注", "", 200, 1, "");
                    frm.dataGrid2.AddColumn("price", "参考进价", "", 100, 3, "0.00");
                    IBLL.IInOutBLL bll = new BLL.InOutBLL();


                    bll.GetInOut(Conv.ToString(row["voucher_no"]),"D", out tb1, out tb2);
                    frm.tb1 = tb1;
                    frm.tb2 = tb2;
                    frm.ShowDialog();
                }
                else if (type == "A")//采购进货单
                {
                    frm.dataGrid1.AddColumn("trans_no", "单据类型", "", 90, 2, "{A:进货入库,D:退货入库,F:退货出库}");
                    frm.dataGrid1.AddColumn("sale_no", "经销方式", "", 90, 2, "{A:购销,B:代销,C:联营,E:联营进货,Z:租赁}");
                    frm.dataGrid1.AddColumn("sheet_no", "进货单号", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("branch_name", "仓库", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("supcust_no", "供应商", "", 80, 2, "");
                    frm.dataGrid1.AddColumn("sup_name", "供应商名称", "", 150, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "单据总金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("deal_man_name", "经办人", "", 80, 1, "");
                    frm.dataGrid1.AddColumn("old_no", "内部单号", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("voucher_no", "订货单号", "", 90, 1, "");
                    frm.dataGrid1.AddColumn("oper_name", "操作员", "", 90, 1, "");
                    frm.dataGrid1.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("approve_man_name", "审核人", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("pay_date", "限付款日", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("other1", "备注1", "", 200, 1, "");
                    frm.dataGrid1.AddColumn("other3", "备注", "", 200, 1, "");
                    frm.dataGrid2.AddColumn("item_subno", "货号", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("item_name", "商品名称", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("unit_no", "单位", "", 60, 2, "{0:否,1:是}");
                    frm.dataGrid2.AddColumn("item_size", "规格", "", 80, 2, "");
                    frm.dataGrid2.AddColumn("yingjie", "应结数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("dinghuo", "订货数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("shishou", "实收数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("sunhao", "差异数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("valid_price", "单价", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("discount", "折扣", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("sub_amount", "金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("branch_name", "分仓", "", 120, 1, "");
                    //frm.dataGrid2.AddColumn("", "特价", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("batch_num", "批号", "", 200, 1, "");
                    frm.dataGrid2.AddColumn("valid_date", "有效期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid2.AddColumn("price", "参考进价", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("other1", "备注", "", 200, 1, "");
                    IBLL.IInOutBLL bll = new BLL.InOutBLL();


                    bll.GetInOut(Conv.ToString(row["voucher_no"]), "A", out tb1, out tb2);
                    frm.tb1 = tb1;
                    frm.tb2 = tb2;
                    frm.ShowDialog();
                }
                else if (type == "F")//采购退货单
                {
                    frm.dataGrid1.AddColumn("trans_no", "单据类型", "", 90, 2, "{A:进货入库,D:退货入库,F:退货出库}");
                    frm.dataGrid1.AddColumn("sale_no", "经销方式", "", 90, 2, "{A:购销,B:代销,C:联营,E:联营进货,Z:租赁}");
                    frm.dataGrid1.AddColumn("sheet_no", "退货单号", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("branch_name", "仓库", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("supcust_no", "供应商", "", 80, 2, "");
                    frm.dataGrid1.AddColumn("sup_name", "供应商名称", "", 150, 1, "");
                    frm.dataGrid1.AddColumn("total_amount", "单据总金额", "", 90, 3, "0.00");
                    frm.dataGrid1.AddColumn("deal_man_name", "经办人", "", 80, 1, "");
                    frm.dataGrid1.AddColumn("old_no", "内部单号", "", 120, 1, "");
                    frm.dataGrid1.AddColumn("voucher_no", "入库单号", "", 90, 1, "");
                    frm.dataGrid1.AddColumn("oper_name", "操作员", "", 90, 1, "");
                    frm.dataGrid1.AddColumn("oper_date", "操作日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("approve_man_name", "审核人", "", 100, 1, "");
                    frm.dataGrid1.AddColumn("approve_date", "审核日期", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("pay_date", "限收款日", "", 100, 1, "yyyy-MM-dd HH:mm:ss");
                    frm.dataGrid1.AddColumn("other1", "备注1", "", 200, 1, "");
                    frm.dataGrid1.AddColumn("other3", "备注", "", 200, 1, "");
                    frm.dataGrid2.AddColumn("item_subno", "货号", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("item_name", "商品名称", "", 150, 1, "");
                    frm.dataGrid2.AddColumn("unit_no", "单位", "", 60, 2, "{0:否,1:是}");
                    frm.dataGrid2.AddColumn("item_size", "规格", "", 80, 2, "");
                    frm.dataGrid2.AddColumn("in_qty", "数量", "", 100, 3, "");
                    frm.dataGrid2.AddColumn("valid_price", "单价", "", 100, 3, "0.00");
                    //frm.dataGrid2.AddColumn("discount", "折扣", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("sub_amount", "金额", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("branch_name", "分仓", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("batch_num", "批号", "", 150, 2, "");
                    //frm.dataGrid2.AddColumn("", "特价", "", 100, 3, "0.00");
                    frm.dataGrid2.AddColumn("other1", "备注", "", 200, 1, "");
                    //frm.dataGrid2.AddColumn("", "生产批号", "", 200, 1, "");
                    //frm.dataGrid2.AddColumn("valid_date", "有效期", "", 100, 1, "yyyy-MM-dd");
                    //frm.dataGrid2.AddColumn("", "库位", "", 120, 1, "");
                    frm.dataGrid2.AddColumn("price", "参考进价", "", 100, 3, "0.00");
                    IBLL.IInOutBLL bll = new BLL.InOutBLL();


                    bll.GetInOut(Conv.ToString(row["voucher_no"]), "F", out tb1, out tb2);
                    frm.tb1 = tb1;
                    frm.tb2 = tb2;
                    frm.ShowDialog();
                }
            }
        }
        //if (runType == 1)
        //    {
        //        all_type_dic.Add("CP", "收款单");
        //        all_type_dic.Add("I", "销售出库");
        //        all_type_dic.Add("D", "退货入库");
        //        all_type_dic.Add("CM", "其它应收");
        //    }
        //    else
        //    {
        //        all_type_dic.Add("RP", "付款单");
        //        all_type_dic.Add("A", "进货入库");
        //        all_type_dic.Add("F", "退货出库");
        //        all_type_dic.Add("GM", "其它应付");
        //    }
    }
}
