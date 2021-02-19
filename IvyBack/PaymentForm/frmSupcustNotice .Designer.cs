namespace IvyBack.VoucherForm
{
    partial class frmSupcustNotice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSupcustNotice));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.tsdbPrintMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrintV = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPrintStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtapprove_date = new IvyBack.cons.MyTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtapprove_man = new IvyBack.cons.MyTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_other1 = new IvyBack.cons.MyTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtQuota = new IvyBack.cons.MyTextBox();
            this.txtSupInitial = new IvyBack.cons.MyTextBox();
            this.txtoper_man = new IvyBack.cons.MyTextBox();
            this.txtNoticeStartDate = new IvyBack.cons.DateTextBox();
            this.txtNoticeEndDate = new IvyBack.cons.DateTextBox();
            this.txtoper_date = new IvyBack.cons.DateTextBox();
            this.txtsheet_no = new IvyBack.cons.MyTextBox();
            this.txt_account = new IvyBack.cons.MyTextBox();
            this.txt_sup = new IvyBack.cons.MyTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tableData = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.editGrid1 = new IvyBack.cons.EditGrid();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.tsdbPrintMenu,
            this.toolStripSeparator1,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1006, 40);
            this.toolStrip1.TabIndex = 2;
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
            this.tsmiPrint.Size = new System.Drawing.Size(180, 22);
            this.tsmiPrint.Text = "打印";
            this.tsmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
            // 
            // tsmiPrintV
            // 
            this.tsmiPrintV.Name = "tsmiPrintV";
            this.tsmiPrintV.Size = new System.Drawing.Size(180, 22);
            this.tsmiPrintV.Text = "打印预览";
            this.tsmiPrintV.Click += new System.EventHandler(this.tsmiPrintV_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmiPrintStyle
            // 
            this.tsmiPrintStyle.Name = "tsmiPrintStyle";
            this.tsmiPrintStyle.Size = new System.Drawing.Size(180, 22);
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
            this.tsbExit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(58, 40);
            this.tsbExit.Text = "关闭Esc";
            this.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(-8, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1012, 44);
            this.label1.TabIndex = 8;
            this.label1.Text = "客户账期通知单";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(11, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "客 户：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(340, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 14);
            this.label3.TabIndex = 9;
            this.label3.Text = "制单日期：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(9, 66);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 14);
            this.label4.TabIndex = 9;
            this.label4.Text = "单 号：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F);
            this.label9.Location = new System.Drawing.Point(9, 242);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 14);
            this.label9.TabIndex = 9;
            this.label9.Text = "本单金额:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F);
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(686, 68);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 9;
            this.label10.Text = "制 单 人:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.txtapprove_date);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtapprove_man);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txt_other1);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtQuota);
            this.panel1.Controls.Add(this.txtSupInitial);
            this.panel1.Controls.Add(this.txtoper_man);
            this.panel1.Controls.Add(this.txtNoticeStartDate);
            this.panel1.Controls.Add(this.txtNoticeEndDate);
            this.panel1.Controls.Add(this.txtoper_date);
            this.panel1.Controls.Add(this.txtsheet_no);
            this.panel1.Controls.Add(this.txt_account);
            this.panel1.Controls.Add(this.txt_sup);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tableData);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 277);
            this.panel1.TabIndex = 0;
            // 
            // txtapprove_date
            // 
            this.txtapprove_date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtapprove_date.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtapprove_date.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtapprove_date.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtapprove_date.Location = new System.Drawing.Point(784, 242);
            this.txtapprove_date.Margin = new System.Windows.Forms.Padding(4);
            this.txtapprove_date.Name = "txtapprove_date";
            this.txtapprove_date.ReadOnly = true;
            this.txtapprove_date.Size = new System.Drawing.Size(207, 15);
            this.txtapprove_date.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Gray;
            this.label13.Location = new System.Drawing.Point(684, 240);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "审核日期：";
            // 
            // txtapprove_man
            // 
            this.txtapprove_man.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtapprove_man.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtapprove_man.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtapprove_man.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtapprove_man.Location = new System.Drawing.Point(438, 242);
            this.txtapprove_man.Margin = new System.Windows.Forms.Padding(4);
            this.txtapprove_man.Name = "txtapprove_man";
            this.txtapprove_man.ReadOnly = true;
            this.txtapprove_man.Size = new System.Drawing.Size(195, 15);
            this.txtapprove_man.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(351, 242);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "审核人：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(354, 195);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 31);
            this.button2.TabIndex = 14;
            this.button2.Text = "选择业务单据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(184, 195);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(145, 31);
            this.button3.TabIndex = 14;
            this.button3.Text = "按单据日期生成单据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 31);
            this.button1.TabIndex = 14;
            this.button1.Text = "按应结日期生成单据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_other1
            // 
            this.txt_other1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txt_other1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_other1.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_other1.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_other1.Location = new System.Drawing.Point(601, 210);
            this.txt_other1.Margin = new System.Windows.Forms.Padding(4);
            this.txt_other1.Name = "txt_other1";
            this.txt_other1.Size = new System.Drawing.Size(394, 16);
            this.txt_other1.TabIndex = 12;
            this.txt_other1.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10F);
            this.label12.Location = new System.Drawing.Point(510, 210);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 14);
            this.label12.TabIndex = 13;
            this.label12.Text = "备   注1：";
            this.label12.Visible = false;
            // 
            // txtQuota
            // 
            this.txtQuota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtQuota.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQuota.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtQuota.Font = new System.Drawing.Font("宋体", 10F);
            this.txtQuota.ForeColor = System.Drawing.Color.Black;
            this.txtQuota.Location = new System.Drawing.Point(784, 110);
            this.txtQuota.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuota.Name = "txtQuota";
            this.txtQuota.ReadOnly = true;
            this.txtQuota.Size = new System.Drawing.Size(211, 16);
            this.txtQuota.TabIndex = 6;
            // 
            // txtSupInitial
            // 
            this.txtSupInitial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtSupInitial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSupInitial.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtSupInitial.Font = new System.Drawing.Font("宋体", 10F);
            this.txtSupInitial.ForeColor = System.Drawing.Color.Black;
            this.txtSupInitial.Location = new System.Drawing.Point(438, 110);
            this.txtSupInitial.Margin = new System.Windows.Forms.Padding(4);
            this.txtSupInitial.Name = "txtSupInitial";
            this.txtSupInitial.ReadOnly = true;
            this.txtSupInitial.Size = new System.Drawing.Size(211, 16);
            this.txtSupInitial.TabIndex = 6;
            // 
            // txtoper_man
            // 
            this.txtoper_man.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtoper_man.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtoper_man.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtoper_man.Font = new System.Drawing.Font("宋体", 10F);
            this.txtoper_man.ForeColor = System.Drawing.Color.Black;
            this.txtoper_man.Location = new System.Drawing.Point(784, 68);
            this.txtoper_man.Margin = new System.Windows.Forms.Padding(4);
            this.txtoper_man.Name = "txtoper_man";
            this.txtoper_man.ReadOnly = true;
            this.txtoper_man.Size = new System.Drawing.Size(211, 16);
            this.txtoper_man.TabIndex = 6;
            // 
            // txtNoticeStartDate
            // 
            this.txtNoticeStartDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtNoticeStartDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNoticeStartDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtNoticeStartDate.Font = new System.Drawing.Font("宋体", 10F);
            this.txtNoticeStartDate.ForeColor = System.Drawing.Color.Black;
            this.txtNoticeStartDate.Location = new System.Drawing.Point(118, 154);
            this.txtNoticeStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtNoticeStartDate.Name = "txtNoticeStartDate";
            this.txtNoticeStartDate.Size = new System.Drawing.Size(211, 16);
            this.txtNoticeStartDate.TabIndex = 3;
            // 
            // txtNoticeEndDate
            // 
            this.txtNoticeEndDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtNoticeEndDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNoticeEndDate.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtNoticeEndDate.Font = new System.Drawing.Font("宋体", 10F);
            this.txtNoticeEndDate.ForeColor = System.Drawing.Color.Black;
            this.txtNoticeEndDate.Location = new System.Drawing.Point(441, 156);
            this.txtNoticeEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtNoticeEndDate.Name = "txtNoticeEndDate";
            this.txtNoticeEndDate.Size = new System.Drawing.Size(208, 16);
            this.txtNoticeEndDate.TabIndex = 3;
            // 
            // txtoper_date
            // 
            this.txtoper_date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtoper_date.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtoper_date.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtoper_date.Font = new System.Drawing.Font("宋体", 10F);
            this.txtoper_date.ForeColor = System.Drawing.Color.Black;
            this.txtoper_date.Location = new System.Drawing.Point(446, 68);
            this.txtoper_date.Margin = new System.Windows.Forms.Padding(4);
            this.txtoper_date.Name = "txtoper_date";
            this.txtoper_date.Size = new System.Drawing.Size(203, 16);
            this.txtoper_date.TabIndex = 3;
            // 
            // txtsheet_no
            // 
            this.txtsheet_no.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtsheet_no.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsheet_no.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtsheet_no.Font = new System.Drawing.Font("宋体", 10F);
            this.txtsheet_no.ForeColor = System.Drawing.Color.Black;
            this.txtsheet_no.Location = new System.Drawing.Point(118, 66);
            this.txtsheet_no.Margin = new System.Windows.Forms.Padding(4);
            this.txtsheet_no.Name = "txtsheet_no";
            this.txtsheet_no.ReadOnly = true;
            this.txtsheet_no.Size = new System.Drawing.Size(211, 16);
            this.txtsheet_no.TabIndex = 2;
            // 
            // txt_account
            // 
            this.txt_account.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txt_account.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_account.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_account.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_account.ForeColor = System.Drawing.Color.Black;
            this.txt_account.Location = new System.Drawing.Point(109, 241);
            this.txt_account.Margin = new System.Windows.Forms.Padding(4);
            this.txt_account.Name = "txt_account";
            this.txt_account.ReadOnly = true;
            this.txt_account.Size = new System.Drawing.Size(212, 16);
            this.txt_account.TabIndex = 7;
            this.txt_account.TextChanged += new System.EventHandler(this.txt_account_TextChanged);
            // 
            // txt_sup
            // 
            this.txt_sup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txt_sup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_sup.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_sup.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_sup.ForeColor = System.Drawing.Color.Black;
            this.txt_sup.Location = new System.Drawing.Point(118, 108);
            this.txt_sup.Margin = new System.Windows.Forms.Padding(4);
            this.txt_sup.Name = "txt_sup";
            this.txt_sup.Size = new System.Drawing.Size(211, 16);
            this.txt_sup.TabIndex = 0;
            this.txt_sup.ClickCellAfter += new IvyBack.cons.MyTextBox.ClickCellHandler(this.txt_sup_ClickCellAfter);
            this.txt_sup.TextChanged += new System.EventHandler(this.txt_sup_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(11, 154);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 14);
            this.label7.TabIndex = 9;
            this.label7.Text = "账期开始日期：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(334, 156);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 14);
            this.label5.TabIndex = 9;
            this.label5.Text = "账期截止日期：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F);
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(686, 110);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "信用额度:";
            // 
            // tableData
            // 
            this.tableData.AutoSize = true;
            this.tableData.Font = new System.Drawing.Font("宋体", 10F);
            this.tableData.ForeColor = System.Drawing.Color.Gray;
            this.tableData.Location = new System.Drawing.Point(340, 110);
            this.tableData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tableData.Name = "tableData";
            this.tableData.Size = new System.Drawing.Size(70, 14);
            this.tableData.TabIndex = 9;
            this.tableData.Text = "信用期限:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // myPanel1
            // 
            this.myPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myPanel1.BackColor = System.Drawing.Color.White;
            this.myPanel1.Controls.Add(this.editGrid1);
            this.myPanel1.Location = new System.Drawing.Point(0, 325);
            this.myPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(1006, 394);
            this.myPanel1.TabIndex = 3;
            // 
            // editGrid1
            // 
            this.editGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editGrid1.Editing = false;
            this.editGrid1.IsAutoAddRow = false;
            this.editGrid1.IsShowIco = true;
            this.editGrid1.Location = new System.Drawing.Point(0, 0);
            this.editGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.editGrid1.Name = "editGrid1";
            this.editGrid1.RowHeight = 25;
            this.editGrid1.Size = new System.Drawing.Size(1006, 394);
            this.editGrid1.TabIndex = 10;
            this.editGrid1.Text = "editGrid1";
            this.editGrid1.CellEndEdit += new IvyBack.cons.EditGrid.CellEndEditHandler(this.editGrid1_CellEndEdit);
            this.editGrid1.DoubleClickCell += new IvyBack.cons.EditGrid.DoubleClickCellHandler(this.editGrid1_DoubleClickCell);
            // 
            // frmSupcustNotice
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSupcustNotice";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "客户账期通知单";
            this.Shown += new System.EventHandler(this.frmCGInSheet_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private cons.MyTextBox txt_sup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private cons.MyTextBox txtsheet_no;
        private cons.DateTextBox txtoper_date;
        private System.Windows.Forms.Label label9;
        private cons.MyTextBox txt_account;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private cons.MyTextBox txtoper_man;
        private System.Windows.Forms.Timer timer1;
        private cons.MyTextBox txt_other1;
        private System.Windows.Forms.Label label12;
        private cons.MyPanel myPanel1;
        private cons.EditGrid editGrid1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tsdbPrintMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrintV;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrintStyle;
        private cons.DateTextBox txtNoticeEndDate;
        private System.Windows.Forms.Label label5;
        private cons.MyTextBox txtSupInitial;
        private System.Windows.Forms.Label tableData;
        private cons.MyTextBox txtQuota;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private cons.MyTextBox txtapprove_man;
        private System.Windows.Forms.Label label8;
        private cons.MyTextBox txtapprove_date;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button2;
        private cons.DateTextBox txtNoticeStartDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
    }
}