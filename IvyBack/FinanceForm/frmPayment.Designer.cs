namespace IvyBack.FinanceForm
{
    partial class frmPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayment));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCreate = new System.Windows.Forms.ToolStripButton();
            this.tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.dgvPay = new IvyBack.cons.DataGrid();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::IvyBack.Properties.Resources.new_导航;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 10F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCreate,
            this.tsbUpload,
            this.tsbDel,
            this.toolStripSeparator4,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(757, 40);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCreate
            // 
            this.tsbCreate.AutoSize = false;
            this.tsbCreate.Image = global::IvyBack.Properties.Resources.new_新增;
            this.tsbCreate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbCreate.Name = "tsbCreate";
            this.tsbCreate.Size = new System.Drawing.Size(53, 40);
            this.tsbCreate.Text = "新建F2";
            this.tsbCreate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbCreate.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbCreate.Click += new System.EventHandler(this.tsbCreate_Click);
            // 
            // tsbUpload
            // 
            this.tsbUpload.AutoSize = false;
            this.tsbUpload.Image = global::IvyBack.Properties.Resources.new_修改;
            this.tsbUpload.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpload.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbUpload.Name = "tsbUpload";
            this.tsbUpload.Size = new System.Drawing.Size(53, 40);
            this.tsbUpload.Text = "修改F3";
            this.tsbUpload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbUpload.Click += new System.EventHandler(this.tsbUpload_Click);
            // 
            // tsbDel
            // 
            this.tsbDel.AutoSize = false;
            this.tsbDel.Image = global::IvyBack.Properties.Resources.new_删除;
            this.tsbDel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbDel.Name = "tsbDel";
            this.tsbDel.Size = new System.Drawing.Size(53, 40);
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
            // tsbExit
            // 
            this.tsbExit.AutoSize = false;
            this.tsbExit.Image = global::IvyBack.Properties.Resources.new_退出;
            this.tsbExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(60, 40);
            this.tsbExit.Text = "关闭ESC";
            this.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // dgvPay
            // 
            this.dgvPay.BackColor = System.Drawing.Color.White;
            this.dgvPay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPay.IsSelect = false;
            this.dgvPay.Location = new System.Drawing.Point(0, 80);
            this.dgvPay.MergeCell = true;
            this.dgvPay.Name = "dgvPay";
            this.dgvPay.RowHeight = 25;
            this.dgvPay.Size = new System.Drawing.Size(757, 388);
            this.dgvPay.TabIndex = 9;
            this.dgvPay.Text = "dataGrid1";
            this.dgvPay.DoubleClickCell += new IvyBack.cons.DataGrid.DoubleClickCellHandler(this.dgvPay_DoubleClickCell);
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.myPanel1.Controls.Add(this.btnSelect);
            this.myPanel1.Controls.Add(this.txtKeyword);
            this.myPanel1.Controls.Add(this.label1);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel1.Location = new System.Drawing.Point(0, 40);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(757, 40);
            this.myPanel1.TabIndex = 8;
            // 
            // btnSelect
            // 
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("宋体", 10F);
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSelect.Location = new System.Drawing.Point(281, 5);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(100, 30);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "查询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Font = new System.Drawing.Font("宋体", 10F);
            this.txtKeyword.ForeColor = System.Drawing.Color.Black;
            this.txtKeyword.Location = new System.Drawing.Point(90, 10);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(160, 23);
            this.txtKeyword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "编码/名称:";
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 468);
            this.Controls.Add(this.dgvPay);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结算方式";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPayment_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbUpload;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private cons.DataGrid dgvPay;
        private cons.MyPanel myPanel1;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbCreate;
        private System.Windows.Forms.Button btnSelect;
    }
}