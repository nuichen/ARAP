using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.control
{
    public partial class OrderLine : UserControl
    {
        public delegate void  DisableHandler(object sender);
        public event DisableHandler Disable;
        BLL.IOrder bll = new BLL.Order();
        private body.wm_order_item line;
        public OrderLine(body.wm_order_item line,int index)
        {
            InitializeComponent();
            this.line = line;
            //
            this.lblindex.Text = index.ToString();
            this.lblgoodsname.Text =line. goods_name;
            this.lblremark.Text = (line.color  + " " + line.size  ).Trim();
            if (this.lblremark.Text == "")
            {
                this.panel2.Visible = false;
            }
            this.lblqty.Text =   line.qty;
            this.lblamount.Text = "￥" + line.amount;
            if (line.enable == "0")
            {
                this.panel1.BackgroundImage = pictureBox1.Image;
                this.panel1.Enabled = false;
                this.panel2.BackgroundImage = pictureBox2.Image;
                this.lblindex.ForeColor = Color.Gray;
                this.lblgoodsname.ForeColor = Color.Gray;
                this.lblremark.ForeColor = Color.Gray;
                this.lblqty.ForeColor = Color.Gray;
                this.lblamount.ForeColor = Color.Gray;
            }
            
            //
            control.ClickActive.addActive(this.panel1);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                //
                if (line.enable == "0")
                {
                    throw new Exception("该行已经失效");
                }
                //
                if (Program.frmMsgYesNo("确认失效该行?") == DialogResult.No)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    return;
                }
                //
                bll.DisableRow(line.ord_id, line.row_index);
                //
                line.enable = "0";
                this.panel1.BackgroundImage = pictureBox1.Image;
                this.panel1.Enabled = false;
                this.panel2.BackgroundImage = pictureBox2.Image;
                this.lblindex.ForeColor = Color.Gray;
                this.lblgoodsname.ForeColor = Color.Gray;
                this.lblremark.ForeColor = Color.Gray;
                this.lblqty.ForeColor = Color.Gray;
                this.lblamount.ForeColor = Color.Gray;
                //
                if (Disable != null)
                {
                    Disable.Invoke(this);
                }
            }
            catch (Exception ex)
            {
                Program.frmMsg(ex.Message);
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

    }
}
