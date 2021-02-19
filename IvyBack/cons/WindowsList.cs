using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using IvyBack.cons;
using IvyBack.Helper;
using IvyBack.NavForm;

namespace IvyBack.cons
{
    public class WindowsList : System.Windows.Forms.Control
    {
        private Timer timer1;
        private IContainer components;
        List<Form> lst = new List<Form>();
        private class CellObject
        {
            public Rectangle r { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int type { get; set; }
            public object data { get; set; }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WindowsList
            // 
            this.SizeChanged += new System.EventHandler(this.WindowsList_SizeChanged);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.WindowsList_ControlAdded);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.WindowsList_ControlRemoved);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WindowsList_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WindowsList_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WindowsList_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WindowsList_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WindowsList_MouseUp);
            this.ResumeLayout(false);

        }

        public void SortKey(Keys key)
        {
            if (key == Keys.Escape)
            {

                var form = (Form)this.Controls[0];
                if (form == lst[0])
                {

                }
                else
                {
                    lst[0].SendToBack();
                    form.Close();

                }

            }
        }

        public WindowsList()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //
            this.InitializeComponent();
            //
            this.TabStop = false;
        }

        private bool CanAdd(Form frm)
        {
            int w = 100;
            Graphics g = this.CreateGraphics();
            for (int i = 1; i < lst.Count; i++)
            {
                w += 15;
                var form = lst[i];
                string header = form.Text;
                int w2 = (int)g.MeasureString(header, this.headerFont).Width + 2 + 5 + 20;
                w += w2;
            }
            if (1 == 1)
            {
                string header = frm.Text;
                int w2 = (int)g.MeasureString(header, this.headerFont).Width + 2 + 5 + 20;
                if (w + w2 > this.Width)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        public void ShowForm(Form form)
        {
            if (this.Controls.Contains(form) == false)
            {
                if (CanAdd(form) == true)
                {
                    form.Dock = DockStyle.None;
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Show();
                    this.Controls.Add(form);
                    form.Left = 0;
                    form.Top = headerHeight;
                    form.Width = this.Width;
                    form.Height = this.Height - headerHeight;

                    form.BringToFront();
                    lst.Add(form);
                }
                else
                {
                    MsgForm.ShowFrom("当前窗口过多，请关闭其他窗口！");

                }

            }
            else
            {
                if (this.Controls[0] != form)
                {
                    form.BringToFront();
                }

            }
            this.Refresh();
        }
        public void ShowLoad(Form par_frm)
        {
            Action act = delegate()
            {
                MainForm.frmLoad.LoadWait(par_frm);
                Application.DoEvents();
            };
            Helper.GlobalData.mainFrm.Invoke(act);
        }
        public void CloseLoad(Form par_frm)
        {
            Action act = delegate()
            {
                MainForm.frmLoad.LoadClose(par_frm);
            };
            Helper.GlobalData.mainFrm.Invoke(act);
        }
        public void Flush()
        {
            Action act = delegate()
            {
                MainForm.frmLoad.Flush();
            };
            Helper.GlobalData.mainFrm.Invoke(act);
        }

        /// <summary>
        /// 标签页起始绘制 X 位置
        /// </summary>
        private const int TabStartXPosition = 154;

        List<CellObject> lstobj = new List<CellObject>();
        private void WindowsList_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (DesignMode == true)
                {
                    Graphics g = e.Graphics;
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    if (1 == 1)
                    {
                        Pen p = new Pen(Color.Gray);
                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        g.DrawLine(p, 0, this.headerHeight, this.Width, this.headerHeight);
                        //
                        Rectangle r = new Rectangle(0, 0, this.Width, this.headerHeight);
                        g.DrawString("导航条", this.headerFont, new SolidBrush(this.ForeColor), r, sf);
                    }
                    if (1 == 1)
                    {
                        Rectangle r = new Rectangle(0, this.headerHeight, this.Width, this.Height - this.headerHeight);
                        g.DrawString("窗体容器", this.Font, new SolidBrush(this.ForeColor), r, sf);
                    }
                }
                else
                {
                    List<CellObject> lstobj = new List<CellObject>();
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    //
                    LinearGradientBrush bru = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), GlobalData.top_main_nav1,
                       GlobalData.top_main_nav2, LinearGradientMode.Vertical);
                    g.DrawImage(ResourcesHelper.GetImage("导航条"), new Rectangle(0, 0, this.Width, this.headerHeight));
                    //
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    int x = 120;
                    int y = 0;
                    int w = 0;
                    int h = this.headerHeight;

                    //画主页按钮
                    if (1 == 1)
                    {
                        if (lst.Count < 1) return;
                        var form = lst[0];
                        string header = form.Text;

                        Image home_img_bg = ResourcesHelper.GetImage("首页图标背景");

                        g.DrawImage(home_img_bg,
                            new Rectangle(0, 0,
                                home_img_bg.Width,
                                home_img_bg.Height));


                        Image home_img = ResourcesHelper.GetImage("工具栏_首页按钮");
                        Rectangle home = new Rectangle(
                            0,
                            (h - 5 - home_img.Height),
                            x,
                            home_img.Height
                            );
                        g.DrawImage(home_img,
                            new Rectangle((x - home_img.Width) / 2, home.Y,
                                home_img.Width,
                                home_img.Height));

                        CellObject obj1 = new CellObject();
                        obj1.r = home;
                        obj1.type = 1;
                        obj1.data = form;
                        lstobj.Add(obj1);
                    }

                    x = TabStartXPosition;

                    Rectangle move_r_all = new Rectangle(-100, -100, -100, -100);
                    for (int i = 1; i < lst.Count; i++)
                    {
                        var form = lst[i];
                        string header = form.Text;
                        w = (int)g.MeasureString(header, this.headerFont).Width + 2;
                        Rectangle r_all = new Rectangle(x, y + 5, w + 5 + 20, h + 2);

                        CellObject obj1 = new CellObject();
                        obj1.r = r_all;
                        obj1.type = 1;
                        obj1.data = form;

                        Rectangle r = new Rectangle(x + (r_all.Width - w) / 2, y + 2, w, h + 2);

                        if (move_form != null && form == (Form)move_form.data)
                        {
                            move_r_all = r_all;
                            r_all = new Rectangle(r_all.X + (this.e_x - this.x), r_all.Y, r_all.Width, r_all.Height);
                            r = new Rectangle(r.X + (this.e_x - this.x), r.Y, r.Width, r.Height);
                        }
                        if (move_form == null || form != (Form)move_form.data)
                        {
                            if (form == this.Controls[0])
                            {
                                g.DrawImage(ResourcesHelper.GetImage("导航栏选中"), r_all);

                                g.DrawString(header, this.headerFont, new SolidBrush(Color.Black), r, sf);
                            }
                            else
                            {
                                g.DrawImage(ResourcesHelper.GetImage("导航栏未选中"), r_all);

                                g.DrawString(header, this.headerFont, new SolidBrush(Color.White), r, sf);
                            }
                        }
                        x += w;
                        //

                        Image dhl_close = ResourcesHelper.GetImage("导航栏关闭");
                        r = new Rectangle(r_all.X + r_all.Width - 8, r_all.Y - 5, dhl_close.Width + 1, dhl_close.Height + 1);

                        x += 40;
                        //
                        CellObject obj2 = new CellObject();
                        obj2.r = r;
                        obj2.type = 2;
                        obj2.data = form;

                        if (move_form == null || form != (Form)move_form.data)
                        {
                            g.DrawImage(DrawHelper.WaySOne((Bitmap)dhl_close), r);
                        }


                        //
                        if (i > 0)
                        {
                            lstobj.Add(obj2);
                        }

                        lstobj.Add(obj1);

                    }

                    //画正在移动的矩形
                    if (isMove == 1 && move_form != null)
                    {
                        var form = (Form)move_form.data;
                        string header = form.Text;
                        w = (int)g.MeasureString(header, this.headerFont).Width + 2;
                        Rectangle r_all = move_r_all;

                        Rectangle r = new Rectangle(r_all.X + (r_all.Width - w) / 2, y + 5, w, h + 2);


                        r_all = new Rectangle(r_all.X + (this.e_x - this.x), r_all.Y, r_all.Width, r_all.Height);
                        r = new Rectangle(r.X + (this.e_x - this.x), r.Y, r.Width, r.Height);

                        if (form == this.Controls[0])
                        {
                            g.DrawImage(ResourcesHelper.GetImage("导航栏选中"), r_all);

                            g.DrawString(header, this.headerFont, new SolidBrush(Color.Black), r, sf);
                        }
                        else
                        {
                            g.DrawImage(ResourcesHelper.GetImage("导航栏未选中"), r_all);

                            g.DrawString(header, this.headerFont, new SolidBrush(Color.White), r, sf);
                        }
                        Image dhl_close = ResourcesHelper.GetImage("导航栏关闭");
                        r = new Rectangle(r_all.X + r_all.Width - 8, r_all.Y - 5, dhl_close.Width + 1, dhl_close.Height + 1);

                        g.DrawImage(DrawHelper.WaySOne((Bitmap)dhl_close), r);


                    }



                    //
                    bru = new LinearGradientBrush(new Rectangle(0, headerHeight - 3, this.Width, 5), Color.White,
                     Color.White, LinearGradientMode.Horizontal);
                    e.Graphics.FillRectangle(bru, new Rectangle(0, headerHeight, this.Width, 2));

                    this.lstobj = lstobj;
                }
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString(ex.ToString(), this.Font, Brushes.Black, 0, 0);
            }
        }

        private int headerHeight = 30;
        [Description("标题高度")]
        [Browsable(true)]
        public int HeaderHeight
        {
            get
            {
                return headerHeight;
            }
            set
            {
                if (value > 10)
                {
                    headerHeight = value;
                }
                else
                {
                    headerHeight = 10;
                }
                this.Refresh();
            }
        }

        private Font headerFont = GlobalData.GetFont();
        [Description("标题字体")]
        [Browsable(true)]
        public Font HeaderFont
        {
            get
            {
                return headerFont;
            }
            set
            {
                headerFont = value;
                this.Refresh();
            }
        }

        private void WindowsList_SizeChanged(object sender, EventArgs e)
        {
            if (this.Controls.Count != 0)
            {
                Form form = (Form)this.Controls[0];
                form.Left = 0;
                form.Top = headerHeight;
                form.Width = this.Width;
                form.Height = this.Height - headerHeight;
            }
        }

        private void WindowsList_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void WindowsList_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (lst.Contains(e.Control) == true)
            {
                lst.Remove((Form)e.Control);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate(new Rectangle(0, 0, this.Width - 1, headerHeight));
        }

        private int isMove = 0;
        private int x;
        private int e_x;
        private CellObject move_form;
        //按下
        private void WindowsList_MouseDown(object sender, MouseEventArgs e)
        {
            this.isMove = 1;
            this.x = e.X;
            this.e_x = e.X;
            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    if (obj.data is frmNav) break;
                    move_form = obj;
                    break;
                }
            }
        }

        //弹出
        private void WindowsList_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = 0;
            move_form = null;
            this.x = e.X;
            this.e_x = e.X;

            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    if (obj.type == 2)
                    {
                        var form = (Form)obj.data;
                        form.Close();
                    }
                    else if (obj.type == 1)
                    {
                        var form = (Form)obj.data;
                        this.ShowForm(form);
                    }
                    break;
                }
            }
        }
        //移动
        private void WindowsList_MouseMove(object sender, MouseEventArgs e)
        {
            this.e_x = e.X;
            if (isMove == 1)
            {
                int index = 0;
                for (int i = 0; i < lstobj.Count; i++)
                {
                    CellObject obj = lstobj[i];
                    if (obj.type != 1) continue;
                    if (move_form == null) break;
                    if (obj.r.X < move_form.r.X)
                    {
                        //往前
                        if ((obj.r.X + obj.r.Width * 0.5) >= e.X)
                        {
                            index = lst.IndexOf((Form)obj.data);
                            break;
                        }
                    }
                    else if (obj.r.X > move_form.r.X)
                    {
                        //往后
                        if ((obj.r.X + obj.r.Width * 0.5) <= e.X)
                        {
                            index = lst.IndexOf((Form)obj.data);
                            break;
                        }
                    }
                }

                if (index > 0)
                {
                    for (int i = lst.Count - 1; i > 0; i--)
                    {
                        if (lst[i] == (Form)move_form.data)
                        {
                            lst.RemoveAt(i);
                            break;
                        }
                    }
                    lst.Insert(index, (Form)move_form.data);
                    this.x = e_x;
                    index = 0;
                }

            }
        }

        private void WindowsList_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    if (obj.type == 2)
                    {
                        lst[0].SendToBack();
                    }
                    else if (obj.type == 1)
                    {
                        var form = (Form)obj.data;
                        this.ShowForm(form);
                    }
                    move_form = obj;
                    break;
                }
            }
        }

    }

}
