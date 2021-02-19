using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace IvyBack.Helper
{
    class GlobalData
    {
        //软件名
        public static string soft_name = "财务管理系统";

        //版本号
        public static string version = "v2.0.0_20200525";
        public static string db_var = "v2.0.0_20181220";
        public static string ser_var = "v2.0.0_20181220";

        //主窗体
        public static Form mainFrm;
        public static cons.WindowsList windows;
        public static cons.ModuleList modeulelist;
        //导航左侧背景色
        public static Color left_nav1 = Color.FromArgb(58, 110, 165);
        public static Color left_nav2 = Color.FromArgb(30, 119, 206);
        //交替行底色
        //public static Color grid_second = Color.FromArgb(246, 246, 246);
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
            switch (AppSetting.fontSize)
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

        public static void InitForm(Control parentC)
        {
            if (parentC is Form frm)
            {
                frm.Icon = IvyBack.Properties.Resources.erp_logo;

                Type type = frm.GetType();
                var operType = type.GetProperties().SingleOrDefault(d => "method_id".Equals(d.Name));
                if (operType != null)
                {
                    var oper = AppSetting.oper_types.SingleOrDefault(i => i.Value.type_name.Equals(frm.Text));
                    if (oper.Equals(default(KeyValuePair<string, Model.sys_t_oper_type>)))
                    {
                        //MsgForm.ShowFrom("system id error ");
                        //System.Environment.Exit(0);
                    }
                    else
                    {
                        operType.SetValue(frm, oper.Value.type_id, null);
                    }

                }
            }
            foreach (Control c in parentC.Controls)
            {
                if (c is Form) continue;
                //c.Font = Helper.GlobalData.GetFont();

                if (c is ToolStrip)
                {
                    cons.ToolStripClick.AddClick(c);
                }

                if (c is TreeView)
                {
                    c.BackColor = treeview_back;
                }

                if (c is cons.DataGrid || c is cons.EditGrid)
                {
                    c.BackColor = grid_back;
                }

                if (c is ComboBox)
                {
                    c.BackColor = cb_back;
                }

                if (c is Button)
                {
                    Button btn = c as Button;
                    if (btn.Text.Equals("确认"))
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
        //表格控件表头颜色
        //public static Color grid_header1 = Color.FromArgb(53, 103, 175);
        //public static Color grid_header1 = Color.FromArgb(101, 140, 185);
        public static Color grid_header1 = Color.FromArgb(91, 155, 213);
        public static Color grid_header2 = Color.FromArgb(240, 240, 240);
        public static Color grid_header_fontcolor = Color.FromArgb(255, 255, 255);
        public static Color grid_header_sort = Color.Gray;
        //交替行底色
        public static Color grid_second = Color.FromArgb(246, 246, 246);
        //表格控件表尾颜色
        public static Color grid_footer1 = Color.FromArgb(240, 240, 240);
        public static Color grid_footer2 = Color.FromArgb(240, 240, 240);

        //表格控件选中颜色
        public static Color grid_sel = Color.FromArgb(223, 237, 254);
        public static Color grid_sel_font = Color.Black;
        public static Color grid_sel_cell = Color.FromArgb(123, 191, 234);
        //表格控件背景色
        public static Color grid_back = Color.FromArgb(250, 250, 250);

        //树状体背景颜色
        public static Color treeview_back = Color.FromArgb(255, 255, 255);
        public static Color treeview_select_back = Color.WhiteSmoke;

        //下拉选择框背景
        public static Color cb_back = Color.FromArgb(255, 255, 255);
        ////表格控件表头颜色
        //public static Color grid_header1 = Color.FromArgb(244, 248, 249);
        //public static Color grid_header2 = Color.FromArgb(240, 240, 240);
        //public static Color grid_header_fontcolor = Color.FromArgb(39, 39, 145);
        //public static Color grid_header_sort = Color.Gray;

        ////表格控件表尾颜色
        //public static Color grid_footer1 = Color.FromArgb(240, 240, 240);
        //public static Color grid_footer2 = Color.FromArgb(240, 240, 240);

        ////表格控件选中颜色
        //public static Color grid_sel = Color.FromArgb(191, 217, 249);
        //public static Color grid_sel_font = Color.Black;
        //public static Color grid_sel_cell = Color.FromArgb(123, 191, 234);
        ////表格控件背景色
        //public static Color grid_back = Color.FromArgb(250, 250, 250);

        ////树状体背景颜色
        //public static Color treeview_back = Color.FromArgb(255, 255, 255);
        //public static Color treeview_select_back = Color.WhiteSmoke;

        ////下拉选择框背景
        //public static Color cb_back = Color.FromArgb(255, 255, 255);
    }
}
