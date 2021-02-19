namespace IvyBack.VoucherForm
{
    partial class OrderMerge
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new IvyBack.cons.MyPanel();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.myButton1 = new IvyBack.cons.MyButton();
            this.myButton2 = new IvyBack.cons.MyButton();
            this.panel2.SuspendLayout();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 672);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel2.Controls.Add(this.myPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 672);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1006, 49);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // myPanel1
            // 
            this.myPanel1.BackgroundImage = global::IvyBack.Properties.Resources.单据Tab1;
            this.myPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.myPanel1.Controls.Add(this.myButton1);
            this.myPanel1.Controls.Add(this.myButton2);
            this.myPanel1.Location = new System.Drawing.Point(1, 1);
            this.myPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(273, 32);
            this.myPanel1.TabIndex = 1;
            // 
            // myButton1
            // 
            this.myButton1.BackColor = System.Drawing.Color.Transparent;
            this.myButton1.ButtonColor = System.Drawing.Color.Transparent;
            this.myButton1.Font = new System.Drawing.Font("宋体", 10F);
            this.myButton1.ForeColor = System.Drawing.Color.White;
            this.myButton1.Location = new System.Drawing.Point(4, 2);
            this.myButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(116, 26);
            this.myButton1.TabIndex = 0;
            this.myButton1.TabStop = false;
            this.myButton1.Text = "单据浏览";
            this.myButton1.Click += new System.EventHandler(this.myButton1_Click);
            // 
            // myButton2
            // 
            this.myButton2.BackColor = System.Drawing.Color.Transparent;
            this.myButton2.ButtonColor = System.Drawing.Color.Transparent;
            this.myButton2.Font = new System.Drawing.Font("宋体", 10F);
            this.myButton2.ForeColor = System.Drawing.Color.White;
            this.myButton2.Location = new System.Drawing.Point(128, -2);
            this.myButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.myButton2.Name = "myButton2";
            this.myButton2.Size = new System.Drawing.Size(145, 35);
            this.myButton2.TabIndex = 0;
            this.myButton2.TabStop = false;
            this.myButton2.Text = "单据录入";
            this.myButton2.Click += new System.EventHandler(this.myButton2_Click);
            // 
            // OrderMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OrderMerge";
            this.ShowIcon = false;
            this.Text = "单据";
            this.Shown += new System.EventHandler(this.OrderMerge_Shown);
            this.panel2.ResumeLayout(false);
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private cons.MyPanel panel2;
        private cons.MyButton myButton2;
        private cons.MyButton myButton1;
        private cons.MyPanel myPanel1;
    }
}