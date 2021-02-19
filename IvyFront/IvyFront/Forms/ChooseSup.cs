using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyFront.Helper;

namespace IvyFront.Forms
{
    public partial class ChooseSup : Form
    {
        List<Model.bi_t_supcust_info> supList = new List<Model.bi_t_supcust_info>();
        List<Model.bi_t_supcust_info> supListAll = new List<Model.bi_t_supcust_info>();
        private Model.bi_t_supcust_info item = null;
        List<CellInfo> pageInfo = new List<CellInfo>();
        int pageIndex = 0;
        int selectIndex = -1;
        Point clickPoint = new Point(-100, -100);

        public ChooseSup()
        {
            InitializeComponent();
            //

            cons.AddClickAct.Add2(label3);
            cons.AddClickAct.Add2(label4);

            //
            //软键盘初始化
            pnlboard.Tag = lbl;
            // this.Controls.Add(pnlboard);
            pnlboard.TabStop = false;
            // pnlboard.BringToFront();
            pnlboard.Controls.Clear();
            string[] lines = new string[] { "1234567890", "QWERTYUIOP", "ASDFGHJKL", "!ZXCVBNM@" };
            int linecount = 4;
            int columncount = 10;
            int lineheight = (pnlboard.Height - 1) / linecount;
            int columnwidth = (pnlboard.Width - 1) / columncount;
            int itop = 0;
            int ileft = 0;
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
                    control.ankey con = new control.ankey();
                    con.BackColor = pnlboard.BackColor;
                    con.TabStop = false;

                    if (c == '!')
                    {
                        con.Text = "删除";
                    }
                    else if (c == '@')
                    {
                        con.Text = "取消";
                    }
                    else
                    {
                        con.Text = c.ToString();
                    }

                    con.Width = columnwidth - 2;
                    con.Height = lineheight - 2;
                    //con.BackColor = Color.Aqua;
                    con.Top = itop;
                    con.Left = ileft;
                    con.MouseDown += this.con_click;
                    ileft += columnwidth;
                    pnlboard.Controls.Add(con);
                }
            }

            try
            {
                IBLL.ISup supBLL = new BLL.Sup();
                supList = supBLL.GetList("");
                foreach (Model.bi_t_supcust_info item in supList)
                {
                    supListAll.Add(item);
                }
            }
            catch (Exception ex)
            {
                new MsgForm(ex.GetMessage()).ShowDialog();
            }
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

        private void con_click(object sender, EventArgs e)
        {
            var con = (System.Windows.Forms.Control)sender;
            string str = con.Text;
            if (pnlboard.Tag != null)
            {
                var con2 = (System.Windows.Forms.Control)pnlboard.Tag;
                con2.Focus();
            }

            //
            if (str == "删除")
            {
                if (lbl.Text.Length == 0)
                {
                }
                else
                {
                    lbl.Text = lbl.Text.Substring(0, lbl.Text.Length - 1);
                    supList.Clear();
                    foreach (Model.bi_t_supcust_info item in supListAll)
                    {
                        if (item.sup_pyname.Contains(lbl.Text) || item.sup_tel.Contains(lbl.Text) || item.supcust_no.Contains(lbl.Text))
                        {
                            supList.Add(item);
                        }
                    }

                    selectIndex = 0;
                    pageIndex = 0;
                    this.panel2.Refresh();
                }
            }
            else if (str == "取消")
            {
                this.Close();
            }
            else
            {
                lbl.Text = lbl.Text + con.Text;
                supList.Clear();
                foreach (Model.bi_t_supcust_info item in supListAll)
                {
                    if (item.sup_pyname.Contains(lbl.Text) || item.sup_tel.Contains(lbl.Text) || item.supcust_no.Contains(lbl.Text))
                    {
                        supList.Add(item);
                    }
                }

                selectIndex = 0;
                pageIndex = 0;
                this.panel2.Refresh();
            }

            this.panel2.Refresh();
            // panel2_Paint(sender, null);
        }

        public Model.bi_t_supcust_info Select()
        {
            this.ShowDialog();
            return item;
        }

        private class CellInfo
        {
            public CellInfo()
            {
            }

            public CellInfo(Rectangle rec, Model.bi_t_supcust_info item)
            {
                this.rec = rec;
                this.item = item;
            }

            public Rectangle rec;
            public Model.bi_t_supcust_info item;
        }

