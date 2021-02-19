namespace IvyFront.Forms
{
    partial class frmXSOrderDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmXSOrderDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_first = new System.Windows.Forms.Panel();
            this.pnl_prev = new System.Windows.Forms.Panel();
            this.pnl_next = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.pnl_last = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_title = new System.Windows.Forms.Label();
            this.pnlclose = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbl_total_amt = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_order_count = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_first
            // 
            this.pnl_first.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_first.BackgroundImage")));
            this.pnl_first.Location = new System.Drawing.Point(687, 2);
            this.pnl_first.Name = "pnl_first";
            this.pnl_first.Size = new System.Drawing.Size(81, 66);
            this.pnl_first.TabIndex = 55;
            this.pnl_first.Click += new System.EventHandler(this.pnl_first_Click);
            // 
            // pnl_prev
            // 
            this.pnl_prev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_prev.BackgroundImage")));
            this.pnl_prev.Location = new System.Drawing.Point(767, 2);
            this.pnl_prev.Name = "pnl_prev";
            this.pnl_prev.Size = new System.Drawing.Size(81, 66);
            this.pnl_prev.TabIndex = 56;
            this.pnl_prev.Click += new System.EventHandler(this.pnl_prev_Click);
            // 
            // pnl_next
            // 
            this.pnl_next.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_next.BackgroundImage")));
            this.pnl_next.Location = new System.Drawing.Point(847, 2);
            this.pnl_next.Name = "pnl_next";
            this.pnl_next.Size = new System.Drawing.Size(88, 66);
            this.pnl_next.TabIndex = 53;
            this.pnl_next.Click += new System.EventHandler(this.pnl_next_Click);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label1.ForeColor = System.Drawing.Color.Gray;
            this.Label1.Location = new System.Drawing.Point(511, 26);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(161, 20);
            this.Label1.TabIndex = 57;
            this.Label1.Text = "第N页，共M页";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_last
            // 
            this.pnl_last.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_last.BackgroundImage")));
            this.pnl_last.Location = new System.Drawing.Point(934, 2);
            this.pnl_last.Name = "pnl_last";
            this.pnl_last.Size = new System.Drawing.Size(88, 66);
            this.pnl_last.TabIndex = 54;
            this.pnl_last.Click += new System.EventHandler(this.pnl_last_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column1,
            this.Column8,
            this.Column7,
            this.Column2,
            this.Column4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(1024, 638);
            this.dataGridView1.TabIndex = 52;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.lbl_title);
            this.panel5.Controls.Add(this.pnlclose);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1024, 60);
            this.panel5.TabIndex = 67;
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(12, 18);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(112, 27);
            this.lbl_title.TabIndex = 5;
            this.lbl_title.Text = "销售单明细";
            // 
            // pnlclose
            // 
            this.pnlclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlclose.BackgroundImage")));
            this.pnlclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlclose.Location = new System.Drawing.Point(871, 0);
            this.pnlclose.Name = "pnlclose";
            this.pnlclose.Size = new System.Drawing.Size(150, 59);
            this.pnlclose.TabIndex = 4;
            this.pnlclose.Click += new System.EventHandler(this.pnl_close_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lbl_total_amt);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.lbl_order_count);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.pnl_first);
            this.panel7.Controls.Add(this.pnl_last);
            this.panel7.Controls.Add(this.Label1);
            this.panel7.Controls.Add(this.pnl_next);
            this.panel7.Controls.Add(this.pnl_prev);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 698);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1024, 70);
            this.panel7.TabIndex = 68;
            // 
            // lbl_total_amt
            // 
            this.lbl_total_amt.AutoSize = true;
            this.lbl_total_amt.Font = new System.Drawing.Font("SimSun", 16F);
            this.lbl_total_amt.ForeColor = System.Drawing.Color.Red;
            this.lbl_total_amt.Location = new System.Drawing.Point(329, 22);
            this.lbl_total_amt.Name = "lbl_total_amt";
            this.lbl_total_amt.Size = new System.Drawing.Size(57, 30);
            this.lbl_total_amt.TabIndex = 69;
            this.lbl_total_amt.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 14F);
            this.label6.Location = new System.Drawing.Point(230, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 25);
            this.label6.TabIndex = 68;
            this.label6.Text = "合计金额:";
            // 
            // lbl_order_count
            // 
            this.lbl_order_count.AutoSize = true;
            this.lbl_order_count.Font = new System.Drawing.Font("SimSun", 16F);
            this.lbl_order_count.ForeColor = System.Drawing.Color.Red;
            this.lbl_order_count.Location = new System.Drawing.Point(107, 22);
            this.lbl_order_count.Name = "lbl_order_count";
            this.lbl_order_count.Size = new System.Drawing.Size(57, 30);
            this.lbl_order_count.TabIndex = 67;
            this.lbl_order_count.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 14F);
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 25);
            this.label4.TabIndex = 66;
            this.label4.Text = "合计数量:";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 60);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1024, 638);
            this.panel8.TabIndex = 69;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.dataGridView1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1024, 638);
            this.panel10.TabIndex = 67;
            // 
            // Column5
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column5.HeaderText = "序号";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "货号";
            this.Column6.Name = "Column6";
            this.Column6.Width = 140;
            // 
            // Column1
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column1.HeaderText = "商品";
            this.Column1.Name = "Column1";
            this.Column1.Width = 240;
            // 
            // Column8
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column8.HeaderText = "单位";
            this.Column8.Name = "Column8";
            this.Column8.Width = 140;
            // 
            // Column7
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column7.HeaderText = "数量";
            this.Column7.Name = "Column7";
            this.Column7.Width = 130;
            // 
            // Column2
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle13;
            this.Column2.HeaderText = "价格";
            this.Column2.Name = "Column2";
            this.Column2.Width = 130;
            // 
            // Column4
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column4.HeaderText = "小计";
            this.Column4.Name = "Column4";
            this.Column4.Width = 130;
            // 
            // frmXSOrderDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmXSOrderDetail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmXSOrderDetail_FormClosed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmXSOrderDetail_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_first;
        private System.Windows.Forms.Panel pnl_prev;
        private System.Windows.Forms.Panel pnl_next;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Panel pnl_last;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lbl_total_amt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_order_count;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel pnlclose;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}
