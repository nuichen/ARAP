namespace b2bclient.goods
{
    partial class frmGoodsChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGoodsChange));
            this.pnltop = new System.Windows.Forms.Panel();
            this.pnl = new System.Windows.Forms.Panel();
            this.pnlbottom = new System.Windows.Forms.Panel();
            this.pnlCancel = new System.Windows.Forms.Panel();
            this.pnlOK = new System.Windows.Forms.Panel();
            this.pnlbottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnltop
            // 
            this.pnltop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnltop.Location = new System.Drawing.Point(0, 0);
            this.pnltop.Name = "pnltop";
            this.pnltop.Size = new System.Drawing.Size(869, 57);
            this.pnltop.TabIndex = 0;
            this.pnltop.Click += new System.EventHandler(this.pnltop_Click);
            this.pnltop.Paint += new System.Windows.Forms.PaintEventHandler(this.pnltop_Paint);
            this.pnltop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnltop_MouseDown);
            // 
            // pnl
            // 
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 57);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(869, 489);
            this.pnl.TabIndex = 1;
            // 
            // pnlbottom
            // 
            this.pnlbottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlbottom.Controls.Add(this.pnlCancel);
            this.pnlbottom.Controls.Add(this.pnlOK);
            this.pnlbottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlbottom.Location = new System.Drawing.Point(0, 546);
            this.pnlbottom.Name = "pnlbottom";
            this.pnlbottom.Size = new System.Drawing.Size(869, 60);
            this.pnlbottom.TabIndex = 2;
            this.pnlbottom.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlbottom_Paint);
            // 
            // pnlCancel
            // 
            this.pnlCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCancel.BackgroundImage")));
            this.pnlCancel.Location = new System.Drawing.Point(491, 8);
            this.pnlCancel.Name = "pnlCancel";
            this.pnlCancel.Size = new System.Drawing.Size(114, 45);
            this.pnlCancel.TabIndex = 2;
            this.pnlCancel.Click += new System.EventHandler(this.pnlCancel_Click);
            // 
            // pnlOK
            // 
            this.pnlOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlOK.BackgroundImage")));
            this.pnlOK.Location = new System.Drawing.Point(270, 8);
            this.pnlOK.Name = "pnlOK";
            this.pnlOK.Size = new System.Drawing.Size(114, 45);
            this.pnlOK.TabIndex = 2;
            this.pnlOK.Click += new System.EventHandler(this.pnlOK_Click);
            // 
            // frmGoodsChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(869, 606);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.pnltop);
            this.Controls.Add(this.pnlbottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmGoodsChange";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "商品编辑";
            this.pnlbottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnltop;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Panel pnlbottom;
        private System.Windows.Forms.Panel pnlCancel;
        private System.Windows.Forms.Panel pnlOK;
    }
}