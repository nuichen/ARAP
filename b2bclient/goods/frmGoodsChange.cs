using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.goods
{
    public partial class frmGoodsChange : Form
    {
        BLL.IGoods bll = new BLL.Goods();
        string goods_id="";
        goods基础信息设置 goods基础信息设置;
        goods上传图片 goods上传图片;
        goods属性组定义 goods属性组定义;

        private ChangeHandler chg;

        string small_img_url = "";
        string small_img_full_url = "";
        string large_img_url = "";
        string large_img_full_url = "";
        string detail_img_url = "";
        string detail_img_full_url = "";
        public frmGoodsChange(string goods_id)
        {
            InitializeComponent();
            //
            try
            {
                chg = new ChangeHandler(changed);
                goods基础信息设置 = new goods.goods基础信息设置();
                goods上传图片 = new goods.goods上传图片();

                this.goods_id = goods_id;
                refreshData("");

                control.FormEsc.Bind(this);
                control.ClickActive.addActive(pnlOK);
                control.ClickActive.addActive(pnlCancel);
                //
                CreateRec();
                //
                pnl.Controls.Add(goods基础信息设置);
                pnl.Controls.Add(goods上传图片);
                pnl.Controls.Add(goods属性组定义);
                goods基础信息设置.BringToFront();
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
           
        }

        public void refreshData(string g_no) 
        {
            string goods_no = "";
            string goods_name = "";
            string long_name = "";
            string cls_id = "";
            string cls_name = "";
            string themes = "";
            string text = "";
            string status = "";
            string is_show_mall = "";
            List<Model.goods_std> lststd;
            if (g_no == "")
            {
                bll.GetInfo(goods_id, out goods_no, out goods_name, out long_name, out cls_id, out cls_name, out small_img_url, out small_img_full_url, out large_img_url
                    , out large_img_full_url, out detail_img_url, out detail_img_full_url, out themes, out text, out status,out is_show_mall, out lststd);
            }
            else
            {
                var g_id = "";
                bll.GetInfo(g_no, out g_id, out goods_no, out goods_name, out long_name, out cls_id, out cls_name, out small_img_url, out small_img_full_url, out large_img_url, 
                    out large_img_full_url, out detail_img_url, out detail_img_full_url, out themes, out text, out status,out is_show_mall, out lststd);
                this.goods_id = g_id;
            }
            //
            goods基础信息设置.comboBox1.SelectedValue = cls_id;
            goods基础信息设置.txt_no.Text = goods_no;
            goods基础信息设置.txt_name.Text = goods_name;
            goods基础信息设置.txt_long_name.Text = long_name;
            goods基础信息设置.is_show_mall.Checked = (is_show_mall == "1" ? true : false);
            if (themes != "") {
                var arr = themes.Split(',');
                if (arr.Length > 0) {
                    for (var i = 0; i < arr.Length; i++) 
                    {
                        var theme_code = arr[i];
                        foreach (Control obj in goods基础信息设置.pnl_theme.Controls) 
                        {
                            var chk = (CheckBox)obj;
                            if (theme_code == chk.Tag.ToString()) chk.Checked = true;
                        }
                    }
                }
            }
            
            goods基础信息设置.txt_context.Text = text;
            //
            goods上传图片.pnlimgA.Text = small_img_url;

            if (large_img_url == "")
            {

            }
            else if (large_img_url.Contains(",") == false)
            {
                goods上传图片.pnlimgB1.Text = large_img_url;
            }
            else
            {
                string[] arr = large_img_url.Split(',');
                if (arr.Length >= 1)
                {
                    goods上传图片.pnlimgB1.Text = arr[0];
                }
                if (arr.Length >= 2)
                {
                    goods上传图片.pnlimgB2.Text = arr[1];
                }
                if (arr.Length >= 3)
                {
                    goods上传图片.pnlimgB3.Text = arr[2];
                }
            }

            if (detail_img_url == "")
            {

            }
            else if (detail_img_url.Contains(",") == false)
            {
                goods上传图片.pnlimgC1.Text = detail_img_url;
            }
            else
            {
                string[] arr = detail_img_url.Split(',');
                if (arr.Length >= 1)
                {
                    goods上传图片.pnlimgC1.Text = arr[0];
                }
                if (arr.Length >= 2)
                {
                    goods上传图片.pnlimgC2.Text = arr[1];
                }
                if (arr.Length >= 3)
                {
                    goods上传图片.pnlimgC3.Text = arr[2];
                }
                if (arr.Length >= 4)
                {
                    goods上传图片.pnlimgC4.Text = arr[3];
                }
                if (arr.Length >= 5)
                {
                    goods上传图片.pnlimgC5.Text = arr[4];
                }
                if (arr.Length >= 6)
                {
                    goods上传图片.pnlimgC6.Text = arr[5];
                }
            }
            goods属性组定义 = new goods.goods属性组定义(lststd);
        }

        void changed(string stype, string value1,string value2)
        {
            goods属性组定义.Refresh();
        }
 
        public Model.goods getItem()
        {
            var item=new  Model.goods();
            item.goods_id = goods_id;
            item.goods_no = goods基础信息设置.txt_no.Text;
            item.goods_name = goods基础信息设置.txt_name.Text;
            if (goods基础信息设置.comboBox1.SelectedValue != null)
            {
                item.cls_id = goods基础信息设置.comboBox1.SelectedValue.ToString();
            }
            var t_is_show_mall = "0";
            if (goods基础信息设置.is_show_mall.Checked) t_is_show_mall = "1";
            item.is_show_mall = t_is_show_mall;
            string themes = "";
            foreach (Control obj in goods基础信息设置.pnl_theme.Controls)
            {
                var chk = (CheckBox)obj;
                if (chk.Checked) themes += chk.Tag.ToString() + ",";
            }
            if (themes.Length > 0) themes = themes.Substring(0, themes.Length - 1);
            item.themes = themes;
            return item;
        }

        private int pnlIndex = 1;
        Rectangle rec1;
        Rectangle rec2;
        Rectangle rec3;
        
        private void CreateRec()
        {
            decimal w = (decimal)pnltop.Width / (decimal)3;
            rec1 = new Rectangle(0, 0, (int)w, pnltop.Height);
            rec2 = new Rectangle((int)w, 0, (int)w, pnltop.Height);
            rec3 = new Rectangle((int)(w * 2), 0, pnltop.Width - (int)(w *2), pnltop.Height);
            
        }

        private void pnltop_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(207, 238, 233));
           
            if (pnlIndex == 1)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(31, 187, 133)),rec1);
            }
            else if (pnlIndex == 2)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(31, 187, 133)), rec2);
            }
            else if (pnlIndex == 3)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(31, 187, 133)), rec3);
            }
           
            //
            var sf = new System.Drawing.StringFormat();
            sf.Alignment = System.Drawing.StringAlignment.Center;
            sf.LineAlignment = System.Drawing.StringAlignment.Center;
            e.Graphics.DrawString("基础信息设置", new Font("微软雅黑", 12), Brushes.Black, rec1,sf);
            e.Graphics.DrawString("上传图片", new Font("微软雅黑", 12), Brushes.Black, rec2, sf);
            e.Graphics.DrawString("属性组定义", new Font("微软雅黑", 12), Brushes.Black, rec3, sf);
          
        }

        private void pnltop_Click(object sender, EventArgs e)
        {
             
        }

        private void pnltop_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                //

                if (rec1.Contains(new Point(e.X, e.Y)))
                {
                    pnlIndex = 1;
                    goods基础信息设置.BringToFront();
                }
                else if (rec2.Contains(new Point(e.X, e.Y)))
                {
                    pnlIndex = 2;
                    if (goods上传图片.pnlimgA.BackgroundImage == null)
                    {
                        goods上传图片.pnlimgA.BackgroundImage = bll.getImage(small_img_full_url);
                    }
                    if (large_img_full_url != "")
                    {
                        string[] arr = large_img_full_url.Split(',');
                        if (goods上传图片.pnlimgB1.BackgroundImage == null && arr.Length >= 1)
                        {
                            goods上传图片.pnlimgB1.BackgroundImage = bll.getImage(arr[0]);
                        }
                        if (goods上传图片.pnlimgB2.BackgroundImage == null && arr.Length >= 2)
                        {
                            goods上传图片.pnlimgB2.BackgroundImage = bll.getImage(arr[1]);
                        }
                        if (goods上传图片.pnlimgB3.BackgroundImage == null && arr.Length >= 3)
                        {
                            goods上传图片.pnlimgB3.BackgroundImage = bll.getImage(arr[2]);
                        }
                    }
                    if (detail_img_full_url != "")
                    {
                        string[] arr = detail_img_full_url.Split(',');
                        if (goods上传图片.pnlimgC1.BackgroundImage == null && arr.Length >= 1)
                        {
                            goods上传图片.pnlimgC1.BackgroundImage = bll.getImage(arr[0]);
                        }
                        if (goods上传图片.pnlimgC2.BackgroundImage == null && arr.Length >= 2)
                        {
                            goods上传图片.pnlimgC2.BackgroundImage = bll.getImage(arr[1]);
                        }
                        if (goods上传图片.pnlimgC3.BackgroundImage == null && arr.Length >= 3)
                        {
                            goods上传图片.pnlimgC3.BackgroundImage = bll.getImage(arr[2]);
                        }
                        if (goods上传图片.pnlimgC4.BackgroundImage == null && arr.Length >= 4)
                        {
                            goods上传图片.pnlimgC4.BackgroundImage = bll.getImage(arr[3]);
                        }
                        if (goods上传图片.pnlimgC5.BackgroundImage == null && arr.Length >= 5)
                        {
                            goods上传图片.pnlimgC5.BackgroundImage = bll.getImage(arr[4]);
                        }
                        if (goods上传图片.pnlimgC6.BackgroundImage == null && arr.Length >= 6)
                        {
                            goods上传图片.pnlimgC6.BackgroundImage = bll.getImage(arr[5]);
                        }
                    }
                  
                    goods上传图片.BringToFront();
                }
                else if (rec3.Contains(new Point(e.X, e.Y)))
                {
                    pnlIndex = 3;
                    goods属性组定义.BringToFront();
                }
               
                pnltop.Refresh();
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

        private void pnlOK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //
                string goods_no = goods基础信息设置.txt_no.Text;
                string goods_name = goods基础信息设置.txt_name.Text;
                string long_name = goods基础信息设置.txt_long_name.Text;
                string cls_id = goods基础信息设置.comboBox1.SelectedValue.ToString() ;
                string small_img_url = goods上传图片.small_img_url();
                string large_img_url = goods上传图片.large_img_url();
                string detail_img_url = goods上传图片.detail_img_url();
                string themes = "";
                foreach (Control obj in goods基础信息设置.pnl_theme.Controls)
                {
                    var chk = (CheckBox)obj;
                    if (chk.Checked) themes += chk.Tag.ToString() + ",";
                }
                string text = goods基础信息设置.txt_context.Text;
                string is_show_mall = "0";
                if (goods基础信息设置.is_show_mall.Checked) is_show_mall = "1";
     
                var lststd = goods属性组定义.GetData();
               
                if (goods_id == "")
                {
                    bll.Add(goods_no,goods_name,long_name,cls_id,small_img_url,large_img_url,detail_img_url,themes,text,is_show_mall,lststd);
                }
                else
                {
                    bll.Change(goods_id,goods_no,goods_name,long_name,cls_id,small_img_url,large_img_url,detail_img_url,themes,text,is_show_mall,lststd);
                }
                this.DialogResult = DialogResult.OK;
                Program.frmMsg("提交成功!");
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void pnlCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlbottom_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Gray);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            e.Graphics.DrawLine(p, 0, 0, pnlbottom.Width, 0);
        }

    }
}
