namespace IvyBack.SysForm
{
    partial class frmRegisterList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegisterList));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.机号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ip地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机器码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机器名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机号类型 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.机号,
            this.Ip地址,
            this.机器码,
            this.机器名,
            this.机号类型});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(577, 313);
            this.dgv.TabIndex = 9;
            // 
            // 机号
            // 
            this.机号.DataPropertyName = "jh";
            this.机号.FillWeight = 45F;
            this.机号.HeaderText = "机号";
            this.机号.Name = "机号";
            this.机号.ReadOnly = true;
            // 
            // Ip地址
            // 
            this.Ip地址.DataPropertyName = "ip";
            this.Ip地址.FillWeight = 80F;
            this.Ip地址.HeaderText = "Ip地址";
            this.Ip地址.Name = "Ip地址";
            this.Ip地址.ReadOnly = true;
            // 
            // 机器码
            // 
            this.机器码.DataPropertyName = "other";
            this.机器码.HeaderText = "机器码";
            this.机器码.Name = "机器码";
            this.机器码.ReadOnly = true;
            // 
            // 机器名
            // 
            this.机器名.DataPropertyName = "comp_name";
            this.机器名.HeaderText = "机器名";
            this.机器名.Name = "机器名";
            this.机器名.ReadOnly = true;
            // 
            // 机号类型
            // 
            this.机号类型.DataPropertyName = "softpos";
            this.机号类型.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.机号类型.FillWeight = 70F;
            this.机号类型.HeaderText = "机号类型";
            this.机号类型.Name = "机号类型";
            this.机号类型.ReadOnly = true;
            this.机号类型.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.机号类型.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.btnClose.Location = new System.Drawing.Point(486, 321);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 39);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.btnLogout.Location = new System.Drawing.Point(394, 321);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(85, 39);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "注销";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // frmRegisterList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(577, 369);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRegisterList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "站点管理";
            this.Load += new System.EventHandler(this.frmRegisterList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机号;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机器码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机器名;
        private System.Windows.Forms.DataGridViewComboBoxColumn 机号类型;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLogout;
    }
}