using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using System.Reflection;
using IvyBack.cons;
using System.Threading;
using System.IO;
using IvyBack.BLL;

namespace IvyBack.ReportForm
{
    public partial class frmBaseReport : Form
    {
        public frmBaseReport()
        {
            InitializeComponent();

            this.dgv.DataSource = new DataTable();
            reportBLL = new frmReportBLL(this);

            page.PageSize = 999999999;
        }

        protected Page<object> page = new Page<object>();
        protected BLL.ReportBLL bll = new BLL.ReportBLL();
        protected frmReportBLL reportBLL;
        protected string key_mod = "";
        protected string select_info = "";
        protected string info = "";
        protected bool auto_gen_select = true;//自动生成查询按钮
        protected Action AfterSelect;//查询之后引发事件
        protected Dictionary<string, string> format_dic = new Dictionary<string, string>();
        protected Dictionary<string, Control> con_dic = new Dictionary<string, Control>();
        //留
        protected void AddControl(int flp_width = 400, int dtp1_width = 170, int dtp2_width = 170)
        {
            AddControl(DateTime.Now.AddMonths(-1), DateTime.Now, flp_width, dtp1_width, dtp2_width);
        }
        //留
        protected void AddDateTime(int flp_width = 240, int dtp1_width = 95, int dtp2_width = 95)
        {
            Label lblTime = new Label();
            lblTime.Text = "截止日期:";
            lblTime.AutoSize = true;
            lblTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTime.Margin = new System.Windows.Forms.Padding(10, 7, 0, 3);
            //tablePanel.Controls.Add(lblTime, 0, 0);

            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Width = flp_width;
            flp.Height = 35;
            flp.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3); 

            DateTimePicker dtpStart = new DateTimePicker();
            dtpStart.Width = dtp1_width;
            dtpStart.Tag = "start_time";
            dtpStart.Margin = new System.Windows.Forms.Padding(0, 1, 0, 3);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd";
            dtpStart.Value = DateTime.Now.Toyyyy_MM_ddStart().ToDateTime();
            con_dic.Add("start_time", dtpStart);

            flp.Controls.AddRange(new Control[] { lblTime, dtpStart });
            tablePanel.Controls.Add(flp, 1, 0);
        }
        //留
        protected void AddControl(DateTime startTme, DateTime endTime, int flp_width = 240, int dtp1_width = 95, int dtp2_width = 95)
        {
            Label lblTime = new Label();
            lblTime.Text = "查询日期:";
            lblTime.AutoSize = true;
            lblTime.Margin = new System.Windows.Forms.Padding(10, 7, 0, 3);
            tablePanel.Controls.Add(lblTime, 0, 0);

            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Width = flp_width;
            flp.Height = 35;
            flp.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);

            Label lbl1 = new Label();
            lbl1.Text = "从";
            lbl1.AutoSize = true;
            lbl1.Margin = new System.Windows.Forms.Padding(0, 4, 0, 3);

            Label lbl2 = new Label();
            lbl2.Text = "到";
            lbl2.AutoSize = true;
            lbl2.Margin = new System.Windows.Forms.Padding(0, 4, 0, 3);

            DateTimePicker dtpStart = new DateTimePicker();
            dtpStart.Width = dtp1_width;
            dtpStart.Tag = "start_time";
            dtpStart.Margin = new System.Windows.Forms.Padding(0, 1, 0, 3);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpStart.Value = startTme.Toyyyy_MM_ddStart().ToDateTime();
            con_dic.Add("start_time", dtpStart);

            DateTimePicker dtpEnd = new DateTimePicker();
            dtpEnd.Width = dtp2_width;
            dtpEnd.Tag = "end_time";
            dtpEnd.Margin = new System.Windows.Forms.Padding(0, 1, 0, 3);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dtpEnd.Value = endTime.Toyyyy_MM_ddEnd().ToDateTime();
            con_dic.Add("end_time", dtpEnd);

            flp.Controls.AddRange(new Control[] { lbl1, dtpStart, lbl2, dtpEnd});

