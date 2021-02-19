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

namespace IvyBack.VoucherForm
{
    public partial class frmCusCollectionPay : Form, IOrder
    {
        private IOrderList orderlist;
        private IOrderMerge ordermerge;

        private DataTable pay_dt = new DataTable();
        IBLL.ICashierBLL bll = new BLL.CashierBLL();
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
                    this.label1.Text = "供应商付款单";
                    this.label2.Text = "供应商：";
                    this.button2.Text = "选择供应商结算单";

                }
                _runType1 = value;
            }
        }
        DataTable tb = new DataTable();
        public frmCusCollectionPay(int run)
        {
           
            InitializeComponent();
            runType1 = run;
            //
            Helper.GlobalData.InitForm(this);
            //
            
            tb.Columns.Add("visa_id");
            tb.Columns.Add("supcust_no");
            tb.Columns.Add("sup_name");
            tb.Columns.Add("voucher_no");
            tb.Columns.Add("oper_date");
            tb.Columns.Add("pay_way");
            tb.Columns.Add("pay_amount", typeof(decimal));

            editGrid1.AddColumn("visa_id", "账户", "", 150, 2, "",false);
            if (runType1 == 1)
            {
                editGrid1.AddColumn("supcust_no", "客户编号", "", 100, 2, "", false);
                editGrid1.AddColumn("sup_name", "客户名称", "", 150, 2, "", false);
            }
            else
            {
                editGrid1.AddColumn("supcust_no", "供应商编号", "", 100, 2, "", false);
                editGrid1.AddColumn("sup_name", "供应商名称", "", 150, 2, "", false);
            }
            if (run==1)
            {
                editGrid1.AddColumn("voucher_no", "收款单号", "", 150, 2, "", false);
            }
            else
            {
                editGrid1.AddColumn("voucher_no", "付款单号", "", 150, 2, "", false);
            }
         
            editGrid1.AddColumn("oper_date", "结算单日期", "", 150, 2, "yyyy-MM-dd", false);
            editGrid1.AddColumn("pay_way", "结算方式", "", 150, 2, "", false);
            editGrid1.AddColumn("pay_amount", "结算金额", "", 150, 3, "0.00", false);
            editGrid1.SetTotalColumn("pay_amount");
            editGrid1.DataSource = tb;

            editGrid1.BindCheck("select_flag");         
            pay_dt.Columns.Add("pay_name");
            pay_dt.Columns.Add("total_amount");

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
                        DataTable cus;
                        int tmp;
                        if (runType1 == 1)
                        {
                            IBLL.ICus Cusbll = new BLL.CusBLL();

                            cus = Cusbll.GetDataTable("", "", 1, 1, 20000, out tmp);
                            this.txtcus.Invoke((MethodInvoker)delegate
                            {
                                this.txtcus.Bind(cus, 300, 200, "supcust_no", "supcust_no:客户编码:100,sup_name:客户名称:150", "supcust_no/sup_name->Text");
                            });
                            // cus = paymentbll.GetSupcustList("C");
                        }
                        else
                        {
                            IBLL.ISup Supbll = new BLL.SupBLL();
                            cus = Supbll.GetDataTable("", "", 1, 1, 20000, out tmp);
                            this.txtcus.Invoke((MethodInvoker)delegate
                            {
                                this.txtcus.Bind(cus, 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150", "supcust_no/sup_name->Text");
                            });

                            //cus = paymentbll.GetSupcustList("S");
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

                        //IBLL.IPeople peopleBLL = new BLL.PeopleBLL();
                        //int tmp1;
                        //var people = peopleBLL.GetDataTable("", "", 1, 1, 20000, out tmp1);

                        //this.txtpeople.Invoke((MethodInvoker)delegate
                        //{
                        //    txtpeople.Bind(people, 250, 200, "oper_id", "oper_id:职员编号:80,oper_name:姓名:80", "oper_id/oper_name->Text");
                        //    //txtpeople.GetDefaultValue();
                        //});

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
                //if (txtcus.Text.Trim().Contains("/") == false)
                //{
                //    throw new Exception("客户必填!");
                //}
                if (txtbank.Text.Trim().Contains("/") == false)
                {
                    throw new Exception("账户必填!");
                }
                if (txtoper_date.Text.Trim() == "")
                {
                    throw new Exception("单据日期必填!");
                }
                Model.rp_t_pay_info ord = new Model.rp_t_pay_info();
                ord.sheet_no = txtsheet_no.Text.Trim();
                ord.supcust_no = txtcus.Text.Split('/')[0];


                ord.supcust_flag =runType1==1? "C":"S";
                ord.coin_no = "RMB";
                ord.coin_rate = "1";
                ord.approve_flag = "0";
                ord.oper_id = txtoper_man.Text.Trim().Split('/')[0];
                ord.oper_date = Helper.Conv.ToDateTime(txtoper_date.Text.Trim());
                ord.approve_man = "";
                ord.visa_id=txtbank.Text.Trim().Split('/')[0];
                ord.start_date = Helper.Conv.ToDateTime(dateTextBox1.Text.Trim());
                ord.end_date = Helper.Conv.ToDateTime(dateTextBox2.Text.Trim());
                ord.other1 = txtmemo.Text.Trim();
                ord.other2 = "";
                ord.other3 = "";
                ord.num1 = 0;
                ord.num2 = 0;
                ord.num3 = 0;
                int flag = 0;
                List<Model.rp_t_pay_detail> lines = new List<Model.rp_t_pay_detail>();
                int index = 0;
            
                decimal sum = 0;
                foreach (DataRow row in editGrid1.DataSource.Rows)
                {
                    index++;
                    if(txtbank.Text.Split('/')[0].Trim()!= Helper.Conv.ToString(row["visa_id"]).Trim().Split('/')[0])
                    {
                        MsgForm.ShowFrom("表单账户和列表中的账户不一致，请改为一致。");
                        return;
                    }
                    if (txtcus.Text.Split('/')[0].Trim() != Helper.Conv.ToString(row["supcust_no"]).Trim().Split('/')[0]&& txtcus.Text.Split('/')[0].Trim()!="")
                    {
                        if(runType1 == 1)
                        MsgForm.ShowFrom("表单客户和列表中的客户不一致，请改为一致。");
                        else
                            MsgForm.ShowFrom("表单供应商和列表中的供应商不一致，请改为一致。");
                        return;
                    }
                    sum += Conv.ToDecimal(row["pay_amount"]);
                        Model.rp_t_pay_detail line = new Model.rp_t_pay_detail();
                       
                        line.sheet_no = ord.sheet_no;
                        line.voucher_no = row["voucher_no"].ToString();
                        line.pay_amount = Helper.Conv.ToDecimal(row["pay_amount"]);
                        line.visa_id = Helper.Conv.ToString(row["visa_id"]).Trim().Split('/')[0];
                        line.supcust_no = Conv.ToString(row["supcust_no"]);
                        line.supcust_flag = runType1 == 1 ? "C" : "S";
                        line.oper_date = Conv.ToDateTime(row["oper_date"]);
                        line.pay_way = Conv.ToString(row["pay_way"]).Trim().Split('/')[0];
                        line.other1 = "";
                        line.other2 = "";
                        line.other3 = "";
                        line.num1 = 0;
                        line.num2 = 0;
                        line.num3 = 0;                     
                        flag = 1;
                        lines.Add(line);
                    

                }
                ord.total_amount = sum;
                //for(int i = 0; i < pay_dt.Rows.Count; i++)
                //{
                //    lr[i].sheet_no = ord.sheet_no;
                //}
                if (flag == 0)
                {
                    throw new Exception("无可结算明细！");
                }
                //if (Conv.ToDecimal(txtfree_money.Text) > sum)
                //{
                //    MsgForm.ShowFrom("免收金额不可大于总核销金额");
                //    return;
                //}
                if (runType == 1)
                {
                    string sheet_no = "";
                    string is_cs = runType1 == 1 ? "C" : "S";
                    bll.AddCollectionPay(ord, lines,out sheet_no);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                }
                else if (runType == 2)
                {
                    string sheet_no = "";
                    bll.ChangeCollectionPay(ord, lines, out sheet_no);
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
                //this.txtpeople.Text = "";//GetDefaultValue();
                this.txtapprove_man.Text = "";
                this.txtoper_man.Text = Program.oper.oper_id + "/" + Program.oper.oper_name;
                this.txtmemo.Text = "";
                this.txtapprove_date.Text = "";
                this.dateTextBox1.Text = "";
                this.dateTextBox2.Text = "";
                this.txtbank.Text = "";
                //txttotal_amount.Text = "0.00";
                //txtfree_money.Text = "0.00";
                //if(pay_dt!=null)
                //pay_dt.Clear();
                //txttotal_amount.Text = "0.00";
                //txtfree_money.Text = "0.00";
                //txtSurplus.Text = "0.00";
                //txtCumulative.Text = "0.00";
                //txtThisSurplus.Text = "0.00";

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

                DataTable tb1;
                DataTable tb2;
                string is_cs = runType1 == 1 ? "C" : "S";
                bll.GetCollectionPayOrder(sheet_no,is_cs, out tb1, out tb2);
               
                var r1 = tb1.Rows[0];
                if (Conv.ToString(r1["supcust_no"]) == "")
                {
                    txtcus.Text = "";
                }
                else
                {
                    txtcus.Text = Conv.ToString(r1["supcust_no"]) + "/" + Conv.ToString(r1["sup_name"]);
                }
                txtsheet_no.Text = r1["sheet_no"].ToString();
                txtoper_date.Text = Helper.Conv.ToDateTime(r1["oper_date"].ToString()).ToString("yyyy-MM-dd");
                if (Conv.ToString(r1["visa_id"]) == "")
                {
                    txtbank.Text = "";
                }
                else
                {
                    txtbank.Text = Conv.ToString(r1["visa_id"]);
                }
                if (Conv.ToString(r1["start_date"]) == "")
                {
                    dateTextBox1.Text = "";
                }
                else
                {
                    dateTextBox1.Text = Conv.ToString(r1["start_date"]);
                }
                if (Conv.ToString(r1["end_date"]) == "")
                {
                    dateTextBox2.Text = "";
                }
                else
                {
                    dateTextBox2.Text = Conv.ToString(r1["end_date"]);
                }
                if (r1["approve_man"].ToString() == "")
                {
                    txtapprove_man.Text = "";
                }
                else
                {
                    txtapprove_man.Text = r1["approve_man"].ToString();
                }
                if (r1["oper_id"].ToString() == "")
                {
                    txtoper_man.Text = "";
                }
                else
                {
                    txtoper_man.Text = Conv.ToString(r1["oper_id"]);
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
                //txttotal_amount.Text = Helper.Conv.ToDecimal(r1["total_amount"].ToString()).ToString("0.00");
                //txtfree_money.Text = Helper.Conv.ToDecimal(r1["free_money"].ToString()).ToString("0.00");
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
                        bll.DeleteCollectionPay(sheet_no,DateTime.Now);
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
                    string is_cs = runType1 == 1 ? "C" : "S";
                    bll.CheckCollectionPay(sheet_no, Program.oper.oper_id,DateTime.Now);
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
           
        }
       public bool isAll = false;
        private void myButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void editGrid1_CellEndEdit(object sender, string column_name, DataRow row)
        {
          

        }
        private void editGrid1_DoubleClick(object sender, EventArgs e)
        {
         
        }

        private void txttotal_amount_TextChanged(object sender, EventArgs e)
        {

        }
        private void txttotal_amount_Leave(object sender, EventArgs e)
        {
        
        }

        private void txtfree_money_TextChanged(object sender, EventArgs e)
        {
           
            //this.editGrid1.SetTotalColumn("pay_free", this.txtfree_money.Text.ToDecimal());
            //SetCumulative();

        }

        public string method_id = "ap014";
        public void GetPrintTb()
        {
            //if (string.IsNullOrEmpty(this.txtsheet_no.Text))
            //    throw new Exception("请选择单据!");

            DataTable tb1;
            DataTable tb2;
            string is_cs = runType1 == 1 ? "C" : "S";
            bll.GetCollectionPayOrder(this.txtsheet_no.Text,is_cs, out tb1, out tb2);

            DataTable print_tb_m = new DataTable();
            DataTable print_tb_d = new DataTable();
            //表头
            {
                Dictionary<string, string> file_dic = new Dictionary<string, string>()
                {
                    {"sheet_no","单据号"},
                    {"oper_id_a","出纳人编号"},
                    {"oper_name","出纳人名称"},
                    {"oper_date","操作日期"},
                    {"approve_man_a","审核人编号"},
                    {"approve_man_name","审核人名称"},
                    {"visa_id_a","账户"},
                    {"visa_nm","账户名称"},
                    {"other1","备注"},
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
                    {"visa_id_a","账户"},
                    {"visa_nm","账户名称"},
                    {"supcust_no","客户/供应商编号"},
                    {"sup_name","客户/供应商名称"},
                    {"voucher_no","结算单号"},
                    {"oper_date","结算单日期"},
                    {"pay_way_a","结算方式编码"},
                    {"pay_name","结算方式名称"},
                    {"pay_amount","结算金额"},
                };
                //editGrid1.AddColumn("visa_id", "账户", "", 150, 2, "", false);
                //if (runType1 == 1)
                //{
                //    editGrid1.AddColumn("supcust_no", "客户编号", "", 100, 2, "", false);
                //    editGrid1.AddColumn("sup_name", "客户名称", "", 150, 2, "", false);
                //}
                //else
                //{
                //    editGrid1.AddColumn("supcust_no", "供应商编号", "", 100, 2, "", false);
                //    editGrid1.AddColumn("sup_name", "供应商名称", "", 150, 2, "", false);
                //}
                //editGrid1.AddColumn("voucher_no", "结算单号", "", 150, 2, "", false);
                //editGrid1.AddColumn("oper_date", "结算单日期", "", 150, 2, "yyyy-MM-dd", false);
                //editGrid1.AddColumn("pay_way", "结算方式", "", 150, 2, "", false);
                //editGrid1.AddColumn("pay_amount", "结算金额", "", 150, 2, "0.00", false);

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
                //            convert_dic.TryGetValue(dc.ColumnName, out format);

                //            row[value] = Conv.Format(dc, dr[dc], format);
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
            //decimal num=0.00m;
            //var frm = new frmPaymentDetailed(pay_dt);
            //if(frm.ShowPayment(out pay_dt) == DialogResult.Yes)
            //{               
            //   for(int i = 0; i < pay_dt.Rows.Count; i++)
            //    {
            //        if(Conv.ToDecimal(pay_dt.Rows[i]["total_amount"])>0)
            //        num += Conv.ToDecimal(pay_dt.Rows[i]["total_amount"]);
            //    }
            //    txttotal_amount.Text = Conv.ToString(num);
            //}
           
        }

        private void txtcus_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            var tb = editGrid1.DataSource;
            tb.Clear();
            editGrid1.Refresh();
        }


        public decimal amount=0;
        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void txtfree_money_Leave(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           

            List<DataRow> list = new List<DataRow>();
            var frm = new frmCollectionPaySelect();
            frm.runType = runType1;
            frm. supcust_no = txtcus.Text.Split('/')[0];
            if (txtbank.Text.Split('/')[0].Trim() == "")
            {
                MsgForm.ShowFrom("请选择账户");
                return;
            }
            frm.visa_id = txtbank.Text.Split('/')[0];
            frm.start_date = dateTextBox1.Text.Trim();
            frm.end_date = dateTextBox2.Text.Trim();
            if (frm.ShowPayment(out list) == DialogResult.Yes)
            {
                if (this.editGrid1.DataSource.Rows.Count > 0)
                {
                    if (YesNoForm.ShowFrom("确认要清空覆盖吗?") == DialogResult.Yes)
                    {
                        tb.Rows.Clear();
                    }
                    else
                    {
                        return;
                    }
                }
                DataTable dt = bll.GetCollectionWayList("");
                foreach (DataRow row in list)
                {
                    var tb2 = dt.AsEnumerable().Where(item => Conv.ToString(item["sheet_no"]) == Conv.ToString(row["voucher_no"])).ToArray();
                    if(tb2.Length>0)
                    {
                        foreach (DataRow row1 in tb2)
                        {
                            DataRow dr = tb.NewRow();
                            dr["visa_id"] = row["visa_id"];
                            dr["supcust_no"] = row["supcust_no"];
                            dr["sup_name"] = row["sup_name"];
                            dr["voucher_no"] = row["voucher_no"];
                            dr["oper_date"] = row["oper_date"];
                            dr["pay_way"] = row1["pay_way"];
                            dr["pay_amount"] = row1["total_amount"];
                            tb.Rows.Add(dr);
                        }
                    }
                   
                }
                this.editGrid1.DataSource = tb;
                this.editGrid1.Refresh();
                
            }
        }

        private void txtCumulative_TextChanged(object sender, EventArgs e)
        {
           
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
                    //IBLL.ICusSettle bll = new BLL.CusSettle();
                    string is_cs = runType1 == 1 ? "C" : "S";
                    bll.NotCheckCollectionPay(sheet_no, Program.oper.oper_id,DateTime.Now);
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

        private void txtbank_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            var tb = editGrid1.DataSource;
            tb.Clear();
            editGrid1.Refresh();
        }
    }
}
