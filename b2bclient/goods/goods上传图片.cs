using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.goods
{
    public partial class goods上传图片 : UserControl
    {
        BLL.IGoods bll = new BLL.Goods();
        public goods上传图片()
        {
            InitializeComponent();
            //
            control.ClickActive.addActive(pnlimgA);
            control.ClickActive.addActive(pnlimgB1);
        }

        private void pnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlimg1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, pnlimgA.Width - 1, pnlimgA.Height - 1));
            if (pnlimgA.BackgroundImage == null)
            {
                var sf = new StringFormat();
                sf.Alignment = System.Drawing.StringAlignment.Center;
                sf.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("点击上传", new Font("微软雅黑", 10), Brushes.LightGray, new Rectangle(0, 0, pnlimgA.Width - 1, pnlimgA.Height - 1), sf);
            }
        }

        private void panelA_Click(object sender, EventArgs e)
        {
            pnlimgA.BackgroundImage = null;
            pnlimgA.Text = "";
        }
        private void pnlimg2_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, pnlimgB1.Width - 1, pnlimgB1.Height - 1));
            if (pnlimgB1.BackgroundImage == null)
            {
                var sf = new StringFormat();
                sf.Alignment = System.Drawing.StringAlignment.Center;
                sf.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("点击上传", new Font("微软雅黑", 10), Brushes.LightGray, new Rectangle(0, 0, pnlimgB1.Width - 1, pnlimgB1.Height - 1), sf);
            }
        }


        private void panelB1_Click(object sender, EventArgs e)
        {
            pnlimgB1.BackgroundImage = null;
            pnlimgB1.Text = "";
        }
        private void pnlimg3_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, pnlimgC1.Width - 1, pnlimgC1.Height - 1));
            if (pnlimgB1.BackgroundImage == null)
            {
                var sf = new StringFormat();
                sf.Alignment = System.Drawing.StringAlignment.Center;
                sf.LineAlignment = System.Drawing.StringAlignment.Center;
                e.Graphics.DrawString("点击上传", new Font("微软雅黑", 10), Brushes.LightGray, new Rectangle(0, 0, pnlimgC1.Width - 1, pnlimgC1.Height - 1), sf);
            }
        }
        private void pnlimg1_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img,300,300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgA.Text = str;
                        pnlimgA.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
           
        }

        private void pnlimg2_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgB1.Text = str;
                        pnlimgB1.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void pnlimg21_Paint(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgB2.Text = str;
                        pnlimgB2.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void panelB2_Click(object sender, EventArgs e)
        {
            pnlimgB2.BackgroundImage = null;
            pnlimgB2.Text = "";
        }
        private void pnlimg22_Paint(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgB3.Text = str;
                        pnlimgB3.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void panelB3_Click(object sender, EventArgs e)
        {
            pnlimgB3.BackgroundImage = null;
            pnlimgB3.Text = "";
        }
        private void pnlimg31_Paint(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgC1.Text = str;
                        pnlimgC1.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void panelC1_Click(object sender, EventArgs e)
        {
            pnlimgC1.BackgroundImage = null;
            pnlimgC1.Text = "";
        }
        private void pnlimg32_Paint(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgC2.Text = str;
                        pnlimgC2.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void panelC2_Click(object sender, EventArgs e)
        {
            pnlimgC2.BackgroundImage = null;
            pnlimgC2.Text = "";
        }
        private void pnlimg33_Paint(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgC3.Text = str;
                        pnlimgC3.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void panelC3_Click(object sender, EventArgs e)
        {
            pnlimgC3.BackgroundImage = null;
            pnlimgC3.Text = "";
        }
        private void pnlimg34_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgC4.Text = str;
                        pnlimgC4.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);

            }
        }

        private void panelC4_Click(object sender, EventArgs e)
        {
            pnlimgC4.BackgroundImage = null;
            pnlimgC4.Text = "";
        }
        private void pnlimg35_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgC5.Text = str;
                        pnlimgC5.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);

            }
        }

        private void panelC5_Click(object sender, EventArgs e)
        {
            pnlimgC5.BackgroundImage = null;
            pnlimgC5.Text = "";
        }
        private void pnlimg36_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    var f2 = new frmImageCut(img, 300, 300);
                    if (f2.ShowDialog() == DialogResult.OK)
                    {
                        img = f2.getImage();
                        string str = "";
                        bll.UploadImage(img, out str);
                        pnlimgC6.Text = str;
                        pnlimgC6.BackgroundImage = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);

            }
        }

        private void panelC6_Click(object sender, EventArgs e)
        {
            pnlimgC6.BackgroundImage = null;
            pnlimgC6.Text = "";
        }

        public string small_img_url()
        {
            return pnlimgA.Text;
        }

        public string large_img_url()
        {
            string str = "";
            if (pnlimgB1.Text != "" && pnlimgB1.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgB1.Text;
                }
                else
                {
                    str =str +","+ pnlimgB1.Text;
                }
            }
            if (pnlimgB2.Text != "" && pnlimgB2.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgB2.Text;
                }
                else
                {
                    str = str + "," + pnlimgB2.Text;
                }
            }
            if (pnlimgB3.Text != "" && pnlimgB3.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgB3.Text;
                }
                else
                {
                    str = str + "," + pnlimgB3.Text;
                }
            }
            return str;
        }

        public string detail_img_url()
        {
            string str = "";
            if (pnlimgC1.Text != "" && pnlimgC1.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgC1.Text;
                }
                else
                {
                    str = str + "," + pnlimgC1.Text;
                }
            }
            if (pnlimgC2.Text != "" && pnlimgC2.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgC2.Text;
                }
                else
                {
                    str = str + "," + pnlimgC2.Text;
                }
            }
            if (pnlimgC3.Text != "" && pnlimgC3.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgC3.Text;
                }
                else
                {
                    str = str + "," + pnlimgC3.Text;
                }
            }
            if (pnlimgC4.Text != "" && pnlimgC4.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgC4.Text;
                }
                else
                {
                    str = str + "," + pnlimgC4.Text;
                }
            }

            if (pnlimgC5.Text != "" && pnlimgC5.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgC5.Text;
                }
                else
                {
                    str = str + "," + pnlimgC5.Text;
                }
            }
            if (pnlimgC6.Text != "" && pnlimgC6.Text != null)
            {
                if (str == "")
                {
                    str = pnlimgC6.Text;
                }
                else
                {
                    str = str + "," + pnlimgC6.Text;
                }
            }

            return str;
        }



    }
}
