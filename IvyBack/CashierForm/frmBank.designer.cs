namespace IvyBack.FinanceForm
{
    partial class frmBank
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBank));
            this.dgvBank = new IvyBack.cons.DataGrid();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCreate = new System.Windows.Forms.ToolStripButton();
            this.tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.myPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvBank
            // 
            this.dgvBank.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.dgvBank, "dgvBank");
            this.dgvBank.IsSelect = false;
            this.dgvBank.MergeCell = true;
            this.dgvBank.Name = "dgvBank";
            this.dgvBank.RowHeight = 25;
            this.dgvBank.DoubleClickCell += new IvyBack.cons.DataGrid.DoubleClickCellHandler(this.dgvBank_DoubleClickCell);
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.myPanel1.Controls.Add(this.btnSelect);
            this.myPanel1.Controls.Add(this.txtKeyword);
            this.myPanel1.Controls.Add(this.label1);
            resources.ApplyResources(this.myPanel1, "myPanel1");
            this.myPanel1.Name = "myPanel1";
            // 
            // btnSelect
            // 
            resources.ApplyResources(this.btnSelect, "btnSelect");
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtKeyword
            // 
            resources.ApplyResources(this.txtKeyword, "txtKeyword");
            this.txtKeyword.ForeColor = System.Drawing.Color.Black;
            this.txtKeyword.Name = "txtKeyword";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::IvyBack.Properties.Resources.new_导航;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCreate,
            this.tsbUpload,
            this.tsbDel,
            this.toolStripSeparator4,
            this.tsbExit});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbCreate
            // 
            resources.ApplyResources(this.tsbCreate, "tsbCreate");
            this.tsbCreate.Image = global::IvyBack.Properties.Resources.new_新增;
            this.tsbCreate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbCreate.Name = "tsbCreate";
            this.tsbCreate.Click += new System.EventHandler(this.tsbCreate_Click);
            // 
            // tsbUpload
            // 
            resources.ApplyResources(this.tsbUpload, "tsbUpload");
            this.tsbUpload.Image = global::IvyBack.Properties.Resources.new_修改;
            this.tsbUpload.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbUpload.Name = "tsbUpload";
            this.tsbUpload.Click += new System.EventHandler(this.tsbUpload_Click);
            // 
            // tsbDel
            // 
            resources.ApplyResources(this.tsbDel, "tsbDel");
            this.tsbDel.Image = global::IvyBack.Properties.Resources.new_删除;
            this.tsbDel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbDel.Name = "tsbDel";
            this.tsbDel.Click += new System.EventHandler(this.tsbDel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // tsbExit
            // 
            resources.ApplyResources(this.tsbExit, "tsbExit");
            this.tsbExit.Image = global::IvyBack.Properties.Resources.new_退出;
            this.tsbExit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // frmBank
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvBank);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.toolStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.Name = "frmBank";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmBank_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBank_KeyDown);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbUpload;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private cons.DataGrid dgvBank;
        private cons.MyPanel myPanel1;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbCreate;
        private System.Windows.Forms.Button btnSelect;
    }
}