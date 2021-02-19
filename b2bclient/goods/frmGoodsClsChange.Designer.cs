namespace b2bclient.goods
{
    partial class frmGoodsClsChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGoodsClsChange));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl = new System.Windows.Forms.Panel();
            this.lbl_cls_name = new System.Windows.Forms.Label();
            this.lbl_cls_no = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.pnl_cancel = new System.Windows.Forms.Panel();
            this.pnl_ok = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 450);
            this.panel1.TabIndex = 2;
            // 
            // pnl
            // 
            this.pnl.Controls.Add(this.lbl_cls_name);
            this.pnl.Controls.Add(this.lbl_cls_no);
            this.pnl.Controls.Add(this.checkBox1);
            this.pnl.Controls.Add(this.pnl_cancel);
            this.pnl.Controls.Add(this.pnl_ok);
            this.pnl.Controls.Add(this.label2);
            this.pnl.Controls.Add(this.label1);
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 0);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(500, 450);
            this.pnl.TabIndex = 3;
            // 
            // lbl_cls_name
            // 
            this.lbl_cls_name.AutoSize = true;
            this.lbl_cls_name.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_cls_name.Location = new System.Drawing.Point(170, 121);
            this.lbl_cls_name.Name = "lbl_cls_name";
            this.lbl_cls_name.Size = new System.Drawing.Size(20, 25);
            this.lbl_cls_name.TabIndex = 7;
            this.lbl_cls_name.Text = "-";
            // 
            // lbl_cls_no
            // 
            this.lbl_cls_no.AutoSize = true;
            this.lbl_cls_no.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_cls_no.Location = new System.Drawing.Point(170, 53);
            this.lbl_cls_no.Name = "lbl_cls_no";
            this.lbl_cls_no.Size = new System.Drawing.Size(20, 25);
            this.lbl_cls_no.TabIndex = 6;
            this.lbl_cls_no.Text = "-";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("SimSun", 11.25F);
            this.checkBox1.Location = new System.Drawing.Point(83, 194);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(148, 24);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "是否展示在微商城";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // pnl_cancel
            // 
            this.pnl_cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_cancel.BackgroundImage")));
            this.pnl_cancel.Location = new System.Drawing.Point(284, 271);
            this.pnl_cancel.Name = "pnl_cancel";
            this.pnl_cancel.Size = new System.Drawing.Size(114, 45);
            this.pnl_cancel.TabIndex = 2;
            this.pnl_cancel.Click += new System.EventHandler(this.pnl_cancel_Click);
            // 
            // pnl_ok
            // 
            this.pnl_ok.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_ok.BackgroundImage")));
            this.pnl_ok.Location = new System.Drawing.Point(93, 271);
            this.pnl_ok.Name = "pnl_ok";
            this.pnl_ok.Size = new System.Drawing.Size(114, 45);
            this.pnl_ok.TabIndex = 2;
            this.pnl_ok.Click += new System.EventHandler(this.pnl_ok_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(78, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "分类名称:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(78, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "分类编号:";
            // 
            // frmGoodsClsChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmGoodsClsChange";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "商品分类编辑";
            this.panel1.ResumeLayout(false);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl;
        private System.Windows.Forms.Panel pnl_cancel;
        private System.Windows.Forms.Panel pnl_ok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lbl_cls_name;
        private System.Windows.Forms.Label lbl_cls_no;
    }
}