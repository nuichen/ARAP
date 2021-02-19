namespace IvyBack.FinanceForm
{
    partial class frmIncomeEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_pay_memo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_pay_flag = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_pay_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_pay_way = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_if_CtFix = new System.Windows.Forms.CheckBox();
            this.chk_is_account = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_is_profit = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chk_auto_cashsheet = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.myPanel5 = new IvyBack.cons.MyPanel();
            this.rd_profit_type2 = new System.Windows.Forms.RadioButton();
            this.rd_profit_type1 = new System.Windows.Forms.RadioButton();
            this.myPanel4 = new IvyBack.cons.MyPanel();
            this.rd_pay_kind2 = new System.Windows.Forms.RadioButton();
            this.rd_pay_kind1 = new System.Windows.Forms.RadioButton();
            this.myPanel3 = new IvyBack.cons.MyPanel();
            this.rd_is_pay3 = new System.Windows.Forms.RadioButton();
            this.rd_is_pay2 = new System.Windows.Forms.RadioButton();
            this.rd_is_pay1 = new System.Windows.Forms.RadioButton();
            this.myPanel2 = new IvyBack.cons.MyPanel();
            this.rd_account_flag2 = new System.Windows.Forms.RadioButton();
            this.rd_account_flag1 = new System.Windows.Forms.RadioButton();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.chk_path = new System.Windows.Forms.CheckBox();
            this.txtvisa = new IvyBack.cons.MyTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.myPanel5.SuspendLayout();
            this.myPanel4.SuspendLayout();
            this.myPanel3.SuspendLayout();
            this.myPanel2.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_pay_memo
            // 
            this.txt_pay_memo.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_pay_memo.ForeColor = System.Drawing.Color.Black;
            this.txt_pay_memo.Location = new System.Drawing.Point(110, 205);
            this.txt_pay_memo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_pay_memo.MaxLength = 255;
            this.txt_pay_memo.Name = "txt_pay_memo";
            this.txt_pay_memo.Size = new System.Drawing.Size(579, 23);
            this.txt_pay_memo.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(66, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 11;
            this.label5.Text = "备注:";
            // 
            // cb_pay_flag
            // 
            this.cb_pay_flag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_pay_flag.Font = new System.Drawing.Font("宋体", 10F);
            this.cb_pay_flag.ForeColor = System.Drawing.Color.Black;
            this.cb_pay_flag.FormattingEnabled = true;
            this.cb_pay_flag.Location = new System.Drawing.Point(507, 57);
            this.cb_pay_flag.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cb_pay_flag.Name = "cb_pay_flag";
            this.cb_pay_flag.Size = new System.Drawing.Size(150, 21);
            this.cb_pay_flag.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(462, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "类型:";
            // 
            // txt_pay_name
            // 
            this.txt_pay_name.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_pay_name.ForeColor = System.Drawing.Color.Black;
            this.txt_pay_name.Location = new System.Drawing.Point(110, 118);
            this.txt_pay_name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_pay_name.MaxLength = 30;
            this.txt_pay_name.Name = "txt_pay_name";
            this.txt_pay_name.Size = new System.Drawing.Size(254, 23);
            this.txt_pay_name.TabIndex = 2;
            this.txt_pay_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_pay_name_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(66, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "名称:";
            // 
            // txt_pay_way
            // 
            this.txt_pay_way.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_pay_way.ForeColor = System.Drawing.Color.Black;
            this.txt_pay_way.Location = new System.Drawing.Point(110, 57);
            this.txt_pay_way.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_pay_way.MaxLength = 3;
            this.txt_pay_way.Name = "txt_pay_way";
            this.txt_pay_way.Size = new System.Drawing.Size(95, 23);
            this.txt_pay_way.TabIndex = 0;
            this.txt_pay_way.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_pay_way_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(66, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "编码:";
            // 
            // chk_if_CtFix
            // 
            this.chk_if_CtFix.AutoSize = true;
            this.chk_if_CtFix.Font = new System.Drawing.Font("宋体", 10F);
            this.chk_if_CtFix.ForeColor = System.Drawing.Color.Black;
            this.chk_if_CtFix.Location = new System.Drawing.Point(68, 352);
            this.chk_if_CtFix.Name = "chk_if_CtFix";
            this.chk_if_CtFix.Size = new System.Drawing.Size(82, 18);
            this.chk_if_CtFix.TabIndex = 4;
            this.chk_if_CtFix.Text = "合同扣项";
            this.chk_if_CtFix.UseVisualStyleBackColor = true;
            this.chk_if_CtFix.Visible = false;
            // 
            // chk_is_account
            // 
            this.chk_is_account.AutoSize = true;
            this.chk_is_account.Font = new System.Drawing.Font("宋体", 10F);
            this.chk_is_account.ForeColor = System.Drawing.Color.Black;
            this.chk_is_account.Location = new System.Drawing.Point(282, 352);
            this.chk_is_account.Name = "chk_is_account";
            this.chk_is_account.Size = new System.Drawing.Size(82, 18);
            this.chk_is_account.TabIndex = 5;
            this.chk_is_account.Text = "现金收支";
            this.chk_is_account.UseVisualStyleBackColor = true;
            this.chk_is_account.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F);
            this.label6.Location = new System.Drawing.Point(461, 354);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 14);
            this.label6.TabIndex = 1;
            this.label6.Text = "现金收支方向:";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F);
            this.label7.Location = new System.Drawing.Point(66, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 14);
            this.label7.TabIndex = 10012;
            this.label7.Text = "参与应收应付:";
            this.label7.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(462, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 14);
            this.label2.TabIndex = 10014;
            this.label2.Text = "应收应付增减:";
            this.label2.Visible = false;
            // 
            // chk_is_profit
            // 
            this.chk_is_profit.AutoSize = true;
            this.chk_is_profit.Font = new System.Drawing.Font("宋体", 10F);
            this.chk_is_profit.ForeColor = System.Drawing.Color.Black;
            this.chk_is_profit.Location = new System.Drawing.Point(283, 307);
            this.chk_is_profit.Name = "chk_is_profit";
            this.chk_is_profit.Size = new System.Drawing.Size(110, 18);
            this.chk_is_profit.TabIndex = 14;
            this.chk_is_profit.Text = "参与利润核算";
            this.chk_is_profit.UseVisualStyleBackColor = true;
            this.chk_is_profit.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F);
            this.label10.Location = new System.Drawing.Point(462, 309);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 14);
            this.label10.TabIndex = 10017;
            this.label10.Text = "利润增减方向:";
            this.label10.Visible = false;
            // 
            // chk_auto_cashsheet
            // 
            this.chk_auto_cashsheet.AutoSize = true;
            this.chk_auto_cashsheet.Font = new System.Drawing.Font("宋体", 10F);
            this.chk_auto_cashsheet.ForeColor = System.Drawing.Color.Black;
            this.chk_auto_cashsheet.Location = new System.Drawing.Point(69, 307);
            this.chk_auto_cashsheet.Name = "chk_auto_cashsheet";
            this.chk_auto_cashsheet.Size = new System.Drawing.Size(138, 18);
            this.chk_auto_cashsheet.TabIndex = 3;
            this.chk_auto_cashsheet.Text = "自动生成现金流水";
            this.chk_auto_cashsheet.UseVisualStyleBackColor = true;
            this.chk_auto_cashsheet.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtvisa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chk_auto_cashsheet);
            this.panel1.Controls.Add(this.cb_pay_flag);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.myPanel5);
            this.panel1.Controls.Add(this.txt_pay_memo);
            this.panel1.Controls.Add(this.chk_is_profit);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_pay_name);
            this.panel1.Controls.Add(this.myPanel4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txt_pay_way);
            this.panel1.Controls.Add(this.myPanel3);
            this.panel1.Controls.Add(this.chk_if_CtFix);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.chk_is_account);
            this.panel1.Controls.Add(this.myPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 475);
            this.panel1.TabIndex = 10018;
            // 
            // myPanel5
            // 
            this.myPanel5.Controls.Add(this.rd_profit_type2);
            this.myPanel5.Controls.Add(this.rd_profit_type1);
            this.myPanel5.Location = new System.Drawing.Point(562, 300);
            this.myPanel5.Name = "myPanel5";
            this.myPanel5.Size = new System.Drawing.Size(144, 30);
            this.myPanel5.TabIndex = 15;
            this.myPanel5.Visible = false;
            // 
            // rd_profit_type2
            // 
            this.rd_profit_type2.AutoSize = true;
            this.rd_profit_type2.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_profit_type2.Location = new System.Drawing.Point(74, 6);
            this.rd_profit_type2.Name = "rd_profit_type2";
            this.rd_profit_type2.Size = new System.Drawing.Size(53, 18);
            this.rd_profit_type2.TabIndex = 16;
            this.rd_profit_type2.TabStop = true;
            this.rd_profit_type2.Tag = "1";
            this.rd_profit_type2.Text = "增加";
            this.rd_profit_type2.UseVisualStyleBackColor = true;
            // 
            // rd_profit_type1
            // 
            this.rd_profit_type1.AutoSize = true;
            this.rd_profit_type1.Checked = true;
            this.rd_profit_type1.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_profit_type1.Location = new System.Drawing.Point(7, 6);
            this.rd_profit_type1.Name = "rd_profit_type1";
            this.rd_profit_type1.Size = new System.Drawing.Size(53, 18);
            this.rd_profit_type1.TabIndex = 15;
            this.rd_profit_type1.TabStop = true;
            this.rd_profit_type1.Tag = "0";
            this.rd_profit_type1.Text = "减少";
            this.rd_profit_type1.UseVisualStyleBackColor = true;
            // 
            // myPanel4
            // 
            this.myPanel4.Controls.Add(this.rd_pay_kind2);
            this.myPanel4.Controls.Add(this.rd_pay_kind1);
            this.myPanel4.Location = new System.Drawing.Point(562, 233);
            this.myPanel4.Name = "myPanel4";
            this.myPanel4.Size = new System.Drawing.Size(144, 30);
            this.myPanel4.TabIndex = 11;
            this.myPanel4.Visible = false;
            // 
            // rd_pay_kind2
            // 
            this.rd_pay_kind2.AutoSize = true;
            this.rd_pay_kind2.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_pay_kind2.Location = new System.Drawing.Point(74, 6);
            this.rd_pay_kind2.Name = "rd_pay_kind2";
            this.rd_pay_kind2.Size = new System.Drawing.Size(53, 18);
            this.rd_pay_kind2.TabIndex = 12;
            this.rd_pay_kind2.Tag = "1";
            this.rd_pay_kind2.Text = "增加";
            this.rd_pay_kind2.UseVisualStyleBackColor = true;
            // 
            // rd_pay_kind1
            // 
            this.rd_pay_kind1.AutoSize = true;
            this.rd_pay_kind1.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_pay_kind1.Location = new System.Drawing.Point(7, 6);
            this.rd_pay_kind1.Name = "rd_pay_kind1";
            this.rd_pay_kind1.Size = new System.Drawing.Size(53, 18);
            this.rd_pay_kind1.TabIndex = 11;
            this.rd_pay_kind1.Tag = "0";
            this.rd_pay_kind1.Text = "减少";
            this.rd_pay_kind1.UseVisualStyleBackColor = true;
            // 
            // myPanel3
            // 
            this.myPanel3.Controls.Add(this.rd_is_pay3);
            this.myPanel3.Controls.Add(this.rd_is_pay2);
            this.myPanel3.Controls.Add(this.rd_is_pay1);
            this.myPanel3.Location = new System.Drawing.Point(168, 233);
            this.myPanel3.Name = "myPanel3";
            this.myPanel3.Size = new System.Drawing.Size(219, 30);
            this.myPanel3.TabIndex = 8;
            this.myPanel3.Visible = false;
            // 
            // rd_is_pay3
            // 
            this.rd_is_pay3.AutoSize = true;
            this.rd_is_pay3.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_is_pay3.Location = new System.Drawing.Point(152, 6);
            this.rd_is_pay3.Name = "rd_is_pay3";
            this.rd_is_pay3.Size = new System.Drawing.Size(53, 18);
            this.rd_is_pay3.TabIndex = 10;
            this.rd_is_pay3.TabStop = true;
            this.rd_is_pay3.Tag = "1";
            this.rd_is_pay3.Text = "应付";
            this.rd_is_pay3.UseVisualStyleBackColor = true;
            // 
            // rd_is_pay2
            // 
            this.rd_is_pay2.AutoSize = true;
            this.rd_is_pay2.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_is_pay2.Location = new System.Drawing.Point(85, 6);
            this.rd_is_pay2.Name = "rd_is_pay2";
            this.rd_is_pay2.Size = new System.Drawing.Size(53, 18);
            this.rd_is_pay2.TabIndex = 9;
            this.rd_is_pay2.TabStop = true;
            this.rd_is_pay2.Tag = "1";
            this.rd_is_pay2.Text = "应收";
            this.rd_is_pay2.UseVisualStyleBackColor = true;
            // 
            // rd_is_pay1
            // 
            this.rd_is_pay1.AutoSize = true;
            this.rd_is_pay1.Checked = true;
            this.rd_is_pay1.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_is_pay1.Location = new System.Drawing.Point(7, 6);
            this.rd_is_pay1.Name = "rd_is_pay1";
            this.rd_is_pay1.Size = new System.Drawing.Size(67, 18);
            this.rd_is_pay1.TabIndex = 8;
            this.rd_is_pay1.TabStop = true;
            this.rd_is_pay1.Tag = "0";
            this.rd_is_pay1.Text = "不参与";
            this.rd_is_pay1.UseVisualStyleBackColor = true;
            this.rd_is_pay1.CheckedChanged += new System.EventHandler(this.rd_is_pay1_CheckedChanged);
            // 
            // myPanel2
            // 
            this.myPanel2.Controls.Add(this.rd_account_flag2);
            this.myPanel2.Controls.Add(this.rd_account_flag1);
            this.myPanel2.Location = new System.Drawing.Point(561, 345);
            this.myPanel2.Name = "myPanel2";
            this.myPanel2.Size = new System.Drawing.Size(144, 30);
            this.myPanel2.TabIndex = 6;
            this.myPanel2.Visible = false;
            // 
            // rd_account_flag2
            // 
            this.rd_account_flag2.AutoSize = true;
            this.rd_account_flag2.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_account_flag2.Location = new System.Drawing.Point(74, 6);
            this.rd_account_flag2.Name = "rd_account_flag2";
            this.rd_account_flag2.Size = new System.Drawing.Size(53, 18);
            this.rd_account_flag2.TabIndex = 1;
            this.rd_account_flag2.TabStop = true;
            this.rd_account_flag2.Tag = "1";
            this.rd_account_flag2.Text = "支出";
            this.rd_account_flag2.UseVisualStyleBackColor = true;
            // 
            // rd_account_flag1
            // 
            this.rd_account_flag1.AutoSize = true;
            this.rd_account_flag1.Checked = true;
            this.rd_account_flag1.Font = new System.Drawing.Font("宋体", 10F);
            this.rd_account_flag1.Location = new System.Drawing.Point(7, 6);
            this.rd_account_flag1.Name = "rd_account_flag1";
            this.rd_account_flag1.Size = new System.Drawing.Size(53, 18);
            this.rd_account_flag1.TabIndex = 0;
            this.rd_account_flag1.TabStop = true;
            this.rd_account_flag1.Tag = "0";
            this.rd_account_flag1.Text = "收入";
            this.rd_account_flag1.UseVisualStyleBackColor = true;
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.myPanel1.Controls.Add(this.btnOk);
            this.myPanel1.Controls.Add(this.btnNo);
            this.myPanel1.Controls.Add(this.chk_path);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(794, 40);
            this.myPanel1.TabIndex = 18;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10F);
            this.btnOk.Location = new System.Drawing.Point(5, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 35);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("宋体", 10F);
            this.btnNo.Location = new System.Drawing.Point(110, 2);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(100, 35);
            this.btnNo.TabIndex = 19;
            this.btnNo.Text = "取消";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // chk_path
            // 
            this.chk_path.AutoSize = true;
            this.chk_path.Font = new System.Drawing.Font("宋体", 10F);
            this.chk_path.ForeColor = System.Drawing.Color.Black;
            this.chk_path.Location = new System.Drawing.Point(315, 11);
            this.chk_path.Name = "chk_path";
            this.chk_path.Size = new System.Drawing.Size(54, 18);
            this.chk_path.TabIndex = 13;
            this.chk_path.Text = "锁定";
            this.chk_path.UseVisualStyleBackColor = true;
            this.chk_path.Visible = false;
            this.chk_path.CheckedChanged += new System.EventHandler(this.chk_path_CheckedChanged);
            // 
            // txtvisa
            // 
            this.txtvisa.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtvisa.Location = new System.Drawing.Point(508, 120);
            this.txtvisa.Name = "txtvisa";
            this.txtvisa.Size = new System.Drawing.Size(180, 21);
            this.txtvisa.TabIndex = 10018;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F);
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(460, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "科目:";
            // 
            // frmIncomeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(794, 515);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.myPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIncomeEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmIncomeEdit_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIncomeEdit_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.myPanel5.ResumeLayout(false);
            this.myPanel5.PerformLayout();
            this.myPanel4.ResumeLayout(false);
            this.myPanel4.PerformLayout();
            this.myPanel3.ResumeLayout(false);
            this.myPanel3.PerformLayout();
            this.myPanel2.ResumeLayout(false);
            this.myPanel2.PerformLayout();
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_pay_way;
        private System.Windows.Forms.TextBox txt_pay_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_pay_flag;
        private System.Windows.Forms.TextBox txt_pay_memo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNo;
        private cons.MyPanel myPanel1;
        private System.Windows.Forms.CheckBox chk_path;
        private System.Windows.Forms.CheckBox chk_if_CtFix;
        private System.Windows.Forms.CheckBox chk_is_account;
        private cons.MyPanel myPanel2;
        private System.Windows.Forms.RadioButton rd_account_flag1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rd_account_flag2;
        private System.Windows.Forms.Label label7;
        private cons.MyPanel myPanel3;
        private System.Windows.Forms.RadioButton rd_is_pay2;
        private System.Windows.Forms.RadioButton rd_is_pay1;
        private System.Windows.Forms.RadioButton rd_is_pay3;
        private System.Windows.Forms.Label label2;
        private cons.MyPanel myPanel4;
        private System.Windows.Forms.RadioButton rd_pay_kind2;
        private System.Windows.Forms.RadioButton rd_pay_kind1;
        private System.Windows.Forms.CheckBox chk_is_profit;
        private System.Windows.Forms.Label label10;
        private cons.MyPanel myPanel5;
        private System.Windows.Forms.RadioButton rd_profit_type2;
        private System.Windows.Forms.RadioButton rd_profit_type1;
        private System.Windows.Forms.CheckBox chk_auto_cashsheet;
        private System.Windows.Forms.Panel panel1;
        private cons.MyTextBox txtvisa;
        private System.Windows.Forms.Label label8;
    }
}