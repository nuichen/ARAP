using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using IvyBack.Helper;
using IvyBack.Interfaces;

namespace IvyBack.cons
{
    class EditGrid : System.Windows.Forms.Control
    {
        Calendar calendar;
        private PictureBox picAdd;
        private PictureBox picCut;
        private PictureBox picInsert;
        private ContextMenuStrip cmsMain;
        private ToolStripMenuItem tsmiDataSearch;
        private ToolStripMenuItem tsmiDataAdd;
        private ToolStripMenuItem tsmiDateDel;
        private ToolStripMenuItem tsmDataClear;
        private ToolStripMenuItem tsmiInsertRow;
        private ToolStripMenuItem tsmiDateCopy;
        public DataList dataList;

        Panel searchPanel;
        Label searchLabel;
        TextBox searchTextBox;
        Button searchButton;
        public EditGrid()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.AllPaintingInWmPaint, true);
            //
            this.InitializeComponent();
            //
            AddColumn("index_1122", "行号", "", 40, 2, "", false);
            //
            this.MouseWheel += this.Panel_MouseWheel;
            //
            calendar = new Calendar();
            calendar.Visible = false;
            calendar.Choosed += this.calendar_Choosed;
            //
            textBox1.GotFocus += textBox1_gotFocus;
            //
            dataList = new DataList();
            dataList.Visible = false;
            dataList.BackColor = Color.LightGoldenrodYellow;
            dataList.ClickCell += dataList_ClickCell;
            dataList.MergeCell = false;
        }

        private Point getPoint()
        {
            Point p = new Point(this.Left, this.Top);
            var par = this.Parent;

            while (par != null && this.FindForm() != par)
            {
                p = new Point(p.X + par.Left, p.Y + par.Top);
                par = par.Parent;
            }
            return p;
        }



        private void ShowDataList()
        {
            if (cur_cell != null)
            {
                var col = lstcolumn[cur_cell.column_index];
                dataList.Visible = false;
                dataList.ClearColumn();

                foreach (string f in col.show_fields.Split(','))
                {
                    var arr = f.Split(':');
                    dataList.AddColumn(arr[0], arr[1], "", Helper.Conv.ToInt16(arr[2]), 1, "");
                }


                dataList.Width = col.ls_width;
                dataList.Height = col.ls_height;

                if (this.FindForm() != null)
                {
                    var frm = this.FindForm();
                    Point p = getPoint();
                    int flag = 0;
                    if (flag == 0)
                    {
                        int x = p.X + cur_cell.r.X;
                        int y = p.Y + cur_cell.r.Y + cur_cell.r.Height + 1;
                        Point p1 = new Point(x, y);
                        Point p2 = new Point(x + dataList.Width, y + dataList.Height);
                        Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                        if (r.Contains(p1) && r.Contains(p2))
                        {
                            dataList.Left = x;
                            dataList.Top = y;
                            flag = 1;
                        }
                    }
                    if (flag == 0)
                    {
                        int x = p.X + cur_cell.r.X + cur_cell.r.Width - dataList.Width;
                        int y = p.Y + cur_cell.r.Y + cur_cell.r.Height + 1;
                        Point p1 = new Point(x, y);
                        Point p2 = new Point(x + dataList.Width, y + dataList.Height);
                        Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                        if (r.Contains(p1) && r.Contains(p2))
                        {
                            dataList.Left = x;
                            dataList.Top = y;
                            flag = 1;
                        }
                    }

                    if (flag == 0)
                    {
                        int x = p.X + cur_cell.r.X;
                        int y = p.Y + cur_cell.r.Y - dataList.Height;
                        Point p1 = new Point(x, y);
                        Point p2 = new Point(x + dataList.Width, y + dataList.Height);
                        Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                        if (r.Contains(p1) && r.Contains(p2))
                        {
                            dataList.Left = x;
                            dataList.Top = y;
                            flag = 1;
                        }
                    }
                    if (flag == 0)
                    {
                        int x = p.X + cur_cell.r.X + cur_cell.r.Width - dataList.Width;
                        int y = p.Y + cur_cell.r.Y - dataList.Height;
                        Point p1 = new Point(x, y);
                        Point p2 = new Point(x + dataList.Width, y + dataList.Height);
                        Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                        if (r.Contains(p1) && r.Contains(p2))
                        {
                            dataList.Left = x;
                            dataList.Top = y;
                            flag = 1;
                        }
                    }


                    if (flag == 0)
                    {

                        dataList.Left = p.X + cur_cell.r.X;
                        dataList.Top = p.Y + cur_cell.r.Y + cur_cell.r.Height;

                    }


                }

                dataList.Visible = true;
                this.FindForm().Controls.Add(dataList);
                dataList.BringToFront();
            }
        }

        private void dataList_ClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {
            if (cur_cell != null)
            {
                var col = lstcolumn[cur_cell.column_index];
                int flag = 0;
                foreach (string str in col.tran_string.Split(','))
                {
                    if (str.Contains("->"))
                    {
                        string value = "";


                        var arr = str.Split("->".ToCharArray(0, 2), StringSplitOptions.RemoveEmptyEntries)[0].Split('/');
                        string target_column_name = str.Split("->".ToCharArray(0, 2), StringSplitOptions.RemoveEmptyEntries)[1];
                        if (arr.Length > 1)
                        {
                            for (int i = 0; i < arr.Length; i++)
                            {
                                string col_name = arr[i];
                                if (i == 0)
                                {
                                    if (row.Table.Columns.Contains(col_name) == true)
                                    {
                                        value += row[col_name].ToString();
                                    }

                                }
                                else
                                {
                                    value += "/";
                                    if (row.Table.Columns.Contains(col_name) == true)
                                    {
                                        value += row[col_name].ToString();
                                    }
                                }
                            }
                            var r = tb.Rows[cur_cell.row_index];
                            r[target_column_name] = value;
                            if (target_column_name == col.column_name)
                            {
                                flag = 1;
                            }

                        }
                        else
                        {
                            var r = tb.Rows[cur_cell.row_index];
                            r[target_column_name] = row[arr[0]];
                            if (target_column_name == col.column_name)
                            {
                                flag = 1;
                            }
                        }
                        //


                    }
                    else
                    {

                    }


                }

                if (flag == 1)
                {
                    var r = tb.Rows[cur_cell.row_index];
                    this.textBox1.Text = r[col.column_name].ToString();
                    if (cur_cell.row_index == tb.Rows.Count - 1)
                    {
                        tb.Rows.Add(tb.NewRow());
                    }
                }


                dataList.Visible = false;
                this.textBox1.SelectionStart = this.textBox1.Text.Length;
                this.Refresh();
            }

        }


        private void calendar_Choosed(object sender)
        {
            this.textBox1.Text = calendar.Date.ToString("yyyy-MM-dd");
            this.calendar.Visible = false;
            this.textBox1.SelectionStart = this.textBox1.Text.Length;
        }

        private void textBox1_gotFocus(object sender, EventArgs e)
        {
            if (cur_cell != null)
            {
                var col = lstcolumn[cur_cell.column_index];
                if (col.col_type == 1)
                {
                    dataList.DataSource = col.tb;
                    this.ShowDataList();
                }
                else if (col.col_type == 2)
                {
                    this.ShowCalendar();
                }

            }

        }

        private System.ComponentModel.IContainer components;
        private TextBox textBox1;
        private Timer timer1;

        internal class ColumnInfo
        {
            public string column_name { get; set; }
            public string header { get; set; }
            public string group_header { get; set; }
            public int width { get; set; }
            public int align { get; set; }
            public string format { get; set; }
            public bool is_total { get; set; }
            public bool is_group { get; set; }
            public bool can_edit { get; set; }
            /// <summary>
            /// 0_默认;1_参照;2_日期;3_复选
            /// </summary>
            internal int col_type { get; set; }
            internal DataTable tb { get; set; }
            internal int ls_width { get; set; }
            internal int ls_height { get; set; }
            internal string key_field { get; set; }
            internal string show_fields { get; set; }
            internal string tran_string { get; set; }
            internal int temp1 { get; set; }
            //
            public ColumnInfo Copy()
            {
                return (ColumnInfo)this.MemberwiseClone();
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="column_name">列名</param>
        /// <param name="tb"></param>
        /// <param name="show_fields"></param>
        /// <returns></returns>
        public void Bind(string column_name, DataTable tb, int width, int height, string key_field, string show_fields, string tran_string)
        {
            foreach (ColumnInfo col in lstcolumn)
            {
                if (column_name == col.column_name)
                {
                    col.col_type = 1;
                    col.tb = tb;
                    col.ls_width = width;
                    col.ls_height = height;
                    col.key_field = key_field;
                    col.show_fields = show_fields;
                    col.tran_string = tran_string;
                    break;
                }
            }
        }

        public void RemoveRow(int row_index)
        {
            if (this.tb != null)
            {
                if (this.tb.Rows.Count > row_index)
                {
                    this.Editing = false;
                    this.tb.Rows.RemoveAt(row_index);
                    this.Refresh();
                }
            }

        }

        private void ShowCalendar()
        {
            if (this.FindForm() != null && this.cur_cell != null)
            {
                var frm = this.FindForm();
                DateTime dt;
                if (DateTime.TryParse(this.textBox1.Text.Trim(), out dt) == true)
                {
                    calendar.Date = dt;
                }
                //
                Point p = getPoint();
                int flag = 0;
                if (flag == 0)
                {
                    int x = p.X + cur_cell.r.X;
                    int y = p.Y + cur_cell.r.Y + cur_cell.r.Height + 1;
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + calendar.Width, y + calendar.Height);
                    Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                    if (r.Contains(p1) && r.Contains(p2))
                    {
                        calendar.Left = x;
                        calendar.Top = y;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    int x = p.X + cur_cell.r.X + cur_cell.r.Width - calendar.Width;
                    int y = p.Y + cur_cell.r.Y + cur_cell.r.Height + 1;
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + calendar.Width, y + calendar.Height);
                    Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                    if (r.Contains(p1) && r.Contains(p2))
                    {
                        calendar.Left = x;
                        calendar.Top = y;
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    int x = p.X + cur_cell.r.X;
                    int y = p.Y + cur_cell.r.Y - calendar.Height;
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + calendar.Width, y + calendar.Height);
                    Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                    if (r.Contains(p1) && r.Contains(p2))
                    {
                        calendar.Left = x;
                        calendar.Top = y;
                        flag = 1;
                    }
                }
                if (flag == 0)
                {
                    int x = p.X + cur_cell.r.X + cur_cell.r.Width - calendar.Width;
                    int y = p.Y + cur_cell.r.Y - calendar.Height;
                    Point p1 = new Point(x, y);
                    Point p2 = new Point(x + calendar.Width, y + calendar.Height);
                    Rectangle r = new Rectangle(0, 0, frm.Width, frm.Height);
                    if (r.Contains(p1) && r.Contains(p2))
                    {
                        calendar.Left = x;
                        calendar.Top = y;
                        flag = 1;
                    }
                }


                if (flag == 0)
                {
                    calendar.Left = p.X + cur_cell.r.X;
                    calendar.Top = p.Y + cur_cell.r.Y + cur_cell.r.Height;
                }

                //
                calendar.Visible = true;
                this.FindForm().Controls.Add(calendar);
                calendar.BringToFront();
                //


            }
        }

        public void BindDate(string column_name)
        {
            foreach (ColumnInfo col in lstcolumn)
            {
                if (col.column_name == column_name)
                {
                    col.col_type = 2;
                    break;
                }
            }

        }

        public void BindCheck(string column_name)
        {
            foreach (ColumnInfo col in lstcolumn)
            {
                if (col.column_name == column_name)
                {
                    col.col_type = 3;
                    break;
                }
            }
        }


        /// <summary>
        /// 当前最大显示行数据
        /// </summary>
        /// <returns></returns>
        public int MaxDisplayRowCount()
        {
            int h = this.Height;
            if (HasGroupHeader() == true)
            {
                h -= rowHeight * 2;
            }
            else
            {
                h -= rowHeight;
            }
            if (HasTotalBar() == true)
            {
                h -= rowHeight;
            }
            int scrollType = this.ScrollType2();
            if (scrollType == 1 || scrollType == 3)
            {
                h -= rowHeight;
            }
            return h / rowHeight;
        }

        private List<ColumnInfo> lstcolumn = new List<ColumnInfo>();

        public List<ColumnInfo> Columns => lstcolumn;

        public void Clear()
        {
            lstcolumn.RemoveAll(t => t.column_name != "index_1122");
        }

        private PictureBox pictureBox1;
        public void AddColumn(string column_name, string header, string group_header, int width, int align, string format, bool can_edit)
        {

            ColumnInfo col = new ColumnInfo();
            col.column_name = column_name;
            col.header = header;
            col.group_header = group_header;
            col.width = width;
            col.align = align;
            col.format = format;
            col.can_edit = can_edit;
            col.col_type = 0;
            lstcolumn.Add(col);
            this.Refresh();
        }

        public void SetTotalColumn(string columns)
        {

            if (columns == "")
            {
                foreach (ColumnInfo col in lstcolumn)
                {
                    col.is_total = false;
                }
            }
            else if (columns.Contains(",") == false)
            {
                foreach (ColumnInfo col in lstcolumn)
                {
                    if (col.column_name.ToLower() == columns.ToLower())
                    {
                        col.is_total = true;
                    }
                    else
                    {
                        col.is_total = false;
                    }
                }
            }
            else
            {

                foreach (ColumnInfo col in lstcolumn)
                {
                    int flag = 0;
                    foreach (string column_name in columns.Split(','))
                    {
                        if (col.column_name.ToLower() == column_name.ToLower())
                        {

                            flag = 1;
                            break;
                        }

                    }
                    if (flag == 1)
                    {
                        col.is_total = true;
                    }
                    else
                    {
                        col.is_total = false;
                    }

                }
            }
            this.Refresh();
        }
        public void SetTotalColumn(string column, decimal value)
        {
            if (column == "")
            {
                total_sum.Clear();
            }
            else
            {
                decimal v;
                if (total_sum.TryGetValue(column, out v))
                {
                    total_sum[column] = value;
                }
                else
                {
                    total_sum.Add(column, value);
                }
            }
            this.Refresh();

        }

        private DataTable tb_source;
        private DataTable _tb;
        private DataTable tb
        {
            get
            {
                if (_tb == null)
                    _tb = new DataTable();
                return _tb;
            }
            set
            {
                _tb = value;
            }
        }
        private DataRow total_row;
        private Dictionary<string, decimal> total_sum = new Dictionary<string, decimal>();

        //清除合计

        public void clear_total()
        {
            foreach (ColumnInfo col in lstcolumn)
            {
                if (col.is_total == true)
                {
                    if (tb.Columns.Contains(col.column_name) == true)
                    {
                        decimal val = 0;

                        total_row[col.column_name] = val.ToString();
                    }

                }

            }
        }

        public delegate void DataSourceChangedHandler(object sender, EventArgs e);

        [Category("属性已更改")]

        public event DataSourceChangedHandler DataSourceChanged;

        public DataTable DataSource
        {
            get
            {
                return tb;
            }
            set
            {
                clear_total();
                if (value == null)
                {
                    Editing = false;
                    cur_cell = null;
                    tb_source = value;
                    tb = value;
                    this.Refresh();
                }
                else
                {
                    Editing = false;
                    cur_cell = null;
                    tb_source = value.Copy();

                    this.Refresh();
                    if (tb_source.Columns.Contains("index_1122") == false)
                    {
                        tb_source.Columns.Add("index_1122");
                    }
                    if (tb_source.Columns.Contains("row_flag") == false)//0父行；1父行展；2子行
                    {
                        tb_source.Columns.Add("row_flag");
                    }

                    tb = value.Copy();
                    if (tb.Columns.Contains("index_1122") == false)
                    {
                        tb.Columns.Add("index_1122");
                    }
                    if (tb.Columns.Contains("row_flag") == false)
                    {
                        tb.Columns.Add("row_flag");
                    }

                    //
                    var tb_total = tb.Clone();
                    total_row = tb_total.NewRow();
                    tb_total.Rows.Add(total_row);
                    foreach (ColumnInfo col in lstcolumn)
                    {
                        if (col.is_total == true)
                        {
                            if (tb.Columns.Contains(col.column_name) == true)
                            {
                                decimal val = 0;
                                foreach (DataRow row in tb.Rows)
                                {
                                    val += Helper.Conv.ToDecimal(row[col.column_name].ToString());
                                }
                                total_row[col.column_name] = val.ToString();
                            }

                        }

                    }
                }


                //
                v_value = 0;
                this.Refresh();

                DataSourceChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int rowHeight = 25;
        public int RowHeight
        {
            get
            {
                return rowHeight;
            }
            set
            {
                if (value > 15)
                {
                    rowHeight = value;
                }
                else
                {
                    rowHeight = 15;
                }
            }
        }



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGrid));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.picAdd = new System.Windows.Forms.PictureBox();
            this.picCut = new System.Windows.Forms.PictureBox();
            this.picInsert = new System.Windows.Forms.PictureBox();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDataSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDataAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDateCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDateDel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDataClear = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsertRow = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInsert)).BeginInit();
            ////搜索

            //searchPanel = new Panel();
            //searchLabel = new Label();
            //searchTextBox = new TextBox();
            //searchButton = new Button();
            //this.searchLabel.Location = new System.Drawing.Point(0, 0);
            //this.searchLabel.Size = new System.Drawing.Size(200, 50);
            //this.searchLabel.TabIndex = 0;
            //this.searchLabel.TabStop = false;

            //this.searchTextBox.Location = new System.Drawing.Point(0, 70);
            //this.searchTextBox.Size = new System.Drawing.Size(200, 50);
            //this.searchTextBox.TabIndex = 0;
            //this.searchTextBox.TabStop = false;

            //this.searchButton.Location = new System.Drawing.Point(0,140);
            //this.searchButton.Size = new System.Drawing.Size(200, 50);
            //this.searchButton.Text = "搜索";
            //this.searchButton.TabIndex = 0;
            //this.searchButton.TabStop = false;
            //this.searchButton.Click += this.searchButton_Click;

            //this.searchButton.Location = new System.Drawing.Point(0, 0);
            //this.searchPanel.Size = new System.Drawing.Size(400, 200);

            //this.searchPanel.TabIndex = 0;
            //this.searchPanel.TabStop = false;

            //this.searchPanel.Controls.Add(searchLabel);
            //this.searchPanel.Controls.Add(searchTextBox);
            //this.searchPanel.Controls.Add(searchButton);
            //this.searchPanel.SendToBack();
            //this.searchPanel.Visible = false;
            //
            this.cmsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.VisibleChanged += new System.EventHandler(this.textBox1_VisibleChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            this.textBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDoubleClick);
            this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDown);
            // 
            // picAdd
            // 
            this.picAdd.Image = global::IvyBack.Properties.Resources.dgv_add;
            this.picAdd.Location = new System.Drawing.Point(0, 0);
            this.picAdd.Name = "picAdd";
            this.picAdd.Size = new System.Drawing.Size(100, 50);
            this.picAdd.TabIndex = 0;
            this.picAdd.TabStop = false;
            // 
            // picCut
            // 
            this.picCut.Image = global::IvyBack.Properties.Resources.dgv_del;
            this.picCut.Location = new System.Drawing.Point(0, 0);
            this.picCut.Name = "picCut";
            this.picCut.Size = new System.Drawing.Size(100, 50);
            this.picCut.TabIndex = 0;
            this.picCut.TabStop = false;
            // 
            // picInsert
            // 
            this.picInsert.Image = global::IvyBack.Properties.Resources.dgv_insert;
            this.picInsert.Location = new System.Drawing.Point(0, 0);
            this.picInsert.Name = "picInsert";
            this.picInsert.Size = new System.Drawing.Size(100, 50);
            this.picInsert.TabIndex = 0;
            this.picInsert.TabStop = false;
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDataSearch,
            this.tsmiDateCopy,
            this.tsmiDataAdd,
            this.tsmiInsertRow,
            this.tsmiDateDel,
            this.tsmDataClear});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(125, 92);
            //
            //tsmiDataSearch
            //
            this.tsmiDataSearch.Name = "tsmiDataSearch";
            this.tsmiDataSearch.Size = new System.Drawing.Size(124, 22);
            this.tsmiDataSearch.Text = "搜索";
            this.tsmiDataSearch.Click += new System.EventHandler(this.tsmiDataSearch_Click);


            ///



            this.tsmiDateCopy.Name = "tsmiDateCopy";
            this.tsmiDateCopy.Size = new System.Drawing.Size(124, 22);
            this.tsmiDateCopy.Text = "复制";
            this.tsmiDateCopy.Click += new System.EventHandler(this.tsmiDateCopy_Click);
            // 
            // tsmiDataAdd
            // 
            this.tsmiDataAdd.Name = "tsmiDataAdd";
            this.tsmiDataAdd.Size = new System.Drawing.Size(124, 22);
            this.tsmiDataAdd.Text = "添加行";
            this.tsmiDataAdd.Click += new System.EventHandler(this.tsmiDataAdd_Click);
            // 
            // tsmiDateDel
            // 
            this.tsmiDateDel.Name = "tsmiDateDel";
            this.tsmiDateDel.Size = new System.Drawing.Size(124, 22);
            this.tsmiDateDel.Text = "删除行";
            this.tsmiDateDel.Click += new System.EventHandler(this.tsmiDateDel_Click);
            // 
            // tsmDataClear
            // 
            this.tsmDataClear.Name = "tsmDataClear";
            this.tsmDataClear.Size = new System.Drawing.Size(124, 22);
            this.tsmDataClear.Text = "清空数据";
            this.tsmDataClear.Click += new System.EventHandler(this.tsmDataClear_Click);
            // 
            // tsmiInsertRow
            // 
            this.tsmiInsertRow.Name = "tsmiInsertRow";
            this.tsmiInsertRow.Size = new System.Drawing.Size(124, 22);
            this.tsmiInsertRow.Text = "插入行";
            this.tsmiInsertRow.Click += new System.EventHandler(this.tsmiInsertRow_Click);
            // 
            // EditGrid
            // 
            this.ContextMenuStrip = this.cmsMain;
            this.VisibleChanged += new System.EventHandler(this.DataGrid_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DataGrid_Paint);
            this.Leave += new System.EventHandler(this.EditGrid_Leave);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseDown);
            this.MouseHover += new System.EventHandler(this.DataGrid_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseUp);
            this.Validated += new System.EventHandler(this.DataGrid_Validated);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picInsert)).EndInit();
            this.cmsMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        string Format(DataColumn col, object value, string format)
        {
            if (value.ToString() == "")
            {
                return "";
            }
            if (format == "")
            {
                return Helper.Conv.ToString(value);
            }
            else if (format.StartsWith("{") == true && format.EndsWith("}") == true)
            {
                format = format.Substring(1, format.Length - 2);

                foreach (string str in format.Split(','))
                {
                    var arr = str.Split(':');
                    if (arr.Length > 1)
                    {
                        if (arr[0] == Helper.Conv.ToString(value))
                        {
                            return arr[1];
                        }
                    }

                }
                return Helper.Conv.ToString(value);

            }
            else if (format == "大写金额")
            {
                return Helper.Conv.DaXie(Helper.Conv.ToDecimal(value).ToString("0.00"));
            }
            else if (col.DataType == typeof(DateTime) || format.Contains("y") || format.Contains("M") || format.Contains("d")
                || format.Contains("H") || format.Contains("m") || format.Contains("s") || format.Contains("f"))
            {
                if (Convert.ToDateTime(value) == DateTime.MinValue)
                {
                    return "";
                }
                else
                {
                    return Convert.ToDateTime(value).ToString(format);
                }
            }
            else
            {
                return Helper.Conv.ToDecimal(value).ToString(format);
            }
        }


        private bool HasGroupHeader()
        {
            foreach (ColumnInfo col in lstcolumn)
            {
                if (col.group_header != "")
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasTotalBar()
        {
            foreach (ColumnInfo col in lstcolumn)
            {
                if (col.is_total == true)
                {
                    return true;
                }
            }
            return false;
        }

        decimal v_page_size = 0;
        decimal v_max = 0;
        public decimal v_value = 0;
        decimal v_height = 0;
        decimal v_len = 0;
        decimal v_top = 0;
        decimal h_page_size = 0;
        decimal h_max = 0;
        decimal h_value = 0;
        decimal h_width = 0;
        decimal h_len = 0;
        decimal h_left = 0;

        void ComputeV()
        {
            v_page_size = this.Height - rowHeight;
            if (HasGroupHeader() == true)
            {
                v_page_size -= rowHeight;
            }
            if (HasTotalBar() == true)
            {

                v_page_size -= rowHeight;
            }
            if (ScrollType2() == 3)
            {
                v_page_size -= rowHeight;
            }
            v_max = tb.Rows.Count * rowHeight;

            v_height = this.Height - rowHeight * 2;
            if (ScrollType2() == 3)
            {
                v_height -= rowHeight;
            }
            v_len = v_height / v_max * v_height;
            if (v_len < 10)
            {
                v_len = 10;
            }
            else if (v_len > v_height)
            {
                v_len = v_height;
            }
            v_top = (v_height - v_len) / (v_max - v_page_size) * v_value;
            v_max = v_max - v_page_size;

            if (v_value < 0)
            {
                v_value = 0;
            }
            else if (v_value > v_max)
            {
                v_value = v_max;
            }
        }

        private decimal ScrollHBoundRate = 0.3M;
        void ComputeH()
        {
            h_page_size = this.Width;
            if (ScrollType2() == 3)
            {
                h_page_size -= rowHeight;
            }
            decimal w = 0;
            foreach (ColumnInfo col in lstcolumn)
            {
                w += col.width;
            }
            h_max = w;
            h_width = this.Width - rowHeight * 2;
            if (ScrollType2() == 3)
            {
                h_width -= rowHeight;
            }
            h_len = h_width / h_max * h_width;
            if (h_len < 10)
            {
                h_len = 10;
            }
            else if (h_len > h_width)
            {
                h_len = h_width;
            }
            h_left = (h_width - h_len) / (h_max - h_page_size) * h_value;
            h_max -= h_page_size;

            if (h_value < 0)
            {
                h_value = 0;
            }
            else if (h_value > h_max)
            {
                h_value = h_max;
            }
        }



        void ResetIndexColumn()
        {
            foreach (ColumnInfo col in lstcolumn)
            {
                if (col.column_name == "index_1122")
                {
                    if (lstcolumn.IndexOf(col) != 0)
                    {
                        lstcolumn.Remove(col);
                        lstcolumn.Insert(0, col);
                    }
                    break;
                }
            }
        }

        private Rectangle DataRectangle()
        {

            int x = 0;
            int y = 0;
            if (HasGroupHeader() == true)
            {
                y = rowHeight * 2;
            }
            else
            {
                y = rowHeight;
            }
            int w = 0;
            int scrollType = ScrollType2();
            if (scrollType == 2)
            {
                w = this.Width - rowHeight - x;
            }
            else if (scrollType == 3)
            {
                w = this.Width - rowHeight - x;
            }
            else
            {
                w = this.Width - x;
            }
            int h = 0;
            if (scrollType == 1)
            {
                h = this.Height - rowHeight - y;
            }
            else if (scrollType == 3)
            {
                h = this.Height - rowHeight - y;
            }
            else
            {
                h = this.Height - y;
            }
            if (HasTotalBar() == true)
            {
                h -= rowHeight;
            }
            return new Rectangle(x, y, w, h);
        }


        private int ScrollType2()
        {
            int scrollType = ScrollType();
            if (scrollType == 0)
            {
                return 1;
            }
            else if (scrollType == 2)
            {
                return 3;
            }
            else
            {
                return scrollType;
            }
        }

        private int ScrollType()
        {
            int flag1 = 0;
            int flag2 = 0;
            //
            int h = this.Height;
            if (HasGroupHeader() == true)
            {
                h -= rowHeight * 2;
            }
            else
            {
                h -= rowHeight;
            }
            if (HasTotalBar() == true)
            {
                h -= rowHeight;
            }
            h -= rowHeight;
            int total_h = 0;
            total_h = tb.Rows.Count * rowHeight;
            if (h < total_h)
            {
                flag2 = 1;
            }
            //
            int w = 0;
            foreach (ColumnInfo col in lstcolumn)
            {
                w += col.width;
            }
            if (w > this.Width)
            {
                flag1 = 1;
            }
            //
            if (flag1 == 0)
            {
                if (flag2 == 1)
                {
                    w += rowHeight;
                }
                if (w > this.Width)
                {
                    flag1 = 1;
                }
            }
            if (flag2 == 0)
            {
                if (flag1 == 1)
                {
                    h -= rowHeight;

                }
                if (h < total_h)
                {
                    flag2 = 1;
                }
            }
            //
            if (flag1 == 0 && flag2 == 0)
            {
                return 0;
            }
            else if (flag1 == 1 && flag2 == 0)
            {
                return 1;
            }
            else if (flag1 == 0 && flag2 == 1)
            {
                return 2;
            }
            else if (flag1 == 1 && flag2 == 1)
            {
                return 3;
            }
            else
            {
                return 0;
            }

        }




        private void a1()
        {

        }
        private Dictionary<string, Color?> cellColors = new Dictionary<string, Color?>();

        public void SetCellColor(int rowIndex, int columnIndex, Color color)
        {
            string key = rowIndex + "-" + columnIndex;
            if (cellColors.TryGetValue(key, out _))
            {
                cellColors[key] = color;
            }
            else
            {
                cellColors.Add(key, color);
            }
        }
        public void SetCellColor(int rowIndex, string columnName, Color color)
        {
            int columnIndex = tb.Columns.IndexOf(columnName);
            SetCellColor(rowIndex, columnIndex, color);
        }
        public void ClearCellColor(int rowIndex, int columnIndex)
        {
            string key = rowIndex + "-" + columnIndex;
            cellColors.Remove(key);
        }
        public void ClearCellColor(int rowIndex, string columnName)
        {
            int columnIndex = tb.Columns.IndexOf(columnName);
            ClearCellColor(rowIndex, columnIndex);
        }
        public void ClearCellColor()
        {
            cellColors.Clear();
        }


        private void DataGrid_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (DesignMode == true)
                {
                    Graphics g = e.Graphics;
                    g.DrawRectangle(Pens.Gray, 0, 0, this.Width - 1, this.Height - 1);
                }
                else
                {
                    ResetIndexColumn();
                    Rectangle dataRectangle = this.DataRectangle();
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    List<CellObject> lstobj = new List<CellObject>();
                    int scrollType = ScrollType();
                    int scrollType2 = ScrollType2();
                    if (scrollType == 1)
                    {
                        ComputeH();
                        v_value = 0;
                    }
                    else if (scrollType == 2)
                    {
                        ComputeV();
                        h_value = 0;
                    }
                    else if (scrollType == 3)
                    {
                        ComputeH();
                        ComputeV();
                    }

                    //正常数据
                    if (tb != null)
                    {
                        int x = 0;
                        int y = 0;
                        int w = 0;
                        int h = rowHeight;
                        int start_top = 0;
                        StringFormat sf = new StringFormat();

                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        if (HasGroupHeader() == true)
                        {
                            start_top = rowHeight * 2;
                        }
                        else
                        {
                            start_top = rowHeight;
                        }
                        y = start_top - (int)v_value;
                        x = -(int)h_value;
                        for (int i = 0; i < tb.Rows.Count; i++)
                        {
                            if (y < -1 * rowHeight * 3)
                            {
                                x = -(int)h_value;
                                y += rowHeight;
                                continue;
                            }
                            for (int j = 0; j < lstcolumn.Count; j++)
                            {

                                var col = lstcolumn[j];
                                w = col.width;
                                Color b;
                                Color f;
                                Rectangle r = new Rectangle(x, y, w, h);
                                int flag = 0;
                                if (cur_cell == null)
                                {
                                    b = Color.White;
                                    f = Color.Black;
                                }
                                else if (cur_cell.row_index == i && cur_cell.column_index == j)
                                {
                                    cur_cell.r = r;
                                    if (Editing == true)
                                    {
                                        b = Color.White;
                                        f = Color.Black;

                                        Rectangle r2 = new Rectangle(cur_cell.r.X + 1, cur_cell.r.Y + (cur_cell.r.Height - textBox1.Height) / 2,
                                            cur_cell.r.Width - 2, textBox1.Height);
                                        if (dataRectangle.Contains(r2))
                                        {
                                            textBox1.Left = cur_cell.r.X + 1;
                                            textBox1.Top = cur_cell.r.Y + (cur_cell.r.Height - textBox1.Height) / 2;
                                            textBox1.Width = cur_cell.r.Width - 2;
                                            flag = 1;
                                        }
                                        else
                                        {
                                            textBox1.Top = -200;
                                        }


                                    }
                                    else
                                    {
                                        b = Helper.GlobalData.grid_sel;
                                        f = Helper.GlobalData.grid_sel_font;
                                    }


                                }
                                else
                                {
                                    b = Color.White;
                                    f = Color.Black;
                                }
                                CellObject obj = new CellObject();
                                obj.r = r;
                                obj.row_index = i;
                                obj.column_index = j;
                                obj.type = 0;
                                obj.ColumnName = col.column_name;
                                lstobj.Add(obj);

                                if (col.align == 1)
                                {
                                    sf.Alignment = StringAlignment.Near;
                                }
                                else if (col.align == 2)
                                {
                                    sf.Alignment = StringAlignment.Center;
                                }
                                else if (col.align == 3)
                                {
                                    sf.Alignment = StringAlignment.Far;
                                }
                                if (tb.Columns.Contains(col.column_name) == true)
                                {
                                    int columnIndex = tb.Columns.IndexOf(col.column_name);
                                    string key = i + "-" + columnIndex;
                                    cellColors.TryGetValue(key, out var cellBackColor);
                                    if (j == 0)
                                    {
                                        string value = (i + 1).ToString();
                                        g.FillRectangle(new SolidBrush(cellBackColor ?? b), new Rectangle(x, y, w, h));
                                        g.DrawRectangle(Pens.Gray, new Rectangle(x, y, w, h));
                                        g.DrawString(value, this.Font, new SolidBrush(f), new Rectangle(x + 1, y + 1, w - 1, h - 1), sf);
                                        col.temp1 = 1;
                                    }
                                    else
                                    {
                                        string value = Format(tb.Columns[col.column_name], tb.Rows[i][col.column_name], col.format);
                                        g.FillRectangle(new SolidBrush(cellBackColor ?? b), new Rectangle(x, y, w, h));
                                        g.DrawRectangle(Pens.Gray, new Rectangle(x, y, w, h));
                                        if (flag == 0)
                                        {

                                            if (col.col_type == 3)
                                            {
                                                if (value == "1" || value.ToLower() == "true")
                                                {

                                                    int s2 = 14;
                                                    Rectangle r2 = new Rectangle(x + (w - s2) / 2, y + (h - s2) / 2, s2, s2);
                                                    g.DrawRectangle(new Pen(f), r2);
                                                    g.DrawLines(new Pen(f, 2), new Point[] {new Point(r2.X+2,r2.Y+r2.Height/2),
                                                    new Point(r2.X+r2.Width/2,r2.Y+r2.Height-3),
                                                    new Point(r2.X+r2.Width-2,r2.Y+2)});
                                                }
                                                else
                                                {
                                                    Rectangle r2 = new Rectangle(x, y, w, h);
                                                    int s2 = 14;
                                                    g.DrawRectangle(new Pen(f), new Rectangle(x + (w - s2) / 2, y + (h - s2) / 2, s2, s2));
                                                }

                                            }
                                            else
                                            {

                                                var db_col = tb.Columns[col.column_name];
                                                if (db_col.DataType == typeof(int) ||
                                                db_col.DataType == typeof(Int16) ||
                                                db_col.DataType == typeof(Int32) ||
                                                db_col.DataType == typeof(Int64) ||
                                                db_col.DataType == typeof(decimal) ||
                                                db_col.DataType == typeof(double) ||
                                                db_col.DataType == typeof(long) ||
                                                db_col.DataType == typeof(Single))
                                                {
                                                    if (Helper.Conv.ToDecimal(value) < 0 ||
                                                    Helper.Conv.ToDecimal(value) > 99999)
                                                    {
                                                        g.DrawString(value, this.Font, new SolidBrush(Color.Red), new Rectangle(x + 1, y + 1, w - 1, h - 1), sf);
                                                    }
                                                    else
                                                    {
                                                        g.DrawString(value, this.Font, new SolidBrush(f), new Rectangle(x + 1, y + 1, w - 1, h - 1), sf);
                                                    }

                                                }
                                                else
                                                {
                                                    g.DrawString(value, this.Font, new SolidBrush(f), new Rectangle(x + 1, y + 1, w - 1, h - 1), sf);
                                                }

                                            }

                                        }
                                        col.temp1 = 1;

                                    }

                                }
                                else
                                {
                                    string value = "";
                                    g.FillRectangle(new SolidBrush(b), new Rectangle(x, y, w, h));
                                    g.DrawRectangle(Pens.Gray, new Rectangle(x, y, w, h));
                                    g.DrawString(value, this.Font, new SolidBrush(f), new Rectangle(x + 1, y + 1, w - 1, h - 1), sf);

                                }
                                x += w;
                            }
                            x = -(int)h_value;
                            y += rowHeight;
                            if (y > this.Height)
                            {
                                break;
                            }
                        }


                    }

                    //正常标题
                    if (1 == 1)
                    {
                        int x = 0;
                        int y = 0;
                        int w = 0;
                        int h = 0;
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        string group_header = "";
                        int total_w = 0;
                        x = -(int)h_value;
                        for (int i = 0; i < lstcolumn.Count; i++)
                        {

                            var col = lstcolumn[i];

                            w = col.width;
                            h = rowHeight;
                            if (HasGroupHeader() == true)
                            {
                                h = rowHeight * 2;
                            }
                            else
                            {
                                h = rowHeight;
                            }

                            string header = col.header;
                            if (col.group_header == "")
                            {
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Center;
                                g.FillRectangle(new SolidBrush(Helper.GlobalData.grid_header1), new Rectangle(x, y, w, h));
                                g.DrawRectangle(Pens.Gray, new Rectangle(x, y, w, h));
                                g.DrawString(header, this.Font, new SolidBrush(Helper.GlobalData.grid_header_fontcolor),
                                new Rectangle(x + 1, y + 1, w - 1, h - 1), sf);
                                CellObject obj = new CellObject();
                                obj.r = new Rectangle(x, y, w, h);
                                obj.row_index = -1;
                                obj.column_index = i;
                                obj.type = 1;
                                obj.ColumnName = col.column_name;
                                lstobj.Add(obj);

                                if (sort_col == col)
                                {
                                    sf.Alignment = StringAlignment.Far;
                                    sf.LineAlignment = StringAlignment.Center;
                                    string str = "";
                                    if (sort_type == "asc")
                                    {
                                        str = "↑";
                                    }
                                    else if (sort_type == "desc")
                                    {
                                        str = "↓";
                                    }

                                    g.DrawString(str, this.Font, new SolidBrush(Color.Maroon),
                                    new Rectangle(x + 1, y + 1, w - 1, h - 1), sf);
                                }
                            }
                            else
                            {
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Center;
                                g.FillRectangle(new SolidBrush(Helper.GlobalData.grid_header1), new Rectangle(x, y + h / 2, w, h / 2));
                                g.DrawRectangle(Pens.Gray, new Rectangle(x, y + h / 2, w, h / 2));
                                g.DrawString(header, this.Font, new SolidBrush(Helper.GlobalData.grid_header_fontcolor),
                                new Rectangle(x, y + h / 2, w, h / 2), sf);
                                CellObject obj = new CellObject();
                                obj.r = new Rectangle(x, y + h / 2, w, h / 2);
                                obj.row_index = -1;
                                obj.column_index = i;
                                obj.type = 1;
                                obj.ColumnName = col.column_name;
                                lstobj.Add(obj);

                                if (sort_col == col)
                                {
                                    sf.Alignment = StringAlignment.Far;
                                    sf.LineAlignment = StringAlignment.Center;
                                    string str = "";
                                    if (sort_type == "asc")
                                    {
                                        str = "↑";
                                    }
                                    else if (sort_type == "desc")
                                    {
                                        str = "↓";
                                    }

                                    g.DrawString(str, this.Font, new SolidBrush(Color.Maroon),
                                    new Rectangle(x, y + h / 2, w, h / 2), sf);
                                }
                            }

                            if (i == 0)
                            {
                                if (i == lstcolumn.Count - 1)
                                {
                                    if (col.group_header != "")
                                    {

                                        sf.Alignment = StringAlignment.Center;
                                        sf.LineAlignment = StringAlignment.Center;
                                        g.FillRectangle(new SolidBrush(Helper.GlobalData.grid_header1), new Rectangle(x - total_w, y, total_w + col.width, h / 2));
                                        g.DrawRectangle(Pens.Gray, new Rectangle(x - total_w, y, total_w + col.width, h / 2));
                                        g.DrawString(group_header, this.Font, new SolidBrush(Helper.GlobalData.grid_header_fontcolor),
                                        new Rectangle(x - total_w, y, total_w + col.width, h / 2), sf);
                                        CellObject obj = new CellObject();
                                        obj.r = new Rectangle(x - total_w, y, total_w + col.width, h / 2);
                                        obj.row_index = 0;
                                        obj.column_index = 0;
                                        obj.type = 2;
                                        obj.ColumnName = col.column_name;
                                        lstobj.Add(obj);

                                        group_header = col.group_header;
                                        total_w = col.width;
                                    }
                                }
                                else
                                {
                                    group_header = col.group_header;
                                    total_w = 0;
                                }
                            }
                            else if (i == lstcolumn.Count - 1)
                            {
                                if (col.group_header != "")
                                {
                                    sf.Alignment = StringAlignment.Center;
                                    sf.LineAlignment = StringAlignment.Center;
                                    g.FillRectangle(new SolidBrush(Helper.GlobalData.grid_header1), new Rectangle(x - total_w, y, total_w + col.width, h / 2));
                                    g.DrawRectangle(Pens.Gray, new Rectangle(x - total_w, y, total_w + col.width, h / 2));
                                    g.DrawString(col.group_header, this.Font, new SolidBrush(Helper.GlobalData.grid_header_fontcolor),
                                    new Rectangle(x - total_w, y, total_w + col.width, h / 2), sf);
                                    CellObject obj = new CellObject();
                                    obj.r = new Rectangle(x - total_w, y, total_w + col.width, h / 2);
                                    obj.row_index = 0;
                                    obj.column_index = 0;
                                    obj.type = 2;
                                    obj.ColumnName = col.column_name;
                                    lstobj.Add(obj);
                                    group_header = col.group_header;
                                    total_w = col.width;
                                }
                            }
                            else
                            {
                                if (col.group_header == "")
                                {
                                    if (col.group_header != group_header)
                                    {
                                        sf.Alignment = StringAlignment.Center;
                                        sf.LineAlignment = StringAlignment.Center;
                                        g.FillRectangle(new SolidBrush(Helper.GlobalData.grid_header1), new Rectangle(x - total_w, y, total_w, h / 2));
                                        g.DrawRectangle(Pens.Gray, new Rectangle(x - total_w, y, total_w, h / 2));
                                        g.DrawString(group_header, this.Font, new SolidBrush(Helper.GlobalData.grid_header_fontcolor),
                                        new Rectangle(x - total_w, y, total_w, h / 2), sf);
                                        CellObject obj = new CellObject();
                                        obj.r = new Rectangle(x - total_w, y, total_w, h / 2);
                                        obj.row_index = 0;
                                        obj.column_index = 0;
                                        obj.type = 2;
                                        obj.ColumnName = col.column_name;
                                        lstobj.Add(obj);
                                        group_header = col.group_header;
                                        total_w = 0;
                                    }
                                    else
                                    {
                                        group_header = "";
                                        total_w = 0;
                                    }

                                }
                                else if (col.group_header != "")
                                {
                                    if (col.group_header != group_header)
                                    {
                                        sf.Alignment = StringAlignment.Center;
                                        sf.LineAlignment = StringAlignment.Center;
                                        g.FillRectangle(new SolidBrush(Helper.GlobalData.grid_header1), new Rectangle(x - total_w, y, total_w, h / 2));
                                        g.DrawRectangle(Pens.Gray, new Rectangle(x - total_w, y, total_w, h / 2));
                                        g.DrawString(group_header, this.Font, new SolidBrush(Helper.GlobalData.grid_header_fontcolor),
                                        new Rectangle(x - total_w, y, total_w, h / 2), sf);
                                        CellObject obj = new CellObject();
                                        obj.r = new Rectangle(x - total_w, y, total_w, h / 2);
                                        obj.row_index = 0;
                                        obj.column_index = 0;
                                        obj.type = 2;
                                        obj.ColumnName = col.column_name;
                                        lstobj.Add(obj);
                                        group_header = col.group_header;
                                        total_w = col.width;
                                    }
                                    else
                                    {

                                        total_w += col.width;
                                    }
                                }

                            }

                            x += w;
                        }
                    }



                    //合计条
                    if (HasTotalBar() == true)
                    {

                        int y = this.Height - rowHeight;
                        if (scrollType2 == 1 || scrollType2 == 3)
                        {
                            y -= rowHeight;
                        }
                        LinearGradientBrush bru = new LinearGradientBrush(new Rectangle(0, y, this.Width, rowHeight), Helper.GlobalData.grid_footer1,
                        Helper.GlobalData.grid_footer2, LinearGradientMode.Vertical);
                        g.FillRectangle(bru, new Rectangle(0, y, this.Width, rowHeight));

                    }
                    //合计正常数据
                    if (HasTotalBar() == true)
                    {
                        int x = 0;
                        int y = this.Height - rowHeight;
                        if (scrollType2 == 1 || scrollType2 == 3)
                        {
                            y -= rowHeight;
                        }
                        int w = 0;
                        int h = rowHeight;
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;


                        x = -(int)h_value;
                        for (int i = 0; i < lstcolumn.Count; i++)
                        {

                            var col = lstcolumn[i];
                            w = col.width;
                            Rectangle r = new Rectangle(x, y, w, h);
                            CellObject obj = new CellObject();
                            obj.r = r;
                            obj.row_index = 0;
                            obj.column_index = i;
                            obj.type = 220M;
                            obj.ColumnName = col.column_name;
                            lstobj.Add(obj);
                            //
                            if (col.is_total == true)
                            {
                                if (tb.Columns.Contains(col.column_name) == true)
                                {
                                    if (col.align == 1)
                                    {
                                        sf.Alignment = StringAlignment.Near;
                                    }
                                    else if (col.align == 2)
                                    {
                                        sf.Alignment = StringAlignment.Center;
                                    }
                                    else if (col.align == 3)
                                    {
                                        sf.Alignment = StringAlignment.Far;
                                    }
                                    object values;
                                    decimal sum = 0;
                                    if (total_sum.TryGetValue(col.column_name, out sum))
                                    {
                                        values = sum;
                                    }
                                    else
                                    {
                                        values = total_row[col.column_name];
                                    }

                                    string value = Format(tb.Columns[col.column_name], values, col.format);
                                    g.DrawString(value, this.Font, Brushes.Black, r, sf);
                                }


                            }
                            else if (col.column_name == "index_1122")
                            {

                            }
                            else
                            {
                                sf.Alignment = StringAlignment.Center;

                                if (col.is_group == true)
                                {
                                    var img = pictureBox1.Image;
                                    int left = obj.r.X + (obj.r.Width - img.Width) / 2;
                                    int top = obj.r.Y + (obj.r.Height - img.Height) / 2;
                                    g.DrawImage(img, left, top);
                                }
                                else
                                {

                                }
                            }

                            //
                            x += w;
                        }
                    }


                    //滚动条

                    if (scrollType2 == 0)
                    {

                    }
                    else if (scrollType2 == 1)
                    {
                        if (scrollType == 0)
                        {
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;

                            int left = Convert.ToInt16(this.Width * ScrollHBoundRate);
                            g.DrawImage(Properties.Resources.滚动条_中间, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));
                            g.DrawImage(Properties.Resources.滚动条_左, new Rectangle(left + (rowHeight - Properties.Resources.滚动条_左.Width) / 2, this.Height - (rowHeight - Properties.Resources.滚动条_左.Height) / 2 - Properties.Resources.滚动条_左.Height, Properties.Resources.滚动条_左.Width, Properties.Resources.滚动条_左.Height));
                            g.DrawImage(Properties.Resources.滚动条_右, new Rectangle(this.Width - (rowHeight - Properties.Resources.滚动条_右.Width) / 2 - Properties.Resources.滚动条_右.Width, this.Height - (rowHeight - Properties.Resources.滚动条_右.Height) / 2 - Properties.Resources.滚动条_右.Height, Properties.Resources.滚动条_右.Width, Properties.Resources.滚动条_右.Height));


                        }
                        else
                        {
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;

                            int left = Convert.ToInt16(this.Width * ScrollHBoundRate);
                            g.DrawImage(Properties.Resources.滚动条_中间, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));
                            g.DrawImage(Properties.Resources.滚动条_左, new Rectangle(left + (rowHeight - Properties.Resources.滚动条_左.Width) / 2, this.Height - (rowHeight - Properties.Resources.滚动条_左.Height) / 2 - Properties.Resources.滚动条_左.Height, Properties.Resources.滚动条_左.Width, Properties.Resources.滚动条_左.Height));
                            g.DrawImage(Properties.Resources.滚动条_右, new Rectangle(this.Width - (rowHeight - Properties.Resources.滚动条_右.Width) / 2 - Properties.Resources.滚动条_右.Width, this.Height - (rowHeight - Properties.Resources.滚动条_右.Height) / 2 - Properties.Resources.滚动条_右.Height, Properties.Resources.滚动条_右.Width, Properties.Resources.滚动条_右.Height));
                            g.DrawImage(Properties.Resources.滚动条_滚动条, new Rectangle(left + rowHeight + (int)h_left, this.Height - (rowHeight - Properties.Resources.滚动条_滚动条.Height) / 2 - Properties.Resources.滚动条_滚动条.Height, (int)h_len - left, Properties.Resources.滚动条_滚动条.Height));

                            //
                            CellObject obj = new CellObject();
                            obj = new CellObject();
                            obj.r = new Rectangle(left, this.Height - rowHeight, rowHeight, rowHeight);
                            obj.type = 901;

                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, this.Height - rowHeight, rowHeight, rowHeight);
                            obj.type = 902;
                            lstobj.Add(obj);
                            //
                            obj = new CellObject();
                            obj.r = new Rectangle(left, this.Height - rowHeight, this.Width, rowHeight);
                            obj.type = 701;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(left + rowHeight + (int)h_left, this.Height - rowHeight, (int)h_len - left, rowHeight);
                            obj.type = 702;
                            lstobj.Add(obj);
                        }
                    }
                    else if (scrollType2 == 2)
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawImage(Properties.Resources.垂直_中间, new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height));
                        g.DrawImage(Properties.Resources.上, new Rectangle(this.Width - (rowHeight - Properties.Resources.上.Width) / 2 - Properties.Resources.上.Width, (rowHeight - Properties.Resources.上.Height) / 2, Properties.Resources.上.Width, Properties.Resources.上.Height));
                        g.DrawImage(Properties.Resources.下, new Rectangle(this.Width - (rowHeight - Properties.Resources.下.Width) / 2 - Properties.Resources.下.Width, this.Height - (rowHeight - Properties.Resources.下.Height) / 2 - Properties.Resources.下.Height, Properties.Resources.下.Width, Properties.Resources.下.Height));
                        g.DrawImage(Properties.Resources.垂直_滚动条, new Rectangle(this.Width - (rowHeight - Properties.Resources.垂直_滚动条.Width) / 2 - Properties.Resources.垂直_滚动条.Width, rowHeight + (int)v_top, Properties.Resources.垂直_滚动条.Width, (int)v_len));

                        //
                        CellObject obj = new CellObject();
                        obj = new CellObject();
                        obj.r = new Rectangle(this.Width - rowHeight, 0, rowHeight, rowHeight);
                        obj.type = 903;
                        lstobj.Add(obj);
                        obj = new CellObject();
                        obj.r = new Rectangle(this.Width - rowHeight, this.Height - rowHeight, rowHeight, rowHeight);
                        obj.type = 904;
                        lstobj.Add(obj);
                        //
                        obj = new CellObject();
                        obj.r = new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height);
                        obj.type = 703;
                        lstobj.Add(obj);
                        obj = new CellObject();
                        obj.r = new Rectangle(this.Width - rowHeight, rowHeight + (int)v_top, rowHeight, (int)v_len);
                        obj.type = 704;
                        lstobj.Add(obj);

                    }
                    else if (scrollType2 == 3)
                    {
                        if (scrollType == 2)
                        {
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;

                            int left = Convert.ToInt16(this.Width * ScrollHBoundRate);
                            g.DrawImage(Properties.Resources.滚动条_中间, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));
                            g.DrawImage(Properties.Resources.滚动条_左, new Rectangle(left + (rowHeight - Properties.Resources.滚动条_左.Width) / 2, this.Height - (rowHeight - Properties.Resources.滚动条_左.Height) / 2 - Properties.Resources.滚动条_左.Height, Properties.Resources.滚动条_左.Width, Properties.Resources.滚动条_左.Height));
                            g.DrawImage(Properties.Resources.滚动条_右, new Rectangle(this.Width - (rowHeight - Properties.Resources.滚动条_右.Width) / 2 - Properties.Resources.滚动条_右.Width - rowHeight, this.Height - (rowHeight - Properties.Resources.滚动条_右.Height) / 2 - Properties.Resources.滚动条_右.Height, Properties.Resources.滚动条_右.Width, Properties.Resources.滚动条_右.Height));

                            g.DrawRectangle(Pens.Gray, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));

                            g.DrawImage(Properties.Resources.垂直_中间, new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height - rowHeight));
                            g.DrawImage(Properties.Resources.上, new Rectangle(this.Width - (rowHeight - Properties.Resources.上.Width) / 2 - Properties.Resources.上.Width, (rowHeight - Properties.Resources.上.Height) / 2, Properties.Resources.上.Width, Properties.Resources.上.Height));
                            g.DrawImage(Properties.Resources.下, new Rectangle(this.Width - (rowHeight - Properties.Resources.下.Width) / 2 - Properties.Resources.下.Width, this.Height - (rowHeight - Properties.Resources.下.Height) / 2 - Properties.Resources.下.Height - rowHeight, Properties.Resources.下.Width, Properties.Resources.下.Height));
                            g.DrawImage(Properties.Resources.垂直_滚动条, new Rectangle(this.Width - (rowHeight - Properties.Resources.垂直_滚动条.Width) / 2 - Properties.Resources.垂直_滚动条.Width, rowHeight + (int)v_top, Properties.Resources.垂直_滚动条.Width, (int)v_len));
                            g.DrawRectangle(Pens.Gray, new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height));
                            //
                            CellObject obj = new CellObject();

                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, 0, rowHeight, rowHeight);
                            obj.type = 903;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, this.Height - rowHeight - rowHeight, rowHeight, rowHeight);
                            obj.type = 904;
                            lstobj.Add(obj);
                            //

                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height - rowHeight);
                            obj.type = 703;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, rowHeight + (int)v_top, rowHeight, (int)v_len);
                            obj.type = 704;
                            lstobj.Add(obj);
                        }
                        else
                        {
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.CompositingQuality = CompositingQuality.HighQuality;

                            int left = Convert.ToInt16(this.Width * ScrollHBoundRate);
                            g.DrawImage(Properties.Resources.滚动条_中间, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));
                            g.DrawImage(Properties.Resources.滚动条_左, new Rectangle(left + (rowHeight - Properties.Resources.滚动条_左.Width) / 2, this.Height - (rowHeight - Properties.Resources.滚动条_左.Height) / 2 - Properties.Resources.滚动条_左.Height, Properties.Resources.滚动条_左.Width, Properties.Resources.滚动条_左.Height));
                            g.DrawImage(Properties.Resources.滚动条_右, new Rectangle(this.Width - (rowHeight - Properties.Resources.滚动条_右.Width) / 2 - Properties.Resources.滚动条_右.Width - rowHeight, this.Height - (rowHeight - Properties.Resources.滚动条_右.Height) / 2 - Properties.Resources.滚动条_右.Height, Properties.Resources.滚动条_右.Width, Properties.Resources.滚动条_右.Height));
                            g.DrawImage(Properties.Resources.滚动条_滚动条, new Rectangle(left + rowHeight + (int)h_left, this.Height - (rowHeight - Properties.Resources.滚动条_滚动条.Height) / 2 - Properties.Resources.滚动条_滚动条.Height, (int)h_len - left, Properties.Resources.滚动条_滚动条.Height));
                            g.DrawRectangle(Pens.Gray, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));

                            g.DrawImage(Properties.Resources.垂直_中间, new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height - rowHeight));
                            g.DrawImage(Properties.Resources.上, new Rectangle(this.Width - (rowHeight - Properties.Resources.上.Width) / 2 - Properties.Resources.上.Width, (rowHeight - Properties.Resources.上.Height) / 2, Properties.Resources.上.Width, Properties.Resources.上.Height));
                            g.DrawImage(Properties.Resources.下, new Rectangle(this.Width - (rowHeight - Properties.Resources.下.Width) / 2 - Properties.Resources.下.Width, this.Height - (rowHeight - Properties.Resources.下.Height) / 2 - Properties.Resources.下.Height - rowHeight, Properties.Resources.下.Width, Properties.Resources.下.Height));
                            g.DrawImage(Properties.Resources.垂直_滚动条, new Rectangle(this.Width - (rowHeight - Properties.Resources.垂直_滚动条.Width) / 2 - Properties.Resources.垂直_滚动条.Width, rowHeight + (int)v_top, Properties.Resources.垂直_滚动条.Width, (int)v_len));
                            g.DrawRectangle(Pens.Gray, new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height));
                            //
                            CellObject obj = new CellObject();
                            obj = new CellObject();
                            obj.r = new Rectangle(left, this.Height - rowHeight, rowHeight, rowHeight);
                            obj.type = 901;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight - rowHeight, this.Height - rowHeight, rowHeight, rowHeight);
                            obj.type = 902;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, 0, rowHeight, rowHeight);
                            obj.type = 903;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, this.Height - rowHeight - rowHeight, rowHeight, rowHeight);
                            obj.type = 904;
                            lstobj.Add(obj);
                            //
                            obj = new CellObject();
                            obj.r = new Rectangle(left, this.Height - rowHeight, this.Width - rowHeight, rowHeight);
                            obj.type = 701;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(left + rowHeight + (int)h_left, this.Height - rowHeight, (int)h_len - left, rowHeight);
                            obj.type = 702;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height - rowHeight);
                            obj.type = 703;
                            lstobj.Add(obj);
                            obj = new CellObject();
                            obj.r = new Rectangle(this.Width - rowHeight, rowHeight + (int)v_top, rowHeight, (int)v_len);
                            obj.type = 704;
                            lstobj.Add(obj);
                        }

                    }
                    g.DrawRectangle(Pens.Gray, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                    //增行,删行,插行
                    if (isShowIco)
                    {
                        int x = 0;
                        int y = this.Height - rowHeight;
                        g.DrawImage(picAdd.Image, new Rectangle(x + 1,
                            y + (rowHeight - picAdd.Image.Height) / 2, picAdd.Image.Width, picAdd.Image.Height));
                        CellObject obj = new CellObject();
                        obj = new CellObject();
                        obj.r = new Rectangle(x, y, picAdd.Image.Width, picAdd.Image.Height);
                        obj.type = 801;
                        lstobj.Add(obj);
                        x += picAdd.Image.Width;
                        g.DrawImage(picCut.Image, new Rectangle(x + 1,
                            y + (rowHeight - picCut.Image.Height) / 2, picCut.Image.Width, picCut.Image.Height));
                        obj = new CellObject();
                        obj.r = new Rectangle(x, y, picCut.Image.Width, picCut.Image.Height);
                        obj.type = 802;
                        lstobj.Add(obj);
                        x += picCut.Image.Width;
                        g.DrawImage(picInsert.Image, new Rectangle(x + 1,
                            y + (rowHeight - picInsert.Image.Height) / 2, picInsert.Image.Width, picInsert.Image.Height));
                        obj = new CellObject();
                        obj.r = new Rectangle(x, y, picInsert.Image.Width, picInsert.Image.Height);
                        obj.type = 803;
                        lstobj.Add(obj);
                        x += picInsert.Image.Width;

                    }
                    //
                    lstobj = this.Sort(lstobj);
                    this.lstobj = lstobj;
                    //
                    if (cmd_type == 201)
                    {

                        var p = this.PointToClient(System.Windows.Forms.Cursor.Position);
                        Rectangle r = down_obj.r;
                        int x = p.X - r.Width / 2;
                        int y = p.Y - r.Height / 2;
                        r = new Rectangle(x, y, r.Width, r.Height);



                        foreach (CellObject obj in lstobj)
                        {
                            if (obj.type == 1)
                            {
                                Rectangle r2 = new Rectangle(obj.r.X, 0, obj.r.Width + 5, this.Height);
                                if (r2.Contains(p) == true)
                                {
                                    if (r.IntersectsWith(r2) == true)
                                    {
                                        Pen pen = new Pen(Color.Gray, 3);
                                        g.DrawLine(pen, obj.r.X, 0, obj.r.X, this.Height);
                                        break;
                                    }
                                }
                            }
                        }

                        var col = lstcolumn[down_obj.column_index];
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.LightBlue)), r);
                        g.DrawRectangle(Pens.Gray, r);
                        g.DrawString(col.header, this.Font, new SolidBrush(Color.Black), r, sf);

                    }
                    else if (cmd_type == 3)
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        var p = this.PointToClient(System.Windows.Forms.Cursor.Position);
                        Rectangle r = down_obj.r;
                        int x = p.X - r.Width / 2;
                        int y = p.Y - r.Height / 2;
                        r = new Rectangle(x, y, r.Width, r.Height);


                        foreach (CellObject obj in lstobj)
                        {
                            if (obj.type == 1)
                            {
                                Rectangle r2 = new Rectangle(obj.r.X, 0, obj.r.Width + 5, this.Height);
                                if (r2.Contains(p) == true)
                                {
                                    if (r2.IntersectsWith(r) == true)
                                    {
                                        Pen pen = new Pen(Color.Gray, 3);
                                        g.DrawLine(pen, obj.r.X + obj.r.Width, 0, obj.r.X + obj.r.Width, this.Height);
                                        break;
                                    }
                                }
                            }
                        }

                        g.FillPolygon(Brushes.Blue, new Point[]
                                        { new Point(x+r.Width,10),
                                        new Point(x+r.Width-5,0),
                                        new Point(x+r.Width+5,0)});

                    }
                    else if (cmd_type == 4)
                    {

                        Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.LightBlue)), r);
                        g.DrawString("排序中...", new Font("宋体", 20), new SolidBrush(Color.Blue), r, sf);
                    }
                    else if (cmd_type == 5)
                    {
                        Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.LightBlue)), r);
                        g.DrawString("分组中...", new Font("宋体", 20), new SolidBrush(Color.Blue), r, sf);
                    }

                }
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString(ex.ToString(), this.Font, Brushes.Black, new Rectangle(0, 0, this.Width, this.Height));
            }
        }

        private void a2()
        {

        }

        public List<CellObject> lstobj = new List<CellObject>();
        public class CellObject
        {
            public Rectangle r { get; set; }
            public int row_index { get; set; }
            public int column_index { get; set; }
            public string ColumnName { get; set; }
            public decimal _type = 0;
            /// <summary>
            /// 约整后：0_数据;1_标题;2_复合标题;110_冻结列标记;220_合计格;901_左按钮;902_右;903_上;904_下;701_水平;702_水平;703_垂直;704_垂直;801_增行;802_删行;803_插行
            /// </summary>
            public decimal type
            {
                get
                {
                    return (int)_type;
                }
                set
                {
                    _type = value;
                }
            }

        }

        public CellObject cur_cell = null;
        private CellObject down_obj = null;
        private Point p_down = new Point(-10, -10);
        /// <summary>
        /// 1_修改列宽;2_改变列位置初始;201_改变列位置;3_改变冻结列;4_排序数据;5分组
        /// </summary>
        private int cmd_type = 0;
        private void DataGrid_MouseDown(object sender, MouseEventArgs e)
        {


            p_down = new Point(e.X, e.Y);
            CellObject target_obj = null; ;
            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    target_obj = obj;
                    break;
                }
            }
            if (target_obj != null)
            {

                if (target_obj.type == 0)
                {
                    if (cur_cell == null)
                    {
                        cur_cell = target_obj;
                        this.Focus();
                    }
                    else
                    {
                        if (cur_cell.row_index == target_obj.row_index && cur_cell.column_index == target_obj.column_index)
                        {
                            if (Editing == true)
                            {
                                cur_cell = target_obj;
                            }
                            else
                            {
                                cur_cell = target_obj;
                                Editing = true;
                            }

                        }
                        else
                        {
                            Editing = false;
                            cur_cell = target_obj;

                            this.Focus();
                            this.Refresh();
                        }

                    }



                }
            }
            //

            //
            down_obj = target_obj;
            if (target_obj != null)
            {
                if (target_obj.type == 0)
                {
                    var col = lstcolumn[target_obj.column_index];
                    if (col.column_name == "index_1122")
                    {
                        if (tb.Columns.Contains("rows") == true)
                        {

                            if (tb.Rows[target_obj.row_index]["row_flag"].ToString() == "1")
                            {
                                tb.Rows[target_obj.row_index]["row_flag"] = "0";
                                var rows = (List<DataRow>)tb.Rows[target_obj.row_index]["rows"];
                                foreach (DataRow r in rows)
                                {
                                    DataRow r2 = tb.NewRow();
                                    r2.ItemArray = r.ItemArray;
                                    tb.Rows.Remove(r);
                                    r.ItemArray = r2.ItemArray;
                                }

                                this.Refresh();


                            }
                            else if (tb.Rows[target_obj.row_index]["row_flag"].ToString() == "0")
                            {

                                tb.Rows[target_obj.row_index]["row_flag"] = "1";
                                if (tb.Rows.Count - 1 == target_obj.row_index)
                                {
                                    var rows = (List<DataRow>)tb.Rows[target_obj.row_index]["rows"];
                                    foreach (DataRow r in rows)
                                    {
                                        tb.Rows.Add(r);
                                    }
                                }
                                else
                                {
                                    int index = target_obj.row_index + 1;
                                    var rows = (List<DataRow>)tb.Rows[target_obj.row_index]["rows"];
                                    foreach (DataRow r in rows)
                                    {
                                        tb.Rows.InsertAt(r, index);
                                        index++;
                                    }
                                }
                                this.Refresh();
                            }
                        }

                    }
                }
                else if (target_obj.type == 1)
                {
                    foreach (CellObject obj in lstobj)
                    {
                        if (obj.type == 1)
                        {
                            var col = lstcolumn[obj.column_index];
                            if (col.column_name == "index_1122")
                            {

                            }
                            else
                            {
                                Rectangle r = new Rectangle(obj.r.X, 0, obj.r.Width + 5, obj.r.Height + obj.r.Y);
                                if (r.Contains(e.X, e.Y))
                                {
                                    if (e.X > r.X + r.Width - 10)
                                    {
                                        down_obj = obj;

                                        col.width += e.X - (r.X + r.Width - 5);
                                        cmd_type = 1;
                                        this.Refresh();
                                        break;
                                    }
                                    else
                                    {
                                        cmd_type = 2;
                                    }
                                }
                            }

                        }

                    }
                }
                else if (target_obj.type == 220)
                {


                }
                else if (target_obj.type == 901)
                {
                    h_value -= rowHeight;

                    if (h_value < 0)
                    {
                        h_value = 0;
                    }

                }
                else if (target_obj.type == 902)
                {
                    h_value += rowHeight;

                    if (h_value > h_max)
                    {
                        h_value = h_max;
                    }

                }
                else if (target_obj.type == 903)
                {
                    v_value -= rowHeight;
                    int mod = (int)v_value % rowHeight;
                    if (mod != 0)
                    {
                        v_value = v_value - mod;
                    }
                    if (v_value < 0)
                    {
                        v_value = 0;
                    }

                }
                else if (target_obj.type == 904)
                {
                    v_value += rowHeight;
                    int mod = (int)v_value % rowHeight;
                    if (mod != 0)
                    {
                        v_value += rowHeight - mod;
                    }
                    if (v_value > v_max)
                    {
                        v_value = v_max;
                    }

                }
                else if (target_obj.type == 701)
                {
                    decimal left = e.X - rowHeight - this.Width * ScrollHBoundRate;
                    if (left < h_left)
                    {


                        h_value = left / ((h_width - h_len) / h_max);
                    }
                    else
                    {
                        left = left - h_len + this.Width * ScrollHBoundRate;
                        if (left < 0)
                        {
                            left = 0;
                        }
                        h_value = left / ((h_width - h_len) / h_max);
                    }

                }
                else if (target_obj.type == 703)
                {
                    decimal top = e.Y - rowHeight;
                    if (top < v_top)
                    {
                        v_value = top / ((v_height - v_len) / v_max);
                    }
                    else
                    {
                        top = top - v_len;
                        if (top < 0)
                        {
                            top = 0;
                        }
                        v_value = top / ((v_height - v_len) / v_max);
                    }


                }
            }
            //
            this.Refresh();
            cnt = 2;
            timer1.Enabled = true;
        }


        private void DataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                if (cmd_type == 1)
                {
                    var col = lstcolumn[down_obj.column_index];
                    col.width += e.X - p_down.X;
                    if (col.width < 10)
                    {
                        col.width = 10;
                    }
                    else if (col.width > 500)
                    {
                        col.width = 500;
                    }
                    p_down = new Point(e.X, e.Y);
                    this.Refresh();
                }
                else if (cmd_type == 2)
                {
                    int inte = Math.Abs(e.X - p_down.X);
                    if (inte < 5)
                    {
                        inte = Math.Abs(e.Y - p_down.Y);
                    }
                    if (inte >= 5)
                    {
                        cmd_type = 201;

                    }


                }
                else if (cmd_type == 201)
                {
                    this.Refresh();
                }
                else if (cmd_type == 3)
                {
                    this.Refresh();
                }
                else
                {
                    if (down_obj != null)
                    {
                        if (down_obj.type == 702)
                        {
                            decimal left = h_left + (e.X - p_down.X);
                            h_value = left / ((h_width - h_len) / h_max);
                            if (h_value < 0)
                            {
                                h_value = 0;
                            }
                            else if (h_value > h_max)
                            {
                                h_value = h_max;
                            }
                            p_down = new Point(e.X, e.Y);
                            this.Refresh();
                        }
                        else if (down_obj.type == 704)
                        {
                            decimal top = v_top + (e.Y - p_down.Y);
                            v_value = top / ((v_height - v_len) / v_max);
                            if (v_value < 0)
                            {
                                v_value = 0;
                            }
                            else if (v_value > v_max)
                            {
                                v_value = v_max;
                            }
                            p_down = new Point(e.X, e.Y);
                            this.Refresh();
                        }
                    }
                }

            }
            else if (e.Button == MouseButtons.Right)
            {

            }
            else if (e.Button == MouseButtons.None)
            {
                CellObject target_obj = null;
                foreach (CellObject obj in lstobj)
                {
                    if (obj.r.Contains(e.X, e.Y) == true)
                    {
                        target_obj = obj;

                        break;
                    }
                }

                if (target_obj != null)
                {
                    if (target_obj.type == 1)
                    {
                        if (target_obj.r.Contains(e.X, e.Y) == true)
                        {
                            if (e.X < target_obj.r.X + 5)
                            {
                                this.Cursor = System.Windows.Forms.Cursors.VSplit;
                            }
                            else if (e.X > target_obj.r.X + target_obj.r.Width - 5)
                            {
                                this.Cursor = System.Windows.Forms.Cursors.VSplit;
                            }
                            else
                            {
                                this.Cursor = System.Windows.Forms.Cursors.Default;
                            }
                        }
                        else
                        {
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                        }

                    }
                    else if (target_obj.type == 801)
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Hand;
                    }
                    else if (target_obj.type == 802)
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Hand;
                    }
                    else if (target_obj.type == 803)
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Hand;
                    }
                    else
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                    }


                }
                else
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private List<CellObject> Sort(List<CellObject> lstobj)
        {
            List<CellObject> lst = new List<CellObject>();
            var tb = new DataTable();
            tb.Columns.Add("type", typeof(decimal));
            tb.Columns.Add("obj", typeof(CellObject));
            tb.Columns.Add("row_id", typeof(int));
            int row_id = 0;
            foreach (CellObject obj in lstobj)
            {
                row_id++;
                tb.Rows.Add(obj._type, obj, row_id);
            }
            tb.DefaultView.Sort = "type desc,row_id asc";
            tb = tb.DefaultView.ToTable();
            foreach (DataRow row in tb.Rows)
            {
                lst.Add((CellObject)row["obj"]);
            }
            tb.Clear();
            tb = null;
            return lst;
        }

        public class GroupThread
        {
            public Control MainThreadCon = null;
            public delegate void GroupEndHandler(DataTable tb);
            public GroupEndHandler deleg = null;
            public DataTable tb;
            internal List<ColumnInfo> lstcolumn = new List<ColumnInfo>();
            public void Group()
            {
                string group_fields = "";
                string sum_fields = "";
                foreach (ColumnInfo col in lstcolumn)
                {
                    if (col.is_group == true)
                    {
                        if (group_fields == "")
                        {
                            group_fields += col.column_name;
                        }
                        else
                        {
                            group_fields += "," + col.column_name;
                        }
                    }
                    if (col.is_total == true)
                    {
                        if (sum_fields == "")
                        {
                            sum_fields += col.column_name;
                        }
                        else
                        {
                            sum_fields += "," + col.column_name;
                        }
                    }
                }

                tb = Helper.Conv.Group(tb, group_fields, sum_fields);

                if (MainThreadCon.IsDisposed == false)
                {
                    MainThreadCon.Invoke(deleg, tb);
                }

            }
        }

        private void GroupEnd(DataTable tb)
        {
            cmd_type = 0;
            foreach (DataRow row in tb.Rows)
            {
                row["row_flag"] = "0";
                var rows = (List<DataRow>)row["rows"];
                int index = 0;
                foreach (DataRow r in rows)
                {
                    index++;
                    r["row_flag"] = index.ToString() + " ";
                }
            }
            this.tb = tb;
            this.Refresh();
        }


        public class SortThread
        {
            public Control MainThreadCon;
            public delegate void SortEndHandler(DataTable tb);
            public SortEndHandler deleg;
            public DataTable tb;
            public string sort_str;
            public void Sort()
            {

                if (tb.Columns.Contains("rows") == true)
                {
                    var tb2 = tb.Clone();
                    foreach (DataRow row in tb.Rows)
                    {
                        if (row["row_flag"].ToString() == "1")
                        {
                            row["row_flag"] = "0";
                        }
                        if (row["row_flag"].ToString() == "0")
                        {
                            tb2.Rows.Add(row.ItemArray);
                        }
                    }

                    tb = tb2;

                    tb.DefaultView.Sort = sort_str;
                    tb = tb.DefaultView.ToTable();
                    foreach (DataRow row in tb.Rows)
                    {
                        var rows = (List<DataRow>)row["rows"];
                        List<object[]> lst = new List<object[]>();
                        foreach (DataRow r in rows)
                        {
                            lst.Add(r.ItemArray);
                        }
                        rows.Clear();
                        foreach (object[] arr in lst)
                        {
                            DataRow r = tb.NewRow();
                            r.ItemArray = arr;
                            rows.Add(r);
                        }
                    }
                }
                else
                {
                    tb = tb.Copy();
                    tb.DefaultView.Sort = sort_str;
                    tb = tb.DefaultView.ToTable();
                }


                if (MainThreadCon.IsDisposed == false)
                {
                    MainThreadCon.Invoke(deleg, tb);
                }

            }
        }


        private void SortEnd(DataTable tb)
        {
            cmd_type = 0;
            this.tb = tb;
            this.Refresh();
        }

        private ColumnInfo sort_col;
        private string sort_type;
        private void DataGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (down_obj != null)
            {
                if (down_obj.type == 1)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (tb != null)
                        {
                            var col = lstcolumn[down_obj.column_index];
                            if (tb.Columns.Contains(col.column_name) == true)
                            {
                                if (col.column_name == "index_1122")
                                {

                                }
                                else
                                {
                                    if (sort_col != col)
                                    {
                                        sort_col = col;
                                        sort_type = "asc";
                                    }
                                    else
                                    {
                                        if (sort_type == "asc")
                                        {
                                            sort_type = "desc";
                                        }
                                        else
                                        {
                                            sort_type = "asc";
                                        }
                                    }

                                    SortThread ins = new SortThread();
                                    ins.MainThreadCon = this;
                                    ins.tb = tb;
                                    ins.deleg = SortEnd;
                                    ins.sort_str = sort_col.column_name + " " + sort_type;
                                    System.Threading.Thread th = new System.Threading.Thread(ins.Sort);
                                    th.Start();

                                    cmd_type = 4;
                                    this.Refresh();
                                }
                            }
                        }


                    }
                }
                else
                {
                    if (this.cur_cell != null && tb.Rows.Count > this.cur_cell.row_index)
                    {
                        DataRow row = tb.Rows[this.cur_cell.row_index];
                    }
                }
            }

            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    if (obj.type == 0)
                    {
                        if (DoubleClickCell != null)
                        {
                            DoubleClickCell.Invoke(this, lstcolumn[obj.column_index].column_name, tb.Rows[obj.row_index], e);
                        }

                    }

                    break;
                }

            }


        }

        private void DataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            if (cmd_type == 1)
            {
                cmd_type = 0;
                this.Refresh();
            }
            else if (cmd_type == 2)
            {
                cmd_type = 0;
                this.Refresh();
            }
            else if (cmd_type == 201)
            {
                var p = this.PointToClient(System.Windows.Forms.Cursor.Position);
                Rectangle r = down_obj.r;
                int x = p.X - r.Width / 2;
                int y = p.Y - r.Height / 2;
                r = new Rectangle(x, y, r.Width, r.Height);


                foreach (CellObject obj in lstobj)
                {
                    if (obj.type == 1)
                    {
                        Rectangle r2 = new Rectangle(obj.r.X, 0, obj.r.Width, this.Height);
                        if (r2.Contains(p) == true)
                        {
                            if (r.IntersectsWith(r2) == true)
                            {
                                if (down_obj.column_index == obj.column_index)
                                {

                                }
                                else
                                {
                                    var col = lstcolumn[down_obj.column_index];
                                    var col2 = lstcolumn[obj.column_index];
                                    lstcolumn.Remove(col);
                                    int index = lstcolumn.IndexOf(col2);
                                    lstcolumn.Insert(index, col);


                                }
                                break;

                            }
                        }
                    }
                }
                cmd_type = 0;
                this.Refresh();
            }
            else if (cmd_type == 3)
            {

            }

            this.down_obj = null;
            this.timer1.Enabled = false;

        }

        private void DataGrid_Validated(object sender, EventArgs e)
        {

        }


        private string GetPath()
        {
            var tb = new DataTable();
            tb.Columns.Add("column_name");
            tb.Columns.Add("header");
            for (int i = 0; i < lstcolumn.Count; i++)
            {
                tb.Rows.Add(lstcolumn[i].column_name, lstcolumn[i].header);
            }
            tb.DefaultView.Sort = "column_name";
            tb = tb.DefaultView.ToTable();
            StringBuilder sb = new StringBuilder();
            sb.Append(this.GetType().ToString());
            foreach (DataRow row in tb.Rows)
            {
                if (sb.Length != 0)
                {
                    sb.Append(",");
                }
                sb.Append(row["column_name"].ToString() + "," + row["header"].ToString());
            }

            string md5 = Helper.Conv.ToMD5(sb.ToString());
            var path = AppSetting.path + "\\DataGrid\\" + md5;
            return path;
        }

        protected override void Dispose(bool disposing)
        {

            //
            this.SaveStyle();
            //
            base.Dispose(disposing);
        }


        private void SaveStyle()
        {
            string path = GetPath();
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            Helper.StringBuilderForXML sb = new Helper.StringBuilderForXML();
            sb.Append("frozen_column_name", "");
            foreach (ColumnInfo col in lstcolumn)
            {
                Helper.StringBuilderForXML sb3 = new Helper.StringBuilderForXML();

                sb3.Append("column_name", col.column_name);
                sb3.Append("width", col.width.ToString());
                string str3 = sb3.ToString();
                sb3.Clear();
                sb3.Append("column", str3);
                //
                sb.Append(sb3.ToString());
            }
            //
            string context = "<xml>" + sb.ToString() + "</xml>";
            string file = path + "\\style.xml";
            System.IO.File.WriteAllText(file, context, Encoding.GetEncoding("gb2312"));
        }


        private void ReadStyle()
        {
            string file = GetPath() + "\\style.xml";
            if (System.IO.File.Exists(file) == true)
            {
                string context = System.IO.File.ReadAllText(file, Encoding.GetEncoding("gb2312"));
                Helper.ReadXml r = new Helper.ReadXml(context);
                string frozen_column_name = Helper.Conv.ToString(r.Read("frozen_column_name"));

                //
                var tb = new DataTable();
                tb.Columns.Add("id", typeof(int));
                tb.Columns.Add("id2", typeof(int));
                tb.Columns.Add("col", typeof(ColumnInfo));
                int index = 0;
                foreach (ColumnInfo col in lstcolumn)
                {
                    index++;
                    tb.Rows.Add(index, 0, col);
                }
                index = 0;
                foreach (Helper.ReadXml r2 in r.ReadList("column"))
                {
                    index++;
                    foreach (DataRow row in tb.Rows)
                    {
                        var col = (ColumnInfo)row["col"];
                        if (col.column_name.ToLower() == r2.Read("column_name").ToLower())
                        {
                            col.width = Helper.Conv.ToInt16(r2.Read("width"));
                            row["id2"] = index;
                            break;
                        }

                    }

                }
                //
                tb.DefaultView.Sort = "id2,id";
                tb = tb.DefaultView.ToTable();
                List<ColumnInfo> lst = new List<ColumnInfo>();
                foreach (DataRow row in tb.Rows)
                {
                    var col = (ColumnInfo)row["col"];
                    lst.Add(col);
                }
                //
                this.lstcolumn = lst;
            }
        }

        private void DataGrid_VisibleChanged(object sender, EventArgs e)
        {

            this.ReadStyle();
        }



        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {

                if (dataList.Visible == true)
                {
                    dataList.WheelUp();
                }
                else
                {
                    int scrollType = this.ScrollType();
                    if (scrollType == 2 || scrollType == 3)
                    {
                        v_value -= rowHeight;
                        if (v_value < 0)
                        {
                            v_value = 0;
                        }
                        this.Refresh();
                    }
                }

            }
            else
            {
                if (dataList.Visible == true)
                {
                    dataList.WheelDown();
                }
                else
                {
                    int scrollType = this.ScrollType();
                    if (scrollType == 2 || scrollType == 3)
                    {
                        v_value += rowHeight;
                        if (v_value > v_max)
                        {
                            v_value = v_max;
                        }
                        this.Refresh();
                    }
                }

            }
        }

        private void KeyUp()
        {
            if (this.cur_cell != null && this.tb.Rows.Count > this.cur_cell.row_index)
            {
                int min_top = 0;
                if (HasGroupHeader() == true)
                {
                    min_top = rowHeight * 2;
                }
                else
                {
                    min_top = rowHeight;
                }
                if (this.cur_cell.r.Y - rowHeight < min_top)
                {
                    v_value -= rowHeight;
                    if (v_value < 0)
                    {
                        v_value = 0;
                    }
                    this.Refresh();
                }
                foreach (CellObject obj in lstobj)
                {
                    if (obj.type == 0)
                    {
                        if (obj.column_index == this.cur_cell.column_index)
                        {
                            if (obj.row_index == this.cur_cell.row_index - 1)
                            {
                                this.cur_cell = obj;
                                Refresh();
                                break;
                            }
                        }
                    }
                }

            }

        }

        private void KeyDown()
        {
            if (this.cur_cell != null && this.tb.Rows.Count > this.cur_cell.row_index)
            {
                int max_top = this.Height;
                if (HasTotalBar() == true)
                {
                    max_top -= rowHeight;
                }
                int scrollType = ScrollType();
                if (scrollType == 1 || scrollType == 3)
                {
                    max_top -= rowHeight;
                }
                if (this.cur_cell.r.Y + rowHeight * 2 > max_top)
                {
                    v_value += rowHeight;
                    if (v_value > v_max)
                    {
                        v_value = v_max;
                        this.Refresh();
                    }
                }
                foreach (CellObject obj in lstobj)
                {
                    if (obj.type == 0)
                    {
                        if (obj.column_index == this.cur_cell.column_index)
                        {
                            if (obj.row_index == this.cur_cell.row_index + 1)
                            {
                                this.cur_cell = obj;
                                Refresh();
                                break;
                            }
                        }
                    }
                }

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                if (dataList.Visible == true)
                {
                    Editing = false;
                    dataList.MoveUp();
                }
                else
                {
                    Editing = false;
                    KeyUp();

                }

                return true;
            }
            else if (keyData == Keys.Down)
            {
                if (dataList.Visible == true)
                {
                    Editing = false;
                    dataList.MoveDown();
                }
                else
                {
                    Editing = false;
                    KeyDown();

                }
                return true;
            }
            else if (keyData == Keys.Left)
            {

                if (this.cur_cell != null && this.tb.Rows.Count > this.cur_cell.row_index)
                {
                    int min_left = 0;

                    if (this.cur_cell.column_index == 0)
                    {
                        h_value = 0;
                        this.Refresh();
                    }
                    else
                    {
                        var pre_col = lstcolumn[this.cur_cell.column_index - 1];
                        int scrollType = ScrollType();
                        if (scrollType == 1 || scrollType == 3)
                        {
                            if (this.cur_cell.r.Left - pre_col.width < min_left)
                            {
                                h_value -= min_left - (this.cur_cell.r.Left - pre_col.width);
                                this.Refresh();
                            }

                        }

                        foreach (CellObject obj in lstobj)
                        {
                            if (obj.type == 0)
                            {
                                if (obj.row_index == this.cur_cell.row_index)
                                {
                                    if (obj.column_index == this.cur_cell.column_index - 1)
                                    {
                                        Editing = false;
                                        this.cur_cell = obj;
                                        Refresh();
                                        break;
                                    }
                                }
                            }
                        }
                    }


                }
                Editing = false;
                return true;
            }
            else if (keyData == Keys.Right || keyData == Keys.Tab)
            {

                if (this.cur_cell != null && this.tb.Rows.Count > this.cur_cell.row_index)
                {
                    int width = this.Width;
                    int scrollType = ScrollType();
                    if (scrollType == 1 || scrollType == 3)
                    {
                        width -= rowHeight;
                    }
                    if (this.cur_cell.column_index == lstcolumn.Count - 1)
                    {
                        h_value = h_max;
                    }
                    else
                    {
                        var next_col = lstcolumn[this.cur_cell.column_index + 1];
                        if (this.cur_cell.r.X + this.cur_cell.r.Width + next_col.width > width)
                        {

                            h_value += (this.cur_cell.r.X + this.cur_cell.r.Width + next_col.width) - width;
                            this.Refresh();
                        }
                    }
                    foreach (CellObject obj in lstobj)
                    {
                        if (obj.type == 0)
                        {
                            if (obj.row_index == this.cur_cell.row_index)
                            {
                                if (obj.column_index == this.cur_cell.column_index + 1)
                                {
                                    Editing = false;
                                    this.cur_cell = obj;
                                    Refresh();
                                    break;
                                }
                            }
                        }
                    }

                }
                Editing = false;
                return true;
            }
            else if (keyData == Keys.Enter)
            {

                if (dataList.Visible == true)
                {

                    if (dataList.CurrentRow() != null)
                    {
                        dataList.KeyEnter();
                        return true;
                    }

                }
                if (this.cur_cell != null && this.tb.Rows.Count > this.cur_cell.row_index)
                {
                    Editing = false;
                    CellObject cur_cell_bk = this.cur_cell;

                    int width = this.Width;
                    int scrollType = ScrollType();
                    if (scrollType == 1 || scrollType == 3)
                    {
                        width -= rowHeight;
                    }
                    if (this.cur_cell != null)
                    {
                        if (this.cur_cell.column_index == lstcolumn.Count - 1)
                        {
                            this.ProcessCmdKey(ref msg, Keys.Down);

                            h_value = 0;
                            this.Refresh();
                            foreach (CellObject obj in lstobj)
                            {
                                if (obj.type == 0)
                                {
                                    if (obj.row_index == this.cur_cell.row_index)
                                    {
                                        if (obj.column_index == 0)
                                        {
                                            this.cur_cell = obj;
                                            Refresh();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var next_col = lstcolumn[this.cur_cell.column_index + 1];
                            if (this.cur_cell.r.X + this.cur_cell.r.Width + next_col.width > width)
                            {
                                h_value += (this.cur_cell.r.X + this.cur_cell.r.Width + next_col.width) - width;
                                this.Refresh();
                            }
                            foreach (CellObject obj in lstobj)
                            {
                                if (obj.type == 0)
                                {
                                    if (obj.row_index == this.cur_cell.row_index)
                                    {
                                        if (obj.column_index == this.cur_cell.column_index + 1)
                                        {
                                            this.cur_cell = obj;
                                            Refresh();
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (cur_cell.row_index == cur_cell_bk.row_index && cur_cell.column_index == cur_cell_bk.column_index)
                        {

                        }
                        else
                        {
                            var col = lstcolumn[cur_cell.column_index];
                            if (col.can_edit == false)
                            {
                                this.ProcessCmdKey(ref msg, Keys.Enter);
                            }
                        }

                    }
                }
                //

                return true;
            }
            else if (keyData == Keys.Escape)
            {
                this.dataList.Visible = false;
                this.calendar.Visible = false;
                this.Editing = false;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.C))
            {
                if (this.cur_cell == null || tb == null || tb.Rows.Count < 1) return true;

                Clipboard.SetDataObject(tb.Rows[cur_cell.row_index][cur_cell.ColumnName].ToString());
                return true;
            }
            else
            {
                if (cur_cell != null)
                {
                    var col = lstcolumn[cur_cell.column_index];
                    if (col.can_edit == true)
                    {
                        if (Editing == false)
                        {
                            Editing = true;
                            keybd_event((byte)keyData, 0, 0, 0);

                            return true;
                        }
                        else
                        {
                            Editing = true;

                        }
                        if (cur_cell.row_index == tb.Rows.Count - 1 && IsAutoAddRow)
                        {
                            tb.Rows.Add(tb.NewRow());
                            this.Refresh();
                        }

                    }

                }
                return base.ProcessCmdKey(ref msg, keyData);

            }

        }

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public delegate void CellEndEditHandler(object sender, string column_name, DataRow row);
        public event CellEndEditHandler CellEndEdit;

        public DataRow CurrentRow()
        {
            if (cur_cell == null)
            {
                return null;
            }
            else
            {
                return tb.Rows[cur_cell.row_index];
            }
        }



        private bool editing = false;
        public bool Editing
        {
            get
            {
                return editing;
            }
            set
            {
                if (editing != value)
                {
                    editing = value;
                    if (value == false)
                    {
                        if (textBox1.Focused == true)
                        {
                            this.Focus();
                        }
                        textBox1.Visible = false;
                        //

                        var col = lstcolumn[cur_cell.column_index];

                        if (col.col_type == 1)
                        {
                            string keyword = textBox1.Text.Trim().Split('/')[0];
                            var tb2 = col.tb.Copy();

                            if (keyword != "")
                            {
                                if (col.tb.Columns.Contains(col.key_field) == true)
                                {
                                    tb2.DefaultView.RowFilter = "isnull(" + col.key_field + ",'')='" + keyword + "'";
                                    tb2 = tb2.DefaultView.ToTable();

                                }

                            }
                            else
                            {
                                tb2.Clear();
                            }

                            if (tb2.Rows.Count == 0)
                            {
                                var r = tb.Rows[cur_cell.row_index];
                                foreach (string str in col.tran_string.Split(','))
                                {
                                    if (str.Contains("->"))
                                    {
                                        var arr = str.Split("->".ToCharArray(0, 2), StringSplitOptions.RemoveEmptyEntries)[0].Split('/');
                                        string target_column_name = str.Split("->".ToCharArray(0, 2), StringSplitOptions.RemoveEmptyEntries)[1];
                                        r[target_column_name] = DBNull.Value;
                                    }

                                }
                            }
                            else
                            {
                                var row = tb2.Rows[0];

                                foreach (string str in col.tran_string.Split(','))
                                {

                                    if (str.Contains("->"))
                                    {
                                        string val = "";
                                        var arr = str.Split("->".ToCharArray(0, 2), StringSplitOptions.RemoveEmptyEntries)[0].Split('/');
                                        string target_column_name = str.Split("->".ToCharArray(0, 2), StringSplitOptions.RemoveEmptyEntries)[1];
                                        if (arr.Length > 1)
                                        {
                                            for (int i = 0; i < arr.Length; i++)
                                            {
                                                string col_name = arr[i];
                                                if (i == 0)
                                                {
                                                    if (row.Table.Columns.Contains(col_name) == true)
                                                    {
                                                        val += row[col_name].ToString();
                                                    }

                                                }
                                                else
                                                {
                                                    val += "/";
                                                    if (row.Table.Columns.Contains(col_name) == true)
                                                    {
                                                        val += row[col_name].ToString();
                                                    }
                                                }
                                            }
                                            var r = tb.Rows[cur_cell.row_index];
                                            r[target_column_name] = val;


                                        }
                                        else
                                        {
                                            string col_name = arr[0];
                                            var r = tb.Rows[cur_cell.row_index];
                                            r[target_column_name] = row[col_name].ToString();
                                        }



                                    }



                                }
                            }



                        }



                        if (CellEndEdit != null)
                        {
                            CellEndEdit.Invoke(this, col.column_name, CurrentRow());
                        }
                        calculate_total();
                        //if (1 == 1)
                        //{
                        //    var tb_total = tb.Clone();
                        //    total_row = tb_total.NewRow();
                        //    tb_total.Rows.Add(total_row);
                        //    foreach (ColumnInfo cl in lstcolumn)
                        //    {
                        //        if (cl.is_total == true)
                        //        {
                        //            if (tb.Columns.Contains(cl.column_name) == true)
                        //            {
                        //                decimal val = 0;
                        //                foreach (DataRow row in tb.Rows)
                        //                {
                        //                    val += Helper.Conv.ToDecimal(row[cl.column_name].ToString());
                        //                }
                        //                total_row[cl.column_name] = val.ToString();
                        //            }

                        //        }

                        //    }
                        //}

                        editing = value;

                    }
                    else
                    {
                        if (cur_cell == null)
                        {
                            editing = false;
                        }
                        else
                        {
                            var col = lstcolumn[cur_cell.column_index];
                            if (col.can_edit == true)
                            {
                                textBox1.BorderStyle = BorderStyle.None;
                                textBox1.Font = this.Font;
                                textBox1.Left = cur_cell.r.X + 1;
                                textBox1.Top = cur_cell.r.Y + (cur_cell.r.Height - textBox1.Height) / 2;
                                textBox1.Width = cur_cell.r.Width - 2;
                                this.Controls.Add(textBox1);
                                textBox1.Focus();

                                if (col.align == 1)
                                {
                                    textBox1.TextAlign = HorizontalAlignment.Left;
                                }
                                else if (col.align == 2)
                                {
                                    textBox1.TextAlign = HorizontalAlignment.Center;
                                }
                                else if (col.align == 3)
                                {
                                    textBox1.TextAlign = HorizontalAlignment.Right;
                                }
                                textBox1.Text = tb.Rows[cur_cell.row_index][col.column_name].ToString();
                                textBox1.Visible = true;
                                textBox1.SelectAll();
                            }
                            else
                            {
                                editing = false;
                            }

                        }

                    }

                    this.Refresh();
                }
                if (value == true)
                {
                    textBox1.Focus();
                }

            }
        }
        void calculate_total()
        {
            if (1 == 1)
            {
                var tb_total = tb.Clone();
                total_row = tb_total.NewRow();
                tb_total.Rows.Add(total_row);
                foreach (ColumnInfo cl in lstcolumn)
                {
                    if (cl.is_total == true)
                    {
                        if (tb.Columns.Contains(cl.column_name) == true)
                        {
                            decimal val = 0;
                            foreach (DataRow row in tb.Rows)
                            {
                                val += Helper.Conv.ToDecimal(row[cl.column_name].ToString());
                            }
                            total_row[cl.column_name] = val.ToString();
                        }

                    }

                }
            }
        }
        private bool isShowIco = true;
        public bool IsShowIco
        {
            get
            {
                return isShowIco;
            }
            set
            {
                isShowIco = value;
            }
        }

        private bool isAutoAddRow = true;
        public bool IsAutoAddRow { get => isAutoAddRow; set => isAutoAddRow = value; }

        private void DataGrid_MouseHover(object sender, EventArgs e)
        {

        }

        int cnt = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (tb != null)
            {
                var tb_total = tb.Clone();
                total_row = tb_total.NewRow();
                tb_total.Rows.Add(total_row);
                foreach (ColumnInfo cl in lstcolumn)
                {
                    if (cl.is_total == true)
                    {
                        if (tb.Columns.Contains(cl.column_name) == true)
                        {
                            decimal val = 0;
                            foreach (DataRow row in tb.Rows)
                            {
                                val += Helper.Conv.ToDecimal(row[cl.column_name].ToString());
                            }
                            total_row[cl.column_name] = val.ToString();
                        }

                    }

                }
            }

            cnt++;
            if (cnt < 5)
            {
                return;
            }
            if (down_obj != null)
            {
                if (down_obj.type == 901)
                {
                    h_value -= rowHeight;

                    if (h_value < 0)
                    {
                        h_value = 0;
                    }
                    this.Refresh();
                }
                else if (down_obj.type == 902)
                {
                    h_value += rowHeight;

                    if (h_value > h_max)
                    {
                        h_value = h_max;
                    }
                    this.Refresh();
                }
                else if (down_obj.type == 903)
                {
                    v_value -= rowHeight;
                    int mod = (int)v_value % rowHeight;
                    if (mod != 0)
                    {
                        v_value = v_value - mod;
                    }
                    if (v_value < 0)
                    {
                        v_value = 0;
                    }
                    this.Refresh();
                }
                else if (down_obj.type == 904)
                {
                    v_value += rowHeight;
                    int mod = (int)v_value % rowHeight;
                    if (mod != 0)
                    {
                        v_value += rowHeight - mod;
                    }
                    if (v_value > v_max)
                    {
                        v_value = v_max;

                    }
                    this.Refresh();
                }
            }

        }


        public delegate void ClickCellHandler(object sender, string column_name, DataRow row, MouseEventArgs e);
        public delegate void DoubleClickCellHandler(object sender, string column_name, DataRow row, MouseEventArgs e);
        /// <summary>
        /// 点击单元格
        /// </summary>
        public event ClickCellHandler ClickCell;
        /// <summary>
        /// 双击单元格
        /// </summary>
        public event DoubleClickCellHandler DoubleClickCell;

        private void DataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    if (obj.type == 0)
                    {
                        if (ClickCell != null)
                        {
                            ClickCell.Invoke(this, lstcolumn[obj.column_index].column_name, tb.Rows[obj.row_index], e);
                            if (CellEndEdit != null)
                            {
                                CellEndEdit.Invoke(this, lstcolumn[obj.column_index].column_name, tb.Rows[obj.row_index]);
                            }
                        }

                    }
                    else if (obj.type == 801)
                    {
                        if (tb != null)
                        {

                            tb.Rows.Add();
                            Editing = false;
                            this.Refresh();
                        }
                    }
                    else if (obj.type == 802)
                    {
                        if (cur_cell != null && tb != null)
                        {
                            if (tb.Rows.Count > cur_cell.row_index)
                            {

                                tb.Rows.RemoveAt(cur_cell.row_index);
                                calculate_total();
                                this.Refresh();
                            }

                        }
                    }
                    else if (obj.type == 803)
                    {
                        if (cur_cell != null && tb != null)
                        {
                            if (tb.Rows.Count > cur_cell.row_index)
                            {

                                var row = tb.NewRow();
                                tb.Rows.InsertAt(row, cur_cell.row_index);
                                Editing = false;
                                this.Refresh();
                            }

                        }
                    }

                    break;
                }

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (cur_cell != null)
            {
                try
                {
                    var col = lstcolumn[cur_cell.column_index];
                    if (tb.Columns.Contains(col.column_name) == true)
                    {
                        var tb_col = tb.Columns[col.column_name];

                        object target = tb.Rows[cur_cell.row_index][col.column_name];
                        if (target is IValueChanged instance)
                        {
                            instance.Set(this, textBox1.Text);
                        }
                        else if (tb_col.DataType == typeof(decimal) || tb_col.DataType == typeof(float) || tb_col.DataType == typeof(double))
                        {
                            tb.Rows[cur_cell.row_index][col.column_name] = Helper.Conv.ToDecimal(textBox1.Text.Trim());
                        }
                        else if (tb_col.DataType == typeof(int) || tb_col.DataType == typeof(Int32) || tb_col.DataType == typeof(Int64))
                        {
                            tb.Rows[cur_cell.row_index][col.column_name] = Helper.Conv.ToInt(textBox1.Text.Trim());
                        }
                        else if (tb_col.DataType == typeof(DateTime))
                        {
                            var dt = Helper.Conv.ToDateTime(textBox1.Text.Trim());
                            if (dt == DateTime.MinValue)
                            {
                                tb.Rows[cur_cell.row_index][col.column_name] = DBNull.Value;
                            }
                            else
                            {
                                tb.Rows[cur_cell.row_index][col.column_name] = dt;
                            }

                        }
                        else
                        {
                            tb.Rows[cur_cell.row_index][col.column_name] = textBox1.Text.Trim();
                        }

                    }


                }
                catch (Exception)
                {

                }
                //
                if (1 == 1)
                {
                    var col = lstcolumn[cur_cell.column_index];
                    if (col.col_type == 1)
                    {
                        string keyword = this.textBox1.Text.Trim().Split('/')[0];
                        var tb2 = col.tb.Copy();
                        StringBuilder sb = new StringBuilder();
                        foreach (string field in col.show_fields.Split(','))
                        {
                            var arr = field.Split(':');
                            if (tb2.Columns.Contains(arr[0]) == true)
                            {
                                var cl = tb2.Columns[arr[0]];
                                if (cl.DataType == typeof(string))
                                {
                                    if (sb.Length == 0)
                                    {
                                        sb.Append("isnull(" + cl.ColumnName + ",'')" + " like '%" + keyword + "%'");
                                    }
                                    else
                                    {
                                        sb.Append(" or ");
                                        sb.Append("isnull(" + cl.ColumnName + ",'')" + " like '%" + keyword + "%'");
                                    }
                                }
                            }

                        }
                        tb2.DefaultView.RowFilter = sb.ToString();
                        tb2 = tb2.DefaultView.ToTable();

                        this.dataList.DataSource = tb2;
                        this.ShowDataList();

                    }
                }


            }
        }

        private void EditGrid_Leave(object sender, EventArgs e)
        {
            this.Editing = false;
        }

        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (textBox1.Visible == false)
            {
                if (calendar.Visible == true)
                {
                    calendar.Visible = false;
                }
                if (dataList.Visible == true)
                {
                    dataList.Visible = false;
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (calendar.Visible == true)
            {
                calendar.Visible = false;
            }
            if (dataList.Visible == true)
            {
                dataList.Visible = false;
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (cur_cell != null)
            {
                var col = lstcolumn[cur_cell.column_index];
                if (col.col_type == 1)
                {

                    this.ShowDataList();
                }
                else if (col.col_type == 2)
                {
                    ShowCalendar();
                }
            }
        }

        public static bool IsNum(char str)
        {
            if ((str >= '0' && str <= '9'))
                return true;
            return false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
                return;
            }
            if (tb != null)
            {
                if (this.cur_cell != null && this.tb.Rows.Count > 0)
                {
                    var col = lstcolumn[this.cur_cell.column_index];
                    if (tb.Columns.Contains(col.column_name))
                    {
                        var tb_col = tb.Columns[col.column_name];
                        if (e.KeyChar == (char)Keys.Delete || e.KeyChar == (char)Keys.Back)
                        {

                        }
                        else if (tb_col.DataType == typeof(decimal) || tb_col.DataType == typeof(float) || tb_col.DataType == typeof(double))
                        {
                            if (textBox1.Text.Contains(".") == true)
                            {
                                if (textBox1.Text.IndexOf('.') >= 8)
                                {
                                    e.Handled = true;
                                    return;
                                }
                            }
                            else
                            {
                                if (textBox1.Text.Length >= 8)
                                {
                                    e.Handled = true;
                                    return;
                                }
                            }

                            if (IsNum(e.KeyChar) == true || e.KeyChar == '.' || e.KeyChar == '-')
                            {

                            }
                            else
                            {
                                e.Handled = true;
                            }
                        }
                        else if (tb_col.DataType == typeof(int) || tb_col.DataType == typeof(Int32) || tb_col.DataType == typeof(Int64) || e.KeyChar == (char)Keys.Delete)
                        {
                            if (textBox1.Text.Length >= 6)
                            {

                            }
                            else if (IsNum(e.KeyChar) == true || e.KeyChar == '-')
                            {

                            }
                            else
                            {
                                e.Handled = true;
                            }
                        }
                        else if (tb_col.DataType == typeof(DateTime))
                        {
                            if (IsNum(e.KeyChar) == true || e.KeyChar == '-')
                            {

                            }
                            else
                            {
                                e.Handled = true;
                            }
                        }

                    }
                }
            }

        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (down_obj != null)
            {
                if (down_obj.type == 1)
                {

                }
                else
                {
                    if (this.cur_cell != null && this.tb.Rows.Count > 0)
                    {
                        DataRow row = tb.Rows[this.cur_cell.row_index];
                    }
                }
            }

            Point p = new Point(e.X + textBox1.Left, e.Y + textBox1.Top);

            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(p.X, p.Y) == true)
                {
                    if (obj.type == 0)
                    {
                        if (DoubleClickCell != null)
                        {
                            DoubleClickCell.Invoke(this, lstcolumn[obj.column_index].column_name, tb.Rows[obj.row_index],
                                 new MouseEventArgs(e.Button, e.Clicks, 1, 1, e.Delta));
                        }

                    }

                    break;
                }

            }



        }

        #region 右键菜单

        private void tsmiDateCopy_Click(object sender, EventArgs e)
        {
            if (this.cur_cell != null)


                Clipboard.SetDataObject(tb.Rows[cur_cell.row_index][cur_cell.ColumnName].ToString());


        }
        private void tsmiDataAdd_Click(object sender, EventArgs e)
        {
            tb.Rows.Add();
            this.Refresh();
        }
        private void tsmiInsertRow_Click(object sender, EventArgs e)
        {
            if (cur_cell != null)
            {
                var row = tb.NewRow();
                tb.Rows.InsertAt(row, cur_cell.row_index);
                this.Refresh();
            }

        }

        private void tsmiDateDel_Click(object sender, EventArgs e)
        {
            if (cur_cell != null)
            {
                tb.Rows.RemoveAt(cur_cell.row_index);
                this.Refresh();
            }
        }

        private void tsmDataClear_Click(object sender, EventArgs e)
        {
            this.tb.Clear();
            this.Refresh();
        }
        private void tsmiDataSearch_Click(object sender, EventArgs e)
        {

            if (cur_cell == null)
                return;
            editGrid_search editGrid_Search = new editGrid_search(this);


            editGrid_Search.searchLabel.Text = "搜索条件：" + lstcolumn[cur_cell.column_index].header;
            editGrid_Search.searchLabel.Tag = lstcolumn[cur_cell.column_index].column_name;
            editGrid_Search.ShowDialog();
        }


        #endregion


    }

}
