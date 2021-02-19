using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.cons;
using IvyBack.Helper;
using System.Threading;
using System.Drawing.Drawing2D;

namespace IvyBack.MainForm
{
    public partial class frmNavCommon : Form
    {
        public delegate void ItemClickHandler(string item);
        private event ItemClickHandler itemClick;
        public event ItemClickHandler ItemClick
        {
            add
            {
                itemClick += value;
            }
            remove
            {
                itemClick -= value;
            }
        }

        WindowsList windowsList; 
        public frmNavCommon(WindowsList windowsList)
        {
            InitializeComponent();
            //
            if (DesignMode == false)
            {
                pnl1.Controls.Clear();
                pnl2.Controls.Clear();
            }
           
            this.windowsList = windowsList;
        }


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



        List<string> lstmenu = new List<string>();
        public void SetMenu(string menus)
        {
            lstmenu.Add(menus);
            pnl1.Refresh();
        }

        List<string> lstreport = new List<string>();
        public void SetReportABC(string reports)
        {
            lstreport.Add(reports);
            pnl2.Refresh(); 
        }

        private int GetMenuPageCount()
        {
            int page_count = 1;
            List<Color> lstcolor = new List<Color>();
            lstcolor.Add(Color.FromArgb(55, 106, 179));
            lstcolor.Add(Color.FromArgb(110, 185, 44));
            lstcolor.Add(Color.FromArgb(91, 168, 222));
            lstcolor.Add(Color.FromArgb(235, 99, 52));
            lstcolor.Add(Color.FromArgb(24, 47, 112));
            Color c1 = Color.FromArgb(233, 235, 240);
            Color c2 = Color.FromArgb(255, 224, 169);
            int bound = 22;
            int w = 150;
            int h = 33;
            int x = bound;
            int y = bound;
            int index = 0;
            for (int i = 0; i < lstmenu.Count; i++)
            {
                x = bound;
                var menus = lstmenu[i].Split(',');
                for (int j = 0; j < menus.Length; j++)
                {
                    if (x + w + 5 > pnl1.Width)
                    {
                        y += h + bound;
                        x = bound + w + 30;
                        if (y + h + bound > pnl1.Height)
                        {
                            page_count++;
                            y = bound;
                        }
                    }
                    string menu = menus[j];
                    Rectangle r = new Rectangle(x, y, w, h);
                    Point p = System.Windows.Forms.Cursor.Position;
                    p = pnl1.PointToClient(p);
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Word;
                    if (j != 0)
                    {
                        CellInfo cell = new CellInfo();
                        cell.type = "01";
                        cell.r = r;
                        cell.menu = menu;
                        lstmenucell.Add(cell);
                    }
                    if (j == 0)
                    {
                       
                    }
                    else if (r.Contains(p) == true)
                    {

                       
                    }
                    else
                    {
                        
                    }
                    x += w + 30;
                }
                y += h + bound;
                if (i<lstmenu.Count-1 && y + h + bound > pnl1.Height)
                {
                    page_count++;
                    y = bound;
                }
                index++;
                if (index >= lstcolor.Count)
                {
                    index = 0;
                }
            }
            return page_count;
        }

        List<CellInfo> lstmenucell = new List<CellInfo>(); 
        private int menu_page_index=1;
        private void pnl1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                GC.Collect();
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                //
                int split_line = 0;
                if (run_mode1 == 1)
                {
                    if (this.menu_page_index > this.menu_page_index_pre)
                    {
                        split_line = (int)(pnl1.Width * turn1 / 10F);
                    }
                    else
                    {
                        split_line = (int)(pnl1.Width * (10 - turn1) / 10F);
                    }

                }
                else
                {
                    split_line = 0;
                }
                //
                Bitmap bmp1 = new Bitmap(pnl1.Width, pnl1.Height);
                Graphics g1 = Graphics.FromImage(bmp1);
                g1.Clear(Color.White);
                g1.CompositingQuality = CompositingQuality.HighQuality;
                g1.SmoothingMode = SmoothingMode.HighQuality; 
                //  
                Bitmap bmp2 = new Bitmap(pnl1.Width, pnl1.Height);
                Graphics g2 = Graphics.FromImage(bmp2);
                g2.Clear(Color.White);
                g2.CompositingQuality = CompositingQuality.HighQuality;
                g2.SmoothingMode = SmoothingMode.HighQuality; 
                
