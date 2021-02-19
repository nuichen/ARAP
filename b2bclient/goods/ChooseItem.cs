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
    public partial class ChooseItem : Form
    {
        private List<System.Windows.Forms.Control> lstcon = new List<System.Windows.Forms.Control>();
        public ChooseItem(List<string> lst)
        {
            InitializeComponent();
            //
            control.ClickActive.addActive(pnlexit);
            control.ClickActive.addActive(pnl_up);
            control.ClickActive.addActive(pnl_down);
            control.ClickActive.addActive(pnl_cancel);
            control.FormEsc.Bind(this);
            //
            int top = 0;
            this.pnl_cls.BackColor = Color.White ;
            foreach (string str in lst)
            {
                var item = new Item(str);
                item.Width = pnl_cls.Width;
                item.Top = top;
                item.BackColor = Color.White;
                top = top + item.Height + 1;
                this.pnl_cls.Controls.Add(item);
                item.Click += this.item_click;
                control.ClickActive.addActive(item);
                lstcon.Add(item);
            }
            
        }

        public void setTitle(string title)
        {
            lbltitle.Text = title;
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


        private void pnlexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnl_cancel_Click(object sender, EventArgs e)
        {
            //this.Close();
            SelectIndex = 0;
            this.DialogResult = DialogResult.OK;
        }

        public string SelectItem;
        public int SelectIndex=0;
        private void item_click(object sender, EventArgs e)
        {
            var con = (System.Windows.Forms.Control)sender;
            SelectItem = con.Text;
            SelectIndex = lstcon.IndexOf(con);
            this.DialogResult = DialogResult.OK;
        }

        private bool bup = false;
        private bool bdown = false;

        private void pnl_up_MouseDown(object sender, MouseEventArgs e)
        {
            control.Roll.up(20, pnl_cls);

            bup = true;
        }

        private void pnl_up_MouseUp(object sender, MouseEventArgs e)
        {
            bup = false;
        }

        private void pnl_down_MouseDown(object sender, MouseEventArgs e)
        {
            control.Roll.down(20, pnl_cls);

            bdown = true;

        }

        private void pnl_down_MouseUp(object sender, MouseEventArgs e)
        {
            bdown = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bup == true)
            {
                control.Roll.up(20, pnl_cls);
            }
            if (bdown == true)
            {
                control.Roll.down(20, pnl_cls);
            }
        }

        private void ChooseItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                pnl_cancel_Click(null, null);
            }
        }

    }
}
