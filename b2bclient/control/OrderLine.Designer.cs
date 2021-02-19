namespace b2bclient.control
{
    partial class OrderLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderLine));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblgoodsname = new System.Windows.Forms.Label();
            this.lblqty = new System.Windows.Forms.Label();
            this.lblamount = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblremark = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblindex = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(10, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(83, 26);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lblgoodsname
            // 
            this.lblgoodsname.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblgoodsname.Location = new System.Drawing.Point(132, 6);
            this.lblgoodsname.Name = "lblgoodsname";
            this.lblgoodsname.Size = new System.Drawing.Size(129, 21);
            this.lblgoodsname.TabIndex = 1;
            this.lblgoodsname.Text = "产品名称";
            // 
            // lblqty
            // 
            this.lblqty.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblqty.Location = new System.Drawing.Point(269, 6);
            this.lblqty.Name = "lblqty";
            this.lblqty.Size = new System.Drawing.Size(53, 20);
            this.lblqty.TabIndex = 1;
            this.lblqty.Text = "数量";
            // 
            // lblamount
            // 
            this.lblamount.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblamount.Location = new System.Drawing.Point(330, 6);
            this.lblamount.Name = "lblamount";
            this.lblamount.Size = new System.Drawing.Size(112, 20);
            this.lblamount.TabIndex = 1;
            this.lblamount.Text = "金额";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 39);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 29);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(50, 39);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 29);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // lblremark
            // 
            this.lblremark.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblremark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(187)))), ((int)(((byte)(166)))));
            this.lblremark.Location = new System.Drawing.Point(150, 26);
            this.lblremark.Name = "lblremark";
            this.lblremark.Size = new System.Drawing.Size(292, 21);
            this.lblremark.TabIndex = 4;
            this.lblremark.Text = "产品名称";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Location = new System.Drawing.Point(138, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(13, 14);
            this.panel2.TabIndex = 5;
            // 
            // lblindex
            // 
            this.lblindex.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblindex.Location = new System.Drawing.Point(99, 6);
            this.lblindex.Name = "lblindex";
            this.lblindex.Size = new System.Drawing.Size(30, 20);
            this.lblindex.TabIndex = 6;
            this.lblindex.Text = "序号";
            this.lblindex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OrderLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblindex);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblremark);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblamount);
            this.Controls.Add(this.lblqty);
            this.Controls.Add(this.lblgoodsname);
            this.Controls.Add(this.panel1);
            this.Name = "OrderLine";
            this.Size = new System.Drawing.Size(445, 49);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblgoodsname;
        private System.Windows.Forms.Label lblqty;
        private System.Windows.Forms.Label lblamount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblremark;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblindex;
    }
}
