namespace IvyBack.NavForm
{
    partial class frmNav
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
            this.moduleList1 = new IvyBack.cons.ModuleList();
            this.SuspendLayout();
            // 
            // moduleList1
            // 
            this.moduleList1.BackColor = System.Drawing.Color.White;
            this.moduleList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moduleList1.HeaderBackImage = null;
            this.moduleList1.HeaderFont = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.moduleList1.HeaderWidth = 180;
            this.moduleList1.IconSize = 30;
            this.moduleList1.Location = new System.Drawing.Point(0, 0);
            this.moduleList1.Name = "moduleList1";
            this.moduleList1.Size = new System.Drawing.Size(861, 485);
            this.moduleList1.TabIndex = 2;
            this.moduleList1.TabStop = false;
            this.moduleList1.Paint += new System.Windows.Forms.PaintEventHandler(this.moduleList1_Paint);
            // 
            // frmNav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(861, 485);
            this.Controls.Add(this.moduleList1);
            this.Name = "frmNav";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "导航";
            this.Shown += new System.EventHandler(this.frmNav_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private cons.ModuleList moduleList1;
    }
}