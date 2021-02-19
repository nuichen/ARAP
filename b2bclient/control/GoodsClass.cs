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
    public partial class GoodsClass : UserControl
    {
        public GoodsClass()
        {
            InitializeComponent();
        }

        private void GoodsClass_Load(object sender, EventArgs e)
        {

        }
         
        public string cls_id { get; set; }
        public string _cls_name;
        public string cls_name
        {
            get
            {
                return _cls_name;
            }
            set
            {
                if (_cls_name != value)
                {
                    _cls_name = value;
                    this.Refresh();
                }
            }
        }

        private bool _Checked = false;
        public bool Checked
        {
            get
            {
                
                return this._Checked;
            }
            set
            {
                if (this._Checked != value)
                {
                    this._Checked = value;
                    this.Refresh();
                }

            }
        }

        private void GoodsClass_Paint(object sender, PaintEventArgs e)
        {

            System.Drawing.StringFormat sf = new StringFormat();
            sf.Alignment = System.Drawing.StringAlignment.Center;
            sf.LineAlignment = System.Drawing.StringAlignment.Center;
            if (this.Checked == false)
            {
                e.Graphics.DrawString(cls_name, new Font("微软雅黑", 13),
                new SolidBrush(Color.FromArgb(64,64,64)), new Rectangle(0, 0, this.Width - 3, this.Height), sf);
                
            }
            if (this.Checked == true)
            {
                e.Graphics.DrawString(cls_name, new Font("微软雅黑", 13),
                new SolidBrush(Color.Red), new Rectangle(0, 0, this.Width - 3, this.Height), sf);
                e.Graphics.DrawLine(new Pen(Color.Red), 20, this.Height - 1, this.Width-20, this.Height - 1);
            }
        }
 
      
       
         
    }
}
