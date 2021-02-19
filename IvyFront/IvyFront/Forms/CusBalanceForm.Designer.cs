namespace IvyFront.Forms
{
    partial class CusBalanceForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_bal_amt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_credit_amt = new System.Windows.Forms.Label();
            this.lbl_use_amt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("SimSun", 20F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(1);
            this.label2.Size = new System.Drawing.Size(400, 50);
            this.label2.TabIndex = 9;
            this.label2.Text = "客户账款余额";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("SimSun", 13F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(51, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(303, 50);
            this.button1.TabIndex = 10;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 18F);
            this.label1.Location = new System.Drawing.Point(47, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "账款余额:";
            // 
            // lbl_bal_amt
            // 
            this.lbl_bal_amt.Font = new System.Drawing.Font("SimSun", 28F);
            this.lbl_bal_amt.ForeColor = System.Drawing.Color.Blue;
            this.lbl_bal_amt.Location = new System.Drawing.Point(171, 92);
            this.lbl_bal_amt.Name = "lbl_bal_amt";
            this.lbl_bal_amt.Size = new System.Drawing.Size(185, 40);
            this.lbl_bal_amt.TabIndex = 12;
            this.lbl_bal_amt.Text = "0.00";
            this.lbl_bal_amt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 18F);
            this.label4.Location = new System.Drawing.Point(47, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 24);
            this.label4.TabIndex = 13;
            this.label4.Text = "信用额度:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 18F);
            this.label5.Location = new System.Drawing.Point(47, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 24);
            this.label5.TabIndex = 14;
            this.label5.Text = "可用额度:";
            // 
            // lbl_credit_amt
            // 
            this.lbl_credit_amt.Font = new System.Drawing.Font("SimSun", 28F);
            this.lbl_credit_amt.ForeColor = System.Drawing.Color.Blue;
            this.lbl_credit_amt.Location = new System.Drawing.Point(171, 158);
            this.lbl_credit_amt.Name = "lbl_credit_amt";
            this.lbl_credit_amt.Size = new System.Drawing.Size(185, 40);
            this.lbl_credit_amt.TabIndex = 15;
            this.lbl_credit_amt.Text = "0.00";
            this.lbl_credit_amt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_use_amt
            // 
            this.lbl_use_amt.Font = new System.Drawing.Font("SimSun", 28F);
            this.lbl_use_amt.ForeColor = System.Drawing.Color.Red;
            this.lbl_use_amt.Location = new System.Drawing.Point(171, 228);
            this.lbl_use_amt.Name = "lbl_use_amt";
            this.lbl_use_amt.Size = new System.Drawing.Size(185, 40);
            this.lbl_use_amt.TabIndex = 16;
            this.lbl_use_amt.Text = "0.00";
            this.lbl_use_amt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CusBalanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.lbl_use_amt);
            this.Controls.Add(this.lbl_credit_amt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_bal_amt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CusBalanceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CusBalanceForm";
            this.Load += new System.EventHandler(this.CusBalanceForm_Load);
            this.Shown += new System.EventHandler(this.CusBalanceForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CusBalanceForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_bal_amt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_credit_amt;
        private System.Windows.Forms.Label lbl_use_amt;
    }
}