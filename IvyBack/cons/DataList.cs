using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;
using IvyBack.Helper;
using IvyBack.Properties;
namespace IvyBack.cons
{
    class DataList : System.Windows.Forms.Control
    {

        private aa a;
        public DataList()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.AllPaintingInWmPaint, true);
            //
            this.InitializeComponent();
            //

            //
            this.TabStop = false;
            this.MouseWheel += this.Panel_MouseWheel;

            //
            a = new aa();
            a.Click += click;
            Application.AddMessageFilter(a);

        }

        private void click()
        {
            Point p = this.PointToClient(System.Windows.Forms.Cursor.Position);
            Rectangle r = new Rectangle(0, 0, this.Width, this.Height);
            if (r.Contains(p) == false)
            {
                this.Visible = false;
            }
        }


        protected override void Dispose(bool disposing)
        {
            Application.RemoveMessageFilter(a);
            //
            base.Dispose(disposing);
        }

        public class aa : IMessageFilter
        {
            public delegate void ClickHandler();
            private ClickHandler click;
            public event ClickHandler Click
            {
                add { click += value; }
                remove { click -= value; }
            }

            bool IMessageFilter.PreFilterMessage(ref Message m)
            {
                switch (m.Msg)
                {
                    case 513://鼠标左键Down
                        if (click != null)
                        {
                            click.Invoke();
                        }
                        break;
                    case 516://鼠标右键Down
                        if (click != null)
                        {
                            click.Invoke();
                        }
                        break;
                }
                return false;
            }
        }



        public void ClearColumn()
        {

            lstcolumn.Clear();
            sort_col = null;
            sort_type = "";

        }

        private void mh_MouseDownEvent(object sender, MouseEventArgs e)
        {

            Point p1 = new Point(0, 0);
            Point p2 = new Point(this.Width, this.Height);
            p1 = this.PointToScreen(p1);
            p2 = this.PointToScreen(p2);
            Rectangle r = new Rectangle(p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);
            if (r.Contains(new Point(e.X, e.Y)) == false)
            {
                this.Visible = false;
            }
        }

        private System.ComponentModel.IContainer components;
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
            public int temp1 { get; set; }
            //
            public ColumnInfo Copy()
            {

                return (ColumnInfo)this.MemberwiseClone();

            }
        }

        private bool _MergeCell = true;
        /// <summary>
        /// 是否合并单元格
        /// </summary>
        public bool MergeCell
        {
            get
            {
                return _MergeCell;
            }
            set
            {
                _MergeCell = value;
                this.Refresh();
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
            int scrollType = this.ScrollType();
            if (scrollType == 1 || scrollType == 3)
            {
                h -= rowHeight;
            }
            return h / rowHeight;
        }

        private List<ColumnInfo> lstcolumn = new List<ColumnInfo>();
        private PictureBox pictureBox1;

        public void AddColumn(string column_name, string header, string group_header, int width, int align, string format)
        {

            ColumnInfo col = new ColumnInfo();
            col.column_name = column_name;
            col.header = header;
            col.group_header = group_header;
            col.width = width;
            col.align = align;
            col.format = format;
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

        private DataTable tb_source;
        private DataTable tb;
        private DataRow total_row;
        public DataTable DataSource
        {
            get
            {
                return tb;
            }
            set
            {
                if (value == null)
                {
                    tb_source = value;
                    tb = value;
                }
                else
                {
                    this.sort_col = null;
                    this.sort_type = "";
                    this.cur_cell = null;
                    tb_source = value.Copy();
                    h_value = 0;
                    v_value = 0;
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
                this.Refresh();
            }
        }

        private int rowHeight = 25;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataList));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // DataList
            // 
            this.VisibleChanged += new System.EventHandler(this.DataList_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DataGrid_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseDown);
            this.MouseHover += new System.EventHandler(this.DataGrid_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataGrid_MouseUp);
            this.Validated += new System.EventHandler(this.DataGrid_Validated);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        decimal v_value = 0;
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
            if (ScrollType() == 3)
            {
                v_page_size -= rowHeight;
            }
            v_max = tb.Rows.Count * rowHeight;

            v_height = this.Height - rowHeight * 2;
            if (ScrollType() == 3)
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

        void ComputeH()
        {
            h_page_size = this.Width;
            if (ScrollType() == 3)
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
            if (ScrollType() == 3)
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
            int scrollType = ScrollType();
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
            return new Rectangle(x, y, w, h);
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

            int total_h = 0;
            if (tb == null)
            {

            }
            else
            {
                total_h = tb.Rows.Count * rowHeight;
            }

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

                    Rectangle dataRectangle = this.DataRectangle();
                    Graphics g = e.Graphics;
                    List<CellObject> lstobj = new List<CellObject>();
                    int scrollType = ScrollType();
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
                                if (cur_cell == null)
                                {
                                    b = this.BackColor;
                                    f = Color.Black;
                                }
                                else if (cur_cell.row_index == i)
                                {
                                    b = Helper.GlobalData.grid_sel;
                                    f = Helper.GlobalData.grid_sel_font;
                                }
                                else
                                {
                                    b = this.BackColor;
                                    f = Color.Black;
                                }
                                CellObject obj = new CellObject();
                                obj.r = new Rectangle(x, y, w, h);
                                obj.row_index = i;
                                obj.column_index = j;
                                obj.type = 0;
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
                                    string value = Format(tb.Columns[col.column_name], tb.Rows[i][col.column_name], col.format);
                                    g.FillRectangle(new SolidBrush(b), new Rectangle(x, y, w, h));
                                    g.DrawRectangle(Pens.Gray, new Rectangle(x, y, w, h));
                                    g.DrawString(value, this.Font, new SolidBrush(f), new Rectangle(x + 1, y + 1, w - 1, h - 1), sf);
                                    col.temp1 = 1;

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
                        if (scrollType == 1 || scrollType == 3)
                        {
                            y -= rowHeight;
                        }
                        LinearGradientBrush bru = new LinearGradientBrush(new Rectangle(0, y, this.Width, rowHeight), Color.AliceBlue,
                        Color.AliceBlue, LinearGradientMode.Vertical);
                        g.FillRectangle(bru, new Rectangle(0, y, this.Width, rowHeight));

                    }
                    //合计正常数据
                    if (HasTotalBar() == true)
                    {
                        int x = 0;
                        int y = this.Height - rowHeight;
                        if (scrollType == 1 || scrollType == 3)
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
                                    string value = Format(tb.Columns[col.column_name], total_row[col.column_name], col.format);
                                    g.DrawString(value, this.Font, Brushes.Black, r, sf);
                                }


                            }
                            else if (col.column_name == "index_1122")
                            {

                            }
                            else
                            {
                                sf.Alignment = StringAlignment.Center;
                                g.FillRectangle(Brushes.AliceBlue, r);
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


                    if (scrollType == 0)
                    {

                    }
                    else if (scrollType == 1)
                    {

                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawImage(Properties.Resources.滚动条_中间, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));
                        g.DrawImage(Properties.Resources.滚动条_左, new Rectangle((rowHeight - Properties.Resources.滚动条_左.Width) / 2, this.Height - (rowHeight - Properties.Resources.滚动条_左.Height) / 2 - Properties.Resources.滚动条_左.Height, Properties.Resources.滚动条_左.Width, Properties.Resources.滚动条_左.Height));
                        g.DrawImage(Properties.Resources.滚动条_右, new Rectangle(this.Width - (rowHeight - Properties.Resources.滚动条_右.Width) / 2 - Properties.Resources.滚动条_右.Width, this.Height - (rowHeight - Properties.Resources.滚动条_右.Height) / 2 - Properties.Resources.滚动条_右.Height, Properties.Resources.滚动条_右.Width, Properties.Resources.滚动条_右.Height));
                        g.DrawImage(Properties.Resources.滚动条_滚动条, new Rectangle(rowHeight + (int)h_left, this.Height - (rowHeight - Properties.Resources.滚动条_滚动条.Height) / 2 - Properties.Resources.滚动条_滚动条.Height, (int)h_len, Properties.Resources.滚动条_滚动条.Height));

                        //
                        CellObject obj = new CellObject();
                        obj = new CellObject();
                        obj.r = new Rectangle(0, this.Height - rowHeight, rowHeight, rowHeight);
                        obj.type = 901;
                        lstobj.Add(obj);
                        obj = new CellObject();
                        obj.r = new Rectangle(this.Width - rowHeight, this.Height - rowHeight, rowHeight, rowHeight);
                        obj.type = 902;
                        lstobj.Add(obj);
                        //
                        obj = new CellObject();
                        obj.r = new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight);
                        obj.type = 701;
                        lstobj.Add(obj);
                        obj = new CellObject();
                        obj.r = new Rectangle(rowHeight + (int)h_left, this.Height - rowHeight, (int)h_len, rowHeight);
                        obj.type = 702;
                        lstobj.Add(obj);

                    }
                    else if (scrollType == 2)
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
                    else if (scrollType == 3)
                    {

                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;

                        g.DrawImage(Properties.Resources.滚动条_中间, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));
                        g.DrawImage(Properties.Resources.滚动条_左, new Rectangle((rowHeight - Properties.Resources.滚动条_左.Width) / 2, this.Height - (rowHeight - Properties.Resources.滚动条_左.Height) / 2 - Properties.Resources.滚动条_左.Height, Properties.Resources.滚动条_左.Width, Properties.Resources.滚动条_左.Height));
                        g.DrawImage(Properties.Resources.滚动条_右, new Rectangle(this.Width - (rowHeight - Properties.Resources.滚动条_右.Width) / 2 - Properties.Resources.滚动条_右.Width - rowHeight, this.Height - (rowHeight - Properties.Resources.滚动条_右.Height) / 2 - Properties.Resources.滚动条_右.Height, Properties.Resources.滚动条_右.Width, Properties.Resources.滚动条_右.Height));
                        g.DrawImage(Properties.Resources.滚动条_滚动条, new Rectangle(rowHeight + (int)h_left, this.Height - (rowHeight - Properties.Resources.滚动条_滚动条.Height) / 2 - Properties.Resources.滚动条_滚动条.Height, (int)h_len, Properties.Resources.滚动条_滚动条.Height));
                        g.DrawRectangle(Pens.Gray, new Rectangle(0, this.Height - rowHeight, this.Width, rowHeight));

                        g.DrawImage(Properties.Resources.垂直_中间, new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height - rowHeight));
                        g.DrawImage(Properties.Resources.上, new Rectangle(this.Width - (rowHeight - Properties.Resources.上.Width) / 2 - Properties.Resources.上.Width, (rowHeight - Properties.Resources.上.Height) / 2, Properties.Resources.上.Width, Properties.Resources.上.Height));
                        g.DrawImage(Properties.Resources.下, new Rectangle(this.Width - (rowHeight - Properties.Resources.下.Width) / 2 - Properties.Resources.下.Width, this.Height - (rowHeight - Properties.Resources.下.Height) / 2 - Properties.Resources.下.Height - rowHeight, Properties.Resources.下.Width, Properties.Resources.下.Height));
                        g.DrawImage(Properties.Resources.垂直_滚动条, new Rectangle(this.Width - (rowHeight - Properties.Resources.垂直_滚动条.Width) / 2 - Properties.Resources.垂直_滚动条.Width, rowHeight + (int)v_top, Properties.Resources.垂直_滚动条.Width, (int)v_len));
                        g.DrawRectangle(Pens.Gray, new Rectangle(this.Width - rowHeight, 0, rowHeight, this.Height));
                        //
                        CellObject obj = new CellObject();
                        obj = new CellObject();
                        obj.r = new Rectangle(0, this.Height - rowHeight, rowHeight, rowHeight);
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
                        obj.r = new Rectangle(0, this.Height - rowHeight, this.Width - rowHeight, rowHeight);
                        obj.type = 701;
                        lstobj.Add(obj);
                        obj = new CellObject();
                        obj.r = new Rectangle(rowHeight + (int)h_left, this.Height - rowHeight, (int)h_len, rowHeight);
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
                    g.DrawRectangle(Pens.Gray, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
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

        List<CellObject> lstobj = new List<CellObject>();
        private class CellObject
        {
            public Rectangle r { get; set; }
            public int row_index { get; set; }
            public int column_index { get; set; }

            public decimal _type = 0;
            /// <summary>
            /// 约整后：0_数据;1_标题;2_复合标题;110_冻结列标记;220_合计格;901_左按钮;902_右;903_上;904_下;701_水平;702_水平;703_垂直;704_垂直
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

        private CellObject cur_cell = null;
        private CellObject down_obj = null;
        private Point p_down = new Point(-10, -10);
        /// <summary>
        /// 1_修改列宽;2_改变列位置初始;201_改变列位置;3_改变冻结列;4_排序数据;5分组
        /// </summary>
        private int cmd_type = 0;
        private void DataGrid_MouseDown(object sender, MouseEventArgs e)
        {

            //
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
                    cur_cell = target_obj;
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
                else if (target_obj.type == 110)
                {
                    cmd_type = 3;
                }
                else if (target_obj.type == 220)
                {
                    var col = lstcolumn[target_obj.column_index];
                    if (col.column_name == "index_1122")
                    {
                    }
                    else
                    {
                        if (col.is_total == false)
                        {
                            if (col.is_group == false)
                            {
                                sort_col = null;
                                sort_type = "";
                                col.is_group = true;


                                GroupThread ins = new GroupThread();
                                ins.MainThreadCon = this;
                                ins.tb = tb_source;
                                ins.deleg = GroupEnd;
                                ins.lstcolumn = this.lstcolumn;
                                System.Threading.Thread th = new System.Threading.Thread(ins.Group);
                                th.Start();

                                h_value = 0;
                                v_value = 0;
                                cmd_type = 5;
                                this.Refresh();
                            }
                            else
                            {
                                sort_col = null;
                                sort_type = "";
                                col.is_group = false;
                                int flag = 0;
                                foreach (ColumnInfo cl in lstcolumn)
                                {
                                    if (cl.is_group == true)
                                    {
                                        flag = 1;
                                        break;
                                    }
                                }
                                if (flag == 1)
                                {
                                    GroupThread ins = new GroupThread();
                                    ins.MainThreadCon = this;
                                    ins.tb = tb_source;
                                    ins.deleg = GroupEnd;
                                    ins.lstcolumn = this.lstcolumn;
                                    System.Threading.Thread th = new System.Threading.Thread(ins.Group);
                                    th.Start();
                                    h_value = 0;
                                    v_value = 0;
                                    cmd_type = 5;
                                    this.Refresh();
                                }
                                else
                                {
                                    this.tb = this.tb_source;
                                    this.Refresh();
                                }



                            }
                        }
                    }

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
                    decimal left = e.X - rowHeight;
                    if (left < h_left)
                    {


                        h_value = left / ((h_width - h_len) / h_max);
                    }
                    else
                    {
                        left = left - h_len;
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
                    else if (target_obj.type == 220)
                    {

                    }
                    else
                    {
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                    }

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
            public Control MainThreadCon;
            public delegate void GroupEndHandler(DataTable tb);
            public GroupEndHandler deleg;
            public DataTable tb;
            internal List<ColumnInfo> lstcolumn;
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
                else
                {
                    if (this.cur_cell != null)
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
            for (int i = 0; i < lstcolumn.Count; i++)
            {
                tb.Rows.Add(lstcolumn[i].column_name);
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
                sb.Append(row["column_name"].ToString());
            }

            string md5 = Helper.Conv.ToMD5(sb.ToString());
            var path = AppSetting.path + "\\DataGrid\\" + md5;
            return path;
        }



        public void WheelUp()
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

        public void WheelDown()
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

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {

            if (e.Delta > 0)
            {

                WheelUp();

            }
            else
            {
                WheelDown();

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {


                MoveUp();
                return true;
            }
            else if (keyData == Keys.Down)
            {

                MoveDown();
                return true;
            }
            else if (keyData == Keys.Left)
            {

                MoveLeft();
                return true;
            }
            else if (keyData == Keys.Right)
            {

                MoveRight();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        public void MoveUp()
        {

            if (this.cur_cell != null)
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
            else if (this.tb != null && this.tb.Rows.Count != 0)
            {
                cur_cell = new CellObject();
                cur_cell.row_index = 0;
                cur_cell.column_index = 0;
                cur_cell.type = 0;
                this.Refresh();
            }
        }

        public void MoveDown()
        {
            if (this.cur_cell != null)
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
            else if (this.tb != null && this.tb.Rows.Count != 0)
            {
                cur_cell = new CellObject();
                cur_cell.row_index = 0;
                cur_cell.column_index = 0;
                cur_cell.type = 0;
                this.Refresh();
            }
        }

        public void MoveLeft()
        {
            if (this.cur_cell != null)
            {
                int min_left = 0;
                if (this.cur_cell.r.Left > 0)
                {
                    if (this.cur_cell.column_index == 0)
                    {
                        h_value = 0;
                        this.Refresh();
                    }
                    else
                    {
                        var pre_col = lstcolumn[this.cur_cell.column_index - 1];
                        if (this.cur_cell.r.Left - pre_col.width < min_left)
                        {
                            h_value -= min_left - (this.cur_cell.r.Left - pre_col.width);
                            this.Refresh();
                        }
                        else
                        {


                        }
                        foreach (CellObject obj in lstobj)
                        {
                            if (obj.type == 0)
                            {
                                if (obj.row_index == this.cur_cell.row_index)
                                {
                                    if (obj.column_index == this.cur_cell.column_index - 1)
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
                else
                {

                }


            }
        }

        public void MoveRight()
        {
            if (this.cur_cell != null)
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
                                this.cur_cell = obj;
                                Refresh();
                                break;
                            }
                        }
                    }
                }

            }
        }

        private void DataGrid_MouseHover(object sender, EventArgs e)
        {

        }

        int cnt = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
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
                        }

                    }

                    break;
                }

            }
        }

        public void KeyEnter()
        {
            foreach (CellObject obj in lstobj)
            {
                if (obj.type == 0)
                {
                    if (cur_cell != null)
                    {
                        if (obj.row_index == cur_cell.row_index)
                        {
                            if (ClickCell != null)
                            {
                                ClickCell.Invoke(this, lstcolumn[obj.column_index].column_name, tb.Rows[obj.row_index], null);
                            }
                            break;
                        }
                    }



                }



            }
        }



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


        private void DataList_VisibleChanged(object sender, EventArgs e)
        {


        }


    }

}
