namespace IvyBack.SysForm
{
    partial class frmOperUpdate
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtOperId = new IvyBack.cons.MyTextBox();
            this.checkIsStop = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new IvyBack.cons.MyTextBox();
            this.txtPw = new IvyBack.cons.MyTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbIsBanch = new System.Windows.Forms.CheckBox();
            this.txtBranch = new IvyBack.cons.MyTextBox();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(30, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "操作员编号:";
            // 
            // txtOperId
            // 
            this.txtOperId.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtOperId.Location = new System.Drawing.Point(120, 92);
            this.txtOperId.MaxLength = 4;
            this.txtOperId.Name = "txtOperId";
            this.txtOperId.ReadOnly = true;
            this.txtOperId.Size = new System.Drawing.Size(173, 23);
            this.txtOperId.TabIndex = 0;
            this.txtOperId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // checkIsStop
            // 
            this.checkIsStop.AutoSize = true;
            this.checkIsStop.BackColor = System.Drawing.Color.Transparent;
            this.checkIsStop.ForeColor = System.Drawing.Color.Red;
            this.checkIsStop.Location = new System.Drawing.Point(514, 222);
            this.checkIsStop.Name = "checkIsStop";
            this.checkIsStop.Size = new System.Drawing.Size(54, 18);
            this.checkIsStop.TabIndex = 6;
            this.checkIsStop.Text = "禁用";
            this.checkIsStop.UseVisualStyleBackColor = false;
            this.checkIsStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(347, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 10012;
            this.label2.Text = "角色:";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(395, 92);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(166, 21);
            this.cbType.TabIndex = 1;
            this.cbType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(30, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 14);
            this.label3.TabIndex = 10014;
            this.label3.Text = "操作员名称:";
            // 
            // txtName
            // 
            this.txtName.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtName.Location = new System.Drawing.Point(120, 157);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(173, 23);
            this.txtName.TabIndex = 2;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // txtPw
            // 
            this.txtPw.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtPw.Location = new System.Drawing.Point(395, 157);
            this.txtPw.MaxLength = 6;
            this.txtPw.Name = "txtPw";
            this.txtPw.PasswordChar = '*';
            this.txtPw.Size = new System.Drawing.Size(173, 23);
            this.txtPw.TabIndex = 3;
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(347, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 10016;
            this.label4.Text = "密码:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(44, 222);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 10018;
            this.label5.Text = "隶属机构:";
            // 
            // cbIsBanch
            // 
            this.cbIsBanch.AutoSize = true;
            this.cbIsBanch.BackColor = System.Drawing.Color.Transparent;
            this.cbIsBanch.ForeColor = System.Drawing.Color.Black;
            this.cbIsBanch.Location = new System.Drawing.Point(395, 222);
            this.cbIsBanch.Name = "cbIsBanch";
            this.cbIsBanch.Size = new System.Drawing.Size(82, 18);
            this.cbIsBanch.TabIndex = 5;
            this.cbIsBanch.Text = "是否分部";
            this.cbIsBanch.UseVisualStyleBackColor = false;
            this.cbIsBanch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // txtBranch
            // 
            this.txtBranch.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtBranch.Location = new System.Drawing.Point(120, 219);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(173, 23);
            this.txtBranch.TabIndex = 4;
            this.txtBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.myPanel1.Controls.Add(this.btnOk);
            this.myPanel1.Controls.Add(this.btnNo);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel1.ForeColor = System.Drawing.Color.Black;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(644, 40);
            this.myPanel1.TabIndex = 10010;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.Location = new System.Drawing.Point(5, 2);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 35);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.ForeColor = System.Drawing.Color.Black;
            this.btnNo.Location = new System.Drawing.Point(110, 2);
            this.btnNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(100, 35);
            this.btnNo.TabIndex = 8;
            this.btnNo.Text = "取消";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // frmOperUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(644, 292);
            this.Controls.Add(this.txtBranch);
            this.Controls.Add(this.cbIsBanch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkIsStop);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOperId);
            this.Font = new System.Drawing.Font("SimSun", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOperUpdate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "操作员";
            this.Load += new System.EventHandler(this.frmBranchUpload_Load);
            this.Shown += new System.EventHandler(this.frmBranchUpload_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBranchUpload_KeyDown);
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private cons.MyTextBox txtOperId;
        private cons.MyPanel myPanel1;
        private System.Windows.Forms.CheckBox checkIsStop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label3;
        private cons.MyTextBox txtName;
        private cons.MyTextBox txtPw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbIsBanch;
        private cons.MyTextBox txtBranch;
    }
}