using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using IvyBack.Helper;
using IvyBack.PaymentForm;
using Model.StaticType;

namespace IvyBack.ReportForm
{
    public class frmFinanceReport : frmBaseReport
    {

        #region 应收应付
        /// <summary>
        /// 客户往来明细账
        /// </summary>
        public void GetCusContactDetails()
        {
            AddControl();
            CusControl(2, 0,"supcust_no");
            //
            dgv.AddColumn("年", "年", "", 50, 1, "");
            dgv.AddColumn("月", "月", "", 50, 1, "");
            dgv.AddColumn("日", "日", "", 50, 1, "");
            dgv.AddColumn("单据类型", "单据类型", "", 100, 1, IvyTransFunction.all_type_str);
            dgv.AddColumn("单据号", "单据号", "", 150, 1, "");
            dgv.AddColumn("摘要", "摘要", "", 100, 1, IvyTransFunction.all_type_str);
            dgv.AddColumn("借方", "借方", "", 100, 3, "0.00");
            dgv.AddColumn("贷方", "贷方", "", 100, 3, "0.00");
            dgv.AddColumn("方向", "方向", "", 50, 1, "");
            dgv.AddColumn("余额", "余额", "", 100, 3, "0.00");
           
            //dgv.SetTotalColumn("收款金额,免收款金额");
            this.key_mod = "GetCusContactDetails";
            this.Text = "客户往来明细账";
            select_info = "查询条件为: 查询时间从[start_time]到[end_time],客户:[supcust_no]";
            format_dic = new Dictionary<string, string>()
            {
                {"start_time","yyyy-MM-dd"},
                {"end_time","yyyy-MM-dd"},
            };
        }
        /// <summary>
        /// 客户往来余额表
        /// </summary>
        public void GetCusBalance()
        {
            AddControl();
            //CusControl(2, 0, "supcust_no");
            Label lblCus1 = new Label();
            lblCus1.Text = "客户:";
            lblCus1.AutoSize = true;
            lblCus1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCus1.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus1, 2, 0);
            cons.MyTextBox companyType = new cons.MyTextBox();
            cons.MyTextBox txtCus = new cons.MyTextBox();
            txtCus.Tag = "cust_from";
            txtCus.Width = 150;
            txtCus.BorderStyle = BorderStyle.Fixed3D;
            txtCus.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("cust_from", txtCus);
            txtCus.ClickCellAfter += (object sender, string column_name, DataRow row, MouseEventArgs e) =>
            {
                companyType.Text = "";
            };
            IBLL.ICus cus_bll = new BLL.CusBLL();
            int total_count;
            var tb3 = cus_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr = tb3.NewRow();
            dr["supcust_no"] = "";
            dr["sup_name"] = "全部";
            tb3.Rows.InsertAt(dr, 0);
            txtCus.Bind(tb3, 230, 200, "supcust_no", "supcust_no:客户编号:80,sup_name:客户:150", "supcust_no/sup_name->Text");
            tablePanel.Controls.Add(txtCus, 2 + 1, 0);
            //CusControl(6, 0, "cust_to");
            //Label lbl2 = new Label();
            //lbl2.Text = "至:";
            //lbl2.AutoSize = true;
            ////lbl2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //lbl2.Margin = new System.Windows.Forms.Padding(5, 6, 0, 0);
            //tablePanel.Controls.Add(lbl2, 4, 0);

            Label lblCus = new Label();
            lblCus.Text = "客户分类:";
            lblCus.AutoSize = true;
            lblCus.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus, 4, 0);

            companyType.Tag = "company_type";
            companyType.Width = 150;
            companyType.BorderStyle = BorderStyle.Fixed3D;
            companyType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("company_type", companyType);
            companyType.ClickCellAfter += (object sender, string column_name, DataRow row, MouseEventArgs e) =>
            {
                txtCus.Text = "";
            };
            CheckBox check = new CheckBox();
            check.Text = "显示金额都为0的客户";
            check.AutoSize = true;
            check.Tag = "isnull";
            check.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            check.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(check, 6, 0);
            con_dic.Add("isnull", check);
            IBLL.ISupcustGroup gbll = new BLL.SupcustGroupBLL();
            DataTable tb = gbll.GetAllCls("1");
           

