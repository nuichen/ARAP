namespace b2bclient
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnl_content = new b2bclient.control.MyPanel();
            this.pnl_logo = new b2bclient.control.MyPanel();
            this.pnl_sys_name = new b2bclient.control.MyPanel();
            this.pnl_right = new b2bclient.control.MyPanel();
            this.newCusMsg1 = new b2bclient.control.NewCusMsg();
            this.newOrderMsg1 = new b2bclient.control.NewOrderMsg();
            this.pnl = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.pnl_content.SuspendLayout();
            this.pnl_right.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel3.Controls.Add(this.pnl_content);
            this.panel3.Controls.Add(this.pnl_right);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1016, 90);
            this.panel3.TabIndex = 1;
            this.panel3.DoubleClick += new System.EventHandler(this.panel3_DoubleClick);
            // 
            // pnl_content
            // 
            this.pnl_content.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_content.BackgroundImage")));
            this.pnl_content.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_content.Controls.Add(this.pnl_logo);
            this.pnl_content.Controls.Add(this.pnl_sys_name);
            this.pnl_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_content.Location = new System.Drawing.Point(0, 0);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(886, 90);
            this.pnl_content.TabIndex = 1;
            // 
            // pnl_logo
            // 
            this.pnl_logo.BackColor = System.Drawing.Color.Transparent;
            this.pnl_logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_logo.BackgroundImage")));
            this.pnl_logo.Location = new System.Drawing.Point(12, 19);
            this.pnl_logo.Name = "pnl_logo";
            this.pnl_logo.Size = new System.Drawing.Size(134, 49);
            this.pnl_logo.TabIndex = 1;
            // 
            // pnl_sys_name
            // 
            this.pnl_sys_name.BackColor = System.Drawing.Color.Transparent;
            this.pnl_sys_name.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_sys_name.BackgroundImage")));
            this.pnl_sys_name.Location = new System.Drawing.Point(506, 23);
            this.pnl_sys_name.Name = "pnl_sys_name";
            this.pnl_sys_name.Size = new System.Drawing.Size(255, 41);
            this.pnl_sys_name.TabIndex = 0;
            // 
            // pnl_right
            // 
            this.pnl_right.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_right.BackgroundImage")));
            this.pnl_right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_right.Controls.Add(this.newCusMsg1);
            this.pnl_right.Controls.Add(this.newOrderMsg1);
            this.pnl_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_right.Location = new System.Drawing.Point(886, 0);
            this.pnl_right.Name = "pnl_right";
            this.pnl_right.Size = new System.Drawing.Size(130, 90);
            this.pnl_right.TabIndex = 0;
            // 
            // newCusMsg1
            // 
            this.newCusMsg1.BackColor = System.Drawing.Color.Transparent;
            this.newCusMsg1.Location = new System.Drawing.Point(84, 30);
            this.newCusMsg1.Name = "newCusMsg1";
            this.newCusMsg1.Size = new System.Drawing.Size(30, 32);
            this.newCusMsg1.TabIndex = 10;
            this.newCusMsg1.Click += new System.EventHandler(this.newCusMsg1_Click);
            // 
            // newOrderMsg1
            // 
            this.newOrderMsg1.BackColor = System.Drawing.Color.Transparent;
            this.newOrderMsg1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.newOrderMsg1.Location = new System.Drawing.Point(35, 30);
            this.newOrderMsg1.Name = "newOrderMsg1";
            this.newOrderMsg1.Size = new System.Drawing.Size(30, 32);
            this.newOrderMsg1.TabIndex = 4;
            this.newOrderMsg1.Click += new System.EventHandler(this.newOrderMsg1_Click);
            // 
            // pnl
            // 
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 90);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(1016, 651);
            this.pnl.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线上订货平台";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.panel3.ResumeLayout(false);
            this.pnl_content.ResumeLayout(false);
            this.pnl_right.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private control.NewOrderMsg newOrderMsg1;
        private System.Windows.Forms.Panel pnl;
        private control.NewCusMsg newCusMsg1;
        private control.MyPanel pnl_content;
        private control.MyPanel pnl_sys_name;
        private control.MyPanel pnl_right;
        private control.MyPanel pnl_logo;
    }
}