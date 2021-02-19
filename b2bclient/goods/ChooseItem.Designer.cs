namespace b2bclient.goods
{
    partial class ChooseItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseItem));
            this.pnl = new System.Windows.Forms.Panel();
            this.pnl_down = new System.Windows.Forms.Panel();
            this.pnl_up = new System.Windows.Forms.Panel();
            this.pnl_cls = new System.Windows.Forms.Panel();
            this.pnl_cancel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlexit = new System.Windows.Forms.Panel();
            this.lbltitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.pnl_down);
            this.pnl.Controls.Add(this.pnl_up);
            this.pnl.Controls.Add(this.pnl_cls);
            this.pnl.Controls.Add(this.pnl_cancel);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 42);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(379, 442);
            this.pnl.TabIndex = 3;
            // 
            // pnl_down
            // 
            this.pnl_down.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_down.BackgroundImage")));
            this.pnl_down.Location = new System.Drawing.Point(281, 329);
            this.pnl_down.Name = "pnl_down";
            this.pnl_down.Size = new System.Drawing.Size(36, 36);
            this.pnl_down.TabIndex = 14;
            this.pnl_down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_down_MouseDown);
            this.pnl_down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_down_MouseUp);
            // 
            // pnl_up
            // 
            this.pnl_up.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_up.BackgroundImage")));
            this.pnl_up.Location = new System.Drawing.Point(281, 10);
            this.pnl_up.Name = "pnl_up";
            this.pnl_up.Size = new System.Drawing.Size(36, 36);
            this.pnl_up.TabIndex = 13;
            this.pnl_up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_up_MouseDown);
            this.pnl_up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_up_MouseUp);
            // 
            // pnl_cls
            // 
            this.pnl_cls.Location = new System.Drawing.Point(33, 10);
            this.pnl_cls.Name = "pnl_cls";
            this.pnl_cls.Size = new System.Drawing.Size(242, 355);
            this.pnl_cls.TabIndex = 3;
            // 
            // pnl_cancel
            // 
            this.pnl_cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_cancel.BackgroundImage")));
            this.pnl_cancel.Location = new System.Drawing.Point(125, 375);
            this.pnl_cancel.Name = "pnl_cancel";
            this.pnl_cancel.Size = new System.Drawing.Size(114, 45);
            this.pnl_cancel.TabIndex = 2;
            this.pnl_cancel.Click += new System.EventHandler(this.pnl_cancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnl);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 484);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(216)))), ((int)(((byte)(190)))));
            this.panel2.Controls.Add(this.pnlexit);
            this.panel2.Controls.Add(this.lbltitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 42);
            this.panel2.TabIndex = 2;
            // 
            // pnlexit
            // 
            this.pnlexit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlexit.BackgroundImage")));
            this.pnlexit.Location = new System.Drawing.Point(300, 3);
            this.pnlexit.Name = "pnlexit";
            this.pnlexit.Size = new System.Drawing.Size(77, 36);
            this.pnlexit.TabIndex = 3;
            this.pnlexit.Click += new System.EventHandler(this.pnlexit_Click);
            // 
            // lbltitle
            // 
            this.lbltitle.AutoSize = true;
            this.lbltitle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbltitle.Location = new System.Drawing.Point(12, 8);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(50, 25);
            this.lbltitle.TabIndex = 2;
            this.lbltitle.Text = "选择";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChooseItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 484);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChooseItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChooseItem";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChooseItem_KeyUp);
            this.pnl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Panel pnl_down;
        private System.Windows.Forms.Panel pnl_up;
        private System.Windows.Forms.Panel pnl_cls;
        private System.Windows.Forms.Panel pnl_cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlexit;
        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.Timer timer1;
    }
}