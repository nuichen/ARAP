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
    public partial class GoodsCls : UserControl
    {
        public string cls_id;
        public string cls_no;
        public string cls_name;
        private string drawstring = "";
        public GoodsCls(string cls_id,string cls_no,string cls_name)
        {
            InitializeComponent();
            //
            this.cls_id = cls_id;
            this.cls_no = cls_no;
            this.cls_name = cls_name;
            this.drawstring  = cls_no + "/" + cls_name;
        }

        private void GoodsCls_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(drawstring, new Font("微软雅黑",11), Brushes.Black, new Point(10, (this.Height - 11) / 2));
            e.Graphics.DrawLine(Pens.LightGray, 0, this.Height - 1, this.Width - 1, this.Height - 1);
        }


    }
}
