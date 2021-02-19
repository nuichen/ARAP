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
    public partial class frmApToAp : Form, IOrder
    {
        private IOrderList orderlist;
        private IOrderMerge ordermerge;
       
        IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
        public frmApToAp()
        {
           
            InitializeComponent();
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
            editGrid1.AddColumn("voucher_first", "业务类型", "", 90, 2, IvyTransFunction.all_type_str, false);
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
            editGrid1.SetTotalColumn("pay_amount,pay_free");
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
                        //BLL.CusFY bll = new BLL.CusFY();
                        //var cus = bll.GetCusList();
                        //var cus= paymentbll.GetSupcustList("S");
                        IBLL.ISup bll = new BLL.SupBLL();
                        int tmp;
                        this.txtcus.Invoke((MethodInvoker)delegate
                        {
                            this.txtcus.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150", "supcust_no/sup_name->Text");
                        });
                        //var sup = paymentbll.GetSupcustList("S");
                        this.txtcus.Invoke((MethodInvoker)delegate
                        {
                            this.txtsup.Bind(bll.GetDataTable("", "", 1, 1, 20000, out tmp), 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150", "supcust_no/sup_name->Text");
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
                    throw new Exception("转入供应商必填!");
                }
                if (txtsup.Text.Trim().Contains("/") == false)
                {
                    throw new Exception("转出供应商必填!");
                }
                if (txtoper_date.Text.Trim() == "")
                {
                    throw new Exception("单据日期必填!");
                }
                if (Conv.ToDecimal(txt_to_amount.Text) == 0)
                {
                    throw new Exception("对冲金额不能为0!");
                }
                rp_t_arap_transformation ord = new rp_t_arap_transformation();
                ord.sheet_no = txtsheet_no.Text.Trim();
                ord.supcust_from = txtcus.Text.Split('/')[0];
                ord.supcust_to = txtsup.Text.Split('/')[0];
                ord.oper_id = txtoper_man.Text.Trim().Split('/')[0];
                ord.oper_date = Helper.Conv.ToDateTime(txtoper_date.Text.Trim());
                ord.sheet_id = "PR";
                ord.approve_man = "";
                //ord.approve_date = DateTime.MinValue;
                ord.update_time = DateTime.Now;
                ord.approve_flag = "0";
                ord.remark = txtmemo.Text;
                ord.total_amount = Conv.ToDecimal(txt_to_amount.Text);
                string sheet_no = "";
                if (runType == 1)
                {
                    
                    paymentbll.AddArAp(ord, out sheet_no);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                }
                else if (runType == 2)
                {
                    paymentbll.ChangeArAp(ord,out sheet_no);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
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

                txtcus.Text = "";
                //this.txtbranch.GetDefaultValue();
                this.txtsheet_no.Text = "";
                this.txtoper_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                this.txtsup.GetDefaultValue();
                this.txtapprove_man.Text = "";
                this.txtoper_man.Text = Program.oper.oper_id + "/" + Program.oper.oper_name;
                this.txtmemo.Text = "";
                this.txtapprove_date.Text = "";
                txtsup.Text = "";
                //txttotal_amount.Text = "0.00";
                txt_to_amount.Text = "0.00";
                //if (pay_dt!=null)
                //pay_dt.Clear();
                ////txttotal_amount.Text = "0.00";

                ////
                //var tb = editGrid1.DataSource;
                //tb.Clear();
                //pay_dt = paymentbll.GetPaymentList();
                //editGrid1.Refresh();

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
                //DataTable tb2;
                //string is_cs = "C";
                paymentbll.GetArApOrder(sheet_no, out tb1);
                //IBLL.ISCPaymentBLL scbll = new BLL.SCPaymentBLL();
                //var dt1 =paymentbll.GetArApList(sheet_no);
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
                if (r1["supcust_from"].ToString() == "")
                {
                    txtcus.Text = "";
                }
                else
                {
                    txtcus.Text = r1["supcust_from"].ToString() + "/" + r1["supcust_from_name"].ToString();
                }
                if (r1["supcust_to"].ToString() == "")
                {
                    txtsup.Text = "";
                }
                else
                {
                    txtsup.Text = r1["supcust_to"].ToString() + "/" + r1["supcust_to_name"].ToString();
                }
                txtsheet_no.Text = r1["sheet_no"].ToString();
                txtoper_date.Text = Helper.Conv.ToDateTime(r1["oper_date"].ToString()).ToString("yyyy-MM-dd");
                //txtThisSurplus.Text = Helper.Conv.ToString(r1["num2"]);
                //if (r1["deal_man"].ToString() == "")
                //{
                //    txtsup.Text = "";
                //}
                //else
                //{
                //    txtsup.Text = r1["deal_man"].ToString() + "/" + r1["deal_man_name"].ToString();
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
                txtmemo.Text = r1["remark"].ToString();
                DateTime dt;
                if (DateTime.TryParse(r1["approve_date"].ToString(), out dt) == true)
                {
                    txtapprove_date.Text = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtapprove_date.Text = "";
                }
                //txttotal_amount.Text = Helper.Conv.ToDecimal(r1["total_amount"].ToString()).ToString("0.00");
                txt_to_amount.Text = Helper.Conv.ToDecimal(r1["total_amount"].ToString()).ToString("0.00");
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
               // editGrid1.DataSource = tb2;


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
                        //IBLL.ICusSettle bll = new BLL.CusSettle();

                        //bll.Delete(sheet_no);
                        paymentbll.DeleteArAp(sheet_no,DateTime.Now);
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
                    string approve_man = txtoper_man.Text.Trim().Split('/')[0];
                    paymentbll.CheckArAp(sheet_no, approve_man, DateTime.Now);
                    //IBLL.ICusSettle bll = new BLL.CusSettle();
                    //string is_cs = "C";
                    //bll.Check(sheet_no, Program.oper.oper_id, is_cs);
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
                    row["pay_amount"] = Conv.ToDecimal(row["yf_amount"]);
                    row["pay_free"] = 0.00;
                }
                else
                {
                    row["select_flag"] = "0";
                    row["pay_amount"] = 0.00;
                    row["pay_free"] = 0.00;
                }

                var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                {
                    int path = 1;

                    //if ("-".Equals(r.Field<object>("path").ToString()) || "-1".Equals(r.Field<object>("path").ToString()))
                    //{
                    //    path = -1;
                    //}
                    return Conv.ToDecimal(r.Field<object>("pay_amount")) * path;
                });
                //this.txttotal_amount.Text = pay_amount.ToString("0.00");

                pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
                {
                    int path = 1;

                    //if ("-".Equals(r.Field<object>("path").ToString()) || "-1".Equals(r.Field<object>("path").ToString()))
                    //{
                    //    path = -1;
                    //}
                    return Conv.ToDecimal(r.Field<object>("pay_free")) * path;
                });
                this.txt_to_amount.Text = pay_amount.ToString("0.00");

                this.editGrid1.DataSource = this.editGrid1.DataSource;
                this.editGrid1.Refresh();

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
                if (row["sheet_amount"].ToDecimal() > 0)
                {
                    if (row["pay_amount"].ToDecimal() < 0)
                    {
                        MsgForm.ShowFrom("当金额>0时，本次核销金额必须>=0");
                        row["pay_amount"] = 0.00;
                    }
                    if(row["paid_amount"].ToDecimal()+ row["pay_amount"].ToDecimal()>row["sheet_amount"].ToDecimal())
                    {
                        MsgForm.ShowFrom("当金额<0时，本次核销金额必须<=0，金额必须<=已核销金额+本次核销金额");
                        row["pay_amount"] = 0.00;
                    }
                }
                else if(row["sheet_amount"].ToDecimal() < 0){
                    if (row["pay_amount"].ToDecimal() > 0)
                    {
                        MsgForm.ShowFrom("当金额<0时，本次核销金额必须<=0");
                        row["pay_amount"] = 0.00;
                    }
                    if (row["paid_amount"].ToDecimal() + row["pay_amount"].ToDecimal() < row["sheet_amount"].ToDecimal())
                    {
                        MsgForm.ShowFrom("当金额<0时，本次核销金额必须<=0，金额必须<=已核销金额+本次核销金额");
                        row["pay_amount"] = 0.00;
                    }

                }else
                {
                    if (row["pay_amount"].ToDecimal()!= 0)
                    {
                        MsgForm.ShowFrom("当金额=0时，本次核销金额必须=0");
                        row["pay_amount"] = 0.00;
                    }
                }
                if (row["pay_amount"].ToDecimal() >= row["yf_amount"].ToDecimal())
                {
                    row["pay_amount"] = row["yf_amount"].ToDecimal();
                    row["num1"] = 0;
                }
                else
                {
                    row["num1"] = row["yf_amount"].ToDecimal() - row["pay_amount"].ToDecimal(); 
                }

             }

            //DataTable dt = editGrid1.DataSource;
            //decimal cum = Conv.ToDecimal(txtCumulative.Text);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (cum >= Conv.ToDecimal(dt.Rows[i]["yf_amount"]))
            //    {
            //        //dt.Rows[i]["pay_amount"] = dt.Rows[i]["yf_amount"];
            //        //dt.Rows[i]["num1"] = 0;
            //        cum -= Conv.ToDecimal(dt.Rows[i]["pay_amount"]);

            //    }
            //    else if (cum > 0 && cum < Conv.ToDecimal(dt.Rows[i]["yf_amount"]))
            //    {
            //        //dt.Rows[i]["pay_amount"] = cum;
            //        //dt.Rows[i]["num1"] = Conv.ToDecimal(dt.Rows[i]["yf_amount"]) - cum;
            //        cum = 0;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //editGrid1.DataSource = dt;
            //txtThisSurplus.Text = Conv.ToString(cum);
            //editGrid1.AddColumn("yf_amount", "待核销金额", "", 100, 3, "0.00", false);
            //editGrid1.AddColumn("pay_amount", "本次核销金额", "", 120, 3, "0.00", true);
            //editGrid1.AddColumn("num1", "未核销金额", "", 100, 3, "0.00", false);
            //if (row["select_flag"].ToDecimal() == 0)
            //{
            //    row["pay_amount"] = 0;
            //    row["pay_free"] = 0;
            //    return;
            //}
            //if (column_name.Equals("pay_amount"))
            //{
            //    if (row["pay_amount"].ToDecimal() >= row["yf_amount"].ToDecimal())
            //    {
            //        row["pay_amount"] = row["yf_amount"];
            //        row["pay_free"] = 0;
            //    }
            //    else
            //    {
            //        decimal max_pay_free = row["yf_amount"].ToDecimal() - row["pay_amount"].ToDecimal();
            //        if (max_pay_free <= row["pay_free"].ToDecimal())
            //        {
            //            row["pay_free"] = max_pay_free;
            //        }
            //    }

            //    var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
            //    {
            //        int path = 1;

            //        if ("-".Equals(r.Field<object>("path")) || "-1".Equals(r.Field<object>("path")))
            //        {
            //            path = -1;
            //        }
            //        return Conv.ToDecimal(r.Field<object>("pay_amount")) * path;
            //    });
            //    this.txttotal_amount.Text = pay_amount.ToString("0.00");
            //}
            //else if (column_name.Equals("pay_free"))
            //{
            //    if (row["pay_free"].ToDecimal() >= row["yf_amount"].ToDecimal())
            //    {
            //        row["pay_free"] = row["yf_amount"];
            //        row["pay_amount"] = 0;
            //    }
            //    else
            //    {
            //        decimal max_pay_amount = row["yf_amount"].ToDecimal() - row["pay_free"].ToDecimal();
            //        if (max_pay_amount <= row["pay_amount"].ToDecimal())
            //        {
            //            row["pay_amount"] = max_pay_amount;
            //        }
            //    }

            //    var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum((r) =>
            //     {
            //         int path = 1;

            //         if ("-".Equals(r.Field<object>("path")) || "-1".Equals(r.Field<object>("path")))
            //         {
            //             path = -1;
            //         }
            //         return Conv.ToDecimal(r.Field<object>("pay_free")) * path;
            //     });
            //    this.txtfree_money.Text = pay_amount.ToString("0.00");
            //}
        }

        string select_flag = "1";
        private void editGrid1_DoubleClick(object sender, EventArgs e)
        {
            foreach (DataRow dr in this.editGrid1.DataSource.Rows)
            {
                if ("0".Equals(select_flag))
                {
                    dr["select_flag"] = "1";
                    dr["pay_amount"] = Conv.ToDecimal(dr["yf_amount"]);
                }
                else
                {
                    dr["select_flag"] = "0";
                    dr["pay_amount"] = 0.00;
                }
            }
            select_flag = "0".Equals(select_flag) ? "1" : "0";

            var pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum(r => Conv.ToDecimal(r.Field<object>("path")) * Conv.ToDecimal(r.Field<object>("pay_amount")));
            //this.txttotal_amount.Text = pay_amount.ToString("0.00");

            pay_amount = this.editGrid1.DataSource.AsEnumerable().Sum(r => Conv.ToDecimal(r.Field<object>("pay_free")));
            this.txt_to_amount.Text = pay_amount.ToString("0.00");

            this.editGrid1.DataSource = this.editGrid1.DataSource;
            this.editGrid1.Refresh();
        }

        public string method_id = "ap011";
        public void GetPrintTb()
        {
            //if (string.IsNullOrEmpty(this.txtsheet_no.Text))
            //    throw new Exception("请选择单据!");

            //IBLL.ICusSettle bll = new BLL.CusSettle();
            DataTable tb1;
            //DataTable tb2 = new DataTable();
            //tb2.Columns.Add("null");
            //string is_cs = "C";
            paymentbll.GetArApOrder(this.txtsheet_no.Text, out tb1);

            DataTable print_tb_m = new DataTable();
            //DataTable print_tb_d = new DataTable();
            //表头

            {
                Dictionary<string, string> file_dic = new Dictionary<string, string>()
                {
                    {"sheet_no","单据号"},
                    {"supcust_from","转入供应商编号"},
                    {"supcust_from_name","转入供应商名称"},
                    {"supcust_to","转出供应商编号"},
                    {"supcust_to_name","转出供应商名称"},
                    {"total_amount","对冲金额"},
                    {"oper_id","操作人编号"},
                    {"oper_name","制单人"},
                    {"oper_date","操作日期"},
                    {"approve_man","审核人编码"},
                    {"approve_man_name","审核人"},
                    {"remark","备注"},
                };

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
            //{
            //    Dictionary<string, string> file_dic = new Dictionary<string, string>()
            //    {
            //        //{"sheet_no","业务单号"},
            //        //{"path","方向"},
            //        //{"voucher_first","业务类型"},
            //        //{"sheet_amount","单据金额"},
            //        //{"paid_amount","已付金额"},
            //        //{"paid_free","已免付金额"},
            //        //{"yf_amount","应付金额"},
            //        //{"pay_amount","本次付款金额"},
            //        //{"pay_free","免付金额"},
            //        //{"pay_date","限付日期"},
            //        //{"memo","备注"},
            //        //{"voucher_type","业务描述"},
            //    };

            //    foreach (var k in file_dic.Keys)
            //    {
            //        var c = file_dic[k];
            //        if (tb2.Columns.Contains(k))
            //        {
            //            print_tb_d.Columns.Add(c, tb2.Columns[k].DataType);
            //        }
            //        else
            //        {
            //            print_tb_d.Columns.Add(c);
            //        }
            //    }
            //    foreach (DataRow dr in tb2.Rows)
            //    {
            //        DataRow row = print_tb_d.NewRow();
            //        foreach (var key in file_dic.Keys)
            //        {
            //            row[file_dic[key]] = dr[key];
            //        }
            //        print_tb_d.Rows.Add(row);
            //    }

            //}


            PrintForm.PrintHelper.tb_main = print_tb_m;
            //PrintForm.PrintHelper.tb_detail = print_tb_d;
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
        private void txtcus_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
           string supcust_from= txtcus.Text.Split('/')[0];
            string supcust_to = txtsup.Text.Split('/')[0];
            if (supcust_from == supcust_to)
            {
                MsgForm.ShowFrom("供应商不能一样");
                txtcus.Text = "";
            }

        }

        private void txt_to_amount_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {

        }

        private void txtsup_ClickCellAfter(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            string supcust_from = txtcus.Text.Split('/')[0];
            string supcust_to = txtsup.Text.Split('/')[0];
            if (supcust_from == supcust_to)
            {
                MsgForm.ShowFrom("供应商不能一样");
                txtsup.Text = "";
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
                    string approve_man = txtoper_man.Text.Trim().Split('/')[0];
                    paymentbll.NotCheckArAp(sheet_no, approve_man, DateTime.Now);
                    //IBLL.ICusSettle bll = new BLL.CusSettle();
                    //string is_cs = "C";
                    //bll.Check(sheet_no, Program.oper.oper_id, is_cs);
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
    }
}
