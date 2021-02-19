namespace IvyBack.VoucherForm
{
    partial class frmArToAr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmArToAr));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtoper_man = new IvyBack.cons.MyTextBox();
            this.txtoper_date = new IvyBack.cons.DateTextBox();
            this.txtsheet_no = new IvyBack.cons.MyTextBox();
            this.txtapprove_date = new IvyBack.cons.MyTextBox();
            this.txtapprove_man = new IvyBack.cons.MyTextBox();
            this.txt_to_amount = new IvyBack.cons.MyTextBox();
            this.txtmemo = new IvyBack.cons.MyTextBox();
            this.txtsup = new IvyBack.cons.MyTextBox();
            this.txtcus = new IvyBack.cons.MyTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
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
            this.editGrid1 = new IvyBack.cons.EditGrid();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panel1.Controls.Add(this.txtoper_man);
            this.panel1.Controls.Add(this.txtoper_date);
            this.panel1.Controls.Add(this.txtsheet_no);
            this.panel1.Controls.Add(this.txtapprove_date);
            this.panel1.Controls.Add(this.txtapprove_man);
            this.panel1.Controls.Add(this.txt_to_amount);
            this.panel1.Controls.Add(this.txtmemo);
            this.panel1.Controls.Add(this.txtsup);
            this.panel1.Controls.Add(this.txtcus);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 681);
            this.panel1.TabIndex = 9;
            // 
            // txtoper_man
            // 
            this.txtoper_man.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtoper_man.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtoper_man.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtoper_man.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtoper_man.Location = new System.Drawing.Point(486, 65);
            this.txtoper_man.Margin = new System.Windows.Forms.Padding(4);
            this.txtoper_man.Name = "txtoper_man";
            this.txtoper_man.ReadOnly = true;
            this.txtoper_man.Size = new System.Drawing.Size(179, 15);
            this.txtoper_man.TabIndex = 6;
            // 
            // txtoper_date
            // 
            this.txtoper_date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtoper_date.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtoper_date.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtoper_date.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtoper_date.Location = new System.Drawing.Point(137, 65);
            this.txtoper_date.Margin = new System.Windows.Forms.Padding(4);
            this.txtoper_date.Name = "txtoper_date";
            this.txtoper_date.Size = new System.Drawing.Size(179, 15);
            this.txtoper_date.TabIndex = 3;
            // 
            // txtsheet_no
            // 
            this.txtsheet_no.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtsheet_no.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsheet_no.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtsheet_no.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtsheet_no.Location = new System.Drawing.Point(791, 65);
            this.txtsheet_no.Margin = new System.Windows.Forms.Padding(4);
            this.txtsheet_no.Name = "txtsheet_no";
            this.txtsheet_no.ReadOnly = true;
            this.txtsheet_no.Size = new System.Drawing.Size(194, 15);
            this.txtsheet_no.TabIndex = 2;
            // 
            // txtapprove_date
            // 
            this.txtapprove_date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtapprove_date.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtapprove_date.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtapprove_date.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtapprove_date.Location = new System.Drawing.Point(140, 145);
            this.txtapprove_date.Margin = new System.Windows.Forms.Padding(4);
            this.txtapprove_date.Name = "txtapprove_date";
            this.txtapprove_date.ReadOnly = true;
            this.txtapprove_date.Size = new System.Drawing.Size(176, 15);
            this.txtapprove_date.TabIndex = 8;
            // 
            // txtapprove_man
            // 
            this.txtapprove_man.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtapprove_man.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtapprove_man.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtapprove_man.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtapprove_man.Location = new System.Drawing.Point(486, 145);
            this.txtapprove_man.Margin = new System.Windows.Forms.Padding(4);
            this.txtapprove_man.Name = "txtapprove_man";
            this.txtapprove_man.ReadOnly = true;
            this.txtapprove_man.Size = new System.Drawing.Size(179, 15);
            this.txtapprove_man.TabIndex = 5;
            this.txtapprove_man.TextChanged += new System.EventHandler(this.txtapprove_man_TextChanged);
            // 
            // txt_to_amount
            // 
            this.txt_to_amount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txt_to_amount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_to_amount.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_to_amount.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_to_amount.Location = new System.Drawing.Point(791, 110);
            this.txt_to_amount.Margin = new System.Windows.Forms.Padding(4);
            this.txt_to_amount.Name = "txt_to_amount";
            this.txt_to_amount.Size = new System.Drawing.Size(194, 15);
            this.txt_to_amount.TabIndex = 10;
            this.txt_to_amount.Text = "0.00";
            this.txt_to_amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtmemo
            // 
            this.txtmemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtmemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtmemo.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtmemo.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtmemo.Location = new System.Drawing.Point(140, 200);
            this.txtmemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtmemo.Name = "txtmemo";
            this.txtmemo.Size = new System.Drawing.Size(845, 15);
            this.txtmemo.TabIndex = 7;
            // 
            // txtsup
            // 
            this.txtsup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtsup.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsup.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtsup.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtsup.Location = new System.Drawing.Point(486, 105);
            this.txtsup.Margin = new System.Windows.Forms.Padding(4);
            this.txtsup.Name = "txtsup";
            this.txtsup.Size = new System.Drawing.Size(179, 15);
            this.txtsup.TabIndex = 4;
            this.txtsup.ClickCellAfter += new IvyBack.cons.MyTextBox.ClickCellHandler(this.txtsup_ClickCellAfter);
            // 
            // txtcus
            // 
            this.txtcus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtcus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcus.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtcus.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtcus.Location = new System.Drawing.Point(140, 105);
            this.txtcus.Margin = new System.Windows.Forms.Padding(4);
            this.txtcus.Name = "txtcus";
            this.txtcus.Size = new System.Drawing.Size(176, 15);
            this.txtcus.TabIndex = 0;
            this.txtcus.ClickCellAfter += new IvyBack.cons.MyTextBox.ClickCellHandler(this.txtcus_ClickCellAfter);
            this.txtcus.TextChanged += new System.EventHandler(this.txtcus_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(56, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "转入客户：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(477, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "应收转应收";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(368, 107);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "转出客户：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(394, 147);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "审核人：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.Maroon;
            this.label12.Location = new System.Drawing.Point(691, 110);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "对冲金额：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(83, 199);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "备注：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(58, 63);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "制单日期：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(394, 65);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "制单人：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(56, 147);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "审核日期：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(717, 67);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "单号：";
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
            this.toolStrip1.Size = new System.Drawing.Size(1006, 40);
            this.toolStrip1.TabIndex = 11;
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
            this.tsbExit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
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
            // editGrid1
            // 
            this.editGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editGrid1.Editing = false;
            this.editGrid1.Enabled = false;
            this.editGrid1.IsAutoAddRow = false;
            this.editGrid1.IsShowIco = true;
            this.editGrid1.Location = new System.Drawing.Point(0, 721);
            this.editGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.editGrid1.Name = "editGrid1";
            this.editGrid1.RowHeight = 25;
            this.editGrid1.Size = new System.Drawing.Size(1006, 0);
            this.editGrid1.TabIndex = 10;
            this.editGrid1.Text = "editGrid1";
            this.editGrid1.Visible = false;
            this.editGrid1.CellEndEdit += new IvyBack.cons.EditGrid.CellEndEditHandler(this.editGrid1_CellEndEdit);
            this.editGrid1.ClickCell += new IvyBack.cons.EditGrid.ClickCellHandler(this.editGrid1_ClickCell);
            this.editGrid1.DoubleClick += new System.EventHandler(this.editGrid1_DoubleClick);
            // 
            // frmArToAr
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.editGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmArToAr";
            this.Text = "应收冲应付";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private cons.MyTextBox txtoper_man;
        private cons.DateTextBox txtoper_date;
        private cons.MyTextBox txtsheet_no;
        private cons.MyTextBox txtapprove_date;
        private cons.MyTextBox txtapprove_man;
        private cons.MyTextBox txt_to_amount;
        private cons.MyTextBox txtmemo;
        private cons.MyTextBox txtsup;
        private cons.MyTextBox txtcus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private cons.EditGrid editGrid1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripDropDownButton tsdbPrintMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrintV;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrintStyle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}