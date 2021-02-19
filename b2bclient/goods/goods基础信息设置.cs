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
    public partial class goods基础信息设置 : UserControl
    {
        public goods基础信息设置()
        {
            InitializeComponent();
            //
            control.ClickActive.addActive(pnl_theme);

            BLL.IGoodsCls bllcls = new BLL.GoodsCls();
            var lst = bllcls.GetListForMenu();
            comboBox1.DataSource = lst;

            init_pnl_theme();
        }

        private void init_pnl_theme() 
        {
            pnl_theme.Controls.Clear();
            BLL.IGoods bll = new BLL.Goods();
            var lst = bll.GetThemeList();
            if (lst != null && lst.Count > 0) 
            {
                var x = 20;
                foreach(body.theme item in lst)
                {
                    CheckBox box = new CheckBox();
                    box.Text = item.theme_name;
                    box.Tag = item.theme_code;
                    box.Checked = false;
                    box.Font = new Font("SimSun", 12, FontStyle.Regular);
                    box.Location = new Point(x,20);
                    x = x + box.Size.Width + 20;
                    pnl_theme.Controls.Add(box);
                }
            }
        }

    }
}
