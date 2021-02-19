namespace IvyFront.Forms
{
    partial class ChooseGoods2
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
            this.pnlboard = new IvyFront.Forms.ChooseGoods2.MyPanel();
            this.panel1 = new IvyFront.Forms.ChooseGoods2.MyPanel();
            this.lbl = new IvyFront.Forms.MyLabel();
            this.label20 = new IvyFront.Forms.MyLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlboard
            // 
            this.pnlboard.BackColor = System.Drawing.Color.White;
            this.pnlboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlboard.Location = new System.Drawing.Point(0, 43);
            this.pnlboard.Name = "pnlboard";
            this.pnlboard.Size = new System.Drawing.Size(771, 236);
            this.pnlboard.TabIndex = 7;
            this.pnlboard.Click += new System.EventHandler(this.pnlboard_Click);
            this.pnlboard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlboard_Paint);
            this.pnlboard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlboard_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.lbl);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(771, 43);
            this.panel1.TabIndex = 8;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.lbl.ForeColor = System.Drawing.Color.White;
            this.lbl.Location = new System.Drawing.Point(88, 8);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(30, 27);
            this.lbl.TabIndex = 6;
            this.lbl.Text = "--";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(6, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 27);
            this.label20.TabIndex = 6;
            this.label20.Text = "关键词：";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // ChooseGoods2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 279);
            this.Controls.Add(this.pnlboard);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChooseGoods2";
            this.Text = "ChooseGoods2";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyPanel pnlboard;
        private MyPanel  panel1;
        private MyLabel  label20;
        private System.Windows.Forms.Timer timer1;
        private MyLabel lbl;
        private System.Windows.Forms.Timer timer2;
    }
}