            //IBLL.ICus cus_bll = new BLL.CusBLL();
            //int total_count;
            //var tb = cus_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr1 = tb.NewRow();
            dr1["type_no"] = "";
            dr1["type_name"] = "全部";
            tb.Rows.InsertAt(dr1, 0);
            companyType.Bind(tb, 230, 200, "type_no", "type_no:编号:80,type_name:名称:150", "type_no/type_name->Text");
            tablePanel.Controls.Add(companyType, 5, 0);
            dgv.AddColumn("编码", "编码", "客户", 100, 1, "");
            dgv.AddColumn("名称", "名称", "客户", 100, 1, "");
            dgv.AddColumn("期初金额方向", "期初金额方向", "期初余额", 100, 1, "");
            dgv.AddColumn("期初金额", "期初金额", "期初余额", 100, 3, "0.00");
            dgv.AddColumn("借方", "借方", "", 100, 3, "0.00");
            dgv.AddColumn("贷方", "贷方", "", 100, 3, "0.00");
            dgv.AddColumn("期末金额方向", "期末金额方向", "期末金额", 100, 1, "");
            dgv.AddColumn("期末金额", "期末金额", "期末金额", 100, 3, "0.00");
            //dgv.SetTotalColumn("收款金额,免收款金额");
            this.key_mod = "GetCusBalance";
            this.Text = "客户往来余额表";
            select_info = "查询条件为: 查询时间从[start_time]到[end_time],客户编号是[cust_from],客商类型是[company_type],显示金额都为0的客户[isnull]";
            format_dic = new Dictionary<string, string>()
            {
                {"start_time","yyyy-MM-dd"},
                {"end_time","yyyy-MM-dd"},
            };
        }
        /// <summary>
        /// 供应商往来明细账
        /// </summary>
        public void GetSupContactDetails()
        {
            AddControl();
            SupControl(2, 0);
            //
            dgv.AddColumn("年", "年", "", 50, 1, "");
            dgv.AddColumn("月", "月", "", 50, 1, "");
            dgv.AddColumn("日", "日", "", 50, 1, "");
            dgv.AddColumn("单据类型", "单据类型", "", 100, 1, IvyTransFunction.all_type_str);
            dgv.AddColumn("单据号", "单据号", "", 150, 1, "");
            dgv.AddColumn("摘要", "摘要", "", 100, 1, IvyTransFunction.all_type_str);
            dgv.AddColumn("借方", "借方", "", 100, 3, "0.00");
            dgv.AddColumn("贷方", "贷方", "", 100, 3, "0.00");
            dgv.AddColumn("方向", "方向", "", 50, 1, "");
            dgv.AddColumn("余额", "余额", "", 100, 3, "0.00");

            //dgv.SetTotalColumn("收款金额,免收款金额");
            this.key_mod = "GetSupContactDetails";
            this.Text = "供应商往来明细账";
            select_info = "查询条件为: 查询时间从[start_time]到[end_time],客户:[supcust_no]";
            format_dic = new Dictionary<string, string>()
            {
                {"start_time","yyyy-MM-dd"},
                {"end_time","yyyy-MM-dd"},
            };
        }
        /// <summary>
        /// 供应商往来余额表
        /// </summary>
        public void GetSupBalance()
        {
            AddControl();
            //CusControl(2, 0, "supcust_no");
            Label lbl1 = new Label();
            lbl1.Text = "供应商:";
            lbl1.AutoSize = true;
            lbl1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl1.Margin = new System.Windows.Forms.Padding(5, 6, 0, 0);
            tablePanel.Controls.Add(lbl1, 2, 0);
            cons.MyTextBox companyType = new cons.MyTextBox();
            cons.MyTextBox txtsup = new cons.MyTextBox();
            txtsup.Tag = "cust_from";
            txtsup.Width = 150;
            txtsup.BorderStyle = BorderStyle.Fixed3D;
            txtsup.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("cust_from", txtsup);
            txtsup.ClickCellAfter += (object sender, string column_name, DataRow row, MouseEventArgs e) =>
            {
                companyType.Text = "";
            };

            IBLL.ISup sup_bll = new BLL.SupBLL();
            int total_count = 0;
            var tb3 = sup_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr = tb3.NewRow();
            dr["supcust_no"] = "";
            dr["sup_name"] = "全部";
            tb3.Rows.InsertAt(dr, 0);
            txtsup.Bind(tb3, 230, 200, "supcust_no", "supcust_no:编号:80,sup_name:供应商:150", "supcust_no/sup_name->Text");
            tablePanel.Controls.Add(txtsup, 2 + 1, 0);
            //SupControl(6, 0, "cust_to");
            //Label lbl2 = new Label();
            //lbl2.Text = "至:";
            //lbl2.AutoSize = true;
            ////lbl2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //lbl2.Margin = new System.Windows.Forms.Padding(5, 6, 0, 0);
            //tablePanel.Controls.Add(lbl2, 4, 0);

            Label lblCus = new Label();
            lblCus.Text = "供应商分类:";
            lblCus.AutoSize = true;
            lblCus.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus, 4, 0);


            companyType.Tag = "company_type";
            companyType.Width = 150;
            companyType.BorderStyle = BorderStyle.Fixed3D;
            companyType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("company_type", companyType);
            companyType.ClickCellAfter += (object sender, string column_name, DataRow row, MouseEventArgs e) =>
            {
                txtsup.Text = "";
            };
            CheckBox check = new CheckBox();
            check.Text = "显示金额都为0的客户";
            check.AutoSize = true;
            check.Tag = "isnull";
            check.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            check.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(check, 6, 0);
            con_dic.Add("isnull", check);
            IBLL.ISupcustGroup gbll = new BLL.SupcustGroupBLL();
            DataTable tb = gbll.GetAllCls("1");

            //IBLL.ICus cus_bll = new BLL.CusBLL();
            //int total_count;
            //var tb = cus_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr1 = tb.NewRow();
            dr1["type_no"] = "";
            dr1["type_name"] = "全部";
            tb.Rows.InsertAt(dr1, 0);
            companyType.Bind(tb, 230, 200, "type_no", "type_no:编号:80,type_name:名称:150", "type_no/type_name->Text");
            tablePanel.Controls.Add(companyType, 5, 0);
            dgv.AddColumn("编码", "编码", "供应商", 100, 1, "");
            dgv.AddColumn("名称", "名称", "供应商", 100, 1, "");
            dgv.AddColumn("期初金额方向", "期初金额方向", "期初余额", 100, 1, "");
            dgv.AddColumn("期初金额", "期初金额", "期初余额", 100, 3, "0.00");
            dgv.AddColumn("借方", "借方", "", 100, 3, "0.00");
            dgv.AddColumn("贷方", "贷方", "", 100, 3, "0.00");
            dgv.AddColumn("期末金额方向", "期末金额方向", "期末金额", 100, 1, "");
            dgv.AddColumn("期末金额", "期末金额", "期末金额", 100, 3, "0.00");
            //dgv.SetTotalColumn("收款金额,免收款金额");
            this.key_mod = "GetSupBalance";
            this.Text = "供应商往来余额表";
            select_info = "查询条件为: 查询时间从[start_time]到[end_time],供应商编号是[cust_from],客商类型是[company_type],显示金额都为0的客户[isnull]";
            format_dic = new Dictionary<string, string>()
            {
                {"start_time","yyyy-MM-dd"},
                {"end_time","yyyy-MM-dd"},
            };
        }
        /// <summary>
        /// 客户账龄分析
        /// </summary>
        public void GetCusAgingGroup()
        {
            IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
            AddDateTime();
            //CusControl(2, 0, "cust_from");
            Label lblCus1 = new Label();
            lblCus1.Text = "客户:";
            lblCus1.AutoSize = true;
            lblCus1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCus1.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus1, 2, 0);
            cons.MyTextBox companyType = new cons.MyTextBox();
            cons.MyTextBox txtCus = new cons.MyTextBox();
            txtCus.Tag = "cust_from";
            txtCus.Width = 150;
            txtCus.BorderStyle = BorderStyle.Fixed3D;
            txtCus.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("cust_from", txtCus);
            txtCus.ClickCellAfter += (object sender, string column_name, DataRow row, MouseEventArgs e) =>
            {
                companyType.Text = "";
            };
            IBLL.ICus cus_bll = new BLL.CusBLL();
            int total_count;
            var tb3 = cus_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr = tb3.NewRow();
            dr["supcust_no"] = "";
            dr["sup_name"] = "全部";
            tb3.Rows.InsertAt(dr, 0);
            txtCus.Bind(tb3, 230, 200, "supcust_no", "supcust_no:客户编号:80,sup_name:客户:150", "supcust_no/sup_name->Text");
            tablePanel.Controls.Add(txtCus, 2 + 1, 0);
            //CusControl(6, 0, "cust_to");
            //Label lbl2 = new Label();
            //lbl2.Text = "至:";
            //lbl2.AutoSize = true;
            ////lbl2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //lbl2.Margin = new System.Windows.Forms.Padding(5, 6, 0, 0);
            //tablePanel.Controls.Add(lbl2, 4, 0);

            Label lblCus = new Label();
            lblCus.Text = "客户分类:";
            lblCus.AutoSize = true;
            lblCus.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus, 6, 0);

            companyType.Tag = "company_type";
            companyType.Width = 150;
            companyType.BorderStyle = BorderStyle.Fixed3D;
            companyType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("company_type", companyType);
            companyType.ClickCellAfter += (object sender, string column_name, DataRow row, MouseEventArgs e) =>
            {
                txtCus.Text = "";
            };
            IBLL.ISupcustGroup gbll = new BLL.SupcustGroupBLL();
            DataTable tb = gbll.GetAllCls("1");

            //IBLL.ICus cus_bll = new BLL.CusBLL();
            //int total_count;
            //var tb = cus_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr1 = tb.NewRow();
            dr1["type_no"] = "";
            dr1["type_name"] = "全部";
            tb.Rows.InsertAt(dr1, 0);
            companyType.Bind(tb, 230, 200, "type_no", "type_no:编号:80,type_name:名称:150", "type_no/type_name->Text");
            tablePanel.Controls.Add(companyType, 7, 0);

            Button btnCG = new Button();
            btnCG.Text = "设置账龄分组";
            btnCG.Width = 100;
            btnCG.Margin = new System.Windows.Forms.Padding(20, 3, 0, 3);
            btnCG.Click += (sender, e) =>
            {
                
                var frm = new frmAgingGroup("C");
                frm.ShowDialog();

                

                DataTable dt1 = paymentbll.GetAgingGroup("C");
                if (dt1.Rows.Count == 0)
                {
                    MsgForm.ShowFrom("请先设置好账龄！");
                    return;
                }
                dgv.DataSource.Clear();
                dgv.ClearColumn();
                dgv.AddColumn("客户编码", "客户编码", "", 100, 1, "");
                dgv.AddColumn("客户名称", "客户名称", "", 100, 1, "");
                int mm;
                string days1 = "0-" + Conv.ToString(dt1.Rows[0]["end_days"]) + "天";
                dgv.AddColumn(days1, days1, "", 100, 3, "");
                for (mm = 1; mm < dt1.Rows.Count - 1; mm++)
                {
                    days1 = Conv.ToString(dt1.Rows[mm]["start_days"]) + "-" + Conv.ToString(dt1.Rows[mm]["end_days"]) + "天";
                    dgv.AddColumn(days1, days1, "", 100, 3, "0.00");
                }
                days1 = "大于" + Conv.ToString(dt1.Rows[mm]["start_days"]) + "天";
                dgv.AddColumn(days1, days1, "", 100, 3, "");
                //dgv.AddColumn("收款未核销金额", "收款未核销金额", "", 150, 3, "0.00");
                //dgv.AddColumn("冲账未核销金额", "冲账未核销金额", "", 150, 3, "0.00");
                dgv.AddColumn("其他", "其他", "", 150, 3, "0.00");
                dgv.AddColumn("应收款余额", "应收款余额", "", 150, 3, "0.00");
                dgv.Refresh();
            };
            tablePanel.Controls.Add(btnCG, 2, 1);
            //AddLastControl(btnCG);

            
            DataTable dt= paymentbll.GetAgingGroup("C");
            dgv.AddColumn("客户编码", "客户编码", "", 100, 1, "");
            dgv.AddColumn("客户名称", "客户名称", "", 100, 1, "");
            if (dt.Rows.Count == 0)
            {
                MsgForm.ShowFrom("请先设置好账龄！");
            }
            else
            {
               
                int m;
                string days = "0-" + Conv.ToString(dt.Rows[0]["end_days"]) + "天";
                dgv.AddColumn(days, days, "", 100, 3, "0.00");
                for (m = 1; m < dt.Rows.Count - 1; m++)
                {
                    days = Conv.ToString(dt.Rows[m]["start_days"]) + "-" + Conv.ToString(dt.Rows[m]["end_days"]) + "天";
                    dgv.AddColumn(days, days, "", 100, 3, "0.00");
                }
                days = "大于" + Conv.ToString(dt.Rows[m]["start_days"]) + "天";
                dgv.AddColumn(days, days, "", 100, 3, "0.00");
               
            }
            //dgv.AddColumn("收款未核销金额", "收款未核销金额", "", 150, 3, "0.00");
            //dgv.AddColumn("冲账未核销金额", "冲账未核销金额", "", 150, 3, "0.00");
            dgv.AddColumn("其他", "其他", "", 150, 3, "0.00");
            dgv.AddColumn("应收款余额", "应收款余额", "", 150, 3, "0.00");
            this.key_mod = "GetCusAgingGroup";
            this.Text = "客户账龄分析";
            select_info = "查询条件为: 对比日期是[start_time],客户编号是[cust_from],客商类型是[company_type],账龄请在账龄分组中查看。";
            format_dic = new Dictionary<string, string>()
            {
                {"start_time","yyyy-MM-dd"},
            };
        }
        /// <summary>
        /// 供应商账龄分析
        /// </summary>
        public void GetSupAgingGroup()
        {
            IBLL.IARAP_SCPaymentBLL paymentbll = new BLL.ARAP_SCPaymentBLL();
            AddDateTime();
            //SupControl(2, 0, "cust_from");
            Label lbl1 = new Label();
            lbl1.Text = "供应商:";
            lbl1.AutoSize = true;
            lbl1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl1.Margin = new System.Windows.Forms.Padding(5, 6, 0, 0);
            tablePanel.Controls.Add(lbl1, 2, 0);
            cons.MyTextBox companyType = new cons.MyTextBox();
            cons.MyTextBox txtsup = new cons.MyTextBox();
            txtsup.Tag = "cust_from";
            txtsup.Width = 150;
            txtsup.BorderStyle = BorderStyle.Fixed3D;
            txtsup.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("cust_from", txtsup);
            txtsup.ClickCellAfter += (object sender, string column_name, DataRow row, MouseEventArgs e) =>
            {
                companyType.Text = "";
            };

            IBLL.ISup sup_bll = new BLL.SupBLL();
            int total_count = 0;
            var tb3 = sup_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr = tb3.NewRow();
            dr["supcust_no"] = "";
            dr["sup_name"] = "全部";
            tb3.Rows.InsertAt(dr, 0);
            txtsup.Bind(tb3, 230, 200, "supcust_no", "supcust_no:编号:80,sup_name:供应商:150", "supcust_no/sup_name->Text");
            tablePanel.Controls.Add(txtsup, 2 + 1, 0);
            //SupControl(6, 0, "cust_to");
            //Label lbl2 = new Label();
            //lbl2.Text = "至:";
            //lbl2.AutoSize = true;
            ////lbl2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //lbl2.Margin = new System.Windows.Forms.Padding(5, 6, 0, 0);
            //tablePanel.Controls.Add(lbl2, 4, 0);

            Label lblCus = new Label();
            lblCus.Text = "供应商分类:";
            lblCus.AutoSize = true;
            lblCus.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus, 6, 0);

            
            companyType.Tag = "company_type";
            companyType.Width = 150;
            companyType.BorderStyle = BorderStyle.Fixed3D;
            companyType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("company_type", companyType);
            companyType.ClickCellAfter += (object sender, string column_name, DataRow row, MouseEventArgs e) =>
            {
                txtsup.Text = "";
            };
            IBLL.ISupcustGroup gbll = new BLL.SupcustGroupBLL();
            DataTable tb = gbll.GetAllCls("1");

            //IBLL.ICus cus_bll = new BLL.CusBLL();
            //int total_count;
            //var tb = cus_bll.GetDataTable("", "", 1, 1, 999999, out total_count);
            DataRow dr1 = tb.NewRow();
            dr1["type_no"] = "";
            dr1["type_name"] = "全部";
            tb.Rows.InsertAt(dr1, 0);
            companyType.Bind(tb, 230, 200, "type_no", "type_no:编号:80,type_name:名称:150", "type_no/type_name->Text");
            tablePanel.Controls.Add(companyType, 7, 0);

            Button btnCG = new Button();
            btnCG.Text = "设置账龄分组";
            btnCG.Width = 100;
            btnCG.Margin = new System.Windows.Forms.Padding(20, 3, 0, 3);
            btnCG.Click += (sender, e) =>
            {

                var frm = new frmAgingGroup("S");
                frm.ShowDialog();



                DataTable dt1 = paymentbll.GetAgingGroup("S");
                if (dt1.Rows.Count == 0)
                {
                    MsgForm.ShowFrom("请先设置好账龄！");
                    return;
                }
                dgv.DataSource.Clear();
                dgv.ClearColumn();
                dgv.AddColumn("供应商编码", "供应商编码", "", 100, 1, "");
                dgv.AddColumn("供应商名称", "供应商名称", "", 100, 1, "");
                int mm;
                string days1 = "0-" + Conv.ToString(dt1.Rows[0]["end_days"]) + "天";
                dgv.AddColumn(days1, days1, "", 100, 3, "");
                for (mm = 1; mm < dt1.Rows.Count - 1; mm++)
                {
                    days1 = Conv.ToString(dt1.Rows[mm]["start_days"]) + "-" + Conv.ToString(dt1.Rows[mm]["end_days"]) + "天";
                    dgv.AddColumn(days1, days1, "", 100, 3, "0.00");
                }
                days1 = "大于" + Conv.ToString(dt1.Rows[mm]["start_days"]) + "天";
                dgv.AddColumn(days1, days1, "", 100, 3, "");
                //dgv.AddColumn("付款未核销金额", "付款未核销金额", "", 150, 3, "0.00");
                //dgv.AddColumn("冲账未核销金额", "冲账未核销金额", "", 150, 3, "0.00");
                dgv.AddColumn("其他", "其他", "", 150, 3, "0.00");
                dgv.AddColumn("应付款余额", "应付款余额", "", 150, 3, "0.00");
                dgv.Refresh();
            };
            tablePanel.Controls.Add(btnCG, 2, 1);
            //AddLastControl(btnCG);


            DataTable dt = paymentbll.GetAgingGroup("S");
            dgv.AddColumn("供应商编码", "供应商编码", "", 100, 1, "");
            dgv.AddColumn("供应商名称", "供应商名称", "", 100, 1, "");
            if (dt.Rows.Count == 0)
            {
                MsgForm.ShowFrom("请先设置好账龄！");
            }
            else
            {
               
                int m;
                string days = "0-" + Conv.ToString(dt.Rows[0]["end_days"]) + "天";
                dgv.AddColumn(days, days, "", 100, 3, "0.00");
                for (m = 1; m < dt.Rows.Count - 1; m++)
                {
                    days = Conv.ToString(dt.Rows[m]["start_days"]) + "-" + Conv.ToString(dt.Rows[m]["end_days"]) + "天";
                    dgv.AddColumn(days, days, "", 100, 3, "0.00");
                }
                days = "大于" + Conv.ToString(dt.Rows[m]["start_days"]) + "天";
                dgv.AddColumn(days, days, "", 100, 3, "0.00");
            }
            //dgv.AddColumn("付款未核销金额", "付款未核销金额", "", 150, 3, "0.00");
            //dgv.AddColumn("冲账未核销金额", "冲账未核销金额", "", 150, 3, "0.00");
            dgv.AddColumn("其他", "其他", "", 150, 3, "0.00");
            dgv.AddColumn("应付款余额", "应付款余额", "", 150, 3, "0.00");
            this.key_mod = "GetSupAgingGroup";
            this.Text = "供应商账龄分析";
            select_info = "查询条件为: 对比日期是[start_time],供应商编号是[cust_from],客商类型是[company_type],账龄请在账龄分组中查看。";
            format_dic = new Dictionary<string, string>()
            {
                {"start_time","yyyy-MM-dd"},
            };
        }
        #endregion

        #region 出纳管理
        public void GetBankCashDetailed()
        {
            AddControl();
            Label lblCus = new Label();
            lblCus.Text = "账户:";
            lblCus.AutoSize = true;
            lblCus.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus, 3, 0);

            cons.MyTextBox companyType = new cons.MyTextBox();
            companyType.Tag = "visa_id";
            companyType.Width = 150;
            companyType.BorderStyle = BorderStyle.Fixed3D;
            companyType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("visa_id", companyType);

            IBLL.IBank bankBLL = new BLL.BankBLL();
            DataTable tb = bankBLL.GetAllList();
            //DataRow dr1 = tb.NewRow();
            //dr1["type_no"] = "";
            //dr1["type_name"] = "全部";
            //tb.Rows.InsertAt(dr1, 0);
            companyType.Bind(tb, 230, 200, "visa_id", "visa_id:编号:80,visa_nm:名称:150", "visa_id/visa_nm->Text");
            tablePanel.Controls.Add(companyType, 4, 0);
            //
            dgv.AddColumn("年", "年", "", 50, 1, "");
            dgv.AddColumn("月", "月", "", 50, 1, "");
            dgv.AddColumn("日", "日", "", 50, 1, "");
            dgv.AddColumn("单据类型", "单据类型", "", 180, 1, "");
            dgv.AddColumn("单据号", "单据号", "", 150, 1, "");
            dgv.AddColumn("摘要", "摘要", "", 100, 1, "");
            dgv.AddColumn("借方", "借方", "", 100, 3, "0.00");
            dgv.AddColumn("贷方", "贷方", "", 100, 3, "0.00");
            dgv.AddColumn("方向", "方向", "", 50, 1, "");
            dgv.AddColumn("余额", "余额", "", 100, 3, "0.00");

            //dgv.SetTotalColumn("收款金额,免收款金额");
            this.key_mod = "GetBankCashDetailed";
            this.Text = "现金银行明细账";
            select_info = "查询条件为: 查询时间从[start_time]到[end_time],账户:[visa_id]";
            format_dic = new Dictionary<string, string>()
            {
                {"start_time","yyyy-MM-dd"},
                {"end_time","yyyy-MM-dd"},
            };
            //MsgForm.ShowFrom(dgv.DataSource.Rows.Count.ToString());
            //for (int i = 0; i < dgv.DataSource.Rows.Count; i++)
            //{

            //}
        }
        /// <summary>
        /// 现金银行余额表
        /// </summary>
        public void GetBankCashBalance()
        {
            AddControl();
            //CusControl(2, 0, "supcust_no");
            Label lblCus = new Label();
            lblCus.Text = "账户:";
            lblCus.AutoSize = true;
            lblCus.Margin = new System.Windows.Forms.Padding(5, 6, 0, 3);
            tablePanel.Controls.Add(lblCus, 3, 0);

            cons.MyTextBox companyType = new cons.MyTextBox();
            companyType.Tag = "visa_id1";
            companyType.Width = 150;
            companyType.BorderStyle = BorderStyle.Fixed3D;
            companyType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            con_dic.Add("visa_id1", companyType);

            IBLL.IBank bankBLL = new BLL.BankBLL();
            DataTable tb = bankBLL.GetAllList();
            //DataRow dr1 = tb.NewRow();
            //dr1["type_no"] = "";
            //dr1["type_name"] = "全部";
            //tb.Rows.InsertAt(dr1, 0);
            companyType.Bind(tb, 230, 200, "visa_id", "visa_id:编号:80,visa_nm:名称:150", "visa_id/visa_nm->Text");
            tablePanel.Controls.Add(companyType, 4, 0);
            dgv.AddColumn("编码", "编码", "账户", 100, 1, "");
            dgv.AddColumn("名称", "名称", "账户", 100, 1, "");
            dgv.AddColumn("期初金额方向", "期初金额方向", "期初余额", 100, 1, "");
            dgv.AddColumn("期初金额", "期初金额", "期初余额", 100, 3, "0.00");
            dgv.AddColumn("借方", "借方", "", 100, 3, "0.00");
            dgv.AddColumn("贷方", "贷方", "", 100, 3, "0.00");
            dgv.AddColumn("期末金额方向", "期末金额方向", "期末金额", 100, 1, "");
            dgv.AddColumn("期末金额", "期末金额", "期末金额", 100, 3, "0.00");
            //dgv.SetTotalColumn("收款金额,免收款金额");
            this.key_mod = "GetBankCashBalance";
            this.Text = "现金银行余额表";
            select_info = "查询条件为: 查询时间从[start_time]到[end_time],账户:[visa_id1]";
            format_dic = new Dictionary<string, string>()
            {
                {"start_time","yyyy-MM-dd"},
                {"end_time","yyyy-MM-dd"},
            };
        }
        #endregion
        //财务


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFinanceReport));
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.Location = new System.Drawing.Point(0, 103);
            this.dgv.Margin = new System.Windows.Forms.Padding(2);
            this.dgv.Size = new System.Drawing.Size(682, 259);
            // 
            // frmFinanceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(682, 402);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmFinanceReport";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}