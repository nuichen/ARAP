namespace IvyBack.PrintForm
{
    partial class frmPrintManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintManager));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGrid1 = new IvyBack.cons.DataGrid();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.myPanel3 = new IvyBack.cons.MyPanel();
            this.myPanel2 = new IvyBack.cons.MyPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrintV = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.myPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel1.Controls.Add(this.dataGrid1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.myPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.myPanel2, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(566, 398);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColor = System.Drawing.Color.White;
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Location = new System.Drawing.Point(3, 82);
            this.dataGrid1.MergeCell = true;
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeight = 25;
            this.dataGrid1.Size = new System.Drawing.Size(464, 313);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.Text = "dataGrid1";
            this.dataGrid1.DoubleClickCell += new IvyBack.cons.DataGrid.DoubleClickCellHandler(this.dataGrid1_DoubleClickCell);
            // 
            // myPanel1
            // 
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.SetColumnSpan(this.myPanel1, 2);
            this.myPanel1.Controls.Add(this.tableLayoutPanel2);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(3, 3);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(560, 73);
            this.myPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.BackgroundImage = Properties.Resources.系统参数背景;
            this.tableLayoutPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.myPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.62992F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.37008F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(560, 73);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // myPanel3
            // 
            this.myPanel3.BackgroundImage = Properties.Resources.报表样式维护文字;
            this.myPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.myPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel3.Location = new System.Drawing.Point(185, 3);
            this.myPanel3.Name = "myPanel3";
            this.tableLayoutPanel2.SetRowSpan(this.myPanel3, 3);
            this.myPanel3.Size = new System.Drawing.Size(190, 67);
            this.myPanel3.TabIndex = 0;
            // 
            // myPanel2
            // 
            this.myPanel2.Controls.Add(this.groupBox1);
            this.myPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel2.Location = new System.Drawing.Point(473, 82);
            this.myPanel2.Name = "myPanel2";
            this.myPanel2.Size = new System.Drawing.Size(90, 313);
            this.myPanel2.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnPrintV);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnDel);
            this.groupBox1.Controls.Add(this.btnChange);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnDefault);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Location = new System.Drawing.Point(0, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(90, 320);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // btnPrintV
            // 
            this.btnPrintV.BackgroundImage = Properties.Resources.功能按钮;
            this.btnPrintV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrintV.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintV.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnPrintV.ForeColor = System.Drawing.Color.Black;
            this.btnPrintV.Location = new System.Drawing.Point(6, 45);
            this.btnPrintV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintV.Name = "btnPrintV";
            this.btnPrintV.Size = new System.Drawing.Size(78, 23);
            this.btnPrintV.TabIndex = 19;
            this.btnPrintV.Text = "预览";
            this.btnPrintV.UseVisualStyleBackColor = true;
            this.btnPrintV.Click += new System.EventHandler(this.btnPrintV_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = Properties.Resources.功能按钮;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(6, 179);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(78, 23);
            this.btnExit.TabIndex = 18;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDel
            // 
            this.btnDel.BackgroundImage = Properties.Resources.功能按钮;
            this.btnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDel.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnDel.ForeColor = System.Drawing.Color.Black;
            this.btnDel.Location = new System.Drawing.Point(6, 152);
            this.btnDel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(78, 23);
            this.btnDel.TabIndex = 17;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnChange
            // 
            this.btnChange.BackgroundImage = Properties.Resources.功能按钮;
            this.btnChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChange.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnChange.ForeColor = System.Drawing.Color.Black;
            this.btnChange.Location = new System.Drawing.Point(6, 125);
            this.btnChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(78, 23);
            this.btnChange.TabIndex = 16;
            this.btnChange.Text = "修改";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = Properties.Resources.功能按钮;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(6, 98);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(78, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.BackgroundImage = Properties.Resources.功能按钮;
            this.btnDefault.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDefault.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnDefault.ForeColor = System.Drawing.Color.Black;
            this.btnDefault.Location = new System.Drawing.Point(6, 71);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(78, 23);
            this.btnDefault.TabIndex = 14;
            this.btnDefault.Text = "默认";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = Properties.Resources.功能按钮;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Font = new System.Drawing.Font("SimSun", 10F);
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Location = new System.Drawing.Point(6, 19);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(78, 23);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmPrintManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(566, 398);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(582, 436);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(582, 436);
            this.Name = "frmPrintManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印样式管理";
            this.Load += new System.EventHandler(this.frmPrintManager_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.myPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private cons.DataGrid dataGrid1;
        private cons.MyPanel myPanel1;
        private cons.MyPanel myPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private cons.MyPanel myPanel3;

    }
}