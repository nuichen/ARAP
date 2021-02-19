using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b2bclient.control
{
    class FormEsc
    {
        public static void Bind(System.Windows.Forms.Form frm)
        {
            frm.KeyPreview = true;
            frm.KeyDown += frm_KeyDown;
        }

        public static void frm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            System.Windows.Forms.Form  frm = (System.Windows.Forms.Form)sender;
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                frm.Close();
            }
        }
    }
}
