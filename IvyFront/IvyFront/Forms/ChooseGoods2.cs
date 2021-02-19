using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IvyFront.Forms
{
    public partial class ChooseGoods2 : Form
    {
        public ChooseGoods2()
        {
            InitializeComponent();
            //
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public class MyPanel : Panel
        {
            public MyPanel()
            {
                this.SetStyle(
                    ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                    ControlStyles.AllPaintingInWmPaint, true);
            }
        }

        string keyword = "";
        private class KeyObject
        {
            public string key { get; set; }
            public Rectangle r { get; set; }
        }

        List<KeyObject> lstKey = new List<KeyObject>();
        private void pnlboard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.LightBlue, 0, 0, pnlboard.Width - 1, pnlboard.Height - 1);
            //
            Graphics g = e.Graphics;
            string[] lines = new string[] { "1234567890", "QWERTYUIOP", "ASDFGHJKL", "!ZXCVBNM@" };
            int linecount = 4;
            int columncount = 10;
            int lineheight = (pnlboard.Height - 1) / linecount;
            int columnwidth = (pnlboard.Width - 1) / columncount;
            int itop = 0;
            int ileft = 0;
            //
            List<KeyObject> lst = new List<KeyObject>();
            for (int i = 0; i < 4; i++)
            {
                itop = i * lineheight + 1;
                if (i == 0)
                {
                    ileft = 1;
                }
                else if (i == 1)
                {
                    ileft = 1;
                }
                else if (i == 2)
                {
                    ileft = columnwidth / 2 + 1;
                }
                else if (i == 3)
                {
                    ileft = columnwidth / 2 + 1;
                }

                foreach (char c in lines[i])
                {
                    
                    string str = "";
                    if (c == '!')
                    {
                        str  = "删除";
                    }
                    else if (c == '@')
                    {
                        str  = "取消";
                    }
                    else
                    {
                        str = c.ToString();
                    }

                    int x =ileft;
                    int y = itop;
                    int w = columnwidth - 2;
                    int h = lineheight - 2;
                    
                    ileft += columnwidth;

                    KeyObject item = new KeyObject();
                    item.key = str;
                    item.r = new Rectangle(x, y, w, h);
                    lst.Add(item);
                    //
                    FillRoundRectangle(g, new SolidBrush(Color.FromArgb(30, 119, 206)), new Rectangle(x, y, w - 1, h - 1), 10);
                    DrawRoundRectangle(g, new Pen(Color.White), new Rectangle(x, y, w - 1, h - 1), 10);
                    System.Drawing.StringFormat sf = new StringFormat();
                    sf.Alignment = System.Drawing.StringAlignment.Center;
                    sf.LineAlignment = System.Drawing.StringAlignment.Center;
                    e.Graphics.DrawString(str, new Font("微软雅黑", 13),
                    new SolidBrush(Color.White), new Rectangle(x, y, w - 3, h), sf);
                }
            }

            this.lstKey = lst;
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

        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lbl.Text = keyword;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (runStatus == -1)
            {
                this.Top -=100;
                if (this.Top + this.Height < System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height  )
                {
                    this.Top = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height  - this.Height;
                    runStatus = 1;
                    timer2.Enabled = false;
                }
            }
        }

        int runStatus = 0;
        public delegate void KeyWorkChangeHandler(string keyword);
        private KeyWorkChangeHandler deleg;
        private static ChooseGoods2 ins;
        public static  void ShowKeyboard(KeyWorkChangeHandler deleg)
        {
            if (ins == null)
            {
                ins = new ChooseGoods2();
            }
            else if (ins.IsDisposed == true)
            {
                ins = new ChooseGoods2();
            }
           

            ins.Show();
            ins.timer2.Enabled = true;
            ins.deleg = deleg;
            ins.Top = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height  ;
            ins.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - ins.Width;
            ins.runStatus = -1;
            ins.TopMost = true;
            ins.keyword = "";
            ins.deleg.Invoke(ins.keyword);
        }

        public static void HideKeyboard()
        {
            if (ins == null)
            {

            }
            else if (ins.IsDisposed == true)
            {

            }
            else
            {
                ins.runStatus = 0;
                ins.keyword = "";
                ins.Close();
            }
        }

        private void pnlboard_Click(object sender, EventArgs e)
        {
           
        }

        private void pnlboard_MouseClick(object sender, MouseEventArgs e)
        {
          
            foreach (KeyObject item in lstKey)
            {
                if (item.r.Contains(new Point(e.X, e.Y)) == true)
                {
                    
                    string str = item.key;
                   
                    //
                    if (str == "删除")
                    {
                        if (keyword.Length == 0)
                        {

                        }
                        else
                        {
                            keyword = keyword.Substring(0, keyword.Length - 1);
                            this.deleg.Invoke(keyword);
                        }
                    }
                    else if (str == "取消")
                    {
                        keyword = "";
                        this.deleg.Invoke(keyword);
                        this.runStatus = 0;
                        this.Close();
                    }
                    else
                    {
                        keyword += str;
                        this.deleg.Invoke(keyword);
                    }
                    //
                    break;

                }
            }
           
        }


    }


}