                // 
                List<CellInfo> lstmenucell = new List<CellInfo>();
                int page_count = GetMenuPageCount();
                int page_index = 1;
                if (1 == 1)
                {
                    List<Color> lstcolor = new List<Color>();
                    lstcolor.Add(Color.FromArgb(55, 106, 179));
                    lstcolor.Add(Color.FromArgb(110, 185, 44));
                    lstcolor.Add(Color.FromArgb(91, 168, 222));
                    lstcolor.Add(Color.FromArgb(235, 99, 52));
                    lstcolor.Add(Color.FromArgb(24, 47, 112));
                    Color c1 = Color.FromArgb(233, 235, 240);
                    Color c2 = Color.FromArgb(255, 224, 169);
                    int bound = 22;
                    int w = 150;
                    int h = 33; 
                    int x = bound;
                    int y = bound;
                    int index = 0; 
                    for (int i = 0; i < lstmenu.Count; i++)
                    {
                        x = bound;
                        var menus = lstmenu[i].Split(',');
                        for (int j = 0; j < menus.Length; j++)
                        {
                            if (x + w + 5 > pnl1.Width)
                            {
                               
                                y += h + bound;
                                x = bound + w + 30;
                                if (y + h + bound > pnl1.Height)
                                {
                                    page_index++;
                                    y = bound;
                                }
                            }
                            string menu = menus[j];
                            Rectangle r = new Rectangle(x, y, w, h);
                            Point p = System.Windows.Forms.Cursor.Position;
                            p = pnl1.PointToClient(p);
                            StringFormat sf = new StringFormat();
                            sf.LineAlignment = StringAlignment.Center;
                            sf.Alignment = StringAlignment.Center;
                            sf.Trimming = StringTrimming.Word;
                            if (j != 0 && page_index==this.menu_page_index)
                            {
                                CellInfo cell = new CellInfo();
                                cell.type = "01";
                                cell.r = r;
                                cell.menu = menu;
                                lstmenucell.Add(cell);
                            }
                            if (j == 0 && page_index == this.menu_page_index)
                            {
                                var c = lstcolor[index];
                                FillRoundRectangle(g1, new SolidBrush(c), r, 5);
                                g1.DrawString(menu, new Font("微软雅黑", 11), new SolidBrush(Color.White), r, sf);
                            }
                            else if (r.Contains(p) == true && page_index == this.menu_page_index)
                            {

                                FillRoundRectangle(g1, new SolidBrush(c2), r, 5);
                                g1.DrawString(menu, new Font("微软雅黑", 11), new SolidBrush(Color.Black), r, sf);
                            }
                            else if(page_index==this.menu_page_index)
                            {
                                FillRoundRectangle(g1, new SolidBrush(c1), r, 5);
                                g1.DrawString(menu, new Font("微软雅黑", 11), new SolidBrush(Color.Black), r, sf);
                            }
                            //
                            if (j != 0 && page_index == this.menu_page_index_pre )
                            {
                                
                            }
                            if (j == 0 && page_index == this.menu_page_index_pre)
                            {
                                var c = lstcolor[index];
                                FillRoundRectangle(g2, new SolidBrush(c), r, 5);
                                g2.DrawString(menu, new Font("微软雅黑", 11), new SolidBrush(Color.White), r, sf);
                            }
                            else if (r.Contains(p) == true && page_index == this.menu_page_index_pre)
                            {

                                FillRoundRectangle(g2, new SolidBrush(c2), r, 5);
                                g2.DrawString(menu, new Font("微软雅黑", 11), new SolidBrush(Color.Black), r, sf);
                            }
                            else if (page_index == this.menu_page_index_pre)
                            {
                                FillRoundRectangle(g2, new SolidBrush(c1), r, 5);
                                g2.DrawString(menu, new Font("微软雅黑", 11), new SolidBrush(Color.Black), r, sf);
                            }
                            //
                            x += w + 30;
                        }
                        y += h + bound;
                        if (i<lstmenu.Count-1 && y + h + bound > pnl1.Height)
                        {
                            page_index ++;
                            y = bound;
                        }
                        index++;
                        if (index >= lstcolor.Count)
                        {
                            index = 0;
                        }
                    }
                
                }
                //
                if (run_mode1 == 1)
                {
                    if (this.menu_page_index > this.menu_page_index_pre)
                    {
                        e.Graphics.DrawImage(bmp1, split_line, 0, pnl1.Width - split_line, pnl1.Height);
                        e.Graphics.DrawImage(bmp2, 0, 0, split_line, pnl1.Height); 
                    }
                    else
                    {
                        e.Graphics.DrawImage(bmp2, split_line, 0, pnl1.Width - split_line, pnl1.Height);
                        e.Graphics.DrawImage(bmp1, 0, 0, split_line, pnl1.Height); 
                    }

                }
                else
                {
                    e.Graphics.DrawImage(bmp1, 0, 0);
                } 
                bmp1.Dispose();
                bmp2.Dispose();
                //
                if (page_count >1)
                {
                     
                    int x = pnl1.Width-40;
                    int y = pnl1.Height-20;
                    int w = 25;
                    int h = 20;
                    for (int i = 1; i <= page_count; i++)
                    {
                        x -= 25;
                        Rectangle r = new Rectangle(x, y, w, h);
                        Point p = System.Windows.Forms.Cursor.Position;
                        p = pnl1.PointToClient(p);
                        if (r.Contains(p))
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.Gold), new Rectangle(x + (w - 8) / 2, y + (h - 8) / 2, 8, 8));
                        }
                        else if ((page_count - i) + 1 == this.menu_page_index)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(55, 106, 179)), new Rectangle(x + (w - 8) / 2, y + (h - 8) / 2, 8, 8));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(233, 235, 240)), new Rectangle(x + (w - 8) / 2, y + (h - 8) / 2, 8, 8));
                        }
                        
                        CellInfo cell = new CellInfo();
                        cell.type = "02";
                        cell.r = r;
                        cell.page_index = (page_count - i) + 1;
                        lstmenucell.Add(cell);
                    }
                }
                //
                this.lstmenucell = lstmenucell;  
                //
                if (menu_page_index > page_count)
                {
                    menu_page_index--;
                    pnl1.Refresh();
                }
                
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString(ex.ToString(), this.Font, Brushes.Black, new Rectangle(0, 0, pnl2.Width, pnl2.Height));
            }
        }

        private class CellInfo
        {
            public Rectangle r { get; set; }
            public string menu { get; set; }
            /// <summary>
            /// 01_按钮;02_分页
            /// </summary>
            public string type { get; set; }
            public int page_index { get; set; }
        }


        private int GetReportPageCount()
        {
            int page_count = 1;
            int bound = pictureBox2.Left + 50;
            int w = 200;
            int h = 35;

            int x = bound;
            int y = 0;
            for (int i = 0; i < lstreport.Count; i++)
            {
                y = pictureBox2.Height + 13;
                var reports = lstreport[i].Split(',');
                for (int j = 0; j < reports.Length; j++)
                {
                    string report = reports[j];
                    Rectangle r = new Rectangle(x, y, w, h);
                    Point p = System.Windows.Forms.Cursor.Position;
                    p = pnl2.PointToClient(p);
                    int logo_w = 15;
                    int logo_h = 15;
                    
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    sf.Trimming = StringTrimming.Word;

                    CellInfo cell = new CellInfo();
                    cell.type = "01";
                    cell.r = r;
                    cell.menu = report;
                    lstreportcell.Add(cell);
                    if (r.Contains(p) == true)
                    {
                       
                    }
                    else
                    {

                        
                    }

                    y += h;
                    
                }
                x += w;
                if (i < lstreport.Count-1 && x + w + 5 > pnl2.Width)
                {
                    page_count++;
                    x = bound;
                }
            }

            return page_count;
        }

        List<CellInfo> lstreportcell = new List<CellInfo>();
        public int report_page_index = 1;
        private void pnl2_Paint(object sender, PaintEventArgs e)
        {
            try
            {
               
               
                // 
                int split_line = 0;
                if (run_mode2 == 1)
                {
                    if (this.report_page_index > this.report_page_index_pre)
                    {
                        split_line = (int)(pnl2.Width * turn2 / 10F);
                    }
                    else
                    {
                        split_line = (int)(pnl2.Width * (10-turn2) / 10F);
                    }
                    
                }
                else
                {
                    split_line = 0;
                }
                //
                Bitmap bmp1 = new Bitmap(pnl2.Width, pnl2.Height);
                Graphics g1 = Graphics.FromImage(bmp1);
                g1.Clear(Color.White);
                g1.CompositingQuality = CompositingQuality.HighQuality;
                g1.SmoothingMode = SmoothingMode.HighQuality;
              
                //
                Bitmap bmp2 = new Bitmap(pnl2.Width, pnl2.Height);
                Graphics g2 = Graphics.FromImage(bmp2);
                g2.Clear(Color.White);
                g2.CompositingQuality = CompositingQuality.HighQuality;
                g2.SmoothingMode = SmoothingMode.HighQuality;
                //
                int page_count = GetReportPageCount();
                List<CellInfo> lstreportcell = new List<CellInfo>();
                int page_index = 1;
                if (1 == 1)
                {
                    int bound = pictureBox2.Left + 50;
                    int w = 200;
                    int h = 35;

                    int x = bound;
                    int y = 0;
                    for (int i = 0; i < lstreport.Count; i++)
                    {
                        y = pictureBox2.Height + 13;
                        var reports = lstreport[i].Split(',');
                        for (int j = 0; j < reports.Length; j++)
                        {
                            string report = reports[j];
                            Rectangle r = new Rectangle(x, y, w, h);
                            Point p = System.Windows.Forms.Cursor.Position;
                            p = pnl2.PointToClient(p);
                            int logo_w = 15;
                            int logo_h = 15;
                            if (page_index == this.report_page_index)
                            {
                                g1.DrawImage(pictureBox1.Image, new Rectangle(x, y + (h - logo_h) / 2, logo_w, logo_h));
                            }
                            if (page_index == this.report_page_index_pre)
                            {
                                g2.DrawImage(pictureBox1.Image, new Rectangle(x, y + (h - logo_h) / 2, logo_w, logo_h));
                            }
                           
                            if (page_index == this.report_page_index)
                            {
                                CellInfo cell = new CellInfo();
                                cell.type = "01";
                                cell.r = r;
                                cell.menu = report;
                                lstreportcell.Add(cell);
                            }
                            StringFormat sf = new StringFormat();
                            sf.LineAlignment = StringAlignment.Center;
                            sf.Alignment = StringAlignment.Near;
                            sf.Trimming = StringTrimming.Word; 
                            if (r.Contains(p) == true && page_index==this.report_page_index)
                            {
                                Rectangle r2 = new Rectangle(r.X + logo_w + 5, r.Y, r.Width - logo_w - 5, r.Height); 
                                g1.DrawString(report, new Font("微软雅黑", 10.5F), new SolidBrush(Color.Red), r2, sf);
                            }
                            else if(page_index==this.report_page_index)
                            { 
                                Rectangle r2 = new Rectangle(r.X + logo_w + 5, r.Y, r.Width - logo_w - 5, r.Height); 
                                g1.DrawString(report, new Font("微软雅黑", 10.5F), new SolidBrush(Color.FromArgb(50, 50, 50)), r2, sf); 
                            }
                            //
                            if (r.Contains(p) == true && page_index == this.report_page_index_pre)
                            {
                                Rectangle r2 = new Rectangle(r.X + logo_w + 5, r.Y, r.Width - logo_w - 5, r.Height);
                                g2.DrawString(report, new Font("微软雅黑", 10.5F), new SolidBrush(Color.Red), r2, sf);
                            }
                            else if (page_index == this.report_page_index_pre)
                            {
                                Rectangle r2 = new Rectangle(r.X + logo_w + 5, r.Y, r.Width - logo_w - 5, r.Height);
                                g2.DrawString(report, new Font("微软雅黑", 10.5F), new SolidBrush(Color.FromArgb(50, 50, 50)), r2, sf);
                            } 
                            
                            y += h;
                        }
                        x += w;
                        if (i < lstreport.Count - 1 && x + w + 5 > pnl2.Width)
                        {
                            page_index++;
                            x = bound;
                        }
                    }
                }
                //
                if (run_mode2 == 1)
                {
                    if (this.report_page_index > this.report_page_index_pre)
                    {
                        e.Graphics.DrawImage(bmp1, split_line, 0, pnl2.Width - split_line, pnl2.Height);
                        e.Graphics.DrawImage(bmp2, 0, 0, split_line, pnl2.Height);
                    }
                    else
                    {
                        e.Graphics.DrawImage(bmp2, split_line, 0, pnl2.Width - split_line, pnl2.Height);
                        e.Graphics.DrawImage(bmp1, 0, 0, split_line, pnl2.Height);
                    }

                }
                else
                { 
                    e.Graphics.DrawImage(bmp1, 0, 0);
                }
                bmp1.Dispose();
                bmp2.Dispose();
                //
                e.Graphics.DrawLine(new Pen(Color.FromArgb(200, 198, 200), 2), pictureBox2.Right - 2, pictureBox2.Top + pictureBox2.Height / 2, pnl2.Right, pictureBox2.Top + pictureBox2.Height / 2);
                e.Graphics.DrawImage(pictureBox2.Image, pictureBox2.Left, pictureBox2.Top, pictureBox2.Width, pictureBox2.Height);
                if (page_count>1)
                {
                    e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                    int x = pnl2.Width-40;
                    int y = pnl2.Height - 30;
                    int w = 25;
                    int h = 20;
                    for (int i = 1; i <= page_count; i++)
                    {
                        x -= 25;
                        Rectangle r = new Rectangle(x, y, w, h);
                        Point p = System.Windows.Forms.Cursor.Position;
                        p = pnl2.PointToClient(p);
                        if (r.Contains(p))
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.Gold), new Rectangle(x + (w - 8) / 2, y + (h - 8) / 2, 8, 8));
                        }
                        else if ((page_count - i) + 1 == this.report_page_index)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(55, 106, 179)), new Rectangle(x + (w - 8) / 2, y + (h - 8) / 2, 8, 8));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(233, 235, 240)), new Rectangle(x + (w - 8) / 2, y + (h - 8) / 2, 8, 8));
                        }
                      
                        CellInfo cell = new CellInfo();
                        cell.type = "02";
                        cell.r = r;
                        cell.page_index = (page_count - i) + 1;
                        lstreportcell.Add(cell);
                    }
                }
                //
                this.lstreportcell = lstreportcell;
                //
                if (report_page_index > page_count)
                {
                    report_page_index--;
                    pnl2.Refresh();
                }
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString(ex.ToString(), this.Font, Brushes.Black, new Rectangle(0, 0, pnl2.Width, pnl2.Height));
            }
        }

        private void frmNavCommon_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pnl2_MouseLeave(object sender, EventArgs e)
        {
            pnl2.Refresh();
        }

        private void frmNavCommon_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void pnl2_MouseMove(object sender, MouseEventArgs e)
        {
            pnl2.Refresh();
            System.Threading.Thread.Sleep(10);
        }

        private void pnl1_MouseMove(object sender, MouseEventArgs e)
        {
            pnl1.Refresh();
            System.Threading.Thread.Sleep(10);
        }

        private void pnl1_MouseLeave(object sender, EventArgs e)
        {
            pnl1.Refresh();
        }

        /// <summary>
        /// 0_常规;1_翻页
        /// </summary>
        private int run_mode1 = 0;
        private int menu_page_index_pre = 0;
        private int turn1 = 0;
        private void pnl1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (CellInfo cell in lstmenucell)
            {
                if (cell.r.Contains(e.X, e.Y))
                {
                    if (cell.type == "01")
                    {
                        if (itemClick != null)
                        {
                            itemClick.Invoke(cell.menu);
                        }
                    }
                    else if (cell.type == "02")
                    {
                        if (this.menu_page_index != cell.page_index)
                        {
                            menu_page_index_pre = this.menu_page_index;
                            this.menu_page_index = cell.page_index;
                            run_mode1 = 1;
                            turn1 = 10;
                            while (turn1 > 0)
                            {
                                this.pnl1.Refresh();
                                System.Threading.Thread.Sleep(10);
                                turn1--;
                            }
                            run_mode1 = 0;
                            this.pnl1.Refresh();
                        }
                    
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 0_常规;1_翻页
        /// </summary>
        private int run_mode2 = 0;
        private int report_page_index_pre = 0;
        private int turn2 = 0;
        private void pnl2_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (CellInfo cell in lstreportcell)
            {
                if (cell.r.Contains(e.X, e.Y))
                {
                    if (cell.type == "01")
                    {
                        if (itemClick != null)
                        {
                            itemClick.Invoke(cell.menu);
                        }
                    }
                    else if (cell.type == "02")
                    {
                        if (this.report_page_index != cell.page_index)
                        {
                            report_page_index_pre = this.report_page_index; 
                            this.report_page_index = cell.page_index; 
                            run_mode2 = 1;
                            turn2 = 10;
                            while (turn2 > 0)
                            {
                                this.pnl2.Refresh();
                                System.Threading.Thread.Sleep(10);
                                turn2--;
                            }
                            run_mode2 = 0;
                            this.pnl2.Refresh();
                        }
                       
                    }
                    break;
                }
            }
        }

        private void frmNavCommon_VisibleChanged(object sender, EventArgs e)
        {
            if (lstreport.Count == 0)
            {
                pnl2.Visible = false;
            }
            else
            {
                pnl2.Visible = true;
            }
        }

 


    }
}
