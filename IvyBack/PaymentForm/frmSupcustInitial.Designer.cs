namespace IvyBack.BaseForm
{
    partial class frmSupcustInitial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSupcustInitial));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSup = new IvyBack.cons.EditGrid();
            this.myPanel2 = new IvyBack.cons.MyPanel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnTra = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblMaxIndex = new System.Windows.Forms.Label();
            this.lblIndex = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtKeyword = new IvyBack.cons.MyTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.myPanel2.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSup);
            this.splitContainer1.Panel2.Controls.Add(this.myPanel2);
            this.splitContainer1.Panel2.Controls.Add(this.myPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(916, 462);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 5;
            // 
            // dgvSup
            // 
            this.dgvSup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSup.Editing = false;
            this.dgvSup.IsAutoAddRow = true;
            this.dgvSup.IsShowIco = false;
            this.dgvSup.Location = new System.Drawing.Point(0, 40);
            this.dgvSup.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSup.Name = "dgvSup";
            this.dgvSup.RowHeight = 25;
            this.dgvSup.Size = new System.Drawing.Size(912, 386);
            this.dgvSup.TabIndex = 3;
            this.dgvSup.Text = "editGrid1";
            this.dgvSup.CellEndEdit += new IvyBack.cons.EditGrid.CellEndEditHandler(this.dgvSup_CellEndEdit);
            this.dgvSup.ClickCell += new IvyBack.cons.EditGrid.ClickCellHandler(this.dgvSup_ClickCell);
            // 
            // myPanel2
            // 
            this.myPanel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.myPanel2.Controls.Add(this.btnHome);
            this.myPanel2.Controls.Add(this.btnPre);
            this.myPanel2.Controls.Add(this.btnTra);
            this.myPanel2.Controls.Add(this.btnNext);
            this.myPanel2.Controls.Add(this.lblMaxIndex);
            this.myPanel2.Controls.Add(this.lblIndex);
            this.myPanel2.Controls.Add(this.label2);
            this.myPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.myPanel2.Location = new System.Drawing.Point(0, 426);
            this.myPanel2.Name = "myPanel2";
            this.myPanel2.Size = new System.Drawing.Size(912, 32);
            this.myPanel2.TabIndex = 2;
            // 
            // btnHome
            // 
            this.btnHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.ForeColor = System.Drawing.Color.Black;
            this.btnHome.Location = new System.Drawing.Point(578, 2);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(80, 30);
            this.btnHome.TabIndex = 14;
            this.btnHome.Text = "首页";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Visible = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnPre
            // 
            this.btnPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPre.ForeColor = System.Drawing.Color.Black;
            this.btnPre.Location = new System.Drawing.Point(660, 2);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(80, 30);
            this.btnPre.TabIndex = 13;
            this.btnPre.Text = "上一页";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Visible = false;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnTra
            // 
            this.btnTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTra.ForeColor = System.Drawing.Color.Black;
            this.btnTra.Location = new System.Drawing.Point(824, 2);
            this.btnTra.Name = "btnTra";
            this.btnTra.Size = new System.Drawing.Size(80, 30);
            this.btnTra.TabIndex = 12;
            this.btnTra.Text = "尾页";
            this.btnTra.UseVisualStyleBackColor = true;
            this.btnTra.Visible = false;
            this.btnTra.Click += new System.EventHandler(this.btnTra_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(742, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 30);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblMaxIndex
            // 
            this.lblMaxIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxIndex.ForeColor = System.Drawing.Color.Black;
            this.lblMaxIndex.Location = new System.Drawing.Point(521, 6);
            this.lblMaxIndex.Name = "lblMaxIndex";
            this.lblMaxIndex.Size = new System.Drawing.Size(42, 19);
            this.lblMaxIndex.TabIndex = 10;
            this.lblMaxIndex.Text = "1";
            this.lblMaxIndex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMaxIndex.Visible = false;
            // 
            // lblIndex
            // 
            this.lblIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIndex.ForeColor = System.Drawing.Color.Black;
            this.lblIndex.Location = new System.Drawing.Point(451, 6);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(42, 19);
            this.lblIndex.TabIndex = 9;
            this.lblIndex.Text = "1";
            this.lblIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblIndex.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(499, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.myPanel1.Controls.Add(this.checkBox1);
            this.myPanel1.Controls.Add(this.btnSelect);
            this.myPanel1.Controls.Add(this.txtKeyword);
            this.myPanel1.Controls.Add(this.label1);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(912, 40);
            this.myPanel1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(40, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 18);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnSelect
            // 
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("宋体", 10F);
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(547, 4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(100, 30);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "查询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtKeyword.Font = new System.Drawing.Font("宋体", 10F);
            this.txtKeyword.ForeColor = System.Drawing.Color.Black;
            this.txtKeyword.Location = new System.Drawing.Point(324, 7);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(191, 23);
            this.txtKeyword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(241, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号/名称:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "openfold.bmp");
            this.imageList1.Images.SetKeyName(1, "closefold.bmp");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::IvyBack.Properties.Resources.new_导航;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripButton4,
            this.toolStripButton3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(916, 40);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
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
            this.tsbSave.ToolTipText = "保存";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton4.Image = global::IvyBack.Properties.Resources.new_新增;
            this.toolStripButton4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(37, 40);
            this.toolStripButton4.Text = "添加";
            this.toolStripButton4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButton4.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.AutoSize = false;
            this.toolStripButton3.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton3.Image = global::IvyBack.Properties.Resources.new_删除;
            this.toolStripButton3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(37, 40);
            this.toolStripButton3.Text = "删除";
            this.toolStripButton3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.AutoSize = false;
            this.toolStripButton1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton1.Image = global::IvyBack.Properties.Resources.new_审核;
            this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(37, 40);
            this.toolStripButton1.Text = "审核";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            this.tsbExit.Size = new System.Drawing.Size(37, 40);
            this.tsbExit.Text = "关闭";
            this.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // frmSupcustInitial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 502);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSupcustInitial";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "供应商期初";
            this.Load += new System.EventHandler(this.frmSupcustInitial_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSupcustInitial_KeyDown);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.myPanel2.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private cons.MyPanel myPanel1;
        private cons.MyTextBox txtKeyword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnSelect;
        private cons.MyPanel myPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.Label lblMaxIndex;
        private System.Windows.Forms.Button btnTra;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private cons.EditGrid dgvSup;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton tsbExit;
    }
}