namespace IvyBack.PaymentForm
{
    partial class frmNoticeSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNoticeSelect));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSup = new IvyBack.cons.DataGrid();
            this.myPanel2 = new IvyBack.cons.MyPanel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.btnTra = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblMaxIndex = new System.Windows.Forms.Label();
            this.lblIndex = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.dateTextBox2 = new System.Windows.Forms.DateTimePicker();
            this.dateTextBox1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtKeyword = new IvyBack.cons.MyTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.myPanel2.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvSup);
            this.splitContainer1.Panel2.Controls.Add(this.myPanel2);
            this.splitContainer1.Panel2.Controls.Add(this.myPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(916, 502);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 5;
            // 
            // dgvSup
            // 
            this.dgvSup.BackColor = System.Drawing.Color.White;
            this.dgvSup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSup.IsSelect = true;
            this.dgvSup.Location = new System.Drawing.Point(0, 40);
            this.dgvSup.MergeCell = false;
            this.dgvSup.Name = "dgvSup";
            this.dgvSup.RowHeight = 25;
            this.dgvSup.Size = new System.Drawing.Size(912, 426);
            this.dgvSup.TabIndex = 1;
            this.dgvSup.Text = "dataGrid1";
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
            this.myPanel2.Location = new System.Drawing.Point(0, 466);
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
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.myPanel1.Controls.Add(this.dateTextBox2);
            this.myPanel1.Controls.Add(this.dateTextBox1);
            this.myPanel1.Controls.Add(this.label3);
            this.myPanel1.Controls.Add(this.label4);
            this.myPanel1.Controls.Add(this.button1);
            this.myPanel1.Controls.Add(this.btnSelect);
            this.myPanel1.Controls.Add(this.txtKeyword);
            this.myPanel1.Controls.Add(this.label1);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(912, 40);
            this.myPanel1.TabIndex = 0;
            // 
            // dateTextBox2
            // 
            this.dateTextBox2.CustomFormat = "yyyy-MM-dd";
            this.dateTextBox2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTextBox2.Location = new System.Drawing.Point(390, 6);
            this.dateTextBox2.Name = "dateTextBox2";
            this.dateTextBox2.Size = new System.Drawing.Size(139, 23);
            this.dateTextBox2.TabIndex = 6;
            // 
            // dateTextBox1
            // 
            this.dateTextBox1.CustomFormat = "yyyy-MM-dd";
            this.dateTextBox1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTextBox1.Location = new System.Drawing.Point(204, 7);
            this.dateTextBox1.Name = "dateTextBox1";
            this.dateTextBox1.Size = new System.Drawing.Size(139, 23);
            this.dateTextBox1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "结束：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "起始：";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("宋体", 10F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(652, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("宋体", 10F);
            this.btnSelect.ForeColor = System.Drawing.Color.Black;
            this.btnSelect.Location = new System.Drawing.Point(546, 4);
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
            this.txtKeyword.Location = new System.Drawing.Point(40, 8);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(117, 23);
            this.txtKeyword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "编号:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "openfold.bmp");
            this.imageList1.Images.SetKeyName(1, "closefold.bmp");
            // 
            // frmNoticeSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 502);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmNoticeSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择账期通知单";
            this.Load += new System.EventHandler(this.frmSupcust_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSupcust_KeyDown);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.myPanel2.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private cons.MyPanel myPanel1;
        private cons.MyTextBox txtKeyword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnSelect;
        private cons.DataGrid dgvSup;
        private cons.MyPanel myPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.Label lblMaxIndex;
        private System.Windows.Forms.Button btnTra;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTextBox1;
        private System.Windows.Forms.DateTimePicker dateTextBox2;
    }
}