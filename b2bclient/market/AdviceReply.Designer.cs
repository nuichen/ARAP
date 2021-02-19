namespace b2bclient.market
{
    partial class AdviceReply
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_context = new System.Windows.Forms.Panel();
            this.txt_ask = new System.Windows.Forms.TextBox();
            this.lbl_nick = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txt_reply = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnl_context.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnl_context);
            this.panel1.Controls.Add(this.lbl_nick);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(420, 155);
            this.panel1.TabIndex = 6;
            // 
            // pnl_context
            // 
            this.pnl_context.Controls.Add(this.txt_ask);
            this.pnl_context.Location = new System.Drawing.Point(0, 38);
            this.pnl_context.Name = "pnl_context";
            this.pnl_context.Size = new System.Drawing.Size(417, 115);
            this.pnl_context.TabIndex = 59;
            this.pnl_context.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlimg2_Paint);
            // 
            // txt_ask
            // 
            this.txt_ask.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ask.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ask.Location = new System.Drawing.Point(9, 4);
            this.txt_ask.Multiline = true;
            this.txt_ask.Name = "txt_ask";
            this.txt_ask.ReadOnly = true;
            this.txt_ask.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_ask.Size = new System.Drawing.Size(396, 107);
            this.txt_ask.TabIndex = 0;
            // 
            // lbl_nick
            // 
            this.lbl_nick.AutoSize = true;
            this.lbl_nick.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_nick.Location = new System.Drawing.Point(10, 5);
            this.lbl_nick.Name = "lbl_nick";
            this.lbl_nick.Size = new System.Drawing.Size(103, 20);
            this.lbl_nick.TabIndex = 58;
            this.lbl_nick.Text = "顾客反馈内容:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(4, 167);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 150);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txt_reply);
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(410, 120);
            this.panel3.TabIndex = 59;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlimg3_Paint);
            // 
            // txt_reply
            // 
            this.txt_reply.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_reply.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_reply.Location = new System.Drawing.Point(4, 6);
            this.txt_reply.Multiline = true;
            this.txt_reply.Name = "txt_reply";
            this.txt_reply.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_reply.Size = new System.Drawing.Size(400, 107);
            this.txt_reply.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 58;
            this.label1.Text = "商家回复内容:";
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.btn_cancel.ForeColor = System.Drawing.Color.Black;
            this.btn_cancel.Location = new System.Drawing.Point(241, 328);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(100, 40);
            this.btn_cancel.TabIndex = 70;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.pnlCancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ok.Font = new System.Drawing.Font("SimSun", 10.5F);
            this.btn_ok.ForeColor = System.Drawing.Color.Black;
            this.btn_ok.Location = new System.Drawing.Point(78, 328);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(100, 40);
            this.btn_ok.TabIndex = 69;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.pnlOK_Click);
            // 
            // AdviceReply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 380);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdviceReply";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "答复";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_context.ResumeLayout(false);
            this.pnl_context.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel pnl_context;
        internal System.Windows.Forms.TextBox txt_ask;
        internal System.Windows.Forms.Label lbl_nick;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.TextBox txt_reply;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
    }
}