            tablePanel.Controls.Add(flp, 1, 0);
        }
        //留
        protected void TabPanel()
        {
            //加按钮
            Button btn = new Button();
            btn.Text = "查询";
            btn.Width = 70;
            btn.Margin = new System.Windows.Forms.Padding(20, 3, 0, 3);
            btn.Click += btnSelect_Click;

            AddLastControl(btn);
        }
        //留
        protected void AddSelectButton(int y, int x)
        {
            //加按钮
            Button btn = new Button();
            btn.Text = "查询";
            btn.Width = 70;
            btn.Margin = new System.Windows.Forms.Padding(20, 3, 0, 3);
            btn.Click += btnSelect_Click;

            tablePanel.Controls.Add(btn, y, x);
        }
        //留
        protected void AddLastControl(Control add_con)
        {
            int x = 0;
            int y = 0;
            int maxSize = 0;
            for (int i = tablePanel.RowCount - 1; i >= 0; i--)
            {
                bool flag = false;

                int row_width = 0;
                for (int j = tablePanel.ColumnCount - 1; j >= 0; j--)
                {
                    var con = tablePanel.GetControlFromPosition(j, i);
                    if (con != null)
                    {
                        int height = con.Height;
                        if (height > row_width)
                            row_width = height;
                        flag = true;
                    }
                    else
                    {
                        if (x == 0 && y == 0)
                        {
                            x = i;
                            y = j;
                        }
                    }


                }

                maxSize += row_width;

                if (!flag)
                {
                    x = 0;
                    y = 0;
                    tablePanel.RowStyles.RemoveAt(i);
                }
            }

            tablePanel.Height = maxSize;

            tablePanel.Controls.Add(add_con, y, x);
        }

        //查询按钮事件
        //留
        public void btnSelect_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Nick对了");
            if (!MyLove.PermissionsBalidation(this.Text, "18"))
            {
                return;
            }
            Thread th = new Thread(() =>
            {
                Helper.GlobalData.windows.ShowLoad(this);
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    this.info = select_info;
                    var t = bll.GetType();
                    MethodInfo method = t.GetMethod(key_mod);
                    ParameterInfo[] par = method.GetParameters();
                    Object[] obj_par = new Object[par.Length];

                    for (int i = 0; i < par.Length; i++)
                    {
                        ParameterInfo info = par[i];

                        if (info.ParameterType == page.GetType())
                        {
                            obj_par[i] = page;
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                obj_par[i] = GetValue(info.Name);
                            });
                        }
                    }
                    page = method.Invoke(bll, obj_par) as Page<object>;
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.dgv.DataSource = page.Tb;
                        this.lblIndex.Text = page.PageIndex.ToString();
                        this.lblMaxIndex.Text = page.PageMax.ToString();
                        this.lblSelectInfo.Text = this.info;

                        AfterSelect?.Invoke();
                    });
                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    LogHelper.writeLog("btnSelect_Click", ex.ToString());
                    MsgForm.ShowFrom(ex.GetMessage());
                }
                Helper.GlobalData.windows.CloseLoad(this);
            });
            th.Start();
        }
        //留
        protected object GetValue(string key)
        {
            Control c;
            object obj = "";
            if (con_dic.TryGetValue(key, out c))
            {
                if (c is MyTextBox)
                {
                    MyTextBox txt = c as MyTextBox;
                    info = info.Replace(key, txt.Text);
                    if (!string.IsNullOrEmpty(txt.Text))
                    {
                        obj = txt.Text.Split('/')[0];
                    }
                }
                else if (c is DateTimePicker)
                {
                    DateTimePicker dtp = c as DateTimePicker;
                    info = info.Replace(key, dtp.Value.ToString(format_dic[key]));
                    obj = dtp.Value;
                }
                else if (c is ComboBox)
                {
                    ComboBox cb = c as ComboBox;
                    info = info.Replace(key, cb.Text.ToString());
                    obj = cb.SelectedValue;
                }
                else if (c is TextBox)
                {
                    TextBox txt = c as TextBox;
                    info = info.Replace(key, txt.Text);
                    obj = txt.Text;
                }
                else if (c is NumericUpDown)
                {
                    NumericUpDown nud = c as NumericUpDown;
                    info = info.Replace(key, nud.Value.ToString());
                    obj = nud.Value.ToString();
                }
                else if (c is CheckBox)
                {
                    CheckBox cb = c as CheckBox;
                    info = info.Replace(key, cb.Checked.ToString());
                    obj = cb.Checked;
                }
            }

            //清除查询条件空条件
            string[] infos = info.Split(',');
            info = "";
            for (int i = 0; i < infos.Length; i++)
            {
                if (!infos[i].Contains("[]"))
                    info += infos[i];
                if (i < infos.Length - 1 && !infos[i + 1].Contains("[]"))
                    info += ",";
            }
            if (string.IsNullOrEmpty(info))
                info = "查询条件:";

            return obj;
        }
        //留
        private void frmReport_Shown(object sender, EventArgs e)
        {
            if (auto_gen_select)
            {
                TabPanel();
            }
            Helper.GlobalData.InitForm(this);
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            page.HomPage();
            btnSelect_Click(sender, e);
        }
        private void btnPre_Click(object sender, EventArgs e)
        {
            page.PrePage();
            btnSelect_Click(sender, e);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            page.NextPage();
            btnSelect_Click(sender, e);
        }
        private void btnTra_Click(object sender, EventArgs e)
        {
            page.TraPage();
            btnSelect_Click(sender, e);
        }
        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        //protected void BranchControl(int column, int row, int width = 150)
        //{
        //    Label lbl = new Label();
        //    lbl.Text = "机构仓库:";
        //    lbl.AutoSize = true;
        //    lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl, column, row);

        //    cons.MyTextBox txt = new cons.MyTextBox();
        //    txt.Tag = "branch_no";
        //    txt.Text = "/全部";
        //    txt.Width = width;
        //    txt.BorderStyle = BorderStyle.Fixed3D;
        //    txt.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add("branch_no", txt);

        //    IBLL.IBranch branch_bll = new BLL.BranchBLL();
        //    var tb = branch_bll.GetAllList("");
        //    DataRow dr = tb.NewRow();
        //    dr["branch_no"] = "";
        //    dr["branch_name"] = "全部";
        //    tb.Rows.InsertAt(dr, 0);
        //    txt.Bind(tb, 230, 200, "branch_no", "branch_no:仓库编号:80,branch_name:仓库:150", "branch_no/branch_name->Text");
        //    tablePanel.Controls.Add(txt, column + 1, row);
        //}
        //protected void SheetControl(int column, int row)
        //{
        //    Label lbl2 = new Label();
        //    lbl2.Text = "单据号:";
        //    lbl2.AutoSize = true;
        //    lbl2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl2.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl2, column, row);

        //    TextBox txt1 = new TextBox();
        //    txt1.Tag = "sheet_no";
        //    txt1.Width = 150;
        //    txt1.BorderStyle = BorderStyle.Fixed3D;
        //    txt1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add("sheet_no", txt1);
        //    tablePanel.Controls.Add(txt1, column + 1, row);
        //}
        //留
        protected void SupControl(int column, int row, string tag = "supcust_no", int width = 150)
        {
            Label lbl1 = new Label();
            lbl1.Text = "供应商:";
            lbl1.AutoSize = true;
            lbl1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl1.Margin = new System.Windows.Forms.Padding(5, 6, 0, 0);
            tablePanel.Controls.Add(lbl1, column, row);

            cons.MyTextBox txtsup = new cons.MyTextBox();
            txtsup.Tag = tag;
            txtsup.Width = width;
            txtsup.BorderStyle = BorderStyle.Fixed3D;
            txtsup.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add(tag, txtsup);

            IBLL.ISup sup_bll = new BLL.SupBLL();
            int total_count = 0;
            var tb = sup_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr = tb.NewRow();
            dr["supcust_no"] = "";
            dr["sup_name"] = "全部";
            tb.Rows.InsertAt(dr, 0);
            txtsup.Bind(tb, 230, 200, "supcust_no", "supcust_no:编号:80,sup_name:供应商:150", "supcust_no/sup_name->Text");
            tablePanel.Controls.Add(txtsup, column + 1, row);
        }
        //留
        protected void CusControl(int column, int row, string tag = "cust_no", int width = 150)
        {
            Label lblCus = new Label();
            lblCus.Text = "客户:";
            lblCus.AutoSize = true;
            lblCus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCus.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus, column, row);

            cons.MyTextBox txtCus = new cons.MyTextBox();
            txtCus.Tag = tag;
            txtCus.Width = width;
            txtCus.BorderStyle = BorderStyle.Fixed3D;
            txtCus.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add(tag, txtCus);

            IBLL.ICus cus_bll = new BLL.CusBLL();
            int total_count;
            var tb = cus_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr = tb.NewRow();
            dr["supcust_no"] = "";
            dr["sup_name"] = "全部";
            tb.Rows.InsertAt(dr, 0);
            txtCus.Bind(tb, 230, 200, "supcust_no", "supcust_no:客户编号:80,sup_name:客户:150", "supcust_no/sup_name->Text");
            tablePanel.Controls.Add(txtCus, column + 1, row);
        }


        //protected void ItemClsControl(int column, int row)
        //{
        //    Label lbl = new Label();
        //    lbl.Text = "商品类别:";
        //    lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl.AutoSize = true;
        //    lbl.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl, column, row);

        //    cons.MyTextBox txt = new cons.MyTextBox();
        //    txt.Tag = "item_clsno";
        //    txt.Width = 150;
        //    txt.BorderStyle = BorderStyle.Fixed3D;
        //    txt.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add("item_clsno", txt);

        //    IBLL.IItemCls cls_bll = new BLL.ItemClsBLL();
        //    var tb = cls_bll.GetDataTable();
        //    DataRow dr = tb.NewRow();
        //    dr["item_clsno"] = "";
        //    dr["item_clsname"] = "全部";
        //    tb.Rows.InsertAt(dr, 0);
        //    txt.Bind(tb, 230, 200, "item_clsno", "item_clsno:仓库编号:80,item_clsname:仓库:150", "item_clsno/item_clsname->Text");
        //    tablePanel.Controls.Add(txt, column + 1, row);
        //}
        //protected void ItemNameControl(int column, int row)
        //{
        //    Label lbl1 = new Label();
        //    lbl1.Text = "商品名称:";
        //    lbl1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl1.AutoSize = true;
        //    lbl1.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl1, column, row);

        //    TextBox txt1 = new TextBox();
        //    txt1.Tag = "item_name";
        //    txt1.Width = 150;
        //    txt1.BorderStyle = BorderStyle.Fixed3D;
        //    txt1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add("item_name", txt1);
        //    tablePanel.Controls.Add(txt1, column + 1, row);
        //}
        //protected void BarcodeControl(int column, int row)
        //{
        //    Label lbl4 = new Label();
        //    lbl4.Text = "条码:";
        //    lbl4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl4.AutoSize = true;
        //    lbl4.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl4, column, row);

        //    TextBox txt4 = new TextBox();
        //    txt4.Tag = "barcode";
        //    txt4.Width = 150;
        //    txt4.BorderStyle = BorderStyle.Fixed3D;
        //    txt4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add("barcode", txt4);
        //    tablePanel.Controls.Add(txt4, column + 1, row);
        //}
        //protected void TextControl(int column, int row, string text, string tag, int width = 150)
        //{
        //    Label lbl1 = new Label();
        //    lbl1.Text = text;
        //    lbl1.AutoSize = true;
        //    lbl1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl1.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl1, column, row);

        //    TextBox txt1 = new TextBox();
        //    txt1.Tag = tag;
        //    txt1.Width = width;
        //    txt1.BorderStyle = BorderStyle.Fixed3D;
        //    txt1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add(tag, txt1);
        //    tablePanel.Controls.Add(txt1, column + 1, row);
        //}
        protected void ComboControl(int column, int row, string text, string tag, object dataSource)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.AutoSize = true;
            lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lbl, column, row);

            ComboBox cbType = new ComboBox();
            cbType.Tag = tag;
            cbType.Width = 150;
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add(tag, cbType);
            cbType.DisplayMember = "Value";
            cbType.ValueMember = "Key";
            cbType.DataSource = dataSource;
            tablePanel.Controls.Add(cbType, column + 1, row);
        }
        //protected void PeopleControl(int column, int row, string tag = "deal_man")
        //{
        //    Label lbl = new Label();
        //    lbl.Text = "经办人:";
        //    lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl.AutoSize = true;
        //    lbl.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl, column, row);

        //    cons.MyTextBox txt = new cons.MyTextBox();
        //    txt.Tag = tag;
        //    txt.Width = 150;
        //    txt.BorderStyle = BorderStyle.Fixed3D;
        //    txt.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add(tag, txt);

        //    IBLL.IPeople bll = new BLL.PeopleBLL();
        //    int total_count;
        //    var tb = bll.GetDataTable("", "", 1, 1, 999999, out total_count);
        //    DataRow dr = tb.NewRow();
        //    dr["oper_id"] = "";
        //    dr["oper_name"] = "全部";
        //    tb.Rows.InsertAt(dr, 0);
        //    txt.Bind(tb, 230, 200, "oper_id", "oper_id:员工编号:80,oper_name:员工:150", "oper_id/oper_name->Text");
        //    tablePanel.Controls.Add(txt, column + 1, row);
        //}
        //protected void WayTypeControl(int column, int row, string pay_flag, string tag = "kk_no", int width = 150)
        //{
        //    Label lbl = new Label();
        //    lbl.Text = "费用类型:";
        //    lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl.AutoSize = true;
        //    lbl.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl, column, row);

        //    cons.MyTextBox txt = new cons.MyTextBox();
        //    txt.Tag = tag;
        //    txt.Width = width;
        //    txt.BorderStyle = BorderStyle.Fixed3D;
        //    txt.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add(tag, txt);

        //    IBLL.IFinanceBLL bll = new BLL.FinanceBLL();
        //    var tb = bll.GetSZTypeList();

        //    var t = tb.Select().Where(d => pay_flag.Contains(d["pay_flag"].ToString()));
        //    if (t != null && t.Count() > 0)
        //        tb = t.CopyToDataTable();

        //    txt.Bind(tb, 230, 200, "pay_way", "pay_way:费用编号:80,pay_name:费用类别:150", "pay_way/pay_name->Text");
        //    tablePanel.Controls.Add(txt, column + 1, row);
        //}
        //protected void PhSheetNoControl(int column, int row, string tag = "ph_sheet_no")
        //{
        //    Label lbl = new Label();
        //    lbl.Text = "批次号:";
        //    lbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        //    lbl.AutoSize = true;
        //    lbl.Margin = new System.Windows.Forms.Padding(10, 6, 0, 3);
        //    tablePanel.Controls.Add(lbl, column, row);

        //    cons.MyTextBox txt = new cons.MyTextBox();
        //    txt.Tag = tag;
        //    txt.Tag = tag;
        //    txt.Width = 200;
        //    txt.BorderStyle = BorderStyle.Fixed3D;
        //    txt.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
        //    con_dic.Add(tag, txt);

        //    IBLL.IOrderBLL bll = new OrderBLL();
        //    DataTable tb = bll.GetPHSheets(DateTime.Now.AddMonths(-1), DateTime.Now);
        //    DataRow dr = tb.NewRow();
        //    dr["sheet_no"] = "";
        //    dr["ms_other1"] = "全部";
        //    tb.Rows.InsertAt(dr, 0);
        //    txt.Bind(tb, 350, 200, "sheet_no", "sheet_no:批次号:120,ms_other1:备注:150", "sheet_no->Text");
        //    if (tb.Rows.Count > 0)
        //    {
        //        txt.GetValue("sheet_no", tb.Rows[0]["sheet_no"].ToString());
        //    }
        //    tablePanel.Controls.Add(txt, column + 1, row);
        //}


        //打印
        public string method_id { get; set; }
        public void GetPrintTb()
        {
            //if (this.dgv.DataSource.Rows.Count < 1)
            //{
            //    throw new Exception("表格中没有数据!");
            //}

            PrintForm.PrintHelper.tb_main = null;
            PrintForm.PrintHelper.tb_detail = this.dgv.DataSource.Copy();
        }
        private void tsmiPrintStyle_Click(object sender, EventArgs e)
        {
            try
            {
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
                GetPrintTb();
                Model.sys_t_print_style_default styleDefault;
                if (PrintForm.PrintHelper.myDefault.TryGetValue(method_id, out styleDefault))
                {
                    string style_data = PrintForm.PrintHelper.myStyle[styleDefault.style_id].style_data;
                    global::PrintHelper.IBLL.IPrint print = new global::PrintHelper.BLL.PrintV();
                    print.Print(style_data,
                        PrintForm.PrintHelper.GetStyle(style_data),
                        PrintForm.PrintHelper.tb_main,
                        PrintForm.PrintHelper.tb_detail);
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
            if (!MyLove.PermissionsBalidation(this.Text, "07"))
            {

                return;
            }
            try
            {
                GetPrintTb();
                Model.sys_t_print_style_default styleDefault;
                if (PrintForm.PrintHelper.myDefault.TryGetValue(method_id, out styleDefault))
                {
                    string style_data = PrintForm.PrintHelper.myStyle[styleDefault.style_id].style_data;
                    global::PrintHelper.IBLL.IPrint print = new global::PrintHelper.BLL.PrintP();
                    print.Print(style_data,
                        PrintForm.PrintHelper.GetStyle(style_data),
                        PrintForm.PrintHelper.tb_main,
                        PrintForm.PrintHelper.tb_detail);
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

        //导出Excel
        protected void tsbExportExcel_Click(object sender, EventArgs e)
        {
            if (!MyLove.PermissionsBalidation(this.Text, "11"))
            {

                return;
            }
            try
            {
                if (this.dgv.DataSource == null || this.dgv.DataSource.Rows.Count < 1)
                {
                    throw new Exception("表格中没有数据");
                }

                string name = DateTime.Now.ToString("yyyy-MM-dd HHmmss") + "  " + this.Text;
                reportBLL.ExportExcel(name, page.Tb);
            }
            catch (Exception ex)
            {
                MsgForm.ShowFrom(ex);
            }

        }
    }
}
