﻿namespace IvyBack.VoucherForm
{
    partial class frmCashOrderList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCashOrderList));
            this.dataGrid1 = new IvyBack.cons.DataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.myTextBox1 = new IvyBack.cons.MyTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTextBox2 = new IvyBack.cons.DateTextBox();
            this.dateTextBox1 = new IvyBack.cons.DateTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.IsSelect = false;
            this.dataGrid1.Location = new System.Drawing.Point(0, 99);
            this.dataGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid1.MergeCell = true;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeight = 25;
            this.dataGrid1.Size = new System.Drawing.Size(1006, 512);
            this.dataGrid1.TabIndex = 21;
            this.dataGrid1.Text = "dataGrid1";
            this.dataGrid1.DoubleClickCell += new IvyBack.cons.DataGrid.DoubleClickCellHandler(this.dataGrid1_DoubleClickCell);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.panel1.Controls.Add(this.btnSelect);
            this.panel1.Controls.Add(this.myTextBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dateTextBox2);
            this.panel1.Controls.Add(this.dateTextBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 59);
            this.panel1.TabIndex = 20;
            // 
            // btnSelect
            // 
            this.btnSelect.BackgroundImage = global::IvyBack.Properties.Resources.功能按钮;
            this.btnSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelect.Font = new System.Drawing.Font("宋体", 10F);
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(899, 7);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(94, 38);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.Text = "查询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // myTextBox1
            // 
            this.myTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.myTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.myTextBox1.Location = new System.Drawing.Point(708, 21);
            this.myTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.myTextBox1.Name = "myTextBox1";
            this.myTextBox1.Size = new System.Drawing.Size(169, 14);
            this.myTextBox1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(648, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "账户：";
            // 
            // dateTextBox2
            // 
            this.dateTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.dateTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dateTextBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateTextBox2.Location = new System.Drawing.Point(415, 20);
            this.dateTextBox2.Margin = new System.Windows.Forms.Padding(4);
            this.dateTextBox2.Name = "dateTextBox2";
            this.dateTextBox2.Size = new System.Drawing.Size(210, 14);
            this.dateTextBox2.TabIndex = 1;
            // 
            // dateTextBox1
            // 
            this.dateTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.dateTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dateTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateTextBox1.Location = new System.Drawing.Point(103, 20);
            this.dateTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTextBox1.Name = "dateTextBox1";
            this.dateTextBox1.Size = new System.Drawing.Size(210, 14);
            this.dateTextBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(327, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "起始日期：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "起始日期：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::IvyBack.Properties.Resources.new_导航;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbUpload,
            this.tsbDel,
            this.tsbCheck,
            this.toolStripButton2,
            this.toolStripSeparator4,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1006, 40);
            this.toolStrip1.TabIndex = 19;
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
            // tsbUpload
            // 
            this.tsbUpload.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbUpload.Image = global::IvyBack.Properties.Resources.new_修改;
            this.tsbUpload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpload.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbUpload.Name = "tsbUpload";
            this.tsbUpload.Size = new System.Drawing.Size(51, 40);
            this.tsbUpload.Text = "修改F3";
            this.tsbUpload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbUpload.Click += new System.EventHandler(this.tsbUpload_Click);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
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
            // tsbExit
            // 
            this.tsbExit.AutoSize = false;
            this.tsbExit.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbExit.Image = global::IvyBack.Properties.Resources.new_退出;
            this.tsbExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(58, 37);
            this.tsbExit.Text = "关闭Esc";
            this.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // frmCashOrderList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1006, 611);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCashOrderList";
            this.Text = "现金银行转账单";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private cons.DataGrid dataGrid1;
        private System.Windows.Forms.Panel panel1;
        private cons.MyTextBox myTextBox1;
        private System.Windows.Forms.Label label3;
        private cons.DateTextBox dateTextBox2;
        private cons.DateTextBox dateTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbUpload;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}