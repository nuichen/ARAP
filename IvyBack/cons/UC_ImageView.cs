using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode.decoder;
using IvyBack.BaseForm;
using IvyBack.BLL.OnLine;
using IvyBack.Helper;
using IvyBack.IBLL.OnLine;

namespace IvyBack.cons
{
    public partial class UC_ImageView : UserControl
    {
        public UC_ImageView()
        {
            InitializeComponent();
        }

        private int image_type;
        public int Image_Type
        {
            get => image_type;
            set
            {
                image_type = value;

                switch (value)
                {
                    case 0://待上传
                        this.lblInfo.Visible = true;
                        this.pbImage.Visible = false;
                        break;
                    case 1://正在上传 刷新状态
                        this.lblInfo.Visible = false;
                        this.pbImage.Visible = true;
                        this.pbImage.Image = IvyBack.Properties.Resources.LoadWait;
                        this.pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                        break;
                    case 2://显示图片
                        this.lblInfo.Visible = false;
                        this.pbImage.Visible = true;
                        this.pbImage.Image = img;
                        this.pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                }
            }
        }


        public Image img { get; set; }

        public bool Is_Upload { get; set; } = true;

        private bool is_cut = false;

        public bool Is_Cut
        {
            get => is_cut;
            set
            {
                is_cut = value;

                this.pl_right.Visible = value;
            }
        }

        public void GetImage(string url)
        {
            Image_Type = 1;
            Action action = () =>
            {
                Image img = null;
                try
                {
                    img = ImageHelper.getImage(url);
                }
                catch
                {
                }

                this.Invoke(new Action(() =>
                {
                    this.img = img;
                    Image_Type = 2;
                }));
            };

            action.BeginInvoke(null, null);
        }

        public void GetGoodsImage(string url)
        {
            Image_Type = 1;
            Action action = () =>
            {
                IGoods bllg = new Goods();
                Image img = null;
                try
                {
                    img = bllg.getImage(AppSetting.imgsvr + url);
                }
                catch
                {
                }

                this.Invoke(new Action(() =>
                {
                    this.img = img;
                    Image_Type = 2;
                }));
            };

            action.BeginInvoke(null, null);
        }

        public void SetImage(Image img)
        {
            this.img = img;
            Image_Type = 2;
        }

        private bool upload_flag = false;
        private void UploadImage()
        {
            if (upload_flag || !Is_Upload) return;
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    upload_flag = true;
                    Action action = () =>
                    {
                        Image img = Image.FromFile(f.FileName);
                        if (Is_Cut)
                        {
                            var f2 = new frmImageCut(img, 300, 300);
                            if (f2.ShowDialog() == DialogResult.OK)
                            {
                                img = f2.getImage();
                            }
                            else
                            {
                                return;
                            }
                        }

                        string str = "";
                        try
                        {
                            this.Invoke(new Action(() => { Image_Type = 1; }));

                            ImageHelper.UploadImage(img, out str);
                        }
                        catch (Exception ex)
                        {
                            MsgForm.ShowFrom(ex);
                        }

                        this.Invoke(new Action(() =>
                        {
                            this.Text = str;
                            this.img = img;
                            upload_flag = false;
                            Image_Type = 2;
                        }));
                    };
                    action.BeginInvoke(null, null);
                }
            }
            catch (Exception ex)
            {
                var frm = new MsgForm(ex.Message);
                frm.ShowDialog();
            }
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void UC_ImageView_Paint(object sender, PaintEventArgs e)
        {
            var p = new Pen(Color.Gray) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
            e.Graphics.DrawRectangle(p, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private void pbDelImage_Click(object sender, EventArgs e)
        {
            if (Image_Type == 2)
            {
                this.img = null;
                Image_Type = 0;
            }

        }

        private void pl_right_Paint(object sender, PaintEventArgs e)
        {
            var p = new Pen(Color.Gray) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
            e.Graphics.DrawLine(p, 0, 0, 0, this.pl_right.Height);
        }
    }
}
