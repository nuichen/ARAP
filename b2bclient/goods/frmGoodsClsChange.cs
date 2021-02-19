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
    public partial class frmGoodsClsChange : Form
    {
        BLL.IGoodsCls bll;
        private string  cls_id ="";
        public frmGoodsClsChange(string cls_id, Model.goods_cls item)
        {
            InitializeComponent();
            //
            control.FormEsc.Bind(this);
            control.ClickActive.addActive(pnl_ok);
            control.ClickActive.addActive(pnl_cancel);

            bll = new BLL.GoodsCls();

            
            //
            this.cls_id = cls_id;
            if (item != null)
            {
                lbl_cls_no.Text = item.cls_no;
                lbl_cls_name.Text = item.cls_name;
                if (item.is_show_mall == "1") checkBox1.Checked = true;
            }
        }

        private void pnl_ok_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                //
                string is_show_mall = "0";
                if (checkBox1.Checked) is_show_mall = "1";
                bll.Change(cls_id, lbl_cls_no.Text, lbl_cls_name.Text, is_show_mall, "");
                this.DialogResult = DialogResult.OK;
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

        private void pnl_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
