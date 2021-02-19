namespace IvyBack.SysForm.market
{
    partial class MemberAdEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberAdEdit));
            this.pnl = new System.Windows.Forms.Panel();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlimgA = new System.Windows.Forms.Panel();
            this.pnlbottom = new System.Windows.Forms.Panel();
            this.pnlCancel = new System.Windows.Forms.Panel();
            this.pnlOK = new System.Windows.Forms.Panel();
            this.pnl.SuspendLayout();
            this.pnlbottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.txt_name);
            this.pnl.Controls.Add(this.label7);
            this.pnl.Controls.Add(this.label2);
            this.pnl.Controls.Add(this.label1);
            this.pnl.Controls.Add(this.pnlimgA);
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(671, 339);
            this.pnl.TabIndex = 4;
            // 
            // txt_name
            // 
            this.txt_name.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_name.Location = new System.Drawing.Point(46, 243);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(566, 33);
            this.txt_name.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(46, 220);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 20);
            this.label7.TabIndex = 61;
            this.label7.Text = "掌上会员广告链接";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(422, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 50);
            this.label2.TabIndex = 8;
            this.label2.Text = "上传图片格式:JPEG 上传图片大小:300KB 宽370px,高150px";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(46, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "掌上会员广告展示图片";
            // 
            // pnlimgA
            // 
            this.pnlimgA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlimgA.Location = new System.Drawing.Point(46, 56);
            this.pnlimgA.Name = "pnlimgA";
            this.pnlimgA.Size = new System.Drawing.Size(370, 150);
            this.pnlimgA.TabIndex = 6;
            this.pnlimgA.Click += new System.EventHandler(this.pnlimg1_Click);
            this.pnlimgA.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlimg1_Paint);
            // 
            // pnlbottom
            // 
            this.pnlbottom.BackColor = System.Drawing.Color.White;
            this.pnlbottom.Controls.Add(this.pnlCancel);
            this.pnlbottom.Controls.Add(this.pnlOK);
            this.pnlbottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlbottom.Location = new System.Drawing.Point(0, 341);
            this.pnlbottom.Name = "pnlbottom";
            this.pnlbottom.Size = new System.Drawing.Size(671, 78);
            this.pnlbottom.TabIndex = 5;
            // 
            // pnlCancel
            // 
            this.pnlCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCancel.BackgroundImage")));
            this.pnlCancel.Location = new System.Drawing.Point(386, 16);
            this.pnlCancel.Name = "pnlCancel";
            this.pnlCancel.Size = new System.Drawing.Size(114, 45);
            this.pnlCancel.TabIndex = 0;
            this.pnlCancel.Click += new System.EventHandler(this.pnlCancel_Click);
            // 
            // pnlOK
            // 
            this.pnlOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlOK.BackgroundImage")));
            this.pnlOK.Location = new System.Drawing.Point(144, 16);
            this.pnlOK.Name = "pnlOK";
            this.pnlOK.Size = new System.Drawing.Size(114, 45);
            this.pnlOK.TabIndex = 0;
            this.pnlOK.Click += new System.EventHandler(this.pnlOK_Click);
            // 
            // MemberAdEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(671, 419);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.pnlbottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MemberAdEdit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会员中心广告";
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.pnlbottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Panel pnlbottom;
        private System.Windows.Forms.Panel pnlCancel;
        private System.Windows.Forms.Panel pnlOK;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Panel pnlimgA;
        internal System.Windows.Forms.TextBox txt_name;
        internal System.Windows.Forms.Label label7;
    }
}