        private void ShowCus(List<Model.bi_t_supcust_info> lst, Control par, ref int pageIndex, Graphics g)
        {
            int bound_left = 8;
            int w = 99;
            int h = 76;
            int w_offset = 0;
            int h_offset = 0;
            int min_offset = 9999;
            for (int i = 0; i < 20; i++)
            {
                int val = w + i;
                int mod = (par.Width - bound_left) % val;
                if (min_offset > mod)
                {
                    min_offset = mod;
                    w_offset = i;
                }
            }

            min_offset = 9999;
            for (int i = 0; i < 20; i++)
            {
                int val = h + i;
                int mod = par.Height % val;
                if (min_offset > mod)
                {
                    min_offset = mod;
                    h_offset = i;
                }
            }

            w += w_offset - 1;
            h += h_offset - 1;
            int colNum = (par.Width - bound_left) / w;
            int rowNum = par.Height / h;
            int pageSize = rowNum * colNum;
            pageSize -= 2;
            //
            pageInfo.Clear();
            //
            int index = pageIndex * pageSize;
            if (index > lst.Count - 1)
            {
                pageIndex--;
                index = pageIndex * pageSize;
            }

            SolidBrush bru = new SolidBrush(Color.WhiteSmoke);
            Pen p = new Pen(Color.Gray);
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    if (i == rowNum - 1 && j == 0)
                    {
                        Rectangle rec = new Rectangle(bound_left + j * w, i * h, w, h);
                        label3.Location = new Point(rec.X + 1, rec.Y + 1);
                        label3.Size = new Size(rec.Width - 1, rec.Height - 1);
                        label3.Visible = true;
                    }
                    else if (i == rowNum - 1 && j == colNum - 1)
                    {
                        Rectangle rec = new Rectangle(bound_left + j * w, i * h, w, h);
                        label4.Location = new Point(rec.X + 1, rec.Y + 1);
                        label4.Size = new Size(rec.Width - 1, rec.Height - 1);
                        label4.Visible = true;
                    }

                    if (index > lst.Count - 1)
                    {
                        Rectangle rec = new Rectangle(bound_left + j * w, i * h, w, h);
                        g.FillRectangle(bru, rec);
                        g.DrawRectangle(p, rec);
                        clickPoint = new Point(-100, -100);
                        pageInfo.Add(new CellInfo(rec, null));
                        index++;
                    }
                    else
                    {
                        Rectangle rec = new Rectangle(bound_left + j * w, i * h, w, h);
                        if (rec.Contains(clickPoint) == true)
                        {
                            selectIndex = index;
                            g.FillRectangle(Brushes.White, rec);
                            clickPoint = new Point(-100, -100);
                        }
                        else if (selectIndex == index)
                        {
                            g.FillRectangle(Brushes.White, rec);
                        }
                        else
                        {
                            g.FillRectangle(bru, rec);
                        }

                        g.DrawRectangle(p, rec);
                        var item = lst[index];
                        pageInfo.Add(new CellInfo(rec, item));
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        g.DrawString(item.supcust_no + "\r\n" + item.sup_name, new Font("微软雅黑", 12), Brushes.Black, rec,
                            sf);
                        index++;
                    }
                }
            }
        }

        private bool GetItem(Point p, out Model.bi_t_supcust_info cus)
        {
            foreach (CellInfo cell in pageInfo)
            {
                if (cell.rec.Contains(p) == true)
                {
                    if (cell.item != null)
                    {
                        cus = cell.item;
                        return true;
                    }
                }
            }

            cus = null;
            return false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (pageIndex > 0)
            {
                pageIndex--;
                panel2.Refresh();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            pageIndex++;
            panel2.Refresh();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (supList != null && supList.Count >= 1)
            {
                this.ShowCus(supList, panel2, ref pageIndex, e.Graphics);
            }
        }

        private void ChooseSup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            clickPoint = new Point(e.X, e.Y);
            this.panel2.Refresh();
            this.panel2.Refresh();
            Model.bi_t_supcust_info cus = new Model.bi_t_supcust_info();

            if (GetItem(new Point(e.X, e.Y), out cus) == true)
            {
                this.item = cus;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ChooseSup_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 255)), 0, 0, this.Width - 1, this.Height - 1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Gray, 0, 0, panel1.Width, 0);
        }

        private void ChooseSup_Load(object sender, EventArgs e)
        {
            
        }
    }
}