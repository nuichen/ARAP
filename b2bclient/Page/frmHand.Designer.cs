namespace b2bclient
{
    partial class frmHand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHand));
            this.btn_disable_row = new System.Windows.Forms.Button();
            this.pnl付款 = new System.Windows.Forms.Panel();
            this.lbl合计 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_un_read_num = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl单号 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl下单时间 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnl_new = new System.Windows.Forms.Panel();
            this.lbl手机 = new System.Windows.Forms.TextBox();
            this.lbl姓名 = new System.Windows.Forms.TextBox();
            this.lbl地址 = new System.Windows.Forms.TextBox();
            this.lab配送 = new System.Windows.Forms.Label();
            this.lab优惠 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_credit_amt = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl备注 = new System.Windows.Forms.TextBox();
            this.pnl_pass = new System.Windows.Forms.Button();
            this.pnl_disable = new System.Windows.Forms.Button();
            this.pnl_later = new System.Windows.Forms.Button();
            this.pnl_exit = new System.Windows.Forms.Button();
            this.pnl_detail = new System.Windows.Forms.Panel();
            this.dg_data = new b2bclient.control.DataGrid();
            this.pnl_bottom = new System.Windows.Forms.Panel();
            this.pnl_top = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl送达时间 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_detail.SuspendLayout();
            this.pnl_bottom.SuspendLayout();
            this.pnl_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_disable_row
            // 
            this.btn_disable_row.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_disable_row.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_disable_row.Font = new System.Drawing.Font("SimSun", 12F);
            this.btn_disable_row.ForeColor = System.Drawing.Color.Navy;
            this.btn_disable_row.Location = new System.Drawing.Point(691, 231);
            this.btn_disable_row.Name = "btn_disable_row";
            this.btn_disable_row.Size = new System.Drawing.Size(100, 35);
            this.btn_disable_row.TabIndex = 47;
            this.btn_disable_row.Text = "取消行";
            this.btn_disable_row.UseVisualStyleBackColor = false;
            this.btn_disable_row.Click += new System.EventHandler(this.btn_disable_row_Click);
            // 
            // pnl付款
            // 
            this.pnl付款.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl付款.BackgroundImage")));
            this.pnl付款.Location = new System.Drawing.Point(690, 555);
            this.pnl付款.Name = "pnl付款";
            this.pnl付款.Size = new System.Drawing.Size(86, 31);
            this.pnl付款.TabIndex = 7;
            // 
            // lbl合计
            // 
            this.lbl合计.AutoSize = true;
            this.lbl合计.Font = new System.Drawing.Font("SimSun", 24F);
            this.lbl合计.ForeColor = System.Drawing.Color.Red;
            this.lbl合计.Location = new System.Drawing.Point(85, 559);
            this.lbl合计.Name = "lbl合计";
            this.lbl合计.Size = new System.Drawing.Size(79, 33);
            this.lbl合计.TabIndex = 6;
            this.lbl合计.Text = "0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 14F);
            this.label5.Location = new System.Drawing.Point(24, 567);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "合计：";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("SimSun", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(102, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(572, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品明细";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(664, 556);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 30);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(635, 556);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 30);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // lbl_un_read_num
            // 
            this.lbl_un_read_num.AutoSize = true;
            this.lbl_un_read_num.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_un_read_num.ForeColor = System.Drawing.Color.Red;
            this.lbl_un_read_num.Location = new System.Drawing.Point(751, 7);
            this.lbl_un_read_num.Name = "lbl_un_read_num";
            this.lbl_un_read_num.Size = new System.Drawing.Size(19, 19);
            this.lbl_un_read_num.TabIndex = 1;
            this.lbl_un_read_num.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(659, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "未阅读订单:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 12F);
            this.label2.Location = new System.Drawing.Point(29, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "单号:";
            // 
            // lbl单号
            // 
            this.lbl单号.AutoSize = true;
            this.lbl单号.Font = new System.Drawing.Font("SimSun", 12F);
            this.lbl单号.Location = new System.Drawing.Point(79, 55);
            this.lbl单号.Name = "lbl单号";
            this.lbl单号.Size = new System.Drawing.Size(16, 16);
            this.lbl单号.TabIndex = 4;
            this.lbl单号.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 12F);
            this.label4.Location = new System.Drawing.Point(29, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "下单时间:";
            // 
            // lbl下单时间
            // 
            this.lbl下单时间.AutoSize = true;
            this.lbl下单时间.Font = new System.Drawing.Font("SimSun", 12F);
            this.lbl下单时间.Location = new System.Drawing.Point(111, 91);
            this.lbl下单时间.Name = "lbl下单时间";
            this.lbl下单时间.Size = new System.Drawing.Size(16, 16);
            this.lbl下单时间.TabIndex = 4;
            this.lbl下单时间.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(28, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 19);
            this.label8.TabIndex = 3;
            this.label8.Text = "姓名:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(341, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 19);
            this.label10.TabIndex = 3;
            this.label10.Text = "手机:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(29, 164);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 15);
            this.label12.TabIndex = 3;
            this.label12.Text = "地址:";
            // 
            // pnl_new
            // 
            this.pnl_new.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_new.BackgroundImage")));
            this.pnl_new.Location = new System.Drawing.Point(664, 43);
            this.pnl_new.Name = "pnl_new";
            this.pnl_new.Size = new System.Drawing.Size(24, 24);
            this.pnl_new.TabIndex = 5;
            // 
            // lbl手机
            // 
            this.lbl手机.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lbl手机.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl手机.Font = new System.Drawing.Font("SimSun", 14.25F);
            this.lbl手机.ForeColor = System.Drawing.Color.Red;
            this.lbl手机.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl手机.Location = new System.Drawing.Point(397, 15);
            this.lbl手机.Name = "lbl手机";
            this.lbl手机.ReadOnly = true;
            this.lbl手机.Size = new System.Drawing.Size(188, 22);
            this.lbl手机.TabIndex = 6;
            this.lbl手机.Text = "-";
            // 
            // lbl姓名
            // 
            this.lbl姓名.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lbl姓名.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl姓名.Font = new System.Drawing.Font("SimSun", 14.25F);
            this.lbl姓名.Location = new System.Drawing.Point(85, 15);
            this.lbl姓名.Name = "lbl姓名";
            this.lbl姓名.ReadOnly = true;
            this.lbl姓名.Size = new System.Drawing.Size(134, 22);
            this.lbl姓名.TabIndex = 7;
            this.lbl姓名.TabStop = false;
            this.lbl姓名.Text = "-";
            // 
            // lbl地址
            // 
            this.lbl地址.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lbl地址.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl地址.Font = new System.Drawing.Font("SimSun", 12F);
            this.lbl地址.Location = new System.Drawing.Point(87, 163);
            this.lbl地址.Name = "lbl地址";
            this.lbl地址.ReadOnly = true;
            this.lbl地址.Size = new System.Drawing.Size(529, 19);
            this.lbl地址.TabIndex = 8;
            this.lbl地址.Text = "-";
            // 
            // lab配送
            // 
            this.lab配送.AutoSize = true;
            this.lab配送.Font = new System.Drawing.Font("SimSun", 12F);
            this.lab配送.Location = new System.Drawing.Point(29, 128);
            this.lab配送.Name = "lab配送";
            this.lab配送.Size = new System.Drawing.Size(120, 16);
            this.lab配送.TabIndex = 9;
            this.lab配送.Text = "配送费:￥00.00";
            // 
            // lab优惠
            // 
            this.lab优惠.AutoSize = true;
            this.lab优惠.Font = new System.Drawing.Font("SimSun", 12F);
            this.lab优惠.Location = new System.Drawing.Point(342, 128);
            this.lab优惠.Name = "lab优惠";
            this.lab优惠.Size = new System.Drawing.Size(136, 16);
            this.lab优惠.TabIndex = 10;
            this.lab优惠.Text = "优惠金额:￥00.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 12F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(342, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "可用信用额:";
            // 
            // lbl_credit_amt
            // 
            this.lbl_credit_amt.AutoSize = true;
            this.lbl_credit_amt.Font = new System.Drawing.Font("SimSun", 12F);
            this.lbl_credit_amt.ForeColor = System.Drawing.Color.Red;
            this.lbl_credit_amt.Location = new System.Drawing.Point(434, 55);
            this.lbl_credit_amt.Name = "lbl_credit_amt";
            this.lbl_credit_amt.Size = new System.Drawing.Size(16, 16);
            this.lbl_credit_amt.TabIndex = 4;
            this.lbl_credit_amt.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(29, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "备注:";
            // 
            // lbl备注
            // 
            this.lbl备注.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lbl备注.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl备注.Font = new System.Drawing.Font("SimSun", 12F);
            this.lbl备注.Location = new System.Drawing.Point(87, 198);
            this.lbl备注.Name = "lbl备注";
            this.lbl备注.ReadOnly = true;
            this.lbl备注.Size = new System.Drawing.Size(529, 19);
            this.lbl备注.TabIndex = 13;
            this.lbl备注.Text = "-";
            // 
            // pnl_pass
            // 
            this.pnl_pass.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_pass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnl_pass.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_pass.ForeColor = System.Drawing.Color.Red;
            this.pnl_pass.Location = new System.Drawing.Point(295, 8);
            this.pnl_pass.Name = "pnl_pass";
            this.pnl_pass.Size = new System.Drawing.Size(100, 45);
            this.pnl_pass.TabIndex = 44;
            this.pnl_pass.Text = "接单";
            this.pnl_pass.UseVisualStyleBackColor = false;
            this.pnl_pass.Click += new System.EventHandler(this.pnl_pass_Click);
            // 
            // pnl_disable
            // 
            this.pnl_disable.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_disable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnl_disable.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_disable.ForeColor = System.Drawing.Color.Navy;
            this.pnl_disable.Location = new System.Drawing.Point(165, 8);
            this.pnl_disable.Name = "pnl_disable";
            this.pnl_disable.Size = new System.Drawing.Size(100, 45);
            this.pnl_disable.TabIndex = 45;
            this.pnl_disable.Text = "拒单";
            this.pnl_disable.UseVisualStyleBackColor = false;
            this.pnl_disable.Click += new System.EventHandler(this.pnl_disable_Click);
            // 
            // pnl_later
            // 
            this.pnl_later.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_later.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnl_later.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_later.ForeColor = System.Drawing.Color.Navy;
            this.pnl_later.Location = new System.Drawing.Point(423, 8);
            this.pnl_later.Name = "pnl_later";
            this.pnl_later.Size = new System.Drawing.Size(100, 45);
            this.pnl_later.TabIndex = 46;
            this.pnl_later.Text = "跳过";
            this.pnl_later.UseVisualStyleBackColor = false;
            // 
            // pnl_exit
            // 
            this.pnl_exit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pnl_exit.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_exit.ForeColor = System.Drawing.Color.Navy;
            this.pnl_exit.Location = new System.Drawing.Point(553, 8);
            this.pnl_exit.Name = "pnl_exit";
            this.pnl_exit.Size = new System.Drawing.Size(100, 45);
            this.pnl_exit.TabIndex = 47;
            this.pnl_exit.Text = "关闭";
            this.pnl_exit.UseVisualStyleBackColor = false;
            this.pnl_exit.Click += new System.EventHandler(this.pnl_exit_Click);
            // 
            // pnl_detail
            // 
            this.pnl_detail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_detail.Controls.Add(this.dg_data);
            this.pnl_detail.Location = new System.Drawing.Point(0, 270);
            this.pnl_detail.Name = "pnl_detail";
            this.pnl_detail.Size = new System.Drawing.Size(794, 282);
            this.pnl_detail.TabIndex = 48;
            // 
            // dg_data
            // 
            this.dg_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_data.Location = new System.Drawing.Point(0, 0);
            this.dg_data.MergeCell = true;
            this.dg_data.Name = "dg_data";
            this.dg_data.RowHeight = 25;
            this.dg_data.Size = new System.Drawing.Size(790, 278);
            this.dg_data.TabIndex = 72;
            this.dg_data.Text = "dataGrid1";
            // 
            // pnl_bottom
            // 
            this.pnl_bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnl_bottom.Controls.Add(this.pnl_pass);
            this.pnl_bottom.Controls.Add(this.pnl_disable);
            this.pnl_bottom.Controls.Add(this.pnl_later);
            this.pnl_bottom.Controls.Add(this.pnl_exit);
            this.pnl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_bottom.Location = new System.Drawing.Point(0, 615);
            this.pnl_bottom.Name = "pnl_bottom";
            this.pnl_bottom.Size = new System.Drawing.Size(794, 60);
            this.pnl_bottom.TabIndex = 49;
            // 
            // pnl_top
            // 
            this.pnl_top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnl_top.Controls.Add(this.label9);
            this.pnl_top.Controls.Add(this.lbl送达时间);
            this.pnl_top.Controls.Add(this.label3);
            this.pnl_top.Controls.Add(this.pnl_new);
            this.pnl_top.Controls.Add(this.lbl_un_read_num);
            this.pnl_top.Controls.Add(this.label8);
            this.pnl_top.Controls.Add(this.label10);
            this.pnl_top.Controls.Add(this.lbl手机);
            this.pnl_top.Controls.Add(this.lbl姓名);
            this.pnl_top.Controls.Add(this.label2);
            this.pnl_top.Controls.Add(this.label4);
            this.pnl_top.Controls.Add(this.lbl备注);
            this.pnl_top.Controls.Add(this.label6);
            this.pnl_top.Controls.Add(this.lbl单号);
            this.pnl_top.Controls.Add(this.label7);
            this.pnl_top.Controls.Add(this.lbl下单时间);
            this.pnl_top.Controls.Add(this.lab优惠);
            this.pnl_top.Controls.Add(this.lbl_credit_amt);
            this.pnl_top.Controls.Add(this.lab配送);
            this.pnl_top.Controls.Add(this.lbl地址);
            this.pnl_top.Controls.Add(this.label12);
            this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_top.Location = new System.Drawing.Point(0, 0);
            this.pnl_top.Name = "pnl_top";
            this.pnl_top.Size = new System.Drawing.Size(794, 225);
            this.pnl_top.TabIndex = 50;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("SimSun", 12F);
            this.label9.Location = new System.Drawing.Point(342, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "送达时间:";
            // 
            // lbl送达时间
            // 
            this.lbl送达时间.AutoSize = true;
            this.lbl送达时间.Font = new System.Drawing.Font("SimSun", 12F);
            this.lbl送达时间.Location = new System.Drawing.Point(424, 91);
            this.lbl送达时间.Name = "lbl送达时间";
            this.lbl送达时间.Size = new System.Drawing.Size(16, 16);
            this.lbl送达时间.TabIndex = 15;
            this.lbl送达时间.Text = "-";
            // 
            // frmHand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(794, 675);
            this.Controls.Add(this.pnl_top);
            this.Controls.Add(this.pnl_bottom);
            this.Controls.Add(this.btn_disable_row);
            this.Controls.Add(this.pnl_detail);
            this.Controls.Add(this.pnl付款);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lbl合计);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHand";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新单提示";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmHand_FormClosed);
            this.Load += new System.EventHandler(this.frmHand_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_detail.ResumeLayout(false);
            this.pnl_bottom.ResumeLayout(false);
            this.pnl_top.ResumeLayout(false);
            this.pnl_top.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl单号;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl下单时间;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl合计;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnl付款;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_un_read_num;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnl_new;
        private System.Windows.Forms.TextBox lbl手机;
        private System.Windows.Forms.TextBox lbl姓名;
        private System.Windows.Forms.TextBox lbl地址;
        private System.Windows.Forms.Label lab配送;
        private System.Windows.Forms.Label lab优惠;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_credit_amt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox lbl备注;
        private System.Windows.Forms.Button pnl_pass;
        private System.Windows.Forms.Button pnl_disable;
        private System.Windows.Forms.Button pnl_later;
        private System.Windows.Forms.Button pnl_exit;
        private System.Windows.Forms.Panel pnl_detail;
        private control.DataGrid dg_data;
        private System.Windows.Forms.Button btn_disable_row;
        private System.Windows.Forms.Panel pnl_bottom;
        private System.Windows.Forms.Panel pnl_top;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl送达时间;
    }
}