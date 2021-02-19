using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.goods
{
    public partial class Item : UserControl
    {
       
        public Item(string str)
        {
            InitializeComponent();
            this.Text = str;
        }

        private void Item_Load(object sender, EventArgs e)
        {

        }

        private void Item_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(this.Text, new Font("微软雅黑", 11), Brushes.Black, new Point(5, (this.Height - 11) / 2));
            e.Graphics.DrawLine(Pens.LightGray, 0, this.Height - 1, this.Width - 1, this.Height - 1);
        }

    }
}
