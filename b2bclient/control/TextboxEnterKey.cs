using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b2bclient.control
{
    public class TextboxEnterKey
    {
        public static void Bind(System.Windows.Forms.TextBox txt)
        {
            var ins = new TextboxEnterKey();
            txt.KeyDown += ins.txt_keydown;
        }

        void txt_keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.SendWait("{Tab}");
            }
        }

    }
}
