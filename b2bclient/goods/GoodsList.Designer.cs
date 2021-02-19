namespace b2bclient.goods
{
    partial class GoodsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoodsList));
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl_top = new System.Windows.Forms.Panel();
            this.chkNoShow = new System.Windows.Forms.CheckBox();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnl_bottom = new System.Windows.Forms.Panel();
            this.btn_last = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_first = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_prev = new System.Windows.Forms.Button();
            this.pnl_container = new System.Windows.Forms.Panel();
            this.dg_data = new b2bclient.control.DataGrid();
            this.pnl_top.SuspendLayout();
            this.pnl_bottom.SuspendLayout();
            this.pnl_container.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(644, -48);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(189, 38);
            this.panel5.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "分类";
            // 
            // pnl_top
            // 
            this.pnl_top.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_top.Controls.Add(this.chkNoShow);
            this.pnl_top.Controls.Add(this.btn_edit);
            this.pnl_top.Controls.Add(this.btn_search);
            this.pnl_top.Controls.Add(this.label1);
            this.pnl_top.Controls.Add(this.textBox1);
            this.pnl_top.Controls.Add(this.comboBox2);
            this.pnl_top.Controls.Add(this.comboBox1);
            this.pnl_top.Controls.Add(this.label3);
            this.pnl_top.Controls.Add(this.label2);
            this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_top.Location = new System.Drawing.Point(0, 0);
            this.pnl_top.Name = "pnl_top";
            this.pnl_top.Size = new System.Drawing.Size(861, 60);
            this.pnl_top.TabIndex = 55;
            // 
            // chkNoShow
            // 
            this.chkNoShow.AutoSize = true;
            this.chkNoShow.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkNoShow.ForeColor = System.Drawing.Color.Navy;
            this.chkNoShow.Location = new System.Drawing.Point(469, 29);
            this.chkNoShow.Name = "chkNoShow";
            this.chkNoShow.Size = new System.Drawing.Size(159, 18);
            this.chkNoShow.TabIndex = 71;
            this.chkNoShow.Text = "仅显示未显示B2B商品";
            this.chkNoShow.UseVisualStyleBackColor = true;
            // 
            // btn_edit
            // 
            this.btn_edit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_edit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_edit.ForeColor = System.Drawing.Color.Navy;
            this.btn_edit.Location = new System.Drawing.Point(755, 12);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(100, 40);
            this.btn_edit.TabIndex = 70;
            this.btn_edit.Text = "修改";
            this.btn_edit.UseVisualStyleBackColor = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_search.ForeColor = System.Drawing.Color.Navy;
            this.btn_search.Location = new System.Drawing.Point(652, 12);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(100, 40);
            this.btn_search.TabIndex = 69;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.pnl_find_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(303, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 59;
            this.label1.Text = "编号/名称";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.textBox1.ForeColor = System.Drawing.Color.Navy;
            this.textBox1.Location = new System.Drawing.Point(307, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(140, 23);
            this.textBox1.TabIndex = 52;
            // 
            // comboBox2
            // 
            this.comboBox2.DisplayMember = "theme_name";
            this.comboBox2.Font = new System.Drawing.Font("宋体", 11.25F);
            this.comboBox2.ForeColor = System.Drawing.Color.Navy;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(173, 27);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(120, 23);
            this.comboBox2.TabIndex = 58;
            this.comboBox2.ValueMember = "theme_code";
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "cls_name";
            this.comboBox1.Font = new System.Drawing.Font("宋体", 11.25F);
            this.comboBox1.ForeColor = System.Drawing.Color.Navy;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 23);
            this.comboBox1.TabIndex = 57;
            this.comboBox1.ValueMember = "cls_no";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(169, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 55;
            this.label3.Text = "主题";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // pnl_bottom
            // 
            this.pnl_bottom.Controls.Add(this.btn_last);
            this.pnl_bottom.Controls.Add(this.label4);
            this.pnl_bottom.Controls.Add(this.btn_first);
            this.pnl_bottom.Controls.Add(this.btn_next);
            this.pnl_bottom.Controls.Add(this.btn_prev);
            this.pnl_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_bottom.Location = new System.Drawing.Point(0, 601);
            this.pnl_bottom.Name = "pnl_bottom";
            this.pnl_bottom.Size = new System.Drawing.Size(861, 50);
            this.pnl_bottom.TabIndex = 68;
            // 
            // btn_last
            // 
            this.btn_last.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_last.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_last.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_last.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_last.ForeColor = System.Drawing.Color.Navy;
            this.btn_last.Location = new System.Drawing.Point(755, 5);
            this.btn_last.Name = "btn_last";
            this.btn_last.Size = new System.Drawing.Size(100, 40);
            this.btn_last.TabIndex = 44;
            this.btn_last.Text = "尾页";
            this.btn_last.UseVisualStyleBackColor = false;
            this.btn_last.Click += new System.EventHandler(this.btn_last_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(162, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(266, 20);
            this.label4.TabIndex = 40;
            this.label4.Text = "第1页，共0页";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_first
            // 
            this.btn_first.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_first.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_first.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_first.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_first.ForeColor = System.Drawing.Color.Navy;
            this.btn_first.Location = new System.Drawing.Point(434, 5);
            this.btn_first.Name = "btn_first";
            this.btn_first.Size = new System.Drawing.Size(100, 40);
            this.btn_first.TabIndex = 41;
            this.btn_first.Text = "首页";
            this.btn_first.UseVisualStyleBackColor = false;
            this.btn_first.Click += new System.EventHandler(this.btn_first_Click);
            // 
            // btn_next
            // 
            this.btn_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_next.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_next.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_next.ForeColor = System.Drawing.Color.Navy;
            this.btn_next.Location = new System.Drawing.Point(648, 5);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(100, 40);
            this.btn_next.TabIndex = 43;
            this.btn_next.Text = "下页";
            this.btn_next.UseVisualStyleBackColor = false;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_prev
            // 
            this.btn_prev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_prev.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_prev.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_prev.ForeColor = System.Drawing.Color.Navy;
            this.btn_prev.Location = new System.Drawing.Point(541, 5);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(100, 40);
            this.btn_prev.TabIndex = 42;
            this.btn_prev.Text = "上页";
            this.btn_prev.UseVisualStyleBackColor = false;
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // pnl_container
            // 
            this.pnl_container.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_container.Controls.Add(this.dg_data);
            this.pnl_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_container.Location = new System.Drawing.Point(0, 60);
            this.pnl_container.Name = "pnl_container";
            this.pnl_container.Size = new System.Drawing.Size(861, 541);
            this.pnl_container.TabIndex = 69;
            // 
            // dg_data
            // 
            this.dg_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_data.Location = new System.Drawing.Point(0, 0);
            this.dg_data.MergeCell = true;
            this.dg_data.Name = "dg_data";
            this.dg_data.RowHeight = 25;
            this.dg_data.Size = new System.Drawing.Size(857, 537);
            this.dg_data.TabIndex = 41;
            this.dg_data.Text = "dataGrid1";
            this.dg_data.ClickCell += new b2bclient.control.DataGrid.ClickCellHandler(this.dg_data_ClickCell);
            // 
            // GoodsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(861, 651);
            this.Controls.Add(this.pnl_container);
            this.Controls.Add(this.pnl_bottom);
            this.Controls.Add(this.pnl_top);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GoodsList";
            this.Text = "商品资料";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GoodsList_FormClosed);
            this.pnl_top.ResumeLayout(false);
            this.pnl_top.PerformLayout();
            this.pnl_bottom.ResumeLayout(false);
            this.pnl_container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel pnl_top;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Panel pnl_bottom;
        private System.Windows.Forms.Button btn_last;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_first;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_prev;
        private System.Windows.Forms.Panel pnl_container;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.CheckBox chkNoShow;
        private control.DataGrid dg_data;

    }
}
