namespace IvyBack.cons
{
    partial class UC_ImageView
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pl_right = new System.Windows.Forms.Panel();
            this.pbDelImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.pl_right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDelImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.White;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Image = global::IvyBack.Properties.Resources.LoadWait;
            this.pbImage.Location = new System.Drawing.Point(1, 1);
            this.pbImage.Margin = new System.Windows.Forms.Padding(0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(385, 332);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            this.pbImage.Visible = false;
            this.pbImage.Click += new System.EventHandler(this.pbImage_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblInfo.ForeColor = System.Drawing.Color.LightGray;
            this.lblInfo.Location = new System.Drawing.Point(1, 1);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(385, 332);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "点击上传";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfo.Click += new System.EventHandler(this.pbImage_Click);
            // 
            // pl_right
            // 
            this.pl_right.Controls.Add(this.pbDelImage);
            this.pl_right.Dock = System.Windows.Forms.DockStyle.Right;
            this.pl_right.Location = new System.Drawing.Point(386, 1);
            this.pl_right.Name = "pl_right";
            this.pl_right.Size = new System.Drawing.Size(30, 332);
            this.pl_right.TabIndex = 2;
            this.pl_right.Visible = false;
            this.pl_right.Paint += new System.Windows.Forms.PaintEventHandler(this.pl_right_Paint);
            // 
            // pbDelImage
            // 
            this.pbDelImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDelImage.Image = global::IvyBack.Properties.Resources.Del;
            this.pbDelImage.Location = new System.Drawing.Point(2, 11);
            this.pbDelImage.Name = "pbDelImage";
            this.pbDelImage.Size = new System.Drawing.Size(25, 25);
            this.pbDelImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDelImage.TabIndex = 0;
            this.pbDelImage.TabStop = false;
            this.pbDelImage.Click += new System.EventHandler(this.pbDelImage_Click);
            // 
            // UC_ImageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pl_right);
            this.Font = new System.Drawing.Font("宋体", 15F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UC_ImageView";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(417, 334);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UC_ImageView_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.pl_right.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDelImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel pl_right;
        private System.Windows.Forms.PictureBox pbDelImage;
    }
}
