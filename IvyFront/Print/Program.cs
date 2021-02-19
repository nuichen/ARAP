using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Print
{
    static class Program
    {
        public static string path;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            path = Application.StartupPath;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmPrintStyle frm = new frmPrintStyle("行号,销售单号,客户,操作员,日期,货号,商品名称,规格,单位,数量,单价,金额,合计数量,合计金额,付款方式,实付金额,抹零金额,整单折扣");

            Application.Run(frm);
        }
    }
}
