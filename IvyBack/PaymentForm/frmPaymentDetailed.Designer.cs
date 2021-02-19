namespace IvyBack.PaymentForm
{
    partial class frmPaymentDetailed
    {
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnOk;
        private cons.MyPanel myPanel1;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaymentDetailed));
            this.btnNo = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.myPanel1 = new IvyBack.cons.MyPanel();
            this.editGrid1 = new IvyBack.cons.EditGrid();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Location = new System.Drawing.Point(110, 2);
            this.btnNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(100, 35);
            this.btnNo.TabIndex = 10004;
            this.btnNo.Text = "取消";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(5, 2);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 35);
            this.btnOk.TabIndex = 10003;
            this.btnOk.Text = "确认";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // myPanel1
            // 
            this.myPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.myPanel1.Controls.Add(this.btnOk);
            this.myPanel1.Controls.Add(this.btnNo);
            this.myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(524, 40);
            this.myPanel1.TabIndex = 10006;
            // 
            // editGrid1
            // 
            this.editGrid1.Editing = false;
            this.editGrid1.IsAutoAddRow = true;
            this.editGrid1.IsShowIco = true;
            this.editGrid1.Location = new System.Drawing.Point(4, 43);
            this.editGrid1.Name = "editGrid1";
            this.editGrid1.RowHeight = 25;
            this.editGrid1.Size = new System.Drawing.Size(519, 301);
            this.editGrid1.TabIndex = 10007;
            this.editGrid1.Text = "editGrid1";
            this.editGrid1.CellEndEdit += new IvyBack.cons.EditGrid.CellEndEditHandler(this.editGrid1_CellEndEdit);
            // 
            // frmPaymentDetailed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(524, 346);
            this.Controls.Add(this.editGrid1);
            this.Controls.Add(this.myPanel1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaymentDetailed";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "支付明细";
            this.Load += new System.EventHandler(this.frmPaymentUpload_Load);
            this.Shown += new System.EventHandler(this.frmPaymentUpload_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPaymentUpload_KeyDown);
            this.myPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private cons.EditGrid editGrid1;
    }
}