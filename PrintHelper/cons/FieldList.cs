using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
namespace PrintHelper.cons
{
    class FieldList:System.Windows.Forms.Control 
    {

        private event EventHandler _ItemClick;
        public event EventHandler ItemClick
        {
            add
            {
                _ItemClick += value;
            }
            remove
            {
                _ItemClick -= value;
            }
        }

        public FieldList()
        {
            this.InitializeComponent();
            //
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.AllPaintingInWmPaint, true);
        }

        private System.Data.DataTable tb = new System.Data.DataTable();
        public System.Data.DataTable DataSource
        {
            get
            {
                return tb;
            }
            set
            {
                tb = value;
                this.Refresh();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FieldList
            // 
            this.SizeChanged += new System.EventHandler(this.FieldList_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FieldList_Paint);
            this.Leave += new System.EventHandler(this.FieldList_Leave);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FieldList_MouseClick);
            this.MouseLeave += new System.EventHandler(this.FieldList_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldList_MouseMove);
            this.ResumeLayout(false);

        }



        private int ComputeWidth(Graphics g)
        {

            int h = 0;
            int w = 0;
            int max_w = 0;  
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                var col=tb.Columns[i];
                if (h + rowHeight*2 > this.Height)
                {
                    h = 0;
                    w += max_w;
                    w += 5;
                }
                else
                {
                    h += rowHeight;
                    int w1 =(int)Math.Ceiling((double) g.MeasureString(col.ColumnName,this.Font).Width);
                    if (max_w < w1)
                    {
                        max_w = w1;
                    }
                }
               
                
            }
            w += max_w;
            return w;

        }

        private int[] ColumnWidth(Graphics g)
        {
            List<int> lst = new List<int>();
            int h = 0;
            int max_w = 0;
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                var col = tb.Columns[i];
                if (h + rowHeight*2 > this.Height)
                {
                    lst.Add(max_w);
                }
                else
                {
                    h += rowHeight;
                    int w1 = (int)Math.Ceiling((double)g.MeasureString(col.ColumnName, this.Font).Width);
                    if (max_w < w1)
                    {
                        max_w = w1;
                    }
                }


            }
            lst.Add(max_w);
            return lst.ToArray();
        }

        int rowHeight = 25;

        private CellObject _cur_obj = null;
        public CellObject cur_obj
        {
            get
            {
                return _cur_obj;
            }
            set
            {
                _cur_obj = value;
                this.Refresh();
            }
        }

        public class CellObject
        {
            public Rectangle r { get; set; }
            public string column_name { get; set; }
        }

        List<CellObject> lstobj = new List<CellObject>();
        private void FieldList_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode == true)
            {

            }
            else
            {
                List<CellObject> lstobj = new List<CellObject>();
                Graphics g = e.Graphics;
                this.Width = ComputeWidth(g);
                //
                int[] ws = ColumnWidth(g);
                int w_index = 0;
                int y = 0;
                int x = 0;
                int h = rowHeight;
                int w = ws[w_index] ;
                for (int i = 0; i < tb.Columns.Count; i++)
                {
                    var col = tb.Columns[i];
                   
                    w = ws[w_index];
                    //
                    CellObject item = new CellObject();
                    item.r = new Rectangle(x, y, w, h);
                    item.column_name = col.ColumnName;
                    lstobj.Add(item);
                    //
                    Point p = this.PointToClient(System.Windows.Forms.Cursor.Position);
                    if (cur_obj != null && cur_obj.column_name == col.ColumnName)
                    {
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(191, 217, 249)), item.r);
                        g.DrawString(item.column_name, this.Font, Brushes.Black, item.r, sf);
                    }
                    else if (item.r.Contains(p) ==  false)
                    {
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        g.DrawString(item.column_name, this.Font, Brushes.Black, item.r, sf);
                    }
                    else
                    {
                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        g.FillRectangle(new SolidBrush(Color.FromArgb(191, 217, 249)), item.r);
                        g.DrawString(item.column_name, this.Font, Brushes.Black, item.r, sf);
                    }
                  
                    //
                    if (y + rowHeight*2 > this.Height)
                    {
                        y = 0;
                        x += w;
                        x += 5;
                        w_index += 1;
                    }
                    else
                    {
                        y += rowHeight;
                    }
                }
                
              
                //
                this.lstobj = lstobj;
                
            }
        }

        private void FieldList_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void FieldList_MouseMove(object sender, MouseEventArgs e)
        {
            this.Refresh();
        }

        private void FieldList_Leave(object sender, EventArgs e)
        {
            
        }

        private void FieldList_MouseLeave(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void FieldList_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    cur_obj = obj;
                    if (_ItemClick != null)
                    {
                        _ItemClick.Invoke(this, new EventArgs());
                    }
                    break;
                }
            }

        }

    }
}
