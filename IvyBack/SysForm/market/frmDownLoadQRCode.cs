using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using com.google.zxing;
using com.google.zxing.qrcode.decoder;
using com.google.zxing.common;
using System.Drawing.Imaging;
using System.Linq.Expressions;

namespace IvyBack.SysForm.market
{
    public partial class frmDownLoadQRCode : Form
    {
        string url = "";
        public frmDownLoadQRCode(string url)
        {
            InitializeComponent();
            //
            this.url = url;
            cons.AddClickAct.Add(pnlexit);
            //
            foreach (System.Windows.Forms.Control con in pnl.Controls)
            {
                con.BackColor = Color.White;
                cons.AddClickAct.Add(con);
                con.Click += this.con_click;
                con.Paint += this.lbl_Paint;
            }
        }

        private frmDiv frm = new frmDiv();
        public new DialogResult ShowDialog()
        {
            frm.Show();
            return base.ShowDialog();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            frm.Visible = false;
        }

        private void con_click(object sender, EventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            Bitmap bmp = null;
            var f = new SaveFileDialog();
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            f.InitialDirectory = dir;
            f.Filter = "*.jpg|*.jpg";
            if (con.Text == "150*150")
            {
                bmp = qrcode(url, 150, 150);
                f.FileName = "随心点二维码150";
            }
            if (con.Text == "200*200")
            {
                bmp = qrcode(url, 200, 200);
                f.FileName = "随心点二维码200";
            }
            if (con.Text == "300*300")
            {
                bmp = qrcode(url, 300, 300);
                f.FileName = "随心点二维码300";
            }
            if (con.Text == "400*400")
            {
                bmp = qrcode(url, 400, 400);
                f.FileName = "随心点二维码400";
            }
            if (con.Text == "500*500")
            {
                bmp = qrcode(url, 500, 500);
                f.FileName = "随心点二维码500";
            }
            if (con.Text == "600*600")
            {
                bmp = qrcode(url, 600, 600);
                f.FileName = "随心点二维码600";
            }


            if (f.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(f.FileName);
                this.Close();
            }
        }

        private void lbl_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Control con = (System.Windows.Forms.Control)sender;
            e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(0, 0, con.Width - 1, con.Height - 1));
        }

        private void pnlexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Bitmap qrcode(string content, int width, int height)
        {
            Color fgColor = System.Drawing.Color.Black; //前景色
            Color bgColor = Color.White; //背景色
            Hashtable hints = new Hashtable();
            hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);//容错能力
            hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");//字符集
            ByteMatrix matrix = new MultiFormatWriter().encode(content, BarcodeFormat.QR_CODE, width, height, hints);
            Bitmap bmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bmap.SetPixel(x, y, matrix.get_Renamed(x, y) != -1 ? fgColor : bgColor);
                }
            }
            return bmap;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            return true;
        }
    }
}
