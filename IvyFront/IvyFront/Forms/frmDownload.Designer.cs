namespace IvyFront.Forms
{
    partial class frmDownload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownload));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlclose = new System.Windows.Forms.Panel();
            this.btn_ok = new System.Windows.Forms.Button();
            this.picCheck = new System.Windows.Forms.PictureBox();
            this.picUncheck = new System.Windows.Forms.PictureBox();
            this.chk3 = new System.Windows.Forms.Panel();
            this.chk2 = new System.Windows.Forms.Panel();
            this.chk1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.pro = new System.Windows.Forms.Panel();
            this.pro2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chk4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_msg = new System.Windows.Forms.Label();
            this.chk5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_clear = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUncheck)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.pro.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pnlclose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 60);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "主档下载";
            // 
            // pnlclose
            // 
            this.pnlclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlclose.BackgroundImage")));
            this.pnlclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlclose.Location = new System.Drawing.Point(706, 1);
            this.pnlclose.Name = "pnlclose";
            this.pnlclose.Size = new System.Drawing.Size(150, 59);
            this.pnlclose.TabIndex = 3;
            this.pnlclose.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ok.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ok.ForeColor = System.Drawing.Color.White;
            this.btn_ok.Location = new System.Drawing.Point(152, 507);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(160, 60);
            this.btn_ok.TabIndex = 21;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // picCheck
            // 
            this.picCheck.Image = ((System.Drawing.Image)(resources.GetObject("picCheck.Image")));
            this.picCheck.Location = new System.Drawing.Point(799, 562);
            this.picCheck.Name = "picCheck";
            this.picCheck.Size = new System.Drawing.Size(45, 28);
            this.picCheck.TabIndex = 30;
            this.picCheck.TabStop = false;
            this.picCheck.Visible = false;
            // 
            // picUncheck
            // 
            this.picUncheck.Image = ((System.Drawing.Image)(resources.GetObject("picUncheck.Image")));
            this.picUncheck.Location = new System.Drawing.Point(747, 562);
            this.picUncheck.Name = "picUncheck";
            this.picUncheck.Size = new System.Drawing.Size(45, 28);
            this.picUncheck.TabIndex = 31;
            this.picUncheck.TabStop = false;
            this.picUncheck.Visible = false;
            // 
            // chk3
            // 
            this.chk3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chk3.BackgroundImage")));
            this.chk3.Location = new System.Drawing.Point(152, 207);
            this.chk3.Name = "chk3";
            this.chk3.Size = new System.Drawing.Size(35, 35);
            this.chk3.TabIndex = 27;
            this.chk3.Tag = "";
            this.chk3.Click += new System.EventHandler(this.chk3_Click);
            // 
            // chk2
            // 
            this.chk2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chk2.BackgroundImage")));
            this.chk2.Location = new System.Drawing.Point(152, 149);
            this.chk2.Name = "chk2";
            this.chk2.Size = new System.Drawing.Size(35, 35);
            this.chk2.TabIndex = 28;
            this.chk2.Tag = "";
            this.chk2.Click += new System.EventHandler(this.chk2_Click);
            // 
            // chk1
            // 
            this.chk1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chk1.BackgroundImage")));
            this.chk1.Location = new System.Drawing.Point(152, 92);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(35, 35);
            this.chk1.TabIndex = 26;
            this.chk1.Tag = "";
            this.chk1.Click += new System.EventHandler(this.chk1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(199, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 24);
            this.label8.TabIndex = 23;
            this.label8.Text = "商品库存/成本";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(199, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 24);
            this.label7.TabIndex = 24;
            this.label7.Text = "操作员档案";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(199, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 24);
            this.label6.TabIndex = 25;
            this.label6.Text = "商品档案";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(507, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 217);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作提示";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 14F);
            this.label3.Location = new System.Drawing.Point(3, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "1.清库操作会清空本地所有业务数据;";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.ForeColor = System.Drawing.Color.White;
            this.btn_cancel.Location = new System.Drawing.Point(546, 507);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(160, 60);
            this.btn_cancel.TabIndex = 32;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // pro
            // 
            this.pro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pro.Controls.Add(this.pro2);
            this.pro.Location = new System.Drawing.Point(140, 397);
            this.pro.Name = "pro";
            this.pro.Size = new System.Drawing.Size(566, 50);
            this.pro.TabIndex = 33;
            this.pro.Visible = false;
            // 
            // pro2
            // 
            this.pro2.Location = new System.Drawing.Point(98, 0);
            this.pro2.Name = "pro2";
            this.pro2.Size = new System.Drawing.Size(120, 50);
            this.pro2.TabIndex = 0;
            this.pro2.Paint += new System.Windows.Forms.PaintEventHandler(this.pro2_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chk4
            // 
            this.chk4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chk4.BackgroundImage")));
            this.chk4.Location = new System.Drawing.Point(152, 267);
            this.chk4.Name = "chk4";
            this.chk4.Size = new System.Drawing.Size(35, 35);
            this.chk4.TabIndex = 35;
            this.chk4.Click += new System.EventHandler(this.chk4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(199, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 24);
            this.label4.TabIndex = 34;
            this.label4.Text = "供应商/客户档案";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // lbl_msg
            // 
            this.lbl_msg.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_msg.ForeColor = System.Drawing.Color.Gray;
            this.lbl_msg.Location = new System.Drawing.Point(140, 451);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(566, 38);
            this.lbl_msg.TabIndex = 36;
            this.lbl_msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk5
            // 
            this.chk5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chk5.BackgroundImage")));
            this.chk5.Location = new System.Drawing.Point(152, 326);
            this.chk5.Name = "chk5";
            this.chk5.Size = new System.Drawing.Size(35, 35);
            this.chk5.TabIndex = 38;
            this.chk5.Click += new System.EventHandler(this.chk5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(199, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 24);
            this.label2.TabIndex = 37;
            this.label2.Text = "供应商/客户价格";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.Silver;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Font = new System.Drawing.Font("SimSun", 14F);
            this.btn_clear.ForeColor = System.Drawing.Color.Black;
            this.btn_clear.Location = new System.Drawing.Point(99, 137);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(140, 50);
            this.btn_clear.TabIndex = 39;
            this.btn_clear.Text = "本地开业清库";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 14F);
            this.label5.Location = new System.Drawing.Point(3, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(324, 19);
            this.label5.TabIndex = 40;
            this.label5.Text = "2.清库操作会清空本地所有基础档案;";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(857, 600);
            this.Controls.Add(this.chk5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.chk4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pro);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.picCheck);
            this.Controls.Add(this.picUncheck);
            this.Controls.Add(this.chk3);
            this.Controls.Add(this.chk2);
            this.Controls.Add(this.chk1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDownload";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.frmDownload_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmDownload_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUncheck)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pro.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlclose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.PictureBox picUncheck;
        private System.Windows.Forms.Panel chk3;
        private System.Windows.Forms.Panel chk2;
        private System.Windows.Forms.Panel chk1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Panel pro;
        private System.Windows.Forms.Panel pro2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel chk4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Panel chk5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Label label5;
    }
}