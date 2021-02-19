namespace IvyBack.SysForm
{
    partial class frmOper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOper));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dgvOper = new IvyBack.cons.DataGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.tsbIniPwd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddType = new System.Windows.Forms.ToolStripButton();
            this.tsbChangeType = new System.Windows.Forms.ToolStripButton();
            this.tsbDelType = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.tsbFlush = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvOper);
            this.splitContainer1.Size = new System.Drawing.Size(805, 440);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 6;
            // 
            // tv
            // 
            this.tv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.Font = new System.Drawing.Font("宋体", 10F);
            this.tv.HideSelection = false;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.imageList1;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tv.Name = "tv";
            this.tv.SelectedImageIndex = 0;
            this.tv.Size = new System.Drawing.Size(146, 436);
            this.tv.TabIndex = 3;
            this.tv.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "group.ico");
            // 
            // dgvOper
            // 
            this.dgvOper.BackColor = System.Drawing.Color.White;
            this.dgvOper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOper.Font = new System.Drawing.Font("宋体", 10F);
            this.dgvOper.IsSelect = false;
            this.dgvOper.Location = new System.Drawing.Point(0, 0);
            this.dgvOper.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvOper.MergeCell = true;
            this.dgvOper.Name = "dgvOper";
            this.dgvOper.RowHeight = 25;
            this.dgvOper.Size = new System.Drawing.Size(649, 436);
            this.dgvOper.TabIndex = 1;
            this.dgvOper.Text = "dataGrid1";
            this.dgvOper.DoubleClickCell += new IvyBack.cons.DataGrid.DoubleClickCellHandler(this.dgvBranch_DoubleClickCell);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::IvyBack.Properties.Resources.工具栏背景;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFlush,
            this.tsbAdd,
            this.tsbUpload,
            this.tsbDel,
            this.tsbIniPwd,
            this.toolStripSeparator1,
            this.tsbAddType,
            this.tsbChangeType,
            this.tsbDelType,
            this.toolStripSeparator4,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(805, 39);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = global::IvyBack.Properties.Resources.new_新增;
            this.tsbAdd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(45, 36);
            this.tsbAdd.Text = "新建F2";
            this.tsbAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbUpload
            // 
            this.tsbUpload.Image = global::IvyBack.Properties.Resources.new_修改;
            this.tsbUpload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpload.Name = "tsbUpload";
            this.tsbUpload.Size = new System.Drawing.Size(45, 36);
            this.tsbUpload.Text = "修改F3";
            this.tsbUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbUpload.Click += new System.EventHandler(this.tsbUpload_Click);
            // 
            // tsbDel
            // 
            this.tsbDel.Image = global::IvyBack.Properties.Resources.new_删除;
            this.tsbDel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDel.Name = "tsbDel";
            this.tsbDel.Size = new System.Drawing.Size(45, 36);
            this.tsbDel.Text = "删除F4";
            this.tsbDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDel.Click += new System.EventHandler(this.tsbDel_Click);
            // 
            // tsbIniPwd
            // 
            this.tsbIniPwd.Image = global::IvyBack.Properties.Resources.初始化01;
            this.tsbIniPwd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbIniPwd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbIniPwd.Name = "tsbIniPwd";
            this.tsbIniPwd.Size = new System.Drawing.Size(69, 36);
            this.tsbIniPwd.Text = "重置密码F5";
            this.tsbIniPwd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbIniPwd.Click += new System.EventHandler(this.tsbIniPwd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbAddType
            // 
            this.tsbAddType.Image = global::IvyBack.Properties.Resources.工具栏_新建组;
            this.tsbAddType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAddType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddType.Name = "tsbAddType";
            this.tsbAddType.Size = new System.Drawing.Size(45, 36);
            this.tsbAddType.Text = "新建组";
            this.tsbAddType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAddType.Click += new System.EventHandler(this.tsbAddType_Click);
            // 
            // tsbChangeType
            // 
            this.tsbChangeType.Image = global::IvyBack.Properties.Resources.工具栏_修改组;
            this.tsbChangeType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbChangeType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChangeType.Name = "tsbChangeType";
            this.tsbChangeType.Size = new System.Drawing.Size(45, 36);
            this.tsbChangeType.Text = "修改组";
            this.tsbChangeType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbChangeType.Click += new System.EventHandler(this.tsbChangeType_Click);
            // 
            // tsbDelType
            // 
            this.tsbDelType.Image = global::IvyBack.Properties.Resources.工具栏_删除组;
            this.tsbDelType.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDelType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelType.Name = "tsbDelType";
            this.tsbDelType.Size = new System.Drawing.Size(45, 36);
            this.tsbDelType.Text = "删除组";
            this.tsbDelType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDelType.Click += new System.EventHandler(this.tsbDelType_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = global::IvyBack.Properties.Resources.new_退出;
            this.tsbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(51, 36);
            this.tsbExit.Text = "关闭Esc";
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // tsbFlush
            // 
            this.tsbFlush.Image = global::IvyBack.Properties.Resources.初始化01;
            this.tsbFlush.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFlush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFlush.Name = "tsbFlush";
            this.tsbFlush.Size = new System.Drawing.Size(45, 36);
            this.tsbFlush.Text = "刷新F5";
            this.tsbFlush.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbFlush.Click += new System.EventHandler(this.tsbFlush_Click);
            // 
            // frmOper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 479);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.Name = "frmOper";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "操作员设置";
            this.Load += new System.EventHandler(this.frmBranch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBranch_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbUpload;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv;
        private cons.DataGrid dgvOper;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tsbIniPwd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbAddType;
        private System.Windows.Forms.ToolStripButton tsbChangeType;
        private System.Windows.Forms.ToolStripButton tsbDelType;
        private System.Windows.Forms.ToolStripButton tsbFlush;
    }
}