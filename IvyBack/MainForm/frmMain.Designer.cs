using System.Resources;
namespace IvyBack
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.windowsList1 = new IvyBack.cons.WindowsList();
            this.panTop = new IvyBack.cons.MyPanel();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panTopLeft = new IvyBack.cons.MyPanel();
            this.myPanel2 = new IvyBack.cons.MyPanel();
            this.myPanel3 = new IvyBack.cons.MyPanel();
            this.myPanel6 = new IvyBack.cons.MyPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlClose = new IvyBack.cons.MyPanel();
            this.myPanel4 = new IvyBack.cons.MyPanel();
            this.myPanel7 = new IvyBack.cons.MyPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panTop.SuspendLayout();
            this.myPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.myPanel2.SuspendLayout();
            this.myPanel3.SuspendLayout();
            this.myPanel6.SuspendLayout();
            this.myPanel4.SuspendLayout();
            this.myPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowsList1
            // 
            this.windowsList1.BackColor = System.Drawing.Color.White;
            this.windowsList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowsList1.HeaderFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowsList1.HeaderHeight = 30;
            this.windowsList1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.windowsList1.Location = new System.Drawing.Point(0, 82);
            this.windowsList1.Margin = new System.Windows.Forms.Padding(0);
            this.windowsList1.Name = "windowsList1";
            this.windowsList1.Size = new System.Drawing.Size(1008, 648);
            this.windowsList1.TabIndex = 1;
            this.windowsList1.TabStop = false;
            this.windowsList1.Text = "生鲜ERP";
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.myPanel1);
            this.panTop.Controls.Add(this.myPanel2);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(1008, 82);
            this.panTop.TabIndex = 2;
            // 
            // myPanel1
            // 
            this.myPanel1.BackgroundImage = global::IvyBack.Properties.Resources.pnl_top_fill;
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel1.Controls.Add(this.pictureBox1);
            this.myPanel1.Controls.Add(this.panTopLeft);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(822, 82);
            this.myPanel1.TabIndex = 1;
            this.myPanel1.Tag = "[name:顶部拉伸条]";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::IvyBack.Properties.Resources.小标题;
            this.pictureBox1.InitialImage = global::IvyBack.Properties.Resources.小标题;
            this.pictureBox1.Location = new System.Drawing.Point(181, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(274, 24);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // panTopLeft
            // 
            this.panTopLeft.BackColor = System.Drawing.Color.Transparent;
            this.panTopLeft.BackgroundImage = global::IvyBack.Properties.Resources.cptx_logo;
            this.panTopLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panTopLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panTopLeft.Location = new System.Drawing.Point(0, 0);
            this.panTopLeft.Name = "panTopLeft";
            this.panTopLeft.Size = new System.Drawing.Size(175, 82);
            this.panTopLeft.TabIndex = 1;
            this.panTopLeft.Tag = "[name:此logo只用在主界面]";
            // 
            // myPanel2
            // 
            this.myPanel2.BackgroundImage = global::IvyBack.Properties.Resources.pnl_top_fill;
            this.myPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel2.Controls.Add(this.myPanel3);
            this.myPanel2.Controls.Add(this.pnlClose);
            this.myPanel2.Controls.Add(this.myPanel4);
            this.myPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.myPanel2.Location = new System.Drawing.Point(822, 0);
            this.myPanel2.Name = "myPanel2";
            this.myPanel2.Size = new System.Drawing.Size(186, 82);
            this.myPanel2.TabIndex = 2;
            this.myPanel2.Tag = "[name:顶部右侧2]";
            // 
            // myPanel3
            // 
            this.myPanel3.BackColor = System.Drawing.Color.Transparent;
            this.myPanel3.BackgroundImage = global::IvyBack.Properties.Resources.购物车;
            this.myPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.myPanel3.Controls.Add(this.myPanel6);
            this.myPanel3.Location = new System.Drawing.Point(6, 9);
            this.myPanel3.Name = "myPanel3";
            this.myPanel3.Size = new System.Drawing.Size(51, 61);
            this.myPanel3.TabIndex = 1;
            this.myPanel3.Tag = "[name:购物车]";
            this.myPanel3.Visible = false;
            // 
            // myPanel6
            // 
            this.myPanel6.BackgroundImage = global::IvyBack.Properties.Resources.圆圈;
            this.myPanel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel6.Controls.Add(this.label1);
            this.myPanel6.Location = new System.Drawing.Point(29, 7);
            this.myPanel6.Name = "myPanel6";
            this.myPanel6.Size = new System.Drawing.Size(20, 20);
            this.myPanel6.TabIndex = 3;
            this.myPanel6.Tag = "[name:圆圈]";
            this.myPanel6.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "10";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlClose
            // 
            this.pnlClose.BackColor = System.Drawing.Color.Transparent;
            this.pnlClose.BackgroundImage = global::IvyBack.Properties.Resources.关闭;
            this.pnlClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlClose.Location = new System.Drawing.Point(133, 9);
            this.pnlClose.Name = "pnlClose";
            this.pnlClose.Size = new System.Drawing.Size(41, 61);
            this.pnlClose.TabIndex = 3;
            this.pnlClose.Tag = "[name:关闭]";
            this.pnlClose.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlClose_Paint);
            this.pnlClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlClose_MouseUp);
            // 
            // myPanel4
            // 
            this.myPanel4.BackColor = System.Drawing.Color.Transparent;
            this.myPanel4.BackgroundImage = global::IvyBack.Properties.Resources.警铃;
            this.myPanel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.myPanel4.Controls.Add(this.myPanel7);
            this.myPanel4.Location = new System.Drawing.Point(69, 9);
            this.myPanel4.Name = "myPanel4";
            this.myPanel4.Size = new System.Drawing.Size(46, 61);
            this.myPanel4.TabIndex = 2;
            this.myPanel4.Tag = "[name:警铃]";
            // 
            // myPanel7
            // 
            this.myPanel7.BackgroundImage = global::IvyBack.Properties.Resources.圆圈;
            this.myPanel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel7.Controls.Add(this.label2);
            this.myPanel7.Location = new System.Drawing.Point(23, 7);
            this.myPanel7.Name = "myPanel7";
            this.myPanel7.Size = new System.Drawing.Size(20, 20);
            this.myPanel7.TabIndex = 4;
            this.myPanel7.Tag = "[name:圆圈]";
            this.myPanel7.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "10";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.windowsList1);
            this.Controls.Add(this.panTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "财务管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.panTop.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.myPanel2.ResumeLayout(false);
            this.myPanel3.ResumeLayout(false);
            this.myPanel6.ResumeLayout(false);
            this.myPanel4.ResumeLayout(false);
            this.myPanel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private cons.WindowsList windowsList1;
        private cons.MyPanel panTop;
        private cons.MyPanel myPanel1;
        private cons.MyPanel panTopLeft;
        private cons.MyPanel myPanel2;
        private cons.MyPanel myPanel3;
        private cons.MyPanel myPanel6;
        private System.Windows.Forms.Label label1;
        private cons.MyPanel pnlClose;
        private cons.MyPanel myPanel4;
        private cons.MyPanel myPanel7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

