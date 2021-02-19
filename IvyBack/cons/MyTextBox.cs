using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
namespace IvyBack.cons
{
    class MyTextBox : System.Windows.Forms.TextBox
    {
        public DataList dataList;
        public MyTextBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.AllPaintingInWmPaint, true);
            //

            this.InitializeComponent();
            //
            this.GotFocus += gotFocus;
            //
            dataList = new DataList();

            dataList.BackColor = Color.White;
            dataList.ClickCell += ClickCell;
            dataList.MergeCell = false;
            //
            this.MouseWheel += my_MouseWheel;
            //
            this.Cursor = System.Windows.Forms.Cursors.Default;

        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

        private void my_MouseWheel(object sender, MouseEventArgs e)
        {

            if (e.Delta > 0)
            {
                if (tb != null)
                {
                    dataList.WheelUp();
                }

            }
            else
            {
                if (tb != null)
                {
                    dataList.WheelDown();

                }

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                if (tb != null)
                {
                    dataList.MoveUp();
                }
                return true;
            }
            else if (keyData == Keys.Down)
            {
                if (tb != null)
                {
                    dataList.MoveDown();
                }

                return true;
            }
            else if (keyData == Keys.Escape)
            {
                this.dataList.Visible = false;
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void ClickCell(object sender, string column_name, DataRow row, MouseEventArgs e)
        {

            foreach (BindData obj in lstBindData)
            {
                string value = "";
                var arr = obj.column_name.Split('/');
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
                var info = this.GetType().GetProperty(obj.property_name);
                if (info != null)
                {
                    info.SetValue(this, value, null);
                }
            }
            dataList.Visible = false;
            //
            this.SelectionStart = this.Text.Length;
            //
            this.Refresh();
            ClickCellAfter?.Invoke(sender, column_name, row, e);
        }

        private void gotFocus(object sender, EventArgs e)
        {

        }

        public void GetDefaultValue()
        {
            var tb = dataList.DataSource;

            foreach (BindData obj in lstBindData)
            {
                string value = "";
                var arr = obj.column_name.Split('/');
                for (int i = 0; i < arr.Length; i++)
                {
                    string col_name = arr[i];
                    if (i == 0)
                    {
                        if (tb.Columns.Contains(col_name) == true)
                        {
                            value += tb.Rows[0][col_name].ToString();
                        }

                    }
                    else
                    {
                        value += "/";
                        if (tb.Columns.Contains(col_name) == true)
                        {
                            value += tb.Rows[0][col_name].ToString();
                        }
                    }
                }
                var info = this.GetType().GetProperty(obj.property_name);
                if (info != null)
                {
                    if ("/".Equals(value)) value = "";
                    info.SetValue(this, value, null);
                }
            }
        }

        public void GetValue(string key_field, string str)
        {
            if (tb != null)
            {
                string keyword = str.Split('/')[0];
                var tb2 = tb.Copy();


                if (tb.Columns.Contains(key_field) == true)
                {
                    tb2.DefaultView.RowFilter = "isnull(" + key_field + ",'')='" + keyword + "'";
                    tb2 = tb2.DefaultView.ToTable();
                    if (tb2.Rows.Count == 0)
                    {
                        this.Text = "";
                    }
                    else
                    {
                        var row = tb2.Rows[0];
                        foreach (BindData obj in lstBindData)
                        {
                            string value = "";
                            var arr = obj.column_name.Split('/');
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
                            var info = this.GetType().GetProperty(obj.property_name);
                            if (info != null)
                            {
                                info.SetValue(this, value, null);
                            }
                        }
                    }
                }

            }
            this.dataList.Visible = false;
        }

        private DataTable tb;
        private string key_field;
        private string fields;
        public void Bind(DataTable tb, int width, int height, string key_field, string fields, string tran_string)
        {
            this.dataList.ClearColumn();
            this.key_field = key_field;
            this.fields = fields;
            this.tb = tb;
            foreach (string f in fields.Split(','))
            {
                var arr = f.Split(':');
                dataList.AddColumn(arr[0], arr[1], "", Helper.Conv.ToInt16(arr[2]), 1, "");
            }
            dataList.Width = width;
            dataList.Height = height;
            dataList.DataSource = tb.Copy();
            foreach (string str in tran_string.Split(','))
            {
                var arr = str.Split(new char[] { '-', '>' }, StringSplitOptions.RemoveEmptyEntries);
                Bind(arr[0], arr[1]);
            }

        }

        private class BindData
        {
            public string column_name { get; set; }
            public string property_name { get; set; }
        }

        List<BindData> lstBindData = new List<BindData>();
        private MyTextBox Bind(string column_name, string property_name)
        {
            lstBindData.Clear();
            BindData item = new BindData();
            item.column_name = column_name;
            item.property_name = property_name;
            lstBindData.Add(item);
            return this;
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
            if (tb != null)
            {
                if (this.FindForm() != null)
                {
                    var frm = this.FindForm();
                    Point p = getPoint();
                    int flag = 0;
                    if (flag == 0)
                    {
                        int x = p.X;
                        int y = p.Y + this.Height + 1;
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
                        int x = p.X + this.Width - dataList.Width;
                        int y = p.Y + this.Height + 1;
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
                        int x = p.X;
                        int y = p.Y - dataList.Height;
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
                        int x = p.X + this.Width - dataList.Width;
                        int y = p.Y - dataList.Height;
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

                        dataList.Left = p.X;
                        dataList.Top = p.Y + this.Height;

                    }

                    //

                    dataList.Visible = true;
                    frm.Controls.Add(dataList);
                    dataList.BringToFront();
                    //


                }
            }
        }


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyTextBox
            // 
            this.BorderStyleChanged += new System.EventHandler(this.MyTextBox_BorderStyleChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MyTextBox_MouseClick);
            this.TextChanged += new System.EventHandler(this.MyTextBox_TextChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MyTextBox_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MyTextBox_KeyPress);
            this.Leave += new System.EventHandler(this.MyTextBox_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyTextBox_MouseDown);
            this.ResumeLayout(false);

        }

        const int WM_PAINT = 0xF;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                if (this.IsDisposed == true)
                {
                    return;
                }

                try
                {
                    using (Graphics g = this.CreateGraphics())
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.DrawLine(Pens.Gray, 0, this.Height - 1, this.Width, this.Height - 1);

                        if (tb != null)
                        {
                            if (this.BorderStyle == BorderStyle.None)
                            {
                                int s = Convert.ToInt16(this.Height * 0.6M);
                                Color c = Color.Gray;
                                g.FillPolygon(new SolidBrush(c), new Point[] { new Point(this.Width -s/2-5,s+(this.Height-s)/2-3-2),
                            new Point(this.Width-s-5,(this.Height-s)/2),
                            new Point(this.Width-5,(this.Height-s)/2)});
                            }
                            else if (this.BorderStyle == BorderStyle.Fixed3D)
                            {
                                int s = this.Height / 2;
                                Color c = Color.Gray;
                                g.FillPolygon(new SolidBrush(c), new Point[] { new Point(this.Width -s/2-5,s+(this.Height-s)/2-3-2),
                            new Point(this.Width-s-5,(this.Height-s)/2),
                            new Point(this.Width-5,(this.Height-s)/2)});
                            }

                        }

                    }
                }
                catch (Exception)
                {

                }

            }
        }

        private void MyTextBox_BorderStyleChanged(object sender, EventArgs e)
        {

        }

        private void MyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tb != null)
                {
                    dataList.KeyEnter();
                }
                SendKeys.SendWait("{Tab}");
            }

        }

        private void MyTextBox_TextChanged(object sender, EventArgs e)
        {

            if (this.Focused == true)
            {
                if (tb != null)
                {
                    if (this.Text.Trim().Contains("/") == true)
                    {
                        string keyword = this.Text.Trim().Split('/')[0];
                        var tb2 = tb.Copy();
                        if (lstBindData.Count != 0)
                        {
                            var arr = lstBindData[0].column_name.Split('/');
                            string key_field = arr[0];
                            tb2.DefaultView.RowFilter = "isnull(" + key_field + ",'')='" + keyword + "'";
                            tb2 = tb2.DefaultView.ToTable();
                            this.dataList.DataSource = tb2;
                            this.ShowDataList();
                        }
                    }
                    else
                    {
                        string keyword = this.Text.Trim().Split('/')[0];
                        var tb2 = tb.Copy();
                        StringBuilder sb = new StringBuilder();
                        foreach (string f in fields.Split(','))
                        {
                            var arr = f.Split(':');
                            if (tb2.Columns.Contains(arr[0]) == true)
                            {
                                var col = tb2.Columns[arr[0]];
                                if (col.DataType == typeof(string))
                                {
                                    if (sb.Length == 0)
                                    {
                                        sb.Append("isnull(" + col.ColumnName + ",'')" + " like '%" + keyword + "%'");
                                    }
                                    else
                                    {
                                        sb.Append(" or ");
                                        sb.Append("isnull(" + col.ColumnName + ",'')" + " like '%" + keyword + "%'");
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
            else
            {
                this.dataList.Visible = false;
            }

            this.Refresh();
        }

        private void MyTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (tb != null)
            {
                int s = this.Height;
                Rectangle r = new Rectangle(this.Width - s, 0, s, s);
                if (r.Contains(e.X, e.Y) == true)
                {

                    this.dataList.DataSource = tb;
                    this.ShowDataList();
                }
                this.Refresh();
            }


        }

        private void MyTextBox_Leave(object sender, EventArgs e)
        {
            if (tb != null)
            {
                string keyword = this.Text.Trim().Split('/')[0];
                var tb2 = tb.Copy();


                if (tb.Columns.Contains(key_field) == true)
                {
                    tb2.DefaultView.RowFilter = "isnull(" + key_field + ",'')='" + keyword + "'";
                    tb2 = tb2.DefaultView.ToTable();
                    if (tb2.Rows.Count == 0)
                    {
                        this.Text = "";
                    }
                    else
                    {
                        var row = tb2.Rows[0];
                        foreach (BindData obj in lstBindData)
                        {
                            string value = "";
                            var arr = obj.column_name.Split('/');
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
                            var info = this.GetType().GetProperty(obj.property_name);
                            if (info != null)
                            {
                                info.SetValue(this, value, null);
                            }
                        }
                    }
                }

            }
            this.dataList.Visible = false;
        }

        private void MyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
            }
        }

        private void MyTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            this.Refresh();
        }

        public delegate void ClickCellHandler(object sender, string column_name, DataRow row, MouseEventArgs e);

        public event ClickCellHandler ClickCellAfter;
    }
}
