using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.BLL.OnLine;
using IvyBack.IBLL.OnLine;

namespace IvyBack.SysForm.market
{
    public partial class MemberAdEdit : Form
    {
        public MemberAdEdit()
        {
            InitializeComponent();
            cons.AddClickAct.Add(pnlOK);
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
                    IGoods bllg = new Goods();
                    bllg.UploadImage(img, out str);
                    pnlimgA.Text = str;
                    pnlimgA.BackgroundImage = img;
                }
            }
            catch (Exception ex)
            {
                var frm = new MsgForm(ex.Message);
                frm.ShowDialog();
            }
        }

        private void pnlOK_Click(object sender, EventArgs e)
        {
            try
            {
                var ad_href = txt_name.Text;
                var ad_img_url = pnlimgA.Text;
                if (ad_img_url == null || ad_img_url == "")
                {
                    var frm2 = new MsgForm("请上传图片");
                    frm2.ShowDialog();
                    return;
                }
                IAd bll = new Ad();
                bll.AddMemberAd(ad_href, ad_img_url);
                var frm = new MsgForm("新增成功！");
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                var frm = new MsgForm(ex.Message);
                frm.ShowDialog();
            }
        }

    }
}
