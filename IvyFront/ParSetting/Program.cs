using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ParSetting
{
    static class Program
    {

        public static string path = "";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            path = Application.StartupPath;
           
            frmCheckPWD frm = new frmCheckPWD();
            if (frm.Check() == true)
            {
                Application.Run(new Form1());
            }
            
        }
    }
}
