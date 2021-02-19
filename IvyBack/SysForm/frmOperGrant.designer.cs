namespace IvyBack.SysForm
{
    partial class frmOperGrant
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOperGrant));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvType = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tvGrant = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAllCheck = new System.Windows.Forms.ToolStripButton();
            this.tsbNoCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvType);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tvGrant);
            this.splitContainer1.Size = new System.Drawing.Size(797, 475);
            this.splitContainer1.SplitterDistance = 167;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvType
            // 
            this.tvType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(240)))));
            this.tvType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvType.Font = new System.Drawing.Font("SimSun", 10F);
            this.tvType.HideSelection = false;
            this.tvType.ImageIndex = 0;
            this.tvType.ImageList = this.imageList1;
            this.tvType.Location = new System.Drawing.Point(0, 0);
            this.tvType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvType.Name = "tvType";
            this.tvType.SelectedImageIndex = 0;
            this.tvType.ShowLines = false;
            this.tvType.ShowPlusMinus = false;
            this.tvType.Size = new System.Drawing.Size(163, 471);
            this.tvType.TabIndex = 4;
            this.tvType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvType_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "group.ico");
            this.imageList1.Images.SetKeyName(1, "权限 密钥.png");
            // 
            // tvGrant
            // 
            this.tvGrant.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tvGrant.CheckBoxes = true;
            this.tvGrant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvGrant.Font = new System.Drawing.Font("SimSun", 11F);
            this.tvGrant.ImageIndex = 1;
            this.tvGrant.ImageList = this.imageList1;
            this.tvGrant.Location = new System.Drawing.Point(0, 0);
            this.tvGrant.Name = "tvGrant";
            this.tvGrant.SelectedImageIndex = 1;
            this.tvGrant.Size = new System.Drawing.Size(622, 471);
            this.tvGrant.TabIndex = 0;
            this.tvGrant.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvGrant_AfterCheck);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage =  Properties.Resources.工具栏背景;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("SimSun", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAllCheck,
            this.tsbNoCheck,
            this.toolStripSeparator2,
            this.tsbSave,
            this.toolStripSeparator1,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(797, 39);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAllCheck
            // 
            this.tsbAllCheck.Image = Properties.Resources.工具栏_全选;
            this.tsbAllCheck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAllCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAllCheck.Name = "tsbAllCheck";
            this.tsbAllCheck.Size = new System.Drawing.Size(33, 36);
            this.tsbAllCheck.Text = "全选";
            this.tsbAllCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbAllCheck.Click += new System.EventHandler(this.tsbAllCheck_Click);
            // 
            // tsbNoCheck
            // 
            this.tsbNoCheck.Image = Properties.Resources.工具栏_全不选;
            this.tsbNoCheck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNoCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNoCheck.Name = "tsbNoCheck";
            this.tsbNoCheck.Size = new System.Drawing.Size(45, 36);
            this.tsbNoCheck.Text = "全不选";
            this.tsbNoCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbNoCheck.Click += new System.EventHandler(this.tsbNoCheck_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = Properties.Resources.new_保存;
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(33, 36);
            this.tsbSave.Text = "保存";
            this.tsbSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbExit
            // 
            this.tsbExit.Image = Properties.Resources.new_退出;
            this.tsbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(51, 36);
            this.tsbExit.Text = "关闭Esc";
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // frmOperGrant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 514);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmOperGrant";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "功能权限";
            this.Load += new System.EventHandler(this.frmOperGrant_Load);
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

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.TreeView tvGrant;
        private System.Windows.Forms.ToolStripButton tsbAllCheck;
        private System.Windows.Forms.ToolStripButton tsbNoCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView tvType;
    }
}