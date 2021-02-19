using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.market
{
    public partial class AdEdit : Form
    {
        BLL.IAd bll = new BLL.Ad();
        BLL.IGoods bllg = new BLL.Goods();
        private string ad_id = "";
        private Model.ad ad;
        public AdEdit()
        {
            InitializeComponent();
            GlobalData.InitForm(this);
        }


        public AdEdit(string ad_id)
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            this.ad_id = ad_id;
            ad = bll.Select(ad_id);
            txt_ad_text.Text = ad.ad_text;
            txt_name.Text = ad.ad_name;
            txt_goods_nos.Text = ad.goods_ids;
            pnlimgA.Text = ad.title_img;
            pnlimgA.BackgroundImage = bllg.getImage(AppSetting.imgsvr + ad.title_img);
            string[] arr = ad.detail_img.Split(',', '，');
            if (arr.Length >= 1)
            {
                pnlimgC1.Text = arr[0];
                pnlimgC1.BackgroundImage = bllg.getImage(AppSetting.imgsvr + arr[0]);
                string url = AppSetting.imgsvr + arr[0];
            }
            if (arr.Length >= 2)
            {
                pnlimgC2.Text = arr[1];
                pnlimgC2.BackgroundImage = bllg.getImage(AppSetting.imgsvr + arr[1]);
            }
            if (arr.Length >= 3)
            {
                pnlimgC3.Text = arr[2];
                pnlimgC3.BackgroundImage = bllg.getImage(AppSetting.imgsvr + arr[2]);
            }
            if (arr.Length >= 4)
            {
                pnlimgC4.Text = arr[3];
                pnlimgC4.BackgroundImage = bllg.getImage(AppSetting.imgsvr + arr[3]);
            }
            if (arr.Length >= 5)
            {
                pnlimgC5.Text = arr[4];
                pnlimgC5.BackgroundImage = bllg.getImage(AppSetting.imgsvr + arr[4]);
            }
            if (arr.Length >= 6)
            {
                pnlimgC6.Text = arr[5];
                pnlimgC6.BackgroundImage = bllg.getImage(AppSetting.imgsvr + arr[5]);
            }
        }

        private void pnlCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void pnlimg1_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    string str = "";
                    bllg.UploadImage(img, out str);
                    pnlimgA.Text = str;
                    pnlimgA.BackgroundImage = img;
                    
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }

        }

        private void pnlimgc1_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    string str = "";
                    bllg.UploadImage(img, out str);
                    pnlimgC1.Text = str;
                    pnlimgC1.BackgroundImage = img;

                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }

        }

        private void pnlimgc2_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    string str = "";
                    bllg.UploadImage(img, out str);
                    pnlimgC2.Text = str;
                    pnlimgC2.BackgroundImage = img;

                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private void pnlimgc3_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    string str = "";
                    bllg.UploadImage(img, out str);
                    pnlimgC3.Text = str;
                    pnlimgC3.BackgroundImage = img;

                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }
        private void pnlimgc4_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    string str = "";
                    bllg.UploadImage(img, out str);
                    pnlimgC4.Text = str;
                    pnlimgC4.BackgroundImage = img;

                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }

        }
        private void pnlimgc5_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    string str = "";
                    bllg.UploadImage(img, out str);
                    pnlimgC5.Text = str;
                    pnlimgC5.BackgroundImage = img;

                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }

        }
        private void pnlimgc6_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    string str = "";
                    bllg.UploadImage(img, out str);
                    pnlimgC6.Text = str;
                    pnlimgC6.BackgroundImage = img;

                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }

        }

        private void pnlOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (ad_id.Length == 0)
                {
                    ad = new Model.ad();
                    ad.ad_type = 0;
                    ad.ad_name = txt_name.Text;
                    ad.ad_text = txt_ad_text.Text;
                    ad.goods_ids = txt_goods_nos.Text;
                    ad.title_img = pnlimgA.Text;
                    ad.detail_img = pnlimgC1.Text + "," + pnlimgC2.Text + "," + pnlimgC3.Text + "," + pnlimgC4.Text + "," + pnlimgC5.Text + "," + pnlimgC6.Text;
                    bll.Add(ad);
                    Program.frmMsg("新增成功!");
                    this.Close();
                }
                else
                {
                    ad.ad_name = txt_name.Text;
                    ad.ad_text = txt_ad_text.Text;
                    ad.goods_ids = txt_goods_nos.Text;
                    ad.title_img = pnlimgA.Text;
                    ad.detail_img = pnlimgC1.Text + "," + pnlimgC2.Text + "," + pnlimgC3.Text + "," + pnlimgC4.Text + "," + pnlimgC5.Text + "," + pnlimgC6.Text;
                    bll.Change(ad);
                    Program.frmMsg("修改成功!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                
                Program.frmMsg(ex.Message);
            }
        }
    }
}
