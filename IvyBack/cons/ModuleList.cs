using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IvyBack.Helper;

namespace IvyBack.cons
{
    public class ModuleList : System.Windows.Forms.Control
    {

        public ModuleList()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //
            this.InitializeComponent();
            this.TabStop = false;
            this.IconSize = 30;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ModuleList
            // 
            this.SizeChanged += new System.EventHandler(this.ModuleList_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ModuleList_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ModuleList_MouseDown);
            this.ResumeLayout(false);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }



        private int headerWidth = 150;
        [Description("标题高度")]
        [Browsable(true)]
        public int HeaderWidth
        {
            get
            {
                return headerWidth;
            }
            set
            {
                if (value > 180)
                {
                    headerWidth = value;
                }
                else
                {
                    headerWidth = 180;
                }
                this.Refresh();

            }
        }

        private Image headerBackImage;
        [Description("标题背景图片")]
        [Browsable(true)]
        public Image HeaderBackImage
        {
            get
            {
                return headerBackImage;
            }
            set
            {
                headerBackImage = value;
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


        List<object> lst = new List<object>();




        int cy_width = 0;
        Dictionary<object, Image> dicIcon = new Dictionary<object, Image>();
        Dictionary<object, Image> dicSelectIcon = new Dictionary<object, Image>();
        public void ShowForm(Form form)
        {
            if (this.Controls.Contains(form) == false)
            {
                form.BackColor = this.BackColor;
                form.Dock = DockStyle.None;
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Show();
                this.Controls.Add(form);
                form.Left = headerWidth + cy_width;
                form.Top = 0;
                form.Width = this.Width - headerWidth - cy_width;
                form.Height = this.Height;
                form.BringToFront();
                lst.Add(form);
            }
            else
            {
                if (this.Controls[0] != form)
                {
                    form.Left = headerWidth;
                    form.Top = 0;
                    form.Width = this.Width - headerWidth;
                    form.Height = this.Height;
                    form.BringToFront();
                }

            }
            this.Refresh();
        }
        public void ShowForm(Form form, Image icon)
        {
            if (this.Controls.Contains(form) == false)
            {
                form.BackColor = this.BackColor;
                form.Dock = DockStyle.None;
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Show();
                this.Controls.Add(form);
                form.Left = headerWidth + cy_width;
                form.Top = 0;
                form.Width = this.Width - headerWidth - cy_width;
                form.Height = this.Height;

                form.BringToFront();
                lst.Add(form);
                dicIcon.Add(form, icon);
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
        public void ShowForm(Form form, Image icon, Image sel_icon)
        {
            if (this.Controls.Contains(form) == false)
            {
                form.BackColor = this.BackColor;
                form.Dock = DockStyle.None;
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.Show();
                this.Controls.Add(form);
                form.Left = headerWidth + cy_width;
                form.Top = 0;
                form.Width = this.Width - headerWidth - cy_width;
                form.Height = this.Height;


                form.BringToFront();

                lst.Add(form);
                dicIcon.Add(form, icon);
                dicSelectIcon.Add(form, sel_icon);
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
        public void ShowForm(int index)
        {
            if (this.lst.Count >= index)
            {
                object obj = lst[index - 1];
                if (obj is Form frm)
                {
                    frm.BringToFront();
                }
                else if (obj is Action action)
                {
                    action();
                }

            }
        }

        public void ShowAction(Action action, Image icon)
        {
            if (lst.Contains(action) == false)
            {
                lst.Add(action);
                dicIcon.Add(action, icon);
            }

            this.Refresh();
        }

        public void ShowLoad(Form par_frm)
        {
            if (this.IsHandleCreated)
            {
                Action act = delegate ()
                {
                    MainForm.frmLoad.LoadWait(par_frm);
                };
                this.Invoke(act);
            }
        }
        public void CloseLoad(Form par_frm)
        {
            Action act = delegate ()
              {
                  MainForm.frmLoad.LoadClose(par_frm);
              };
            this.Invoke(act);
        }


        private class CellObject
        {
            public Rectangle r { get; set; }

            public object data { get; set; }
        }

        public int IconSize { get; set; }

        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.DrawPath(pen, path);
            }
        }

        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        /// <summary>
        /// 模块起始绘制 Y 位置
        /// </summary>
        private const int ModuleStartYPosition = 30;

        /// <summary>
        /// 模块块绘制减少间距, 注意: 是减少
        /// </summary>
        private const int ModuleReduceBlockSpacing = 8;

        List<CellObject> lstobj = new List<CellObject>();
        private void ModuleList_Paint(object sender, PaintEventArgs e)
        {
            try
            {

                if (DesignMode == true)
                {
                    Graphics g = e.Graphics;
                    /*
                    if (headerBackImage != null)
                    {
                        g.DrawImage(headerBackImage, 0, 0, headerWidth, this.Height);
                    }*/
                    //
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    if (1 == 1)
                    {
                        Pen p = new Pen(Color.Gray);
                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        g.DrawLine(p, headerWidth, 0, headerWidth, this.Height);

                        //
                        Rectangle r = new Rectangle(0, 0, headerWidth, this.Height);
                        g.DrawString("标题", this.headerFont, new SolidBrush(this.ForeColor), r, sf);
                    }
                    if (1 == 1)
                    {
                        Rectangle r = new Rectangle(headerWidth, 0, this.Width - headerWidth, this.Height);
                        g.DrawString("容器", this.Font, new SolidBrush(this.ForeColor), r, sf);
                    }
                }
                else
                {
                    List<CellObject> lstobj = new List<CellObject>();
                    Graphics g = e.Graphics;

                    //
                    LinearGradientBrush bru = new LinearGradientBrush(new Rectangle(0, 0, headerWidth + 50, this.Height + 2), GlobalData.left_nav1,
                        GlobalData.left_nav2, LinearGradientMode.Horizontal);
                    //  g.FillRectangle(bru, new Rectangle(0, -1, headerWidth + 50, this.Height + 2));

                    g.DrawImage(ResourcesHelper.GetImage("菜单栏"), new Rectangle(0, -1, ResourcesHelper.GetImage("菜单栏").Width, this.Height + 1));

                    //
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    //
                    FillRoundRectangle(g, new SolidBrush(this.BackColor), new Rectangle(headerWidth, -1, 100, this.Height + 50), 20);

                    //
                    StringFormat sf = new StringFormat();

                    int w = 130;
                    int h = 0;
                    int x = 0;
                    int y = ModuleStartYPosition;

                    if (headerBackImage != null)
                    {
                        g.DrawImage(headerBackImage, 0, 0, headerWidth, this.Height);
                    }
                    //
                    for (int i = 0; i < lst.Count; i++)
                    {
                        string header = "";
                        object obj = lst[i];
                        if (obj is Form frm)
                        {
                            header = frm.Text;
                        }

                        h = 60;
                        Rectangle r = new Rectangle(x, y, w, h);
                        CellObject obj1 = new CellObject();
                        obj1.r = new Rectangle(0, y, this.headerWidth, h);
                        obj1.data = obj;
                        lstobj.Add(obj1);

                        Image icon = null;
                        dicIcon.TryGetValue(obj, out icon);
                        var p = this.PointToClient((System.Windows.Forms.Cursor.Position));

                        if (obj == this.Controls[0])
                        {
                            if (icon == null)
                            {
                                Helper.DrawHelper.FillRoundRectangle(g, Brushes.Blue, r, 5);
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Center;
                                g.DrawString(header, this.headerFont, new SolidBrush(Color.White), r, sf);
                            }
                            else
                            {
                                h = icon.Height + 5;
                                dicSelectIcon.TryGetValue(obj, out icon);
                                g.DrawImage(icon, new Rectangle(r.X, r.Y, icon.Width, icon.Height));

                            }
                        }
                        else if (r.Contains(p) == true)
                        {
                            if (icon == null)
                            {
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Center;
                                g.DrawString(header, this.headerFont, new SolidBrush(Color.White), r, sf);
                            }
                            else
                            {
                                h = icon.Height + 5;
                                g.DrawImage(icon, new Rectangle(r.X, r.Y, icon.Width, icon.Height));
                            }

                        }
                        else
                        {
                            if (icon == null)
                            {
                                sf.Alignment = StringAlignment.Center;
                                sf.LineAlignment = StringAlignment.Center;
                                g.DrawString(header, this.headerFont, new SolidBrush(Color.White), r, sf);
                            }
                            else
                            {
                                h = icon.Height + 5;
                                g.DrawImage(icon, new Rectangle(r.X, r.Y, icon.Width, icon.Height));
                            }

                        }
                        //

                        //
                        y += h - ModuleReduceBlockSpacing;
                    }

                    // Image gzhImage = ResourcesHelper.GetImage("左侧底部公众号");
                    //if (y - gzhImage.Height > this.Height)
                    //{
                    //    g.DrawImage(gzhImage, new Rectangle(0, y + 2, headerWidth, this.Height - y));
                    //   }


                    this.lstobj = lstobj;
                    //

                }
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString(ex.ToString(), this.Font, Brushes.Black, 0, 0);
            }

        }

        private void ModuleList_SizeChanged(object sender, EventArgs e)
        {
            if (this.Controls.Count != 0)
            {
                Form form = (Form)this.Controls[0];
                form.Left = headerWidth;
                form.Top = 0;
                form.Width = this.Width - headerWidth;
                form.Height = this.Height;
                // int Rgn = Win32.CreateRoundRectRgn(0, 0, form.Width + 20, form.Height + 20, 20, 20);
                //Win32.SetWindowRgn(form.Handle, Rgn, true);
            }
        }

        private void ModuleList_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (CellObject obj in lstobj)
            {
                if (obj.r.Contains(e.X, e.Y) == true)
                {
                    if (obj.data is Form form)
                    {
                        this.ShowForm(form);
                    }
                    else if (obj.data is Action action)
                    {
                        action();
                    }
                    break;
                }
            }
        }

        public void Flush()
        {
            foreach (var frm in lst)
            {
                if (frm is Form f)
                {
                    f.Left = headerWidth + cy_width;
                    f.Top = 0;
                    f.Width = this.Width - headerWidth - cy_width;
                    f.Height = this.Height;
                }
              
                // int Rgn = Win32.CreateRoundRectRgn(0, 0, frm.Width+20, frm.Height+20, 20, 20);
                // Win32.SetWindowRgn(frm.Handle, Rgn, true);
            }
        }

    }
}
