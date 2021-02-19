namespace b2bclient.report
{
    partial class Rep客户对账表
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rep客户对账表));
            this.pnl_top = new System.Windows.Forms.Panel();
            this.txt_keyword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.pnl_container = new System.Windows.Forms.Panel();
            this.dg_data = new b2bclient.control.DataGrid();
            this.pnl_top.SuspendLayout();
            this.pnl_container.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_top
            // 
            this.pnl_top.Controls.Add(this.txt_keyword);
            this.pnl_top.Controls.Add(this.label2);
            this.pnl_top.Controls.Add(this.comboBox1);
            this.pnl_top.Controls.Add(this.label5);
            this.pnl_top.Controls.Add(this.btn_search);
            this.pnl_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_top.Location = new System.Drawing.Point(0, 0);
            this.pnl_top.Name = "pnl_top";
            this.pnl_top.Size = new System.Drawing.Size(869, 60);
            this.pnl_top.TabIndex = 69;
            // 
            // txt_keyword
            // 
            this.txt_keyword.Font = new System.Drawing.Font("宋体", 11.25F);
            this.txt_keyword.ForeColor = System.Drawing.Color.Navy;
            this.txt_keyword.Location = new System.Drawing.Point(284, 18);
            this.txt_keyword.Name = "txt_keyword";
            this.txt_keyword.Size = new System.Drawing.Size(132, 25);
            this.txt_keyword.TabIndex = 78;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(21, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 77;
            this.label2.Text = "客户组";
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "t_value";
            this.comboBox1.Font = new System.Drawing.Font("宋体", 11.25F);
            this.comboBox1.ForeColor = System.Drawing.Color.Navy;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(76, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(134, 23);
            this.comboBox1.TabIndex = 76;
            this.comboBox1.ValueMember = "t_key";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11.25F);
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(227, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 75;
            this.label5.Text = "关键字";
            // 
            // btn_search
            // 
            this.btn_search.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_search.ForeColor = System.Drawing.Color.Navy;
            this.btn_search.Location = new System.Drawing.Point(431, 10);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(100, 40);
            this.btn_search.TabIndex = 71;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = false;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // pnl_container
            // 
            this.pnl_container.Controls.Add(this.dg_data);
            this.pnl_container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_container.Location = new System.Drawing.Point(0, 60);
            this.pnl_container.Name = "pnl_container";
            this.pnl_container.Size = new System.Drawing.Size(869, 618);
            this.pnl_container.TabIndex = 71;
            // 
            // dg_data
            // 
            this.dg_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_data.Location = new System.Drawing.Point(0, 0);
            this.dg_data.MergeCell = true;
            this.dg_data.Name = "dg_data";
            this.dg_data.RowHeight = 25;
            this.dg_data.Size = new System.Drawing.Size(869, 618);
            this.dg_data.TabIndex = 73;
            this.dg_data.Text = "dataGrid1";
            // 
            // Rep客户对账表
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(869, 678);
            this.Controls.Add(this.pnl_container);
            this.Controls.Add(this.pnl_top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Rep客户对账表";
            this.Text = "客户对账表";
            this.pnl_top.ResumeLayout(false);
            this.pnl_top.PerformLayout();
            this.pnl_container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_top;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Panel pnl_container;
        private System.Windows.Forms.Label label5;
        private control.DataGrid dg_data;
        private System.Windows.Forms.TextBox txt_keyword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
