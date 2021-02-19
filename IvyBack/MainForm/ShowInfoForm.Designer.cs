namespace IvyBack
{
    partial class ShowInfoForm
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
            this.myPanel2 = new IvyBack.cons.MyPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInfoMain = new System.Windows.Forms.Label();
            this.txtInfoDetail = new System.Windows.Forms.TextBox();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.myPanel2.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myPanel2
            // 
            this.myPanel2.BackgroundImage = global::IvyBack.Properties.Resources.顶部;
            this.myPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel2.Controls.Add(this.label2);
            this.myPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel2.Location = new System.Drawing.Point(3, 3);
            this.myPanel2.Name = "myPanel2";
            this.myPanel2.Size = new System.Drawing.Size(627, 53);
            this.myPanel2.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(1);
            this.label2.Size = new System.Drawing.Size(627, 53);
            this.label2.TabIndex = 8;
            this.label2.Text = "     信息提示";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInfoMain
            // 
            this.lblInfoMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfoMain.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfoMain.ForeColor = System.Drawing.Color.Black;
            this.lblInfoMain.Location = new System.Drawing.Point(3, 56);
            this.lblInfoMain.Name = "lblInfoMain";
            this.lblInfoMain.Size = new System.Drawing.Size(627, 99);
            this.lblInfoMain.TabIndex = 16;
            this.lblInfoMain.Text = "信息提示";
            this.lblInfoMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInfoDetail
            // 
            this.txtInfoDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfoDetail.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txtInfoDetail.Location = new System.Drawing.Point(3, 155);
            this.txtInfoDetail.Multiline = true;
            this.txtInfoDetail.Name = "txtInfoDetail";
            this.txtInfoDetail.ReadOnly = true;
            this.txtInfoDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInfoDetail.Size = new System.Drawing.Size(627, 263);
            this.txtInfoDetail.TabIndex = 17;
            this.txtInfoDetail.Text = "详细信息";
            // 
            // myPanel1
            // 
            this.myPanel1.Controls.Add(this.btnNo);
            this.myPanel1.Controls.Add(this.btnOK);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.myPanel1.Location = new System.Drawing.Point(3, 418);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(627, 60);
            this.myPanel1.TabIndex = 18;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = global::IvyBack.Properties.Resources.按钮_1;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(500, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 50);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.Transparent;
            this.btnNo.BackgroundImage = global::IvyBack.Properties.Resources.按钮_2;
            this.btnNo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.Location = new System.Drawing.Point(339, 4);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(120, 50);
            this.btnNo.TabIndex = 13;
            this.btnNo.Text = "取消";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Visible = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // ShowInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(633, 481);
            this.Controls.Add(this.txtInfoDetail);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.lblInfoMain);
            this.Controls.Add(this.myPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowInfoForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowInfoForm";
            this.Shown += new System.EventHandler(this.ShowInfoForm_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ShowInfoForm_Paint);
            this.myPanel2.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private cons.MyPanel myPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInfoMain;
        private System.Windows.Forms.TextBox txtInfoDetail;
        private cons.MyPanel myPanel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnNo;
    }
}