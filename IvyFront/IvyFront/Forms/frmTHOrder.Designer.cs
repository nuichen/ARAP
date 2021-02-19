namespace IvyFront.Forms
{
    partial class frmTHOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTHOrder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbl_fact_amt = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_total_amt = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_order_count = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btn_search = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pnldate2 = new System.Windows.Forms.Panel();
            this.pnldate1 = new System.Windows.Forms.Panel();
            this.pnl_first = new System.Windows.Forms.Panel();
            this.pnl_last = new System.Windows.Forms.Panel();
            this.pnl_next = new System.Windows.Forms.Panel();
            this.pnl_prev = new System.Windows.Forms.Panel();
            this.pnl_close = new System.Windows.Forms.Panel();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(8, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 65;
            this.label3.Text = "日期";
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
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column8,
            this.Column7,
            this.Column2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(1022, 551);
            this.dataGridView1.TabIndex = 52;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(119)))), ((int)(((byte)(206)))));
            this.panel5.Controls.Add(this.pnl_close);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(1, 1);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1022, 85);
            this.panel5.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(403, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 43);
            this.label2.TabIndex = 0;
            this.label2.Text = "销售退货入库单";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lbl_fact_amt);
            this.panel7.Controls.Add(this.label8);
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
            this.panel7.Location = new System.Drawing.Point(1, 697);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1022, 70);
            this.panel7.TabIndex = 68;
            // 
            // lbl_fact_amt
            // 
            this.lbl_fact_amt.AutoSize = true;
            this.lbl_fact_amt.Font = new System.Drawing.Font("SimSun", 16F);
            this.lbl_fact_amt.ForeColor = System.Drawing.Color.Red;
            this.lbl_fact_amt.Location = new System.Drawing.Point(430, 22);
            this.lbl_fact_amt.Name = "lbl_fact_amt";
            this.lbl_fact_amt.Size = new System.Drawing.Size(57, 30);
            this.lbl_fact_amt.TabIndex = 71;
            this.lbl_fact_amt.Text = "0.00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 14F);
            this.label8.Location = new System.Drawing.Point(339, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 25);
            this.label8.TabIndex = 70;
            this.label8.Text = "合计实退:";
            // 
            // lbl_total_amt
            // 
            this.lbl_total_amt.AutoSize = true;
            this.lbl_total_amt.Font = new System.Drawing.Font("SimSun", 16F);
            this.lbl_total_amt.ForeColor = System.Drawing.Color.Red;
            this.lbl_total_amt.Location = new System.Drawing.Point(213, 22);
            this.lbl_total_amt.Name = "lbl_total_amt";
            this.lbl_total_amt.Size = new System.Drawing.Size(57, 30);
            this.lbl_total_amt.TabIndex = 69;
            this.lbl_total_amt.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 14F);
            this.label6.Location = new System.Drawing.Point(141, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 25);
            this.label6.TabIndex = 68;
            this.label6.Text = "总金额:";
            // 
            // lbl_order_count
            // 
            this.lbl_order_count.AutoSize = true;
            this.lbl_order_count.Font = new System.Drawing.Font("SimSun", 16F);
            this.lbl_order_count.ForeColor = System.Drawing.Color.Red;
            this.lbl_order_count.Location = new System.Drawing.Point(80, 22);
            this.lbl_order_count.Name = "lbl_order_count";
            this.lbl_order_count.Size = new System.Drawing.Size(26, 30);
            this.lbl_order_count.TabIndex = 67;
            this.lbl_order_count.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 14F);
            this.label4.Location = new System.Drawing.Point(8, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 25);
            this.label4.TabIndex = 66;
            this.label4.Text = "订单数:";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(1, 86);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1022, 611);
            this.panel8.TabIndex = 69;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.dataGridView1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 60);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1022, 551);
            this.panel10.TabIndex = 67;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btn_search);
            this.panel9.Controls.Add(this.label5);
            this.panel9.Controls.Add(this.pnldate2);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Controls.Add(this.pnldate1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1022, 60);
            this.panel9.TabIndex = 66;
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(119)))), ((int)(((byte)(206)))));
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_search.ForeColor = System.Drawing.Color.White;
            this.btn_search.Location = new System.Drawing.Point(478, 7);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(120, 45);
            this.btn_search.TabIndex = 67;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.pnl_search_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(246, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 20);
            this.label5.TabIndex = 66;
            this.label5.Text = "至";
            // 
            // pnldate2
            // 
            this.pnldate2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnldate2.BackgroundImage")));
            this.pnldate2.Location = new System.Drawing.Point(276, 12);
            this.pnldate2.Name = "pnldate2";
            this.pnldate2.Size = new System.Drawing.Size(189, 38);
            this.pnldate2.TabIndex = 59;
            this.pnldate2.Click += new System.EventHandler(this.pnldate2_Click);
            this.pnldate2.Paint += new System.Windows.Forms.PaintEventHandler(this.pnldate2_Paint);
            // 
            // pnldate1
            // 
            this.pnldate1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnldate1.BackgroundImage")));
            this.pnldate1.Location = new System.Drawing.Point(51, 12);
            this.pnldate1.Name = "pnldate1";
            this.pnldate1.Size = new System.Drawing.Size(189, 38);
            this.pnldate1.TabIndex = 60;
            this.pnldate1.Click += new System.EventHandler(this.pnldate1_Click);
            this.pnldate1.Paint += new System.Windows.Forms.PaintEventHandler(this.pnldate1_Paint);
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
            // pnl_last
            // 
            this.pnl_last.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_last.BackgroundImage")));
            this.pnl_last.Location = new System.Drawing.Point(934, 2);
            this.pnl_last.Name = "pnl_last";
            this.pnl_last.Size = new System.Drawing.Size(88, 66);
            this.pnl_last.TabIndex = 54;
            this.pnl_last.Click += new System.EventHandler(this.pnl_last_Click);
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
            // pnl_prev
            // 
            this.pnl_prev.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_prev.BackgroundImage")));
            this.pnl_prev.Location = new System.Drawing.Point(767, 2);
            this.pnl_prev.Name = "pnl_prev";
            this.pnl_prev.Size = new System.Drawing.Size(81, 66);
            this.pnl_prev.TabIndex = 56;
            this.pnl_prev.Click += new System.EventHandler(this.pnl_prev_Click);
            // 
            // pnl_close
            // 
            this.pnl_close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_close.BackgroundImage")));
            this.pnl_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_close.ForeColor = System.Drawing.Color.Transparent;
            this.pnl_close.Location = new System.Drawing.Point(948, 6);
            this.pnl_close.Name = "pnl_close";
            this.pnl_close.Size = new System.Drawing.Size(70, 70);
            this.pnl_close.TabIndex = 1;
            this.pnl_close.Click += new System.EventHandler(this.pnl_close_Click);
            // 
            // Column5
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column5.HeaderText = "序号";
            this.Column5.Name = "Column5";
            this.Column5.Width = 60;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "单号";
            this.Column6.Name = "Column6";
            this.Column6.Width = 180;
            // 
            // Column1
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column1.HeaderText = "客户";
            this.Column1.Name = "Column1";
            this.Column1.Width = 210;
            // 
            // Column3
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column3.HeaderText = "付款方式";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle13;
            this.Column4.HeaderText = "总金额";
            this.Column4.Name = "Column4";
            // 
            // Column8
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column8.HeaderText = "实付金额";
            this.Column8.Name = "Column8";
            // 
            // Column7
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle15;
            this.Column7.HeaderText = "状态";
            this.Column7.Name = "Column7";
            // 
            // Column2
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle16;
            this.Column2.HeaderText = "操作时间";
            this.Column2.Name = "Column2";
            this.Column2.Width = 160;
            // 
            // frmTHOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTHOrder";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmXSOrder_FormClosed);
            this.Click += new System.EventHandler(this.frmSaleFlow_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnldate2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnldate1;
        private System.Windows.Forms.Panel pnl_first;
        private System.Windows.Forms.Panel pnl_prev;
        private System.Windows.Forms.Panel pnl_next;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Panel pnl_last;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnl_close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lbl_total_amt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_order_count;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_fact_amt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}
