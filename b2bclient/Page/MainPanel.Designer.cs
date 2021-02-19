namespace b2bclient
{
    partial class MainPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPanel));
            this.pnlmain = new System.Windows.Forms.Panel();
            this.pnl = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_qrcode = new b2bclient.control.MyPanel();
            this.pnl_report = new b2bclient.control.MyPanel();
            this.pnl_goods = new b2bclient.control.MyPanel();
            this.pnl_mall = new b2bclient.control.MyPanel();
            this.pnl_customer = new b2bclient.control.MyPanel();
            this.pnl_order = new b2bclient.control.MyPanel();
            this.pnl_new = new b2bclient.control.MyPanel();
            this.pnlmain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlmain
            // 
            this.pnlmain.Controls.Add(this.pnl);
            this.pnlmain.Controls.Add(this.panel1);
            this.pnlmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlmain.Location = new System.Drawing.Point(0, 0);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(1016, 651);
            this.pnlmain.TabIndex = 6;
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(155, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(861, 651);
            this.pnl.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.pnl_qrcode);
            this.panel1.Controls.Add(this.pnl_report);
            this.panel1.Controls.Add(this.pnl_goods);
            this.panel1.Controls.Add(this.pnl_mall);
            this.panel1.Controls.Add(this.pnl_customer);
            this.panel1.Controls.Add(this.pnl_order);
            this.panel1.Controls.Add(this.pnl_new);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(155, 651);
            this.panel1.TabIndex = 5;
            // 
            // pnl_qrcode
            // 
            this.pnl_qrcode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnl_qrcode.BackColor = System.Drawing.Color.Transparent;
            this.pnl_qrcode.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_qrcode.BackgroundImage")));
            this.pnl_qrcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_qrcode.Location = new System.Drawing.Point(0, 401);
            this.pnl_qrcode.Name = "pnl_qrcode";
            this.pnl_qrcode.Size = new System.Drawing.Size(155, 217);
            this.pnl_qrcode.TabIndex = 5;
            // 
            // pnl_report
            // 
            this.pnl_report.BackColor = System.Drawing.Color.Transparent;
            this.pnl_report.BackgroundImage = global::b2bclient.Properties.Resources.菜单_6_1;
            this.pnl_report.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_report.Location = new System.Drawing.Point(0, 267);
            this.pnl_report.Name = "pnl_report";
            this.pnl_report.Size = new System.Drawing.Size(155, 50);
            this.pnl_report.TabIndex = 4;
            this.pnl_report.Click += new System.EventHandler(this.pnl_report_Click);
            // 
            // pnl_goods
            // 
            this.pnl_goods.BackColor = System.Drawing.Color.Transparent;
            this.pnl_goods.BackgroundImage = global::b2bclient.Properties.Resources.菜单_5_1;
            this.pnl_goods.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_goods.Location = new System.Drawing.Point(0, 217);
            this.pnl_goods.Name = "pnl_goods";
            this.pnl_goods.Size = new System.Drawing.Size(155, 50);
            this.pnl_goods.TabIndex = 3;
            this.pnl_goods.Click += new System.EventHandler(this.pnl_goods_Click);
            // 
            // pnl_mall
            // 
            this.pnl_mall.BackColor = System.Drawing.Color.Transparent;
            this.pnl_mall.BackgroundImage = global::b2bclient.Properties.Resources.菜单_4_1;
            this.pnl_mall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_mall.Location = new System.Drawing.Point(0, 167);
            this.pnl_mall.Name = "pnl_mall";
            this.pnl_mall.Size = new System.Drawing.Size(155, 50);
            this.pnl_mall.TabIndex = 2;
            this.pnl_mall.Click += new System.EventHandler(this.pnl_mall_Click);
            // 
            // pnl_customer
            // 
            this.pnl_customer.BackColor = System.Drawing.Color.Transparent;
            this.pnl_customer.BackgroundImage = global::b2bclient.Properties.Resources.菜单_3_1;
            this.pnl_customer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_customer.Location = new System.Drawing.Point(0, 117);
            this.pnl_customer.Name = "pnl_customer";
            this.pnl_customer.Size = new System.Drawing.Size(155, 50);
            this.pnl_customer.TabIndex = 2;
            this.pnl_customer.Click += new System.EventHandler(this.pnl_customer_Click);
            // 
            // pnl_order
            // 
            this.pnl_order.BackColor = System.Drawing.Color.Transparent;
            this.pnl_order.BackgroundImage = global::b2bclient.Properties.Resources.菜单_2_1;
            this.pnl_order.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_order.Location = new System.Drawing.Point(0, 67);
            this.pnl_order.Name = "pnl_order";
            this.pnl_order.Size = new System.Drawing.Size(155, 50);
            this.pnl_order.TabIndex = 1;
            this.pnl_order.Click += new System.EventHandler(this.pnl_order_Click);
            // 
            // pnl_new
            // 
            this.pnl_new.BackColor = System.Drawing.Color.Transparent;
            this.pnl_new.BackgroundImage = global::b2bclient.Properties.Resources.菜单_1_1;
            this.pnl_new.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_new.Location = new System.Drawing.Point(0, 17);
            this.pnl_new.Name = "pnl_new";
            this.pnl_new.Size = new System.Drawing.Size(155, 50);
            this.pnl_new.TabIndex = 0;
            this.pnl_new.Click += new System.EventHandler(this.pnl_new_Click);
            // 
            // MainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlmain);
            this.Name = "MainPanel";
            this.Size = new System.Drawing.Size(1016, 651);
            this.pnlmain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlmain;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Panel panel1;
        private control.MyPanel pnl_qrcode;
        private control.MyPanel pnl_report;
        private control.MyPanel pnl_goods;
        private control.MyPanel pnl_mall;
        private control.MyPanel pnl_customer;
        private control.MyPanel pnl_order;
        private control.MyPanel pnl_new;
    }
}
