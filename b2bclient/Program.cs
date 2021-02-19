using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace b2bclient
{
    public static class Program
    {
        public static string oper_id;
        public static string pwd;
        public static string mc_id;
        public static string op_type;
        public static string path;
        public static List<string> lstNewOrder = new List<string>();
        public static List<string> lstNewCus = new List<string>();
        public static Action<string> frmMsg;
        public static Func<string, DialogResult> frmMsgYesNo;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                path = Application.StartupPath;
                //生成文件夹
                if (System.IO.Directory.Exists(path + "\\sound") == false)
                {
                    System.IO.Directory.CreateDirectory(path + "\\sound");
                }
                if (System.IO.Directory.Exists(path + "\\data") == false)
                {
                    System.IO.Directory.CreateDirectory(path + "\\data");
                }
                if (System.IO.Directory.Exists(path + "\\print_file") == false)
                {
                    System.IO.Directory.CreateDirectory(path + "\\print_file");
                }
                if (System.IO.Directory.Exists(path + "\\print_style") == false)
                {
                    System.IO.Directory.CreateDirectory(path + "\\print_style");
                }
                if (System.IO.Directory.Exists(path + "\\logs") == false)
                {
                    System.IO.Directory.CreateDirectory(path + "\\logs");
                }
                //生成文件
                if (System.IO.File.Exists(Program.path + "\\login.txt") == false)
                {
                    System.IO.File.WriteAllText(Program.path + "\\login.txt", "");
                }

                //Program.oper_id = temp_oper_id;
                //Program.pwd = tempPwd;
                //Program.mc_id = mc_id;
                //Program.op_type = oper_type;
                //Application.Run(new frmMain());

            }
            catch (Exception ex)
            {
                LogHelper.writeLog("Program -> Main()", ex.ToString(), null);
                MessageBox.Show("系统错误！Message:" + ex.Message, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
