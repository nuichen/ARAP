namespace IvyBack.FinanceForm
{
    partial class frmIncomeRevenue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIncomeRevenue));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvItem = new IvyBack.cons.EditGrid();
            this.txt_cust_id = new IvyBack.cons.MyTextBox();
            this.myPanel3 = new IvyBack.cons.MyPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.myPanel5 = new IvyBack.cons.MyPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.myPanel6 = new IvyBack.cons.MyPanel();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.btnChange = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNums = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnl_container = new System.Windows.Forms.Panel();
            this.myPanel1.SuspendLayout();
            this.pnl_container.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(124, 14);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(0, 7, 0, 7);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(109, 29);
            this.dateTimePicker1.TabIndex = 19;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(299, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 21);
            this.label3.TabIndex = 18;
            this.label3.Text = "客户:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(56, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 21);
            this.label1.TabIndex = 16;
            this.label1.Text = "月  份:";
            // 
            // dgvItem
            // 
            this.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItem.Editing = false;
            this.dgvItem.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.dgvItem.IsAutoAddRow = true;
            this.dgvItem.IsShowIco = true;
            this.dgvItem.Location = new System.Drawing.Point(0, 0);
            this.dgvItem.Name = "dgvItem";
            this.dgvItem.RowHeight = 25;
            this.dgvItem.Size = new System.Drawing.Size(1184, 600);
            this.dgvItem.TabIndex = 6;
            this.dgvItem.Text = "editGrid1";
            // 
            // txt_cust_id
            // 
            this.txt_cust_id.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txt_cust_id.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_cust_id.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_cust_id.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txt_cust_id.Location = new System.Drawing.Point(358, 17);
            this.txt_cust_id.Margin = new System.Windows.Forms.Padding(0, 7, 0, 7);
            this.txt_cust_id.Name = "txt_cust_id";
            this.txt_cust_id.Size = new System.Drawing.Size(200, 22);
            this.txt_cust_id.TabIndex = 17;
            this.txt_cust_id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_cust_id_KeyDown);
            // 
            // myPanel3
            // 
            this.myPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(178)))), ((int)(((byte)(255)))));
            this.myPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel3.Location = new System.Drawing.Point(10, 48);
            this.myPanel3.Name = "myPanel3";
            this.myPanel3.Size = new System.Drawing.Size(293, 5);
            this.myPanel3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(293, 38);
            this.label4.TabIndex = 4;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myPanel5
            // 
            this.myPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(178)))), ((int)(((byte)(255)))));
            this.myPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel5.Location = new System.Drawing.Point(10, 48);
            this.myPanel5.Name = "myPanel5";
            this.myPanel5.Size = new System.Drawing.Size(293, 5);
            this.myPanel5.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(293, 38);
            this.label5.TabIndex = 4;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myPanel6
            // 
            this.myPanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.myPanel6.Location = new System.Drawing.Point(0, 0);
            this.myPanel6.Name = "myPanel6";
            this.myPanel6.Size = new System.Drawing.Size(200, 100);
            this.myPanel6.TabIndex = 0;
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.myPanel1.Controls.Add(this.btnChange);
            this.myPanel1.Controls.Add(this.label2);
            this.myPanel1.Controls.Add(this.txtNums);
            this.myPanel1.Controls.Add(this.btnRefresh);
            this.myPanel1.Controls.Add(this.btnSave);
            this.myPanel1.Controls.Add(this.dateTimePicker1);
            this.myPanel1.Controls.Add(this.label1);
            this.myPanel1.Controls.Add(this.txt_cust_id);
            this.myPanel1.Controls.Add(this.label3);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(1184, 111);
            this.myPanel1.TabIndex = 21;
            // 
            // btnChange
            // 
            this.btnChange.BackgroundImage = global::IvyBack.Properties.Resources.功能按钮;
            this.btnChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChange.Font = new System.Drawing.Font("宋体", 11F);
            this.btnChange.ForeColor = System.Drawing.Color.Black;
            this.btnChange.Location = new System.Drawing.Point(245, 75);
            this.btnChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(100, 30);
            this.btnChange.TabIndex = 24;
            this.btnChange.Text = "修改";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(34, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 21);
            this.label2.TabIndex = 23;
            this.label2.Text = "统一修改:";
            // 
            // txtNums
            // 
            this.txtNums.Location = new System.Drawing.Point(124, 76);
            this.txtNums.Name = "txtNums";
            this.txtNums.Size = new System.Drawing.Size(109, 29);
            this.txtNums.TabIndex = 22;
            this.txtNums.Text = "0";
            this.txtNums.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = global::IvyBack.Properties.Resources.功能按钮;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 11F);
            this.btnRefresh.ForeColor = System.Drawing.Color.Black;
            this.btnRefresh.Location = new System.Drawing.Point(603, 13);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 21;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::IvyBack.Properties.Resources.功能按钮;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("宋体", 11F);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(740, 13);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnl_container
            // 
            this.pnl_container.Controls.Add(this.dgvItem);
            this.pnl_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_container.Location = new System.Drawing.Point(0, 111);
            this.pnl_container.Name = "pnl_container";
            this.pnl_container.Size = new System.Drawing.Size(1184, 600);
            this.pnl_container.TabIndex = 22;
            // 
            // frmIncomeRevenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1184, 711);
            this.Controls.Add(this.pnl_container);
            this.Controls.Add(this.myPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(0, 7, 0, 7);
            this.MinimumSize = new System.Drawing.Size(1100, 750);
            this.Name = "frmIncomeRevenue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户预计收入";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmIncomeRevenue_Shown);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.pnl_container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private cons.MyTextBox txt_cust_id;
        private System.Windows.Forms.Label label1;
        private cons.EditGrid dgvItem;
        private cons.MyPanel myPanel3;
        private System.Windows.Forms.Label label4;
        private cons.MyPanel myPanel5;
        private System.Windows.Forms.Label label5;
        private cons.MyPanel myPanel6;
        private cons.MyPanel myPanel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnl_container;
        private System.Windows.Forms.TextBox txtNums;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChange;
    }
}