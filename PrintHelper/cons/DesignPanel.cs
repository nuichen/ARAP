using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
namespace PrintHelper.cons
{
    class DesignPanel:System.Windows.Forms.Control ,IGridable 
    {

        private  event EventHandler dataChange;
        public event EventHandler DataChange
        {
            add
            {
                dataChange += value;
            }
            remove
            {
                dataChange -= value;
            }
        }

        public DesignPanel()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.AllPaintingInWmPaint, true);
            //
            this.InitializeComponent();
            //
            AHeight = 80;
            BHeight = 80;
            CHeight = 30;
            DHeight = 80;
            EHeight = 80;
            //
            this.PageWidth = 1000;
            this.PageHeight = 3000;
            //
            info.font = new Font("宋体", 9);
            info.left = Convert.ToInt16(Helper.Conv.getAnCMInterval() * 0.5F);
            //
            tbstyle.Columns.Add("display", typeof(bool));
            tbstyle.Columns.Add("colname");
            tbstyle.Columns.Add("colbyname");
            tbstyle.Columns.Add("width", typeof(int));
            tbstyle.Columns.Add("align");
            tbstyle.Columns.Add("format");
            tbstyle.Columns.Add("smalltotal", typeof(bool));
        }

        public int Area(System.Windows.Forms.Control con)
        {
            if (Area1(con) == true)
            {
                return 1;
            }
            else if (Area2(con) == true)
            {
                return 2;
            }
            else if (Area3(con) == true)
            {
                return 3;
            }
            else if (Area4(con) == true)
            {
                return 4;
            }
            else if (Area5(con) == true)
            {
                return 5;
            }
            else
            {
                return 5;
            }
        }

        public bool Area1(System.Windows.Forms.Control con)
        {
            Rectangle r = new Rectangle(0, 0, this.Width, AHeight + BarHeight);
            if (con.Top < 0)
            {
                return true;
            }
            else if (r.Contains(con.Left, con.Top) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Area2(System.Windows.Forms.Control con)
        {
            Rectangle r = new Rectangle(0, AHeight + BarHeight, this.Width, BHeight + BarHeight);
            if (r.Contains(con.Left, con.Top) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Area3(System.Windows.Forms.Control con)
        {
            Rectangle r = new Rectangle(0, AHeight + BHeight+2*BarHeight, this.Width, CHeight + BarHeight);
            if (r.Contains(con.Left, con.Top) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Area4(System.Windows.Forms.Control con)
        {
            Rectangle r = new Rectangle(0, AHeight + BHeight +CHeight+ 3 * BarHeight, this.Width, DHeight + BarHeight);
            if (r.Contains(con.Left, con.Top) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Area5(System.Windows.Forms.Control con)
        {
            Rectangle r = new Rectangle(0, AHeight + BHeight + CHeight +DHeight + 4 * BarHeight, this.Width, EHeight + BarHeight);
            if (r.Contains(con.Left, con.Top) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private DataTable tb;
        private PictureBox pictureBox1;
    
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
                    tb = value;
                    this.Refresh();
                }
                else
                {
                    tb = value.Copy();
                    //
                    
                    this.Refresh();
                }
               
            }
        }

        private int BarHeight = 25;

        public int AHeight { get; set; }
        public int BHeight { get; set; }
        public int CHeight { get; set; }
        public int DHeight { get; set; }
        public int EHeight { get; set; }

       
        public int PageWidth { get; set; }
        public int PageHeight { get; set; }

        public int DesignHeight()
        {
            return AHeight + BHeight + CHeight + DHeight + EHeight + BarHeight * 5;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignPanel));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
            // DesignPanel
            // 
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DesignPanel_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DesignPanel_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DesignPanel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DesignPanel_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DesignPanel_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        public class CellObject
        {
            public Rectangle r { get; set; }
            /// <summary>
            /// 101_右边框,201_页眉底部,202_页头底部,203_明细底部,204_页尾底部,205_页脚底部,301_表格内容设计,401_列右边框,402_列
            /// </summary>
            public int obj_type { get; set; }
            public string column_name { get; set; }
        }

        List<CellObject> lstobj = new List<CellObject>();
        private void DesignPanel_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode == false)
            {
                try
                {

                    List<CellObject> lstobj = new List<CellObject>();
                    Graphics g = e.Graphics;
                    //
                    if (1 == 1)
                    {
                        int h = AHeight + BHeight + CHeight + DHeight + EHeight + BarHeight * 5;
                        g.FillRectangle(Brushes.White, new Rectangle(0, 0, PageWidth, h));
                        //
                        CellObject item = new CellObject();
                        item.r = new Rectangle(PageWidth - 5, 0, 5, h);
                        item.obj_type = 101;
                        lstobj.Add(item);
                    }
                    //画bar
                    if (1 == 1)
                    {
                        int top = 0;
                        SolidBrush bru = new SolidBrush(Color.FromArgb(214, 214, 214));
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        CellObject item = new CellObject();
                        //画页眉bar
                        g.FillRectangle(bru, new Rectangle(0, top, PageWidth, BarHeight));
                        g.DrawString("页眉", this.Font, Brushes.Black, new Rectangle(0, top, PageWidth, BarHeight), sf);
                        top += AHeight + BarHeight;
                        item = new CellObject();
                        item.r = new Rectangle(0, top - 5, PageWidth, 5);
                        item.obj_type = 201;
                        lstobj.Add(item);
                        //画页头bar
                        g.FillRectangle(bru, new Rectangle(0, top, PageWidth, BarHeight));
                        g.DrawString("报表头", this.Font, Brushes.Black, new Rectangle(0, top, PageWidth, BarHeight), sf);
                        top += BHeight + BarHeight;
                        item = new CellObject();
                        item.r = new Rectangle(0, top - 5, PageWidth, 5);
                        item.obj_type = 202;
                        lstobj.Add(item);
                        //画明细bar
                        g.FillRectangle(bru, new Rectangle(0, top, PageWidth, BarHeight));
                        g.DrawString("明细网格", this.Font, Brushes.Black, new Rectangle(0, top, PageWidth, BarHeight), sf);
                        
                        g.DrawImage(pictureBox1.Image,
                           new Rectangle(70, top + (BarHeight - pictureBox1.Image.Height) / 2,
                               pictureBox1.Image.Width, pictureBox1.Image.Height));
                        item = new CellObject();
                        item.r = new Rectangle(70, top + (BarHeight - pictureBox1.Image.Height) / 2,
                               pictureBox1.Image.Width, pictureBox1.Image.Height);
                        item.obj_type = 301;
                        lstobj.Add(item);
                        top += CHeight + BarHeight;
                        item = new CellObject();
                        item.r = new Rectangle(0, top - 5, PageWidth, 5);
                        item.obj_type = 203;
                        lstobj.Add(item);
                        //
                       
                        //画页尾bar
                        g.FillRectangle(bru, new Rectangle(0, top, PageWidth, BarHeight));
                        g.DrawString("报表尾", this.Font, Brushes.Black, new Rectangle(0, top, PageWidth, BarHeight), sf);
                        top += DHeight + BarHeight;
                        item = new CellObject();
                        item.r = new Rectangle(0, top - 5, PageWidth, 5);
                        item.obj_type = 204;
                        lstobj.Add(item);
                        //画页脚bar
                        g.FillRectangle(bru, new Rectangle(0, top, PageWidth, BarHeight));
                        g.DrawString("页脚", this.Font, Brushes.Black, new Rectangle(0, top, PageWidth, BarHeight), sf);
                        top += EHeight + BarHeight;
                        item = new CellObject();
                        item.r = new Rectangle(0, top - 5, PageWidth, 5);
                        item.obj_type = 205;
                        lstobj.Add(item);
                    }
                    //画表格
                    if (1 == 1)
                    {
                        int left = info.left;
                        int top = AHeight + BHeight + BarHeight * 3 ;
                        foreach (DataRow row in tbstyle.Rows)
                        {
                            if ((bool)row["display"] == true)
                            {
                                Rectangle rec = new Rectangle(left, top, Convert.ToInt16(row["width"]), CHeight);
                                int align = 1;
                                if (row["align"].ToString() == "左")
                                {
                                    align = 1;
                                }
                                else if (row["align"].ToString() == "中")
                                {
                                    align = 2;
                                }
                                else if (row["align"].ToString() == "右")
                                {
                                    align = 3;
                                }
                                var sf = Helper.Conv.AlignToStringFormat(align);
                                e.Graphics.DrawString(row["colbyname"].ToString(), info.font, new SolidBrush(Color.Black), rec, sf);
                                e.Graphics.DrawRectangle(Pens.Black, rec);
                                var item = new CellObject();
                                //
                                item = new CellObject();
                                item.obj_type = 402;
                                item.r = rec;
                                item.column_name = row["colname"].ToString();
                                lstobj.Add(item);
                                //
                                left += Convert.ToInt16(row["width"]);
                                //
                                item = new CellObject();
                                item.obj_type = 401;
                                item.r = new Rectangle(left - 5, top, 5, CHeight);
                                item.column_name = row["colname"].ToString();
                                lstobj.Add(item);
                            }
                        }
                    }
                    //
                    if (cmd == 402)
                    {
                        int x = 0;
                        int y = AHeight + BHeight + BarHeight * 3;
                        
                        foreach (DataRow row in tbstyle.Rows)
                        {
                            if ((bool)row["display"] == true)
                            {
                               
                                if (row["colname"].ToString() == cur_column.column_name)
                                {
                                    int w = Convert.ToInt16(row["width"]);
                                    int h = CHeight;
                                    var sf = Helper.Conv.AlignToStringFormat(Convert.ToInt16(row["align"]));
                                    Point p = this.PointToClient(System.Windows.Forms.Cursor.Position);
                                    Rectangle rec = new Rectangle(p.X-w/2, p.Y-h/2,w, h);
                                    g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.LightBlue)), rec);
                                    e.Graphics.DrawString(row["colbyname"].ToString(), info.font, new SolidBrush(Color.FromArgb(128, Color.Black)), rec, sf);
                                    break;
                                }
                                
                               
                            }
                           
                        }
                        //
                        x = 0;
                        y = AHeight + BHeight + BarHeight * 3;
                        foreach (DataRow row in tbstyle.Rows)
                        {
                            if ((bool)row["display"] == true)
                            {
                                int w = Convert.ToInt16(row["width"].ToString());
                                Rectangle rec = new Rectangle(x, y, Convert.ToInt16(row["width"]), CHeight);
                               
                                Point p = this.PointToClient(System.Windows.Forms.Cursor.Position);
                                if (rec.X <= p.X && rec.X + rec.Width > p.X)
                                {
                                    if (rec.X + rec.Width / 2 > p.X)
                                    {
                                        g.DrawLine(new Pen(Color.Red, 3), rec.X, rec.Y, rec.X, rec.Y+ CHeight);
                                    }
                                    else
                                    {
                                        g.DrawLine(new Pen(Color.Red, 3), rec.X + w, rec.Y, rec.X + w,rec.Y+ CHeight);
                                    }

                                }
                                

                                x += w;
                            }

                        }
                    }
                    //
                    
                    this.lstobj = Sort(lstobj);
            
                }
                catch (Exception ex)
                {
                    e.Graphics.DrawString(ex.ToString(), this.Font, Brushes.Black, new Rectangle(0, 0, this.Width, this.Height));
                }

            }
           

        }

        private CellObject cur_column = null;
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
                tb.Rows.Add(obj.obj_type, obj, row_id);
            }
            tb.DefaultView.Sort = "type asc,row_id asc";
            tb = tb.DefaultView.ToTable();
            foreach (DataRow row in tb.Rows)
            {
                lst.Add((CellObject)row["obj"]);
            }
            tb.Clear();
            tb = null;
            return lst;
        }

        private int size_change = 0;

        private void DesignPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (cmd == 0)
            {
                int flag = 0;
                foreach (CellObject obj in lstobj)
                {
                    if (obj.r.Contains(e.X, e.Y))
                    {
                        if (obj.obj_type == 101)
                        {
                            this.Cursor = Cursors.SizeWE;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 201)
                        {
                            this.Cursor = Cursors.SizeNS;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 202)
                        {
                            this.Cursor = Cursors.SizeNS;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 203)
                        {
                            this.Cursor = Cursors.SizeNS;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 204)
                        {
                            this.Cursor = Cursors.SizeNS;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 205)
                        {
                            this.Cursor = Cursors.SizeNS;
                            reject_select = 1;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 301)
                        {
                            this.Cursor = Cursors.Hand;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 401)
                        {
                           
                            this.Cursor = Cursors.SizeWE;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 402)
                        {
                            this.Cursor = Cursors.Default;
                            reject_select = 1;
                            flag = 1;
                        }
                        break;
                    }

                }
                //
                if (flag == 1)
                {

                }
                else
                {
                    if (this.Cursor == Cursors.SizeWE)
                    {
                        this.Cursor = Cursors.Default;
                    }
                    else if (this.Cursor == Cursors.SizeNS)
                    {
                        this.Cursor = Cursors.Default;
                    }
                    else if (this.Cursor == Cursors.Hand)
                    {
                        this.Cursor = Cursors.Default;
                    }
                    reject_select = 0;
                }
            }
            else if (cmd == 101)
            {
                int offset_x = e.X - p_down.X;
                if (offset_x != 0)
                {
                    this.PageWidth += e.X - p_down.X;
                    p_down = new Point(e.X, e.Y);
                    this.Refresh();
                    size_change = 1;
                }
               
            }
            else if (cmd == 201)
            {
                int offset_y = e.Y - p_down.Y;
                if (this.AHeight + offset_y < 5)
                {
                    return;
                }
                if (offset_y < 0)
                {
                    Rectangle r = new Rectangle(0, 0, PageWidth, AHeight + BarHeight);
                    foreach (Control con in this.Controls)
                    {
                        if (r.Contains(con.Location))
                        {
                            if (r.Top + r.Height < con.Top + con.Height)
                            {
                                return;
                            }
                        }
                    }
                }

                if (offset_y != 0)
                {
                    List<Control> lst = new List<Control>();
                    int top = AHeight + 1 * BarHeight;
                    foreach (Control con in this.Controls)
                    {
                        if (con.Top > top)
                        {
                            lst.Add(con);
                        }
                    }
                    this.AHeight += offset_y;
                    foreach (Control con in lst)
                    {
                        con.Top += offset_y;
                    }
                    p_down = new Point(e.X, e.Y);
                    this.Refresh();
                    size_change = 1;
                }
               
            }
            else if (cmd == 202)
            {
                int offset_y = e.Y - p_down.Y;
                if (this.BHeight + offset_y < 5)
                {
                    return;
                }
                if (offset_y < 0)
                {
                    Rectangle r = new Rectangle(0, 0, PageWidth, AHeight +BHeight  + 2*BarHeight);
                    foreach (Control con in this.Controls)
                    {
                        if (r.Contains(con.Location))
                        {
                            if (r.Top + r.Height < con.Top + con.Height)
                            {
                                return;
                            }
                        }
                    }
                }
                if (offset_y != 0)
                {
                    List<Control> lst = new List<Control>();
                    int top = AHeight + BHeight + 2 * BarHeight;
                    foreach (Control con in this.Controls)
                    {
                        if (con.Top > top)
                        {
                            lst.Add(con);
                        }
                    }
                    this.BHeight += offset_y;
                    foreach (Control con in lst)
                    {
                        con.Top += offset_y;
                    }
                    p_down = new Point(e.X, e.Y);
                    this.Refresh();
                    size_change = 1;
                }
               
            }
            else if (cmd == 203)
            {
                int offset_y = e.Y - p_down.Y;
                if (this.CHeight + offset_y < 15)
                {
                    return;
                }
                if (this.CHeight + offset_y > 100)
                {
                    return;
                }
                if (offset_y < 0)
                {
                    Rectangle r = new Rectangle(0, 0, PageWidth, AHeight + BHeight +CHeight + 3 * BarHeight);
                    foreach (Control con in this.Controls)
                    {
                        if (r.Contains(con.Location))
                        {
                            if (r.Top + r.Height < con.Top + con.Height)
                            {
                                return;
                            }
                        }
                    }
                }
                if (offset_y != 0)
                {
                    List<Control> lst = new List<Control>();
                    int top = AHeight + BHeight + CHeight + 3 * BarHeight;
                    foreach (Control con in this.Controls)
                    {
                        if (con.Top > top)
                        {
                            lst.Add(con);
                        }
                    }
                    this.CHeight += offset_y;
                    foreach (Control con in lst)
                    {
                        con.Top += offset_y;
                    }
                    p_down = new Point(e.X, e.Y);
                    this.Refresh();
                    size_change = 1;
                }
              
            }
            else if (cmd == 204)
            {
                int offset_y = e.Y - p_down.Y;
                if (this.DHeight + offset_y < 5)
                {
                    return;
                }
                if (offset_y < 0)
                {
                    Rectangle r = new Rectangle(0, 0, PageWidth, AHeight + BHeight +CHeight+DHeight + 4 * BarHeight);
                    foreach (Control con in this.Controls)
                    {
                        if (r.Contains(con.Location))
                        {
                            if (r.Top + r.Height < con.Top + con.Height)
                            {
                                return;
                            }
                        }
                    }
                }
                if (offset_y != 0)
                {
                    List<Control> lst = new List<Control>();
                    int top = AHeight + BHeight + CHeight + DHeight + 4 * BarHeight;
                    foreach (Control con in this.Controls)
                    {
                        if (con.Top > top)
                        {
                            lst.Add(con);
                        }
                    }
                    this.DHeight += offset_y;
                    foreach (Control con in lst)
                    {
                        con.Top += offset_y;
                    }
                    p_down = new Point(e.X, e.Y);
                    this.Refresh();
                    size_change = 1;
                }
                
            }
            else if (cmd == 205)
            {
               int offset_y= e.Y - p_down.Y;
                if (this.EHeight + offset_y < 5)
                {
                    return;
                }
                if (offset_y < 0)
                {
                    Rectangle r = new Rectangle(0, 0, PageWidth, AHeight + BHeight + CHeight + DHeight+EHeight + 5 * BarHeight);
                    foreach (Control con in this.Controls)
                    {
                        if (r.Contains(con.Location))
                        {
                            if (r.Top + r.Height < con.Top + con.Height)
                            {
                                return;
                            }
                        }
                    }
                }
                if (offset_y != 0)
                {
                    this.EHeight += offset_y;
                    p_down = new Point(e.X, e.Y);
                    this.Refresh();
                    size_change = 1;
                }
               
            }
            else if (cmd == 401)
            {
                int offset_x =  e.X - p_down.X;
                int flag = 0;
                if (offset_x != 0)
                {
                    foreach (DataRow row in tbstyle.Rows)
                    {
                        if (row["colname"].ToString() == cur_column.column_name)
                        {
                            int w1 = Convert.ToInt16(row["width"].ToString());
                            int w2 =w1+ offset_x;
                            if (w2 < 5)
                            {
                                if (w1 == 5)
                                {

                                }
                                else
                                {
                                    row["width"] = 5;
                                    flag = 1;
                                }
                            }
                            else if (w2 > 500)
                            {
                                if (w1 == 500)
                                {

                                }
                                else
                                {
                                    row["width"] = 500;
                                    flag = 1;
                                }
                            }
                            else
                            {
                                row["width"] = w2;
                                flag = 1;
                            }
                           
                            break;
                        }
                    }
                }
               
                if (flag==1)
                {
                    p_down = new Point(e.X, e.Y);
                    this.Refresh();
                    size_change = 1;
                }
               
            }
            else if (cmd == 402.1M)
            {
                int offset_x =Math.Abs( e.X - p_down.X);
                int offset_y =Math.Abs( e.Y - p_down.Y);
                if (offset_x > 5 || offset_y >5)
                {
                    cmd = 402;
                    
                    reject_select = 1;
                  
                }
                this.Refresh();
            }
            else if (cmd == 402)
            {

                this.Refresh();
            }
            

         
        }

        /// <summary>
        /// 101_拉页宽带,201_拉页眉,202_拉页头,203_拉明细,204_拉页尾,205_拉页脚,401_拉列宽,402.1_改列位置预期,402_改列位置
        /// </summary>
        public decimal  cmd = 0;
        public int reject_select = 0;
        Point p_down = new Point(-1, -1);
        private void DesignPanel_MouseDown(object sender, MouseEventArgs e)
        {
            int flag = 0;
            if (e.Button == MouseButtons.Left)
            {
                foreach (CellObject obj in lstobj)
                {
                    if (obj.r.Contains(e.X, e.Y))
                    {
                        if (obj.obj_type == 101)
                        {
                            this.Cursor = Cursors.SizeWE;
                            cmd = 101;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 201)
                        {
                            this.Cursor = Cursors.SizeNS;
                            cmd = 201;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 202)
                        {
                            this.Cursor = Cursors.SizeNS;
                            cmd = 202;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 203)
                        {
                            this.Cursor = Cursors.SizeNS;
                            cmd = 203;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 204)
                        {
                            this.Cursor = Cursors.SizeNS;
                            cmd = 204;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 205)
                        {
                            this.Cursor = Cursors.SizeNS;
                            cmd = 205;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 401)
                        {
                            this.Cursor = Cursors.SizeWE;
                            cmd = 401;
                            cur_column = obj;
                            reject_select = 1;
                            flag = 1;
                        }
                        else if (obj.obj_type == 402)
                        {
                           
                            cmd = 402.1M;
                            cur_column = obj;
                            reject_select = 1;
                            flag = 1;
                        }
                        break;
                    }
                }
                p_down = new Point(e.X, e.Y);
            }
        }

        private DataRow Copy(DataRow row )
        {
            var r = row.Table.NewRow();
            r.ItemArray = row.ItemArray;
            return r;
        }

        private void DesignPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (cmd == 101)
            {
                if (size_change == 1 && dataChange != null)
                {
                    dataChange.Invoke(this, new EventArgs());
                }
            }
            else if (cmd == 201)
            {
                if (size_change == 1 && dataChange != null)
                {
                    dataChange.Invoke(this, new EventArgs());
                }
            }
            else if (cmd == 202)
            {
                if (size_change == 1 && dataChange != null)
                {
                    dataChange.Invoke(this, new EventArgs());
                }
            }
            else if (cmd == 203)
            {
                if (size_change == 1 && dataChange != null)
                {
                    dataChange.Invoke(this, new EventArgs());
                }
            }
            else if (cmd == 204)
            {
                if (size_change == 1 && dataChange != null)
                {
                    dataChange.Invoke(this, new EventArgs());
                }
            }
            else if (cmd == 205)
            {
                if (size_change == 1 && dataChange != null)
                {
                    dataChange.Invoke(this, new EventArgs());
                }
            }
            else if (cmd == 401)
            {
                if (size_change == 1 && dataChange != null)
                {
                    dataChange.Invoke(this, new EventArgs());
                }
            }
            else if (cmd == 402)
            {
                
                int x = 0;
                int y = AHeight + BHeight + BarHeight * 3;
                int flag = 0;
                DataRow cur_row = null;
                foreach (DataRow row in tbstyle.Rows)
                {
                    if (row["colname"].ToString() == cur_column.column_name)
                    {
                        cur_row = row;
                        break;
                    }
                }
                foreach (DataRow row in tbstyle.Rows)
                {
                    if ((bool)row["display"] == true)
                    {
                        int w = Convert.ToInt16(row["width"].ToString());
                        Rectangle rec = new Rectangle(x, y, Convert.ToInt16(row["width"]), CHeight);

                        Point p = this.PointToClient(System.Windows.Forms.Cursor.Position);
                        if (rec.X <= p.X && rec.X + rec.Width > p.X)
                        {
                            if (rec.X + rec.Width / 2 > p.X)
                            {
                                int index2 = tbstyle.Rows.IndexOf(cur_row);
                                int index = tbstyle.Rows.IndexOf(row);
                               
                                if (index2 == index)
                                {
                                    
                                }
                                else if (index2 < index)
                                {
                                    var r = Copy(cur_row);
                                    tbstyle.Rows.Remove(cur_row);
                                    tbstyle.Rows.InsertAt(r, index-1);
                                    flag = 1;
                                }
                                else
                                {
                                    var r = Copy(cur_row);
                                    tbstyle.Rows.Remove(cur_row);
                                    tbstyle.Rows.InsertAt(r, index);
                                    flag = 1;
                                }
                                
                               
                                break;
                            }
                            else
                            {
                                int index2 = tbstyle.Rows.IndexOf(cur_row);
                                int index = tbstyle.Rows.IndexOf(row);

                                if (index2 == index)
                                {
                                    
                                }
                                else if (index2 < index)
                                {
                                  
                                    var r = Copy(cur_row);
                                    tbstyle.Rows.Remove(cur_row);
                                    tbstyle.Rows.InsertAt(r, index );
                                    flag = 1;
                                }
                                else
                                {
                                     
                                    var r = Copy(cur_row);
                                    tbstyle.Rows.Remove(cur_row);
                                    tbstyle.Rows.InsertAt(r, index+1);
                                    flag = 1;
                                }

                                break;
                            }

                        }


                        x += w;
                    }

                }
                if (flag ==1 && dataChange != null)
                {
                    dataChange.Invoke(this, new EventArgs());
                }
            }

            size_change = 0;
            reject_select = 0;
            cmd = 0;
            this.Refresh();
        }

        private Font gridFont = new Font("宋体", 9);
        private Font GridFont
        {
            get
            {
                return gridFont;
            }
            set
            {
                gridFont = value;
            }
        }

       private  GridStyleInfo info = new GridStyleInfo();
        private System.Data.DataTable tbstyle = new DataTable();

        public string GetXML()
        {
            Helper.StringBuilderForXML sb = new Helper.StringBuilderForXML();
            sb.Append("Width", this.PageWidth.ToString());
            sb.Append("Height", this.PageHeight.ToString());
            sb.Append("BarHeight", this.BarHeight.ToString());
            sb.Append("AHeight", this.AHeight.ToString());
            sb.Append("BHeight", this.BHeight.ToString());
            sb.Append("CHeight", this.CHeight.ToString());
            sb.Append("DHeight", this.DHeight.ToString());
            sb.Append("EHeight", this.EHeight.ToString());
            sb.Append("GridLeft", this.info.left.ToString());
            if (tb == null)
            {
                sb.Append("ExistGrid", "0");
            }
            else
            {
                sb.Append("ExistGrid", "1");
            }

            sb.Append("AppendEmptyRow", info.AppendEmptyRow.ToString());
            FontConverter fc = new FontConverter();
            sb.Append("GridFont", fc.ConvertToInvariantString(info.font));
            //
            foreach (DataRow row in tbstyle.Rows)
            {
                Helper.StringBuilderForXML sb3 = new Helper.StringBuilderForXML();
                if ((bool)row["display"] == false)
                {
                    sb3.Append("Display", "0");
                }
                else
                {
                    sb3.Append("Display", "1");
                }
                sb3.Append("ColName", row["colname"].ToString());
                sb3.Append("ColByname", row["colbyname"].ToString());
                sb3.Append("Width", row["width"].ToString());
                if (row["align"].ToString() == "左")
                {
                    sb3.Append("Align", "1");
                }
                else if (row["align"].ToString() == "中")
                {
                    sb3.Append("Align", "2");
                }
                else if (row["align"].ToString() == "右")
                {
                    sb3.Append("Align", "3");
                }
                
                sb3.Append("Format", row["format"].ToString());
                if ((bool)row["smalltotal"] == false)
                {
                    sb3.Append("SmallTotal", "0");
                }
                else
                {
                    sb3.Append("SmallTotal", "1");
                }
                string str3 = sb3.ToString();
                sb3.Clear();
                sb3.Append("Column", str3);
                //
                sb.Append(sb3.ToString());
            }
            //
            string str = sb.ToString();
            sb.Clear();
            sb.Append("Page", str);
            return sb.ToString();
        }

        public void SetXML(string value)
        {
            Helper.ReadXml r = new Helper.ReadXml("<xml>" + value + "</xml>");

            FontConverter fc = new FontConverter();
            info.font = (Font)fc.ConvertFromString(r.Read("GridFont"));
            this.BarHeight = Convert.ToInt16(r.Read("BarHeight"));
            this.AHeight = Convert.ToInt16(r.Read("AHeight"));
            this.BHeight = Convert.ToInt16(r.Read("BHeight"));
            this.CHeight = Convert.ToInt16(r.Read("CHeight"));
            this.DHeight = Convert.ToInt16(r.Read("DHeight"));
            this.EHeight = Convert.ToInt16(r.Read("EHeight"));
            this.PageWidth = Convert.ToInt16(r.Read("Width"));
            this.PageHeight = Convert.ToInt16(r.Read("Height"));
            info.left = Convert.ToInt16(r.Read("GridLeft"));
            info.AppendEmptyRow = Helper.Conv.ToInt16(r.Read("AppendEmptyRow"));
            //

            tbstyle.Rows.Clear();

            foreach (Helper.ReadXml r2 in r.ReadList("Column"))
            {
                var row = tbstyle.NewRow();
                tbstyle.Rows.Add(row);
                if (r2.Read("Display") == "0")
                {
                    row["display"] = false;
                }
                else
                {
                    row["display"] = true;
                }
                row["colname"] = r2.Read("ColName");
                row["colbyname"] = r2.Read("ColByname");

                row["width"] = Convert.ToInt16(r2.Read("Width"));
                if (r2.Read("Align") == "1")
                {
                    row["align"] = "左";
                }
                else if (r2.Read("Align") == "2")
                {
                    row["align"] = "中";
                }
                else if (r2.Read("Align") == "3")
                {
                    row["align"] = "右";
                }
                row["format"] = r2.Read("Format");
                if (r2.Read("SmallTotal") == "0")
                {
                    row["SmallTotal"] = false;
                }
                else
                {
                    row["SmallTotal"] = true;
                }
            }
            //
            this.Refresh();
        }

   

        bool IGridable.EditGrid(string[] fields)
        {
            var tb = new DataTable();
            IEditGrid edit = new EditGrid();
            foreach (string field in fields)
            {
                int flag = 0;
                foreach (DataRow row in tbstyle.Rows)
                {
                    if (row["colname"].ToString() == field)
                    {
                        flag = 1;
                    }
                }
                //
                if (flag == 0)
                {
                    DataRow r = tbstyle.NewRow();
                    tbstyle.Rows.Add(r);
                    r["display"] = false;
                    r["colname"] = field;
                    r["colbyname"] = field;
                    r["width"] = 100;
                    r["align"] = "左";
                    r["format"] = "";
                    r["smalltotal"] = false;
                }

            }
            //
            List<DataRow> lstrow = new List<DataRow>();
            foreach (DataRow row in tbstyle.Rows)
            {
                string colname = row["colname"].ToString();
                int flag = 0;
                foreach (string field in fields)
                {
                    if (colname == field)
                    {
                        flag = 1;
                    }
                }
                //
                if (flag == 0)
                {
                    lstrow.Add(row);
                }
            }
            foreach (DataRow row in lstrow)
            {
                tbstyle.Rows.Remove(row);
            }
            //
            GridStyleInfo info2;
            if (edit.Edit(tbstyle,info, out tb,out info2) == true)
            {
                 
                tbstyle = tb;
                info = info2;
                this.Refresh();
                return true;
            }
            else
            {
                return false;
            }
        }


         

        private void DesignPanel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y))
                {
                    if (obj.obj_type == 301)
                    {
                        cons.IGridable gridable = this;
                        List<string> lst = new List<string>();
                         
                        foreach (System.Data.DataColumn col in tb.Columns)
                        {
                            lst.Add(col.ColumnName);
                        }

                        if (gridable.EditGrid(lst.ToArray()) == true)
                        {
                            if (this.dataChange != null)
                            {
                                this.dataChange.Invoke(this, new EventArgs());
                            }
                        }
                        break;
                    }
                }
            }
        }

    }
}
