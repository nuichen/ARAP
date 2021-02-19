namespace IvyBack.ReportForm
{
    partial class frmBaseReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaseReport));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbExportExcel = new System.Windows.Forms.ToolStripButton();
            this.tsdbPrintMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrintV = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPrintStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.lblSelectInfo = new System.Windows.Forms.Label();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.dgv = new IvyBack.cons.DataGrid();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnTra = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblMaxIndex = new System.Windows.Forms.Label();
            this.lblIndex = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::IvyBack.Properties.Resources.new_导航;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbExportExcel,
            this.tsdbPrintMenu,
            this.toolStripSeparator4,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(877, 40);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.AutoSize = false;
            this.tsbAdd.Image = global::IvyBack.Properties.Resources.new_刷新;
            this.tsbAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(45, 40);
            this.tsbAdd.Text = "刷新F5";
            this.tsbAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbAdd.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tsbExportExcel
            // 
            this.tsbExportExcel.AutoSize = false;
            this.tsbExportExcel.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbExportExcel.Image = global::IvyBack.Properties.Resources.new_导出;
            this.tsbExportExcel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tsbExportExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportExcel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tsbExportExcel.Name = "tsbExportExcel";
            this.tsbExportExcel.Size = new System.Drawing.Size(72, 40);
            this.tsbExportExcel.Text = "导出Excel";
            this.tsbExportExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbExportExcel.Click += new System.EventHandler(this.tsbExportExcel_Click);
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
            this.tsdbPrintMenu.Visible = false;
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
            this.tsbExit.Size = new System.Drawing.Size(51, 40);
            this.tsbExit.Text = "关闭Esc";
            this.tsbExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            // 
            // lblSelectInfo
            // 
            this.lblSelectInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblSelectInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSelectInfo.Font = new System.Drawing.Font("宋体", 9.5F);
            this.lblSelectInfo.Location = new System.Drawing.Point(0, 40);
            this.lblSelectInfo.Name = "lblSelectInfo";
            this.lblSelectInfo.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.lblSelectInfo.Size = new System.Drawing.Size(877, 38);
            this.lblSelectInfo.TabIndex = 8;
            this.lblSelectInfo.Text = "查询条件:";
            // 
            // tablePanel
            // 
            this.tablePanel.AutoSize = true;
            this.tablePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.tablePanel.ColumnCount = 8;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanel.Location = new System.Drawing.Point(0, 40);
            this.tablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 3;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanel.Size = new System.Drawing.Size(877, 0);
            this.tablePanel.TabIndex = 10;
            // 
            // dgv
            // 
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.IsSelect = false;
            this.dgv.Location = new System.Drawing.Point(0, 78);
            this.dgv.MergeCell = true;
            this.dgv.Name = "dgv";
            this.dgv.RowHeight = 25;
            this.dgv.Size = new System.Drawing.Size(877, 408);
            this.dgv.TabIndex = 9;
            this.dgv.Text = "dataGrid1";
            // 
            // myPanel1
            // 
            this.myPanel1.Controls.Add(this.btnHome);
            this.myPanel1.Controls.Add(this.btnPre);
            this.myPanel1.Controls.Add(this.btnTra);
            this.myPanel1.Controls.Add(this.btnNext);
            this.myPanel1.Controls.Add(this.lblMaxIndex);
            this.myPanel1.Controls.Add(this.lblIndex);
            this.myPanel1.Controls.Add(this.label2);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.myPanel1.Location = new System.Drawing.Point(0, 486);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(877, 40);
            this.myPanel1.TabIndex = 11;
            // 
            // btnHome
            // 
            this.btnHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.ForeColor = System.Drawing.Color.Black;
            this.btnHome.Location = new System.Drawing.Point(523, 6);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(80, 30);
            this.btnHome.TabIndex = 21;
            this.btnHome.Text = "首页";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnPre
            // 
            this.btnPre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPre.ForeColor = System.Drawing.Color.Black;
            this.btnPre.Location = new System.Drawing.Point(605, 6);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(80, 30);
            this.btnPre.TabIndex = 20;
            this.btnPre.Text = "上一页";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // btnTra
            // 
            this.btnTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTra.ForeColor = System.Drawing.Color.Black;
            this.btnTra.Location = new System.Drawing.Point(770, 6);
            this.btnTra.Name = "btnTra";
            this.btnTra.Size = new System.Drawing.Size(80, 30);
            this.btnTra.TabIndex = 19;
            this.btnTra.Text = "尾页";
            this.btnTra.UseVisualStyleBackColor = true;
            this.btnTra.Click += new System.EventHandler(this.btnTra_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(687, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 30);
            this.btnNext.TabIndex = 18;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblMaxIndex
            // 
            this.lblMaxIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxIndex.Font = new System.Drawing.Font("宋体", 10F);
            this.lblMaxIndex.ForeColor = System.Drawing.Color.Black;
            this.lblMaxIndex.Location = new System.Drawing.Point(429, 13);
            this.lblMaxIndex.Name = "lblMaxIndex";
            this.lblMaxIndex.Size = new System.Drawing.Size(42, 19);
            this.lblMaxIndex.TabIndex = 17;
            this.lblMaxIndex.Text = "1";
            this.lblMaxIndex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIndex
            // 
            this.lblIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIndex.Font = new System.Drawing.Font("宋体", 10F);
            this.lblIndex.ForeColor = System.Drawing.Color.Black;
            this.lblIndex.Location = new System.Drawing.Point(361, 13);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(42, 19);
            this.lblIndex.TabIndex = 16;
            this.lblIndex.Text = "1";
            this.lblIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(409, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 19);
            this.label2.TabIndex = 15;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmBaseReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(877, 526);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.lblSelectInfo);
            this.Controls.Add(this.tablePanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.myPanel1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.KeyPreview = true;
            this.Name = "frmBaseReport";
            this.Text = "报表";
            this.Shown += new System.EventHandler(this.frmReport_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmReport_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.Label lblSelectInfo;
        private cons.MyPanel myPanel1;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button btnTra;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblMaxIndex;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        protected System.Windows.Forms.ToolStrip toolStrip1;
        protected cons.DataGrid dgv;
        protected System.Windows.Forms.TableLayoutPanel tablePanel;
        protected System.Windows.Forms.ToolStripButton tsbAdd;
        protected System.Windows.Forms.ToolStripDropDownButton tsdbPrintMenu;
        protected System.Windows.Forms.ToolStripMenuItem tsmiPrint;
        protected System.Windows.Forms.ToolStripMenuItem tsmiPrintV;
        protected System.Windows.Forms.ToolStripMenuItem tsmiPrintStyle;
        protected System.Windows.Forms.ToolStripButton tsbExportExcel;
    }
}