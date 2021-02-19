namespace IvyFront.Forms
{
    partial class frmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.lbl_title = new System.Windows.Forms.Label();
            this.lbl_version = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pic_network = new System.Windows.Forms.PictureBox();
            this.sys_msg_block = new IvyFront.Forms.MyPanel();
            this.lbl_sys_title = new System.Windows.Forms.Label();
            this.lbl_sys_msg = new System.Windows.Forms.Label();
            this.myPanel1 = new IvyFront.Forms.MyPanel();
            this.btn_item_cls = new System.Windows.Forms.Button();
            this.btn_setting = new System.Windows.Forms.Button();
            this.btn_download = new System.Windows.Forms.Button();
            this.btn_change_pwd = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.pnl_menu2 = new IvyFront.Forms.MyPanel();
            this.btn_pay_flow = new System.Windows.Forms.Button();
            this.btn_xsck = new System.Windows.Forms.Button();
            this.btn_xsth = new System.Windows.Forms.Button();
            this.btn_cgrk = new System.Windows.Forms.Button();
            this.pnl_menu1 = new IvyFront.Forms.MyPanel();
            this.btn_pos = new System.Windows.Forms.Button();
            this.btn_cg = new System.Windows.Forms.Button();
            this.btn_th = new System.Windows.Forms.Button();
            this.picture1 = new IvyFront.Picture();
            ((System.ComponentModel.ISupportInitialize)(this.pic_network)).BeginInit();
            this.sys_msg_block.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.pnl_menu2.SuspendLayout();
            this.pnl_menu1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.Font = new System.Drawing.Font("SimSun", 26F, System.Drawing.FontStyle.Bold);
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(80, 22);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(862, 69);
            this.lbl_title.TabIndex = 94;
            this.lbl_title.Text = "智慧农贸";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_version
            // 
            this.lbl_version.Font = new System.Drawing.Font("SimSun", 10F);
            this.lbl_version.ForeColor = System.Drawing.Color.White;
            this.lbl_version.Location = new System.Drawing.Point(204, 740);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(602, 23);
            this.lbl_version.TabIndex = 99;
            this.lbl_version.Text = "V 2.0.0";
            this.lbl_version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pic_network
            // 
            this.pic_network.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_network.Image = global::IvyFront.Properties.Resources.disconnect;
            this.pic_network.Location = new System.Drawing.Point(948, 6);
            this.pic_network.Name = "pic_network";
            this.pic_network.Size = new System.Drawing.Size(70, 70);
            this.pic_network.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pic_network.TabIndex = 103;
            this.pic_network.TabStop = false;
            this.pic_network.Click += new System.EventHandler(this.pic_network_Click);
            // 
            // sys_msg_block
            // 
            this.sys_msg_block.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sys_msg_block.BackgroundImage")));
            this.sys_msg_block.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sys_msg_block.Controls.Add(this.lbl_sys_title);
            this.sys_msg_block.Controls.Add(this.lbl_sys_msg);
            this.sys_msg_block.Location = new System.Drawing.Point(850, 603);
            this.sys_msg_block.Name = "sys_msg_block";
            this.sys_msg_block.Size = new System.Drawing.Size(171, 163);
            this.sys_msg_block.TabIndex = 102;
            // 
            // lbl_sys_title
            // 
            this.lbl_sys_title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_sys_title.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_sys_title.ForeColor = System.Drawing.Color.White;
            this.lbl_sys_title.Location = new System.Drawing.Point(15, 10);
            this.lbl_sys_title.Name = "lbl_sys_title";
            this.lbl_sys_title.Size = new System.Drawing.Size(151, 16);
            this.lbl_sys_title.TabIndex = 101;
            this.lbl_sys_title.Text = "系统消息";
            this.lbl_sys_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_sys_title.Visible = false;
            this.lbl_sys_title.Click += new System.EventHandler(this.lbl_sys_msg_Click);
            // 
            // lbl_sys_msg
            // 
            this.lbl_sys_msg.BackColor = System.Drawing.Color.Transparent;
            this.lbl_sys_msg.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_sys_msg.ForeColor = System.Drawing.Color.Black;
            this.lbl_sys_msg.Location = new System.Drawing.Point(12, 39);
            this.lbl_sys_msg.Name = "lbl_sys_msg";
            this.lbl_sys_msg.Size = new System.Drawing.Size(154, 110);
            this.lbl_sys_msg.TabIndex = 100;
            this.lbl_sys_msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_sys_msg.Visible = false;
            this.lbl_sys_msg.Click += new System.EventHandler(this.lbl_sys_msg_Click);
            // 
            // myPanel1
            // 
            this.myPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("myPanel1.BackgroundImage")));
            this.myPanel1.Controls.Add(this.btn_item_cls);
            this.myPanel1.Controls.Add(this.btn_setting);
            this.myPanel1.Controls.Add(this.btn_download);
            this.myPanel1.Controls.Add(this.btn_change_pwd);
            this.myPanel1.Controls.Add(this.btn_exit);
            this.myPanel1.Controls.Add(this.btn_upload);
            this.myPanel1.Location = new System.Drawing.Point(137, 446);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(751, 209);
            this.myPanel1.TabIndex = 109;
            // 
            // btn_item_cls
            // 
            this.btn_item_cls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_item_cls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_item_cls.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_item_cls.ForeColor = System.Drawing.Color.Black;
            this.btn_item_cls.Location = new System.Drawing.Point(293, 26);
            this.btn_item_cls.Name = "btn_item_cls";
            this.btn_item_cls.Size = new System.Drawing.Size(150, 60);
            this.btn_item_cls.TabIndex = 98;
            this.btn_item_cls.Text = "品类启/停用";
            this.btn_item_cls.UseVisualStyleBackColor = false;
            this.btn_item_cls.Click += new System.EventHandler(this.btn_item_cls_Click);
            // 
            // btn_setting
            // 
            this.btn_setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_setting.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_setting.ForeColor = System.Drawing.Color.Black;
            this.btn_setting.Location = new System.Drawing.Point(28, 113);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(150, 60);
            this.btn_setting.TabIndex = 87;
            this.btn_setting.Text = "参数设置";
            this.btn_setting.UseVisualStyleBackColor = false;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_download
            // 
            this.btn_download.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_download.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_download.ForeColor = System.Drawing.Color.Black;
            this.btn_download.Location = new System.Drawing.Point(28, 26);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(150, 60);
            this.btn_download.TabIndex = 90;
            this.btn_download.Text = "主档下载";
            this.btn_download.UseVisualStyleBackColor = false;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // btn_change_pwd
            // 
            this.btn_change_pwd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_change_pwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_change_pwd.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_change_pwd.ForeColor = System.Drawing.Color.Black;
            this.btn_change_pwd.Location = new System.Drawing.Point(293, 113);
            this.btn_change_pwd.Name = "btn_change_pwd";
            this.btn_change_pwd.Size = new System.Drawing.Size(150, 60);
            this.btn_change_pwd.TabIndex = 92;
            this.btn_change_pwd.Text = "修改密码";
            this.btn_change_pwd.UseVisualStyleBackColor = false;
            this.btn_change_pwd.Click += new System.EventHandler(this.btn_change_pwd_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_exit.ForeColor = System.Drawing.Color.Black;
            this.btn_exit.Location = new System.Drawing.Point(558, 113);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(150, 60);
            this.btn_exit.TabIndex = 93;
            this.btn_exit.Text = "退出系统";
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_upload.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_upload.ForeColor = System.Drawing.Color.Black;
            this.btn_upload.Location = new System.Drawing.Point(558, 26);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(150, 60);
            this.btn_upload.TabIndex = 97;
            this.btn_upload.Text = "单据上传";
            this.btn_upload.UseVisualStyleBackColor = false;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // pnl_menu2
            // 
            this.pnl_menu2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_menu2.BackgroundImage")));
            this.pnl_menu2.Controls.Add(this.btn_pay_flow);
            this.pnl_menu2.Controls.Add(this.btn_xsck);
            this.pnl_menu2.Controls.Add(this.btn_xsth);
            this.pnl_menu2.Controls.Add(this.btn_cgrk);
            this.pnl_menu2.Location = new System.Drawing.Point(137, 225);
            this.pnl_menu2.Name = "pnl_menu2";
            this.pnl_menu2.Size = new System.Drawing.Size(751, 209);
            this.pnl_menu2.TabIndex = 108;
            // 
            // btn_pay_flow
            // 
            this.btn_pay_flow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_pay_flow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pay_flow.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_pay_flow.ForeColor = System.Drawing.Color.Black;
            this.btn_pay_flow.Location = new System.Drawing.Point(28, 28);
            this.btn_pay_flow.Name = "btn_pay_flow";
            this.btn_pay_flow.Size = new System.Drawing.Size(150, 60);
            this.btn_pay_flow.TabIndex = 91;
            this.btn_pay_flow.Text = "收支对账";
            this.btn_pay_flow.UseVisualStyleBackColor = false;
            this.btn_pay_flow.Click += new System.EventHandler(this.btn_pay_flow_Click);
            // 
            // btn_xsck
            // 
            this.btn_xsck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_xsck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_xsck.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_xsck.ForeColor = System.Drawing.Color.Black;
            this.btn_xsck.Location = new System.Drawing.Point(28, 118);
            this.btn_xsck.Name = "btn_xsck";
            this.btn_xsck.Size = new System.Drawing.Size(150, 60);
            this.btn_xsck.TabIndex = 95;
            this.btn_xsck.Text = "销售出库单";
            this.btn_xsck.UseVisualStyleBackColor = false;
            this.btn_xsck.Click += new System.EventHandler(this.btn_xsck_Click);
            // 
            // btn_xsth
            // 
            this.btn_xsth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_xsth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_xsth.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_xsth.ForeColor = System.Drawing.Color.Black;
            this.btn_xsth.Location = new System.Drawing.Point(558, 118);
            this.btn_xsth.Name = "btn_xsth";
            this.btn_xsth.Size = new System.Drawing.Size(150, 60);
            this.btn_xsth.TabIndex = 106;
            this.btn_xsth.Text = "销售退货单";
            this.btn_xsth.UseVisualStyleBackColor = false;
            this.btn_xsth.Click += new System.EventHandler(this.btn_xsth_Click);
            // 
            // btn_cgrk
            // 
            this.btn_cgrk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_cgrk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cgrk.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cgrk.ForeColor = System.Drawing.Color.Black;
            this.btn_cgrk.Location = new System.Drawing.Point(293, 118);
            this.btn_cgrk.Name = "btn_cgrk";
            this.btn_cgrk.Size = new System.Drawing.Size(150, 60);
            this.btn_cgrk.TabIndex = 96;
            this.btn_cgrk.Text = "采购入库单";
            this.btn_cgrk.UseVisualStyleBackColor = false;
            this.btn_cgrk.Click += new System.EventHandler(this.btn_cgrk_Click);
            // 
            // pnl_menu1
            // 
            this.pnl_menu1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_menu1.BackgroundImage")));
            this.pnl_menu1.Controls.Add(this.btn_pos);
            this.pnl_menu1.Controls.Add(this.btn_cg);
            this.pnl_menu1.Controls.Add(this.btn_th);
            this.pnl_menu1.Location = new System.Drawing.Point(137, 115);
            this.pnl_menu1.Name = "pnl_menu1";
            this.pnl_menu1.Size = new System.Drawing.Size(751, 101);
            this.pnl_menu1.TabIndex = 107;
            // 
            // btn_pos
            // 
            this.btn_pos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_pos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pos.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_pos.ForeColor = System.Drawing.Color.Red;
            this.btn_pos.Location = new System.Drawing.Point(28, 20);
            this.btn_pos.Name = "btn_pos";
            this.btn_pos.Size = new System.Drawing.Size(150, 60);
            this.btn_pos.TabIndex = 0;
            this.btn_pos.Text = "销售开单";
            this.btn_pos.UseVisualStyleBackColor = false;
            this.btn_pos.Click += new System.EventHandler(this.btn_pos_Click);
            // 
            // btn_cg
            // 
            this.btn_cg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_cg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cg.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cg.ForeColor = System.Drawing.Color.Red;
            this.btn_cg.Location = new System.Drawing.Point(293, 20);
            this.btn_cg.Name = "btn_cg";
            this.btn_cg.Size = new System.Drawing.Size(150, 60);
            this.btn_cg.TabIndex = 1;
            this.btn_cg.Text = "采购开单";
            this.btn_cg.UseVisualStyleBackColor = false;
            this.btn_cg.Click += new System.EventHandler(this.btn_cg_Click);
            // 
            // btn_th
            // 
            this.btn_th.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.btn_th.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_th.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_th.ForeColor = System.Drawing.Color.Red;
            this.btn_th.Location = new System.Drawing.Point(558, 20);
            this.btn_th.Name = "btn_th";
            this.btn_th.Size = new System.Drawing.Size(150, 60);
            this.btn_th.TabIndex = 2;
            this.btn_th.Text = "销售退货";
            this.btn_th.UseVisualStyleBackColor = false;
            this.btn_th.Click += new System.EventHandler(this.btn_th_Click);
            // 
            // picture1
            // 
            this.picture1.BackColor = System.Drawing.Color.Transparent;
            this.picture1.Image = ((System.Drawing.Image)(resources.GetObject("picture1.Image")));
            this.picture1.Location = new System.Drawing.Point(12, 12);
            this.picture1.Name = "picture1";
            this.picture1.Size = new System.Drawing.Size(83, 30);
            this.picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture1.TabIndex = 110;
            this.picture1.TabStop = false;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(110)))), ((int)(((byte)(165)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.picture1);
            this.Controls.Add(this.sys_msg_block);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.pnl_menu2);
            this.Controls.Add(this.pnl_menu1);
            this.Controls.Add(this.pic_network);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.lbl_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智慧农贸";
            ((System.ComponentModel.ISupportInitialize)(this.pic_network)).EndInit();
            this.sys_msg_block.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.pnl_menu2.ResumeLayout(false);
            this.pnl_menu1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_pos;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.Button btn_pay_flow;
        private System.Windows.Forms.Button btn_change_pwd;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_xsck;
        private System.Windows.Forms.Button btn_cgrk;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Button btn_item_cls;
        private System.Windows.Forms.Label lbl_version;
        private System.Windows.Forms.Label lbl_sys_msg;
        private System.Windows.Forms.Label lbl_sys_title;
        private MyPanel sys_msg_block;
        private System.Windows.Forms.PictureBox pic_network;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_cg;
        private System.Windows.Forms.Button btn_th;
        private System.Windows.Forms.Button btn_xsth;
        private MyPanel pnl_menu1;
        private MyPanel pnl_menu2;
        private MyPanel myPanel1;
        private Picture picture1;
    }
}