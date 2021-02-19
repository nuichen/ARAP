namespace IvyBack.SysForm
{
    partial class frmRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegister));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.机号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ip地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机器码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机器名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.机号类型 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.btnOK.Location = new System.Drawing.Point(341, 234);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(89, 45);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "注册";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            this.btnClose.Location = new System.Drawing.Point(436, 234);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 45);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.dgv.Size = new System.Drawing.Size(531, 214);
            this.dgv.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.label1.Location = new System.Drawing.Point(12, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 21);
            this.label1.TabIndex = 9;
            this.label1.Text = "机号:";
            // 
            // txt
            // 
            this.txt.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.txt.Location = new System.Drawing.Point(64, 234);
            this.txt.MaxLength = 4;
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(117, 29);
            this.txt.TabIndex = 10;
            this.txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(13, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "请输入要增加的机器号,机器号为4位字符";
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
            // frmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(531, 293);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "机器注册";
            this.Load += new System.EventHandler(this.frmRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机号;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机器码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 机器名;
        private System.Windows.Forms.DataGridViewComboBoxColumn 机号类型;
    }
}