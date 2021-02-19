namespace IvyBack.VoucherForm
{
    partial class frmSheetTypeInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSheetTypeInfo));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dataGrid1 = new IvyBack.cons.DataGrid();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.dataGrid2 = new IvyBack.cons.DataGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 141);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1006, 4);
            this.splitter1.TabIndex = 14;
            this.splitter1.TabStop = false;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGrid1.IsSelect = false;
            this.dataGrid1.Location = new System.Drawing.Point(0, 40);
            this.dataGrid1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid1.MergeCell = true;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeight = 25;
            this.dataGrid1.Size = new System.Drawing.Size(1006, 101);
            this.dataGrid1.TabIndex = 13;
            this.dataGrid1.Text = "dataGrid1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::IvyBack.Properties.Resources.new_导航;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1006, 40);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
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
            // dataGrid2
            // 
            this.dataGrid2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dataGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid2.IsSelect = false;
            this.dataGrid2.Location = new System.Drawing.Point(0, 145);
            this.dataGrid2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid2.MergeCell = true;
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.RowHeight = 25;
            this.dataGrid2.Size = new System.Drawing.Size(1006, 576);
            this.dataGrid2.TabIndex = 15;
            this.dataGrid2.Text = "dataGrid2";
            // 
            // frmSheetTypeInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.dataGrid2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSheetTypeInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单据明细";
            this.Load += new System.EventHandler(this.frmSheetTypeInfo_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        public cons.DataGrid dataGrid1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbExit;
        public cons.DataGrid dataGrid2;
    }
}