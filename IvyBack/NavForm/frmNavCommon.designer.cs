namespace IvyBack.MainForm
{
    partial class frmNavCommon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNavCommon));
            this.pnl2 = new IvyBack.cons.MyPanel();
            this.pnl1 = new IvyBack.cons.MyPanel();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOperFnc = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddMyLove = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl2
            // 
            this.pnl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl2.Location = new System.Drawing.Point(0, 444);
            this.pnl2.Name = "pnl2";
            this.pnl2.Size = new System.Drawing.Size(922, 184);
            this.pnl2.TabIndex = 12;
            this.pnl2.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl2_Paint);
            this.pnl2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl2_MouseClick);
            this.pnl2.MouseLeave += new System.EventHandler(this.pnl2_MouseLeave);
            this.pnl2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl2_MouseMove);
            // 
            // pnl1
            // 
            this.pnl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl1.Location = new System.Drawing.Point(0, 0);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(922, 444);
            this.pnl1.TabIndex = 13;
            this.pnl1.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl1_Paint);
            this.pnl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl1_MouseClick);
            this.pnl1.MouseLeave += new System.EventHandler(this.pnl1_MouseLeave);
            this.pnl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl1_MouseMove);
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOperFnc,
            this.tsmiAddMyLove});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(125, 48);
            // 
            // tsmiOperFnc
            // 
            this.tsmiOperFnc.Name = "tsmiOperFnc";
            this.tsmiOperFnc.Size = new System.Drawing.Size(124, 22);
            this.tsmiOperFnc.Text = "打开";
            // 
            // tsmiAddMyLove
            // 
            this.tsmiAddMyLove.Name = "tsmiAddMyLove";
            this.tsmiAddMyLove.Size = new System.Drawing.Size(124, 22);
            this.tsmiAddMyLove.Text = "加入收藏";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(20, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(112, 41);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(405, 202);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // frmNavCommon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(922, 628);
            this.Controls.Add(this.pnl1);
            this.Controls.Add(this.pnl2);
            this.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmNavCommon";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "信息档案";
            this.VisibleChanged += new System.EventHandler(this.frmNavCommon_VisibleChanged);
            this.MouseLeave += new System.EventHandler(this.frmNavCommon_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmNavCommon_MouseMove);
            this.cms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem tsmiOperFnc;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddMyLove;
        private System.Windows.Forms.PictureBox pictureBox1;
        private cons.MyPanel pnl2;
        private cons.MyPanel pnl1;

    }
}