using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IvyBack.BLL.OnLine;
using IvyBack.Helper;
using IvyBack.IBLL.OnLine;
using IvyBack.MainForm;

namespace IvyBack.SysForm.market
{
    public partial class AdEdit : Form
    {
        IAd bll = new Ad();
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
        }

        private void Init()
        {
            ad = bll.Select(ad_id);

            this.BeginInvoke(new Action(() =>
            {
                txt_ad_text.Text = ad.ad_text;
                txt_name.Text = ad.ad_name;
                txt_goods_nos.Text = ad.goods_ids;
                pnlimgA.Text = ad.title_img;
                pnlimgA.GetGoodsImage(AppSetting.imgsvr + ad.title_img);
                string[] arr = ad.detail_img.Split(',', '，');
                if (arr.Length >= 1)
                {
                    pnlimgC1.Text = arr[0];
                    pnlimgC1.GetGoodsImage(AppSetting.imgsvr + arr[0]);
                    string url = AppSetting.imgsvr + arr[0];
                }
                if (arr.Length >= 2)
                {
                    pnlimgC2.Text = arr[1];
                    pnlimgC2.GetGoodsImage(AppSetting.imgsvr + arr[1]);
                }
                if (arr.Length >= 3)
                {
                    pnlimgC3.Text = arr[2];
                    pnlimgC3.GetGoodsImage(AppSetting.imgsvr + arr[2]);
                }
                if (arr.Length >= 4)
                {
                    pnlimgC4.Text = arr[3];
                    pnlimgC4.GetGoodsImage(AppSetting.imgsvr + arr[3]);
                }
                if (arr.Length >= 5)
                {
                    pnlimgC5.Text = arr[4];
                    pnlimgC5.GetGoodsImage(AppSetting.imgsvr + arr[4]);
                }
                if (arr.Length >= 6)
                {
                    pnlimgC6.Text = arr[5];
                    pnlimgC6.GetGoodsImage(AppSetting.imgsvr + arr[5]);
                }

                frmLoad.LoadClose(this);
            }));

        }

        private void pnlCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    var frm = new MsgForm("新增成功！");
                    frm.ShowDialog();
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
                    var frm = new MsgForm("修改成功！");
                    frm.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                var frm = new MsgForm(ex.Message);
                frm.ShowDialog();
            }
        }

        private void AdEdit_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ad_id)) return;

            frmLoad.LoadWait(this);
            Init();
        }

    }
}
