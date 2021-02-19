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
using com.google.zxing.common;
using com.google.zxing.qrcode.decoder;
using System.Drawing.Imaging;

namespace b2bclient.market
{
    public partial class frmConfig : Form
    {
        BLL.IConfig bll = new BLL.Config();
        public frmConfig(bool enable)
        {
            InitializeComponent();
            GlobalData.InitForm(this);
            //
            string kefu_tel = "";
            string peisong_tel = "";
            string wx_notice = "";
            string peisong_jl = "3";
            string peisong_amt = "";
            string take_fee = "";
            string kefu_qrcode = "";
            string full_kefu_qrcode = "";
            string print_count = "1";
            string start_time = "";
            string end_time = "";
            bll.read(out kefu_tel, out peisong_tel, out wx_notice, out peisong_jl, out peisong_amt, out take_fee, out kefu_qrcode
                , out full_kefu_qrcode, out print_count, out start_time, out end_time);
            //
            this.kefu_tel.Text = kefu_tel;
            this.peisong_tel.Text = peisong_tel;
            this.wx_notice.Text = wx_notice;
            this.low_amt.Text = (peisong_amt == "" ? "0" : peisong_amt);
            this.take_fee.Text = (take_fee == "" ? "0" : take_fee);
            if (kefu_qrcode != "") {
                pnlimgA.Text = kefu_qrcode;
                pnlimgA.BackgroundImage = ImageHelper.getImage(full_kefu_qrcode);
            }

            this.txt_end_time.Text = end_time;
            this.txt_start_time.Text = start_time;

            this.pnl_qrcode.BackgroundImage = qrcode(AppSetting.wxsvr + "/login.html", this.pnl_qrcode.Width, this.pnl_qrcode.Height);
        }

        private void pnlOK_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                var kefu_tel = this.kefu_tel.Text;
                var peisong_tel = this.peisong_tel.Text;
                var wx_notice = this.wx_notice.Text;
                var peisong_jl = "0";
                var peisong_amt = this.low_amt.Text;
                var take_fee = this.take_fee.Text;
                var kefu_qrcode = pnlimgA.Text;
                var start_time = txt_start_time.Text;
                var end_time = txt_end_time.Text;

                bll.save(kefu_tel, peisong_tel, wx_notice, peisong_jl, peisong_amt, take_fee, kefu_qrcode, "1", start_time, end_time);
                //
                Program.frmMsg("保存成功");
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
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

        private void low_amt_TextChanged(object sender, EventArgs e)
        {
            lbl_low_amt.Text = low_amt.Text;
        }

        private void pnlimgA_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new OpenFileDialog();
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Image img = Image.FromFile(f.FileName);
                    string str = "";
                    ImageHelper.UploadImage(img, out str);
                    pnlimgA.Text = str;
                    pnlimgA.BackgroundImage = img;

                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
        }

        private Bitmap qrcode(string content, int width, int height)
        {
            Color fgColor = System.Drawing.Color.Green; //前景色
            Color bgColor = this.pnl_qrcode.BackColor; //背景色
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
        

        private void pnl_save_Click(object sender, EventArgs e)
        {
            var frm = new frmDownLoadQRCode(AppSetting.wxsvr + "/login.html");
            frm.ShowDialog();
        }

        private void pnl_url_Click(object sender, EventArgs e)
        {
            var txt = new TextBox();
            txt.Text = AppSetting.wxsvr + "/login.html";
            txt.SelectAll();
            txt.Copy();
            string msg = "已复制链接到剪切版\r\n该链接可用于设置公众号菜单";

            Program.frmMsg(msg);
        }

    }
}
