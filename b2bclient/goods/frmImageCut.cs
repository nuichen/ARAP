using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace b2bclient.goods
{
    public partial class frmImageCut : Form
    {
        private Image image;
        private Point p1;
        private Point p2;
        private Rectangle rect;
        private Point pz;
        //private Image img;
        //private Image imgtmp;
        //private decimal rate;
        private int wish_width = 0;
        private int wish_height = 0;
        public frmImageCut(Image img,int wish_width,int wish_height)
        {
            InitializeComponent();
            pictureBox1.Image = img;
            this.image = img;
        //    //
        //    this.img = img;
            this.wish_width = wish_width;
            this.wish_height = wish_height;
        //    //
        //    int flag = 0;
        //    if (img.Width > img.Height)
        //    {
        //        flag = 1;
        //    }
        //    else
        //    {
        //        flag = 2;
        //    }
        //    //
        //    if (flag == 1)
        //    {
        //        rate = (decimal) panel1.Width/(decimal) img.Width;   
        //    }
        //    else
        //    {
        //        rate = (decimal) panel1.Height/(decimal) img.Height;  
        //    }
            
        //    imgtmp = new Bitmap(500, 500);
        //    Graphics g = Graphics.FromImage(imgtmp);
        //    g.Clear(Color.White);
        //    g.DrawImage(img, new Rectangle(0, 0, (int)((decimal)img.Width * rate), (int)((decimal)img.Height * rate)), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
        //    //
        //    control.FormEsc.Bind(this);
        //    control.ClickActive.addActive(pnlOK);
        //    control.ClickActive.addActive(pnlCancel);
        }

       

        //private void frmImageCut_Load(object sender, EventArgs e)
        //{

        //}

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{
        //    e.Graphics.DrawImage(imgtmp, new Point(0, 0));
        //}

        public Image getImage()
        {
            return image;
        }

        //bool bdown = false;
        //Point p = new Point(0, 0);
        //Point p2 = new Point(0, 0);
        //private void panel1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    bdown = true;
        //    p = new Point(e.X, e.Y);
        //}

        //private Rectangle rec;
        //private void panel1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if(bdown==true)
        //    {
        //           p2 = new Point(e.X, e.Y);
        //           if (p2.X < 0)
        //           {
        //               p2.X = 0;
        //           }
        //           if (p2.Y < 0)
        //           {
        //               p2.Y = 0;
        //           }
        //           if (p2.X > panel1.Width)
        //           {
        //               p2.X = panel1.Width;
        //           }
        //           if (p2.Y > panel1.Height)
        //           {
        //               p2.Y = panel1.Height;
        //           }
        //           int minx;
        //           int miny;
        //           int w;
        //           int h;
        //           if (p.X > p2.X)
        //           {
        //               minx = p2.X;
        //           }
        //           else
        //           {
        //               minx = p.X;
        //           }
        //           if (p.Y > p2.Y)
        //           {
        //               miny = p2.Y;
        //           }
        //           else
        //           {
        //               miny = p.Y;
        //           }
        //           w = Math.Abs(p.X - p2.X);
        //           h = Math.Abs(p.Y - p2.Y);
        //           int w2 = 0;
        //           int h2 = 0;
        //           //
        //           if (w > 5 && h > 5)
        //           {
        //               if ((decimal)w / (decimal)h > (decimal)wish_width / (decimal)wish_height)
        //               {
        //                   w2 = (int)((decimal)wish_width / (decimal)wish_height * (decimal)h);
        //                   h2 = h;
        //               }
        //               else
        //               {
        //                   h2 =(int)( (decimal)w / ((decimal)wish_width / (decimal)wish_height));
        //                   w2 = w;
        //               }
        //           }
        //           Rectangle rec;
        //           rec = new Rectangle(minx, miny, w2, h2);
        //           if (p.X > p2.X)
        //           {
        //               rec.X = rec.X - (w2 - w);
        //           }
        //           if (p.Y > p2.Y)
        //           {
        //               rec.Y = rec.Y - (h2 - h);
        //           }
                   
                 
        //           if (this.rec != null)
        //           {
        //               Rectangle rectmp = new Rectangle(this.rec.Location, this.rec.Size);
        //               rectmp.Location = panel1.PointToScreen(rectmp.Location);
        //               ControlPaint.DrawReversibleFrame(rectmp, Color.Red, FrameStyle.Thick);
        //           }
        //           if (1 == 1)
        //           {
        //               Rectangle rectmp = new Rectangle(rec.Location, rec.Size);
        //               rectmp.Location = panel1.PointToScreen(rectmp.Location);
        //               ControlPaint.DrawReversibleFrame(rectmp, Color.Red, FrameStyle.Thick);
        //               this.rec = rec;
        //           }

        //    }
         
        //}

        //private void panel1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    bdown = false;
        //}

        private void pnlOK_Click(object sender, EventArgs e)
        {
            
            //try
            //{
            //    if (rec == null)
            //    {
            //        throw new Exception("请用鼠标选择剪切区域");
            //    }
            //    int x;
            //    int y;
            //    int w;
            //    int h;
            //    x =(int)((decimal) rec.X / rate);
            //    y = (int)((decimal)rec.Y / rate);
            //    w = (int)((decimal)rec.Width / rate);
            //    h = (int)((decimal)rec.Height / rate);
            //    if (w < 5 || h < 5)
            //    {
            //        throw new Exception("请用鼠标选择剪切区域");
            //    }
            //    //
            //    imgtrue = new Bitmap(w, h);
            //    Graphics g = Graphics.FromImage(imgtrue);
            //    g.Clear(Color.White);
            //    //
              
            //    if (x< img.Width && y<img.Height)
            //    {
            //        int w2=0;
            //        int h2=0;
            //        if (img.Width < x + w)
            //        {
            //            w2 = img.Width - x;
            //        }
            //        else
            //        {
            //            w2 = w;
            //        }
            //        if (img.Height < y + h)
            //        {
            //            h2 = img.Height - y;
            //        }
            //        else
            //        {
            //            h2 = h;
            //        }
            //        Rectangle rec1 = new Rectangle(0, 0, w2,h2);
            //        Rectangle rec2 = new Rectangle(x, y, w2, h2);
            //        g.DrawImage(img, rec1, rec2, GraphicsUnit.Pixel);
            //    }
             
            //    //
            //    DialogResult = DialogResult.OK;
            //}
            //catch (Exception ex)
            //{
            //    Program.frmMsg(ex.Message);
            //    frm.ShowDialog();
            //}
            try
            {
                if (p2.X == 0&&p2.Y == 0)
                {
                    DialogResult = DialogResult.OK;

                }
                else
                {
                    p1 = pz;
                    Bitmap bitmap = new Bitmap(pictureBox1.Image);
                    int width = 0;
                    int height = 0;
                    int qw = 0;
                    int qh = 0;
                    if (bitmap.Width > bitmap.Height)
                    {
                        height = (500 * bitmap.Height / bitmap.Width);
                        width = 500;
                    }
                    else
                    {
                        width = (500 * bitmap.Width / bitmap.Height);
                        height = 500;
                    }
                    Size size = new Size(width, height);
                    Bitmap bmp = new Bitmap(500, 500);
                    Graphics g = Graphics.FromImage(bmp);
                    Bitmap bitmap1 = new Bitmap(pictureBox1.Image, size);
                    Brush brush = new SolidBrush(Color.FromArgb(50, Color.White));
                    //g.FillEllipse(brush, 0,0 , 500, 500);
                    g.Clear(Color.White);
                    g.DrawImage(bitmap1, 250 - bitmap1.Width / 2, 250 - bitmap1.Height / 2);
                    //this.CreateGraphics().DrawImage(bmp, 0, 0);
                    qw = p1.X;// -(500 - width) / 2;
                    qh = p1.Y;// -(500 - height) / 2;
                    var rectp = new Rectangle(new Point(qw, qh), new Size(p2.X - p1.X, (p2.X - p1.X) * wish_height / wish_width));
                    Bitmap cloneBitmap = bmp.Clone(rectp, PixelFormat.DontCare);
                    image = cloneBitmap;
                    //cloneBitmap.Save("图片剪切.png", System.Drawing.Imaging.ImageFormat.Png); 
                    DialogResult = DialogResult.OK;
                    //MessageBox.Show("保存成功");
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("请选择图片或者在规定区域内剪切");//   
                Program.frmMsg("请选择图片或者在规定区域内剪切");
            }
        }

        private void pnlCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = image;
            }
            catch (Exception)
            {
                MessageBox.Show("请选择图片");
            }

        }
        //定义鼠标事件
        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Cross;
            this.p1 = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            //this.p2 = new Point(e.X, e.Y); Rectangle rect = new Rectangle(p1, new Size(p2.X - p1.X, p2.Y - p1.Y));
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Cursor == Cursors.Cross)
            {
                this.p2 = new Point(e.X, e.Y );
                this.pictureBox1.Invalidate();
            }
        }
        //定义鼠标事件
        
        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)//在图片框画图
        {
            Pen p = new Pen(Color.Black, 1);//画笔
            pz = p1;
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (p2.X>500)
            {
                p2.X = 500;
            }
            if (p2.X < 0)
            {
                p2.X = 0;
            }
            if (p2.X > p1.X&&p2.Y<p1.Y)
            {
                //pz = p1;
                //pz.Y = p2.Y;
                //p2.Y = p1.Y;
                p2 = p1;
            }
            if (p2.X < p1.X && p2.Y > p1.Y)
            {
                pz = p1;
                pz.X = p2.X;
                p2.X = p1.X;
            }
            if (p2.X < p1.X && p2.Y < p1.Y)
            {
                //pz = p2;
                //p2 = p1;
                p2 = p1;
            }
            rect = new Rectangle(pz, new Size(p2.X - pz.X, (p2.X - pz.X) * wish_height / wish_width));
            e.Graphics.DrawRectangle(p, rect);
            Point p11 = new Point(0, 0);
            Rectangle rect11 = new Rectangle(p11, new Size(pz.X, 500));
            Point p12 = new Point(p2.X, 0);
            Rectangle rect12 = new Rectangle(p12, new Size(500, 500));
            Point p21 = new Point(pz.X, 0);
            Rectangle rect21 = new Rectangle(p21, new Size(p2.X - pz.X, pz.Y));
            Point p22 = new Point(pz.X, pz.Y + (p2.X - pz.X) * wish_height / wish_width);
            Rectangle rect22 = new Rectangle(p22, new Size(p2.X - pz.X, 500 - pz.Y - (p2.X - pz.X) * wish_height / wish_width));
            Graphics g = pictureBox1.CreateGraphics();
            System.Drawing.Color Mycolor = System.Drawing.Color.FromArgb(128, Color.Black);//说明：1-（128/255）=1-0.5=0.5 透明度为0.5，即50%
            System.Drawing.SolidBrush sb1 = new System.Drawing.SolidBrush(Mycolor);
            Brush b = new SolidBrush(Color.FromArgb(50, Color.Brown));
            e.Graphics.FillRectangle(sb1, rect11);//填充颜色
            e.Graphics.FillRectangle(sb1, rect12);//填充颜色
            e.Graphics.FillRectangle(sb1, rect21);//填充颜色
            e.Graphics.FillRectangle(sb1, rect22);//填充颜色
            //g.Dispose();
        }


        //private void button4_Click(object sender, EventArgs e)
        //{
            
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox1.Image = image;
            }
            catch (Exception)
            {
                MessageBox.Show("请选择图片");
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
