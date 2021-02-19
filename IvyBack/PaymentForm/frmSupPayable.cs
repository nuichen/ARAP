using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.cons;
using IvyBack.Helper;

namespace IvyBack.VoucherForm
{
    public partial class frmSupPayable : Form, IOrder
    {
        private IOrderList orderlist;
        private IOrderMerge ordermerge;
        WindowsList windowsList;
        public frmSupPayable(WindowsList windowsList)
        {
            this.windowsList = windowsList;
            InitializeComponent();
            //
            Helper.GlobalData.InitForm(this);
            //
            var tb = new DataTable();
            tb.Columns.Add("kk_no");
            tb.Columns.Add("kk_name");
            tb.Columns.Add("kk_cash");
            tb.Columns.Add("pay_kind");
            tb.Columns.Add("other1");

            editGrid1.AddColumn("kk_no", "代码", "", 90, 1, "", true);
            editGrid1.AddColumn("kk_name", "名称", "", 140, 1, "", false);
            //editGrid1.AddColumn("pay_kind", "方向", "", 75, 2, "{0:借,1:贷,2:平}", false);
            editGrid1.AddColumn("kk_cash", "金额", "", 100, 3, "0.00", true);
            editGrid1.AddColumn("other1", "备注", "", 250, 1, "", true);
            editGrid1.DataSource = tb;
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
                        BLL.SupFY bll = new BLL.SupFY();

                        var editgrid = bll.GetSZList();
                        this.editGrid1.Invoke((MethodInvoker)delegate
                        {
                            var edgrid = editgrid.Copy();
                            //foreach (DataRow dr in edgrid.Rows)
                            //{
                            //    if (dr["pay_kind"].ToInt32() == 0)
                            //    {
                            //        dr["pay_kind"] = "借";
                            //    }
                            //    else if (dr["pay_kind"].ToInt32() == 1)
                            //        dr["pay_kind"] = "贷";
                            //    else
                            //        dr["pay_kind"] = "平";
                            //    dr["pay_kind"] = dr["pay_kind"].ToInt32() == 0 ? "-" : "+";
                            //}
                            editGrid1.Bind("kk_no", edgrid, 400, 200, "pay_way", "pay_way:代码:80,pay_name:名称:150", "pay_way->kk_no,pay_name->kk_name");
                        });

                        var cus = bll.GetSupList();
                        this.txtcus.Invoke((MethodInvoker)delegate
                        {
                            this.txtcus.Bind(cus, 300, 200, "supcust_no", "supcust_no:供应商编码:100,sup_name:供应商名称:150", "supcust_no/sup_name->Text");
                        });

                        var visa = bll.GetBankList();
                        this.txtvisa.Invoke((MethodInvoker)delegate
                        {
                            txtvisa.Bind(visa, 300, 200, "visa_id", "visa_id:编号:100,visa_nm:名称:130", "visa_id/visa_nm->Text");
                            txtvisa.GetDefaultValue();
                        });

                        var branch = bll.GetBranchList();
                        this.txtbranch.Invoke((MethodInvoker)delegate
                        {
                            txtbranch.Bind(branch, 300, 200, "branch_no", "branch_no:机构号:80,branch_name:机构名:140", "branch_no/branch_name->Text");
                        });

                        IBLL.IPeople peopleBLL = new BLL.PeopleBLL();
                        int tmp;
                        var people = peopleBLL.GetDataTable("", "", 1, 1, 20000, out tmp);

                        this.txtpeople.Invoke((MethodInvoker)delegate
                        {
                            txtpeople.Bind(people, 250, 200, "oper_id", "oper_id:职员编号:80,oper_name:姓名:80", "oper_id/oper_name->Text");
                           // txtpeople.GetDefaultValue();
                        });

                        this.Invoke((MethodInvoker)delegate
                        {
                            IOrder ins = this;
                            ins.Add();
                        });
                    }
                    catch (Exception ex)
                    {
                        IvyBack.Helper.LogHelper.writeLog("frmSupFY", ex.ToString());
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
                if (txtbranch.Text.Trim().Contains("/") == false)
                {
                    throw new Exception("机构必填!");
                }
                if (txtoper_date.Text.Trim() == "")
                {
                    throw new Exception("单据日期必填!");
                }
                this.editGrid1.DataSource.ClearTable("kk_no");
                if (this.editGrid1.DataSource.Rows.Count > 1)
                {
                    throw new Exception("只允许录入一行!");
                }

                Model.rp_t_supcust_fy_master ord = new Model.rp_t_supcust_fy_master();
                ord.sheet_no = txtsheet_no.Text.Trim();
                ord.supcust_no = txtcus.Text.Trim().Split('/')[0];
                ord.supcust_flag = "S";
                ord.pay_date = System.DateTime.Now;
                ord.old_no = txtold_no.Text.Trim();
                ord.oper_id = this.txtoper_man.Text.Trim().Split('/')[0];
                ord.oper_date = Helper.Conv.ToDateTime(this.txtoper_date.Text);
                ord.approve_flag = "0";
                ord.approve_man = "";
                //ord.approve_date = System.DateTime.MinValue;
                ord.is_payed = "0";
                ord.sale_man = txtpeople.Text.Trim().Split('/')[0];
                ord.branch_no = txtbranch.Text.Trim().Split('/')[0];
                ord.cm_branch = "00";
                ord.other1 = txtmemo.Text.Trim();
                ord.other2 = "";
                ord.other3 = "";
                ord.num1 = 0;
                ord.num2 = 0;
                ord.num3 = 0;
                ord.visa_id = txtvisa.Text.Trim().Split('/')[0];
                ord.is_over = "0";
                //ord.total_amount = 0;
                ord.paid_amount = 0;
                ord.pay_way = "";
                ord.pay_name = "";
                int flag = 0;
                List<Model.rp_t_supcust_fy_detail> lines = new List<Model.rp_t_supcust_fy_detail>();
                int index = 0;
                decimal num = 0;
                //int i = 0;
                foreach (DataRow row in editGrid1.DataSource.Rows)
                {
                    index++;
                    //if (i == 1)
                    //{
                    //    if (Conv.ToString(row["kk_no"]).Trim() != "")
                    //    {
                    //        MsgForm.ShowFrom("一次只能录入一行数据，我们仅保存第一行的数据，后面的数据请新建后录入！");
                    //    }
                    //        break;
                    //}
                    if (row["kk_no"].ToString() != "")
                    {
                        //if (Helper.Conv.ToDecimal(row["kk_cash"].ToString()) <= 0)
                        //{
                        //    throw new Exception("第i行费用金额不正确！".Replace("i", index.ToString()));
                        //}
                        if (Helper.Conv.ToDecimal(row["kk_cash"].ToString()) == 0)
                        {
                            MsgForm.ShowFrom("金额不能为0");
                            return;
                        }
                        Model.rp_t_supcust_fy_detail line = new Model.rp_t_supcust_fy_detail();
                        lines.Add(line);
                        line.sheet_no = ord.sheet_no;
                        line.kk_no = row["kk_no"].ToString();
                        line.kk_cash = Helper.Conv.ToDecimal(row["kk_cash"].ToString());
                        line.other1 = row["other1"].ToString();
                        line.other2 = "";
                        line.other3 = "";
                        line.num1 = 0;//Helper.Conv.ToDecimal(row["pay_kind"].ToString());//存方向0代表借,1代表贷
                        line.num2 = 0;
                        line.num3 = 0;
                        num += line.kk_cash;
                        //if (row["pay_kind"].ToString() == "0" || row["pay_kind"].ToString() == "借")
                        //{
                        //    num += line.kk_cash;
                        //    line.num1 = 0;//存方向0代表借,1代表贷
                        //}
                        //else if (row["pay_kind"].ToString() == "1" || row["pay_kind"].ToString() == "贷")
                        //{
                        //    num -= line.kk_cash;
                        //    line.num1 = 1;//存方向0代表借,1代表贷
                        //}
                        flag = 1;
                    }
                    //i++;
                }
                if (num > 0)
                {
                    ord.total_amount = num;
                    ord.pay_type = "0";
                }
                else if (num < 0)
                {
                    ord.pay_type = "1";
                    ord.total_amount = 0 - num;
                }
                else
                {
                    ord.total_amount = 0;
                    ord.pay_type = "2";
                }
                if (flag == 0)
                {
                    throw new Exception("表体无合法数据！");
                }
                if (runType == 1)
                {
                    IBLL.ISupFY bll = new BLL.SupFY();
                    string sheet_no;
                    bll.Add(ord, lines, out sheet_no);
                    IOrder ins = this;
                    ins.ShowOrder(sheet_no);
                }
                else if (runType == 2)
                {
                    IBLL.ISupFY bll = new BLL.SupFY();
                    bll.Change(ord, lines);
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

                txtcus.Text = "";
                this.txtbranch.GetDefaultValue();
                this.txtsheet_no.Text = "";
                this.txtoper_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                this.txtpeople.Text = "";//GetDefaultValue();
                this.txtapprove_man.Text = "";
                this.txtoper_man.Text = Program.oper.oper_id + "/" + Program.oper.oper_name;
                this.txtmemo.Text = "";
                this.txtapprove_date.Text = "";
                this.txtold_no.Text = "";
                this.txtvisa.GetDefaultValue();
                //
                var tb = editGrid1.DataSource;
                tb.Clear();
                for (int i = 0; i < 1; i++)
                {
                    tb.Rows.Add(tb.NewRow());
                }
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

                IBLL.ISupFY bll = new BLL.SupFY();
                DataTable tb1;
                DataTable tb2;
                bll.GetOrder(sheet_no, out tb1, out tb2);
                //

                //
                var r1 = tb1.Rows[0];
                if (r1["sup_no"].ToString() == "")
                {
                    txtcus.Text = "";
                }
                else
                {
                    txtcus.Text = r1["sup_no"].ToString() + "/" + r1["sup_name"].ToString();
                }
                if (r1["branch_no"].ToString() == "")
                {
                    txtbranch.Text = "";
                }
                else
                {
                    txtbranch.Text = r1["branch_no"].ToString() + "/" + r1["branch_name"].ToString();
                }
                txtsheet_no.Text = r1["sheet_no"].ToString();
                txtoper_date.Text = Helper.Conv.ToDateTime(r1["oper_date"].ToString()).ToString("yyyy-MM-dd");
                if (r1["sale_man"].ToString() == "")
                {
                    txtpeople.Text = "";
                }
                else
                {
                    txtpeople.Text = r1["sale_man"].ToString() + "/" + r1["sale_man_name"].ToString();
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
                txtold_no.Text = r1["old_no"].ToString();
                if (r1["visa_id"].ToString() == "")
                {
                    txtvisa.Text = "";
                }
                else
                {
                    txtvisa.Text = r1["visa_id"].ToString() + "/" + r1["visa_name"].ToString();
                }
                //
                editGrid1.DataSource = tb2;

                Dictionary<string, object> dic = this.Tag as Dictionary<string, object>;
                this.Tag = Helper.Conv.ControlsAdds(this, dic);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
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
                        IBLL.ISupFY bll = new BLL.SupFY();
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
                    IBLL.ISupFY bll = new BLL.SupFY();
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

        public string method_id = "ap002";

        public void GetPrintTb()
        {
            //if (string.IsNullOrEmpty(this.txtsheet_no.Text))
            //    throw new Exception("请选择单据!");

            IBLL.ISupFY bll = new BLL.SupFY();
            DataTable tb1;
            DataTable tb2;
            bll.GetOrder(this.txtsheet_no.Text, out tb1, out tb2);

            DataTable print_tb_m = new DataTable();
            DataTable print_tb_d = new DataTable();
            //表头
            {
                Dictionary<string, string> file_dic = new Dictionary<string, string>()
                {
                    {"sheet_no","单据号"},
                    {"sup_no","供应商编码"},
                    {"sup_name","供应商名称"},
                    {"pay_date","支付日期"},
                    {"total_amount","金额"},
                    {"oper_id","操作员编号"},
                    {"oper_name","操作员"},
                    {"oper_date","操作日期"},
                    {"approve_man","审核人编号"},
                    {"approve_man_name","审核人"},
                    {"approve_date","审核日期"},
                    {"branch_no","仓库编码"},
                    {"branch_name","仓库名称"},
                    {"sale_man","经办人编号"},
                    {"sale_man_name","经办人"},
                    {"visa_id","账号id"},
                    {"visa_name","账号名称"},
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
            {
                Dictionary<string, string> file_dic = new Dictionary<string, string>()
                {
                    {"kk_no","费用代码"},
                    {"kk_name","费用名称"},
                    {"kk_cash","费用(金额)"},
                    {"other1","备注"},
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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtold_no_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var frm = new FinanceForm.frmIncome();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
            BLL.SupFY bll = new BLL.SupFY();

            var editgrid = bll.GetSZList();
            this.editGrid1.Invoke((MethodInvoker)delegate
            {
                var edgrid = editgrid.Copy();
                editGrid1.Bind("kk_no", edgrid, 400, 200, "pay_way", "pay_way:代码:80,pay_name:名称:150",
                "pay_way->kk_no,pay_name->kk_name");
            });
        }

        private void editGrid1_CellEndEdit(object sender, string column_name, DataRow row)
        {
            //if (Helper.Conv.ToDecimal(row["kk_cash"]) > 0)
            //{
            //    row["pay_kind"] = 1;

            //}
            //else if (Helper.Conv.ToDecimal(row["kk_cash"]) < 0)
            //{
            //    row["pay_kind"] = 0;
            //    row["kk_cash"] = Math.Abs(Helper.Conv.ToDecimal(row["kk_cash"]));
            //}
            //else
            //{
            //    row["pay_kind"] = 2;
            //}
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
                    if (this.txtapprove_man.Text=="")
                    {
                        MsgForm.ShowFrom("单据未审核");
                        return;
                    }
                    string sheet_no = txtsheet_no.Text.Trim();
                    IBLL.ISupFY bll = new BLL.SupFY();
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
