namespace IvyFront.Forms
{
    partial class frmGoodsCls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGoodsCls));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_first = new System.Windows.Forms.Panel();
            this.pnl_prev = new System.Windows.Forms.Panel();
            this.pnl_next = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.pnl_last = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnl_close = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column1,
            this.Column7});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(1022, 551);
            this.dataGridView1.TabIndex = 52;
            // 
            // Column5
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column5.HeaderText = "序号";
            this.Column5.Name = "Column5";
            this.Column5.Width = 60;
            // 
            // Column6
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column6.HeaderText = "品类编号";
            this.Column6.Name = "Column6";
            this.Column6.Width = 200;
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "品类名称";
            this.Column1.Name = "Column1";
            this.Column1.Width = 300;
            // 
            // Column7
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column7.HeaderText = "状态";
            this.Column7.Name = "Column7";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(409, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 43);
            this.label2.TabIndex = 0;
            this.label2.Text = "品类启/停用";
            // 
            // panel7
            // 
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
            this.panel9.Controls.Add(this.btn_stop);
            this.panel9.Controls.Add(this.btn_start);
            this.panel9.Controls.Add(this.btn_search);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1022, 60);
            this.panel9.TabIndex = 66;
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(119)))), ((int)(((byte)(206)))));
            this.btn_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stop.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_stop.ForeColor = System.Drawing.Color.White;
            this.btn_stop.Location = new System.Drawing.Point(255, 6);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(120, 45);
            this.btn_stop.TabIndex = 69;
            this.btn_stop.Text = "停用";
            this.btn_stop.UseVisualStyleBackColor = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(119)))), ((int)(((byte)(206)))));
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_start.ForeColor = System.Drawing.Color.White;
            this.btn_start.Location = new System.Drawing.Point(129, 6);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(120, 45);
            this.btn_start.TabIndex = 68;
            this.btn_start.Text = "启用";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(119)))), ((int)(((byte)(206)))));
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_search.ForeColor = System.Drawing.Color.White;
            this.btn_search.Location = new System.Drawing.Point(3, 6);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(120, 45);
            this.btn_search.TabIndex = 67;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.pnl_search_Click);
            // 
            // frmGoodsCls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmGoodsCls";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmGoodsCls_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnl_close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}
