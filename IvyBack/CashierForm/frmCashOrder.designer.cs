namespace IvyBack.VoucherForm
{
    partial class frmCashOrder
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtcash = new IvyBack.cons.MyTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtvisa2 = new IvyBack.cons.MyTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtoper_man = new IvyBack.cons.MyTextBox();
            this.txtoper_date = new IvyBack.cons.DateTextBox();
            this.txtsheet_no = new IvyBack.cons.MyTextBox();
            this.txtapprove_date = new IvyBack.cons.MyTextBox();
            this.txtapprove_man = new IvyBack.cons.MyTextBox();
            this.txtvisa = new IvyBack.cons.MyTextBox();
            this.txtmemo = new IvyBack.cons.MyTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tsdbPrintMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrintV = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPrintStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panel1.Controls.Add(this.txtcash);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtvisa2);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtoper_man);
            this.panel1.Controls.Add(this.txtoper_date);
            this.panel1.Controls.Add(this.txtsheet_no);
            this.panel1.Controls.Add(this.txtapprove_date);
            this.panel1.Controls.Add(this.txtapprove_man);
            this.panel1.Controls.Add(this.txtvisa);
            this.panel1.Controls.Add(this.txtmemo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 534);
            this.panel1.TabIndex = 9;
            // 
            // txtcash
            // 
            this.txtcash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtcash.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcash.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtcash.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtcash.Location = new System.Drawing.Point(601, 92);
            this.txtcash.Name = "txtcash";
            this.txtcash.Size = new System.Drawing.Size(134, 15);
            this.txtcash.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Maroon;
            this.label14.Location = new System.Drawing.Point(527, 91);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "转账金额：";
            // 
            // txtvisa2
            // 
            this.txtvisa2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtvisa2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtvisa2.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtvisa2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtvisa2.Location = new System.Drawing.Point(347, 91);
            this.txtvisa2.Name = "txtvisa2";
            this.txtvisa2.Size = new System.Drawing.Size(134, 15);
            this.txtvisa2.TabIndex = 11;
            this.txtvisa2.ClickCellAfter += new IvyBack.cons.MyTextBox.ClickCellHandler(this.txtvisa2_ClickCellAfter);
            this.txtvisa2.TextChanged += new System.EventHandler(this.txtvisa2_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Maroon;
            this.label13.Location = new System.Drawing.Point(271, 91);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "转入账户：";
            // 
            // txtoper_man
            // 
            this.txtoper_man.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtoper_man.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtoper_man.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtoper_man.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtoper_man.Location = new System.Drawing.Point(601, 52);
            this.txtoper_man.Name = "txtoper_man";
            this.txtoper_man.ReadOnly = true;
            this.txtoper_man.Size = new System.Drawing.Size(134, 15);
            this.txtoper_man.TabIndex = 6;
            // 
            // txtoper_date
            // 
            this.txtoper_date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtoper_date.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtoper_date.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtoper_date.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtoper_date.Location = new System.Drawing.Point(347, 52);
            this.txtoper_date.Name = "txtoper_date";
            this.txtoper_date.Size = new System.Drawing.Size(136, 15);
            this.txtoper_date.TabIndex = 3;
            // 
            // txtsheet_no
            // 
            this.txtsheet_no.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtsheet_no.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsheet_no.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtsheet_no.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtsheet_no.Location = new System.Drawing.Point(80, 52);
            this.txtsheet_no.Name = "txtsheet_no";
            this.txtsheet_no.ReadOnly = true;
            this.txtsheet_no.Size = new System.Drawing.Size(170, 15);
            this.txtsheet_no.TabIndex = 2;
            // 
            // txtapprove_date
            // 
            this.txtapprove_date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtapprove_date.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtapprove_date.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtapprove_date.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtapprove_date.Location = new System.Drawing.Point(601, 126);
            this.txtapprove_date.Name = "txtapprove_date";
            this.txtapprove_date.ReadOnly = true;
            this.txtapprove_date.Size = new System.Drawing.Size(134, 15);
            this.txtapprove_date.TabIndex = 8;
            // 
            // txtapprove_man
            // 
            this.txtapprove_man.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtapprove_man.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtapprove_man.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtapprove_man.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtapprove_man.Location = new System.Drawing.Point(347, 126);
            this.txtapprove_man.Name = "txtapprove_man";
            this.txtapprove_man.ReadOnly = true;
            this.txtapprove_man.Size = new System.Drawing.Size(134, 15);
            this.txtapprove_man.TabIndex = 5;
            // 
            // txtvisa
            // 
            this.txtvisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtvisa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtvisa.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtvisa.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtvisa.Location = new System.Drawing.Point(80, 92);
            this.txtvisa.Name = "txtvisa";
            this.txtvisa.Size = new System.Drawing.Size(170, 15);
            this.txtvisa.TabIndex = 10;
            this.txtvisa.ClickCellAfter += new IvyBack.cons.MyTextBox.ClickCellHandler(this.txtvisa_ClickCellAfter);
            // 
            // txtmemo
            // 
            this.txtmemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtmemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtmemo.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtmemo.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtmemo.Location = new System.Drawing.Point(80, 126);
            this.txtmemo.Name = "txtmemo";
            this.txtmemo.Size = new System.Drawing.Size(170, 15);
            this.txtmemo.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(351, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "现金银行转账单";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(287, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "审核人：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Maroon;
            this.label12.Location = new System.Drawing.Point(5, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "转出账户：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(33, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "摘要：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(277, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "单据日期：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(545, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "出纳:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(526, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "审核日期：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(19, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "单据号：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::IvyBack.Properties.Resources.new_导航;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbDel,
            this.tsbSave,
            this.tsbCheck,
            this.toolStripButton2,
            this.tsdbPrintMenu,
            this.toolStripSeparator1,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(754, 43);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.AutoSize = false;
            this.tsbAdd.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbAdd.Image = global::IvyBack.Properties.Resources.new_新增;
            this.tsbAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(51, 40);
            this.tsbAdd.Text = "新建F2";
            this.tsbAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbDel
            // 
            this.tsbDel.AutoSize = false;
            this.tsbDel.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbDel.Image = global::IvyBack.Properties.Resources.new_删除;
            this.tsbDel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbDel.Name = "tsbDel";
            this.tsbDel.Size = new System.Drawing.Size(51, 40);
            this.tsbDel.Text = "删除F4";
            this.tsbDel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbDel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbDel.Click += new System.EventHandler(this.tsbDel_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.AutoSize = false;
            this.tsbSave.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbSave.Image = global::IvyBack.Properties.Resources.new_保存;
            this.tsbSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(37, 40);
            this.tsbSave.Text = "保存";
            this.tsbSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbCheck
            // 
            this.tsbCheck.AutoSize = false;
            this.tsbCheck.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbCheck.Image = global::IvyBack.Properties.Resources.new_审核;
            this.tsbCheck.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheck.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbCheck.Name = "tsbCheck";
            this.tsbCheck.Size = new System.Drawing.Size(37, 40);
            this.tsbCheck.Text = "审核";
            this.tsbCheck.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbCheck.Click += new System.EventHandler(this.tsbCheck_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoSize = false;
            this.toolStripButton2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton2.Image = global::IvyBack.Properties.Resources.new_反审;
            this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(37, 40);
            this.toolStripButton2.Text = "反审";
            this.toolStripButton2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // tsdbPrintMenu
            // 
            this.tsdbPrintMenu.AutoSize = false;
            this.tsdbPrintMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPrint,
            this.tsmiPrintV,
            this.toolStripMenuItem1,
            this.tsmiPrintStyle});
            this.tsdbPrintMenu.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsdbPrintMenu.Image = global::IvyBack.Properties.Resources.new_打印;
            this.tsdbPrintMenu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsdbPrintMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsdbPrintMenu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsdbPrintMenu.Name = "tsdbPrintMenu";
            this.tsdbPrintMenu.Size = new System.Drawing.Size(46, 40);
            this.tsdbPrintMenu.Text = "打印";
            this.tsdbPrintMenu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsdbPrintMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // tsmiPrint
            // 
            this.tsmiPrint.Name = "tsmiPrint";
            this.tsmiPrint.Size = new System.Drawing.Size(126, 22);
            this.tsmiPrint.Text = "打印";
            this.tsmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
            // 
            // tsmiPrintV
            // 
            this.tsmiPrintV.Name = "tsmiPrintV";
            this.tsmiPrintV.Size = new System.Drawing.Size(126, 22);
            this.tsmiPrintV.Text = "打印预览";
            this.tsmiPrintV.Click += new System.EventHandler(this.tsmiPrintV_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(123, 6);
            // 
            // tsmiPrintStyle
            // 
            this.tsmiPrintStyle.Name = "tsmiPrintStyle";
            this.tsmiPrintStyle.Size = new System.Drawing.Size(126, 22);
            this.tsmiPrintStyle.Text = "打印样式";
            this.tsmiPrintStyle.Click += new System.EventHandler(this.tsmiPrintStyle_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // tsbExit
            // 
            this.tsbExit.AutoSize = false;
            this.tsbExit.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbExit.Image = global::IvyBack.Properties.Resources.new_退出;
            this.tsbExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(58, 40);
            this.tsbExit.Text = "关闭Esc";
            this.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmCashOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(754, 577);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmCashOrder";
            this.Text = "现金银行转账单";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private cons.MyTextBox txtvisa2;
        private System.Windows.Forms.Label label13;
        private cons.MyTextBox txtoper_man;
        private cons.DateTextBox txtoper_date;
        private cons.MyTextBox txtsheet_no;
        private cons.MyTextBox txtapprove_date;
        private cons.MyTextBox txtapprove_man;
        private cons.MyTextBox txtvisa;
        private cons.MyTextBox txtmemo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Timer timer1;
        private cons.MyTextBox txtcash;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsdbPrintMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrintV;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrintStyle;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}