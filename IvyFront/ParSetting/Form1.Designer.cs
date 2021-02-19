namespace ParSetting
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ws_svr = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_is_mobile_pay = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_mer_key = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_svr = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cb_print_name = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chk_is_print = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_bind_ip = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器地址";
            // 
            // txt_ws_svr
            // 
            this.txt_ws_svr.Location = new System.Drawing.Point(135, 50);
            this.txt_ws_svr.Name = "txt_ws_svr";
            this.txt_ws_svr.Size = new System.Drawing.Size(355, 26);
            this.txt_ws_svr.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(298, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 44);
            this.button2.TabIndex = 5;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(126, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(563, 494);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.chk_bind_ip);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.chk_is_mobile_pay);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txt_mer_key);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.txt_svr);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txt_ws_svr);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(555, 464);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本参数";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "启用移动支付";
            // 
            // chk_is_mobile_pay
            // 
            this.chk_is_mobile_pay.AutoSize = true;
            this.chk_is_mobile_pay.Location = new System.Drawing.Point(136, 177);
            this.chk_is_mobile_pay.Name = "chk_is_mobile_pay";
            this.chk_is_mobile_pay.Size = new System.Drawing.Size(15, 14);
            this.chk_is_mobile_pay.TabIndex = 23;
            this.chk_is_mobile_pay.UseVisualStyleBackColor = true;
            this.chk_is_mobile_pay.CheckedChanged += new System.EventHandler(this.chk_is_mobile_pay_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(53, 311);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 16);
            this.label14.TabIndex = 22;
            this.label14.Text = "商家密钥";
            this.label14.Visible = false;
            // 
            // txt_mer_key
            // 
            this.txt_mer_key.Location = new System.Drawing.Point(135, 308);
            this.txt_mer_key.Name = "txt_mer_key";
            this.txt_mer_key.Size = new System.Drawing.Size(355, 26);
            this.txt_mer_key.TabIndex = 21;
            this.txt_mer_key.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 241);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 16);
            this.label10.TabIndex = 5;
            this.label10.Text = "移动支付接口";
            this.label10.Visible = false;
            // 
            // txt_svr
            // 
            this.txt_svr.Location = new System.Drawing.Point(135, 237);
            this.txt_svr.Name = "txt_svr";
            this.txt_svr.Size = new System.Drawing.Size(355, 26);
            this.txt_svr.TabIndex = 4;
            this.txt_svr.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cb_print_name);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.chk_is_print);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(555, 464);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "打印设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cb_print_name
            // 
            this.cb_print_name.DisplayMember = "value_str";
            this.cb_print_name.FormattingEnabled = true;
            this.cb_print_name.Location = new System.Drawing.Point(123, 120);
            this.cb_print_name.Name = "cb_print_name";
            this.cb_print_name.Size = new System.Drawing.Size(340, 24);
            this.cb_print_name.TabIndex = 16;
            this.cb_print_name.ValueMember = "key_str";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 15;
            this.label12.Text = "打印小票";
            // 
            // chk_is_print
            // 
            this.chk_is_print.AutoSize = true;
            this.chk_is_print.Checked = true;
            this.chk_is_print.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_is_print.Location = new System.Drawing.Point(123, 58);
            this.chk_is_print.Name = "chk_is_print";
            this.chk_is_print.Size = new System.Drawing.Size(15, 14);
            this.chk_is_print.TabIndex = 14;
            this.chk_is_print.UseVisualStyleBackColor = true;
            this.chk_is_print.CheckedChanged += new System.EventHandler(this.chk_is_print_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "打印机名";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 494);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(563, 79);
            this.panel3.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "服务器固定IP";
            // 
            // chk_bind_ip
            // 
            this.chk_bind_ip.AutoSize = true;
            this.chk_bind_ip.Location = new System.Drawing.Point(136, 114);
            this.chk_bind_ip.Name = "chk_bind_ip";
            this.chk_bind_ip.Size = new System.Drawing.Size(15, 14);
            this.chk_bind_ip.TabIndex = 25;
            this.chk_bind_ip.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(563, 573);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ws_svr;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_svr;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chk_is_print;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_mer_key;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_is_mobile_pay;
        private System.Windows.Forms.ComboBox cb_print_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_bind_ip;
    }
}

