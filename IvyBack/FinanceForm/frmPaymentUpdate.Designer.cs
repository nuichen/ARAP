namespace IvyBack.FinanceForm
{
    partial class frmPaymentUpdate
    {
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private cons.MyTextBox txtPaynum;
        private System.Windows.Forms.Label label2;
        private cons.MyTextBox txtPayName;
        private cons.MyPanel myPanel1;
        private System.Windows.Forms.Label label3;
        private cons.MyTextBox txtvisa;
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
            this.btnNo = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPayName = new IvyBack.cons.MyTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPaynum = new IvyBack.cons.MyTextBox();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtvisa = new IvyBack.cons.MyTextBox();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Location = new System.Drawing.Point(110, 2);
            this.btnNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(100, 35);
            this.btnNo.TabIndex = 10004;
            this.btnNo.Text = "取消";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(5, 2);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 35);
            this.btnOk.TabIndex = 10003;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(95, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "名称:";
            // 
            // txtPayName
            // 
            this.txtPayName.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtPayName.Location = new System.Drawing.Point(149, 164);
            this.txtPayName.MaxLength = 50;
            this.txtPayName.Name = "txtPayName";
            this.txtPayName.Size = new System.Drawing.Size(253, 23);
            this.txtPayName.TabIndex = 1;
            this.txtPayName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtPayName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPayName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(95, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "编号:";
            // 
            // txtPaynum
            // 
            this.txtPaynum.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtPaynum.Location = new System.Drawing.Point(149, 94);
            this.txtPaynum.MaxLength = 2;
            this.txtPaynum.Name = "txtPaynum";
            this.txtPaynum.Size = new System.Drawing.Size(253, 23);
            this.txtPaynum.TabIndex = 0;
            this.txtPaynum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtPaynum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPaynum_KeyPress);
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.myPanel1.Controls.Add(this.btnOk);
            this.myPanel1.Controls.Add(this.btnNo);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(524, 40);
            this.myPanel1.TabIndex = 10006;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(67, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "现金银行:";
            // 
            // txtvisa
            // 
            this.txtvisa.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtvisa.Location = new System.Drawing.Point(149, 228);
            this.txtvisa.MaxLength = 50;
            this.txtvisa.Name = "txtvisa";
            this.txtvisa.Size = new System.Drawing.Size(253, 23);
            this.txtvisa.TabIndex = 10008;
            // 
            // frmPaymentUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(524, 346);
            this.Controls.Add(this.txtvisa);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPayName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPaynum);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaymentUpdate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结算方式";
            this.Load += new System.EventHandler(this.frmPaymentUpload_Load);
            this.Shown += new System.EventHandler(this.frmPaymentUpload_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPaymentUpload_KeyDown);
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}