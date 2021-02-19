using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace b2bclient
{
    class GlobalData
    {

        public static void InitForm(Control parentC)
        {
            foreach (Control c in parentC.Controls)
            {
                if (c is Form) continue;
                if (c is Button)
                {
                    Button btn = c as Button;
                    if (btn.Text.Equals("确定"))
                    {
                        btn.BackgroundImage = Properties.Resources.按钮_1;
                        btn.ForeColor = Color.White;
                    }
                    else if (btn.Text.Equals("取消"))
                    {
                        btn.BackgroundImage = Properties.Resources.按钮_2;
                        btn.ForeColor = Color.White;
                    }
                    else
                    {
                        btn.BackgroundImage = Properties.Resources.功能按钮;
                    }
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.FlatStyle = FlatStyle.Popup;
                }

                if (c.HasChildren)
                {
                    InitForm(c);
                }
            }
        }

        //*****************************软件版权信息*************************************//
        public static string cust_public_id = "";//客户通用id
        public static string cust_private_id = "";//客户加密id
        public static string cust_name = "";//客户加密名称
        public static string real_cust_name = "往来生鲜";//客户解密名称
        public static string ivy_app_id = "96008"; //产品id
        public static string software_name = "线上订货平台";//软件版本号
        public static string software_version = "v_2.0.20190320";//软件版本号
        public static string db_version = "v_1.0.0";//数据版本号
        //*****************************软件版权信息*************************************//

        //导航左侧背景色
        public static Color left_nav1 = Color.FromArgb(58, 110, 165);
        public static Color left_nav2 = Color.FromArgb(30, 119, 206);

        //主窗口顶部导航背景色
        public static Color top_main_nav1 = Color.FromArgb(250, 250, 250);
        public static Color top_main_nav2 = Color.FromArgb(250, 250, 250);
        public static Color top_main_nav_btn = Color.FromArgb(30, 119, 206);


        //字体
        private static Font small_font = new Font("SimSun", 10);
        public static Font middle_font = new Font("SimSun", 11);
        public static Font big_font = new Font("SimSun", 12);
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="fontStyle">1:小字体 2:种字体 3:大字体</param>
        /// <returns></returns>
        public static Font GetFont(int fontSize)
        {
            switch (fontSize)
            {
                default:
                case 1:
                    return small_font;
                case 2:
                    return middle_font;
                case 3:
                    return big_font;
            }
        }
        /// <summary>
        /// 获取字体
        /// </summary>
        /// <param name="fontStyle">1:小字体 2:种字体 3:大字体</param>
        /// <returns></returns>
        public static Font GetFont()
        {
            return middle_font;
        }

        //表格控件表头颜色
        public static Color grid_header1 = Color.FromArgb(244, 248, 249);
        public static Color grid_header2 = Color.FromArgb(240, 240, 240);
        public static Color grid_header_fontcolor = Color.FromArgb(39, 39, 145);
        public static Color grid_header_sort = Color.Gray;

        //表格控件表尾颜色
        public static Color grid_footer1 = Color.FromArgb(240, 240, 240);
        public static Color grid_footer2 = Color.FromArgb(240, 240, 240);

        //表格控件选中颜色
        public static Color grid_sel = Color.FromArgb(191, 217, 249);
        public static Color grid_sel_font = Color.Black;
        //表格控件背景色
        public static Color grid_back = Color.FromArgb(250, 250, 250);

        //树状体背景颜色
        public static Color treeview_back = Color.FromArgb(255, 255, 255);
        public static Color treeview_select_back = Color.WhiteSmoke;

        //下拉选择框背景
        public static Color cb_back = Color.FromArgb(255, 255, 255);
    }
}
